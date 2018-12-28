using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaMesBl
    {
        public List<GerenciaMes> Filter(DateTime anio)
        {
            try
            {
                var startDate = new DateTime(anio.Year, 1, 1);
                var endDate = startDate.AddYears(1);
                var dbContext = new SigesoftEntitiesModel();

                //var x = dbContext.gerenciacredito(startDate, endDate, -1, 2);
                var query = (from a in dbContext.gerenciacredito(startDate, endDate, -1, 2)
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
                                CondicionFactura = a.CondicionFactura
                            }).ToList();

                var list = new List<GerenciaMes>();

                string[] meses = {"ENERO","FEBRERO","MARZO","ABRIL","MAYO","JUNIO","JULIO","AGOSTO","SETIEMBRE","OCTUBRE","NOVIEMBRE","DICIEMBRE"};
                foreach (var mes in meses)
                {
                    var oGerenciaMes = new GerenciaMes();
                    var servicios = query.FindAll(p => p.FechaServicio.Value.ToString("MMMM").ToUpper() == mes);
                    oGerenciaMes.Cantidad = servicios.Count;
                    oGerenciaMes.CostoServicio = decimal.Parse(servicios.Sum(s => s.CostoExamen).ToString());
                    oGerenciaMes.Mes = mes;
                    list.Add(oGerenciaMes);
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
