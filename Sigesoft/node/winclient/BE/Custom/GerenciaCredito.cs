using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class GerenciaCredito
    {
        public DateTime? FechaServicio { get; set; }
        public string ServiceId { get; set; }
        public string Trabajador { get; set; }
        public string Ocupacion { get; set; }
        public string TipoEso { get; set; }
        public double? CostoExamen { get; set; }
        public string Compania { get; set; }
        public string Contratista { get; set; }
        public string EmpresaFacturacion { get; set; }
        public string Comprobante { get; set; }
        public string NroLiquidacion { get; set; }
        public DateTime? FechaFactura { get; set; }
        public decimal? ImporteTotalFactura { get; set; }
        public decimal? d_NetoXCobrarFactura { get; set; }
        public string CondicionFactura { get; set; }
        public string xxx { get; set; }

    }
}
