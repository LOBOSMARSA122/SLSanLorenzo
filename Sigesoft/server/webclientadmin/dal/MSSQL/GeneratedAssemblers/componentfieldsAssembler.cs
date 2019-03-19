//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/14 - 10:38:47
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
    /// Assembler for <see cref="componentfields"/> and <see cref="componentfieldsDto"/>.
    /// </summary>
    public static partial class componentfieldsAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldsDto"/> converted from <see cref="componentfields"/>.</param>
        static partial void OnDTO(this componentfields entity, componentfieldsDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="componentfields"/> converted from <see cref="componentfieldsDto"/>.</param>
        static partial void OnEntity(this componentfieldsDto dto, componentfields entity);

        /// <summary>
        /// Converts this instance of <see cref="componentfieldsDto"/> to an instance of <see cref="componentfields"/>.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldsDto"/> to convert.</param>
        public static componentfields ToEntity(this componentfieldsDto dto)
        {
            if (dto == null) return null;

            var entity = new componentfields();

            entity.v_ComponentId = dto.v_ComponentId;
            entity.v_ComponentFieldId = dto.v_ComponentFieldId;
            entity.v_Group = dto.v_Group;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="componentfields"/> to an instance of <see cref="componentfieldsDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="componentfields"/> to convert.</param>
        public static componentfieldsDto ToDTO(this componentfields entity)
        {
            if (entity == null) return null;

            var dto = new componentfieldsDto();

            dto.v_ComponentId = entity.v_ComponentId;
            dto.v_ComponentFieldId = entity.v_ComponentFieldId;
            dto.v_Group = entity.v_Group;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfieldsDto"/> to an instance of <see cref="componentfields"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<componentfields> ToEntities(this IEnumerable<componentfieldsDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfields"/> to an instance of <see cref="componentfieldsDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<componentfieldsDto> ToDTOs(this IEnumerable<componentfields> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
