using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
    public class MovementList
    {
        [DataMember]
        public int i_NodeId { get; set; }
        [DataMember]
        public int i_MovementId { get; set; }
        [DataMember]
        public int? i_OrganizationId { get; set; }
        [DataMember]
        public int? i_WarehouseId { get; set; }
        [DataMember]
        public int? i_SupplierId { get; set; }
        [DataMember]
        public string v_SupplierName { get; set; }
        [DataMember]
        public string v_NodeName { get; set; }
        [DataMember]
        public int? i_MovementTypeId { get; set; }
        [DataMember]
        public string v_MovementTypeDescription { get; set; }
        [DataMember]
        public int? i_MotiveTypeId { get; set; }
        [DataMember]
        public string v_MotiveTypeDescription { get; set; }
        [DataMember]
        public DateTime? d_MovementDate { get; set; }
        [DataMember]
        public float? r_TotalQuantity { get; set; }
        [DataMember]
        public string v_ReferentDocument { get; set; }
        [DataMember]
        public string v_IsProcessed { get; set; }
        [DataMember]
        public int? i_IsProcessed { get; set; }

        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
        [DataMember]
        public string v_UpdateNodeName { get; set; }
    }

    [DataContract]
    public class MovementDetailList
    {
        [DataMember]
        public int i_MovementNodeId { get; set; }
        [DataMember]
        public string v_MovementNodeName { get; set; }
        [DataMember]
        public int i_MovementId { get; set; }
        [DataMember]
        public int i_ProductId { get; set; }
        [DataMember]
        public int i_OrganizationId { get; set; }
        [DataMember]
        public int i_WarehouseId { get; set; }
        [DataMember]
        public int? i_MovementTypeId { get; set; }
        [DataMember]
        public string v_MovementTypeDescription { get; set; }
        [DataMember]
        public string v_ProductName { get; set; }
        [DataMember]
        public string v_WarehouseName { get; set; }
        [DataMember]
        public string v_OrganizationName { get; set; }
        [DataMember]
        public string v_CategoryName { get; set; }
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
        public DateTime? d_ExpirationDate { get; set; }
        [DataMember]
        public int? i_MeasurementUnitId { get; set; }
        [DataMember]
        public DateTime? d_Date { get; set; }
        [DataMember]
        public float? r_Quantity { get; set; }
        [DataMember]
        public float? r_Price { get; set; }
        [DataMember]
        public float? r_SubTotal { get; set; }
        [DataMember]
        public float? r_StockMin { get; set; }
        [DataMember]
        public float? r_StockMax { get; set; }
        [DataMember]
        public float? r_StockActual { get; set; }
    }
}
