using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class FacturaCobranza
    {
        public string v_IdVenta { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public decimal? NetoXCobrar { get; set; }
        public decimal? TotalPagado { get; set; }
        public string DocuemtosReferencia { get; set; }
        public string NroComprobante { get; set; }
        public string Condicion { get; set; }
    }
}
