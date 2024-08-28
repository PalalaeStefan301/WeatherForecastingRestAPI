using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Abstract
{
    public interface IGeocodingService
    {
        Task<City?> GetCityAsync(string cityName);
    }
}
