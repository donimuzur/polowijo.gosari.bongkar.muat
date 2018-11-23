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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS
{
    /// <summary>
    /// Interaction logic for Petugas_Details.xaml
    /// </summary>
    public partial class Petugas_Details : Window
    {
        private IPekerjaServices _pekerjaService;
        public Petugas_Details(decimal Id)
        {
            _pekerjaService = new PekerjaServices();

            var Data =_pekerjaService.GetById(Id);

            InitializeComponent();
                        
            if (Data != null)
            {
                IdPetugas.Text = Data.ID.ToString();
                NamaPetugas.Text = Data.NAMA_PETUGAS;
                Alamat.Text = Data.ALAMAT;
                FirstName.Text = Data.FIRST_NAME;
                LastName.Text = Data.LAST_NAME;
                Handphone.Text = Data.HANDPHONE;
                StatusPetugas.SelectedItem = Data.STATUS;
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
                var Dto = new MasterPetugasDto();
                Dto.NAMA_PETUGAS = NamaPetugas.Text;
                Dto.HANDPHONE = Handphone.Text;
                Dto.FIRST_NAME = FirstName.Text;
                Dto.ALAMAT = Alamat.Text;
                Dto.LAST_NAME = LastName.Text;
                Dto.ID = Convert.ToDecimal(IdPetugas.Text);
                Dto.STATUS = (Status)StatusPetugas.SelectedItem;
                _pekerjaService.Save(Dto);
                MessageBox.Show("Update Data Sukses", "Sukses");
                CloseWin();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Update Data Error", "Error");
            }
            
        }
        private void Btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
        }
    }
}
