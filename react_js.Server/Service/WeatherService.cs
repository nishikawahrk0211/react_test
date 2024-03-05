using react_js.Server.Context;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace react_js.Server.Service
{
    public class PostData
    {
        public string q {  get; set; }

        public PostData(string city) {
            q = city;
        }
    }

    public class WeatherService
    {
        private readonly string? _apiKey;
        private readonly HttpClient? _httpClient;

        private const string URI = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherService()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");

            _apiKey = apiKey;  // コンストラクタでAPIキーを初期化。
            _httpClient = new HttpClient();  // HttpClientのインスタンスを作成。
        }

        public string Post()
        {
            var result = Task.Run(PostAsync).Result;

            return result;
        }

        public async Task<string> PostAsync() {
            try
            {
                if(_apiKey == null)
                {
                    throw new HttpRequestException("APIKEYがない");
                }

                if (_httpClient == null)
                {
                    throw new HttpRequestException("HttpClientがない");
                }

                var param = JsonSerializer.Serialize(new PostData("tokyo"));
                var content = new StringContent(param, Encoding.UTF8, "application/json");
                var accept = new MediaTypeWithQualityHeaderValue("application/json");

                _httpClient.DefaultRequestHeaders.Accept.Add(accept);
                HttpResponseMessage response = await _httpClient.PostAsync(URI, content);

                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadAsStringAsync(); ;
                    return weatherData;
                }
                else
                {
                    return $"天気データの取得に失敗しました: {response.StatusCode}";  // レスポンスが失敗だった場合のエラーメッセージ。
                }
            }
            catch (HttpRequestException e)
            {
                return $"リクエストエラー: {e.Message}";  // HTTPリクエストの例外処理。
            }

        }

        private string GetRequestUri(string city)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={city},jp&appid={_apiKey}&units=metric&lang=ja";
        }
    }
}
