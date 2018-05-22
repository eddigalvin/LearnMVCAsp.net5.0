using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Models;

namespace DataAccessLayer
{
    public class Dal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("tblCustomer");   //this hardwires data acccess layer CustVwModel t CustomerDB??? CHECK
            modelBuilder.Entity<User>().ToTable("tblUser");
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
