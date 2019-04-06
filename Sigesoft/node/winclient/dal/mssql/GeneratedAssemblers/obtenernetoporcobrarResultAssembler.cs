//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/04/05 - 15:03:28
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
    /// Assembler for <see cref="obtenernetoporcobrarResult"/> and <see cref="obtenernetoporcobrarResultDto"/>.
    /// </summary>
    public static partial class obtenernetoporcobrarResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="obtenernetoporcobrarResultDto"/> converted from <see cref="obtenernetoporcobrarResult"/>.</param>
        static partial void OnDTO(this obtenernetoporcobrarResult entity, obtenernetoporcobrarResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="obtenernetoporcobrarResult"/> converted from <see cref="obtenernetoporcobrarResultDto"/>.</param>
        static partial void OnEntity(this obtenernetoporcobrarResultDto dto, obtenernetoporcobrarResult entity);

        /// <summary>
        /// Converts this instance of <see cref="obtenernetoporcobrarResultDto"/> to an instance of <see cref="obtenernetoporcobrarResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="obtenernetoporcobrarResultDto"/> to convert.</param>
        public static obtenernetoporcobrarResult ToEntity(this obtenernetoporcobrarResultDto dto)
        {
            if (dto == null) return null;

            var entity = new obtenernetoporcobrarResult();

            entity.d_NetoXCobrar = dto.d_NetoXCobrar;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="obtenernetoporcobrarResult"/> to an instance of <see cref="obtenernetoporcobrarResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="obtenernetoporcobrarResult"/> to convert.</param>
        public static obtenernetoporcobrarResultDto ToDTO(this obtenernetoporcobrarResult entity)
        {
            if (entity == null) return null;

            var dto = new obtenernetoporcobrarResultDto();

            dto.d_NetoXCobrar = entity.d_NetoXCobrar;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="obtenernetoporcobrarResultDto"/> to an instance of <see cref="obtenernetoporcobrarResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<obtenernetoporcobrarResult> ToEntities(this IEnumerable<obtenernetoporcobrarResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="obtenernetoporcobrarResult"/> to an instance of <see cref="obtenernetoporcobrarResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<obtenernetoporcobrarResultDto> ToDTOs(this IEnumerable<obtenernetoporcobrarResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
