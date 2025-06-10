using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MWA.Models;

namespace MWA.Services
{
    public class WeatherService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        public async Task<WeatherResponse?> GetWeatherDataAsync(string city)
        {
            string apiKey = _config["WeatherAPI:Key"];
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            try
            {
                var response = await _http.GetAsync(apiUrl);
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<OpenWeatherResponse>(json);

                if (weatherData == null)
                    return null;

                return new WeatherResponse
                {
                    Temperature = weatherData.Main.Temp,
                    Humidity = weatherData.Main.Humidity,
                    WindSpeed = weatherData.Wind.Speed,
                    Main = weatherData.Weather[0].Main,
                    Description = weatherData.Weather[0].Description
                };
            }
            catch
            {
                return null;
            }
        }
    }
}