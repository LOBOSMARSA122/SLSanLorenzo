using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class GerenciaTreeTipoExamen
    {
        public string Agrupador { get; set; }
        public decimal? Total { get; set; }
        public int Cantidad { get; set; }
        public List<Perfil> Perfiles { get; set; }
    }

    public class Perfil
    {
        public int Cantidad { get; set; }
        public string TipoEso { get; set; }
        public decimal? Total { get; set; }
        public List<EmpresaTipoEso> Empresas { get; set; }
    }
    public class EmpresaTipoEso
    {
        public string TipoEso { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal? Total { get; set; }
    }
}
