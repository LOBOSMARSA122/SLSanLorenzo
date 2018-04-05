using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ReportInterconsulta
    {
       public string ServiceId { get; set; }
        public string Paciente { get; set; }
        public DateTime FechaNacimiento { get; set; } 
        public int Edad { get; set; }
        public int Genero { get; set;} 
        public DateTime FechaServicio { get; set;}
        public string NroDocumento { get; set; }
        public string Puesto { get; set; }

        public string Altitud { get; set; }
        public string Especialidad { get; set; }
        public string Dx { get; set; }
        public string Cie10 { get; set; }
        public string Labor { get; set; }

        public string Solicita { get; set; }
        public string Observaciones { get; set; }
        public byte[] FirmaMedicoEvaluador { get; set; }
        public byte[] FirmaPaciente { get; set; }
        public byte[] HuellaPaciente { get; set; }
        public byte[] Logo { get; set; }
        public string TipoEso { get; set; }
     
    }
}
