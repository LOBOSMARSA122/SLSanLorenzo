using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class frmEsoCuidadosPreventivos
    {
        public string Nombre { get; set; }
        public bool Check { get; set; }
        public int ParameterId { get; set; }
        List<frmEsoCuidadosPreventivos> Hijos { get; set; }
    }
}
