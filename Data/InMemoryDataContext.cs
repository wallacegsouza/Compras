using Microsoft.EntityFrameworkCore;

namespace Compras.Data
{
    public class InMemoryDataContext : DataContext
    {
        public InMemoryDataContext(DbContextOptions<InMemoryDataContext> options) 
            : base(options)
        {}
    }
}