//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/30 - 10:17:13
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
    /// Assembler for <see cref="additionalexam"/> and <see cref="additionalexamDto"/>.
    /// </summary>
    public static partial class additionalexamAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="additionalexamDto"/> converted from <see cref="additionalexam"/>.</param>
        static partial void OnDTO(this additionalexam entity, additionalexamDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="additionalexam"/> converted from <see cref="additionalexamDto"/>.</param>
        static partial void OnEntity(this additionalexamDto dto, additionalexam entity);

        /// <summary>
        /// Converts this instance of <see cref="additionalexamDto"/> to an instance of <see cref="additionalexam"/>.
        /// </summary>
        /// <param name="dto"><see cref="additionalexamDto"/> to convert.</param>
        public static additionalexam ToEntity(this additionalexamDto dto)
        {
            if (dto == null) return null;

            var entity = new additionalexam();

            entity.v_AdditionalExamId = dto.v_AdditionalExamId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_ProtocolId = dto.v_ProtocolId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.v_Commentary = dto.v_Commentary;
            entity.i_IsProcessed = dto.i_IsProcessed;
            entity.i_IsNewService = dto.i_IsNewService;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="additionalexam"/> to an instance of <see cref="additionalexamDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="additionalexam"/> to convert.</param>
        public static additionalexamDto ToDTO(this additionalexam entity)
        {
            if (entity == null) return null;

            var dto = new additionalexamDto();

            dto.v_AdditionalExamId = entity.v_AdditionalExamId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_ProtocolId = entity.v_ProtocolId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.v_Commentary = entity.v_Commentary;
            dto.i_IsProcessed = entity.i_IsProcessed;
            dto.i_IsNewService = entity.i_IsNewService;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="additionalexamDto"/> to an instance of <see cref="additionalexam"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<additionalexam> ToEntities(this IEnumerable<additionalexamDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="additionalexam"/> to an instance of <see cref="additionalexamDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<additionalexamDto> ToDTOs(this IEnumerable<additionalexam> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
