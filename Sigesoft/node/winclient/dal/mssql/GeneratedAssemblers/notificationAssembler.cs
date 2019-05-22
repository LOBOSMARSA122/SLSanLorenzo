//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/18 - 11:45:27
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
    /// Assembler for <see cref="notification"/> and <see cref="notificationDto"/>.
    /// </summary>
    public static partial class notificationAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="notificationDto"/> converted from <see cref="notification"/>.</param>
        static partial void OnDTO(this notification entity, notificationDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="notification"/> converted from <see cref="notificationDto"/>.</param>
        static partial void OnEntity(this notificationDto dto, notification entity);

        /// <summary>
        /// Converts this instance of <see cref="notificationDto"/> to an instance of <see cref="notification"/>.
        /// </summary>
        /// <param name="dto"><see cref="notificationDto"/> to convert.</param>
        public static notification ToEntity(this notificationDto dto)
        {
            if (dto == null) return null;

            var entity = new notification();

            entity.v_NotificationId = dto.v_NotificationId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.d_NotificationDate = dto.d_NotificationDate;
            entity.v_PersonId = dto.v_PersonId;
            entity.v_Title = dto.v_Title;
            entity.v_Body = dto.v_Body;
            entity.i_TypeNotificationId = dto.i_TypeNotificationId;
            entity.d_ScheduleDate = dto.d_ScheduleDate;
            entity.i_IsRead = dto.i_IsRead;
            entity.i_StateNotificationId = dto.i_StateNotificationId;
            entity.v_Path = dto.v_Path;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.v_ComentaryUpdate = dto.v_ComentaryUpdate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="notification"/> to an instance of <see cref="notificationDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="notification"/> to convert.</param>
        public static notificationDto ToDTO(this notification entity)
        {
            if (entity == null) return null;

            var dto = new notificationDto();

            dto.v_NotificationId = entity.v_NotificationId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.d_NotificationDate = entity.d_NotificationDate;
            dto.v_PersonId = entity.v_PersonId;
            dto.v_Title = entity.v_Title;
            dto.v_Body = entity.v_Body;
            dto.i_TypeNotificationId = entity.i_TypeNotificationId;
            dto.d_ScheduleDate = entity.d_ScheduleDate;
            dto.i_IsRead = entity.i_IsRead;
            dto.i_StateNotificationId = entity.i_StateNotificationId;
            dto.v_Path = entity.v_Path;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.v_ComentaryUpdate = entity.v_ComentaryUpdate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="notificationDto"/> to an instance of <see cref="notification"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<notification> ToEntities(this IEnumerable<notificationDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="notification"/> to an instance of <see cref="notificationDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<notificationDto> ToDTOs(this IEnumerable<notification> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
