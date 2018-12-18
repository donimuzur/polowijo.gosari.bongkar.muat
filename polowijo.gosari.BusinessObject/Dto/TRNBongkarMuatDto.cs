using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polowijo.gosari.BusinessObject.Dto
{
    public class TRNBongkarMuatDto
    {
        public int ID { get; set; }
        public System.DateTime TANGGAL_KIRIM { get; set; }
        public polowijo.gosari.Core.ItemType JENIS_BARANG { get; set; }
        public System.DateTime BERANGKAT { get; set; }
        public System.DateTime SAMPAI { get; set; }
        public System.DateTime LAMA_WAKTU { get; set; }
        public string PENGIRIM { get; set; }
        public string NOPOL { get; set; }
        public Nullable<decimal> KWANTUM { get; set; }
        public Nullable<decimal> HARGA { get; set; }
        public string NAMA_BARANG { get; set; }
        public Nullable<decimal> TOTAL_HARGA { get; set; }
        public Nullable<decimal> ONGKOS { get; set; }
        public Nullable<decimal> MUAT_KONTAINER { get; set; }
        public Nullable<decimal> HARGA_KONTAINER { get; set; }
        public Nullable<decimal> TOTAL_KONTAINER { get; set; }
        public Nullable<decimal> UANG_MAKAN { get; set; }
        public int JUMLAH_PEKERJA { get; set; }
        public int ID_BARANG { get; set; }
        public polowijo.gosari.Core.ActionType KEGIATAN { get; set; }
        public int ID_PENGIRIM { get; set; }
        public int ID_NOPOL { get; set; }
        public string NAMA_PEKERJA { get; set; }

        public List<TRNBongkarMuatDetailsPekerjaDto> trn_bongkat_muat_details_pekerja { get; set; }
        public MasterItemDto master_item { get; set; }
        public MasterSupplierDto master_supplier { get; set; }
        public MasterTransportDto  master_transport { get; set; }
    }
}
