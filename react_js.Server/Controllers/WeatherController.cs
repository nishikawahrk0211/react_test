using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using react_js.Server.Context;
using react_js.Server.Service;
using System.Text.Json;

namespace react_js.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet(Name = "GetWeather")]
        public IEnumerable<WeatherContext> Get()
        {
            return PostAPI().ToArray();
        }

        private List<WeatherContext> PostAPI()
        {
            var service = new WeatherService();
            var response = service.Post();
            var list = new List<WeatherContext>();

            var json = JsonSerializer.Deserialize<OpenWeatherContext>(response);

            if (json == null)
            {
                return list;
            }

            var current = json.current;
            var hourly = json.hourly;
            var daily = json.daily;

            if (current == null || hourly == null || daily == null)
            {
                return list;
            }

            list.Add(new WeatherContext(current.pressure));

            // foreach (var item in hourly)
            // {

            // }

            return list;
        }
    }
}
