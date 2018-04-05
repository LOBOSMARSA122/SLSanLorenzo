using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Common
{
    [DataContract(IsReference = true)]
    public class OperationResult
    {
        [DataMember]
        public int Success { get; set; }  // 0 : Error, 1: Success
        [DataMember]
        public string ExceptionMessage { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string AdditionalInformation { get; set; }
        [DataMember]
        public string ReturnValue { get; set; }
        [DataMember]
        public string OriginalExceptionMessage { get; set; }
    }
}
