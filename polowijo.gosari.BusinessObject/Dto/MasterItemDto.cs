using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.BusinessObject.Dto
{
    public class MasterItemDto
    {
        public int ID { get; set; }
        public polowijo.gosari.Core.ItemType JENIS_BARANG { get; set; }
        public string NAMA_BARANG { get; set; }
        public decimal HARGA { get; set; }
        public decimal ONGKOS_CONTAINER { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }
    }
}
