using System.Text.Json.Serialization;

namespace react_js.Server.Context
{
    public class WeatherContext
    {
        public WeatherContext(float atmosphere) {
            Atmosphere = atmosphere;
        }

        public float Atmosphere { get; set; }
    }

    public class OpenWeatherContext {
        public partial class WeatherResult
        {
            [JsonPropertyName("cod")]
            public long Cod { get; set; }

            [JsonPropertyName("message")]
            public long Message { get; set; }

            [JsonPropertyName("cnt")]
            public long Cnt { get; set; }

            [JsonPropertyName("list")]
            public WeatherSummary[]? List { get; set; }

            [JsonPropertyName("city")]
            public City? City { get; set; }
        }

        public partial class City
        {
            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("coord")]
            public Coord? Coord { get; set; }

            [JsonPropertyName("country")]
            public string? Country { get; set; }

            [JsonPropertyName("population")]
            public long Population { get; set; }

            [JsonPropertyName("timezone")]
            public long Timezone { get; set; }

            [JsonPropertyName("sunrise")]
            public long Sunrise { get; set; }

            [JsonPropertyName("sunset")]
            public long Sunset { get; set; }
        }

        public partial class Coord
        {
            [JsonPropertyName("lat")]
            public double Lat { get; set; }

            [JsonPropertyName("lon")]
            public double Lon { get; set; }
        }

        public partial class WeatherSummary
        {
            [JsonPropertyName("dt")]
            public long Dt { get; set; }

            [JsonPropertyName("main")]
            public MainClass? Main { get; set; }

            [JsonPropertyName("weather")]
            public Weather[]? Weather { get; set; }

            [JsonPropertyName("clouds")]
            public Clouds? Clouds { get; set; }

            [JsonPropertyName("wind")]
            public Wind? Wind { get; set; }

            [JsonPropertyName("visibility")]
            public long Visibility { get; set; }

            [JsonPropertyName("pop")]
            public double Pop { get; set; }

            [JsonPropertyName("snow")]
            public Snow? Snow { get; set; }

            [JsonPropertyName("sys")]
            public Sys Sys { get; set; }

            [JsonPropertyName("dt_txt")]
            public DateTimeOffset DtTxt { get; set; }
        }

        public partial class Clouds
        {
            [JsonPropertyName("all")]
            public long All { get; set; }
        }

        public partial class MainClass
        {
            [JsonPropertyName("temp")]
            public double Temp { get; set; }

            [JsonPropertyName("feels_like")]
            public double FeelsLike { get; set; }

            [JsonPropertyName("temp_min")]
            public double TempMin { get; set; }

            [JsonPropertyName("temp_max")]
            public double TempMax { get; set; }

            [JsonPropertyName("pressure")]
            public long Pressure { get; set; }

            [JsonPropertyName("sea_level")]
            public long SeaLevel { get; set; }

            [JsonPropertyName("grnd_level")]
            public long GrndLevel { get; set; }

            [JsonPropertyName("humidity")]
            public long Humidity { get; set; }

            [JsonPropertyName("temp_kf")]
            public double TempKf { get; set; }
        }

        public partial class Snow
        {
            [JsonPropertyName("3h")]
            public double The3H { get; set; }
        }

        public partial class Sys
        {
            [JsonPropertyName("pod")]
            public string? Pod { get; set; }
        }

        public partial class Weather
        {
            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("main")]
            public string? Main { get; set; }

            [JsonPropertyName("description")]
            public string? Description { get; set; }

            [JsonPropertyName("icon")]
            public string? Icon { get; set; }
        }

        public partial class Wind
        {
            [JsonPropertyName("speed")]
            public double Speed { get; set; }

            [JsonPropertyName("deg")]
            public long Deg { get; set; }
        }
    }
}
