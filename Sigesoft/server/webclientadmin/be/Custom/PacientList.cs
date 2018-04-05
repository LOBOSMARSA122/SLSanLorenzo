using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
    public class PacientList
    {
        [DataMember]
        public Int32 i_PersonId { get; set; }
        [DataMember]
        public String v_FirstName { get; set; }
        [DataMember]
        public String v_FirstLastName { get; set; }
        [DataMember]
        public String v_SecondLastName { get; set; }
        [DataMember]
        public Nullable<Int32> i_DocTypeId { get; set; }
        [DataMember]
        public String v_DocNumber { get; set; }
        [DataMember]
        public Nullable<DateTime> d_Birthdate { get; set; }
        [DataMember]
        public String v_BirthPlace { get; set; }
        [DataMember]
        public Nullable<Int32> i_SexTypeId { get; set; }
        [DataMember]
        public Nullable<Int32> i_MaritalStatusId { get; set; }
        [DataMember]
        public Nullable<Int32> i_LevelOfId { get; set; }
        [DataMember]
        public String v_TelephoneNumber { get; set; }
        [DataMember]
        public String v_AdressLocation { get; set; }
        [DataMember]
        public String v_GeografyLocationId { get; set; }
        [DataMember]
        public String v_ContactName { get; set; }
        [DataMember]
        public String v_EmergencyPhone { get; set; }
        [DataMember]
        public String v_Mail { get; set; }
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
        [DataMember]
        public int? i_UpdateNodeId { get; set; }
        [DataMember]
        public Byte[] b_Photo { get; set; }
        
    }
}
