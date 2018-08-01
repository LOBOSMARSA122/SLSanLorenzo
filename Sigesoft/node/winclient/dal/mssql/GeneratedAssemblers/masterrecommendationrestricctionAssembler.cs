//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/01 - 09:36:09
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
    /// Assembler for <see cref="masterrecommendationrestricction"/> and <see cref="masterrecommendationrestricctionDto"/>.
    /// </summary>
    public static partial class masterrecommendationrestricctionAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="masterrecommendationrestricctionDto"/> converted from <see cref="masterrecommendationrestricction"/>.</param>
        static partial void OnDTO(this masterrecommendationrestricction entity, masterrecommendationrestricctionDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="masterrecommendationrestricction"/> converted from <see cref="masterrecommendationrestricctionDto"/>.</param>
        static partial void OnEntity(this masterrecommendationrestricctionDto dto, masterrecommendationrestricction entity);

        /// <summary>
        /// Converts this instance of <see cref="masterrecommendationrestricctionDto"/> to an instance of <see cref="masterrecommendationrestricction"/>.
        /// </summary>
        /// <param name="dto"><see cref="masterrecommendationrestricctionDto"/> to convert.</param>
        public static masterrecommendationrestricction ToEntity(this masterrecommendationrestricctionDto dto)
        {
            if (dto == null) return null;

            var entity = new masterrecommendationrestricction();

            entity.v_MasterRecommendationRestricctionId = dto.v_MasterRecommendationRestricctionId;
            entity.v_Name = dto.v_Name;
            entity.i_TypifyingId = dto.i_TypifyingId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="masterrecommendationrestricction"/> to an instance of <see cref="masterrecommendationrestricctionDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="masterrecommendationrestricction"/> to convert.</param>
        public static masterrecommendationrestricctionDto ToDTO(this masterrecommendationrestricction entity)
        {
            if (entity == null) return null;

            var dto = new masterrecommendationrestricctionDto();

            dto.v_MasterRecommendationRestricctionId = entity.v_MasterRecommendationRestricctionId;
            dto.v_Name = entity.v_Name;
            dto.i_TypifyingId = entity.i_TypifyingId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="masterrecommendationrestricctionDto"/> to an instance of <see cref="masterrecommendationrestricction"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<masterrecommendationrestricction> ToEntities(this IEnumerable<masterrecommendationrestricctionDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="masterrecommendationrestricction"/> to an instance of <see cref="masterrecommendationrestricctionDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<masterrecommendationrestricctionDto> ToDTOs(this IEnumerable<masterrecommendationrestricction> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
