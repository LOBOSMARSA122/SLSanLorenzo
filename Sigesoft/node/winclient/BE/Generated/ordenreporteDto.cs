//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/01/23 - 17:48:38
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
    public partial class ordenreporteDto
    {
        [DataMember()]
        public String v_OrdenReporteId { get; set; }

        [DataMember()]
        public String v_OrganizationId { get; set; }

        [DataMember()]
        public String v_NombreReporte { get; set; }

        [DataMember()]
        public String v_ComponenteId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Orden { get; set; }

        [DataMember()]
        public String v_NombreCrystal { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NombreCrystalId { get; set; }

        public ordenreporteDto()
        {
        }

        public ordenreporteDto(String v_OrdenReporteId, String v_OrganizationId, String v_NombreReporte, String v_ComponenteId, Nullable<Int32> i_Orden, String v_NombreCrystal, Nullable<Int32> i_NombreCrystalId)
        {
			this.v_OrdenReporteId = v_OrdenReporteId;
			this.v_OrganizationId = v_OrganizationId;
			this.v_NombreReporte = v_NombreReporte;
			this.v_ComponenteId = v_ComponenteId;
			this.i_Orden = i_Orden;
			this.v_NombreCrystal = v_NombreCrystal;
			this.i_NombreCrystalId = i_NombreCrystalId;
        }
    }
}
