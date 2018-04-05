using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    public class RolCuotaDetalleList
    {
        public int i_Correlative { get; set; }

        public string v_RolCuotaDetalleId { get; set; }
        public string v_RolCuotaId { get; set; }
        public string v_IdProducto { get; set; }
        public string v_ProductoNombre { get; set; }
        public int? i_Cuota { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
