//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/27 - 12:12:49
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
    /// Assembler for <see cref="productsformigration"/> and <see cref="productsformigrationDto"/>.
    /// </summary>
    public static partial class productsformigrationAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="productsformigrationDto"/> converted from <see cref="productsformigration"/>.</param>
        static partial void OnDTO(this productsformigration entity, productsformigrationDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="productsformigration"/> converted from <see cref="productsformigrationDto"/>.</param>
        static partial void OnEntity(this productsformigrationDto dto, productsformigration entity);

        /// <summary>
        /// Converts this instance of <see cref="productsformigrationDto"/> to an instance of <see cref="productsformigration"/>.
        /// </summary>
        /// <param name="dto"><see cref="productsformigrationDto"/> to convert.</param>
        public static productsformigration ToEntity(this productsformigrationDto dto)
        {
            if (dto == null) return null;

            var entity = new productsformigration();

            entity.i_ProductForMigrationId = dto.i_ProductForMigrationId;
            entity.v_WarehouseId = dto.v_WarehouseId;
            entity.v_ProductId = dto.v_ProductId;
            entity.i_CategoryId = dto.i_CategoryId;
            entity.v_Name = dto.v_Name;
            entity.r_StockMin = dto.r_StockMin;
            entity.r_StockMax = dto.r_StockMax;
            entity.r_StockActual = dto.r_StockActual;
            entity.i_MovementTypeId = dto.i_MovementTypeId;
            entity.v_MovementType = dto.v_MovementType;
            entity.i_MotiveTypeId = dto.i_MotiveTypeId;
            entity.v_MotiveType = dto.v_MotiveType;
            entity.d_MovementDate = dto.d_MovementDate;
            entity.d_InsertDate = dto.d_InsertDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="productsformigration"/> to an instance of <see cref="productsformigrationDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="productsformigration"/> to convert.</param>
        public static productsformigrationDto ToDTO(this productsformigration entity)
        {
            if (entity == null) return null;

            var dto = new productsformigrationDto();

            dto.i_ProductForMigrationId = entity.i_ProductForMigrationId;
            dto.v_WarehouseId = entity.v_WarehouseId;
            dto.v_ProductId = entity.v_ProductId;
            dto.i_CategoryId = entity.i_CategoryId;
            dto.v_Name = entity.v_Name;
            dto.r_StockMin = entity.r_StockMin;
            dto.r_StockMax = entity.r_StockMax;
            dto.r_StockActual = entity.r_StockActual;
            dto.i_MovementTypeId = entity.i_MovementTypeId;
            dto.v_MovementType = entity.v_MovementType;
            dto.i_MotiveTypeId = entity.i_MotiveTypeId;
            dto.v_MotiveType = entity.v_MotiveType;
            dto.d_MovementDate = entity.d_MovementDate;
            dto.d_InsertDate = entity.d_InsertDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="productsformigrationDto"/> to an instance of <see cref="productsformigration"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<productsformigration> ToEntities(this IEnumerable<productsformigrationDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="productsformigration"/> to an instance of <see cref="productsformigrationDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<productsformigrationDto> ToDTOs(this IEnumerable<productsformigration> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
