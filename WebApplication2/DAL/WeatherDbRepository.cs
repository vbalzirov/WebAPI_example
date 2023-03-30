using WebApplication2.DAL.Models;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public class WeatherDbRepository
    {
        private static WeatherContext context;

        public WeatherDbRepository()
        { 
            context = new WeatherContext();
        }

        public WeatherForecast Create(WeatherForecast forcast)
        {
            var forcastDal = new WeatherForecastDal 
            {
                Date = forcast.Date,
                Summary = forcast.Summary,
                TemperatureC = forcast.TemperatureC
            };

            var detailsDal = new ForcastDetailesDal
            {
                Description = "Desrption",
            };

            forcastDal.ForcastDetailes.Add(detailsDal);

            context.Add(detailsDal);
            context.Add(forcastDal);

            context.SaveChanges();

            return new WeatherForecast
            {
                Id = forcastDal.Id,
                Date = forcastDal.Date,
                Summary = forcastDal.Summary,
                TemperatureC = forcastDal.TemperatureC
            };
        }

        public IEnumerable<WeatherForecastDal> Get()
        {
            return context.Forcasts.Where(t => !t.IsDeleted).ToList();
        }

        public WeatherForecastDal Get(int id)
        {
            return context.Forcasts
                .Single(t => t.Id == id);
        }

        public void Udate(WeatherForecastDal model)
        {
            var dal = context.Forcasts.Single(t => t.Id == model.Id);
            dal.Date = model.Date;
            dal.TemperatureC = model.TemperatureC;

            context.SaveChanges();
        }
    }
}
