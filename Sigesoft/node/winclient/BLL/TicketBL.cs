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
    public class TicketBL
    {
        public string AddTicket(ref OperationResult objOperationResult, ticketDto objticketDto, List<ticketdetalleDto> pobjticketdetalleDto, List<string> ClientSession)
        {
            string NewId0 = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ticket objEntity = ticketAssembler.ToEntity(objticketDto);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 345), "TK"); ;
                objEntity.v_TicketId = NewId0;

                dbContext.AddToticket(objEntity);
                dbContext.SaveChanges();

                foreach (var item in pobjticketdetalleDto)
                {
                    
                    ticketdetalle objEntity1 = ticketdetalleAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 346), "KD");
                    objEntity1.v_TicketDetalleId = NewId1;
                    objEntity1.v_TicketId = NewId0;

                    dbContext.AddToticketdetalle(objEntity1);
                    dbContext.SaveChanges();
                }

                objOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TICKET", "v_TicketId=" + NewId0.ToString(), Success.Ok, null);
             
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TICKET", "v_TicketId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
                
            }
            return NewId0;
        }
    }
}
