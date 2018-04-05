using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MedicationList
    {      
        public string v_MedicationId { get; set; }
        public string v_ProductId { get; set; }
        public string v_ServiceId { get; set; }
        public float? r_Quantity { get; set; }
        public string v_Doses { get; set; }    
        public int? i_ViaId { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_ProductName { get; set; }
        public string v_GenericName { get; set; }
        public string v_PresentationName { get; set; }
        public string v_ViaName { get; set; }

    }
}
