using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class GerenciaCampanias
    {
        public int? IdCondicionPago { get; set; }
        public string CondicionPago { get; set; }
        public int? IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public DateTime? FechaFactura { get; set; }
        public string Comprobante { get; set; }
        public string Empresa { get; set; }
        public decimal? Importe { get; set; }
        public string ServiceId { get; set; }
        public string Trabajador { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string Compania { get; set; }
        public string Contratista { get; set; }
        public double? CostoExamen { get; set; }
        public string TipoEso { get; set; }
    }
}
