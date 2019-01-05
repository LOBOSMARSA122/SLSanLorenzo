using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
   public class GerenciaCreditoBl
    {
        public List<GerenciaCredito> Filter(DateTime startDate, DateTime endDate)
        {
            var dbContext = new SigesoftEntitiesModel();
            var query = (from a in dbContext.gerenciacredito(startDate, endDate, -1,2)
                select new GerenciaCredito
                {
                    FechaServicio = a.FechaServicio,
                    ServiceId = a.ServiceId,
                    Trabajador = a.Trabajador,
                    Ocupacion = a.Ocupacion,
                    TipoEso = a.TipoEso,
                    CostoExamen = a.CostoExamen,
                    Compania = a.Compania,
                    Contratista = a.Contratista,
                    EmpresaFacturacion =a.EmpresaFacturacion,
                    Comprobante = a.Comprobante,
                    NroLiquidacion =a.NroLiquidacion,
                    FechaFactura = a.FechaFactura,
                    ImporteTotalFactura = a.ImporteTotalFactura,
                    d_NetoXCobrarFactura = a.d_NetoXCobrarFactura,
                    CondicionFactura = a.CondicionFactura,
                    xxx = a.xxx
                }).ToList();

            return query;
        }

        public List<GerenciaCredito> SinLiquidarXEmpresa(DateTime startDate, DateTime endDate, string empresa)
        {
            var dbContext = new SigesoftEntitiesModel();
            var query = (from a in dbContext.gerenciacreditoxempresa(startDate, endDate, -1, 2, empresa)
                         select new GerenciaCredito
                         {
                             FechaServicio = a.FechaServicio,
                             ServiceId = a.ServiceId,
                             Trabajador = a.Trabajador,
                             Ocupacion = a.Ocupacion,
                             TipoEso = a.TipoEso,
                             CostoExamen = a.CostoExamen,
                             Compania = a.Compania,
                             Contratista = a.Contratista,
                             EmpresaFacturacion = a.EmpresaFacturacion,
                             Comprobante = a.Comprobante,
                             NroLiquidacion = a.NroLiquidacion,
                             FechaFactura = a.FechaFactura,
                             ImporteTotalFactura = a.ImporteTotalFactura,
                             d_NetoXCobrarFactura = a.d_NetoXCobrarFactura,
                             CondicionFactura = a.CondicionFactura,
                             xxx = a.xxx
                         }).ToList();

            return query;
        }

        public List<GerenciaTreeCredito> ProcessDataTreeView(List<GerenciaCredito> data)
        {
            var list = Agrupador(data);
            list = AgregarTipos(list, data);
            list = AgregarEmpresas(list, data);
            return list;
        }

      

        public List<GerenciaTreeCredito> Agrupador(List<GerenciaCredito> data)
        {
            var list = new List<GerenciaTreeCredito>();
            var oGerenciaTreeCredito = new GerenciaTreeCredito();
            oGerenciaTreeCredito.Agrupador = "CREDITOS";

            var sumaComprobantes = data.FindAll(p => p.xxx == "FACTURADO").GroupBy(g => g.Comprobante).Select(s => s.First()).Sum(s => s.d_NetoXCobrarFactura);
            var servicios = decimal.Parse(data.FindAll(p => p.xxx != "FACTURADO").Sum(s => s.CostoExamen).ToString());

            var total = sumaComprobantes + servicios;
            if (total != null) oGerenciaTreeCredito.Total = Math.Round(total.Value,2, MidpointRounding.AwayFromZero);
            list.Add(oGerenciaTreeCredito);

            return list;
        }

        public List<GerenciaTreeCredito> AgregarTipos(List<GerenciaTreeCredito> list,List<GerenciaCredito> data)
        {
            var tipos = new List<TipoCredito>();

            foreach (var grupo in list)
            {
                var otipos = data.GroupBy(g => g.xxx).Select(s => s.First()).ToList();

                foreach (var tipo in otipos)
                {
                    var oTipoCredito = new TipoCredito();
                    oTipoCredito.Agrupador = grupo.Agrupador;
                    oTipoCredito.Tipo = tipo.xxx;

                    var comprobantes = data.FindAll(p => p.xxx == tipo.xxx).GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
                    if (oTipoCredito.Tipo == "FACTURADO")
                    {
                        var total = comprobantes.Sum(s => s.d_NetoXCobrarFactura);
                        if (total != null)
                            oTipoCredito.Total = Math.Round(total.Value, 2, MidpointRounding.AwayFromZero);
                    }

                    else
                    {
                        var total = decimal.Parse(data.FindAll(p => p.xxx == tipo.xxx).Sum(s => s.CostoExamen).ToString());
                        oTipoCredito.Total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
                    }

                    tipos.Add(oTipoCredito);
                }

                grupo.Tipos = tipos;
            }

            return list;
        }

        private List<GerenciaTreeCredito> AgregarEmpresas(List<GerenciaTreeCredito> list, List<GerenciaCredito> data)
        {
            var tipos = list[0].Tipos;
           
            foreach (var tipo in tipos)
            {
                var empresas = new List<EmpresaCredito>();
                var oempresas = data.FindAll(p => p.xxx == tipo.Tipo).GroupBy(g => g.EmpresaFacturacion).Select(s => s.First()).ToList();

                var comprobantes = oempresas.GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
                foreach (var empresa in oempresas)
                {
                    var oEmpresaCredito = new EmpresaCredito();
                    oEmpresaCredito.Tipo = tipo.Tipo;
                    oEmpresaCredito.Empresa = empresa.EmpresaFacturacion;

                    if (tipo.Tipo == "FACTURADO")
                        oEmpresaCredito.Total = comprobantes.FindAll(p => p.EmpresaFacturacion == empresa.EmpresaFacturacion).Sum(s => s.d_NetoXCobrarFactura);
                    else
                        oEmpresaCredito.Total = decimal.Parse(data.FindAll(p => p.xxx == tipo.Tipo && p.EmpresaFacturacion == empresa.EmpresaFacturacion).Sum(s => s.CostoExamen).ToString());
                   
                    empresas.Add(oEmpresaCredito);
                }
                tipo.Empresas = empresas;
            }

            return list;
        }
    }
}
