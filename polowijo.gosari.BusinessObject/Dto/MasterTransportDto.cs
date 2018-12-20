using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
    public class TransportDtoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                MasterTransportDto myValue = (MasterTransportDto)value;
                string description = myValue.NO_POLISI;
                return description;
            }
            catch (Exception)
            {

                return "Please Select";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
