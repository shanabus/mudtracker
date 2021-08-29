using System.Collections.Generic;

namespace MudTracker.Shared
{
    public class ChanceOfMudProbability
    {
        public bool WasCalculated { get; set; }
        
        public decimal Probability { get; set; }

        public string Message { get; set; }

        public List<DailyItem> Daily { get; set; }
    }
}