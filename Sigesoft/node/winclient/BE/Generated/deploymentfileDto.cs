//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/30 - 11:05:03
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
    public partial class deploymentfileDto
    {
        [DataMember()]
        public Int32 i_DeploymentFileId { get; set; }

        [DataMember()]
        public String v_FileName { get; set; }

        [DataMember()]
        public Byte[] b_FileData { get; set; }

        [DataMember()]
        public String v_Description { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SoftwareComponentId { get; set; }

        [DataMember()]
        public String v_TargetSoftwareComponentVersion { get; set; }

        [DataMember()]
        public String v_PackageFiles { get; set; }

        [DataMember()]
        public Nullable<Single> r_PackageSizeKb { get; set; }

        [DataMember()]
        public List<softwarecomponentreleaseDto> softwarecomponentrelease { get; set; }

        public deploymentfileDto()
        {
        }

        public deploymentfileDto(Int32 i_DeploymentFileId, String v_FileName, Byte[] b_FileData, String v_Description, Nullable<Int32> i_SoftwareComponentId, String v_TargetSoftwareComponentVersion, String v_PackageFiles, Nullable<Single> r_PackageSizeKb, List<softwarecomponentreleaseDto> softwarecomponentrelease)
        {
			this.i_DeploymentFileId = i_DeploymentFileId;
			this.v_FileName = v_FileName;
			this.b_FileData = b_FileData;
			this.v_Description = v_Description;
			this.i_SoftwareComponentId = i_SoftwareComponentId;
			this.v_TargetSoftwareComponentVersion = v_TargetSoftwareComponentVersion;
			this.v_PackageFiles = v_PackageFiles;
			this.r_PackageSizeKb = r_PackageSizeKb;
			this.softwarecomponentrelease = softwarecomponentrelease;
        }
    }
}
