//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/04/21 - 18:38:31
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
    /// Assembler for <see cref="restrictedwarehouseprofile"/> and <see cref="restrictedwarehouseprofileDto"/>.
    /// </summary>
    public static partial class restrictedwarehouseprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="restrictedwarehouseprofileDto"/> converted from <see cref="restrictedwarehouseprofile"/>.</param>
        static partial void OnDTO(this restrictedwarehouseprofile entity, restrictedwarehouseprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="restrictedwarehouseprofile"/> converted from <see cref="restrictedwarehouseprofileDto"/>.</param>
        static partial void OnEntity(this restrictedwarehouseprofileDto dto, restrictedwarehouseprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="restrictedwarehouseprofileDto"/> to an instance of <see cref="restrictedwarehouseprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="restrictedwarehouseprofileDto"/> to convert.</param>
        public static restrictedwarehouseprofile ToEntity(this restrictedwarehouseprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new restrictedwarehouseprofile();

            entity.i_SystemUserId = dto.i_SystemUserId;
            entity.v_WarehouseId = dto.v_WarehouseId;
            entity.i_NodeId = dto.i_NodeId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="restrictedwarehouseprofile"/> to an instance of <see cref="restrictedwarehouseprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="restrictedwarehouseprofile"/> to convert.</param>
        public static restrictedwarehouseprofileDto ToDTO(this restrictedwarehouseprofile entity)
        {
            if (entity == null) return null;

            var dto = new restrictedwarehouseprofileDto();

            dto.i_SystemUserId = entity.i_SystemUserId;
            dto.v_WarehouseId = entity.v_WarehouseId;
            dto.i_NodeId = entity.i_NodeId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="restrictedwarehouseprofileDto"/> to an instance of <see cref="restrictedwarehouseprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<restrictedwarehouseprofile> ToEntities(this IEnumerable<restrictedwarehouseprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="restrictedwarehouseprofile"/> to an instance of <see cref="restrictedwarehouseprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<restrictedwarehouseprofileDto> ToDTOs(this IEnumerable<restrictedwarehouseprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
