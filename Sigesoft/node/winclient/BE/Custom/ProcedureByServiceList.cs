using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProcedureByServiceList
    {       
        public string v_ProcedureByServiceId { get; set; }      
        public string v_ServiceId { get; set; }     
        public int? i_ProcedureId { get; set; }

        public int? i_ItemId { get; set; }
        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_ProcedureName { get; set; }

    }
}
