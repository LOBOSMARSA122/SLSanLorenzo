//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/14 - 10:38:13
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
    public partial class servicemultimediaDto
    {
        [DataMember()]
        public String v_ServiceMultimediaId { get; set; }

        [DataMember()]
        public String v_ServiceId { get; set; }

        [DataMember()]
        public String v_MultimediaFileId { get; set; }

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
        public multimediafileDto multimediafile { get; set; }

        [DataMember()]
        public serviceDto service { get; set; }

        public servicemultimediaDto()
        {
        }

        public servicemultimediaDto(String v_ServiceMultimediaId, String v_ServiceId, String v_MultimediaFileId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, multimediafileDto multimediafile, serviceDto service)
        {
			this.v_ServiceMultimediaId = v_ServiceMultimediaId;
			this.v_ServiceId = v_ServiceId;
			this.v_MultimediaFileId = v_MultimediaFileId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.multimediafile = multimediafile;
			this.service = service;
        }
    }
}
