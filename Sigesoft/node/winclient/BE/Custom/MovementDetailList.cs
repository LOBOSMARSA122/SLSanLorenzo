using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    public class MovementDetailList
    {
        public string v_MovementNodeId { get; set; }

        public string v_MovementNodeName { get; set; }

        public string v_MovementId { get; set; }

        public string v_ProductId { get; set; }

        public string v_OrganizationId { get; set; }

        public string v_WarehouseId { get; set; }

        public int? i_MovementTypeId { get; set; }

        public string v_MovementTypeDescription { get; set; }

        public string v_ProductName { get; set; }

        public string v_WarehouseName { get; set; }

        public string v_OrganizationName { get; set; }

        public string v_CategoryName { get; set; }

        public string v_GenericName { get; set; }

        public string v_BarCode { get; set; }

        public string v_ProductCode { get; set; }

        public string v_Brand { get; set; }

        public string v_Model { get; set; }

        public string v_SerialNumber { get; set; }

        public DateTime? d_ExpirationDate { get; set; }

        public int? i_MeasurementUnitId { get; set; }

        public DateTime? d_Date { get; set; }

        public float? r_Quantity { get; set; }

        public float? r_Price { get; set; }

        public float? r_SubTotal { get; set; }

        public float? r_StockMin { get; set; }

        public float? r_StockMax { get; set; }

        public float? r_StockActual { get; set; }

        public int? i_MotiveTypeId { get; set; }

        public string v_MotiveTypeName { get; set; }

        public string v_SupplierName { get; set; }
    }
}
