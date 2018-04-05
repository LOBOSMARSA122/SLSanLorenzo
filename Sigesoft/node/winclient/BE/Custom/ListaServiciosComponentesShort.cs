using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ListaServiciosComponentesShort
    {
        public string ServiceId { get; set; }
        public string Paciente { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string ComponentId { get; set; }
        public int ServiceComponentStatusId { get; set; }
        public string ProtocoloId { get; set; }
        public string ServiceComponentName { get; set; }
    }
}
