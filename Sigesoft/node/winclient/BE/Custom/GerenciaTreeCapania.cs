using System.Collections.Generic;

namespace Sigesoft.Node.WinClient.BE
{
    public class GerenciaTreeCapania
    {
        public int Cantidad { get; set; }
        public string Agrupador { get; set; }
        public double? Total { get; set; }
        public List<Compania> Companias { get; set; }
    }

    public class Compania
    {
        public int Cantidad { get; set; }
        public string CompaniaName { get; set; }
        public double? Total { get; set; }
        public List<Contrata> Contratas { get; set; }
    }

    public class Contrata
    {
        public string CompaniaName { get; set; }
        public int Cantidad { get; set; }
        public string ContrataName { get; set; }
        public double? Total { get; set; }
    }
}
