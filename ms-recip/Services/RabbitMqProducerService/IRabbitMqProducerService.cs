using ms_recip.Models;

namespace ms_recip.Services.RabbitMqProducerService;

public interface IRabbitMqProducerService
{
    void PublishMessage<T>(RabbitMqMessageBase<T> message, string exchangeName, string routingKey);
}
