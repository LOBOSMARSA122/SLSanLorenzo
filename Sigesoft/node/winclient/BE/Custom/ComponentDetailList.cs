using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComponentDetailList
    {
        public int? i_IsProcessed { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_ServiceComponentId { get; set; }
        public int StatusComponentId { get; set; }
        public string StatusComponent { get; set; }
        public int? i_ApprovedInsertUserId { get; set; }
        public string ApprovedUpdateUser { get; set; }
    }
}
