using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class GerenciaTreeCredito
    {
        public string Agrupador { get; set; }
        public decimal? Total { get; set; }
        public List<TipoCredito> Tipos { get; set; }
    }

    public class TipoCredito
    {
        public string Agrupador { get; set; }
        public string Tipo { get; set; }
        public decimal? Total { get; set; }
        public List<EmpresaCredito> Empresas { get; set; }
    }

    public class EmpresaCredito
    {
        public string Tipo { get; set; }
        public string Empresa { get; set; }
        public decimal? Total { get; set; }
    }
}
