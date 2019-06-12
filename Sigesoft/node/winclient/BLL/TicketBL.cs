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
                                  d_Cantidad = (int)A.d_Cantidad,
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
                                  d_Cantidad = (int)A.d_Cantidad,
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

        public serviceDto GetService(ref OperationResult objOperationResult, string serviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                serviceDto objDtoEntity = null;

                var objEntity = (from a in dbContext.service
                                 where a.v_ServiceId == serviceId
                    select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = serviceAssembler.ToDTO(objEntity);

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
        public List<TicketList> GetTicketById(string _serviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = from A in dbContext.hospitalizacion
                                 join B in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals B.v_HopitalizacionId
                                 join C in dbContext.service on B.v_HospitalizacionServiceId equals C.v_ServiceId
                                 join D in dbContext.ticket on C.v_ServiceId equals D.v_ServiceId
                                 join E in dbContext.ticketdetalle on D.v_TicketId equals E.v_TicketId
                                 where C.v_ServiceId  == _serviceId && D.i_IsDeleted == 0
                                 select new TicketList
                                 {
                                     v_ServiceId = D.v_ServiceId,
                                     v_TicketId = D.v_TicketId,
                                     d_Fecha = D.d_Fecha,
                                     i_conCargoA = D.i_ConCargoA,
                                     i_tipoCuenta =D.i_TipoCuentaId
                                 };
                List<TicketList> objData = objEntity.ToList();
                return objData;
            }
            catch (Exception ex)
            {
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
                                         where E.v_TicketId == _tickId
                                         && A.i_IsDeleted == 0 && F.i_IsDeleted == 0
                                         select new TicketDetalleList
                                         {
                                             v_TicketDetalleId = F.v_TicketDetalleId,
                                             v_TicketId = F.v_TicketId,
                                             d_Cantidad = F.d_Cantidad.Value,
                                             v_NombreProducto = F.v_Descripcion,
                                             v_CodInterno =  F.v_CodInterno,
                                             v_IdProductoDetalle = F.v_IdProductoDetalle,
                                             d_PrecioVenta = F.d_PrecioVenta.Value,
                                             i_RecordStatus = (int)RecordStatus.Grabado,
                                             i_RecordType = (int)RecordType.NoTemporal,
                                             d_SaldoAseguradora = F.d_SaldoAseguradora,
                                             d_SaldoPaciente = F.d_SaldoPaciente
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
                if (objEntitySource.d_InsertDate == null || objEntitySource.i_InsertUserId == null)
                {
                    objticketDto.d_InsertDate = DateTime.Now;
                    objticketDto.i_InsertUserId = Int32.Parse(ClientSession[2]);
                }
                else
                {
                    objticketDto.d_InsertDate = objEntitySource.d_InsertDate;
                    objticketDto.i_InsertUserId = objEntitySource.i_InsertUserId;
                }
                
                objticketDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objticketDto.i_IsDeleted = 0;
                ticket objStrongEntity = ticketAssembler.ToEntity(objticketDto);
                dbContext.ticket.ApplyCurrentValues(objStrongEntity);
                dbContext.SaveChanges();
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
                        updatedetalleticket.d_SaldoAseguradora = item.d_SaldoAseguradora;
                        updatedetalleticket.d_SaldoPaciente = item.d_SaldoPaciente;
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

        public void DeleteTicket( string ticketId, List<string> ClientSession)
        {
             OperationResult objOperationResult = new OperationResult();
             SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            try
            {

                var objEntitySource1 = (from a in dbContext.ticket
                                        where a.v_TicketId == ticketId
                                        select a).FirstOrDefault();

                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource1.i_IsDeleted = 1;
                dbContext.SaveChanges();


                var ticketDetail = GetTicketDetails(ref objOperationResult, ticketId);

                foreach (var detail in ticketDetail)
                {
                    DeleteTicketDetail(detail.v_TicketDetalleId, ClientSession);
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public void DeleteTicketDetail(string ticketDetailId, List<string> ClientSession)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            try
            {
                var objEntitySource1 = (from a in dbContext.ticketdetalle
                                        where a.v_TicketDetalleId == ticketDetailId
                                        select a).FirstOrDefault();

                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource1.i_IsDeleted = 1;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        public decimal ObtenerPrecioTarifario(string serviceId, string productoDetalleId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
          var precio =  dbContext.obtenerpreciotarifario(serviceId, productoDetalleId).ToList()[0].d_Precio;

          return precio.Value;

        }

        public List<PlanList> TienePlan(string protocolId, string unidadProductivaId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.plan
                                 where a.v_ProtocoloId == protocolId && a.v_IdUnidadProductiva == unidadProductivaId
                                 select new PlanList
                                 {
                                    i_PlanId = a.i_PlanId,
                                    v_OrganizationSeguroId =a.v_OrganizationSeguroId,
                                    v_ProtocoloId = a.v_ProtocoloId,
                                    v_IdUnidadProductiva = a.v_IdUnidadProductiva,
                                    i_EsDeducible = a.i_EsDeducible.Value,
                                    i_EsCoaseguro = a.i_EsCoaseguro.Value,
                                    d_Importe = a.d_Importe.Value,
                                    d_ImporteCo = a.d_ImporteCo.Value
                                 } ).ToList();


                return objEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PlanList> TienePlan_(string protocolId, int plan)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.plan
                    where a.v_ProtocoloId == protocolId && a.i_PlanId == plan
                    select new PlanList
                    {
                        i_PlanId = a.i_PlanId,
                        v_OrganizationSeguroId = a.v_OrganizationSeguroId,
                        v_ProtocoloId = a.v_ProtocoloId,
                        v_IdUnidadProductiva = a.v_IdUnidadProductiva,
                        i_EsDeducible = a.i_EsDeducible.Value,
                        i_EsCoaseguro = a.i_EsCoaseguro.Value,
                        d_Importe = a.d_Importe.Value,
                        d_ImporteCo = a.d_ImporteCo.Value
                    }).ToList();


                return objEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
