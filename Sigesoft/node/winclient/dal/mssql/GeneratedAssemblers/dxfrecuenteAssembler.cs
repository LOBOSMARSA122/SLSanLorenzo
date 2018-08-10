//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 09:27:55
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
    /// Assembler for <see cref="dxfrecuente"/> and <see cref="dxfrecuenteDto"/>.
    /// </summary>
    public static partial class dxfrecuenteAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="dxfrecuenteDto"/> converted from <see cref="dxfrecuente"/>.</param>
        static partial void OnDTO(this dxfrecuente entity, dxfrecuenteDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="dxfrecuente"/> converted from <see cref="dxfrecuenteDto"/>.</param>
        static partial void OnEntity(this dxfrecuenteDto dto, dxfrecuente entity);

        /// <summary>
        /// Converts this instance of <see cref="dxfrecuenteDto"/> to an instance of <see cref="dxfrecuente"/>.
        /// </summary>
        /// <param name="dto"><see cref="dxfrecuenteDto"/> to convert.</param>
        public static dxfrecuente ToEntity(this dxfrecuenteDto dto)
        {
            if (dto == null) return null;

            var entity = new dxfrecuente();

            entity.v_DxFrecuenteId = dto.v_DxFrecuenteId;
            entity.v_DiseasesId = dto.v_DiseasesId;
            entity.v_CIE10Id = dto.v_CIE10Id;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="dxfrecuente"/> to an instance of <see cref="dxfrecuenteDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="dxfrecuente"/> to convert.</param>
        public static dxfrecuenteDto ToDTO(this dxfrecuente entity)
        {
            if (entity == null) return null;

            var dto = new dxfrecuenteDto();

            dto.v_DxFrecuenteId = entity.v_DxFrecuenteId;
            dto.v_DiseasesId = entity.v_DiseasesId;
            dto.v_CIE10Id = entity.v_CIE10Id;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="dxfrecuenteDto"/> to an instance of <see cref="dxfrecuente"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<dxfrecuente> ToEntities(this IEnumerable<dxfrecuenteDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="dxfrecuente"/> to an instance of <see cref="dxfrecuenteDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<dxfrecuenteDto> ToDTOs(this IEnumerable<dxfrecuente> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
