using polowijo.gosari.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.Services
{
    public class UnitOfWorks : IDisposable, IUnitOfWorks
    { 
        public UnitOfWorks()
        {
        }
        private ModelEntities _context = new ModelEntities();
        private bool disposed = false;
        public ISqlGenericRepository<T> GetGenericRepository<T>()
            where T : class
        {
            return new SqlGenericRepository<T>(_context); ;
        }
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }
        public void RevertChanges()
        {
            _context = new ModelEntities();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
