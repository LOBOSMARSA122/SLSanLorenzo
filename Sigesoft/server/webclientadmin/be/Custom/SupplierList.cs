using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
  public  class SupplierList
    {
        [DataMember]
        public Int32 i_SupplierId { get; set; }
        [DataMember]
        public Int32 i_SectorTypeId { get; set; }
        [DataMember]
        public string v_SectorTypeIdName { get; set; }
        [DataMember]
        public string v_IdentificationNumber { get; set; }
        [DataMember]
        public string v_Name { get; set; }
        [DataMember]
        public string v_Address { get; set; }
        [DataMember]
        public string v_PhoneNumber { get; set; }
        [DataMember]
        public string v_Mail { get; set; }
        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
        [DataMember]
        public int? i_IsDeleted { get; set; }
    }
}
