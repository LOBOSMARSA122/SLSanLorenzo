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
        public string AddTicket(ref OperationResult objOperationResult, ticketDto objticketDto, List<string> ClientSession)
        {
            //mon.IsActive = true;
           
            //    int SecuentialId = -1;
            //    string newId = string.Empty;
            //    ticket objEntity1 = null;
            //try
            //   {
            //    SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            //    objEntity1 = ticketAssembler.ToEntity(objticketDto);

            //    objEntity1.d_InsertDate = DateTime.Now;
            //    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
            //    objEntity1.i_IsDeleted = 0;
            //    // Autogeneramos el Pk de la tabla
            //    SecuentialId = Utils.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 345);
            //    newId = Common.Utils.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "TK");
            //    objEntity1.v_TicketId = newId;

            //    dbContext.AddToticket(objEntity1);
            //    dbContext.SaveChanges();

            //    // Grabar Usuario
            //    if (pobjSystemUser != null)
            //    {
            //        OperationResult objOperationResult3 = new OperationResult();
            //        pobjSystemUser.v_PersonId = objEntity1.v_TicketId;
            //        new SecurityBL().AddSystemUSer(ref objOperationResult3, pobjSystemUser, ClientSession);
            //    }

            //    objOperationResult.Success = 1;
            //    // Llenar entidad Log
            //    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_TicketId, Success.Ok, null);
            //}
            //catch(Exception ex)
            //{
            //    objOperationResult.Success = 0;
            //    objOperationResult.ExceptionMessage = ex.Message;
            //    // Llenar entidad Log
            //    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_TicketId, Success.Failed, ex.Message);
            
            //}
            //return newId;

            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ticket objEntity = ticketAssembler.ToEntity(objticketDto);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 345), "TK"); ;
                objEntity.v_TicketId = NewId;

                dbContext.AddToticket(objEntity);
                dbContext.SaveChanges();

                objOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TK", "v_TicketId=" + NewId.ToString(), Success.Ok, null);
             
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TK", "v_TicketId=" + NewId.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
                
            }
            return NewId;
        }
    }
}
