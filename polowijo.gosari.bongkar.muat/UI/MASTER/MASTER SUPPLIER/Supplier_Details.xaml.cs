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
                var Dto = new MasterSupplierDto();
                Dto.NAMA_SUPPLIER = NamaSupplier.Text;
                Dto.ALAMAT_SUPPLIER = Alamat.Text;
                Dto.ID = Convert.ToInt32(IdSupplier.Text);
                Dto.STATUS = (Status)StatusSupplier.SelectedItem;
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
    }
}
