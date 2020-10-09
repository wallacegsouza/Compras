using System.ComponentModel.DataAnnotations;

namespace Compras.Controller.Dtos
{
    public class User
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
    }
}