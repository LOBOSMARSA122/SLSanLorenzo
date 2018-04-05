using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ProductWarehouseList
    {
        
        public string v_WarehouseId{ get; set; }
        
        public string v_WarehouseName{ get; set; }

        public string v_ProductId { get; set; }

        public string v_OrganizationId { get; set; }
        
        public float r_StockMin{ get; set; }
        
        public float r_StockMax{ get; set; }
        
        public float r_StockActual{ get; set; }
        
        public string v_ProductName{ get; set; }
        
        public string v_Brand{ get; set; }
        
        public string v_Model{ get; set; }
        
        public string v_SerialNumber{ get; set; }
        
        public int i_CategoryId{ get; set; }
        
        public string v_CategoryName{ get; set; }

        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_OrganizationName { get; set; }

        public string v_GenericName { get; set; }
        public string v_ProductCode { get; set; }
        public string v_Presentation { get; set; }

        public string v_CategoriaId { get; set; }

        public decimal? d_Comision { get; set; }

        public int? i_Cuota { get; set; }

        public decimal? d_ValorVenta { get; set; }
       
    }
}
