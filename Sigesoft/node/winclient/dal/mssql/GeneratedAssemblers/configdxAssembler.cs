//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/04/01 - 14:37:16
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
    /// Assembler for <see cref="configdx"/> and <see cref="configdxDto"/>.
    /// </summary>
    public static partial class configdxAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="configdxDto"/> converted from <see cref="configdx"/>.</param>
        static partial void OnDTO(this configdx entity, configdxDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="configdx"/> converted from <see cref="configdxDto"/>.</param>
        static partial void OnEntity(this configdxDto dto, configdx entity);

        /// <summary>
        /// Converts this instance of <see cref="configdxDto"/> to an instance of <see cref="configdx"/>.
        /// </summary>
        /// <param name="dto"><see cref="configdxDto"/> to convert.</param>
        public static configdx ToEntity(this configdxDto dto)
        {
            if (dto == null) return null;

            var entity = new configdx();

            entity.v_ConfigDxId = dto.v_ConfigDxId;
            entity.v_DiseaseId = dto.v_DiseaseId;
            entity.v_ProductId = dto.v_ProductId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="configdx"/> to an instance of <see cref="configdxDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="configdx"/> to convert.</param>
        public static configdxDto ToDTO(this configdx entity)
        {
            if (entity == null) return null;

            var dto = new configdxDto();

            dto.v_ConfigDxId = entity.v_ConfigDxId;
            dto.v_DiseaseId = entity.v_DiseaseId;
            dto.v_ProductId = entity.v_ProductId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="configdxDto"/> to an instance of <see cref="configdx"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<configdx> ToEntities(this IEnumerable<configdxDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="configdx"/> to an instance of <see cref="configdxDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<configdxDto> ToDTOs(this IEnumerable<configdx> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
