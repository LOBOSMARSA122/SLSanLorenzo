using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class Categoria
    {
        public int? i_IsProcessed { get; set; }
        public string v_CategoryName { get; set; }
        public string v_CodigoSegus { get; set; }
        public string v_SubCategoryName { get; set; }
        public int? i_CategoryId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public string v_ServiceComponentStatusName { get; set; }
        public string v_QueueStatusName { get; set; }
        public int i_ServiceComponentStatusId { get; set; }
        public int i_ApprovedInsertUserId { get; set; }
        
        public string ApprovedUpdateUser { get; set; }
        public List<ComponentDetailList> Componentes {get; set;}
    }
}
