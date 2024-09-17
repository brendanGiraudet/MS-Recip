using Ms_configuration.Models;
using ms_recip.Models;

namespace ms_recip.Services.ConfigurationService;

public interface IConfigurationService
{
    Task<MethodResult<RabbitMqConfigModel>> GetRabbitMqConfigAsync();
}
