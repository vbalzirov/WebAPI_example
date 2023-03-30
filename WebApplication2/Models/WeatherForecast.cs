namespace WebApplication2.Models
{
    public class WeatherForecast : WeatherForecastBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public abstract class WeatherForecastBase
    {
        public DateTime Date { get; set; }

        public decimal TemperatureC { get; set; }

        public string? Summary { get; set; }
    }

    public class CreateWeatherForecastRequest : WeatherForecastBase
    {
    }

    public class CreateWeatherForecastResponse : WeatherForecast
    {
    }

    public class UpdateWeatherForecastRespose : WeatherForecast
    { 
    }

    public class UpdateWeatherForecastRequest : WeatherForecast
    {
    }
}