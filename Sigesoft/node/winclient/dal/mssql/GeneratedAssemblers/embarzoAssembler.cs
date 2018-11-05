//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/03 - 14:44:54
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
    /// Assembler for <see cref="embarzo"/> and <see cref="embarzoDto"/>.
    /// </summary>
    public static partial class embarzoAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="embarzoDto"/> converted from <see cref="embarzo"/>.</param>
        static partial void OnDTO(this embarzo entity, embarzoDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="embarzo"/> converted from <see cref="embarzoDto"/>.</param>
        static partial void OnEntity(this embarzoDto dto, embarzo entity);

        /// <summary>
        /// Converts this instance of <see cref="embarzoDto"/> to an instance of <see cref="embarzo"/>.
        /// </summary>
        /// <param name="dto"><see cref="embarzoDto"/> to convert.</param>
        public static embarzo ToEntity(this embarzoDto dto)
        {
            if (dto == null) return null;

            var entity = new embarzo();

            entity.v_EmbarazoId = dto.v_EmbarazoId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_Anio = dto.v_Anio;
            entity.v_Cpn = dto.v_Cpn;
            entity.v_Complicacion = dto.v_Complicacion;
            entity.v_Parto = dto.v_Parto;
            entity.v_PesoRn = dto.v_PesoRn;
            entity.v_Puerpio = dto.v_Puerpio;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_ObservacionesGestacion = dto.v_ObservacionesGestacion;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="embarzo"/> to an instance of <see cref="embarzoDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="embarzo"/> to convert.</param>
        public static embarzoDto ToDTO(this embarzo entity)
        {
            if (entity == null) return null;

            var dto = new embarzoDto();

            dto.v_EmbarazoId = entity.v_EmbarazoId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_Anio = entity.v_Anio;
            dto.v_Cpn = entity.v_Cpn;
            dto.v_Complicacion = entity.v_Complicacion;
            dto.v_Parto = entity.v_Parto;
            dto.v_PesoRn = entity.v_PesoRn;
            dto.v_Puerpio = entity.v_Puerpio;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_ObservacionesGestacion = entity.v_ObservacionesGestacion;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="embarzoDto"/> to an instance of <see cref="embarzo"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<embarzo> ToEntities(this IEnumerable<embarzoDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="embarzo"/> to an instance of <see cref="embarzoDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<embarzoDto> ToDTOs(this IEnumerable<embarzo> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
