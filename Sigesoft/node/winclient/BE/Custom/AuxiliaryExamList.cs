using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class AuxiliaryExamList
    {    
        public String v_AuxiliaryExamId { get; set; }   
        public String v_ServiceId { get; set; }    
        public String v_ComponentId { get; set; }    
        public Nullable<Int32> i_IsDeleted { get; set; }      
        public Nullable<Int32> i_InsertUserId { get; set; }     
        public Nullable<DateTime> d_InsertDate { get; set; }
        public Nullable<Int32> i_UpdateUserId { get; set; }   
        public Nullable<DateTime> d_UpdateDate { get; set; }  
        public int? i_CategoryId { get; set; }
        public String v_ComponentName { get; set; }
        public String v_CategoryName { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }
    }
}
