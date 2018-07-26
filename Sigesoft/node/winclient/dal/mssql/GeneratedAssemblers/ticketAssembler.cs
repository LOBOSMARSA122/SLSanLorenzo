//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/26 - 11:19:01
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
    /// Assembler for <see cref="ticket"/> and <see cref="ticketDto"/>.
    /// </summary>
    public static partial class ticketAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="ticketDto"/> converted from <see cref="ticket"/>.</param>
        static partial void OnDTO(this ticket entity, ticketDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="ticket"/> converted from <see cref="ticketDto"/>.</param>
        static partial void OnEntity(this ticketDto dto, ticket entity);

        /// <summary>
        /// Converts this instance of <see cref="ticketDto"/> to an instance of <see cref="ticket"/>.
        /// </summary>
        /// <param name="dto"><see cref="ticketDto"/> to convert.</param>
        public static ticket ToEntity(this ticketDto dto)
        {
            if (dto == null) return null;

            var entity = new ticket();

            entity.v_TicketId = dto.v_TicketId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.d_Fecha = dto.d_Fecha;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="ticket"/> to an instance of <see cref="ticketDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="ticket"/> to convert.</param>
        public static ticketDto ToDTO(this ticket entity)
        {
            if (entity == null) return null;

            var dto = new ticketDto();

            dto.v_TicketId = entity.v_TicketId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.d_Fecha = entity.d_Fecha;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="ticketDto"/> to an instance of <see cref="ticket"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ticket> ToEntities(this IEnumerable<ticketDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="ticket"/> to an instance of <see cref="ticketDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ticketDto> ToDTOs(this IEnumerable<ticket> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
