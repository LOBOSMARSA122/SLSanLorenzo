using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class NodeServiceProfileList
    {
        public string v_NodeServiceProfileId { get; set; }
        public int? i_NodeId { get; set; }
        public string v_NodeName { get; set; }

        public int? i_ServiceTypeId { get; set; }
        public string v_ServiceTypeName { get; set; }

        public int? i_MasterServiceId { get; set; }
        public string v_MasterServiceName { get; set; }

        public int? i_ParentParameterId { get; set; }

    }
}
