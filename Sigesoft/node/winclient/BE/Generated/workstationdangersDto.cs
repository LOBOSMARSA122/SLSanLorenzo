//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/31 - 17:06:11
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
    public partial class workstationdangersDto
    {
        [DataMember()]
        public String v_WorkstationDangersId { get; set; }

        [DataMember()]
        public String v_HistoryId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DangerId { get; set; }

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
        public Nullable<Int32> i_NoiseSource { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NoiseLevel { get; set; }

        [DataMember()]
        public String v_TimeOfExposureToNoise { get; set; }

        [DataMember()]
        public historyDto history { get; set; }

        public workstationdangersDto()
        {
        }

        public workstationdangersDto(String v_WorkstationDangersId, String v_HistoryId, Nullable<Int32> i_DangerId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_NoiseSource, Nullable<Int32> i_NoiseLevel, String v_TimeOfExposureToNoise, historyDto history)
        {
			this.v_WorkstationDangersId = v_WorkstationDangersId;
			this.v_HistoryId = v_HistoryId;
			this.i_DangerId = i_DangerId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.i_NoiseSource = i_NoiseSource;
			this.i_NoiseLevel = i_NoiseLevel;
			this.v_TimeOfExposureToNoise = v_TimeOfExposureToNoise;
			this.history = history;
        }
    }
}
