//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 09:27:59
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
    /// Assembler for <see cref="nodeserviceprofile"/> and <see cref="nodeserviceprofileDto"/>.
    /// </summary>
    public static partial class nodeserviceprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="nodeserviceprofileDto"/> converted from <see cref="nodeserviceprofile"/>.</param>
        static partial void OnDTO(this nodeserviceprofile entity, nodeserviceprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="nodeserviceprofile"/> converted from <see cref="nodeserviceprofileDto"/>.</param>
        static partial void OnEntity(this nodeserviceprofileDto dto, nodeserviceprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="nodeserviceprofileDto"/> to an instance of <see cref="nodeserviceprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="nodeserviceprofileDto"/> to convert.</param>
        public static nodeserviceprofile ToEntity(this nodeserviceprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new nodeserviceprofile();

            entity.v_NodeServiceProfileId = dto.v_NodeServiceProfileId;
            entity.i_NodeId = dto.i_NodeId;
            entity.i_ServiceTypeId = dto.i_ServiceTypeId;
            entity.i_MasterServiceId = dto.i_MasterServiceId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="nodeserviceprofile"/> to an instance of <see cref="nodeserviceprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="nodeserviceprofile"/> to convert.</param>
        public static nodeserviceprofileDto ToDTO(this nodeserviceprofile entity)
        {
            if (entity == null) return null;

            var dto = new nodeserviceprofileDto();

            dto.v_NodeServiceProfileId = entity.v_NodeServiceProfileId;
            dto.i_NodeId = entity.i_NodeId;
            dto.i_ServiceTypeId = entity.i_ServiceTypeId;
            dto.i_MasterServiceId = entity.i_MasterServiceId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeserviceprofileDto"/> to an instance of <see cref="nodeserviceprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<nodeserviceprofile> ToEntities(this IEnumerable<nodeserviceprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeserviceprofile"/> to an instance of <see cref="nodeserviceprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<nodeserviceprofileDto> ToDTOs(this IEnumerable<nodeserviceprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
