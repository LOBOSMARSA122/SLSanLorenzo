using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LiquidacionServicios
    {
        public bool Select { get; set; }
        public string Paciente { get; set; }
        public string Tipo { get; set; }
        public string Aseguradora { get; set; }
        public string v_ServiceId { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public decimal r_costo { get; set; }
        public decimal r_Comision { get; set; }
        public decimal r_Total { get; set; }
        public string Componente { get; set; }
    }
}
