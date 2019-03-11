using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceComponentFieldValuesList
    {     
        public string v_ServiceComponentFieldValuesId { get; set; }     
        public string v_ComponentFieldValuesId { get; set; }  
        public string v_ServiceComponentFieldsId { get; set; } 
        public string v_Value1 { get; set; }
        public string v_Value2 { get; set; }
        public int? i_Index { get; set; }
        public int? i_Value1 { get; set; }
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_ComponentFieldId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public string v_ComponentFielName { get; set; }


        public string v_Value1Name { get; set; }

        public int i_GroupId { get; set; }
        public int? i_CategoryId { get; set; }

        public string v_UnidadMedida { get; set; }
        public string v_ComponentId { get; set; }

        public byte[] fotoTipo { get; set; }

        public string v_ServicioId { get; set; }
        public DateTime? d_ServiceDate{ get; set; }

        public int? i_ServiceComponentStatusId { get; set; }
        public int? i_GenderId { get; set; }
    }
}
