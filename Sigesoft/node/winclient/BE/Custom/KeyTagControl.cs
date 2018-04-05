using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.BE
{
    public class KeyTagControl
    {    
        public int i_ControlId { get; set; }     
        public string v_ComponentFieldsId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }

        /// <summary>
        /// Indica si el campo es de fuente para algun calculo
        /// </summary>
        public int i_IsSourceFieldToCalculate { get; set; }
        /// <summary>
        /// Campo1 participante del calculo 
        /// </summary>
        //public string v_SourceFieldToCalculateId1 { get; set; }
        ///// <summary>
        ///// Campo2 participante del calculo
        ///// </summary>
        //public string v_SourceFieldToCalculateId2 { get; set; }
        /// <summary>
        /// Campo donde se muestra el resultado del calculo
        /// </summary>
        public List<TargetFieldOfCalculate> TargetFieldOfCalculateId { get; set; }
        public string v_TargetFieldOfCalculateId { get; set; }
        /// <summary>
        /// descripción de la formula campo textual
        /// </summary>
        public List<Formulate> Formula { get; set; }
        public string v_Formula { get; set; }
        public string v_FormulaChild { get; set; }

        public string v_SourceFieldToCalculateJoin { get; set; }
        public string v_TextLabel { get; set; }
        public string v_ComponentName { get; set; }
        public string v_ComponentId { get; set; }
        
        
    }
}
