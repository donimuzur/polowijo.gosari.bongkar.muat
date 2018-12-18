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

namespace polowijo.gosari.bongkar.muat.UI.HOME
{
    /// <summary>
    /// Interaction logic for Home_View.xaml
    /// </summary>
    public partial class Home_View : UserControl
    {
        private ITRNBongkarMuatServices _trnServices;
        private Window _insertWindow;
        private Window _detailsWindow;
        private ICollectionView _data;
        private IValueConverter _converter;
        Dictionary<string, Predicate<TRNBongkarMuatDto>> filters= new Dictionary<string, Predicate<TRNBongkarMuatDto>>();
        public Home_View()
        {
            _trnServices = new TRNBongkarMuatServices();
            _converter = new EnumDescriptionConverter();

            _insertWindow = new Home_Insert();
            _detailsWindow = new Home_Details(0);

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

            DataGridTextColumn Id = new DataGridTextColumn();
            DataGridTextColumn TanggalKirim = new DataGridTextColumn();
            DataGridTextColumn JenisBarang = new DataGridTextColumn();
            DataGridTextColumn Berangkat = new DataGridTextColumn();
            DataGridTextColumn Sampai = new DataGridTextColumn();
            DataGridTextColumn LamaWaktu = new DataGridTextColumn();
            DataGridTextColumn Pengirim = new DataGridTextColumn();
            DataGridTextColumn Nopol = new DataGridTextColumn();
            DataGridTextColumn Kwantum = new DataGridTextColumn();
            DataGridTextColumn Harga = new DataGridTextColumn();
            DataGridTextColumn NamaBarang = new DataGridTextColumn();
            DataGridTextColumn TotalHarga = new DataGridTextColumn();
            DataGridTextColumn Ongkos = new DataGridTextColumn();
            DataGridTextColumn MuatKontainer = new DataGridTextColumn();
            DataGridTextColumn HargaKontainer = new DataGridTextColumn();
            DataGridTextColumn TotalKontainer = new DataGridTextColumn();
            DataGridTextColumn UangMakan = new DataGridTextColumn();
            DataGridTextColumn JumlahPekerja = new DataGridTextColumn();
            DataGridTextColumn IdBarang = new DataGridTextColumn();
            DataGridTextColumn Kegiatan = new DataGridTextColumn();
            DataGridTextColumn NamaPekerja = new DataGridTextColumn();
            DataGridTextColumn dummy = new DataGridTextColumn();

            Binding TglKirimBinding = new Binding("TANGGAL_KIRIM");
            TglKirimBinding.StringFormat = "dd MMM yyyy";
            
            Binding JenisBarangBinding = new Binding("JENIS_BARANG");
            JenisBarangBinding.Converter =new EnumDescriptionConverter();

            Binding BerangkatBinding = new Binding("BERANGKAT");
            BerangkatBinding.StringFormat = "HH:mm";

            Binding SampaiBinding = new Binding("SAMPAI");
            SampaiBinding.StringFormat = "HH:mm";

            Binding LamaWaktuBinding = new Binding("LAMA_WAKTU");
            LamaWaktuBinding.StringFormat = "HH:mm";

            Binding KwantumBinding = new Binding("KWANTUM");      
            KwantumBinding.StringFormat = "{0:N0}";

            Binding HargaBinding = new Binding("HARGA");
            HargaBinding.StringFormat = "{0:N0}";

            Binding TotalHargaBinding = new Binding("TOTAL_HARGA");
            TotalHargaBinding.StringFormat = "{0:N0}";

            Binding OngkosBinding = new Binding("ONGKOS");
            OngkosBinding.StringFormat = "{0:N0}";

            Binding HargaKontainerBinding = new Binding("HARGA_KONTAINER");
            HargaKontainerBinding.StringFormat = "{0:N0}";

            Binding TotalKontainerBinding = new Binding("TOTAL_KONTAINER");
            TotalKontainerBinding.StringFormat = "{0:N0}";

            Id.Binding = new Binding("ID");
            TanggalKirim.Binding = TglKirimBinding;
            JenisBarang.Binding = JenisBarangBinding;
            Berangkat.Binding = BerangkatBinding;
            Sampai.Binding = SampaiBinding;
            LamaWaktu.Binding = LamaWaktuBinding;
            Pengirim.Binding = new Binding("PENGIRIM");
            Nopol.Binding = new Binding("NOPOL");
            Kwantum.Binding = KwantumBinding;
            Harga.Binding = HargaBinding;
            NamaBarang.Binding = new Binding("NAMA_BARANG");
            TotalHarga.Binding = TotalHargaBinding;
            Ongkos.Binding = OngkosBinding;
            MuatKontainer.Binding = new Binding("MUAT_KONTAINER");
            HargaKontainer.Binding = HargaKontainerBinding;
            TotalKontainer.Binding = TotalKontainerBinding;
            UangMakan.Binding = new Binding("UANG_MAKAN");
            JumlahPekerja.Binding = new Binding("JUMLAH_PEKERJA");
            IdBarang.Binding = new Binding("ID_BARANG");
            Kegiatan.Binding = new Binding("KEGIATAN");
            NamaPekerja.Binding = new Binding("NAMA_PEKERJA");

            Id.Header = "ID";
            TanggalKirim.Header = "TANGGAL KIRIM";
            JenisBarang.Header = "JENIS BARANG";
            Berangkat.Header = "BERANGKAT";
            Sampai.Header = "SAMPAI";
            LamaWaktu.Header = "LAMA WAKTU";
            Pengirim.Header = "SUPLIER/PENGIRIM";
            Nopol.Header = "NOPOL";
            Kwantum.Header = "KWANTUM (KG)";
            Harga.Header = "HARGA PER (KG)";
            NamaBarang.Header = "NAMA BARANG";
            TotalHarga.Header = "ONGKOS TOTAL";
            Ongkos.Header = "ONGKOS PER (ORG)";
            MuatKontainer.Header = "MUAT KONTAINER";
            HargaKontainer.Header = "HARGA KONTAINER";
            TotalKontainer.Header = "TOTAL ONGKOS KONTAINER";
            UangMakan.Header = "UANG MAKAN";
            JumlahPekerja.Header = "JUMLAH PEKERJA";
            IdBarang.Header = "ID BARANGR";
            Kegiatan.Header = "KEGIATAN";
            NamaPekerja.Header = "NAMA TENAGA KERJA";

            Dgv_Home.Columns.Add(TanggalKirim);
            //Dgv_Home.Columns.Add(Kegiatan);
            Dgv_Home.Columns.Add(JenisBarang);
            Dgv_Home.Columns.Add(NamaBarang);
            Dgv_Home.Columns.Add(Berangkat);
            Dgv_Home.Columns.Add(Sampai);
            Dgv_Home.Columns.Add(LamaWaktu);
            Dgv_Home.Columns.Add(NamaPekerja);
            Dgv_Home.Columns.Add(Pengirim);
            Dgv_Home.Columns.Add(Nopol);
            Dgv_Home.Columns.Add(Kwantum);
            Dgv_Home.Columns.Add(Harga);
            Dgv_Home.Columns.Add(TotalHarga);
            Dgv_Home.Columns.Add(Ongkos);
            Dgv_Home.Columns.Add(MuatKontainer);
            Dgv_Home.Columns.Add(HargaKontainer);
            Dgv_Home.Columns.Add(TotalKontainer);
            Dgv_Home.Columns.Add(UangMakan);
            Dgv_Home.Columns.Add(JumlahPekerja);
            Dgv_Home.Columns.Add(IdBarang);
            Dgv_Home.Columns.Add(Id);
            Dgv_Home.Columns.Add(dummy);

            MuatKontainer.Visibility = Visibility.Hidden;
            HargaKontainer.Visibility = Visibility.Hidden;
            TotalKontainer.Visibility = Visibility.Hidden;
            UangMakan.Visibility = Visibility.Hidden;
            JumlahPekerja.Visibility = Visibility.Hidden;
            IdBarang.Visibility = Visibility.Hidden;
            Id.Visibility = Visibility.Hidden;

            //TanggalKirim.Width = 150;
            //NamaBarang.Width = 200;
            //JenisBarang.Width = 150;
            //Harga.Width = 200;
            //Berangkat.Width = 150;
            //Sampai.Width = 150;
            //LamaWaktu.Width = 150;
            //NamaPekerja.Width = 200;
            //Pengirim.Width = 150;
            //Nopol.Width = 100;
            //Kwantum.Width = 150;
            //Harga.Width = 150;
            //TotalHarga.Width = 150;
            //Ongkos.Width = 200;
            //dummy.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            PopulateData();
            #endregion
        }
        private void PopulateData()
        {
            var Data = AutoMapper.Mapper.Map<List<TRNBongkarMuatDto>>(_trnServices.GetAll());
            Data.ForEach(x =>
            {
                x.NAMA_PEKERJA = string.Join(",", x.trn_bongkat_muat_details_pekerja.Select(idx => idx.NAMA_PEKERJA).ToArray());
            });
            _data = CollectionViewSource.GetDefaultView(Data);

            _data.GroupDescriptions.Clear();
            _data.GroupDescriptions.Add(new PropertyGroupDescription("KEGIATAN"));
            _data.Filter = new Predicate<object>(FilterCandidates);

            Dgv_Home.ItemsSource = _data;
        }
        private bool FilterCandidates(object obj)
        {
            TRNBongkarMuatDto c = (TRNBongkarMuatDto)obj;
            return filters.Values
                .Aggregate(true,
                    (prevValue, predicate) => prevValue && predicate(c));
        }
        private void AddFilterAndRefresh(string name, Predicate<TRNBongkarMuatDto> predicate)
        {
            filters.Add(name, predicate);
            _data.Refresh();
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            //filters.Clear();
            //AddFilterAndRefresh("NAMA_BARANG", candidate => !string.IsNullOrWhiteSpace(candidate.NAMA_BARANG) && (candidate.NAMA_BARANG == null ? "".Contains(Filter_NamaBarang.Text) : candidate.NAMA_BARANG.ToUpper().Contains(Filter_NamaBarang.Text.ToUpper())));
            //try
            //{
            //    var SelectedItem = (ComboBoxItem)Filter_JenisBarang.SelectedItem;
            //}
            //catch (Exception)
            //{
            //    AddFilterAndRefresh("JENIS_BARANG", candidate => candidate.JENIS_BARANG == (ItemType)Filter_JenisBarang.SelectedItem);
            //}
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            //Filter_NamaBarang.Text = "";
            //Filter_JenisBarang.SelectedIndex = 0;
            //filters.Clear();
            //_data.Refresh();
        }
        private void Tambah_Click(object sender, RoutedEventArgs e)
        {
            _insertWindow = new Home_Insert();
            var result = _insertWindow.ShowDialog();
            if (result == true)
            {
                PopulateData();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var idx = (TRNBongkarMuatDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                _detailsWindow = new Home_Details(idx.ID);
                var result = _detailsWindow.ShowDialog();
                if (result == true)
                {
                    PopulateData();
                }
            }
        }
        private void Hapus_Click(object sender, RoutedEventArgs e)
        {
            var idx = (TRNBongkarMuatDto)Dgv_Home.SelectedItem;
            if (idx == null)
                MessageBox.Show("Tidak ada data yg dipilih", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                var Result = MessageBox.Show("Apakah and ingin menghapus data ini ?", "Info!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _trnServices.DeleteById(idx.ID);
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
