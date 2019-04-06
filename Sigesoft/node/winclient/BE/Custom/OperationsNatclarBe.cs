using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class OperationsNatclarBe
    {
        public bool b_select { get; set; }
        public string v_PersonId { get; set; }
        public string v_ServiceId { get; set; }
        public string v_Pacient { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string v_Paquete { get; set; }
        public int? i_EstadoId { get; set; }
    }
}
