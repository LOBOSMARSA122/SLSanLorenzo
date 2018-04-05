using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportRadiologico
    {
        public string v_ServiceId { get; set; }
        public DateTime? d_BirthDate { get; set; }

       public  string Paciente { get; set; }     
       public string ExamenSolicitado { get; set; }
       public string Empresa { get; set; }
       public DateTime Fecha { get; set; }
       public byte[] FirmaTecnologo { get; set; }
       public byte[] FirmaMedicoEva { get; set; }

       public int Edad { get; set; }

       public string Vertices { get; set; }
       public string CamposPulmonares { get; set; }
       public string SenosCosto { get; set; }
       public string SenosCardio { get; set; }
       public string Mediastinos { get; set; }
       public string Silueta { get; set; }
       public string Indice { get; set; }
       public string PartesBlandas { get; set; }
       public string Conclusiones { get; set; }

       public string Hilos { get; set; }
       public string Hallazgos { get; set; }


       public byte[] b_Logo { get; set; }
       public string EmpresaPropietaria { get; set; }
       public string EmpresaPropietariaDireccion { get; set; }
       public string EmpresaPropietariaTelefono { get; set; }
       public string EmpresaPropietariaEmail { get; set; }

       public string CodigoPlaca { get; set; }

       public string NombreUsuarioGraba { get; set; }

    }
}
