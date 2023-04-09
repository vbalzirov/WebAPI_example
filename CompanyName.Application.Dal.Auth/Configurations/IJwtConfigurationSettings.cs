namespace CompanyName.Application.Dal.Auth.Configurations
{
    public interface IJwtConfigurationSettings
    {
        string Key { get; set; }

        int TokenTimeToLiveMinutes { get; set; }
    }
}
