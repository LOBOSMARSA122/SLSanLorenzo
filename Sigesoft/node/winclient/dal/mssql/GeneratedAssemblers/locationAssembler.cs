//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/26 - 17:37:02
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
    /// Assembler for <see cref="location"/> and <see cref="locationDto"/>.
    /// </summary>
    public static partial class locationAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="locationDto"/> converted from <see cref="location"/>.</param>
        static partial void OnDTO(this location entity, locationDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="location"/> converted from <see cref="locationDto"/>.</param>
        static partial void OnEntity(this locationDto dto, location entity);

        /// <summary>
        /// Converts this instance of <see cref="locationDto"/> to an instance of <see cref="location"/>.
        /// </summary>
        /// <param name="dto"><see cref="locationDto"/> to convert.</param>
        public static location ToEntity(this locationDto dto)
        {
            if (dto == null) return null;

            var entity = new location();

            entity.v_LocationId = dto.v_LocationId;
            entity.v_OrganizationId = dto.v_OrganizationId;
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
        /// Converts this instance of <see cref="location"/> to an instance of <see cref="locationDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="location"/> to convert.</param>
        public static locationDto ToDTO(this location entity)
        {
            if (entity == null) return null;

            var dto = new locationDto();

            dto.v_LocationId = entity.v_LocationId;
            dto.v_OrganizationId = entity.v_OrganizationId;
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
        /// Converts each instance of <see cref="locationDto"/> to an instance of <see cref="location"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<location> ToEntities(this IEnumerable<locationDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="location"/> to an instance of <see cref="locationDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<locationDto> ToDTOs(this IEnumerable<location> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
