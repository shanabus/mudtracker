using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MudTracker.Shared
{
    public class HourlyItem
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }

        [JsonPropertyName("weather")]
        public List <WeatherItem> Weather { get; set; }
        
        //public int pop { get; set; }
    }
}
