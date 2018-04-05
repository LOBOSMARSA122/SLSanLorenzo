using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   
   public  class SystemParameterList
    {
       
       public int i_GroupId { get;set; }
       
       public int i_ParameterId { get; set; }
       
       public string v_Value1 { get; set; }
       
       public string v_Value2 { get; set; }
       
       public int i_ParentGroupId { get; set; }
       
       public string v_ParentGroupName { get; set; }
       
       public int i_ParentParameterId { get; set; }
       
       public string v_ParentParameterName { get; set; }
       
       public string v_CreationUser { get; set; }
       
       public string v_UpdateUser { get; set; }
       
       public DateTime? d_CreationDate { get; set; }
       
       public DateTime? d_UpdateDate { get; set; }
       
       public int? i_IsDeleted { get; set; }

       public string v_DiseasesName { get; set; }

       public string Enfermedad { get; set; }

       public string v_DiseaseName { get; set; }

       public Boolean SI { get; set; }

       public Boolean NO { get; set; }

       public Boolean ND { get; set; }

       public int i_Answer { get; set; }


    }

}
