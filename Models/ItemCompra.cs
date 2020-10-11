using System;

namespace Compras.Models
{
    public class ItemCompra
    {
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public Compra Compra { get; set; }
        public int CompraId { get; set; }
        public int Quantidade { get; set; }

        public override string ToString()
        {
            return String.Format("[ ItemId: {0}, CompraId: {1}, Quantidade: {2} ]", ItemId, CompraId, Quantidade);
        }
    }
}