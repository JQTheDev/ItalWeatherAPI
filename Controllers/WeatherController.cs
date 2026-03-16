using ItalWeatherAPI.Models;
using ItalWeatherAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ItalWeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private const string BaseUrl = "https://api.weatherapi.com/v1/forecast.json?";
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["WeatherApi:ApiKey"]!;
        }

        [HttpGet]
        public async Task<ActionResult<CurrentWeatherDto>> GetTodaysWeather()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}key={_apiKey}&q=London");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponseModel>(
                    json, 
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }); //explicitly defined otherwise swagger test returns null values.

                var currentDay = weatherApiResponse?.Forecast?.ForecastDay?[0];
                //null check on time for warning but should be impossible?
                var remainingHours = currentDay?.Hour?.Where(x => x.Time != null && DateTime.Parse(x.Time) > DateTime.Now);
                var restOfDay = remainingHours?.Select(x => new HourlyForecast
                {
                    Time = x.Time,
                    Condition = x.Condition?.Text ?? "",
                    TemperatureC = x.TempC,
                    WindMph = x.WindMph,
                    ChanceOfRain = x.ChanceOfRain
                }).ToList();

                var todaysWeather = new CurrentWeatherDto
                {
                    Date = currentDay?.Date,
                    SunriseTime = currentDay?.Astro?.Sunrise,
                    SunsetTime = currentDay?.Astro?.Sunset,
                    HourlyForecasts = restOfDay ?? new List<HourlyForecast>(),
                };

                return todaysWeather;
            }
            return StatusCode((int)response.StatusCode, "Weather service unavailable");
        }

        [HttpGet("days/3")]
        public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetThreeDayForecast()
        {

            var response = await _httpClient.GetAsync($"{BaseUrl}key={_apiKey}&q=London&days=3");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiResponseModel>(
                                    json,
                                    new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true
                                    });

                var forecastDays = weatherApiResponse?.Forecast?.ForecastDay;
                //reduces need for several null safety operators in DTO; more readable
                if (forecastDays == null || forecastDays.Count < 3) return StatusCode(500, "Weather service unavailable"); 

                List<WeatherForecastDto> forecastList = new();

                for (int i = 0; i < 3; i++)
                {
                    WeatherForecastDto dailyForecast = new WeatherForecastDto
                    {
                        AvgTemp = forecastDays[i].Day?.AvgTempC ?? 0,
                        ChanceOfRain = forecastDays[i].Day?.DailyChanceOfRain ?? 0,
                        Date = forecastDays[i].Date,
                        Condition = forecastDays[i].Day?.Condition?.Text
                    };

                    forecastList.Add(dailyForecast);
                }

                return forecastList;
            }
            return StatusCode((int)response.StatusCode, "Weather service unavailable");
        }
    }


}

