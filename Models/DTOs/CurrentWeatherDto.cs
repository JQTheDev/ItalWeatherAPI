using ItalWeatherAPI.Controllers;

namespace ItalWeatherAPI.Models.DTOs
{
    public class CurrentWeatherDto
    {
        //Made nullable to avoid warnings even though it will never be null
        public string? SunriseTime { get; set; }
        public string? Date { get; set; }
        public string? SunsetTime { get; set; }
        public List<HourlyForecast>? HourlyForecasts { get; set; }
    }
}
