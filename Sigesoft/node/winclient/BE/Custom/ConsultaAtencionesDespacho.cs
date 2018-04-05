using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ConsultaAtencionesDespacho
    {
        public string ServiceId { get; set; }
        public string NombresPaciente { get; set; }
        public string ApellidosPaciente { get; set; }
        public string NombreCompleto {
            get { return ApellidosPaciente + " " + NombresPaciente; }
        }
        public DateTime FechaAtencion { get; set; }
        public bool Atendido { get; set; }
        public string Estado {
            get { return Atendido ? "ATENDIDO" : "PENDIENTE"; }
        }

        public string NroDocumento { get; set; }
    }
}
