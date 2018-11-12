//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/10 - 09:34:10
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Server.WebClientAdmin.DAL;

namespace Sigesoft.Server.WebClientAdmin.BE
{

    /// <summary>
    /// Assembler for <see cref="attentioninareacomponent"/> and <see cref="attentioninareacomponentDto"/>.
    /// </summary>
    public static partial class attentioninareacomponentAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="attentioninareacomponentDto"/> converted from <see cref="attentioninareacomponent"/>.</param>
        static partial void OnDTO(this attentioninareacomponent entity, attentioninareacomponentDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="attentioninareacomponent"/> converted from <see cref="attentioninareacomponentDto"/>.</param>
        static partial void OnEntity(this attentioninareacomponentDto dto, attentioninareacomponent entity);

        /// <summary>
        /// Converts this instance of <see cref="attentioninareacomponentDto"/> to an instance of <see cref="attentioninareacomponent"/>.
        /// </summary>
        /// <param name="dto"><see cref="attentioninareacomponentDto"/> to convert.</param>
        public static attentioninareacomponent ToEntity(this attentioninareacomponentDto dto)
        {
            if (dto == null) return null;

            var entity = new attentioninareacomponent();

            entity.v_AttentioninAreaComponentId = dto.v_AttentioninAreaComponentId;
            entity.v_AttentionInAreaId = dto.v_AttentionInAreaId;
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
        /// Converts this instance of <see cref="attentioninareacomponent"/> to an instance of <see cref="attentioninareacomponentDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="attentioninareacomponent"/> to convert.</param>
        public static attentioninareacomponentDto ToDTO(this attentioninareacomponent entity)
        {
            if (entity == null) return null;

            var dto = new attentioninareacomponentDto();

            dto.v_AttentioninAreaComponentId = entity.v_AttentioninAreaComponentId;
            dto.v_AttentionInAreaId = entity.v_AttentionInAreaId;
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
        /// Converts each instance of <see cref="attentioninareacomponentDto"/> to an instance of <see cref="attentioninareacomponent"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<attentioninareacomponent> ToEntities(this IEnumerable<attentioninareacomponentDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="attentioninareacomponent"/> to an instance of <see cref="attentioninareacomponentDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<attentioninareacomponentDto> ToDTOs(this IEnumerable<attentioninareacomponent> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
