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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace polowijo.gosari.bongkar.muat.UI.HOME
{
    /// <summary>
    /// Interaction logic for Home_View.xaml
    /// </summary>
    public partial class Home_View : UserControl
    {
        public Home_View()
        {
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
            Dgv_Home.CanUserResizeColumns = false;
            Dgv_Home.CanUserResizeRows = false;
            Dgv_Home.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;

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

            Id.Binding = new Binding("ID");
            TanggalKirim.Binding = new Binding("TANGGAL_KIRIM");
            JenisBarang.Binding = new Binding("JENIS_BARANG");
            Berangkat.Binding = new Binding("BERANGKAT");
            Sampai.Binding = new Binding("SAMPAI");
            LamaWaktu.Binding = new Binding("LAMA_WAKTU");
            Pengirim.Binding = new Binding("PENGIRIM");
            Nopol.Binding = new Binding("NOPOL");
            Kwantum.Binding = new Binding("KWANTUM");
            Harga.Binding = new Binding("HARGA");
            NamaBarang.Binding = new Binding("NAMA_BARANG");
            TotalHarga.Binding = new Binding("TOTAL_HARGA");
            Ongkos.Binding = new Binding("ONGKOS");
            MuatKontainer.Binding = new Binding("MUAT_KONTAINER");
            HargaKontainer.Binding = new Binding("HARGA_KONTAINER");
            TotalKontainer.Binding = new Binding("TOTAL_KONTAINER");
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
            Dgv_Home.Columns.Add(Kegiatan);
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

            MuatKontainer.Visibility = Visibility.Hidden;
            HargaKontainer.Visibility = Visibility.Hidden;
            TotalKontainer.Visibility = Visibility.Hidden;
            UangMakan.Visibility = Visibility.Hidden;
            JumlahPekerja.Visibility = Visibility.Hidden;
            IdBarang.Visibility = Visibility.Hidden;
            Id.Visibility = Visibility.Hidden;
            
            //Dgv_Home.ItemsSource = _pekerjaServices.GetAll();
            #endregion
        }
    }
}
