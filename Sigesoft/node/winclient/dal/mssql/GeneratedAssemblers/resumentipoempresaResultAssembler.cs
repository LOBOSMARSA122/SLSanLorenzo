//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/23 - 18:06:07
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
    /// Assembler for <see cref="resumentipoempresaResult"/> and <see cref="resumentipoempresaResultDto"/>.
    /// </summary>
    public static partial class resumentipoempresaResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="resumentipoempresaResultDto"/> converted from <see cref="resumentipoempresaResult"/>.</param>
        static partial void OnDTO(this resumentipoempresaResult entity, resumentipoempresaResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="resumentipoempresaResult"/> converted from <see cref="resumentipoempresaResultDto"/>.</param>
        static partial void OnEntity(this resumentipoempresaResultDto dto, resumentipoempresaResult entity);

        /// <summary>
        /// Converts this instance of <see cref="resumentipoempresaResultDto"/> to an instance of <see cref="resumentipoempresaResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="resumentipoempresaResultDto"/> to convert.</param>
        public static resumentipoempresaResult ToEntity(this resumentipoempresaResultDto dto)
        {
            if (dto == null) return null;

            var entity = new resumentipoempresaResult();

            entity.v_ServiceId = dto.v_ServiceId;
            entity.EmpresaCliente = dto.EmpresaCliente;
            entity.EmpresaEmpleadora = dto.EmpresaEmpleadora;
            entity.EmpresaTrabajo = dto.EmpresaTrabajo;
            entity.Precio = dto.Precio;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="resumentipoempresaResult"/> to an instance of <see cref="resumentipoempresaResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="resumentipoempresaResult"/> to convert.</param>
        public static resumentipoempresaResultDto ToDTO(this resumentipoempresaResult entity)
        {
            if (entity == null) return null;

            var dto = new resumentipoempresaResultDto();

            dto.v_ServiceId = entity.v_ServiceId;
            dto.EmpresaCliente = entity.EmpresaCliente;
            dto.EmpresaEmpleadora = entity.EmpresaEmpleadora;
            dto.EmpresaTrabajo = entity.EmpresaTrabajo;
            dto.Precio = entity.Precio;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="resumentipoempresaResultDto"/> to an instance of <see cref="resumentipoempresaResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<resumentipoempresaResult> ToEntities(this IEnumerable<resumentipoempresaResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="resumentipoempresaResult"/> to an instance of <see cref="resumentipoempresaResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<resumentipoempresaResultDto> ToDTOs(this IEnumerable<resumentipoempresaResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
