using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Sigesoft.Common
{
    [DataContract]
    public class SoftwareComponentCheckDto
    {
        [DataMember]
        public int i_SoftwareComponentId { get; set; } 
        [DataMember]
        public string v_LocalVersion { get; set; }
        [DataMember]
        public string v_ServerVersion { get; set; }
        [DataMember]
        public bool b_RequireUpdate { get; set; }
        [DataMember]
        public int? i_DeploymentFileId { get; set; }
    }
}