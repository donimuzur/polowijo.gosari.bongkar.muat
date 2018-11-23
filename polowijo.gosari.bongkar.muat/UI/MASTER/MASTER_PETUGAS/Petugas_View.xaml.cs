using polowijo.gosari.BusinessObject.Dto;
using polowijo.gosari.Core;
using polowijo.gosari.DAL.IServices;
using polowijo.gosari.DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_PETUGAS
{
    /// <summary>
    /// Interaction logic for Petugas_View.xaml
    /// </summary>
    public partial class Petugas_View : UserControl
    {
        private IPekerjaServices _pekerjaServices;
        private Window _insertWindow;
        private Window _detailsWindow;
        private ICollectionView _data;
        Dictionary<string, Predicate<MasterPetugasDto>> filters
        = new Dictionary<string, Predicate<MasterPetugasDto>>();
        public Petugas_View()
        {
            _pekerjaServices = new PekerjaServices();

            _insertWindow = new Petugas_Insert();
            _detailsWindow = new Petugas_Details(0);

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            #region --- DGV Main ---
            //SourceMain.DataSource = ListCustomer;
            //Dgv_Main.DataSource = SourceMain;
            Dgv_Home.CanUserAddRows = false;
            Dgv_Home.CanUserDeleteRows = false;
            Dgv_Home.IsReadOnly = true;
            Dgv_Home.AutoGenerateColumns = false;

            DataGridTextColumn NamaPetugas = new DataGridTextColumn();
            DataGridTextColumn Status = new DataGridTextColumn();
            DataGridTextColumn StatusPerkawinan = new DataGridTextColumn();
            DataGridTextColumn Handphone = new DataGridTextColumn();
            DataGridTextColumn LastName = new DataGridTextColumn();
            DataGridTextColumn FirstName = new DataGridTextColumn();
            DataGridTextColumn Id = new DataGridTextColumn();

            Binding StatusBinding = new Binding("STATUS");
            StatusBinding.Converter = new EnumDescriptionConverter();
       
            NamaPetugas.Binding = new Binding("NAMA_PETUGAS");
            Status.Binding = StatusBinding;
            StatusPerkawinan.Binding = new Binding("STATUS_PERKAWINAN");
            Handphone.Binding = new Binding("HANDPHONE");
            FirstName.Binding = new Binding("FIRST_NAME");
            LastName.Binding = new Binding("LAST_NAME");
            Id.Binding = new Binding("ID");

            NamaPetugas.Header = "Nama Petugas";
            Status.Header = "Status";
            StatusPerkawinan.Header = "Status Perkawinan";
            Handphone.Header = "Handphone";
            FirstName.Header = "First Name";
            LastName.Header = "Last Name";
            Id.Header = "ID";

            Dgv_Home.Columns.Add(NamaPetugas);
            Dgv_Home.Columns.Add(FirstName);
            Dgv_Home.Columns.Add(LastName);
            Dgv_Home.Columns.Add(Handphone);
            Dgv_Home.Columns.Add(StatusPerkawinan);
            Dgv_Home.Columns.Add(Status);
            Dgv_Home.Columns.Add(Id);

            Id.Visibility = Visibility.Hidden;
            StatusPerkawinan.Visibility = Visibility.Hidden;
            #endregion

            PopulateData();
        }
        private void PopulateData()
        {
            var Data = AutoMapper.Mapper.Map<List<MasterPetugasDto>>(_pekerjaServices.GetAll());
            _data = CollectionViewSource.GetDefaultView(Data);

            _data.Filter = new Predicate<object>(FilterCandidates); 
           
            Dgv_Home.ItemsSource = _data;
        }
        private bool FilterCandidates(object obj)
        {
            MasterPetugasDto c = (MasterPetugasDto)obj;
            return filters.Values
                .Aggregate(true,
                    (prevValue, predicate) => prevValue && predicate(c));
        }
        private void AddFilterAndRefresh(string name, Predicate<MasterPetugasDto> predicate)
        {
            filters.Add(name, predicate);
            _data.Refresh();
        }
        
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            filters.Clear();
            AddFilterAndRefresh("NAMA_PETUGAS", candidate => !string.IsNullOrWhiteSpace(candidate.NAMA_PETUGAS) && candidate.NAMA_PETUGAS.Contains(Filter_NamaPetugas.Text));
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Filter_NamaPetugas.Text = "";
            _data.Refresh();
        }
        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _insertWindow = new Petugas_Insert();
            var result = _insertWindow.ShowDialog();
            if (result == true)
            {
                PopulateData();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var idx =(MasterPetugasDto) Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning", MessageBoxButton.OK,MessageBoxImage.Warning);
            else
            {
                _detailsWindow = new Petugas_Details(idx.ID);
                var result = _detailsWindow.ShowDialog();
                if (result == true)
                {
                    PopulateData();
                }
            }
           
        }

        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterPetugasDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning!", MessageBoxButton.OK,MessageBoxImage.Warning);
            else
            {
                var Result = MessageBox.Show("Apakah and ingin menghapus data ini ?", "Info!", MessageBoxButton.YesNo,MessageBoxImage.Information);
                if(Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _pekerjaServices.DeleteById(idx.ID);
                        MessageBox.Show("Sukses Hapus Data", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Error Hapus Data", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
