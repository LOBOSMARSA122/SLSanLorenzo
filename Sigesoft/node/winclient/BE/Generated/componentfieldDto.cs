//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 09:27:20
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class componentfieldDto
    {
        [DataMember()]
        public String v_ComponentFieldId { get; set; }

        [DataMember()]
        public String v_TextLabel { get; set; }

        [DataMember()]
        public Nullable<Int32> i_LabelWidth { get; set; }

        [DataMember()]
        public String v_abbreviation { get; set; }

        [DataMember()]
        public String v_DefaultText { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ControlId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GroupId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ItemId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_WidthControl { get; set; }

        [DataMember()]
        public Nullable<Int32> i_HeightControl { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MaxLenght { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsRequired { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsCalculate { get; set; }

        [DataMember()]
        public String v_Formula { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Order { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MeasurementUnitId { get; set; }

        [DataMember()]
        public Nullable<Single> r_ValidateValue1 { get; set; }

        [DataMember()]
        public Nullable<Single> r_ValidateValue2 { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Column { get; set; }

        [DataMember()]
        public Nullable<Int32> i_defaultIndex { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NroDecimales { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ReadOnly { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Enabled { get; set; }

        [DataMember()]
        public List<componentfieldsDto> componentfields { get; set; }

        [DataMember()]
        public List<componentfieldvaluesDto> componentfieldvalues { get; set; }

        public componentfieldDto()
        {
        }

        public componentfieldDto(String v_ComponentFieldId, String v_TextLabel, Nullable<Int32> i_LabelWidth, String v_abbreviation, String v_DefaultText, Nullable<Int32> i_ControlId, Nullable<Int32> i_GroupId, Nullable<Int32> i_ItemId, Nullable<Int32> i_WidthControl, Nullable<Int32> i_HeightControl, Nullable<Int32> i_MaxLenght, Nullable<Int32> i_IsRequired, Nullable<Int32> i_IsCalculate, String v_Formula, Nullable<Int32> i_Order, Nullable<Int32> i_MeasurementUnitId, Nullable<Single> r_ValidateValue1, Nullable<Single> r_ValidateValue2, Nullable<Int32> i_Column, Nullable<Int32> i_defaultIndex, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<Int32> i_NroDecimales, Nullable<Int32> i_ReadOnly, Nullable<Int32> i_Enabled, List<componentfieldsDto> componentfields, List<componentfieldvaluesDto> componentfieldvalues)
        {
			this.v_ComponentFieldId = v_ComponentFieldId;
			this.v_TextLabel = v_TextLabel;
			this.i_LabelWidth = i_LabelWidth;
			this.v_abbreviation = v_abbreviation;
			this.v_DefaultText = v_DefaultText;
			this.i_ControlId = i_ControlId;
			this.i_GroupId = i_GroupId;
			this.i_ItemId = i_ItemId;
			this.i_WidthControl = i_WidthControl;
			this.i_HeightControl = i_HeightControl;
			this.i_MaxLenght = i_MaxLenght;
			this.i_IsRequired = i_IsRequired;
			this.i_IsCalculate = i_IsCalculate;
			this.v_Formula = v_Formula;
			this.i_Order = i_Order;
			this.i_MeasurementUnitId = i_MeasurementUnitId;
			this.r_ValidateValue1 = r_ValidateValue1;
			this.r_ValidateValue2 = r_ValidateValue2;
			this.i_Column = i_Column;
			this.i_defaultIndex = i_defaultIndex;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.i_NroDecimales = i_NroDecimales;
			this.i_ReadOnly = i_ReadOnly;
			this.i_Enabled = i_Enabled;
			this.componentfields = componentfields;
			this.componentfieldvalues = componentfieldvalues;
        }
    }
}
