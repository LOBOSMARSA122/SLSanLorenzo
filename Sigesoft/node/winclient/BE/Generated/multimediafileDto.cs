//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/07 - 10:47:07
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
    public partial class multimediafileDto
    {
        [DataMember()]
        public String v_MultimediaFileId { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public String v_FileName { get; set; }

        [DataMember()]
        public Byte[] b_File { get; set; }

        [DataMember()]
        public Byte[] b_ThumbnailFile { get; set; }

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
        public personDto person { get; set; }

        [DataMember()]
        public List<servicecomponentfieldvaluesmultimediaDto> servicecomponentfieldvaluesmultimedia { get; set; }

        [DataMember()]
        public List<servicecomponentmultimediaDto> servicecomponentmultimedia { get; set; }

        [DataMember()]
        public List<servicemultimediaDto> servicemultimedia { get; set; }

        public multimediafileDto()
        {
        }

        public multimediafileDto(String v_MultimediaFileId, String v_PersonId, String v_FileName, Byte[] b_File, Byte[] b_ThumbnailFile, String v_Comment, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, personDto person, List<servicecomponentfieldvaluesmultimediaDto> servicecomponentfieldvaluesmultimedia, List<servicecomponentmultimediaDto> servicecomponentmultimedia, List<servicemultimediaDto> servicemultimedia)
        {
			this.v_MultimediaFileId = v_MultimediaFileId;
			this.v_PersonId = v_PersonId;
			this.v_FileName = v_FileName;
			this.b_File = b_File;
			this.b_ThumbnailFile = b_ThumbnailFile;
			this.v_Comment = v_Comment;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.person = person;
			this.servicecomponentfieldvaluesmultimedia = servicecomponentfieldvaluesmultimedia;
			this.servicecomponentmultimedia = servicecomponentmultimedia;
			this.servicemultimedia = servicemultimedia;
        }
    }
}
