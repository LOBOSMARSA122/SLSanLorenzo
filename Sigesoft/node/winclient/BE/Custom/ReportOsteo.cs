using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public  class ReportOsteo
    {
        public string ServiceId { get; set; }

        public string TareasHorasDias{get; set;}

        public string TareasFrecuencia{get; set;}

        public string TareasHorasSemana{get; set;}

        public string TareasTipo{get; set;}

        public string TareasCiclo{get; set;}

        public string TareasCarga{get; set;}

        public string LateralCervical{get; set;}

        public string LateralDorsal{get; set;}

        public string LateralLumbar{get; set;}

        public string LordosisCervical{get; set;}

        public string LordosisLumbar{get; set;}

        public string EscoliosisLumbar{get; set;}

        public string ContracturaMuscular{get; set;}

        public string DolorEspalda{get; set;}

        public string ConclusionDescripcion{get; set;}

        public string Dx{get; set;}

        public string Aptitud{get; set;}

        public byte[] FirmaTrabajador{get; set;}

        public byte[]  HuellaTrabajador { get; set; }

        public byte[]  FirmaMedico { get; set; }

        public string Recomendaciones { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string NombreUsuarioGraba { get; set; }
    }
}
