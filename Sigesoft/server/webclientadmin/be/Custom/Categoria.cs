using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class Categoria
    {
        public string v_CategoryName { get; set; }

        public int? i_CategoryId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public int? i_IsRequiredId { get; set; }
        public bool Adicional { get; set; }
        public int? i_Adicional { get; set; }
        public string v_ProtocoloId { get; set; }
        public List<ComponentDetailList> Componentes { get; set; }
        public int Flag { get; set; }
        public float r_Price { get; set; }
    }
}
