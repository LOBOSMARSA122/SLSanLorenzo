//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/01 - 09:36:39
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
    /// Assembler for <see cref="systemuser"/> and <see cref="systemuserDto"/>.
    /// </summary>
    public static partial class systemuserAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="systemuserDto"/> converted from <see cref="systemuser"/>.</param>
        static partial void OnDTO(this systemuser entity, systemuserDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="systemuser"/> converted from <see cref="systemuserDto"/>.</param>
        static partial void OnEntity(this systemuserDto dto, systemuser entity);

        /// <summary>
        /// Converts this instance of <see cref="systemuserDto"/> to an instance of <see cref="systemuser"/>.
        /// </summary>
        /// <param name="dto"><see cref="systemuserDto"/> to convert.</param>
        public static systemuser ToEntity(this systemuserDto dto)
        {
            if (dto == null) return null;

            var entity = new systemuser();

            entity.i_SystemUserId = dto.i_SystemUserId;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_UserName = dto.v_UserName;
            entity.v_Password = dto.v_Password;
            entity.v_SecretQuestion = dto.v_SecretQuestion;
            entity.v_SecretAnswer = dto.v_SecretAnswer;
            entity.d_ExpireDate = dto.d_ExpireDate;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_SystemUserTypeId = dto.i_SystemUserTypeId;
            entity.i_RolVentaId = dto.i_RolVentaId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="systemuser"/> to an instance of <see cref="systemuserDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="systemuser"/> to convert.</param>
        public static systemuserDto ToDTO(this systemuser entity)
        {
            if (entity == null) return null;

            var dto = new systemuserDto();

            dto.i_SystemUserId = entity.i_SystemUserId;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_UserName = entity.v_UserName;
            dto.v_Password = entity.v_Password;
            dto.v_SecretQuestion = entity.v_SecretQuestion;
            dto.v_SecretAnswer = entity.v_SecretAnswer;
            dto.d_ExpireDate = entity.d_ExpireDate;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_SystemUserTypeId = entity.i_SystemUserTypeId;
            dto.i_RolVentaId = entity.i_RolVentaId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="systemuserDto"/> to an instance of <see cref="systemuser"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<systemuser> ToEntities(this IEnumerable<systemuserDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="systemuser"/> to an instance of <see cref="systemuserDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<systemuserDto> ToDTOs(this IEnumerable<systemuser> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
