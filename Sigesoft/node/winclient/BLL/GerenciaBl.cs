using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaBl
    {
        public List<ConsultaGerencia> GetCurrentData()
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.service
                    join b in dbContext.servicecomponent on a.v_ServiceId equals b.v_ServiceId
                    join c in dbContext.protocol on a.v_ProtocolId equals c.v_ProtocolId
                    join d in dbContext.organization on c.v_EmployerOrganizationId equals d.v_OrganizationId
                    join e in dbContext.organization on c.v_WorkingOrganizationId equals e.v_OrganizationId
                    join f in dbContext.organization on c.v_CustomerOrganizationId equals f.v_OrganizationId
                    join g in dbContext.person on a.v_PersonId equals g.v_PersonId

                    join j1 in dbContext.systemparameter on new { a = c.i_EsoTypeId.Value, b = 118 }
                        equals new { a = j1.i_ParameterId, b = j1.i_GroupId } into j1Join 
                    from j1 in j1Join.DefaultIfEmpty()

                    join j2 in dbContext.systemparameter on new { a = c.i_MasterServiceTypeId.Value, b = 119 } equals new { a = j2.i_ParameterId, b = j2.i_GroupId } into j2Join
                    from j2 in j2Join.DefaultIfEmpty()

                    where a.i_IsDeleted == 0
                    select new ConsultaGerencia
                    {
                        ServiceId = a.v_ServiceId,
                        ServiceDate = a.d_ServiceDate,
                        ProtocolId = a.v_ProtocolId,
                        ProtocolName = c.v_Name,
                        WorkerId =  a.v_PersonId,
                        WorkerName = g.v_FirstName + " " + g.v_FirstLastName + " " + g.v_SecondLastName,
                        EsoTypeId = c.i_EsoTypeId.Value,
                        EsoTypeName = j1.v_Value1,
                        EmployerOrganizationId = c.v_EmployerOrganizationId ,
                        WorkingOrganizationId = c.v_WorkingOrganizationId ,
                        CustomerOrganizationId = c.v_CustomerOrganizationId ,
                        MasterServiceTypeId = c.i_MasterServiceTypeId.Value ,
                        MasterServiceTypeName = j2.v_Value1 ,
                        ComponentId = b.v_ComponentId ,
                        PriceComponent = b.r_Price ,
                        SaldoPaciente = b.d_SaldoPaciente.Value ,
                        SaldoAseguradora = b.d_SaldoAseguradora.Value ,
                        ConCargoA =  b.i_ConCargoA.Value
                    }).ToList();

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
