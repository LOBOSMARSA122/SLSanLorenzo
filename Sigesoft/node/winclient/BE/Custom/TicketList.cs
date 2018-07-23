using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class TicketList
    {
        public string v_TicketId { get; set; }
        public string v_ServiceId { get; set; }
        public DateTime? d_Fecha { get; set; }

        public List<TicketDetalleList> Productos { get; set; }
    }
}
