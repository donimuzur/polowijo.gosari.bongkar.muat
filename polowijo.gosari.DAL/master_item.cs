//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace polowijo.gosari.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class master_item
    {
        public master_item()
        {
            this.trn_bongkar_muat = new HashSet<trn_bongkar_muat>();
        }
    
        public int ID { get; set; }
        public polowijo.gosari.Core.ItemType JENIS_BARANG { get; set; }
        public string NAMA_BARANG { get; set; }
        public decimal HARGA { get; set; }
        public decimal ONGKOS_CONTAINER { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }
    
        public virtual ICollection<trn_bongkar_muat> trn_bongkar_muat { get; set; }
    }
}