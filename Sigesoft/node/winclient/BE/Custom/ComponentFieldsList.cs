using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComponentFieldsList
    {
        public string v_ComponentFieldId { get; set; }
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
        public int i_Column { get; set; }

        public int? i_HasAutomaticDxId { get; set; }
        public string v_HasAutomaticDxComponentFieldsId { get; set; }

        public string v_MeasurementUnitName { get; set; }

        /// <summary>
        /// Indica si el campo es de fuente para algun calculo
        /// </summary>
        public int i_IsSourceFieldToCalculate { get; set; }
        /// <summary>
        /// Campo1 participante del calculo 
        /// </summary>
        public string v_SourceFieldToCalculateId1 { get; set; }
        /// <summary>
        /// Campo2 participante del calculo
        /// </summary>
        public string v_SourceFieldToCalculateId2 { get; set; }
        /// <summary>
        /// Campo donde se muestra el resultado del calculo
        /// </summary>
        public string v_TargetFieldOfCalculateId { get; set; }
        public string v_Formula { get; set; }
        public string v_FormulaChild { get; set; }

        public string v_SourceFieldToCalculateJoin { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public List<ComponentFieldValues> Values { get; set; }

        public List<TargetFieldOfCalculate> TargetFieldOfCalculateId { get; set; }
        public List<Formulate> Formula { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string v_ComponentName { get; set; }

        public int? i_NroDecimales { get; set; }
        public int? i_ReadOnly { get; set; }
        public int? i_Enabled { get; set; }
    }

    public class SearchComponentFieldsList
    {
        public string Nombre { get; set; }       
        public string Grupo { get; set; }
        public string Componente { get; set; }
    }
}
