using AutoMapper;
using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.Services
{
    public class TRNBongkarMuatServices : ITRNBongkarMuatServices
    {

        private ISqlGenericRepository<trn_bongkar_muat> _trnBongkarMuatRepo;
        private ISqlGenericRepository<trn_bongkat_muat_details_pekerja> _trnBongkarMuatDetailsPekerjaRepo;

        private IUnitOfWorks Uow;
        public TRNBongkarMuatServices()
        {
            Uow = new UnitOfWorks();
            _trnBongkarMuatRepo = Uow.GetGenericRepository<trn_bongkar_muat>();
            _trnBongkarMuatDetailsPekerjaRepo = Uow.GetGenericRepository<trn_bongkat_muat_details_pekerja>();
        }
        public List<trn_bongkar_muat> GetAll()
        {
            Uow = new UnitOfWorks();
            _trnBongkarMuatRepo = Uow.GetGenericRepository<trn_bongkar_muat>();
            return _trnBongkarMuatRepo.Get(null,null, "trn_bongkat_muat_details_pekerja").ToList();
        }
        public trn_bongkar_muat GetById(int Id)
        {
            Uow = new UnitOfWorks();
            _trnBongkarMuatRepo = Uow.GetGenericRepository<trn_bongkar_muat>();
            return _trnBongkarMuatRepo.Get(x => x.ID == Id).FirstOrDefault();
        }
        //public void Save(TRNBongkarMuatDto Dto)
        //{
        //    var Db = AutoMapper.Mapper.Map<trn_bongkar_muat>(Dto);

        //    Uow = new UnitOfWorks();
        //    _trnBongkarMuatRepo = Uow.GetGenericRepository<trn_bongkar_muat>();

        //    _trnBongkarMuatRepo.InsertOrUpdate(Db);
        //    Uow.SaveChanges();
        //}
        public void DeleteTrnBongkarMuatDetails(int TrnBongkarMuatID)
        {
            var dataToDelete = _trnBongkarMuatDetailsPekerjaRepo.Get(c => c.ID_TRN_BONGKAR_MUAT == TrnBongkarMuatID);
            if (dataToDelete != null)
            {
                foreach (var DeleteItem in dataToDelete.ToList())
                {
                    DeleteItem.trn_bongkar_muat = null;
                    DeleteItem.master_petugas = null;
                    _trnBongkarMuatDetailsPekerjaRepo.Delete(DeleteItem);
                    Uow.SaveChanges();
                }
            }   
        }
        public void Save(TRNBongkarMuatDto item)
        {
            trn_bongkar_muat model;
            
            if (item == null)
            {
                throw new Exception("Invalid Data Entry");
            }
            try
            {
                if (item.ID > 0)
                {
                    //update
                    model = _trnBongkarMuatRepo.Get(c => c.ID == item.ID).FirstOrDefault();

                    if (model == null)
                        throw new Exception("Data Not Found");
                   
                    var oriDto = Mapper.Map<TRNBongkarMuatDto>(model);
                    
                }
                var dbTrnBongkarMuat = Mapper.Map<trn_bongkar_muat>(item);
                
                model = Mapper.Map<trn_bongkar_muat>(item);
                var modelItem = model.trn_bongkat_muat_details_pekerja.ToList();

                try
                {
                    model.master_item = null;
                    model.master_supplier = null;
                    model.master_transport = null;
                    model.trn_bongkat_muat_details_pekerja = null;
                     _trnBongkarMuatRepo.InsertOrUpdate(model);

                    DeleteTrnBongkarMuatDetails(item.ID);
                    foreach (var TrnBongkarMuatDetailsItem in modelItem)
                    {
                        TrnBongkarMuatDetailsItem.ID_TRN_BONGKAR_MUAT = model.ID;
                        _trnBongkarMuatDetailsPekerjaRepo.InsertOrUpdate(TrnBongkarMuatDetailsItem);
                    }
                    Uow.SaveChanges();
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void DeleteById(int Id)
        {
            Uow = new UnitOfWorks();
            _trnBongkarMuatRepo = Uow.GetGenericRepository<trn_bongkar_muat>();

            _trnBongkarMuatRepo.Delete(Id);
            Uow.SaveChanges();
        }
    }
}
