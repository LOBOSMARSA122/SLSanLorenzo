//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/01 - 08:27:51
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
    public partial class recommendationDto
    {
        [DataMember()]
        public String v_RecommendationId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_DiagnosticRepositoryId { get; set; }

        [DataMember()]
        public String v_ComponentId { get; set; }

        [DataMember()]
        public String v_MasterRecommendationId { get; set; }

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
        public componentDto component { get; set; }

        [DataMember()]
        public diagnosticrepositoryDto diagnosticrepository { get; set; }

        [DataMember()]
        public serviceDto service { get; set; }

        public recommendationDto()
        {
        }

        public recommendationDto(String v_RecommendationId, String v_ServiceId, String v_DiagnosticRepositoryId, String v_ComponentId, String v_MasterRecommendationId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, componentDto component, diagnosticrepositoryDto diagnosticrepository, serviceDto service)
        {
			this.v_RecommendationId = v_RecommendationId;
			this.v_ServiceId = v_ServiceId;
			this.v_DiagnosticRepositoryId = v_DiagnosticRepositoryId;
			this.v_ComponentId = v_ComponentId;
			this.v_MasterRecommendationId = v_MasterRecommendationId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.component = component;
			this.diagnosticrepository = diagnosticrepository;
			this.service = service;
        }
    }
}
