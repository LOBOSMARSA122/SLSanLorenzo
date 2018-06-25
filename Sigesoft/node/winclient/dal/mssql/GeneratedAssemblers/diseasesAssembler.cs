//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/19 - 20:56:54
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
    /// Assembler for <see cref="diseases"/> and <see cref="diseasesDto"/>.
    /// </summary>
    public static partial class diseasesAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="diseasesDto"/> converted from <see cref="diseases"/>.</param>
        static partial void OnDTO(this diseases entity, diseasesDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="diseases"/> converted from <see cref="diseasesDto"/>.</param>
        static partial void OnEntity(this diseasesDto dto, diseases entity);

        /// <summary>
        /// Converts this instance of <see cref="diseasesDto"/> to an instance of <see cref="diseases"/>.
        /// </summary>
        /// <param name="dto"><see cref="diseasesDto"/> to convert.</param>
        public static diseases ToEntity(this diseasesDto dto)
        {
            if (dto == null) return null;

            var entity = new diseases();

            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.v_CIE10Id = dto.v_CIE10Id;
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
        /// Converts this instance of <see cref="diseases"/> to an instance of <see cref="diseasesDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="diseases"/> to convert.</param>
        public static diseasesDto ToDTO(this diseases entity)
        {
            if (entity == null) return null;

            var dto = new diseasesDto();

            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.v_CIE10Id = entity.v_CIE10Id;
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
        /// Converts each instance of <see cref="diseasesDto"/> to an instance of <see cref="diseases"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<diseases> ToEntities(this IEnumerable<diseasesDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="diseases"/> to an instance of <see cref="diseasesDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<diseasesDto> ToDTOs(this IEnumerable<diseases> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
