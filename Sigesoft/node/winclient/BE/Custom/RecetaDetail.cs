using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class RecetaDetail
    {
        public string v_IdProductoDetalle { get; set; }
        public decimal? d_SaldoPaciente { get; set; }
        public decimal? d_SaldoAseguradora { get; set; }
        public decimal? d_Importe { get; set; }
        public decimal? ImporteCoaseguro { get; set; }
    }
}
