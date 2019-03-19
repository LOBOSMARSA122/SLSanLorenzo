//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/14 - 10:39:43
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Sigesoft.Server.WebClientAdmin.DAL;

namespace Sigesoft.Server.WebClientAdmin.BE
{

    /// <summary>
    /// Assembler for <see cref="vigilanciaservice"/> and <see cref="vigilanciaserviceDto"/>.
    /// </summary>
    public static partial class vigilanciaserviceAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="vigilanciaserviceDto"/> converted from <see cref="vigilanciaservice"/>.</param>
        static partial void OnDTO(this vigilanciaservice entity, vigilanciaserviceDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="vigilanciaservice"/> converted from <see cref="vigilanciaserviceDto"/>.</param>
        static partial void OnEntity(this vigilanciaserviceDto dto, vigilanciaservice entity);

        /// <summary>
        /// Converts this instance of <see cref="vigilanciaserviceDto"/> to an instance of <see cref="vigilanciaservice"/>.
        /// </summary>
        /// <param name="dto"><see cref="vigilanciaserviceDto"/> to convert.</param>
        public static vigilanciaservice ToEntity(this vigilanciaserviceDto dto)
        {
            if (dto == null) return null;

            var entity = new vigilanciaservice();

            entity.v_VigilanciaServiceId = dto.v_VigilanciaServiceId;
            entity.v_VigilanciaId = dto.v_VigilanciaId;
            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_Commentary = dto.v_Commentary;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="vigilanciaservice"/> to an instance of <see cref="vigilanciaserviceDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="vigilanciaservice"/> to convert.</param>
        public static vigilanciaserviceDto ToDTO(this vigilanciaservice entity)
        {
            if (entity == null) return null;

            var dto = new vigilanciaserviceDto();

            dto.v_VigilanciaServiceId = entity.v_VigilanciaServiceId;
            dto.v_VigilanciaId = entity.v_VigilanciaId;
            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_Commentary = entity.v_Commentary;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="vigilanciaserviceDto"/> to an instance of <see cref="vigilanciaservice"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<vigilanciaservice> ToEntities(this IEnumerable<vigilanciaserviceDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="vigilanciaservice"/> to an instance of <see cref="vigilanciaserviceDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<vigilanciaserviceDto> ToDTOs(this IEnumerable<vigilanciaservice> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
