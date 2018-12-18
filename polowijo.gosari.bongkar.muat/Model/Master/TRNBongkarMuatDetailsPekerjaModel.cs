using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.bongkar.muat.Model.Master
{
    public class TRNBongkarMuatDetailsPekerjaModel
    {
        public TRNBongkarMuatDetailsPekerjaModel()
        {
            trn_bongkar_muat = new TRNBongkarMuatModel();
            master_petugas = new PekerjaModel();
        }
        public int ID { get; set; }
        public int ID_TRN_BONGKAR_MUAT { get; set; }
        public string NAMA_PEKERJA { get; set; }
        public int ID_PEKERJA { get; set; }

        public TRNBongkarMuatModel trn_bongkar_muat { get; set; }
        public PekerjaModel master_petugas { get; set; }
    }
}
