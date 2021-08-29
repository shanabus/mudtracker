using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudTracker.Server.Services;
using MudTracker.Shared;

namespace Tests
{
    public class WeatherServiceTests
    {
        [TestClass]
        public class ChanceOfMud
        {
            [TestMethod]
            public void Calculates_Day3_When_Temp_Rain_And_Probability_Present()
            {
                var service = new WeatherService(null);

                var forecast = new WeatherForecast() { Daily = new List<DailyItem>() };
                forecast.Daily.Add(new DailyItem()); // day 1
                forecast.Daily.Add(new DailyItem()); // day 2
                var day3 = new DailyItem() { Rain = 0.5, ProbabilityOfPrecipitation = 0.6m, Temp = new Temp() { Min = 35 } };
                forecast.Daily.Add(day3); // day 3

                var result = service.ChanceOfMud(forecast, 3);
                
                Assert.AreEqual(day3.ProbabilityOfPrecipitation * 100, result.Probability);       
                Assert.IsTrue(result.WasCalculated);       
            }

            [TestMethod]
            public void Calculates_Day3_When_Rain_Not_Present()
            {
                var service = new WeatherService(null);

                var forecast = new WeatherForecast() { Daily = new List<DailyItem>() };
                forecast.Daily.Add(new DailyItem()); // day 1
                forecast.Daily.Add(new DailyItem()); // day 2
                forecast.Daily.Add(new DailyItem() { Rain = 0, ProbabilityOfPrecipitation = 0.2m, Temp = new Temp() { Min = 35 } }); // day 3

                var result = service.ChanceOfMud(forecast, 3);
                
                Assert.AreEqual(0, result.Probability);    
                Assert.IsTrue(result.WasCalculated);         
            }

            [TestMethod]
            public void Calculates_Day3_When_Probability_Not_Present()
            {
                var service = new WeatherService(null);

                var forecast = new WeatherForecast() { Daily = new List<DailyItem>() };
                forecast.Daily.Add(new DailyItem()); // day 1
                forecast.Daily.Add(new DailyItem()); // day 2
                forecast.Daily.Add(new DailyItem() { Rain = 5, ProbabilityOfPrecipitation = 0, Temp = new Temp() { Min = 35 } }); // day 3

                var result = service.ChanceOfMud(forecast, 3);
                
                Assert.AreEqual(0, result.Probability);     
                Assert.IsTrue(result.WasCalculated);        
            }

            [TestMethod]
            public void Calculates_Day3_When_Temp_Too_Low()
            {
                var service = new WeatherService(null);

                var forecast = new WeatherForecast() { Daily = new List<DailyItem>() };
                forecast.Daily.Add(new DailyItem()); // skip day 1
                forecast.Daily.Add(new DailyItem()); // skip day 2
                forecast.Daily.Add(new DailyItem() { Rain = 5, ProbabilityOfPrecipitation = 0, Temp = new Temp() { Min = 31 } }); // day 3

                var result = service.ChanceOfMud(forecast, 3);
                
                Assert.AreEqual(0, result.Probability);          
                Assert.IsTrue(result.WasCalculated);   
            }

            [TestMethod]
            public void Calculates_Day2_When_Rain_Present()
            {
                var service = new WeatherService(null);

                var forecast = new WeatherForecast() { Daily = new List<DailyItem>() };
                forecast.Daily.Add(new DailyItem()); // skip day 1
                forecast.Daily.Add(new DailyItem() { Rain = 5, ProbabilityOfPrecipitation = 0.6m, Temp = new Temp() { Min = 35 } }); // day 2
                forecast.Daily.Add(new DailyItem() { Rain = 0, ProbabilityOfPrecipitation = 0m, Temp = new Temp() { Min = 35 } }); // day 3

                var result = service.ChanceOfMud(forecast, 3);
                
                Assert.AreEqual(50, result.Probability);    
                Assert.IsTrue(result.WasCalculated);    
            }

            [TestMethod]
            public void Calculates_Day2_When_Rain_Humidity_And_Wind_Present()
            {
                var service = new WeatherService(null);

                var forecast = new WeatherForecast() { Daily = new List<DailyItem>() };
                forecast.Daily.Add(new DailyItem()); // day 1
                forecast.Daily.Add(new DailyItem() { Rain = 5, ProbabilityOfPrecipitation = 0.6m, Temp = new Temp() { Min = 35 } }); // day 2
                forecast.Daily.Add(new DailyItem() { Rain = 0, ProbabilityOfPrecipitation = 0m, Temp = new Temp() { Min = 35 }, WindSpeed = 5, Humidity = 40 }); // day 3

                var result = service.ChanceOfMud(forecast, 3);
                
                Assert.AreEqual(30, result.Probability);    
                Assert.IsTrue(result.WasCalculated);   
            }
        }        
    }
}
