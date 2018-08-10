using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class recetaList
    {
        public string v_IdProductoDetalle { get; set; }
        public string NombreMedicamento { get; set; }
        public decimal? d_SaldoPaciente { get; set; }
        public decimal? d_SaldoAseguradora { get; set; }

        public string v_IdUnidadProductiva { get; set; }
        public int i_EsDeducible { get; set; }
        public int i_EsCoaseguro { get; set; }

        public decimal? d_Importe { get; set; }
        public decimal? i_Cantidad { get; set; }
        public decimal? d_PrecioVenta { get; set; }
    }
}
