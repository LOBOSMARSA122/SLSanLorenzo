//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/01 - 09:35:24
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
    public partial class rolenodecomponentprofileDto
    {
        [DataMember()]
        public String v_RoleNodeComponentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NodeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_RoleId { get; set; }

        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Read { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Write { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Dx { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Approved { get; set; }

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
        public componentDto component { get; set; }

        [DataMember()]
        public rolenodeDto rolenode { get; set; }

        public rolenodecomponentprofileDto()
        {
        }

        public rolenodecomponentprofileDto(String v_RoleNodeComponentId, Nullable<Int32> i_NodeId, Nullable<Int32> i_RoleId, String v_ComponentId, Nullable<Int32> i_Read, Nullable<Int32> i_Write, Nullable<Int32> i_Dx, Nullable<Int32> i_Approved, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, componentDto component, rolenodeDto rolenode)
        {
			this.v_RoleNodeComponentId = v_RoleNodeComponentId;
			this.i_NodeId = i_NodeId;
			this.i_RoleId = i_RoleId;
			this.v_ComponentId = v_ComponentId;
			this.i_Read = i_Read;
			this.i_Write = i_Write;
			this.i_Dx = i_Dx;
			this.i_Approved = i_Approved;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.component = component;
			this.rolenode = rolenode;
        }
    }
}
