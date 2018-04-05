using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
   public class ProductWarehouseList
    {
        [DataMember]
        public string v_WarehouseId { get; set; }
        [DataMember]
        public string v_WarehouseName { get; set; }
        [DataMember]
        public string v_ProductId { get; set; }
        [DataMember]
        public string v_OrganizationId { get; set; }
        [DataMember]
        public float r_StockMin { get; set; }
        [DataMember]
        public float r_StockMax { get; set; }
        [DataMember]
        public float r_StockActual { get; set; }

        [DataMember]
        public string v_ProductName { get; set; }
        [DataMember]
        public string v_Brand { get; set; }
        [DataMember]
        public string v_Model { get; set; }
        [DataMember]
        public string v_SerialNumber { get; set; }
        [DataMember]
        public int i_CategoryId { get; set; }
        [DataMember]
        public string v_CategoryName { get; set; }
    }
}
