//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/01/12 - 10:53:48
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
    /// Assembler for <see cref="groupoccupation"/> and <see cref="groupoccupationDto"/>.
    /// </summary>
    public static partial class groupoccupationAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="groupoccupationDto"/> converted from <see cref="groupoccupation"/>.</param>
        static partial void OnDTO(this groupoccupation entity, groupoccupationDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="groupoccupation"/> converted from <see cref="groupoccupationDto"/>.</param>
        static partial void OnEntity(this groupoccupationDto dto, groupoccupation entity);

        /// <summary>
        /// Converts this instance of <see cref="groupoccupationDto"/> to an instance of <see cref="groupoccupation"/>.
        /// </summary>
        /// <param name="dto"><see cref="groupoccupationDto"/> to convert.</param>
        public static groupoccupation ToEntity(this groupoccupationDto dto)
        {
            if (dto == null) return null;

            var entity = new groupoccupation();

            entity.v_GroupOccupationId = dto.v_GroupOccupationId;
            entity.v_LocationId = dto.v_LocationId;
            entity.v_Name = dto.v_Name;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="groupoccupation"/> to an instance of <see cref="groupoccupationDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="groupoccupation"/> to convert.</param>
        public static groupoccupationDto ToDTO(this groupoccupation entity)
        {
            if (entity == null) return null;

            var dto = new groupoccupationDto();

            dto.v_GroupOccupationId = entity.v_GroupOccupationId;
            dto.v_LocationId = entity.v_LocationId;
            dto.v_Name = entity.v_Name;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="groupoccupationDto"/> to an instance of <see cref="groupoccupation"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<groupoccupation> ToEntities(this IEnumerable<groupoccupationDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="groupoccupation"/> to an instance of <see cref="groupoccupationDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<groupoccupationDto> ToDTOs(this IEnumerable<groupoccupation> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
