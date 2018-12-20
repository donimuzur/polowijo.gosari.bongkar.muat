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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_SUPPLIER
{
    /// <summary>
    /// Interaction logic for Supplier_Details.xaml
    /// </summary>
    public partial class Supplier_Details : Window
    {
        private ISupplierServices _supplierServices;
        public Supplier_Details(int Id)
        {
            InitializeComponent();

            _supplierServices = new SupplierServices();

            var Data = _supplierServices.GetById(Id);

            InitializeComponent();

            if (Data != null)
            {
                IdSupplier.Text = Data.ID.ToString();
                NamaSupplier.Text = Data.NAMA_SUPPLIER;
                Alamat.Text = Data.ALAMAT_SUPPLIER;
                StatusSupplier.SelectedItem = Data.STATUS;
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
                _supplierServices = new SupplierServices();

                if (string.IsNullOrEmpty(NamaSupplier.Text))
                {
                    MessageBox.Show("Nama Supplier tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var Dto = new MasterSupplierDto();
                Dto.NAMA_SUPPLIER = NamaSupplier.Text;
                Dto.ALAMAT_SUPPLIER = Alamat.Text;
                Dto.ID = Convert.ToInt32(IdSupplier.Text);
                Dto.STATUS = (Status)StatusSupplier.SelectedItem;

                var GetDataExisting = _supplierServices.GetAll().Where(x => !string.IsNullOrEmpty(x.NAMA_SUPPLIER) && x.NAMA_SUPPLIER.ToUpper() == Dto.NAMA_SUPPLIER.ToUpper()).FirstOrDefault();
                if (GetDataExisting != null && GetDataExisting.ID != Dto.ID)
                {
                    MessageBox.Show("Nama Supplier sudah ada", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                _supplierServices.Save(Dto);
                MessageBox.Show("Update Data Sukses", "Sukses",MessageBoxButton.OK,MessageBoxImage.Information);
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
        private void W1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
