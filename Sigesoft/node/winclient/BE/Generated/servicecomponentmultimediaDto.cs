//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/10 - 12:32:46
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
    public partial class servicecomponentmultimediaDto
    {
        [DataMember()]
        public String v_ServiceComponentMultimediaId { get; set; }

        [DataMember()]
        public String v_ServiceComponentId { get; set; }

        [DataMember()]
        public String v_MultimediaFileId { get; set; }

        [DataMember()]
        public String v_Comment { get; set; }

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
        public servicecomponentDto servicecomponent { get; set; }

        public servicecomponentmultimediaDto()
        {
        }

        public servicecomponentmultimediaDto(String v_ServiceComponentMultimediaId, String v_ServiceComponentId, String v_MultimediaFileId, String v_Comment, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, multimediafileDto multimediafile, servicecomponentDto servicecomponent)
        {
			this.v_ServiceComponentMultimediaId = v_ServiceComponentMultimediaId;
			this.v_ServiceComponentId = v_ServiceComponentId;
			this.v_MultimediaFileId = v_MultimediaFileId;
			this.v_Comment = v_Comment;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.multimediafile = multimediafile;
			this.servicecomponent = servicecomponent;
        }
    }
}
