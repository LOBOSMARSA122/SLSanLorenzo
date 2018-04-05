using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Common
{
    public class KeyValueDTOForTree
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }

        public override string ToString()
        {
            return String.Format("Id={0} / ParentId={1} / Value1={2} / Value2={3}", Id, ParentId, Value1, Value2);
        }
    }
}
