//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/27 - 18:17:20
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
    public partial class nodedatabasestatusDto
    {
        [DataMember()]
        public Int32 i_NodeId { get; set; }

        [DataMember()]
        public String v_CurrentDatabaseVersion { get; set; }

        [DataMember()]
        public String v_PreviousDatabaseVersion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CurrentDatabaseStatus { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_LastDatabaseUpgrade { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_LastDatabaseDataSync { get; set; }

        public nodedatabasestatusDto()
        {
        }

        public nodedatabasestatusDto(Int32 i_NodeId, String v_CurrentDatabaseVersion, String v_PreviousDatabaseVersion, Nullable<Int32> i_CurrentDatabaseStatus, Nullable<DateTime> d_LastDatabaseUpgrade, Nullable<DateTime> d_LastDatabaseDataSync)
        {
			this.i_NodeId = i_NodeId;
			this.v_CurrentDatabaseVersion = v_CurrentDatabaseVersion;
			this.v_PreviousDatabaseVersion = v_PreviousDatabaseVersion;
			this.i_CurrentDatabaseStatus = i_CurrentDatabaseStatus;
			this.d_LastDatabaseUpgrade = d_LastDatabaseUpgrade;
			this.d_LastDatabaseDataSync = d_LastDatabaseDataSync;
        }
    }
}
