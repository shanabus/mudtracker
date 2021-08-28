using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MudTracker.Server.Services.Interfaces;
using MudTracker.Shared;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace MudTracker.Server.Services
{

    public class WeatherService : IWeatherService
    {
        private readonly double FREEZE_POINT = 32;
        
        // the mm of water the soil can absorb before becoming muddy
        private readonly double SOIL_DENSITY_OFFSET_MM = 0;
        
        // the amount of trust given to the accuracy of this forecast
        private readonly decimal METEOROLOGIST_TRUST_FACTOR = 0;

        private HttpClient _client;
        private readonly IOptions<WeatherSettings> _settings;

        public WeatherService(IOptions<WeatherSettings> settings)
        {
            _client = new HttpClient();    

            _settings = settings;
        }

        public async Task<WeatherForecast> GetForecast(double lat = 42.963, double lon = -85.668)
        {
            //var response = await _client.GetByteArrayAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&appid={_settings.Value.ApiKey}");
            var response = await _client.GetFromJsonAsync<WeatherForecast>($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&appid={_settings.Value.ApiKey}");

            if (response != null)
            {
                Console.WriteLine(response.ToString());

                return response;
            } else {
                Console.WriteLine("Open Weather Map returned an empty response");
            }

            return null;
        }

        public ChanceOfMudProbability ChanceOfMud(WeatherForecast forecast, int dayToEvaluate = 3)
        {
            var chanceOfMud = 0;
            var message = "test";
            var secondDay = forecast.Daily[dayToEvaluate - 2];
            var thirdDay = forecast.Daily[dayToEvaluate - 1];

            if (thirdDay.Temp.Min > FREEZE_POINT)
            {
                if (thirdDay.ProbabilityOfPrecipitation > (0 + METEOROLOGIST_TRUST_FACTOR) && thirdDay.Rain > (0 + SOIL_DENSITY_OFFSET_MM))
                {
                    chanceOfMud = 100;
                    message = "rain coming";
                }
                else if (secondDay.ProbabilityOfPrecipitation > (0 + METEOROLOGIST_TRUST_FACTOR) && secondDay.Rain > (0 + SOIL_DENSITY_OFFSET_MM))
                {
                    chanceOfMud = 50;
                    message = "rain the day before";
                }
            }

            return new ChanceOfMudProbability() { Probability = chanceOfMud, Message = message };
        }
    }
}
