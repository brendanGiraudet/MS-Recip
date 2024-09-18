using Ms_configuration.Models;
using ms_recip.Models;
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
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message from {_queueName}: {message}");

            switch (ea.RoutingKey)
            {
                case "CreateRecip":
                    {
                        await CreateRecipAsync(message);
                    }
                    break;
                default:

                    break;
            }

        };

        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }

    private async Task CreateRecipAsync(string message)
    {
        try
        {
            var deserializedMessage = JsonSerializer.Deserialize<RabbitMqMessageBase<RecipModel>>(message);
            if (deserializedMessage != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var recipsRepository = scope.ServiceProvider.GetRequiredService<IRecipsRepository>();

                    var result = await recipsRepository.CreateItemAsync(deserializedMessage.Payload);

                    if (result.IsSuccess)
                    {
                        var routingKey = "CreateRecipResult";
                        var rabbitMqMessageBase = new RabbitMqMessageBase<RecipModel>()
                        {
                            ApplicationName = "ms-recip",
                            Payload = deserializedMessage.Payload,
                            RoutingKey = routingKey,
                            Timestamp = DateTime.UtcNow,
                            UserId = deserializedMessage.UserId
                        };

                        _rabbitMqProducerService.PublishMessage(rabbitMqMessageBase, "recip", routingKey);
                    }
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
