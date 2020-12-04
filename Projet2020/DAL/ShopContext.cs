using Projet2020.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Projet2020.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("ShopContext"){}
        public DbSet<Clients> Client { get; set; }
        public DbSet<Produits> Product { get; set; }
        public DbSet<Commandes> orders { get; set; }
        public DbSet<Comments> Com { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}