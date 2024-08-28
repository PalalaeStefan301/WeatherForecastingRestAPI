using BLL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastingRestAPI.Models;

namespace BLL.Abstract
{
    public interface IWeatherService
    {
        Task<WeatherResponse?> GetWeatherByCoordsAsync(City city, DateTime date, EnumDegreeTypes degreeTypes, EnumTimezones enumTimezones);
    }
}
