using System.Linq;
using System.Threading.Tasks;

using Compras.Models;
using Compras.Data;

namespace Compras.Repositories
{
    public class ClienteRepository
    {
        private DataContext _context;

        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public Cliente Get(string login, string senha)
        {
            return _context.Clientes
                .Where(x => x.Login.ToLower() == login.ToLower() && x.Senha.ToLower() == senha.ToLower())
                .FirstOrDefault();
        }

        public async Task<Cliente> Save(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}