using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LiquidacionEmpresa
    {
        public string v_OrganizationName { get; set; }

        public string v_LiquidacionId { get; set; }
        public string v_NroLiquidacion { get; set; }
        public string v_ServiceId { get; set; }
        public string v_OrganizationId { get; set; }
        public decimal? d_Monto { get; set; }
        public DateTime? d_FechaVencimiento { get; set; }
        public string v_NroFactura { get; set; }

        public List<LiquidacionEmpresaDetalle> detalle { get; set; }
    }

    public class LiquidacionEmpresaDetalle
    {
        public string v_LiquidacionId { get; set; }
        public decimal? d_Debe { get; set; }
        public decimal? d_Pago { get; set; }
        public decimal? d_Total { get; set; }
    }

}
