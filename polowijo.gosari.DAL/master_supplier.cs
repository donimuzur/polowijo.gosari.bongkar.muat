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
    
    public partial class master_supplier
    {
        public master_supplier()
        {
            this.trn_bongkar_muat = new HashSet<trn_bongkar_muat>();
        }
    
        public int ID { get; set; }
        public string NAMA_SUPPLIER { get; set; }
        public string ALAMAT_SUPPLIER { get; set; }
        public Nullable<int> KODE_WILAYAH { get; set; }
        public polowijo.gosari.Core.Status STATUS { get; set; }
    
        public virtual ICollection<trn_bongkar_muat> trn_bongkar_muat { get; set; }
    }
}