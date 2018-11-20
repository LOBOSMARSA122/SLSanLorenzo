using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class TicketDetalleList
    {
        public string v_TicketDetalleId { get; set; }
        public string v_TicketId { get; set; }
        public string v_IdProductoDetalle { get; set; }
        public string v_NombreProducto { get; set; }
        public string v_CodInterno { get; set; }
        public string v_Descripcion { get; set; }
        public decimal d_Cantidad { get; set; }
        public int i_EsDespachado { get; set; }
        public string EsDespachado { get; set; }
        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }
        public decimal d_PrecioVenta{ get; set; }
        public decimal Total { get; set; }
        public int? i_IsDeletd { get; set; }
        
        public decimal? d_SaldoPaciente { get; set; }
        public decimal? d_SaldoAseguradora { get; set; }

        public string v_IdUnidadProductiva { get; set; }
        public int i_EsDeducible { get; set; }
        public int i_EsCoaseguro { get; set; }

        public decimal? d_Importe { get; set; }
    }
}
