using Ms_configuration.Models;
using ms_recip.Constants;
using ms_recip.Models;
using ms_recip.Repositories.IngredientsRepository;
using ms_recip.Repositories.RecipsRepository;
using ms_recip.Services.ConfigurationService;
using ms_recip.Services.RabbitMqProducerService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ms_recip.Services.RabbitMq;

public class RabbitMqSubscriberService : IHostedService, IDisposable
{
    private readonly IConfigurationService _configurationService;
    private readonly Timer _timer;
    private IModel _channel;
    private IConnection _connection;
    private string _queueName;
    private readonly int _pollingInterval;
    private readonly IRabbitMqProducerService _rabbitMqProducerService;
    private readonly IServiceProvider _serviceProvider;
    private RabbitMqConfigModel? _rabbitMqConfigModel;

    public RabbitMqSubscriberService(
        IConfigurationService configurationService,
        IConfiguration configuration,
        IServiceProvider serviceProvider)
    {
        _configurationService = configurationService;
        _pollingInterval = int.Parse(configuration["RabbitMQ:PollingInterval"] ?? "5000");

        _timer = new Timer(ProcessMessages, null, Timeout.Infinite, _pollingInterval);

        _rabbitMqProducerService = serviceProvider.GetRequiredService<IRabbitMqProducerService>();
        _serviceProvider = serviceProvider;

        _queueName = configuration["RabbitMqQueueName"];
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var rabbitMqConfigResult = await _configurationService.GetRabbitMqConfigAsync();

        if (!rabbitMqConfigResult.IsSuccess)
            return;

        _rabbitMqConfigModel = rabbitMqConfigResult.Value;

        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqConfigModel.Hostname,
            Port = _rabbitMqConfigModel.Port,
            UserName = _rabbitMqConfigModel.Username,
            Password = _rabbitMqConfigModel.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        _timer.Change(0, _pollingInterval);
    }

    private void ProcessMessages(object state)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, basicDeliverEventArgs) =>
        {
            var body = basicDeliverEventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message from {_queueName}: {message}");

            if(RecipActions.Contains(basicDeliverEventArgs.RoutingKey))
                await HandleRecipAsync(message, basicDeliverEventArgs.RoutingKey);
            
            if(IngredientActions.Contains(basicDeliverEventArgs.RoutingKey))
                await HandleIngredientAsync(message, basicDeliverEventArgs.RoutingKey);
        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }

    private string[] RecipActions =
        {
            RabbitmqConstants.CreateRecipRoutingKey,
            RabbitmqConstants.UpdateRecipRoutingKey,
            RabbitmqConstants.DeleteRecipRoutingKey
        };
    
    private string[] IngredientActions =
        {
            RabbitmqConstants.CreateIngredientRoutingKey,
            RabbitmqConstants.UpdateIngredientRoutingKey,
            RabbitmqConstants.DeleteIngredientRoutingKey
        };

    private async Task HandleRecipAsync(string message, string routingKey)
    {
        try
        {
            var deserializedMessage = JsonSerializer.Deserialize<RabbitMqMessageBase<RecipModel>>(message);
            if (deserializedMessage != null)
            {
                using var scope = _serviceProvider.CreateScope();

                var recipsRepository = scope.ServiceProvider.GetRequiredService<IRecipsRepository>();

                MethodResult<RecipModel>? result;

                var routingKeyResult = string.Empty;

                switch (routingKey)
                {
                    case RabbitmqConstants.CreateRecipRoutingKey:
                        {
                            result = await recipsRepository.CreateItemAsync(deserializedMessage.Payload);

                            routingKeyResult = RabbitmqConstants.CreateRecipResultRoutingKey;
                        }
                        break;

                    case RabbitmqConstants.UpdateRecipRoutingKey:
                        {
                            result = await recipsRepository.UpdateItemAsync(deserializedMessage.Payload);

                            routingKeyResult = RabbitmqConstants.UpdateRecipResultRoutingKey;
                        }
                        break;

                    case RabbitmqConstants.DeleteRecipRoutingKey:
                        {
                            var deleteResult = await recipsRepository.DeleteItemAsync(deserializedMessage.Payload);
                            result = new MethodResult<RecipModel>
                            {
                                IsSuccess = deleteResult.IsSuccess,
                                Message = deleteResult.Message,
                                Value = deserializedMessage.Payload
                            };

                            routingKeyResult = RabbitmqConstants.DeleteRecipResultRoutingKey;
                        }
                        break;

                    default:
                        result = MethodResult<RecipModel>.CreateErrorResult("problem");

                        break;
                }

                if (result.IsSuccess)
                {
                    var rabbitMqMessageBase = new RabbitMqMessageBase<RecipModel>()
                    {
                        ApplicationName = deserializedMessage.ApplicationName,
                        Payload = deserializedMessage.Payload,
                        RoutingKey = routingKeyResult,
                        Timestamp = DateTime.UtcNow,
                        UserId = deserializedMessage.UserId
                    };

                    _rabbitMqProducerService.PublishMessage(rabbitMqMessageBase, RabbitmqConstants.RecipExchangeName, routingKeyResult);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du traitement du message: {ex.Message}");
        }
    }

    private async Task HandleIngredientAsync(string message, string routingKey)
    {
        try
        {
            var deserializedMessage = JsonSerializer.Deserialize<RabbitMqMessageBase<IngredientModel>>(message);

            if (deserializedMessage != null)
            {
                using var scope = _serviceProvider.CreateScope();

                var repository = scope.ServiceProvider.GetRequiredService<IIngredientsRepository>();

                MethodResult<IngredientModel>? result;

                var routingKeyResult = string.Empty;

                switch (routingKey)
                {
                    case RabbitmqConstants.CreateIngredientRoutingKey:
                        {
                            result = await repository.CreateItemAsync(deserializedMessage.Payload);

                            routingKeyResult = RabbitmqConstants.CreateIngredientResultRoutingKey;
                        }
                        break;

                    case RabbitmqConstants.UpdateIngredientRoutingKey:
                        {
                            result = await repository.UpdateItemAsync(deserializedMessage.Payload);

                            routingKeyResult = RabbitmqConstants.UpdateIngredientResultRoutingKey;
                        }
                        break;

                    case RabbitmqConstants.DeleteIngredientRoutingKey:
                        {
                            var deleteResult = await repository.DeleteItemAsync(deserializedMessage.Payload);

                            result = new MethodResult<IngredientModel>
                            {
                                IsSuccess = deleteResult.IsSuccess,
                                Message = deleteResult.Message,
                                Value = deserializedMessage.Payload
                            };

                            routingKeyResult = RabbitmqConstants.DeleteRecipResultRoutingKey;
                        }
                        break;

                    default:
                        result = MethodResult<IngredientModel>.CreateErrorResult("problem");

                        break;
                }

                if (result.IsSuccess)
                {
                    var rabbitMqMessageBase = new RabbitMqMessageBase<IngredientModel>()
                    {
                        ApplicationName = deserializedMessage.ApplicationName,
                        Payload = deserializedMessage.Payload,
                        RoutingKey = routingKeyResult,
                        Timestamp = DateTime.UtcNow,
                        UserId = deserializedMessage.UserId
                    };

                    _rabbitMqProducerService.PublishMessage(rabbitMqMessageBase, RabbitmqConstants.RecipExchangeName, routingKeyResult);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors du traitement du message: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);
        _channel?.Close();
        _connection?.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
