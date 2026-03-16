using System.Text.Json.Serialization;

namespace ItalWeatherAPI.Models
{
    public class WeatherApiResponseModel
    {
        public Current? Current { get; set; }
        public Forecast? Forecast { get; set; }
    }

    public class Current
    {
        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }

        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }

        public Condition? Condition { get; set; }
    }

    public class Forecast
    {
        public List<ForecastDay>? ForecastDay { get; set; }
    }

    public class Condition
    {
        public string? Text { get; set; }
    }

    public class ForecastDay
    {
        public string? Date { get; set; }
        public Day? Day { get; set; }
        public Astro? Astro { get; set; }
        public List<Hour>? Hour { get; set; }

    }

    public class Day
    {
        [JsonPropertyName("avgtemp_c")]
        public double AvgTempC { get; set; }

        [JsonPropertyName("daily_chance_of_rain")]
        public int DailyChanceOfRain { get; set; }

        public Condition? Condition { get; set; }
    }

    public class Astro
    {
        public string? Sunrise { get; set; }
        public string? Sunset { get; set; }
    }

    public class Hour
    {
        public string? Time { get; set; }

        [JsonPropertyName("temp_c")]
        public double TempC { get; set; }

        [JsonPropertyName("chance_of_rain")]
        public int ChanceOfRain { get; set; }

        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }

        public Condition? Condition { get; set; }
    }
}
