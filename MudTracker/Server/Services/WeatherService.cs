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
            var response = await _client.GetFromJsonAsync<WeatherForecast>($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&appid={_settings.Value.ApiKey}&units=imperial");

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
            var chance = new ChanceOfMudProbability() { WasCalculated = false };
                        
            if (forecast.Daily?.Count < 3)
            {
                chance.Message = "Not enough daily items returned from API";
            }
            else
            {
                var secondDay = forecast.Daily[dayToEvaluate - 2];
                var thirdDay = forecast.Daily[dayToEvaluate - 1];

                // is it warm enough?
                if (thirdDay.Temp.Min > FREEZE_POINT)
                {
                    // does the third day imply wet soil?
                    if (thirdDay.Rain > 0 && thirdDay.ProbabilityOfPrecipitation > 0.5m)
                    {
                        chance.Probability = thirdDay.ProbabilityOfPrecipitation * 100;
                        chance.Message = "Rain is coming, should be muddy";
                    }
                    // does the day before it look wet enough?
                    else if (secondDay.Temp?.Min > FREEZE_POINT && secondDay.Rain > 0 && secondDay.ProbabilityOfPrecipitation > 0)
                    {
                        chance.Probability = 50;
                        chance.Message = "Rain the day before";

                        if (thirdDay.WindSpeed > 0 && thirdDay.Humidity < 50)
                        {
                            chance.Probability = 30;
                            chance.Message += $", but with good wind ({thirdDay.WindSpeed}) and lower humidity ({thirdDay.Humidity * 100} %), you are probably fine.";
                        }
                    }
                }
                else
                {
                    chance.Probability = 0;
                    chance.Message = "If its muddy you could need an ice pick to find it";                    
                }

                chance.WasCalculated = true;
            }            

            return chance;
        }
    }
}
