using BLL.Abstract;
using DAL.Abstract;
using DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastingRestAPI.Models;

namespace BLL.Concrete
{
    public class GeocodingService : IGeocodingService
    {
        private readonly ILogger<GeocodingService> _logger;
        private readonly ICityRepository cityRepository;

        public GeocodingService(ILogger<GeocodingService> logger,
                                ICityRepository cityRepository)
        {
            this._logger = logger;
            this.cityRepository = cityRepository;
        }

        public async Task<City?> GetCityAsync(string cityName)
        {
            //first step is to check if there's some city already saved in db

            City city = cityRepository.GetCity(cityName);
            if (city != null)
            {
                return city;
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"https://geocoding-api.open-meteo.com/v1/search?name={cityName}&count=1&language=en&format=json");
                    var responseDict = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    City newCity = new City()
                    {
                        Elevation = ((int)responseDict.RootElement.GetProperty("results").EnumerateArray().FirstOrDefault().GetProperty("elevation").GetDouble()),
                        Latitude = responseDict.RootElement.GetProperty("results").EnumerateArray().FirstOrDefault().GetProperty("latitude").GetDouble(),
                        Longitude = responseDict.RootElement.GetProperty("results").EnumerateArray().FirstOrDefault().GetProperty("longitude").GetDouble(),
                        Name = cityName
                    };
                    cityRepository.AddCity(newCity);
                    return newCity;

                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Request error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Request error: {ex.Message}");
                }
            }

            return null;
        }
    }
}
