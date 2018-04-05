using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class CotizacionProtocolo
    {
        public string ProtocoloId{ get; set; }
        public string ProtocoloNombre { get; set; }
        public Decimal? CostoProtocolo { get; set; }

        public List<CotizacionProtocoloDetalle> Detalle { get; set; }


    }
}
