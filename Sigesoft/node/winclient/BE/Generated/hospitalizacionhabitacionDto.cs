//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/01 - 08:27:05
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
    public partial class hospitalizacionhabitacionDto
    {
        [DataMember()]
        public String v_HospitalizacionHabitacionId { get; set; }

        [DataMember()]
        public String v_HopitalizacionId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_HabitacionId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_StartDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_EndDate { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Precio { get; set; }

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

        public hospitalizacionhabitacionDto()
        {
        }

        public hospitalizacionhabitacionDto(String v_HospitalizacionHabitacionId, String v_HopitalizacionId, Nullable<Int32> i_HabitacionId, Nullable<DateTime> d_StartDate, Nullable<DateTime> d_EndDate, Nullable<Decimal> d_Precio, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate)
        {
			this.v_HospitalizacionHabitacionId = v_HospitalizacionHabitacionId;
			this.v_HopitalizacionId = v_HopitalizacionId;
			this.i_HabitacionId = i_HabitacionId;
			this.d_StartDate = d_StartDate;
			this.d_EndDate = d_EndDate;
			this.d_Precio = d_Precio;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
        }
    }
}
