//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/02/23 - 11:27:45
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
    public partial class testcorrunceDto
    {
        [DataMember()]
        public String TestConcurrenceId { get; set; }

        [DataMember()]
        public String Value { get; set; }

        public testcorrunceDto()
        {
        }

        public testcorrunceDto(String testConcurrenceId, String value)
        {
			this.TestConcurrenceId = testConcurrenceId;
			this.Value = value;
        }
    }
}
