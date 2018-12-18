using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.Services
{
    public class PekerjaServices : IPekerjaServices
    {
        private ISqlGenericRepository<master_petugas> _masterPetugasRepo;
        private IUnitOfWorks Uow;
        public PekerjaServices()
        {
            Uow = new UnitOfWorks();
            _masterPetugasRepo = Uow.GetGenericRepository<master_petugas>();
        }
        public List<master_petugas> GetAll()
        {
            Uow = new UnitOfWorks();
            _masterPetugasRepo = Uow.GetGenericRepository<master_petugas>();
            return _masterPetugasRepo.Get().ToList();
        }
        public master_petugas GetById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterPetugasRepo = Uow.GetGenericRepository<master_petugas>();
            return _masterPetugasRepo.GetByID(Id);
        }
        public void Save(MasterPetugasDto Dto)
        {
            var Db = AutoMapper.Mapper.Map<master_petugas>(Dto);

            Uow = new UnitOfWorks();
            _masterPetugasRepo = Uow.GetGenericRepository<master_petugas>();

            _masterPetugasRepo.InsertOrUpdate(Db);
            Uow.SaveChanges();
        }
        public void DeleteById(int Id)
        {
            Uow = new UnitOfWorks();
            _masterPetugasRepo = Uow.GetGenericRepository<master_petugas>();

            _masterPetugasRepo.Delete(Id);
            Uow.SaveChanges();
        }
    }
}
