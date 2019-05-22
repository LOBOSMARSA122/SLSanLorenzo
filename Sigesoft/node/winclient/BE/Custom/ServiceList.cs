using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ServiceList
    {
        public double? r_ClinicDescount { get; set; } 
        public string v_ServiceId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_CalendarId { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_PersonId { get; set; }
        public int? i_MasterServiceId { get; set; }
        public string v_MasterServiceName { get; set; }
        public int? i_ServiceStatusId { get; set; }
        public string v_ServiceStatusName { get; set; }
        public int? i_AptitudeStatusId{ get; set; }
        public DateTime? d_ServiceDate{ get; set; }
        public DateTime? d_GlobalExpirationDate{ get; set; }
        public DateTime? d_ObsExpirationDate { get; set; }
        public int? i_FlagAgentId{ get; set; }
        public String v_OrganizationId { get; set; }
        public DateTime d_DateTimeCalendar { get; set; }
        public string v_OrganizationName { get; set; }
        public string v_LocationName { get; set; }
        public string v_GenderName { get; set; }
        public int? i_HasSymptomId { get; set; } 

        public string v_ServiceTypeName { get; set; }
        public string v_LocationId { get; set; }
        public string v_MainSymptom { get; set; }   
        public int? i_TimeOfDisease { get; set; }
        public int? i_TimeOfDiseaseTypeId { get; set; }
        public string v_Story { get; set; }
        public int? i_DreamId { get; set; }
        public int? i_UrineId { get; set; }
        public int? i_DepositionId { get; set; }
        public int? i_AppetiteId { get; set; }
        public int? i_ThirstId { get; set; }
        public DateTime? d_Fur { get; set; }
        public string v_CatemenialRegime { get; set; }
        public int? i_MacId { get; set; }

        public int? i_IsNewControl { get; set; }
        public int? i_HasMedicalBreakId { get; set; }
        public int? i_HasRestrictionId { get; set; }
        public DateTime? d_MedicalBreakStartDate { get; set; }
        public DateTime? d_MedicalBreakEndDate { get; set; }
        public string v_GeneralRecomendations { get; set; }
        public int? i_DestinationMedicationId { get; set; }
        public int? i_TransportMedicationId { get; set; }
        public DateTime? d_StartDateRestriction { get; set; }   
        public DateTime? d_EndDateRestriction { get; set; }
        public DateTime? d_BirthDate { get; set; }

        public string v_DiseaseName { get; set; }

        //// Falta el campo Tiene Restricciones -> Si / No                                                                                                                

        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_ProtocolName { get; set; }
        public int i_PersonId { get; set; }     
        public string v_FirstName { get; set; }    
        public string v_FirstLastName { get; set; }    
        public string v_SecondLastName { get; set; }
        public string v_GroupOcupationName { get; set; }
        public string v_EsoTypeName { get; set; }
        public int i_ServiceTypeId { get; set; }
        public int i_EsoTypeId { get; set; }
        public string v_Pacient { get; set; }
        public string v_PacientDocument { get; set; }
        public int? i_SexTypeId { get; set; }
        public string v_AptitudeStatusName { get; set; }

        public byte[] b_PersonImage { get; set; }

        public Nullable<Int32> i_HazInterconsultationId { get; set; }
        public DateTime? d_NextAppointment { get; set; }
        public int? i_SendToTracking { get; set; }
             
        public string v_DocTypeName { get; set; }
        public string v_DocNumber { get; set; }

        public string i_Age { get; set; }
        public int Year { get; set; }
        public string CostoProtocolo { get; set; }
        public float? CostoTotalProtocolo { get; set; }  
        public DateTime FechaNacimiento { get; set; }

        public string v_CurrentOccupation { get; set; }

        public string v_Dream { get; set; }
        public string v_Urine { get; set; }
        public string v_Deposition { get; set; }
        public string v_Appetite { get; set; }
        public string v_Thirst { get; set; }
        public string v_Mac { get; set; }
        public string RUC { get; set; }
        public string RUC2 { get; set; }
        public string RUC3 { get; set; }
        // Antecedentes ginecologicos
        public DateTime? d_PAP { get; set; }
        public DateTime? d_Mamografia { get; set; }
        public string v_Gestapara { get; set; }
        public string v_Menarquia { get; set; }
        public string v_CiruGine { get; set; }

        public string v_Findings { get; set; }


        public string v_FechaUltimoPAP { get; set; }
        public string v_ResultadosPAP { get; set; }
        public string v_FechaUltimaMamo { get; set; }
        public string v_ResultadoMamo { get; set; }
    
    
       // //////

        public string v_CustomerOrganizationName { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }

        public string v_EmployerOrganizationName { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }

        public string v_WorkingOrganizationName { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }


        public int i_Edad { get; set; }
        public string v_BirthPlace { get; set; }
        public int i_DiaN { get; set; }
        public int i_MesN { get; set; }
        public int i_AnioN { get; set; }

        public int i_DiaV { get; set; }
        public int i_MesV { get; set; }
        public int i_AnioV { get; set; }
        public string NivelInstruccion { get; set; }
        public string LugarResidencia { get; set; }
        public string PuestoTrabajo { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string DireccionEmpresaTrabajo { get; set; }
        public string DireccionEmpresaTrabajo2 { get; set; }
        public string DireccionEmpresaTrabajo3 { get; set; }
        public string RubroEmpresaTrabajo { get; set; }

        public string DepartamentoEmpresaTrabajo { get; set; }
        public string ProvinciaEmpresaTrabajo { get; set; }
        public string DistritoEmpresaTrabajo { get; set; }

        public string v_ServiceComponentId { get; set; }
        public string MotivoEvaluacion { get; set; }
        public string NivelIntelectual { get; set; }
        public string CoordinacionVisomotriz { get; set; }
        public string NivelMemoria { get; set; }
        public string Personalidad { get; set; }
        public string Afectividad { get; set; }
        public string AreaCognitiva { get; set; }
        public string AreaEmocional { get; set; }
        public string Conclusiones { get; set; }
        public string Restriccion { get; set; }
        public string Recomendacion { get; set; }
        public string vAdecuado { get; set; }
        public string vInadecuado { get; set; }
        public string Presentacion { get; set; }
        public string Postura { get; set; }
        public string DiscursoRitmo { get; set; }
        public string DiscursoTono { get; set; }
        public string DiscursoArticulacion { get; set; }

        public string OrientacionTiempo { get; set; }
        public string OrientacionEspacio { get; set; }
        public string OrientacionPersona { get; set; }
        public Byte[] Rubrica { get; set; }

        public string v_AdressLocation { get; set; }

        public string DepartamentoPaciente { get; set; }
        public string ProvinciaPaciente { get; set; }
        public string DistritoPaciente { get; set; }
        public int i_ResidenceInWorkplaceId { get; set; }
        public string v_ResidenceTimeInWorkplace { get; set; }
        public int i_TypeOfInsuranceId { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string EstadoCivil { get; set; }
        public string GradoInstruccion { get; set; }

        public int TotalHijos { get; set; }
        public int? HijosDependientes { get; set; }



        public string AbdomenExcelente { get; set; }
        public string AbdomenPromedio { get; set; }
        public string AbdomenRegular { get; set; }
        public string AbdomenPobre { get; set; }
        public string AbdomenPuntos { get; set; }
        public string AbdomenObs { get; set; }

        public string CaderaExcelente { get; set; }
        public string CaderaPromedio { get; set; }
        public string CaderaRegular { get; set; }
        public string CaderaPobre { get; set; }
        public string CaderaPuntos { get; set; }
        public string CaderaObs { get; set; }

        public string MusloExcelente { get; set; }
        public string MusloPromedio { get; set; }
        public string MusloRegular { get; set; }
        public string MusloPobre { get; set; }
        public string MusloPuntos { get; set; }
        public string MusloObs { get; set; }

        public string AbdomenLateralExcelente { get; set; }
        public string AbdomenLateralPromedio { get; set; }
        public string AbdomenLateralRegular { get; set; }
        public string AbdomenLateralPobre { get; set; }
        public string AbdomenLateralPuntos { get; set; }
        public string AbdomenLateralObs { get; set; }

        public string AbduccionHombroNormalOptimo { get; set; }
        public string AbduccionHombroNormalLimitado { get; set; }
        public string AbduccionHombroNormalMuyLimitado { get; set; }
        public string AbduccionHombroNormalPuntos { get; set; }
        public string AbduccionHombroNormalObs { get; set; }

        public string AbduccionHombroOptimo { get; set; }
        public string AbduccionHombroLimitado { get; set; }
        public string AbduccionHombroMuyLimitado { get; set; }
        public string AbduccionHombroPuntos { get; set; }
        public string AbduccionHombroObs { get; set; }

        public string RotacionExternaOptimo { get; set; }
        public string RotacionExternaLimitado { get; set; }
        public string RotacionExternaMuyLimitado { get; set; }
        public string RotacionExternaPuntos { get; set; }
        public string RotacionExternaObs { get; set; }

        public string RotacionExternaHombroInternoOptimo { get; set; }
        public string RotacionExternaHombroInternoLimitado { get; set; }
        public string RotacionExternaHombroInternoMuyLimitado { get; set; }
        public string RotacionExternaHombroInternoPuntos { get; set; }
        public string RotacionExternaHombroInternoObs { get; set; }

        public string Total1 { get; set; }
        public string Total2 { get; set; }

        public string AptitudMusculoEsqueletico { get; set; }
        public string AptitudMusculoEsqueleticoEspalda { get; set; }
        public int i_DocTypeId { get; set; }

        public int? HijosVivos { get; set; }
        public int? HijosMuertos { get; set; }

        public int? i_InsertUserMedicalAnalystId { get; set; }

        public byte[] FirmaDoctor { get; set; }

        public int i_PlaceWorkId { get; set; }

        public string v_ExploitedMineral { get; set; }

        public int i_AltitudeWorkId { get; set; }

        public string v_EmergencyPhone { get; set; }
       
       public int i_MaritalStatusId { get; set; }

       public int i_LevelOfId { get; set; }
        //public string v_NamePacient { get; set; }
        //public string v_Surnames { get; set; }
        //public string DireccionPaciente { get; set; }
        //public byte[] FirmaMedico { get; set; }
        //public string ApellidosDoctor { get; set; }
        //public string NombreDoctor { get; set; }
        //public string CMP { get; set; }
        //public string DireccionDoctor { get; set; }
        //public string EmpresaEmpleadora { get; set; }

       public string EmpresaClienteId  { get; set; }

        //Reportes
        public int i_AgePacient { get; set; }
        public string v_NamePacient { get; set; }
        public string v_Surnames { get; set; }
        //public string v_DocNumber { get; set; }
        //public int? i_SexTypeId { get; set; }

        public string Agrofobia { get; set; }
        public string Acrofobia { get; set; }
        public string Alcohol { get; set; }
        public string Drogas { get; set; }
        public string TrauEnce { get; set; }
        public string Convulsiones { get; set; }
        public string Vertigo { get; set; }
        public string Sincope { get; set; }
        public string Mioclonias { get; set; }
        public string Acatisia { get; set; }
        public string Cefalea { get; set; }
        public string Diabetes { get; set; }
        public string Insuficiencia { get; set; }
        public string Hipertension { get; set; }
        public string Alteraciones { get; set; }
        public string Ametropia { get; set; }
        public string Esteropsia { get; set; }
        public string Asma { get; set; }
        public string Hipoacusia { get; set; }
        public string EntrPrimAux { get; set; }
        public string EntrTraAlt { get; set; }
        public string EmfPsiqui { get; set; }

        public string Obs1 { get; set; }
        public string Timpanos { get; set; }
        public string Audicion { get; set; }
        public string Sustentacion { get; set; }
        public string Camina3mts { get; set; }
        public string CaminaOjosVendados3mts { get; set; }
        public string CaminaPuntaTalon { get; set; }
        public string Imitacion { get; set; }
        public string AdiodocoquinesiaDirecta { get; set; }
        public string AdiodocoquinesiaCruzada { get; set; }
        public string Nistagmus { get; set; }
        public string Obs2 { get; set; }
        public string AptoMayorAltura18Mts { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaMedico { get; set; }
        public string DireccionPaciente { get; set; }
        public string EmpresaEmpleadora { get; set; }
        public string ActividadRealizar { get; set; }

        //Funciones Vitales
        public string FC { get; set; }
        public string PA { get; set; }
        public string FR { get; set; }
        public string IMC { get; set; }
        public string Sat { get; set; }
        public string PAD { get; set; }

        public string Peso { get; set; }

        //Ascenso a grandes altitudes

        public string Anemia { get; set; }
        public string Cirugia { get; set; }
        public string Desordenes { get; set; }
        public string Embarazo { get; set; }
        public string ProbNeurologicos { get; set; }
        public string Infecciones { get; set; }
        public string Obesidad { get; set; }
        public string ProCardiacos { get; set; }
        public string ProRespiratorios { get; set; }
        public string ProOftalmologico { get; set; }
        public string ProDigestivo { get; set; }
        public string Apnea { get; set; }
        public string Otra { get; set; }
        public string Alergia { get; set; }
        public string MedicacionActual { get; set; }
        public string ApellidosDoctor { get; set; }
        public string DireccionDoctor { get; set; }
        public string NombreDoctor { get; set; }
        public string CMP { get; set; }
        public string AptoAscenderAlturas { get; set; }
        public string v_OwnerOrganizationName { get; set; }
        public string Descripcion { get; set; }

        public int? i_StatusLiquidation { get; set; }
        public object Liq { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string AreaPersonal { get; set; }
        public string talla { get; set; }
        public byte[] RubricaTrabajador { get; set; }

        public string ReflejoTotulianoDerechoSiNo { get; set; }
        public string ReflejoTotulianoIzquierdoSiNo { get; set; }
        public string ReflejoAquileoDerechoSiNo { get; set; }
        public string ReflejoAquileoIzquierdoSiNo { get; set; }
        public string TestPhalenDerechoSiNo { get; set; }
        public string TestPhalenIzquierdoSiNo { get; set; }
        public string TestTinelDerechoSiNo { get; set; }
        public string TestTinelIzquierdoSiNo { get; set; }
        public string SignoLasagueIzquierdoSiNo { get; set; }
        public string SignoLasagueDerechoSiNo { get; set; }
        public string SignoBragardIzquierdoSiNo { get; set; }
        public string SignoBragardDerechoSiNo { get; set; }

        public int? i_IsFac { get; set; }

        public byte[] FirmaMedicoMedicina { get; set; }
        public int i_ServiceId { get; set; }

        public int? i_InicioEnf { get; set; }
        public int? i_CursoEnf { get; set; }
        public int? i_Evolucion { get; set; }
        public int? i_ServiceComponentStatusId { get; set; }

        public string v_ExaAuxResult { get; set; }

        public string v_ObsStatusService { get; set; }


        public DateTime? HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime? TiempoAtencion { get; set; }
        public int? TiempoMuerto { get; set; }
        public int? i_CategoriaId { get; set; }
        public string v_Categoria { get; set; }

        public List<DiagnosticRepositoryList> Diagnosticos { get; set; }
        public string DiagnosticosConcatenado { get; set; }

        public Boolean b_FechaEntrega { get; set; }

        public DateTime? d_FechaEntrega { get; set; }


        public string v_DiagnosticRepositoryId { get; set; }

       public Boolean Seleccion { get; set; }

       public string NombreUsuarioGraba { get; set; }

       public int? i_NroHermanos { get; set; }
       public string v_TipoExamen { get; set; }

       public DateTime? d_InsertDateMedicalAnalyst { get; set; }
       public DateTime? d_InsertDateOccupationalMedical { get; set; }
       public DateTime? d_UpdateDateMedicalAnalyst { get; set; }
       public DateTime? d_UpdateDateOccupationalMedical { get; set; }

       public int? i_InsertUserOccupationalMedicalId { get; set; }
       public int? i_ModalityOfInsurance { get; set; }
       public int? i_ServiceTypeOfInsurance { get; set; }
       public int? i_UpdateUserMedicalAnalystId { get; set; }
       public int? i_UpdateUserOccupationalMedicaltId { get; set; }
       public decimal r_Costo { get; set; }
       public string v_AreaId { get; set; }
       public string v_Motive { get; set; }

       public string v_GroupOccupationId { get; set; }

       public string v_InicioVidaSexaul { get; set; }
       public string v_NroParejasActuales { get; set; }
       public string v_NroAbortos { get; set; }
       public string v_PrecisarCausas { get; set; }


       public int? i_BloodFactorId { get; set; }
       public int? i_BloodGroupId { get; set; }
       public string v_Procedencia { get; set; }
       public string v_CentroEducativo { get; set; }
       public string v_Mail { get; set; }
       public string v_TelephoneNumber { get; set; }

       public List<ServiceComponentList> ListServicesComponents { get; set; }
       //datos Tramas
       public string nombre { get; set; }
       public string genero { get; set; }
       public DateTime? fechaservicio { get; set; }
       public int? edad { get; set; }
       public string tipoServicio { get; set; }
   }


}
