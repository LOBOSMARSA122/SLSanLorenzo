//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/18 - 11:43:35
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
    public partial class planDto
    {
        [DataMember()]
        public Int32 i_PlanId { get; set; }

        [DataMember()]
        public String v_OrganizationSeguroId { get; set; }

        [DataMember()]
        public String v_ProtocoloId { get; set; }

        [DataMember()]
        public String v_IdUnidadProductiva { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsDeducible { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsCoaseguro { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_Importe { get; set; }

        [DataMember()]
        public Nullable<Decimal> d_ImporteCo { get; set; }

        [DataMember()]
        public String v_ComentaryUpdate { get; set; }

        public planDto()
        {
        }

        public planDto(Int32 i_PlanId, String v_OrganizationSeguroId, String v_ProtocoloId, String v_IdUnidadProductiva, Nullable<Int32> i_EsDeducible, Nullable<Int32> i_EsCoaseguro, Nullable<Decimal> d_Importe, Nullable<Decimal> d_ImporteCo, String v_ComentaryUpdate)
        {
			this.i_PlanId = i_PlanId;
			this.v_OrganizationSeguroId = v_OrganizationSeguroId;
			this.v_ProtocoloId = v_ProtocoloId;
			this.v_IdUnidadProductiva = v_IdUnidadProductiva;
			this.i_EsDeducible = i_EsDeducible;
			this.i_EsCoaseguro = i_EsCoaseguro;
			this.d_Importe = d_Importe;
			this.d_ImporteCo = d_ImporteCo;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
        }
    }
}
