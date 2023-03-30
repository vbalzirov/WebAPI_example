using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication2.DAL;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly WeatherDbRepository repo;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            repo = new WeatherDbRepository();
        }

        //weather
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(repo.Get());
        }

        //weatherforecast/10
        [HttpGet("{id}", Name = "GetWeatherForecastById")]
        public IActionResult GetById(int id)
        {
            return Ok(repo.Get(id));
        }

        [HttpGet("{id}/temperatureType", Name = "GetTemperatureById")]
        public IActionResult GetTemperatureById(int id, int temperatureType)
        {
            return Ok(36.6d);
        }

        [HttpGet("{id}/date", Name = "GetDateById")]
        public IActionResult GetTemperatureById(int id, string date)
        {
            return Ok(DateTime.Now);
        }

        /// <summary>
        /// Creates Item
        /// </summary>
        /// <param name="forcast"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateForcast(WeatherForecast forcast)
        {
            var result = repo.Create(forcast);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteForcast(int id)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateForcast(int id, WeatherForecast forcast)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}