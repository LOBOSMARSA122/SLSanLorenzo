using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract]
    public class OrganizationList
    {
        //beto
        [DataMember]
        public string v_OrganizationId { get; set; }
        [DataMember]
        public Int32 i_OrganizationTypeId { get; set; }
        [DataMember]
        public string v_OrganizationTypeIdName { get; set; }
        [DataMember]
        public Int32 i_UserInterfaceOrder { get; set; }
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
        public string v_SectorName { get; set; }

        public string v_SectorCodigo { get; set; }

        public string v_EmailContacto { get; set; }
        public string v_Sede { get; set; }
        public byte[] b_Image { get; set; }
        public bool b_Seleccionar { get; set; }
        public string v_ContactoMedico { get; set; }
        public string v_EmailMedico { get; set; }

        public string v_ContacName { get; set; }
        public string v_Contacto { get; set; }
        public string v_Observation { get; set; }
        
        public Int32? i_NumberQuotasOrganization { get; set; }
        public Int32? i_NumberQuotasMen { get; set; }
        public Int32? i_DepartmentId { get; set; }
        public Int32? i_ProvinceId { get; set; }
        public Int32? i_DistrictId { get; set; }
        public Int32? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public Int32? i_UpdateUserId { get; set; }


        public string v_CIIUDescription1 { get; set; }
    }
}
