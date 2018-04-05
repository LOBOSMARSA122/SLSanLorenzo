using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ReportCuestionarioActividadFisica
    {
       public string Nombre_Trabajador { get; set; }
       public DateTime FechaServicio { get; set; }
       public string IdServicio { get; set; }

       public string CUESTIONARIO_ACTIVIDAD_FISICA_1 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_2 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_3 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_4 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_5 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_6 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_7 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_8 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_9 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_10 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_11 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_12 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_13 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_14 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_15 { get; set; }
       public string CUESTIONARIO_ACTIVIDAD_FISICA_16 { get; set; }
       public byte[] b_Logo { get; set; }
       public string EmpresaPropietaria { get; set; }
       public string EmpresaPropietariaDireccion { get; set; }
       public string EmpresaPropietariaTelefono { get; set; }
       public string EmpresaPropietariaEmail { get; set; }
       public string Conclusiones { get; set; }

       public byte[] FirmaTrabajador { get; set; }
       public byte[] HuellaTrabajador { get; set; }
       public string NombreUsuarioGraba { get; set; }

    }
}
