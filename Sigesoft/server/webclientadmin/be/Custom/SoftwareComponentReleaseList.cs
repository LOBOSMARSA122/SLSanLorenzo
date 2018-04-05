using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class SoftwareComponentReleaseList
    {
        public int i_SoftwareComponentId { get; set;}
        public string v_SoftwareComponentVersion { get; set;}
        public int i_DeploymentFileId { get; set; }
        public DateTime d_ReleaseDate { get; set; }
        public string v_DatabaseVersionRequired { get; set; }
        public string v_ReleaseNotes { get; set; }
        public string v_AdditionalInformation1 { get; set; }
        public string v_AdditionalInformation2 { get; set; }
        public int i_IsPublished { get; set; }
        public int i_IsLastVersion { get; set; }
        public DateTime d_InsertDate { get; set; }
        public DateTime d_UpdateDate { get; set; }

    }
}
