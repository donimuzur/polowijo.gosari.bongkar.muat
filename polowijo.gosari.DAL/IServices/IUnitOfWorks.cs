using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface IUnitOfWorks : IDisposable
    {
        ISqlGenericRepository<T> GetGenericRepository<T>()
           where T : class;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// the dispose method is called automatically by the injector depending on the lifestyle
        /// </summary>
        new
        void Dispose();
        void SaveChanges();
        void RevertChanges();
    }
}
