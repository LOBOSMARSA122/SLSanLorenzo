//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/08 - 10:01:40
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
    /// Assembler for <see cref="pacientmultimediadata"/> and <see cref="pacientmultimediadataDto"/>.
    /// </summary>
    public static partial class pacientmultimediadataAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="pacientmultimediadataDto"/> converted from <see cref="pacientmultimediadata"/>.</param>
        static partial void OnDTO(this pacientmultimediadata entity, pacientmultimediadataDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="pacientmultimediadata"/> converted from <see cref="pacientmultimediadataDto"/>.</param>
        static partial void OnEntity(this pacientmultimediadataDto dto, pacientmultimediadata entity);

        /// <summary>
        /// Converts this instance of <see cref="pacientmultimediadataDto"/> to an instance of <see cref="pacientmultimediadata"/>.
        /// </summary>
        /// <param name="dto"><see cref="pacientmultimediadataDto"/> to convert.</param>
        public static pacientmultimediadata ToEntity(this pacientmultimediadataDto dto)
        {
            if (dto == null) return null;

            var entity = new pacientmultimediadata();

            entity.v_PacientMultimediaDataId = dto.v_PacientMultimediaDataId;
            entity.v_PersonId = dto.v_PersonId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="pacientmultimediadata"/> to an instance of <see cref="pacientmultimediadataDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="pacientmultimediadata"/> to convert.</param>
        public static pacientmultimediadataDto ToDTO(this pacientmultimediadata entity)
        {
            if (entity == null) return null;

            var dto = new pacientmultimediadataDto();

            dto.v_PacientMultimediaDataId = entity.v_PacientMultimediaDataId;
            dto.v_PersonId = entity.v_PersonId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="pacientmultimediadataDto"/> to an instance of <see cref="pacientmultimediadata"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<pacientmultimediadata> ToEntities(this IEnumerable<pacientmultimediadataDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="pacientmultimediadata"/> to an instance of <see cref="pacientmultimediadataDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<pacientmultimediadataDto> ToDTOs(this IEnumerable<pacientmultimediadata> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
