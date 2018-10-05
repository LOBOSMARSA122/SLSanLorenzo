using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class JerarquiaServicioCamposValores
    {
        public string ServicioId { get; set; }
        public List<ValorComponenteList> CampoValores { get; set; }
        public Antecedentes Antecedentes { get; set; }
    }

    public class Antecedentes
    {
        public string  Personales  { get; set; }
        public string Familiares { get; set; }
        public string Alcohol { get; set; }
        public string Tabaco { get; set; }
        public string Drogas { get; set; }
    }
}
