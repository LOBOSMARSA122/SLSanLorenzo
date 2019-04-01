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
    /// Assembler for <see cref="componentfieldvaluesrecommendation"/> and <see cref="componentfieldvaluesrecommendationDto"/>.
    /// </summary>
    public static partial class componentfieldvaluesrecommendationAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldvaluesrecommendationDto"/> converted from <see cref="componentfieldvaluesrecommendation"/>.</param>
        static partial void OnDTO(this componentfieldvaluesrecommendation entity, componentfieldvaluesrecommendationDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="componentfieldvaluesrecommendation"/> converted from <see cref="componentfieldvaluesrecommendationDto"/>.</param>
        static partial void OnEntity(this componentfieldvaluesrecommendationDto dto, componentfieldvaluesrecommendation entity);

        /// <summary>
        /// Converts this instance of <see cref="componentfieldvaluesrecommendationDto"/> to an instance of <see cref="componentfieldvaluesrecommendation"/>.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldvaluesrecommendationDto"/> to convert.</param>
        public static componentfieldvaluesrecommendation ToEntity(this componentfieldvaluesrecommendationDto dto)
        {
            if (dto == null) return null;

            var entity = new componentfieldvaluesrecommendation();

            entity.v_ComponentFieldValuesRecommendationId = dto.v_ComponentFieldValuesRecommendationId;
            entity.v_ComponentFieldValuesId = dto.v_ComponentFieldValuesId;
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
        /// Converts this instance of <see cref="componentfieldvaluesrecommendation"/> to an instance of <see cref="componentfieldvaluesrecommendationDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="componentfieldvaluesrecommendation"/> to convert.</param>
        public static componentfieldvaluesrecommendationDto ToDTO(this componentfieldvaluesrecommendation entity)
        {
            if (entity == null) return null;

            var dto = new componentfieldvaluesrecommendationDto();

            dto.v_ComponentFieldValuesRecommendationId = entity.v_ComponentFieldValuesRecommendationId;
            dto.v_ComponentFieldValuesId = entity.v_ComponentFieldValuesId;
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
        /// Converts each instance of <see cref="componentfieldvaluesrecommendationDto"/> to an instance of <see cref="componentfieldvaluesrecommendation"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<componentfieldvaluesrecommendation> ToEntities(this IEnumerable<componentfieldvaluesrecommendationDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfieldvaluesrecommendation"/> to an instance of <see cref="componentfieldvaluesrecommendationDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<componentfieldvaluesrecommendationDto> ToDTOs(this IEnumerable<componentfieldvaluesrecommendation> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
