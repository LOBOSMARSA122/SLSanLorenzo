using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServicioFacturado
    {
        public string EmpresaCliente { get; set; }
        public double TotalCobrado { get; set; }
        public double TotalCobrar { get; set; }

        public List<ServicioFacturadoDetalle> ServicioFacturadoDetalle { get; set; }
    }
}
