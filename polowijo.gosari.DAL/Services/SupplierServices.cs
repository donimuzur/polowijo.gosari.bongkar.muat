using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.Services
{
    public class SupplierServices : ISupplierServices
    {
        private ISqlGenericRepository<master_supplier> _masterSupplierRepo;
        private IUnitOfWorks Uow;
        public SupplierServices()
        {
            Uow = new UnitOfWorks();
            _masterSupplierRepo = Uow.GetGenericRepository<master_supplier>();
        }
        public List<master_supplier> GetAll()
        {
            Uow = new UnitOfWorks();
            _masterSupplierRepo = Uow.GetGenericRepository<master_supplier>();
            return _masterSupplierRepo.Get().ToList();
        }
        public master_supplier GetById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterSupplierRepo = Uow.GetGenericRepository<master_supplier>();
            return _masterSupplierRepo.GetByID(Id);
        }
        public void Save(MasterSupplierDto Dto)
        {
            var Db = AutoMapper.Mapper.Map<master_supplier>(Dto);

            Uow = new UnitOfWorks();
            _masterSupplierRepo = Uow.GetGenericRepository<master_supplier>();

            _masterSupplierRepo.InsertOrUpdate(Db);
            Uow.SaveChanges();
        }
        public void DeleteById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterSupplierRepo = Uow.GetGenericRepository<master_supplier>();

            _masterSupplierRepo.Delete(Id);
            Uow.SaveChanges();
        }
    }
}
