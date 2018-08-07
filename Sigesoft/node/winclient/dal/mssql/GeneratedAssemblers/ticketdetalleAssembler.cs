//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/07 - 10:47:52
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
    /// Assembler for <see cref="ticketdetalle"/> and <see cref="ticketdetalleDto"/>.
    /// </summary>
    public static partial class ticketdetalleAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="ticketdetalleDto"/> converted from <see cref="ticketdetalle"/>.</param>
        static partial void OnDTO(this ticketdetalle entity, ticketdetalleDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="ticketdetalle"/> converted from <see cref="ticketdetalleDto"/>.</param>
        static partial void OnEntity(this ticketdetalleDto dto, ticketdetalle entity);

        /// <summary>
        /// Converts this instance of <see cref="ticketdetalleDto"/> to an instance of <see cref="ticketdetalle"/>.
        /// </summary>
        /// <param name="dto"><see cref="ticketdetalleDto"/> to convert.</param>
        public static ticketdetalle ToEntity(this ticketdetalleDto dto)
        {
            if (dto == null) return null;

            var entity = new ticketdetalle();

            entity.v_TicketDetalleId = dto.v_TicketDetalleId;
            entity.v_TicketId = dto.v_TicketId;
            entity.v_Descripcion = dto.v_Descripcion;
            entity.v_IdProductoDetalle = dto.v_IdProductoDetalle;
            entity.v_CodInterno = dto.v_CodInterno;
            entity.d_Cantidad = dto.d_Cantidad;
            entity.d_PrecioVenta = dto.d_PrecioVenta;
            entity.d_SaldoPaciente = dto.d_SaldoPaciente;
            entity.d_SaldoAseguradora = dto.d_SaldoAseguradora;
            entity.i_EsDespachado = dto.i_EsDespachado;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="ticketdetalle"/> to an instance of <see cref="ticketdetalleDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="ticketdetalle"/> to convert.</param>
        public static ticketdetalleDto ToDTO(this ticketdetalle entity)
        {
            if (entity == null) return null;

            var dto = new ticketdetalleDto();

            dto.v_TicketDetalleId = entity.v_TicketDetalleId;
            dto.v_TicketId = entity.v_TicketId;
            dto.v_Descripcion = entity.v_Descripcion;
            dto.v_IdProductoDetalle = entity.v_IdProductoDetalle;
            dto.v_CodInterno = entity.v_CodInterno;
            dto.d_Cantidad = entity.d_Cantidad;
            dto.d_PrecioVenta = entity.d_PrecioVenta;
            dto.d_SaldoPaciente = entity.d_SaldoPaciente;
            dto.d_SaldoAseguradora = entity.d_SaldoAseguradora;
            dto.i_EsDespachado = entity.i_EsDespachado;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="ticketdetalleDto"/> to an instance of <see cref="ticketdetalle"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ticketdetalle> ToEntities(this IEnumerable<ticketdetalleDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="ticketdetalle"/> to an instance of <see cref="ticketdetalleDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<ticketdetalleDto> ToDTOs(this IEnumerable<ticketdetalle> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
