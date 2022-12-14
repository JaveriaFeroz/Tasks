using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace servicebus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post(WeatherForecast data)
        {
            var message = new WeatherForecastAdded()
            {
                id = Guid.NewGuid(),
                CreatedDateTime = DateTime.UtcNow,
                ForDate = data.Date

            };
            var connectionString = "Endpoint=sb://taskservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Mfm/HZ213g0GVjtuH+dr7toNd7MEGiEaWpYvk8KHzoY=";
            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender("weather-forecast-added");
            var body = JsonSerializer.Serialize(message);
            var sbmessage = new ServiceBusMessage(body);
            sbmessage.ApplicationProperties.Add("Month",data.Date.ToString("MMMM"));
            await sender.SendMessageAsync(sbmessage);
        }
    }
}