using CompanyName.Application.Core.Configurations;
using CompanyName.Application.Dal.Orders.Configuratioin;

namespace CompanyName.Application.WebApi.OrdersApi.Configuratioin
{
    public class OrderRepositorySettings : IOrderRepositorySettings
    {
        public string ConnectionString { get; set; } = null!;

        public bool IsInMemory { get; set; }

        public string DatabaseName { get; set; } = null!;
    }
}

