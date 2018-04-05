using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.BLL
{
    public class LogBL
    {
        #region "Log"
        public List<LogList> GetLogsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.log
                            join B in dbContext.node on A.i_NodeLogId equals B.i_NodeId
                            join C in dbContext.organization on A.v_OrganizationId equals C.v_OrganizationId into C_join
                            from C in C_join.DefaultIfEmpty()

                            join D in dbContext.systemuser on A.i_SystemUserId equals D.i_SystemUserId into D_join
                            from D in D_join.DefaultIfEmpty()

                            join J1 in dbContext.systemparameter on new { a = 102, b = A.i_EventTypeId.Value }
                                                       equals new { a = J1.i_GroupId, b = J1.i_ParameterId }
                            join J2 in dbContext.systemparameter on new { a = 111, b = A.i_Success.Value }
                                                      equals new { a = J2.i_GroupId, b = J2.i_ParameterId }
                            select new LogList
                            {
                                v_LogId = A.v_LogId,
                                i_EventTypeId = A.i_EventTypeId.Value,
                                v_EventTypeName = J1.v_Value1,
                                i_NodeId = A.i_NodeLogId.Value,
                                v_NodeName = B.v_Description,
                                v_OrganizationId = A.v_OrganizationId,
                                v_OrganizationName = C.v_Name,
                                i_SystemUserId = A.i_SystemUserId.Value,
                                v_SystemUserName = D.v_UserName,
                                d_Date = A.d_Date.Value,
                                v_ProcessEntity = A.v_ProcessEntity,
                                v_ElementItem = A.v_ElementItem,
                                i_Success = A.i_Success.Value,
                                v_SuccessName = J2.v_Value1,
                                v_ErrorException = A.v_ErrorException
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<LogList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public int GetLogsCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.log select a;

                if (!string.IsNullOrEmpty(filterExpression))
                    query = query.Where(filterExpression);

                int intResult = query.Count();

                pobjOperationResult.Success = 1;
                return intResult;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return 0;
            }
        }

        public LogList GetLog(ref OperationResult pobjOperationResult, string pintLogId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.log
                             join B in dbContext.node on A.i_NodeLogId equals B.i_NodeId
                             join C in dbContext.organization on A.v_OrganizationId equals C.v_OrganizationId into C_join
                             from C in C_join.DefaultIfEmpty()
                             join D in dbContext.systemuser on A.i_SystemUserId equals D.i_SystemUserId into D_join
                             from D in D_join.DefaultIfEmpty()
                             join J1 in dbContext.systemparameter on new { a = 102, b = A.i_EventTypeId.Value }
                                                        equals new { a = J1.i_GroupId, b = J1.i_ParameterId }
                             join J2 in dbContext.systemparameter on new { a = 111, b = A.i_Success.Value }
                                                       equals new { a = J2.i_GroupId, b = J2.i_ParameterId }
                             where A.v_LogId == pintLogId
                             select new LogList
                             {
                                 v_LogId = A.v_LogId,
                                 i_EventTypeId = A.i_EventTypeId.Value,
                                 v_EventTypeName = J1.v_Value1,
                                 i_NodeId = A.i_NodeLogId.Value,
                                 v_NodeName = B.v_Description,
                                 v_OrganizationId = A.v_OrganizationId,
                                 v_OrganizationName = C.v_Name,
                                 i_SystemUserId = A.i_SystemUserId.Value,
                                 v_SystemUserName = D.v_UserName,
                                 d_Date = A.d_Date.Value,
                                 v_ProcessEntity = A.v_ProcessEntity,
                                 v_ElementItem = A.v_ElementItem,
                                 i_Success = A.i_Success.Value,
                                 v_SuccessName = J2.v_Value1,
                                 v_ErrorException = A.v_ErrorException
                             }).FirstOrDefault();


                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static void SaveLog(string pintNodeId, string pintOrganizationId, string pintSystemUserId, LogEventType pEnuEventType, string pstrProcess, string pstrItem, Success pEnuSuccess, string pstrErrorMessage)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            log objEntity = new log();

            objEntity.i_NodeLogId = int.Parse(pintNodeId);
            //pintOrganizationId == null ? (int?)null : int.Parse(pintOrganizationId);
            //objEntity.v_OrganizationId = pintOrganizationId;
            objEntity.i_SystemUserId = pintSystemUserId == null ? (int?)null : int.Parse(pintSystemUserId);
            objEntity.i_EventTypeId = (int)pEnuEventType;
            objEntity.v_ProcessEntity = pstrProcess;
            objEntity.v_ElementItem = pstrItem;
            objEntity.i_Success = (int)pEnuSuccess;
            objEntity.v_ErrorException = pstrErrorMessage;
            objEntity.d_Date = DateTime.Now;

            // Autogeneramos el Pk de la tabla
            int intNodeId = int.Parse(pintNodeId);            
            objEntity.v_LogId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 7), "ZZ"); ;
            dbContext.AddTolog(objEntity);
            dbContext.SaveChanges();
        }
        #endregion
    }
}
