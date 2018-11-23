//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/23 - 18:06:18
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
    /// Assembler for <see cref="recetadespacho"/> and <see cref="recetadespachoDto"/>.
    /// </summary>
    public static partial class recetadespachoAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="recetadespachoDto"/> converted from <see cref="recetadespacho"/>.</param>
        static partial void OnDTO(this recetadespacho entity, recetadespachoDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="recetadespacho"/> converted from <see cref="recetadespachoDto"/>.</param>
        static partial void OnEntity(this recetadespachoDto dto, recetadespacho entity);

        /// <summary>
        /// Converts this instance of <see cref="recetadespachoDto"/> to an instance of <see cref="recetadespacho"/>.
        /// </summary>
        /// <param name="dto"><see cref="recetadespachoDto"/> to convert.</param>
        public static recetadespacho ToEntity(this recetadespachoDto dto)
        {
            if (dto == null) return null;

            var entity = new recetadespacho();

            entity.i_IdDespacho = dto.i_IdDespacho;
            entity.i_IdReceta = dto.i_IdReceta;
            entity.d_MontoDespachado = dto.d_MontoDespachado;
            entity.t_FechaDespacho = dto.t_FechaDespacho;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="recetadespacho"/> to an instance of <see cref="recetadespachoDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="recetadespacho"/> to convert.</param>
        public static recetadespachoDto ToDTO(this recetadespacho entity)
        {
            if (entity == null) return null;

            var dto = new recetadespachoDto();

            dto.i_IdDespacho = entity.i_IdDespacho;
            dto.i_IdReceta = entity.i_IdReceta;
            dto.d_MontoDespachado = entity.d_MontoDespachado;
            dto.t_FechaDespacho = entity.t_FechaDespacho;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="recetadespachoDto"/> to an instance of <see cref="recetadespacho"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<recetadespacho> ToEntities(this IEnumerable<recetadespachoDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="recetadespacho"/> to an instance of <see cref="recetadespachoDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<recetadespachoDto> ToDTOs(this IEnumerable<recetadespacho> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
