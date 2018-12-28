using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Common;
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
                
                var query = (from a in dbContext.service
                             join b in dbContext.person on a.v_PersonId equals b.v_PersonId
                             join c in dbContext.protocol on a.v_ProtocolId equals c.v_ProtocolId
                             join d in dbContext.organization on c.v_CustomerOrganizationId equals d.v_OrganizationId into djoin
                             from d in djoin.DefaultIfEmpty()
                             join e in dbContext.organization on c.v_EmployerOrganizationId equals e.v_OrganizationId into ejoin
                             from e in ejoin.DefaultIfEmpty()
                             join f in dbContext.calendar on a.v_ServiceId equals f.v_ServiceId
                             join et in dbContext.systemparameter on new { a = c.i_EsoTypeId.Value, b = 118 }
                             equals new { a = et.i_ParameterId, b = et.i_GroupId } into etjoin
                             from et in etjoin.DefaultIfEmpty()
                             where a.i_IsDeleted == 0 && a.d_ServiceDate.Value >= startDate && a.d_ServiceDate.Value <= endDate && f.i_LineStatusId == (int)LineStatus.EnCircuito && a.v_ProtocolId != null
                             select new GerenciaTipoPago
                             {
                                 ServiceId = a.v_ServiceId,
                                 Trabajador = b.v_FirstLastName + " " + b.v_SecondLastName + " " + b.v_FirstName,
                                 FechaServicio = a.d_ServiceDate,
                                 Compania = d.v_Name,
                                 Contratista = e.v_Name,
                                 TipoEso = et.v_Value1
                             }).ToList();

                var result = (from a in query
                              select new GerenciaTipoPago
                              {
                                  ServiceId = a.ServiceId,
                                  Trabajador = a.Trabajador,
                                  FechaServicio = a.FechaServicio,
                                  Compania = a.Compania,
                                  Contratista = a.Contratista,
                                  TipoEso = a.TipoEso,
                                  CostoExamen = double.Parse(new ServiceBL().GetServiceCost(a.ServiceId))
                              }).ToList();

                return result;
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

            var contratas = data.GroupBy(g => g.Contratista).Select(s => s.First());

            foreach (var contrata in contratas)
            {
                var oContrata = new Contrata_();

                oContrata.Cantidad = data.FindAll(p => p.Contratista == contrata.Contratista).ToList().Count;
                oContrata.ContrataName = contrata.Contratista;
                oContrata.Total = data.FindAll(p => p.Contratista == contrata.Contratista).ToList().Sum(s => s.CostoExamen);
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
                oContrata.Cantidad = companias.FindAll(p => p.Compania == compania.Compania).Count;
                oContrata.CompaniaName = compania.Compania;
                oContrata.Total = companias.FindAll(p => p.Compania == compania.Compania).Sum(s => s.CostoExamen);
                listCompanias.Add(oContrata);
            }

            return listCompanias;
        }
    }
}
