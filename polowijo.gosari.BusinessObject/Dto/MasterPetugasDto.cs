using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace polowijo.gosari.BusinessObject.Dto
{
    public class MasterPetugasDto
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
    public class PetugasDtoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                MasterPetugasDto myValue = (MasterPetugasDto)value;
                string description = myValue.NAMA_PETUGAS;
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
