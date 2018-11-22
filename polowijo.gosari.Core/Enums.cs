using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace polowijo.gosari.Core
{
    public enum ActionType
    {
        [Description("Bongkar")]
        Bongkar = 1,
        [Description("Muat")]
        Muat = 2
    }
    public enum ItemType
    {
        [Description("Barang Jadi")]
        BarangJadi = 1,
        [Description("Bahan Baku")]
        BahanBaku = 2
    }
    public enum Status
    {
        [Description("Tidak Aktif")]
        TidakAktif = 0,
        [Description("Aktif")]
        Aktif = 1
    }
    public enum StatusPerkawinan
    {
        [Description("Kawin")]
        kawin,
        [Description("Tidak Kawin")]
        tidakkawin
    }
    public class EnumDescriptionConverter : IValueConverter
    {
        private string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                DescriptionAttribute attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Enum myEnum = (Enum)value;
                string description = GetEnumDescription(myEnum);
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
