using polowijo.gosari.bongkar.muat.Model.Master;
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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS
{
    /// <summary>
    /// Interaction logic for Petugas_Insert.xaml
    /// </summary>
    public partial class Petugas_Insert : Window
    {
        private IPekerjaServices _pekerjaService;
        public Petugas_Insert()
        {
            _pekerjaService = new PekerjaServices();

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
                if (string.IsNullOrEmpty(NamaPetugas.Text))
                {
                    MessageBox.Show("Nama Petugas tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                };

                var Dto = new MasterPetugasDto();
                Dto.NAMA_PETUGAS = NamaPetugas.Text;
                Dto.HANDPHONE = Handphone.Text;
                Dto.FIRST_NAME = FirstName.Text;
                Dto.ALAMAT = Alamat.Text;
                Dto.LAST_NAME = LastName.Text;
                Dto.STATUS = Core.Status.Aktif;

                var GetDataExisting = _pekerjaService.GetAll().Where(x => !string.IsNullOrEmpty(x.NAMA_PETUGAS) && x.NAMA_PETUGAS.ToUpper() == Dto.NAMA_PETUGAS.ToUpper()).FirstOrDefault();
                if (GetDataExisting != null )
                {
                    MessageBox.Show("Nama Petugas sudah ada", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                _pekerjaService.Save(Dto);

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
        private void Handphone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            long i;
            return long.TryParse(str, out i) && i >= 0;
        }
    }
}
