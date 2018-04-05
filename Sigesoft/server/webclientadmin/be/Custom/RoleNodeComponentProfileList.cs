using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class RoleNodeComponentProfileList
    {
        public string v_RoleNodeComponentId { get; set; }
        public int? i_NodeId { get; set; }
        public int? i_RoleId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_Read { get; set; }
        public string v_Write { get; set; }
        public string v_Dx { get; set; }
        public string v_Approved { get; set; }

    
    }
}
