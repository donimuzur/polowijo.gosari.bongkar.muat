using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.BusinessObject.Dto
{
    public class MasterTransportDto
    {
        public MasterTransportDto()
        {
            this.trn_bongkar_muat = new List<TRNBongkarMuatDto>();
        }

        public int ID { get; set; }
        public string NO_POLISI { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }

        public virtual List<TRNBongkarMuatDto> trn_bongkar_muat { get; set; }
    }
}
