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

        public List<WeatherContext> PostAPI()
        {
            var service = new WeatherService();
            var response = service.Post();

            var jsonRoot = JsonDocument.Parse(response).RootElement;
            var current = jsonRoot.GetProperty("current.pressure").GetString();
            var hourly = jsonRoot.GetProperty("hourly.pressure").GetString();
            var daily = jsonRoot.GetProperty("daily.pressure").GetString();



            var list = new List<WeatherContext>();

            if(current == null ||  hourly == null || daily == null)
            {
                return list;
            }

            list.Add(new WeatherContext(int.Parse(current ?? "0")));

            foreach (var item in hourly)
            {

            }

            return new List<WeatherContext>();
        }
    }
}
