using Microsoft.EntityFrameworkCore;
using Compras.Models;

namespace Compras.Data
{
    public abstract class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) 
            : base(options)
        {}
        public DbSet<Cliente> Clientes { get; set; }
    }
}