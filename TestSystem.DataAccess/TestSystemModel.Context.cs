﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestSystem.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ALGORITHM_TYPES> ALGORITHM_TYPES { get; set; }
        public virtual DbSet<ALGORITHMS> ALGORITHMS { get; set; }
        public virtual DbSet<TEST_DATA_SETS> TEST_DATA_SETS { get; set; }
        public virtual DbSet<TEST_RUN_RESULTS> TEST_RUN_RESULTS { get; set; }
        public virtual DbSet<TEST_RUNS> TEST_RUNS { get; set; }
        public virtual DbSet<USER_DETAILS> USER_DETAILS { get; set; }
        public virtual DbSet<USER_LOGIN_ROLES> USER_LOGIN_ROLES { get; set; }
        public virtual DbSet<USER_SAVED_SETTINGS> USER_SAVED_SETTINGS { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
    }
}
