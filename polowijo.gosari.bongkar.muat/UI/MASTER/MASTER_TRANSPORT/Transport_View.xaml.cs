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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_TRANSPORT
{
    /// <summary>
    /// Interaction logic for Transport_View.xaml
    /// </summary>
    public partial class Transport_View : UserControl
    {
        private ITransportServices _transportServices;
        private Window _insertWindow;
        private Window _detailsWindow;
        private ICollectionView _data;
        Dictionary<string, Predicate<MasterTransportDto>> filters
        = new Dictionary<string, Predicate<MasterTransportDto>>();
        public Transport_View()
        {
            _transportServices = new TransportServices();

            _insertWindow = new Transport_Insert();
            _detailsWindow = new Transport_Details(0);

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

            DataGridTextColumn NoPolisi = new DataGridTextColumn();
            DataGridTextColumn Status = new DataGridTextColumn();
            DataGridTextColumn Id = new DataGridTextColumn();
            DataGridTextColumn dummy = new DataGridTextColumn();

            Binding StatusBinding = new Binding("STATUS");
            StatusBinding.Converter = new EnumDescriptionConverter();

            NoPolisi.Binding = new Binding("NO_POLISI");
            Status.Binding = StatusBinding;
            Id.Binding = new Binding("ID");

            NoPolisi.Header = "No Polisi";
            Status.Header = "Status";
            Id.Header = "ID";

            Dgv_Home.Columns.Add(NoPolisi);
            Dgv_Home.Columns.Add(Status);
            Dgv_Home.Columns.Add(Id);
            Dgv_Home.Columns.Add(dummy);

            NoPolisi.Width = 200;
            Status.Width = 100;
            dummy.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            Id.Visibility = Visibility.Hidden;
            #endregion

            PopulateData();
        }
        private void PopulateData()
        {
            var Data = AutoMapper.Mapper.Map<List<MasterTransportDto>>(_transportServices.GetAll());
            _data = CollectionViewSource.GetDefaultView(Data);

            _data.Filter = new Predicate<object>(FilterCandidates);

            Dgv_Home.ItemsSource = _data;
        }
        private bool FilterCandidates(object obj)
        {
            MasterTransportDto c = (MasterTransportDto)obj;
            return filters.Values
                .Aggregate(true,
                    (prevValue, predicate) => prevValue && predicate(c));
        }
        private void AddFilterAndRefresh(string name, Predicate<MasterTransportDto> predicate)
        {
            filters.Add(name, predicate);
            _data.Refresh();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            filters.Clear();
            AddFilterAndRefresh("NO_POLISI", candidate => !string.IsNullOrWhiteSpace(candidate.NO_POLISI) && (candidate.NO_POLISI == null ? "".Contains(Filter_NoPolisi.Text) : candidate.NO_POLISI.ToUpper().Contains(Filter_NoPolisi.Text.ToUpper())));
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Filter_NoPolisi.Text = "";
            _data.Refresh();
        }
        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _insertWindow = new Transport_Insert();
            var result = _insertWindow.ShowDialog();
            if (result == true)
            {
                PopulateData();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterTransportDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                _detailsWindow = new Transport_Details(idx.ID);
                var result = _detailsWindow.ShowDialog();
                if (result == true)
                {
                    PopulateData();
                }
            }
        }
        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterTransportDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                var Result = MessageBox.Show("Apakah and ingin menghapus data ini ?", "Info!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _transportServices.DeleteById(idx.ID);
                        MessageBox.Show("Sukses Hapus Data", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                        PopulateData();
                    }
                    catch (Exception exp)
                    {
                        var a = exp.Message;
                        MessageBox.Show("Error Hapus Data", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void Filter_NoPolisi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Filter_Click(sender, e);
            }
        }
    }
}
