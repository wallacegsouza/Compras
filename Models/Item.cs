using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Compras.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        private string Nome { get; set; }
        private string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataValidade { get; set; }
        private string UnidadeMedida { get; set; }
        private string Quantificador { get; set; }
        public ICollection<ItemCompra> Compras { get; set; }
    }
}