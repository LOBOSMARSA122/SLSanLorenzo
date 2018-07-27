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
            string NewId0 = null;
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
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TK", "v_TicketId=" + NewId0.ToString(), Success.Ok, null);             
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TK", "v_TicketId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
            }
            return NewId0;
        }

        public bool IsExistsproductoInTicket(ref OperationResult pobjOperationResult, string[] pobjComponenttIdToComparerList, string pstrProductoidToFind)
        {
            bool IsExists = false;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener campos de una lista de componente pertenecientes a un mismo protocolo
                var query1 = (from A in dbContext.ticketdetalle
                              where (A.i_IsDeleted == 0) && (pobjComponenttIdToComparerList.Contains(A.v_IdProductoDetalle))
                              orderby A.v_IdProductoDetalle
                              select new TicketDetalleList
                              {
                                  v_IdProductoDetalle = A.v_IdProductoDetalle,
                                  v_TicketId = A.v_TicketId,
                                  d_Cantidad = A.d_Cantidad.Value,
                                  i_EsDespachado = A.i_EsDespachado.Value
                              }).ToList();

                // Obtener campos del componente concurrente
                var query2 = (from A in dbContext.ticketdetalle
                              where (A.i_IsDeleted == 0) && (A.v_IdProductoDetalle == pstrProductoidToFind)
                              orderby A.v_IdProductoDetalle
                              select new TicketDetalleList
                              {
                                  v_IdProductoDetalle = A.v_IdProductoDetalle,
                                  v_TicketId = A.v_TicketId,
                                  d_Cantidad = A.d_Cantidad.Value,
                                  i_EsDespachado = A.i_EsDespachado.Value
                              }).ToList();

                // Buscar los campos del componente concurrente obtenido en la lista 
                // de campos pertenecientes a un mismo protocolo            
                IsExists = query2.Exists(s => query1.Any(a => a.v_IdProductoDetalle == s.v_IdProductoDetalle));
                var IsExists_ = query2.Where(s => query1.Any(a => a.v_IdProductoDetalle == s.v_IdProductoDetalle)).ToList();
                return IsExists;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }

            return IsExists;
        }


        public ticketDto GetProtocol(ref OperationResult objOperationResult, string _tickId)
        {
            throw new NotImplementedException();
        }
    }
}
