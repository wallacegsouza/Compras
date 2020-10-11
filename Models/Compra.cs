using System;
using System.Text.Json;
using System.Collections.Generic;
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
        private string _Itens;

        [NotMapped]
        public Item[] Itens
        {
            get { return (_Itens == null) ? null : JsonSerializer.Deserialize<Item[]>(_Itens); }
            set { _Itens = JsonSerializer.Serialize(value); }
        }

        public override string ToString()
        {
            return String.Format("[ Id: {0}, Valor: {1}, Data: {2} Itens: {3} ]", Id, Valor, Data, _Itens);
        }
    }
}