using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using Compras.Data;
using Compras.Models;
using Microsoft.Extensions.Logging;

namespace Compras.Controller
{
    [ApiController]
    [Route("v1/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger _logger;

        public ClienteController(ILogger<Cliente> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] DataContext context)
        {
            _logger.LogInformation("Get all clients");
            var clientes = await context.Clientes.ToListAsync();
            return clientes;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Cliente>> GetById([FromServices] DataContext context, int id)
        {
            var cliente = await context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync<Cliente>(x => x.Id == id);
            return cliente;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<Cliente>> Post(
            [FromServices] DataContext context,
            [FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
                return cliente;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}