using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class frmEsoCuidadosPreventivosFechas
    {
        public DateTime FechaServicio { get; set; }
        public List<frmEsoCuidadosPreventivos> Listado { get; set; }
    }
    public class frmEsoCuidadosPreventivos
    {
        public string Nombre { get; set; }
        public bool Check { get; set; }
        public int ParameterId { get; set; }
        public int GrupoId { get; set; }
        public int PadreId { get; set; }
        public List<frmEsoCuidadosPreventivos> Hijos { get; set; }
    }
}
