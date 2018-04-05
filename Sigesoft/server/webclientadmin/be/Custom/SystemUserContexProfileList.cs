using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
    public class SystemUserContexProfileList
    {
        [DataMember]
        public int i_OrganizationId { get; set; }
        [DataMember]
        public int i_ApplicationHierarchyId { get; set; }
        [DataMember]
        public int i_NodeId { get; set; }
        [DataMember]
        public int i_SystemUserId { get; set; }
        [DataMember]
        public string v_NodeName { get; set; }
        [DataMember]
        public string v_OrganizationName { get; set; }
       
    }
}
