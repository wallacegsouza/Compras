using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Compras.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Compras.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService (IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Cliente cliente)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            // var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( new Claim[]
                {
                    new Claim(ClaimTypes.Name, cliente.Login.ToString()),
                    new Claim(ClaimTypes.Email, cliente.Email.ToString()),
                    new Claim(ClaimTypes.Role, cliente.Tipo.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}