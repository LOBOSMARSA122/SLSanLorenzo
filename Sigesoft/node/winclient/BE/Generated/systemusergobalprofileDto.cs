//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/04/05 - 15:03:14
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
    public partial class systemusergobalprofileDto
    {
        [DataMember()]
        public Int32 i_SystemUserId { get; set; }

        [DataMember()]
        public Int32 i_ApplicationHierarchyId { get; set; }

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
        public systemuserDto systemuser { get; set; }

        [DataMember()]
        public applicationhierarchyDto applicationhierarchy { get; set; }

        public systemusergobalprofileDto()
        {
        }

        public systemusergobalprofileDto(Int32 i_SystemUserId, Int32 i_ApplicationHierarchyId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, systemuserDto systemuser, applicationhierarchyDto applicationhierarchy)
        {
			this.i_SystemUserId = i_SystemUserId;
			this.i_ApplicationHierarchyId = i_ApplicationHierarchyId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.systemuser = systemuser;
			this.applicationhierarchy = applicationhierarchy;
        }
    }
}
