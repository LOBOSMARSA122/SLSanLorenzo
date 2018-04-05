using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class RevisionPuntosList
    {
        public string v_ProductId { get; set; }
        public string v_ProductName { get; set; }
        public float? i_Cuota { get; set; }
        public float? r_Quantity { get; set; }
        public float? i_CantidadVendida { get; set; }
        public float? i_Saldo { get; set; }

        public decimal d_ValorVenta { get; set; }
        public decimal d_Importe { get; set; }

        public DateTime? d_InsertDate { get; set; }


    }
}
