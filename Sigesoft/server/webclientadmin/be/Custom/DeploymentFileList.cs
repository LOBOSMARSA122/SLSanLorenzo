using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class DeploymentFileList
    {
        public int i_DeploymentFileId { get; set; }
        public string v_FileName { get; set; }
        public byte b_FileData { get; set; }
        public string v_Description { get; set; }
        public int i_SoftwareComponentId { get; set; }
        public string v_TargetSoftwareComponentVersion { get; set; }
        public string v_PackageFiles { get; set; }
        public Single r_PackageSizeKb { get; set; }
    }

    public class TestList
    {
        public string v_FileName { get; set; }
        public string v_Path { get; set; }
    }
}
