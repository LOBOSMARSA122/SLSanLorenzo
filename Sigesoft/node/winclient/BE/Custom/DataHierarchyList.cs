using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class DataHierarchyList
    {        
        public int i_GroupId { get; set; }
       
        public int i_ItemId { get; set; }

        public string v_Value1 { get; set; }

        public string v_Value2 { get; set; }

        public int i_ParentItemId { get; set; }

        public string v_ParentItemName { get; set; }

        public string v_CreationUser { get; set; }

        public string v_UpdateUser { get; set; }

        public DateTime? d_CreationDate { get; set; }
     
        public DateTime? d_UpdateDate { get; set; }

        public int? i_IsDeleted { get; set; }
    }
}
