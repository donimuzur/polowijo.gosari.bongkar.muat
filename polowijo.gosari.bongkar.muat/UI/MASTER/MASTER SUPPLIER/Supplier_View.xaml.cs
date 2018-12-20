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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_SUPPLIER
{
    /// <summary>
    /// Interaction logic for Supplier_View.xaml
    /// </summary>
    public partial class Supplier_View : UserControl
    {
        private ISupplierServices _supplierServices;
        private Window _insertWindow;
        private Window _detailsWindow;
        private ICollectionView _data;
        Dictionary<string, Predicate<MasterSupplierDto>> filters
        = new Dictionary<string, Predicate<MasterSupplierDto>>();
        public Supplier_View()
        {
            _supplierServices = new SupplierServices();

            _insertWindow = new Supplier_Insert();
            _detailsWindow = new Supplier_Details(0);

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

            DataGridTextColumn NamaSupplier = new DataGridTextColumn();
            DataGridTextColumn Alamat = new DataGridTextColumn();
            DataGridTextColumn Status = new DataGridTextColumn();
            DataGridTextColumn KodeWilayah = new DataGridTextColumn();
            DataGridTextColumn Id = new DataGridTextColumn();
            DataGridTextColumn dummy = new DataGridTextColumn();

            Binding StatusBinding = new Binding("STATUS");
            StatusBinding.Converter = new EnumDescriptionConverter();

            NamaSupplier.Binding = new Binding("NAMA_SUPPLIER");
            Status.Binding = StatusBinding;
            Alamat.Binding = new Binding("ALAMAT_SUPPLIER");
            KodeWilayah.Binding = new Binding("KODE_WILAYAH");
            Id.Binding = new Binding("ID");

            NamaSupplier.Header = "Nama Supplier";
            Status.Header = "Status";
            Alamat.Header = "Alamat";
            KodeWilayah.Header = "First Name";
            Id.Header = "ID";
            
            Dgv_Home.Columns.Add(NamaSupplier);
            Dgv_Home.Columns.Add(Alamat);
            Dgv_Home.Columns.Add(Status);
            Dgv_Home.Columns.Add(KodeWilayah);
            Dgv_Home.Columns.Add(Id);
            Dgv_Home.Columns.Add(dummy);

            NamaSupplier.Width = 200;
            Alamat.MinWidth =500;
            Alamat.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToCells);
            Status.Width = 200;
            dummy.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            Id.Visibility = Visibility.Hidden;
            KodeWilayah.Visibility = Visibility.Hidden;
            #endregion

            PopulateData();
        }
        private void PopulateData()
        {
            var Data = AutoMapper.Mapper.Map<List<MasterSupplierDto>>(_supplierServices.GetAll());
            _data = CollectionViewSource.GetDefaultView(Data);

            _data.Filter = new Predicate<object>(FilterCandidates);

            Dgv_Home.ItemsSource = _data;
        }
        private bool FilterCandidates(object obj)
        {
            MasterSupplierDto c = (MasterSupplierDto)obj;
            return filters.Values
                .Aggregate(true,
                    (prevValue, predicate) => prevValue && predicate(c));
        }
        private void AddFilterAndRefresh(string name, Predicate<MasterSupplierDto> predicate)
        {
            filters.Add(name, predicate);
            _data.Refresh();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            filters.Clear();
            AddFilterAndRefresh("NAMA_SUPPLIER", candidate => !string.IsNullOrWhiteSpace(candidate.NAMA_SUPPLIER) && (candidate.NAMA_SUPPLIER == null ? "".Contains(Filter_NamaSupplier.Text) :candidate.NAMA_SUPPLIER.ToUpper().Contains(Filter_NamaSupplier.Text.ToUpper())));
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Filter_NamaSupplier.Text = "";
            _data.Refresh();
        }
        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _insertWindow = new Supplier_Insert();
            var result = _insertWindow.ShowDialog();
            if (result == true)
            {
                PopulateData();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterSupplierDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                _detailsWindow = new Supplier_Details(idx.ID);
                var result = _detailsWindow.ShowDialog();
                if (result == true)
                {
                    PopulateData();
                }
            }
        }
        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterSupplierDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                var Result = MessageBox.Show("Apakah and ingin menghapus data ini ?", "Info!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _supplierServices.DeleteById(idx.ID);
                        MessageBox.Show("Sukses Hapus Data", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                        PopulateData();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Error Hapus Data", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Filter_NamaSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Filter_Click(sender, e);
            }
        }

        private void Dgv_Home_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var idx = (MasterSupplierDto)Dgv_Home.SelectedItem;
            if (idx != null)
            {
                _detailsWindow = new Supplier_Details(idx.ID);
                var result = _detailsWindow.ShowDialog();
                if (result == true)
                {
                    PopulateData();
                }
            }
        }
    }
}
