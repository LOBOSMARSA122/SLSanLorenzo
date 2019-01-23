//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/01/23 - 17:50:15
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
    /// Assembler for <see cref="applicationhierarchy"/> and <see cref="applicationhierarchyDto"/>.
    /// </summary>
    public static partial class applicationhierarchyAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="applicationhierarchyDto"/> converted from <see cref="applicationhierarchy"/>.</param>
        static partial void OnDTO(this applicationhierarchy entity, applicationhierarchyDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="applicationhierarchy"/> converted from <see cref="applicationhierarchyDto"/>.</param>
        static partial void OnEntity(this applicationhierarchyDto dto, applicationhierarchy entity);

        /// <summary>
        /// Converts this instance of <see cref="applicationhierarchyDto"/> to an instance of <see cref="applicationhierarchy"/>.
        /// </summary>
        /// <param name="dto"><see cref="applicationhierarchyDto"/> to convert.</param>
        public static applicationhierarchy ToEntity(this applicationhierarchyDto dto)
        {
            if (dto == null) return null;

            var entity = new applicationhierarchy();

            entity.i_ApplicationHierarchyId = dto.i_ApplicationHierarchyId;
            entity.i_ApplicationHierarchyTypeId = dto.i_ApplicationHierarchyTypeId;
            entity.i_Level = dto.i_Level;
            entity.v_Description = dto.v_Description;
            entity.v_Form = dto.v_Form;
            entity.v_Code = dto.v_Code;
            entity.i_ParentId = dto.i_ParentId;
            entity.i_ScopeId = dto.i_ScopeId;
            entity.i_TypeFormId = dto.i_TypeFormId;
            entity.i_ExternalUserFunctionalityTypeId = dto.i_ExternalUserFunctionalityTypeId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="applicationhierarchy"/> to an instance of <see cref="applicationhierarchyDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="applicationhierarchy"/> to convert.</param>
        public static applicationhierarchyDto ToDTO(this applicationhierarchy entity)
        {
            if (entity == null) return null;

            var dto = new applicationhierarchyDto();

            dto.i_ApplicationHierarchyId = entity.i_ApplicationHierarchyId;
            dto.i_ApplicationHierarchyTypeId = entity.i_ApplicationHierarchyTypeId;
            dto.i_Level = entity.i_Level;
            dto.v_Description = entity.v_Description;
            dto.v_Form = entity.v_Form;
            dto.v_Code = entity.v_Code;
            dto.i_ParentId = entity.i_ParentId;
            dto.i_ScopeId = entity.i_ScopeId;
            dto.i_TypeFormId = entity.i_TypeFormId;
            dto.i_ExternalUserFunctionalityTypeId = entity.i_ExternalUserFunctionalityTypeId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="applicationhierarchyDto"/> to an instance of <see cref="applicationhierarchy"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<applicationhierarchy> ToEntities(this IEnumerable<applicationhierarchyDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="applicationhierarchy"/> to an instance of <see cref="applicationhierarchyDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<applicationhierarchyDto> ToDTOs(this IEnumerable<applicationhierarchy> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
