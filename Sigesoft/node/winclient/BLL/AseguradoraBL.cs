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
    public class AseguradoraBL
    {
        public List<LiquidacionAseguradora> GetLiquidacionAseguradoraPagedAndFiltered(ref OperationResult pobjOperationResult,string pstrFilterExpression,DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.service
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                            join D in dbContext.organization on C.v_EmployerOrganizationId equals D.v_OrganizationId
                    where A.i_IsDeleted == 0 && D.i_OrganizationTypeId == 4
                    select new LiquidacionAseguradora
                    {
                        ServicioId = A.v_ServiceId,
                        FechaServicio = A.d_ServiceDate.Value,
                        Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                        PacientDocument = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_DocNumber,
                        EmpresaId = C.v_EmployerOrganizationId
                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        query = query.Where(pstrFilterExpression);
                    }
                    if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                    {
                        query = query.Where("FechaServicio >= @0 && FechaServicio <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                    }

                    pobjOperationResult.Success = 1;
                    return query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
