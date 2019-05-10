using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.DAL;
using System.ComponentModel;

namespace Sigesoft.Node.Contasol.Integration.Contasol
{
    public class PlanBl
    {
        public void UpdatePlan(string pstrIdEmpAseguradora, string pstrProtocolId,
            List<planDto> listadoPlanes, List<planDto> listadoPlanesDelete)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var prot = dbContext.protocol.FirstOrDefault(p => p.v_ProtocolId.Equals(pstrProtocolId));
                    if (prot == null) throw new Exception("Protocolo no encontrado!");
                    prot.v_AseguradoraOrganizationId = pstrIdEmpAseguradora;
                    dbContext.protocol.ApplyCurrentValues(prot);

                    var agrupado = listadoPlanes.Where(p => !string.IsNullOrEmpty(p.v_IdUnidadProductiva) && p.Editado)
                        .GroupBy(g => g.TipoReg);

                    foreach (var items in agrupado)
                    {
                        switch (items.Key)
                        {
                            case TipoRegistro.Nueva:
                                var nuevos = items.Select(p => p.ToEntity()).ToList();
                                nuevos.ForEach(p => dbContext.plan.AddObject(p));
                                break;

                            case TipoRegistro.Edicion:
                                foreach (var item in items)
                                {
                                    var o = dbContext.plan.FirstOrDefault(p => p.i_PlanId == item.i_PlanId);
                                    if (o != null)
                                    {
                                        o = item.ToEntity();
                                        dbContext.plan.ApplyCurrentValues(o);
                                    }
                                }
                                break;
                        }
                    }

                    foreach (var item in listadoPlanesDelete)
                    {
                        if (item.i_PlanId == null) continue;
                        var o = dbContext.plan.FirstOrDefault(p => p.i_PlanId == item.i_PlanId);
                        if (o != null)
                        {
                            dbContext.plan.DeleteObject(o);
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BindingList<planDto> ObtenerPlanesPorProtocolo(ref OperationResult pobjOperationResult, string pstrProtocolId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var lineas = MedicamentoDao.ObtenerLineas().ToDictionary(k => k.IdLinea, o => o.Nombre);
                    var data = dbContext.plan.Where(p => p.v_ProtocoloId.Equals(pstrProtocolId)).ToDTOs();
                    data.ForEach(p =>
                    {
                        string linea;
                        if (lineas.TryGetValue(p.v_IdUnidadProductiva, out linea))
                        {
                            p.NombreLinea = linea;
                            p.Editado = false;
                            p.TipoReg = TipoRegistro.Edicion;
                            p.EsDeducible = p.i_EsDeducible == 1;
                            p.EsCoaseguro = p.i_EsCoaseguro == 1;
                        }
                    });

                    return new BindingList<planDto>(data);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetComentaryUpdateByPlanId(int planId)
        {
            using (var dbContext = new SigesoftEntitiesModel())
            {

                var comentario = (from pl in dbContext.plan
                    where pl.i_PlanId == planId
                    select pl.v_ComentaryUpdate).FirstOrDefault();

                return comentario;
            }
        }
    }
}
