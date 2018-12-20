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
                var Dto = AutoMapper.Mapper.Map<TRNBongkarMuatDto>(Data);
                IdHome.Text = Dto.ID.ToString();

                TglKirim.Text = Dto.TANGGAL_KIRIM.ToString("dd MMM yyyy");

                Kegiatan.SelectedItem = Dto.KEGIATAN;
                JenisBarang.SelectedItem = Dto.JENIS_BARANG;

                NamaBarang.SelectedValuePath = "ID";
                NamaBarang.SelectedValue = Dto.master_item.ID;

                NamaPengirim.SelectedValuePath = "ID";
                NamaPengirim.SelectedValue = Dto.master_supplier.ID;

                NoPolisi.SelectedValuePath = "ID";
                NoPolisi.SelectedValue = Dto.master_transport.ID;

                Kwantum.Text = string.Format("{0:N0}", Dto.KWANTUM.Value);

                Berangkat.Value = Dto.BERANGKAT.TimeOfDay;
                Sampai.Value = Dto.SAMPAI.TimeOfDay;

                Ongkos.Text = Dto.ONGKOS.ToString();

                Harga.Text = Dto.HARGA.HasValue ? Dto.HARGA.Value.ToString("{0:N2}") : "0" ;
                TotalHarga.Text = Dto.TOTAL_HARGA.HasValue ? Dto.TOTAL_HARGA.Value.ToString("{0:N0}") : "0";
                HargaKontainer.Text = Dto.HARGA_KONTAINER.HasValue ? Dto.HARGA_KONTAINER.Value.ToString("{0:N0}"):"0";
                TotalKontainer.Text = Dto.TOTAL_KONTAINER.HasValue ? Dto.TOTAL_KONTAINER.Value.ToString("{0:N0}") : "0"; 
                
                var ListPetugas = Dto.trn_bongkat_muat_details_pekerja.Select(x => x.master_petugas).ToList();
                var ListPetugasDto = AutoMapper.Mapper.Map< List<MasterPetugasDto>>(ListPetugas);
                foreach(var pekerja in ListPetugasDto )
                {
                    ListPekerja.Items.Add(pekerja);
                    ListPekerja.DisplayMemberPath = "NAMA_PETUGAS";
                }
            }
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

            var TransportCompositeCollection = new CompositeCollection();
            TransportCompositeCollection.Add(new ComboBoxItem() { Content = "Please Select" });
            TransportCompositeCollection.Add(new CollectionContainer() { Collection = _dataBarang });

            NamaBarang.ItemsSource = TransportCompositeCollection;

            filters.Clear();
            if (_dataBarang != null)
                AddFilterAndRefresh("JENIS_BARANG", candidate => (JenisBarang.SelectedItem.GetType() == typeof(ItemType) && candidate.JENIS_BARANG == (ItemType)JenisBarang.SelectedItem));
            //NamaBarang.SelectedValuePath = "ID";
            //NamaBarang.DisplayMemberPath = "NAMA_BARANG";
            NamaBarang.SelectedIndex = 0;
        }
        private void PopulateComboboxPetugas()
        {
            _itemRepo = new ItemServices();
            var Data = AutoMapper.Mapper.Map<List<MasterPetugasDto>>(_pekerjaRepo.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataPetugas = CollectionViewSource.GetDefaultView(Data);

            var TransportCompositeCollection = new CompositeCollection();
            TransportCompositeCollection.Add(new ComboBoxItem() { Content = "Please Select" });
            TransportCompositeCollection.Add(new CollectionContainer() { Collection = _dataPetugas });

            NamaPetugas.ItemsSource = TransportCompositeCollection;
            NamaPetugas.SelectedValuePath = "ID";
            NamaPetugas.SelectedIndex = 0;
        }
        private void PopulateComboboxNamaPengirim()
        {
            _supplierServices = new SupplierServices();
            var Data = AutoMapper.Mapper.Map<List<MasterSupplierDto>>(_supplierServices.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataSupplier = CollectionViewSource.GetDefaultView(Data);

            var TransportCompositeCollection = new CompositeCollection();
            TransportCompositeCollection.Add(new ComboBoxItem() { Content = "Please Select" });
            TransportCompositeCollection.Add(new CollectionContainer() { Collection = _dataSupplier });

            NamaPengirim.ItemsSource = TransportCompositeCollection;
            NamaPengirim.SelectedValuePath = "ID";
            NamaPengirim.SelectedIndex = 0;
        }
        private void PopulateComboboxNoPolisi()
        {
            _transportServices = new TransportServices();
            var Data = AutoMapper.Mapper.Map<List<MasterTransportDto>>(_transportServices.GetAll().Where(x => x.STATUS == Status.Aktif));
            _dataTransport = CollectionViewSource.GetDefaultView(Data);

            var TransportCompositeCollection = new CompositeCollection();
            TransportCompositeCollection.Add(new ComboBoxItem() { Content = "Please Select" });
            TransportCompositeCollection.Add(new CollectionContainer() { Collection = _dataTransport });

            NoPolisi.ItemsSource = TransportCompositeCollection;
            NoPolisi.SelectedValuePath = "ID";
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
                if (Kegiatan.SelectedItem.GetType() != typeof(ActionType))
                {
                    MessageBox.Show("Kegiatan harus dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Kegiatan.Focus();
                    return;
                }

                if (JenisBarang.SelectedItem.GetType() != typeof(ItemType))
                {
                    MessageBox.Show("Jenis Barang harus dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    JenisBarang.Focus();
                    return;
                }

                if (NamaBarang.SelectedItem.GetType() != typeof(MasterItemDto))
                {
                    MessageBox.Show("Nama Barang harus dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NamaBarang.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(Kwantum.Text))
                {
                    MessageBox.Show("Kwantum (KG) tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Kwantum.Focus();
                    return;
                }

                if (ListPekerja.Items.Count == 0)
                {
                    MessageBox.Show("Pekerja tidak boleh kosong", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NamaPetugas.Focus();
                    return;
                }

                if (NoPolisi.SelectedItem.GetType() != typeof(MasterTransportDto))
                {
                    MessageBox.Show("No Polisi harus dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NoPolisi.Focus();
                    return;
                }

                if (NamaPengirim.SelectedItem.GetType() != typeof(MasterSupplierDto))
                {
                    MessageBox.Show("Pengirim harus dipilih", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NamaPengirim.Focus();
                    return;
                }

                if (Berangkat.Value == new TimeSpan(0, 0, 0))
                {
                    MessageBox.Show("Waktu berangkat harus lebih dari jam 00:00:00", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Berangkat.Focus();
                    return;
                }

                if (Sampai.Value == new TimeSpan(0, 0, 0))
                {
                    MessageBox.Show("Waktu sampai harus lebih dari jam 00:00:00", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Sampai.Focus();
                    return;
                }

                if (Sampai.Value <= Berangkat.Value)
                {
                    MessageBox.Show("Waktu sampai harus lebih dari waktu Berangkat", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Sampai.Focus();
                    return;
                }

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

                var LamaWaktu = Dto.SAMPAI.Add(Dto.BERANGKAT.TimeOfDay);
                Dto.LAMA_WAKTU = new DateTime(Dto.TANGGAL_KIRIM.Year, Dto.TANGGAL_KIRIM.Month, Dto.TANGGAL_KIRIM.Day, LamaWaktu.Hour, LamaWaktu.Minute, LamaWaktu.Second);

                Dto.NAMA_BARANG = Barang.NAMA_BARANG;
                Dto.NOPOL = Transport.NO_POLISI;
                Dto.PENGIRIM = Pengirim.NAMA_SUPPLIER;
                Dto.TOTAL_HARGA = Dto.KWANTUM * Dto.HARGA;
                Dto.ONGKOS = Dto.TOTAL_HARGA / ((decimal)ListPekerja.Items.Count);
                Dto.ID_NOPOL = Transport.ID;

                //not mandatory
                if (!string.IsNullOrEmpty(UangMakan.Text))
                {
                    Dto.UANG_MAKAN = decimal.Parse(UangMakan.Text);
                }

                if (!string.IsNullOrEmpty(MuatContainer.Text))
                {
                    Dto.MUAT_KONTAINER = decimal.Parse(MuatContainer.Text);
                    Dto.TOTAL_KONTAINER = Dto.MUAT_KONTAINER * Barang.ONGKOS_CONTAINER;
                }

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
            e.Handled = !IsValidDecimal(((TextBox)sender).Text + e.Text);
        }
        private void OngkosKontainer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidDecimal(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValidDecimal(string str)
        {
            double i;
            return double.TryParse(str, out i) && i >= 1;
        }
        public static bool IsValidInteger(string str)
        {
            Int64 i;
            return Int64.TryParse(str, out i) && i >= 1;
        }
        private void Btn_Tambah_Pekerja_Click(object sender, RoutedEventArgs e)
        {
            var lItem = ListPekerja.Items
                    .Cast<MasterPetugasDto>()
                     .Select(item => item)
                                 .ToList();
            var selectedMasterPetugas = (MasterPetugasDto)NamaPetugas.SelectedItem;
            if (lItem.Where(x => x.ID == selectedMasterPetugas.ID).Count() > 0)
            {
                MessageBox.Show("Pekerja Sudah Ada", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                NamaPetugas.Focus();
                return;
            }

            if (NamaPetugas.SelectedItem.GetType() != typeof(MasterPetugasDto))
            {
                MessageBox.Show("Pilih pekerja terlebih dahulu", "Info", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                NamaPetugas.Focus();
                return;
            }

            if (ListPekerja.Items.Contains(NamaPetugas.SelectedItem))
            {
                MessageBox.Show("Pekerja Sudah Ada", "Info", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                NamaPetugas.Focus();
                return;
            }
            ListPekerja.Items.Add(NamaPetugas.SelectedItem);
            ListPekerja.DisplayMemberPath = "NAMA_PETUGAS";
        }
        private void Btn_Reset_Pekerja_Click(object sender, RoutedEventArgs e)
        {
            ListPekerja.Items.Clear();
            NamaPetugas.SelectedIndex = 0;
        }
        private void Kwantum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidDecimal(((TextBox)sender).Text + e.Text);
        }
        private void JenisBarang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filters.Clear();
            if (_dataBarang != null)
                AddFilterAndRefresh("JENIS_BARANG", candidate => (JenisBarang.SelectedItem.GetType() == typeof(ItemType) && candidate.JENIS_BARANG == (ItemType)JenisBarang.SelectedItem));
        }

        private void MuatContainer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidDecimal(((TextBox)sender).Text + e.Text);
        }

        private void UangMakan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidDecimal(((TextBox)sender).Text + e.Text);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
