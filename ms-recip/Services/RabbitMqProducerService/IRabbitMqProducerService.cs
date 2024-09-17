namespace ms_recip.Services.RabbitMqProducerService;

public interface IRabbitMqProducerService
{
    void PublishMessage<T>(T message, string exchangeName, string routingKey);
}
