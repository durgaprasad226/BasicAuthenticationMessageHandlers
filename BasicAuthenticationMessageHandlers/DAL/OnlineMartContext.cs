using BasicAuthenticationMessageHandlers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasicAuthenticationMessageHandlers.DAL
{
    public class OnlineMartContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserMaster>().ToTable("UserMaster");
        }
        public DbSet<UserMaster> userMasters { get; set; }
    }
}