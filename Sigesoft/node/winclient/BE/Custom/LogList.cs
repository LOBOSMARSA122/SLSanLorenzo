using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract]
    public class LogList
    {
        [DataMember]
        public string v_LogId { get; set; }
        [DataMember]
        public int i_EventTypeId { get; set; }
        [DataMember]
        public string v_EventTypeName { get; set; }
        [DataMember]
        public int i_NodeId { get; set; }
        [DataMember]
        public string v_NodeName { get; set; }
        [DataMember]
        public string v_OrganizationId { get; set; }
        [DataMember]
        public string v_OrganizationName { get; set; }
        [DataMember]
        public int i_SystemUserId { get; set; }
        [DataMember]
        public string v_SystemUserName { get; set; }
        [DataMember]
        public DateTime d_Date { get; set; }
        [DataMember]
        public string v_ProcessEntity { get; set; }
        [DataMember]
        public string v_ElementItem { get; set; }
        [DataMember]
        public int i_Success { get; set; }
        [DataMember]
        public string v_SuccessName { get; set; }
        [DataMember]
        public string v_ErrorException { get; set; }
    }
}
