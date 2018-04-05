using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
  public  class CIE10List
    {
        public string v_CIE10Id { get; set; }
        public string v_CIE10Description1 { get; set; }
        public string v_CIE10Description2 { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public int? i_IsDeleted { get; set; }
    }
}
