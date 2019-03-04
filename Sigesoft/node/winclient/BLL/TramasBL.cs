using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class TramasBL
    {
        public void AddTramas(ref OperationResult pobjOperationResult, tramasDto pobjDtoEntity, List<string> ClientSession)
        {
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                tramas objEntity = tramasAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 360), "TR"); ;
                objEntity.v_TramaId = NewId;

                dbContext.AddTotramas(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TRAMA", "v_TramaId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TRAMA", "v_TramaId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }
    }
}
