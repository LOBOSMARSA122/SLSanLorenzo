//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/26 - 11:18:27
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
    /// Assembler for <see cref="log"/> and <see cref="logDto"/>.
    /// </summary>
    public static partial class logAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="logDto"/> converted from <see cref="log"/>.</param>
        static partial void OnDTO(this log entity, logDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="log"/> converted from <see cref="logDto"/>.</param>
        static partial void OnEntity(this logDto dto, log entity);

        /// <summary>
        /// Converts this instance of <see cref="logDto"/> to an instance of <see cref="log"/>.
        /// </summary>
        /// <param name="dto"><see cref="logDto"/> to convert.</param>
        public static log ToEntity(this logDto dto)
        {
            if (dto == null) return null;

            var entity = new log();

            entity.v_LogId = dto.v_LogId;
            entity.i_NodeLogId = dto.i_NodeLogId;
            entity.i_EventTypeId = dto.i_EventTypeId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.d_Date = dto.d_Date;
            entity.i_SystemUserId = dto.i_SystemUserId;
            entity.v_ProcessEntity = dto.v_ProcessEntity;
            entity.v_ElementItem = dto.v_ElementItem;
            entity.i_Success = dto.i_Success;
            entity.v_ErrorException = dto.v_ErrorException;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="log"/> to an instance of <see cref="logDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="log"/> to convert.</param>
        public static logDto ToDTO(this log entity)
        {
            if (entity == null) return null;

            var dto = new logDto();

            dto.v_LogId = entity.v_LogId;
            dto.i_NodeLogId = entity.i_NodeLogId;
            dto.i_EventTypeId = entity.i_EventTypeId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.d_Date = entity.d_Date;
            dto.i_SystemUserId = entity.i_SystemUserId;
            dto.v_ProcessEntity = entity.v_ProcessEntity;
            dto.v_ElementItem = entity.v_ElementItem;
            dto.i_Success = entity.i_Success;
            dto.v_ErrorException = entity.v_ErrorException;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="logDto"/> to an instance of <see cref="log"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<log> ToEntities(this IEnumerable<logDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="log"/> to an instance of <see cref="logDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<logDto> ToDTOs(this IEnumerable<log> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
