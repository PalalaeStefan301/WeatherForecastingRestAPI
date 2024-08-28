using DAL.Abstract;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CityRepository : ICityRepository
    {
        private readonly ApiContext _context;
        public CityRepository(ApiContext context) => _context = context;
        public City GetCity(string name)
        {
            return _context.Cities.FirstOrDefault(x => x.Name == name);
        }
        public void AddCity(City city)
        {
            _context.Cities.Add(city);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
