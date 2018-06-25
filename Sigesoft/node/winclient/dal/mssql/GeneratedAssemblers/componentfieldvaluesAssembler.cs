//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/19 - 20:56:52
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
    /// Assembler for <see cref="componentfieldvalues"/> and <see cref="componentfieldvaluesDto"/>.
    /// </summary>
    public static partial class componentfieldvaluesAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldvaluesDto"/> converted from <see cref="componentfieldvalues"/>.</param>
        static partial void OnDTO(this componentfieldvalues entity, componentfieldvaluesDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="componentfieldvalues"/> converted from <see cref="componentfieldvaluesDto"/>.</param>
        static partial void OnEntity(this componentfieldvaluesDto dto, componentfieldvalues entity);

        /// <summary>
        /// Converts this instance of <see cref="componentfieldvaluesDto"/> to an instance of <see cref="componentfieldvalues"/>.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldvaluesDto"/> to convert.</param>
        public static componentfieldvalues ToEntity(this componentfieldvaluesDto dto)
        {
            if (dto == null) return null;

            var entity = new componentfieldvalues();

            entity.v_ComponentFieldValuesId = dto.v_ComponentFieldValuesId;
            entity.v_Diseases = dto.v_Diseases;
            entity.v_ComponentFieldId = dto.v_ComponentFieldId;
            entity.v_AnalyzingValue1 = dto.v_AnalyzingValue1;
            entity.v_AnalyzingValue2 = dto.v_AnalyzingValue2;
            entity.i_OperatorId = dto.i_OperatorId;
            entity.v_LegalStandard = dto.v_LegalStandard;
            entity.i_IsAnormal = dto.i_IsAnormal;
            entity.i_ValidationMonths = dto.i_ValidationMonths;
            entity.i_GenderId = dto.i_GenderId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="componentfieldvalues"/> to an instance of <see cref="componentfieldvaluesDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="componentfieldvalues"/> to convert.</param>
        public static componentfieldvaluesDto ToDTO(this componentfieldvalues entity)
        {
            if (entity == null) return null;

            var dto = new componentfieldvaluesDto();

            dto.v_ComponentFieldValuesId = entity.v_ComponentFieldValuesId;
            dto.v_Diseases = entity.v_Diseases;
            dto.v_ComponentFieldId = entity.v_ComponentFieldId;
            dto.v_AnalyzingValue1 = entity.v_AnalyzingValue1;
            dto.v_AnalyzingValue2 = entity.v_AnalyzingValue2;
            dto.i_OperatorId = entity.i_OperatorId;
            dto.v_LegalStandard = entity.v_LegalStandard;
            dto.i_IsAnormal = entity.i_IsAnormal;
            dto.i_ValidationMonths = entity.i_ValidationMonths;
            dto.i_GenderId = entity.i_GenderId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfieldvaluesDto"/> to an instance of <see cref="componentfieldvalues"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<componentfieldvalues> ToEntities(this IEnumerable<componentfieldvaluesDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfieldvalues"/> to an instance of <see cref="componentfieldvaluesDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<componentfieldvaluesDto> ToDTOs(this IEnumerable<componentfieldvalues> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
