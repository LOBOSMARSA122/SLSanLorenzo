//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/18 - 11:14:15
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract()]
    public partial class resumentipoempresaResultDto
    {
        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String EmpresaCliente { get; set; }

        [DataMember()]
        public String EmpresaEmpleadora { get; set; }

        [DataMember()]
        public String EmpresaTrabajo { get; set; }

        [DataMember()]
        public Nullable<Double> Precio { get; set; }

        [DataMember()]
        public String Trabajador { get; set; }

        [DataMember()]
        public Nullable<DateTime> FechaExamen { get; set; }

        public resumentipoempresaResultDto()
        {
        }

        public resumentipoempresaResultDto(String v_ServiceId, String empresaCliente, String empresaEmpleadora, String empresaTrabajo, Nullable<Double> precio, String trabajador, Nullable<DateTime> fechaExamen)
        {
			this.v_ServiceId = v_ServiceId;
			this.EmpresaCliente = empresaCliente;
			this.EmpresaEmpleadora = empresaEmpleadora;
			this.EmpresaTrabajo = empresaTrabajo;
			this.Precio = precio;
			this.Trabajador = trabajador;
			this.FechaExamen = fechaExamen;
        }
    }
}
