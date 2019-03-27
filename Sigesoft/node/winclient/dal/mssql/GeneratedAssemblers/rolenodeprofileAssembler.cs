//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/26 - 16:30:05
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
    /// Assembler for <see cref="rolenodeprofile"/> and <see cref="rolenodeprofileDto"/>.
    /// </summary>
    public static partial class rolenodeprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="rolenodeprofileDto"/> converted from <see cref="rolenodeprofile"/>.</param>
        static partial void OnDTO(this rolenodeprofile entity, rolenodeprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="rolenodeprofile"/> converted from <see cref="rolenodeprofileDto"/>.</param>
        static partial void OnEntity(this rolenodeprofileDto dto, rolenodeprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="rolenodeprofileDto"/> to an instance of <see cref="rolenodeprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="rolenodeprofileDto"/> to convert.</param>
        public static rolenodeprofile ToEntity(this rolenodeprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new rolenodeprofile();

            entity.i_NodeId = dto.i_NodeId;
            entity.i_RoleId = dto.i_RoleId;
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
        /// Converts this instance of <see cref="rolenodeprofile"/> to an instance of <see cref="rolenodeprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="rolenodeprofile"/> to convert.</param>
        public static rolenodeprofileDto ToDTO(this rolenodeprofile entity)
        {
            if (entity == null) return null;

            var dto = new rolenodeprofileDto();

            dto.i_NodeId = entity.i_NodeId;
            dto.i_RoleId = entity.i_RoleId;
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
        /// Converts each instance of <see cref="rolenodeprofileDto"/> to an instance of <see cref="rolenodeprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<rolenodeprofile> ToEntities(this IEnumerable<rolenodeprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="rolenodeprofile"/> to an instance of <see cref="rolenodeprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<rolenodeprofileDto> ToDTOs(this IEnumerable<rolenodeprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
