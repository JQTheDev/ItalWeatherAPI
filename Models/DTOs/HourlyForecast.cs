namespace ItalWeatherAPI.Models.DTOs
{
    public class HourlyForecast
    {
        public string? Time { get; set; }
        public string? Condition { get; set; }
        public double TemperatureC { get; set; }
        public double WindMph { get; set; }
        public int ChanceOfRain { get; set; }
    }
}
