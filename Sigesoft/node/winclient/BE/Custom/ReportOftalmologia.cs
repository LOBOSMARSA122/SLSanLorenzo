using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportOftalmologia
    {
        public string NombrePaciente { get; set; }

        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaServicio { get; set; }

        public string EmprresaTrabajadora { get; set; }

        public string PuestoTrabajo { get; set; }

        public string v_ComponentId { get; set; }

        public string v_ServiceId { get; set; }

        public byte[] FirmaTecnologo { get; set; }
        public byte[] FirmaDoctor { get; set; }
        public string NombreTecnologo { get; set; }

        public byte[] b_Logo { get; set; }

        public string EmpresaPropietaria { get; set; }

        public string EmpresaPropietariaDireccion { get; set; }

        public string EmpresaPropietariaTelefono { get; set; }

        public string EmpresaPropietariaEmail { get; set; }



        public string USO_DE_CORRECTORES { get; set; }

        public string SI { get; set; }

        public string NO { get; set; }

        public string ULTIMAREFRACCION { get; set; }

        public string DIABETES { get; set; }

        public string HIPERTENSION { get; set; }

        public string SUSTQUIMICAS { get; set; }

        public string EXPRADIACION { get; set; }

        public string MIOPIA { get; set; }

        public string CIRUGIAOCULAR { get; set; }

        public string TRAUMAOCULAR { get; set; }

        public string GLAUCOMA { get; set; }

        public string ASTIGMATISMO { get; set; }

        public string OTROSESPECIFICAR { get; set; }

        public string SINPATOLOGIAS { get; set; }

        public string OTRASPATOLOGIA { get; set; }

        public string PTOSISPALPEBRAL { get; set; }

        public string CONJUNTIVITIS { get; set; }

        public string PTERIGIUM { get; set; }

        public string ESTRABISMO { get; set; }

        public string TRANSCORNEA { get; set; }

        public string CATARATAS { get; set; }

        public string CHALAZION { get; set; }

        public string ODSCLEJOS { get; set; }

        public string OI_SC_LEJOS { get; set; }

        public string OD_CC_LEJOS { get; set; }

        public string OI_CC_LEJOS { get; set; }

        public string OD_AE_LEJOS2 { get; set; }

        public string OI_AE_LEJOS2 { get; set; }

        public string SC_LEJOSOJODERECHO { get; set; }

        public string SCLEJOSOJOIZQUIERDO { get; set; }

        public string CCLEJOSOJODERECHO { get; set; }

        public string CCLEJOSOJ_IZQUIERDO { get; set; }

        public string AELEJOSOJODERECHO { get; set; }

        public string AELEJOSOJOIZQUIERDO { get; set; }

        public string SCCERCAOJODERECHO { get; set; }

        public string S_CCERCAOJOIZQUIERDO { get; set; }

        public string CCCERCAOJODERECHO { get; set; }

        public string CCCERCAOJOIZQUIERDO { get; set; }

        public string AECERCAOJODERECHO { get; set; }

        public string AECERCAOJOIZQUIERDO { get; set; }

        public string NORMAL { get; set; }

        public string ANORMAL { get; set; }

        public string DESCRIPCION { get; set; }

        public string EMETROPE { get; set; }

        public string PRESBICIACORREGIDA { get; set; }

        public string AMETROPIACORREGIDA { get; set; }

        public string PRESBICIANOCORREGIDA { get; set; }

        public string AMETROPIANOCORREGIA { get; set; }

        public string AMBLIOPIA { get; set; }

        public string AMETROPIACORREGIDAPARCIAL { get; set; }

        public string DISMINUCIONDELA_VISIONUNOJO { get; set; }

        public string DISMINUCIONDELAVISIONBILATERAL { get; set; }



        public string PARPADOOJODERECHO { get; set; }

        public string PARPADOOJOIZQUIERDO { get; set; }

        public string CONJUNTIVAOJODERECHO { get; set; }

        public string CONJUNTIVAOJOIZQUIERDO { get; set; }

        public string CORNEAOJODERECHO { get; set; }

        public string CORNEAOJOIZQUIERDO { get; set; }

        public string IRISOJODERECHO { get; set; }

        public string IRISOJOIZQUIERDO { get; set; }

        public string MOVOCULARESOJODERECHO { get; set; }

        public string MOVOCULARESOJOIZQUIERDO { get; set; }

        public string NORMAL2 { get; set; }

        public string ANORMAL2 { get; set; }

        public string DESCRIPCION2 { get; set; }

        public string NORMAL3 { get; set; }

        public string ANORMAL3 { get; set; }

        public string ENCANDILAMIENTO { get; set; }

        public string TIEMPO { get; set; }

        public string RECUPERACION { get; set; }

        public string CAMPIMETRIAOD { get; set; }

        public string CAMPIMETRIAOI { get; set; }

        public string TONOMETRIAOD { get; set; }

        public string TONOMETRIAOI { get; set; }

        public string OFTALMOLOGIA_CRISTALINO_OJO_DERECHO_ID { get; set; }

        public string OFTALMOLOGIA_CRISTALINO_OJO_IZQUIERDO_ID { get; set; }


        public string Dx { get; set; }
        public string Recomendaciones { get; set; }

        public int TipoEso { get; set; }
        public byte[] HuellaPaciente { get; set; }
        public byte[] FirmaPaciente { get; set; }
    }
}
