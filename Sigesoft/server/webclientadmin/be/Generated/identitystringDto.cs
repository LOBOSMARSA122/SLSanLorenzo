//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/14 - 10:36:49
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
    public partial class identitystringDto
    {
        [DataMember()]
        public Int32 Id { get; set; }

        [DataMember()]
        public String CombinedId { get; set; }

        [DataMember()]
        public String Nombre { get; set; }

        public identitystringDto()
        {
        }

        public identitystringDto(Int32 id, String combinedId, String nombre)
        {
			this.Id = id;
			this.CombinedId = combinedId;
			this.Nombre = nombre;
        }
    }
}
