//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 09:28:07
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
    /// Assembler for <see cref="supplier"/> and <see cref="supplierDto"/>.
    /// </summary>
    public static partial class supplierAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="supplierDto"/> converted from <see cref="supplier"/>.</param>
        static partial void OnDTO(this supplier entity, supplierDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="supplier"/> converted from <see cref="supplierDto"/>.</param>
        static partial void OnEntity(this supplierDto dto, supplier entity);

        /// <summary>
        /// Converts this instance of <see cref="supplierDto"/> to an instance of <see cref="supplier"/>.
        /// </summary>
        /// <param name="dto"><see cref="supplierDto"/> to convert.</param>
        public static supplier ToEntity(this supplierDto dto)
        {
            if (dto == null) return null;

            var entity = new supplier();

            entity.v_SupplierId = dto.v_SupplierId;
            entity.i_SectorTypeId = dto.i_SectorTypeId;
            entity.v_IdentificationNumber = dto.v_IdentificationNumber;
            entity.v_Name = dto.v_Name;
            entity.v_Address = dto.v_Address;
            entity.v_PhoneNumber = dto.v_PhoneNumber;
            entity.v_Mail = dto.v_Mail;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_UpdateNodeId = dto.i_UpdateNodeId;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="supplier"/> to an instance of <see cref="supplierDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="supplier"/> to convert.</param>
        public static supplierDto ToDTO(this supplier entity)
        {
            if (entity == null) return null;

            var dto = new supplierDto();

            dto.v_SupplierId = entity.v_SupplierId;
            dto.i_SectorTypeId = entity.i_SectorTypeId;
            dto.v_IdentificationNumber = entity.v_IdentificationNumber;
            dto.v_Name = entity.v_Name;
            dto.v_Address = entity.v_Address;
            dto.v_PhoneNumber = entity.v_PhoneNumber;
            dto.v_Mail = entity.v_Mail;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_UpdateNodeId = entity.i_UpdateNodeId;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="supplierDto"/> to an instance of <see cref="supplier"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<supplier> ToEntities(this IEnumerable<supplierDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="supplier"/> to an instance of <see cref="supplierDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<supplierDto> ToDTOs(this IEnumerable<supplier> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
