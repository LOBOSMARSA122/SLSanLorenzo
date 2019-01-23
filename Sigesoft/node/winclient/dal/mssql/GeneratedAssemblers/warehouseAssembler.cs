//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/01/23 - 17:51:18
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
    /// Assembler for <see cref="warehouse"/> and <see cref="warehouseDto"/>.
    /// </summary>
    public static partial class warehouseAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="warehouseDto"/> converted from <see cref="warehouse"/>.</param>
        static partial void OnDTO(this warehouse entity, warehouseDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="warehouse"/> converted from <see cref="warehouseDto"/>.</param>
        static partial void OnEntity(this warehouseDto dto, warehouse entity);

        /// <summary>
        /// Converts this instance of <see cref="warehouseDto"/> to an instance of <see cref="warehouse"/>.
        /// </summary>
        /// <param name="dto"><see cref="warehouseDto"/> to convert.</param>
        public static warehouse ToEntity(this warehouseDto dto)
        {
            if (dto == null) return null;

            var entity = new warehouse();

            entity.v_WarehouseId = dto.v_WarehouseId;
            entity.v_OrganizationId = dto.v_OrganizationId;
            entity.v_LocationId = dto.v_LocationId;
            entity.v_Name = dto.v_Name;
            entity.v_AdditionalInformation = dto.v_AdditionalInformation;
            entity.i_CostCenterId = dto.i_CostCenterId;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="warehouse"/> to an instance of <see cref="warehouseDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="warehouse"/> to convert.</param>
        public static warehouseDto ToDTO(this warehouse entity)
        {
            if (entity == null) return null;

            var dto = new warehouseDto();

            dto.v_WarehouseId = entity.v_WarehouseId;
            dto.v_OrganizationId = entity.v_OrganizationId;
            dto.v_LocationId = entity.v_LocationId;
            dto.v_Name = entity.v_Name;
            dto.v_AdditionalInformation = entity.v_AdditionalInformation;
            dto.i_CostCenterId = entity.i_CostCenterId;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="warehouseDto"/> to an instance of <see cref="warehouse"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<warehouse> ToEntities(this IEnumerable<warehouseDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="warehouse"/> to an instance of <see cref="warehouseDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<warehouseDto> ToDTOs(this IEnumerable<warehouse> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
