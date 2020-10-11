using System.Linq;
using System.Threading.Tasks;
using Compras.Models;
using Compras.Data;

namespace Compras.Repositories
{
    public class CompraRepository
    {
        private DataContext _context;

        public CompraRepository(DataContext context)
        {
            _context = context;
        }

        public Compra Get(int id)
        {
            return _context.Compras.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Compra> Save(Compra compra)
        {
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
            return compra;
        }
    }
}