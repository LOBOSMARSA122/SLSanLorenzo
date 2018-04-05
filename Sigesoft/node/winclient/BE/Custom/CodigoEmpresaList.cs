using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class CodigoEmpresaList
    {
        public string v_CodigoEmpresaId { get; set; }
        public string v_CIIUId { get; set; }
        public string v_Name { get; set; }
        public string v_CIIUDescription1 { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

    }
}
