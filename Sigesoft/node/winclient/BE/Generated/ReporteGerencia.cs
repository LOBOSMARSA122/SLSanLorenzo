using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteGerencia
    {
        public string ServiceId { get; set; }
        public string OrganizationId { get; set; }
        public int? TipoOrganizationId { get; set; }
        public int? i_MasterServiceId { get; set; }
        public int? i_ServiceTypeId { get; set; }
        public int? i_EsoTypeId { get; set; }
        public string Empresa { get; set; }
        public int? NroTrabajadores { get; set; }
        public float? Total { get; set; }
        public float? Cancelado { get; set; }
        public float? Saldo { get; set; }

        public float? Precio { get; set; }

        public int? i_IsFac { get; set; }
    }
}
