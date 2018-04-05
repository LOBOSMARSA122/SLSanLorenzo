using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class NodeList
    {
        [DataMember]
        public int i_NodeId { get; set; }
        [DataMember]
        public string v_Description { get; set; }
        [DataMember]
        public string v_GeografyLocationId { get; set; }
        [DataMember]
        public string v_GeografyLocationDescription { get; set; }
        [DataMember]
        public string i_NodeTypeId { get; set; }
        [DataMember]
        public DateTime? d_BeginDate { get; set; }
        [DataMember]
        public DateTime? d_EndDate { get; set; }
        [DataMember]
        public int? i_IsDeleted { get; set; }
        [DataMember]
        public int? i_InsertUserId { get; set; }
        [DataMember]
        public DateTime? d_InsertDate { get; set; }
        [DataMember]
        public int? i_UpdateUserId { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
        [DataMember]
        public string v_InsertUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public string v_NodeType { get; set; }

    }
}
