//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/23 - 10:30:54
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
    public partial class planintegralDto
    {
        [DataMember()]
        public String v_PlanIntegral { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoId { get; set; }

        [DataMember()]
        public String v_Descripcion { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_Fecha { get; set; }

        [DataMember()]
        public String v_Lugar { get; set; }

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

        public planintegralDto()
        {
        }

        public planintegralDto(String v_PlanIntegral, String v_PersonId, Nullable<Int32> i_TipoId, String v_Descripcion, Nullable<DateTime> d_Fecha, String v_Lugar, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate)
        {
			this.v_PlanIntegral = v_PlanIntegral;
			this.v_PersonId = v_PersonId;
			this.i_TipoId = i_TipoId;
			this.v_Descripcion = v_Descripcion;
			this.d_Fecha = d_Fecha;
			this.v_Lugar = v_Lugar;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
        }
    }
}
