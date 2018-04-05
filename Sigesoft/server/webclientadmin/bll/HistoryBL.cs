using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;

namespace Sigesoft.Server.WebClientAdmin.BLL
{
    public class HistoryBL
    {
        public List<Sigesoft.Node.WinClient.BE.HistoryList> GetHistoryReport(string pstrPersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.history
                             //join B in dbContext.workstationdangers on A.v_HistoryId equals B.v_HistoryId
                             //join B1 in dbContext.systemparameter on new { a = B.i_DangerId.Value, b = 145 } equals new { a = B1.i_ParameterId, b = B1.i_GroupId }
                             //join C in dbContext.typeofeep on A.v_HistoryId equals C.v_HistoryId
                             //join C1 in dbContext.systemparameter on new { a = C.i_TypeofEEPId.Value, b = 146 } equals new { a = C1.i_ParameterId, b = C1.i_GroupId }
                             join D in dbContext.systemparameter on new { a = A.i_TypeOperationId.Value, b = 204 } equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                             from D in D_join.DefaultIfEmpty()
                             where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId
                             //&& B.i_IsDeleted == 0 && C.i_IsDeleted == 0

                             select new Sigesoft.Node.WinClient.BE.HistoryList
                             {
                                 v_HistoryId = A.v_HistoryId,
                                 d_StartDate = A.d_StartDate,
                                 d_EndDate = A.d_EndDate,
                                 v_Organization = A.v_Organization,
                                 v_TypeActivity = A.v_TypeActivity,
                                 i_GeografixcaHeight = A.i_GeografixcaHeight,
                                 v_workstation = A.v_workstation,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,
                                 b_FingerPrintImage = A.b_FingerPrintImage,
                                 b_RubricImage = A.b_RubricImage,
                                 t_RubricImageText = A.t_RubricImageText,
                                 v_TypeOperationName = D.v_Value1,
                                 i_SoloAnio = A.i_SoloAnio.Value
                             }).ToList();
                var q = (from a in query
                         let xxx = new ServiceBL().GetYearsAndMonth(a.d_EndDate, a.d_StartDate)
                         select new Sigesoft.Node.WinClient.BE.HistoryList
                         {
                             v_HistoryId = a.v_HistoryId,
                             d_StartDate = a.d_StartDate,
                             d_EndDate = a.d_EndDate,
                             v_Organization = a.v_Organization,
                             v_TypeActivity = a.v_TypeActivity,
                             i_GeografixcaHeight = a.i_GeografixcaHeight,
                             v_workstation = a.v_workstation,
                             d_CreationDate = a.d_CreationDate,
                             d_UpdateDate = a.d_UpdateDate,
                             b_FingerPrintImage = a.b_FingerPrintImage,
                             b_RubricImage = a.b_RubricImage,
                             t_RubricImageText = a.t_RubricImageText,
                             Fecha = "Fecha Inicio: " + a.d_StartDate.ToString().Substring(4, 7) + "  Fecha Fin: " + a.d_EndDate.ToString().Substring(4, 7),
                             Exposicion = ConcatenateExposiciones(a.v_HistoryId),
                             Epps = ConcatenateEpps(a.v_HistoryId),
                             v_TypeOperationName = a.v_TypeOperationName,
                             TiempoLabor = xxx,
                             i_SoloAnio = a.i_SoloAnio
                         }).ToList();
                List<Sigesoft.Node.WinClient.BE.HistoryList> objData = q.ToList();
                return objData;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string ConcatenateExposiciones(string pstrHistoryId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var qry = (from a in dbContext.workstationdangers
                       join B1 in dbContext.systemparameter on new { a = a.i_DangerId.Value, b = 145 } equals new { a = B1.i_ParameterId, b = B1.i_GroupId }
                       where a.v_HistoryId == pstrHistoryId &&
                       a.i_IsDeleted == 0
                       select new
                       {
                           v_Exposicion = B1.v_Value1
                       }).ToList();

            return string.Join(", ", qry.Select(p => p.v_Exposicion));
        }

        private string ConcatenateEpps(string pstrHistoryId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var qry = (from a in dbContext.typeofeep
                       join C1 in dbContext.systemparameter on new { a = a.i_TypeofEEPId.Value, b = 146 } equals new { a = C1.i_ParameterId, b = C1.i_GroupId }
                       where a.v_HistoryId == pstrHistoryId &&
                       a.i_IsDeleted == 0
                       select new
                       {
                           v_Epps = C1.v_Value1
                       }).ToList();

            return string.Join(", ", qry.Select(p => p.v_Epps));
        }

        public List<Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList> GetFamilyMedicalAntecedentsReport(string pstrPersonId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.familymedicalantecedents
                             join B in dbContext.systemparameter on new { a = A.i_TypeFamilyId.Value, b = 149 }
                                                            equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                             from B in B_join.DefaultIfEmpty()
                             join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                          equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                             from C in C_join.DefaultIfEmpty()
                             join D in dbContext.diseases on new { a = A.v_DiseasesId }
                                                    equals new { a = D.v_DiseasesId } into D_join
                             from D in D_join.DefaultIfEmpty()

                             where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                             select new Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList
                             {
                                 v_FamilyMedicalAntecedentsId = A.v_FamilyMedicalAntecedentsId,
                                 v_PersonId = A.v_PersonId,
                                 v_DiseasesId = A.v_DiseasesId,
                                 v_DiseaseName = D.v_Name,
                                 //i_TypeFamilyId = A.i_TypeFamilyId.Value,
                                 i_TypeFamilyId = C.i_ParameterId,
                                 v_TypeFamilyName = C.v_Value1,
                                 v_Comment = A.v_Comment,
                                 v_FullAntecedentName = D.v_Name + " / " + C.v_Value1 + ", " + A.v_Comment,
                                 DxAndComment = D.v_Name + "," + A.v_Comment
                             }).ToList();

                // add the sequence number on the fly
                var query1 = query.Select((x, index) => new Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList
                {
                    i_Item = index + 1,
                    v_FamilyMedicalAntecedentsId = x.v_FamilyMedicalAntecedentsId,
                    v_PersonId = x.v_PersonId,
                    v_DiseasesId = x.v_DiseasesId,
                    v_DiseaseName = x.v_DiseaseName,
                    i_TypeFamilyId = x.i_TypeFamilyId,
                    v_TypeFamilyName = x.v_TypeFamilyName,
                    v_Comment = x.v_Comment,
                    v_FullAntecedentName = x.v_FullAntecedentName,
                    DxAndComment = x.DxAndComment
                }).ToList();

