using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class WorkerData
    {
        public string PersonId { get; set; }
        public string Trabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string Genero { get; set; }
        public string Puesto { get; set; }
        public string Protocolo { get; set; }
        public string Grupo { get; set; }
        public string TipoExamen { get; set; }
        public byte[] PersonImage { get; set; }
    }
}
