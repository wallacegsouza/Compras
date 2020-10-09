using Microsoft.EntityFrameworkCore;

namespace Compras.Data
{
    public class MysqlDataContext : DataContext
    {
        public MysqlDataContext(DbContextOptions<MysqlDataContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}