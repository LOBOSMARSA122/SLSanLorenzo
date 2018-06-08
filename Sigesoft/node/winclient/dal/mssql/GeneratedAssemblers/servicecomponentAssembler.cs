//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/08 - 10:01:50
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
    /// Assembler for <see cref="servicecomponent"/> and <see cref="servicecomponentDto"/>.
    /// </summary>
    public static partial class servicecomponentAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="servicecomponentDto"/> converted from <see cref="servicecomponent"/>.</param>
        static partial void OnDTO(this servicecomponent entity, servicecomponentDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="servicecomponent"/> converted from <see cref="servicecomponentDto"/>.</param>
        static partial void OnEntity(this servicecomponentDto dto, servicecomponent entity);

        /// <summary>
        /// Converts this instance of <see cref="servicecomponentDto"/> to an instance of <see cref="servicecomponent"/>.
        /// </summary>
        /// <param name="dto"><see cref="servicecomponentDto"/> to convert.</param>
        public static servicecomponent ToEntity(this servicecomponentDto dto)
        {
            if (dto == null) return null;

            var entity = new servicecomponent();

            entity.v_ServiceComponentId = dto.v_ServiceComponentId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.i_ServiceComponentStatusId = dto.i_ServiceComponentStatusId;
            entity.i_ExternalInternalId = dto.i_ExternalInternalId;
            entity.i_ServiceComponentTypeId = dto.i_ServiceComponentTypeId;
            entity.i_IsVisibleId = dto.i_IsVisibleId;
            entity.i_IsInheritedId = dto.i_IsInheritedId;
            entity.d_CalledDate = dto.d_CalledDate;
            entity.d_StartDate = dto.d_StartDate;
            entity.d_EndDate = dto.d_EndDate;
            entity.i_index = dto.i_index;
            entity.r_Price = dto.r_Price;
            entity.i_IsInvoicedId = dto.i_IsInvoicedId;
            entity.i_IsRequiredId = dto.i_IsRequiredId;
            entity.i_IsManuallyAddedId = dto.i_IsManuallyAddedId;
            entity.i_QueueStatusId = dto.i_QueueStatusId;
            entity.v_NameOfice = dto.v_NameOfice;
            entity.v_Comment = dto.v_Comment;
            entity.i_Iscalling = dto.i_Iscalling;
            entity.i_IsApprovedId = dto.i_IsApprovedId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_ApprovedInsertUserId = dto.i_ApprovedInsertUserId;
            entity.i_ApprovedUpdateUserId = dto.i_ApprovedUpdateUserId;
            entity.d_ApprovedInsertDate = dto.d_ApprovedInsertDate;
            entity.d_ApprovedUpdateDate = dto.d_ApprovedUpdateDate;
            entity.i_InsertUserMedicalAnalystId = dto.i_InsertUserMedicalAnalystId;
            entity.i_UpdateUserMedicalAnalystId = dto.i_UpdateUserMedicalAnalystId;
            entity.d_InsertDateMedicalAnalyst = dto.d_InsertDateMedicalAnalyst;
            entity.d_UpdateDateMedicalAnalyst = dto.d_UpdateDateMedicalAnalyst;
            entity.i_InsertUserTechnicalDataRegisterId = dto.i_InsertUserTechnicalDataRegisterId;
            entity.i_UpdateUserTechnicalDataRegisterId = dto.i_UpdateUserTechnicalDataRegisterId;
            entity.d_InsertDateTechnicalDataRegister = dto.d_InsertDateTechnicalDataRegister;
            entity.d_UpdateDateTechnicalDataRegister = dto.d_UpdateDateTechnicalDataRegister;
            entity.i_Iscalling_1 = dto.i_Iscalling_1;
            entity.i_AuditorInsertUserId = dto.i_AuditorInsertUserId;
            entity.d_AuditorInsertUser = dto.d_AuditorInsertUser;
            entity.i_AuditorUpdateUserId = dto.i_AuditorUpdateUserId;
            entity.d_AuditorUpdateUser = dto.d_AuditorUpdateUser;
            entity.v_IdUnidadProductiva = dto.v_IdUnidadProductiva;
            entity.d_SaldoPaciente = dto.d_SaldoPaciente;
            entity.d_SaldoAseguradora = dto.d_SaldoAseguradora;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="servicecomponent"/> to an instance of <see cref="servicecomponentDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="servicecomponent"/> to convert.</param>
        public static servicecomponentDto ToDTO(this servicecomponent entity)
        {
            if (entity == null) return null;

            var dto = new servicecomponentDto();

            dto.v_ServiceComponentId = entity.v_ServiceComponentId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.i_ServiceComponentStatusId = entity.i_ServiceComponentStatusId;
            dto.i_ExternalInternalId = entity.i_ExternalInternalId;
            dto.i_ServiceComponentTypeId = entity.i_ServiceComponentTypeId;
            dto.i_IsVisibleId = entity.i_IsVisibleId;
            dto.i_IsInheritedId = entity.i_IsInheritedId;
            dto.d_CalledDate = entity.d_CalledDate;
            dto.d_StartDate = entity.d_StartDate;
            dto.d_EndDate = entity.d_EndDate;
            dto.i_index = entity.i_index;
            dto.r_Price = entity.r_Price;
            dto.i_IsInvoicedId = entity.i_IsInvoicedId;
            dto.i_IsRequiredId = entity.i_IsRequiredId;
            dto.i_IsManuallyAddedId = entity.i_IsManuallyAddedId;
            dto.i_QueueStatusId = entity.i_QueueStatusId;
            dto.v_NameOfice = entity.v_NameOfice;
            dto.v_Comment = entity.v_Comment;
            dto.i_Iscalling = entity.i_Iscalling;
            dto.i_IsApprovedId = entity.i_IsApprovedId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_ApprovedInsertUserId = entity.i_ApprovedInsertUserId;
            dto.i_ApprovedUpdateUserId = entity.i_ApprovedUpdateUserId;
            dto.d_ApprovedInsertDate = entity.d_ApprovedInsertDate;
            dto.d_ApprovedUpdateDate = entity.d_ApprovedUpdateDate;
            dto.i_InsertUserMedicalAnalystId = entity.i_InsertUserMedicalAnalystId;
            dto.i_UpdateUserMedicalAnalystId = entity.i_UpdateUserMedicalAnalystId;
            dto.d_InsertDateMedicalAnalyst = entity.d_InsertDateMedicalAnalyst;
            dto.d_UpdateDateMedicalAnalyst = entity.d_UpdateDateMedicalAnalyst;
            dto.i_InsertUserTechnicalDataRegisterId = entity.i_InsertUserTechnicalDataRegisterId;
            dto.i_UpdateUserTechnicalDataRegisterId = entity.i_UpdateUserTechnicalDataRegisterId;
            dto.d_InsertDateTechnicalDataRegister = entity.d_InsertDateTechnicalDataRegister;
            dto.d_UpdateDateTechnicalDataRegister = entity.d_UpdateDateTechnicalDataRegister;
            dto.i_Iscalling_1 = entity.i_Iscalling_1;
            dto.i_AuditorInsertUserId = entity.i_AuditorInsertUserId;
            dto.d_AuditorInsertUser = entity.d_AuditorInsertUser;
            dto.i_AuditorUpdateUserId = entity.i_AuditorUpdateUserId;
            dto.d_AuditorUpdateUser = entity.d_AuditorUpdateUser;
            dto.v_IdUnidadProductiva = entity.v_IdUnidadProductiva;
            dto.d_SaldoPaciente = entity.d_SaldoPaciente;
            dto.d_SaldoAseguradora = entity.d_SaldoAseguradora;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="servicecomponentDto"/> to an instance of <see cref="servicecomponent"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<servicecomponent> ToEntities(this IEnumerable<servicecomponentDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="servicecomponent"/> to an instance of <see cref="servicecomponentDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<servicecomponentDto> ToDTOs(this IEnumerable<servicecomponent> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
