using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract]
    public class AutorizationList
    {
        [DataMember]
        public string V_Description { get; set; }
        [DataMember]
        public int? I_ParentId { get; set; }
        [DataMember]
        public int I_ApplicationHierarchyId { get; set; }
        [DataMember]
        public string V_Form { get; set; }
        [DataMember]
        public int? I_ApplicationHierarchyTypeId { get; set; }
        [DataMember]
        public string V_RoleName { get; set; }
        public int? i_RoleId { get; set; }
    }
}
