using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizMiBanco
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }	
        public string Dni { get; set; }
        public string Protocolo { get; set; }
        public string TipoEmo { get; set; }
        public string Empresa { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Localidad { get; set; }

        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Area { get; set; }
        public string Puesto { get; set; }
        public string GrupoSanguineo { get; set; }
        public string Fc { get; set; }
        public string Fr { get; set; }
        public string Temperatura { get; set; }
        public string PAS { get; set; }

        public string PAD { get; set; }
        public string PerimetoToraxReposo { get; set; }
        public string PerimetoToraxMaxima { get; set; }
        public string PerimetoToraxForzada { get; set; }
        public string PerimetoAbdominal { get; set; }
        public string PerimetoCadera { get; set; }
        public string ICC { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }

        public string SatO2 { get; set; }
        public string HbGdl { get; set; }
        public string Hto { get; set; }
        public string Glicemia { get; set; }
        public string ExamenOrina { get; set; }
        public string AntecedentesObesidad { get; set; }
        public string AntecedentesDiabetes { get; set; }
        public string AntecedentesHipertension { get; set; }
        public string EvaluacionOsteoMuscular { get; set; }
        public string DxEkg { get; set; }
        public string Urea { get; set; }

        public string OjoDerechoSC { get; set; }
        public string OjoIzquierdoSC { get; set; }
        public string VLejosODCC { get; set; }
        public string VLejosOICC { get; set; }
        public string DxOftalmologiaOD { get; set; }
        public string DxOftalmologiaOI { get; set; }
        public string OtrosDxOftalmologia { get; set; }
        public string OtrosDxOftalmologia2 { get; set; }
        public string OtrosDxOftalmologia3 { get; set; }
        public string OtrosDxOftalmologia4 { get; set; }

        public string OtrosDxOftalmologia5 { get; set; }
        public string FPsicoTiempoTrabajo { get; set; }
        public string FPsicoTiempoAutonomia { get; set; }
        public string FPsicoTrabajo { get; set; }
        public string FPsicoDemandas { get; set; }
        public string FPsicoTarea { get; set; }
        public string FPsicoSupervision { get; set; }
        public string FPsicoInteres { get; set; }
        public string FPsicoDesempenio { get; set; }
        public string FPsicoRelaciones { get; set; }

        public string TestFatiga { get; set; }
        public string BurnoutAgotamiento { get; set; }
        public string BurnoutCinismo { get; set; }
        public string BurnoutBajaRealizacion { get; set; }
        public string ResultadoGlobal { get; set; }
        public string PsicoClinica { get; set; }
        public string Toxicologico { get; set; }
        public string OD125Aerea { get; set; }
        public string OD250Aerea { get; set; }
        public string OD500Aerea { get; set; }

        public string OD1000Aerea { get; set; }
        public string OD2000Aerea { get; set; }
        public string OD3000Aerea { get; set; }
        public string OD4000Aerea { get; set; }
        public string OD6000Aerea { get; set; }
        public string OD8000Aerea { get; set; }
        public string OD250Osea { get; set; }
        public string OD500Osea { get; set; }
        public string OD1000Osea { get; set; }
        public string OD2000Osea { get; set; }

        public string OD3000Osea { get; set; }
        public string OD4000Osea { get; set; }
        public string OD6000Osea { get; set; }
        public string OI125Aerea { get; set; }
        public string OI250Aerea { get; set; }
        public string OI500Aerea { get; set; }
        public string OI1000Aerea { get; set; }
        public string OI2000Aerea { get; set; }
        public string OI3000Aerea { get; set; }
        public string OI4000Aerea { get; set; }

        public string OI6000Aerea { get; set; }
        public string OI8000Aerea { get; set; }
        public string OI250Osea { get; set; }
        public string OI500Osea { get; set; }
        public string OI1000Osea { get; set; }
        public string OI2000Osea { get; set; }
        public string OI3000Osea { get; set; }
        public string OI4000Osea { get; set; }
        public string OI6000Osea { get; set; }
        public string DxOIDerecho { get; set; }

        public string DxOIIzquierdo { get; set; }
        public string DxImc { get; set; }
        public string DxHipertension { get; set; }
        public string DxDiabetes { get; set; }
        public string DxDisplidemia { get; set; }
        public string AntecedenteTBC { get; set; }
        public string DxHipoacusia { get; set; }
        public string AntecedenteImportancia { get; set; }
        public string Dx1 { get; set; }
        public string Dx2 { get; set; }

        public string Dx3 { get; set; }
        public string Dx4 { get; set; }
        public string Recomendacion1 { get; set; }
        public string Recomendacion2 { get; set; }
        public string Recomendacion3 { get; set; }
        public string Recomendacion4 { get; set; }
        public string Recomendacion5 { get; set; }
        public string Recomendacion6 { get; set; }
        public string Recomendacion7 { get; set; }
        public string Recomendacion8 { get; set; }

        public string Recomendacion9 { get; set; }
        public string Recomendacion10 { get; set; }
        public string MedicoFirmante { get; set; }
        public string FechaEvaluacion { get; set; }
        public string SMO { get; set; }
        public string Vigencia { get; set; }
        public string FechaCaducidad { get; set; }
        public string RecepsionFisica { get; set; }
        public string Estado { get; set; }
        public string GradoAptitud { get; set; }

        public string Restriccion { get; set; }
        public string MedicoAuditor { get; set; }
        public string FechaRecepcion { get; set; }
        public string ObservacionesCredicorp { get; set; }

        public string EscenciaTarea { get; set; }
        public string SistemaTrabajo { get; set; }
        public string InteraccionSocial { get; set; }
        public string Organizacionales { get; set; }
        public string Resultado { get; set; }



    }
}