                return query1;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList> GetPersonMedicalHistoryReport(string pstrPersonId)
        {
            //mon.IsActive = true;
            try
            {


                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.personmedicalhistory

                             join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 }
                                                               equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                             from B in B_join.DefaultIfEmpty()

                             join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                               equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                             from C in C_join.DefaultIfEmpty()

                             join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                             join E in dbContext.systemparameter on new { a = A.i_TypeDiagnosticId.Value, b = 139 }
                                                               equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                             from E in E_join.DefaultIfEmpty()

                             where (A.i_IsDeleted == 0) && (A.v_PersonId == pstrPersonId)

                             select new Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList
                             {
                                 v_PersonMedicalHistoryId = A.v_PersonMedicalHistoryId,
                                 v_PersonId = A.v_PersonId,
                                 v_DiseasesId = A.v_DiseasesId,
                                 v_DiseasesName = D.v_Name,
                                 i_TypeDiagnosticId = A.i_TypeDiagnosticId,
                                 d_StartDate = A.d_StartDate.Value,
                                 v_TreatmentSite = A.v_TreatmentSite,
                                 v_GroupName = C.v_Value1,
                                 v_TypeDiagnosticName = E.v_Value1,
                                 v_DiagnosticDetail = A.v_DiagnosticDetail,
                                 i_Answer = A.i_AnswerId.Value,

                             }).ToList();

                // add the sequence number on the fly
                var query1 = new List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList>();

                query1 = query.Select((x, index) => new Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList
                {
                    i_Item = index + 1,
                    v_PersonMedicalHistoryId = x.v_PersonMedicalHistoryId,
                    v_PersonId = x.v_PersonId,
                    v_DiseasesId = x.v_DiseasesId,
                    v_DiseasesName = x.v_DiseasesName,
                    i_TypeDiagnosticId = x.i_TypeDiagnosticId,
                    d_StartDate = x.d_StartDate,
                    v_TreatmentSite = x.v_TreatmentSite,
                    v_GroupName = x.v_GroupName,
                    v_TypeDiagnosticName = x.v_TypeDiagnosticName,
                    v_DiagnosticDetail = x.v_DiagnosticDetail,
                    i_Answer = x.i_Answer,
                }).ToList();

                //List<PersonMedicalHistoryList> objData = query.ToList();

                return query1;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.NoxiousHabitsList> GetNoxiousHabitsReport(string pstrPersonId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.noxioushabits
                             join B in dbContext.systemparameter on new { a = A.i_TypeHabitsId.Value, b = 148 }
                                                            equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                             from B in B_join.DefaultIfEmpty()

                             where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                             select new Sigesoft.Node.WinClient.BE.NoxiousHabitsList
                             {
                                 v_NoxiousHabitsId = A.v_NoxiousHabitsId,
                                 v_NoxiousHabitsName = B.v_Value1,
                                 v_PersonId = A.v_PersonId,
                                 v_Frequency = A.v_Frequency + ", " + A.v_Comment,
                                 v_Comment = A.v_Comment,
                                 i_TypeHabitsId = B.i_ParameterId,
                                 v_TypeHabitsName = B.v_Value1,
                                 i_RecordStatus = 0,// grabado
                                 i_RecordType = 2,// no temporal
                                 v_DescriptionQuantity = A.v_DescriptionQuantity,
                                 v_DescriptionHabit = A.v_DescriptionHabit,
                                 v_FrecuenciaHabito = A.v_Frequency

                             }).ToList();


                // add the sequence number on the fly
                var query1 = query.Select((x, index) => new Sigesoft.Node.WinClient.BE.NoxiousHabitsList
                {
                    i_Item = index + 1,
                    v_NoxiousHabitsId = x.v_NoxiousHabitsId,
                    v_NoxiousHabitsName = x.v_NoxiousHabitsName,
                    v_PersonId = x.v_PersonId,
                    v_Frequency = x.v_Frequency,
                    v_Comment = x.v_Comment,
                    i_TypeHabitsId = x.i_TypeHabitsId,
                    v_TypeHabitsName = x.v_TypeHabitsName,
                    v_DescriptionQuantity = x.v_DescriptionQuantity,
                    v_DescriptionHabit = x.v_DescriptionHabit,
                    v_FrecuenciaHabito = x.v_FrecuenciaHabito

                }).ToList();


                return query1;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GrabarHistoria312(ref OperationResult pobjOperationResult, List<historyDto> pobjListaHistory, List<personmedicalhistoryDto> pobjListapersonmedicalhistory, List<noxioushabitsDto> pobjListanoxioushabits, List<familymedicalantecedentsDto> pobjListafamilymedicalantecedentsDto, List<string> ClientSession)
        {
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Historia
                foreach (var item in pobjListaHistory)
                {
                    //Buscar HistoryId para actualizar

                    var qry = (from a in dbContext.history
                               where a.v_HistoryId == item.v_HistoryId
                               select a
                                   ).FirstOrDefault();

                    if (qry != null)
                    {
                        item.d_UpdateDate = DateTime.Now;
                        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        history objEntity = historyAssembler.ToEntity(item);

                        // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                        dbContext.history.ApplyCurrentValues(objEntity);

                    }
                    else
                    {
                        history objEntity = historyAssembler.ToEntity(item);

                        #region Historial
                        objEntity.d_InsertDate = DateTime.Now;
                        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntity.i_IsDeleted = 0;
                        // Autogeneramos el Pk de la tabla                 
                        int intNodeId = int.Parse(ClientSession[0]);
                        NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 37), "HH"); ;
                        objEntity.v_HistoryId = NewId;

                        dbContext.AddTohistory(objEntity);
                    }

                }
                        #endregion




                #endregion

                #region Patológicos Personales
                foreach (var item in pobjListapersonmedicalhistory)
                {
                    //Buscar HistoryId para actualizar

                    var qry = (from a in dbContext.personmedicalhistory
                               where a.v_PersonMedicalHistoryId == item.v_PersonMedicalHistoryId
                               select a
                               ).FirstOrDefault();

                    if (qry != null)
                    {
                        item.d_UpdateDate = DateTime.Now;
                        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        personmedicalhistory objEntity = personmedicalhistoryAssembler.ToEntity(item);

                        // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                        dbContext.personmedicalhistory.ApplyCurrentValues(objEntity);
                    }
                    else
                    {
                        personmedicalhistory objEntity = personmedicalhistoryAssembler.ToEntity(item);

                        objEntity.d_InsertDate = DateTime.Now;
                        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntity.i_IsDeleted = 0;
                        // Autogeneramos el Pk de la tabla                 
                        int intNodeId = int.Parse(ClientSession[0]);
                        NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 40), "PH"); ;
                        objEntity.v_PersonMedicalHistoryId = NewId;

