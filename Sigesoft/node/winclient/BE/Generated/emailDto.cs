//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/08/06 - 13:08:50
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
    public partial class emailDto
    {
        [DataMember()]
        public Int32 i_EmailId { get; set; }

        [DataMember()]
        public String v_Email { get; set; }

        public emailDto()
        {
        }

        public emailDto(Int32 i_EmailId, String v_Email)
        {
			this.i_EmailId = i_EmailId;
			this.v_Email = v_Email;
        }
    }
}
