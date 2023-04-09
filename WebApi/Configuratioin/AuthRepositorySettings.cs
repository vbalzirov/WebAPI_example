using CompanyName.Application.Dal.Auth.Configurations;

namespace CompanyName.Application.WebApi.OrdersApi.Configuratioin
{
    public class AuthRepositorySettings : IAuthRepositorySettings
    {
        public string ConnectionString { get; set; }

        public bool IsInMemory { get; set; }

        public string DatabaseName { get; set; }
    }
}
