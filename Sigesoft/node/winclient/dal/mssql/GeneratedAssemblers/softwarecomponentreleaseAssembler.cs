//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/23 - 10:31:29
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
    /// Assembler for <see cref="softwarecomponentrelease"/> and <see cref="softwarecomponentreleaseDto"/>.
    /// </summary>
    public static partial class softwarecomponentreleaseAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="softwarecomponentreleaseDto"/> converted from <see cref="softwarecomponentrelease"/>.</param>
        static partial void OnDTO(this softwarecomponentrelease entity, softwarecomponentreleaseDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="softwarecomponentrelease"/> converted from <see cref="softwarecomponentreleaseDto"/>.</param>
        static partial void OnEntity(this softwarecomponentreleaseDto dto, softwarecomponentrelease entity);

        /// <summary>
        /// Converts this instance of <see cref="softwarecomponentreleaseDto"/> to an instance of <see cref="softwarecomponentrelease"/>.
        /// </summary>
        /// <param name="dto"><see cref="softwarecomponentreleaseDto"/> to convert.</param>
        public static softwarecomponentrelease ToEntity(this softwarecomponentreleaseDto dto)
        {
            if (dto == null) return null;

            var entity = new softwarecomponentrelease();

            entity.i_SoftwareComponentId = dto.i_SoftwareComponentId;
            entity.v_SoftwareComponentVersion = dto.v_SoftwareComponentVersion;
            entity.i_DeploymentFileId = dto.i_DeploymentFileId;
            entity.d_ReleaseDate = dto.d_ReleaseDate;
            entity.v_DatabaseVersionRequired = dto.v_DatabaseVersionRequired;
            entity.v_ReleaseNotes = dto.v_ReleaseNotes;
            entity.v_AdditionalInformation1 = dto.v_AdditionalInformation1;
            entity.v_AdditionalInformation2 = dto.v_AdditionalInformation2;
            entity.i_IsPublished = dto.i_IsPublished;
            entity.i_IsLastVersion = dto.i_IsLastVersion;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="softwarecomponentrelease"/> to an instance of <see cref="softwarecomponentreleaseDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="softwarecomponentrelease"/> to convert.</param>
        public static softwarecomponentreleaseDto ToDTO(this softwarecomponentrelease entity)
        {
            if (entity == null) return null;

            var dto = new softwarecomponentreleaseDto();

            dto.i_SoftwareComponentId = entity.i_SoftwareComponentId;
            dto.v_SoftwareComponentVersion = entity.v_SoftwareComponentVersion;
            dto.i_DeploymentFileId = entity.i_DeploymentFileId;
            dto.d_ReleaseDate = entity.d_ReleaseDate;
            dto.v_DatabaseVersionRequired = entity.v_DatabaseVersionRequired;
            dto.v_ReleaseNotes = entity.v_ReleaseNotes;
            dto.v_AdditionalInformation1 = entity.v_AdditionalInformation1;
            dto.v_AdditionalInformation2 = entity.v_AdditionalInformation2;
            dto.i_IsPublished = entity.i_IsPublished;
            dto.i_IsLastVersion = entity.i_IsLastVersion;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="softwarecomponentreleaseDto"/> to an instance of <see cref="softwarecomponentrelease"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<softwarecomponentrelease> ToEntities(this IEnumerable<softwarecomponentreleaseDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="softwarecomponentrelease"/> to an instance of <see cref="softwarecomponentreleaseDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<softwarecomponentreleaseDto> ToDTOs(this IEnumerable<softwarecomponentrelease> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
