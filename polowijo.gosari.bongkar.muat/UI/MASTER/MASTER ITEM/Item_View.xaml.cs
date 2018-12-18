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

namespace polowijo.gosari.bongkar.muat.UI.MASTER.MASTER_ITEM
{
    /// <summary>
    /// Interaction logic for Item_View.xaml
    /// </summary>
    public partial class Item_View : UserControl
    {
        private IItemServices _itemServices;
        private Window _insertWindow;
        private Window _detailsWindow;
        private ICollectionView _data;
        private IValueConverter _converter;
        Dictionary<string, Predicate<MasterItemDto>> filters
        = new Dictionary<string, Predicate<MasterItemDto>>();
        public Item_View()
        {
            _itemServices = new ItemServices();
            _converter = new EnumDescriptionConverter();

            _insertWindow = new Item_Insert();
            _detailsWindow = new Item_Details(0);

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

            DataGridTextColumn NamaBarang = new DataGridTextColumn();
            DataGridTextColumn JenisBarang = new DataGridTextColumn();
            DataGridTextColumn Harga= new DataGridTextColumn();
            DataGridTextColumn OngkosKontainer = new DataGridTextColumn();
            DataGridTextColumn Status = new DataGridTextColumn();
            DataGridTextColumn Id = new DataGridTextColumn();
            DataGridTextColumn dummy = new DataGridTextColumn();

            Binding StatusBinding = new Binding("STATUS");
            StatusBinding.Converter = new EnumDescriptionConverter();

            Binding JenisBarangBinding = new Binding("JENIS_BARANG");
            JenisBarangBinding.Converter = new EnumDescriptionConverter();

            Binding HargaBinding = new Binding("HARGA");
            HargaBinding.StringFormat = "Rp {0:N}";

            Binding OngkosKontainerBinding = new Binding("ONGKOS_CONTAINER");
            OngkosKontainerBinding.StringFormat = "Rp {0:N}";

            NamaBarang.Binding = new Binding("NAMA_BARANG");
            Status.Binding = StatusBinding;
            JenisBarang.Binding = JenisBarangBinding;
            Harga.Binding = HargaBinding;
            OngkosKontainer.Binding = OngkosKontainerBinding;
            Id.Binding = new Binding("ID");

            NamaBarang.Header = "Nama Barang";
            Status.Header = "Status";
            JenisBarang.Header = "Jenis Barang";
            Harga.Header = "Harga";
            OngkosKontainer.Header = "Ongkos Kontainer";
            Id.Header = "ID";
                        
            Dgv_Home.Columns.Add(NamaBarang);
            Dgv_Home.Columns.Add(JenisBarang);
            Dgv_Home.Columns.Add(Harga);
            Dgv_Home.Columns.Add(OngkosKontainer);
            Dgv_Home.Columns.Add(Status);
            Dgv_Home.Columns.Add(Id);
            Dgv_Home.Columns.Add(dummy);

            NamaBarang.Width = 200;
            JenisBarang.Width = 150;
            Harga.Width = 200;
            OngkosKontainer.Width = 200;
            Status.Width = 100;
            dummy.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            Id.Visibility = Visibility.Hidden;
            #endregion

            PopulateData();
        }
        private void PopulateData()
        {
            var Data = AutoMapper.Mapper.Map<List<MasterItemDto>>(_itemServices.GetAll());
            _data = CollectionViewSource.GetDefaultView(Data);

            _data.GroupDescriptions.Clear();
            _data.GroupDescriptions.Add(new PropertyGroupDescription("JENIS_BARANG"));
            _data.Filter = new Predicate<object>(FilterCandidates);

            Dgv_Home.ItemsSource = _data;
        }
        private bool FilterCandidates(object obj)
        {
            MasterItemDto c = (MasterItemDto)obj;
            return filters.Values
                .Aggregate(true,
                    (prevValue, predicate) => prevValue && predicate(c));
        }
        private void AddFilterAndRefresh(string name, Predicate<MasterItemDto> predicate)
        {
            filters.Add(name, predicate);
            _data.Refresh();
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            filters.Clear();
            AddFilterAndRefresh("NAMA_BARANG", candidate => !string.IsNullOrWhiteSpace(candidate.NAMA_BARANG) && (candidate.NAMA_BARANG == null ? "".Contains(Filter_NamaBarang.Text) : candidate.NAMA_BARANG.ToUpper().Contains(Filter_NamaBarang.Text.ToUpper())));
            try
            {
                var SelectedItem = (ComboBoxItem)Filter_JenisBarang.SelectedItem;
            }
            catch (Exception)
            {
                AddFilterAndRefresh("JENIS_BARANG", candidate => candidate.JENIS_BARANG == (ItemType)Filter_JenisBarang.SelectedItem);
            }
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Filter_NamaBarang.Text = "";
            Filter_JenisBarang.SelectedIndex = 0;
            filters.Clear();
            _data.Refresh();
        }
        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _insertWindow = new Item_Insert();
            var result = _insertWindow.ShowDialog();
            if (result == true)
            {
                PopulateData();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterItemDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                _detailsWindow = new Item_Details(idx.ID);
                var result = _detailsWindow.ShowDialog();
                if (result == true)
                {
                    PopulateData();
                }
            }
        }
        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            var idx = (MasterItemDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                var Result = MessageBox.Show("Apakah and ingin menghapus data ini ?", "Info!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _itemServices.DeleteById(idx.ID);
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

        private void Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Filter_Click(sender, e);
            }
        }
    }
}
