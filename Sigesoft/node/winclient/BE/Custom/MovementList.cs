using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Sigesoft.Node.WinClient.BE
{
    public class MovementList
    {
        
        public int i_NodeId{ get; set; }
        
        public string v_MovementId{ get; set; }

        public string v_OrganizationId { get; set; }

        public string v_WarehouseId { get; set; }

        public string v_SupplierId { get; set; }
        
        public string v_SupplierName{ get; set; }
        
        public string v_NodeName{ get; set; }
        
        public int? i_MovementTypeId{ get; set; }
        
        public string v_MovementTypeDescription{ get; set; }
        
        public int? i_MotiveTypeId{ get; set; }
        
        public string v_MotiveTypeDescription{ get; set; }
        
        public DateTime? d_MovementDate{ get; set; }
        
        public float? r_TotalQuantity{ get; set; }
        
        public string v_ReferentDocument{ get; set; }
        
        public string v_IsProcessed{ get; set; }

        public int? i_ProcessTypeId { get; set; }

        
        public string v_CreationUser{ get; set; }
        
        public string v_UpdateUser{ get; set; }
        
        public DateTime? d_CreationDate{ get; set; }
        
        public DateTime? d_UpdateDate{ get; set; }
        
        public string v_UpdateNodeName{ get; set; }
    }
}
