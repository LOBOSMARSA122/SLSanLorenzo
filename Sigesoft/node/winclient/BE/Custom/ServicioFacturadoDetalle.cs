using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServicioFacturadoDetalle
    {
        public string v_FacturacionId { get; set; }
        public string NroFactura {get;set;}
        public string EstadoFacturacion { get; set; }
        public double MontoFacturado { get; set; }
        public DateTime? FechaCobro { get; set; }

        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
