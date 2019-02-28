//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/02/27 - 16:14:50
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
    public partial class calendarDto
    {
        [DataMember()]
        public String v_CalendarId { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_DateTimeCalendar { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_CircuitStartDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_EntryTimeCM { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CalendarStatusId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ServiceId { get; set; }

        [DataMember()]
        public String v_ProtocolId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NewContinuationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_LineStatusId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsVipId { get; set; }

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
        public Nullable<DateTime> d_SalidaCM { get; set; }

        [DataMember()]
        public personDto person { get; set; }

        [DataMember()]
        public serviceDto service { get; set; }

        public calendarDto()
        {
        }

        public calendarDto(String v_CalendarId, String v_PersonId, String v_ServiceId, Nullable<DateTime> d_DateTimeCalendar, Nullable<DateTime> d_CircuitStartDate, Nullable<DateTime> d_EntryTimeCM, Nullable<Int32> i_ServiceTypeId, Nullable<Int32> i_CalendarStatusId, Nullable<Int32> i_ServiceId, String v_ProtocolId, Nullable<Int32> i_NewContinuationId, Nullable<Int32> i_LineStatusId, Nullable<Int32> i_IsVipId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<DateTime> d_SalidaCM, personDto person, serviceDto service)
        {
			this.v_CalendarId = v_CalendarId;
			this.v_PersonId = v_PersonId;
			this.v_ServiceId = v_ServiceId;
			this.d_DateTimeCalendar = d_DateTimeCalendar;
			this.d_CircuitStartDate = d_CircuitStartDate;
			this.d_EntryTimeCM = d_EntryTimeCM;
			this.i_ServiceTypeId = i_ServiceTypeId;
			this.i_CalendarStatusId = i_CalendarStatusId;
			this.i_ServiceId = i_ServiceId;
			this.v_ProtocolId = v_ProtocolId;
			this.i_NewContinuationId = i_NewContinuationId;
			this.i_LineStatusId = i_LineStatusId;
			this.i_IsVipId = i_IsVipId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.d_SalidaCM = d_SalidaCM;
			this.person = person;
			this.service = service;
        }
    }
}
