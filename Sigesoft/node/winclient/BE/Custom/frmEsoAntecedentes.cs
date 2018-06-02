using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class frmEsoAntecedentesPadre
    {
        public int GrupoId { get; set; }
        public int ParametroId { get; set; }
        public string Nombre { get; set; }
        public List<frmEsoAntecedentesHijo> Hijos { get; set; }
    }

    public class frmEsoAntecedentesHijo
    {
        public string Nombre { get; set; }
        public bool SI { get; set; }
        public bool NO { get; set; }
        public int GrupoId { get; set; }
        public int ParametroId { get; set; }
    }
}
