//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/06/19 - 20:56:40
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
    public partial class serviceorderdetailDto
    {
        [DataMember()]
        public String v_ServiceOrderDetailId { get; set; }

        [DataMember()]
        public String v_ServiceOrderId { get; set; }

        [DataMember()]
        public String v_ProtocolId { get; set; }

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
        public Nullable<Single> r_ProtocolPrice { get; set; }

        [DataMember()]
        public Nullable<Int32> i_NumberOfWorkerProtocol { get; set; }

        [DataMember()]
        public Nullable<Single> r_Total { get; set; }

        [DataMember()]
        public protocolDto protocol { get; set; }

        [DataMember()]
        public serviceorderDto serviceorder { get; set; }

        public serviceorderdetailDto()
        {
        }

        public serviceorderdetailDto(String v_ServiceOrderDetailId, String v_ServiceOrderId, String v_ProtocolId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, Nullable<Single> r_ProtocolPrice, Nullable<Int32> i_NumberOfWorkerProtocol, Nullable<Single> r_Total, protocolDto protocol, serviceorderDto serviceorder)
        {
			this.v_ServiceOrderDetailId = v_ServiceOrderDetailId;
			this.v_ServiceOrderId = v_ServiceOrderId;
			this.v_ProtocolId = v_ProtocolId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.r_ProtocolPrice = r_ProtocolPrice;
			this.i_NumberOfWorkerProtocol = i_NumberOfWorkerProtocol;
			this.r_Total = r_Total;
			this.protocol = protocol;
			this.serviceorder = serviceorder;
        }
    }
}
