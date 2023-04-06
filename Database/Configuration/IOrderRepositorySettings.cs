namespace CompanyName.Application.Dal.Orders.Configuration
{
    public interface IOrderRepositorySettings
    {
        string ConnectionString { get; }

        bool IsInMemory { get; }

        string DatabaseName { get; }

        int[] array
        {
            get;
        }
    }
}
