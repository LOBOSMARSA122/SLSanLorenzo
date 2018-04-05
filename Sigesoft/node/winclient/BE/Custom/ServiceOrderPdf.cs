using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public  class ServiceOrderPdf
    {
       public string v_ServiceOrderId { get; set; }
       public string EmpresaCliente { get; set; }
       public string TipoGeso { get; set; }
       public string TipoESO { get; set; }
       public float? TotalProtocolo { get; set; }

      public List<ServiceOrderDetailPdf> DetalleServiceOrder { get; set; }
    }
}
