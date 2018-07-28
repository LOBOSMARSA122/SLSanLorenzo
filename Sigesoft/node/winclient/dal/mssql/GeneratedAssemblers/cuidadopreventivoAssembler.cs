//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/27 - 18:18:28
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
    /// Assembler for <see cref="cuidadopreventivo"/> and <see cref="cuidadopreventivoDto"/>.
    /// </summary>
    public static partial class cuidadopreventivoAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="cuidadopreventivoDto"/> converted from <see cref="cuidadopreventivo"/>.</param>
        static partial void OnDTO(this cuidadopreventivo entity, cuidadopreventivoDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="cuidadopreventivo"/> converted from <see cref="cuidadopreventivoDto"/>.</param>
        static partial void OnEntity(this cuidadopreventivoDto dto, cuidadopreventivo entity);

        /// <summary>
        /// Converts this instance of <see cref="cuidadopreventivoDto"/> to an instance of <see cref="cuidadopreventivo"/>.
        /// </summary>
        /// <param name="dto"><see cref="cuidadopreventivoDto"/> to convert.</param>
        public static cuidadopreventivo ToEntity(this cuidadopreventivoDto dto)
        {
            if (dto == null) return null;

            var entity = new cuidadopreventivo();

            entity.v_CuidadoPreventivoId = dto.v_CuidadoPreventivoId;
            entity.v_PersonId = dto.v_PersonId;
            entity.d_ServiceDate = dto.d_ServiceDate;
            entity.i_GrupoId = dto.i_GrupoId;
            entity.i_ParametroId = dto.i_ParametroId;
            entity.i_Valor = dto.i_Valor;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="cuidadopreventivo"/> to an instance of <see cref="cuidadopreventivoDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="cuidadopreventivo"/> to convert.</param>
        public static cuidadopreventivoDto ToDTO(this cuidadopreventivo entity)
        {
            if (entity == null) return null;

            var dto = new cuidadopreventivoDto();

            dto.v_CuidadoPreventivoId = entity.v_CuidadoPreventivoId;
            dto.v_PersonId = entity.v_PersonId;
            dto.d_ServiceDate = entity.d_ServiceDate;
            dto.i_GrupoId = entity.i_GrupoId;
            dto.i_ParametroId = entity.i_ParametroId;
            dto.i_Valor = entity.i_Valor;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="cuidadopreventivoDto"/> to an instance of <see cref="cuidadopreventivo"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<cuidadopreventivo> ToEntities(this IEnumerable<cuidadopreventivoDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="cuidadopreventivo"/> to an instance of <see cref="cuidadopreventivoDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<cuidadopreventivoDto> ToDTOs(this IEnumerable<cuidadopreventivo> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
