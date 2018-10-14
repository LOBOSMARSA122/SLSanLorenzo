using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizShauindo
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string ProtocolId { get; set; }

        public string TipoEmo { get; set; }
        public string DniPasaporte { get; set; }
        public DateTime? FechaExamen { get; set; }
        public string ApellidosNombres{ get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string TelefonoContacto{ get; set; }
        public int? edad { get; set; }
        public int? _Sexo { get; set; }
        public int? _Grupo { get; set; }
        public int? _Factor { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil{ get; set; }
        public string GradoInstruccion { get; set; }
        public string GrupoFactorSanguineo{ get; set; }
        public string Procedencia{ get; set; }
        public string Ocupacion { get; set; }
        public string Empresa { get; set; }
        public string Area { get; set; }

        public string Ruido { get; set; }
        public string Cancerigenos { get; set; }
        public string Temperaturas { get; set; }
        public string Cargas { get; set; }
        public string Polvo { get; set; }
        public string Mutagenicos { get; set; }
        public string Biologicos { get; set; }
        public string MovRepet { get; set; }
        public string Epp1 { get; set; }
        public string VigSegmentaria { get; set; }

        public string Solventes { get; set; }
        public string Posturas { get; set; }
        public string PantallaPVD { get; set; }
        public string Epp2 { get; set; }
        public string ViBTotal { get; set; }
        public string MetalPesado { get; set; }
        public string Turnos { get; set; }
        public string Otros { get; set; }
        public string Describir { get; set; }
        public string AntecedentesPersonales { get; set; }

        public string AntecedentesFamiliares { get; set; }
        public string HabitosNocivosAlcohol { get; set; }
        public string HabitosNocivosTabaco { get; set; }
        public string HabitosNocivosDrogas { get; set; }
        public int NumeroHijos { get; set; }
        public string Alergias { get; set; }
        public string InmunizacionTetano{ get; set; }
        public string InmunizacionInfluenza{ get; set; }
        public string InmunizacionHepatitisB { get; set; }
        public string InmunizacionFiebreAmarilla { get; set; }

        public string Talla { get; set; }
        public string Peso { get; set; }
        public string IMC { get; set; }
        public string PerimetroAbdominal { get; set; }
        public string PerimetroCadera { get; set; }
        public string IndiceCintura { get; set; }
        public string PorcentajeGrasaCorporal { get; set; }
        public string PresionSistolica { get; set; }
        public string PresionDiastolica { get; set; }
        public string FrecuenciaCardiaca { get; set; }

        public string Temperatura { get; set; }
        public string FrecuenciaRespiratoria { get; set; }
        public string SaturacionOxigeno { get; set; }
        public string CabezaDescripcion { get; set; }
        public string CuelloDescripcion { get; set; }
        public string NarizDescripcion { get; set; }
        public string BocaAdmigdalaFaringeLaringeDescripcion { get; set; }
        public string MiembrosSuperioresDescripcion { get; set; }
        public string MiembrosInferioresDescripcion { get; set; }
        public string ReflejosOsteoTendinososDescripcion { get; set; }

        public string MarchaDescripcion { get; set; }
        public string ColumnaDescripcion { get; set; }
        public string AbdomenDescripcion { get; set; }
        public string DescripcionGeneral { get; set; }
        public string AnillosInguinalesDescripcion { get; set; }
        public string HerniasDescripcion { get; set; }
        public string VaricesDescripcion { get; set; }
        public string OrganosGenitalesDescripcion { get; set; }
        public string GangliosDescripcion { get; set; }
        public string LenAtenMemoOrientIntelAfect { get; set; }

        public string Gingivitis { get; set; }
        public string PiezasMalEstado { get; set; }
        public string PiezasFaltan { get; set; }
        public string AntecedentesLaborales { get; set; }
        public string AntecedentesPatologicos { get; set; }
        public string VisionCercaScod { get; set; }
        public string VisionCercaScoi { get; set; }
        public string VisonCercaCcoi { get; set; }
        public string VisionLejosScod { get; set; }
        public string VisionLejosScoi { get; set; }

        public string VisionLejosCcod { get; set; }
        public string VisonLejosCcoi { get; set; }
        public string VisonColores { get; set; }
        public string EnfermedadesOculares { get; set; }
        public string ReflejosPupilares { get; set; }
        public string AptitudTrabajarSi { get; set; }
        public string AptitudTrabajarNo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Calibracion { get; set; }

        public string ProtectoresAuditivos { get; set; }
        public string ApreciacionRuido { get; set; }
        public string TiempoExpPond { get; set; }
        public string CambiosAltitud { get; set; }
        public string ViajesFrecuentesAltura { get; set; }
        public string HorasDescansoExamen { get; set; }
        public string TiempoTrabajo { get; set; }
        public string SorderaDismAud { get; set; }
        public string Zumbido { get; set; }
        public string VertigosMareos { get; set; }

        public string OtalgiaDolorOido { get; set; }
        public string SecrecionOticaInfeccion { get; set; }
        public string OtrosSint { get; set; }
        public string OtrosOidos { get; set; }
        public string InfeccionesAuditivas { get; set; }
        public string InfeccionesOrofaringeas { get; set; }
        public string Resfrios { get; set; }
        public string AccidentesTraumaticoAuditivo { get; set; }
        public string USoMedicamentosOtotoxicos { get; set; }
        public string EnfermedadTiroidea { get; set; }

        public string Tec { get; set; }
        public string ConsumoTabaco { get; set; }
        public string ServicioMilitar { get; set; }
        public string HobbiesCexposARuido { get; set; }
        public string ExposLaboralQuimicos { get; set; }
        public string OidoDerecho { get; set; }
        public string OidoIzquierdo { get; set; }
        public string TxtVaOd125 { get; set; }
        public string TxtVaOd250 { get; set; }
        public string TxtVaOd500 { get; set; }

        public string TxtVaOd1000 { get; set; }
        public string TxtVaOd2000 { get; set; }
        public string TxtVaOd3000 { get; set; }
        public string TxtVaOd4000 { get; set; }
        public string TxtVaOd6000 { get; set; }
        public string TxtVaOd8000 { get; set; }
        public string TxtVoOd125 { get; set; }
        public string TxtVoOd250 { get; set; }
        public string TxtVoOd500 { get; set; }
        public string TxtVoOd1000 { get; set; }

        public string TxtVoOd2000 { get; set; }
        public string TxtVoOd3000 { get; set; }
        public string TxtVoOd4000 { get; set; }
        public string TxtVoOd6000 { get; set; }
        public string TxtVoOd8000 { get; set; }
        public string TxtVaOi125 { get; set; }
        public string TxtVaOi250 { get; set; }
        public string TxtVaOi500 { get; set; }
        public string TxtVaOi1000 { get; set; }
        public string TxtVaOi2000 { get; set; }

        public string TxtVaOi3000 { get; set; }
        public string TxtVaOi4000 { get; set; }
        public string TxtVaOi6000 { get; set; }
        public string TxtVaOi8000 { get; set; }
        public string TxtVoOi125 { get; set; }
        public string TxtVoOi250 { get; set; }
        public string TxtVoOi500 { get; set; }
        public string TxtVoOi1000 { get; set; }
        public string TxtVoOi2000 { get; set; }
        public string TxtVoOi3000 { get; set; }

        public string TxtVoOi4000 { get; set; }
        public string TxtVoOi6000 { get; set; }
        public string TxtVoOi8000 { get; set; }
        public string TxtEmOd125 { get; set; }
        public string TxtEmOd250 { get; set; }
        public string TxtEmOd500 { get; set; }
        public string TxtEmOd1000 { get; set; }
        public string TxtEmOd2000 { get; set; }
        public string TxtEmOd3000 { get; set; }
        public string TxtEmOd4000 { get; set; }

        public string TxtEmOd6000 { get; set; }
        public string TxtEmOd8000 { get; set; }
        public string TxtEmOi125 { get; set; }
        public string TxtEmOi250 { get; set; }
        public string TxtEmOi500 { get; set; }
        public string TxtEmOi1000 { get; set; }
        public string TxtEmOi2000 { get; set; }
        public string TxtEmOi3000 { get; set; }
        public string TxtEmOi4000 { get; set; }
        public string TxtEmOi6000 { get; set; }

        public string TxtEmOi8000 { get; set; }
        public string TxtAnOd125 { get; set; }
        public string TxtAnOd250 { get; set; }
        public string TxtAnOd500 { get; set; }
        public string TxtAnOd1000 { get; set; }
        public string TxtAnOd2000 { get; set; }
        public string TxtAnOd3000 { get; set; }
        public string TxtAnOd4000 { get; set; }
        public string TxtAnOd6000 { get; set; }
        public string TxtAnOd8000 { get; set; }

        public string TxtAnOi125 { get; set; }
        public string TxtAnOi250 { get; set; }
        public string TxtAnOi500 { get; set; }
        public string TxtAnOi1000 { get; set; }
        public string TxtAnOi2000 { get; set; }
        public string TxtAnOi3000 { get; set; }
        public string TxtAnOi4000 { get; set; }
        public string TxtAnOi6000 { get; set; }
        public string TxtAnOi8000 { get; set; }
        public string Cvf { get; set; }

        public string Vef1 { get; set; }
        public string Vef1Cvf { get; set; }
        public string ConclusionEspirometrica { get; set; }
        public string Cero { get; set; }
        public string CeroCero { get; set; }
        public string CeroUno { get; set; }
        public string UnoCero { get; set; }
        public string UnoUno { get; set; }
        public string UnoDos { get; set; }
        public string DosUno { get; set; }

        public string DosDos { get; set; }
        public string DosTres { get; set; }
        public string TresUno { get; set; }
        public string TresDos { get; set; }
        public string TresMas { get; set; }
        public string ConclusionRayosX { get; set; }
        public string FrecuenciaCardiaca_ { get; set; }
        public string RitmoCardiaco { get; set; }
        public string IntervaloPR { get; set; }
        public string ComplejoQRS { get; set; }

        public string IntervaloQTC { get; set; }
        public string EjeCardiaco { get; set; }
        public string HallazgoInformeElectricoCardiaco { get; set; }
        public string ConclusionesEKG { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string Hematies { get; set; }
        public string LeucocitosTotales { get; set; }
        public string Plaquetas { get; set; }
        public string Basofilos { get; set; }

        public string Eosinofilos { get; set; }
        public string Monocitos { get; set; }
        public string Linfocitos { get; set; }
        public string NeutrofilosSegementados { get; set; }
        public string Mielocitos { get; set; }
        public string Juveniles { get; set; }
        public string Abastonados { get; set; }
        public string Segmentados { get; set; }
        public string CCVolumenCorpuscularMedio { get; set; }
        public string CCHBCorpuscular { get; set; }

        public string Glucosa { get; set; }
        public string ProteinasTotales { get; set; }
        public string Albumina { get; set; }
        public string Globulina { get; set; }
        public string TGO { get; set; }
        public string TGP { get; set; }
        public string FosfatasaAlcalina { get; set; }
        public string GammaGlutamilTranspeptidasa { get; set; }
        public string BilirrubinaTotal { get; set; }
        public string BilirrubinaDirecta { get; set; }

        public string BilirrubinaIndirecta { get; set; }
        public string ColesterolTotal { get; set; }
        public string Trigliceridos { get; set; }
        public string ColesterolHDL { get; set; }
        public string ColesterolLDL { get; set; }
        public string CreatininaSuero { get; set; }
        public string Color { get; set; }
        public string Aspecto { get; set; }
        public string Densidad { get; set; }
        public string ReaccionPH { get; set; }

        public string SangreOrina { get; set; }
        public string Nitritos { get; set; }
        public string Proteinas { get; set; }
        public string GlucosaExamenCompletoOrina { get; set; }
        public string PigmentosBiliares { get; set; }
        public string Urobilinogeno { get; set; }
        public string Cetonas { get; set; }
        public string AcidoAscorbico { get; set; }
        public string Leucocitos { get; set; }
        public string Bilirrubinas { get; set; }

        public string Leucocitos2 { get; set; }
        public string Hematies_ { get; set; }
        public string Germenes { get; set; }
        public string Levaduras { get; set; }
        public string CristDeoxalatosCalcio { get; set; }
        public string CristDeurAmorfos { get; set; }
        public string CristFosfAmorfos { get; set; }
        public string CristFosfTriples { get; set; }
        public string CelulasEpiteliales { get; set; }
        public string CilindrosHialinos { get; set; }

        public string CilindrosGranulosos { get; set; }
        public string CilindrosLeucocitarios { get; set; }
        public string CilindrosHematicos { get; set; }
        public string Piocitos { get; set; }
        public string FilamentosMucoides { get; set; }
        public string Patologico { get; set; }
        public string LeusVDRL { get; set; }
        public string PlomoSangre { get; set; }
        public string Magnesio { get; set; }
        public string Marihuana { get; set; }

        public string Cocaina { get; set; }
        public string AptitudPsicologica { get; set; }
        public string Recomendaciones { get; set; }
        public string Dx1 { get; set; }
        public string Recomendacion1 { get; set; }
        public string Dx2{ get; set; }
        public string Recomendacion2 { get; set; }
        public string Dx3 { get; set; }
        public string Recomendacion3 { get; set; }
        public string Dx4 { get; set; }

        public string Recomendacion4 { get; set; }
        public string Dx5 { get; set; }
        public string Recomendacion5 { get; set; }
        public string Dx6 { get; set; }
        public string Recomendacion6 { get; set; }
        public string AptitudFinal { get; set; }
        public string MotivoObservacion { get; set; }
        public string VigenciaEmo { get; set; }
        public string ClinicaRealizoEmo { get; set; }

        public string ConclusionesRx { get; set; }
        public string RecomendacionesConcatenadas { get; set; }
        public string RestriccionConcatenadas { get; set; }
        public string ConclusionLabo { get; set; }
       
        //
        public string VLDL { get; set; }
        public string CoeficienteIntelectual { get; set; } //goldfields

    }
}
