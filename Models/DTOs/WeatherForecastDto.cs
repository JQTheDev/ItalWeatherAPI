namespace ItalWeatherAPI.Models.DTOs
{
    public class WeatherForecastDto
    {
        public double AvgTemp { get; set; }
        public int ChanceOfRain { get; set; }
        public string? Date { get; set; }
        public string? Condition { get; set; }
    }
}
