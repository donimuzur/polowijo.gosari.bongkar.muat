using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.BusinessObject.Dto
{
    public class MasterSupplierDto
    {
        public int ID { get; set; }
        public string NAMA_SUPPLIER { get; set; }
        public string ALAMAT_SUPPLIER { get; set; }
        public Nullable<long> KODE_WILAYAH { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }
    }
}
