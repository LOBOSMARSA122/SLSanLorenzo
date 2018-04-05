using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class FacturacionList
    {
        public string v_FacturacionId { get; set; }
        public DateTime? d_FechaRegistro { get; set; }
        public DateTime? d_FechaCobro { get; set; }
        public string v_NumeroFactura { get; set; }
        public int i_EstadoFacturacion { get; set; }
        public string v_EstadoFacturacion { get; set; }
        public string v_EmpresaCliente { get; set; }
        public string v_EmpresaSede { get; set; }
        public decimal? d_MontoTotal { get; set; }
        public string EmpresaClienteId { get; set; }


        public int?  i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public decimal? d_Deducible { get; set; }

    }
}
