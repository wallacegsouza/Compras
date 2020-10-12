using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Compras.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Range(0.01, Double.MaxValue)]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Column("DataVenda")]
        public DateTime Data { get; set; }

        public ICollection<ItemCompra> Itens { get; set; }

        public override string ToString()
        {
            return String.Format("[ Id: {0}, Valor: {1}, Data: {2} Itens: {3} ]", Id, Valor, Data, Itens);
        }
    }
}