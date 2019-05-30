using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class ServiceDetailBL
    {
        public List<ServiceDetailCustom> GetDetailsByserviceId(string serviceId)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();

                var query = (from com in cnx.component

                    join src in cnx.servicecomponent on com.v_ComponentId equals src.v_ComponentId
                    join ser in cnx.service on src.v_ServiceId equals ser.v_ServiceId

                    join sys in cnx.systemparameter on new {a = src.i_ServiceComponentStatusId.Value, b = 127}
                        equals new {a = sys.i_ParameterId, b = sys.i_GroupId} into sys_join
                    from sys in sys_join.DefaultIfEmpty()

                    join pro in cnx.protocol on new {a = ser.v_ProtocolId}
                        equals new {a = pro.v_ProtocolId} into pro_join
                    from pro in pro_join.DefaultIfEmpty()

                    join pl in cnx.plan on new {a = pro.v_ProtocolId}
                        equals new {a = pl.v_ProtocoloId} into pl_join
                    from pl in pl_join.DefaultIfEmpty()

                    join org in cnx.organization on new { a = pro.v_EmployerOrganizationId }
                    equals new { a = org.v_OrganizationId } into org_join
                    from org in org_join.DefaultIfEmpty()

                    join org2 in cnx.organization on new { a = pl.v_OrganizationSeguroId }
                    equals new { a = org2.v_OrganizationId } into org2_join
                    from org2 in org2_join.DefaultIfEmpty()
                   
                    where src.v_ServiceId == serviceId && src.r_Price != 0

                    select new ServiceDetailCustom
                    {
                        Examenes = com.v_Name,
                        Estado = sys.v_Value1,
                        Aseguradora = org2.v_Name,
                        Empresa = org.v_Name,
                        IGV = src.r_Price.Value,
                    }).ToList();
                List<ServiceDetailCustom> FinalQuery = new List<ServiceDetailCustom>();
                foreach (var item in query)
                {
                    item.Precio = float.Parse(Math.Round(item.IGV - (item.IGV * float.Parse("0.18")), 2).ToString());
                    item.IGV = float.Parse(Math.Round(item.IGV * float.Parse("0.18"), 2).ToString());
                    var find = FinalQuery.Find(x => x.Examenes == item.Examenes);
                    if (find == null)
                    {
                        FinalQuery.Add(item);
                    }
                    
                }


                return FinalQuery;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
