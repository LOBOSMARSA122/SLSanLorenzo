using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceComponentFieldsList
    {

        public string v_ServiceComponentFieldsId { get; set; }
        public string v_ComponentFieldsId { get; set; }
        public string v_ServiceComponentId { get; set; }    
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public int? i_HasAutomaticDxId { get; set; }

        public List<ServiceComponentFieldValuesList> ServiceComponentFieldValues { get; set; }

        // Datos de valores de campos tabla [ServiceComponentFieldValues]
        public string v_ServiceComponentFieldValuesId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }      
        public string v_Value1 { get; set; }
        public string v_ComponentFielName { get; set; }  // v_TextLabel

        // Conclusiones + Diagnosticos
        public string v_ConclusionAndDiagnostic { get; set; }

        public string v_Value1Name { get; set; }
        public int? i_GroupIdd { get; set; }
        public int i_GroupId { get; set; }
        public string v_MeasurementUnitName { get; set; }

        public string v_ComponentId { get; set; }

        public string v_ServiceId { get; set; }
        public string v_Paciente { get; set; }       
        public DateTime? d_ServiceDate { get; set; }
        public string v_ComponentFieldId { get; set; }

    }
}
