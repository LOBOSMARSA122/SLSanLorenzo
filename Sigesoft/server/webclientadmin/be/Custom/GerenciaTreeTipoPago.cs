using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class GerenciaTreeTipoPago
    {
        public int Cantidad { get; set; }
        public string Agrupador { get; set; }
        public decimal? Total { get; set; }
        public List<Tipo> Tipos { get; set; }
    }

    public class Tipo
    {
        public string Agrupador { get; set; }
        public int Cantidad { get; set; }
        public string TipoPago { get; set; }
        public decimal? Total { get; set; }
        public List<Empresa> Empresas { get; set; }
    }

    public class Empresa
    {
        public string TipoPago { get; set; }
        public int Cantidad { get; set; }
        public string EmpresaNombre { get; set; }
        public decimal Total { get; set; }
        public List<TipoEso> TipoEsos { get; set; }
    }

    public class TipoEso
    {
        public string TipoPago { get; set; }
        public string EmpresaNombre { get; set; }
        public int Cantidad { get; set; }
        public string Eso { get; set; }
        public decimal Total { get; set; }
    }
    
}
