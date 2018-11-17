using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProduccionProfesional
    {
        public string Usuario { get; set; }
        public string Consultorio { get; set; }
        public int Total { get; set; }
        public double PrecioUnitario { get; set; }
        public double Pagar { get; set; }
        public List<ProduccionProfesionalDetalle> ProduccionProfesionalDetalle { get; set; }
    }
}
