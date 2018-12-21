//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/20 - 15:57:36
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
    public partial class medicationDto
    {
        [DataMember()]
        public String v_MedicationId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_ProductId { get; set; }

        [DataMember()]
        public String v_ProductName { get; set; }

        [DataMember()]
        public String v_PresentationName { get; set; }

        [DataMember()]
        public Nullable<Single> r_Quantity { get; set; }

        [DataMember()]
        public Nullable<Single> r_QuantityVendida { get; set; }

        [DataMember()]
        public String v_Doses { get; set; }

        [DataMember()]
        public Nullable<Int32> i_ViaId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SeVendio { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SeCreoAca { get; set; }

        [DataMember()]
        public String v_TipoDocVenta { get; set; }

        [DataMember()]
        public String v_NroDocVenta { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_PrecioVenta { get; set; }

        [DataMember()]
        public String v_MedioPago { get; set; }

        [DataMember()]
        public String v_Vendedor { get; set; }

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
        public serviceDto service { get; set; }

        public medicationDto()
        {
        }

        public medicationDto(String v_MedicationId, String v_ServiceId, String v_ProductId, String v_ProductName, String v_PresentationName, Nullable<Single> r_Quantity, Nullable<Single> r_QuantityVendida, String v_Doses, Nullable<Int32> i_ViaId, Nullable<Int32> i_SeVendio, Nullable<Int32> i_SeCreoAca, String v_TipoDocVenta, String v_NroDocVenta, Nullable<Decimal> d_PrecioVenta, String v_MedioPago, String v_Vendedor, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, serviceDto service)
        {
			this.v_MedicationId = v_MedicationId;
			this.v_ServiceId = v_ServiceId;
			this.v_ProductId = v_ProductId;
			this.v_ProductName = v_ProductName;
			this.v_PresentationName = v_PresentationName;
			this.r_Quantity = r_Quantity;
			this.r_QuantityVendida = r_QuantityVendida;
			this.v_Doses = v_Doses;
			this.i_ViaId = i_ViaId;
			this.i_SeVendio = i_SeVendio;
			this.i_SeCreoAca = i_SeCreoAca;
			this.v_TipoDocVenta = v_TipoDocVenta;
			this.v_NroDocVenta = v_NroDocVenta;
			this.d_PrecioVenta = d_PrecioVenta;
			this.v_MedioPago = v_MedioPago;
			this.v_Vendedor = v_Vendedor;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.service = service;
        }
    }
}
