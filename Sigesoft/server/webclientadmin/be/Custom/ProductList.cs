using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
   public class ProductList
    {
        [DataMember]
        public int i_ProductId { get; set; }
        [DataMember]
        public int i_CategoryId { get; set; }
        [DataMember]
        public string v_CategoryName { get; set; }
        [DataMember]
        public string v_Name { get; set; }
        [DataMember]
        public string v_GenericName { get; set; }
        [DataMember]
        public string v_BarCode { get; set; }
        [DataMember]
        public string v_ProductCode { get; set; }
        [DataMember]
        public string v_Brand { get; set; }
        [DataMember]
        public string v_Model { get; set; }
        [DataMember]
        public string v_SerialNumber { get; set; }
        [DataMember]
        public DateTime d_ExpirationDate { get; set; }
        [DataMember]
        public int i_MeasurementUnitId { get; set; }
        [DataMember]
        public decimal r_ReferentialCostPrice { get; set; }
        [DataMember]
        public decimal r_ReferentialSalesPrice { get; set; }
        [DataMember]
        public string v_Presentation { get; set; }
        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
        [DataMember]
        public int? i_IsDeleted { get; set; }
        [DataMember]
        public string v_ConcatenatedData { get; set; }
        [DataMember]
        public float? r_StockMin { get; set; }
        [DataMember]
        public float? r_StockMax { get; set; }
        [DataMember]
        public float? r_StockActual { get; set; }
    }
}
