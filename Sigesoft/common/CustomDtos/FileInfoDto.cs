using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Common
{
    public class FileInfoDto
    {
        public int? Id { get; set; }
        public string PersonId { get; set; }
        public string ServiceComponentId { get; set; }   
        public string MultimediaFileId { get; set; }
        public string ServiceComponentMultimediaId { get; set; }              
        public string FileName { get; set; }
        public string Description { get; set; }
        public int Action { get; set; }
        public byte[] ByteArrayFile { get; set; }
        public byte[] ThumbnailFile { get; set; }
        public string ServiceId { get; set; }

        public string RutaCorta { get; set; }
        public string RutaLarga { get; set; }
        public string ProtocolId { get; set; }
        public string dni { get; set; }
        public DateTime? FechaServicio { get; set; }

        public string Dni { get; set; }
        public string Fecha { get; set; }
        public string Consultorio { get; set; }
    }
}
