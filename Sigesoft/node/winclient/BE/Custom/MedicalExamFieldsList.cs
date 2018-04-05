using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MedicalExamFieldsList
    {
        public string v_MedicalExamFieldId { get; set; }
        public string v_ComponentId { get; set; }        
        public string v_Group { get; set; }

        public string v_TextLabel { get; set; }
        public int i_LabelWidth { get; set; }
        public string v_DefaultText { get; set; }
        public int i_ControlId { get; set; }
        public int i_GroupId { get; set; }
        public int i_ItemId { get; set; }
        public int i_ControlWidth { get; set; }
        public int i_HeightControl { get; set; }
        public int i_MaxLenght { get; set; }
        public int i_IsRequired { get; set; }
        public string v_IsRequired { get; set; }
        public int i_IsCalculate { get; set; }
        public int i_Order { get; set; }
        public int i_MeasurementUnitId { get; set; }
        public Single r_ValidateValue1 { get; set; }
        public Single r_ValidateValue2 { get; set; }  


        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_ComponentFieldId { get; set; }
    }
}
