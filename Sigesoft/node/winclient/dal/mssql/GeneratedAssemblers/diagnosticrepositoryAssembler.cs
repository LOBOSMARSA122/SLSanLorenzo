//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 11:57:57
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BE
{

    /// <summary>
    /// Assembler for <see cref="diagnosticrepository"/> and <see cref="diagnosticrepositoryDto"/>.
    /// </summary>
    public static partial class diagnosticrepositoryAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="diagnosticrepositoryDto"/> converted from <see cref="diagnosticrepository"/>.</param>
        static partial void OnDTO(this diagnosticrepository entity, diagnosticrepositoryDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="diagnosticrepository"/> converted from <see cref="diagnosticrepositoryDto"/>.</param>
        static partial void OnEntity(this diagnosticrepositoryDto dto, diagnosticrepository entity);

        /// <summary>
        /// Converts this instance of <see cref="diagnosticrepositoryDto"/> to an instance of <see cref="diagnosticrepository"/>.
        /// </summary>
        /// <param name="dto"><see cref="diagnosticrepositoryDto"/> to convert.</param>
        public static diagnosticrepository ToEntity(this diagnosticrepositoryDto dto)
        {
            if (dto == null) return null;

            var entity = new diagnosticrepository();

            entity.v_DiagnosticRepositoryId = dto.v_DiagnosticRepositoryId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.v_ComponentFieldId = dto.v_ComponentFieldId;
            entity.i_AutoManualId = dto.i_AutoManualId;
            entity.i_PreQualificationId = dto.i_PreQualificationId;
            entity.i_FinalQualificationId = dto.i_FinalQualificationId;
            entity.i_DiagnosticTypeId = dto.i_DiagnosticTypeId;
            entity.i_IsSentToAntecedent = dto.i_IsSentToAntecedent;
            entity.d_ExpirationDateDiagnostic = dto.d_ExpirationDateDiagnostic;
            entity.i_GenerateMedicalBreak = dto.i_GenerateMedicalBreak;
            entity.v_Recomendations = dto.v_Recomendations;
            entity.i_DiagnosticSourceId = dto.i_DiagnosticSourceId;
            entity.i_ShapeAccidentId = dto.i_ShapeAccidentId;
            entity.i_BodyPartId = dto.i_BodyPartId;
            entity.i_ClassificationOfWorkAccidentId = dto.i_ClassificationOfWorkAccidentId;
            entity.i_RiskFactorId = dto.i_RiskFactorId;
            entity.i_ClassificationOfWorkdiseaseId = dto.i_ClassificationOfWorkdiseaseId;
            entity.i_SendToInterconsultationId = dto.i_SendToInterconsultationId;
            entity.i_InterconsultationDestinationId = dto.i_InterconsultationDestinationId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_InterconsultationDestinationId = dto.v_InterconsultationDestinationId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="diagnosticrepository"/> to an instance of <see cref="diagnosticrepositoryDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="diagnosticrepository"/> to convert.</param>
        public static diagnosticrepositoryDto ToDTO(this diagnosticrepository entity)
        {
            if (entity == null) return null;

            var dto = new diagnosticrepositoryDto();

            dto.v_DiagnosticRepositoryId = entity.v_DiagnosticRepositoryId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.v_ComponentFieldId = entity.v_ComponentFieldId;
            dto.i_AutoManualId = entity.i_AutoManualId;
            dto.i_PreQualificationId = entity.i_PreQualificationId;
            dto.i_FinalQualificationId = entity.i_FinalQualificationId;
            dto.i_DiagnosticTypeId = entity.i_DiagnosticTypeId;
            dto.i_IsSentToAntecedent = entity.i_IsSentToAntecedent;
            dto.d_ExpirationDateDiagnostic = entity.d_ExpirationDateDiagnostic;
            dto.i_GenerateMedicalBreak = entity.i_GenerateMedicalBreak;
            dto.v_Recomendations = entity.v_Recomendations;
            dto.i_DiagnosticSourceId = entity.i_DiagnosticSourceId;
            dto.i_ShapeAccidentId = entity.i_ShapeAccidentId;
            dto.i_BodyPartId = entity.i_BodyPartId;
            dto.i_ClassificationOfWorkAccidentId = entity.i_ClassificationOfWorkAccidentId;
            dto.i_RiskFactorId = entity.i_RiskFactorId;
            dto.i_ClassificationOfWorkdiseaseId = entity.i_ClassificationOfWorkdiseaseId;
            dto.i_SendToInterconsultationId = entity.i_SendToInterconsultationId;
            dto.i_InterconsultationDestinationId = entity.i_InterconsultationDestinationId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_InterconsultationDestinationId = entity.v_InterconsultationDestinationId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="diagnosticrepositoryDto"/> to an instance of <see cref="diagnosticrepository"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<diagnosticrepository> ToEntities(this IEnumerable<diagnosticrepositoryDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="diagnosticrepository"/> to an instance of <see cref="diagnosticrepositoryDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<diagnosticrepositoryDto> ToDTOs(this IEnumerable<diagnosticrepository> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
