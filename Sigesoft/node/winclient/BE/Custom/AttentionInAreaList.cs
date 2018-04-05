using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
  public class AttentionInAreaList
    {
        public string v_AttentionInAreaId { get; set; }
        public int i_NodeId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_Name { get; set; }
        public string v_OfficeNumber { get; set; }
        public string v_Pacient { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
