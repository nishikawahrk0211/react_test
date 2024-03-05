using System.Text.Json.Serialization;

namespace react_js.Server.Context
{
    public class WeatherContext
    {
        public WeatherContext(float atmosphere)
        {
            Atmosphere = atmosphere;
        }

        public float Atmosphere { get; set; }
    }

    public class OpenWeatherContext
    {
        [JsonPropertyName("current")]
        public Current? current { get; set; }

        [JsonPropertyName("hourly")]
        public Hourly? hourly { get; set; }

        [JsonPropertyName("daily")]
        public Daily? daily { get; set; }

        public class Current
        {
            [JsonPropertyName("pressure")]
            public float pressure { get; set; }
        }

        public class Hourly
        {
            [JsonPropertyName("pressure")]
            public float pressure { get; set; }
        }

        public class Daily
        {
            [JsonPropertyName("pressure")]
            public float pressure { get; set; }
        }
    }
}
