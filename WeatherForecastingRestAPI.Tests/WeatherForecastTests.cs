using BLL.Abstract;
using BLL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherForecastingRestAPI.Controllers;
using WeatherForecastingRestAPI.Models;
using WeatherForecastingRestAPI.Tests.Utils;

namespace WeatherForecastingRestAPI.Tests
{
    public class WeatherForecastTests
    {
        private readonly WeatherForecastController _controller;
        private readonly Mock<IWeatherService> weatherService;
        private readonly Mock<IGeocodingService> geocodingService;
        private readonly Mock<ILogger<WeatherForecastController>> _logger;

        public WeatherForecastTests()
        {
            this.weatherService = new Mock<IWeatherService>();
            this.geocodingService = new Mock<IGeocodingService>();
            this._logger = new Mock<ILogger<WeatherForecastController>>();

            BeforeAndAfterAttribute.Mocks.Add(weatherService);
            BeforeAndAfterAttribute.Mocks.Add(geocodingService);

            _controller = new WeatherForecastController(_logger.Object, geocodingService.Object, weatherService.Object);
        }

        [Fact]
        [BeforeAndAfter]
        public async void GetWeather_GET_200()
        {
            //Arrange
            string cityName = "Rotterdam";
            City? city = new City()
            { 
                Elevation = 10,
                Latitude = 10,
                Longitude = 10,
                Name = cityName
            };
            WeatherResponse? weatherResponse = new WeatherResponse()
            {
                City = city,
                CurrentTime = DateTime.Now,
                Temperature = 10,
                Timezone = EnumTimezones.Europe_Berlin
            };
            DateTime date = DateTime.Now;

            geocodingService.Setup(x => x.GetCityAsync(cityName))
                .ReturnsAsync(city);

            weatherService.Setup(x => x.GetWeatherByCoordsAsync(city, date, EnumTemperatureTypes.Celsius, EnumTimezones.Europe_Berlin))
                .ReturnsAsync(weatherResponse);

            //Act
            Microsoft.AspNetCore.Mvc.IActionResult result = await _controller.GetWeather(cityName, date);


            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        [BeforeAndAfter]
        public async void GetWeather_GET_404()
        {
            //Arrange
            string cityName = "Rotterdammm";
            City city = new City()
            {
                Elevation = 10,
                Latitude = 10,
                Longitude = 10,
                Name = cityName
            };
            WeatherResponse? weatherResponse = new WeatherResponse()
            {
                City = city,
                CurrentTime = DateTime.Now,
                Temperature = 10,
                Timezone = EnumTimezones.Europe_Berlin
            };
            DateTime date = DateTime.Now;

            weatherService.Setup(x => x.GetWeatherByCoordsAsync(city, date, EnumTemperatureTypes.Celsius, EnumTimezones.Europe_Berlin))
                .ReturnsAsync(weatherResponse);

            //Act
            Microsoft.AspNetCore.Mvc.IActionResult result = await _controller.GetWeather(cityName,date);


            //Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, okResult.StatusCode);
        }
        [Fact]
        [BeforeAndAfter]
        public async void GetWeather_GET_400()
        {
            //Arrange
            string cityName = "Rotterdammm";
            City city = new City()
            {
                Elevation = 10,
                Latitude = 10,
                Longitude = 10,
                Name = cityName
            };
            DateTime date = new DateTime(1,1,1);

            geocodingService.Setup(x => x.GetCityAsync(cityName))
                .ReturnsAsync(city);


            //Act
            Microsoft.AspNetCore.Mvc.IActionResult result = await _controller.GetWeather(cityName, date);


            //Assert
            var okResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, okResult.StatusCode);
        }
    }
}