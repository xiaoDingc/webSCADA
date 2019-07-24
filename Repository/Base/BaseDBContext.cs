// <copyright file="BaseDBContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Repository.Base
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Model;

    public class BaseDBContext : DbContext
    {
        public BaseDBContext() : base("name=BaseDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
       
        public DbSet<OpcHelperItem> OpcHelperItem { get; set; }
    }
    public class BaseDBContextSlave : DbContext
    {
        public BaseDBContextSlave() : base("name=BaseDBContextSlave")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<OpcHelperItem> OpcHelperItem { get; set; }
        //重写SaveChanges方法
        
    }

}