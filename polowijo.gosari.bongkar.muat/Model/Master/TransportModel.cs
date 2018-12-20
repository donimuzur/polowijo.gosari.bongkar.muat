using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.bongkar.muat.Model.Master
{
    public class TransportModel
    {
        public TransportModel()
        {
            this.trn_bongkar_muat = new List<TRNBongkarMuatModel>();
        }

        public int ID { get; set; }
        public string NO_POLISI { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }

        public virtual List<TRNBongkarMuatModel> trn_bongkar_muat { get; set; }
    }
}
