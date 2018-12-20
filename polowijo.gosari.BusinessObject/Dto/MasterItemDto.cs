using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
    public class ItemDtoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                MasterItemDto myValue = (MasterItemDto)value;
                return myValue.NAMA_BARANG;
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
