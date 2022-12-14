using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace WeatherDataProcessor
{
    public static class Function1
    {

        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("weather-forecast-added", "send-email", Connection = "WeatherDataConnection")] string mySbMsg, ILogger logger)
        {
        
            logger.LogInformation($"C# Send EMAIL: {mySbMsg}");

        }

            [FunctionName("updates")]
            public static void Run1([ServiceBusTrigger("weather-forecast-added", "updates", Connection = "WeatherDataConnection")] string mySbMsg, ILogger logger)
            {
               
                logger.LogInformation($"Updating REPORT: {mySbMsg}");
            }
        }
}
