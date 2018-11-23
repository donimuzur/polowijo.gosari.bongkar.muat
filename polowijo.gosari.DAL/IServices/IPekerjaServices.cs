using polowijo.gosari.BusinessObject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface IPekerjaServices
    {
        List<master_petugas> GetAll();
        void Save(MasterPetugasDto Dto);
        master_petugas GetById(decimal Id);
        void DeleteById(decimal Id);
    }
}
