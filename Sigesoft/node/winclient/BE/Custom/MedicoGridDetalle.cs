using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MedicoGridDetalle
    {
        public int i_SystemUserId { get; set; }
        public int i_GrupoId { get; set; }
        public string Grupo { get; set; }
        public decimal r_Clinica { get; set; }
        public decimal r_Medico { get; set; }
    }
}
