using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteCargoHistorias
    {
        public byte[] LogoEmpresaPropietaria { get; set; }
        public string RazonSocialEmpresaPropietaria { get; set; }
        public string FechaActual { get; set; }
        public string SedeEmpresaPropietaria { get; set; }
        public string RazonSocialEmpresaCliente { get; set; }
        public string Correlativo { get; set; }
        public string FechaHoraServicio { get; set; }
        public string Paciente { get; set; }
        public string TipoEso { get; set; }
        public string Aptitud { get; set; }
        public string EmailRepresentanteLegalEP { get; set; }
        public string EmailContactoEP { get; set; }
        public string Parametro { get; set; }
        public string AnioMes { get; set; }
    }
}
