using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract]
    public class PacientList
    {
        [DataMember]
        public string v_PersonId { get; set; }
        [DataMember]
        public String v_FirstName { get; set; }
        [DataMember]
        public String v_FirstLastName { get; set; }
        [DataMember]
        public String v_SecondLastName { get; set; }
        [DataMember]
        public int? i_DocTypeId { get; set; }
        [DataMember]
        public String v_DocNumber { get; set; }
        [DataMember]
        public Nullable<DateTime> d_Birthdate { get; set; }
        [DataMember]
        public String v_BirthPlace { get; set; }
        [DataMember]
        public int? i_SexTypeId { get; set; }
        [DataMember]
        public int? i_MaritalStatusId { get; set; }
        [DataMember]
        public int? i_LevelOfId { get; set; }
        [DataMember]
        public String v_TelephoneNumber { get; set; }
        [DataMember]
        public String v_AdressLocation { get; set; }
        [DataMember]
        public String v_GeografyLocationId { get; set; }
        [DataMember]
        public String v_ContactName { get; set; }
        [DataMember]
        public String v_EmergencyPhone { get; set; }
        [DataMember]
        public String v_Mail { get; set; }
        [DataMember]
        public string v_DocTypeName { get; set; }
        [DataMember]
        public string v_SexTypeName { get; set; }
        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
     
        public int? i_IsDeleted { get; set; }
        [DataMember]
        public int? i_UpdateNodeId { get; set; }
      
        public Byte[] b_Photo { get; set; }

        public int? i_Correlative { get; set; }

        public int? i_BloodGroupId { get; set; }

        [DataMember()]
        public Byte[] b_FingerPrintTemplate { get; set; }

        [DataMember()]
        public int? i_FingerPrintQuality { get; set; }

        [DataMember()]
        public int? i_FingerPrintId { get; set; }

        [DataMember()]
        public String v_FingerPrintId { get; set; }

        [DataMember()]
        public Byte[] b_RubricImage { get; set; }
        public int? i_BloodFactorId { get; set; }
        [DataMember()]
        public Byte[] b_FingerPrintImage { get; set; }

        [DataMember()]
        public String t_RubricImageText { get; set; }

        [DataMember()]
        public String b_RubricImageBitVaring { get; set; }

        public string v_CurrentOccupation { get; set; }
        
        [DataMember()]
        public int? i_DepartmentId { get; set; }

        [DataMember()]
        public int? i_ProvinceId { get; set; }

        [DataMember()]
        public int? i_DistrictId { get; set; }

        [DataMember()]
        public int? i_ResidenceInWorkplaceId { get; set; }

        [DataMember()]
        public String v_ResidenceTimeInWorkplace { get; set; }

        [DataMember()]
        public int? i_TypeOfInsuranceId { get; set; }

        [DataMember()]
        public int? i_NumberLivingChildren { get; set; }

        [DataMember()]
        public int? i_NumberDependentChildren { get; set; }

        public String v_OrganitationName { get; set; }

        public string v_MaritalStatus { get; set; }

        public string v_DepartamentName { get; set; }

        public string v_ProvinceName { get; set; }

        public string v_DistrictName { get; set; }


        public int? i_Relationship { get; set; }
        public string v_ExploitedMineral { get; set; }
        public int? i_AltitudeWorkId { get; set; }
        public int? i_PlaceWorkId { get; set; }

        public int? i_Age { get; set; }
        public string v_TypeOfInsuranceName { get; set; }

        public string v_WorkingOrganizationName { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }

        public string v_FullWorkingOrganizationName { get; set; }
        public string v_OwnerName { get; set; }
        public string v_RelationshipName { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string v_OwnerOrganizationName { get; set; }

        public string v_DoctorPhysicalExamName { get; set; }

        public byte[] FirmaTrabajador { get; set; }
        public byte[] FirmaMedico { get; set; }
        public byte[] HuellaTrabajador { get; set; }

        public string v_BloodGroupName { get; set; }
        public string v_BloodFactorName { get; set; }

        public string   v_SexTypeId { get; set; }
        public string v_TipoExamen { get; set; }
        public string v_NombreProtocolo { get; set; }
        public string Aptitud { get; set; }

        public string v_IdService { get; set; }

        public string v_Story { get; set; }
        public string v_MainSymptom { get; set; }

        public int? TimeOfDisease { get; set; }
        public string TiempoEnfermedad { get; set; }
        public string InicioEnfermedad { get; set; }
        public string CursoEnfermedad { get; set; }

        public byte[] FirmaDoctor { get; set; }

        public string v_ExaAuxResult { get; set; }
        public string v_ProtocoloId { get; set; }

        public int? i_EsoTypeId { get; set; }

        public string v_NroPoliza { get; set; }
        public decimal? v_Deducible { get; set; }

        public int? i_NroHermanos { get; set; }

        public byte[] FirmaDoctorAuditor { get; set; }

        public int? i_InsertUserId { get; set; }
        public int? i_UpdateUserId { get; set; }

        public DateTime? d_InsertDate { get; set; }



        public int? i_OccupationTypeId { get; set; }
        public int? i_NumberLiveChildren { get; set; }
        public int? i_NumberDeadChildren { get; set; }

        public byte[] b_PersonImage { get; set; }
     
        public int? i_InsertNodeId { get; set; }

        public String v_Password { get; set; }
        public String GESO { get; set; }
        public int? i_AptitudeStatusId { get; set; }

        public String v_ObsStatusService { get; set; }

        // Antecedentes ginecologicos
        public DateTime? d_PAP { get; set; }
        public DateTime? d_Mamografia { get; set; }
        public string v_Gestapara { get; set; }
        public string v_Menarquia { get; set; }
        public string v_CiruGine { get; set; }

        public string v_Findings { get; set; }
        public DateTime? d_Fur { get; set; }
        public string v_CatemenialRegime { get; set; }
        public int? i_MacId { get; set; }
        public string v_Mac { get; set; }

        public string Trabajador { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string MedicoGrabaMedicina { get; set; }
        public int? Edad { get; set; }
        public string Genero { get; set; }
        public string Empresa { get; set; }
        public string Sede { get; set; }

        public byte[] b_FirmaEvaluador { get; set; }
        public byte[] b_FirmaAuditor { get; set; }

        public string GradoInstruccion { get; set; }
        public string EstadoCivil { get; set; }
    }
}
