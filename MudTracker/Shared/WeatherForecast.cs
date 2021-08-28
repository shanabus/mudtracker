using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MudTracker.Shared
{
    public class WeatherForecast
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
        
        [JsonPropertyName("timezone_offset")]
        public int TimezoneOffset { get; set; }
        public Current current { get; set; }
        
        [JsonPropertyName("minutely")]
        public List<MinutelyItem> Minutely { get; set; }
                
        [JsonPropertyName("hourly")]
        public List<HourlyItem> Hourly { get; set; }
                
        [JsonPropertyName("daily")]
        public List<DailyItem> Daily { get; set; }
    }
}
