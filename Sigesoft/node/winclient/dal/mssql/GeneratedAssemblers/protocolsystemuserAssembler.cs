//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/26 - 17:37:18
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
    /// Assembler for <see cref="protocolsystemuser"/> and <see cref="protocolsystemuserDto"/>.
    /// </summary>
    public static partial class protocolsystemuserAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="protocolsystemuserDto"/> converted from <see cref="protocolsystemuser"/>.</param>
        static partial void OnDTO(this protocolsystemuser entity, protocolsystemuserDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="protocolsystemuser"/> converted from <see cref="protocolsystemuserDto"/>.</param>
        static partial void OnEntity(this protocolsystemuserDto dto, protocolsystemuser entity);

        /// <summary>
        /// Converts this instance of <see cref="protocolsystemuserDto"/> to an instance of <see cref="protocolsystemuser"/>.
        /// </summary>
        /// <param name="dto"><see cref="protocolsystemuserDto"/> to convert.</param>
        public static protocolsystemuser ToEntity(this protocolsystemuserDto dto)
        {
            if (dto == null) return null;

            var entity = new protocolsystemuser();

            entity.v_ProtocolSystemUserId = dto.v_ProtocolSystemUserId;
            entity.i_SystemUserId = dto.i_SystemUserId;
            entity.v_ProtocolId = dto.v_ProtocolId;
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
        /// Converts this instance of <see cref="protocolsystemuser"/> to an instance of <see cref="protocolsystemuserDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="protocolsystemuser"/> to convert.</param>
        public static protocolsystemuserDto ToDTO(this protocolsystemuser entity)
        {
            if (entity == null) return null;

            var dto = new protocolsystemuserDto();

            dto.v_ProtocolSystemUserId = entity.v_ProtocolSystemUserId;
            dto.i_SystemUserId = entity.i_SystemUserId;
            dto.v_ProtocolId = entity.v_ProtocolId;
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
        /// Converts each instance of <see cref="protocolsystemuserDto"/> to an instance of <see cref="protocolsystemuser"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<protocolsystemuser> ToEntities(this IEnumerable<protocolsystemuserDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="protocolsystemuser"/> to an instance of <see cref="protocolsystemuserDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<protocolsystemuserDto> ToDTOs(this IEnumerable<protocolsystemuser> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
