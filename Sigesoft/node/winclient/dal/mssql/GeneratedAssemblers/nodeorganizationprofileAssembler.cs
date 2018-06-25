//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/25 - 17:52:47
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
    /// Assembler for <see cref="nodeorganizationprofile"/> and <see cref="nodeorganizationprofileDto"/>.
    /// </summary>
    public static partial class nodeorganizationprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="nodeorganizationprofileDto"/> converted from <see cref="nodeorganizationprofile"/>.</param>
        static partial void OnDTO(this nodeorganizationprofile entity, nodeorganizationprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="nodeorganizationprofile"/> converted from <see cref="nodeorganizationprofileDto"/>.</param>
        static partial void OnEntity(this nodeorganizationprofileDto dto, nodeorganizationprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="nodeorganizationprofileDto"/> to an instance of <see cref="nodeorganizationprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="nodeorganizationprofileDto"/> to convert.</param>
        public static nodeorganizationprofile ToEntity(this nodeorganizationprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new nodeorganizationprofile();

            entity.i_NodeId = dto.i_NodeId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="nodeorganizationprofile"/> to an instance of <see cref="nodeorganizationprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="nodeorganizationprofile"/> to convert.</param>
        public static nodeorganizationprofileDto ToDTO(this nodeorganizationprofile entity)
        {
            if (entity == null) return null;

            var dto = new nodeorganizationprofileDto();

            dto.i_NodeId = entity.i_NodeId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeorganizationprofileDto"/> to an instance of <see cref="nodeorganizationprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<nodeorganizationprofile> ToEntities(this IEnumerable<nodeorganizationprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeorganizationprofile"/> to an instance of <see cref="nodeorganizationprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<nodeorganizationprofileDto> ToDTOs(this IEnumerable<nodeorganizationprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
