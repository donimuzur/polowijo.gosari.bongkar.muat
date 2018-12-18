using polowijo.gosari.BusinessObject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.DAL.IServices
{
    public interface ITransportServices
    {
        List<master_transport> GetAll();
        void Save(MasterTransportDto Dto);
        master_transport GetById(int Id);
        void DeleteById(int Id);
    }
}
