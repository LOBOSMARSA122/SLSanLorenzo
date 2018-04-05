using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
  public  class AttentionInAreaList
    {
      public string v_AttentionInAreaId { get; set; }
      public int i_NodeId { get; set; }
      public string v_NodeName { get; set; }
      public string v_ComponentId { get; set; }
      public string v_ComponentName { get; set; }
      public string v_Name { get; set; }
      public string v_OfficeNumber { get; set; }
      public string i_IsDeleted { get; set; }
      public string v_CreationUser { get; set; }
      public string v_UpdateUser { get; set; }
      public DateTime? d_CreationDate { get; set; }
      public DateTime? d_UpdateDate { get; set; }
    }
}
