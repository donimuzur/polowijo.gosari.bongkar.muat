﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelEntities : DbContext
    {
        public ModelEntities()
            : base("name=ModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<master_petugas> master_petugas { get; set; }
        public DbSet<master_supplier> master_supplier { get; set; }
        public DbSet<master_item> master_item { get; set; }
        public DbSet<trn_bongkar_muat> trn_bongkar_muat { get; set; }
        public DbSet<trn_bongkat_muat_details_pekerja> trn_bongkat_muat_details_pekerja { get; set; }
        public DbSet<master_transport> master_transport { get; set; }
    }
}
