using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MedicoGrid
    {
        public string v_MedicoId { get; set; }
        public int i_SystemUserId { get; set; }
        public string Medico { get; set; }
        public List<MedicoGridDetalle> Detalle { get; set; }
    }
}
