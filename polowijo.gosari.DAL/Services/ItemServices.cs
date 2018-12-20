using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.Services
{
    public class ItemServices : IItemServices
    {
        private ISqlGenericRepository<master_item> _masterItemRepo;
        private IUnitOfWorks Uow;
        public ItemServices()
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_item>();
        }
        public List<master_item> GetAll()
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_item>();
            return _masterItemRepo.Get().ToList();
        }
        public master_item GetById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_item>();
            return _masterItemRepo.GetByID(Id);
        }
        public void Save(MasterItemDto Dto)
        {
            var Db = AutoMapper.Mapper.Map<master_item>(Dto);

            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_item>();

            _masterItemRepo.InsertOrUpdate(Db);
            Uow.SaveChanges();
        }
        public void DeleteById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterItemRepo = Uow.GetGenericRepository<master_item>();

            _masterItemRepo.Delete(Id);
            Uow.SaveChanges();
        }
    }
}
