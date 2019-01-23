using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Common;
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
                                            && (c.v_CustomerOrganizationId != "N009-OO000000052" || c.v_EmployerOrganizationId != "N009-OO000000052" || c.v_WorkingOrganizationId != "N009-OO000000052")
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

        public List<GerenciaTipoPago> _Filter(DateTime startDate, DateTime endDate)
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
                                         && (c.v_CustomerOrganizationId == "N009-OO000000052" || c.v_EmployerOrganizationId == "N009-OO000000052" || c.v_WorkingOrganizationId == "N009-OO000000052")
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

                oCompania.Cantidad = data.FindAll(p => p.Compania == compania.Compania).ToList().Count;
                oCompania.CompaniaName = compania.Compania;
                oCompania.Total = data.FindAll(p => p.Compania == compania.Compania).ToList().Sum(s => s.CostoExamen);
                oCompania.Contratas = Contratas(oCompania, data);
                listCompanias.Add(oCompania);
            }

            return listCompanias;
        }

        public List<Contrata> Contratas(Compania oCompania, List<GerenciaTipoPago> data)
        {
            var listContratas = new List<Contrata>();

            var contratas = data.FindAll(p => p.Compania == oCompania.CompaniaName).ToList();
            var contratasAgrupadas = contratas.GroupBy(g => g.Contratista).Select(s => s.First());

            foreach (var contrata in contratasAgrupadas)
            {
                var oContrata = new Contrata();
                oContrata.CompaniaName = oCompania.CompaniaName;
                oContrata.Cantidad = contratas.FindAll(p => p.Contratista == contrata.Contratista).Count;
                oContrata.ContrataName = contrata.Contratista;
                oContrata.Total = contratas.FindAll(p => p.Contratista == contrata.Contratista).Sum(s => s.CostoExamen);
                listContratas.Add(oContrata);
            }

            return listContratas;
        }
    }
}
