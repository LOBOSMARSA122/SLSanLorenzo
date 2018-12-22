using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaCampaniaBl
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

        public List<GerenciaTreeCapania> ProcessDataTreeView(List<GerenciaTipoPago> data)
        {
            var list = Agrupador(data);
            list[0].Companias = Companias(data);
            return list;
        }

        public List<GerenciaTreeCapania> Agrupador(List<GerenciaTipoPago> data)
        {
            var list = new List<GerenciaTreeCapania>();
            var oGerenciaTreeCapania = new GerenciaTreeCapania();
            oGerenciaTreeCapania.Cantidad = data.Count;
            oGerenciaTreeCapania.Agrupador = "EXAMENES";
            oGerenciaTreeCapania.Total = data.Sum(s => s.CostoExamen);
            list.Add(oGerenciaTreeCapania);
            return list;
        }

        public List<Compania> Companias(List<GerenciaTipoPago> data)
        {
            var listCompanias = new List<Compania>();
            
            var companias = data.GroupBy(g => g.Compania).Select(s => s.First());

            foreach (var compania in companias)
            {
                var oCompania = new Compania();
                
                oCompania.Cantidad = data.FindAll(p => p.Contratista == compania.Compania).ToList().Count;
                oCompania.CompaniaName = compania.Compania;
                oCompania.Total = data.FindAll(p => p.Contratista == compania.Compania).ToList().Sum(s => s.CostoExamen);
                oCompania.Contratas = Contratas(oCompania, data);
                listCompanias.Add(oCompania);
            }

            return listCompanias;
        }

        public List<Contrata> Contratas(Compania oCompania, List<GerenciaTipoPago> data)
        {
            var listContratas = new List<Contrata>();

            var contratas = data.FindAll(p => p.Contratista == oCompania.CompaniaName).ToList();
            var contratasAgrupadas = contratas.GroupBy(g => g.Contratista).Select(s => s.First());

            foreach (var contrata in contratasAgrupadas)
            {
                var oContrata = new Contrata();
                oContrata.CompaniaName = oCompania.CompaniaName;
                oContrata.Cantidad = contratas.Count;
                oContrata.ContrataName = contrata.Contratista;
                oContrata.Total = contratas.Sum(s => s.CostoExamen);
                listContratas.Add(oContrata);
            }

            return listContratas;
        }
    }
}
