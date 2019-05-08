using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizSolucionesManteIntegrales
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

        public string TipoEmo { get; set; }
        public string Dni { get; set; }
        public string NumCelular { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string PruebaHcgb { get; set; }
        public string Procedencia { get; set; }

        public string ClinicaProveedora { get; set; }
        public string PuestoTrabajo { get; set; }
        public string Area { get; set; }
        public string Proyecto { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public string Aptitud { get; set; }
        public string Tabaco { get; set; }
        public string Alcohol { get; set; }
        public string Drogas { get; set; }
        public string GrupoFactorSanguineo { get; set; }

        public string Hematocrito { get; set; }
        public string Hemoglobina { get; set; }
        public string Glucosa { get; set; }
        public string Urea { get; set; }
        public string Creatina { get; set; }
        public string ColesterolTotal { get; set; }
        public string Hdl { get; set; }
        public string Ldl { get; set; }
        public string Trigliceridos { get; set; }
        public string RprVdrl { get; set; }

        public string OrinaCompleto { get; set; }
        public string OrinaToxicologico { get; set; }
        public string OdontoPiezasMalEstado { get; set; }
        public string DxOdontologia { get; set; }
        public string OiVa500 { get; set; }
        public string OiVa1000 { get; set; }
        public string OiVa2000 { get; set; }
        public string OiVa3000 { get; set; }
        public string OiVa4000 { get; set; }
        public string OiVa6000 { get; set; }

        public string OiVa8000 { get; set; }
        public string OdVa500 { get; set; }
        public string OdVa1000 { get; set; }
        public string OdVa2000 { get; set; }
        public string OdVa3000 { get; set; }
        public string OdVa4000 { get; set; }
        public string OdVa6000 { get; set; }
        public string OdVa8000 { get; set; }
        public string OiVo500 { get; set; }
        public string OiVo1000 { get; set; }

        public string OiVo2000 { get; set; }
        public string OiVo3000 { get; set; }
        public string OiVo4000 { get; set; }
        public string OiVo6000 { get; set; }
        public string OiVo8000 { get; set; }
        public string OdVo500 { get; set; }
        public string OdVo1000 { get; set; }
        public string OdVo2000 { get; set; }
        public string OdVo3000 { get; set; }
        public string OdVo4000 { get; set; }

        public string OdVo6000 { get; set; }
        public string OdVo8000 { get; set; }
        public string DxOiDerecho { get; set; }
        public string DxOiIzquierdo { get; set; }
        public string RadioToraxOit { get; set; }
        public string RadioToraxSecuelaTbc { get; set; }
        public string RadioToraxDx { get; set; }
        public string EspirometriaCvf { get; set; }
        public string EspirometriaFev1 { get; set; }
        public string EspirometriaFec1Fvc { get; set; }

        public string EspirometriaFef2575 { get; set; }
        public string EspirometriaDx { get; set; }
        public string SatO2 { get; set; }
        public string CardiologiaPaS { get; set; }
        public string CardiologiaPaD { get; set; }
        public string CardiologiaEkg { get; set; }
        public string CardiologiaPruebaEsfuerzo { get; set; }
        public string OftalmoVCODSC { get; set; }
        public string OftalmoVCOISC { get; set; }
        public string OftalmoVCODCC { get; set; }

        public string OftalmoVCOICC { get; set; }
        public string OftalmoVLODSC { get; set; }
        public string OftalmoVLOISC { get; set; }
        public string OftalmoVLODCC { get; set; }
        public string OftalmoVLOICC { get; set; }
        public string AgudezaBinocular { get; set; }
        public string FondoOjo { get; set; }
        public string CampoVisual { get; set; }
        public string VisionColores { get; set; }
        public string Peso { get; set; }

        public string Talla { get; set; }
        public string Imc { get; set; }
        public string PerimetroAbdominal { get; set; }
        public string Cintura { get; set; }
        public string Cadera { get; set; }
        public string Icc { get; set; }
        public string AntecedentesLaborales { get; set; }
        public string AntecedentesPersonales { get; set; }
        public string AntecedentesCirugias { get; set; }
        public string AntecedentesAlergias { get; set; }

        public string InmunizacionesTetano { get; set; }
        public string InmunizacionesHepatitisA { get; set; }
        public string InmunizacionesHepatitisB { get; set; }
        public string InmunizacionesInfluenza { get; set; }
        public string InmunizacionesOtras { get; set; }
        public string InmunizacionesOtras2 { get; set; }
        public string ExamenFisicoHallazgos { get; set; }
        public string ExamenFisicoRecomendaciones { get; set; }
        public string TuberculosisSintomas { get; set; }
        public string ExamenFisicoBaciloscipia { get; set; }

        public string EvaPsicoDx { get; set; }
        public string EvaPsicoConclusiones { get; set; }
        public string EvaPsicoseFecha { get; set; }
        public string EvaPsicoseCondicion { get; set; }
        public string EvaPsicoseRestriccion { get; set; }
        public string PesquizaNeuro { get; set; }
        public string PesquizaFatiga { get; set; }
        public string Apendice10 { get; set; }
        public string PerfilTrabajoAltura { get; set; }
        public string ExaFototipoConclusion { get; set; }

        public string ExaFototipoTipoBLoqueador { get; set; }
        public string EvaMusculoResultado { get; set; }
        public string EnfermCronicasFechaDx { get; set; }
        public string EnfermCronicasDx { get; set; }
        public string EnfermCronicasTratamiento { get; set; }
        public string EnfermCronicasUltimoControl { get; set; }
        public string Dx1 { get; set; }
        public string Dx2 { get; set; }
        public string Dx3 { get; set; }
        public string Dx4 { get; set; }

        public string Dx5 { get; set; }
        public string Reco1 { get; set; }
        public string Reco2 { get; set; }
        public string Reco3 { get; set; }
        public string Reco4 { get; set; }
        public string Reco5 { get; set; }
    }
}
