//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/29 - 17:29:06
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
    public partial class protocolDto
    {
        [DataMember()]
        public String v_ProtocolId { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

        [DataMember()]
        public String v_EmployerOrganizationId { get; set; }

        [DataMember()]
        public String v_EmployerLocationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsoTypeId { get; set; }

        [DataMember()]
        public String v_GroupOccupationId { get; set; }

        [DataMember()]
        public String v_CustomerOrganizationId { get; set; }

        [DataMember()]
        public String v_CustomerLocationId { get; set; }

        [DataMember()]
        public String v_NombreVendedor { get; set; }

        [DataMember()]
        public String v_WorkingOrganizationId { get; set; }

        [DataMember()]
        public String v_WorkingLocationId { get; set; }

        [DataMember()]
        public String v_CostCenter { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MasterServiceTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MasterServiceId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_HasVigency { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ValidInDays { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsActive { get; set; }

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
        public String v_AseguradoraOrganizationId { get; set; }

        [DataMember()]
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public Nullable<Double> r_PriceFactor { get; set; }

        [DataMember()]
        public Nullable<Double> r_MedicineDiscount { get; set; }

        [DataMember()]
        public Nullable<Double> r_HospitalBedPrice { get; set; }

        [DataMember()]
        public List<protocolcomponentDto> protocolcomponent { get; set; }

        [DataMember()]
        public List<protocolsystemuserDto> protocolsystemuser { get; set; }

        [DataMember()]
        public List<serviceDto> service { get; set; }

        [DataMember()]
        public List<serviceorderdetailDto> serviceorderdetail { get; set; }

        public protocolDto()
        {
        }

        public protocolDto(String v_ProtocolId, String v_Name, String v_EmployerOrganizationId, String v_EmployerLocationId, Nullable<Int32> i_EsoTypeId, String v_GroupOccupationId, String v_CustomerOrganizationId, String v_CustomerLocationId, String v_NombreVendedor, String v_WorkingOrganizationId, String v_WorkingLocationId, String v_CostCenter, Nullable<Int32> i_MasterServiceTypeId, Nullable<Int32> i_MasterServiceId, Nullable<Int32> i_HasVigency, Nullable<Int32> i_ValidInDays, Nullable<Int32> i_IsActive, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_AseguradoraOrganizationId, String v_ComentaryUpdate, Nullable<Double> r_PriceFactor, Nullable<Double> r_MedicineDiscount, Nullable<Double> r_HospitalBedPrice, List<protocolcomponentDto> protocolcomponent, List<protocolsystemuserDto> protocolsystemuser, List<serviceDto> service, List<serviceorderdetailDto> serviceorderdetail)
        {
			this.v_ProtocolId = v_ProtocolId;
			this.v_Name = v_Name;
			this.v_EmployerOrganizationId = v_EmployerOrganizationId;
			this.v_EmployerLocationId = v_EmployerLocationId;
			this.i_EsoTypeId = i_EsoTypeId;
			this.v_GroupOccupationId = v_GroupOccupationId;
			this.v_CustomerOrganizationId = v_CustomerOrganizationId;
			this.v_CustomerLocationId = v_CustomerLocationId;
			this.v_NombreVendedor = v_NombreVendedor;
			this.v_WorkingOrganizationId = v_WorkingOrganizationId;
			this.v_WorkingLocationId = v_WorkingLocationId;
			this.v_CostCenter = v_CostCenter;
			this.i_MasterServiceTypeId = i_MasterServiceTypeId;
			this.i_MasterServiceId = i_MasterServiceId;
			this.i_HasVigency = i_HasVigency;
			this.i_ValidInDays = i_ValidInDays;
			this.i_IsActive = i_IsActive;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_AseguradoraOrganizationId = v_AseguradoraOrganizationId;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.r_PriceFactor = r_PriceFactor;
			this.r_MedicineDiscount = r_MedicineDiscount;
			this.r_HospitalBedPrice = r_HospitalBedPrice;
			this.protocolcomponent = protocolcomponent;
			this.protocolsystemuser = protocolsystemuser;
			this.service = service;
			this.serviceorderdetail = serviceorderdetail;
        }
    }
}
