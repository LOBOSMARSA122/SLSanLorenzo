//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/26 - 10:55:14
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
    public partial class dxfrecuentedetalleDto
    {
        [DataMember()]
        public String v_DxFrecuenteDetalleId { get; set; }

        [DataMember()]
        public String v_DxFrecuenteId { get; set; }

        [DataMember()]
        public String v_MasterRecommendationRestricctionId { get; set; }

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
        public dxfrecuenteDto dxfrecuente { get; set; }

        public dxfrecuentedetalleDto()
        {
        }

        public dxfrecuentedetalleDto(String v_DxFrecuenteDetalleId, String v_DxFrecuenteId, String v_MasterRecommendationRestricctionId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, dxfrecuenteDto dxfrecuente)
        {
			this.v_DxFrecuenteDetalleId = v_DxFrecuenteDetalleId;
			this.v_DxFrecuenteId = v_DxFrecuenteId;
			this.v_MasterRecommendationRestricctionId = v_MasterRecommendationRestricctionId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.dxfrecuente = dxfrecuente;
        }
    }
}
