using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.Core;
using polowijo.gosari.DAL.IServices;
using polowijo.gosari.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_ITEM
{
    /// <summary>
    /// Interaction logic for Item_Details.xaml
    /// </summary>
    public partial class Item_Details : Window
    {
        private IItemServices _itemServices;
        public Item_Details(int Id)
        {
            InitializeComponent();

            _itemServices = new ItemServices();

            var Data = _itemServices.GetById(Id);

            InitializeComponent();

            if (Data != null)
            {
                IdBarang.Text = Data.ID.ToString();
                NamaBarang.Text = Data.NAMA_BARANG;
                JenisBarang.SelectedItem = Data.JENIS_BARANG;
                HargaBarang.Text = Data.HARGA.ToString();
                OngkosKontainer.Text = Data.ONGKOS_CONTAINER.ToString();
                StatusBarang.SelectedItem = Data.STATUS;
            }

        }
        private void CloseWin()
        {
            DialogResult = true;
            this.Close();
        }
        private void Btn_Simpan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Dto = new MasterItemDto();
                Dto.NAMA_BARANG = NamaBarang.Text;
                Dto.HARGA = decimal.Parse(HargaBarang.Text);
                Dto.JENIS_BARANG = (ItemType)JenisBarang.SelectedItem;
                Dto.ONGKOS_CONTAINER = decimal.Parse(OngkosKontainer.Text);
                Dto.STATUS = Core.Status.Aktif;
                Dto.ID =int.Parse(IdBarang.Text);

                _itemServices.Save(Dto);
                MessageBox.Show("Update Data Sukses", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseWin();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Update Data Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
        }
        private void HargaBarang_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        private void OngkosKontainer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1;
        }
    }
}
