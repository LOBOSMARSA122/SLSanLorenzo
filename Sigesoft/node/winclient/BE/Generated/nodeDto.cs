//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/07 - 10:47:08
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
    public partial class nodeDto
    {
        [DataMember()]
        public Int32 i_NodeId { get; set; }

        [DataMember()]
        public String v_Description { get; set; }

        [DataMember()]
        public String v_GeografyLocationId { get; set; }

        [DataMember()]
        public String v_GeografyLocationDescription { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NodeTypeId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_BeginDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_EndDate { get; set; }

        [DataMember()]
        public String v_PharmacyWarehouseId { get; set; }

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
        public List<attentioninareaDto> attentioninarea { get; set; }

        [DataMember()]
        public List<nodeorganizationlocationprofileDto> nodeorganizationlocationprofile { get; set; }

        [DataMember()]
        public List<nodeorganizationlocationwarehouseprofileDto> nodeorganizationlocationwarehouseprofile { get; set; }

        [DataMember()]
        public List<nodeorganizationprofileDto> nodeorganizationprofile { get; set; }

        [DataMember()]
        public List<nodeserviceprofileDto> nodeserviceprofile { get; set; }

        [DataMember()]
        public List<rolenodeDto> rolenode { get; set; }

        public nodeDto()
        {
        }

        public nodeDto(Int32 i_NodeId, String v_Description, String v_GeografyLocationId, String v_GeografyLocationDescription, Nullable<Int32> i_NodeTypeId, Nullable<DateTime> d_BeginDate, Nullable<DateTime> d_EndDate, String v_PharmacyWarehouseId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, List<attentioninareaDto> attentioninarea, List<nodeorganizationlocationprofileDto> nodeorganizationlocationprofile, List<nodeorganizationlocationwarehouseprofileDto> nodeorganizationlocationwarehouseprofile, List<nodeorganizationprofileDto> nodeorganizationprofile, List<nodeserviceprofileDto> nodeserviceprofile, List<rolenodeDto> rolenode)
        {
			this.i_NodeId = i_NodeId;
			this.v_Description = v_Description;
			this.v_GeografyLocationId = v_GeografyLocationId;
			this.v_GeografyLocationDescription = v_GeografyLocationDescription;
			this.i_NodeTypeId = i_NodeTypeId;
			this.d_BeginDate = d_BeginDate;
			this.d_EndDate = d_EndDate;
			this.v_PharmacyWarehouseId = v_PharmacyWarehouseId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.attentioninarea = attentioninarea;
			this.nodeorganizationlocationprofile = nodeorganizationlocationprofile;
			this.nodeorganizationlocationwarehouseprofile = nodeorganizationlocationwarehouseprofile;
			this.nodeorganizationprofile = nodeorganizationprofile;
			this.nodeserviceprofile = nodeserviceprofile;
			this.rolenode = rolenode;
        }
    }
}
