using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    public class RolCuotaList
    {
        public string v_RolCuotaId { get; set; }
        public int i_RolId { get; set; }

        public string v_RolName { get; set; }
        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
