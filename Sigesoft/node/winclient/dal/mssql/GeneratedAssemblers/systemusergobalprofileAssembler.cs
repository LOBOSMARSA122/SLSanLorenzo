//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/10 - 12:33:09
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
    /// Assembler for <see cref="systemusergobalprofile"/> and <see cref="systemusergobalprofileDto"/>.
    /// </summary>
    public static partial class systemusergobalprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="systemusergobalprofileDto"/> converted from <see cref="systemusergobalprofile"/>.</param>
        static partial void OnDTO(this systemusergobalprofile entity, systemusergobalprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="systemusergobalprofile"/> converted from <see cref="systemusergobalprofileDto"/>.</param>
        static partial void OnEntity(this systemusergobalprofileDto dto, systemusergobalprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="systemusergobalprofileDto"/> to an instance of <see cref="systemusergobalprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="systemusergobalprofileDto"/> to convert.</param>
        public static systemusergobalprofile ToEntity(this systemusergobalprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new systemusergobalprofile();

            entity.i_SystemUserId = dto.i_SystemUserId;
            entity.i_ApplicationHierarchyId = dto.i_ApplicationHierarchyId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="systemusergobalprofile"/> to an instance of <see cref="systemusergobalprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="systemusergobalprofile"/> to convert.</param>
        public static systemusergobalprofileDto ToDTO(this systemusergobalprofile entity)
        {
            if (entity == null) return null;

            var dto = new systemusergobalprofileDto();

            dto.i_SystemUserId = entity.i_SystemUserId;
            dto.i_ApplicationHierarchyId = entity.i_ApplicationHierarchyId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="systemusergobalprofileDto"/> to an instance of <see cref="systemusergobalprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<systemusergobalprofile> ToEntities(this IEnumerable<systemusergobalprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="systemusergobalprofile"/> to an instance of <see cref="systemusergobalprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<systemusergobalprofileDto> ToDTOs(this IEnumerable<systemusergobalprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
