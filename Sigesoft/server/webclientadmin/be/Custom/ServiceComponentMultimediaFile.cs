using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE.Custom
{
    public class ServiceComponentMultimediaFile
    {
        public string v_ServiceComponetMultimediaId { get; set; }
        public string v_MultimediaFileId { get; set; }
        public byte[] b_file { get; set; }
    }
}
