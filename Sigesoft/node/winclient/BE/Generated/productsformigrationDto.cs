//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/26 - 17:36:00
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
    public partial class productsformigrationDto
    {
        [DataMember()]
        public Int32 i_ProductForMigrationId { get; set; }

        [DataMember()]
        public String v_WarehouseId { get; set; }

        [DataMember()]
        public String v_ProductId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

        [DataMember()]
        public Nullable<Single> r_StockMin { get; set; }

        [DataMember()]
        public Nullable<Single> r_StockMax { get; set; }

        [DataMember()]
        public Nullable<Single> r_StockActual { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MovementTypeId { get; set; }

        [DataMember()]
        public String v_MovementType { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MotiveTypeId { get; set; }

        [DataMember()]
        public String v_MotiveType { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_MovementDate { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        public productsformigrationDto()
        {
        }

        public productsformigrationDto(Int32 i_ProductForMigrationId, String v_WarehouseId, String v_ProductId, Nullable<Int32> i_CategoryId, String v_Name, Nullable<Single> r_StockMin, Nullable<Single> r_StockMax, Nullable<Single> r_StockActual, Nullable<Int32> i_MovementTypeId, String v_MovementType, Nullable<Int32> i_MotiveTypeId, String v_MotiveType, Nullable<DateTime> d_MovementDate, Nullable<DateTime> d_InsertDate)
        {
			this.i_ProductForMigrationId = i_ProductForMigrationId;
			this.v_WarehouseId = v_WarehouseId;
			this.v_ProductId = v_ProductId;
			this.i_CategoryId = i_CategoryId;
			this.v_Name = v_Name;
			this.r_StockMin = r_StockMin;
			this.r_StockMax = r_StockMax;
			this.r_StockActual = r_StockActual;
			this.i_MovementTypeId = i_MovementTypeId;
			this.v_MovementType = v_MovementType;
			this.i_MotiveTypeId = i_MotiveTypeId;
			this.v_MotiveType = v_MotiveType;
			this.d_MovementDate = d_MovementDate;
			this.d_InsertDate = d_InsertDate;
        }
    }
}
