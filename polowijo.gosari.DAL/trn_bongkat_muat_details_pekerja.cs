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
    
    public partial class trn_bongkat_muat_details_pekerja
    {
        public int ID { get; set; }
        public int ID_TRN_BONGKAR_MUAT { get; set; }
        public string NAMA_PEKERJA { get; set; }
        public int ID_PEKERJA { get; set; }
    
        public virtual trn_bongkar_muat trn_bongkar_muat { get; set; }
        public virtual master_petugas master_petugas { get; set; }
    }
}
