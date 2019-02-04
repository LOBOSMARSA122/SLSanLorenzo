using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.BLL
{
   public class OperationsNatclarBl
    {
        public List<OperationsNatclarBe> Filter(DateTime? fi, DateTime? ff)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var query = (from a in dbContext.service
                        join b in dbContext.person on a.v_PersonId equals b.v_PersonId
                        where a.d_ServiceDate.Value >= fi && a.d_ServiceDate.Value <= ff
                        select new OperationsNatclarBe
                        {
                            v_ServiceId = a.v_ServiceId,
                            v_Pacient = b.v_FirstName + " " + b.v_FirstLastName + " " + b.v_SecondLastName,
                            d_ServiceDate = a.d_ServiceDate.Value
                        }).ToList();

            return query;
        }

        public void DatosPersonales(string serviceId)
        {

        }
    }
}
