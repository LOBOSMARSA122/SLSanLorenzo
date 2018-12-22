using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class GerenciaTreeContrata
    {
        public int Cantidad { get; set; }
        public string Agrupador { get; set; }
        public double? Total { get; set; }
        public List<Contrata_> Contratas { get; set; }
    }

    public class Contrata_
    {
        public int Cantidad { get; set; }
        public string ContrataName { get; set; }
        public double? Total { get; set; }
        public List<Compania_> Companias { get; set; }
    }

    public class Compania_
    {
        public string ContrataName { get; set; }
        public int Cantidad { get; set; }
        public string CompaniaName { get; set; }
        public double? Total { get; set; }
    }
}
