//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/04/05 - 15:04:03
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
    /// Assembler for <see cref="product"/> and <see cref="productDto"/>.
    /// </summary>
    public static partial class productAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="productDto"/> converted from <see cref="product"/>.</param>
        static partial void OnDTO(this product entity, productDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="product"/> converted from <see cref="productDto"/>.</param>
        static partial void OnEntity(this productDto dto, product entity);

        /// <summary>
        /// Converts this instance of <see cref="productDto"/> to an instance of <see cref="product"/>.
        /// </summary>
        /// <param name="dto"><see cref="productDto"/> to convert.</param>
        public static product ToEntity(this productDto dto)
        {
            if (dto == null) return null;

            var entity = new product();

            entity.v_ProductId = dto.v_ProductId;
            entity.i_CategoryId = dto.i_CategoryId;
            entity.v_Name = dto.v_Name;
            entity.v_GenericName = dto.v_GenericName;
            entity.v_BarCode = dto.v_BarCode;
            entity.v_ProductCode = dto.v_ProductCode;
            entity.v_Brand = dto.v_Brand;
            entity.v_Model = dto.v_Model;
            entity.v_SerialNumber = dto.v_SerialNumber;
            entity.d_ExpirationDate = dto.d_ExpirationDate;
            entity.i_MeasurementUnitId = dto.i_MeasurementUnitId;
            entity.r_ReferentialCostPrice = dto.r_ReferentialCostPrice;
            entity.r_ReferentialSalesPrice = dto.r_ReferentialSalesPrice;
            entity.v_Presentation = dto.v_Presentation;
            entity.v_AdditionalInformation = dto.v_AdditionalInformation;
            entity.b_Image = dto.b_Image;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="product"/> to an instance of <see cref="productDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="product"/> to convert.</param>
        public static productDto ToDTO(this product entity)
        {
            if (entity == null) return null;

            var dto = new productDto();

            dto.v_ProductId = entity.v_ProductId;
            dto.i_CategoryId = entity.i_CategoryId;
            dto.v_Name = entity.v_Name;
            dto.v_GenericName = entity.v_GenericName;
            dto.v_BarCode = entity.v_BarCode;
            dto.v_ProductCode = entity.v_ProductCode;
            dto.v_Brand = entity.v_Brand;
            dto.v_Model = entity.v_Model;
            dto.v_SerialNumber = entity.v_SerialNumber;
            dto.d_ExpirationDate = entity.d_ExpirationDate;
            dto.i_MeasurementUnitId = entity.i_MeasurementUnitId;
            dto.r_ReferentialCostPrice = entity.r_ReferentialCostPrice;
            dto.r_ReferentialSalesPrice = entity.r_ReferentialSalesPrice;
            dto.v_Presentation = entity.v_Presentation;
            dto.v_AdditionalInformation = entity.v_AdditionalInformation;
            dto.b_Image = entity.b_Image;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="productDto"/> to an instance of <see cref="product"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<product> ToEntities(this IEnumerable<productDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="product"/> to an instance of <see cref="productDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<productDto> ToDTOs(this IEnumerable<product> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
