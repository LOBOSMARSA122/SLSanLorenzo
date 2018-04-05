using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Sigesoft.Node.WinClient.BE
{
   public class DiseasesList
    {
        public string v_DiseasesId { get; set; }        
       public string v_CIE10Id { get; set; }
       public string v_Name { get; set; }
        public string v_CIE10Description1 { get; set; }
       
        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public string v_CIE10Idv_Name { get; set; }
    }
}
