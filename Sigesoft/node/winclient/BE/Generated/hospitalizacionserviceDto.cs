//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/29 - 17:28:17
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
    public partial class hospitalizacionserviceDto
    {
        [DataMember()]
        public String v_HospitalizacionServiceId { get; set; }

        [DataMember()]
        public String v_HopitalizacionId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

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
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public hospitalizacionDto hospitalizacion { get; set; }

        public hospitalizacionserviceDto()
        {
        }

        public hospitalizacionserviceDto(String v_HospitalizacionServiceId, String v_HopitalizacionId, String v_ServiceId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_ComentaryUpdate, hospitalizacionDto hospitalizacion)
        {
			this.v_HospitalizacionServiceId = v_HospitalizacionServiceId;
			this.v_HopitalizacionId = v_HopitalizacionId;
			this.v_ServiceId = v_ServiceId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.hospitalizacion = hospitalizacion;
        }
    }
}
