using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportInformeEspirometria
    {
        public DateTime? FechaNacimiento { get; set; }

        public string EspirometriaNro{get; set;}

        public DateTime Fecha{get; set;}

        public string HCL{get; set;}

        public int i_TipoEvaluacion{get; set;}

        public string TipoEvaluacion { get; set; }

        public string LugarExamen{get; set;}

        public string RazonSocial{get; set;}

        public string ActividadEconomica{get; set;}

        public string PuestoTrabajo{get; set;}

        public string TiempoTrabajo{get; set;}

        public string NombreTrabajador{get; set;}

        public int Edad{get; set;}

        public int i_Sexo{get; set;}

        public string Sexo { get; set; }

        public string Talla { get; set; }

        public string Peso { get; set; }

        public string OrigenEtnico{get; set;}

        public string FumadorSiNo{get; set;}

        public string CVF{get; set;}

        public string VEF1{get; set;}

        public string VEF1CVF{get; set;}

        public string FET{get; set;}

        public string FEV2575{get; set;}

        public string PEF{get; set;}

        public string CVFDes{get; set;}

        public string VEF1Des{get; set;}

        public string VEF1CVFDes{get; set;}

        public string FETDes{get; set;}

        public string FEV2575Des{get; set;}

        public string PEFDes{get; set;}

        public string EdadPulmonar{get; set;}

        public string Resultado{get; set;}

        public string Observacion{get; set;}

        public byte[] FirmaRealizaEspirometria { get; set; }

        public byte[] FirmaMedicoInterpreta{get; set;}

        public byte[] Logo { get; set; }

        public string Dx { get; set; }



        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
    }
}
