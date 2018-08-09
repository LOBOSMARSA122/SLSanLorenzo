//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 11:57:56
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
    /// Assembler for <see cref="cuidadopreventivocomentario"/> and <see cref="cuidadopreventivocomentarioDto"/>.
    /// </summary>
    public static partial class cuidadopreventivocomentarioAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="cuidadopreventivocomentarioDto"/> converted from <see cref="cuidadopreventivocomentario"/>.</param>
        static partial void OnDTO(this cuidadopreventivocomentario entity, cuidadopreventivocomentarioDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="cuidadopreventivocomentario"/> converted from <see cref="cuidadopreventivocomentarioDto"/>.</param>
        static partial void OnEntity(this cuidadopreventivocomentarioDto dto, cuidadopreventivocomentario entity);

        /// <summary>
        /// Converts this instance of <see cref="cuidadopreventivocomentarioDto"/> to an instance of <see cref="cuidadopreventivocomentario"/>.
        /// </summary>
        /// <param name="dto"><see cref="cuidadopreventivocomentarioDto"/> to convert.</param>
        public static cuidadopreventivocomentario ToEntity(this cuidadopreventivocomentarioDto dto)
        {
            if (dto == null) return null;

            var entity = new cuidadopreventivocomentario();

            entity.v_CuidadoPreventivoComentarioId = dto.v_CuidadoPreventivoComentarioId;
            entity.v_PersonId = dto.v_PersonId;
            entity.i_GrupoId = dto.i_GrupoId;
            entity.i_ParametroId = dto.i_ParametroId;
            entity.v_Comentario = dto.v_Comentario;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="cuidadopreventivocomentario"/> to an instance of <see cref="cuidadopreventivocomentarioDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="cuidadopreventivocomentario"/> to convert.</param>
        public static cuidadopreventivocomentarioDto ToDTO(this cuidadopreventivocomentario entity)
        {
            if (entity == null) return null;

            var dto = new cuidadopreventivocomentarioDto();

            dto.v_CuidadoPreventivoComentarioId = entity.v_CuidadoPreventivoComentarioId;
            dto.v_PersonId = entity.v_PersonId;
            dto.i_GrupoId = entity.i_GrupoId;
            dto.i_ParametroId = entity.i_ParametroId;
            dto.v_Comentario = entity.v_Comentario;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="cuidadopreventivocomentarioDto"/> to an instance of <see cref="cuidadopreventivocomentario"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<cuidadopreventivocomentario> ToEntities(this IEnumerable<cuidadopreventivocomentarioDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="cuidadopreventivocomentario"/> to an instance of <see cref="cuidadopreventivocomentarioDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<cuidadopreventivocomentarioDto> ToDTOs(this IEnumerable<cuidadopreventivocomentario> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
