//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/24 - 08:36:40
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
    public partial class ticketDto
    {
        [DataMember()]
        public String v_TicketId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_Fecha { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoCuentaId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ConCargoA { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public List<ticketdetalleDto> ticketdetalle { get; set; }

        public ticketDto()
        {
        }

        public ticketDto(String v_TicketId, String v_ServiceId, Nullable<DateTime> d_Fecha, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<Int32> i_TipoCuentaId, Nullable<Int32> i_ConCargoA, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, List<ticketdetalleDto> ticketdetalle)
        {
			this.v_TicketId = v_TicketId;
			this.v_ServiceId = v_ServiceId;
			this.d_Fecha = d_Fecha;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.i_TipoCuentaId = i_TipoCuentaId;
			this.i_ConCargoA = i_ConCargoA;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.ticketdetalle = ticketdetalle;
        }
    }
}
