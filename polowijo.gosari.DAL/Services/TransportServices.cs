using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.Services
{
    public class TransportServices : ITransportServices
    {
        private ISqlGenericRepository<master_transport> _masterItemRepo;
        private IUnitOfWorks Uow;
        public TransportServices()
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_transport>();
        }
        public List<master_transport> GetAll()
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_transport>();
            return _masterItemRepo.Get().ToList();
        }
        public master_transport GetById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_transport>();
            return _masterItemRepo.GetByID(Id);
        }
        public void Save(MasterTransportDto Dto)
        {
            var Db = AutoMapper.Mapper.Map<master_transport>(Dto);

            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_transport>();

            _masterItemRepo.InsertOrUpdate(Db);
            Uow.SaveChanges();
        }
        public void DeleteById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_transport>();

            _masterItemRepo.Delete(Id);
            Uow.SaveChanges();
        }
    }
}
