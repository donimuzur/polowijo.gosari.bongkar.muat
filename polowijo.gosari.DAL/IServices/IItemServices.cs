using polowijo.gosari.BusinessObject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface IItemServices
    {
        List<master_item> GetAll();
        void Save(MasterItemDto Dto);
        master_item GetById(int Id);
        void DeleteById(int Id);
    }
}
