using polowijo.gosari.BusinessObject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface ISupplierServices
    {
        List<master_supplier> GetAll();
        void Save(MasterSupplierDto Dto);
        master_supplier GetById(int Id);
        void DeleteById(int Id);
    }
}
