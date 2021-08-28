using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MudTracker.Shared
{
    public class DailyItem
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public double moon_phase { get; set; }

        [JsonPropertyName("temp")]
        public Temp Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public FeelsLike FeelsLike { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        
        [JsonPropertyName("wind_deg")]
        public int WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public double WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List <WeatherItem> Weather { get; set; }
        
        [JsonPropertyName("clouds")]
        public int Clouds { get; set; }
        
        [JsonPropertyName("pop")]
        public decimal ProbabilityOfPrecipitation { get; set; }

        [JsonPropertyName("rain")]
        public double Rain { get; set; }
        [JsonPropertyName("uvi")]
        public double Uvi { get; set; }
    }
}
