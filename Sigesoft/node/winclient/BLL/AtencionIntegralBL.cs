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
    }
}
