using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComisionVendedorList
    {
        public string v_ServicioId { get; set; }
        public string Protocolo { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Comision { get; set; }
        public decimal? SubTotal { get; set; }

        public string CostoComision { get; set; }
        public string Vendedor { get; set; }


    }
}
