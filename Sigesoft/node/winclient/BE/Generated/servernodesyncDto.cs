//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/26 - 17:36:17
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
    public partial class servernodesyncDto
    {
        [DataMember()]
        public Int32 i_NodeId { get; set; }

        [DataMember()]
        public String v_DataSyncVersion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DataSyncFrecuency { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Enabled { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_LastSuccessfulDataSync { get; set; }

        [DataMember()]
        public Nullable<Int32> i_LastServerProcessStatus { get; set; }

        [DataMember()]
        public Nullable<Int32> i_LastNodeProcessStatus { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_NextDataSync { get; set; }

        [DataMember()]
        public String v_LastServerPackageFileName { get; set; }

        [DataMember()]
        public String v_LastServerProcessErrorMessage { get; set; }

        [DataMember()]
        public String v_LastNodePackageFileName { get; set; }

        [DataMember()]
        public String v_LastNodeProcessErrorMessage { get; set; }

        public servernodesyncDto()
        {
        }

        public servernodesyncDto(Int32 i_NodeId, String v_DataSyncVersion, Nullable<Int32> i_DataSyncFrecuency, Nullable<Int32> i_Enabled, Nullable<DateTime> d_LastSuccessfulDataSync, Nullable<Int32> i_LastServerProcessStatus, Nullable<Int32> i_LastNodeProcessStatus, Nullable<DateTime> d_NextDataSync, String v_LastServerPackageFileName, String v_LastServerProcessErrorMessage, String v_LastNodePackageFileName, String v_LastNodeProcessErrorMessage)
        {
			this.i_NodeId = i_NodeId;
			this.v_DataSyncVersion = v_DataSyncVersion;
			this.i_DataSyncFrecuency = i_DataSyncFrecuency;
			this.i_Enabled = i_Enabled;
			this.d_LastSuccessfulDataSync = d_LastSuccessfulDataSync;
			this.i_LastServerProcessStatus = i_LastServerProcessStatus;
			this.i_LastNodeProcessStatus = i_LastNodeProcessStatus;
			this.d_NextDataSync = d_NextDataSync;
			this.v_LastServerPackageFileName = v_LastServerPackageFileName;
			this.v_LastServerProcessErrorMessage = v_LastServerProcessErrorMessage;
			this.v_LastNodePackageFileName = v_LastNodePackageFileName;
			this.v_LastNodeProcessErrorMessage = v_LastNodeProcessErrorMessage;
        }
    }
}
