using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
   public class GerenciaContrataBl
    {
        public List<GerenciaTipoPago> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.gerenciatipopago(startDate, endDate, -1)
                             select new GerenciaTipoPago
                             {
                                 IdCondicionPago = a.IdCondicionPago,
                                 CondicionPago = a.CondicionPago,
                                 IdFormaPago = a.IdFormaPago,
                                 FormaPago = a.FormaPago,
                                 FechaFactura = a.FechaFactura,
                                 Comprobante = a.Comprobante,
                                 Empresa = a.Empresa,
                                 Importe = a.Importe,
                                 ServiceId = a.ServiceId,
                                 Trabajador = a.Trabajador,
                                 FechaServicio = a.FechaServicio,
                                 Compania = a.Compania,
                                 Contratista = a.Contratista,
                                 CostoExamen = a.CostoExamen,
                                 TipoEso = a.TipoEso
                             }).ToList();

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaTreeContrata> ProcessDataTreeView(List<GerenciaTipoPago> data)
        {
            var list = Agrupador(data);
            list[0].Contratas = Contratas(data);
            return list;
        }

        public List<GerenciaTreeContrata> Agrupador(List<GerenciaTipoPago> data)
        {
            var list = new List<GerenciaTreeContrata>();
            var oGerenciaTreeContrata = new GerenciaTreeContrata();
            oGerenciaTreeContrata.Cantidad = data.Count;
            oGerenciaTreeContrata.Agrupador = "EXAMENES";
            oGerenciaTreeContrata.Total = data.Sum(s => s.CostoExamen);
            list.Add(oGerenciaTreeContrata);
            return list;
        }

        public List<Contrata_> Contratas(List<GerenciaTipoPago> data)
        {
            var listContratas = new List<Contrata_>();

            var contratas = data.GroupBy(g => g.Compania).Select(s => s.First());

            foreach (var contrata in contratas)
            {
                var oContrata = new Contrata_();

                oContrata.Cantidad = data.FindAll(p => p.Contratista == contrata.Compania).ToList().Count;
                oContrata.ContrataName = contrata.Contratista;
                oContrata.Total = data.FindAll(p => p.Contratista == contrata.Compania).ToList().Sum(s => s.CostoExamen);
                oContrata.Companias = Companias(oContrata, data);
                listContratas.Add(oContrata);
            }

            return listContratas;
        }

        public List<Compania_> Companias(Contrata_ oVContrata, List<GerenciaTipoPago> data)
        {
            var listCompanias = new List<Compania_>();

            var companias = data.FindAll(p => p.Contratista == oVContrata.ContrataName).ToList();
            var CompaniasAgrupadas = companias.GroupBy(g => g.Compania).Select(s => s.First());

            foreach (var compania in CompaniasAgrupadas)
            {
                var oContrata = new Compania_();
                oContrata.ContrataName = oVContrata.ContrataName;
                oContrata.Cantidad = companias.Count;
                oContrata.CompaniaName = compania.Compania;
                oContrata.Total = companias.Sum(s => s.CostoExamen);
                listCompanias.Add(oContrata);
            }

            return listCompanias;
        }
    }
}
