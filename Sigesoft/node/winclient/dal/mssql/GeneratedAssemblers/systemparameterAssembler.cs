//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/09 - 17:46:39
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
    /// Assembler for <see cref="systemparameter"/> and <see cref="systemparameterDto"/>.
    /// </summary>
    public static partial class systemparameterAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="systemparameterDto"/> converted from <see cref="systemparameter"/>.</param>
        static partial void OnDTO(this systemparameter entity, systemparameterDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="systemparameter"/> converted from <see cref="systemparameterDto"/>.</param>
        static partial void OnEntity(this systemparameterDto dto, systemparameter entity);

        /// <summary>
        /// Converts this instance of <see cref="systemparameterDto"/> to an instance of <see cref="systemparameter"/>.
        /// </summary>
        /// <param name="dto"><see cref="systemparameterDto"/> to convert.</param>
        public static systemparameter ToEntity(this systemparameterDto dto)
        {
            if (dto == null) return null;

            var entity = new systemparameter();

            entity.i_GroupId = dto.i_GroupId;
            entity.i_ParameterId = dto.i_ParameterId;
            entity.v_Value1 = dto.v_Value1;
            entity.v_Value2 = dto.v_Value2;
            entity.v_Field = dto.v_Field;
            entity.i_ParentParameterId = dto.i_ParentParameterId;
            entity.i_Sort = dto.i_Sort;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="systemparameter"/> to an instance of <see cref="systemparameterDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="systemparameter"/> to convert.</param>
        public static systemparameterDto ToDTO(this systemparameter entity)
        {
            if (entity == null) return null;

            var dto = new systemparameterDto();

            dto.i_GroupId = entity.i_GroupId;
            dto.i_ParameterId = entity.i_ParameterId;
            dto.v_Value1 = entity.v_Value1;
            dto.v_Value2 = entity.v_Value2;
            dto.v_Field = entity.v_Field;
            dto.i_ParentParameterId = entity.i_ParentParameterId;
            dto.i_Sort = entity.i_Sort;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="systemparameterDto"/> to an instance of <see cref="systemparameter"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<systemparameter> ToEntities(this IEnumerable<systemparameterDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="systemparameter"/> to an instance of <see cref="systemparameterDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<systemparameterDto> ToDTOs(this IEnumerable<systemparameter> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
