//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/11 - 15:29:15
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class organizationDto
    {
        [DataMember()]
        public String v_OrganizationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_OrganizationTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SectorTypeId { get; set; }

        [DataMember()]
        public String v_SectorName { get; set; }

        [DataMember()]
        public String v_SectorCodigo { get; set; }

        [DataMember()]
        public String v_IdentificationNumber { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

        [DataMember()]
        public String v_Address { get; set; }

        [DataMember()]
        public String v_PhoneNumber { get; set; }

        [DataMember()]
        public String v_Mail { get; set; }

        [DataMember()]
        public String v_ContacName { get; set; }

        [DataMember()]
        public String v_Contacto { get; set; }

        [DataMember()]
        public String v_EmailContacto { get; set; }

        [DataMember()]
        public String v_Observation { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NumberQuotasOrganization { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NumberQuotasMen { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NumberQuotasWomen { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DepartmentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ProvinceId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DistrictId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Byte[] b_Image { get; set; }

        [DataMember()]
        public String v_ContactoMedico { get; set; }

        [DataMember()]
        public String v_EmailMedico { get; set; }

        [DataMember()]
        public Nullable<Decimal> r_Factor { get; set; }

        [DataMember()]
        public Nullable<Decimal> r_FactorMed { get; set; }

        [DataMember()]
        public List<liquidacionDto> liquidacion { get; set; }

        [DataMember()]
        public List<locationDto> location { get; set; }

        [DataMember()]
        public List<nodeorganizationlocationprofileDto> nodeorganizationlocationprofile { get; set; }

        [DataMember()]
        public List<nodeorganizationlocationwarehouseprofileDto> nodeorganizationlocationwarehouseprofile { get; set; }

        [DataMember()]
        public List<nodeorganizationprofileDto> nodeorganizationprofile { get; set; }

        [DataMember()]
        public List<warehouseDto> warehouse { get; set; }

        public organizationDto()
        {
        }

        public organizationDto(String v_OrganizationId, Nullable<Int32> i_OrganizationTypeId, Nullable<Int32> i_SectorTypeId, String v_SectorName, String v_SectorCodigo, String v_IdentificationNumber, String v_Name, String v_Address, String v_PhoneNumber, String v_Mail, String v_ContacName, String v_Contacto, String v_EmailContacto, String v_Observation, Nullable<Int32> i_NumberQuotasOrganization, Nullable<Int32> i_NumberQuotasMen, Nullable<Int32> i_NumberQuotasWomen, Nullable<Int32> i_DepartmentId, Nullable<Int32> i_ProvinceId, Nullable<Int32> i_DistrictId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Byte[] b_Image, String v_ContactoMedico, String v_EmailMedico, Nullable<Decimal> r_Factor, Nullable<Decimal> r_FactorMed, List<liquidacionDto> liquidacion, List<locationDto> location, List<nodeorganizationlocationprofileDto> nodeorganizationlocationprofile, List<nodeorganizationlocationwarehouseprofileDto> nodeorganizationlocationwarehouseprofile, List<nodeorganizationprofileDto> nodeorganizationprofile, List<warehouseDto> warehouse)
        {
			this.v_OrganizationId = v_OrganizationId;
			this.i_OrganizationTypeId = i_OrganizationTypeId;
			this.i_SectorTypeId = i_SectorTypeId;
			this.v_SectorName = v_SectorName;
			this.v_SectorCodigo = v_SectorCodigo;
			this.v_IdentificationNumber = v_IdentificationNumber;
			this.v_Name = v_Name;
			this.v_Address = v_Address;
			this.v_PhoneNumber = v_PhoneNumber;
			this.v_Mail = v_Mail;
			this.v_ContacName = v_ContacName;
			this.v_Contacto = v_Contacto;
			this.v_EmailContacto = v_EmailContacto;
			this.v_Observation = v_Observation;
			this.i_NumberQuotasOrganization = i_NumberQuotasOrganization;
			this.i_NumberQuotasMen = i_NumberQuotasMen;
			this.i_NumberQuotasWomen = i_NumberQuotasWomen;
			this.i_DepartmentId = i_DepartmentId;
			this.i_ProvinceId = i_ProvinceId;
			this.i_DistrictId = i_DistrictId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.b_Image = b_Image;
			this.v_ContactoMedico = v_ContactoMedico;
			this.v_EmailMedico = v_EmailMedico;
			this.r_Factor = r_Factor;
			this.r_FactorMed = r_FactorMed;
			this.liquidacion = liquidacion;
			this.location = location;
			this.nodeorganizationlocationprofile = nodeorganizationlocationprofile;
			this.nodeorganizationlocationwarehouseprofile = nodeorganizationlocationwarehouseprofile;
			this.nodeorganizationprofile = nodeorganizationprofile;
			this.warehouse = warehouse;
        }
    }
}
