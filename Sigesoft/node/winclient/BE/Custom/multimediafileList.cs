using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class multimediafileList
    {
        public string v_MultimediaFileId { get; set; }
        public string v_FileName { get; set; }
        public byte[] b_File { get; set; }
        public DateTime? FechaServicio { get; set; }
        public int? CategoryId { get; set; }
    }
}
