using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LiquidacionMedicoList
    {
        public int? MedicoTratanteId { get; set; }
        public string MedicoTratante { get; set; }
        
        public string Paciente { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string v_ServiceId { get; set; }
        
        public List<LiquidacionServicios> Servicios { get; set; }
    }
}
