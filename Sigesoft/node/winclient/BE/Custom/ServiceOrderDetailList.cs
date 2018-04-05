using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceOrderDetailList
    {

        public string v_ServiceOrderDetailId { get; set; }
        public string v_ServiceOrderId{get;set;}
        public string v_ProtocolId { get; set; }
        public string v_ProtocolName { get; set; }
        public float? r_ProtocolPrice { get; set; }
        public int? i_NumberOfWorkerProtocol { get; set; }
        public float? r_Total { get; set; }

        public Nullable<Int32> i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }




    }
}
