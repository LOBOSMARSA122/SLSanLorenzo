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
    public class CalendarBL
    {
        public List<CalendarList> GetCalendarsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.calendar
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.systemparameter on new { a = A.i_LineStatusId.Value, b = 120 } equals new { a = C.i_ParameterId, b = C.i_GroupId }
                            join D in dbContext.service on A.v_ServiceId equals D.v_ServiceId
                            join E in dbContext.systemparameter on new { a = A.i_ServiceTypeId.Value, b = 119 } equals new { a = E.i_ParameterId, b = E.i_GroupId }
                            join F in dbContext.systemparameter on new { a = A.i_ServiceId.Value, b = 119 } equals new { a = F.i_ParameterId, b = F.i_GroupId }
                            join G in dbContext.systemparameter on new { a = A.i_NewContinuationId.Value, b = 121 } equals new { a = G.i_ParameterId, b = G.i_GroupId }
                            join H in dbContext.systemparameter on new { a = A.i_CalendarStatusId.Value, b = 122 } equals new { a = H.i_ParameterId, b = H.i_GroupId }
                            join I in dbContext.systemparameter on new { a = A.i_IsVipId.Value, b = 111 } equals new { a = I.i_ParameterId, b = I.i_GroupId }

                            join J in dbContext.protocol on new { a = D.v_ProtocolId }
                                         equals new { a = J.v_ProtocolId } into J_join
                            from J in J_join.DefaultIfEmpty()

                            join K in dbContext.systemparameter on new { a = J.i_EsoTypeId.Value, b = 118 }
                                         equals new { a = K.i_ParameterId, b = K.i_GroupId } into K_join
                            from K in K_join.DefaultIfEmpty()

                            // Empresa / Sede Cliente **************
                            join oc in dbContext.organization on new { a = J.v_CustomerOrganizationId }
                                    equals new { a = oc.v_OrganizationId } into oc_join
                            from oc in oc_join.DefaultIfEmpty()

                            join lc in dbContext.location on new { a = J.v_CustomerOrganizationId, b = J.v_CustomerLocationId }
                                  equals new { a = lc.v_OrganizationId, b = lc.v_LocationId } into lc_join
                            from lc in lc_join.DefaultIfEmpty()

                            // Empresa / Sede Trabajo  ********************************************************
                            join ow in dbContext.organization on new { a = J.v_WorkingOrganizationId }
                                    equals new { a = ow.v_OrganizationId } into ow_join
                            from ow in ow_join.DefaultIfEmpty()

                            join lw in dbContext.location on new { a = J.v_WorkingOrganizationId, b = J.v_WorkingLocationId }
                                 equals new { a = lw.v_OrganizationId, b = lw.v_LocationId } into lw_join
                            from lw in lw_join.DefaultIfEmpty()

                            //************************************************************************************

                            join N in dbContext.organization on new { a = D.v_OrganizationId }
                                    equals new { a = N.v_OrganizationId } into N_join
                            from N in N_join.DefaultIfEmpty()

                            join O in dbContext.location on new { a = N.v_OrganizationId, b = D.v_LocationId }
                                    equals new { a = O.v_OrganizationId, b = O.v_LocationId } into O_join
                            from O in O_join.DefaultIfEmpty()

                            join J3 in dbContext.systemparameter on new { a = D.i_ServiceStatusId.Value, b = 125 }
                                            equals new { a = J3.i_ParameterId, b = J3.i_GroupId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                            join J4 in dbContext.systemparameter on new { a = D.i_AptitudeStatusId.Value, b = 124 }
                                            equals new { a = J4.i_ParameterId, b = J4.i_GroupId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            join J5 in dbContext.datahierarchy on new { a = B.i_DocTypeId.Value, b = 106 }
                                           equals new { a = J5.i_ItemId, b = J5.i_GroupId } into J5_join
                            from J5 in J5_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                      equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.i_CalendarStatusId == (int)CalendarStatus.Atendido
                            select new CalendarList
                            {
                                v_CalendarId = A.v_CalendarId,
                                d_DateTimeCalendar = A.d_DateTimeCalendar.Value,
                                v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                v_NumberDocument = B.v_DocNumber,
                                v_LineStatusName = C.v_Value1,
                                v_ServiceId = A.v_ServiceId,
                                v_ProtocolId = A.v_ProtocolId,
                                v_ProtocolName = J.v_Name,
                                v_ServiceStatusName = J3.v_Value1,
                                v_AptitudeStatusName = J4.v_Value1,
                                v_ServiceTypeName = E.v_Value1,
                                v_ServiceName = F.v_Value1,
                                v_NewContinuationName = G.v_Value1,

                                v_PersonId = A.v_PersonId,
                                v_CalendarStatusName = H.v_Value1,
                                i_ServiceStatusId = D.i_ServiceStatusId.Value,
                                v_IsVipName = I.v_Value1,

                                i_ServiceId = A.i_ServiceId.Value,
                                i_ServiceTypeId = A.i_ServiceTypeId.Value,
                                i_CalendarStatusId = A.i_CalendarStatusId.Value,
                                i_MasterServiceId = A.i_ServiceId.Value,

                                i_NewContinuationId = A.i_NewContinuationId.Value,
                                i_LineStatusId = A.i_LineStatusId.Value,
                                i_IsVipId = A.i_IsVipId.Value,

                                i_EsoTypeId = J.i_EsoTypeId.Value,
                                v_EsoTypeName = K.v_Value1,

                                v_OrganizationLocationProtocol = oc.v_Name + " / " + lc.v_Name,
                                v_OrganizationLocationService = N.v_Name + " / " + O.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,

                                v_CustomerOrganizationId = oc.v_OrganizationId,
                                v_CustomerLocationId = lc.v_LocationId,

                                v_DocTypeName = J5.v_Value1,
                                v_DocNumber = B.v_DocNumber,
                                i_DocTypeId = B.i_DocTypeId.Value,
                                d_EntryTimeCM = A.d_EntryTimeCM.Value,

                                v_WorkingOrganizationName = ow.v_Name,
                                d_SalidaCM = A.d_SalidaCM
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_DateTimeCalendar >= @0 && d_DateTimeCalendar <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                //if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                //{
                //    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                //    query = query.Skip(intStartRowIndex);
                //}
                //if (pintResultsPerPage.HasValue)
                //{
                //    query = query.Take(pintResultsPerPage.Value);
                //}

                List<CalendarList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<CalendarList> GetCalendarsPagedAndFiltered_(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {


            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.calendar
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.systemparameter on new { a = A.i_LineStatusId.Value, b = 120 } equals new { a = C.i_ParameterId, b = C.i_GroupId }
                            join D in dbContext.service on A.v_ServiceId equals D.v_ServiceId
                            join E in dbContext.systemparameter on new { a = A.i_ServiceTypeId.Value, b = 119 } equals new { a = E.i_ParameterId, b = E.i_GroupId }
                            join F in dbContext.systemparameter on new { a = A.i_ServiceId.Value, b = 119 } equals new { a = F.i_ParameterId, b = F.i_GroupId }
                            join G in dbContext.systemparameter on new { a = A.i_NewContinuationId.Value, b = 121 } equals new { a = G.i_ParameterId, b = G.i_GroupId }
                            join H in dbContext.systemparameter on new { a = A.i_CalendarStatusId.Value, b = 122 } equals new { a = H.i_ParameterId, b = H.i_GroupId }
                            join I in dbContext.systemparameter on new { a = A.i_IsVipId.Value, b = 111 } equals new { a = I.i_ParameterId, b = I.i_GroupId }

                            join J in dbContext.protocol on new { a = D.v_ProtocolId }
                                         equals new { a = J.v_ProtocolId } into J_join
                            from J in J_join.DefaultIfEmpty()

                            join K in dbContext.systemparameter on new { a = J.i_EsoTypeId.Value, b = 118 }
                                         equals new { a = K.i_ParameterId, b = K.i_GroupId } into K_join
                            from K in K_join.DefaultIfEmpty()

                            // Empresa / Sede Cliente **************
                            join oc in dbContext.organization on new { a = J.v_CustomerOrganizationId }
                                    equals new { a = oc.v_OrganizationId } into oc_join
                            from oc in oc_join.DefaultIfEmpty()

                            join lc in dbContext.location on new { a = J.v_CustomerOrganizationId, b = J.v_CustomerLocationId }
                                  equals new { a = lc.v_OrganizationId, b = lc.v_LocationId } into lc_join
                            from lc in lc_join.DefaultIfEmpty()

                            //// Empresa / Sede Empleadora *******************************************
                            //join oe in dbContext.organization on new { a = J.v_EmployerOrganizationId }
                            //      equals new { a = oe.v_OrganizationId } into oe_join
                            //from oe in oe_join.DefaultIfEmpty()

                            //join le in dbContext.location on new { a = J.v_EmployerOrganizationId, b = J.v_EmployerLocationId }
                            //       equals new { a = le.v_OrganizationId, b = le.v_LocationId } into le_join
                            //from le in le_join.DefaultIfEmpty()

                            // Empresa / Sede Trabajo  ********************************************************
                            join ow in dbContext.organization on new { a = J.v_WorkingOrganizationId }
                                    equals new { a = ow.v_OrganizationId } into ow_join
                            from ow in ow_join.DefaultIfEmpty()

                            join lw in dbContext.location on new { a = J.v_WorkingOrganizationId, b = J.v_WorkingLocationId }
                                 equals new { a = lw.v_OrganizationId, b = lw.v_LocationId } into lw_join
                            from lw in lw_join.DefaultIfEmpty()

                            //************************************************************************************

                            join N in dbContext.organization on new { a = D.v_OrganizationId }
                                    equals new { a = N.v_OrganizationId } into N_join
                            from N in N_join.DefaultIfEmpty()

                            join O in dbContext.location on new { a = N.v_OrganizationId, b = D.v_LocationId }
                                    equals new { a = O.v_OrganizationId, b = O.v_LocationId } into O_join
                            from O in O_join.DefaultIfEmpty()

                            join J3 in dbContext.systemparameter on new { a = D.i_ServiceStatusId.Value, b = 125 }
                                            equals new { a = J3.i_ParameterId, b = J3.i_GroupId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                            join J4 in dbContext.systemparameter on new { a = D.i_AptitudeStatusId.Value, b = 124 }
                                            equals new { a = J4.i_ParameterId, b = J4.i_GroupId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            join J5 in dbContext.datahierarchy on new { a = B.i_DocTypeId.Value, b = 106 }
                                           equals new { a = J5.i_ItemId, b = J5.i_GroupId } into J5_join
                            from J5 in J5_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                      equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            //let a = new ServiceBL().GetRestrictionByServiceId(A.v_ServiceId)
                            select new CalendarList
                            {
                                //b_Seleccionar = false,
                                v_CalendarId = A.v_CalendarId,
                                d_DateTimeCalendar = A.d_DateTimeCalendar.Value,
                                v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                v_NumberDocument = B.v_DocNumber,
                                v_LineStatusName = C.v_Value1,
                                v_ServiceId = A.v_ServiceId,
                                v_ProtocolId = A.v_ProtocolId,
                                v_ProtocolName = J.v_Name,
                                v_ServiceStatusName = J3.v_Value1,
                                v_AptitudeStatusName = J4.v_Value1,
                                v_ServiceTypeName = E.v_Value1,
                                v_ServiceName = F.v_Value1,
                                v_NewContinuationName = G.v_Value1,

                                v_PersonId = A.v_PersonId,
                                v_CalendarStatusName = H.v_Value1,
                                i_ServiceStatusId = D.i_ServiceStatusId.Value,
                                v_IsVipName = I.v_Value1,

                                i_ServiceId = A.i_ServiceId.Value,
                                i_ServiceTypeId = A.i_ServiceTypeId.Value,
                                i_CalendarStatusId = A.i_CalendarStatusId.Value,
                                i_MasterServiceId = A.i_ServiceId.Value,

                                i_NewContinuationId = A.i_NewContinuationId.Value,
                                i_LineStatusId = A.i_LineStatusId.Value,
                                i_IsVipId = A.i_IsVipId.Value,

                                i_EsoTypeId = J.i_EsoTypeId.Value,
                                v_EsoTypeName = K.v_Value1,

                                v_OrganizationLocationProtocol = oc.v_Name + " / " + lc.v_Name,
                                v_OrganizationLocationService = N.v_Name + " / " + O.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,

                                v_CustomerOrganizationId = oc.v_OrganizationId,
                                v_CustomerLocationId = lc.v_LocationId,

                                v_DocTypeName = J5.v_Value1,
                                v_DocNumber = B.v_DocNumber,
                                i_DocTypeId = B.i_DocTypeId.Value,
                                d_EntryTimeCM = A.d_EntryTimeCM.Value,

                                v_WorkingOrganizationName = ow.v_Name,
                                //Observaciones = D.v_ObsStatusService,
                                d_SalidaCM = A.d_SalidaCM
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_DateTimeCalendar >= @0 && d_DateTimeCalendar <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                //if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                //{
                //    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                //    query = query.Skip(intStartRowIndex);
                //}
                //if (pintResultsPerPage.HasValue)
                //{
                //    query = query.Take(pintResultsPerPage.Value);
                //}

                List<CalendarList> objData = query.ToList();

                var q = (from a in objData
                         select new CalendarList
                         {
                             v_CalendarId = a.v_CalendarId,
                             d_DateTimeCalendar = a.d_DateTimeCalendar,
                             v_Pacient = a.v_Pacient,
                             v_NumberDocument = a.v_NumberDocument,
                             v_LineStatusName = a.v_LineStatusName,
                             v_ServiceId = a.v_ServiceId,
                             v_ProtocolId = a.v_ProtocolId,
                             v_ProtocolName = a.v_ProtocolName,
                             v_ServiceStatusName = a.v_ServiceStatusName,
                             v_AptitudeStatusName = a.v_AptitudeStatusName,
                             v_ServiceTypeName = a.v_ServiceTypeName,
                             v_ServiceName = a.v_ServiceName,
                             v_NewContinuationName = a.v_NewContinuationName,

                             v_PersonId = a.v_PersonId,
                             v_CalendarStatusName = a.v_CalendarStatusName,
                             i_ServiceStatusId = a.i_ServiceStatusId,
                             v_IsVipName = a.v_IsVipName,

                             i_ServiceId = a.i_ServiceId,
                             i_ServiceTypeId = a.i_ServiceTypeId,
                             i_CalendarStatusId = a.i_CalendarStatusId,
                             i_MasterServiceId = a.i_MasterServiceId,

                             i_NewContinuationId = a.i_NewContinuationId,
                             i_LineStatusId = a.i_LineStatusId,
                             i_IsVipId = a.i_IsVipId,

                             i_EsoTypeId = a.i_EsoTypeId,
                             v_EsoTypeName = a.v_EsoTypeName,

                             v_OrganizationLocationProtocol = a.v_OrganizationLocationProtocol,
                             v_OrganizationLocationService = a.v_OrganizationLocationService,
                             v_CreationUser = a.v_CreationUser,
                             v_UpdateUser = a.v_UpdateUser,
                             d_CreationDate = a.d_CreationDate,
                             d_UpdateDate = a.d_UpdateDate,
                             i_IsDeleted = a.i_IsDeleted,

                             v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                             v_CustomerLocationId = a.v_CustomerLocationId,

                             v_DocTypeName = a.v_DocTypeName,
                             v_DocNumber = a.v_DocNumber,
                             i_DocTypeId = a.i_DocTypeId,
                             d_EntryTimeCM = a.d_EntryTimeCM,

                             v_WorkingOrganizationName = a.v_WorkingOrganizationName,
                             //Restricciones = new ServiceBL().GetRestrictionByServiceId(a.v_ServiceId),
                             //Observaciones = a.Observaciones,
                             d_SalidaCM = a.d_SalidaCM
                         }).ToList();


                pobjOperationResult.Success = 1;
                return q;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public int ValidateShedule(ref OperationResult pobjOperationResult, string pstrProtocolId, string pstrPacientId, DateTime pdate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.calendar
                            where A.v_ProtocolId == pstrProtocolId && A.v_PersonId == pstrPacientId && A.i_CalendarStatusId == 1
                            select A;

                var query1 = query.AsEnumerable()

                .Where(j => j.d_DateTimeCalendar.Value.Date == Convert.ToDateTime(pdate.Date).Date);

                int intResult = query1.Count();

                pobjOperationResult.Success = 1;
                return intResult;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return 0;
            }
        }

        public List<CalendarList> GetHeaderRoadMap(string calendarId)
        {


            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var serviceBL = new ServiceBL();
                var logoEmpresa = serviceBL.GetLogoMedicalCenter();
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                //var empresaPropietariaName = serviceBL.GetNameMedicalCenterName();
                //var empresaPropietariaAddress = serviceBL.GetNameMedicalCenteraddress();

                var query = from A in dbContext.calendar
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join D in dbContext.service on A.v_ServiceId equals D.v_ServiceId
                            join E in dbContext.systemparameter on new { a = A.i_ServiceTypeId.Value, b = 119 } equals new { a = E.i_ParameterId, b = E.i_GroupId }
                            join F in dbContext.systemparameter on new { a = A.i_ServiceId.Value, b = 119 } equals new { a = F.i_ParameterId, b = F.i_GroupId }

                            join J in dbContext.protocol on new { a = D.v_ProtocolId }
                                         equals new { a = J.v_ProtocolId } into J_join
                            from J in J_join.DefaultIfEmpty()

                            join K in dbContext.systemparameter on new { a = J.i_EsoTypeId.Value, b = 118 }
                                         equals new { a = K.i_ParameterId, b = K.i_GroupId } into K_join
                            from K in K_join.DefaultIfEmpty()

                            join L in dbContext.organization on new { a = J.v_CustomerOrganizationId }
                                    equals new { a = L.v_OrganizationId } into L_join
                            from L in L_join.DefaultIfEmpty()

                            join M in dbContext.location on new { a = L.v_OrganizationId, b = J.v_EmployerLocationId }
                                    equals new { a = M.v_OrganizationId, b = M.v_LocationId } into M_join
                            from M in M_join.DefaultIfEmpty()

                            join N in dbContext.organization on new { a = D.v_OrganizationId }
                                    equals new { a = N.v_OrganizationId } into N_join
                            from N in N_join.DefaultIfEmpty()

                            join O in dbContext.location on new { a = N.v_OrganizationId, b = D.v_LocationId }
                                    equals new { a = O.v_OrganizationId, b = O.v_LocationId } into O_join
                            from O in O_join.DefaultIfEmpty()

                            join P in dbContext.organization on new { a = J.v_WorkingOrganizationId }
                                    equals new { a = P.v_OrganizationId } into P_join
                            from P in P_join.DefaultIfEmpty()

                            join J1 in dbContext.systemparameter on new { a = B.i_SexTypeId.Value, b = 100 }
                                               equals new { a = J1.i_ParameterId, b = J1.i_GroupId }  // GENERO

                            where (A.i_IsDeleted == 0) &&
                                  (A.v_CalendarId == calendarId)

                            select new CalendarList
                            {
                                v_CalendarId = A.v_CalendarId,
                                v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + ", " + B.v_FirstName,
                                v_ProtocolName = J.v_Name,
                                v_ServiceTypeName = E.v_Value1,
                                v_ServiceName = F.v_Value1,
                                v_PersonId = A.v_PersonId,
                                v_NumberDocument = B.v_DocNumber,
                                d_Birthdate = B.d_Birthdate.Value,
                                v_EsoTypeName = K.v_Value1,
                                v_OrganizationName = L.v_Name + " / " + M.v_Name,
                                v_OrganizationLocationService = N.v_Name + " / " + O.v_Name,
                                v_OrganizationIntermediaryName = P.v_Name,
                                v_DocNumber = B.v_DocNumber,
                                b_PersonImage = B.b_PersonImage,
                                d_ServiceDate = D.d_ServiceDate.Value,
                                Puesto = B.v_CurrentOccupation,
                                v_TelephoneNumber = B.v_TelephoneNumber,
                                ServicioId = D.v_ServiceId,
                                Genero = J1.v_Value1
                            };

                var finalQuery = (from a in query.ToList()
                                  select new CalendarList
                                  {
                                      v_CalendarId = a.v_CalendarId,
                                      v_Pacient = a.v_Pacient,
                                      v_ProtocolName = a.v_ProtocolName,
                                      v_ServiceTypeName = a.v_ServiceTypeName,
                                      v_ServiceName = a.v_ServiceName,
                                      v_PersonId = a.v_PersonId,
                                      v_NumberDocument = a.v_NumberDocument,
                                      d_Birthdate = a.d_Birthdate,
                                      i_Edad = new ServiceBL().GetAge(a.d_Birthdate),
                                      v_EsoTypeName = a.v_EsoTypeName,
                                      v_OrganizationName = a.v_OrganizationName,
                                      v_OrganizationLocationService = a.v_OrganizationLocationService,
                                      v_OrganizationIntermediaryName = a.v_OrganizationIntermediaryName,
                                      v_DocNumber = a.v_DocNumber,
                                      b_PersonImage = a.b_PersonImage,
                                      b_Logo = logoEmpresa,
                                      EmpresaPropietaria = MedicalCenter.v_Name,
                                      EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                                      EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                                      d_ServiceDate = a.d_ServiceDate,
                                      //Puesto = a.Puesto,
                                      v_TelephoneNumber = a.v_TelephoneNumber,
                                      ServicioId = a.ServicioId,
                                      Genero = a.Genero
                                  }).ToList();

                return finalQuery;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public calendarDto GetCalendar(ref OperationResult pobjOperationResult, string pstrCalendarId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                calendarDto objDtoEntity = null;

                var objEntity = (from a in dbContext.calendar
                                 where a.v_CalendarId == pstrCalendarId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = calendarAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public void UpdateCalendar(ref OperationResult pobjOperationResult, calendarDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.calendar
                                       where a.v_CalendarId == pobjDtoEntity.v_CalendarId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                // Ingreso al centro medico
                if (objEntitySource.d_EntryTimeCM == null)
                {
                    pobjDtoEntity.d_EntryTimeCM = pobjDtoEntity.d_CircuitStartDate;
                }

                calendar objEntity = calendarAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.calendar.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AGENDA", "v_CalendarId=" + objEntity.v_CalendarId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AGENDA", "v_CalendarId=" + pobjDtoEntity.v_CalendarId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public string AddShedule(ref OperationResult pobjOperationResult, calendarDto pobjDtoEntity, List<string> ClientSession, string pstrProtocolId, string pstrPacientId, int pstrMasterServiceId, string pstrNuevoContinuacion)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            string ServiceId = String.Empty;
            string ComponentId;
            try
            {

                OperationResult objOperationResult = new OperationResult();
                CalendarBL _objCalendarBL = new CalendarBL();
                calendarDto objCalendarDto = new calendarDto();

                componentDto objComponentDto = new componentDto();
                MedicalExamBL objComponentBL = new MedicalExamBL();

                ServiceBL _ObjServiceBL = new ServiceBL();
                serviceDto objServiceDto = new serviceDto();
                servicecomponentDto objServiceComponentDto = new servicecomponentDto();

                ProtocolBL _objProtocolBL = new ProtocolBL();
                List<Sigesoft.Node.WinClient.BE.ProtocolComponentList> objProtocolComponentList = new List<Sigesoft.Node.WinClient.BE.ProtocolComponentList>();

                PacientBL objPacientBL = new PacientBL();
                personDto objPersonDto = new personDto();

                //Validar que un paciente no se pueda agendar el mismo día con el mismo protocolo
                string pstrFilterExpression = "v_PersonId==" + "\"" + pstrPacientId + "\"" + " && " + "v_ProtocolId==" + "\"" + pobjDtoEntity.v_ProtocolId + "\"" + " && " + "d_DateTimeCalendar==" + "\"" + pobjDtoEntity.d_DateTimeCalendar.ToString() + "\"";
                int Contador = _objCalendarBL.ValidateShedule(ref objOperationResult, pobjDtoEntity.v_ProtocolId, pstrPacientId, pobjDtoEntity.d_DateTimeCalendar.Value);
                //if (Contador > 0)
                //{
                //    pobjOperationResult.ErrorMessage = "No puede agendar a un mismo paciente con un mismo protocolo y en un mismo día.";
                //    return null;
                //}

                //if (pstrNuevoContinuacion == "Nuevo")
                //{
                //    int Contador1 = _objCalendarBL.ValidateSheduleCurrent(ref objOperationResult, pstrPacientId, pobjDtoEntity.d_DateTimeCalendar.Value);
                //    if (Contador1 > 0)
                //    {
                //        pobjOperationResult.ErrorMessage = "El paciente no se puede agendar porque tiene un servicio por iniciar, iniciado o incompleto.";
                //        return null;
                //    }
                //}


                //Crear Instancia del Servicio y de Componente del Servicio
                objServiceDto.v_ProtocolId = pobjDtoEntity.v_ProtocolId;
                objServiceDto.v_PersonId = pstrPacientId;
                objServiceDto.i_MasterServiceId = pstrMasterServiceId;
                //Se setea el estado del servicio en "Por iniciar"
                objServiceDto.i_ServiceStatusId = (int)Common.ServiceStatus.PorIniciar;
                //Se setea el estado de la aptitud en "Sin Aptitud" en caso sea un Eso
                objServiceDto.i_AptitudeStatusId = (int)Common.AptitudeStatus.SinAptitud;
                objServiceDto.d_ServiceDate = null;
                objServiceDto.d_GlobalExpirationDate = null;
                objServiceDto.d_ObsExpirationDate = null;
                objServiceDto.i_FlagAgentId = 1;
                objServiceDto.v_Motive = string.Empty;
                objServiceDto.i_IsFac = 0;

                if (pstrNuevoContinuacion == "Nuevo")
                {
                    // Es un nuevo Servicio
                    ServiceId = _ObjServiceBL.AddService(ref objOperationResult, objServiceDto, ClientSession);

                    objProtocolComponentList = _objProtocolBL.GetProtocolComponents(ref objOperationResult, pobjDtoEntity.v_ProtocolId);

                    for (int i = 0; i <= objProtocolComponentList.Count - 1; i++)
                    {
                        ComponentId = objProtocolComponentList[i].v_ComponentId;
                        objComponentDto = objComponentBL.GetMedicalExam(ref objOperationResult, ComponentId);

                        objServiceComponentDto.v_ServiceId = ServiceId;
                        objServiceComponentDto.i_ExternalInternalId = (int)Common.ComponenteProcedencia.Interno;
                        objServiceComponentDto.i_ServiceComponentTypeId = objComponentDto.i_ComponentTypeId;
                        objServiceComponentDto.i_IsVisibleId = objComponentDto.i_UIIsVisibleId;
                        objServiceComponentDto.i_IsInheritedId = (int)Common.SiNo.NO;
                        objServiceComponentDto.d_StartDate = null;
                        objServiceComponentDto.d_EndDate = null;
                        objServiceComponentDto.i_index = objComponentDto.i_UIIndex;
                        objServiceComponentDto.r_Price = objProtocolComponentList[i].r_Price;
                        objServiceComponentDto.v_ComponentId = objProtocolComponentList[i].v_ComponentId;
                        objServiceComponentDto.i_IsInvoicedId = (int)Common.SiNo.NO;
                        objServiceComponentDto.i_ServiceComponentStatusId = (int)Common.ServiceStatus.PorIniciar;
                        objServiceComponentDto.i_QueueStatusId = (int)Common.QueueStatusId.LIBRE;
                        //objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        objServiceComponentDto.i_Iscalling = (int)Common.Flag_Call.NoseLlamo;
                        objServiceComponentDto.i_Iscalling_1 = (int)Common.Flag_Call.NoseLlamo;





                        ////////Condicionales///////////////////////////////////////

                        int Conditional = (int)objProtocolComponentList[i].i_IsConditionalId;

                        if (Conditional == (int)Common.SiNo.SI)
                        {
                            objPersonDto = objPacientBL.GetPerson(ref objOperationResult, pstrPacientId);
                            DateTime nacimiento = (DateTime)objPersonDto.d_Birthdate;

                            //Datos del paciente
                            int PacientAge = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
                            int PacientGender = (int)objPersonDto.i_SexTypeId;

                            //Datos del protocolo
                            int AnalyzeAge = (int)objProtocolComponentList[i].i_Age;
                            int AnalyzeGender = (int)objProtocolComponentList[i].i_GenderId;
                            Operator2Values Operator = (Operator2Values)objProtocolComponentList[i].i_OperatorId;

                            if ((int)Operator == -1)
                            {
                                //si la condicional del operador queda en --Seleccionar--
                                if (AnalyzeGender == (int)GenderConditional.AMBOS)
                                {
                                    objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                                }
                                else if (PacientGender == AnalyzeGender)
                                {
                                    objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                                }
                                else
                                {
                                    objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                                }
                            }
                            else
                            {
                                if (AnalyzeGender == (int)GenderConditional.MASCULINO)
                                {
                                    objServiceComponentDto.i_IsRequiredId = switchOperator2Values(PacientAge, AnalyzeAge, Operator, PacientGender, AnalyzeGender);
                                }
                                else if (AnalyzeGender == (int)GenderConditional.FEMENINO)
                                {
                                    objServiceComponentDto.i_IsRequiredId = switchOperator2Values(PacientAge, AnalyzeAge, Operator, PacientGender, AnalyzeGender);
                                }
                                else if (AnalyzeGender == (int)GenderConditional.AMBOS)
                                {
                                    objServiceComponentDto.i_IsRequiredId = switchOperator2Values(PacientAge, AnalyzeAge, Operator, PacientGender, AnalyzeGender);
                                }
                            }
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                            if (objProtocolComponentList[i].i_isAdditional != null)
                            {
                                var Adicional = (int)objProtocolComponentList[i].i_isAdditional;
                                if (Adicional == 1)
                                {
                                    objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                                }

                            }

                        }

                        //////////////////////////////////////////////////////////////////
                        objServiceComponentDto.i_IsManuallyAddedId = (int)Common.SiNo.NO;
                        _ObjServiceBL.AddServiceComponent(ref objOperationResult, objServiceComponentDto, ClientSession);
                    }
                }
                else if (pstrNuevoContinuacion == "Continuacion")
                {
                    // Es una Continuación
                    //objServiceDto = _ObjServiceBL.GetService(ref objOperationResult, pobjDtoEntity.v_ServiceId);
                    //_ObjServiceBL.UpdateService(ref objOperationResult, objServiceDto, ClientSession);
                    ServiceId = pobjDtoEntity.v_ServiceId;
                }


                // Grabar Agenda *********************************

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                calendar objEntity = calendarAssembler.ToEntity(pobjDtoEntity);

                objEntity.v_ServiceId = ServiceId;
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 22), "CA");
                objEntity.v_CalendarId = NewId;

                dbContext.AddTocalendar(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "AGENDA", "v_CalendarId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "AGENDA", "v_CalendarId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public int switchOperator2Values(int PacientAge, int AnalyzeAge, Operator2Values Operator, int PacientGender, int AnalyzeGender)
        {
            servicecomponentDto objServiceComponentDto = new servicecomponentDto();
            switch (Operator)
            {
                case Operator2Values.X_esIgualque_A:
                    if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    {
                        if (PacientAge == AnalyzeAge)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    else
                    {
                        if (PacientAge == AnalyzeAge && PacientGender == AnalyzeGender)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }

                    break;
                case Operator2Values.X_noesIgualque_A:
                    if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    {
                        if (PacientAge != AnalyzeAge)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    else
                    {
                        if (PacientAge != AnalyzeAge && PacientGender == AnalyzeGender)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }

                    break;
                case Operator2Values.X_esMenorque_A:

                    if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    {
                        if (PacientAge < AnalyzeAge)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    else
                    {
                        if (PacientAge < AnalyzeAge && PacientGender == AnalyzeGender)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }

                    break;
                case Operator2Values.X_esMenorIgualque_A:

                    if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    {
                        if (PacientAge <= AnalyzeAge)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    else
                    {
                        if (PacientAge <= AnalyzeAge && PacientGender == AnalyzeGender)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }

                    break;
                case Operator2Values.X_esMayorque_A:
                    if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    {
                        if (PacientAge > AnalyzeAge)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    else
                    {
                        if (PacientAge > AnalyzeAge && PacientGender == AnalyzeGender)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    break;
                case Operator2Values.X_esMayorIgualque_A:
                    if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    {
                        if (PacientAge >= AnalyzeAge)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }
                    else
                    {
                        if (PacientAge >= AnalyzeAge && PacientGender == AnalyzeGender)
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                        }
                        else
                        {
                            objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.NO;
                        }
                    }

                    break;
                default:

                    //if (AnalyzeGender == (int)GenderConditional.MASCULINO)
                    //{
                    //    objServiceComponentDto.i_IsRequiredId = switchOperator2Values(PacientAge, AnalyzeAge, Operator);
                    //}
                    //else if (AnalyzeGender == (int)GenderConditional.FEMENINO)
                    //{
                    //    objServiceComponentDto.i_IsRequiredId = switchOperator2Values(PacientAge, AnalyzeAge, Operator);
                    //}
                    //else if (AnalyzeGender == (int)GenderConditional.AMBOS)
                    //{
                    //    objServiceComponentDto.i_IsRequiredId = switchOperator2Values(PacientAge, AnalyzeAge, Operator);
                    //}        
                    //objServiceComponentDto.i_IsRequiredId = (int)Common.SiNo.SI;
                    break;
            }

            return objServiceComponentDto.i_IsRequiredId.Value;
        }

        public void CircuitStart(ref OperationResult pobjOperationResult, string pstrCalendarId, DateTime CircuitStartDate, List<string> ClientSession)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objCalendarDto = new calendarDto();
            ServiceBL objServiceBL = new ServiceBL();
            serviceDto objServiceDto = new serviceDto();
            OperationResult objOperationResult = new OperationResult();
            try
            {
                objCalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, pstrCalendarId);
                objCalendarDto.v_CalendarId = pstrCalendarId;
                objCalendarDto.i_LineStatusId = (int)Common.LineStatus.EnCircuito;
                objCalendarDto.i_CalendarStatusId = (int)Common.CalendarStatus.Atendido;
                objCalendarDto.d_CircuitStartDate = DateTime.Now;
                objCalendarBL.UpdateCalendar(ref objOperationResult, objCalendarDto, ClientSession);

                objServiceDto = objServiceBL.GetService(ref objOperationResult, objCalendarDto.v_ServiceId);
                objServiceDto.d_ServiceDate = objCalendarDto.d_CircuitStartDate;
                objServiceBL.UpdateService(ref objOperationResult, objServiceDto, ClientSession);


            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }
     

        public void Reschedule(ref OperationResult pobjOperationResult, List<string> ClientSession, string pstrCalendarId, DateTime pdatDateTimeStar, int pintddlVipId, string pstrProtocolId, string pstrPacientId, int pstrMasterServiceId)
        {
            OperationResult objOperationResult = new OperationResult();
            CalendarBL objCalendarBL = new CalendarBL();
            ServiceBL objServiceBL = new ServiceBL();
            calendarDto objCalendarDto = new calendarDto();
            serviceDto objServiceDto = new serviceDto();
            try
            {

                //objCalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, pstrCalendarId);

                //Primero cancelo la cita      
                objCalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, pstrCalendarId);
                objCalendarDto.i_CalendarStatusId = (int)Common.CalendarStatus.Cancelado;
                objCalendarBL.UpdateCalendar(ref objOperationResult, objCalendarDto, ClientSession);

                //Creo una nueva cita 
                objCalendarDto.d_DateTimeCalendar = pdatDateTimeStar;
                objCalendarDto.i_CalendarStatusId = (int)Common.CalendarStatus.Agendado;
                objCalendarDto.i_IsVipId = pintddlVipId;


                //AddCalendar(ref objOperationResult, objCalendarDto, ClientSession);
                string Result = AddShedule(ref pobjOperationResult, objCalendarDto, ClientSession, pstrProtocolId, pstrPacientId, pstrMasterServiceId, "Nuevo");

                if (Result == null)
                {
                    pobjOperationResult.Success = 0;

                }
                else
                {
                    objServiceDto = objServiceBL.GetService(ref objOperationResult, objCalendarDto.v_ServiceId);

                    objServiceBL.UpdateService(ref objOperationResult, objServiceDto, ClientSession);

                    pobjOperationResult.Success = 1;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AGENDA", "v_CalendarId=" + objCalendarDto.v_CalendarId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }
      
    }
}
