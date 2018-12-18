using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.bongkar.muat.Model.Master
{
    public class PekerjaModel
    {
        public int ID { get; set; }
        public string NAMA_PETUGAS { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }
        public string STATUS_PERKAWINAN { get; set; }
        public string ALAMAT { get; set; }
        public string HANDPHONE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }
}
