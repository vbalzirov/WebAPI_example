using CompanyName.Application.Dal.Auth.Configurations;

namespace CompanyName.Application.WebApi.OrdersApi.Configuratioin
{
    public class JwtConfigurationSettings : IJwtConfigurationSettings
    {
        public string Key { get; set; } = null!;

        public int TokenTimeToLiveMinutes { get; set; }
    }
}
