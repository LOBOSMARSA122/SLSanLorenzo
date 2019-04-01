//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/04/01 - 14:34:43
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class diagnosticrepositoryDto
    {
        [DataMember()]
        public String v_DiagnosticRepositoryId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_DiseasesId { get; set; }

        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_ComponentFieldId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_AutoManualId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_PreQualificationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_FinalQualificationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DiagnosticTypeId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsSentToAntecedent { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_ExpirationDateDiagnostic { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GenerateMedicalBreak { get; set; }

        [DataMember()]
        public String v_Recomendations { get; set; }

        [DataMember()]
        public Nullable<Int32> i_DiagnosticSourceId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ShapeAccidentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_BodyPartId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ClassificationOfWorkAccidentId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_RiskFactorId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ClassificationOfWorkdiseaseId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SendToInterconsultationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InterconsultationDestinationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public String v_InterconsultationDestinationId { get; set; }

        [DataMember()]
        public componentfieldsDto componentfields { get; set; }

        [DataMember()]
        public diseasesDto diseases { get; set; }

        [DataMember()]
        public serviceDto service { get; set; }

        [DataMember()]
        public List<recommendationDto> recommendation { get; set; }

        [DataMember()]
        public List<restrictionDto> restriction { get; set; }

        public diagnosticrepositoryDto()
        {
        }

        public diagnosticrepositoryDto(String v_DiagnosticRepositoryId, String v_ServiceId, String v_DiseasesId, String v_ComponentId, String v_ComponentFieldId, Nullable<Int32> i_AutoManualId, Nullable<Int32> i_PreQualificationId, Nullable<Int32> i_FinalQualificationId, Nullable<Int32> i_DiagnosticTypeId, Nullable<Int32> i_IsSentToAntecedent, Nullable<DateTime> d_ExpirationDateDiagnostic, Nullable<Int32> i_GenerateMedicalBreak, String v_Recomendations, Nullable<Int32> i_DiagnosticSourceId, Nullable<Int32> i_ShapeAccidentId, Nullable<Int32> i_BodyPartId, Nullable<Int32> i_ClassificationOfWorkAccidentId, Nullable<Int32> i_RiskFactorId, Nullable<Int32> i_ClassificationOfWorkdiseaseId, Nullable<Int32> i_SendToInterconsultationId, Nullable<Int32> i_InterconsultationDestinationId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_InterconsultationDestinationId, componentfieldsDto componentfields, diseasesDto diseases, serviceDto service, List<recommendationDto> recommendation, List<restrictionDto> restriction)
        {
			this.v_DiagnosticRepositoryId = v_DiagnosticRepositoryId;
			this.v_ServiceId = v_ServiceId;
			this.v_DiseasesId = v_DiseasesId;
			this.v_ComponentId = v_ComponentId;
			this.v_ComponentFieldId = v_ComponentFieldId;
			this.i_AutoManualId = i_AutoManualId;
			this.i_PreQualificationId = i_PreQualificationId;
			this.i_FinalQualificationId = i_FinalQualificationId;
			this.i_DiagnosticTypeId = i_DiagnosticTypeId;
			this.i_IsSentToAntecedent = i_IsSentToAntecedent;
			this.d_ExpirationDateDiagnostic = d_ExpirationDateDiagnostic;
			this.i_GenerateMedicalBreak = i_GenerateMedicalBreak;
			this.v_Recomendations = v_Recomendations;
			this.i_DiagnosticSourceId = i_DiagnosticSourceId;
			this.i_ShapeAccidentId = i_ShapeAccidentId;
			this.i_BodyPartId = i_BodyPartId;
			this.i_ClassificationOfWorkAccidentId = i_ClassificationOfWorkAccidentId;
			this.i_RiskFactorId = i_RiskFactorId;
			this.i_ClassificationOfWorkdiseaseId = i_ClassificationOfWorkdiseaseId;
			this.i_SendToInterconsultationId = i_SendToInterconsultationId;
			this.i_InterconsultationDestinationId = i_InterconsultationDestinationId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_InterconsultationDestinationId = v_InterconsultationDestinationId;
			this.componentfields = componentfields;
			this.diseases = diseases;
			this.service = service;
			this.recommendation = recommendation;
			this.restriction = restriction;
        }
    }
}
