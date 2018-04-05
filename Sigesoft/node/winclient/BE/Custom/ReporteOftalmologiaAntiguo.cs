using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ReporteOftalmologiaAntiguo
    {
       public string USO_DE_CORRECTORES_SI { get; set; }

       public string USO_DE_CORRECTORES_NO { get; set; }

       public string  ULTIMA_REFRACCION {get;set;}

       public string  ANTECEDENTE_DIABETES {get;set;}

       public string  ANTECEDENTE_HIPERTENSION {get;set;}

       public string  ANTECEDENTE_SUST_QUIMICAS {get;set;}

       public string  ANTECEDENTE_EXP_A_RADIACION {get;set;}

       public string  ANTECEDENTE_MIOPIA {get;set;}

       public string  ANTECEDENTE_CIRUGIA_OCULAR {get;set;}

       public string  ANTECEDENTE_TRAUMA_OCULAR {get;set;}

       public string  ANTECEDENTE_GLAUCOMA {get;set;}

       public string  ANTECEDENTE_ASTIGMATISMO {get;set;}

       public string  ANTECEDENTE_OTROS {get;set;}

       public string  PATOLOGIA_SIN_PATOLOGIAS {get;set;}

       public string  PATOLOGIA_PTOSIS_PALPEBRAL {get;set;}

       public string  PATOLOGIA_CONJUNTIVITIS {get;set;}

       public string  PATOLOGIA_PTERIGIUM {get;set;}

       public string  PATOLOGIA_ESTRABISMO {get;set;}

       public string  PATOLOGIA_TRANS_DE_CORNEA {get;set;}

       public string  PATOLOGIA_CATARATAS {get;set;}

       public string  PATOLOGIA_CHALAZION {get;set;}

       public string  PATOLOGIA_OTRAS {get;set;}

       public string  S_C_LEJOS_O_D {get;set;}

       public string  S_C_LEJOS_O_I {get;set;}

       public string  C_C_LEJOS_O_D {get;set;}

       public string  C_C_LEJOS_O_I {get;set;}

       public string  A_E_LEJOS_O_D {get;set;}

       public string  A_E_LEJOS_O_I {get;set;}

       public string  S_C_CERCA_O_D {get;set;}

       public string  S_C_CERCA_O_I {get;set;}

       public string  C_C_CERCA_O_D {get;set;}

       public string  C_C_CERCA_O_I {get;set;}

       public string  A_E_CERCA_O_D {get;set;}

       public string  A_E_CERCA_O_I {get;set;}

       public string  FONDO_OJO_MACULOPATIA_O_D {get;set;}

       public string  FONDO_OJO_MACULOPATIA_O_I {get;set;}

       public string  FONDO_OJO_NEURITIS_O_D {get;set;}

       public string  FONDO_OJO_NEURITIS_O_I {get;set;}

       public string  FONDO_OJO_RETINOPATIA_O_D {get;set;}

       public string  FONDO_OJO_RETINOPATIA_O_I {get;set;}

       public string  FONDO_OJO_ANGIOPATIA_O_D {get;set;}

       public string  FONDO_OJO_ANGIOPATIA_O_I {get;set;}

       public string  FONDO_OJO_ATROFIA_DE_N_O {get;set;}

       public string  FONDO_OJO_ATROFIA_DE_NO_O_D {get;set;}

       public string  FONDO_OJO_ATROFIA_DE_NO_O_I {get;set;}

       public string  TEST_ISHIHARA_NORMAL {get;set;}

       public string  TEST_ISHIHARA_ANORMAL {get;set;}

       public string  TEST_ISHIHARA_DESCRIPCION {get;set;}

       public string  TEST_ESTEREOPSIS_NORMAL {get;set;}

       public string  TEST_ESTEREOPSIS_ANORMAL {get;set;}

       public string  TEST_ESTEREOPSIS_ENCANDILAMIENTO {get;set;}

       public string  TEST_ESTEREOPSIS_TIEMPO {get;set;}

       public string  TEST_ESTEREOPSIS_RECUPERACION {get;set;}

       public string  CAMPIMETRIA_O_D {get;set;}

       public string  CAMPIMETRIA_O_I {get;set;}

       public string  TONOMETRIA_O_D {get;set;}

       public string  TONOMETRIA_O_I {get;set;}

       public string  REFLEJOS_PUPILARES_NORMAL {get;set;}

       public string  REFLEJOS_PUPILARES_ANORMAL {get;set;}

       public string  REFLEJOS_PUPILARES_DESCRIPCION {get;set;}

       public string  Dx {get;set;}

       public string Recomendaciones { get; set; }

       public byte[] FirmaTecnico { get; set; }

       public byte[] FirmaMedico { get; set; }

       public byte[] LogoEmpresa { get; set; }

       public byte[] LogoEmpresaCliente { get; set; }

       public string Paciente { get; set; }

       public string DNI { get; set; }

       public string PuestoTrabajo { get; set; }

       public string EmpresaCliente { get; set; }

       public DateTime? FechaServicio { get; set; }
       public DateTime? FechaNacimiento { get; set; }

       
       public int Edad { get; set; }

       public string v_ServiceId { get; set; }
    }
}
