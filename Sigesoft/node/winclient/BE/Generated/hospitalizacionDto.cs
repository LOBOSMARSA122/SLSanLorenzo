//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/07 - 10:47:03
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
    public partial class hospitalizacionDto
    {
        [DataMember()]
        public String v_HopitalizacionId { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaIngreso { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaAlta { get; set; }

        [DataMember()]
        public String v_Comentario { get; set; }

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
        public List<hospitalizacionserviceDto> hospitalizacionservice { get; set; }

        public hospitalizacionDto()
        {
        }

        public hospitalizacionDto(String v_HopitalizacionId, String v_PersonId, Nullable<DateTime> d_FechaIngreso, Nullable<DateTime> d_FechaAlta, String v_Comentario, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, List<hospitalizacionserviceDto> hospitalizacionservice)
        {
			this.v_HopitalizacionId = v_HopitalizacionId;
			this.v_PersonId = v_PersonId;
			this.d_FechaIngreso = d_FechaIngreso;
			this.d_FechaAlta = d_FechaAlta;
			this.v_Comentario = v_Comentario;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.hospitalizacionservice = hospitalizacionservice;
        }
    }
}
