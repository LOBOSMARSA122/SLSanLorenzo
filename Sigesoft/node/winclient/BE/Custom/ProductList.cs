using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class ProductList
    {
        
        public string v_ProductId{ get; set; }
        
        public int i_CategoryId{ get; set; }
        
        public string v_CategoryName{ get; set; }
        
        public string v_Name{ get; set; }
        
        public string v_GenericName{ get; set; }
        
        public string v_BarCode{ get; set; }
        
        public string v_ProductCode{ get; set; }
        
        public string v_Brand{ get; set; }
        
        public string v_Model{ get; set; }
        
        public string v_SerialNumber{ get; set; }
        
        public DateTime d_ExpirationDate{ get; set; }
        
        public int i_MeasurementUnitId{ get; set; }
        
        public decimal r_ReferentialCostPrice{ get; set; }
        
        public decimal r_ReferentialSalesPrice{ get; set; }
        
        public string v_Presentation{ get; set; }
        
        public string v_CreationUser{ get; set; }
        
        public string v_UpdateUser{ get; set; }
        
        public DateTime? d_CreationDate{ get; set; }
        
        public DateTime? d_UpdateDate{ get; set; }
        
        public int? i_IsDeleted{ get; set; }
        
        public string v_ConcatenatedData{ get; set; }
        
        public float? r_StockMin{ get; set; }
        
        public float? r_StockMax{ get; set; }
        
        public float? r_StockActual{ get; set; }
    }
}
