﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLogic.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class iGymConnectEntities : DbContext
    {
        public iGymConnectEntities()
            : base("name=iGymConnectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<TransactionChild> TransactionChilds { get; set; }
        public virtual DbSet<TransactionMaster> TransactionMasters { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<CheckInMaster> CheckInMasters { get; set; }
        public virtual DbSet<MembershipTypeMaster> MembershipTypeMasters { get; set; }
        public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public virtual DbSet<MemberMaster> MemberMasters { get; set; }
        public virtual DbSet<CardMaster> CardMasters { get; set; }
        public virtual DbSet<POSCardItem> POSCardItems { get; set; }
    }
}
