using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class TicketDetalleList
    {
        public string v_TicketDetalleId { get; set; }
        public string v_TicketId { get; set; }
        public string v_IdProductoDetalle { get; set; }
        public string v_NombreProducto { get; set; }
        public decimal d_Cantidad { get; set; }
        public int i_EsDespachado { get; set; }
    }
}
