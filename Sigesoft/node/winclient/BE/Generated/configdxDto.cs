//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/04/05 - 15:01:17
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
    public partial class configdxDto
    {
        [DataMember()]
        public String v_ConfigDxId { get; set; }

        [DataMember()]
        public String v_DiseaseId { get; set; }

        [DataMember()]
        public String v_ProductId { get; set; }

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
        public diseasesDto diseases { get; set; }

        [DataMember()]
        public productDto product { get; set; }

        public configdxDto()
        {
        }

        public configdxDto(String v_ConfigDxId, String v_DiseaseId, String v_ProductId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, diseasesDto diseases, productDto product)
        {
			this.v_ConfigDxId = v_ConfigDxId;
			this.v_DiseaseId = v_DiseaseId;
			this.v_ProductId = v_ProductId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.diseases = diseases;
			this.product = product;
        }
    }
}
