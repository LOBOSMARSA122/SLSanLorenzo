using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class MedicalExamList
    {
        public string v_MedicalExamId { get; set; }
        public string v_Name { get; set; }
        public string v_IsGroupName { get; set; }
        public int i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }
        public int i_DiagnosableId { get; set; }
        public string v_DiagnosableName { get; set; }
        public int? i_ComponentTypeId { get; set; }
        public string v_ComponentTypeName { get; set; }
        public float? r_BasePrice { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }


        // jerarquia

        public List<Group> Groups { get; set; }
        public int i_UIIndex { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string v_AuxiliaryExamId { get; set; }

        public string v_ServiceId { get; set; }



        public float? r_Price { get; set; }
        public string v_ComponentId { get; set; }
        public bool AtSchool { get; set; }
        public bool Adicional { get; set; }
        public bool Condicional { get; set; }
        public int Operador { get; set; }
        public int? i_Age { get; set; }
        public int? i_OperatorId { get; set; }
        public int? i_GenderId { get; set; }

    }
}
