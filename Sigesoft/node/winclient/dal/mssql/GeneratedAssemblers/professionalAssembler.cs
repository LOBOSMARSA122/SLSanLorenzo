//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/09 - 17:46:17
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
    /// Assembler for <see cref="professional"/> and <see cref="professionalDto"/>.
    /// </summary>
    public static partial class professionalAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="professionalDto"/> converted from <see cref="professional"/>.</param>
        static partial void OnDTO(this professional entity, professionalDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="professional"/> converted from <see cref="professionalDto"/>.</param>
        static partial void OnEntity(this professionalDto dto, professional entity);

        /// <summary>
        /// Converts this instance of <see cref="professionalDto"/> to an instance of <see cref="professional"/>.
        /// </summary>
        /// <param name="dto"><see cref="professionalDto"/> to convert.</param>
        public static professional ToEntity(this professionalDto dto)
        {
            if (dto == null) return null;

            var entity = new professional();

            entity.v_PersonId = dto.v_PersonId;
            entity.i_ProfessionId = dto.i_ProfessionId;
            entity.v_ProfessionalCode = dto.v_ProfessionalCode;
            entity.v_ProfessionalInformation = dto.v_ProfessionalInformation;
            entity.b_SignatureImage = dto.b_SignatureImage;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_UpdateNodeId = dto.i_UpdateNodeId;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="professional"/> to an instance of <see cref="professionalDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="professional"/> to convert.</param>
        public static professionalDto ToDTO(this professional entity)
        {
            if (entity == null) return null;

            var dto = new professionalDto();

            dto.v_PersonId = entity.v_PersonId;
            dto.i_ProfessionId = entity.i_ProfessionId;
            dto.v_ProfessionalCode = entity.v_ProfessionalCode;
            dto.v_ProfessionalInformation = entity.v_ProfessionalInformation;
            dto.b_SignatureImage = entity.b_SignatureImage;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_UpdateNodeId = entity.i_UpdateNodeId;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="professionalDto"/> to an instance of <see cref="professional"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<professional> ToEntities(this IEnumerable<professionalDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="professional"/> to an instance of <see cref="professionalDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<professionalDto> ToDTOs(this IEnumerable<professional> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
