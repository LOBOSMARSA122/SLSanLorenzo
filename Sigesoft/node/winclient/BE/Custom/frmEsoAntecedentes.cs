using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class frmEsoAntecedentes
    {
        public int GrupoId { get; set; }
        public int ParametroId { get; set; }
        public string Nombre { get; set; }
        public List<frmEsoAntecedentes> Hijos { get; set; }
    }
}
