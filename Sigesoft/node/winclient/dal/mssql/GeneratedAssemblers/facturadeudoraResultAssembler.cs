//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/26 - 16:29:12
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
    /// Assembler for <see cref="facturadeudoraResult"/> and <see cref="facturadeudoraResultDto"/>.
    /// </summary>
    public static partial class facturadeudoraResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="facturadeudoraResultDto"/> converted from <see cref="facturadeudoraResult"/>.</param>
        static partial void OnDTO(this facturadeudoraResult entity, facturadeudoraResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="facturadeudoraResult"/> converted from <see cref="facturadeudoraResultDto"/>.</param>
        static partial void OnEntity(this facturadeudoraResultDto dto, facturadeudoraResult entity);

        /// <summary>
        /// Converts this instance of <see cref="facturadeudoraResultDto"/> to an instance of <see cref="facturadeudoraResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="facturadeudoraResultDto"/> to convert.</param>
        public static facturadeudoraResult ToEntity(this facturadeudoraResultDto dto)
        {
            if (dto == null) return null;

            var entity = new facturadeudoraResult();

            entity.FechaCreacion = dto.FechaCreacion;
            entity.FechaVencimiento = dto.FechaVencimiento;
            entity.v_IdVenta = dto.v_IdVenta;
            entity.NetoXCobrar = dto.NetoXCobrar;
            entity.NroComprobante = dto.NroComprobante;
            entity.TotalPagado = dto.TotalPagado;
            entity.Condicion = dto.Condicion;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="facturadeudoraResult"/> to an instance of <see cref="facturadeudoraResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="facturadeudoraResult"/> to convert.</param>
        public static facturadeudoraResultDto ToDTO(this facturadeudoraResult entity)
        {
            if (entity == null) return null;

            var dto = new facturadeudoraResultDto();

            dto.FechaCreacion = entity.FechaCreacion;
            dto.FechaVencimiento = entity.FechaVencimiento;
            dto.v_IdVenta = entity.v_IdVenta;
            dto.NetoXCobrar = entity.NetoXCobrar;
            dto.NroComprobante = entity.NroComprobante;
            dto.TotalPagado = entity.TotalPagado;
            dto.Condicion = entity.Condicion;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="facturadeudoraResultDto"/> to an instance of <see cref="facturadeudoraResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<facturadeudoraResult> ToEntities(this IEnumerable<facturadeudoraResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="facturadeudoraResult"/> to an instance of <see cref="facturadeudoraResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<facturadeudoraResultDto> ToDTOs(this IEnumerable<facturadeudoraResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
