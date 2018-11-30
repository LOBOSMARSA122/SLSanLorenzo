using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class LiquidacionesConsolidado
    {
        public string v_OrganizationName { get; set; }
        public string v_Ruc { get; set; }
        public string v_AddressLocation { get; set; }
        public string v_TelephoneNumber { get; set; }
        public string v_ContactName { get; set; }

        public string v_NroLiquidacion { get; set; }
        public string v_ServiceId { get; set; }
        public string v_OrganizationId { get; set; }
        public DateTime? d_creaionLiq { get; set; }
        public List<LiquidacionesConsolidadoDetalle> detalle { get; set; }
    }

    public class LiquidacionesConsolidadoDetalle
    {
        public string v_ServiceId { get; set; }
        public string v_NroLiquidacion { get; set; }
        public string v_OrganizationId { get; set; }     public string v_Paciente { get; set; }
        public DateTime? d_exam { get; set; }
        public float? d_price { get; set; }
        public string v_UsuarRecord { get; set; }
        public string v_CenterCost { get; set; }
    }
}
