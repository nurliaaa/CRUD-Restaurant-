using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Restaurant.Models
{
    public class TransaksiContext : DbContext
    {
        public TransaksiContext(DbContextOptions<TransaksiContext> options) : base(options)
        {
        }

        public DbSet<Transaksi> Transaksi { get; set; }
        public DbSet<Food> Food { get; set; } // Contoh entitas makanan
        public DbSet<Customer> Customer { get; set; } // Contoh entitas pelanggan

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaksi>()
                .HasOne(t => t.Food)
                .WithMany(f => f.Transaksi)
                .HasForeignKey(t => t.FoodID);

            modelBuilder.Entity<Transaksi>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Transaksi)
                .HasForeignKey(t => t.CustomerID);
        }
    }
}