using BLL.Abstract;
using BLL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

using WeatherForecastingRestAPI.Models;

namespace WeatherForecastingRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGeocodingService geocodingService;
        private readonly IWeatherService weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                        IGeocodingService geocodingService,
                                        IWeatherService weatherService)
        {
            _logger = logger;
            this.geocodingService = geocodingService;
            this.weatherService = weatherService;
        }

        [HttpGet("{cityName}")]
        [HttpGet("{cityName}/{date}")]
        [HttpGet("{cityName}/{date}/{temperature}")]
        [HttpGet("{cityName}/{date}/{temperature}/{timezone}")]
        [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWeather(
            [SwaggerParameter(Description = "The name of the city. E.g. Rotterdam", Required = true)] string cityName,
            [SwaggerParameter(Description = "Exact day of the weather's data. E.g. 2024-08-26T12:00")] DateTime? date = null,
            [SwaggerParameter(Description = "Celsium or Fahrenheit. Default is Celsius")] EnumTemperatureTypes temperature = EnumTemperatureTypes.Celsius,
            [SwaggerParameter(Description = "Default is Europe_Berlin")] EnumTimezones timezone = EnumTimezones.Europe_Berlin)
        {
            City? city = await geocodingService.GetCityAsync(cityName);

            if (city == null) return NotFound();

            WeatherResponse? response = await weatherService.GetWeatherByCoordsAsync(city!, date??DateTime.Now, temperature, timezone);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }
}
