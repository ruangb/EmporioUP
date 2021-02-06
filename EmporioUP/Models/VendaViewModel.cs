using System;
using System.Collections.Generic;

namespace EmporioUP.Models
{
    public class VendaViewModel
    {
        Venda venda = new Venda();
        Cliente cliente = new Cliente();
        Produto produto = new Produto();

        public int id_venda { get; set; }
        public decimal? valor { get; set; }
        public int? qtde { get; set; }
        public decimal? total { get; set; }
        public DateTime dt_venda { get; set; }

        public int id_cliente { get; set; }
        public string nome { get; set; }

        public int id_produto { get; set; }
        public string descricao { get; set; }
        public string img { get; set; }

        public virtual Produto v_produto { get; set; }
    }
}