using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MudTracker.Shared;
using MudTracker.Server.Services.Interfaces;

namespace MudTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<WeatherForecast> Get()
        {
            // Grand Rapids, MI = 42.963, -85.668
            var result = await _weatherService.GetForecast(42.963, -85.668);

            return result;
        }
    }
}
