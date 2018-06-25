//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/19 - 20:56:55
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
    /// Assembler for <see cref="dxfrecuentedetalle"/> and <see cref="dxfrecuentedetalleDto"/>.
    /// </summary>
    public static partial class dxfrecuentedetalleAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="dxfrecuentedetalleDto"/> converted from <see cref="dxfrecuentedetalle"/>.</param>
        static partial void OnDTO(this dxfrecuentedetalle entity, dxfrecuentedetalleDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="dxfrecuentedetalle"/> converted from <see cref="dxfrecuentedetalleDto"/>.</param>
        static partial void OnEntity(this dxfrecuentedetalleDto dto, dxfrecuentedetalle entity);

        /// <summary>
        /// Converts this instance of <see cref="dxfrecuentedetalleDto"/> to an instance of <see cref="dxfrecuentedetalle"/>.
        /// </summary>
        /// <param name="dto"><see cref="dxfrecuentedetalleDto"/> to convert.</param>
        public static dxfrecuentedetalle ToEntity(this dxfrecuentedetalleDto dto)
        {
            if (dto == null) return null;

            var entity = new dxfrecuentedetalle();

            entity.v_DxFrecuenteDetalleId = dto.v_DxFrecuenteDetalleId;
            entity.v_DxFrecuenteId = dto.v_DxFrecuenteId;
            entity.v_MasterRecommendationRestricctionId = dto.v_MasterRecommendationRestricctionId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="dxfrecuentedetalle"/> to an instance of <see cref="dxfrecuentedetalleDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="dxfrecuentedetalle"/> to convert.</param>
        public static dxfrecuentedetalleDto ToDTO(this dxfrecuentedetalle entity)
        {
            if (entity == null) return null;

            var dto = new dxfrecuentedetalleDto();

            dto.v_DxFrecuenteDetalleId = entity.v_DxFrecuenteDetalleId;
            dto.v_DxFrecuenteId = entity.v_DxFrecuenteId;
            dto.v_MasterRecommendationRestricctionId = entity.v_MasterRecommendationRestricctionId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="dxfrecuentedetalleDto"/> to an instance of <see cref="dxfrecuentedetalle"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<dxfrecuentedetalle> ToEntities(this IEnumerable<dxfrecuentedetalleDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="dxfrecuentedetalle"/> to an instance of <see cref="dxfrecuentedetalleDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<dxfrecuentedetalleDto> ToDTOs(this IEnumerable<dxfrecuentedetalle> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
