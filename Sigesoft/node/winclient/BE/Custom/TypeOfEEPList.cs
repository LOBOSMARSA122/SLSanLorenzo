using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class TypeOfEEPList
    {
        
        public String v_TypeofEEPId { get; set; }
        public string v_History { get; set; }
        public Nullable<Int32> i_TypeofEEPId { get; set; }
        public string v_TypeofEEPName { get; set; }        
        public Nullable<Single> r_Percentage { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
