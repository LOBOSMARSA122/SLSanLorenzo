using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.BLL
{
   public class AtencionIntegralBL
    {
       public List<ProblemasList> GetAtencionIntegral(string personId)
        {
                try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.problema
                            join B in dbContext.systemparameter on new { a = A.i_EsControlado.Value, b = 111 } equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                            from B in B_join.DefaultIfEmpty()
                            where A.v_PersonId == personId && A.i_IsDeleted == 0
                            select new ProblemasList
                            {
                                v_PersonId = A.v_PersonId,
                                d_Fecha = A.d_Fecha,
                                i_Tipo = A.i_Tipo,
                                v_Descripcion = A.v_Descripcion,
                                i_EsControlado = A.i_EsControlado,
                                v_EsControlao = B.v_Value1,
                                v_Observacion = A.v_Observacion
                            };

                List<ProblemasList> objData = query.ToList();
                 return objData;
            }
            catch (Exception ex)
            {
                return null;    
            }
        }

       public List<TipoAtencionList> GetPlanIntegral(string personId)
       {
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               var tiposAtencion = (from A in dbContext.systemparameter
                                    where A.i_GroupId == 281
                                    select new
                                    {
                                        Id = A.i_ParameterId,
                                        Value = A.v_Value1
                                    }).ToList();
               
               var planes = (from A in dbContext.planintegral
                           join B in dbContext.systemparameter on new { a = A.i_TipoId.Value, b = 281 } 
                                                                equals new { a = B.i_ParameterId, b = B.i_GroupId }
                           where A.v_PersonId == personId && A.i_IsDeleted == 0
                           select new 
                           {
                               v_PlanIntegral = A.v_PlanIntegral,
                               v_PersonId = A.v_PersonId,
                               i_TipoId = A.i_TipoId,
                               v_Descripcion = A.v_Descripcion,
                               d_Fecha = A.d_Fecha,
                               v_Lugar = A.v_Lugar,
                               v_Tipo = B.v_Value1                   
                           }).ToList();


               TipoAtencionList tipoAtencionList = null;
               List<TipoAtencionList> listaAtenciones = new List<TipoAtencionList>();
               foreach (var atencion in tiposAtencion)
               {
                   tipoAtencionList = new TipoAtencionList();
                   tipoAtencionList.Id = atencion.Id;
                   tipoAtencionList.Value = atencion.Value;
                   var detalles = planes.FindAll(p => p.i_TipoId == atencion.Id);
                   List<PlanIntegralList> List = new List<PlanIntegralList>();
                   PlanIntegralList planAtencionIntegral;
                   if (detalles.Count == 0)
                   {
                       planAtencionIntegral = new PlanIntegralList();
                       planAtencionIntegral.v_Descripcion = "";
                       planAtencionIntegral.v_Fecha = "";
                       planAtencionIntegral.v_Lugar = "";
                       List.Add(planAtencionIntegral);
                   }
                   else
                   {
                       foreach (var detalle in detalles)
                       {
                           planAtencionIntegral = new PlanIntegralList();
                           planAtencionIntegral.v_Descripcion = detalle.v_Descripcion;
                           planAtencionIntegral.v_Fecha = detalle.d_Fecha.Value.ToShortDateString();
                           planAtencionIntegral.v_Lugar = detalle.v_Lugar;
                           List.Add(planAtencionIntegral);
                       }
                   }
                  
                   tipoAtencionList.List = List;
                   listaAtenciones.Add(tipoAtencionList);
               }

               List<TipoAtencionList> objData = listaAtenciones;
               return objData;
           }
           catch (Exception ex)
           {
               return null;
           }
       }


    }
}
