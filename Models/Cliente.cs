using System.ComponentModel.DataAnnotations;
using Compras.Models.Enums;

namespace Compras.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public TipoPessoa Tipo { get; set; }
    }
}