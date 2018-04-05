using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    // Utilizado en la grilla de administración de parámetros / grupos
    [DataContract]
    public class WarehouseList
    {
        [DataMember]
        public int? i_OrganizationId; 
        [DataMember]
        public int i_WarehouseId;
        [DataMember]
        public string v_Name;
        [DataMember]
        public string v_AdditionalInformation;
        [DataMember] 
        public string v_CreationUser;
        [DataMember]
        public string v_UpdateUser;
        [DataMember]
        public DateTime? d_CreationDate;
        [DataMember]
        public DateTime? d_UpdateDate;
        [DataMember]
        public int? i_IsDeleted;
        [DataMember]
        public string v_IsDeleted;
        [DataMember]
        public string v_AssignedNode;
    }
}
