//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/18 - 18:52:01
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
    public partial class resumentipopagoResultDto
    {
        [DataMember()]
        public Nullable<Int32> IdCondicionPago { get; set; }

        [DataMember()]
        public String CondicionPago { get; set; }

        [DataMember()]
        public Nullable<Int32> IdFormaPago { get; set; }

        [DataMember()]
        public String FormaPago { get; set; }

        [DataMember()]
        public Nullable<DateTime> Fecha { get; set; }

        [DataMember()]
        public String Comprobante { get; set; }

        [DataMember()]
        public String Empresa { get; set; }

        [DataMember()]
        public Nullable<Decimal> Importe { get; set; }

        public resumentipopagoResultDto()
        {
        }

        public resumentipopagoResultDto(Nullable<Int32> idCondicionPago, String condicionPago, Nullable<Int32> idFormaPago, String formaPago, Nullable<DateTime> fecha, String comprobante, String empresa, Nullable<Decimal> importe)
        {
			this.IdCondicionPago = idCondicionPago;
			this.CondicionPago = condicionPago;
			this.IdFormaPago = idFormaPago;
			this.FormaPago = formaPago;
			this.Fecha = fecha;
			this.Comprobante = comprobante;
			this.Empresa = empresa;
			this.Importe = importe;
        }
    }
}
