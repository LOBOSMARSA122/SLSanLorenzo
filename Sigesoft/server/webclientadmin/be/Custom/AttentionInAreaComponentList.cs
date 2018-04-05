using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class AttentionInAreaComponentList
    {
        public string v_AttentioninAreaComponentId { get; set; }
        public string v_AttentionInAreaId { get; set; }
        public string v_ComponentId { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
    }
}
