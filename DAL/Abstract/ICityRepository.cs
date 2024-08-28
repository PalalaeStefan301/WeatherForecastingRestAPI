using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICityRepository : IDisposable
    {
        City GetCity(string name);
        void AddCity(City city);
    }
}
