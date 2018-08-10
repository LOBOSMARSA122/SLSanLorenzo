//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/09 - 16:46:45
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
    /// Assembler for <see cref="auxiliaryexam"/> and <see cref="auxiliaryexamDto"/>.
    /// </summary>
    public static partial class auxiliaryexamAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="auxiliaryexamDto"/> converted from <see cref="auxiliaryexam"/>.</param>
        static partial void OnDTO(this auxiliaryexam entity, auxiliaryexamDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="auxiliaryexam"/> converted from <see cref="auxiliaryexamDto"/>.</param>
        static partial void OnEntity(this auxiliaryexamDto dto, auxiliaryexam entity);

        /// <summary>
        /// Converts this instance of <see cref="auxiliaryexamDto"/> to an instance of <see cref="auxiliaryexam"/>.
        /// </summary>
        /// <param name="dto"><see cref="auxiliaryexamDto"/> to convert.</param>
        public static auxiliaryexam ToEntity(this auxiliaryexamDto dto)
        {
            if (dto == null) return null;

            var entity = new auxiliaryexam();

            entity.v_AuxiliaryExamId = dto.v_AuxiliaryExamId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ComponentId = dto.v_ComponentId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="auxiliaryexam"/> to an instance of <see cref="auxiliaryexamDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="auxiliaryexam"/> to convert.</param>
        public static auxiliaryexamDto ToDTO(this auxiliaryexam entity)
        {
            if (entity == null) return null;

            var dto = new auxiliaryexamDto();

            dto.v_AuxiliaryExamId = entity.v_AuxiliaryExamId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ComponentId = entity.v_ComponentId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="auxiliaryexamDto"/> to an instance of <see cref="auxiliaryexam"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<auxiliaryexam> ToEntities(this IEnumerable<auxiliaryexamDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="auxiliaryexam"/> to an instance of <see cref="auxiliaryexamDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<auxiliaryexamDto> ToDTOs(this IEnumerable<auxiliaryexam> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
