//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/05/28 - 11:58:44
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
    /// Assembler for <see cref="nodeorganizationlocationwarehouseprofile"/> and <see cref="nodeorganizationlocationwarehouseprofileDto"/>.
    /// </summary>
    public static partial class nodeorganizationlocationwarehouseprofileAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="nodeorganizationlocationwarehouseprofileDto"/> converted from <see cref="nodeorganizationlocationwarehouseprofile"/>.</param>
        static partial void OnDTO(this nodeorganizationlocationwarehouseprofile entity, nodeorganizationlocationwarehouseprofileDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="nodeorganizationlocationwarehouseprofile"/> converted from <see cref="nodeorganizationlocationwarehouseprofileDto"/>.</param>
        static partial void OnEntity(this nodeorganizationlocationwarehouseprofileDto dto, nodeorganizationlocationwarehouseprofile entity);

        /// <summary>
        /// Converts this instance of <see cref="nodeorganizationlocationwarehouseprofileDto"/> to an instance of <see cref="nodeorganizationlocationwarehouseprofile"/>.
        /// </summary>
        /// <param name="dto"><see cref="nodeorganizationlocationwarehouseprofileDto"/> to convert.</param>
        public static nodeorganizationlocationwarehouseprofile ToEntity(this nodeorganizationlocationwarehouseprofileDto dto)
        {
            if (dto == null) return null;

            var entity = new nodeorganizationlocationwarehouseprofile();

            entity.i_NodeId = dto.i_NodeId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.v_LocationId = dto.v_LocationId;
            entity.v_WarehouseId = dto.v_WarehouseId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="nodeorganizationlocationwarehouseprofile"/> to an instance of <see cref="nodeorganizationlocationwarehouseprofileDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="nodeorganizationlocationwarehouseprofile"/> to convert.</param>
        public static nodeorganizationlocationwarehouseprofileDto ToDTO(this nodeorganizationlocationwarehouseprofile entity)
        {
            if (entity == null) return null;

            var dto = new nodeorganizationlocationwarehouseprofileDto();

            dto.i_NodeId = entity.i_NodeId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.v_LocationId = entity.v_LocationId;
            dto.v_WarehouseId = entity.v_WarehouseId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeorganizationlocationwarehouseprofileDto"/> to an instance of <see cref="nodeorganizationlocationwarehouseprofile"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<nodeorganizationlocationwarehouseprofile> ToEntities(this IEnumerable<nodeorganizationlocationwarehouseprofileDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="nodeorganizationlocationwarehouseprofile"/> to an instance of <see cref="nodeorganizationlocationwarehouseprofileDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<nodeorganizationlocationwarehouseprofileDto> ToDTOs(this IEnumerable<nodeorganizationlocationwarehouseprofile> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
