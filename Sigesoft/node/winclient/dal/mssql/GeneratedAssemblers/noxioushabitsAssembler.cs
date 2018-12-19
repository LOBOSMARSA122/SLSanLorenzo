//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/18 - 18:54:30
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
    /// Assembler for <see cref="noxioushabits"/> and <see cref="noxioushabitsDto"/>.
    /// </summary>
    public static partial class noxioushabitsAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="noxioushabitsDto"/> converted from <see cref="noxioushabits"/>.</param>
        static partial void OnDTO(this noxioushabits entity, noxioushabitsDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="noxioushabits"/> converted from <see cref="noxioushabitsDto"/>.</param>
        static partial void OnEntity(this noxioushabitsDto dto, noxioushabits entity);

        /// <summary>
        /// Converts this instance of <see cref="noxioushabitsDto"/> to an instance of <see cref="noxioushabits"/>.
        /// </summary>
        /// <param name="dto"><see cref="noxioushabitsDto"/> to convert.</param>
        public static noxioushabits ToEntity(this noxioushabitsDto dto)
        {
            if (dto == null) return null;

            var entity = new noxioushabits();

            entity.v_NoxiousHabitsId = dto.v_NoxiousHabitsId;
            entity.v_PersonId = dto.v_PersonId;
            entity.i_TypeHabitsId = dto.i_TypeHabitsId;
            entity.v_Frequency = dto.v_Frequency;
            entity.v_Comment = dto.v_Comment;
            entity.v_DescriptionHabit = dto.v_DescriptionHabit;
            entity.v_DescriptionQuantity = dto.v_DescriptionQuantity;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="noxioushabits"/> to an instance of <see cref="noxioushabitsDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="noxioushabits"/> to convert.</param>
        public static noxioushabitsDto ToDTO(this noxioushabits entity)
        {
            if (entity == null) return null;

            var dto = new noxioushabitsDto();

            dto.v_NoxiousHabitsId = entity.v_NoxiousHabitsId;
            dto.v_PersonId = entity.v_PersonId;
            dto.i_TypeHabitsId = entity.i_TypeHabitsId;
            dto.v_Frequency = entity.v_Frequency;
            dto.v_Comment = entity.v_Comment;
            dto.v_DescriptionHabit = entity.v_DescriptionHabit;
            dto.v_DescriptionQuantity = entity.v_DescriptionQuantity;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="noxioushabitsDto"/> to an instance of <see cref="noxioushabits"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<noxioushabits> ToEntities(this IEnumerable<noxioushabitsDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="noxioushabits"/> to an instance of <see cref="noxioushabitsDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<noxioushabitsDto> ToDTOs(this IEnumerable<noxioushabits> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
