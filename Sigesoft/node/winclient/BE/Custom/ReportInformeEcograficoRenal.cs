using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportInformeEcograficoRenal
    {

        public string  Nombre_Trabajador {get;set;}
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaServicio { get; set; }
        public int Edad { get; set; }

        public string ECOGRAFIA_RENAL_MOTIVO_EXAMEN { get; set; }

        public string ECOGRAFIA_RENAL_MORFOLOGIA_MOVILIDAD_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_DESCRIPCION_ANORMAL_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_ECOGENICIDAD_RINION_DERECHO { get; set; }

        public string ECOGRAFIA_RENAL_LONGITUD_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_PARENQUIMA_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_IMG_EXP_SOLIDAS_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_QUISTICAS_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_HIDRONEFROSIS_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_MICROLITIAS_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_HIDRONEFROSIS_MEDIDAD_RINION_DERECHO { get; set; }

        public string ECOGRAFIA_RENAL_CALCULOS_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_CALCULOS_MEDIDA_RINION_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_MORFOLOGIA_MOVILIDAD_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_DESCRIPCION_ANORMAL_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_ECOGENICIDAD_RINION_IZQUIERDO { get; set; }

        public string ECOGRAFIA_RENAL_LONGITUD_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_PARENQUIMA_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_IMG_EXP_SOLIDAS_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_QUISTICAS_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_MICROLITIAS_RINION_IZQUIERDO { get; set; }

        public string ECOGRAFIA_RENAL_CALCULOS_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_HIDRONEFROSIS_MEDIDAD_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_CALCULOS_MEDIDA_RINION_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_DESCRIPCION_OTROS_RIÑON_DERECHO { get; set; }
        public string ECOGRAFIA_RENAL_DESCRIPCION_OTROS_RIÑON_IZQUIERDO { get; set; }
        public string ECOGRAFIA_RENAL_REPLICACION { get; set; }
        public string ECOGRAFIA_RENAL_PAREDES { get; set; }
        public string ECOGRAFIA_RENAL_CONT_ANECOICO { get; set; }
        public string ECOGRAFIA_RENAL_IMG_EXPANSIVAS { get; set; }
        public string ECOGRAFIA_RENAL_CALCULOS_INTERIOR { get; set; }
        public string ECOGRAFIA_RENAL_CALCULOS_INTERIOR_MEDIDA { get; set; }
        public string ECOGRAFIA_RENAL_VOL_PREMICCIONAL { get; set; }
        public string ECOGRAFIA_RENAL_VOL_POSMICCIONAL { get; set; }
        public string ECOGRAFIA_RENAL_RETENCION { get; set; }
        public string ECOGRAFIA_RENAL_DESCRIPCION_VEGIGA { get; set; }
        public string ECOGRAFIA_RENAL_NIGUNA { get; set; }
        public string ECOGRAFIA_RENAL_OBSERVACIONES { get; set; }
        public string ECOGRAFIA_RENAL_CONCLUSIONES { get; set; }
        public string ECOGRAFIA_RENAL_HIDRONEFROSIS_RINION_IZQUIERDO { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string NombreUsuarioGraba { get; set; }
    }
}
