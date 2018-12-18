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
using System.Windows.Shapes;

namespace polowijo.gosari.bongkar.muat.UI.HOME
{
    /// <summary>
    /// Interaction logic for Home_Details.xaml
    /// </summary>
    public partial class Home_Details : Window
    {
        public IItemServices _itemRepo;
        public IPekerjaServices _pekerjaRepo;
        public ISupplierServices _supplierServices;
        public ITransportServices _transportServices;
        public ITRNBongkarMuatServices _trnServices;

        private ICollectionView _dataBarang;
        private ICollectionView _dataPetugas;
        private ICollectionView _dataSupplier;
        private ICollectionView _dataTransport;

        Dictionary<string, Predicate<MasterItemDto>> filters = new Dictionary<string, Predicate<MasterItemDto>>();
        public Home_Details(int Id)
        {
            _itemRepo = new ItemServices();
            _pekerjaRepo = new PekerjaServices();
            _supplierServices = new SupplierServices();
            _transportServices = new TransportServices();
            _trnServices = new TRNBongkarMuatServices();

            var Data = _trnServices.GetById(Id);

            InitializeComponent();

            PopulateComboboxBarang();
            PopulateComboboxPetugas();
            PopulateComboboxNamaPengirim();
            PopulateComboboxNoPolisi();

            if (Data != null)
            {
                IdHome.Text = Data.ID.ToString();
                Kegiatan.SelectedItem = Data.KEGIATAN;
                
                NamaBarang.Text = Data.NAMA_BARANG;
                JenisBarang.SelectedItem = Data.JENIS_BARANG;
                TglKirim.Text = Data.TANGGAL_KIRIM.ToString("dd MMM yyyy");
                JenisBarang.SelectedItem = Data.JENIS_BARANG;
                NamaBarang.SelectedItem = Data.ID_BARANG;
                Kwantum.Text = string.Format("{0:N0}", Data.KWANTUM.Value);
                Berangkat.Value = Data.BERANGKAT.TimeOfDay;
                Sampai.Value = Data.SAMPAI.TimeOfDay;
                var ListPetugas = Data.trn_bongkat_muat_details_pekerja.Select(x => x.master_petugas).ToList();
                var ListPetugasDto = AutoMapper.Mapper.Map< List<MasterPetugasDto>>(ListPetugas);
                foreach(var pekerja in ListPetugasDto )
                {
                    ListPekerja.Items.Add(pekerja);
                    ListPekerja.DisplayMemberPath = "NAMA_PETUGAS";
                }
            }

            TglKirim.Text = DateTime.Now.ToString("yyyy MMM dd");
            TglKirim.IsReadOnly = true;
        }
        private void CloseWin()
        {
            DialogResult = true;
            this.Close();
        }
        private void PopulateComboboxBarang()
        {
            _itemRepo = new ItemServices();
            var Data = AutoMapper.Mapper.Map<List<MasterItemDto>>(_itemRepo.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataBarang = CollectionViewSource.GetDefaultView(Data);

            _dataBarang.Filter = new Predicate<object>(FilterCandidates);

            NamaBarang.ItemsSource = _dataBarang;
            NamaBarang.SelectedValuePath = "ID";
            NamaBarang.DisplayMemberPath = "NAMA_BARANG";
            NamaBarang.SelectedIndex = 0;
        }
        private void PopulateComboboxPetugas()
        {
            _itemRepo = new ItemServices();
            var Data = AutoMapper.Mapper.Map<List<MasterPetugasDto>>(_pekerjaRepo.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataPetugas = CollectionViewSource.GetDefaultView(Data);

            NamaPetugas.ItemsSource = _dataPetugas;
            NamaPetugas.SelectedValuePath = "ID";
            NamaPetugas.DisplayMemberPath = "NAMA_PETUGAS";
            NamaPetugas.SelectedIndex = 0;
        }
        private void PopulateComboboxNamaPengirim()
        {
            _supplierServices = new SupplierServices();
            var Data = AutoMapper.Mapper.Map<List<MasterSupplierDto>>(_supplierServices.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataSupplier = CollectionViewSource.GetDefaultView(Data);

            NamaPengirim.ItemsSource = _dataSupplier;
            NamaPengirim.SelectedValuePath = "ID";
            NamaPengirim.DisplayMemberPath = "NAMA_SUPPLIER";
            NamaPengirim.SelectedIndex = 0;
        }
        private void PopulateComboboxNoPolisi()
        {
            _transportServices = new TransportServices();
            var Data = AutoMapper.Mapper.Map<List<MasterTransportDto>>(_transportServices.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataTransport = CollectionViewSource.GetDefaultView(Data);

            NoPolisi.ItemsSource = _dataTransport;
            NoPolisi.SelectedValuePath = "ID";
            NoPolisi.DisplayMemberPath = "NO_POLISI";
            NoPolisi.SelectedIndex = 0;
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
            _dataBarang.Refresh();
            NamaBarang.SelectedIndex = 0;
        }
        private void Btn_Batal_Click(object sender, RoutedEventArgs e)
        {
            CloseWin();
        }
        private void Btn_Simpan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Dto = new TRNBongkarMuatDto();
                var Barang = (MasterItemDto)NamaBarang.SelectedItem;
                var Transport = (MasterTransportDto)NoPolisi.SelectedItem;
                var Pengirim = (MasterSupplierDto)NamaPengirim.SelectedItem;

                Dto.ID = int.Parse(IdHome.Text);
                Dto.TANGGAL_KIRIM = DateTime.Parse(TglKirim.Text);
                Dto.BERANGKAT = new DateTime(Dto.TANGGAL_KIRIM.Year, Dto.TANGGAL_KIRIM.Month, Dto.TANGGAL_KIRIM.Day, Berangkat.Value.Hours, Berangkat.Value.Minutes, Berangkat.Value.Seconds);
                Dto.SAMPAI = new DateTime(Dto.TANGGAL_KIRIM.Year, Dto.TANGGAL_KIRIM.Month, Dto.TANGGAL_KIRIM.Day, Sampai.Value.Hours, Sampai.Value.Minutes, Sampai.Value.Seconds);
                Dto.HARGA = Barang.HARGA;
                Dto.HARGA_KONTAINER = Barang.ONGKOS_CONTAINER;
                Dto.ID_BARANG = Barang.ID;
                Dto.ID_PENGIRIM = Pengirim.ID;
                Dto.JENIS_BARANG = (ItemType)JenisBarang.SelectedItem;
                Dto.JUMLAH_PEKERJA = ListPekerja.Items.Count;
                Dto.KEGIATAN = (ActionType)Kegiatan.SelectedItem;
                Dto.KWANTUM = decimal.Parse(Kwantum.Text);

                var LamaWaktu = Dto.SAMPAI.Subtract(Dto.BERANGKAT);
                Dto.LAMA_WAKTU = new DateTime(Dto.TANGGAL_KIRIM.Year, Dto.TANGGAL_KIRIM.Month, Dto.TANGGAL_KIRIM.Day, LamaWaktu.Hours, LamaWaktu.Minutes, LamaWaktu.Seconds);
                Dto.MUAT_KONTAINER = (string.IsNullOrEmpty(MuatContainer.Text) ? 0 : decimal.Parse(MuatContainer.Text));
                Dto.NAMA_BARANG = Barang.NAMA_BARANG;
                Dto.NOPOL = Transport.NO_POLISI;
                Dto.PENGIRIM = Pengirim.NAMA_SUPPLIER;
                Dto.TOTAL_HARGA = Dto.KWANTUM * Dto.HARGA;
                Dto.ONGKOS = Dto.TOTAL_HARGA / (decimal)(ListPekerja.Items.Count == 0 ? 1 : ListPekerja.Items.Count);
                Dto.TOTAL_KONTAINER = Dto.MUAT_KONTAINER * Barang.ONGKOS_CONTAINER;
                Dto.UANG_MAKAN = (string.IsNullOrEmpty(UangMakan.Text) ? 0 : decimal.Parse(UangMakan.Text)); ;
                Dto.ID_NOPOL = Transport.ID;

                var ListPetugas = ListPekerja.Items.Cast<MasterPetugasDto>()
                                 .Select(item => item)
                                 .ToList();

                Dto.trn_bongkat_muat_details_pekerja = AutoMapper.Mapper.Map<List<TRNBongkarMuatDetailsPekerjaDto>>(ListPetugas);
                _trnServices.Save(Dto);
                MessageBox.Show("Save Data Sukses", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseWin();
            }
            catch (Exception exp)
            {
                var a = exp.Message;
                MessageBox.Show("Save Data Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void HargaBarang_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        private void OngkosKontainer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1;
        }
        private void Btn_Tambah_Pekerja_Click(object sender, RoutedEventArgs e)
        {
            var lItem = ListPekerja.Items
                    .Cast<MasterPetugasDto>()
                     .Select(item => item)
                                 .ToList();
            var selectedMasterPetugas = (MasterPetugasDto)NamaPetugas.SelectedItem;
            if (lItem.Where(x => x.ID == selectedMasterPetugas.ID).Count() > 0)
                MessageBox.Show("Pekerja Sudah Ada", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                ListPekerja.Items.Add(NamaPetugas.SelectedItem);
                ListPekerja.DisplayMemberPath = "NAMA_PETUGAS";
            }
        }
        private void Btn_Reset_Pekerja_Click(object sender, RoutedEventArgs e)
        {
            ListPekerja.Items.Clear();
        }
        private void Kwantum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        private void JenisBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filters.Clear();
            if (_dataBarang != null)
                AddFilterAndRefresh("JENIS_BARANG", candidate => (candidate.JENIS_BARANG == (ItemType)JenisBarang.SelectedItem));
        }

        private void MuatContainer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        private void UangMakan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
    }
}
