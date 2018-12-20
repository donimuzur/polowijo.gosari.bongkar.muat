using polowijo.gosari.BusinessObject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface ITRNBongkarMuatServices
    {
        List<trn_bongkar_muat> GetAll();
        void Save(TRNBongkarMuatDto Dto);
        trn_bongkar_muat GetById(int Id);
        void DeleteById(int Id);
    }
}
