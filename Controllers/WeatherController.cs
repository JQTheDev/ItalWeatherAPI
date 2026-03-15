using Microsoft.AspNetCore.Mvc;

namespace ItalWeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //forecast.json added in prefix as both endpoints can be achieved with this param.
        private const string BaseUrl = "https://api.weatherapi.com/v1/forecast.json?";
        private const string apiKey = "c28da1f128494a70bee234532261403";

        // GET: api/<WeatherController>
        [HttpGet]
        public async Task<ActionResult<string>> GetTodaysWeather()
        {
            HttpClient client = new HttpClient();

            var requestResponse = await client.GetAsync($"{BaseUrl}key={apiKey}&q=Paris");
            requestResponse.EnsureSuccessStatusCode();

            var responseAsJson = await requestResponse.Content.ReadAsStringAsync();

            return responseAsJson;
        }

        // GET api/<WeatherController>/5
        [HttpGet("{days}")]
        public async Task<ActionResult<string>> Get(int noOfDays)
        {
            HttpClient client = new HttpClient();

            var requestResponse = await client.GetAsync($"{BaseUrl}key={apiKey}&q=Paris&days={noOfDays}");
            requestResponse.EnsureSuccessStatusCode();

            var responseAsJson = await requestResponse.Content.ReadAsStringAsync();

            return responseAsJson;
        }

    }
}

//include date, time of sunrise, time of sunset, and weather forecast
//for the remaining hours (condition, chance of rain, temperature
//centigrade, wind (mph)).
