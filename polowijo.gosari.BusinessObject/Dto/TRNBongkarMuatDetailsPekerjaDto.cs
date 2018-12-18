using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.BusinessObject.Dto
{
    public class TRNBongkarMuatDetailsPekerjaDto
    {
        public int ID { get; set; }
        public int ID_TRN_BONGKAR_MUAT { get; set; }
        public string NAMA_PEKERJA { get; set; }
        public int ID_PEKERJA { get; set; }

        public TRNBongkarMuatDto trn_bongkar_muat { get; set; }
        public MasterPetugasDto master_petugas { get; set; }
    }
}
