//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/08 - 11:57:22
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
    public partial class facturacionDto
    {
        [DataMember()]
        public String v_FacturacionId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaRegistro { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaCobro { get; set; }

        [DataMember()]
        public String v_NumeroFactura { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EstadoFacturacion { get; set; }

        [DataMember()]
        public String v_EmpresaCliente { get; set; }

        [DataMember()]
        public String v_EmpresaSede { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Detraccion { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_SubTotal { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Igv { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_MontoTotal { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaInicio { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaFin { get; set; }

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
        public String v_Descripcion { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Descuento { get; set; }

        [DataMember()]
        public List<facturaciondetalleDto> facturaciondetalle { get; set; }

        public facturacionDto()
        {
        }

        public facturacionDto(String v_FacturacionId, Nullable<DateTime> d_FechaRegistro, Nullable<DateTime> d_FechaCobro, String v_NumeroFactura, Nullable<Int32> i_EstadoFacturacion, String v_EmpresaCliente, String v_EmpresaSede, Nullable<Decimal> d_Detraccion, Nullable<Decimal> d_SubTotal, Nullable<Decimal> d_Igv, Nullable<Decimal> d_MontoTotal, Nullable<DateTime> d_FechaInicio, Nullable<DateTime> d_FechaFin, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_Descripcion, Nullable<Decimal> d_Descuento, List<facturaciondetalleDto> facturaciondetalle)
        {
			this.v_FacturacionId = v_FacturacionId;
			this.d_FechaRegistro = d_FechaRegistro;
			this.d_FechaCobro = d_FechaCobro;
			this.v_NumeroFactura = v_NumeroFactura;
			this.i_EstadoFacturacion = i_EstadoFacturacion;
			this.v_EmpresaCliente = v_EmpresaCliente;
			this.v_EmpresaSede = v_EmpresaSede;
			this.d_Detraccion = d_Detraccion;
			this.d_SubTotal = d_SubTotal;
			this.d_Igv = d_Igv;
			this.d_MontoTotal = d_MontoTotal;
			this.d_FechaInicio = d_FechaInicio;
			this.d_FechaFin = d_FechaFin;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_Descripcion = v_Descripcion;
			this.d_Descuento = d_Descuento;
			this.facturaciondetalle = facturaciondetalle;
        }
    }
}
