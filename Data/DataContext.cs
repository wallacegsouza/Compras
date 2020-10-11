using Microsoft.EntityFrameworkCore;
using Compras.Models;

namespace Compras.Data
{
    public abstract class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) 
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCompra>()
                .HasKey(t => new { t.CompraId, t.ItemId });

            modelBuilder.Entity<ItemCompra>()
                .HasOne(ic => ic.Compra)
                .WithMany(c => c.Itens)
                .HasForeignKey(ic => ic.CompraId);

            modelBuilder.Entity<ItemCompra>()
                .HasOne(ic => ic.Item)
                .WithMany(t => t.Compras)
                .HasForeignKey(ic => ic.ItemId);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<ItemCompra> ItemCompra { get; set; }
    }
}