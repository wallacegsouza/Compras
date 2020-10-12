using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Compras.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Descricao { get; set; }

        [Range(0, Double.MaxValue)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataValidade { get; set; }

        public string UnidadeMedida { get; set; }
        public string Quantificador { get; set; }
        public ICollection<ItemCompra> Compras { get; set; }
    }
}