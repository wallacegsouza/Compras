using Microsoft.EntityFrameworkCore;
using Compras.Models;

namespace Compras.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {}

        public DbSet<Cliente> Clientes { get; set; }

    }
}