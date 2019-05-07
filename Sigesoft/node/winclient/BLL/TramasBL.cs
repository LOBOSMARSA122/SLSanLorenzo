using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.Objects;
using ConnectionState = System.Data.ConnectionState;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.BLL
{
    public class TramasBL
    {
        public List<TramasList> GettramasPageAndFilteredAmbulatorio(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.tramas
                            join B in dbContext.systemparameter on A.i_GrupoEtario equals B.i_ParameterId
                            join C in dbContext.systemuser on A.i_InsertUserId equals C.i_SystemUserId
                            join D in dbContext.person on C.v_PersonId equals D.v_PersonId

                            join E in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = E.i_SystemUserId } into E_join
                            from E in E_join.DefaultIfEmpty()

                            join F in dbContext.person on new { v_PersonId = E.v_PersonId }
                                equals new { v_PersonId = F.v_PersonId } into F_join
                            from F in F_join.DefaultIfEmpty()

                            
                            where A.i_IsDeleted == 0 && B.i_GroupId == 347 && A.v_TipoRegistro == "Ambulatorio"

                            select new TramasList
                            {
                                v_TramaId = A.v_TramaId,
                                v_TipoRegistro = A.v_TipoRegistro,
                                d_FechaIngreso = A.d_FechaIngreso,
                                i_Genero = A.i_Genero,
                                GrupoEtario = B.v_Value1,
                                v_DiseasesName = A.v_DiseasesName,
                                v_CIE10Id = A.v_CIE10Id,
                                d_InsertDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                User_Crea = D.v_FirstName + " " + D.v_FirstLastName + " " +  D.v_SecondLastName,
                                User_Act = E.i_SystemUserId == null ? "---" : F.v_FirstName + " " + F.v_FirstLastName + " " + F.v_SecondLastName
                           };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
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

                List<TramasList> objData = query.ToList();
                var tramasdetalle = (from a in objData
                    select new TramasList
                    {
                        v_TramaId = a.v_TramaId,
                        v_TipoRegistro = a.v_TipoRegistro,
                        d_FechaIngreso = a.d_FechaIngreso,
                        Genero = a.i_Genero == 1 ? "MASC" : "FEM",
                        GrupoEtario = a.GrupoEtario,
                        v_DiseasesName = a.v_DiseasesName,
                        v_CIE10Id = a.v_CIE10Id,
                        User_Crea  = a.User_Crea,
                        User_Act = a.User_Act,
                        d_InsertDate = a.d_InsertDate,
                        d_UpdateDate = a.d_UpdateDate
                    }).ToList();
                pobjOperationResult.Success = 1;
                return tramasdetalle;

            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(e);
                return null;
            }
        }

        public List<TramasList> GettramasPageAndFilteredEmergencia(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.tramas
                            join B in dbContext.systemparameter on A.i_GrupoEtario equals B.i_ParameterId
                            join C in dbContext.systemuser on A.i_InsertUserId equals C.i_SystemUserId
                            join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                            join E in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = E.i_SystemUserId } into E_join
                            from E in E_join.DefaultIfEmpty()

                            join F in dbContext.person on new { v_PersonId = E.v_PersonId }
                                equals new { v_PersonId = F.v_PersonId } into F_join
                            from F in F_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && B.i_GroupId == 347 && A.v_TipoRegistro == "Emergencia"

                            select new TramasList
                            {
                                v_TramaId = A.v_TramaId,
                                v_TipoRegistro = A.v_TipoRegistro,
                                d_FechaIngreso = A.d_FechaIngreso,
                                i_Genero = A.i_Genero,
                                GrupoEtario = B.v_Value1,
                                v_DiseasesName = A.v_DiseasesName,
                                v_CIE10Id = A.v_CIE10Id,
                                d_InsertDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                User_Crea = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                User_Act = E.i_SystemUserId == null ? "---" : F.v_FirstName + " " + F.v_FirstLastName + " " + F.v_SecondLastName
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
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

                List<TramasList> objData = query.ToList();
                var tramasdetalle = (from a in objData
                                     select new TramasList
                                     {
                                         v_TramaId = a.v_TramaId,
                                         v_TipoRegistro = a.v_TipoRegistro,
                                         d_FechaIngreso = a.d_FechaIngreso,
                                         Genero = a.i_Genero == 1 ? "MASC" : "FEM",
                                         GrupoEtario = a.GrupoEtario,
                                         v_DiseasesName = a.v_DiseasesName,
                                         v_CIE10Id = a.v_CIE10Id,
                                         User_Crea = a.User_Crea,
                                         User_Act = a.User_Act,
                                         d_InsertDate = a.d_InsertDate,
                                         d_UpdateDate = a.d_UpdateDate

                                     }).ToList();
                pobjOperationResult.Success = 1;
                return tramasdetalle;

            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(e);
                return null;
                //Console.WriteLine(e);
                //throw;
            }
        }
        
        public List<TramasList> GettramasPageAndFilteredHospitalizacion(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.tramas

                            join B in dbContext.systemparameter on new { a = A.i_GrupoEtario.Value, b = 347 }
                                equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join // GRUPO ETARIO
                            from B in B_join.DefaultIfEmpty()

                            join C in dbContext.systemuser on A.i_InsertUserId equals C.i_SystemUserId
                            join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                            join E in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = E.i_SystemUserId } into E_join
                            from E in E_join.DefaultIfEmpty()

                            join F in dbContext.person on new { v_PersonId = E.v_PersonId }
                                equals new { v_PersonId = F.v_PersonId } into F_join
                            from F in F_join.DefaultIfEmpty()

                            join G in dbContext.systemparameter on new { a = A.i_UPS.Value, b = 349 }
                                equals new { a = G.i_ParameterId, b = G.i_GroupId } into G_join // UPS DETAIL
                            from G in G_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.v_TipoRegistro == "Hospitalización"

                            select new TramasList
                            {
                                v_TramaId = A.v_TramaId,
                                v_TipoRegistro = A.v_TipoRegistro,
                                d_FechaIngreso = A.d_FechaIngreso.Value,
                                i_Genero = A.i_Genero,
                                GrupoEtario = B.v_Value1,
                                v_DiseasesName = A.v_DiseasesName,
                                v_CIE10Id = A.v_CIE10Id,
                                d_InsertDate = A.d_InsertDate.Value,
                                d_UpdateDate = A.d_UpdateDate.Value,
                                User_Crea = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                User_Act = E.i_SystemUserId == null ? "---" : F.v_FirstName + " " + F.v_FirstLastName + " " + F.v_SecondLastName,
                                d_FechaAlta = A.d_FechaAlta.Value,
                                i_UPS = A.i_UPS.Value,
                                i_Procedimiento = A.i_Procedimiento,
                                ups_Detail = G.v_Value1
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
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

                List<TramasList> objData = query.ToList();
                var tramasdetalle = (from a in objData
                                     select new TramasList
                                     {
                                         v_TramaId = a.v_TramaId,
                                         v_TipoRegistro = a.v_TipoRegistro,
                                         d_FechaIngreso = a.d_FechaIngreso,
                                         Genero = a.i_Genero == 1 ? "MASC" : "FEM",
                                         GrupoEtario = a.GrupoEtario,
                                         v_DiseasesName = a.v_DiseasesName,
                                         v_CIE10Id = a.v_CIE10Id,
                                         User_Crea = a.User_Crea,
                                         User_Act = a.User_Act,
                                         d_InsertDate = a.d_InsertDate,
                                         d_UpdateDate = a.d_UpdateDate,

                                         d_FechaAlta = a.d_FechaAlta,
                                         i_UPS = a.i_UPS,
                                         ups_Detail = a.ups_Detail,
                                         i_Procedimiento = a.i_Procedimiento,
                                         dead_prod = a.i_Procedimiento == 0 ? "NO" : "SI",
                                     }).ToList();
                pobjOperationResult.Success = 1;
                return tramasdetalle;

            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(e);
                return null;
                //Console.WriteLine(e);
                //throw;
            }
        }

        public List<TramasList> GettramasPageAndFilteredProcedimientosCirugia(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.tramas

                            join C in dbContext.systemuser on A.i_InsertUserId equals C.i_SystemUserId
                            join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                            join E in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = E.i_SystemUserId } into E_join
                            from E in E_join.DefaultIfEmpty()

                            join F in dbContext.person on new { v_PersonId = E.v_PersonId }
                                equals new { v_PersonId = F.v_PersonId } into F_join
                            from F in F_join.DefaultIfEmpty()

                            join G in dbContext.systemparameter on new { a = A.i_UPS.Value, b = 349 }
                                equals new { a = G.i_ParameterId, b = G.i_GroupId } into G_join // UPS DETAIL
                            from G in G_join.DefaultIfEmpty()

                            join H in dbContext.systemparameter on new { a = A.i_Procedimiento.Value, b = 348 }
                                equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join // PROCEDIMIENTO DETAIL
                            from H in H_join.DefaultIfEmpty()

                            join I in dbContext.systemparameter on new { a = A.i_Programacion.Value, b = 353 }
                                equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join // PROGRAMACION DETAIL
                            from I in I_join.DefaultIfEmpty()

                            join J in dbContext.systemparameter on new { a = A.i_TipoCirugia.Value, b = 354 }
                                equals new { a = J.i_ParameterId, b = J.i_GroupId } into J_join // TIPO CIRUJIA DETAIL
                            from J in J_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.v_TipoRegistro == "Procedimientos / Cirugía"

                            select new TramasList
                            {
                                v_TramaId = A.v_TramaId,
                                v_TipoRegistro = A.v_TipoRegistro,
                                d_FechaIngreso = A.d_FechaIngreso.Value,
                                i_UPS = A.i_UPS.Value,
                                ups_Detail = G.v_Value1,
                                i_Procedimiento = A.i_Procedimiento,
                                procedimiento_Detail = H.v_Value1,
                                programacion_Detail = I.v_Value1,
                                tipoCirugia_Detail = J.v_Value1,
                                i_HorasProg = A.i_HorasProg,
                                i_HorasEfect = A.i_HorasEfect,
                                i_HorasActo = A.i_HorasActo,
                                d_InsertDate = A.d_InsertDate.Value,
                                d_UpdateDate = A.d_UpdateDate.Value,
                                User_Crea = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                User_Act = E.i_SystemUserId == null ? "---" : F.v_FirstName + " " + F.v_FirstLastName + " " + F.v_SecondLastName,
                            };
                #region
                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
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
                #endregion
                List<TramasList> objData = query.ToList();
                var tramasdetalle = (from a in objData
                                     select new TramasList
                                     {
                                         v_TramaId = a.v_TramaId,
                                         v_TipoRegistro = a.v_TipoRegistro,
                                         d_FechaIngreso = a.d_FechaIngreso,
                                         i_UPS = a.i_UPS,
                                         ups_Detail = a.ups_Detail,
                                         i_Procedimiento = a.i_Procedimiento,
                                         procedimiento_Detail = a.procedimiento_Detail,
                                         programacion_Detail = a.programacion_Detail,
                                         tipoCirugia_Detail = a.tipoCirugia_Detail,
                                         i_HorasProg = a.i_HorasProg,
                                         i_HorasEfect = a.i_HorasEfect,
                                         i_HorasActo = a.i_HorasActo,
                                         User_Crea = a.User_Crea,
                                         User_Act = a.User_Act,
                                         d_InsertDate = a.d_InsertDate,
                                         d_UpdateDate = a.d_UpdateDate,
                                     }).ToList();
                pobjOperationResult.Success = 1;
                return tramasdetalle;

            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(e);
                return null;
                //Console.WriteLine(e);
                //throw;
            }
        }

        public List<TramasList> GettramasPageAndFilteredPartos(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.tramas

                            join C in dbContext.systemuser on A.i_InsertUserId equals C.i_SystemUserId
                            join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                            join E in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                equals new { i_UpdateUserId = E.i_SystemUserId } into E_join
                            from E in E_join.DefaultIfEmpty()

                            join F in dbContext.person on new { v_PersonId = E.v_PersonId }
                                equals new { v_PersonId = F.v_PersonId } into F_join
                            from F in F_join.DefaultIfEmpty()

                            join G in dbContext.systemparameter on new { a = A.i_TipoParto.Value, b = 350 }
                                equals new { a = G.i_ParameterId, b = G.i_GroupId } into G_join // TIPO PARTO DETAIL
                            from G in G_join.DefaultIfEmpty()

                            join H in dbContext.systemparameter on new { a = A.i_TipoNacimiento.Value, b = 351 }
                                equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join // NACIMIENTO DETAIL
                            from H in H_join.DefaultIfEmpty()

                            join I in dbContext.systemparameter on new { a = A.i_TipoComplicacion.Value, b = 352 }
                                equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join // COMPLICACION DETAIL
                            from I in I_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.v_TipoRegistro == "Partos"

                            select new TramasList
                            {
                                v_TramaId = A.v_TramaId,
                                v_TipoRegistro = A.v_TipoRegistro,
                                d_FechaIngreso = A.d_FechaIngreso.Value,
                                tipoParto_Detail = G.v_Value1,
                                tipoNacimiento_Detail = H.v_Value1,
                                tipoCompliacacion_Detail = I.v_Value1,
                                d_InsertDate = A.d_InsertDate.Value,
                                d_UpdateDate = A.d_UpdateDate.Value,
                                User_Crea = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                User_Act = E.i_SystemUserId == null ? "---" : F.v_FirstName + " " + F.v_FirstLastName + " " + F.v_SecondLastName,
                            };
                #region
                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
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
                #endregion
                List<TramasList> objData = query.ToList();
                var tramasdetalle = (from a in objData
                                     select new TramasList
                                     {
                                         v_TramaId = a.v_TramaId,
                                         v_TipoRegistro = a.v_TipoRegistro,
                                         d_FechaIngreso = a.d_FechaIngreso,
                                         tipoParto_Detail = a.tipoParto_Detail,
                                         tipoNacimiento_Detail = a.tipoNacimiento_Detail,
                                         tipoCompliacacion_Detail = a.tipoCompliacacion_Detail,
                                         User_Crea = a.User_Crea,
                                         User_Act = a.User_Act,
                                         d_InsertDate = a.d_InsertDate,
                                         d_UpdateDate = a.d_UpdateDate,
                                     }).ToList();
                pobjOperationResult.Success = 1;
                return tramasdetalle;

            }
            catch (Exception e)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(e);
                return null;
                //Console.WriteLine(e);
                //throw;
            }
        }

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

        public void UpdateTrama(ref OperationResult pobjOperationResult, tramasDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.tramas
                                       where a.v_TramaId == pobjDtoEntity.v_TramaId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                tramas objEntity = tramasAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.tramas.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TRAMA", "v_TramaId=" + objEntity.v_TramaId.ToString(), Success.Ok, null);

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TRAMA", "v_TramaId=" + pobjDtoEntity.v_TramaId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);

                return;
            }
        }
        public tramasDto GetTrama(ref OperationResult objOperationResult, string _tramaId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                tramasDto objDtoEntity = null;

                var objEntity = (from a in dbContext.tramas
                                 where a.v_TramaId == _tramaId
                    select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = tramasAssembler.ToDTO(objEntity);

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

        public void DeleteTrama(string tramaId, List<string> ClientSession)
        {
            OperationResult objOperationResult = new OperationResult();
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            try
            {

                var objEntitySource1 = (from a in dbContext.tramas
                                        where a.v_TramaId == tramaId
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
    }
}
