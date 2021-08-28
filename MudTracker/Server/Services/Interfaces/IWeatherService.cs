using System.Threading.Tasks;
using MudTracker.Shared;

namespace MudTracker.Server.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetForecast(double lat, double lon);

        ChanceOfMudProbability ChanceOfMud(WeatherForecast forecast, int dayToEvaluate);
    }    
}