                        dbContext.AddTopersonmedicalhistory(objEntity);
                    }

                }
                #endregion

                //#region Absentismo
                //foreach (var item in pobjListaAbsentismoDto)
                //{
                //    //Buscar HistoryId para actualizar

                //    var qry = (from a in dbContext.absentismo
                //               where a.v_AbsentismoId == item.v_AbsentismoId
                //               select a
                //               ).FirstOrDefault();

                //    if (qry != null)
                //    {
                //        item.d_UpdateDate = DateTime.Now;
                //        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                //        absentismo objEntity = absentismoAssembler.ToEntity(item);

                //        // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                //        dbContext.absentismo.ApplyCurrentValues(objEntity);
                //    }
                //    else
                //    {
                //        absentismo objEntity = absentismoAssembler.ToEntity(item);

                //        objEntity.d_InsertDate = DateTime.Now;
                //        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                //        objEntity.i_IsDeleted = 0;
                //        // Autogeneramos el Pk de la tabla                 
                //        int intNodeId = int.Parse(ClientSession[0]);
                //        NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 60), "AB"); ;
                //        objEntity.v_AbsentismoId = NewId;

                //        dbContext.AddToabsentismo(objEntity);
                //    }

                //}

                //#endregion

                #region Hábitos Nocivos
                foreach (var item in pobjListanoxioushabits)
                {
                    //Buscar HistoryId para actualizar

                    var qry = (from a in dbContext.noxioushabits
                               where a.v_NoxiousHabitsId == item.v_NoxiousHabitsId
                               select a
                               ).FirstOrDefault();

                    if (qry != null)
                    {
                        item.d_UpdateDate = DateTime.Now;
                        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        noxioushabits objEntity = noxioushabitsAssembler.ToEntity(item);

                        // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                        dbContext.noxioushabits.ApplyCurrentValues(objEntity);
                    }
                    else
                    {
                        noxioushabits objEntity = noxioushabitsAssembler.ToEntity(item);

                        objEntity.d_InsertDate = DateTime.Now;
                        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntity.i_IsDeleted = 0;
                        // Autogeneramos el Pk de la tabla                 
                        int intNodeId = int.Parse(ClientSession[0]);
                        NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 41), "NX"); ;
                        objEntity.v_NoxiousHabitsId = NewId;

                        dbContext.AddTonoxioushabits(objEntity);
                    }

                }
                #endregion

                #region Antecedentes Familiares
                foreach (var item in pobjListafamilymedicalantecedentsDto)
                {
                    //Buscar HistoryId para actualizar

                    var qry = (from a in dbContext.familymedicalantecedents
                               where a.v_FamilyMedicalAntecedentsId == item.v_FamilyMedicalAntecedentsId
                               select a
                               ).FirstOrDefault();

                    if (qry != null)
                    {
                        item.d_UpdateDate = DateTime.Now;
                        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        familymedicalantecedents objEntity = familymedicalantecedentsAssembler.ToEntity(item);

                        // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                        dbContext.familymedicalantecedents.ApplyCurrentValues(objEntity);
                    }
                    else
                    {
                        familymedicalantecedents objEntity = familymedicalantecedentsAssembler.ToEntity(item);

                        objEntity.d_InsertDate = DateTime.Now;
                        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntity.i_IsDeleted = 0;
                        // Autogeneramos el Pk de la tabla                 
                        int intNodeId = int.Parse(ClientSession[0]);
                        NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 42), "FA"); ;
                        objEntity.v_FamilyMedicalAntecedentsId = NewId;

                        dbContext.AddTofamilymedicalantecedents(objEntity);
                    }

                }
                #endregion

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                return null;
            }
        }

        public static void SetAuditoryDataOnServiceComponent(ref OperationResult pobjOperationResult, List<KeyValuePair<string, int>> plistKeyValuePairs, int pintCategoriaId)
        {
            try
            {
                using (var ts = TransactionUtils.CreateTransactionScope())
                {
                    using (var dbContext = new SigesoftEntitiesModel())
                    {
                        var scId = plistKeyValuePairs.Select(p => p.Key);
                        var servicesComponentsToEdit = dbContext.servicecomponent.Where(p => p.i_IsDeleted == 0 && scId.Contains(p.v_ServiceComponentId)).ToList();
                        var services = servicesComponentsToEdit.Select(p => p.v_ServiceId).Distinct().ToList();
                        var serviceComponetId = servicesComponentsToEdit[0].v_ServiceComponentId;
                        #region recolecta todos los services components del sistema
                        var servicesComponentsToEditAll = (from n in dbContext.service
                                                           join J1 in dbContext.servicecomponent on n.v_ServiceId equals J1.v_ServiceId into J1_join                                                             
                                                           from J1 in J1_join.DefaultIfEmpty()
                                                           //join J2 in dbContext.component on J1.v_ComponentId equals J2.v_ComponentId into J2_join
                                                           //from J2 in J2_join.DefaultIfEmpty()
                                                           where
                                                           J1.v_ServiceComponentId == serviceComponetId
                                                           //services.Contains(n.v_ServiceId) 
                                                           //&& J2.i_CategoryId == pintCategoriaId
                                                           select J1).ToList();
                        #endregion

                        #region recolecta los usuarios del sistema en un diccionario
                        var usersDictionary = (from n in dbContext.systemuser
                                               join J1 in dbContext.person on n.v_PersonId equals J1.v_PersonId into J1_join
                                               from J1 in J1_join.DefaultIfEmpty()

                                               join J2 in dbContext.professional on J1.v_PersonId equals J2.v_PersonId into J2_join
                                               from J2 in J2_join.DefaultIfEmpty()
                                               where n.i_IsDeleted == 0
                                               select new
                                               {
                                                   id = n.i_SystemUserId,
                                                   tipoProfesional = J2.i_ProfessionId ?? 31
                                               }).ToDictionary(p => p.id, o => (TipoProfesional)o.tipoProfesional);
                        #endregion

                        #region Recorre los componentes actualizando la auditoria

                        var valuesDictionary = plistKeyValuePairs.ToDictionary(p => p.Key, o => o.Value);

                        foreach (var serviceC in servicesComponentsToEdit)
                        {
                            int idUsuario = valuesDictionary.TryGetValue(serviceC.v_ServiceComponentId, out idUsuario)
                                ? idUsuario
                                : 1;
                            var serviceCToEdit =
                                servicesComponentsToEditAll.Where(p => p.v_ServiceId.Equals(serviceC.v_ServiceId) && p.i_IsDeleted == 0).ToList();
                            TipoProfesional tipoProfesional = usersDictionary.TryGetValue(idUsuario, out tipoProfesional)
                                ? tipoProfesional
                                : TipoProfesional.Auditor;
                            foreach (var servicecomponent in serviceCToEdit)
                            {
                                switch (tipoProfesional)
                                {
                                    case TipoProfesional.Evaluador:
                                        if (servicecomponent.i_ApprovedInsertUserId == null)
                                        {
                                            //servicecomponent.i_ApprovedInsertUserId = idUsuario;
                                            //servicecomponent.d_ApprovedInsertDate = DateTime.Now;

                                            //servicecomponent.i_ApprovedUpdateUserId = idUsuario;
                                            //servicecomponent.d_ApprovedUpdateDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            //servicecomponent.i_ApprovedUpdateUserId = idUsuario;
                                            //servicecomponent.d_ApprovedUpdateDate = DateTime.Now;
                                        }

                                        dbContext.servicecomponent.ApplyCurrentValues(servicecomponent);
                                        break;

                                    //case TipoProfesional.Auditor:
                                    //    if (servicecomponent.i_AuditorInsertUserId == null)
                                    //    {
                                    //        servicecomponent.i_AuditorInsertUserId = idUsuario;
                                    //        servicecomponent.d_AuditorInsertUser = DateTime.Now;

                                    //        servicecomponent.i_AuditorUpdateUserId = idUsuario;
                                    //        servicecomponent.d_AuditorUpdateUser = DateTime.Now;
                                    //    }
                                    //    else
                                    //    {
                                    //        servicecomponent.i_AuditorUpdateUserId = idUsuario;
                                    //        servicecomponent.d_AuditorUpdateUser = DateTime.Now;
                                    //    }
                                    //    dbContext.servicecomponent.ApplyCurrentValues(servicecomponent);
                                    //    break;
                                    default:
                                        throw new ArgumentOutOfRangeException(
                                            "El tipo de profesional no se encuentra declarado en el Enum TipoProfesional");
                                }
                            }

                        }

                        #endregion

                        dbContext.SaveChanges();
                        pobjOperationResult.Success = 1;
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }
        }     

        public void DeleteHistory(ref OperationResult pobjOperationResult, string pstrHistoryId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.history
                                       where a.v_HistoryId == pstrHistoryId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "HISTORIA", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "HISTORIA", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public List<HistoryList> ListaHistoriaPorPersonId(string pPersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objListaHistoriaOcupacional = (from a in dbContext.history
                                                   where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                                   select new HistoryList
                                                   {
                                                       v_PersonId = a.v_PersonId,
                                                       v_Organization = a.v_Organization,
                                                       v_TypeActivity = a.v_TypeActivity,
                                                       v_workstation = a.v_workstation,
                                                       //v_Fechas = a.v_Fechas,
                                                       //v_Exposicion = a.v_Exposicion,
                                                       //v_Epps = a.v_Epps,
                                                       //v_TiempoTrabajo = a.v_TiempoTrabajo,
                                                       i_TrabajoActual = a.i_TrabajoActual.Value,
                                                       v_HistoryId = a.v_HistoryId,
                                                       i_IsDeleted = a.i_IsDeleted.Value,
                                                       i_InsertUserId = a.i_InsertUserId.Value,
                                                       d_InsertDate = a.d_InsertDate,
                                                       i_UpdateUserId = a.i_UpdateUserId.Value,
                                                       d_UpdateDate = a.d_UpdateDate
                                                   }).ToList();

                return objListaHistoriaOcupacional;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public historyDto GetHistory(ref OperationResult pobjOperationResult, string pstrHistoryId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                historyDto objDtoEntity = null;

                var objEntity = (from a in dbContext.history
                                 where a.v_HistoryId == pstrHistoryId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = historyAssembler.ToDTO(objEntity);

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

        public string AddHistory(ref OperationResult pobjOperationResult, historyDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                history objEntity = historyAssembler.ToEntity(pobjDtoEntity);

                workstationdangersDto objworkstationdangersDto = new workstationdangersDto();
                typeofeepDto objtypeofeepDto = new typeofeepDto();

                #region Historial
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 37), "HH"); ;
                objEntity.v_HistoryId = NewId;

                dbContext.AddTohistory(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                #endregion



                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA", "v_HistoryId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA", "v_HistoryId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public void UpdateHistory(ref OperationResult pobjOperationResult, historyDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Actualizar Cabecera
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.history
                                       where a.v_HistoryId == pobjDtoEntity.v_HistoryId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                history objEntity = historyAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.history.ApplyCurrentValues(objEntity);


                #endregion

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "HISTORIA", "v_HistoryId=" + objEntity.v_HistoryId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "HISTORIA", "v_HistoryId=" + pobjDtoEntity.v_HistoryId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public List<HistoryList> ListarGrillaHistoriaOcupacional(string pPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objListaHistoriaOcupacional = (from a in dbContext.history
                                               where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                               select new HistoryList
                                               {
                                                   d_StartDate = a.d_StartDate.Value,
                                                   d_EndDate = a.d_EndDate.Value,
                                                   v_PersonId = a.v_PersonId,
                                                   v_Organization = a.v_Organization,
                                                   v_TypeActivity = a.v_TypeActivity,
                                                   v_workstation = a.v_workstation,
                                                   i_GeografixcaHeight = a.i_GeografixcaHeight.Value,
                                                   i_TrabajoActual = a.i_TrabajoActual.Value,
                                                   v_HistoryId = a.v_HistoryId
                                               }).ToList();

            return objListaHistoriaOcupacional;

        }

        public List<PersonMedicalHistoryList> ListarGrillaMedicoPersonal(string pPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objListaPatoPersonales = (from a in dbContext.personmedicalhistory
                                          join B in dbContext.systemparameter on new { a = a.v_DiseasesId, b = 147 }
                                                  equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                                          from B in B_join.DefaultIfEmpty()

                                          join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                                            equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                          from C in C_join.DefaultIfEmpty()
                                          join D in dbContext.diseases on a.v_DiseasesId equals D.v_DiseasesId
                                          join E in dbContext.systemparameter on new { a = a.i_TypeDiagnosticId.Value, b = 139 }
                                                       equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                          from E in E_join.DefaultIfEmpty()

                                          where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                          select new PersonMedicalHistoryList
                                          {
                                              v_GroupName = C.v_Value1 == null ? "ENFERMEDADES OTROS" : C.v_Value1,
                                              v_DiseasesName = D.v_Name,
                                              v_TypeDiagnosticName = E.v_Value1,
                                              d_StartDate = a.d_StartDate,
                                              v_PersonId = a.v_PersonId,
                                              v_DiseasesId = a.v_DiseasesId,
                                              v_DiagnosticDetail = a.v_DiagnosticDetail,
                                              v_TreatmentSite = a.v_TreatmentSite,
                                              v_PersonMedicalHistoryId = a.v_PersonMedicalHistoryId
                                          }).ToList();

            return objListaPatoPersonales;

        }

        public List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList> GetPersonMedicalHistoryPagedAndFilteredByPersonId1(ref OperationResult pobjOperationResult,string pstrPersonId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.personmedicalhistory
                            join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 }
                                                              equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                            from B in B_join.DefaultIfEmpty()

                            join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                              equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                            from C in C_join.DefaultIfEmpty()

                            join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                            join E in dbContext.systemparameter on new { a = A.i_TypeDiagnosticId.Value, b = 139 }
                                                              equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                            from E in E_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                             equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                            select new Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList
                            {
                                v_PersonMedicalHistoryId = A.v_PersonMedicalHistoryId,
                                v_PersonId = A.v_PersonId,
                                v_DiseasesId = A.v_DiseasesId,
                                v_DiseasesName = D.v_Name,
                                i_TypeDiagnosticId = A.i_TypeDiagnosticId,
                                d_StartDate = A.d_StartDate.Value,
                                v_TreatmentSite = A.v_TreatmentSite,
                                v_GroupName = C.v_Value1,
                                v_TypeDiagnosticName = E.v_Value1,
                                v_DiagnosticDetail = A.v_DiagnosticDetail,
                                d_UpdateDate = A.d_UpdateDate,
                                i_Answer = A.i_AnswerId.Value
                            };

                List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList> objData = query.ToList();
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

        public List<Sigesoft.Node.WinClient.BE.SystemParameterList> GetSystemParametersPagedAndFilteredNew(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, int? pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.systemparameter
                            join B in dbContext.systemparameter on new { a = A.i_ParentParameterId.Value, b = A.i_GroupId }
                                                                equals new { a = B.i_ParameterId, b = 147 } into B_join
                            from B in B_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J3 in dbContext.diseases on new { a = A.v_Value1 }
                                                          equals new { a = J3.v_DiseasesId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                            where B.i_GroupId == pintGroupId && B.i_IsDeleted == 0
                            select new Sigesoft.Node.WinClient.BE.SystemParameterList
                            {
                                i_GroupId = A.i_GroupId,
                                i_ParameterId = A.i_ParameterId,
                                i_ParentGroupId = A.i_GroupId,
                                i_ParentParameterId = A.i_ParentParameterId.Value,
                                v_Value1 = A.v_Value1,
                                v_Value2 = A.v_Value2,
                                v_DiseasesName = J3.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,
                                Enfermedad = B.v_Value1,
                                v_DiseaseName = J3.v_Name,
                                SI = false,
                                NO = true,
                                ND = false,
                                i_Answer = 0
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
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<Sigesoft.Node.WinClient.BE.SystemParameterList> objData = query.ToList();
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

        public void AddPersonMedicalHistory(ref OperationResult pobjOperationResult, List<personmedicalhistoryDto> pobjpersonmedicalhistoryAdd, List<personmedicalhistoryDto> pobjpersonmedicalhistoryUpdate, List<personmedicalhistoryDto> pobjpersonmedicalhistoryDelete, List<string> ClientSession)
        {
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);
                #region Crear Person Medical
                foreach (var item in pobjpersonmedicalhistoryAdd)
                {
                    personmedicalhistory objEntity1 = new personmedicalhistory();
                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 40), "PH");
                    objEntity1.v_PersonMedicalHistoryId = NewId;
                    objEntity1.v_PersonId = item.v_PersonId;
                    objEntity1.v_DiseasesId = item.v_DiseasesId;
                    objEntity1.i_TypeDiagnosticId = item.i_TypeDiagnosticId;
                    objEntity1.d_StartDate = item.d_StartDate;
                    objEntity1.v_DiagnosticDetail = item.v_DiagnosticDetail;
                    objEntity1.v_TreatmentSite = item.v_TreatmentSite;
                    objEntity1.i_AnswerId = item.i_AnswerId;
                    objEntity1.i_SoloAnio = item.i_SoloAnio;
                    dbContext.AddTopersonmedicalhistory(objEntity1);
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Update Person Medical
                if (pobjpersonmedicalhistoryUpdate != null)
                {
                    // Actualizar Componentes del protocolo
                    foreach (var item in pobjpersonmedicalhistoryUpdate)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.personmedicalhistory
                                                where a.v_PersonMedicalHistoryId == item.v_PersonMedicalHistoryId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados

                        objEntitySource1.i_TypeDiagnosticId = item.i_TypeDiagnosticId;
                        objEntitySource1.d_StartDate = item.d_StartDate;
                        objEntitySource1.v_DiagnosticDetail = item.v_DiagnosticDetail;
                        objEntitySource1.v_TreatmentSite = item.v_TreatmentSite;
                        objEntitySource1.i_SoloAnio = item.i_SoloAnio;
                        objEntitySource1.i_IsDeleted = 0;

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Delete Person Medical
                if (pobjpersonmedicalhistoryDelete != null)
                {
                    foreach (var item in pobjpersonmedicalhistoryDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.personmedicalhistory
                                                where a.v_PersonMedicalHistoryId == item.v_PersonMedicalHistoryId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA MÉDICA PERSONAL", "v_PersonMedicalHistoryId=" + NewId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA MÉDICA PERSONAL", "v_PersonMedicalHistoryId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public void AddMedicoPersonal(ref OperationResult pobjOperationResult, personmedicalhistoryDto pobjpersonmedicalhistoryAdd, List<string> ClientSession)
        {
            try
            {
                string NewId = "(No generado)";

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);
                          
                personmedicalhistory objEntity1 = new personmedicalhistory();
                objEntity1.d_InsertDate = DateTime.Now;
                objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity1.i_IsDeleted = 0;

                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 40), "PH");
                objEntity1.v_PersonMedicalHistoryId = NewId;
                objEntity1.v_PersonId = pobjpersonmedicalhistoryAdd.v_PersonId;
                objEntity1.v_DiseasesId = pobjpersonmedicalhistoryAdd.v_DiseasesId;
                objEntity1.i_TypeDiagnosticId = pobjpersonmedicalhistoryAdd.i_TypeDiagnosticId;
                objEntity1.d_StartDate = pobjpersonmedicalhistoryAdd.d_StartDate;
                objEntity1.v_DiagnosticDetail = pobjpersonmedicalhistoryAdd.v_DiagnosticDetail;
                objEntity1.v_TreatmentSite = pobjpersonmedicalhistoryAdd.v_TreatmentSite;
                objEntity1.i_AnswerId = pobjpersonmedicalhistoryAdd.i_AnswerId;
                objEntity1.i_SoloAnio = pobjpersonmedicalhistoryAdd.i_SoloAnio;
                dbContext.AddTopersonmedicalhistory(objEntity1);

           
                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public void UpdateMedicoPersonal(ref OperationResult pobjOperationResult, personmedicalhistoryDto pobjpersonmedicalhistoryAdd, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener la entidad fuente
                var objEntitySource1 = (from a in dbContext.personmedicalhistory
                                        where a.v_PersonMedicalHistoryId == pobjpersonmedicalhistoryAdd.v_PersonMedicalHistoryId
                                        select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados

                objEntitySource1.i_TypeDiagnosticId = pobjpersonmedicalhistoryAdd.i_TypeDiagnosticId;
                objEntitySource1.v_DiseasesId = pobjpersonmedicalhistoryAdd.v_DiseasesId;
                objEntitySource1.d_StartDate = pobjpersonmedicalhistoryAdd.d_StartDate;
                objEntitySource1.v_DiagnosticDetail = pobjpersonmedicalhistoryAdd.v_DiagnosticDetail;
                objEntitySource1.v_TreatmentSite = pobjpersonmedicalhistoryAdd.v_TreatmentSite;
                objEntitySource1.i_IsDeleted = 0;
                objEntitySource1.i_SoloAnio = pobjpersonmedicalhistoryAdd.i_SoloAnio;
                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public void DeleteMedicoPersonal(ref OperationResult pobjOperationResult, string pstrPersonalMedicalId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                  // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.personmedicalhistory
                                                where a.v_PersonMedicalHistoryId == pstrPersonalMedicalId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                        dbContext.SaveChanges();
                        pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public personmedicalhistoryDto GetpersonmedicalhistoryDto(ref OperationResult pobjOperationResult, string pstrPersonMedicalHistoryId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                personmedicalhistoryDto objDtoEntity = null;

                var objEntity = (from a in dbContext.personmedicalhistory
                                 where a.v_PersonMedicalHistoryId == pstrPersonMedicalHistoryId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = personmedicalhistoryAssembler.ToDTO(objEntity);

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

        public noxioushabitsDto GetnoxioushabitsDto(ref OperationResult pobjOperationResult, string pstrNoxiousHabitsId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                noxioushabitsDto objDtoEntity = null;

                var objEntity = (from a in dbContext.noxioushabits
                                 where a.v_NoxiousHabitsId == pstrNoxiousHabitsId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = noxioushabitsAssembler.ToDTO(objEntity);

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
        
        public void AddNocivo(ref OperationResult pobjOperationResult, noxioushabitsDto pobjnoxioushabitsAdd, List<string> ClientSession)
        {
            try
            {
                string NewId = "(No generado)";

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);


                noxioushabits objEntity1 = new noxioushabits();
                objEntity1.d_InsertDate = DateTime.Now;
                objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity1.i_IsDeleted = 0;

                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 41), "NX");
                objEntity1.v_NoxiousHabitsId = NewId;
                objEntity1.v_PersonId = pobjnoxioushabitsAdd.v_PersonId;
                objEntity1.i_TypeHabitsId = pobjnoxioushabitsAdd.i_TypeHabitsId;
                objEntity1.v_Frequency = pobjnoxioushabitsAdd.v_Frequency;
                objEntity1.v_Comment = pobjnoxioushabitsAdd.v_Comment;
                dbContext.AddTonoxioushabits(objEntity1);

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public void UpdateNocivo(ref OperationResult pobjOperationResult, noxioushabitsDto pobjnoxioushabitsAdd, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener la entidad fuente
                var objEntitySource1 = (from a in dbContext.noxioushabits
                                        where a.v_NoxiousHabitsId == pobjnoxioushabitsAdd.v_NoxiousHabitsId
                                        select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados

                objEntitySource1.i_TypeHabitsId = pobjnoxioushabitsAdd.i_TypeHabitsId;
                objEntitySource1.v_Frequency = pobjnoxioushabitsAdd.v_Frequency;
                objEntitySource1.v_Comment = pobjnoxioushabitsAdd.v_Comment;
                objEntitySource1.i_IsDeleted = 0;

                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public void DeleteNocivos(ref OperationResult pobjOperationResult, string pstrNoxiousHabitsId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener la entidad fuente
                var objEntitySource1 = (from a in dbContext.noxioushabits
                                        where a.v_NoxiousHabitsId == pstrNoxiousHabitsId
                                        select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource1.i_IsDeleted = 1;

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }
        
        public List<NoxiousHabitsList> ListarGrillaNocivos(string pPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objListaHabitosNocivos = (from a in dbContext.noxioushabits
                                          join B in dbContext.systemparameter on new { a = a.i_TypeHabitsId.Value, b = 148 }
                                                     equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                                          from B in B_join.DefaultIfEmpty()
                                          where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                          select new NoxiousHabitsList
                                          {
                                              v_TypeHabitsName = B.v_Value1,
                                              v_Frequency = a.v_Frequency,
                                              v_PersonId = a.v_PersonId,
                                              i_TypeHabitsId = a.i_TypeHabitsId,
                                              v_Comment = a.v_Comment,
                                              v_NoxiousHabitsId = a.v_NoxiousHabitsId
                                          }).ToList();

            return objListaHabitosNocivos;

        }
        
        public familymedicalantecedentsDto GetfamilymedicalantecedentsDto(ref OperationResult pobjOperationResult, string pstrfamilymedicalantecedentsId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                familymedicalantecedentsDto objDtoEntity = null;

                var objEntity = (from a in dbContext.familymedicalantecedents
                                 where a.v_FamilyMedicalAntecedentsId == pstrfamilymedicalantecedentsId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = familymedicalantecedentsAssembler.ToDTO(objEntity);

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

        public void AddFamiliar(ref OperationResult pobjOperationResult, familymedicalantecedentsDto pobjnoxioushabitsAdd, List<string> ClientSession)
        {
            try
            {
                string NewId = "(No generado)";

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);


                familymedicalantecedents objEntity1 = new familymedicalantecedents();
                objEntity1.d_InsertDate = DateTime.Now;
                objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity1.i_IsDeleted = 0;

                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 42), "FA");
                objEntity1.v_FamilyMedicalAntecedentsId = NewId;
                objEntity1.v_PersonId = pobjnoxioushabitsAdd.v_PersonId;
                objEntity1.v_DiseasesId = pobjnoxioushabitsAdd.v_DiseasesId;
                objEntity1.i_TypeFamilyId = pobjnoxioushabitsAdd.i_TypeFamilyId;
                objEntity1.v_Comment = pobjnoxioushabitsAdd.v_Comment;
                dbContext.AddTofamilymedicalantecedents(objEntity1);

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public void UpdateFamiliar(ref OperationResult pobjOperationResult, familymedicalantecedentsDto pobjfamilymedicalantecedentsAdd, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener la entidad fuente
                var objEntitySource1 = (from a in dbContext.familymedicalantecedents
                                        where a.v_FamilyMedicalAntecedentsId == pobjfamilymedicalantecedentsAdd.v_FamilyMedicalAntecedentsId
                                        select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados

                objEntitySource1.v_DiseasesId = pobjfamilymedicalantecedentsAdd.v_DiseasesId;
                objEntitySource1.v_Comment = pobjfamilymedicalantecedentsAdd.v_Comment;
                objEntitySource1.i_IsDeleted = 0;
                objEntitySource1.i_TypeFamilyId = pobjfamilymedicalantecedentsAdd.i_TypeFamilyId;
                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public void DeleteFamiliars(ref OperationResult pobjOperationResult, string pstrfamilymedicalantecedentsId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener la entidad fuente
                var objEntitySource1 = (from a in dbContext.familymedicalantecedents
                                        where a.v_FamilyMedicalAntecedentsId == pstrfamilymedicalantecedentsId
                                        select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource1.i_IsDeleted = 1;

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
            }
            catch (Exception)
            {
                pobjOperationResult.Success = 0;
                throw;
            }
        }

        public List<FamilyMedicalAntecedentsList> ListarGrillaFamiliars(string pPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objFamilyMedicalAntecedentsList = (from a in dbContext.familymedicalantecedents

                                                   join B in dbContext.systemparameter on new { a = a.i_TypeFamilyId.Value, b = 149 }
                                                        equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                                                   from B in B_join.DefaultIfEmpty()

                                                   join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                                   from C in C_join.DefaultIfEmpty()
                                                   join D in dbContext.diseases on new { a = a.v_DiseasesId }
                                                        equals new { a = D.v_DiseasesId } into D_join
                                                   from D in D_join.DefaultIfEmpty()
                                                   where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                                   select new FamilyMedicalAntecedentsList
                                                   {
                                                       v_PersonId = a.v_PersonId,
                                                       i_TypeFamilyId = a.i_TypeFamilyId.Value,
                                                       v_CommentFamili = a.v_Comment,
                                                       v_FamilyMedicalAntecedentsId = a.v_FamilyMedicalAntecedentsId,
                                                       v_TypeFamilyName = C.v_Value1,
                                                       v_DiseaseName = D.v_Name,
                                                       v_Comment = a.v_Comment
                                                   }).ToList();


            return objFamilyMedicalAntecedentsList;

        }

        #region Peligros

        public List<Sigesoft.Node.WinClient.BE.WorkstationDangersList> GetWorkstationDangersagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrHistorytId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.workstationdangers
                            join B in dbContext.systemparameter on new { a = A.i_DangerId.Value, b = 145 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 145 } equals new { a = C.i_ParameterId, b = C.i_GroupId }
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.v_HistoryId == pstrHistorytId

                            select new Sigesoft.Node.WinClient.BE.WorkstationDangersList
                            {
                                v_WorkstationDangersId = A.v_WorkstationDangersId,
                                v_ParentName = C.v_Value1,
                                i_DangerId = A.i_DangerId,
                                v_DangerName = B.v_Value1,
                                i_RecordStatus = (int)RecordStatus.Grabado,
                                i_RecordType = (int)RecordType.NoTemporal,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate
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

                List<Sigesoft.Node.WinClient.BE.WorkstationDangersList> objData = query.ToList();
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

        public workstationdangersDto GetWorkstationDangers(ref OperationResult pobjOperationResult, string pstrWorkstationDangersId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                workstationdangersDto objDtoEntity = null;

                var objEntity = (from a in dbContext.workstationdangers
                                 where a.v_WorkstationDangersId == pstrWorkstationDangersId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = workstationdangersAssembler.ToDTO(objEntity);

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

        public void AddWorkstationDangers(ref OperationResult pobjOperationResult, workstationdangersDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                workstationdangers objEntity = workstationdangersAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 39), "HW"); ;
                objEntity.v_WorkstationDangersId = NewId;

                dbContext.AddToworkstationdangers(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateWorkstationDangers(ref OperationResult pobjOperationResult, workstationdangersDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.workstationdangers
                                       where a.v_WorkstationDangersId == pobjDtoEntity.v_WorkstationDangersId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                workstationdangers objEntity = workstationdangersAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.workstationdangers.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + objEntity.v_WorkstationDangersId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + pobjDtoEntity.v_WorkstationDangersId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteWorkstationDangers(ref OperationResult pobjOperationResult, string pstrWorkstationDangersId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.workstationdangers
                                       where a.v_WorkstationDangersId == pstrWorkstationDangersId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PELIGRO EN EL PUESTO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PELIGRO EN EL PUESTO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }
      
        #endregion

        #region TypeOfEEP

        public List<Sigesoft.Node.WinClient.BE.TypeOfEEPList> GetTypeOfEEPPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrHistorytId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.typeofeep
                            join B in dbContext.systemparameter on new { a = A.i_TypeofEEPId.Value, b = 146 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                         equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 && A.v_HistoryId == pstrHistorytId

                            select new Sigesoft.Node.WinClient.BE.TypeOfEEPList
                            {
                                v_TypeofEEPId = A.v_TypeofEEPId,
                                v_TypeofEEPName = B.v_Value1,
                                i_TypeofEEPId = A.i_TypeofEEPId,
                                r_Percentage = A.r_Percentage,
                                i_RecordStatus = (int)RecordStatus.Grabado,
                                i_RecordType = (int)RecordType.NoTemporal,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate
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

                List<Sigesoft.Node.WinClient.BE.TypeOfEEPList> objData = query.ToList();
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

        public typeofeepDto GetTypeOfEEPP(ref OperationResult pobjOperationResult, string pstrTypeOfEEPId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                typeofeepDto objDtoEntity = null;

                var objEntity = (from a in dbContext.typeofeep
                                 where a.v_TypeofEEPId == pstrTypeOfEEPId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = typeofeepAssembler.ToDTO(objEntity);

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

        public void AddTypeOfEEPP(ref OperationResult pobjOperationResult, typeofeepDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                typeofeep objEntity = typeofeepAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 38), "HE"); ;
                objEntity.v_TypeofEEPId = NewId;

                dbContext.AddTotypeofeep(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TIPO DE EEP", "v_TypeofEEPId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TIPO DE EEP", "v_TypeofEEPId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateTypeOfEEPP(ref OperationResult pobjOperationResult, typeofeepDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.typeofeep
                                       where a.v_TypeofEEPId == pobjDtoEntity.v_TypeofEEPId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                typeofeep objEntity = typeofeepAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.typeofeep.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TIPO DE EEP", "v_TypeofEEPId=" + objEntity.v_TypeofEEPId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TIPO DE EEP", "v_TypeofEEPId=" + pobjDtoEntity.v_TypeofEEPId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteTypeOfEEPP(ref OperationResult pobjOperationResult, string pstrTypeOfEEPPId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.typeofeep
                                       where a.v_TypeofEEPId == pstrTypeOfEEPPId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "TIPO DE EEP", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "TIPO DE EEP", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        #endregion

        public void AddNoxiousHabits(ref OperationResult pobjOperationResult, List<noxioushabitsDto> pobjnoxioushabitsAdd, List<noxioushabitsDto> pobjnoxioushabitsUpdate, List<noxioushabitsDto> pobjnoxioushabitsDelete, List<string> ClientSession)
        {
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);
                #region Crear Noxious Habits
                foreach (var item in pobjnoxioushabitsAdd)
                {
                    noxioushabits objEntity1 = new noxioushabits();
                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 41), "NX");
                    objEntity1.v_NoxiousHabitsId = NewId;
                    objEntity1.v_PersonId = item.v_PersonId;
                    objEntity1.v_Frequency = item.v_Frequency;
                    objEntity1.v_Comment = item.v_Comment;
                    objEntity1.i_TypeHabitsId = item.i_TypeHabitsId;
                    dbContext.AddTonoxioushabits(objEntity1);
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Update Noxious Habits
                if (pobjnoxioushabitsUpdate != null)
                {
                    // Actualizar Componentes del protocolo
                    foreach (var item in pobjnoxioushabitsUpdate)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.noxioushabits
                                                where a.v_NoxiousHabitsId == item.v_NoxiousHabitsId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados

                        objEntitySource1.v_Frequency = item.v_Frequency;
                        objEntitySource1.v_Comment = item.v_Comment;
                        objEntitySource1.i_IsDeleted = 0;
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Delete Noxious Habits
                if (pobjnoxioushabitsDelete != null)
                {
                    foreach (var item in pobjnoxioushabitsDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.noxioushabits
                                                where a.v_NoxiousHabitsId == item.v_NoxiousHabitsId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HÁBITOS NOCIVOS", "v_NoxiousHabitsId=" + NewId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HÁBITOS NOCIVOS", "v_NoxiousHabitsId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public void AddFamilyMedicalAntecedents(ref OperationResult pobjOperationResult, List<familymedicalantecedentsDto> pobjfamilymedicalantecedentsAdd, List<familymedicalantecedentsDto> pobjfamilymedicalantecedentsUpdate, List<familymedicalantecedentsDto> pobjfamilymedicalantecedentsDelete, List<string> ClientSession)
        {
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);
                #region Crear Antecedentes Familiares
                foreach (var item in pobjfamilymedicalantecedentsAdd)
                {
                    familymedicalantecedents objEntity1 = new familymedicalantecedents();
                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 42), "FA");
                    objEntity1.v_FamilyMedicalAntecedentsId = NewId;
                    objEntity1.v_PersonId = item.v_PersonId;
                    objEntity1.v_DiseasesId = item.v_DiseasesId;
                    objEntity1.i_TypeFamilyId = item.i_TypeFamilyId;
                    objEntity1.v_Comment = item.v_Comment;
                    dbContext.AddTofamilymedicalantecedents(objEntity1);
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Update Antecedentes Familiares
                if (pobjfamilymedicalantecedentsUpdate != null)
                {
                    // Actualizar Componentes del protocolo
                    foreach (var item in pobjfamilymedicalantecedentsUpdate)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.familymedicalantecedents
                                                where a.v_FamilyMedicalAntecedentsId == item.v_FamilyMedicalAntecedentsId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.v_DiseasesId = item.v_DiseasesId;
                        objEntitySource1.v_Comment = item.v_Comment;
                        objEntitySource1.i_IsDeleted = 0;

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Delete Antecedentes Familiares
                if (pobjfamilymedicalantecedentsDelete != null)
                {
                    foreach (var item in pobjfamilymedicalantecedentsDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.familymedicalantecedents
                                                where a.v_FamilyMedicalAntecedentsId == item.v_FamilyMedicalAntecedentsId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ANTEDECENTES FAMILIARES", "v_FamilyMedicalAntecedentsId=" + NewId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ANTECEDENTES FAMILIARES", "v_FamilyMedicalAntecedentsId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        //public static CamposAuditoria CamposAuditoria(string pstrIdServicesComponent)
        //{
        //    try
        //    {
        //        using (var dbContex = new SigesoftEntitiesModel())
        //        {
        //            var sc = dbContex.servicecomponent.FirstOrDefault(p => p.v_ServiceComponentId.Equals(pstrIdServicesComponent));
        //            var systemUsers = dbContex.systemuser.Where(p => p.i_IsDeleted == 0).ToList();
        //            if (sc == null) return null;
        //            var auI = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_AuditorInsertUserId ?? -1));
        //            var auE = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_AuditorUpdateUserId ?? -1));
        //            var evI = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_ApprovedInsertUserId ?? -1));
        //            var evE = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_ApprovedUpdateUserId ?? -1));
        //            var result = new CamposAuditoria
        //            {
        //                FechaHoraAuditoriaInsert = sc.d_AuditorInsertUser != null ? sc.d_AuditorInsertUser.Value.ToShortDateString() + " " + sc.d_AuditorInsertUser.Value.ToShortTimeString() : string.Empty,
        //                FechaHoraEvaluadorInsert = sc.d_ApprovedInsertDate != null ? sc.d_ApprovedInsertDate.Value.ToShortDateString() + " " + sc.d_ApprovedInsertDate.Value.ToShortTimeString() : string.Empty,
        //                FechaHoraAuditoriaEdit = sc.d_AuditorUpdateUser != null ? sc.d_AuditorUpdateUser.Value.ToShortDateString() + " " + sc.d_AuditorUpdateUser.Value.ToShortTimeString() : string.Empty,
        //                FechaHoraEvaluadorEdit = sc.d_ApprovedUpdateDate != null ? sc.d_ApprovedUpdateDate.Value.ToShortDateString() + " " + sc.d_ApprovedUpdateDate.Value.ToShortTimeString() : string.Empty,
        //                UserNameAuditoriaEdit = auE != null ? auE.v_UserName : string.Empty,
        //                UserNameAuditoriaInsert = auI != null ? auI.v_UserName : string.Empty,
        //                UserNameEvaluadorInsert = evI != null ? evI.v_UserName : string.Empty,
        //                UserNameEvaluadorEdit = evE != null ? evE.v_UserName : string.Empty,
        //            };

        //            return result;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public static CamposAuditoria CamposAuditoria(string pstrIdServicesComponent)
        {
            try
            {
                using (var dbContex = new SigesoftEntitiesModel())
                {
                    var sc = dbContex.servicecomponent.FirstOrDefault(p => p.v_ServiceComponentId.Equals(pstrIdServicesComponent));
                    var systemUsers = dbContex.systemuser.Where(p => p.i_IsDeleted == 0).ToList();
                    if (sc == null) return null;
                    var auI = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_AuditorInsertUserId ?? -1));
                    var auE = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_AuditorUpdateUserId ?? -1));

                    var evI = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_ApprovedInsertUserId ?? -1));
                    var evE = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_ApprovedUpdateUserId ?? -1));

                    var inI = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_InsertUserId ?? -1));
                    var inE = systemUsers.FirstOrDefault(p => p.i_SystemUserId == (sc.i_UpdateUserId ?? -1));
                    var result = new CamposAuditoria
                    {
                        FechaHoraAuditoriaInsert = sc.d_AuditorInsertUser != null ? sc.d_AuditorInsertUser.Value.ToShortDateString() + " " + sc.d_AuditorInsertUser.Value.ToShortTimeString() : string.Empty,
                        FechaHoraEvaluadorInsert = sc.d_ApprovedInsertDate != null ? sc.d_ApprovedInsertDate.Value.ToShortDateString() + " " + sc.d_ApprovedInsertDate.Value.ToShortTimeString() : string.Empty,
                        FechaHoraInformadorInsert = sc.d_InsertDate != null ? sc.d_InsertDate.Value.ToShortDateString() + " " + sc.d_InsertDate.Value.ToShortTimeString() : string.Empty,


                        FechaHoraAuditoriaEdit = sc.d_AuditorUpdateUser != null ? sc.d_AuditorUpdateUser.Value.ToShortDateString() + " " + sc.d_AuditorUpdateUser.Value.ToShortTimeString() : string.Empty,
                        FechaHoraEvaluadorEdit = sc.d_ApprovedUpdateDate != null ? sc.d_ApprovedUpdateDate.Value.ToShortDateString() + " " + sc.d_ApprovedUpdateDate.Value.ToShortTimeString() : string.Empty,
                        FechaHoraInformadorEdit = sc.d_UpdateDate != null ? sc.d_UpdateDate.Value.ToShortDateString() + " " + sc.d_UpdateDate.Value.ToShortTimeString() : string.Empty,
                        

                        UserNameAuditoriaEdit = auE != null ? auE.v_UserName : string.Empty,
                        UserNameAuditoriaInsert = auI != null ? auI.v_UserName : string.Empty,

                        UserNameEvaluadorInsert = evI != null ? evI.v_UserName : string.Empty,
                        UserNameEvaluadorEdit = evE != null ? evE.v_UserName : string.Empty,

                        UserNameInformadorInsert = inI != null ? inI.v_UserName : string.Empty,
                        UserNameInformadorEdit = inE != null ? inE.v_UserName : string.Empty,

                    };

                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
