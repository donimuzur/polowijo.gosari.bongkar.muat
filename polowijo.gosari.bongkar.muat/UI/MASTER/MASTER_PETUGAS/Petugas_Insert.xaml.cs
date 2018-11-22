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
                var Dto = new MasterPetugasDto();
                Dto.NAMA_PETUGAS = NamaPetugas.Text;
                Dto.HANDPHONE = Handphone.Text;
                Dto.FIRST_NAME = FirstName.Text;
                Dto.ALAMAT = Alamat.Text;
                Dto.LAST_NAME = LastName.Text;

                _pekerjaService.Save(Dto);
                MessageBox.Show("Save Data Sukses", "Sukses");
                CloseWin();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Save Data Error", "Error");
            }
        }
    }
}
