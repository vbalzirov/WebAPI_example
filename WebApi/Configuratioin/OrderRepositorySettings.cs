using CompanyName.Application.Dal.Orders.Configuration;

namespace CompanyName.Application.WebApi.OrdersApi.Configuratioin
{
    public class OrderRepositorySettings : IOrderRepositorySettings
    {
        public string ConnectionString { get; set; }

        public bool IsInMemory { get; set; }

        public string DatabaseName { get; set; }

        public int[] array { get; set; }
    }
}

