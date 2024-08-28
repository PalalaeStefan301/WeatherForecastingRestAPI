using BLL.Abstract;
using BLL.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastingRestAPI.Models;
using DAL.Models;

namespace BLL.Concrete
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger<GeocodingService> _logger;
        public WeatherService(ILogger<GeocodingService> logger)
        {
            this._logger = logger;
        }

        public async Task<WeatherResponse?> GetWeatherByCoordsAsync(City city, DateTime date , EnumDegreeTypes degreeTypes, EnumTimezones enumTimezones)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    UriBuilder uriBuilder = new UriBuilder($"https://api.open-meteo.com/v1/forecast");
                    
                    uriBuilder.Query += $"?latitude={city.Latitude}";
                    uriBuilder.Query += $"&longitude={city.Longitude}";
                    uriBuilder.Query += $"&current=temperature_2m&hourly=temperature_2m&timezone={enumTimezones.ToString().Replace("_","/")}";
                    uriBuilder.Query += $"&start_date={date.Year}-{date.ToString("MM")}-{date.ToString("dd")}&end_date={date.Year}-{date.ToString("MM")}-{date.AddDays(1).ToString("dd")}";
                    if(degreeTypes != EnumDegreeTypes.Celsius)
                    {
                        uriBuilder.Query += $"temperature_unit={degreeTypes}";
                    }


                    HttpResponseMessage response = await httpClient.GetAsync(uriBuilder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var responseDict = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                    int indexOfHour = responseDict.RootElement.GetProperty("hourly").GetProperty("time").EnumerateArray().Select((value, index) => new { value, index })
                                                                                                                           .Where(x => x.value.GetDateTime().Date == date.Date && x.value.GetDateTime().Hour == date.Hour)
                                                                                                                           .Select(x => x.index)
                                                                                                                           .FirstOrDefault();
                    return new WeatherResponse()
                    {
                        City = city,
                        CurrentTime = date,
                        Temperature = responseDict.RootElement.GetProperty("hourly").GetProperty("temperature_2m").EnumerateArray().ElementAt(indexOfHour).GetDouble(),
                        Timezone = enumTimezones
                    };
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
