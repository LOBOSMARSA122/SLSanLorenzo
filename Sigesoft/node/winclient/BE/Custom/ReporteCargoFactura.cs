using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteCargoFactura
    {
        public byte[] LogoEmpresaPropietaria { get; set; }
        public string RazonSocialEmpresaPropietaria { get; set; }
        public string FechaActual { get; set; }
        public string SedeEmpresaPropietaria { get; set; }
        public string RazonSocialEmpresaCliente { get; set; }      
        public string EmailRepresentanteLegalEP { get; set; }
        public string EmailContactoEP { get; set; }
        public string Mes { get; set; }
        public string Fecha { get; set; }
        public string NroFactura { get; set; }
        public string NroTrabajadores { get; set; }
        public string Parametro { get; set; }
        public string AnioMes { get; set; }
    }
}
