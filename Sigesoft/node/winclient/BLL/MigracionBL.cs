using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Collections;

namespace Sigesoft.Node.WinClient.BLL
{
    public class MigracionBL
    {
//        public List<OrganizationList> DevolverDatosEmpresaOLD()
//        {
//            //mon.IsActive = true;

//            try
//            {
//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                var query = from A in dbContext.organization_old
//                            where A.i_IsDeleted == 0
//                            select new OrganizationList
//                            {
//                                v_OrganizationId = A.v_OrganizationId,
//                                i_OrganizationTypeId = (int)A.i_OrganizationTypeId,
//                                i_SectorTypeId = (int)A.i_SectorTypeId,
//                                v_SectorName = A.v_SectorName,
//                                v_SectorCodigo = A.v_SectorCodigo,
//                                v_IdentificationNumber = A.v_IdentificationNumber,
//                                v_Name = A.v_Name,
//                                v_Address = A.v_Address,
//                                v_PhoneNumber = A.v_PhoneNumber,
//                                v_Mail = A.v_Mail,
//                                v_ContacName = A.v_ContacName,
//                                v_Contacto = A.v_Contacto,
//                                v_EmailContacto = A.v_EmailContacto,
//                                v_Observation = A.v_Observation,
//                                i_NumberQuotasOrganization = A.i_NumberQuotasOrganization,
//                                i_NumberQuotasMen = A.i_NumberQuotasMen,
//                                i_DepartmentId = A.i_DepartmentId.Value,
//                                i_ProvinceId = A.i_ProvinceId.Value,
//                                i_DistrictId = A.i_DistrictId.Value,
//                                i_IsDeleted = A.i_IsDeleted.Value,
//                                i_InsertUserId = A.i_InsertUserId.Value,
//                                d_InsertDate = A.d_InsertDate,
//                                i_UpdateUserId = A.i_UpdateUserId.Value,
//                                d_UpdateDate = A.d_UpdateDate,
//                                b_Image = A.b_Image,
//                                v_ContactoMedico = A.v_ContactoMedico,
//                                v_EmailMedico = A.v_EmailMedico
//                            };

//                List<OrganizationList> objData = query.ToList();

//                return objData;

//            }
//            catch (Exception ex)
//            {

//                return null;
//            }
//        }

//        public bool VerificarSiExisteEmpresaAntigua(string psrtRUC)
//        {
//            try
//            {
//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                var query = (from A in dbContext.organization
//                             where A.i_IsDeleted == 0 && A.v_IdentificationNumber == psrtRUC
//                             select new OrganizationList
//                             {
//                                 v_IdentificationNumber = A.v_IdentificationNumber
//                             }).FirstOrDefault();

//                if (query != null)
//                {
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        public EmpresaMigracionList VerificarSiExisteEmpresaAntigua_(string psrtRUC)
//        {
//            try
//            {
//                EmpresaMigracionList oEmpresaMigracionList = new EmpresaMigracionList();
//                LocationMigracionList oLocationMigracionList = new LocationMigracionList();

//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                var qEmpresa = (from A in dbContext.organization
//                                where A.i_IsDeleted == 0 && A.v_IdentificationNumber == psrtRUC
//                                select new EmpresaMigracionList
//                                {
//                                    v_OrganizationId = A.v_OrganizationId,
//                                    v_Name = A.v_Name,
//                                    v_IdentificationNumber = A.v_IdentificationNumber

//                                }).FirstOrDefault();


//                var qSedes = (from A in dbContext.location
//                              where A.i_IsDeleted == 0 && A.v_OrganizationId == qEmpresa.v_OrganizationId
//                              select new LocationMigracionList
//                                {
//                                    v_LocationId = A.v_LocationId,
//                                    v_OrganizationId = A.v_OrganizationId,
//                                    v_Name = A.v_Name
//                                }).ToList();

//                var x = qSedes[0].v_LocationId;

//                var qGESOS = (from A in dbContext.groupoccupation
//                              where A.i_IsDeleted == 0 && A.v_LocationId == x
//                              select new groupoccupationMigracionList
//                               {
//                                   v_GroupOccupationId = A.v_GroupOccupationId,
//                                   v_LocationId = A.v_LocationId,
//                                   v_Name = A.v_Name
//                               }).ToList();

//                oEmpresaMigracionList.v_OrganizationId = qEmpresa.v_OrganizationId;
//                oEmpresaMigracionList.v_Name = qEmpresa.v_Name;
//                oEmpresaMigracionList.v_IdentificationNumber = qEmpresa.v_IdentificationNumber;
//                oEmpresaMigracionList.Sedes = qSedes;
//                oEmpresaMigracionList.GESOS = qGESOS;

//                return oEmpresaMigracionList;

//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        public List<LocationList> DevolverSedesAntiguasPorIdEmpresa(string pstrEmpresaId)
//        {
//            //mon.IsActive = true;

//            try
//            {
//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                var query = from A in dbContext.location_old
//                            where A.i_IsDeleted == 0 && A.v_OrganizationId == pstrEmpresaId
//                            select new LocationList
//                            {
//                                v_LocationId = A.v_LocationId,
//                                v_OrganizationId = A.v_OrganizationId,
//                                v_Name = A.v_Name,
//                                i_IsDeleted = A.i_IsDeleted,
//                                i_InsertUserId = A.i_InsertUserId,
//                                d_InsertDate = A.d_InsertDate,
//                                i_UpdateUserId = A.i_UpdateUserId,
//                                d_UpdateDate = A.d_UpdateDate
//                            };

//                List<LocationList> objData = query.ToList();

//                return objData;

//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public List<GroupOccupationList> DevolverGESOporLocationId(string pstrLocationId)
//        {
//            //mon.IsActive = true;

//            try
//            {
//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                var query = from A in dbContext.groupoccupation_old
//                            where A.i_IsDeleted == 0 && A.v_LocationId == pstrLocationId
//                            select new GroupOccupationList
//                            {
//                                v_LocationId = A.v_LocationId,
//                                v_Name = A.v_Name,
//                                i_IsDeleted = A.i_IsDeleted.Value,
//                                i_InsertUserId = A.i_InsertUserId.Value,
//                                d_InsertDate = A.d_InsertDate,
//                                i_UpdateUserId = A.i_UpdateUserId.Value,
//                                d_UpdateDate = A.d_UpdateDate
//                            };

//                List<GroupOccupationList> objData = query.ToList();

//                return objData;

//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public List<PacientList> DevolverDatosPacientLD()
//        {
//            //mon.IsActive = true;
//            try
//            {
//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                var query = from A in dbContext.pacient_old
//                            join B in dbContext.person_old on A.v_PersonId equals B.v_PersonId
//                            where A.i_IsDeleted == 0
//                            select new PacientList
//                            {
//                                v_PersonId = A.v_PersonId,
//                                i_IsDeleted = A.i_IsDeleted.Value,
//                                d_UpdateDate = A.d_UpdateDate,
//                                i_UpdateNodeId = A.i_UpdateNodeId.Value,
//                                v_DocNumber = B.v_DocNumber,
//                                v_FirstName = B.v_FirstName,
//                                v_FirstLastName = B.v_FirstLastName,
//                                v_SecondLastName = B.v_SecondLastName,
//                                i_DocTypeId = B.i_DocTypeId.Value,

//                                d_Birthdate = B.d_Birthdate,
//                                v_BirthPlace = B.v_BirthPlace,
//                                i_SexTypeId = B.i_SexTypeId.Value,
//                                i_MaritalStatusId = B.i_MaritalStatusId.Value,
//                                i_LevelOfId = B.i_LevelOfId.Value,

//                                v_TelephoneNumber = B.v_TelephoneNumber,
//                                v_AdressLocation = B.v_AdressLocation,
//                                v_GeografyLocationId = B.v_GeografyLocationId,
//                                v_ContactName = B.v_ContactName,
//                                v_EmergencyPhone = B.v_EmergencyPhone,
//                                b_PersonImage = B.b_PersonImage,
//                                v_Mail = B.v_Mail,
//                                i_BloodGroupId = B.i_BloodGroupId.Value,
//                                i_BloodFactorId = B.i_BloodFactorId.Value,
//                                b_FingerPrintTemplate = B.b_FingerPrintTemplate,
//                                b_RubricImage = B.b_RubricImage,
//                                b_FingerPrintImage = B.b_FingerPrintImage,
//                                t_RubricImageText = B.t_RubricImageText,
//                                v_CurrentOccupation = B.v_CurrentOccupation,
//                                i_DepartmentId = B.i_DepartmentId.Value,
//                                i_ProvinceId = B.i_ProvinceId.Value,
//                                i_DistrictId = B.i_DistrictId.Value,
//                                i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId.Value,
//                                v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
//                                i_TypeOfInsuranceId = B.i_TypeOfInsuranceId.Value,
//                                i_NumberLivingChildren = B.i_NumberLivingChildren.Value,
//                                i_NumberDependentChildren = B.i_NumberDependentChildren.Value,
//                                i_OccupationTypeId = B.i_OccupationTypeId.Value,
//                                v_OwnerName = B.v_OwnerName,
//                                i_NumberLiveChildren = B.i_NumberLiveChildren.Value,
//                                i_NumberDeadChildren = B.i_NumberDeadChildren.Value,

//                                i_InsertUserId = B.i_InsertUserId.Value,
//                                d_InsertDate = B.d_InsertDate.Value,
//                                i_UpdateUserId = B.i_UpdateUserId.Value,

//                                i_InsertNodeId = B.i_InsertNodeId.Value,

//                                i_Relationship = B.i_Relationship.Value,
//                                v_ExploitedMineral = B.v_ExploitedMineral,
//                                i_AltitudeWorkId = B.i_AltitudeWorkId.Value,
//                                i_PlaceWorkId = B.i_PlaceWorkId.Value,
//                                v_NroPoliza = B.v_NroPoliza,
//                                v_Deducible = B.v_Deducible.Value,
//                                i_NroHermanos = B.i_NroHermanos.Value,
//                                v_Password = B.v_Password

//                            };

//                List<PacientList> objData = query.ToList();

//                return objData;

//            }
//            catch (Exception ex)
//            {

//                return null;
//            }
//        }


//        #region Historia
//        //public List<HistoryList> ObtenerLisatHistoriaPorPersonId(string pstrPersonId)
//        //{
//        //    //mon.IsActive = true;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        var query = from A in dbContext.history_old
//        //                    where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

//        //                    select new HistoryList
//        //                    {
//        //                        v_HistoryId = A.v_HistoryId,
//        //                        d_StartDate = A.d_StartDate,
//        //                        d_EndDate = A.d_EndDate,
//        //                        v_Organization = A.v_Organization,
//        //                        v_TypeActivity = A.v_TypeActivity,
//        //                        i_GeografixcaHeight = A.i_GeografixcaHeight,
//        //                        v_workstation = A.v_workstation,
//        //                        b_RubricImage = A.b_RubricImage,
//        //                        b_FingerPrintImage = A.b_FingerPrintImage,
//        //                        t_RubricImageText = A.t_RubricImageText,
//        //                        i_TypeOperationId = A.i_TypeOperationId.Value
//        //                    };

//        //        List<HistoryList> objData = query.ToList();


//        //        return objData;

//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        return null;
//        //    }
//        //}

//        //public List<WorkstationDangersList> ObtenerListaPeligrosPorHistoryId(string pstrHistorytId)
//        //{
//        //    //mon.IsActive = true;
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        var query = from A in dbContext.workstationdangers_old
//        //                    where A.i_IsDeleted == 0 && A.v_HistoryId == pstrHistorytId

//        //                    select new WorkstationDangersList
//        //                    {
//        //                        v_WorkstationDangersId = A.v_WorkstationDangersId,
//        //                        i_DangerId = A.i_DangerId,
//        //                        i_NoiseSource = A.i_NoiseSource,
//        //                        i_NoiseLevel = A.i_NoiseLevel,
//        //                        v_TimeOfExposureToNoise = A.v_TimeOfExposureToNoise
//        //                    };

//        //        List<WorkstationDangersList> objData = query.ToList();
//        //        return objData;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public List<TypeOfEEPList> ObtenerListaEPPSPorHistoryId(string pstrHistorytId)
//        //{
//        //    //mon.IsActive = true;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        var query = from A in dbContext.typeofeep
//        //                    where A.i_IsDeleted == 0 && A.v_HistoryId == pstrHistorytId

//        //                    select new TypeOfEEPList
//        //                    {
//        //                        i_TypeofEEPId = A.i_TypeofEEPId,
//        //                        r_Percentage = A.r_Percentage

//        //                    };

//        //        List<TypeOfEEPList> objData = query.ToList();

//        //        return objData;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public List<PersonMedicalHistoryList> DevolverListaMedicoPersonalesPorPersonId(string pstrPersonId)
//        //{
//        //    //mon.IsActive = true;
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        var query = from A in dbContext.personmedicalhistory_old
//        //                    join B in dbContext.diseases_old on A.v_DiseasesId equals B.v_DiseasesId
//        //                    where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId
//        //                    select new PersonMedicalHistoryList
//        //                    {
//        //                        v_PersonMedicalHistoryId = A.v_PersonMedicalHistoryId,
//        //                        v_PersonId = A.v_PersonId,
//        //                        v_DiseasesId = A.v_DiseasesId,
//        //                        i_TypeDiagnosticId = A.i_TypeDiagnosticId,
//        //                        d_StartDate = A.d_StartDate.Value,
//        //                        v_TreatmentSite = A.v_TreatmentSite,
//        //                        v_DiagnosticDetail = A.v_DiagnosticDetail,
//        //                        i_Answer = A.i_AnswerId.Value,
//        //                        v_CIE10Id = B.v_CIE10Id,
//        //                        v_Name = B.v_Name
//        //                    };

//        //        List<PersonMedicalHistoryList> objData = query.ToList();

//        //        return objData;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public List<FamilyMedicalAntecedentsList> DevolverListaMedicoFamiliaresPorPersonId(string pstrPersonId)
//        //{
//        //    //mon.IsActive = true;
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        var query = from A in dbContext.familymedicalantecedents_old
//        //                    join B in dbContext.diseases_old on A.v_DiseasesId equals B.v_DiseasesId
//        //                    where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

//        //                    select new FamilyMedicalAntecedentsList
//        //                    {
//        //                        v_FamilyMedicalAntecedentsId = A.v_FamilyMedicalAntecedentsId,
//        //                        v_PersonId = A.v_PersonId,
//        //                        v_DiseasesId = A.v_DiseasesId,
//        //                        i_TypeFamilyId = A.i_TypeFamilyId.Value,
//        //                        v_Comment = A.v_Comment,
//        //                        v_CIE10Id = B.v_CIE10Id,
//        //                        v_Name = B.v_Name
//        //                    };

//        //        List<FamilyMedicalAntecedentsList> objData = query.ToList();

//        //        return objData;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public List<NoxiousHabitsList> DevolverListaHabitosPorPersonId(string pstrPersonId)
//        //{
//        //    //mon.IsActive = true;
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        var query = from A in dbContext.noxioushabits_old
//        //                    where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

//        //                    select new NoxiousHabitsList
//        //                    {
//        //                        v_NoxiousHabitsId = A.v_NoxiousHabitsId,
//        //                        v_PersonId = A.v_PersonId,
//        //                        v_Frequency = A.v_Frequency,
//        //                        v_Comment = A.v_Comment
//        //                    };


//        //        List<NoxiousHabitsList> objData = query.ToList();

//        //        return objData;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}



//        #endregion

//        public string ValidarDiseaseSiExiste(string pstrCie10Id, string pstrName, List<string> ClientSession)
//        {
//            OperationResult _objOperationResult = new OperationResult();
//            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//            var objEntity = (from a in dbContext.diseases
//                             where a.v_CIE10Id == pstrCie10Id && a.v_Name == pstrName
//                             select a).FirstOrDefault();

//            if (objEntity != null)
//            {
//                return objEntity.v_DiseasesId;
//            }
//            else
//            {
//                diseasesDto odiseasesDto = new diseasesDto();
//                odiseasesDto.v_CIE10Id = pstrCie10Id;
//                odiseasesDto.v_Name = pstrName;
//                var DiseaseId = new MedicalExamFieldValuesBL().AddDiseases(ref _objOperationResult, odiseasesDto, ClientSession);

//                return DiseaseId;
//            }

//        }

//        #region Service
//        public List<CalendarList> DevolverListaCalendarOLD(string pstrServiceOLD)
//        {
//            //SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//            //var query = (from A in dbContext.calendar_old
//            //             where A.i_IsDeleted == 0 && A.v_ServiceId == pstrServiceOLD
//            //             select new CalendarList
//            //             {
//            //                 v_PersonId = A.v_PersonId,
//            //                 v_ProtocolId = A.v_ProtocolId,
//            //                 v_ServiceId = A.v_ServiceId,
//            //                 d_CircuitStartDate = A.d_CircuitStartDate.Value,
//            //                 d_DateTimeCalendar = A.d_DateTimeCalendar.Value,
//            //                 d_EntryTimeCM = A.d_EntryTimeCM.Value,
//            //                 d_SalidaCM = A.d_SalidaCM.Value,
//            //                 i_CalendarStatusId = A.i_CalendarStatusId.Value,
//            //                 i_IsVipId = A.i_IsVipId.Value,
//            //                 i_LineStatusId = A.i_LineStatusId.Value,
//            //                 i_NewContinuationId = A.i_NewContinuationId.Value,
//            //                 i_ServiceId = A.i_ServiceId.Value,
//            //                 i_ServiceTypeId = A.i_ServiceTypeId.Value,

//            //             }).ToList();


//            return null;
//        }

//        public List<ServiceList> DevolverListaServiciosOLD()
//        {
////            //mon.IsActive = true;
////            DateTime fechaInicio = DateTime.Parse("2011-11-23 00:00:01");
////            DateTime fechaIFin = DateTime.Parse("2018-11-19 23:59:59");
////            try
////            {
////                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
////                var query = (from A in dbContext.service_old
////                             join B in dbContext.person_old on A.v_PersonId equals B.v_PersonId
////                             where A.i_IsDeleted == 0 && A.d_ServiceDate > fechaInicio && A.d_ServiceDate < fechaIFin
////                             && A.d_ServiceDate != null
////                             && A.v_ServiceId == "N009-SR000006052"
////|| A.v_ServiceId == "N009-SR000006053"
////|| A.v_ServiceId == "N009-SR000006054"
////|| A.v_ServiceId == "N009-SR000006055"
////|| A.v_ServiceId == "N009-SR000006056"
////|| A.v_ServiceId == "N009-SR000006057"
////|| A.v_ServiceId == "N009-SR000006000"
////|| A.v_ServiceId == "N009-SR000006058"
////|| A.v_ServiceId == "N009-SR000006059"
////|| A.v_ServiceId == "N009-SR000006060"
////|| A.v_ServiceId == "N009-SR000006069"
////|| A.v_ServiceId == "N009-SR000006061"
////|| A.v_ServiceId == "N009-SR000006064"
////|| A.v_ServiceId == "N009-SR000006067"
////|| A.v_ServiceId == "N009-SR000006063"
////|| A.v_ServiceId == "N009-SR000006062"
////|| A.v_ServiceId == "N009-SR000006068"
////|| A.v_ServiceId == "N009-SR000006073"
////|| A.v_ServiceId == "N009-SR000006072"
////|| A.v_ServiceId == "N009-SR000006076"
////|| A.v_ServiceId == "N009-SR000006077"
////|| A.v_ServiceId == "N009-SR000006078"
////|| A.v_ServiceId == "N009-SR000006079"
////|| A.v_ServiceId == "N009-SR000006075"
////|| A.v_ServiceId == "N009-SR000006074"
////|| A.v_ServiceId == "N009-SR000006080"
////|| A.v_ServiceId == "N009-SR000006081"
////|| A.v_ServiceId == "N009-SR000006082"
////|| A.v_ServiceId == "N009-SR000006065"
////|| A.v_ServiceId == "N009-SR000006083"
////|| A.v_ServiceId == "N009-SR000006084"
////|| A.v_ServiceId == "N009-SR000006085"
////|| A.v_ServiceId == "N009-SR000006086"
////|| A.v_ServiceId == "N009-SR000006071"
////|| A.v_ServiceId == "N009-SR000006087"
////|| A.v_ServiceId == "N009-SR000006088"
////|| A.v_ServiceId == "N009-SR000006089"
////|| A.v_ServiceId == "N009-SR000006090"
////|| A.v_ServiceId == "N009-SR000006097"
////|| A.v_ServiceId == "N009-SR000006099"
////|| A.v_ServiceId == "N009-SR000006100"
////|| A.v_ServiceId == "N009-SR000006101"
////|| A.v_ServiceId == "N009-SR000006102"
////|| A.v_ServiceId == "N009-SR000006103"
////|| A.v_ServiceId == "N009-SR000006095"
////|| A.v_ServiceId == "N009-SR000006104"
////|| A.v_ServiceId == "N009-SR000006105"
////|| A.v_ServiceId == "N009-SR000006114"
////|| A.v_ServiceId == "N009-SR000006115"
////|| A.v_ServiceId == "N009-SR000006108"
////|| A.v_ServiceId == "N009-SR000006107"
////|| A.v_ServiceId == "N009-SR000006117"
////|| A.v_ServiceId == "N009-SR000006116"
////|| A.v_ServiceId == "N009-SR000006119"
////|| A.v_ServiceId == "N009-SR000006118"
////|| A.v_ServiceId == "N009-SR000006120"
////|| A.v_ServiceId == "N009-SR000006123"
////|| A.v_ServiceId == "N009-SR000006122"
////|| A.v_ServiceId == "N009-SR000006124"
////|| A.v_ServiceId == "N009-SR000006121"
////|| A.v_ServiceId == "N009-SR000006130"
////|| A.v_ServiceId == "N009-SR000006125"
////|| A.v_ServiceId == "N009-SR000006131"
////|| A.v_ServiceId == "N009-SR000006127"
////|| A.v_ServiceId == "N009-SR000006128"
////|| A.v_ServiceId == "N009-SR000006126"
////|| A.v_ServiceId == "N009-SR000006129"
////|| A.v_ServiceId == "N009-SR000006112"
////|| A.v_ServiceId == "N009-SR000006111"
////|| A.v_ServiceId == "N009-SR000006132"
////|| A.v_ServiceId == "N009-SR000006106"
////|| A.v_ServiceId == "N009-SR000006133"
////|| A.v_ServiceId == "N009-SR000006134"
////|| A.v_ServiceId == "N009-SR000006135"
////|| A.v_ServiceId == "N009-SR000006136"
////|| A.v_ServiceId == "N009-SR000006137"
////|| A.v_ServiceId == "N009-SR000006138"
////|| A.v_ServiceId == "N009-SR000006140"
////|| A.v_ServiceId == "N009-SR000006139"
////|| A.v_ServiceId == "N009-SR000006142"
////|| A.v_ServiceId == "N009-SR000006143"
////|| A.v_ServiceId == "N009-SR000006144"
////|| A.v_ServiceId == "N009-SR000006141"
////|| A.v_ServiceId == "N009-SR000006145"
////|| A.v_ServiceId == "N009-SR000006146"
////|| A.v_ServiceId == "N009-SR000006148"
////|| A.v_ServiceId == "N009-SR000006151"
////|| A.v_ServiceId == "N009-SR000006150"
////|| A.v_ServiceId == "N009-SR000006149"
////|| A.v_ServiceId == "N009-SR000006152"
////|| A.v_ServiceId == "N009-SR000006153"
////|| A.v_ServiceId == "N009-SR000006154"
////|| A.v_ServiceId == "N009-SR000006156"
////|| A.v_ServiceId == "N009-SR000006157"
////|| A.v_ServiceId == "N009-SR000006158"
////|| A.v_ServiceId == "N009-SR000006159"
////|| A.v_ServiceId == "N009-SR000006160"
////|| A.v_ServiceId == "N009-SR000006161"
////|| A.v_ServiceId == "N009-SR000006162"
////|| A.v_ServiceId == "N009-SR000006163"
////|| A.v_ServiceId == "N009-SR000006147"
////|| A.v_ServiceId == "N009-SR000006164"
////|| A.v_ServiceId == "N009-SR000006165"
////|| A.v_ServiceId == "N009-SR000006166"
////|| A.v_ServiceId == "N009-SR000006167"
////|| A.v_ServiceId == "N009-SR000006170"
////|| A.v_ServiceId == "N009-SR000006181"
////|| A.v_ServiceId == "N009-SR000006179"
////|| A.v_ServiceId == "N009-SR000006171"
////|| A.v_ServiceId == "N009-SR000006175"
////|| A.v_ServiceId == "N009-SR000006172"
////|| A.v_ServiceId == "N009-SR000006173"
////|| A.v_ServiceId == "N009-SR000006176"
////|| A.v_ServiceId == "N009-SR000006178"
////|| A.v_ServiceId == "N009-SR000006180"
////|| A.v_ServiceId == "N009-SR000006182"
////|| A.v_ServiceId == "N009-SR000006183"
////|| A.v_ServiceId == "N009-SR000006168"
////|| A.v_ServiceId == "N009-SR000006184"
////|| A.v_ServiceId == "N009-SR000006177"
////|| A.v_ServiceId == "N009-SR000006185"
////|| A.v_ServiceId == "N009-SR000006186"
////|| A.v_ServiceId == "N009-SR000006187"
////|| A.v_ServiceId == "N009-SR000006188"
////|| A.v_ServiceId == "N009-SR000006189"
////|| A.v_ServiceId == "N009-SR000006190"
////|| A.v_ServiceId == "N009-SR000006191"
////|| A.v_ServiceId == "N009-SR000006192"
////|| A.v_ServiceId == "N009-SR000006193"
////|| A.v_ServiceId == "N009-SR000006196"
////|| A.v_ServiceId == "N009-SR000006194"
////|| A.v_ServiceId == "N009-SR000006195"
////|| A.v_ServiceId == "N009-SR000006197"
////|| A.v_ServiceId == "N009-SR000006198"
////|| A.v_ServiceId == "N009-SR000006200"
////|| A.v_ServiceId == "N009-SR000006201"
////|| A.v_ServiceId == "N009-SR000006202"
////|| A.v_ServiceId == "N009-SR000006207"
////|| A.v_ServiceId == "N009-SR000006206"
////|| A.v_ServiceId == "N009-SR000006212"
////|| A.v_ServiceId == "N009-SR000006213"
////|| A.v_ServiceId == "N009-SR000006214"
////|| A.v_ServiceId == "N009-SR000006216"
////|| A.v_ServiceId == "N009-SR000006203"
////|| A.v_ServiceId == "N009-SR000006218"
////|| A.v_ServiceId == "N009-SR000006217"
////|| A.v_ServiceId == "N009-SR000006215"
////|| A.v_ServiceId == "N009-SR000006211"
////|| A.v_ServiceId == "N009-SR000006210"
////|| A.v_ServiceId == "N009-SR000006223"
////|| A.v_ServiceId == "N009-SR000006222"
////|| A.v_ServiceId == "N009-SR000006221"
////|| A.v_ServiceId == "N009-SR000006220"
////|| A.v_ServiceId == "N009-SR000006219"
////|| A.v_ServiceId == "N009-SR000006209"
////|| A.v_ServiceId == "N009-SR000006224"
////|| A.v_ServiceId == "N009-SR000006208"
////|| A.v_ServiceId == "N009-SR000006205"
////|| A.v_ServiceId == "N009-SR000006204"
////|| A.v_ServiceId == "N009-SR000006225"
////|| A.v_ServiceId == "N009-SR000006226"
////|| A.v_ServiceId == "N009-SR000006227"
////|| A.v_ServiceId == "N009-SR000006228"
////|| A.v_ServiceId == "N009-SR000006243"
////|| A.v_ServiceId == "N009-SR000006239"
////|| A.v_ServiceId == "N009-SR000006246"
////|| A.v_ServiceId == "N009-SR000006247"
////|| A.v_ServiceId == "N009-SR000006244"
////|| A.v_ServiceId == "N009-SR000006248"
////|| A.v_ServiceId == "N009-SR000006241"
////|| A.v_ServiceId == "N009-SR000006249"
////|| A.v_ServiceId == "N009-SR000006250"
////|| A.v_ServiceId == "N009-SR000006240"
////|| A.v_ServiceId == "N009-SR000006238"
////|| A.v_ServiceId == "N009-SR000006229"
////|| A.v_ServiceId == "N009-SR000006251"
////|| A.v_ServiceId == "N009-SR000006230"
////|| A.v_ServiceId == "N009-SR000006237"
////|| A.v_ServiceId == "N009-SR000006252"
////|| A.v_ServiceId == "N009-SR000006253"
////|| A.v_ServiceId == "N009-SR000006233"
////|| A.v_ServiceId == "N009-SR000006254"
////|| A.v_ServiceId == "N009-SR000006255"
////|| A.v_ServiceId == "N009-SR000006256"
////|| A.v_ServiceId == "N009-SR000006257"
////|| A.v_ServiceId == "N009-SR000006258"
////|| A.v_ServiceId == "N009-SR000006259"
////|| A.v_ServiceId == "N009-SR000006260"
////|| A.v_ServiceId == "N009-SR000006261"
////|| A.v_ServiceId == "N009-SR000006264"
////|| A.v_ServiceId == "N009-SR000006263"
////|| A.v_ServiceId == "N009-SR000006262"
////|| A.v_ServiceId == "N009-SR000006277"
////|| A.v_ServiceId == "N009-SR000006278"
////|| A.v_ServiceId == "N009-SR000006279"
////|| A.v_ServiceId == "N009-SR000006280"
////|| A.v_ServiceId == "N009-SR000006281"
////|| A.v_ServiceId == "N009-SR000006282"
////|| A.v_ServiceId == "N009-SR000006284"
////|| A.v_ServiceId == "N009-SR000006283"
////|| A.v_ServiceId == "N009-SR000006285"
////|| A.v_ServiceId == "N009-SR000006273"
////|| A.v_ServiceId == "N009-SR000006274"
////|| A.v_ServiceId == "N009-SR000006270"
////|| A.v_ServiceId == "N009-SR000006271"
////|| A.v_ServiceId == "N009-SR000006272"
////|| A.v_ServiceId == "N009-SR000006286"
////|| A.v_ServiceId == "N009-SR000006287"
////|| A.v_ServiceId == "N009-SR000006288"
////|| A.v_ServiceId == "N009-SR000006289"
////|| A.v_ServiceId == "N009-SR000006290"
////|| A.v_ServiceId == "N009-SR000006292"
////|| A.v_ServiceId == "N009-SR000006291"
////|| A.v_ServiceId == "N009-SR000006293"
////|| A.v_ServiceId == "N009-SR000006295"
////|| A.v_ServiceId == "N009-SR000006294"
////|| A.v_ServiceId == "N009-SR000006296"
////|| A.v_ServiceId == "N009-SR000006297"
////|| A.v_ServiceId == "N009-SR000006299"
////|| A.v_ServiceId == "N009-SR000006298"
////|| A.v_ServiceId == "N009-SR000006300"
////|| A.v_ServiceId == "N009-SR000006268"
////|| A.v_ServiceId == "N009-SR000006301"
////|| A.v_ServiceId == "N009-SR000006302"
////|| A.v_ServiceId == "N009-SR000006265"
////|| A.v_ServiceId == "N009-SR000006267"
////|| A.v_ServiceId == "N009-SR000006308"
////|| A.v_ServiceId == "N009-SR000006304"
////|| A.v_ServiceId == "N009-SR000006303"
////|| A.v_ServiceId == "N009-SR000006314"
////|| A.v_ServiceId == "N009-SR000006316"
////|| A.v_ServiceId == "N009-SR000006315"
////|| A.v_ServiceId == "N009-SR000006318"
////|| A.v_ServiceId == "N009-SR000006317"
////|| A.v_ServiceId == "N009-SR000006319"
////|| A.v_ServiceId == "N009-SR000006320"
////|| A.v_ServiceId == "N009-SR000006305"
////|| A.v_ServiceId == "N009-SR000006307"
////|| A.v_ServiceId == "N009-SR000006306"
////|| A.v_ServiceId == "N009-SR000006321"
////|| A.v_ServiceId == "N009-SR000006312"
////|| A.v_ServiceId == "N009-SR000006309"
////|| A.v_ServiceId == "N009-SR000006313"
////|| A.v_ServiceId == "N009-SR000006322"
////|| A.v_ServiceId == "N009-SR000006323"
////|| A.v_ServiceId == "N009-SR000006324"
////|| A.v_ServiceId == "N009-SR000006325"
////|| A.v_ServiceId == "N009-SR000006345"
////|| A.v_ServiceId == "N009-SR000006344"
////|| A.v_ServiceId == "N009-SR000006335"
////|| A.v_ServiceId == "N009-SR000006346"
////|| A.v_ServiceId == "N009-SR000006347"
////|| A.v_ServiceId == "N009-SR000006349"
////|| A.v_ServiceId == "N009-SR000006350"
////|| A.v_ServiceId == "N009-SR000006348"
////|| A.v_ServiceId == "N009-SR000006332"
////|| A.v_ServiceId == "N009-SR000006331"
////|| A.v_ServiceId == "N009-SR000006329"
////|| A.v_ServiceId == "N009-SR000006333"
////|| A.v_ServiceId == "N009-SR000006339"
////|| A.v_ServiceId == "N009-SR000006351"
////|| A.v_ServiceId == "N009-SR000006334"
////|| A.v_ServiceId == "N009-SR000006352"
////|| A.v_ServiceId == "N009-SR000006353"
////|| A.v_ServiceId == "N009-SR000006337"
////|| A.v_ServiceId == "N009-SR000006354"
////|| A.v_ServiceId == "N009-SR000006338"
////|| A.v_ServiceId == "N009-SR000006355"
////|| A.v_ServiceId == "N009-SR000006336"
////|| A.v_ServiceId == "N009-SR000006340"
////|| A.v_ServiceId == "N009-SR000006356"
////|| A.v_ServiceId == "N009-SR000006341"
////|| A.v_ServiceId == "N009-SR000006343"
////|| A.v_ServiceId == "N009-SR000006357"
////|| A.v_ServiceId == "N009-SR000006358"
////|| A.v_ServiceId == "N009-SR000006359"
////|| A.v_ServiceId == "N009-SR000006327"
////|| A.v_ServiceId == "N009-SR000006364"
////|| A.v_ServiceId == "N009-SR000006371"
////|| A.v_ServiceId == "N009-SR000006372"
////|| A.v_ServiceId == "N009-SR000006374"
////|| A.v_ServiceId == "N009-SR000006375"
////|| A.v_ServiceId == "N009-SR000006366"
////|| A.v_ServiceId == "N009-SR000006361"
////|| A.v_ServiceId == "N009-SR000006369"
////|| A.v_ServiceId == "N009-SR000006377"
////|| A.v_ServiceId == "N009-SR000006362"
////|| A.v_ServiceId == "N009-SR000006378"
////|| A.v_ServiceId == "N009-SR000006379"
////|| A.v_ServiceId == "N009-SR000006380"
////|| A.v_ServiceId == "N009-SR000006365"
////|| A.v_ServiceId == "N009-SR000006381"
////|| A.v_ServiceId == "N009-SR000006370"
////|| A.v_ServiceId == "N009-SR000006363"
////|| A.v_ServiceId == "N009-SR000006383"
////|| A.v_ServiceId == "N009-SR000006384"
////|| A.v_ServiceId == "N009-SR000006385"
////|| A.v_ServiceId == "N009-SR000006386"
////|| A.v_ServiceId == "N009-SR000006392"
////|| A.v_ServiceId == "N009-SR000006387"
////|| A.v_ServiceId == "N009-SR000006432"
////|| A.v_ServiceId == "N009-SR000006408"
////|| A.v_ServiceId == "N009-SR000006433"
////|| A.v_ServiceId == "N009-SR000006434"
////|| A.v_ServiceId == "N009-SR000006401"
////|| A.v_ServiceId == "N009-SR000006407"
////|| A.v_ServiceId == "N009-SR000006403"
////|| A.v_ServiceId == "N009-SR000006393"
////|| A.v_ServiceId == "N009-SR000006417"
////|| A.v_ServiceId == "N009-SR000006435"
////|| A.v_ServiceId == "N009-SR000006399"
////|| A.v_ServiceId == "N009-SR000006418"
////|| A.v_ServiceId == "N009-SR000006406"
////|| A.v_ServiceId == "N009-SR000006410"
////|| A.v_ServiceId == "N009-SR000006436"
////|| A.v_ServiceId == "N009-SR000006437"
////|| A.v_ServiceId == "N009-SR000006431"
////|| A.v_ServiceId == "N009-SR000006428"
////|| A.v_ServiceId == "N009-SR000006425"
////|| A.v_ServiceId == "N009-SR000006429"
////|| A.v_ServiceId == "N009-SR000006426"
////|| A.v_ServiceId == "N009-SR000006438"
////|| A.v_ServiceId == "N009-SR000006439"
////|| A.v_ServiceId == "N009-SR000006424"
////|| A.v_ServiceId == "N009-SR000006440"
////|| A.v_ServiceId == "N009-SR000006441"
////|| A.v_ServiceId == "N009-SR000006411"
////|| A.v_ServiceId == "N009-SR000006430"
////|| A.v_ServiceId == "N009-SR000006397"
////|| A.v_ServiceId == "N009-SR000006404"
////|| A.v_ServiceId == "N009-SR000006402"
////|| A.v_ServiceId == "N009-SR000006400"
////|| A.v_ServiceId == "N009-SR000006442"
////|| A.v_ServiceId == "N009-SR000006395"
////|| A.v_ServiceId == "N009-SR000006423"
////|| A.v_ServiceId == "N009-SR000006396"
////|| A.v_ServiceId == "N009-SR000006443"
////|| A.v_ServiceId == "N009-SR000006422"
////|| A.v_ServiceId == "N009-SR000006419"
////|| A.v_ServiceId == "N009-SR000006444"
////|| A.v_ServiceId == "N009-SR000006427"
////|| A.v_ServiceId == "N009-SR000006445"
////|| A.v_ServiceId == "N009-SR000006446"
////|| A.v_ServiceId == "N009-SR000006447"
////|| A.v_ServiceId == "N009-SR000006448"
////|| A.v_ServiceId == "N009-SR000006398"
////|| A.v_ServiceId == "N009-SR000006394"
////|| A.v_ServiceId == "N009-SR000006449"
////|| A.v_ServiceId == "N009-SR000006452"
////|| A.v_ServiceId == "N009-SR000006450"
////|| A.v_ServiceId == "N009-SR000006451"
////|| A.v_ServiceId == "N009-SR000006453"
////|| A.v_ServiceId == "N009-SR000006454"
////|| A.v_ServiceId == "N009-SR000006455"
////|| A.v_ServiceId == "N009-SR000006456"
////|| A.v_ServiceId == "N009-SR000006457"
////|| A.v_ServiceId == "N009-SR000006458"
////|| A.v_ServiceId == "N009-SR000006459"
////|| A.v_ServiceId == "N009-SR000006460"
////|| A.v_ServiceId == "N009-SR000006461"
////|| A.v_ServiceId == "N009-SR000006462"
////|| A.v_ServiceId == "N009-SR000006463"
////|| A.v_ServiceId == "N009-SR000006464"
////|| A.v_ServiceId == "N009-SR000006465"
////|| A.v_ServiceId == "N009-SR000006468"
////|| A.v_ServiceId == "N009-SR000006469"
////|| A.v_ServiceId == "N009-SR000006470"
////|| A.v_ServiceId == "N009-SR000006471"
////|| A.v_ServiceId == "N009-SR000006472"
////|| A.v_ServiceId == "N009-SR000006473"
////|| A.v_ServiceId == "N009-SR000006474"
////|| A.v_ServiceId == "N009-SR000006475"
////|| A.v_ServiceId == "N009-SR000006476"
////|| A.v_ServiceId == "N009-SR000006477"
////|| A.v_ServiceId == "N009-SR000006478"
////|| A.v_ServiceId == "N009-SR000006480"
////|| A.v_ServiceId == "N009-SR000006481"
////|| A.v_ServiceId == "N009-SR000006482"
////|| A.v_ServiceId == "N009-SR000006483"
////|| A.v_ServiceId == "N009-SR000006484"
////|| A.v_ServiceId == "N009-SR000006486"
////|| A.v_ServiceId == "N009-SR000006485"
////|| A.v_ServiceId == "N009-SR000006487"
////|| A.v_ServiceId == "N009-SR000006490"
////|| A.v_ServiceId == "N009-SR000006489"
////|| A.v_ServiceId == "N009-SR000006488"
////|| A.v_ServiceId == "N009-SR000006491"
////|| A.v_ServiceId == "N009-SR000006492"
////|| A.v_ServiceId == "N009-SR000006493"
////|| A.v_ServiceId == "N009-SR000006494"
////|| A.v_ServiceId == "N009-SR000006500"
////|| A.v_ServiceId == "N009-SR000006496"
////|| A.v_ServiceId == "N009-SR000006508"
////|| A.v_ServiceId == "N009-SR000006509"
////|| A.v_ServiceId == "N009-SR000006510"
////|| A.v_ServiceId == "N009-SR000006511"
////|| A.v_ServiceId == "N009-SR000006512"
////|| A.v_ServiceId == "N009-SR000006513"
////|| A.v_ServiceId == "N009-SR000006514"
////|| A.v_ServiceId == "N009-SR000006515"
////|| A.v_ServiceId == "N009-SR000006519"
////|| A.v_ServiceId == "N009-SR000006521"
////|| A.v_ServiceId == "N009-SR000006495"
////|| A.v_ServiceId == "N009-SR000006520"
////|| A.v_ServiceId == "N009-SR000006507"
////|| A.v_ServiceId == "N009-SR000006502"
////|| A.v_ServiceId == "N009-SR000006518"
////|| A.v_ServiceId == "N009-SR000006522"
////|| A.v_ServiceId == "N009-SR000006504"
////|| A.v_ServiceId == "N009-SR000006503"
////|| A.v_ServiceId == "N009-SR000006506"
////|| A.v_ServiceId == "N009-SR000006523"
////|| A.v_ServiceId == "N009-SR000006524"
////|| A.v_ServiceId == "N009-SR000006525"
////|| A.v_ServiceId == "N009-SR000006517"
////|| A.v_ServiceId == "N009-SR000006526"
////|| A.v_ServiceId == "N009-SR000006499"
////|| A.v_ServiceId == "N009-SR000006527"
////|| A.v_ServiceId == "N009-SR000006528"
////|| A.v_ServiceId == "N009-SR000006498"
////|| A.v_ServiceId == "N009-SR000006529"
////|| A.v_ServiceId == "N009-SR000006530"
////|| A.v_ServiceId == "N009-SR000006531"
////|| A.v_ServiceId == "N009-SR000006532"
////|| A.v_ServiceId == "N009-SR000006533"
////|| A.v_ServiceId == "N009-SR000006534"
////|| A.v_ServiceId == "N009-SR000006535"
////|| A.v_ServiceId == "N009-SR000006501"
////|| A.v_ServiceId == "N009-SR000006536"
////|| A.v_ServiceId == "N009-SR000006537"
////|| A.v_ServiceId == "N009-SR000006538"
////|| A.v_ServiceId == "N009-SR000006546"
////|| A.v_ServiceId == "N009-SR000006547"
////|| A.v_ServiceId == "N009-SR000006548"
////|| A.v_ServiceId == "N009-SR000006549"
////|| A.v_ServiceId == "N009-SR000006550"
////|| A.v_ServiceId == "N009-SR000006551"
////|| A.v_ServiceId == "N009-SR000006558"
////|| A.v_ServiceId == "N009-SR000006553"
////|| A.v_ServiceId == "N009-SR000006552"
////|| A.v_ServiceId == "N009-SR000006559"
////|| A.v_ServiceId == "N009-SR000006554"
////|| A.v_ServiceId == "N009-SR000006560"
////|| A.v_ServiceId == "N009-SR000006557"
////|| A.v_ServiceId == "N009-SR000006556"
////|| A.v_ServiceId == "N009-SR000006561"
////|| A.v_ServiceId == "N009-SR000006563"
////|| A.v_ServiceId == "N009-SR000006562"
////|| A.v_ServiceId == "N009-SR000006564"
////|| A.v_ServiceId == "N009-SR000006565"
////|| A.v_ServiceId == "N009-SR000006566"
////|| A.v_ServiceId == "N009-SR000006567"
////|| A.v_ServiceId == "N009-SR000006568"
////|| A.v_ServiceId == "N009-SR000006569"
////|| A.v_ServiceId == "N009-SR000006570"
////|| A.v_ServiceId == "N009-SR000006571"
////|| A.v_ServiceId == "N009-SR000006575"
////|| A.v_ServiceId == "N009-SR000006576"
////|| A.v_ServiceId == "N009-SR000006577"
////|| A.v_ServiceId == "N009-SR000006578"
////|| A.v_ServiceId == "N009-SR000006579"
////|| A.v_ServiceId == "N009-SR000006580"
////|| A.v_ServiceId == "N009-SR000006581"
////|| A.v_ServiceId == "N009-SR000006582"
////|| A.v_ServiceId == "N009-SR000006583"
////|| A.v_ServiceId == "N009-SR000006584"
////|| A.v_ServiceId == "N009-SR000006585"
////|| A.v_ServiceId == "N009-SR000006586"
////|| A.v_ServiceId == "N009-SR000006588"
////|| A.v_ServiceId == "N009-SR000006589"
////|| A.v_ServiceId == "N009-SR000006590"
////|| A.v_ServiceId == "N009-SR000006591"
////|| A.v_ServiceId == "N009-SR000006592"
////|| A.v_ServiceId == "N009-SR000006593"
////|| A.v_ServiceId == "N009-SR000006594"
////|| A.v_ServiceId == "N009-SR000006595"
////|| A.v_ServiceId == "N009-SR000006596"
////|| A.v_ServiceId == "N009-SR000006597"
////|| A.v_ServiceId == "N009-SR000006598"
////|| A.v_ServiceId == "N009-SR000006599"
////|| A.v_ServiceId == "N009-SR000006600"
////|| A.v_ServiceId == "N009-SR000006601"
////|| A.v_ServiceId == "N009-SR000006603"
////|| A.v_ServiceId == "N009-SR000006604"
////|| A.v_ServiceId == "N009-SR000006605"
////|| A.v_ServiceId == "N009-SR000006606"
////|| A.v_ServiceId == "N009-SR000006607"
////|| A.v_ServiceId == "N009-SR000006608"
////|| A.v_ServiceId == "N009-SR000006609"
////|| A.v_ServiceId == "N009-SR000006610"
////|| A.v_ServiceId == "N009-SR000006611"
////|| A.v_ServiceId == "N009-SR000006612"
////|| A.v_ServiceId == "N009-SR000006615"
////|| A.v_ServiceId == "N009-SR000006616"
////|| A.v_ServiceId == "N009-SR000006617"
////|| A.v_ServiceId == "N009-SR000006618"
////|| A.v_ServiceId == "N009-SR000006619"
////|| A.v_ServiceId == "N009-SR000006620"
////|| A.v_ServiceId == "N009-SR000006621"
////|| A.v_ServiceId == "N009-SR000006622"
////|| A.v_ServiceId == "N009-SR000006623"
////|| A.v_ServiceId == "N009-SR000006624"
////|| A.v_ServiceId == "N009-SR000006625"
////|| A.v_ServiceId == "N009-SR000006626"
////|| A.v_ServiceId == "N009-SR000006627"
////|| A.v_ServiceId == "N009-SR000006628"
////|| A.v_ServiceId == "N009-SR000006629"
////|| A.v_ServiceId == "N009-SR000006630"
////|| A.v_ServiceId == "N009-SR000006631"
////|| A.v_ServiceId == "N009-SR000006632"
////|| A.v_ServiceId == "N009-SR000006633"
////|| A.v_ServiceId == "N009-SR000006634"
////|| A.v_ServiceId == "N009-SR000006635"
////|| A.v_ServiceId == "N009-SR000006636"
////|| A.v_ServiceId == "N009-SR000006637"
////|| A.v_ServiceId == "N009-SR000006638"
////|| A.v_ServiceId == "N009-SR000006640"
////|| A.v_ServiceId == "N009-SR000006614"
////|| A.v_ServiceId == "N009-SR000006641"
////|| A.v_ServiceId == "N009-SR000006642"
////|| A.v_ServiceId == "N009-SR000006643"
////|| A.v_ServiceId == "N009-SR000006644"
////|| A.v_ServiceId == "N009-SR000006645"
////|| A.v_ServiceId == "N009-SR000006646"
////|| A.v_ServiceId == "N009-SR000006647"
////|| A.v_ServiceId == "N009-SR000006648"
////|| A.v_ServiceId == "N009-SR000006649"
////|| A.v_ServiceId == "N009-SR000006650"
////|| A.v_ServiceId == "N009-SR000006652"
////|| A.v_ServiceId == "N009-SR000006654"
////|| A.v_ServiceId == "N009-SR000006664"
////|| A.v_ServiceId == "N009-SR000006665"
////|| A.v_ServiceId == "N009-SR000006669"
////|| A.v_ServiceId == "N009-SR000006670"
////|| A.v_ServiceId == "N009-SR000006671"
////|| A.v_ServiceId == "N009-SR000006672"
////|| A.v_ServiceId == "N009-SR000006673"
////|| A.v_ServiceId == "N009-SR000006661"
////|| A.v_ServiceId == "N009-SR000006662"
////|| A.v_ServiceId == "N009-SR000006663"
////|| A.v_ServiceId == "N009-SR000006674"
////|| A.v_ServiceId == "N009-SR000006675"
////|| A.v_ServiceId == "N009-SR000006676"
////|| A.v_ServiceId == "N009-SR000006658"
////|| A.v_ServiceId == "N009-SR000006660"
////|| A.v_ServiceId == "N009-SR000006657"
////|| A.v_ServiceId == "N009-SR000006677"
////|| A.v_ServiceId == "N009-SR000006678"
////|| A.v_ServiceId == "N009-SR000006666"
////|| A.v_ServiceId == "N009-SR000006684"
////|| A.v_ServiceId == "N009-SR000006683"
////|| A.v_ServiceId == "N009-SR000006680"
////|| A.v_ServiceId == "N009-SR000006685"
////|| A.v_ServiceId == "N009-SR000006679"
////|| A.v_ServiceId == "N009-SR000006688"
////|| A.v_ServiceId == "N009-SR000006681"
////|| A.v_ServiceId == "N009-SR000006689"
////|| A.v_ServiceId == "N009-SR000006682"
////|| A.v_ServiceId == "N009-SR000006695"
////|| A.v_ServiceId == "N009-SR000006690"
////|| A.v_ServiceId == "N009-SR000006693"
////|| A.v_ServiceId == "N009-SR000006696"
////|| A.v_ServiceId == "N009-SR000006697"
////|| A.v_ServiceId == "N009-SR000006698"
////|| A.v_ServiceId == "N009-SR000006699"
////|| A.v_ServiceId == "N009-SR000006700"
////|| A.v_ServiceId == "N009-SR000006701"
////|| A.v_ServiceId == "N009-SR000006702"
////|| A.v_ServiceId == "N009-SR000006703"
////|| A.v_ServiceId == "N009-SR000006704"
////|| A.v_ServiceId == "N009-SR000006705"
////|| A.v_ServiceId == "N009-SR000006712"
////|| A.v_ServiceId == "N009-SR000006713"
////|| A.v_ServiceId == "N009-SR000006714"
////|| A.v_ServiceId == "N009-SR000006715"
////|| A.v_ServiceId == "N009-SR000006716"
////|| A.v_ServiceId == "N009-SR000006717"
////|| A.v_ServiceId == "N009-SR000006719"
////|| A.v_ServiceId == "N009-SR000006721"
////|| A.v_ServiceId == "N009-SR000006722"
////|| A.v_ServiceId == "N009-SR000006718"
////|| A.v_ServiceId == "N009-SR000006720"
////|| A.v_ServiceId == "N009-SR000006723"
////|| A.v_ServiceId == "N009-SR000006724"
////|| A.v_ServiceId == "N009-SR000006725"
////|| A.v_ServiceId == "N009-SR000006726"
////|| A.v_ServiceId == "N009-SR000006727"
////|| A.v_ServiceId == "N009-SR000006728"
////|| A.v_ServiceId == "N009-SR000006729"
////|| A.v_ServiceId == "N009-SR000006730"
////|| A.v_ServiceId == "N009-SR000006731"
////|| A.v_ServiceId == "N009-SR000006732"
////|| A.v_ServiceId == "N009-SR000006733"
////|| A.v_ServiceId == "N009-SR000006734"
////|| A.v_ServiceId == "N009-SR000006735"
////|| A.v_ServiceId == "N009-SR000006736"
////|| A.v_ServiceId == "N009-SR000006737"
////|| A.v_ServiceId == "N009-SR000006738"
////|| A.v_ServiceId == "N009-SR000006740"
////|| A.v_ServiceId == "N009-SR000006742"
////|| A.v_ServiceId == "N009-SR000006739"
////|| A.v_ServiceId == "N009-SR000006741"
////|| A.v_ServiceId == "N009-SR000006710"
////|| A.v_ServiceId == "N009-SR000006708"
////|| A.v_ServiceId == "N009-SR000006754"
////|| A.v_ServiceId == "N009-SR000006756"
////|| A.v_ServiceId == "N009-SR000006757"
////|| A.v_ServiceId == "N009-SR000006758"
////|| A.v_ServiceId == "N009-SR000006746"
////|| A.v_ServiceId == "N009-SR000006760"
////|| A.v_ServiceId == "N009-SR000006761"
////|| A.v_ServiceId == "N009-SR000006759"
////|| A.v_ServiceId == "N009-SR000006747"
////|| A.v_ServiceId == "N009-SR000006762"
////|| A.v_ServiceId == "N009-SR000006765"
////|| A.v_ServiceId == "N009-SR000006764"
////|| A.v_ServiceId == "N009-SR000006748"
////|| A.v_ServiceId == "N009-SR000006766"
////|| A.v_ServiceId == "N009-SR000006767"
////|| A.v_ServiceId == "N009-SR000006771"
////|| A.v_ServiceId == "N009-SR000006772"
////|| A.v_ServiceId == "N009-SR000006775"
////|| A.v_ServiceId == "N009-SR000006776"
////|| A.v_ServiceId == "N009-SR000006773"
////|| A.v_ServiceId == "N009-SR000006774"
////|| A.v_ServiceId == "N009-SR000006780"
////|| A.v_ServiceId == "N009-SR000006779"
////|| A.v_ServiceId == "N009-SR000006777"
////|| A.v_ServiceId == "N009-SR000006778"
////|| A.v_ServiceId == "N009-SR000006783"
////|| A.v_ServiceId == "N009-SR000006782"
////|| A.v_ServiceId == "N009-SR000006781"
////|| A.v_ServiceId == "N009-SR000006784"
////|| A.v_ServiceId == "N009-SR000006785"
////|| A.v_ServiceId == "N009-SR000006786"
////|| A.v_ServiceId == "N009-SR000006787"
////|| A.v_ServiceId == "N009-SR000006788"
////|| A.v_ServiceId == "N009-SR000006790"
////|| A.v_ServiceId == "N009-SR000006791"
////|| A.v_ServiceId == "N009-SR000006794"
////|| A.v_ServiceId == "N009-SR000006793"
////|| A.v_ServiceId == "N009-SR000006795"
////|| A.v_ServiceId == "N009-SR000006792"
////|| A.v_ServiceId == "N009-SR000006796"
////|| A.v_ServiceId == "N009-SR000006797"
////|| A.v_ServiceId == "N009-SR000006798"
////|| A.v_ServiceId == "N009-SR000006799"
////|| A.v_ServiceId == "N009-SR000006801"
////|| A.v_ServiceId == "N009-SR000006800"
////|| A.v_ServiceId == "N009-SR000006802"
////|| A.v_ServiceId == "N009-SR000006803"
////|| A.v_ServiceId == "N009-SR000006804"
////|| A.v_ServiceId == "N009-SR000006805"
////|| A.v_ServiceId == "N009-SR000006806"
////|| A.v_ServiceId == "N009-SR000006808"
////|| A.v_ServiceId == "N009-SR000006809"
////|| A.v_ServiceId == "N009-SR000006807"
////|| A.v_ServiceId == "N009-SR000006810"
////|| A.v_ServiceId == "N009-SR000006811"
////|| A.v_ServiceId == "N009-SR000006812"
////|| A.v_ServiceId == "N009-SR000006813"
////|| A.v_ServiceId == "N009-SR000006814"










////                             select new ServiceList
////                             {
////                                 v_ServiceId = A.v_ServiceId,
////                                 v_ProtocolId = A.v_ProtocolId,
////                                 v_DocNumber = B.v_DocNumber,
////                                 d_EndDateRestriction = A.d_EndDateRestriction,
////                                 d_FechaEntrega = A.d_FechaEntrega,
////                                 d_Fur = A.d_Fur,
////                                 d_GlobalExpirationDate = A.d_GlobalExpirationDate,
////                                 d_InsertDateMedicalAnalyst = A.d_InsertDateMedicalAnalyst,
////                                 d_InsertDateOccupationalMedical = A.d_InsertDateOccupationalMedical,
////                                 d_Mamografia = A.d_Mamografia,
////                                 d_MedicalBreakEndDate = A.d_MedicalBreakEndDate,
////                                 d_MedicalBreakStartDate = A.d_MedicalBreakStartDate,
////                                 d_NextAppointment = A.d_NextAppointment,
////                                 d_ObsExpirationDate = A.d_ObsExpirationDate,
////                                 d_PAP = A.d_PAP,
////                                 d_ServiceDate = A.d_ServiceDate,
////                                 d_StartDateRestriction = A.d_StartDateRestriction,
////                                 d_UpdateDate = A.d_UpdateDate,
////                                 d_UpdateDateMedicalAnalyst = A.d_UpdateDateMedicalAnalyst,
////                                 d_UpdateDateOccupationalMedical = A.d_UpdateDateOccupationalMedical,
////                                 i_AppetiteId = A.i_AppetiteId,
////                                 i_AptitudeStatusId = A.i_AptitudeStatusId,
////                                 i_CursoEnf = A.i_CursoEnf,
////                                 i_DepositionId = A.i_DepositionId,
////                                 i_DestinationMedicationId = A.i_DestinationMedicationId,
////                                 i_DreamId = A.i_DreamId,
////                                 i_Evolucion = A.i_Evolucion,
////                                 i_FlagAgentId = A.i_FlagAgentId,
////                                 i_HasMedicalBreakId = A.i_HasMedicalBreakId,
////                                 i_HasRestrictionId = A.i_HasRestrictionId,
////                                 i_HasSymptomId = A.i_HasSymptomId,
////                                 i_HazInterconsultationId = A.i_HazInterconsultationId,
////                                 i_InicioEnf = A.i_InicioEnf,
////                                 i_InsertUserMedicalAnalystId = A.i_InsertUserMedicalAnalystId,
////                                 i_InsertUserOccupationalMedicalId = A.i_InsertUserOccupationalMedicalId,
////                                 i_IsDeleted = A.i_IsDeleted,
////                                 i_IsFac = A.i_IsFac,
////                                 i_IsNewControl = A.i_IsNewControl,
////                                 i_MacId = A.i_MacId,
////                                 i_MasterServiceId = A.i_MasterServiceId,
////                                 i_ModalityOfInsurance = A.i_ModalityOfInsurance,
////                                 i_SendToTracking = A.i_SendToTracking,
////                                 i_ServiceStatusId = A.i_ServiceStatusId,
////                                 i_ServiceTypeOfInsurance = A.i_ServiceTypeOfInsurance,
////                                 i_StatusLiquidation = A.i_StatusLiquidation,
////                                 i_ThirstId = A.i_ThirstId,
////                                 i_TimeOfDisease = A.i_TimeOfDisease,
////                                 i_TimeOfDiseaseTypeId = A.i_TimeOfDiseaseTypeId,
////                                 i_TransportMedicationId = A.i_TransportMedicationId,
////                                 i_UpdateUserMedicalAnalystId = A.i_UpdateUserMedicalAnalystId,
////                                 i_UpdateUserOccupationalMedicaltId = A.i_UpdateUserOccupationalMedicaltId,
////                                 i_UrineId = A.i_UrineId,
////                                 //r_Costo = A.r_Costo.Value,
////                                 v_AreaId = A.v_AreaId,
////                                 v_CatemenialRegime = A.v_CatemenialRegime,
////                                 v_CiruGine = A.v_CiruGine,
////                                 v_ExaAuxResult = A.v_ExaAuxResult,
////                                 v_FechaUltimaMamo = A.v_FechaUltimaMamo,
////                                 v_FechaUltimoPAP = A.v_FechaUltimoPAP,
////                                 v_Findings = A.v_Findings,
////                                 v_GeneralRecomendations = A.v_GeneralRecomendations,
////                                 v_Gestapara = A.v_Gestapara,
////                                 v_LocationId = A.v_LocationId,
////                                 v_MainSymptom = A.v_MainSymptom,
////                                 v_Menarquia = A.v_Menarquia,
////                                 v_Motive = A.v_Motive,
////                                 v_ObsStatusService = A.v_ObsStatusService,
////                                 v_OrganizationId = A.v_OrganizationId,
////                                 v_PersonId = A.v_PersonId,
////                                 v_ResultadoMamo = A.v_ResultadoMamo,
////                                 v_ResultadosPAP = A.v_ResultadosPAP,
////                                 v_Story = A.v_Story

////                             }).ToList();

//                return null;

//            //}
//            //catch (Exception ex)
//            //{

//            //    return null;
//            //}
//        }

//        //public location_oldDto GetLocation_OLD(ref OperationResult pobjOperationResult, string pstrEmpresaIdOLD)
//        //{
//        //    //mon.IsActive = true;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        location_oldDto objDtoEntity = null;

//        //        var objEntity = (from a in dbContext.location_old
//        //                         where a.v_OrganizationId == pstrEmpresaIdOLD
//        //                         select a).FirstOrDefault();

//        //        if (objEntity != null)
//        //            objDtoEntity = location_oldAssembler.ToDTO(objEntity);

//        //        pobjOperationResult.Success = 1;
//        //        return objDtoEntity;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        pobjOperationResult.Success = 0;
//        //        pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
//        //        return null;
//        //    }
//        //}

//        //public groupoccupation_oldDto GetGroupOccupation_OLD(ref OperationResult pobjOperationResult, string pstrLocationIdOLD)
//        //{
//        //    //mon.IsActive = true;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        groupoccupation_oldDto objDtoEntity = null;

//        //        var objEntity = (from a in dbContext.groupoccupation_old
//        //                         where a.v_LocationId == pstrLocationIdOLD
//        //                         select a).FirstOrDefault();

//        //        if (objEntity != null)
//        //            objDtoEntity = groupoccupation_oldAssembler.ToDTO(objEntity);

//        //        pobjOperationResult.Success = 1;
//        //        return objDtoEntity;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        pobjOperationResult.Success = 0;
//        //        pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
//        //        return null;
//        //    }
//        //}

//        //public string DevolverProtocoloOLD(string pstrProtocolOLDId, List<string> ClientSession)
//        //{
//        //    OperationResult _objOperationResult = new OperationResult();
//        //    string ProtocolId = "";
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

//        //        //obtner datos del protocolo antiguo para buscar en la nueva bd
//        //        var objEntityOLD = (from a in dbContext.protocol_old
//        //                            where a.v_ProtocolId == pstrProtocolOLDId
//        //                            select a).FirstOrDefault();

//        //        //buscar el protocolo antiguo en la nueva BD por su nombre
//        //        var objEntityNuevo = (from a in dbContext.protocol
//        //                              where a.v_Name == objEntityOLD.v_Name.Trim()
//        //                              select a).FirstOrDefault();

//        //        if (objEntityNuevo == null)// si no lo encuentra crear un nuevo protocolo con detalle y devolver el Id
//        //        {
//        //            ProtocolBL oProtocolBL = new ProtocolBL();
//        //            protocolDto oprotocolDto = new protocolDto();
//        //            List<protocolcomponentDto> oListaprotocolcomponentDto = new List<protocolcomponentDto>();
//        //            protocolcomponentDto oprotocolcomponentDto;

//        //            oprotocolDto.v_Name = objEntityOLD.v_Name.ToString().Trim();

//        //            var JerarquiaEmpresaEmpleado = DevolverEmpresaIDNuevo(objEntityOLD.v_EmployerOrganizationId);
//        //            oprotocolDto.v_EmployerOrganizationId = JerarquiaEmpresaEmpleado.v_OrganizationId;

//        //            var LocationEmployerOLD = GetLocation_OLD(ref _objOperationResult, objEntityOLD.v_EmployerOrganizationId);
//        //            var aa = JerarquiaEmpresaEmpleado.Sedes.Find(p => p.v_Name.ToString().Trim() == LocationEmployerOLD.v_Name.ToString().Trim());
//        //            var v_EmployerLocationId_Nuevo = aa == null ? JerarquiaEmpresaEmpleado.Sedes[0].v_LocationId : aa.v_LocationId;
//        //            oprotocolDto.v_EmployerLocationId = v_EmployerLocationId_Nuevo;

//        //            oprotocolDto.i_EsoTypeId = objEntityOLD.i_EsoTypeId;

//        //            var JerarquiaEmpresaCliente = DevolverEmpresaIDNuevo(objEntityOLD.v_CustomerOrganizationId);
//        //            oprotocolDto.v_CustomerOrganizationId = JerarquiaEmpresaCliente.v_OrganizationId;

//        //            var LocationClienteOLD = GetLocation_OLD(ref _objOperationResult, objEntityOLD.v_CustomerOrganizationId);
//        //            var v_CustomerLocationIdCliente_Nuevo = JerarquiaEmpresaCliente.Sedes.Find(p => p.v_Name.ToString().Trim() == LocationClienteOLD.v_Name.ToString().Trim());
//        //            oprotocolDto.v_CustomerLocationId = v_CustomerLocationIdCliente_Nuevo == null ? JerarquiaEmpresaCliente.Sedes[0].v_LocationId : v_CustomerLocationIdCliente_Nuevo.v_LocationId;



//        //            oprotocolDto.v_NombreVendedor = objEntityOLD.v_NombreVendedor;
//        //            var JerarquiaEmpresaTrabajo = DevolverEmpresaIDNuevo(objEntityOLD.v_WorkingOrganizationId);
//        //            oprotocolDto.v_WorkingOrganizationId = JerarquiaEmpresaTrabajo.v_OrganizationId;

//        //            var LocationTrabajoOLD = GetLocation_OLD(ref _objOperationResult, objEntityOLD.v_WorkingOrganizationId);
//        //            var v_LocationIdTrabajo_Nuevo = JerarquiaEmpresaTrabajo.Sedes.Find(p => p.v_Name.ToString().Trim() == LocationTrabajoOLD.v_Name.ToString().Trim());
//        //            oprotocolDto.v_WorkingLocationId = v_LocationIdTrabajo_Nuevo == null ? JerarquiaEmpresaTrabajo.Sedes[0].v_LocationId : v_LocationIdTrabajo_Nuevo.v_LocationId;


//        //            var v_GroupOccupationOLD = GetGroupOccupation_OLD(ref _objOperationResult, objEntityOLD.v_EmployerLocationId);
//        //            var v_GroupOccupationId_Nuevo = JerarquiaEmpresaEmpleado.GESOS.Find(p => p.v_Name.ToString().Trim() == v_GroupOccupationOLD.v_Name.ToString().Trim());
//        //            oprotocolDto.v_GroupOccupationId = v_GroupOccupationId_Nuevo == null ? JerarquiaEmpresaEmpleado.GESOS[0].v_GroupOccupationId : v_GroupOccupationId_Nuevo.v_GroupOccupationId;



//        //            oprotocolDto.v_CostCenter = objEntityOLD.v_CostCenter;
//        //            oprotocolDto.i_MasterServiceTypeId = objEntityOLD.i_MasterServiceTypeId;
//        //            oprotocolDto.i_MasterServiceId = objEntityOLD.i_MasterServiceId;
//        //            oprotocolDto.i_HasVigency = objEntityOLD.i_HasVigency;
//        //            oprotocolDto.i_ValidInDays = objEntityOLD.i_ValidInDays;
//        //            oprotocolDto.i_IsActive = objEntityOLD.i_IsActive;

//        //            //Obtener Detalle protocolo

//        //            var ListaProtocolComponentOLD = DevolverProtocomponentOLD(objEntityOLD.v_ProtocolId);

//        //            foreach (var item in ListaProtocolComponentOLD)
//        //            {
//        //                oprotocolcomponentDto = new protocolcomponentDto();
//        //                oprotocolcomponentDto.v_ComponentId = item.v_ComponentId;
//        //                oprotocolcomponentDto.r_Price = item.r_Price;
//        //                oprotocolcomponentDto.i_OperatorId = item.i_OperatorId;
//        //                oprotocolcomponentDto.i_Age = item.i_Age;
//        //                oprotocolcomponentDto.i_GenderId = item.i_GenderId;

//        //                oprotocolcomponentDto.i_IsConditionalId = item.i_IsConditionalId;
//        //                oprotocolcomponentDto.i_IsConditionalIMC = item.i_IsConditionalIMC;
//        //                oprotocolcomponentDto.r_Imc = item.r_Imc;
//        //                oprotocolcomponentDto.i_IsAdditional = item.i_isAdditional;
//        //                oListaprotocolcomponentDto.Add(oprotocolcomponentDto);

//        //            }

//        //            ProtocolId = oProtocolBL.AddProtocol(ref _objOperationResult, oprotocolDto, oListaprotocolcomponentDto, ClientSession);


//        //        }
//        //        else// devolver el nuevo id del protocolo
//        //        {
//        //            ProtocolId = objEntityNuevo.v_ProtocolId;
//        //        }


//        //        return ProtocolId;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public EmpresaMigracionList DevolverEmpresaIDNuevo(string pstrEmpresaIdOLD)
//        //{
//        //    SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //    var query = (from A in dbContext.organization_old
//        //                 where A.i_IsDeleted == 0 && A.v_OrganizationId == pstrEmpresaIdOLD
//        //                 select new OrganizationList
//        //                 {
//        //                     v_IdentificationNumber = A.v_IdentificationNumber
//        //                 }).FirstOrDefault();

//        //    return (VerificarSiExisteEmpresaAntigua_(query.v_IdentificationNumber));

//        //}

//        //public List<ProtocolComponentList> DevolverProtocomponentOLD(string pstrProtocolId_OLD)
//        //{
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

//        //        var objEntity = (from a in dbContext.protocolcomponent_old

//        //                         where a.v_ProtocolId == pstrProtocolId_OLD
//        //                         && a.i_IsDeleted == 0

//        //                         select new ProtocolComponentList
//        //                         {
//        //                             v_ProtocolComponentId = a.v_ProtocolComponentId,
//        //                             v_ProtocolId = a.v_ProtocolId,
//        //                             v_ComponentId = a.v_ComponentId,
//        //                             r_Price = a.r_Price,
//        //                             i_OperatorId = a.i_OperatorId,
//        //                             i_Age = a.i_Age,
//        //                             i_GenderId = a.i_GenderId,
//        //                             i_IsConditionalId = a.i_IsConditionalId,
//        //                             i_IsConditionalIMC = a.i_IsConditionalIMC,
//        //                             r_Imc = a.r_Imc,
//        //                             i_isAdditional = a.i_IsAdditional
//        //                         }).ToList();


//        //        return objEntity;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return null;
//        //    }
//        //}

//        //public List<ServiceComponentList> GetServiceComponents_OLD(string pstrServiceId_OLD)
//        //{
//        //    int isDeleted = (int)SiNo.NO;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

//        //        var query = (from A in dbContext.servicecomponent_old
//        //                     where A.v_ServiceId == pstrServiceId_OLD &&
//        //                           A.i_IsDeleted == isDeleted

//        //                     select new ServiceComponentList
//        //                     {
//        //                         v_ComponentId = A.v_ComponentId,
//        //                         i_ServiceComponentStatusId = A.i_ServiceComponentStatusId.Value,
//        //                         d_StartDate = A.d_StartDate.Value,
//        //                         d_EndDate = A.d_EndDate.Value,
//        //                         i_QueueStatusId = A.i_QueueStatusId.Value,
//        //                         v_ServiceComponentId = A.v_ServiceComponentId,
//        //                         d_ApprovedInsertDate = A.d_ApprovedInsertDate,
//        //                         d_ApprovedUpdateDate = A.d_ApprovedUpdateDate,
//        //                         d_CalledDate = A.d_CalledDate,
//        //                         d_InsertDateMedicalAnalyst = A.d_InsertDateMedicalAnalyst,
//        //                         d_InsertDateTechnicalDataRegister = A.d_InsertDateTechnicalDataRegister,
//        //                         d_UpdateDateMedicalAnalyst = A.d_UpdateDateMedicalAnalyst,
//        //                         d_UpdateDateTechnicalDataRegister = A.d_UpdateDateTechnicalDataRegister,
//        //                         i_ApprovedInsertUserId = A.i_ApprovedInsertUserId,
//        //                         i_ApprovedUpdateUserId = A.i_ApprovedUpdateUserId,
//        //                         i_ExternalInternalId = A.i_ExternalInternalId,
//        //                         i_index = A.i_index,
//        //                         i_InsertUserMedicalAnalystId = A.i_InsertUserMedicalAnalystId,
//        //                         i_InsertUserTechnicalDataRegisterId = A.i_InsertUserTechnicalDataRegisterId,
//        //                         i_IsApprovedId = A.i_IsApprovedId,
//        //                         i_Iscalling = A.i_Iscalling,
//        //                         i_Iscalling_1 = A.i_Iscalling_1,
//        //                         i_IsInheritedId = A.i_IsInheritedId,
//        //                         i_IsInvoicedId = A.i_IsInvoicedId,
//        //                         i_IsManuallyAddedId = A.i_IsManuallyAddedId,
//        //                         i_IsRequiredId = A.i_IsRequiredId,
//        //                         i_IsVisibleId = A.i_IsVisibleId,
//        //                         i_ServiceComponentTypeId = A.i_ServiceComponentTypeId,
//        //                         i_UpdateUserMedicalAnalystId = A.i_UpdateUserMedicalAnalystId,
//        //                         i_UpdateUserTechnicalDataRegisterId = A.i_UpdateUserTechnicalDataRegisterId,
//        //                         r_Price = A.r_Price,
//        //                         v_Comment = A.v_Comment,
//        //                         v_NameOfice = A.v_NameOfice,
//        //                         v_ServiceId = A.v_ServiceId
//        //                     });

//        //        List<ServiceComponentList> obj = query.ToList();

//        //        return obj;
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        return null;
//        //    }
//        //}

//        //public List<ServiceComponentFieldsList> GetServiceComponentFields_Y_Values_OLD(string pstrServiceComponentId_OLD, string pstrServiceComponentId_NEW, List<string> ClientSession)
//        //{
//        //    int isDeleted = (int)SiNo.NO;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

//        //        var query = (from A in dbContext.servicecomponentfields_old
//        //                     join B in dbContext.servicecomponentfieldvalues_old on A.v_ServiceComponentFieldsId equals B.v_ServiceComponentFieldsId
//        //                     where A.v_ServiceComponentId == pstrServiceComponentId_OLD &&
//        //                           A.i_IsDeleted == isDeleted

//        //                     select new ServiceComponentFieldsList
//        //                     {
//        //                         v_ServiceComponentFieldsId = A.v_ServiceComponentFieldsId,
//        //                         v_ServiceComponentId = A.v_ServiceComponentId,
//        //                         v_ComponentId = A.v_ComponentId,
//        //                         v_ComponentFieldId = A.v_ComponentFieldId,
//        //                         v_ServiceComponentFieldValuesId = B.v_ServiceComponentFieldValuesId,
//        //                         v_ComponentFieldValuesId = B.v_ComponentFieldValuesId,
//        //                         v_Value1 = B.v_Value1,

//        //                     }).ToList();

//        //        List<ServiceComponentFieldsList> obj = query.ToList();
//        //        int intNodeId = int.Parse(ClientSession[0]);
//        //        string NewIdSCF = "";
//        //        foreach (var item in query)
//        //        {

//        //            servicecomponentfields objEntity = new servicecomponentfields();

//        //            objEntity.v_ComponentFieldId = item.v_ComponentFieldsId;
//        //            objEntity.v_ServiceComponentId = pstrServiceComponentId_NEW;
//        //            objEntity.d_InsertDate = DateTime.Now;
//        //            objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
//        //            objEntity.i_IsDeleted = 0;

//        //            // Autogeneramos el Pk de la tabla               
//        //            NewIdSCF = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 35), "CF");
//        //            objEntity.v_ServiceComponentFieldsId = NewIdSCF;

//        //            dbContext.AddToservicecomponentfields(objEntity);
//        //            //dbContext.SaveChanges();

//        //            servicecomponentfieldvalues objEntity1 = new servicecomponentfieldvalues();

//        //            objEntity1.v_ComponentFieldValuesId = null;//item.v_ComponentFieldValuesId;
//        //            objEntity1.v_Value1 = item.v_Value1;
//        //            objEntity1.d_InsertDate = DateTime.Now;
//        //            objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
//        //            objEntity1.i_IsDeleted = 0;

//        //            // Autogeneramos el Pk de la tabla               
//        //            var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 36), "CV");
//        //            objEntity1.v_ServiceComponentFieldValuesId = NewId1;
//        //            objEntity1.v_ServiceComponentFieldsId = NewIdSCF;

//        //            dbContext.AddToservicecomponentfieldvalues(objEntity1);
//        //            //dbContext.SaveChanges();
//        //        }

//        //        dbContext.SaveChanges();

//        //        return obj;
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        return null;
//        //    }
//        //}

//        public string AddServiceComponent(ref OperationResult pobjOperationResult, servicecomponentDto pobjDtoEntity, List<string> ClientSession)
//        {
//            //mon.IsActive = true;
//            string NewId = "(No generado)";
//            try
//            {
//                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//                servicecomponent objEntity = servicecomponentAssembler.ToEntity(pobjDtoEntity);

//                objEntity.d_InsertDate = DateTime.Now;
//                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
//                objEntity.i_IsDeleted = 0;
//                // Autogeneramos el Pk de la tabla
//                int intNodeId = int.Parse(ClientSession[0]);
//                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 24), "SC");
//                objEntity.v_ServiceComponentId = NewId;

//                dbContext.AddToservicecomponent(objEntity);
//                dbContext.SaveChanges();

//                pobjOperationResult.Success = 1;
//                // Llenar entidad Log
//                return NewId;
//            }
//            catch (Exception ex)
//            {
//                pobjOperationResult.Success = 0;
//                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
//                // Llenar entidad Log
//                return null;
//            }
//        }

//        //public string  AddServiceComponentField(List<servicecomponentfieldsDto> lpobjDtoEntity, List<string> ClientSession)
//        //{
//        //    //mon.IsActive = true;
//        //    string NewId = "(No generado)";
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        servicecomponentfields objEntity = servicecomponentfieldsAssembler.ToEntity(pobjDtoEntity);

//        //        objEntity.d_InsertDate = DateTime.Now;
//        //        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
//        //        objEntity.i_IsDeleted = 0;
//        //        // Autogeneramos el Pk de la tabla
//        //        int intNodeId = int.Parse(ClientSession[0]);
//        //        NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 35), "CF");
//        //        objEntity.v_ServiceComponentFieldsId = NewId;

//        //        dbContext.AddToservicecomponentfields(objEntity);
//        //        dbContext.SaveChanges();
//        //        return NewId;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //         // Llenar entidad Log
//        //           return null;
//        //    }
//        //}

//        //public List<ServiceComponentFieldValuesList> GetServiceComponentFieldsValues_OLD(string pstrServiceComponentFieldsId_OLD)
//        //{
//        //    int isDeleted = (int)SiNo.NO;

//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

//        //        var query = (from A in dbContext.servicecomponentfieldvalues_old
//        //                     where A.v_ServiceComponentFieldsId == pstrServiceComponentFieldsId_OLD &&
//        //                           A.i_IsDeleted == isDeleted

//        //                     select new ServiceComponentFieldValuesList
//        //                     {
//        //                         v_ComponentFieldValuesId = A.v_ComponentFieldValuesId,
//        //                         v_ServiceComponentFieldsId = A.v_ServiceComponentFieldsId,
//        //                         v_Value1 = A.v_Value1,
//        //                         v_Value2 = A.v_Value2,
//        //                         i_Index = A.i_Index,
//        //                         i_Value1 = A.i_Value1
//        //                     });

//        //        List<ServiceComponentFieldValuesList> obj = query.ToList();

//        //        return obj;
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        return null;
//        //    }
//        //}

//        //public string AddServiceComponentFieldValues(List<servicecomponentfieldvaluesDto> ListapobjDtoEntity, List<string> ClientSession)
//        //{
//        //    //mon.IsActive = true;
//        //    string NewId = "(No generado)";
//        //    try
//        //    {
//        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        foreach (var fv in ListapobjDtoEntity)
//        //        {                    
//        //            servicecomponentfieldvalues objEntity1 = new servicecomponentfieldvalues();

//        //            objEntity1.v_ComponentFieldValuesId = fv.v_ComponentFieldValuesId;
//        //            objEntity1.v_Value1 = fv.v_Value1;
//        //            objEntity1.d_InsertDate = DateTime.Now;
//        //            objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
//        //            objEntity1.i_IsDeleted = 0;
//        //            int intNodeId = int.Parse(ClientSession[0]);
//        //            // Autogeneramos el Pk de la tabla               
//        //            var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 36), "CV");
//        //            objEntity1.v_ServiceComponentFieldValuesId = NewId1;
//        //            objEntity1.v_ServiceComponentFieldsId = NewId;

//        //            dbContext.AddToservicecomponentfieldvalues(objEntity1);
//        //        }

//        //        dbContext.SaveChanges();


//        //        //SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//        //        //servicecomponentfieldvalues objEntity = servicecomponentfieldvaluesAssembler.ToEntity(pobjDtoEntity);

//        //        //objEntity.d_InsertDate = DateTime.Now;
//        //        //objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
//        //        //objEntity.i_IsDeleted = 0;
//        //        //// Autogeneramos el Pk de la tabla
//        //        //int intNodeId = int.Parse(ClientSession[0]);

//        //        //NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 36), "CV");
//        //        //objEntity.v_ServiceComponentFieldValuesId = NewId;

//        //        //dbContext.AddToservicecomponentfieldvalues(objEntity);
//        //        //dbContext.SaveChanges();
//        //        //return NewId;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Llenar entidad Log
//        //          return null;
//        //    }
//        //}


//        #endregion

//        public List<DiagnosticRepositoryList> DevolverListaDiagnosticOLD(string pstrServiceOLD)
//        {
//            //SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
//            //var query = (from A in dbContext.diagnosticrepository_old
//            //             join B in dbContext.diseases_old on A.v_DiseasesId equals B.v_DiseasesId into B_join
//            //             from B in B_join.DefaultIfEmpty()
//            //             where A.i_IsDeleted == 0 && A.v_ServiceId == pstrServiceOLD
//            //             select new DiagnosticRepositoryList
//            //             {

//            //                 v_ServiceId = A.v_ServiceId,
//            //                 v_DiseasesId = A.v_DiseasesId,

//            //                 v_ComponentId = A.v_ComponentId,
//            //                 v_ComponentFieldId = A.v_ComponentFieldId,
//            //                 i_AutoManualId = A.i_AutoManualId.Value,
//            //                 i_PreQualificationId = A.i_PreQualificationId.Value,
//            //                 i_FinalQualificationId = A.i_FinalQualificationId.Value,

//            //                 i_DiagnosticTypeId = A.i_DiagnosticTypeId.Value,
//            //                 i_IsSentToAntecedent = A.i_IsSentToAntecedent.Value,
//            //                 d_ExpirationDateDiagnostic = A.d_ExpirationDateDiagnostic,
//            //                 i_GenerateMedicalBreak = A.i_GenerateMedicalBreak.Value,
//            //                 v_Recomendations = A.v_Recomendations,

//            //                 i_DiagnosticSourceId = A.i_DiagnosticSourceId.Value,
//            //                 i_ShapeAccidentId = A.i_ShapeAccidentId.Value,
//            //                 i_BodyPartId = A.i_BodyPartId.Value,
//            //                 i_ClassificationOfWorkAccidentId = A.i_ClassificationOfWorkAccidentId.Value,
//            //                 i_RiskFactorId = A.i_RiskFactorId.Value,

//            //                 i_ClassificationOfWorkdiseaseId = A.i_ClassificationOfWorkdiseaseId.Value,
//            //                 i_SendToInterconsultationId = A.i_SendToInterconsultationId.Value,
//            //                 i_InterconsultationDestinationId = A.i_InterconsultationDestinationId.Value,
//            //                 v_InterconsultationDestinationId = A.v_InterconsultationDestinationId,

//            //                 v_Cie10 = B.v_CIE10Id,
//            //                 v_Name = B.v_Name

//            //             }).ToList();


//            return null;
//        }

    }
}
