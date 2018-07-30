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


        public ticketDto GetTicket(ref OperationResult objOperationResult, string _tickId)
        {
            try 
            { 
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ticketDto objDtoEntity= null;

                var objEntity = (from  a in dbContext.ticket
                                 where a.v_TicketId == _tickId
                                 select a).FirstOrDefault();

                if (objEntity != null)
		            objDtoEntity = ticketAssembler.ToDTO(objEntity);

                objOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<TicketDetalleList> GetTicketDetails(ref OperationResult _pobjOperationResult, string _tickId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from A in dbContext.hospitalizacion
                                         join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                                         join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                                         join E in dbContext.ticket on D.v_ServiceId equals E.v_ServiceId
                                         join F in dbContext.ticketdetalle on E.v_TicketId equals F.v_TicketId
                                         //join G in dbContext.productsformigration on F.v_IdProductoDetalle equals G.v_ProductId
                                         where E.v_TicketId == _tickId
                                         && A.i_IsDeleted == 0
                                         select new TicketDetalleList
                                         {
                                             v_TicketDetalleId = F.v_TicketDetalleId,
                                             v_TicketId = F.v_TicketId,
                                             d_Cantidad = F.d_Cantidad.Value,
                                             v_NombreProducto = F.v_Descripcion,
                                             v_CodInterno =  F.v_CodInterno,
                                             v_IdProductoDetalle = F.v_IdProductoDetalle,

                                             i_RecordStatus = (int)RecordStatus.Grabado,
                                             i_RecordType = (int)RecordType.NoTemporal
                                         }).ToList();
                _pobjOperationResult.Success = 1;
                return objEntity;
            }
            catch (Exception ex)
            {
                _pobjOperationResult.Success = 0;
                _pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void UpdateTicket(ref OperationResult _pobjOperationResult, ticketDto objticketDto, List<ticketdetalleDto> _ticketdetalleDTOAdd, List<ticketdetalleDto> _ticketdetalleDTOUpdate, List<ticketdetalleDto> _ticketdetalleDTODelete, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Actualizar ticket
                var objEntitySource = (from a in dbContext.ticket
                                       where a.v_TicketId == objticketDto.v_TicketId
                                       select a).FirstOrDefault();
                objticketDto.d_UpdateDate = DateTime.Now;
                objticketDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                var objStrongEntity = ticketAssembler.ToEntity(objticketDto);
                dbContext.ticket.ApplyCurrentValues(objStrongEntity);
                #endregion
                int intNodeId = int.Parse(ClientSession[0]);
                #region add detalle

                foreach (var item in _ticketdetalleDTOAdd)
                {

                    ticketdetalle objEntity1 = ticketdetalleAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 346), "KD");
                    objEntity1.v_TicketDetalleId = NewId1;
                    objEntity1.v_TicketId = objticketDto.v_TicketId;
                    dbContext.AddToticketdetalle(objEntity1);
                    dbContext.SaveChanges();

                }
                #endregion

                #region upd detalle
                if (_ticketdetalleDTOUpdate != null)
                {
                    foreach (var item in _ticketdetalleDTOUpdate)
                    {
                        var updatedetalleticket = (from a in dbContext.ticketdetalle
                                                where a.v_TicketDetalleId == item.v_TicketDetalleId
                                                select a).FirstOrDefault();

                        //objEntitySource1.v_ComponentId = item.v_ComponentId;

                        updatedetalleticket.d_Cantidad = item.d_Cantidad;
                        updatedetalleticket.i_EsDespachado = item.i_EsDespachado;

                        updatedetalleticket.i_IsDeleted = 0;

                        updatedetalleticket.d_UpdateDate = DateTime.Now;
                        updatedetalleticket.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        dbContext.SaveChanges();
                    }
                }

                #endregion
                #region del detalle

                if (_ticketdetalleDTODelete != null)
                {
                    foreach (var item in _ticketdetalleDTODelete)
                    {
                        var objEntitySource1 = (from a in dbContext.ticketdetalle
                                                where a.v_TicketDetalleId == item.v_TicketDetalleId
                                                select a).FirstOrDefault();

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;
                        dbContext.SaveChanges();

                    }
                }
                #endregion

                //dbContext.SaveChanges();
                _pobjOperationResult.Success = 1;

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TICKET / DETALLE","v_TicketId=" + objticketDto.v_TicketId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                _pobjOperationResult.Success = 0;
                _pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TICKET / DETALLE", "v_TicketId=" + objticketDto.v_TicketId.ToString(), Success.Failed, _pobjOperationResult.ExceptionMessage);
                return;
            }
        }
    }
}
