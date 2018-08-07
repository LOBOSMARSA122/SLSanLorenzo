//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/07 - 10:47:46
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
    /// Assembler for <see cref="receta"/> and <see cref="recetaDto"/>.
    /// </summary>
    public static partial class recetaAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="recetaDto"/> converted from <see cref="receta"/>.</param>
        static partial void OnDTO(this receta entity, recetaDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="receta"/> converted from <see cref="recetaDto"/>.</param>
        static partial void OnEntity(this recetaDto dto, receta entity);

        /// <summary>
        /// Converts this instance of <see cref="recetaDto"/> to an instance of <see cref="receta"/>.
        /// </summary>
        /// <param name="dto"><see cref="recetaDto"/> to convert.</param>
        public static receta ToEntity(this recetaDto dto)
        {
            if (dto == null) return null;

            var entity = new receta();

            entity.i_IdReceta = dto.i_IdReceta;
            entity.v_DiagnosticRepositoryId = dto.v_DiagnosticRepositoryId;
            entity.d_Cantidad = dto.d_Cantidad;
            entity.v_Posologia = dto.v_Posologia;
            entity.v_Duracion = dto.v_Duracion;
            entity.t_FechaFin = dto.t_FechaFin;
            entity.v_IdProductoDetalle = dto.v_IdProductoDetalle;
            entity.v_Lote = dto.v_Lote;
            entity.i_IdAlmacen = dto.i_IdAlmacen;
            entity.i_Lleva = dto.i_Lleva;
            entity.i_NoLleva = dto.i_NoLleva;
            entity.v_IdVentaPaciente = dto.v_IdVentaPaciente;
            entity.v_IdVentaAseguradora = dto.v_IdVentaAseguradora;
            entity.v_IdUnidadProductiva = dto.v_IdUnidadProductiva;
            entity.d_SaldoPaciente = dto.d_SaldoPaciente;
            entity.d_SaldoAseguradora = dto.d_SaldoAseguradora;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="receta"/> to an instance of <see cref="recetaDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="receta"/> to convert.</param>
        public static recetaDto ToDTO(this receta entity)
        {
            if (entity == null) return null;

            var dto = new recetaDto();

            dto.i_IdReceta = entity.i_IdReceta;
            dto.v_DiagnosticRepositoryId = entity.v_DiagnosticRepositoryId;
            dto.d_Cantidad = entity.d_Cantidad;
            dto.v_Posologia = entity.v_Posologia;
            dto.v_Duracion = entity.v_Duracion;
            dto.t_FechaFin = entity.t_FechaFin;
            dto.v_IdProductoDetalle = entity.v_IdProductoDetalle;
            dto.v_Lote = entity.v_Lote;
            dto.i_IdAlmacen = entity.i_IdAlmacen;
            dto.i_Lleva = entity.i_Lleva;
            dto.i_NoLleva = entity.i_NoLleva;
            dto.v_IdVentaPaciente = entity.v_IdVentaPaciente;
            dto.v_IdVentaAseguradora = entity.v_IdVentaAseguradora;
            dto.v_IdUnidadProductiva = entity.v_IdUnidadProductiva;
            dto.d_SaldoPaciente = entity.d_SaldoPaciente;
            dto.d_SaldoAseguradora = entity.d_SaldoAseguradora;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="recetaDto"/> to an instance of <see cref="receta"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<receta> ToEntities(this IEnumerable<recetaDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="receta"/> to an instance of <see cref="recetaDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<recetaDto> ToDTOs(this IEnumerable<receta> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
