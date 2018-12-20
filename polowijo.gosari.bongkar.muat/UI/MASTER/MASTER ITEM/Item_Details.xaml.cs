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
                HargaBarang.Text = string.Format("{0:N2}", Data.HARGA);
                OngkosKontainer.Text = string.Format("{0:N2}", Data.ONGKOS_CONTAINER); 
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
                _itemServices = new ItemServices();

                if (JenisBarang.SelectedItem.GetType() != typeof(ItemType))
                {
                    MessageBox.Show("Jenis Barang harus dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(NamaBarang.Text))
                {
                    MessageBox.Show("Nama Barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(HargaBarang.Text))
                {
                    MessageBox.Show("Harga Barang tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(OngkosKontainer.Text))
                {
                    MessageBox.Show("Ongkos Kontainer tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var Dto = new MasterItemDto();
                Dto.NAMA_BARANG = NamaBarang.Text;
                Dto.HARGA = decimal.Parse(HargaBarang.Text);
                Dto.JENIS_BARANG = (ItemType)JenisBarang.SelectedItem;
                Dto.ONGKOS_CONTAINER = decimal.Parse(OngkosKontainer.Text);
                Dto.STATUS = Core.Status.Aktif;
                Dto.ID =int.Parse(IdBarang.Text);

                var DataExisting = _itemServices.GetAll().Where(x => !string.IsNullOrEmpty(x.NAMA_BARANG) &&
                            x.NAMA_BARANG.ToUpper() == Dto.NAMA_BARANG.ToUpper() && x.JENIS_BARANG == Dto.JENIS_BARANG).FirstOrDefault();

                if (DataExisting != null && DataExisting.ID != Dto.ID)
                {
                    MessageBox.Show("Barang sudah ada di database", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

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
            decimal i;
            return decimal.TryParse(str, out i) && i >= 1;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
