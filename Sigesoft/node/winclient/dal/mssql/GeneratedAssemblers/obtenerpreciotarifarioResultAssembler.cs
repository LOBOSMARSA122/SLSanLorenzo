//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/18 - 11:44:58
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
    /// Assembler for <see cref="obtenerpreciotarifarioResult"/> and <see cref="obtenerpreciotarifarioResultDto"/>.
    /// </summary>
    public static partial class obtenerpreciotarifarioResultAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="obtenerpreciotarifarioResultDto"/> converted from <see cref="obtenerpreciotarifarioResult"/>.</param>
        static partial void OnDTO(this obtenerpreciotarifarioResult entity, obtenerpreciotarifarioResultDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="obtenerpreciotarifarioResult"/> converted from <see cref="obtenerpreciotarifarioResultDto"/>.</param>
        static partial void OnEntity(this obtenerpreciotarifarioResultDto dto, obtenerpreciotarifarioResult entity);

        /// <summary>
        /// Converts this instance of <see cref="obtenerpreciotarifarioResultDto"/> to an instance of <see cref="obtenerpreciotarifarioResult"/>.
        /// </summary>
        /// <param name="dto"><see cref="obtenerpreciotarifarioResultDto"/> to convert.</param>
        public static obtenerpreciotarifarioResult ToEntity(this obtenerpreciotarifarioResultDto dto)
        {
            if (dto == null) return null;

            var entity = new obtenerpreciotarifarioResult();

            entity.v_ServiceId = dto.v_ServiceId;
            entity.v_ProtocolId = dto.v_ProtocolId;
            entity.v_EmployerOrganizationId = dto.v_EmployerOrganizationId;
            entity.v_Name = dto.v_Name;
            entity.v_IdentificationNumber = dto.v_IdentificationNumber;
            entity.i_IdListaPrecios = dto.i_IdListaPrecios;
            entity.d_Precio = dto.d_Precio;
            entity.i_IdLista = dto.i_IdLista;
            entity.v_IdListaPrecios = dto.v_IdListaPrecios;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="obtenerpreciotarifarioResult"/> to an instance of <see cref="obtenerpreciotarifarioResultDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="obtenerpreciotarifarioResult"/> to convert.</param>
        public static obtenerpreciotarifarioResultDto ToDTO(this obtenerpreciotarifarioResult entity)
        {
            if (entity == null) return null;

            var dto = new obtenerpreciotarifarioResultDto();

            dto.v_ServiceId = entity.v_ServiceId;
            dto.v_ProtocolId = entity.v_ProtocolId;
            dto.v_EmployerOrganizationId = entity.v_EmployerOrganizationId;
            dto.v_Name = entity.v_Name;
            dto.v_IdentificationNumber = entity.v_IdentificationNumber;
            dto.i_IdListaPrecios = entity.i_IdListaPrecios;
            dto.d_Precio = entity.d_Precio;
            dto.i_IdLista = entity.i_IdLista;
            dto.v_IdListaPrecios = entity.v_IdListaPrecios;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="obtenerpreciotarifarioResultDto"/> to an instance of <see cref="obtenerpreciotarifarioResult"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<obtenerpreciotarifarioResult> ToEntities(this IEnumerable<obtenerpreciotarifarioResultDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="obtenerpreciotarifarioResult"/> to an instance of <see cref="obtenerpreciotarifarioResultDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<obtenerpreciotarifarioResultDto> ToDTOs(this IEnumerable<obtenerpreciotarifarioResult> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
