using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportEvaCardiologica
    {
        public string NombreTrabajador { get; set; }
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public byte[] FirmaMedico { get; set; }

        public  string EVA_CARDIOLOGICA_SOPLO_CARDIACO { get; set; } 
        public  string EVA_CARDIOLOGICA_PRESION_ALTA { get; set; } 
        public  string EVA_CARDIOLOGICA_CANSANCIO_RAPIDO { get; set; }
        public  string EVA_CARDIOLOGICA_MAREOS { get; set; } 
        public  string EVA_CARDIOLOGICA_DOLOR_PRECORDIAL { get; set; }
        public  string EVA_CARDIOLOGICA_PALPITACIONES { get; set; } 
        public  string EVA_CARDIOLOGICA_ATAQUE_CORAZON { get; set; } 
        public  string EVA_CARDIOLOGICA_PERDIDA_CONCIENCIA { get; set; }
        public  string EVA_CARDIOLOGICA_OBESIDAD { get; set; } 
        public  string EVA_CARDIOLOGICA_TABAQUISMO { get; set; }
        public  string EVA_CARDIOLOGICA_DIABETES { get; set; } 
        public  string EVA_CARDIOLOGICA_DISLIPIDEMIA { get; set; }
        public  string EVA_CARDIOLOGICA_VARICES_PIERNAS { get; set; } 
        public  string EVA_CARDIOLOGICA_SEDENTARISMO { get; set; }
        public  string EVA_CARDIOLOGICA_OTROS { get; set; }

        public  string EVA_CARDIOLOGICA_PRECORDIAL_1 { get; set; } 
        public  string EVA_CARDIOLOGICA_DESMAYOS { get; set; }
        public string EVA_CARDIOLOGICA_PALPITACIONES_1 { get; set; }
        public  string EVA_CARDIOLOGICA_DISNEA_PAROXISTICA { get; set; }
        public string EVA_CARDIOLOGICA_MAREOS_1 { get; set; }
        public  string EVA_CARDIOLOGICA_CLAUDICACION { get; set; }
        public  string EVA_CARDIOLOGICA_OTROS_1 { get; set; }

        public  string EVA_CARDIOLOGICA_FREC_CARDIACA { get; set; }
        public  string EVA_CARDIOLOGICA_PRESION_ARTERIAL { get; set; }
        public  string EVA_CARDIOLOGICA_CHOQUE_PUNTA { get; set; }

        public  string EVA_CARDIOLOGICA_RITMO { get; set; }
        public  string EVA_CARDIOLOGICA_EJE { get; set; }
        public  string EVA_CARDIOLOGICA_FC { get; set; } 
        public  string EVA_CARDIOLOGICA_PR { get; set; } 
        public  string EVA_CARDIOLOGICA_QRS { get; set; }
        public  string EVA_CARDIOLOGICA_QT { get; set; }
        public  string EVA_CARDIOLOGICA_ONDA_Q { get; set; } 
        public  string EVA_CARDIOLOGICA_ONDA_P { get; set; } 
        public  string EVA_CARDIOLOGICA_ONDA_R { get; set; } 
        public  string EVA_CARDIOLOGICA_ONDA_S { get; set; } 
        public  string EVA_CARDIOLOGICA_ONDA_T { get; set; }
        public  string EVA_CARDIOLOGICA_ONDA_U { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string Conclusiones { get; set; }
        public string NombreUsuarioGraba { get; set; }
    }
}
