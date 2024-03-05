using react_js.Server.Context;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace react_js.Server.Service
{
    public class CityData
    {
        [JsonPropertyName("q")]
        public string Q { get; set; }

        [JsonPropertyName("appid")]
        public string ApiKey { get; set; }

        public CityData(string city, string apiKey)
        {
            Q = city;
            ApiKey = apiKey;
        }
    }

    public class WeatherData
    {
        [JsonPropertyName("lat")]
        public int Lat { get; set; }

        [JsonPropertyName("lon")]
        public int Lon { get; set; }

        [JsonPropertyName("appid")]
        public string ApiKey { get; set; }

        public WeatherData(int lat, int lon, string apiKey)
        {
            Lat = lat;
            Lon = lon;
            ApiKey = apiKey;
        }
    }

    public class WeatherService
    {
        private readonly string? _apiKey;

        private const string WEATHER_URI = "https://api.openweathermap.org/data/3.0/onecall";
        private const string CITY_URI = "http://api.openweathermap.org/geo/1.0/direct";


        public WeatherService()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY", EnvironmentVariableTarget.Process);
            _apiKey = apiKey;  // コンストラクタでAPIキーを初期化。
        }

        public string Post()
        {
            try
            {
                if (_apiKey == null)
                {
                    throw new HttpRequestException("APIKEYがない");
                }

                var cityData = Task.Run(() => GetCityAsync("tokyo", _apiKey)).Result;
                var result = Task.Run(() => PostAsync(cityData.Lat, cityData.Lon, _apiKey)).Result;

                return result;
            }
            catch
            {
                return "";
            }
        }

        private async Task<string> PostAsync(int lat, int lon, string apiKey)
        {
            try
            {
                var param = JsonSerializer.Serialize(new WeatherData(lat, lon, apiKey));
                var content = new StringContent(param, Encoding.UTF8, "application/json");
                var accept = new MediaTypeWithQualityHeaderValue("application/json");
                var _httpClient = new HttpClient();

                _httpClient.DefaultRequestHeaders.Accept.Add(accept);
                HttpResponseMessage response = await _httpClient.PostAsync(WEATHER_URI, content);

                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadAsStringAsync(); ;
                    return weatherData;
                }
                else
                {
                    throw new HttpRequestException("天気の取得に失敗");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                return "";
            }

        }
        public async Task<WeatherData> GetCityAsync(string cityName, string apiKey)
        {
            try
            {
                var param = JsonSerializer.Serialize(new CityData(cityName, apiKey));
                var content = new StringContent(param, Encoding.UTF8, "application/json");
                var accept = new MediaTypeWithQualityHeaderValue("application/json");
                var _httpClient = new HttpClient();

                _httpClient.DefaultRequestHeaders.Accept.Add(accept);
                HttpResponseMessage response = await _httpClient.PostAsync(CITY_URI, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync(); ;
                    var weatherData = JsonSerializer.Deserialize<WeatherData>(responseString);

                    if (weatherData == null)
                    {
                        throw new HttpRequestException("地域のデータが空");
                    }

                    return weatherData;
                }
                else
                {
                    throw new HttpRequestException("地域の取得に失敗");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
                return new WeatherData(0, 0, apiKey);
            }
            finally
            {
            }
        }
    }
}
