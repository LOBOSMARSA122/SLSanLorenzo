using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportEvaOsteo
    {
        public string EVA_OSTEO_ESCOLIOSIS_ID { get; set; }
        public  string EVA_OSTEO_CIFOSIS_ID {get;set;}
        public  string EVA_OSTEO_LORDOSIS_ID {get;set;}
        public  string EVA_OSTEO_COMENTARIOS_1_ID {get;set;}

        public  string EVA_OSTEO_FLEXION_HACIA_DELANTE_ID {get;set;}
        public  string EVA_OSTEO_FLEXION_HACIA_DELANTE_DESC_ID {get;set;}

        public  string EVA_OSTEO_HIPERTENSION_ID {get;set;}
        public  string EVA_OSTEO_HIPERTENSION_DESC_ID {get;set;}

        public  string EVA_OSTEO_FLEXION_LATERAL_IZQ_ID {get;set;}
        public  string EVA_OSTEO_FLEXION_LATERAL_IZQ_DESC_ID {get;set;}

        public  string EVA_OSTEO_FLEXION_LATERAL_DER_ID {get;set;}
        public  string EVA_OSTEO_FLEXION_LATERAL_DER_DESC_ID {get;set;}

        public  string EVA_OSTEO_ROTACION_IZQUIERDO_ID {get;set;}
        public  string EVA_OSTEO_ROTACION_IZQUIERDO_DESC_ID {get;set;}

        public  string EVA_OSTEO_ROTACION_DERECHO_ID {get;set;}
        public  string EVA_OSTEO_ROTACION_DERECHO_DESC_ID {get;set;}

        public  string EVA_OSTEO_COMENTARIOS_2_ID {get;set;}


        public  string EVA_OSTEO_CORRIENDO_ID {get;set;}
        public  string EVA_OSTEO_CORRIENDO_DESC_ID {get;set;}
        public  string EVA_OSTEO_CAMINANDO_ID {get;set;}
        public  string EVA_OSTEO_CAMINANDO_DESC_ID {get;set;}

        public  string EVA_OSTEO_CUNCLILLA_3_MIN_ID {get;set;}
        public  string EVA_OSTEO_CUNCLILLA_3_MIN_DESC_ID {get;set;}


        public  string EVA_OSTEO_CODO_DER_ID {get;set;}
        public  string EVA_OSTEO_CODO_IZQ_ID {get;set;}
        public  string EVA_OSTEO_CODO_DESC_ID {get;set;}

        public  string EVA_OSTEO_RODILLA_DER_ID {get;set;}
        public  string EVA_OSTEO_RODILLA_IZQ_ID {get;set;}
        public  string EVA_OSTEO_RODILLA_DESC_ID {get;set;}

        public  string EVA_OSTEO_FUERZA_ID {get;set;}
        public  string EVA_OSTEO_FUERZA_DESC_ID {get;set;}


        public  string EVA_OSTEO_ABDOMINAL_EXC_ID {get;set;}
        public  string EVA_OSTEO_ABDOMINAL_BUE_ID {get;set;}
        public  string EVA_OSTEO_ABDOMINAL_REG_ID {get;set;}
        public  string EVA_OSTEO_ABDOMINAL_POB_ID {get;set;}
        public  string EVA_OSTEO_ABDOMINAL_DESC_ID {get;set;}

        public  string EVA_OSTEO_CADERA_EXC_ID {get;set;}
        public  string EVA_OSTEO_CADERA_BUE_ID {get;set;}
        public  string EVA_OSTEO_CADERA_REG_ID {get;set;}
        public  string EVA_OSTEO_CADERA_POB_ID {get;set;}
        public  string EVA_OSTEO_CADERA_DESC_ID {get;set;}

        public  string EVA_OSTEO_MUSLO_EXC_ID {get;set;}
        public  string EVA_OSTEO_MUSLO_BUE_ID {get;set;}
        public  string EVA_OSTEO_MUSLO_REG_ID {get;set;}
        public  string EVA_OSTEO_MUSLO_POB_ID {get;set;}
        public  string EVA_OSTEO_MUSLO_DESC_ID {get;set;}

        public  string EVA_OSTEO_LATERAL_EXC_ID {get;set;}
        public  string EVA_OSTEO_LATERAL_BUE_ID {get;set;}
        public  string EVA_OSTEO_LATERAL_REG_ID {get;set;}
        public  string EVA_OSTEO_LATERAL_POB_ID {get;set;}
        public  string EVA_OSTEO_LATERAL_DESC_ID {get;set;}


        public  string EVA_OSTEO_RESULTADO_1_ID {get;set;}
        public  string EVA_OSTEO_RESULTADO_2_ID {get;set;}

        public  string EVA_OSTEO_COMENTARIOS_ID {get;set;}

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string Trabajador { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string Codigo { get; set; }
        public byte[] FirmaDoctor { get; set; }
        public string NombreUsuarioGraba { get; set; }
    }
}
