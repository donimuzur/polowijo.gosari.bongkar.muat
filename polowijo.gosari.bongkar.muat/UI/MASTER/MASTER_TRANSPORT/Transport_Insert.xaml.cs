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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_TRANSPORT
{
    /// <summary>
    /// Interaction logic for Transport_Insert.xaml
    /// </summary>
    public partial class Transport_Insert : Window
    {
        private ITransportServices _transportService;
        public Transport_Insert()
        {
            _transportService = new TransportServices();

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
                _transportService = new TransportServices();

                if(string.IsNullOrEmpty(NoPolisi.Text))
                {
                    MessageBox.Show("No Polisi tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var Dto = new MasterTransportDto();
                Dto.NO_POLISI = NoPolisi.Text.ToUpper();
                Dto.STATUS = Core.Status.Aktif;

                var GetDataExisting = _transportService.GetAll().Where(x => !string.IsNullOrEmpty(x.NO_POLISI) && x.NO_POLISI == Dto.NO_POLISI).FirstOrDefault();
                if (GetDataExisting != null)
                {
                    MessageBox.Show("No Polisi sudah ada", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                _transportService.Save(Dto);
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
