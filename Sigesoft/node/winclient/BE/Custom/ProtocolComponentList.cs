using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProtocolComponentList
    {
        public string v_ProtocolId { get; set; }
        public string v_ProtocolComponentId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_Gender { get; set; }
        public string v_IsConditional { get; set; }
        public string v_Operator { get; set; }
        public string v_ComponentTypeName { get; set; }
        public string v_CategoryName { get; set; }

        public string v_Porcentajes { get; set; }
        public int i_ServiceComponentStatusId { get; set; }
        public string v_ServiceComponentStatusName { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public float? r_Price { get; set; }
        public int? i_Age { get; set; }
        public int? i_GenderId { get; set; }
        public int? i_GrupoEtarioId { get; set; }
        public int? i_IsConditionalId { get; set; }
        public int? i_OperatorId { get; set; }
        public int? i_ComponentTypeId { get; set; }

        public int? i_IsConditionalIMC { get; set; }
        public decimal? r_Imc { get; set; }

        
        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public int? i_isAdditional { get; set; }
        public int? i_CategoryId { get;set;}
    }
}
