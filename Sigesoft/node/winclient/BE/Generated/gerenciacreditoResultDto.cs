//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/01/02 - 18:48:07
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
    public partial class gerenciacreditoResultDto
    {
        [DataMember()]
        public Nullable<DateTime> FechaServicio { get; set; }

        [DataMember()]
        public String ServiceId { get; set; }

        [DataMember()]
        public String Trabajador { get; set; }

        [DataMember()]
        public String Ocupacion { get; set; }

        [DataMember()]
        public String TipoEso { get; set; }

        [DataMember()]
        public Nullable<Double> CostoExamen { get; set; }

        [DataMember()]
        public String Compania { get; set; }

        [DataMember()]
        public String Contratista { get; set; }

        [DataMember()]
        public String EmpresaFacturacion { get; set; }

        [DataMember()]
        public String Comprobante { get; set; }

        [DataMember()]
        public String NroLiquidacion { get; set; }

        [DataMember()]
        public Nullable<DateTime> FechaFactura { get; set; }

        [DataMember()]
        public Nullable<Decimal> ImporteTotalFactura { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_NetoXCobrarFactura { get; set; }

        [DataMember()]
        public String CondicionFactura { get; set; }

        [DataMember()]
        public String xxx { get; set; }

        public gerenciacreditoResultDto()
        {
        }

        public gerenciacreditoResultDto(Nullable<DateTime> fechaServicio, String serviceId, String trabajador, String ocupacion, String tipoEso, Nullable<Double> costoExamen, String compania, String contratista, String empresaFacturacion, String comprobante, String nroLiquidacion, Nullable<DateTime> fechaFactura, Nullable<Decimal> importeTotalFactura, Nullable<Decimal> d_NetoXCobrarFactura, String condicionFactura, String xxx)
        {
			this.FechaServicio = fechaServicio;
			this.ServiceId = serviceId;
			this.Trabajador = trabajador;
			this.Ocupacion = ocupacion;
			this.TipoEso = tipoEso;
			this.CostoExamen = costoExamen;
			this.Compania = compania;
			this.Contratista = contratista;
			this.EmpresaFacturacion = empresaFacturacion;
			this.Comprobante = comprobante;
			this.NroLiquidacion = nroLiquidacion;
			this.FechaFactura = fechaFactura;
			this.ImporteTotalFactura = importeTotalFactura;
			this.d_NetoXCobrarFactura = d_NetoXCobrarFactura;
			this.CondicionFactura = condicionFactura;
			this.xxx = xxx;
        }
    }
}
