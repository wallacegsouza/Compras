using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compras.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Column("DataVenda")]
        public DateTime Data { get; set; }

        [Column(TypeName = "json")]
        public Item[] Itens { get; set; }
    }
}