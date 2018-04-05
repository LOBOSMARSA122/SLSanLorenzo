using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceOrderDetailPdf
    {
        public string v_ServiceOrderDetailId { get; set; }
        public string v_ServiceOrderId { get; set; }
        public string v_ComponentId { get; set; }
        public string Componente { get; set; }
        public float? v_Precio { get; set; }
        
    }
}
