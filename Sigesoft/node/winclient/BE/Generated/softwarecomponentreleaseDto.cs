//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/29 - 17:15:04
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
    public partial class softwarecomponentreleaseDto
    {
        [DataMember()]
        public Int32 i_SoftwareComponentId { get; set; }

        [DataMember()]
        public String v_SoftwareComponentVersion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DeploymentFileId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_ReleaseDate { get; set; }

        [DataMember()]
        public String v_DatabaseVersionRequired { get; set; }

        [DataMember()]
        public String v_ReleaseNotes { get; set; }

        [DataMember()]
        public String v_AdditionalInformation1 { get; set; }

        [DataMember()]
        public String v_AdditionalInformation2 { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsPublished { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsLastVersion { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public deploymentfileDto deploymentfile { get; set; }

        public softwarecomponentreleaseDto()
        {
        }

        public softwarecomponentreleaseDto(Int32 i_SoftwareComponentId, String v_SoftwareComponentVersion, Nullable<Int32> i_DeploymentFileId, Nullable<DateTime> d_ReleaseDate, String v_DatabaseVersionRequired, String v_ReleaseNotes, String v_AdditionalInformation1, String v_AdditionalInformation2, Nullable<Int32> i_IsPublished, Nullable<Int32> i_IsLastVersion, Nullable<DateTime> d_InsertDate, Nullable<DateTime> d_UpdateDate, deploymentfileDto deploymentfile)
        {
			this.i_SoftwareComponentId = i_SoftwareComponentId;
			this.v_SoftwareComponentVersion = v_SoftwareComponentVersion;
			this.i_DeploymentFileId = i_DeploymentFileId;
			this.d_ReleaseDate = d_ReleaseDate;
			this.v_DatabaseVersionRequired = v_DatabaseVersionRequired;
			this.v_ReleaseNotes = v_ReleaseNotes;
			this.v_AdditionalInformation1 = v_AdditionalInformation1;
			this.v_AdditionalInformation2 = v_AdditionalInformation2;
			this.i_IsPublished = i_IsPublished;
			this.i_IsLastVersion = i_IsLastVersion;
			this.d_InsertDate = d_InsertDate;
			this.d_UpdateDate = d_UpdateDate;
			this.deploymentfile = deploymentfile;
        }
    }
}
