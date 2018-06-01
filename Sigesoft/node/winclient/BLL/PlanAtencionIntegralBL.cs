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
 public   class PlanAtencionIntegralBL
    {
     public List<PlanAtencionIntegral> GetAtencionIntegral()
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.person
                            join B in dbContext.planintegral on A.v_PersonId equals B.v_PersonId
                            where A.v_PersonId == A.v_PersonId
                            select new PlanAtencionIntegral
                            {
                                v_PersonId = A.v_PersonId,
                                d_Fecha = B.d_Fecha,
                                v_Descripcion = B.v_Descripcion,
                                v_Lugar = B.v_Lugar,


                            };

                List<PlanAtencionIntegral> objData = query.ToList();
                return objData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
