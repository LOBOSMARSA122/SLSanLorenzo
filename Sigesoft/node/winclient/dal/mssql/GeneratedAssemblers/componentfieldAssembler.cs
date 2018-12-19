//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/18 - 18:54:15
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
    /// Assembler for <see cref="componentfield"/> and <see cref="componentfieldDto"/>.
    /// </summary>
    public static partial class componentfieldAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldDto"/> converted from <see cref="componentfield"/>.</param>
        static partial void OnDTO(this componentfield entity, componentfieldDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="componentfield"/> converted from <see cref="componentfieldDto"/>.</param>
        static partial void OnEntity(this componentfieldDto dto, componentfield entity);

        /// <summary>
        /// Converts this instance of <see cref="componentfieldDto"/> to an instance of <see cref="componentfield"/>.
        /// </summary>
        /// <param name="dto"><see cref="componentfieldDto"/> to convert.</param>
        public static componentfield ToEntity(this componentfieldDto dto)
        {
            if (dto == null) return null;

            var entity = new componentfield();

            entity.v_ComponentFieldId = dto.v_ComponentFieldId;
            entity.v_TextLabel = dto.v_TextLabel;
            entity.i_LabelWidth = dto.i_LabelWidth;
            entity.v_abbreviation = dto.v_abbreviation;
            entity.v_DefaultText = dto.v_DefaultText;
            entity.i_ControlId = dto.i_ControlId;
            entity.i_GroupId = dto.i_GroupId;
            entity.i_ItemId = dto.i_ItemId;
            entity.i_WidthControl = dto.i_WidthControl;
            entity.i_HeightControl = dto.i_HeightControl;
            entity.i_MaxLenght = dto.i_MaxLenght;
            entity.i_IsRequired = dto.i_IsRequired;
            entity.i_IsCalculate = dto.i_IsCalculate;
            entity.v_Formula = dto.v_Formula;
            entity.i_Order = dto.i_Order;
            entity.i_MeasurementUnitId = dto.i_MeasurementUnitId;
            entity.r_ValidateValue1 = dto.r_ValidateValue1;
            entity.r_ValidateValue2 = dto.r_ValidateValue2;
            entity.i_Column = dto.i_Column;
            entity.i_defaultIndex = dto.i_defaultIndex;
            entity.i_IsDeleted = dto.i_IsDeleted;
            entity.i_InsertUserId = dto.i_InsertUserId;
            entity.d_InsertDate = dto.d_InsertDate;
            entity.i_UpdateUserId = dto.i_UpdateUserId;
            entity.d_UpdateDate = dto.d_UpdateDate;
            entity.i_NroDecimales = dto.i_NroDecimales;
            entity.i_ReadOnly = dto.i_ReadOnly;
            entity.i_Enabled = dto.i_Enabled;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="componentfield"/> to an instance of <see cref="componentfieldDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="componentfield"/> to convert.</param>
        public static componentfieldDto ToDTO(this componentfield entity)
        {
            if (entity == null) return null;

            var dto = new componentfieldDto();

            dto.v_ComponentFieldId = entity.v_ComponentFieldId;
            dto.v_TextLabel = entity.v_TextLabel;
            dto.i_LabelWidth = entity.i_LabelWidth;
            dto.v_abbreviation = entity.v_abbreviation;
            dto.v_DefaultText = entity.v_DefaultText;
            dto.i_ControlId = entity.i_ControlId;
            dto.i_GroupId = entity.i_GroupId;
            dto.i_ItemId = entity.i_ItemId;
            dto.i_WidthControl = entity.i_WidthControl;
            dto.i_HeightControl = entity.i_HeightControl;
            dto.i_MaxLenght = entity.i_MaxLenght;
            dto.i_IsRequired = entity.i_IsRequired;
            dto.i_IsCalculate = entity.i_IsCalculate;
            dto.v_Formula = entity.v_Formula;
            dto.i_Order = entity.i_Order;
            dto.i_MeasurementUnitId = entity.i_MeasurementUnitId;
            dto.r_ValidateValue1 = entity.r_ValidateValue1;
            dto.r_ValidateValue2 = entity.r_ValidateValue2;
            dto.i_Column = entity.i_Column;
            dto.i_defaultIndex = entity.i_defaultIndex;
            dto.i_IsDeleted = entity.i_IsDeleted;
            dto.i_InsertUserId = entity.i_InsertUserId;
            dto.d_InsertDate = entity.d_InsertDate;
            dto.i_UpdateUserId = entity.i_UpdateUserId;
            dto.d_UpdateDate = entity.d_UpdateDate;
            dto.i_NroDecimales = entity.i_NroDecimales;
            dto.i_ReadOnly = entity.i_ReadOnly;
            dto.i_Enabled = entity.i_Enabled;

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfieldDto"/> to an instance of <see cref="componentfield"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<componentfield> ToEntities(this IEnumerable<componentfieldDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="componentfield"/> to an instance of <see cref="componentfieldDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<componentfieldDto> ToDTOs(this IEnumerable<componentfield> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }

    }
}
