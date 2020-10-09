using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Compras.Models;
using Compras.Services;
using Compras.Repositories;
using Compras.Controller.Dtos;

namespace Compras.Controller
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate(
            [FromServices] ClienteRepository clienteRepository,
            [FromServices] TokenService tokenService,
            [FromBody] User model)
        {
            var cliente = clienteRepository.Get(model.Login, model.Senha);

            if (cliente == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = tokenService.GenerateToken(cliente);
            cliente.Senha = "";
            return new
            {
                user = User,
                token = token
            };
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("pessoa-fisica")]
        [Authorize(Roles = "Fisica")]
        public string PessoaFisica() => "Pessoa Fisica";

        [HttpGet]
        [Route("pessoa-juridica")]
        [Authorize(Roles = "Juridica")]
        public string PessoaJuridica() => "Pessoa Juridica";

    }
}