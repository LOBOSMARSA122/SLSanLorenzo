using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class ServiceShort
    {
        public string Empresa { get; set; }
        public string Paciente { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string DNI { get; set; }
    }
}
