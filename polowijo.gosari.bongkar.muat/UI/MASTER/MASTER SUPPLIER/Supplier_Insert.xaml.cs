using polowijo.gosari.BusinessObject.Dto;
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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_SUPPLIER
{
    /// <summary>
    /// Interaction logic for Supplier_Insert.xaml
    /// </summary>
    public partial class Supplier_Insert : Window
    {
        private ISupplierServices _supplierServices;
        public Supplier_Insert()
        {
            _supplierServices = new SupplierServices();
            InitializeComponent();
        }
        private void CloseWin()
        {
            DialogResult = true;
            this.Close();
        }
        private void Btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
        }
        private void Btn_Simpan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _supplierServices = new SupplierServices();

                if (string.IsNullOrEmpty(NamaSupplier.Text))
                {
                    MessageBox.Show("Nama Supplier tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var Dto = new MasterSupplierDto();
                Dto.NAMA_SUPPLIER = NamaSupplier.Text;
                Dto.ALAMAT_SUPPLIER = Alamat.Text;
                Dto.STATUS = Core.Status.Aktif;

                var GetDataExisting = _supplierServices.GetAll().Where(x => !string.IsNullOrEmpty(x.NAMA_SUPPLIER) && x.NAMA_SUPPLIER.ToUpper() == Dto.NAMA_SUPPLIER.ToUpper()).FirstOrDefault();
                if (GetDataExisting != null)
                {
                    MessageBox.Show("Nama Supplier sudah ada", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                _supplierServices.Save(Dto);
                MessageBox.Show("Save Data Sukses", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseWin();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Save Data Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
