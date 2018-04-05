using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ReportCuestionarioAudio
    {
        public string NombrePaciente { get; set; }
        public string DNI { get; set; }
        public string EmpresaCliente { get; set; }
        public string Edad { get; set; }
        public string Genero { get; set; }
        public string FechaExamen { get; set; }
        public byte[] FirmaPaciente { get; set; }
        public byte[] FirmaMedico { get; set; }
        public byte[] HuellaPaciente { get; set; }
        public byte[] LogoClinica { get; set; }
        public string IdServicio { get; set; }
        public string IdServiceCompoenent { get; set; }
    
       public string AntiguedadPuesto { get; set; }
    }
}
