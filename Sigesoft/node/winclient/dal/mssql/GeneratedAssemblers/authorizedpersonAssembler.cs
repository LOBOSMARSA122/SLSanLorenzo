//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/27 - 18:18:24
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
    /// Assembler for <see cref="authorizedperson"/> and <see cref="authorizedpersonDto"/>.
    /// </summary>
    public static partial class authorizedpersonAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="authorizedpersonDto"/> converted from <see cref="authorizedperson"/>.</param>
        static partial void OnDTO(this authorizedperson entity, authorizedpersonDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="authorizedperson"/> converted from <see cref="authorizedpersonDto"/>.</param>
        static partial void OnEntity(this authorizedpersonDto dto, authorizedperson entity);

        /// <summary>
        /// Converts this instance of <see cref="authorizedpersonDto"/> to an instance of <see cref="authorizedperson"/>.
        /// </summary>
        /// <param name="dto"><see cref="authorizedpersonDto"/> to convert.</param>
        public static authorizedperson ToEntity(this authorizedpersonDto dto)
        {
            if (dto == null) return null;

            var entity = new authorizedperson();

            entity.v_AuthorizedPersonId = dto.v_AuthorizedPersonId;
            entity.v_FirstName = dto.v_FirstName;
            entity.v_FirstLastName = dto.v_FirstLastName;
            entity.v_SecondLastName = dto.v_SecondLastName;
            entity.i_DocTypeId = dto.i_DocTypeId;
            entity.v_DocTypeName = dto.v_DocTypeName;
            entity.v_DocNumber = dto.v_DocNumber;
            entity.i_SexTypeId = dto.i_SexTypeId;
            entity.v_SexTypeName = dto.v_SexTypeName;
            entity.d_BirthDate = dto.d_BirthDate;
            entity.v_OccupationName = dto.v_OccupationName;
            entity.v_OrganitationName = dto.v_OrganitationName;
            entity.d_EntryToMedicalCenter = dto.d_EntryToMedicalCenter;
            entity.v_ProtocolId = dto.v_ProtocolId;
            entity.v_ProtocolName = dto.v_ProtocolName;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="authorizedperson"/> to an instance of <see cref="authorizedpersonDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="authorizedperson"/> to convert.</param>
        public static authorizedpersonDto ToDTO(this authorizedperson entity)
        {
            if (entity == null) return null;

            var dto = new authorizedpersonDto();

            dto.v_AuthorizedPersonId = entity.v_AuthorizedPersonId;
            dto.v_FirstName = entity.v_FirstName;
            dto.v_FirstLastName = entity.v_FirstLastName;
            dto.v_SecondLastName = entity.v_SecondLastName;
            dto.i_DocTypeId = entity.i_DocTypeId;
            dto.v_DocTypeName = entity.v_DocTypeName;
            dto.v_DocNumber = entity.v_DocNumber;
            dto.i_SexTypeId = entity.i_SexTypeId;
            dto.v_SexTypeName = entity.v_SexTypeName;
            dto.d_BirthDate = entity.d_BirthDate;
            dto.v_OccupationName = entity.v_OccupationName;
            dto.v_OrganitationName = entity.v_OrganitationName;
            dto.d_EntryToMedicalCenter = entity.d_EntryToMedicalCenter;
            dto.v_ProtocolId = entity.v_ProtocolId;
            dto.v_ProtocolName = entity.v_ProtocolName;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="authorizedpersonDto"/> to an instance of <see cref="authorizedperson"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<authorizedperson> ToEntities(this IEnumerable<authorizedpersonDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="authorizedperson"/> to an instance of <see cref="authorizedpersonDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<authorizedpersonDto> ToDTOs(this IEnumerable<authorizedperson> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
