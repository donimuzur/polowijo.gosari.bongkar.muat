using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
    public class SupplierDtoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                MasterSupplierDto myValue = (MasterSupplierDto)value;
                string description = myValue.NAMA_SUPPLIER;
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
