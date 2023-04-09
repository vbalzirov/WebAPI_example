namespace CompanyName.Application.Core.Configurations
{
    public interface IRepositorySettings
    {
        string ConnectionString { get; }

        bool IsInMemory { get; }

        string DatabaseName { get; }
    }
}
