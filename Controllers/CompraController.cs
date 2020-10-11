using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using Compras.Models;
using Compras.Messaging;

namespace Compras.Controller
{
    [ApiController]
    [Route("v1/compras")]
    public class CompraController : ControllerBase
    {
        private readonly ILogger _logger;

        public CompraController(ILogger<CompraController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public string Send([FromServices] Producer producer, [FromBody] Compra compra)
        {
            _logger.LogInformation("Producer.Send Queue-Create-Compras");
            producer.Publish(compra);
            return "Mensagem enviada";
        }

    }
}