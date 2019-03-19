//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/14 - 10:38:36
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract()]
    public partial class warehouseDto
    {
        [DataMember()]
        public String v_WarehouseId { get; set; }

        [DataMember()]
        public String v_OrganizationId { get; set; }

        [DataMember()]
        public String v_LocationId { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

        [DataMember()]
        public String v_AdditionalInformation { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CostCenterId { get; set; }

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
        public List<movementDto> movement { get; set; }

        [DataMember()]
        public List<movementdetailDto> movementdetail { get; set; }

        [DataMember()]
        public List<nodeorganizationlocationwarehouseprofileDto> nodeorganizationlocationwarehouseprofile { get; set; }

        [DataMember()]
        public List<productwarehouseDto> productwarehouse { get; set; }

        [DataMember()]
        public List<restrictedwarehouseprofileDto> restrictedwarehouseprofile { get; set; }

        [DataMember()]
        public locationDto location { get; set; }

        [DataMember()]
        public organizationDto organization { get; set; }

        public warehouseDto()
        {
        }

        public warehouseDto(String v_WarehouseId, String v_OrganizationId, String v_LocationId, String v_Name, String v_AdditionalInformation, Nullable<Int32> i_CostCenterId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, List<movementDto> movement, List<movementdetailDto> movementdetail, List<nodeorganizationlocationwarehouseprofileDto> nodeorganizationlocationwarehouseprofile, List<productwarehouseDto> productwarehouse, List<restrictedwarehouseprofileDto> restrictedwarehouseprofile, locationDto location, organizationDto organization)
        {
			this.v_WarehouseId = v_WarehouseId;
			this.v_OrganizationId = v_OrganizationId;
			this.v_LocationId = v_LocationId;
			this.v_Name = v_Name;
			this.v_AdditionalInformation = v_AdditionalInformation;
			this.i_CostCenterId = i_CostCenterId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.movement = movement;
			this.movementdetail = movementdetail;
			this.nodeorganizationlocationwarehouseprofile = nodeorganizationlocationwarehouseprofile;
			this.productwarehouse = productwarehouse;
			this.restrictedwarehouseprofile = restrictedwarehouseprofile;
			this.location = location;
			this.organization = organization;
        }
    }
}
