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
                         join c in dbContext.envionatclar on a.v_ServiceId equals c.v_ServiceId into c_join
                         from c in c_join.DefaultIfEmpty()
                        where a.d_ServiceDate.Value >= fi && a.d_ServiceDate.Value <= ff
                        select new OperationsNatclarBe
                        {
                            b_select = false,
                            v_ServiceId = a.v_ServiceId,
                            v_Pacient = b.v_FirstName + " " + b.v_FirstLastName + " " + b.v_SecondLastName,
                            d_ServiceDate = a.d_ServiceDate.Value,
                            v_Paquete = c.v_Paquete,
                            i_EstadoId = c.i_EstadoId,
                            v_PersonId = b.v_PersonId
                        }).ToList();

            return query;
        }

        public void GrabarEnvio(envionatclarDto pobjDtoEntity, List<string> ClientSession)
        {
            var NewId = Common.Utils.GetNewId(int.Parse(ClientSession[0]), Utils.GetNextSecuentialId(int.Parse(ClientSession[0]), 355), "EN");
            string serviceId = pobjDtoEntity.v_ServiceId;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                envionatclar objEntity = envionatclarAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                objEntity.v_EnvioNatclarId = NewId;
                objEntity.v_ServiceId = serviceId;

                dbContext.AddToenvionatclar(objEntity);
                dbContext.SaveChanges();
                
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
