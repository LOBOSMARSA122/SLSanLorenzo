using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LiquiAseguradoraDetalle
    {
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public decimal? SaldoPaciente { get; set; }
        public decimal? SaldoAseguradora { get; set; }
        public decimal SubTotal { get; set; }

        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        
    }
}
