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
    public class PacientBL
    {
        //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
        ServiceBL serviceBL = new ServiceBL();

        #region Person

        public personDto GetPerson(ref OperationResult pobjOperationResult, string pstrPersonId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                personDto objDtoEntity = null;

                var objEntity = (from a in dbContext.person
                                 where a.v_PersonId == pstrPersonId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = personAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }      

        public string AddPerson(ref OperationResult pobjOperationResult, personDto pobjPerson, professionalDto pobjProfessional, systemuserDto pobjSystemUser, List<string> ClientSession)
        {
            //mon.IsActive = true;
            int SecuentialId = -1;
            string newId = string.Empty;
            person objEntity1 = null;

            try
            {
                #region Validations
                // Validar el DNI de la persona
                if (pobjPerson != null)
                {
                    OperationResult objOperationResult6 = new OperationResult();
                    string strfilterExpression1 = string.Format("v_DocNumber==\"{0}\"&&i_Isdeleted==0", pobjPerson.v_DocNumber);
                    var _recordCount1 = GetPersonCount(ref objOperationResult6, strfilterExpression1);

                    if (_recordCount1 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El número de documento " + pobjPerson.v_DocNumber + " ya se encuentra registrado.\nPor favor ingrese otro número de documento.";
                        return "-1";
                    }
                }

                // Validar existencia de UserName en la BD
                if (pobjSystemUser != null)
                {
                    OperationResult objOperationResult7 = new OperationResult();
                    string strfilterExpression2 = string.Format("v_UserName==\"{0}\"&&i_Isdeleted==0", pobjSystemUser.v_UserName);
                    var _recordCount2 = new SecurityBL().GetSystemUserCount(ref objOperationResult7, strfilterExpression2);

                    if (_recordCount2 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El nombre de usuario  <font color='red'>" + pobjSystemUser.v_UserName + "</font> ya se encuentra registrado.<br> Por favor ingrese otro nombre de usuario.";
                        return "-1";
                    }
                }
                #endregion

                // Grabar Persona
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                objEntity1 = personAssembler.ToEntity(pobjPerson);

                objEntity1.d_InsertDate = DateTime.Now;
                objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity1.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                SecuentialId = Utils.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 8);
                newId = Common.Utils.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "PP");
                objEntity1.v_PersonId = newId;

                dbContext.AddToperson(objEntity1);
                dbContext.SaveChanges();

                // Grabar Profesional
                if (pobjProfessional != null)
                {
                    OperationResult objOperationResult2 = new OperationResult();
                    pobjProfessional.v_PersonId = objEntity1.v_PersonId;
                    AddProfessional(ref objOperationResult2, pobjProfessional, ClientSession);
                }

                // Grabar Usuario
                if (pobjSystemUser != null)
                {
                    OperationResult objOperationResult3 = new OperationResult();
                    pobjSystemUser.v_PersonId = objEntity1.v_PersonId;
                    new SecurityBL().AddSystemUSer(ref objOperationResult3, pobjSystemUser, ClientSession);
                }

                ////Seteamos si el registro es agregado en el DataCenter o en un nodo
                //if (ClientSession[0] == "1")
                //{
                //    objEntity1.i_IsInMaster = 1;
                //}
                //else
                //{
                //    objEntity1.i_IsInMaster = 0;
                //}

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_PersonId, Success.Ok, null);
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_PersonId, Success.Failed, ex.Message);
            }

            return newId;
        }

        public string UpdatePerson(ref OperationResult pobjOperationResult, bool pbIsChangeDocNumber, personDto pobjPerson, professionalDto pobjProfessional, bool pbIsChangeUserName, systemuserDto pobjSystemUser, List<string> ClientSession)
        {

            try
            {
                #region Validate SystemUSer
                // Validar existencia de UserName en la BD
                if (pobjSystemUser != null && pbIsChangeUserName == true)
                {
                    OperationResult objOperationResult7 = new OperationResult();
                    string strfilterExpression2 = string.Format("v_UserName==\"{0}\"&&i_Isdeleted==0", pobjSystemUser.v_UserName);
                    var _recordCount2 = new SecurityBL().GetSystemUserCount(ref objOperationResult7, strfilterExpression2);

                    if (_recordCount2 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El nombre de usuario  <font color='red'>" + pobjSystemUser.v_UserName + "</font> ya se encuentra registrado.<br> Por favor ingrese otro nombre de usuario.";
                        return "-1";
                    }
                }

                #endregion

                #region Validate Document Number

                // Validar el DNI de la persona
                if (pobjPerson != null && pbIsChangeDocNumber == true)
                {
                    OperationResult objOperationResult6 = new OperationResult();
                    string strfilterExpression1 = string.Format("v_DocNumber==\"{0}\"&&i_Isdeleted==0", pobjPerson.v_DocNumber);
                    var _recordCount1 = GetPersonCount(ref objOperationResult6, strfilterExpression1);

                    if (_recordCount1 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El número de documento  <font color='red'>" + pobjPerson.v_DocNumber + "</font> ya se encuentra registrado.<br> Por favor ingrese otro número de documento.";
                        return "-1";
                    }
                }

                #endregion

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Actualiza Persona
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.person
                                       where a.v_PersonId == pobjPerson.v_PersonId
                                       select a).FirstOrDefault();

                pobjPerson.d_UpdateDate = DateTime.Now;
                pobjPerson.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                person objEntity = personAssembler.ToEntity(pobjPerson);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.person.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                // Actualiza Profesional
                if (pobjProfessional != null)
                {
                    OperationResult objOperationResult2 = new OperationResult();
                    UpdateProfessional(ref objOperationResult2, pobjProfessional, ClientSession);
                }

                // Actualiza Usuario
                if (pobjSystemUser != null)
                {
                    OperationResult objOperationResult3 = new OperationResult();
                    //new SecurityBL().UpdateSystemUSer(ref objOperationResult3, pobjSystemUser, ClientSession);
                }

                //if (ClientSession[0] == "1")
                //{
                //    objEntitySource.i_IsInMaster = 1;
                //}
                //else
                //{
                //    objEntitySource.i_IsInMaster = 0;
                //}

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PERSONA", "v_PersonId=" + pobjPerson.v_PersonId, Success.Ok, null);
                return "1";
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PERSONA", "v_PersonId=" + pobjPerson.v_PersonId, Success.Failed, ex.Message);
                return "-1";
            }
        }

        public void DeletePerson(ref OperationResult pobjOperationResult, string pstrPersonId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.person
                                       where a.v_PersonId == pstrPersonId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PERSONA", "v_PersonId=" + objEntitySource.v_PersonId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PERSONA", "", Success.Failed, null);
                return;
            }
        }

        public int GetPersonCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.person select a;

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

        public personDto GetPersonByNroDocument(ref OperationResult pobjOperationResult, string pstNroDocument)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                personDto objDtoEntity = null;

                var objEntity = (from a in dbContext.person
                                 where a.v_DocNumber == pstNroDocument && a.i_IsDeleted == 0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = personAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        //public personDto GetPacientByNroDocument(ref OperationResult pobjOperationResult, string pstNroDocument)
        //{
        //    //mon.IsActive = true;
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        personDto objDtoEntity = null;

        //        var objEntity = (from a in dbContext.pacient
        //                        join b in dbContext.person on a.v_PersonId equals b.v_PersonId
        //            where b.v_DocNumber == pstNroDocument && a.i_IsDeleted == 0 && b.i_IsDeleted == 0
        //            select a).FirstOrDefault();

        //        if (objEntity != null)
        //            objDtoEntity = personAssembler.ToDTO(objEntity);

        //        pobjOperationResult.Success = 1;
        //        return objDtoEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return null;
        //    }
        //}

        //public person_oldDto GetPersonByNroDocument_OLD(ref OperationResult pobjOperationResult, string pstNroDocument)
        //{
        //    //mon.IsActive = true;
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        person_oldDto objDtoEntity = null;

        //        var objEntity = (from a in dbContext.person_old
        //                         where a.v_DocNumber == pstNroDocument && a.i_IsDeleted == 0
        //                         select a).FirstOrDefault();

        //        if (objEntity != null)
        //            objDtoEntity = person_oldAssembler.ToDTO(objEntity);

        //        pobjOperationResult.Success = 1;
        //        return objDtoEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return null;
        //    }
        //}


        public personDto GetPersonImage(string personId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.person
                                 where a.v_PersonId == personId
                                 select new personDto
                                 {
                                     b_PersonImage = a.b_PersonImage
                                 }).FirstOrDefault();

                return objEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }      
        


        //public void AddPersonOrganization(ref OperationResult pobjOperationResult, int PersonId, int OrganizationId, List<string> ClientSession)
        //{
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        personorganization objEntity = new PersonOrganization();

        //        objEntity.v_PersonId = PersonId;
        //        objEntity.i_OrganizationId = OrganizationId;
        //        objEntity.d_InsertDate = DateTime.Now;
        //        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);

        //        dbContext.AddToPersonOrganizations(objEntity);
        //        dbContext.SaveChanges();

        //        pobjOperationResult.Success = 1;
        //        // Llenar entidad Log
        //        //new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.CREACION, "ORGANIZACIÓN", "i_OrganizationId=" + objEntity.i_OrganizationId.ToString(), Constants.Success.Ok, null);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        // Llenar entidad Log
        //        //new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_PersonId, Constants.Success.Failed, ex.Message);
        //    }
        //}

        #endregion

        #region Professional

        public professionalDto GetProfessional(ref OperationResult pobjOperationResult, string pstrPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                professionalDto objDtoEntity = null;

                var objEntity = (from a in dbContext.professional
                                 where a.v_PersonId == pstrPersonId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = professionalAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public void AddProfessional(ref OperationResult pobjOperationResult, professionalDto pobjDtoEntity, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                professional objEntity = professionalAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                dbContext.AddToprofessional(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PROFESIONAL", "i_ProfessionId=" + objEntity.i_ProfessionId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PROFESIONAL", "i_ProfessionId=" + pobjDtoEntity.i_ProfessionId, Success.Failed, ex.Message);
                return;
            }
        }

        public void UpdateProfessional(ref OperationResult pobjOperationResult, professionalDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.professional
                                       where a.v_PersonId == pobjDtoEntity.v_PersonId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados

                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                professional objProfessionalTyped = professionalAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.professional.ApplyCurrentValues(objProfessionalTyped);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PROFESIONAL", "i_ProfessionId=" + pobjDtoEntity.i_ProfessionId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PROFESIONAL", "i_ProfessionId=" + pobjDtoEntity.i_ProfessionId, Success.Failed, ex.Message);
                return;
            }
        }

        #endregion

        #region Pacient

        public string AddPacient(ref OperationResult pobjOperationResult, personDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                pacient objEntityPacient = pacientAssembler.ToEntity(new pacientDto());

                objEntityPacient.v_PersonId = AddPerson(ref pobjOperationResult, pobjDtoEntity, null, null, ClientSession);

                if (objEntityPacient.v_PersonId == "-1")
                {
                    pobjOperationResult.Success = 0;
                    return "-1";
                }
                pobjDtoEntity = GetPerson(ref pobjOperationResult, objEntityPacient.v_PersonId);

                objEntityPacient.i_IsDeleted = pobjDtoEntity.i_IsDeleted;
                objEntityPacient.d_InsertDate = DateTime.Now;
                objEntityPacient.i_InsertUserId = Int32.Parse(ClientSession[2]);

                NewId = objEntityPacient.v_PersonId;
                
                dbContext.AddTopacient(objEntityPacient);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PACIENTE", "v_PersonId=" + NewId.ToString(), Success.Ok, null);

                #region Creacion de habitos nocivos

                HistoryBL objHistoryBL = new HistoryBL();
                List<noxioushabitsDto> _noxioushabitsDto = new List<noxioushabitsDto>();
                noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();
                noxioushabitsDto.v_Frequency = "NO";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 1;
                _noxioushabitsDto.Add(noxioushabitsDto);

                noxioushabitsDto = new noxioushabitsDto();
                noxioushabitsDto.v_Frequency = "NO";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 2;
                _noxioushabitsDto.Add(noxioushabitsDto);

                noxioushabitsDto = new noxioushabitsDto();
                noxioushabitsDto.v_Frequency = "NO";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 3;
                _noxioushabitsDto.Add(noxioushabitsDto);


                objHistoryBL.AddNoxiousHabits(ref pobjOperationResult, _noxioushabitsDto, null, null, ClientSession);

                #endregion

                #region Creación de Médicos Familiares
                List<familymedicalantecedentsDto> _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();
                
                //Padre
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 53;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);

                //Madre
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 41;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                //Hermanos
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 32;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                //Esposos
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 19;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                //Hijos
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 67;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                objHistoryBL.AddFamilyMedicalAntecedents(ref pobjOperationResult, _familymedicalantecedentsDto, null, null, ClientSession);


                #endregion
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PACIENTE", "v_PersonId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public string UpdatePacient(ref OperationResult pobjOperationResult, personDto pobjDtoEntity, List<string> ClientSession ,string NumbreDocument, string _NumberDocument)
        {
            //mon.IsActive = true;
            string resultado;
            try
            {
                //Actualizamos la tabla Person
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);


                if (NumbreDocument == _NumberDocument)
                {
                     resultado = UpdatePerson(ref pobjOperationResult, false, pobjDtoEntity, null, false, null, ClientSession);
                }
                else
                {
                     resultado = UpdatePerson(ref pobjOperationResult, true, pobjDtoEntity, null, false, null, ClientSession);
                }

           

               if (resultado== "-1")
               {
                   pobjOperationResult.Success = 0;
                   return resultado;
               }
                // Obtener la entidad fuente de la tabla Pacient
                var objEntitySource = (from a in dbContext.pacient
                                       where a.v_PersonId == pobjDtoEntity.v_PersonId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.pacient.ApplyCurrentValues(objEntitySource);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PACIENTE", "v_PacientId=" + pobjDtoEntity.v_PersonId, Success.Ok, null);
                return "1";

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PACIENTE", "v_PacientId=" + pobjDtoEntity.v_PersonId, Success.Failed, ex.Message);
                return "-1";
            }
        }


        public void DeletePacient(ref OperationResult pobjOperationResult, string pstrPersonId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                DeletePerson(ref pobjOperationResult, pstrPersonId, ClientSession);

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PACIENTE", "", Success.Failed, null);
            }

        }

        public int GetPacientsCount(ref OperationResult pobjOperationResult, string pstrFirstLastNameorDocNumber)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                Int32 intId = -1;
                bool FindById = int.TryParse(pstrFirstLastNameorDocNumber, out intId);
                var Id = intId.ToString();
                var query = (from A in dbContext.pacient
                             join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                             where (B.v_FirstName.Contains(pstrFirstLastNameorDocNumber) || B.v_FirstLastName.Contains(pstrFirstLastNameorDocNumber)
                                    || B.v_SecondLastName.Contains(pstrFirstLastNameorDocNumber)) && B.i_IsDeleted == 0
                             select A).Concat
                             (from A in dbContext.pacient
                              join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                              where B.v_DocNumber.Equals(Id)
                              select A);

                pobjOperationResult.Success = 1;
                return query.Count();
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return 0;
            }
        }

        public PacientList GetPacient(ref OperationResult pobjOperationResult, string pstrPacientId,string pstNroDocument)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from A in dbContext.pacient
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                       equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                 from J1 in J1_join.DefaultIfEmpty()

                                 join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                                 equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                                 from J2 in J2_join.DefaultIfEmpty()
                                 where A.v_PersonId == pstrPacientId || B.v_DocNumber == pstNroDocument
                                 select new PacientList
                                 {
                                     v_PersonId = A.v_PersonId,
                                     v_FirstName = B.v_FirstName,
                                     v_FirstLastName = B.v_FirstLastName,
                                     v_SecondLastName = B.v_SecondLastName,
                                     v_DocNumber = B.v_DocNumber,
                                     v_BirthPlace = B.v_BirthPlace,

                                     i_MaritalStatusId = B.i_MaritalStatusId.Value,
                                     i_LevelOfId = B.i_LevelOfId.Value,
                                     i_DocTypeId = B.i_DocTypeId.Value,
                                     i_SexTypeId = B.i_SexTypeId.Value,

                                     v_TelephoneNumber = B.v_TelephoneNumber,
                                     v_AdressLocation = B.v_AdressLocation,
                                     v_Mail = B.v_Mail,
                                     b_Photo = B.b_PersonImage,
                                     d_Birthdate = B.d_Birthdate,

                                     i_BloodFactorId = B.i_BloodFactorId.Value,
                                     i_BloodGroupId = B.i_BloodGroupId.Value,

                                     b_FingerPrintTemplate = B.b_FingerPrintTemplate,
                                     b_FingerPrintImage = B.b_FingerPrintImage,
                                     b_RubricImage = B.b_RubricImage,
                                     t_RubricImageText = B.t_RubricImageText,
                                     v_CurrentOccupation = B.v_CurrentOccupation,
                                     i_DepartmentId = B.i_DepartmentId.Value,
                                     i_ProvinceId = B.i_ProvinceId.Value,
                                     i_DistrictId = B.i_DistrictId.Value,
                                     i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId.Value,
                                     v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
                                     i_TypeOfInsuranceId = B.i_TypeOfInsuranceId.Value,
                                     i_NumberLivingChildren = B.i_NumberLivingChildren.Value,
                                     i_NumberDependentChildren = B.i_NumberDependentChildren.Value,
                                     i_Relationship = B.i_Relationship.Value,
                                    v_ExploitedMineral = B.v_ExploitedMineral,
                                     i_AltitudeWorkId = B.i_AltitudeWorkId.Value,
                                     i_PlaceWorkId = B.i_PlaceWorkId.Value,
                                    v_OwnerName = B.v_OwnerName,
                                     v_NroPoliza = B.v_NroPoliza,
                                     v_Deducible = B.v_Deducible.Value,
                                     i_NroHermanos = B.i_NroHermanos.Value,
                                     i_NumberLiveChildren = B.i_NumberLiveChildren,
                                     i_NumberDeadChildren = B.i_NumberDeadChildren,
                                     v_Nacionalidad =  B.v_Nacionalidad,
                                     v_ResidenciaAnterior = B.v_ResidenciaAnterior,
                                     v_Religion = B.v_Religion
                                 }).FirstOrDefault();

                pobjOperationResult.Success = 1;
                return objEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<PacientList> GetPacientsPagedAndFiltered_(ref OperationResult pobjOperationResult, int? pintPageIndex, int pintResultsPerPage, string pstrFirstLastNameorDocNumber)
        {
            //mon.IsActive = true;
            try
            {
                int intId = -1;
                bool FindById = int.TryParse(pstrFirstLastNameorDocNumber, out intId);
                var Id = intId.ToString();
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.pacient
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                             where B.v_PersonId == "N009-PP000004226"
                              
                            select new PacientList
                            {
                                v_PersonId = A.v_PersonId,
                                v_FirstName = B.v_FirstName,
                                v_FirstLastName = B.v_FirstLastName,
                                v_SecondLastName = B.v_SecondLastName,
                                v_AdressLocation = B.v_AdressLocation,
                                v_TelephoneNumber = B.v_TelephoneNumber,
                                v_Mail = B.v_Mail
                            });

                List<PacientList> objData = query.ToList();
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


        public List<PacientList> GetPacientsPagedAndFiltered(ref OperationResult pobjOperationResult, int pintPageIndex, int pintResultsPerPage, string pstrFirstLastNameorDocNumber)
        {
            //mon.IsActive = true;
            try
            {
                int intId = -1;
                bool FindById = int.TryParse(pstrFirstLastNameorDocNumber, out intId);
                var Id = intId.ToString();
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.pacient
                             join B in dbContext.person on A.v_PersonId equals B.v_PersonId

                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where (B.v_FirstName.Contains(pstrFirstLastNameorDocNumber) || B.v_FirstLastName.Contains(pstrFirstLastNameorDocNumber)
                                    || B.v_SecondLastName.Contains(pstrFirstLastNameorDocNumber)) && B.i_IsDeleted == 0
                             select new PacientList
                             {
                                 v_PersonId = A.v_PersonId,
                                 v_FirstName = B.v_FirstName,
                                 v_FirstLastName = B.v_FirstLastName,
                                 v_SecondLastName = B.v_SecondLastName,
                                 v_AdressLocation = B.v_AdressLocation,
                                 v_TelephoneNumber = B.v_TelephoneNumber,
                                 v_Mail = B.v_Mail,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,
                                 i_DepartmentId = B.i_DepartmentId,
                                 i_ProvinceId = B.i_ProvinceId,
                                 i_DistrictId = B.i_DistrictId,
                                 i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId,
                                 v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
                                 i_TypeOfInsuranceId = B.i_TypeOfInsuranceId,
                                 i_NumberLivingChildren = B.i_NumberLivingChildren,
                                 i_NumberDependentChildren = B.i_NumberDependentChildren

                             }).Concat
                            (from A in dbContext.pacient
                             join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                           
                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where B.v_DocNumber == Id && B.i_IsDeleted == 0
                             select new PacientList
                             {
                                 v_PersonId = A.v_PersonId,
                                 v_FirstName = B.v_FirstName,
                                 v_FirstLastName = B.v_FirstLastName,
                                 v_SecondLastName = B.v_SecondLastName,
                                 v_AdressLocation = B.v_AdressLocation,
                                 v_TelephoneNumber = B.v_TelephoneNumber,
                                 v_Mail = B.v_Mail,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,
                                 i_DepartmentId = B.i_DepartmentId,
                                 i_ProvinceId = B.i_ProvinceId,
                                 i_DistrictId = B.i_DistrictId,
                                 i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId,
                                 v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
                                 i_TypeOfInsuranceId = B.i_TypeOfInsuranceId,
                                 i_NumberLivingChildren = B.i_NumberLivingChildren,
                                 i_NumberDependentChildren = B.i_NumberDependentChildren
                             }).OrderBy("v_FirstLastName").Take(pintResultsPerPage);

                List<PacientList> objData = query.ToList();
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

        public PacientList GetPacientReport(string pstrPacientId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from A in dbContext.pacient
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join C in dbContext.systemparameter on new { a = B.i_MaritalStatusId.Value, b = 101 }
                                                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                 from C in C_join.DefaultIfEmpty()
                                 join D in dbContext.datahierarchy on new { a = B.i_DocTypeId.Value, b = 106 }
                                                                        equals new { a = D.i_ItemId, b = D.i_GroupId } into D_join
                                 from D in D_join.DefaultIfEmpty()

                                 join E in dbContext.datahierarchy on new { a = B.i_DepartmentId.Value, b = 113 }
                                                                    equals new { a = E.i_ItemId, b = E.i_GroupId } into E_join
                                 from E in E_join.DefaultIfEmpty()


                                 join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                       equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                 from J1 in J1_join.DefaultIfEmpty()

                                 join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                                 equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                                 from J2 in J2_join.DefaultIfEmpty()
                            
                                 where A.v_PersonId == pstrPacientId
                                 select new PacientList
                                 {
                                     v_PersonId = A.v_PersonId,
                                     v_FirstName = B.v_FirstName,
                                     v_FirstLastName = B.v_FirstLastName,
                                     v_SecondLastName = B.v_SecondLastName,
                                     v_DocNumber = B.v_DocNumber,
                                     v_BirthPlace = B.v_BirthPlace,
                                     i_MaritalStatusId = B.i_MaritalStatusId,
                                     v_MaritalStatus = C.v_Value1,
                                     i_LevelOfId = B.i_LevelOfId,
                                     i_DocTypeId = B.i_DocTypeId,
                                     v_DocTypeName = D.v_Value1,
                                     i_SexTypeId = B.i_SexTypeId,
                                     v_TelephoneNumber = B.v_TelephoneNumber,
                                     v_AdressLocation = B.v_AdressLocation,
                                     v_Mail = B.v_Mail,
                                     b_Photo = B.b_PersonImage,
                                     d_Birthdate = B.d_Birthdate,
                                     i_BloodFactorId = B.i_BloodFactorId.Value,
                                     i_BloodGroupId = B.i_BloodGroupId.Value,
                                     b_FingerPrintTemplate = B.b_FingerPrintTemplate,
                                     b_FingerPrintImage = B.b_FingerPrintImage,
                                     b_RubricImage = B.b_RubricImage,
                                     t_RubricImageText = B.t_RubricImageText,
                                     v_CurrentOccupation = B.v_CurrentOccupation,
                                     i_DepartmentId = B.i_DepartmentId,
                                     v_DepartamentName = E.v_Value1,
                                     i_ProvinceId = B.i_ProvinceId,
                                     v_ProvinceName = E.v_Value1,
                                     i_DistrictId = B.i_DistrictId,
                                     v_DistrictName = E.v_Value1,
                                     i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId,
                                     v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
                                     i_TypeOfInsuranceId = B.i_TypeOfInsuranceId,
                                     i_NumberLivingChildren = B.i_NumberLivingChildren,
                                     i_NumberDependentChildren = B.i_NumberDependentChildren

                                 }).FirstOrDefault();

                return objEntity;
            }
            catch (Exception ex)
            {
             
                return null;
            }
        }

        public PacientList GetPacientReportEPS(string serviceId)
        {
            //mon.IsActive = true;
            
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from s in dbContext.service
                                 join pr in dbContext.protocol on s.v_ProtocolId equals pr.v_ProtocolId                             
                                 join pe in dbContext.person on s.v_PersonId equals pe.v_PersonId

                                 join C in dbContext.systemparameter on new { a = pe.i_TypeOfInsuranceId.Value, b = 188 }  // Tipo de seguro
                                                              equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                 from C in C_join.DefaultIfEmpty()

                                 join d in dbContext.systemparameter on new { a = pe.i_Relationship.Value, b = 207 }  // Parentesco
                                                              equals new { a = d.i_ParameterId, b = d.i_GroupId } into d_join
                                 from d in d_join.DefaultIfEmpty()




                                 join ee in dbContext.systemparameter on new { a = s.i_InicioEnf.Value, b = 118 }  // Inicio Enfermedad
                                                            equals new { a = ee.i_ParameterId, b = ee.i_GroupId } into ee_join
                                 from ee in ee_join.DefaultIfEmpty()

                                 join ff in dbContext.systemparameter on new { a = s.i_TimeOfDiseaseTypeId.Value, b = 133 }  // Tiempor Enfer
                                                          equals new { a = ff.i_ParameterId, b = ff.i_GroupId } into ff_join
                                 from ff in ff_join.DefaultIfEmpty()

                                 join gg in dbContext.systemparameter on new { a = s.i_CursoEnf.Value, b = 119 }  // Curso Enfermedad
                                                          equals new { a = gg.i_ParameterId, b = gg.i_GroupId } into gg_join
                                 from gg in gg_join.DefaultIfEmpty()


                                 join F in dbContext.groupoccupation on pr.v_GroupOccupationId equals F.v_GroupOccupationId


                                 // Grupo sanguineo ****************************************************
                                 join gs in dbContext.systemparameter on new { a = pe.i_BloodGroupId.Value, b = 154 }  // AB
                                                             equals new { a = gs.i_ParameterId, b = gs.i_GroupId } into gs_join
                                 from gs in gs_join.DefaultIfEmpty()

                                 // Factor sanguineo ****************************************************
                                 join fs in dbContext.systemparameter on new { a = pe.i_BloodFactorId.Value, b = 155 }  // NEGATIVO
                                                           equals new { a = fs.i_ParameterId, b = fs.i_GroupId } into fs_join
                                 from fs in fs_join.DefaultIfEmpty()

                                 // Empresa / Sede Trabajo  ********************************************************
                                 join ow in dbContext.organization on new { a = pr.v_CustomerOrganizationId }
                                         equals new { a = ow.v_OrganizationId } into ow_join
                                 from ow in ow_join.DefaultIfEmpty()

                                 join lw in dbContext.location on new { a = pr.v_WorkingOrganizationId, b = pr.v_WorkingLocationId }
                                      equals new { a = lw.v_OrganizationId, b = lw.v_LocationId } into lw_join
                                 from lw in lw_join.DefaultIfEmpty()

                                join D in dbContext.systemparameter on new { a = pe.i_SexTypeId.Value, b = 100 }  // Tipo de seguro
                                                              equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                                 from D in D_join.DefaultIfEmpty()

                                 join H in dbContext.systemparameter on new { a = pe.i_MaritalStatusId.Value, b = 101 }  // Tipo de seguro
                                                             equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join
                                 from H in H_join.DefaultIfEmpty()


                                join L in dbContext.systemparameter on new { a = pr.i_EsoTypeId.Value, b = 118 }
                                                 equals new { a = L.i_ParameterId, b = L.i_GroupId } into L_join
                                 from L in L_join.DefaultIfEmpty()
                                 //************************************************************************************


                                 join su in dbContext.systemuser on s.i_UpdateUserMedicalAnalystId.Value equals su.i_SystemUserId into su_join
                                 from su in su_join.DefaultIfEmpty()

                                 join pr1 in dbContext.professional on su.v_PersonId equals pr1.v_PersonId into pr1_join
                                 from pr1 in pr1_join.DefaultIfEmpty()

                                 //************************************************************************************


                                 join su1 in dbContext.systemuser on s.i_UpdateUserOccupationalMedicaltId.Value equals su1.i_SystemUserId into su1_join
                                 from su1 in su1_join.DefaultIfEmpty()

                                 join pr2 in dbContext.professional on su1.v_PersonId equals pr2.v_PersonId into pr2_join
                                 from pr2 in pr2_join.DefaultIfEmpty()

                                 join B in dbContext.protocol on s.v_ProtocolId equals B.v_ProtocolId into B_join
                                 from B in B_join.DefaultIfEmpty()
                                 join C1 in dbContext.organization on B.v_EmployerOrganizationId equals C1.v_OrganizationId into C1_join
                                 from C1 in C1_join.DefaultIfEmpty()
                                 join C2 in dbContext.organization on B.v_CustomerOrganizationId equals C2.v_OrganizationId into C2_join
                                 from C2 in C2_join.DefaultIfEmpty()
                                 join C3 in dbContext.organization on B.v_WorkingOrganizationId equals C3.v_OrganizationId into C3_join
                                 from C3 in C3_join.DefaultIfEmpty()


                                 where s.v_ServiceId == serviceId
                                 select new PacientList
                                 {

                                     empresa_ = C2.v_Name,
                                     contrata = C1.v_Name,
                                     subcontrata = C3.v_Name,

                                     TimeOfDisease = s.i_TimeOfDisease,
                                    v_ObsStatusService = s.v_ObsStatusService,
                                     TiempoEnfermedad = ff.v_Value1,
                                     InicioEnfermedad = ee.v_Value1,
                                     CursoEnfermedad = gg.v_Value1,
                                     v_CurrentOccupation = pe.v_CurrentOccupation,
                                     v_PersonId = pe.v_PersonId,
                                     v_FirstName = pe.v_FirstName,
                                     v_FirstLastName = pe.v_FirstLastName,
                                     v_SecondLastName = pe.v_SecondLastName,                                                                                                      
                                     b_Photo = pe.b_PersonImage,                                  
                                     v_TypeOfInsuranceName = C.v_Value1,
                                     v_FullWorkingOrganizationName = ow.v_Name + " / " + lw.v_Name,
                                     v_OrganitationName = ow.v_Name,
                                     v_RelationshipName = d.v_Value1,
                                     v_OwnerName = "",
                                     d_ServiceDate = s.d_ServiceDate,
                                     d_Birthdate = pe.d_Birthdate,
                                     i_DocTypeId = pe.i_DocTypeId,
                                     i_NumberDependentChildren = pe.i_NumberDependentChildren,
                                     i_NumberLivingChildren = pe.i_NumberLivingChildren,
                                     FirmaTrabajador = pe.b_RubricImage,
                                     HuellaTrabajador = pe.b_FingerPrintImage,
                                     v_BloodGroupName = gs.v_Value1,
                                     v_BloodFactorName = fs.v_Value1,
                                     v_SexTypeName = D.v_Value1,
                                     v_TipoExamen = L.v_Value1,
                                     v_NombreProtocolo = pr.v_Name,
                                     i_EsoTypeId = pr.i_EsoTypeId,
                                     v_DocNumber = pe.v_DocNumber,
                                     v_IdService = s.v_ServiceId,

                                     v_Story = s.v_Story,
                                     v_MainSymptom = s.v_MainSymptom,
                                     FirmaDoctor = pr1.b_SignatureImage,
                                     v_ExaAuxResult = s.v_ExaAuxResult,
                                     FirmaDoctorAuditor = pr2.b_SignatureImage,
                                     GESO = F.v_Name,
                                     i_AptitudeStatusId = s.i_AptitudeStatusId,
                                     v_MaritalStatus = H.v_Value1,
                                     EmpresaClienteId = ow.v_OrganizationId,
                                     logoCliente = ow.b_Image
                                 });

             
                var sql = (from a in objEntity.ToList()
                         
                           select new PacientList
                            {
                                empresa_ = a.empresa_,
                                contrata = a.contrata,
                                subcontrata = a.subcontrata,

                                FirmaDoctor =a.FirmaMedico,
                                v_Story = a.v_Story,
                                v_MainSymptom =a.v_MainSymptom,
                                TimeOfDisease = a.TimeOfDisease,
                                v_CurrentOccupation = a.v_CurrentOccupation,
                                TiempoEnfermedad = a.TimeOfDisease + " " + a.TiempoEnfermedad,
                                InicioEnfermedad = a.InicioEnfermedad,
                                CursoEnfermedad = a.CursoEnfermedad,
                                i_EsoTypeId = a.i_EsoTypeId,

                                v_PersonId = a.v_PersonId,
                                  i_DocTypeId = a.i_DocTypeId,
                                v_FirstName = a.v_FirstName,
                                v_FirstLastName = a.v_FirstLastName,
                                v_SecondLastName = a.v_SecondLastName,
                                i_Age = GetAge(a.d_Birthdate.Value),
                                b_Photo = a.b_Photo,
                                v_TypeOfInsuranceName = a.v_TypeOfInsuranceName,
                                v_FullWorkingOrganizationName = a.v_FullWorkingOrganizationName,
                                v_RelationshipName = a.v_RelationshipName,
                                v_OwnerName = a.v_FirstName + " " + a.v_FirstLastName + " " + a.v_SecondLastName,
                                d_ServiceDate = a.d_ServiceDate,
                                i_NumberDependentChildren = a.i_NumberDependentChildren,
                                i_NumberLivingChildren = a.i_NumberLivingChildren,
                                v_OwnerOrganizationName = (from n in dbContext.organization
                                                           where n.v_OrganizationId == Constants.OWNER_ORGNIZATION_ID
                                                           select n.v_Name).SingleOrDefault<string>(),
                                v_DoctorPhysicalExamName = (from sc in dbContext.servicecomponent
                                                            join J1 in dbContext.systemuser on new { i_InsertUserId = sc.i_ApprovedUpdateUserId.Value }
                                                                       equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                                            from J1 in J1_join.DefaultIfEmpty()
                                                            join pe in dbContext.person on J1.v_PersonId equals pe.v_PersonId
                                                            where (sc.v_ServiceId == serviceId) &&
                                                                  (sc.v_ComponentId == Constants.EXAMEN_FISICO_ID)
                                                            select pe.v_FirstName + " " + pe.v_FirstLastName).SingleOrDefault<string>(),
                                FirmaTrabajador = a.FirmaTrabajador,
                                HuellaTrabajador = a.HuellaTrabajador,
                                v_BloodGroupName = a.v_BloodGroupName,
                                v_BloodFactorName = a.v_BloodFactorName,
                                v_SexTypeName = a.v_SexTypeName,
                                v_TipoExamen = a.v_TipoExamen,
                                v_NombreProtocolo = a.v_NombreProtocolo,
                                v_DocNumber = a.v_DocNumber,
                                v_IdService = a.v_IdService,
                                v_ExaAuxResult = a.v_ExaAuxResult,
                                FirmaDoctorAuditor = a.FirmaDoctorAuditor,
                                GESO = a.GESO,
                                v_OrganitationName = a.v_OrganitationName,
                                i_AptitudeStatusId = a.i_AptitudeStatusId,
                                v_ObsStatusService = a.v_ObsStatusService,
                                v_MaritalStatus = a.v_MaritalStatus,
                                EmpresaClienteId = a.EmpresaClienteId,
                                logoCliente = a.logoCliente
                            }).FirstOrDefault();

                return sql;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public DatosAtencion GetDatosPersonalesAtencion(string serviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from s in dbContext.service
                                join pe in dbContext.person on s.v_PersonId equals pe.v_PersonId
                                 join C in dbContext.systemparameter on new { a = pe.i_SexTypeId.Value, b = 100 } 
                                                              equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                from C in C_join.DefaultIfEmpty()
                                where s.v_ServiceId == serviceId
                                select new DatosAtencion
                                {
                                  Paciente = pe.v_FirstLastName,
                                  Genero = C.v_Value1
                                }).FirstOrDefault();

                return objEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public PacientList GetPacientReportEPSFirmaMedicoOcupacional(string serviceId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from s in dbContext.service
                                 join pr in dbContext.protocol on s.v_ProtocolId equals pr.v_ProtocolId
                                 join pe in dbContext.person on s.v_PersonId equals pe.v_PersonId

                                 join C in dbContext.systemparameter on new { a = pe.i_TypeOfInsuranceId.Value, b = 188 }  // Tipo de seguro
                                                              equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                 from C in C_join.DefaultIfEmpty()

                                 join d in dbContext.systemparameter on new { a = pe.i_Relationship.Value, b = 207 }  // Parentesco
                                                              equals new { a = d.i_ParameterId, b = d.i_GroupId } into d_join
                                 from d in d_join.DefaultIfEmpty()




                                 join ee in dbContext.systemparameter on new { a = s.i_InicioEnf.Value, b = 118 }  // Inicio Enfermedad
                                                            equals new { a = ee.i_ParameterId, b = ee.i_GroupId } into ee_join
                                 from ee in ee_join.DefaultIfEmpty()

                                 join ff in dbContext.systemparameter on new { a = s.i_TimeOfDiseaseTypeId.Value, b = 133 }  // Tiempor Enfer
                                                          equals new { a = ff.i_ParameterId, b = ff.i_GroupId } into ff_join
                                 from ff in ff_join.DefaultIfEmpty()

                                 join gg in dbContext.systemparameter on new { a = s.i_CursoEnf.Value, b = 119 }  // Curso Enfermedad
                                                          equals new { a = gg.i_ParameterId, b = gg.i_GroupId } into gg_join
                                 from gg in gg_join.DefaultIfEmpty()





                                 // Grupo sanguineo ****************************************************
                                 join gs in dbContext.systemparameter on new { a = pe.i_BloodGroupId.Value, b = 154 }  // AB
                                                             equals new { a = gs.i_ParameterId, b = gs.i_GroupId } into gs_join
                                 from gs in gs_join.DefaultIfEmpty()

                                 // Factor sanguineo ****************************************************
                                 join fs in dbContext.systemparameter on new { a = pe.i_BloodFactorId.Value, b = 155 }  // NEGATIVO
                                                           equals new { a = fs.i_ParameterId, b = fs.i_GroupId } into fs_join
                                 from fs in fs_join.DefaultIfEmpty()

                                 // Empresa / Sede Trabajo  ********************************************************
                                 join ow in dbContext.organization on new { a = pr.v_WorkingOrganizationId }
                                         equals new { a = ow.v_OrganizationId } into ow_join
                                 from ow in ow_join.DefaultIfEmpty()

                                 join lw in dbContext.location on new { a = pr.v_WorkingOrganizationId, b = pr.v_WorkingLocationId }
                                      equals new { a = lw.v_OrganizationId, b = lw.v_LocationId } into lw_join
                                 from lw in lw_join.DefaultIfEmpty()

                                 join D in dbContext.systemparameter on new { a = pe.i_SexTypeId.Value, b = 100 }  // Tipo de seguro
                                                               equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                                 from D in D_join.DefaultIfEmpty()

                                 join L in dbContext.systemparameter on new { a = pr.i_EsoTypeId.Value, b = 118 }
                                                  equals new { a = L.i_ParameterId, b = L.i_GroupId } into L_join
                                 from L in L_join.DefaultIfEmpty()
                                 //************************************************************************************


                                 join su in dbContext.systemuser on s.i_UpdateUserOccupationalMedicaltId.Value equals su.i_SystemUserId into su_join
                                 from su in su_join.DefaultIfEmpty()

                                 join pr1 in dbContext.professional on su.v_PersonId equals pr1.v_PersonId into pr1_join
                                 from pr1 in pr1_join.DefaultIfEmpty()


                                 where s.v_ServiceId == serviceId
                                 select new PacientList
                                 {
                                     TimeOfDisease = s.i_TimeOfDisease,
                                     v_CurrentOccupation = pe.v_CurrentOccupation,
                                     TiempoEnfermedad = ff.v_Value1,
                                     InicioEnfermedad = ee.v_Value1,
                                     CursoEnfermedad = gg.v_Value1,

                                     v_PersonId = pe.v_PersonId,
                                     v_FirstName = pe.v_FirstName,
                                     v_FirstLastName = pe.v_FirstLastName,
                                     v_SecondLastName = pe.v_SecondLastName,
                                     b_Photo = pe.b_PersonImage,
                                     v_TypeOfInsuranceName = C.v_Value1,
                                     v_FullWorkingOrganizationName = ow.v_Name + " / " + lw.v_Name,
                                     v_RelationshipName = d.v_Value1,
                                     v_OwnerName = "",
                                     d_ServiceDate = s.d_ServiceDate,
                                     d_Birthdate = pe.d_Birthdate,
                                     i_DocTypeId = pe.i_DocTypeId,
                                     i_NumberDependentChildren = pe.i_NumberDependentChildren,
                                     i_NumberLivingChildren = pe.i_NumberLivingChildren,
                                     FirmaTrabajador = pe.b_RubricImage,
                                     HuellaTrabajador = pe.b_FingerPrintImage,
                                     v_BloodGroupName = gs.v_Value1,
                                     v_BloodFactorName = fs.v_Value1,
                                     v_SexTypeName = D.v_Value1,
                                     v_TipoExamen = L.v_Value1,
                                     v_NombreProtocolo = pr.v_Name,
                                     v_DocNumber = pe.v_DocNumber,
                                     v_IdService = s.v_ServiceId,

                                     v_Story = s.v_Story,
                                     v_MainSymptom = s.v_MainSymptom,
                                     FirmaDoctor = pr1.b_SignatureImage,
                                     v_ExaAuxResult = s.v_ExaAuxResult,

                                     //FirmaDoctor = pr.b_SignatureImage,
                                     NombreDoctor = pe.v_FirstName + " " + pe.v_FirstLastName + " " + pe.v_SecondLastName,
                                     CMP = pr1.v_ProfessionalCode

                                 });


                var sql = (from a in objEntity.ToList()

                           select new PacientList
                           {
                               FirmaDoctor = a.FirmaDoctor,
                               NombreDoctor = a.NombreDoctor,
                               CMP = a.CMP,
                               v_CurrentOccupation = a.v_CurrentOccupation,
                               v_Story = a.v_Story,
                               v_MainSymptom = a.v_MainSymptom,
                               TimeOfDisease = a.TimeOfDisease,

                               TiempoEnfermedad = a.TimeOfDisease + " " + a.TiempoEnfermedad,
                               InicioEnfermedad = a.InicioEnfermedad,
                               CursoEnfermedad = a.CursoEnfermedad,


                               v_PersonId = a.v_PersonId,
                               i_DocTypeId = a.i_DocTypeId,
                               v_FirstName = a.v_FirstName,
                               v_FirstLastName = a.v_FirstLastName,
                               v_SecondLastName = a.v_SecondLastName,
                               i_Age = GetAge(a.d_Birthdate.Value),
                               b_Photo = a.b_Photo,
                               v_TypeOfInsuranceName = a.v_TypeOfInsuranceName,
                               v_FullWorkingOrganizationName = a.v_FullWorkingOrganizationName,
                               v_RelationshipName = a.v_RelationshipName,
                               v_OwnerName = a.v_FirstName + " " + a.v_FirstLastName + " " + a.v_SecondLastName,
                               d_ServiceDate = a.d_ServiceDate,
                               i_NumberDependentChildren = a.i_NumberDependentChildren,
                               i_NumberLivingChildren = a.i_NumberLivingChildren,
                               v_OwnerOrganizationName = (from n in dbContext.organization
                                                          where n.v_OrganizationId == Constants.OWNER_ORGNIZATION_ID
                                                          select n.v_Name).SingleOrDefault<string>(),
                               v_DoctorPhysicalExamName = (from sc in dbContext.servicecomponent
                                                           join J1 in dbContext.systemuser on new { i_InsertUserId = sc.i_ApprovedUpdateUserId.Value }
                                                                      equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                                           from J1 in J1_join.DefaultIfEmpty()
                                                           join pe in dbContext.person on J1.v_PersonId equals pe.v_PersonId
                                                           where (sc.v_ServiceId == serviceId) &&
                                                                 (sc.v_ComponentId == Constants.EXAMEN_FISICO_ID)
                                                           select pe.v_FirstName + " " + pe.v_FirstLastName).SingleOrDefault<string>(),
                               FirmaTrabajador = a.FirmaTrabajador,
                               HuellaTrabajador = a.HuellaTrabajador,
                               v_BloodGroupName = a.v_BloodGroupName,
                               v_BloodFactorName = a.v_BloodFactorName,
                               v_SexTypeName = a.v_SexTypeName,
                               v_TipoExamen = a.v_TipoExamen,
                               v_NombreProtocolo = a.v_NombreProtocolo,
                               v_DocNumber = a.v_DocNumber,
                               v_IdService = a.v_IdService,
                               v_ExaAuxResult = a.v_ExaAuxResult

                           }).FirstOrDefault();

                return sql;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int GetAge(DateTime FechaNacimiento)
        {
            return int.Parse((DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1).ToString());

        }

        // Alberto
        public List<ServiceList> GetFichaPsicologicaOcupacional(string pstrserviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();          

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                                 join D in dbContext.organization on C.v_WorkingOrganizationId equals D.v_OrganizationId
                                 join E in dbContext.servicecomponent on new { a = A.v_ServiceId, b = "N002-ME000000033" } 
                                                                        equals new { a = E.v_ServiceId , b = E.v_ComponentId }                    

                                 join J1 in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                                    equals new { a = J1.i_ItemId, b = J1.i_GroupId } into J1_join
                                 from J1 in J1_join.DefaultIfEmpty()                           

                                 // Usuario Medico Evaluador / Medico Aprobador ****************************
                                 join me in dbContext.systemuser on E.i_ApprovedUpdateUserId equals me.i_SystemUserId into me_join
                                 from me in me_join.DefaultIfEmpty()

                                 join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                 from pme in pme_join.DefaultIfEmpty()

                                 //*********************************************************************

                                 join H in dbContext.systemparameter on new { a = C.i_EsoTypeId.Value, b = 118 }
                                                 equals new { a = H.i_ParameterId, b = H.i_GroupId }  // TIPO ESO [ESOA,ESOR,ETC]
                            
                                 where A.v_ServiceId == pstrserviceId
                                 select new ServiceList
                                 {
                                     v_PersonId = A.v_PersonId,
                                     v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName ,
                                     d_BirthDate = B.d_Birthdate,
                                     d_ServiceDate = A.d_ServiceDate,
                                     v_BirthPlace = B.v_BirthPlace,
                                     i_DiaN = B.d_Birthdate.Value.Day,
                                     i_MesN = B.d_Birthdate.Value.Month,
                                     i_AnioN = B.d_Birthdate.Value.Year,
                                     i_DiaV = A.d_ServiceDate.Value.Day,
                                     i_MesV = A.d_ServiceDate.Value.Month,
                                     i_AnioV = A.d_ServiceDate.Value.Year,
                                     NivelInstruccion = J1.v_Value1,
                                     LugarResidencia = B.v_AdressLocation,
                                     PuestoTrabajo = B.v_CurrentOccupation,
                                     EmpresaTrabajo = D.v_Name,
                                     v_ServiceComponentId = E.v_ServiceComponentId,
                                     v_ServiceId = A.v_ServiceId,
                                     Rubrica = pme.b_SignatureImage,
                                     RubricaTrabajador = B.b_RubricImage,
                                     HuellaTrabajador = B.b_FingerPrintImage,
                                     v_EsoTypeName = H.v_Value1,
                                 });

                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter(); 

                var sql = (from a in objEntity.ToList()
                           let Psico = serviceBL.ValoresComponente(pstrserviceId, Constants.PSICOLOGIA_ID)
                           select new ServiceList
                            {
                                v_ServiceId = a.v_ServiceId,
                                v_ServiceComponentId = a.v_ServiceComponentId,                               
                                v_PersonId = a.v_PersonId,
                                v_Pacient = a.v_Pacient,
                                i_Edad = GetAge(a.d_BirthDate.Value),
                                v_BirthPlace = a.v_BirthPlace,
                                i_DiaN = a.i_DiaN,
                                i_MesN =a.i_MesN,
                                i_AnioN = a.i_AnioN,
                                i_DiaV = a.i_DiaV,
                                i_MesV = a.i_MesV,
                                i_AnioV = a.i_AnioV,
                                NivelInstruccion = a.NivelInstruccion,
                                LugarResidencia = a.LugarResidencia,
                                PuestoTrabajo = a.PuestoTrabajo,
                                EmpresaTrabajo = a.EmpresaTrabajo,

                                MotivoEvaluacion = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).v_Value1,
                                NivelIntelectual = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_NIVEL_ACTUAL) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_NIVEL_ACTUAL).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000630", "NOCOMBO", 0, "NO"),

                                CoordinacionVisomotriz = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_CoordinacionVisomotriz) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_CoordinacionVisomotriz).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000631", "NOCOMBO", 0,"NO"),
                                NivelMemoria = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_NivelMemoria) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_NivelMemoria).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000632", "NOCOMBO", 0,"NO"),
                                Personalidad = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_PERSONALIDAD) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_PERSONALIDAD).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000633", "NOCOMBO", 0,"NO"),
                                Afectividad = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_Afectividad) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.PSICOLOGIA_Afectividad).v_Value1,//  GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000634", "NOCOMBO", 0,"NO"),
                                AreaCognitiva = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_area_psicolo_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_area_psicolo_ID).v_Value1,//  GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000336", "NOCOMBO", 0, "NO"),

                                AreaEmocional = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_area_emocianal_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_area_emocianal_ID).v_Value1,//  GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000337", "NOCOMBO", 0, "NO"),
                                Conclusiones = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_conclusiones_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_conclusiones_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000338", "NOCOMBO", 0,"NO"),
                                Restriccion = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_restriccion_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_restriccion_ID).v_Value1,//  GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000081", "NOCOMBO", 0,"NO"),
                                Recomendacion = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_recomendaciones_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_recomendaciones_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000080", "SICOMBO", 190, "NO"),

                                Presentacion = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_presentacion_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_presentacion_ID).v_Value1,//  GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000283", "SICOMBO", 175,"SI"),

                                Postura = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_postura_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_postura_ID).v_Value1,//  GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000282", "SICOMBO", 173, "SI"),



                                DiscursoRitmo = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_discuerso_ritmo_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_discuerso_ritmo_ID).v_Value1,// (a.v_ServiceId, "N002-ME000000033", "N002-MF000000280", "SICOMBO", 214, "SI"),
                                DiscursoTono = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_discurso_tono_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_discurso_tono_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000281", "SICOMBO", 215, "SI"),
                                DiscursoArticulacion = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_discurso_articulacion_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_discurso_articulacion_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N002-MF000000279", "SICOMBO", 216, "SI"),

                                OrientacionTiempo = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_orientacion_tiempo_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_orientacion_tiempo_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000077", "SICOMBO", 189, "SI"),
                                OrientacionEspacio = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_orientacion_espacio_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_orientacion_espacio_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000078", "SICOMBO", 189, "SI"),
                                OrientacionPersona = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_orientacion_persona_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_orientacion_persona_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000000079", "SICOMBO", 189, "SI"),
                                AreaPersonal = Psico.Count == 0 || Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_area_personal_ID) == null ? string.Empty : Psico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_MENTAL_area_personal_ID).v_Value1,// GetServiceComponentFielValue(a.v_ServiceId, "N002-ME000000033", "N009-MF000001298", "NOCOMBO", 0, "NO"),


                                Rubrica = a.Rubrica,

                                b_Logo = MedicalCenter.b_Image,
                                EmpresaPropietaria = MedicalCenter.v_Name,
                                EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                                EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                                EmpresaPropietariaEmail = MedicalCenter.v_Mail,

                                
                                v_EsoTypeName = a.v_EsoTypeName,
                                RubricaTrabajador = a.RubricaTrabajador,
                                HuellaTrabajador = a.HuellaTrabajador,
                            }).ToList();

                return sql;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        // Alberto
        public List<ServiceList> GetMusculoEsqueletico(string pstrserviceId, string pstrComponentId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId into B_join
                                 from B in B_join.DefaultIfEmpty()
                                 join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId into C_join
                                 from C in C_join.DefaultIfEmpty()
                                 join D in dbContext.organization on C.v_WorkingOrganizationId equals D.v_OrganizationId into D_join
                                 from D in D_join.DefaultIfEmpty()
                                  
                                 join E in dbContext.servicecomponent on new { a = A.v_ServiceId, b = pstrComponentId }
                                                                        equals new { a = E.v_ServiceId, b = E.v_ComponentId }

                                 join J1 in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                                    equals new { a = J1.i_ItemId, b = J1.i_GroupId } into J1_join
                                 from J1 in J1_join.DefaultIfEmpty()

                                 join F in dbContext.systemuser on E.i_InsertUserId equals F.i_SystemUserId
                                 join G in dbContext.professional on F.v_PersonId equals G.v_PersonId into G_join
                                 from G in G_join.DefaultIfEmpty()

                                 join F1 in dbContext.systemuser on E.i_ApprovedUpdateUserId equals F1.i_SystemUserId into F1_join
                                 from F1 in F1_join.DefaultIfEmpty()

                                 join Z in dbContext.person on F.v_PersonId equals Z.v_PersonId

                                 where A.v_ServiceId == pstrserviceId
                                 select new ServiceList
                                 {
                                     v_PersonId = A.v_PersonId,
                                     v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                     d_ServiceDate = A.d_ServiceDate,                                    
                                     EmpresaTrabajo = D.v_Name,                                   
                                     v_ServiceId = A.v_ServiceId,
                                     v_ComponentId = E.v_ServiceComponentId,
                                     NombreUsuarioGraba = Z.v_FirstLastName + " " + Z.v_SecondLastName + " " + Z.v_FirstName,
                                 });

                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter(); 

                var sql = (from a in objEntity.ToList()

                           let OsteoMuscular = new ServiceBL().ValoresComponente(pstrserviceId, Constants.OSTEO_MUSCULAR_ID_1)
                           select new ServiceList
                           {
                               v_PersonId = a.v_PersonId,
                               v_Pacient = a.v_Pacient,
                               d_ServiceDate = a.d_ServiceDate,
                               EmpresaTrabajo = a.EmpresaTrabajo,
                               v_ServiceId = a.v_ServiceId,
                               v_ComponentId = a.v_ComponentId,

                               AbdomenExcelente = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_EXCELENTE)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_EXCELENTE).v_Value1,
                               AbdomenPromedio = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_PROMEDIO)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_PROMEDIO).v_Value1,
                               AbdomenRegular = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_REGULAR)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_REGULAR).v_Value1,
                               AbdomenPobre = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_POBRE)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_POBRE).v_Value1,
                               AbdomenPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PUNTOS_ABDOMEN)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PUNTOS_ABDOMEN).v_Value1,
                               AbdomenObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_OBSERVACIONES)== null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_OBSERVACIONES).v_Value1,
                               CaderaExcelente = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_EXCELENTE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_EXCELENTE).v_Value1,
                               CaderaPromedio = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PROMEDIO) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PROMEDIO).v_Value1,
                               CaderaRegular = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_REGULAR)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_REGULAR).v_Value1,
                               CaderaPobre = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_POBRE)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_POBRE).v_Value1,
                               CaderaPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PUNTOS)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PUNTOS).v_Value1,
                               CaderaObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADRA_OBSERVACIONES)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADRA_OBSERVACIONES).v_Value1,
                               MusloExcelente = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_EXCELENTE) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_EXCELENTE).v_Value1,
                               MusloPromedio = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants._MUSLO_PROMEDIO)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants._MUSLO_PROMEDIO).v_Value1,
                               MusloRegular = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_REGULAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_REGULAR).v_Value1,
                               MusloPobre = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_POBRE) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_POBRE).v_Value1,
                               MusloPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_PUNTOS) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_PUNTOS).v_Value1,
                               MusloObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_OBSERVACIONES) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_OBSERVACIONES).v_Value1,
                               AbdomenLateralExcelente = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_EXCELENTE)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_EXCELENTE).v_Value1,
                               AbdomenLateralPromedio = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PROMEDIO) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PROMEDIO).v_Value1,
                               AbdomenLateralRegular = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_REGULAR)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_REGULAR).v_Value1,                               
                               AbdomenLateralPobre = OsteoMuscular.Count == 0  || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_POBRE) == null ?string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_POBRE).v_Value1,                               
                               AbdomenLateralPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PUNTOS)== null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PUNTOS).v_Value1,
                               AbdomenLateralObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_OBSERVACIONES) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_OBSERVACIONES).v_Value1,
                               AbduccionHombroNormalOptimo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR).v_Value1,
                               AbduccionHombroNormalLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_NORMAL_LIMITADO_ID)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_NORMAL_LIMITADO_ID).v_Value1,
                               AbduccionHombroNormalMuyLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL_CONTRACTURA)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL_CONTRACTURA).v_Value1,
                               AbduccionHombroNormalPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR_CONTACTURA) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR_CONTACTURA).v_Value1,
                               AbduccionHombroNormalObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL) ==null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL).v_Value1Name,
                               AbduccionHombroOptimo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_OPTIMO_ID) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_OPTIMO_ID).v_Value1,
                               AbduccionHombroLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION).v_Value1,
                               AbduccionHombroMuyLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_MUY_LIMITADO_ID) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_MUY_LIMITADO_ID).v_Value1,                               
                               AbduccionHombroPuntos = OsteoMuscular.Count == 0 ||OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL)==null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL).v_Value1,
                               AbduccionHombroObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA).v_Value1Name,
                               RotacionExternaOptimo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC).v_Value1,
                               RotacionExternaLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH).v_Value1,
                               RotacionExternaMuyLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ROTACION_EXTERNA_MUY_LIMITADO_ID)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ROTACION_EXTERNA_MUY_LIMITADO_ID).v_Value1,
                               RotacionExternaPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION).v_Value1,
                               RotacionExternaObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ROTACION_EXTERNA_DOLOR_ID)  == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ROTACION_EXTERNA_DOLOR_ID).v_Value1Name,
                               RotacionExternaHombroInternoOptimo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR).v_Value1,
                               RotacionExternaHombroInternoLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO).v_Value1,
                               RotacionExternaHombroInternoMuyLimitado = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION).v_Value1,
                               RotacionExternaHombroInternoPuntos = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ).v_Value1,
                               RotacionExternaHombroInternoObs = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT).v_Value1Name,
                               Total1 = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOTAL) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOTAL).v_Value1,
                               Total2 = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC).v_Value1,
                               AptitudMusculoEsqueletico = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.APTITUD) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.APTITUD).v_Value1,
                               Conclusiones = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DESCRIPCION) == null? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DESCRIPCION).v_Value1,
                               AptitudMusculoEsqueleticoEspalda = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.APTITUDESPALDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.APTITUDESPALDA).v_Value1,
                               ReflejoTotulianoDerechoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_IZQUIERDO).v_Value1Name,
                               //ReflejoTotulianoIzquierdoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_REFLEJO_TOTULIANO_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_REFLEJO_TOTULIANO_IZQUIERDO).v_Value1Name,
                               ReflejoAquileoDerechoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_CAVO_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_CAVO_IZQUIERDO).v_Value1Name,
                               ReflejoAquileoIzquierdoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_DERECHO).v_Value1Name,
                               TestPhalenDerechoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_DERECHO).v_Value1,
                               TestPhalenIzquierdoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_IZQUIERDO).v_Value1,
                               TestTinelDerechoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_DERECHO).v_Value1,
                               TestTinelIzquierdoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_IZQUIERDO).v_Value1,
                               SignoLasagueIzquierdoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_IZQUIERDO).v_Value1,
                               SignoLasagueDerechoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_DERECHO).v_Value1,
                               SignoBragardIzquierdoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_IZQUIERDO).v_Value1,
                               SignoBragardDerechoSiNo = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_DERECHO).v_Value1,

                               b_Logo = MedicalCenter.b_Image,
                               EmpresaPropietaria = MedicalCenter.v_Name,
                               EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                               EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                               EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                               NombreUsuarioGraba = a.NombreUsuarioGraba
                           }).ToList();

                return sql;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Alberto
        public List<ReportAlturaEstructural> GetAlturaEstructural(string pstrserviceId, string pstrComponentId, string idComponentReport)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                                 join I in dbContext.systemparameter on new { a = C.i_EsoTypeId.Value, b = 118 }
                                                    equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                 from I in I_join.DefaultIfEmpty()


                                 join D in dbContext.organization on C.v_WorkingOrganizationId equals D.v_OrganizationId
                                 join D1 in dbContext.organization on C.v_CustomerOrganizationId equals D1.v_OrganizationId
                                 join E in dbContext.servicecomponent on new { a = A.v_ServiceId, b = pstrComponentId }
                                                                        equals new { a = E.v_ServiceId, b = E.v_ComponentId }

                                 join J1 in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                                    equals new { a = J1.i_ItemId, b = J1.i_GroupId } into J1_join
                                 from J1 in J1_join.DefaultIfEmpty()
                                 join F in dbContext.systemuser on E.i_ApprovedUpdateUserId equals F.i_SystemUserId into F_join
                                 from F in F_join.DefaultIfEmpty()
                                 join G in dbContext.professional on F.v_PersonId equals G.v_PersonId

                                 join Z in dbContext.person on F.v_PersonId equals Z.v_PersonId

                                 join BB in dbContext.protocol on A.v_ProtocolId equals BB.v_ProtocolId into BB_join
                                 from BB in BB_join.DefaultIfEmpty()

                                 join CC in dbContext.organization on BB.v_WorkingOrganizationId equals CC.v_OrganizationId into CC_join
                                 from CC in CC_join.DefaultIfEmpty()

                                 join C2 in dbContext.organization on BB.v_CustomerOrganizationId equals C2.v_OrganizationId into C2_join
                                 from C2 in C2_join.DefaultIfEmpty()

                                 join C1 in dbContext.organization on BB.v_EmployerOrganizationId equals C1.v_OrganizationId into C1_join
                                 from C1 in C1_join.DefaultIfEmpty()

                                 where A.v_ServiceId == pstrserviceId
                                 select new ReportAlturaEstructural
                                 {
                                     EmpresaCliente = C2.v_Name, //general
                                     EmpresaTrabajadora = C1.v_Name, //contrata
                                     EmpresaPropietariaDireccion = CC.v_Name , //subcontrata
                                     EmpresaPropietariaEmail = C1.v_Name + " / " + CC.v_Name,

                                     v_ComponentId = E.v_ServiceComponentId,
                                     v_ServiceId = A.v_ServiceId,
                                     NombrePaciente = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                     
                                     Fecha = A.d_ServiceDate.Value,
                                     FechaNacimiento = B.d_Birthdate.Value,
                                     PuestoTrabajo = B.v_CurrentOccupation,
                                     ServicioId = A.v_ServiceId,
                                     RubricaMedico = G.b_SignatureImage,
                                     RubricaTrabajador = B.b_RubricImage,
                                     HuellaTrabajador = B.b_FingerPrintImage,
                                     NombreUsuarioGraba = Z.v_FirstLastName + " " + Z.v_SecondLastName + " " + Z.v_FirstName,
                                     TipoExamen = I.v_Value1,
                                     DNI = B.v_DocNumber
                                 });


                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter(); 

                var funcionesVitales = serviceBL.ReportFuncionesVitales(pstrserviceId, Constants.FUNCIONES_VITALES_ID);
                var antropometria = serviceBL.ReportAntropometria(pstrserviceId, Constants.ANTROPOMETRIA_ID);
                var valores = new ServiceBL().ValoresComponente(pstrserviceId, idComponentReport);
                var sql = (from a in objEntity.ToList()
                           select new ReportAlturaEstructural
                            {
                                EmpresaCliente = a.EmpresaCliente,
                                EmpresaTrabajadora = a.EmpresaTrabajadora,
                                EmpresaPropietariaDireccion = a.EmpresaPropietariaDireccion,

                                v_ComponentId = a.v_ComponentId,
                                v_ServiceId = a.v_ServiceId,
                               ServicioId = a.ServicioId,
                               NombrePaciente = a.NombrePaciente,
                               
                               Fecha = a.Fecha,
                               FechaNacimiento = a.FechaNacimiento,
                               Edad = GetAge(a.FechaNacimiento),
                               PuestoTrabajo = a.PuestoTrabajo,
                                AntecedenteTecSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_SI_ID).v_Value1,
                                AntecedenteTecNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_NO_ID).v_Value1,
                                AntecedenteTecObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_OBS_ID).v_Value1,
                                ConvulsionesSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_SI_ID).v_Value1,
                                ConvulsionesNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_NO_ID).v_Value1,
                                ConvulsionesObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_OBS_ID).v_Value1,
                                MareosSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_MAREOS_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_MAREOS_SI_ID).v_Value1,
                                MareosNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_MAREOS_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_MAREOS_NO_ID).v_Value1,
                                MareosObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_MAREOS_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_MAREOS_OBS_ID).v_Value1,
                                AgorafobiaSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_SI_ID).v_Value1,
                                AgorafobiaNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_NO_ID).v_Value1,
                                AgorafobiaObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_OBS_ID).v_Value1,
                                AcrofobiaSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_SI_ID).v_Value1,
                                AcrofobiaNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_NO_ID).v_Value1,
                                AcrofobiaObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_OBS_ID).v_Value1,
                                InsuficienciaCardiacaSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_SI_ID).v_Value1,
                                InsuficienciaCardiacaNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_NO_ID).v_Value1,
                                InsuficienciaCardiacaObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_OBS_ID).v_Value1,
                                EstereopsiaSI = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_SI_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_SI_ID).v_Value1,
                                EstereopsiaNO = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_NO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_NO_ID).v_Value1,
                                EstereopsiaObs = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_OBS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_OBS_ID).v_Value1,
                                NistagmusEspontaneo = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_ESPONTANEO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_ESPONTANEO_ID).v_Value1,
                                NistagmusProvocado = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_PROVOCADO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_PROVOCADO_ID).v_Value1,
                                PrimerosAuxilios = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_PRIMEROS_AUXILIOS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_PRIMEROS_AUXILIOS_ID).v_Value1,
                                TrabajoNivelMar = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_TRABAJO_SOBRE_NIVEL_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_TRABAJO_SOBRE_NIVEL_ID).v_Value1,
                                Timpanos = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_TIMPANOS_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_TIMPANOS_ID).v_Value1,
                                Equilibrio = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_EQUILIBRIO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_EQUILIBRIO_ID).v_Value1,
                                SustentacionPie20Seg = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_SUST_PIE_20_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_SUST_PIE_20_ID).v_Value1,
                                CaminarLibre3Mts = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_RECTA_3_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_RECTA_3_ID).v_Value1,
                                CaminarLibreVendado3Mts = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_3_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_3_ID).v_Value1,
                                CaminarLibreVendadoPuntaTalon3Mts = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_PUNTA_TALON_3_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_PUNTA_TALON_3_ID).v_Value1,
                                Rotar = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ROTAR_SILLA_GIRATORIA_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ROTAR_SILLA_GIRATORIA_ID).v_Value1,
                                AdiadocoquinesiaDirecta = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_DIRECTA_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_DIRECTA_ID).v_Value1,
                                AdiadocoquinesiaCruzada = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_CRUZADA_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_CRUZADA_ID).v_Value1,
                                Apto = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_APTO_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_APTO_ID).v_Value1,
                                descripcion = valores.Count == 0 || valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID) == null ? string.Empty : valores.Find(p => p.v_ComponentFieldId == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).v_Value1,


                                   //AntecedenteTecSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_SI_ID, "NOCOMBO", 0, "SI"),
                                   //AntecedenteTecNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_NO_ID, "NOCOMBO", 0, "SI"),
                                   // AntecedenteTecObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //ConvulsionesSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_SI_ID, "NOCOMBO", 0, "SI"),
                                   //ConvulsionesNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_NO_ID, "NOCOMBO", 0, "SI"),
                                   //ConvulsionesObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //MareosSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_MAREOS_SI_ID, "NOCOMBO", 0, "SI"),
                                   //MareosNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_MAREOS_NO_ID, "NOCOMBO", 0, "SI"),
                                   //MareosObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_MAREOS_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //AgorafobiaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_SI_ID, "NOCOMBO", 0, "SI"),
                                   //AgorafobiaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_NO_ID, "NOCOMBO", 0, "SI"),
                                   //AgorafobiaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //AcrofobiaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_SI_ID, "NOCOMBO", 0, "SI"),
                                   //AcrofobiaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_NO_ID, "NOCOMBO", 0, "SI"),
                                   //AcrofobiaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //InsuficienciaCardiacaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_SI_ID, "NOCOMBO", 0, "SI"),
                                   //InsuficienciaCardiacaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_NO_ID, "NOCOMBO", 0, "SI"),
                                   //InsuficienciaCardiacaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //EstereopsiaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_SI_ID, "NOCOMBO", 0, "SI"),
                                   //EstereopsiaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_NO_ID, "NOCOMBO", 0, "SI"),
                                   //EstereopsiaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_OBS_ID, "NOCOMBO", 0, "SI"),

                                   //NistagmusEspontaneo = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_ESPONTANEO_ID, "NOCOMBO", 0, "SI"),
                                   //NistagmusProvocado = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_PROVOCADO_ID, "NOCOMBO", 0, "SI"),

                                   //PrimerosAuxilios = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_PRIMEROS_AUXILIOS_ID, "NOCOMBO", 0, "SI"),
                                   //TrabajoNivelMar = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_TRABAJO_SOBRE_NIVEL_ID, "NOCOMBO", 0, "SI"),

                                //Timpanos = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_TIMPANOS_ID, "NOCOMBO", 0, "SI"),
                                   //Equilibrio = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_EQUILIBRIO_ID, "NOCOMBO", 0, "SI"),
                                   //SustentacionPie20Seg = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_SUST_PIE_20_ID, "NOCOMBO", 0, "SI"),
                                   //CaminarLibre3Mts = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_RECTA_3_ID, "NOCOMBO", 0, "SI"),
                                   //CaminarLibreVendado3Mts = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_3_ID, "NOCOMBO", 0, "SI"),
                                   //CaminarLibreVendadoPuntaTalon3Mts = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_PUNTA_TALON_3_ID, "NOCOMBO", 0, "SI"),
                                   //Rotar = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ROTAR_SILLA_GIRATORIA_ID, "NOCOMBO", 0, "SI"),
                                   //AdiadocoquinesiaDirecta = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_DIRECTA_ID, "NOCOMBO", 0, "SI"),
                                   //AdiadocoquinesiaCruzada = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_CRUZADA_ID, "NOCOMBO", 0, "SI"),
                                   //Apto = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_APTO_ID, "NOCOMBO", 0, "SI"),
                                   //descripcion = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID, "NOCOMBO", 0, "SI"),
                               RubricaMedico = a.RubricaMedico,
                               RubricaTrabajador = a.RubricaTrabajador,
                               HuellaTrabajador = a.HuellaTrabajador,

                                b_Logo = MedicalCenter.b_Image,
                                EmpresaPropietaria = MedicalCenter.v_Name,
                                EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                                EmpresaPropietariaEmail = MedicalCenter.v_Mail,

                                IMC = antropometria.Count == 0 ? string.Empty : antropometria[0].IMC,
                                Peso = antropometria.Count == 0 ? string.Empty : antropometria[0].Peso,
                                FC = funcionesVitales.Count == 0 ? string.Empty : funcionesVitales[0].FC,
                                PA = funcionesVitales.Count == 0 ? string.Empty : funcionesVitales[0].PA,
                                FR = funcionesVitales.Count == 0 ? string.Empty : funcionesVitales[0].FR,
                                Sat = funcionesVitales.Count == 0 ? string.Empty : funcionesVitales[0].Sat,
                                PAD = funcionesVitales.Count == 0 ? string.Empty : funcionesVitales[0].PAD,
                                talla = antropometria.Count == 0 ? string.Empty : antropometria[0].talla,
                                NombreUsuarioGraba = a.NombreUsuarioGraba,
                                TipoExamen = a.TipoExamen,
                                DNI = a.DNI
                           }).ToList();

                return sql;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // Alberto
        public List<ReportOftalmologia> GetOftalmologia(string pstrserviceId, string pstrComponentId)
        {
       

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                                 join D in dbContext.organization on C.v_WorkingOrganizationId equals D.v_OrganizationId
                                 join E in dbContext.servicecomponent on new { a = A.v_ServiceId, b = pstrComponentId }
                                                                        equals new { a = E.v_ServiceId, b = E.v_ComponentId }

                                 join J1 in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                                    equals new { a = J1.i_ItemId, b = J1.i_GroupId } into J1_join
                                 from J1 in J1_join.DefaultIfEmpty()                          
                              
                                 // Usuario Medico Evaluador / Medico Aprobador ****************************
                                 join me in dbContext.systemuser on E.i_ApprovedUpdateUserId equals me.i_SystemUserId into me_join
                                 from me in me_join.DefaultIfEmpty()

                                 join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                 from pme in pme_join.DefaultIfEmpty()

                                 // Usuario Tecnologo *************************************
                                 join tec in dbContext.systemuser on E.i_UpdateUserTechnicalDataRegisterId equals tec.i_SystemUserId into tec_join
                                 from tec in tec_join.DefaultIfEmpty()

                                 join prtec in dbContext.professional on tec.v_PersonId equals prtec.v_PersonId into prtec_join
                                 from prtec in prtec_join.DefaultIfEmpty()

                                 join petec in dbContext.person on tec.v_PersonId equals petec.v_PersonId into petec_join
                                 from petec in petec_join.DefaultIfEmpty()
                                 // *******************************************************                            
                               
                                 where A.v_ServiceId == pstrserviceId
                               
                                 select new ReportOftalmologia
                                 {
                                     v_ComponentId =  E.v_ComponentId,
                                     v_ServiceId = A.v_ServiceId,
                                     NombrePaciente = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                     EmprresaTrabajadora = D.v_Name,
                                     FechaServicio = A.d_ServiceDate.Value,
                                     FechaNacimiento = B.d_Birthdate.Value,
                                     PuestoTrabajo = B.v_CurrentOccupation,
                                     FirmaDoctor = pme.b_SignatureImage,
                                     FirmaTecnologo = prtec.b_SignatureImage,
                                     NombreTecnologo = petec.v_FirstLastName + " " + petec.v_SecondLastName + " " + petec.v_FirstName,
                                     TipoEso = C.i_EsoTypeId.Value,
                                     HuellaPaciente = B.b_FingerPrintImage,
                                     FirmaPaciente = B.b_RubricImage
                                     //v_ServiceComponentId = E.v_ServiceComponentId
                                 });

                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter();

                var oftalmo = serviceBL.ValoresComponente(pstrserviceId, Constants.OFTALMOLOGIA_ID);
                var FondoOjo = serviceBL.ValoresComponente(pstrserviceId, Constants.FONDO_OJO_ID);
                var TestColores = serviceBL.ValoresComponente(pstrserviceId, Constants.TEST_ISHIHARA_ID);
                var TestEsterepsis = serviceBL.ValoresComponente(pstrserviceId, Constants.TEST_ESTEREOPSIS_ID);
                var Campimetria = serviceBL.ValoresComponente(pstrserviceId, Constants.CAMPIMETRIA_ID);
                var Tonometria = serviceBL.ValoresComponente(pstrserviceId, Constants.TONOMETRIA_ID);

                var sql = (from a in objEntity.ToList()

                            select new ReportOftalmologia
                            {
                                v_ServiceId = a.v_ServiceId,
                                NombrePaciente = a.NombrePaciente,
                                EmprresaTrabajadora = a.EmprresaTrabajadora,
                                FechaServicio = a.FechaServicio,
                                FechaNacimiento = a.FechaNacimiento,
                                Edad = GetAge(a.FechaNacimiento),
                                PuestoTrabajo = a.PuestoTrabajo,
                                TipoEso = a.TipoEso,
                                USO_DE_CORRECTORES = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000172") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000172").v_Value1,

                                HuellaPaciente = a.HuellaPaciente,
                                FirmaPaciente = a.FirmaPaciente,
                                SI  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000224") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000224").v_Value1,

                                NO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000719") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000719").v_Value1,

                                ULTIMAREFRACCION  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000225") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000225").v_Value1,

                                DIABETES  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000176") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000176").v_Value1,

                                HIPERTENSION  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000175") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000175").v_Value1,

                                SUSTQUIMICAS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000180") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000180").v_Value1,

                                EXPRADIACION  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000182") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000182").v_Value1,

                                MIOPIA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000709") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000709").v_Value1,

                                CIRUGIAOCULAR  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000181") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000181").v_Value1,

                                TRAUMAOCULAR  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000178") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000178").v_Value1,

                                GLAUCOMA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000177") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000177").v_Value1,

                                ASTIGMATISMO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000179") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000179").v_Value1,

                                OTROSESPECIFICAR  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000710") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000710").v_Value1,

                                SINPATOLOGIAS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002092") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002092").v_Value1,

                                OTRASPATOLOGIA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002091") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002091").v_Value1,

                                PTOSISPALPEBRAL  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002084") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002084").v_Value1,

                                CONJUNTIVITIS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002085") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002085").v_Value1,

                                PTERIGIUM  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002086") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002086").v_Value1,

                                ESTRABISMO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002087") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002087").v_Value1,

                                TRANSCORNEA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002088") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002088").v_Value1,

                                CATARATAS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002089") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002089").v_Value1,

                                CHALAZION  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002090") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002090").v_Value1,

                                ODSCLEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000637") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000637").v_Value1Name,

                                OI_SC_LEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000638") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000638").v_Value1Name,

                                OD_CC_LEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000639") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000639").v_Value1Name,

                                OI_CC_LEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000647") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000647").v_Value1Name,

                                OD_AE_LEJOS2 = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002078") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002078").v_Value1Name,

                                OI_AE_LEJOS2 = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002079") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002079").v_Value1Name,

                                SC_LEJOSOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000234") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000234").v_Value1Name,

                                SCLEJOSOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000230") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000230").v_Value1Name,

                                CCLEJOSOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000231") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000231").v_Value1Name,

                                CCLEJOSOJ_IZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000236") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000236").v_Value1Name,

                                AELEJOSOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002080") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002080").v_Value1,

                                AELEJOSOJOIZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002081") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002081").v_Value1,

                                SCCERCAOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000233") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000233").v_Value1Name,

                                S_CCERCAOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000227") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000227").v_Value1Name,

                                CCCERCAOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000235") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000235").v_Value1Name,

                                CCCERCAOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000646") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000646").v_Value1Name,

                                AECERCAOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002082") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002082").v_Value1,

                                AECERCAOJOIZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002083") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002083").v_Value1,

                                NORMAL  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000711") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000711").v_Value1,

                                ANORMAL  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000712") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000712").v_Value1,

                               
                                EMETROPE  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002071") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002071").v_Value1,

                                PRESBICIACORREGIDA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002073") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002073").v_Value1,

                                AMETROPIACORREGIDA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002072") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002072").v_Value1,

                                PRESBICIANOCORREGIDA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002074") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002074").v_Value1,

                                AMETROPIANOCORREGIA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002075") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002075").v_Value1,

                                AMBLIOPIA  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002076") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002076").v_Value1,

                                AMETROPIACORREGIDAPARCIAL = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002077") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002077").v_Value1,

                                DISMINUCIONDELA_VISIONUNOJO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002152") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002152").v_Value1,

                                DISMINUCIONDELAVISIONBILATERAL = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002153") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002153").v_Value1,


                                PARPADOOJODERECHO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000251") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000251").v_Value1,

                                PARPADOOJOIZQUIERDO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000252") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000252").v_Value1,

                                CONJUNTIVAOJODERECHO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000254") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000254").v_Value1,

                                CONJUNTIVAOJOIZQUIERDO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000255") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N002-MF000000255").v_Value1,

                                CORNEAOJODERECHO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000524") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000524").v_Value1,

                                CORNEAOJOIZQUIERDO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000525") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000525").v_Value1,

                                IRISOJODERECHO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000530") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000530").v_Value1,

                                IRISOJOIZQUIERDO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000531") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000531").v_Value1,

                                MOVOCULARESOJODERECHO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000533") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000533").v_Value1,

                                MOVOCULARESOJOIZQUIERDO = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000534") == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == "N009-MF000000534").v_Value1,



                                NORMAL2 = oftalmo.Count == 0 || TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000711") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000711").v_Value1,

                                ANORMAL2 = oftalmo.Count == 0 || TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000712") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000712").v_Value1,

                                DESCRIPCION2 = oftalmo.Count == 0 || TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000522") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000522").v_Value1Name,




                                NORMAL3 = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000343") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000343").v_Value1,

                                ANORMAL3 = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000342") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000342").v_Value1,

                                ENCANDILAMIENTO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000226") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000226").v_Value1,
                                DESCRIPCION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000261") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000261").v_Value1Name,

                                TIEMPO = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000258") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000258").v_Value1,

                                RECUPERACION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002093") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002093").v_Value1,




                                CAMPIMETRIAOD = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002094") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002094").v_Value1Name,

                                CAMPIMETRIAOI = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002095") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002095").v_Value1,


                                TONOMETRIAOD = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002096") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002096").v_Value1,

                                TONOMETRIAOI = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002097") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002097").v_Value1,

                                OFTALMOLOGIA_CRISTALINO_OJO_DERECHO_ID = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == Constants.OFTALMOLOGIA_CRISTALINO_OJO_DERECHO_ID) == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == Constants.OFTALMOLOGIA_CRISTALINO_OJO_DERECHO_ID).v_Value1,

                                OFTALMOLOGIA_CRISTALINO_OJO_IZQUIERDO_ID = FondoOjo.Count == 0 || FondoOjo.Find(p => p.v_ComponentFieldId == Constants.OFTALMOLOGIA_CRISTALINO_OJO_IZQUIERDO_ID) == null ? string.Empty : FondoOjo.Find(p => p.v_ComponentFieldId == Constants.OFTALMOLOGIA_CRISTALINO_OJO_IZQUIERDO_ID).v_Value1,


                                FirmaDoctor = a.FirmaDoctor,
                                FirmaTecnologo = a.FirmaTecnologo,
                                NombreTecnologo = a.NombreTecnologo,

                                b_Logo = MedicalCenter.b_Image,
                                EmpresaPropietaria = MedicalCenter.v_Name,
                                EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                                EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                                EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                                Dx = new ServiceBL().GetDiagnosticByServiceIdAndCategoryId(pstrserviceId, 14),
                                Recomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(14,pstrserviceId)

                            }).ToList();
                        return sql;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetDisgnosticsByServiceIdAndComponentConcatec(string pstrServiceId, string pstrComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = (from ccc in dbContext.diagnosticrepository
                                                        join ddd in dbContext.diseases on ccc.v_DiseasesId equals ddd.v_DiseasesId  // Diagnosticos                                                  

                                                       
                                                        where ccc.v_ServiceId == pstrServiceId && ccc.v_ComponentId == pstrComponentId &&
                                                              ccc.i_IsDeleted == 0

                                                        select new 
                                                        {
                                                         
                                                            v_DiseasesName = ddd.v_Name,

                                                        }).ToList();


               return string.Join(", ", query.Select(p => p.v_DiseasesName));
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public string GetDisgnosticsCIE10ByServiceIdAndComponentConcatec(string pstrServiceId, string pstrComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from ccc in dbContext.diagnosticrepository
                             join ddd in dbContext.diseases on ccc.v_DiseasesId equals ddd.v_DiseasesId  // Diagnosticos                                                  


                             where ccc.v_ServiceId == pstrServiceId && ccc.v_ComponentId == pstrComponentId &&
                                   ccc.i_IsDeleted == 0

                             select new
                             {
                                 v_DiseasesName = ddd.v_Name +"/" + ddd.v_CIE10Id,

                             }).ToList();


                return string.Join(", ", query.Select(p => p.v_DiseasesName));
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string GetRecomendationByServiceIdAndComponentConcatec(string pstrServiceId, string pstrComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from ccc in dbContext.recommendation
                             join ddd in dbContext.masterrecommendationrestricction on ccc.v_MasterRecommendationId equals ddd.v_MasterRecommendationRestricctionId                                                  


                             where ccc.v_ServiceId == pstrServiceId && ccc.v_ComponentId == pstrComponentId &&
                                   ccc.i_IsDeleted == 0

                             select new
                             {

                                 Recomendations = ddd.v_Name,

                             }).ToList();


                return string.Join(", ", query.Select(p => p.Recomendations));
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        // Alberto
        public string GetServiceComponentFielValue(string pstrServiceId, string pstrComponentId, string pstrFieldId, string Type , int pintParameter, string pstrConX )
        {
            try
            {
                ServiceBL oServiceBL = new ServiceBL();
                List<ServiceComponentFieldValuesList> oServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                string xx = "" ;
                if (Type == "NOCOMBO")
                {
                   oServiceComponentFieldValuesList = oServiceBL.ValoresComponente(pstrServiceId, pstrComponentId);
                   xx = oServiceComponentFieldValuesList.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;
                }
                else
                {
                    oServiceComponentFieldValuesList = oServiceBL.ValoresExamenComponete(pstrServiceId, pstrComponentId, pintParameter);
                    if (pstrConX == "SI")
                    {
                        xx = oServiceComponentFieldValuesList.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;
                    }
                    else
                    {
                        xx = oServiceComponentFieldValuesList.Count() == 0 ? string.Empty : ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1Name;
                    }
                    
                }
               
                return xx;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        //Alberto
        public List<ReportConsentimiento> GetReportConsentimiento(string pstrServiceId)
        {
            //mon.IsActive = true;
            var groupUbigeo = 113;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                 from B in B_join.DefaultIfEmpty()

                                 join C in dbContext.organization on B.v_EmployerOrganizationId equals C.v_OrganizationId into C_join
                                 from C in C_join.DefaultIfEmpty()
                                 //gaaa
                                 join C3 in dbContext.organization on B.v_CustomerOrganizationId equals C3.v_OrganizationId into C3_join
                                 from C3 in C3_join.DefaultIfEmpty()
                      
                                 join P1 in dbContext.person on new { a = A.v_PersonId }
                                         equals new { a = P1.v_PersonId } into P1_join
                                 from P1 in P1_join.DefaultIfEmpty()

                                 join p in dbContext.person on A.v_PersonId equals p.v_PersonId

                                 // Ubigeo de la persona *******************************************************
                                 join dep in dbContext.datahierarchy on new { a = p.i_DepartmentId.Value, b = groupUbigeo }
                                                      equals new { a = dep.i_ItemId, b = dep.i_GroupId } into dep_join
                                 from dep in dep_join.DefaultIfEmpty()

                                 join prov in dbContext.datahierarchy on new { a = p.i_ProvinceId.Value, b = groupUbigeo }
                                                       equals new { a = prov.i_ItemId, b = prov.i_GroupId } into prov_join
                                 from prov in prov_join.DefaultIfEmpty()

                                 join distri in dbContext.datahierarchy on new { a = p.i_DistrictId.Value, b = groupUbigeo }
                                                       equals new { a = distri.i_ItemId, b = distri.i_GroupId } into distri_join
                                 from distri in distri_join.DefaultIfEmpty()
                                 //*********************************************************************************************

                                 join BB in dbContext.protocol on A.v_ProtocolId equals BB.v_ProtocolId into BB_join
                                 from BB in B_join.DefaultIfEmpty()

                                 join CC in dbContext.organization on BB.v_WorkingOrganizationId equals CC.v_OrganizationId into CC_join
                                 from CC in C_join.DefaultIfEmpty()

                                 join C2 in dbContext.organization on BB.v_CustomerOrganizationId equals C2.v_OrganizationId into C2_join
                                 from C2 in C2_join.DefaultIfEmpty()

                                 join C1 in dbContext.organization on BB.v_EmployerOrganizationId equals C1.v_OrganizationId into C1_join
                                 from C1 in C1_join.DefaultIfEmpty()
                                 let varDpto = dep.v_Value1 == null ? "" : dep.v_Value1
                                 let varProv = prov.v_Value1 == null ? "" : prov.v_Value1
                                 let varDistri = distri.v_Value1 == null ? "" : distri.v_Value1

                                 where A.v_ServiceId == pstrServiceId

                                 select new ReportConsentimiento
                                 {
                                     NombreTrabajador = P1.v_FirstName + " " + P1.v_FirstLastName +  " " + P1.v_SecondLastName,
                                     NroDocumento = P1.v_DocNumber,
                                     Ocupacion = P1.v_CurrentOccupation,
                                     Contratista = C3.v_Name,
                                     FirmaTrabajador = P1.b_RubricImage,
                                     HuellaTrabajador = P1.b_FingerPrintImage,
                                     LugarProcedencia = varDistri + "-" + varProv + "-" + varDpto, // Santa Anita - Lima - Lima
                                     v_AdressLocation = p.v_AdressLocation,
                                     d_ServiceDate = A.d_ServiceDate,

                                     EmpresaPropietaria = C2.v_Name,
                                     Empresa = C1.v_Name,
                                     EmpresaPropietariaDireccion = C.v_Name,
                                     EmpresaPropietariaEmail = C1.v_Name + " / " + C.v_Name

                                 });

                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter();
                var ComponetesConcatenados = DevolverComponentesConcatenados(pstrServiceId);
                var ComponentesLaboratorioConcatenados = DevolverComponentesLaboratorioConcatenados(pstrServiceId);
                var sql = (from a in objEntity.ToList()
                           select new ReportConsentimiento
                           {
                               Fecha = DateTime.Now.ToShortDateString(),
                               Logo = MedicalCenter.b_Image,
                               NombreTrabajador = a.NombreTrabajador,
                               NroDocumento = a.NroDocumento,
                               Ocupacion = a.Ocupacion,
                               Empresa = a.Empresa,
                               Contratista = a.Contratista,
                               FirmaTrabajador = a.FirmaTrabajador,
                               HuellaTrabajador = a.HuellaTrabajador,
                               b_Logo = MedicalCenter.b_Image,
                               EmpresaPropietaria = MedicalCenter.v_Name,
                               EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                               EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                               EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                               LugarProcedencia = a.LugarProcedencia,
                               v_AdressLocation = a.v_AdressLocation,
                               v_ServiceDate = a.d_ServiceDate == null ? string.Empty : a.d_ServiceDate.Value.ToShortDateString(),
                               Componentes = ComponetesConcatenados,
                               ComponentesLaboratorio = ComponentesLaboratorioConcatenados
                           }).ToList();

                return sql;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string DevolverComponentesConcatenados(string pstrServiceId)
        {
           
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               int isRequired = (int)SiNo.SI;
               var objEntity = (from A in dbContext.service
                                join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                                join C in dbContext.component on B.v_ComponentId equals C.v_ComponentId

                                join E in dbContext.systemparameter on new { a = C.i_CategoryId.Value, b = 116 }
                                                                  equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                from E in E_join.DefaultIfEmpty()

                                where A.v_ServiceId == pstrServiceId && C.i_CategoryId != 1 && B.i_IsDeleted == 0 && B.i_IsRequiredId == isRequired
                                select new
                                {
                                    NombreComponente = E.v_Value1
                                }).ToList().Distinct();

               return string.Join(", ", objEntity.Select(p => p.NombreComponente));
        }

        public string DevolverComponentesLaboratorioConcatenados(string pstrServiceId)
        {
            int isRequired = (int)SiNo.SI;
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objEntity = (from A in dbContext.service
                             join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                             join C in dbContext.component on B.v_ComponentId equals C.v_ComponentId
                             where A.v_ServiceId == pstrServiceId && C.i_CategoryId == 1 && B.i_IsDeleted == 0 && B.v_ComponentId != Constants.INFORME_LABORATORIO_ID && B.i_IsRequiredId == isRequired
                             select new
                             {
                                 NombreComponente = C.v_Name
                             }).ToList();

            return string.Join(", ", objEntity.Select(p => p.NombreComponente));
        }

        #endregion       

        public List<InterfaceSeguimiento> GetInterfaceSeguimiento(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service
                                    join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                    join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                                    join D in dbContext.systemparameter on new { a = A.i_AptitudeStatusId.Value, b = 124 } equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                                    from D in D_join.DefaultIfEmpty()
                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin 
                                    //&& A.v_ServiceId=="N009-SR000003662"
                                    select new InterfaceSeguimiento
                                   {
                                        IdTrabajador = A.v_PersonId,
                                        IdServicio = A.v_ServiceId,
                                        Trabajador = B.v_FirstName + " " + B.v_FirstLastName + " " +B.v_SecondLastName,
                                        DNI = B.v_DocNumber,
                                        NombresTrabajador = B.v_FirstName,
                                        ApePaternoTrabajador = B.v_FirstLastName,
                                        ApeMaternoTrabajador = B.v_SecondLastName,
                                        TipoDocumentoTrabajador = B.i_DocTypeId,
                                        NroDocumentoTrabajador = B.v_DocNumber,
                                        FechaNacimiento = B.d_Birthdate,
                                        Direccion = B.v_AdressLocation,
                                        Telefono = B.v_TelephoneNumber,
                                        GeneroId = B.i_SexTypeId,
                                        Email = B.v_Mail,
                                        Puesto = B.v_CurrentOccupation,
                                        AreaId =1,
                                        Proveedor = "CENTRO MEDICO OCUPACIONAL HOLOSALUD",
                                        Protocolo = C.v_Name,
                                        FechaAtencion = A.d_ServiceDate,
                                        v_CustomerOrganizationId = C.v_CustomerOrganizationId,
                                        IdProtocolId = C.v_ProtocolId,
                                        v_CustomerLocationId = C.v_CustomerLocationId,
                                        AptitudId = A.i_AptitudeStatusId,
                                        Aptitud= D.v_Value1,
                                        Foto = B.b_PersonImage
                                   };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.IdTrabajador);
                        ServicioIds.Add(item.IdServicio);
                    }

                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);

                    var sql = (from a in objEntity.ToList()
                               let ValorPAS = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor
                               let ValorPAD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor

                               let Colesterol1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID) == null ? "  " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor == "" ? "" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor
                               let Colesterol2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor == "" ? "" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor

                               let Trigli1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor
                               let Trigli2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.TRIGLICERIDOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.TRIGLICERIDOS).Valor
                               let DxTriaje = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.ANTROPOMETRIA_ID)
                               let DxPA = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.FUNCIONES_VITALES_ID)

                               let DxExamenFisico = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.EXAMEN_FISICO_ID)

                               let DxOsteomuscular = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.OSTEO_MUSCULAR_ID_1)

                               let DxAudiometria = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.AUDIOMETRIA_ID)

                               let DxOftalmologia = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.OFTALMOLOGIA_ID)

                               let DxEspiro = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.ESPIROMETRIA_ID)

                               let DxRx = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.RX_TORAX_ID)
                               
                               let DxOIT = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.OIT_ID)

                               let DXEKG = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.ELECTROCARDIOGRAMA_ID)
                               
                               let DXGlucosa = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.GLUCOSA_ID)

                               let DXColesterol = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento_(a.IdServicio, Constants.COLESTEROL_ID, Constants.PERFIL_LIPIDICO)

                               let DXTrigliceridos = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento_(a.IdServicio, Constants.TRIGLICERIDOS_ID, Constants.PERFIL_LIPIDICO)

                               select new InterfaceSeguimiento
                                {

                                    IdTrabajador = a.IdTrabajador,
                                    IdProtocolId = a.IdProtocolId,
                                    v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                                    v_CustomerLocationId = a.v_CustomerLocationId,


                                    Trabajador = a.Trabajador,
                                    DNI = a.DNI,
                                    NombresTrabajador = a.NombresTrabajador,
                                    ApePaternoTrabajador = a.ApePaternoTrabajador,
                                    ApeMaternoTrabajador = a.ApeMaternoTrabajador,
                                    TipoDocumentoTrabajador = a.TipoDocumentoTrabajador,
                                    NroDocumentoTrabajador = a.NroDocumentoTrabajador,
                                    FechaNacimiento = a.FechaNacimiento,
                                    Direccion = a.Direccion,
                                    Telefono = a.Telefono,
                                    //10

                                   
                                    GeneroId = a.GeneroId,
                                    Email = a.Email,
                                    Puesto = a.Puesto,
                                    AreaId = a.AreaId,
                                    Proveedor = a.Proveedor,
                                    Protocolo = a.Protocolo,
                                    FechaAtencion = a.FechaAtencion,
                                    TabTipo = "",
                                    TabCantidad = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 1).v_Comment,
                                    TabFrecuencia = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 1).v_Frequency,
                                    //20

                                   
                                  
                                    AlcoTipo ="",
                                    AlcoCantidad = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 2).v_Comment,
                                    AlcoFrecuencia = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 2).v_Comment,
                                    DrogaTipo ="",
                                    DrogaCantidad = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 3).v_Comment,
                                    DrogaFrecuencia = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 3).v_Comment,                                   
                                    ActFisiFrecuencia = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 4).v_Comment,
                                    ActFisiDetalle = new ServiceBL().GetHabitoNoscivoSeguimiento(a.IdTrabajador, 4).v_Comment,
                                    DxActividadFisica = "",
                                    DxActividadFisicaCie10 = "",
                                    //30


                                 
                                    DxTabaquismo ="",
                                    DxTabaquismoCie10 ="",
                                    PresionAsistolica = ValorPAS + "/" + ValorPAD,
                                    Talla = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor,
                                    Peso = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor,
                                    Imc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor,
                                    DxNutricion = DxTriaje.Count > 0 ? DxTriaje.FindAll(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ? "a" : DxTriaje.FindAll(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID)[0].v_DiseasesName : "sin dx",
                                    DxNutricionCie10 = DxTriaje.Count > 0 ? DxTriaje.FindAll(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ? "a" : DxTriaje.FindAll(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID)[0].v_Cie10 : "sin dx",
                                    DxPresionArterial = DxPA.Count > 0 ? DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "a" : DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID)[0].v_DiseasesName : "sin dx",
                                    DxPresionArterialCie10 = DxPA.Count > 0 ? DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "a" : DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID)[0].v_Cie10 : "sin dx",
                                   
                                    //40   
                                    TriajeRecomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(10, a.IdServicio),
                                    TriajeRestricciones =new ServiceBL().ConcatenateRestrictionByCategoria(10,a.IdServicio),
                                    ExaFisicoConclusion = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_HALLAZGOS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_HALLAZGOS_ID).Valor,
                                    ExaFisicoDx1 = DxExamenFisico.Count > 0 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[0].v_DiseasesName : "sin dx",
                                    ExaFisicoDx1Cie10 = DxExamenFisico.Count > 0 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[0].v_Cie10 : "sin dx",
                                    ExaFisicoDx2 = DxExamenFisico.Count > 1 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[1].v_DiseasesName : "sin dx",
                                    ExaFisicoDx2Cie10 = DxExamenFisico.Count > 1 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[1].v_Cie10 : "sin dx",
                                    ExaFisicoDx3 = DxExamenFisico.Count > 2 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[2].v_DiseasesName : "sin dx",
                                    ExaFisicoDx3Cie10 = DxExamenFisico.Count > 2 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[2].v_Cie10 : "sin dx",
                                    ExaFisicoDx4 = DxExamenFisico.Count > 3 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[3].v_DiseasesName : "sin dx",
                                    //50
                                    
                                    ExaFisicoDx4Cie10 = DxExamenFisico.Count > 3 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[3].v_Cie10 : "sin dx",
                                    ExaFisicoDx5 = DxExamenFisico.Count > 4 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[4].v_DiseasesName : "sin dx",
                                    ExaFisicoDx5Cie10 = DxExamenFisico.Count > 4 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[4].v_Cie10 : "sin dx",
                                    ExaFisicoDx6 = DxExamenFisico.Count > 5 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[5].v_DiseasesName : "sin dx",
                                    ExaFisicoDx6Cie10 = DxExamenFisico.Count > 5 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[5].v_Cie10 : "sin dx",
                                    ExaFisicoDx7 = DxExamenFisico.Count > 6 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[6].v_DiseasesName : "sin dx",
                                    ExaFisicoDx7Cie10 = DxExamenFisico.Count > 6 ? DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID) == null ? "a" : DxExamenFisico.FindAll(p => p.v_ComponentId == Constants.EXAMEN_FISICO_ID)[6].v_Cie10 : "sin dx",
                                    ExaFisicoRecomendaciones =new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio,Constants.EXAMEN_FISICO_ID),
                                    ExaFisicoRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId( Constants.EXAMEN_FISICO_ID,a.IdServicio),
                                    //Osteomuscular
                                    OsteomuscularDescripcion = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OSTEO_MUSCULAR_ID_1) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OSTEO_MUSCULAR_ID_1 && o.IdCampo == Constants.DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OSTEO_MUSCULAR_ID_1 && o.IdCampo == Constants.DESCRIPCION).Valor,
                                    //60

                                    OsteomuscularConclusion ="",
                                    OsteomuscularDx1 = DxOsteomuscular.Count > 0 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[0].v_DiseasesName : "sin dx",
                                    OsteomuscularDx1Cie10 = DxOsteomuscular.Count > 0 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[0].v_Cie10 : "sin dx",
                                    OsteomuscularDx2 = DxOsteomuscular.Count > 1 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[1].v_DiseasesName : "sin dx",
                                    OsteomuscularDx2Cie10 = DxOsteomuscular.Count > 1 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[1].v_Cie10 : "sin dx",
                                    OsteomuscularDx3 = DxOsteomuscular.Count > 2 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[2].v_DiseasesName : "sin dx",
                                    OsteomuscularDx3Cie10 = DxOsteomuscular.Count > 2 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[2].v_Cie10 : "sin dx",
                                    OsteomuscularDx4 = DxOsteomuscular.Count > 3 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[3].v_DiseasesName : "sin dx",
                                    OsteomuscularDx4Cie10 = DxOsteomuscular.Count > 3 ? DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1) == null ? "a" : DxOsteomuscular.FindAll(p => p.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)[3].v_Cie10 : "sin dx",
                                    OsteomuscularRecomendaciones = new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio, Constants.OSTEO_MUSCULAR_ID_1),
                                    //70

                                    OsteomuscularRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId(Constants.OSTEO_MUSCULAR_ID_1, a.IdServicio),
                                    //Audimetría
                                    AudioOtoscopiaOd = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OD).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OD).Valor,
                                    AudioOtoscopiaOi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OI).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OI).Valor,
                                    AudioDx1 = DxAudiometria.Count > 0 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[0].v_DiseasesName : "sin dx",
                                    AudioDx1Cie10 = DxAudiometria.Count > 0 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[0].v_Cie10 : "sin dx",
                                    AudioDx2 = DxAudiometria.Count > 1 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[1].v_DiseasesName : "sin dx",
                                    AudioDx2Cie10 = DxAudiometria.Count > 1 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[1].v_Cie10 : "sin dx",
                                    AudioDx3 = DxAudiometria.Count > 2 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[2].v_DiseasesName : "sin dx",
                                    AudioDx3Cie10 = DxAudiometria.Count > 2 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[2].v_Cie10 : "sin dx",
                                    AudioDx4 = DxAudiometria.Count > 3 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[3].v_DiseasesName : "sin dx",
                                    //80


                                    AudioDx4Cie10 = DxAudiometria.Count > 3 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[3].v_Cie10 : "sin dx",
                                    AudioRecomendaciones = new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio, Constants.AUDIOMETRIA_ID),
                                    AudioRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId(Constants.AUDIOMETRIA_ID, a.IdServicio),
                                    //Oftalmología
                                    OftalmoAnamnesis ="",
                                    OftalmoVlScOd = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor,
                                    OftalmoVlScOi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID).Valor,
                                    OftalmoVlCrOd = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID).Valor,
                                    OftalmoVlCrOi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID).Valor,
                                    OftalmoVlEsOd = "Revisar",//varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AE_CERCA_OJO_IZQUIERDO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AE_CERCA_OJO_IZQUIERDO_ID).Valor,
                                    OftalmoVlEsOi = "Revisar",// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AE_CERCA_OJO_DERECHO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AE_CERCA_OJO_DERECHO_ID).Valor,
                                    //90
                                    
                                    OftalmoVcScOd = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_CERCA_OJO_DERECHO_ID).Valor,
                                    OftalmoVcScOi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_CERCA_OJO_IZQUIERDO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_CERCA_OJO_IZQUIERDO_ID).Valor,
                                    OftalmoVcCrOd = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_DERECHO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_DERECHO_ID).Valor,
                                    OftalmoVcCrOi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID).Valor,
                                    OftalmoVcEsOd = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.AE_CERCA_OJO_DERECHO).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.AE_CERCA_OJO_DERECHO).Valor,
                                    OftalmoVcEsOi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.AE_CERCA_OJO_IZQUIERDO).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.AE_CERCA_OJO_IZQUIERDO).Valor,
                                    OftalmoTestColoresOd ="",
                                    OftalmoTestColoresOi ="",
                                    OftalmoEstereopsisOd ="",
                                    OftalmoEstereopsisOi ="",
                                    //100
                                    
                                    OftalmoHallazgos ="",
                                    OftalmoDx1 = DxOftalmologia.Count > 0 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[0].v_DiseasesName : "sin dx",
                                    OftalmoDx1Cie10 = DxOftalmologia.Count > 0 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[0].v_Cie10 : "sin dx",
                                    OftalmoDx2 = DxOftalmologia.Count > 1 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[1].v_DiseasesName : "sin dx",
                                    OftalmoDx2Cie10 = DxOftalmologia.Count > 1 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[1].v_Cie10 : "sin dx",
                                    OftalmoDx3 =DxOftalmologia.Count > 2 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[2].v_DiseasesName : "sin dx",
                                    OftalmoDx3Cie10 =DxOftalmologia.Count > 2 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[2].v_Cie10 : "sin dx",
                                    OftalmoDx4 =DxOftalmologia.Count >3 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[3].v_DiseasesName : "sin dx",
                                    OftalmoDx4Cie10 =DxOftalmologia.Count > 3 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[3].v_Cie10 : "sin dx",
                                    OftalmoDx5 =DxOftalmologia.Count > 4 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[4].v_DiseasesName : "sin dx",
                                    //110


                                    OftalmoDx5Cie10 =DxOftalmologia.Count > 4 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[4].v_Cie10 : "sin dx",
                                    OftalmoDx6 = DxOftalmologia.Count > 5 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[5].v_DiseasesName : "sin dx",
                                    OftalmoDx6Cie10 = DxOftalmologia.Count > 5 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[5].v_Cie10 : "sin dx",
                                    OftalmoRecomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(15, a.IdServicio),
                                    OftalmoRestricciones = new ServiceBL().ConcatenateRestrictionByCategoria(15, a.IdServicio),                                    
                                    //Espirometría
                                    EspiroAntecedentes ="",
                                    EspiroObservacion ="",
                                    EspiroDx1 = DxEspiro.Count > 0 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[0].v_DiseasesName : "sin dx",
                                    EspiroDx1Cie10 = DxEspiro.Count > 0 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[0].v_Cie10 : "sin dx",
                                    EspiroDx2 = DxEspiro.Count > 1 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[1].v_DiseasesName : "sin dx",
                                    //120
                                    
                                    EspiroDx2Cie10 = DxEspiro.Count > 1 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[1].v_Cie10 : "sin dx",
                                    EspiroDx3 = DxEspiro.Count > 2 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[2].v_DiseasesName : "sin dx",
                                    EspiroDx3Cie10 = DxEspiro.Count > 2 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[2].v_Cie10 : "sin dx",
                                    EspiroDx4 = DxEspiro.Count > 3 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[3].v_DiseasesName : "sin dx",
                                    EspiroDx4Cie10 = DxEspiro.Count > 3 ? DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID) == null ? "a" : DxEspiro.FindAll(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID)[3].v_Cie10 : "sin dx",
                                    EspiroRecomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(16, a.IdServicio),
                                    EspiroRestricciones = new ServiceBL().ConcatenateRestrictionByCategoria(16, a.IdServicio),
                                    //Rx
                                    RxNroPlaca = "Revisar",//varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.RX_TORAX_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.RX_TORAX_ID && o.IdCampo == Constants.RX_CODIGO_PLACA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.RX_TORAX_ID && o.IdCampo == Constants.RX_CODIGO_PLACA_ID).Valor,
                                    RxConclusiones = "Revisar",//varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.RX_TORAX_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.RX_TORAX_ID && o.IdCampo == Constants.RX_HALLAZGOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.RX_TORAX_ID && o.IdCampo == Constants.RX_HALLAZGOS).Valor,
                                    RxDx1 = DxRx.Count > 0 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[0].v_DiseasesName : "sin dx",
                                    //130

                                    RxDx1Cie10 = DxRx.Count > 0 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[0].v_Cie10 : "sin dx",
                                    RxDx2 = DxRx.Count > 1 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[1].v_DiseasesName : "sin dx",
                                    RxDx2Cie10 = DxRx.Count > 1 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[1].v_Cie10 : "sin dx",
                                    RxDx3 = DxRx.Count > 2 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[2].v_DiseasesName : "sin dx",
                                    RxDx3Cie10 = DxRx.Count > 2 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[2].v_Cie10 : "sin dx",
                                    RxDx4 = DxRx.Count > 3 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[3].v_DiseasesName : "sin dx",
                                    RxDx4Cie10 = DxRx.Count > 3 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[3].v_Cie10 : "sin dx",
                                    RxRecomendaciones = new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio, Constants.RX_TORAX_ID),
                                    RxRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId(Constants.RX_TORAX_ID, a.IdServicio),
                                    //OIT
                                    OitNroPlaca = "Revisar",// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OIT_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OIT_ID && o.IdCampo == Constants.RX_CALIDAD_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OIT_ID && o.IdCampo == Constants.RX_CALIDAD_ID).Valor,
                                    //140


                                    OitNeumoconiosis ="",
                                    OitConclusiones = "Revisar",// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OIT_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OIT_ID && o.IdCampo == Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OIT_ID && o.IdCampo == Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID).Valor,
                                    OitConclusionesDescripcion ="",
                                    OitDx1 = DxOIT.Count > 0 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[0].v_DiseasesName : "sin dx",
                                    OitDx1Cie10 = DxOIT.Count > 0 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[0].v_Cie10 : "sin dx",
                                    OitDx2 = DxOIT.Count > 1 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[1].v_DiseasesName : "sin dx",
                                    OitDx2Cie10 = DxOIT.Count > 1 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[1].v_Cie10 : "sin dx",
                                    OitDx3 = DxOIT.Count > 2 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[2].v_DiseasesName : "sin dx",
                                    OitDx3Cie10 = DxOIT.Count > 2 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[2].v_Cie10 : "sin dx",
                                    OitDx4 = DxOIT.Count > 3 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[3].v_DiseasesName : "sin dx",
                                    //150
                                    
                                    OitDx4Cie10 = DxOIT.Count > 3 ? DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID) == null ? "a" : DxOIT.FindAll(p => p.v_ComponentId == Constants.OIT_ID)[3].v_Cie10 : "sin dx",
                                    OitRecomendaciones = new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio, Constants.OIT_ID),
                                    OitRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId(Constants.OIT_ID, a.IdServicio),
                                    //Psicología
                                    PsicoAreaCognitiva = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor,
                                    PsicoAreaEmocional = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_EMOCIONAL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_EMOCIONAL_ID).Valor,
                                    PsicoEvaEspaciosConfinados ="",
                                    PsicoEvaEspaciosAltura ="",
                                    PsicoRecomendaciones ="",
                                    PsicoRestricciones ="",
                                    //EKG
                                    EkgAntecedentes ="",
                                    //160

                                    EkgHr ="",
                                    EkgRr ="",
                                    EkgPq ="",
                                    EkgQrs = "Revisar",//varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == Constants.ELECTROCARDIOGRAMA_COMPLEJO_QRS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == Constants.ELECTROCARDIOGRAMA_COMPLEJO_QRS_ID).Valor,
                                    EkgQt ="",
                                    EkgQtc ="",
                                    EkgSt ="Revisar",// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == Constants.ELECTROCARDIOGRAMA_SEGMENTO_ST_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == Constants.ELECTROCARDIOGRAMA_SEGMENTO_ST_ID).Valor,
                                    EkgDx1 = DXEKG.Count > 0 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[0].v_DiseasesName : "sin dx",
                                    EkgDx1Ce10 = DXEKG.Count > 0 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[0].v_Cie10 : "sin dx",
                                    EkgDx2 = DXEKG.Count > 1 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[1].v_DiseasesName : "sin dx",
                                    //170


                                    EkgDx2Ce10 = DXEKG.Count > 1 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[1].v_Cie10 : "sin dx",
                                    EkgDx3 = DXEKG.Count > 2 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[2].v_DiseasesName : "sin dx",
                                    EkgDx3Ce10 = DXEKG.Count > 2 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[2].v_Cie10 : "sin dx",
                                    EkgDx4 = DXEKG.Count > 3 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[3].v_DiseasesName : "sin dx",
                                    EkgDx4Ce10 = DXEKG.Count > 3 ? DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID) == null ? "a" : DXEKG.FindAll(p => p.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)[3].v_Cie10 : "sin dx",
                                    EkgRecomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(18, a.IdServicio),
                                    EkgRestricciones = new ServiceBL().ConcatenateRestrictionByCategoria(18, a.IdServicio),
                                    //GrupoSanguíneo
                                    GrupoSanguineo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).ValorName,
                                    FactorSanguineo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).ValorName,
                                    //Colesterol
                                    Colesterol = Colesterol1 + " " + Colesterol2,
                                    //180


                                    ColesterolDx = DXColesterol.Count > 0 ? DXColesterol.FindAll(p => p.v_ComponentId == Constants.COLESTEROL_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO) == null ? "a" : DXColesterol.FindAll(p => p.v_ComponentId == Constants.COLESTEROL_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO)[0].v_DiseasesName : "sin dx",
                                    ColesterolDxCie10 = DXColesterol.Count > 0 ? DXColesterol.FindAll(p => p.v_ComponentId == Constants.COLESTEROL_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO) == null ? "a" : DXColesterol.FindAll(p => p.v_ComponentId == Constants.COLESTEROL_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO)[0].v_Cie10 : "sin dx",
                                    ColesterolRecomendaciones = new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio, Constants.COLESTEROL_ID),
                                    ColesterolRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId(Constants.COLESTEROL_ID, a.IdServicio),
                                    //Glucosa
                                    Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor,
                                    GlucosaDx = DXGlucosa.Count > 0 ? DXGlucosa.FindAll(p => p.v_ComponentId == Constants.GLUCOSA_ID) == null ? "a" : DXGlucosa.FindAll(p => p.v_ComponentId == Constants.GLUCOSA_ID)[0].v_DiseasesName : "sin dx",
                                    GlucosaDxCie10 = DXGlucosa.Count > 0 ? DXGlucosa.FindAll(p => p.v_ComponentId == Constants.GLUCOSA_ID) == null ? "a" : DXGlucosa.FindAll(p => p.v_ComponentId == Constants.GLUCOSA_ID)[0].v_Cie10 : "sin dx",
                                    GlucosaRecomendaciones = new ServiceBL().GetRecommendationByServiceIdAndComponent(a.IdServicio, Constants.GLUCOSA_ID),
                                    GlucosaRestricciones = new ServiceBL().ConcatenateRestrictionByComponentId(Constants.GLUCOSA_ID, a.IdServicio),
                                    //Triglicéridos
                                    Trigliceridos = Trigli1 + " " + Trigli2,
                                    //190

                                    TrigliceridosDx = DXTrigliceridos.Count > 0 ? DXTrigliceridos.FindAll(p => p.v_ComponentId == Constants.TRIGLICERIDOS_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO) == null ? "a" : DXTrigliceridos.FindAll(p => p.v_ComponentId == Constants.TRIGLICERIDOS_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO)[0].v_DiseasesName : "sin dx",
                                    TrigliceridosDxCie10 = DXTrigliceridos.Count > 0 ? DXTrigliceridos.FindAll(p => p.v_ComponentId == Constants.TRIGLICERIDOS_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO) == null ? "a" : DXTrigliceridos.FindAll(p => p.v_ComponentId == Constants.TRIGLICERIDOS_ID || p.v_ComponentId == Constants.PERFIL_LIPIDICO)[0].v_Cie10 : "sin dx",
                                    TrigliceridosRecomendaciones ="",
                                    TrigliceridosRestricciones ="",
                                    Aptitud =a.Aptitud,
                                    Comentario ="",
                                    AptitudId =a.AptitudId,
                                    Foto_String = Convert.ToBase64String(a.Foto) // System.Text.Encoding.UTF8.GetString(a.Foto)
                             
                                }).ToList();


                    return sql;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #region Matrix Excel
        public List<MatrizExcel> ReporteMatrizExcel(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            //Hola
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service
                                    join B in dbContext.person on A.v_PersonId equals B.v_PersonId

                                    join E1 in dbContext.datahierarchy on new { a = B.i_DepartmentId.Value, b = 113 }
                                                      equals new { a = E1.i_ItemId, b = E1.i_GroupId } into E1_join
                                    from E1 in E1_join.DefaultIfEmpty()

                                    join F1 in dbContext.datahierarchy on new { a = B.i_ProvinceId.Value, b = 113 }
                                                          equals new { a = F1.i_ItemId, b = F1.i_GroupId } into F1_join
                                    from F1 in F1_join.DefaultIfEmpty()

                                    join G1 in dbContext.datahierarchy on new { a = B.i_DistrictId.Value, b = 113 }
                                                          equals new { a = G1.i_ItemId, b = G1.i_GroupId } into G1_join
                                    from G1 in G1_join.DefaultIfEmpty()



                                    join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.organization on C.v_CustomerOrganizationId equals D.v_OrganizationId into D_join
                                    from D in D_join.DefaultIfEmpty()

                                    join E in dbContext.location on new { a = C.v_CustomerOrganizationId, b = C.v_CustomerLocationId }
                                                                      equals new { a = E.v_OrganizationId, b = E.v_LocationId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = B.i_MaritalStatusId.Value, b = 101 }
                                          equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.protocol on A.v_ProtocolId equals H.v_ProtocolId into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = H.i_EsoTypeId.Value, b = 118 }
                                                    equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.groupoccupation on H.v_GroupOccupationId equals J.v_GroupOccupationId

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    join J1 in dbContext.systemparameter on new { a = B.i_Relationship.Value, b = 207 }
                                              equals new { a = J1.i_ParameterId, b = J1.i_GroupId } into J1_join
                                    from J1 in J1_join.DefaultIfEmpty()

                                    join J2 in dbContext.systemparameter on new { a = A.i_ServiceTypeOfInsurance.Value, b = 225 }
                                           equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                                    from J2 in J2_join.DefaultIfEmpty()

                                    join J3 in dbContext.systemparameter on new { a = A.i_ModalityOfInsurance.Value, b = 226 }
                                       equals new { a = J3.i_ParameterId, b = J3.i_GroupId } into J3_join
                                    from J3 in J3_join.DefaultIfEmpty()

                                    join J4 in dbContext.systemparameter on new { a = A.i_AptitudeStatusId.Value, b = 124 }
                                       equals new { a = J4.i_ParameterId, b = J4.i_GroupId } into J4_join
                                    from J4 in J4_join.DefaultIfEmpty()

                                    join J5 in dbContext.systemparameter on new { a = A.i_MacId.Value, b = 134 }
                                       equals new { a = J5.i_ParameterId, b = J5.i_GroupId } into J5_join
                                    from J5 in J5_join.DefaultIfEmpty()

                                    // Usuario Medico Evaluador / Medico Aprobador ****************************
                                     join me in dbContext.systemuser on A.i_UpdateUserOccupationalMedicaltId equals me.i_SystemUserId into me_join
                                     from me in me_join.DefaultIfEmpty()

                                     join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                     from pme in pme_join.DefaultIfEmpty()

                                    join pe in dbContext.person on me.v_PersonId equals pe.v_PersonId into pe_join
                                    from pe in pe_join.DefaultIfEmpty()

                                    join P in dbContext.calendar on new { a = A.v_ServiceId, b = 1 } equals new { a = P.v_ServiceId, b = P.i_LineStatusId.Value }

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin 
                                    //&& A.i_ServiceStatusId == (int)ServiceStatus.Culminado
                                    select new MatrizExcel
                                    {
                                        IdServicio = A.v_ServiceId,
                                        IdTrabajador = B.v_PersonId,
                                        v_CustomerOrganizationId = H.v_CustomerOrganizationId,
                                        IdProtocolId = H.v_ProtocolId,
                                        v_CustomerLocationId = H.v_CustomerLocationId,
                                        NombreCompleto = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                        Dni = B.v_DocNumber,
                                        LugarNacimiento = B.v_BirthPlace,
                                        FechaNacimiento = B.d_Birthdate.Value,
                                        //Edad 
                                        //RangoEdad
                                        Sexo = B.i_SexTypeId == 1 ? "M" : "F",
                                        Domicilio = B.v_AdressLocation,
                                        Ubigueo = E1.v_Value1 + " - " + F1.v_Value1 + " - " + G1.v_Value1,
                                        EstadoCivil = F.v_Value1,
                                        NroHijos = B.i_NumberLivingChildren == null ? 0 : B.i_NumberLivingChildren.Value,
                                        NivelEstudio = G.v_Value1,
                                        Telefono = B.v_TelephoneNumber,
                                        EmpresaSede = D.v_Name + " " + E.v_Name,
                                        TipoExamen = I.v_Value1,
                                        Grupo = J.v_Name,
                                        PuestoPostula = B.v_CurrentOccupation,
                                        Area = K.v_Name,
                                        //Proveedor = "",
                                        FechaExamen = A.d_ServiceDate.Value,

                                        v_Menarquia = A.v_Menarquia,
                                        d_Fur = A.d_Fur.Value,
                                        v_CatemenialRegime = A.v_CatemenialRegime,
                                        d_PAP = A.d_PAP.Value,
                                        v_FechaUltimaMamo = A.v_FechaUltimaMamo,
                                        v_Gestapara = A.v_Gestapara,
                                        //i_MacId = A.i_MacId.Value,
                                        v_Mac = J5.v_Value1,
                                        v_CiruGine = A.v_CiruGine,
                                        v_ResultadosPAP = A.v_ResultadosPAP,
                                        v_ResultadoMamo = A.v_ResultadoMamo,
                                        Sintomatologia = A.v_Story,
                                        AptitudId = A.i_AptitudeStatusId,
                                        AptitudMedica = J4.v_Value1,                                        
                                        MotivoAptitud = A.v_ObsStatusService,
                                        ComentarioAptitud = A.v_ObsStatusService,
                                        Evaluador =  pe.v_FirstLastName + " " + pe.v_SecondLastName  + " "+ pe.v_FirstName ,
                                        CMP= pme.v_ProfessionalCode
                                    };


                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }


                    //Llenar los Servicios en una Lista de strings

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.IdTrabajador);
                        ServicioIds.Add(item.IdServicio);
                    }
                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var MedicalCenter = GetInfoMedicalCenter();
                    var varDx = DevolverJerarquiaDxMejoradoSinDescartados(ServicioIds);
                    var varDxConDescartados = DevolverJerarquiaDxMejoradoConDescartados(ServicioIds);
                    //var c = varDx.Find(p => p.ServicioId == "N009-SR000006637").DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID);
                    //var Dx_Rec = varDx.SelectMany(p => p.DetalleDxRecomendaciones).ToList();


                    string[] CamposIndiceNeumoconiosis = new string[] 
                    {
                        Constants.RX_0_NADA_ID,
                        Constants.RX_0_0_ID,
                        Constants.RX_0_1_ID,
                        Constants.RX_1_0_ID,
                        Constants.RX_1_1_ID,
                        Constants.RX_1_2_ID,
                        Constants.RX_2_1_ID,
                        Constants.RX_2_2_ID,
                        Constants.RX_2_3_ID,
                        Constants.RX_3_2_ID,
                        Constants.RX_3_3_ID,
                        Constants.RX_3_MAS_ID,
                    };




                    //var x = varValores.Find(p => p.ServicioId == "N009-SR000007020").CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID) == null ? "  " : varValores.Find(p => p.ServicioId == "N009-SR000007020").CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor == "" ? " " : varValores.Find(p => p.ServicioId == "N009-SR000007020").CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor;
                    //var x = varDxConDescartados.Find(p => p.ServicioId == "N009-SR000007020").DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL);

                    var sql = (from a in objEntity.ToList()
                               let ValorPAS = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor
                               let ValorPAD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor

                               let NutrcionDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID)
                               let Nutrcion = NutrcionDx != null ? string.Join("/ ", NutrcionDx.Select(p => p.Descripcion)) : "Normal"

                               let ExaMedGeneralDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EXAMEN_FISICO_ID)
                               let ExaMedGeneral = ExaMedGeneralDx != null ? string.Join("/ ", ExaMedGeneralDx.Select(p => p.Descripcion)) : "Normal"

                               let ExaMusculoEsqueleticoDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OSTEO_MUSCULAR_ID_1 || o.IdComponente == Constants.EVA_OSTEO_ID)
                               let ExaMusculoEsqueletico = ExaMusculoEsqueleticoDx != null ? string.Join("/ ", ExaMusculoEsqueleticoDx.Select(p => p.Descripcion)) : "Normal"


                               let EvaAlturaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID)
                               let EvaAltura = EvaAlturaDx != null ? string.Join("/ ", EvaAlturaDx.Select(p => p.Descripcion)) : "Normal"

                               let Exa7DDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ALTURA_7D_ID)
                               let Exa7D = Exa7DDx != null ? string.Join("/ ", Exa7DDx.Select(p => p.Descripcion)) : "Normal"


                               let EvaNeurologicaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID)
                               let EvaNeurologica = EvaNeurologicaDx != null ? string.Join("/ ", EvaNeurologicaDx.Select(p => p.Descripcion)) : "Normal"

                               let TamizajeDerDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID)
                               let TamizajeDer = TamizajeDerDx != null ? string.Join("/ ", TamizajeDerDx.Select(p => p.Descripcion)) : "Normal"

                               let RadioToraxDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.RX_TORAX_ID)
                               let RadioTorax = RadioToraxDx != null ? string.Join("/ ", RadioToraxDx.Select(p => p.Descripcion)) : "Radiografía de Torax Normal"

                               let RadioOITDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OIT_ID)
                               let RadioOIT = RadioOITDx != null ? string.Join("/ ", RadioOITDx.Select(p => p.Descripcion)) : "Radiografía de Torax Normal"

                               let X = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.FindAll(o => o.IdComponente == Constants.OIT_ID)
                               let Y = X.Count == 0 ? "" : X.Find(p => CamposIndiceNeumoconiosis.Contains(p.IdCampo) && p.Valor == "1").NombreCampo

                               let AudiometriaValores = ValoresComponenteOdontogramaValue1(a.IdServicio, Constants.AUDIOMETRIA_ID)

                               let AudiometriaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.AUDIOMETRIA_ID)
                               let AudiometriaDxs = AudiometriaDx != null ? string.Join("/ ", AudiometriaDx.Select(p => p.Descripcion)) : "Audición Normal"

                               let EspirometriaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ESPIROMETRIA_ID)
                               let Espirometria = EspirometriaDx != null ? string.Join("/ ", EspirometriaDx.Select(p => p.Descripcion)) : "Función Ventilatoria"

                               let UsaLentesNO = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CORRECTORES_OCULARES_NO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CORRECTORES_OCULARES_NO_ID).Valor == "1" ? "NO" : ""
                               let UsaLentesSI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CORRECTORES_OCULARES_SI_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CORRECTORES_OCULARES_SI_ID).Valor == "1" ? "SI" : ""

                               let IshiharaNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == Constants.TEST_ISHIHARA_NORMAL) == null ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == Constants.TEST_ISHIHARA_NORMAL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == Constants.TEST_ISHIHARA_NORMAL).Valor == "1" ? "Normal" : ""
                               let IshiharaAnormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == Constants.TEST_ISHIHARA_ANORMAL) == null ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == Constants.TEST_ISHIHARA_ANORMAL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == Constants.TEST_ISHIHARA_ANORMAL).Valor == "1" ? "Anormal" : ""

                               let EstereopsisNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_NORMAL_ID) == null ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_NORMAL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_NORMAL_ID).Valor == "1" ? "Normal" : ""
                               let EstereopsisAnormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_ANORMAL_ID) == null ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_ANORMAL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == Constants.OFTALMOLOGIA_TEST_ESTEREOPSIS_ANORMAL_ID).Valor == "1" ? "Anormal" : ""

                               let OftalmologiaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OFTALMOLOGIA_ID)
                               let Oftalmologia = OftalmologiaDx != null ? string.Join("/ ", OftalmologiaDx.Select(p => p.Descripcion)) : "Normal"

                               let OdontogramaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ODONTOGRAMA_ID)
                               let Odontograma = OdontogramaDx != null ? string.Join("/ ", OdontogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let ElectrocardiogramaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID || o.IdComponente == Constants.EVA_CARDIOLOGICA_ID)
                               let Electrocardiograma = ElectrocardiogramaDx != null ? string.Join("/ ", ElectrocardiogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let PbaEsfuerzoDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.PRUEBA_ESFUERZO_ID)
                               let PbaEsfuerzo = PbaEsfuerzoDx != null ? string.Join("/ ", PbaEsfuerzoDx.Select(p => p.Descripcion)) : "Normal"
                               
                               let PsicologiaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.PSICOLOGIA_ID)
                               let Psicologia = PsicologiaDx != null ? string.Join("/ ", PsicologiaDx.Select(p => p.Descripcion)) : "Normal"

                               let Grupo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).ValorName
                               let Factor = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).ValorName

                               let LeucocitosDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS)
                               let DxLeucocitos = LeucocitosDx == null ? null : LeucocitosDx != null ? string.Join("/ ", LeucocitosDx.Select(p => p.Descripcion)) : "Normal"

                               let HemoglobinaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA)
                               let DxHemoglobina = HemoglobinaDx == null ? null : HemoglobinaDx != null ? string.Join("/ ", HemoglobinaDx.Select(p => p.Descripcion)) : "Normal"

                               let HemogramaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID)
                               let DxHemograma = HemogramaDx == null ? null : HemogramaDx != null ? string.Join("/ ", HemogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let GlucosaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION)
                               let DxGlucosa = GlucosaDx == null ? null : GlucosaDx != null ? string.Join("/ ", GlucosaDx.Select(p => p.Descripcion)) : "Normal"

                               //Colesterol 1
                               let Colesterol1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID) == null ? "  " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor == "" ? "" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor
                               let Colesterol1Dx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID)
                               let DxColesterol1 = Colesterol1Dx == null ? null : Colesterol1Dx != null ? string.Join("/ ", Colesterol1Dx.Select(p => p.Descripcion)) : "Normal"

                               //Colesterol Lipidico
                               let Colesterol2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "Calibri" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor == "" ? "" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor
                               let Colesterol2Dx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_TOTAL)
                               let DxColesterol2 = Colesterol2Dx == null ? null : Colesterol2Dx != null ? string.Join("/ ", Colesterol2Dx.Select(p => p.Descripcion)) : "Normal"

                               //Trigli 1
                               let Trigli1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor
                               let TGCD1x = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS)
                               let DxTGC1 = TGCD1x == null ? "No Aplica" : TGCD1x.Count == 0 ? "Normal" : string.Join("/ ", TGCD1x.Select(p => p.Descripcion))// TGCD1x.Count == 0 ? null : TGCD1x != null ? string.Join("/ ", TGCD1x.Select(p => p.Descripcion)) : "Normal"


                               //Trigli 2
                               let Trigli2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.TRIGLICERIDOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.TRIGLICERIDOS).Valor
                               let TGCD2x = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.TRIGLICERIDOS)
                               let DxTGC2 = TGCD2x== null ? "No Aplica": TGCD2x.Count == 0 ? "Normal": string.Join("/ ", TGCD2x.Select(p => p.Descripcion))//TGCD2x.Count == 0 ? null : TGCD2x != null ? string.Join("/ ", TGCD2x.Select(p => p.Descripcion)) : "Normal"






                               let HDLDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o =>  o.IdCampo == Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL)
                               let DxHDL = HDLDx== null ? "No Aplica": HDLDx.Count == 0 ? "Normal": string.Join("/ ", HDLDx.Select(p => p.Descripcion))

                               let LDLDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL)
                               let DxLDL = LDLDx == null ? "No Aplica" : LDLDx.Count == 0 ? "Normal" : string.Join("/ ", LDLDx.Select(p => p.Descripcion))

                               let VLDLx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o =>  o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL)
                               let DxVLDL = VLDLx == null ? "No Aplica" : VLDLx.Count == 0 ? "Normal" : string.Join("/ ", VLDLx.Select(p => p.Descripcion))

                             
                               //let OrinaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID)
                               //let DxOrina = OrinaDx != null ? string.Join("/ ", OrinaDx.Select(p => p.Descripcion)) : "Normal"

                               let TGO1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor
                               let TGO2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor

                               let TGP1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor
                               let TGP2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor
                                 
                               select new MatrizExcel
                               {

                                   IdServicio = a.IdServicio,
                                   IdTrabajador = a.IdTrabajador,

                                   NombreCompleto = a.NombreCompleto,
                                   Dni = a.Dni,
                                   LugarNacimiento = a.LugarNacimiento,
                                   FechaNacimiento = a.FechaNacimiento,
                                   Edad = GetAge(a.FechaNacimiento.Value),
                                   RangoEdad = GetGrupoEtario(GetAge(a.FechaNacimiento.Value)),
                                   Sexo = a.Sexo,
                                   Domicilio = a.Domicilio,
                                   Ubigueo = a.Ubigueo,
                                   EstadoCivil = a.EstadoCivil,
                                   NroHijos = a.NroHijos,
                                   NivelEstudio = a.NivelEstudio,
                                   Telefono = a.Telefono,
                                   EmpresaSede = a.EmpresaSede,
                                   TipoExamen = a.TipoExamen,
                                   Grupo = a.Grupo,
                                   PuestoPostula = a.PuestoPostula,
                                   Area = a.Area,
                                   Proveedor = MedicalCenter.v_Name,
                                   FechaExamen = a.FechaExamen,

                                   ActividadFisica = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Frequency,
                                   ActividadFisicaDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Comment,
                                   ConsumoDrogas = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Frequency,
                                   ConsumoDrogasDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Comment,
                                   ConsumoAlcohol = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Frequency,
                                   ConsumoAlcoholDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Comment,
                                   ConsumoTabaco = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Frequency,
                                   ConsumoTabacoDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Comment,

                                   v_Menarquia = a.v_Menarquia,
                                   d_Fur = a.d_Fur,
                                   v_CatemenialRegime = a.v_CatemenialRegime,
                                   d_PAP = a.d_PAP,
                                   v_FechaUltimaMamo = a.v_FechaUltimaMamo,
                                   v_Gestapara = a.v_Gestapara,
                                   v_Mac = a.v_Mac,
                                   v_CiruGine = a.v_CiruGine,
                                   v_ResultadosPAP = a.v_ResultadosPAP,
                                   v_ResultadoMamo = a.v_ResultadoMamo,

                                   AnteGinecologicos = a.Sexo == "M" ? "No Aplica" : a.v_Menarquia + " / " + a.d_Fur + " / " + a.v_CatemenialRegime + " / " + a.d_PAP + " / " + a.v_FechaUltimaMamo + " / " + a.v_Gestapara + " / " + a.v_Mac + " / " + a.v_CiruGine + " / " + a.v_ResultadosPAP + " / " + a.v_ResultadoMamo,
                                   AntePatologicos = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " " : AntecedentesPatologicosConcatenados(Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical),
                                   AnteFamiliares = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaAntecedentesFamiliares == null ? " " : AntecedentesFamiliaresConcatenados(Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaAntecedentesFamiliares),

                                   Alergias = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   HipertensionArterial = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000436") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000436").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   AnteQuirurgicos = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000637") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000637").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Gastritis = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000401") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000401").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   DiabetesMellitus = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000642") == null ? " NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000642").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Tuberculosis = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000540") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000540").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Cancer = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000638") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000638").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Convulsiones = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000639") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000639").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   AsmaBronquial = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000599") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000599").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Otros = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_GroupName == "ENFERMEDADES OTROS") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_GroupName == "ENFERMEDADES OTROS").i_Answer.ToString() == "1" ? "SI" : "NO",

                                   Pa = ValorPAS + " / " + ValorPAD,
                                   Fr = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor,
                                   Fc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor,


                                   PerAbdominal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor,
                                   PerCadera = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor,
                                   Icc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor,


                                   Peso = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor,
                                   Talla = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor,
                                   Imc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor,


                                   DxNutricional = Nutrcion,
                                   Sintomatologia = a.Sintomatologia,
                                   PielAnexos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).Valor,







                                   Cabello = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).Valor,
                                   Ojos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).Valor,
                                   Oidos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).Valor,
                                   Nariz = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).Valor,

                                   Boca = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).Valor,
                                   Cuello = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).Valor,

                                   Torax = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).Valor,
                                   Cardiovascular = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).Valor,
                                   Abdomen = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).Valor,

                                   ApGenitourinario = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).Valor,
                                   Locomotor = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).Valor,
                                   Marcha = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).Valor,

                                   Columna = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).Valor,
                                   ExtremidadesSuperiores = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).Valor,
                                   ExtremidadesInferiores = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).Valor,
                                   SistemaLinfatico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).Valor,
                                   Neurologico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).Valor,

                                   Cabeza7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).Valor,
                                   Cuello7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION).Valor,
                                   Nariz7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION).Valor,

                                   Boca7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION).Valor,
                                   ReflejosPupilares7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION).Valor,
                                   MiembrosSuperiores7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION).Valor,
                                   MiembrosInferiores7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION).Valor,


                                   ReflejosOsteotendiosos7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION).Valor,
                                   Marcha7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION).Valor,
                                   Columna7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION).Valor,
                                   Abdomen7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION).Valor,

                                   AnillosIInguinales7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION).Valor,
                                   Hernias7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION).Valor,
                                   Varices7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION).Valor,
                                   Genitales7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION).Valor,
                                   Ganclios7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION).Valor,
                                   Pulmones7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION).Valor,
                                   TactoRectal7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION).Valor,


                                   DxExaMedicoGeneral = ExaMedGeneral == "" ? "NO APLICA" : ExaMedGeneral,
                                   DxMusculoEsqueletico = ExaMusculoEsqueletico == "" ? "NO APLICA" : ExaMusculoEsqueletico,



                                   EvAltura180 = EvaAltura == "" ? "NO APLICA" : EvaAltura,// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor,
                                   Exa7D = Exa7D == "" ? "NO APLICA" : Exa7D,//eva varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).ValorName,
                                   EvaNeurologica = EvaNeurologica == "" ? "NO APLICA" : EvaNeurologica, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor,
                                   TamizajeDermatologico = TamizajeDer == "" ? "NO APLICA" : TamizajeDer, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor,



                                   DxRadiografiaTorax = RadioTorax == "" ? "NO APLICA" : RadioTorax,
                                   DxRadiografiaOIT = RadioOIT == "" ? "NO APLICA" : RadioOIT,
                                   InidceNeumoconiosis = Y == "" ? "NO APLICA" : Y,

                                   OD_VA_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125).v_Value1,
                                   OD_VA_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250).v_Value1,
                                   OD_VA_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500).v_Value1,
                                   OD_VA_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000).v_Value1,
                                   OD_VA_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000).v_Value1,
                                   OD_VA_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000).v_Value1,
                                   OD_VA_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000).v_Value1,
                                   OD_VA_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000).v_Value1,
                                   OD_VA_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000).v_Value1,

                                   OI_VA_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125).v_Value1,
                                   OI_VA_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250).v_Value1,
                                   OI_VA_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500).v_Value1,
                                   OI_VA_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000).v_Value1,
                                   OI_VA_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000).v_Value1,
                                   OI_VA_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000).v_Value1,
                                   OI_VA_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000).v_Value1,
                                   OI_VA_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000).v_Value1,
                                   OI_VA_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000).v_Value1,


                                   OD_VO_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_125).v_Value1,
                                   OD_VO_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_250).v_Value1,
                                   OD_VO_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500).v_Value1,
                                   OD_VO_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000).v_Value1,
                                   OD_VO_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000).v_Value1,
                                   OD_VO_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000).v_Value1,
                                   OD_VO_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000).v_Value1,
                                   OD_VO_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000).v_Value1,
                                   OD_VO_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000).v_Value1,


                                   OI_VO_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_125).v_Value1,
                                   OI_VO_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_250).v_Value1,
                                   OI_VO_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500).v_Value1,
                                   OI_VO_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000).v_Value1,
                                   OI_VO_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000).v_Value1,
                                   OI_VO_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000).v_Value1,
                                   OI_VO_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000).v_Value1,
                                   OI_VO_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000).v_Value1,
                                   OI_VO_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000).v_Value1,
                                   Dxaudiometria = AudiometriaDxs == "" ? "NO APLICA" : AudiometriaDxs,


                                   Fvc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor,
                                   Fev1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor,
                                   Fev1_Fvc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF).Valor,
                                   Fev25_75 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75).Valor,
                                   DxEspirometria = Espirometria == "" ? "NO APLICA" : Espirometria,


                                   UsaLentes = UsaLentesSI + UsaLentesNO,
                                   VisionCercaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID).Valor,
                                   VisionCercaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor,

                                   AgudezaVisualLejosOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO).Valor,
                                   AgudezaVisualLejosOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO).Valor,
                                   VisionCercaCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID).Valor,
                                   VisionCercaCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID).Valor,
                                   AgudezaVisualLejosCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO).Valor,
                                   AgudezaVisualLejosCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID).Valor,

                                   TestIshihara = IshiharaNormal + IshiharaAnormal,
                                   Estereopsis = EstereopsisNormal + EstereopsisAnormal,
                                   DxOftalmología = new ServiceBL().GetDiagnosticByServiceIdAndCategoryId(a.IdServicio, 14),


                                   NroPiezasCaries = GetCantidadCaries(a.IdServicio, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID),
                                   NroPiezasAusentes = GetCantidadAusentes(a.IdServicio, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID),


                                   ritmo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000190").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000190").Valor,
                                   pr = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000187").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000187").Valor,
                                   qrs = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N009-MF000000225").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N009-MF000000225").Valor,
                                   qt = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000189").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000189").Valor,
                                   st = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N009-MF000001006").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N009-MF000001006").Valor,
                                   ejeqrs = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N009-MF000000143").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N009-MF000000143").Valor,
                                   frecuenciacardiaca = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000186").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID && o.IdCampo == "N002-MF000000186").Valor,





                                   DxOdontologia = Odontograma == "" ? "NO APLICA" : Odontograma,
                                   DxElectrocardiograma = Electrocardiograma == "" ? "NO APLICA" : Electrocardiograma,
                                   //PruebaEsfuerzo = PbaEsfuerzo== "" ? "NO APLICA" :PbaEsfuerzo ,

                                   AreaCognitiva = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor,
                                   AreaEmocional = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor,
                                   AreaPersonal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor,
                                   AptitudPsicologica = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor,
                                   DxPsicologia = Psicologia == "" ? "NO APLICA" : Psicologia,

                                   GrupoFactor = Grupo + " - " + Factor,
                                   //Leucocitos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor,
                                   //DxLeucocitos = DxLeucocitos ==null?"NO APLICA" :DxLeucocitos== "" ? "NORMAL" : DxLeucocitos,
                                   Hemoglobina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor,

                                   //DxHemoglobina = DxHemoglobina == null ? "NO APLICA" : DxHemoglobina == "" ? "NORMAL" : DxHemoglobina,
                                   //Eosi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor,
                                   //RecuentoPlaquetas = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor,

                                   //DxHemograma = DxHemograma == null ? "NO APLICA" : DxHemograma == "" ? "NORMAL" : DxHemograma,
                                   Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor,
                                   //DxGlucosa = DxGlucosa == null ? "NO APLICA" : DxGlucosa == "" ? "NORMAL" : DxGlucosa,
                                   Colesterol = Colesterol1,
                                   //DxColesterol = DxColesterol1 == "" ? "NO APLICA" : DxColesterol1 == "" ? "NORMAL" : DxColesterol1,
                                   Colesterolv2 = Colesterol2,
                                   //DxColesterolLipidico = DxColesterol2 == "" ? "NO APLICA" : DxColesterol2 == "" ? "NORMAL" : DxColesterol2,

                                   Hdl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor,
                                   //DxHdl =   DxHDL,// == "SinDx" ? "NORMAL" : DxHDL == "" ? "NORMAL" : DxHDL,
                                   Ldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor,
                                   //DxLdl = DxLDL == null ? "NO APLICA" : DxLDL == "" ? "NORMAL" : DxLDL,
                                   Vldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor,
                                   //DxVldl = DxVLDL == null ? "NO APLICA" : DxVLDL == "" ? "NORMAL" : DxVLDL,
                                   Trigliceridos = Trigli1 == "" ? "NO APLICA" : Trigli1,
                                   //DxTgc = DxTGC1 == null ? "NO APLICA" : DxTGC1 == "" ? "NORMAL" : DxTGC1,

                                   Trigliceridos2 = Trigli2 == "" ? "NO APLICA" : Trigli2,
                                   //DxTgc2 = DxTGC2 == null ? "NO APLICA" : DxTGC2 == "" ? "NORMAL" : DxTGC2,

                                   //Urea = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor,
                                   //Creatina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor,

                                   //Tgo = TGO1 + TGO2,
                                   //Tgp = TGP1 + TGP2,
                                   //Leuc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor,
                                   //Hemat = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor,

                                   Marihuana = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).ValorName,
                                   Cocaina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).ValorName,

                                   //Vdrl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor,
                                   //Colinesterasa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == "N009-ME000000042") == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == "N009-ME000000042" && o.IdCampo == "N009-MF000000393").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == "N009-ME000000042" && o.IdCampo == "N009-MF000000393").Valor,

                                   DxOcu1 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 0),
                                   DxOcu2 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 1),
                                   DxOcu3 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 2),
                                   DxOcu4 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 3),
                                   DxOcu5 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 4),
                                   DxOcu6 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 5),
                                   DxOcu7 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 6),
                                   DxOcu8 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 7),

                                   DxMed1 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 0),
                                   DxMed2 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 1),
                                   DxMed3 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 2),
                                   DxMed4 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 3),
                                   DxMed5 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 4),
                                   DxMed6 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 5),
                                   DxMed7 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 6),
                                   DxMed8 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 7),
                                   DxMed9 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 8),
                                   DxMed10 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 9),

                                   Reco1 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[18].Descripcion,
                                   Reco2 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[19].Descripcion,
                                   Reco3 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[20].Descripcion,
                                   Reco4 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[21].Descripcion,
                                   Reco5 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[22].Descripcion,
                                   Reco6 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[23].Descripcion,
                                   Reco7 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[24].Descripcion,
                                   Reco8 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[25].Descripcion,
                                   Reco9 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[26].Descripcion,
                                   Reco10 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[27].Descripcion,
                                   Reco11 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[28].Descripcion,
                                   Reco12 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[29].Descripcion,
                                   Reco13 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[30].Descripcion,
                                   Reco14 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[31].Descripcion,

                                   AptitudId = a.AptitudId,
                                   AptitudMedica = a.AptitudMedica,
                                   MotivoAptitud = a.AptitudId == (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   ComentarioAptitud = a.AptitudId != (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   Evaluador = a.Evaluador,
                                   CMP = a.CMP,
                                   Restricciones = ConcatenateRestrictionByService(a.IdServicio)
                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<MatrizExcel> ReporteMatrizExcelSanJoaquin(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service
                                    join B in dbContext.person on A.v_PersonId equals B.v_PersonId

                                    join E1 in dbContext.datahierarchy on new { a = B.i_DepartmentId.Value, b = 113 }
                                                      equals new { a = E1.i_ItemId, b = E1.i_GroupId } into E1_join
                                    from E1 in E1_join.DefaultIfEmpty()

                                    join F1 in dbContext.datahierarchy on new { a = B.i_ProvinceId.Value, b = 113 }
                                                          equals new { a = F1.i_ItemId, b = F1.i_GroupId } into F1_join
                                    from F1 in F1_join.DefaultIfEmpty()

                                    join G1 in dbContext.datahierarchy on new { a = B.i_DistrictId.Value, b = 113 }
                                                          equals new { a = G1.i_ItemId, b = G1.i_GroupId } into G1_join
                                    from G1 in G1_join.DefaultIfEmpty()



                                    join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.organization on C.v_CustomerOrganizationId equals D.v_OrganizationId into D_join
                                    from D in D_join.DefaultIfEmpty()

                                    join E in dbContext.location on new { a = C.v_CustomerOrganizationId, b = C.v_CustomerLocationId }
                                                                      equals new { a = E.v_OrganizationId, b = E.v_LocationId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = B.i_MaritalStatusId.Value, b = 101 }
                                          equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.protocol on A.v_ProtocolId equals H.v_ProtocolId into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = H.i_EsoTypeId.Value, b = 118 }
                                                    equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.groupoccupation on H.v_GroupOccupationId equals J.v_GroupOccupationId

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    join J1 in dbContext.systemparameter on new { a = B.i_Relationship.Value, b = 207 }
                                              equals new { a = J1.i_ParameterId, b = J1.i_GroupId } into J1_join
                                    from J1 in J1_join.DefaultIfEmpty()

                                    join J2 in dbContext.systemparameter on new { a = A.i_ServiceTypeOfInsurance.Value, b = 225 }
                                           equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                                    from J2 in J2_join.DefaultIfEmpty()

                                    join J3 in dbContext.systemparameter on new { a = A.i_ModalityOfInsurance.Value, b = 226 }
                                       equals new { a = J3.i_ParameterId, b = J3.i_GroupId } into J3_join
                                    from J3 in J3_join.DefaultIfEmpty()

                                    join J4 in dbContext.systemparameter on new { a = A.i_AptitudeStatusId.Value, b = 124 }
                                       equals new { a = J4.i_ParameterId, b = J4.i_GroupId } into J4_join
                                    from J4 in J4_join.DefaultIfEmpty()

                                    join J5 in dbContext.systemparameter on new { a = A.i_MacId.Value, b = 134 }
                                       equals new { a = J5.i_ParameterId, b = J5.i_GroupId } into J5_join
                                    from J5 in J5_join.DefaultIfEmpty()

                                    // Usuario Medico Evaluador / Medico Aprobador ****************************
                                    join me in dbContext.systemuser on A.i_UpdateUserOccupationalMedicaltId equals me.i_SystemUserId into me_join
                                    from me in me_join.DefaultIfEmpty()

                                    join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                    from pme in pme_join.DefaultIfEmpty()

                                    join pe in dbContext.person on me.v_PersonId equals pe.v_PersonId into pe_join
                                    from pe in pe_join.DefaultIfEmpty()

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin && A.i_ServiceStatusId == (int)ServiceStatus.Culminado
                                    select new MatrizExcel
                                    {
                                        IdServicio = A.v_ServiceId,
                                        IdTrabajador = B.v_PersonId,
                                        v_CustomerOrganizationId = H.v_CustomerOrganizationId,
                                        IdProtocolId = H.v_ProtocolId,
                                        v_CustomerLocationId = H.v_CustomerLocationId,
                                        NombreCompleto = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                        Dni = B.v_DocNumber,
                                        LugarNacimiento = B.v_BirthPlace,
                                        FechaNacimiento = B.d_Birthdate.Value,
                                        //Edad 
                                        //RangoEdad
                                        Sexo = B.i_SexTypeId == 1 ? "M" : "F",
                                        Domicilio = B.v_AdressLocation,
                                        Ubigueo = E1.v_Value1 + " - " + F1.v_Value1 + " - " + G1.v_Value1,
                                        EstadoCivil = F.v_Value1,
                                        NroHijos = B.i_NumberLivingChildren == null ? 0 : B.i_NumberLivingChildren.Value,
                                        NivelEstudio = G.v_Value1,
                                        Telefono = B.v_TelephoneNumber,
                                        EmpresaSede = D.v_Name + " " + E.v_Name,
                                        TipoExamen = I.v_Value1,
                                        Grupo = J.v_Name,
                                        PuestoPostula = B.v_CurrentOccupation,
                                        Area = K.v_Name,
                                        //Proveedor = "",
                                        FechaExamen = A.d_ServiceDate.Value,

                                        v_Menarquia = A.v_Menarquia,
                                        d_Fur = A.d_Fur.Value,
                                        v_CatemenialRegime = A.v_CatemenialRegime,
                                        d_PAP = A.d_PAP.Value,
                                        v_FechaUltimaMamo = A.v_FechaUltimaMamo,
                                        v_Gestapara = A.v_Gestapara,
                                        //i_MacId = A.i_MacId.Value,
                                        v_Mac = J5.v_Value1,
                                        v_CiruGine = A.v_CiruGine,
                                        v_ResultadosPAP = A.v_ResultadosPAP,
                                        v_ResultadoMamo = A.v_ResultadoMamo,
                                        Sintomatologia = A.v_Story,
                                        AptitudId = A.i_AptitudeStatusId,
                                        AptitudMedica = J4.v_Value1,
                                        MotivoAptitud = A.v_ObsStatusService,
                                        ComentarioAptitud = A.v_ObsStatusService,
                                        Evaluador = pe.v_FirstLastName + " " + pe.v_SecondLastName + " " + pe.v_FirstName,
                                        CMP = pme.v_ProfessionalCode
                                    };


                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }


                    //Llenar los Servicios en una Lista de strings

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.IdTrabajador);
                        ServicioIds.Add(item.IdServicio);
                    }
                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var MedicalCenter = GetInfoMedicalCenter();
                    var varDx = DevolverJerarquiaDxMejoradoSinDescartados(ServicioIds);
                    //var Dx_Rec = varDx.SelectMany(p => p.DetalleDxRecomendaciones).ToList();


                    string[] CamposIndiceNeumoconiosis = new string[] 
                    {
                        Constants.RX_0_NADA_ID,
                        Constants.RX_0_0_ID,
                        Constants.RX_0_1_ID,
                        Constants.RX_1_0_ID,
                        Constants.RX_1_1_ID,
                        Constants.RX_1_2_ID,
                        Constants.RX_2_1_ID,
                        Constants.RX_2_2_ID,
                        Constants.RX_2_3_ID,
                        Constants.RX_3_2_ID,
                        Constants.RX_3_3_ID,
                        Constants.RX_3_MAS_ID,
                    };


                    var sql = (from a in objEntity.ToList()
                               let ValorPAS = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor
                               let ValorPAD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor

                               let NutrcionDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID)
                               let Nutrcion = NutrcionDx != null ? string.Join("/ ", NutrcionDx.Select(p => p.Descripcion)) : "Normal"

                               let ExaMedGeneralDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EXAMEN_FISICO_ID)
                               let ExaMedGeneral = ExaMedGeneralDx != null ? string.Join("/ ", ExaMedGeneralDx.Select(p => p.Descripcion)) : "Normal"

                               let ExaMusculoEsqueleticoDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OSTEO_MUSCULAR_ID_1 || o.IdComponente == Constants.EVA_OSTEO_ID)
                               let ExaMusculoEsqueletico = ExaMusculoEsqueleticoDx != null ? string.Join("/ ", ExaMusculoEsqueleticoDx.Select(p => p.Descripcion)) : "Normal"


                               let EvaAlturaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID)
                               let EvaAltura = EvaAlturaDx != null ? string.Join("/ ", EvaAlturaDx.Select(p => p.Descripcion)) : "Normal"

                               let Exa7DDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ALTURA_7D_ID)
                               let Exa7D = Exa7DDx != null ? string.Join("/ ", Exa7DDx.Select(p => p.Descripcion)) : "Normal"


                               let EvaNeurologicaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID)
                               let EvaNeurologica = EvaNeurologicaDx != null ? string.Join("/ ", EvaNeurologicaDx.Select(p => p.Descripcion)) : "Normal"

                               let TamizajeDerDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID)
                               let TamizajeDer = TamizajeDerDx != null ? string.Join("/ ", TamizajeDerDx.Select(p => p.Descripcion)) : "Normal"

                               let RadioToraxDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.RX_TORAX_ID)
                               let RadioTorax = RadioToraxDx != null ? string.Join("/ ", RadioToraxDx.Select(p => p.Descripcion)) : "Radiografía de Torax Normal"

                               let RadioOITDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OIT_ID)
                               let RadioOIT = RadioOITDx != null ? string.Join("/ ", RadioOITDx.Select(p => p.Descripcion)) : "Radiografía de Torax Normal"

                               let X = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.FindAll(o => o.IdComponente == Constants.OIT_ID)
                               let Y = X.Count == 0 ? "" : X.Find(p => CamposIndiceNeumoconiosis.Contains(p.IdCampo) && p.Valor == "1").NombreCampo

                               let AudiometriaValores = ValoresComponenteOdontogramaValue1(a.IdServicio, Constants.AUDIOMETRIA_ID)

                               let AudiometriaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.AUDIOMETRIA_ID)
                               let AudiometriaDxs = AudiometriaDx != null ? string.Join("/ ", AudiometriaDx.Select(p => p.Descripcion)) : "Audición Normal"

                               let EspirometriaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ESPIROMETRIA_ID)
                               let Espirometria = EspirometriaDx != null ? string.Join("/ ", EspirometriaDx.Select(p => p.Descripcion)) : "Función Ventilatoria"


                               //Oftalmología--------------------------------
                               let UsaLentesNO = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000719").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000719").Valor == "1" ? "NO" : ""
                               let UsaLentesSI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000224").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000224").Valor == "1" ? "SI" : ""

                               let IshiharaNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000717") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000717").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000717").Valor == "1" ? "Normal" : ""
                               let IshiharaAnormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000718") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000718").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000718").Valor == "1" ? "Anormal" : ""

                               let EstereopsisNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000343") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000343").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000343").Valor == "1" ? "Normal" : ""
                               let EstereopsisAnormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000342") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000342").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000342").Valor == "1" ? "Anormal" : ""

                               let OftalmologiaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OFTALMOLOGIA_ID)
                               let Oftalmologia = OftalmologiaDx != null ? string.Join("/ ", OftalmologiaDx.Select(p => p.Descripcion)) : "Normal"

                               //------------------------------------------------------------------------------

                               let OdontogramaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ODONTOGRAMA_ID)
                               let Odontograma = OdontogramaDx != null ? string.Join("/ ", OdontogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let ElectrocardiogramaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID || o.IdComponente == Constants.EVA_CARDIOLOGICA_ID)
                               let Electrocardiograma = ElectrocardiogramaDx != null ? string.Join("/ ", ElectrocardiogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let PbaEsfuerzoDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.PRUEBA_ESFUERZO_ID)
                               let PbaEsfuerzo = PbaEsfuerzoDx != null ? string.Join("/ ", PbaEsfuerzoDx.Select(p => p.Descripcion)) : "Normal"

                               let PsicologiaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.PSICOLOGIA_ID)
                               let Psicologia = PsicologiaDx != null ? string.Join("/ ", PsicologiaDx.Select(p => p.Descripcion)) : "Normal"

                               let Grupo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).ValorName
                               let Factor = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).ValorName

                               let LeucocitosDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS)
                               let DxLeucocitos = LeucocitosDx != null ? string.Join("/ ", LeucocitosDx.Select(p => p.Descripcion)) : "Normal"

                               let HemoglobinaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA)
                               let DxHemoglobina = HemoglobinaDx != null ? string.Join("/ ", HemoglobinaDx.Select(p => p.Descripcion)) : "Normal"

                               let HemogramaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID)
                               let DxHemograma = HemogramaDx != null ? string.Join("/ ", HemogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let GlucosaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION)
                               let DxGlucosa = GlucosaDx != null ? string.Join("/ ", GlucosaDx.Select(p => p.Descripcion)) : "Normal"

                               let Colesterol1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID) == null ? "  " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor == "" ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor
                               let Colesterol2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor == "" ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor

                               let ColesterolDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_TOTAL_DESEABLE && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID)
                               let DxColesterol = ColesterolDx != null ? string.Join("/ ", ColesterolDx.Select(p => p.Descripcion)) : "Normal"

                               let HDLDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.COLESTEROL_HDL_ID && o.IdCampo == Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL)
                               let DxHDL = HDLDx != null ? string.Join("/ ", HDLDx.Select(p => p.Descripcion)) : "Normal"

                               let LDLDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.COLESTEROL_LDL_ID && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL)
                               let DxLDL = LDLDx != null ? string.Join("/ ", LDLDx.Select(p => p.Descripcion)) : "Normal"

                               let VLDLx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL)
                               let DxVLDL = VLDLx != null ? string.Join("/ ", VLDLx.Select(p => p.Descripcion)) : "Normal"

                               let Trigli1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor

                               let TGCDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_HDL_DESEABLE && o.IdCampo == Constants.TRIGLICERIDOS)
                               let DxTGC = TGCDx != null ? string.Join("/ ", TGCDx.Select(p => p.Descripcion)) : "Normal"


                               let TGO1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor
                               let TGO2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor

                               let TGP1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor
                               let TGP2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor

                               select new MatrizExcel
                               {

                                   IdServicio = a.IdServicio,
                                   IdTrabajador = a.IdTrabajador,

                                   NombreCompleto = a.NombreCompleto,
                                   Dni = a.Dni,
                                   LugarNacimiento = a.LugarNacimiento,
                                   FechaNacimiento = a.FechaNacimiento,
                                   Edad = GetAge(a.FechaNacimiento.Value),
                                   RangoEdad = GetGrupoEtario(GetAge(a.FechaNacimiento.Value)),
                                   Sexo = a.Sexo,
                                   Domicilio = a.Domicilio,
                                   Ubigueo = a.Ubigueo,
                                   EstadoCivil = a.EstadoCivil,
                                   NroHijos = a.NroHijos,
                                   NivelEstudio = a.NivelEstudio,
                                   Telefono = a.Telefono,
                                   EmpresaSede = a.EmpresaSede,
                                   TipoExamen = a.TipoExamen,
                                   Grupo = a.Grupo,
                                   PuestoPostula = a.PuestoPostula,
                                   Area = a.Area,
                                   Proveedor = MedicalCenter.v_Name,
                                   FechaExamen = a.FechaExamen,

                                   v_Menarquia = a.v_Menarquia,
                                   d_Fur = a.d_Fur,
                                   v_CatemenialRegime = a.v_CatemenialRegime,
                                   d_PAP = a.d_PAP,
                                   v_FechaUltimaMamo = a.v_FechaUltimaMamo,
                                   v_Gestapara = a.v_Gestapara,
                                   v_Mac = a.v_Mac,
                                   v_CiruGine = a.v_CiruGine,
                                   v_ResultadosPAP = a.v_ResultadosPAP,
                                   v_ResultadoMamo = a.v_ResultadoMamo,
                                   Pa = ValorPAS + " / " + ValorPAD,
                                   DxNutricional = Nutrcion,
                                   AnteGinecologicos = a.Sexo == "M" ? "No Aplica" : a.v_Menarquia + " / " + a.d_Fur + " / " + a.v_CatemenialRegime + " / " + a.d_PAP + " / " + a.v_FechaUltimaMamo + " / " + a.v_Gestapara + " / " + a.v_Mac + " / " + a.v_CiruGine + " / " + a.v_ResultadosPAP + " / " + a.v_ResultadoMamo,
                                   AntePatologicos = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " " : AntecedentesPatologicosConcatenados(Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical),
                                   AnteFamiliares = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaAntecedentesFamiliares == null ? " " : AntecedentesFamiliaresConcatenados(Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaAntecedentesFamiliares),

                                   Alergias = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   HipertensionArterial = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000436") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000436").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   AnteQuirurgicos = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000637") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000637").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Gastritis = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000401") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000401").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   DiabetesMellitus = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000642") == null ? " NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000642").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Tuberculosis = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000540") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000540").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Cancer = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000638") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000638").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Convulsiones = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000639") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000639").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   AsmaBronquial = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000599") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000599").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   Otros = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_GroupName == "ENFERMEDADES OTROS") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_GroupName == "ENFERMEDADES OTROS").i_Answer.ToString() == "1" ? "SI" : "NO",











                                   DxExaMedicoGeneral = ExaMedGeneral == "" ? "NO APLICA" : ExaMedGeneral,
                                   DxMusculoEsqueletico = ExaMusculoEsqueletico == "" ? "NO APLICA" : ExaMusculoEsqueletico,



                                   EvAltura180 = EvaAltura == "" ? "NO APLICA" : EvaAltura,// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor,
                                   Exa7D = Exa7D == "" ? "NO APLICA" : Exa7D,//eva varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).ValorName,
                                   EvaNeurologica = EvaNeurologica == "" ? "NO APLICA" : EvaNeurologica, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor,
                                   TamizajeDermatologico = TamizajeDer == "" ? "NO APLICA" : TamizajeDer, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor,



                                   DxRadiografiaTorax = RadioTorax == "" ? "NO APLICA" : RadioTorax,
                                   DxRadiografiaOIT = RadioOIT == "" ? "NO APLICA" : RadioOIT,
                                   InidceNeumoconiosis = Y == "" ? "NO APLICA" : Y,

                                   OD_VA_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125).v_Value1,
                                   OD_VA_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250).v_Value1,
                                   OD_VA_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500).v_Value1,
                                   OD_VA_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000).v_Value1,
                                   OD_VA_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000).v_Value1,
                                   OD_VA_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000).v_Value1,
                                   OD_VA_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000).v_Value1,
                                   OD_VA_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000).v_Value1,
                                   OD_VA_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000).v_Value1,

                                   OI_VA_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125).v_Value1,
                                   OI_VA_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250).v_Value1,
                                   OI_VA_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500).v_Value1,
                                   OI_VA_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000).v_Value1,
                                   OI_VA_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000).v_Value1,
                                   OI_VA_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000).v_Value1,
                                   OI_VA_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000).v_Value1,
                                   OI_VA_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000).v_Value1,
                                   OI_VA_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000).v_Value1,


                                   OD_VO_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_125).v_Value1,
                                   OD_VO_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_250).v_Value1,
                                   OD_VO_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500).v_Value1,
                                   OD_VO_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000).v_Value1,
                                   OD_VO_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000).v_Value1,
                                   OD_VO_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000).v_Value1,
                                   OD_VO_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000).v_Value1,
                                   OD_VO_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000).v_Value1,
                                   OD_VO_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000).v_Value1,


                                   OI_VO_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_125).v_Value1,
                                   OI_VO_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_250).v_Value1,
                                   OI_VO_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500).v_Value1,
                                   OI_VO_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000).v_Value1,
                                   OI_VO_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000).v_Value1,
                                   OI_VO_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000).v_Value1,
                                   OI_VO_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000).v_Value1,
                                   OI_VO_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000).v_Value1,
                                   OI_VO_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000).v_Value1,
                                   Dxaudiometria = AudiometriaDxs == "" ? "NO APLICA" : AudiometriaDxs,






                                   DxEspirometria = Espirometria == "" ? "NO APLICA" : Espirometria,

                                   //---Oftalmología
                                   UsaLentes = UsaLentesSI + UsaLentesNO,
                                   VisionCercaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor,
                                   VisionCercaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000230").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor,

                                   AgudezaVisualLejosOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor,
                                   AgudezaVisualLejosOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor,
                                   VisionCercaCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor,
                                   VisionCercaCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor,
                                   AgudezaVisualLejosCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor,
                                   AgudezaVisualLejosCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor,

                                   TestIshihara = IshiharaNormal + IshiharaAnormal,
                                   Estereopsis = EstereopsisNormal + EstereopsisAnormal,
                                   DxOftalmología = new ServiceBL().GetDiagnosticByServiceIdAndCategoryId(a.IdServicio, 14),
                                   //-----------------------


                                   DxOdontologia = Odontograma == "" ? "NO APLICA" : Odontograma,
                                   DxElectrocardiograma = Electrocardiograma == "" ? "NO APLICA" : Electrocardiograma,
                                   PruebaEsfuerzo = PbaEsfuerzo == "" ? "NO APLICA" : PbaEsfuerzo,

                                   DxPsicologia = Psicologia == "" ? "NO APLICA" : Psicologia,

                                   GrupoFactor = Grupo + " - " + Factor,
                                   DxLeucocitos = DxLeucocitos == "" ? "NO APLICA" : DxLeucocitos,
                                   DxHemoglobina = DxHemoglobina == "" ? "NO APLICA" : DxHemoglobina,
                                   DxHemograma = DxHemograma == "" ? "NO APLICA" : DxHemograma,
                                   DxGlucosa = DxGlucosa == "" ? "NO APLICA" : DxGlucosa,
                                   Colesterol = Colesterol1 + " " + Colesterol2,
                                   DxColesterol = DxColesterol == "" ? "NO APLICA" : DxColesterol,
                                   DxHdl = DxHDL == "" ? "NO APLICA" : DxHDL,
                                   DxLdl = DxLDL == "" ? "NO APLICA" : DxLDL,
                                   DxVldl = DxVLDL == "" ? "NO APLICA" : DxVLDL,
                                   Trigliceridos = Trigli1 == "" ? "NO APLICA" : Trigli1,
                                   DxTgc = DxTGC == "" ? "NO APLICA" : DxTGC,

                                   Tgo = TGO1 + TGO2,
                                   Tgp = TGP1 + TGP2,













                                   AreaCognitiva = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor,
                                   AreaEmocional = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor,
                                   //AreaPersonal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor,
                                   AptitudPsicologica = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor,

                                   Leucocitos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor,

                                   Hemoglobina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor,

                                   Eosi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor,
                                   RecuentoPlaquetas = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor,

                                   Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor,

                                   Hdl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor,

                                   Ldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_LDL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_LDL_ID && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_LDL_ID && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor,

                                   Vldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor,

                                   Urea = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor,
                                   Creatina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor,

                                   NroPiezasCaries = GetCantidadCaries(a.IdServicio, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID),
                                   NroPiezasAusentes = GetCantidadAusentes(a.IdServicio, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID),

                                   Cabello = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).Valor,
                                   Ojos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).Valor,
                                   Oidos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).Valor,
                                   Nariz = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).Valor,

                                   Boca = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).Valor,
                                   Cuello = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).Valor,

                                   Torax = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).Valor,
                                   Cardiovascular = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).Valor,
                                   Abdomen = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).Valor,

                                   ApGenitourinario = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).Valor,
                                   Locomotor = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).Valor,
                                   Marcha = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).Valor,

                                   Columna = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).Valor,
                                   ExtremidadesSuperiores = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).Valor,
                                   ExtremidadesInferiores = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).Valor,
                                   SistemaLinfatico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).Valor,
                                   Neurologico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).Valor,

                                   Cabeza7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).Valor,
                                   Cuello7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION).Valor,
                                   Nariz7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION).Valor,

                                   Boca7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION).Valor,
                                   ReflejosPupilares7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION).Valor,
                                   MiembrosSuperiores7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION).Valor,
                                   MiembrosInferiores7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION).Valor,


                                   ReflejosOsteotendiosos7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION).Valor,
                                   Marcha7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION).Valor,
                                   Columna7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION).Valor,
                                   Abdomen7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION).Valor,

                                   AnillosIInguinales7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION).Valor,
                                   Hernias7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION).Valor,
                                   Varices7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION).Valor,
                                   Genitales7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION).Valor,
                                   Ganclios7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION).Valor,
                                   Pulmones7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION).Valor,
                                   TactoRectal7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION).Valor,

                                   Fr = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor,
                                   Fc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor,
                                   PerAbdominal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor,
                                   PerCadera = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor,
                                   Icc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor,
                                   Peso = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor,
                                   Talla = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor,
                                   Imc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor,
                                   Sintomatologia = a.Sintomatologia,
                                   PielAnexos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).Valor,

                                   ActividadFisica = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Frequency,
                                   ActividadFisicaDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Comment,
                                   ConsumoDrogas = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Frequency,
                                   ConsumoDrogasDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Comment,
                                   ConsumoAlcohol = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Frequency,
                                   ConsumoAlcoholDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Comment,
                                   ConsumoTabaco = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Frequency,
                                   ConsumoTabacoDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Comment,


                                   Fvc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor,
                                   Fev1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor,
                                   Fev1_Fvc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF).Valor,
                                   Fev25_75 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75).Valor,

                                   Leuc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor,
                                   Hemat = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor,

                                   Marihuana = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).ValorName,
                                   Cocaina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).ValorName,

                                   Vdrl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor,

                                   DxOcu1 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 0),
                                   DxOcu2 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 1),
                                   DxOcu3 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 2),
                                   DxOcu4 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 3),
                                   DxOcu5 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 4),
                                   DxOcu6 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 5),
                                   DxOcu7 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 6),
                                   DxOcu8 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 7),

                                   DxMed1 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 0),
                                   DxMed2 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 1),
                                   DxMed3 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 2),
                                   DxMed4 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 3),
                                   DxMed5 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 4),
                                   DxMed6 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 5),
                                   DxMed7 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 6),
                                   DxMed8 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 7),
                                   DxMed9 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 8),
                                   DxMed10 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 9),

                                   Reco1 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[18].Descripcion,
                                   Reco2 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[19].Descripcion,
                                   Reco3 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[20].Descripcion,
                                   Reco4 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[21].Descripcion,
                                   Reco5 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[22].Descripcion,
                                   Reco6 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[23].Descripcion,
                                   Reco7 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[24].Descripcion,
                                   Reco8 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[25].Descripcion,
                                   Reco9 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[26].Descripcion,
                                   Reco10 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[27].Descripcion,
                                   Reco11 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[28].Descripcion,
                                   Reco12 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[29].Descripcion,
                                   Reco13 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[30].Descripcion,
                                   Reco14 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[31].Descripcion,

                                   AptitudId = a.AptitudId,
                                   AptitudMedica = a.AptitudMedica,
                                   MotivoAptitud = a.AptitudId == (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   ComentarioAptitud = a.AptitudId != (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   Evaluador = a.Evaluador,
                                   CMP = a.CMP,
                                   Restricciones = ConcatenateRestrictionByService(a.IdServicio)
                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<MatrizSeguimiento> ReporteMatrizSeguimientoSanJoaquin(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service
                                    join B in dbContext.person on A.v_PersonId equals B.v_PersonId

                                    join E1 in dbContext.datahierarchy on new { a = B.i_DepartmentId.Value, b = 113 }
                                                      equals new { a = E1.i_ItemId, b = E1.i_GroupId } into E1_join
                                    from E1 in E1_join.DefaultIfEmpty()

                                    join F1 in dbContext.datahierarchy on new { a = B.i_ProvinceId.Value, b = 113 }
                                                          equals new { a = F1.i_ItemId, b = F1.i_GroupId } into F1_join
                                    from F1 in F1_join.DefaultIfEmpty()

                                    join G1 in dbContext.datahierarchy on new { a = B.i_DistrictId.Value, b = 113 }
                                                          equals new { a = G1.i_ItemId, b = G1.i_GroupId } into G1_join
                                    from G1 in G1_join.DefaultIfEmpty()



                                    join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.organization on C.v_CustomerOrganizationId equals D.v_OrganizationId into D_join
                                    from D in D_join.DefaultIfEmpty()

                                    join E in dbContext.location on new { a = C.v_CustomerOrganizationId, b = C.v_CustomerLocationId }
                                                                      equals new { a = E.v_OrganizationId, b = E.v_LocationId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = B.i_MaritalStatusId.Value, b = 101 }
                                          equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = B.i_LevelOfId.Value, b = 108 }
                                                equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.protocol on A.v_ProtocolId equals H.v_ProtocolId into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = H.i_EsoTypeId.Value, b = 118 }
                                                    equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.groupoccupation on H.v_GroupOccupationId equals J.v_GroupOccupationId

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    join J1 in dbContext.systemparameter on new { a = B.i_Relationship.Value, b = 207 }
                                              equals new { a = J1.i_ParameterId, b = J1.i_GroupId } into J1_join
                                    from J1 in J1_join.DefaultIfEmpty()

                                    join J2 in dbContext.systemparameter on new { a = A.i_ServiceTypeOfInsurance.Value, b = 225 }
                                           equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                                    from J2 in J2_join.DefaultIfEmpty()

                                    join J3 in dbContext.systemparameter on new { a = A.i_ModalityOfInsurance.Value, b = 226 }
                                       equals new { a = J3.i_ParameterId, b = J3.i_GroupId } into J3_join
                                    from J3 in J3_join.DefaultIfEmpty()

                                    join J4 in dbContext.systemparameter on new { a = A.i_AptitudeStatusId.Value, b = 124 }
                                       equals new { a = J4.i_ParameterId, b = J4.i_GroupId } into J4_join
                                    from J4 in J4_join.DefaultIfEmpty()

                                    join J5 in dbContext.systemparameter on new { a = A.i_MacId.Value, b = 134 }
                                       equals new { a = J5.i_ParameterId, b = J5.i_GroupId } into J5_join
                                    from J5 in J5_join.DefaultIfEmpty()

                                    join J6 in dbContext.datahierarchy on new { a = B.i_DocTypeId.Value, b = 106 }
                                        equals new { a = J6.i_ItemId, b = J6.i_GroupId } into J6_join
                                    from J6 in J6_join.DefaultIfEmpty()

                                    join J7 in dbContext.systemparameter on new { a = B.i_SexTypeId.Value, b = 100 }
                                        equals new { a = J7.i_ParameterId, b = J7.i_GroupId } into J7_join
                                    from J7 in J7_join.DefaultIfEmpty()

                                    join J8 in dbContext.systemparameter on new { a = B.i_PlaceWorkId.Value, b = 204 }
                                        equals new { a = J8.i_ParameterId, b = J8.i_GroupId } into J8_join
                                    from J8 in J8_join.DefaultIfEmpty()

                                    // Usuario Medico Evaluador / Medico Aprobador ****************************
                                    join me in dbContext.systemuser on A.i_UpdateUserOccupationalMedicaltId equals me.i_SystemUserId into me_join
                                    from me in me_join.DefaultIfEmpty()

                                    join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                    from pme in pme_join.DefaultIfEmpty()

                                    join pe in dbContext.person on me.v_PersonId equals pe.v_PersonId into pe_join
                                    from pe in pe_join.DefaultIfEmpty()

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin && A.i_ServiceStatusId == (int)ServiceStatus.Culminado
                                    select new MatrizSeguimiento
                                    {
                                        Tipo_Documento = J6 == null ? "N/D" : J6.v_Value1,
                                        Tipo_Documento_ID = B.i_DocTypeId.HasValue ? B.i_DocTypeId.Value : 0,
                                        Nro_Documento = B.v_DocNumber,
                                        Nombres = B.v_FirstName,
                                        AP_Paterno = B.v_FirstLastName,
                                        AP_Materno = B.v_SecondLastName,
                                        Fecha_Nac = B.d_Birthdate.HasValue ? B.d_Birthdate.Value : DateTime.Now,
                                        Genero = J7 == null ? "N/D" : J7.v_Value1,
                                        Genero_ID = B.i_SexTypeId.HasValue ? B.i_SexTypeId.Value : 0,
                                        Grado_Inst = G == null ? "N/D" : G.v_Value1,
                                        Grado_Inst_ID = B.i_LevelOfId.HasValue ? B.i_LevelOfId.Value : 0,
                                        Puesto_Laboral = B.v_CurrentOccupation,
                                        Area = K == null ? "" : K.v_Name,
                                        Zona = J8 == null ? "N/D" : J8.v_Value1,
                                        Zona_ID = B.i_PlaceWorkId.HasValue ? B.i_PlaceWorkId.Value : 0,
                                        Lugar_de_Trabajo = G1 == null ? "N/D" : G1.v_Value1,
                                        Discapacitado = "",
                                        Discapacitado_ID = 0,
                                        Proveedor_Clinica = D == null ? "" : D.v_Name,
                                        RUC = D == null ? "" : D.v_IdentificationNumber,
                                        Fecha_Examen = A.d_ServiceDate.HasValue ? A.d_ServiceDate.Value : DateTime.Now,
                                        Tipo_Examen = I == null ? "N/D" : I.v_Value1,
                                        Tipo_Examen_ID = H.i_EsoTypeId.HasValue ? H.i_EsoTypeId.Value : 0,
                                        Aptitud = J4 == null ? "N/D" : J4.v_Value1,
                                        Aptitud_ID = A.i_AptitudeStatusId.HasValue ? A.i_AptitudeStatusId.Value : 0,
                                        v_CustomerOrganizationId = D == null ? "" : D.v_OrganizationId,
                                        v_CustomerLocationId = E == null ? "" : E.v_LocationId,
                                        IdServicio = A.v_ServiceId,
                                        IdTrabajador = B.v_PersonId
                                    };


                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }


                    //Llenar los Servicios en una Lista de strings

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.IdTrabajador);
                        ServicioIds.Add(item.IdServicio);
                    }
                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var MedicalCenter = GetInfoMedicalCenter();
                    var varDx = DevolverJerarquiaDxMejoradoSinDescartados(ServicioIds);
                    //var Dx_Rec = varDx.SelectMany(p => p.DetalleDxRecomendaciones).ToList();


                    string[] CamposIndiceNeumoconiosis = new string[] 
                    {
                        Constants.RX_0_NADA_ID,
                        Constants.RX_0_0_ID,
                        Constants.RX_0_1_ID,
                        Constants.RX_1_0_ID,
                        Constants.RX_1_1_ID,
                        Constants.RX_1_2_ID,
                        Constants.RX_2_1_ID,
                        Constants.RX_2_2_ID,
                        Constants.RX_2_3_ID,
                        Constants.RX_3_2_ID,
                        Constants.RX_3_3_ID,
                        Constants.RX_3_MAS_ID,
                    };

                    int Contador = 0;
                    decimal temporal;
                    var sql = (from a in objEntity.ToList()
                               let ValorPAS = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAS_ID).Valor
                               let ValorPAD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_PAD_ID).Valor

                               let NutrcionDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID)
                               let Nutrcion = NutrcionDx != null ? string.Join("/ ", NutrcionDx.Select(p => p.Descripcion)) : "Normal"

                               let ExaMedGeneralDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EXAMEN_FISICO_ID)
                               let ExaMedGeneral = ExaMedGeneralDx != null ? string.Join("/ ", ExaMedGeneralDx.Select(p => p.Descripcion)) : "Normal"

                               let ExaMusculoEsqueleticoDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OSTEO_MUSCULAR_ID_1 || o.IdComponente == Constants.EVA_OSTEO_ID)
                               let ExaMusculoEsqueletico = ExaMusculoEsqueleticoDx != null ? string.Join("/ ", ExaMusculoEsqueleticoDx.Select(p => p.Descripcion)) : "Normal"


                               let EvaAlturaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID)
                               let EvaAltura = EvaAlturaDx != null ? string.Join("/ ", EvaAlturaDx.Select(p => p.Descripcion)) : "Normal"

                               let Exa7DDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ALTURA_7D_ID)
                               let Exa7D = Exa7DDx != null ? string.Join("/ ", Exa7DDx.Select(p => p.Descripcion)) : "Normal"


                               let EvaNeurologicaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID)
                               let EvaNeurologica = EvaNeurologicaDx != null ? string.Join("/ ", EvaNeurologicaDx.Select(p => p.Descripcion)) : "Normal"

                               let TamizajeDerDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID)
                               let TamizajeDer = TamizajeDerDx != null ? string.Join("/ ", TamizajeDerDx.Select(p => p.Descripcion)) : "Normal"

                               let RadioToraxDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.RX_TORAX_ID)
                               let RadioTorax = RadioToraxDx != null ? string.Join("/ ", RadioToraxDx.Select(p => p.Descripcion)) : "Radiografía de Torax Normal"

                               let RadioOITDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OIT_ID)
                               let RadioOIT = RadioOITDx != null ? string.Join("/ ", RadioOITDx.Select(p => p.Descripcion)) : "Radiografía de Torax Normal"

                               let X = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.FindAll(o => o.IdComponente == Constants.OIT_ID)
                               let Y = X.Count == 0 ? "" : X.Find(p => CamposIndiceNeumoconiosis.Contains(p.IdCampo) && p.Valor == "1").NombreCampo

                               let AudiometriaValores = ValoresComponenteOdontogramaValue1(a.IdServicio, Constants.AUDIOMETRIA_ID)

                               let AudiometriaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.AUDIOMETRIA_ID)
                               let AudiometriaDxs = AudiometriaDx != null ? string.Join("/ ", AudiometriaDx.Select(p => p.Descripcion)) : "Audición Normal"

                               let EspirometriaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ESPIROMETRIA_ID)
                               let Espirometria = EspirometriaDx != null ? string.Join("/ ", EspirometriaDx.Select(p => p.Descripcion)) : "Función Ventilatoria"


                               //Oftalmología--------------------------------
                               let UsaLentesNO = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000719").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000719").Valor == "1" ? "NO" : ""
                               let UsaLentesSI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000224").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000224").Valor == "1" ? "SI" : ""

                               let IshiharaNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000717") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000717").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000717").Valor == "1" ? "Normal" : ""
                               let IshiharaAnormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000718") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000718").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ISHIHARA_ID && o.IdCampo == "N009-MF000000718").Valor == "1" ? "Anormal" : ""

                               let EstereopsisNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000343") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000343").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000343").Valor == "1" ? "Normal" : ""
                               let EstereopsisAnormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000342") == null ? "id no conincinde" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000342").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TEST_ESTEREOPSIS_ID && o.IdCampo == "N002-MF000000342").Valor == "1" ? "Anormal" : ""

                               let OftalmologiaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.OFTALMOLOGIA_ID)
                               let Oftalmologia = OftalmologiaDx != null ? string.Join("/ ", OftalmologiaDx.Select(p => p.Descripcion)) : "Normal"

                               //------------------------------------------------------------------------------

                               let OdontogramaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ODONTOGRAMA_ID)
                               let Odontograma = OdontogramaDx != null ? string.Join("/ ", OdontogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let ElectrocardiogramaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.ELECTROCARDIOGRAMA_ID || o.IdComponente == Constants.EVA_CARDIOLOGICA_ID)
                               let Electrocardiograma = ElectrocardiogramaDx != null ? string.Join("/ ", ElectrocardiogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let PbaEsfuerzoDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.PRUEBA_ESFUERZO_ID)
                               let PbaEsfuerzo = PbaEsfuerzoDx != null ? string.Join("/ ", PbaEsfuerzoDx.Select(p => p.Descripcion)) : "Normal"

                               let PsicologiaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.PSICOLOGIA_ID)
                               let Psicologia = PsicologiaDx != null ? string.Join("/ ", PsicologiaDx.Select(p => p.Descripcion)) : "Normal"

                               let Grupo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.GRUPO_SANGUINEO_ID).ValorName
                               let Factor = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID && o.IdCampo == Constants.FACTOR_SANGUINEO_ID).ValorName

                               let LeucocitosDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS)
                               let DxLeucocitos = LeucocitosDx != null ? string.Join("/ ", LeucocitosDx.Select(p => p.Descripcion)) : "Normal"

                               let HemoglobinaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA)
                               let DxHemoglobina = HemoglobinaDx != null ? string.Join("/ ", HemoglobinaDx.Select(p => p.Descripcion)) : "Normal"

                               let HemogramaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID)
                               let DxHemograma = HemogramaDx != null ? string.Join("/ ", HemogramaDx.Select(p => p.Descripcion)) : "Normal"

                               let GlucosaDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION)
                               let DxGlucosa = GlucosaDx != null ? string.Join("/ ", GlucosaDx.Select(p => p.Descripcion)) : "Normal"

                               let Colesterol1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID) == null ? "  " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor == "" ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_ID && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).Valor
                               let Colesterol2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor == "" ? " " : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_TOTAL).Valor

                               let ColesterolDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_TOTAL_DESEABLE && o.IdCampo == Constants.COLESTEROL_COLESTEROL_TOTAL_ID)
                               let DxColesterol = ColesterolDx != null ? string.Join("/ ", ColesterolDx.Select(p => p.Descripcion)) : "Normal"

                               let HDLDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.COLESTEROL_HDL_ID && o.IdCampo == Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL)
                               let DxHDL = HDLDx != null ? string.Join("/ ", HDLDx.Select(p => p.Descripcion)) : "Normal"

                               let LDLDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.COLESTEROL_LDL_ID && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL)
                               let DxLDL = LDLDx != null ? string.Join("/ ", LDLDx.Select(p => p.Descripcion)) : "Normal"

                               let VLDLx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL)
                               let DxVLDL = VLDLx != null ? string.Join("/ ", VLDLx.Select(p => p.Descripcion)) : "Normal"

                               let Trigli1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TRIGLICERIDOS_ID && o.IdCampo == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).Valor

                               let TGCDx = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_HDL_DESEABLE && o.IdCampo == Constants.TRIGLICERIDOS)
                               let DxTGC = TGCDx != null ? string.Join("/ ", TGCDx.Select(p => p.Descripcion)) : "Normal"


                               let TGO1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor
                               let TGO2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor

                               let TGP1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor
                               let TGP2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor


                               let DxTriaje = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.ANTROPOMETRIA_ID)
                               let DxPA = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.FUNCIONES_VITALES_ID)
                               let DxOftalmologia = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.OFTALMOLOGIA_ID)
                               let DxAudiometria = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.AUDIOMETRIA_ID)

                               let DxRx = new ServiceBL().GetDiagnosticByServiceIdAndComponentSeguimiento(a.IdServicio, Constants.RX_TORAX_ID)
                               
                               select new MatrizSeguimiento
                               {
                                   Nro = Contador++,
                                   //////////// DATOS PERSONA ////////////

                                   Tipo_Documento = a.Tipo_Documento,
                                   Tipo_Documento_ID = a.Tipo_Documento_ID,
                                   Nro_Documento = a.Nro_Documento,
                                   Nombres = a.Nombres,
                                   AP_Paterno = a.AP_Paterno,
                                   AP_Materno = a.AP_Materno,
                                   Fecha_Nac = a.Fecha_Nac,
                                   Edad = GetAge(a.Fecha_Nac),
                                   Genero = a.Genero,
                                   Genero_ID = a.Genero_ID,
                                   Grado_Inst = a.Grado_Inst,
                                   Grado_Inst_ID = a.Grado_Inst_ID,
                                   Puesto_Laboral = a.Puesto_Laboral,
                                   Area = a.Area,
                                   Zona = a.Zona,
                                   Zona_ID = a.Zona_ID,
                                   Lugar_de_Trabajo = a.Lugar_de_Trabajo,
                                   Discapacitado = a.Discapacitado,
                                   Discapacitado_ID = a.Discapacitado_ID,
                                   Proveedor_Clinica = a.Proveedor_Clinica,
                                   RUC = a.RUC,
                                   Fecha_Examen = a.Fecha_Examen,
                                   Tipo_Examen = a.Tipo_Examen,
                                   Tipo_Examen_ID = a.Tipo_Examen_ID,
                                   Aptitud = a.Aptitud,
                                   Aptitud_ID = a.Aptitud_ID,
                                   ////////////////// HABITOS NOSCIVOS ////////////////////
                                   Fumar = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "NADA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "NADA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1) == null ? "NADA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1).v_Frequency,
                                   Fumar_ID = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1) == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1).v_Frequency == "NADA" ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1).v_Frequency == "POCO" ? 2 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1).v_Frequency == "HABITUAL" ? 3 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 1).v_Frequency == "EXCESIVO" ? 4 : 0,
                                   Licor = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "NUNCA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "NUNCA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2) == null ? "NUNCA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2).v_Frequency,
                                   Licor_ID = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2) == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2).v_Frequency == "NUNCA" ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2).v_Frequency == "POCO" ? 2 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2).v_Frequency == "HABITUAL" ? 3 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 2).v_Frequency == "EXCESIVO" ? 4 : 0,
                                   Drogas = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "NUNCA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "NUNCA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3) == null ? "NUNCA" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3).v_Frequency,
                                   Drogas_ID = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3) == null ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3).v_Frequency == "NUNCA" ? 1 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3).v_Frequency == "POCO" ? 2 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3).v_Frequency == "HABITUAL" ? 3 : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == 3).v_Frequency == "EXCESIVO" ? 4 : 0,
                                   ////////////////////// TRIAJE //////////////////////////
                                   Peso = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor),
                                   Talla = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor),
                                   IMC = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor),
                                   
                                   IMC_CIE10 = DxTriaje.Count > 0 ? DxTriaje.FindAll(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ? "a" : DxTriaje.FindAll(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID)[0].v_Cie10 : "sin dx",
                                   
                                   IMC_Obs = "",
                                   Cintura = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor),
                                   Cadera = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor),
                                   ICC = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor),
                                   Sistolica = ValorPAS,
                                   Sistolica_CIE10 = DxPA.Count > 0 ? DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "a" : DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID)[0].v_Cie10 : "sin dx",
                                   Sistolica_Obs = "",
                                   Diastolica = ValorPAD,
                                   Diastolica_CIE10 = DxPA.Count > 0 ? DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID) == null ? "a" : DxPA.FindAll(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID)[0].v_Cie10 : "sin dx",
                                   Diastolica_Obs = "?????????",
                                   FC = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor),
                                   FR = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor),
                                   ///////////////////// OFTALMO //////////////////////////
                                   Sin_Corr_Cerca_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").ValorName,
                                   Sin_Corr_Cerca_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000230").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).ValorName,
                                   Sin_Corr_Lejos_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").ValorName,
                                   Sin_Corr_Lejos_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").ValorName,
                                   Corr_Cerca_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").ValorName,
                                   Corr_Cerca_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").ValorName,
                                   Corr_Lejos_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").ValorName,
                                   Corr_Lejos_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").ValorName,
                                   OD_CIE10 = DxOftalmologia.Count > 0 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[0].v_Cie10 : "sin dx",
                                   OD_Obs = "",
                                   OI_CIE10 = DxOftalmologia.Count > 0 ? DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID) == null ? "a" : DxOftalmologia.FindAll(p => p.v_ComponentId == Constants.OFTALMOLOGIA_ID)[0].v_Cie10 : "sin dx",
                                   OI_Obs = "",
                                   Discro = "",
                                   Discro_CIE10 = "",
                                   Discro_Obs = "",
                                   /////////////////// AUDIOMETRIA ////////////////////////
                                   Otoscopia_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OD).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OD).Valor,
                                   Otoscopia_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OI).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.AUDIOMETRIA_ID && o.IdCampo == Constants.AUDIOMETRIA_OTOSCOPIA_OI).Valor,
                                   
                                   Oido_Der_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125).v_Value1,
                                   Oido_Der_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250).v_Value1,
                                   Oido_Der_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500).v_Value1,
                                   Oido_Der_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000).v_Value1,
                                   Oido_Der_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000).v_Value1,
                                   Oido_Der_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000).v_Value1,
                                   Oido_Der_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000).v_Value1,
                                   Oido_Der_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000).v_Value1,
                                   Oido_Der_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000).v_Value1,

                                   Oido_Izq_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125).v_Value1,
                                   Oido_Izq_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250).v_Value1,
                                   Oido_Izq_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500).v_Value1,
                                   Oido_Izq_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000).v_Value1,
                                   Oido_Izq_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000).v_Value1,
                                   Oido_Izq_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000).v_Value1,
                                   Oido_Izq_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000).v_Value1,
                                   Oido_Izq_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000).v_Value1,
                                   Oido_Izq_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000).v_Value1,


                                   Audiometria_D_CIE10 = DxAudiometria.Count > 0 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[0].v_DiseasesName : "sin dx",
                                   Audiometria_D_Obs = "",
                                   Audiometria_I_CIE10 = DxAudiometria.Count > 0 ? DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID) == null ? "a" : DxAudiometria.FindAll(p => p.v_ComponentId == Constants.AUDIOMETRIA_ID)[0].v_DiseasesName : "sin dx",
                                   Audiometria_I_Obs = "",
                                   ///////////////// LABORATORIO //////////////////////////
                                   Grupo_Sanguineo = Grupo,
                                   Grupo_Sanguineo_ID = 0,
                                   Factor_RH = Factor,
                                   Factor_RH_ID = 0,
                                   Hemoglobina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor),
                                   Hemoglobina_CIE10 = DxHemoglobina,
                                   Hemoglobina_Obs = "",
                                   Colesterol = decimal.TryParse(Colesterol1, out temporal) ? decimal.Parse(Colesterol1) : 0, /////// Colesterol2 ?
                                   Colesterol_CIE10 = DxColesterol,
                                   Colesterol_Obs = "",
                                   Trigliceridos = decimal.TryParse(Trigli1, out temporal) ? decimal.Parse(Trigli1) : 0,
                                   Trigliceridos_CIE10 = DxTGC,
                                   Trigliceridos_Obs = "",
                                   Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor),
                                   Glucosa_CIE10 = DxGlucosa,
                                   Glucosa_Obs = "",
                                   /////////////////// ESPIRO /////////////////////////////
                                   FEV1_Teorico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor,
                                   FVC_Teorico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor,
                                   Espiro_CIE10 = Espirometria,
                                   Espiro_Obs = "",
                                   //////////////// MEDICINA ////////////////////////////
                                   Osteomuscular_CIE10 = ExaMusculoEsqueletico,
                                   Osteomuscular_Obs = "",
                                   Clinico_CIE10 = ExaMedGeneral,
                                   Clinico_Obs = "",
                                   /////////////////// ODONTO /////////////////////////
                                   Odonto_CIE10 = Odontograma,
                                   Odonto_Obs = "",
                                   /////////////////////// EKG ////////////////////////////
                                   EKG_CIE10 = Electrocardiograma,
                                   EKG_Obs = "",
                                   ///////////////////// RAYOS X /////////////////////////
                                   Rayos_X_CIE10 = DxRx.Count > 0 ? DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID) == null ? "a" : DxRx.FindAll(p => p.v_ComponentId == Constants.RX_TORAX_ID)[0].v_Cie10 : "sin dx",
                                   Rayos_X_Obs = "",
                                   ///////////////////// PSICOLOGIA //////////////////////
                                   Psico_CIE10 = Psicologia,
                                   Psico_Obs = "",
                                   ////////////////////////////////////////////////////////

                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string ConcatenateRestrictionByService(string pstrServiceId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var qry = (from a in dbContext.restriction  // RESTRICCIONES POR Diagnosticos
                       join eee in dbContext.masterrecommendationrestricction on a.v_MasterRestrictionId equals eee.v_MasterRecommendationRestricctionId
                       where a.v_ServiceId == pstrServiceId &&
                       a.i_IsDeleted == 0 && eee.i_TypifyingId == (int)Typifying.Restricciones
                       select new
                       {
                           v_RestrictionsName = eee.v_Name
                       }).ToList();

            return string.Join(", ", qry.Select(p => p.v_RestrictionsName));
        }
        
        public string ObtenerDxOcupacionales(List<DiagnosticosRecomendacionesList> ListaDxOcupacionales, int Posicion)
        { 

            if (ListaDxOcupacionales == null)
	        {
		         return "";
	        }
            else
            {
                var Resultados = ListaDxOcupacionales.FindAll(p => p.i_DiagnosticTypeId == (int)TipoDx.Enfermedad_Ocupacional || p.i_DiagnosticTypeId == (int)TipoDx.Accidente_Ocupacional).ToList();

              if (Resultados.Count > 0)
              {
                  int CantidadRegistros = Resultados.Count;
                  if (CantidadRegistros-1 >= Posicion)
                  {
                      return Resultados[Posicion].DiseasesName;
                  }
                  else
                  {
                      return "";
                  }
              }
              else
              {
                  return "";
              }
            }       
            
        }

        public string ObtenerDxMedicos(List<DiagnosticosRecomendacionesList> ListaDxOcupacionales, int Posicion)
        {

            if (ListaDxOcupacionales == null)
            {
                return "";
            }
            else
            {
                var Resultados = ListaDxOcupacionales.FindAll(p => p.i_DiagnosticTypeId == (int)TipoDx.Enfermedad_Comun || p.i_DiagnosticTypeId == (int)TipoDx.Accidente_Común || p.i_DiagnosticTypeId == (int)TipoDx.Otros || p.i_DiagnosticTypeId == (int)TipoDx.Normal || p.i_DiagnosticTypeId == (int)TipoDx.SinDx).ToList();

                if (Resultados.Count > 0)
                {
                    int CantidadRegistros = Resultados.Count;
                    if (CantidadRegistros-1 >= Posicion)
                    {
                        return Resultados[Posicion].DiseasesName;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }

        }

        public organizationDto GetInfoMedicalCenter()
        {
            using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
            {

                var sql = (from o in dbContext.organization
                           where o.v_OrganizationId == Constants.OWNER_ORGNIZATION_ID
                           select new organizationDto
                           {
                               v_Name = o.v_Name,
                               v_Address = o.v_Address,
                               b_Image = o.b_Image,
                               v_PhoneNumber = o.v_PhoneNumber,
                               v_Mail = o.v_Mail,

                           }).SingleOrDefault();


                return sql;
            }
        }

        public List<DiagnosticosRecomendaciones> DevolverJerarquiaDxMejoradoSinDescartados(List<string> ServicioIds)
        {
            try
            {
                int isDeleted = (int)SiNo.NO;
                int definitivo = (int)FinalQualification.Definitivo;
                int presuntivo = (int)FinalQualification.Presuntivo;
                int descartado = (int)FinalQualification.Descartado;

                List<DiagnosticosRecomendaciones> ListaTotalJerarquizada = new List<DiagnosticosRecomendaciones>();
                DiagnosticosRecomendaciones ListaJerarquizadaDxRecomendaciones = new DiagnosticosRecomendaciones();
                List<DiagnosticosRecomendacionesList> ListaDxRecomendacionesPorServicio;

                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    var ListaDxRecoTodos = (from ccc in dbContext.diagnosticrepository
                                            join bbb in dbContext.component on ccc.v_ComponentId equals bbb.v_ComponentId into J7_join
                                            from bbb in J7_join.DefaultIfEmpty()
                                            join ddd in dbContext.diseases on ccc.v_DiseasesId equals ddd.v_DiseasesId  // Diagnosticos 
                                            join eee in dbContext.service on ccc.v_ServiceId equals eee.v_ServiceId
                                            where (ccc.i_IsDeleted == isDeleted) &&
                                                (ccc.i_FinalQualificationId == definitivo ||
                                                ccc.i_FinalQualificationId == presuntivo )
                                                && ServicioIds.Contains(eee.v_ServiceId)
                                            //&& eee.d_ServiceDate < FeFin && eee.d_ServiceDate > FeIni
                                            orderby eee.v_ServiceId
                                            select new DiagnosticosRecomendacionesList
                                            {
                                                ServicioId = eee.v_ServiceId,
                                                Descripcion = ddd.v_Name,
                                                IdCampo = ccc.v_ComponentFieldId,
                                                Tipo = "D",
                                                IdComponente = bbb.v_ComponentId,
                                                IdDeseases = ddd.v_DiseasesId,
                                                i_FinalQualiticationId = ccc.i_FinalQualificationId,
                                                DiseasesName = ddd.v_Name,
                                                i_DiagnosticTypeId  = ccc.i_DiagnosticTypeId
                                            }).Union(from ccc in dbContext.recommendation
                                                     join ddd in dbContext.masterrecommendationrestricction on ccc.v_MasterRecommendationId equals ddd.v_MasterRecommendationRestricctionId  // Diagnosticos      
                                                     join eee in dbContext.service on ccc.v_ServiceId equals eee.v_ServiceId
                                                     where ccc.i_IsDeleted == isDeleted
                                                       && ServicioIds.Contains(eee.v_ServiceId)
                                                     orderby eee.v_ServiceId
                                                     select new DiagnosticosRecomendacionesList
                                                     {
                                                         ServicioId = eee.v_ServiceId,
                                                         Descripcion = ddd.v_Name,
                                                         IdCampo = "sin nada",
                                                         Tipo = "R",
                                                         IdComponente = "sin nada",
                                                         IdDeseases = "sin nada",
                                                         i_FinalQualiticationId = 0,
                                                         DiseasesName = "sin nada",
                                                         i_DiagnosticTypeId =0
                                                     }).ToList();



                    var ListaJerarquizada = (from A in dbContext.service
                                             where ServicioIds.Contains(A.v_ServiceId)
                                             //A.d_ServiceDate < FeFin && A.d_ServiceDate > FeIni
                                             select new DiagnosticosRecomendaciones
                                             {
                                                 ServicioId = A.v_ServiceId
                                             }).ToList();

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.DetalleDxRecomendaciones = ListaDxRecoTodos.FindAll(p => p.ServicioId == a.ServicioId);
                    });


                    
                    foreach (var item in ListaJerarquizada)
                    {
                        ListaJerarquizadaDxRecomendaciones = new DiagnosticosRecomendaciones();
                        ListaDxRecomendacionesPorServicio = new List<DiagnosticosRecomendacionesList>();

                        ListaJerarquizadaDxRecomendaciones.ServicioId = item.ServicioId;


                        var DetalleTodos = ListaJerarquizada.SelectMany(p => p.DetalleDxRecomendaciones).ToList();

                        //Lista Dx
                        var DetalleDx = DetalleTodos.FindAll(p => p.ServicioId == item.ServicioId && p.Tipo == "D");

                        for (int i = 0; i < 18; i++)
                        {
                            if (i < DetalleDx.Count())
                            {
                                if (i == 17)
                                {
                                    int Contador = DetalleDx.Count - 17;
                                    var x = DetalleDx.GetRange(17, Contador);

                                    DetalleDx[i].Descripcion = string.Join(", ", x.Select(p => p.Descripcion));
                                    ListaDxRecomendacionesPorServicio.Add(DetalleDx[i]);
                                }
                                else
                                {
                                    DetalleDx[i].Descripcion = DetalleDx[i].Descripcion;
                                    ListaDxRecomendacionesPorServicio.Add(DetalleDx[i]);
                                }
                            }
                            else
                            {
                                ListaDxRecomendacionesPorServicio.Add(new DiagnosticosRecomendacionesList());
                            }
                        }

                        //Lista Recomendaciones
                        var DetalleReco = DetalleTodos.FindAll(p => p.ServicioId == item.ServicioId && p.Tipo == "R");

                        for (int i = 0; i < 14; i++)
                        {
                            if (i < DetalleReco.Count())
                            {
                                if (i == 13)
                                {
                                    int Contador = DetalleReco.Count - 13;
                                    var x = DetalleReco.GetRange(13, Contador);

                                    DetalleReco[i].Descripcion = string.Join(", ", x.Select(p => p.Descripcion));
                                    ListaDxRecomendacionesPorServicio.Add(DetalleReco[i]);
                                }
                                else
                                {
                                    DetalleReco[i].Descripcion = DetalleReco[i].Descripcion;
                                    ListaDxRecomendacionesPorServicio.Add(DetalleReco[i]);
                                }
                            }
                            else
                            {
                                ListaDxRecomendacionesPorServicio.Add(new DiagnosticosRecomendacionesList());
                            }
                        }
                        ListaJerarquizadaDxRecomendaciones.DetalleDxRecomendaciones = ListaDxRecomendacionesPorServicio;

                        ListaTotalJerarquizada.Add(ListaJerarquizadaDxRecomendaciones);
                    }
                }
                return ListaTotalJerarquizada;
           

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<DiagnosticosRecomendaciones> DevolverJerarquiaDxMejoradoConDescartados(List<string> ServicioIds)
        {
            try
            {
                int isDeleted = (int)SiNo.NO;
                int definitivo = (int)FinalQualification.Definitivo;
                int presuntivo = (int)FinalQualification.Presuntivo;
                int descartado = (int)FinalQualification.Descartado;

                List<DiagnosticosRecomendaciones> ListaTotalJerarquizada = new List<DiagnosticosRecomendaciones>();
                DiagnosticosRecomendaciones ListaJerarquizadaDxRecomendaciones = new DiagnosticosRecomendaciones();
                List<DiagnosticosRecomendacionesList> ListaDxRecomendacionesPorServicio;

                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    var ListaDxRecoTodos = (from ccc in dbContext.diagnosticrepository
                                            join bbb in dbContext.component on ccc.v_ComponentId equals bbb.v_ComponentId into J7_join
                                            from bbb in J7_join.DefaultIfEmpty()
                                            join ddd in dbContext.diseases on ccc.v_DiseasesId equals ddd.v_DiseasesId  // Diagnosticos 
                                            join eee in dbContext.service on ccc.v_ServiceId equals eee.v_ServiceId
                                            where (ccc.i_IsDeleted == isDeleted) &&
                                                (ccc.i_FinalQualificationId == definitivo ||
                                                ccc.i_FinalQualificationId == presuntivo ||
                                                ccc.i_FinalQualificationId == descartado
                                                )
                                                && ServicioIds.Contains(eee.v_ServiceId)
                                            //&& eee.d_ServiceDate < FeFin && eee.d_ServiceDate > FeIni
                                            orderby eee.v_ServiceId
                                            select new DiagnosticosRecomendacionesList
                                            {
                                                ServicioId = eee.v_ServiceId,
                                                Descripcion = ddd.v_Name,
                                                IdCampo = ccc.v_ComponentFieldId,
                                                Tipo = "D",
                                                IdComponente = bbb.v_ComponentId,
                                                IdDeseases = ddd.v_DiseasesId,
                                                i_FinalQualiticationId = ccc.i_FinalQualificationId,
                                                DiseasesName = ddd.v_Name,
                                                i_DiagnosticTypeId = ccc.i_DiagnosticTypeId,
                                                CIE10 = ddd.v_CIE10Id 
                                            }).Union(from ccc in dbContext.recommendation
                                                     join ddd in dbContext.masterrecommendationrestricction on ccc.v_MasterRecommendationId equals ddd.v_MasterRecommendationRestricctionId  // Diagnosticos      
                                                     join eee in dbContext.service on ccc.v_ServiceId equals eee.v_ServiceId
                                                     where ccc.i_IsDeleted == isDeleted
                                                       && ServicioIds.Contains(eee.v_ServiceId)
                                                     orderby eee.v_ServiceId
                                                     select new DiagnosticosRecomendacionesList
                                                     {
                                                         ServicioId = eee.v_ServiceId,
                                                         Descripcion = ddd.v_Name,
                                                         IdCampo = "sin nada",
                                                         Tipo = "R",
                                                         IdComponente = "sin nada",
                                                         IdDeseases = "sin nada",
                                                         i_FinalQualiticationId = 0,
                                                         DiseasesName = "sin nada",
                                                         i_DiagnosticTypeId = 0,
                                                         CIE10 = ""
                                                     }).ToList();



                    var ListaJerarquizada = (from A in dbContext.service
                                             where ServicioIds.Contains(A.v_ServiceId)
                                             //A.d_ServiceDate < FeFin && A.d_ServiceDate > FeIni
                                             select new DiagnosticosRecomendaciones
                                             {
                                                 ServicioId = A.v_ServiceId
                                             }).ToList();

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.DetalleDxRecomendaciones = ListaDxRecoTodos.FindAll(p => p.ServicioId == a.ServicioId);
                    });



                    foreach (var item in ListaJerarquizada)
                    {
                        ListaJerarquizadaDxRecomendaciones = new DiagnosticosRecomendaciones();
                        ListaDxRecomendacionesPorServicio = new List<DiagnosticosRecomendacionesList>();

                        ListaJerarquizadaDxRecomendaciones.ServicioId = item.ServicioId;


                        var DetalleTodos = ListaJerarquizada.SelectMany(p => p.DetalleDxRecomendaciones).ToList();

                        //Lista Dx
                        var DetalleDx = DetalleTodos.FindAll(p => p.ServicioId == item.ServicioId && p.Tipo == "D");

                        for (int i = 0; i < 18; i++)
                        {
                            if (i < DetalleDx.Count())
                            {
                                if (i == 17)
                                {
                                    int Contador = DetalleDx.Count - 17;
                                    var x = DetalleDx.GetRange(17, Contador);

                                    DetalleDx[i].Descripcion = string.Join(", ", x.Select(p => p.Descripcion));
                                    ListaDxRecomendacionesPorServicio.Add(DetalleDx[i]);
                                }
                                else
                                {
                                    DetalleDx[i].Descripcion = DetalleDx[i].Descripcion;
                                    ListaDxRecomendacionesPorServicio.Add(DetalleDx[i]);
                                }
                            }
                            else
                            {
                                ListaDxRecomendacionesPorServicio.Add(new DiagnosticosRecomendacionesList());
                            }
                        }

                        //Lista Recomendaciones
                        var DetalleReco = DetalleTodos.FindAll(p => p.ServicioId == item.ServicioId && p.Tipo == "R");

                        for (int i = 0; i < 14; i++)
                        {
                            if (i < DetalleReco.Count())
                            {
                                if (i == 13)
                                {
                                    int Contador = DetalleReco.Count - 13;
                                    var x = DetalleReco.GetRange(13, Contador);

                                    DetalleReco[i].Descripcion = string.Join(", ", x.Select(p => p.Descripcion));
                                    ListaDxRecomendacionesPorServicio.Add(DetalleReco[i]);
                                }
                                else
                                {
                                    DetalleReco[i].Descripcion = DetalleReco[i].Descripcion;
                                    ListaDxRecomendacionesPorServicio.Add(DetalleReco[i]);
                                }
                            }
                            else
                            {
                                ListaDxRecomendacionesPorServicio.Add(new DiagnosticosRecomendacionesList());
                            }
                        }
                        ListaJerarquizadaDxRecomendaciones.DetalleDxRecomendaciones = ListaDxRecomendacionesPorServicio;

                        ListaTotalJerarquizada.Add(ListaJerarquizadaDxRecomendaciones);
                    }
                }
                return ListaTotalJerarquizada;


            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public List<JerarquiaServicioCamposValores> DevolverValorCampoPorServicioMejorado(List<string> ListaServicioIds)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            int isDeleted = (int)SiNo.NO;

            try
            {
                int rpta = 0;
                var PreQuery = (from A in dbContext.service
                                join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                                join C in dbContext.servicecomponentfields on B.v_ServiceComponentId equals C.v_ServiceComponentId
                                join D in dbContext.servicecomponentfieldvalues on C.v_ServiceComponentFieldsId equals D.v_ServiceComponentFieldsId
                                join F in dbContext.componentfields on C.v_ComponentFieldId equals F.v_ComponentFieldId into F_join
                                from F in F_join.DefaultIfEmpty()
                                join G in dbContext.componentfield on C.v_ComponentFieldId equals G.v_ComponentFieldId into G_join
                                from G in G_join.DefaultIfEmpty()
                                join H in dbContext.component on F.v_ComponentId equals H.v_ComponentId into H_join
                                from H in H_join.DefaultIfEmpty()
                                where B.i_IsDeleted == isDeleted
                                     && C.i_IsDeleted == isDeleted
                                     && ListaServicioIds.Contains(A.v_ServiceId)
                                //&& A.d_ServiceDate < FechaFin && A.d_ServiceDate > FechaIni

                                orderby A.v_ServiceId
                                select new ValorComponenteList
                                {
                                    ServicioId = A.v_ServiceId,
                                    Valor = D.v_Value1,
                                    NombreComponente = H.v_Name,
                                    IdComponente = H.v_ComponentId,
                                    NombreCampo = G.v_TextLabel,
                                    IdCampo = C.v_ComponentFieldId,
                                    i_GroupId = G.i_GroupId.Value
                                }

                            );

                var PreQuery2 = (from A in PreQuery
                                select new ValorComponenteList
                                {
                                    ServicioId = A.ServicioId,
                                    Valor = A.Valor,
                                    NombreComponente = A.NombreComponente,
                                    IdComponente = A.IdComponente,
                                    NombreCampo = A.NombreCampo,
                                    IdCampo = A.IdCampo,
                                    i_GroupId = A.i_GroupId == null ? -1 : A.i_GroupId
                                }).ToList();

                var finalQuery = (from a in PreQuery2

                                  let value1 = int.TryParse(a.Valor, out rpta)
                                  join sp in dbContext.systemparameter on new { a = a.i_GroupId, b = rpta }
                                                  equals new { a = sp.i_GroupId, b = sp.i_ParameterId } into sp_join
                                  from sp in sp_join.DefaultIfEmpty()

                                  select new ValorComponenteList
                                  {
                                      ServicioId = a.ServicioId,
                                      Valor = a.Valor,
                                      NombreComponente = a.NombreComponente,
                                      IdComponente = a.IdComponente,
                                      NombreCampo = a.NombreCampo,
                                      IdCampo = a.IdCampo,
                                      ValorName = sp == null ? "" : sp.v_Value1
                                  }).ToList();

                var ListaJerarquizada = (from A in dbContext.service
                                         where ListaServicioIds.Contains(A.v_ServiceId)

                                         //A.d_ServiceDate < FechaFin && A.d_ServiceDate > FechaIni
                                         select new JerarquiaServicioCamposValores
                                         {
                                             ServicioId = A.v_ServiceId
                                         }).ToList();

                ListaJerarquizada.ForEach(a =>
                {
                    a.CampoValores = finalQuery.FindAll(p => p.ServicioId == a.ServicioId);
                });


                return ListaJerarquizada;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<JerarquiaServicioCamposValores> DevolverValorCampoPorServicioMejorado_Mejorado(List<string> ListaServicioIds)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            int isDeleted = (int)SiNo.NO;

            try
            {
                int rpta = 0;
                var PreQuery = (from A in dbContext.service
                                join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                                join C in dbContext.servicecomponentfields on B.v_ServiceComponentId equals C.v_ServiceComponentId
                                join D in dbContext.servicecomponentfieldvalues on C.v_ServiceComponentFieldsId equals D.v_ServiceComponentFieldsId
                                join F in dbContext.componentfields on C.v_ComponentFieldId equals F.v_ComponentFieldId
                                join G in dbContext.componentfield on C.v_ComponentFieldId equals G.v_ComponentFieldId
                                join H in dbContext.component on F.v_ComponentId equals H.v_ComponentId
                                where B.i_IsDeleted == isDeleted
                                     && C.i_IsDeleted == isDeleted
                                     && ListaServicioIds.Contains(A.v_ServiceId)
                                //&& A.d_ServiceDate < FechaFin && A.d_ServiceDate > FechaIni

                                orderby A.v_ServiceId
                                select new ValorComponenteList
                                {
                                    ServicioId = A.v_ServiceId,
                                    Valor = D.v_Value1,
                                    NombreComponente = H.v_Name,
                                    IdComponente = H.v_ComponentId,
                                    NombreCampo = G.v_TextLabel,
                                    IdCampo = C.v_ComponentFieldId,
                                    i_GroupId = G.i_GroupId.Value
                                }

                            );

                var finalQuery = (from a in PreQuery.ToList()

                                  let value1 = int.TryParse(a.Valor, out rpta)
                                  join sp in dbContext.systemparameter on new { a = a.i_GroupId, b = rpta }
                                                  equals new { a = sp.i_GroupId, b = sp.i_ParameterId } into sp_join
                                  from sp in sp_join.DefaultIfEmpty()

                                  select new ValorComponenteList
                                  {
                                      ServicioId = a.ServicioId,
                                      Valor = a.Valor,
                                      NombreComponente = a.NombreComponente,
                                      IdComponente = a.IdComponente,
                                      NombreCampo = a.NombreCampo,
                                      IdCampo = a.IdCampo,
                                      ValorName = sp == null ? "" : sp.v_Value1
                                  }).ToList();



                var ListaJerarquizada = (from A in dbContext.service
                                         where ListaServicioIds.Contains(A.v_ServiceId)

                                         //A.d_ServiceDate < FechaFin && A.d_ServiceDate > FechaIni
                                         select new JerarquiaServicioCamposValores
                                         {
                                             ServicioId = A.v_ServiceId
                                         }).ToList();

                ListaJerarquizada.ForEach(a =>
                {
                    a.CampoValores = finalQuery.FindAll(p => p.ServicioId == a.ServicioId);
                });


                return ListaJerarquizada;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Antecedentes> DevolverHabitos_Personales(List<string> PersonIds)
        {
            try
            {

                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {

                    var ListaMedicosPersonales = (from A in dbContext.personmedicalhistory
                                                  join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 }
                                                     equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                                                  from B in B_join.DefaultIfEmpty()

                                                  join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                                                    equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                                  from C in C_join.DefaultIfEmpty()

                                                  join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                                                  where A.i_IsDeleted == 0
                                                  && PersonIds.Contains(A.v_PersonId)
                                                  orderby A.v_PersonId
                                                  select new PersonMedicalHistoryList
                                                  {
                                                      v_PersonId = A.v_PersonId,
                                                      v_DiseasesId = D.v_DiseasesId,
                                                      v_DiseasesName = D.v_Name,
                                                      i_Answer = A.i_AnswerId.Value,
                                                      v_GroupName = C.v_Value1 == null ? "ENFERMEDADES OTROS" : C.v_Value1,
                                                  }).ToList();

                    var ListaAntecedentesFamiliares = (from A in dbContext.familymedicalantecedents

                                                       join B in dbContext.systemparameter on new { a = A.i_TypeFamilyId.Value, b = 149 }
                                                           equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                                                       from B in B_join.DefaultIfEmpty()

                                                       join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                           equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                                       from C in C_join.DefaultIfEmpty()

                                                       join D in dbContext.diseases on new { a = A.v_DiseasesId }
                                                equals new { a = D.v_DiseasesId } into D_join
                                                       from D in D_join.DefaultIfEmpty()
                                                       where A.i_IsDeleted == 0
                                                       && PersonIds.Contains(A.v_PersonId)
                                                       orderby A.v_PersonId
                                                       select new FamilyMedicalAntecedentsList
                                                       {
                                                           v_PersonId = A.v_PersonId,
                                                           v_DiseaseName = D.v_Name,
                                                           v_TypeFamilyName = C.v_Value1,
                                                           v_Comment = A.v_Comment
                                                       }).ToList();

                    var ListaHabitosNoxivos = (from A in dbContext.noxioushabits
                                               where A.i_IsDeleted == 0
                                               && PersonIds.Contains(A.v_PersonId)
                                               orderby A.v_PersonId
                                               select new NoxiousHabitsList
                                               {
                                                   v_PersonId = A.v_PersonId,
                                                   i_TypeHabitsId = A.i_TypeHabitsId.Value,
                                                   v_Frequency = A.v_Frequency,
                                                   v_Comment = A.v_Comment
                                               }).ToList();


                    var ListaJerarquizada = (from A in dbContext.person
                                             where PersonIds.Contains(A.v_PersonId)

                                             select new Antecedentes
                                             {
                                                 PersonId = A.v_PersonId
                                             }).ToList();

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.ListaPersonalMedical = ListaMedicosPersonales.FindAll(p => p.v_PersonId == a.PersonId);
                    });

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.ListaHabitos = ListaHabitosNoxivos.FindAll(p => p.v_PersonId == a.PersonId);
                    });

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.ListaAntecedentesFamiliares = ListaAntecedentesFamiliares.FindAll(p => p.v_PersonId == a.PersonId);
                    });


                    return ListaJerarquizada;


                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Antecedentes> DevolverHabitos_PersonalesSolo(string PersonIds)
        {
            try
            {

                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {

                    var ListaMedicosPersonales = (from A in dbContext.personmedicalhistory
                                                  join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 }
                                                     equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                                                  from B in B_join.DefaultIfEmpty()

                                                  join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                                                    equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                                  from C in C_join.DefaultIfEmpty()

                                                  join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                                                  where A.i_IsDeleted == 0
                                                  && PersonIds.Contains(A.v_PersonId)
                                                  orderby A.v_PersonId
                                                  select new PersonMedicalHistoryList
                                                  {
                                                      v_PersonId = A.v_PersonId,
                                                      v_DiseasesId = D.v_DiseasesId,
                                                      v_DiseasesName = D.v_Name,
                                                      i_Answer = A.i_AnswerId.Value,
                                                      v_GroupName = C.v_Value1 == null ? "ENFERMEDADES OTROS" : C.v_Value1,
                                                  }).ToList();

                    var ListaAntecedentesFamiliares = (from A in dbContext.familymedicalantecedents

                                                       join B in dbContext.systemparameter on new { a = A.i_TypeFamilyId.Value, b = 149 }
                                                           equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                                                       from B in B_join.DefaultIfEmpty()

                                                       join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                           equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                                       from C in C_join.DefaultIfEmpty()

                                                       join D in dbContext.diseases on new { a = A.v_DiseasesId }
                                                equals new { a = D.v_DiseasesId } into D_join
                                                       from D in D_join.DefaultIfEmpty()
                                                       where A.i_IsDeleted == 0
                                                       && PersonIds.Contains(A.v_PersonId)
                                                       orderby A.v_PersonId
                                                       select new FamilyMedicalAntecedentsList
                                                       {
                                                           v_PersonId = A.v_PersonId,
                                                           v_DiseaseName = D.v_Name,
                                                           v_TypeFamilyName = C.v_Value1,
                                                           v_Comment = A.v_Comment
                                                       }).ToList();

                    var ListaHabitosNoxivos = (from A in dbContext.noxioushabits
                                               where A.i_IsDeleted == 0
                                               && PersonIds.Contains(A.v_PersonId)
                                               orderby A.v_PersonId
                                               select new NoxiousHabitsList
                                               {
                                                   v_PersonId = A.v_PersonId,
                                                   i_TypeHabitsId = A.i_TypeHabitsId.Value,
                                                   v_Frequency = A.v_Frequency,
                                                   v_Comment = A.v_Comment
                                               }).ToList();


                    var ListaJerarquizada = (from A in dbContext.person
                                             where PersonIds.Contains(A.v_PersonId)

                                             select new Antecedentes
                                             {
                                                 PersonId = A.v_PersonId
                                             }).ToList();

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.ListaPersonalMedical = ListaMedicosPersonales.FindAll(p => p.v_PersonId == a.PersonId);
                    });

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.ListaHabitos = ListaHabitosNoxivos.FindAll(p => p.v_PersonId == a.PersonId);
                    });

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.ListaAntecedentesFamiliares = ListaAntecedentesFamiliares.FindAll(p => p.v_PersonId == a.PersonId);
                    });


                    return ListaJerarquizada;


                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ServiceComponentFieldValuesList> ValoresComponenteOdontogramaValue1(string pstrServiceId, string pstrComponentId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            try
            {
                List<ServiceComponentFieldValuesList> serviceComponentFieldValues = (from A in dbContext.service
                                                                                     join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                                                                                     join C in dbContext.servicecomponentfields on B.v_ServiceComponentId equals C.v_ServiceComponentId
                                                                                     join D in dbContext.servicecomponentfieldvalues on C.v_ServiceComponentFieldsId equals D.v_ServiceComponentFieldsId

                                                                                     where (A.v_ServiceId == pstrServiceId)
                                                                                           && (B.v_ComponentId == pstrComponentId)
                                                                                           && (B.i_IsDeleted == 0)
                                                                                           && (C.i_IsDeleted == 0)

                                                                                     select new ServiceComponentFieldValuesList
                                                                                     {
                                                                                         //v_ComponentId = B.v_ComponentId,
                                                                                         v_ComponentFieldId = C.v_ComponentFieldId,
                                                                                         //v_ComponentFieldId = G.v_ComponentFieldId,
                                                                                         //v_ComponentFielName = G.v_TextLabel,
                                                                                         v_ServiceComponentFieldsId = C.v_ServiceComponentFieldsId,
                                                                                         v_Value1 = D.v_Value1
                                                                                     }).ToList();


                return serviceComponentFieldValues;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string AntecedentesPatologicosConcatenados(List<PersonMedicalHistoryList> Lista)
        {
            try
            {
                return string.Join(", ", Lista.Select(p => p.v_GroupName + " / " + p.v_DiseasesName));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string AntecedentesFamiliaresConcatenados(List<FamilyMedicalAntecedentsList> Lista)
        {
            try
            {
                return string.Join(", ", Lista.Select(p => p.v_TypeFamilyName + " / " + p.v_DiseaseName));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetCantidadAusentes(string pstrServiceId, string pstrComponentId, string pstrFieldId)
        {
            try
            {
                string retornar = "0";
                string[] componentId = null;
                ServiceBL oServiceBL = new ServiceBL();
                List<ServiceComponentFieldValuesList> oServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();

                oServiceComponentFieldValuesList = oServiceBL.ValoresComponenteOdontograma1(pstrServiceId, pstrComponentId);
                var xx = oServiceComponentFieldValuesList.Count() == 0 || ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)) == null ? string.Empty : ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;

                componentId = xx.Split(';');

                if (componentId[0] == "")
                {
                    retornar = "0";
                }
                else
                {
                    retornar = componentId.Count().ToString();
                }
                return retornar;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string GetCantidadCaries(string pstrServiceId, string pstrComponentId, string pstrFieldId)
        {
            try
            {
                string Retornar = "0";
                string[] componentId = null;
                ServiceBL oServiceBL = new ServiceBL();
                List<ServiceComponentFieldValuesList> oServiceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();

                oServiceComponentFieldValuesList = oServiceBL.ValoresComponenteOdontograma1(pstrServiceId, pstrComponentId);
                var xx = oServiceComponentFieldValuesList.Count() == 0 || ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)) == null ? string.Empty : ((ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;

                componentId = xx.Split(';');
                if (componentId[0] == "")
                {
                    Retornar = "0";
                }
                else
                {
                    Retornar = componentId.Count().ToString();
                }
                return Retornar;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string GetGrupoEtario(int Edad)
        {
            string GrupoEterario = "";
            try
            {
                if (Edad < 18)
                {
                    GrupoEterario = "Menor de 18";
                }
                else if (18 <= Edad && Edad <= 29)
                {
                    GrupoEterario = "de 18 a 29";
                }
                else if (30 <= Edad && Edad <= 39)
                {
                    GrupoEterario = "de 30 a 39";
                }
                else if (40 <= Edad && Edad <= 49)
                {
                    GrupoEterario = "de 40 a 49";
                }
                else if (Edad >= 50)
                {
                    GrupoEterario = "mayor de 50";
                }

                return GrupoEterario;
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion     

        #region Matriz Multiple

        public List<MatrizShauindo> ReporteMatrizShauindo(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service
                                        join A1 in dbContext.calendar on A.v_ServiceId equals A1.v_ServiceId

                                        join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                        from B in B_join.DefaultIfEmpty()

                                        join C in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                            equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                        from C in C_join.DefaultIfEmpty()

                                        join D in dbContext.person on A.v_PersonId equals D.v_PersonId
                                        join E in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 100 }
                                            equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                        from E in E_join.DefaultIfEmpty()

                                        join F in dbContext.systemparameter on new { a = D.i_MaritalStatusId.Value, b = 101 }
                                            equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                        from F in F_join.DefaultIfEmpty()

                                        join G in dbContext.datahierarchy on new { a = D.i_LevelOfId.Value, b = 108 }
                                               equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                        from G in G_join.DefaultIfEmpty()

                                        join H in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 154 }
                                              equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join
                                        from H in H_join.DefaultIfEmpty()

                                        join I in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 155 }
                                            equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                        from I in I_join.DefaultIfEmpty()

                                        join J in dbContext.organization on new { a = B.v_CustomerOrganizationId }
									            equals new { a = J.v_OrganizationId } into J_join
							            from J in J_join.DefaultIfEmpty()

                                        join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                        from K in K_join.DefaultIfEmpty()
                                        join L in dbContext.person on A.v_PersonId equals L.v_PersonId

                                        join L1 in dbContext.datahierarchy on new { a = L.i_DepartmentId.Value, b = 113 }
                                        equals new { a = L1.i_ItemId, b = L1.i_GroupId } into L1_join
                                        from L1 in L1_join.DefaultIfEmpty()
                                        join F1 in dbContext.datahierarchy on new { a = L.i_ProvinceId.Value, b = 113 }
                                            equals new { a = F1.i_ItemId, b = F1.i_GroupId } into F1_join
                                        from F1 in F1_join.DefaultIfEmpty()

                                        join G1 in dbContext.datahierarchy on new { a = L.i_DistrictId.Value, b = 113 }
                                                          equals new { a = G1.i_ItemId, b = G1.i_GroupId } into G1_join
                                        from G1 in G1_join.DefaultIfEmpty()

                                        join C2 in dbContext.organization on B.v_CustomerOrganizationId equals C2.v_OrganizationId into C2_join
                                        from C2 in C2_join.DefaultIfEmpty()

                                        join C1 in dbContext.organization on B.v_EmployerOrganizationId equals C1.v_OrganizationId into C1_join
                                        from C1 in C1_join.DefaultIfEmpty()

                                        join CC in dbContext.organization on B.v_WorkingOrganizationId equals CC.v_OrganizationId into CC_join
                                        from CC in CC_join.DefaultIfEmpty()


                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin && A1.i_CalendarStatusId != 4

                                        select new MatrizShauindo
                                        {
                                            ServiceId = A.v_ServiceId,
                                            PersonId = D.v_PersonId,
                                            ProtocolId = B.v_ProtocolId,

                                            v_CustomerOrganizationId = C2.v_OrganizationId,
                                            v_EmployerOrganizationId = C1.v_OrganizationId,
                                            v_WorkingOrganizationId = CC.v_OrganizationId,

                                            v_CustomerLocationId = B.v_CustomerLocationId,
                                            v_WorkingLocationId = B.v_WorkingLocationId,
                                            v_EmployerLocationId = B.v_EmployerLocationId,
                                
                                            TipoEmo = C.v_Value1,
                                            DniPasaporte = D.v_DocNumber,
                                            FechaExamen = A.d_ServiceDate.Value,
                                            ApellidosNombres = D.v_FirstLastName + " " + D.v_SecondLastName + ", " + D.v_FirstName,
                                            FechaNacimiento = D.d_Birthdate.Value,
                                            TelefonoContacto = D.v_TelephoneNumber,
                                            _Sexo = D.i_SexTypeId,
                                            EstadoCivil = F.v_Value1,
                                            GradoInstruccion = G.v_Value1,
                                            _Grupo = D.i_BloodGroupId,
                                            _Factor = D.i_BloodFactorId,
                                            //Procedencia = D.v_ResidenciaAnterior,
                                            Ocupacion = D.v_CurrentOccupation,
                                            
                                            General = C2.v_Name,
                                            Contrata = C1.v_Name,
                                            Subcontrata = CC.v_Name,
                                            Unida = C1.v_Name + " / " + CC.v_Name,
                                            _AptitudFinal = A.i_AptitudeStatusId,
                                            _VigenciaEmo = A.d_GlobalExpirationDate.Value,
                                            
                                            Area = K.v_Name,
                                            Procedencia = L1.v_Value1 + " - " + F1.v_Value1 + " - " + G1.v_Value1,
                                            NumeroHijos = D.i_NumberLivingChildren == null ? 0 : D.i_NumberLivingChildren.Value,
                                            
                                        };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }
                    
                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.PersonId);
                        ServicioIds.Add(item.ServiceId);
                    }

                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    //var Antecedentes_Personales = DevolverAntecedentesPersonales(PersonIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var dxs = new ServiceBL().ListGetDiagnosticByServiceIdAndCategoryId(ServicioIds).ToList();
                    var Reco = new ServiceBL().ListGetRecommendationByServiceId(ServicioIds).ToList();
                    var Restri = new ServiceBL().ListGetRestrictionByServiceId(ServicioIds).ToList();
                    var Dxa = new ServiceBL().ListGetDiagnosticByServiceId1(ServicioIds).ToList();
                    //var diagnosticRepository = new ServiceBL().GetServiceComponentConclusionesDxServiceId(ServicioIds).ToList(); 
                   
                    //var filterDiagnosticRepository = diagnosticRepository.FindAll(p => p.i_FinalQualificationId != (int)Sigesoft.Common.FinalQualification.Descartado);

                    var sql = (from a in objEntity.ToList()


                               select new MatrizShauindo
                               {
                                   ServiceId = a.ServiceId,
                                   PersonId = a.PersonId,
                                   v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                                   v_EmployerOrganizationId = a.v_EmployerOrganizationId,
                                   v_WorkingOrganizationId = a.v_WorkingOrganizationId,

                                   v_CustomerLocationId = a.v_CustomerLocationId,
                                   v_EmployerLocationId = a.v_EmployerLocationId,
                                   v_WorkingLocationId = a.v_WorkingLocationId,

                                   AptitudFinal = a._AptitudFinal==null?"-":a._AptitudFinal == 1 ? "SIN APTITUD" : a._AptitudFinal == 2 ? "APTO" : a._AptitudFinal == 3 ? "NO APTO" : a._AptitudFinal == 4 ? "OBSERVADO" : a._AptitudFinal == 5 ? "APTO CON RESTRICCIONES" : a._AptitudFinal == 6 ? "ASISTENCIAL" : a._AptitudFinal == 7 ? "EVALUADO" : a.AptitudFinal,
                                   VigenciaEmo = a._VigenciaEmo == null ? "-" : a._VigenciaEmo.ToString().Split(' ')[0],
                                   TipoEmo = a.TipoEmo == null ? "" : a.TipoEmo,
                                   DniPasaporte = a.DniPasaporte,
                                   FechaExamen = a.FechaExamen,
                                   ApellidosNombres = a.ApellidosNombres,
                                   FechaNacimiento = a.FechaNacimiento,
                                   edad =GetAge(a.FechaNacimiento.Value),
                                   TelefonoContacto = a.TelefonoContacto == null?"-":a.TelefonoContacto,
                                   Sexo = a._Sexo == 1 ?"MASCULINO" :a._Sexo == 2 ?"FEMENINO":"",
                                   GrupoFactorSanguineo = (a._Grupo == null ? "-" : a._Grupo == 1 ? "O" : a._Grupo == 2 ? "A" : a._Grupo == 3 ? "B" : a._Grupo == 4 ? "AB" : "NO") + "-" + (a._Factor == null ? "" : a._Factor == 1 ? "POSITIVO" : a._Factor == 2 ? "NEGATIVO" : "LLEVA"),
                                   EstadoCivil = a.EstadoCivil==null?"-":a.EstadoCivil,
                                   GradoInstruccion = a.GradoInstruccion == null ? "" : a.GradoInstruccion,
                                   //GrupoFactorSanguineo = a.GrupoFactorSanguineo == null ? "NO LLEVA" : a.GrupoFactorSanguineo,
                                   Procedencia = a.Procedencia == null ? "-" : a.Procedencia == "" ? "-" : a.Procedencia,
                                   Ocupacion = a.Ocupacion==null?"-":a.Ocupacion==""?"-":a.Ocupacion,
                                   //MINA
                                   Mina = a.General,
                                   Empresa = a.General == a.Subcontrata ? a.Contrata : a.General != a.Subcontrata ? a.Unida : a.Empresa,
                                   //Area = a.Area,
                                   NumeroHijos = a.NumeroHijos == null ? 0 : a.NumeroHijos,
                                   PiezasMalEstado = GetCantidadCaries(a.ServiceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID),
                                   PiezasFaltan = GetCantidadAusentes(a.ServiceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID),

                                   Marca = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000004322")  == null ? "-" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000004322").Valor,
                                   Modelo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000004323") == null ? "-" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000004323").Valor,

                                   Ruido = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000667") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000667").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000667").Valor == "0" ? "-" : "",
                                   Cancerigenos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000668") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000668").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000668").Valor == "0" ? "-" : "",
                                   Temperaturas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000669") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000669").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000669").Valor == "0" ? "-" : "",
                                   Cargas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000670") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000670").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000670").Valor == "0" ? "-" : "",
                                   Polvo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000671") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000671").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000671").Valor == "0" ? "-" : "",
                                   Mutagenicos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000672") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000672").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000672").Valor == "0" ? "-" : "",
                                   Biologicos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000673") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000673").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000673").Valor == "0" ? "-" : "",
                                   MovRepet = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000674") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000674").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000674").Valor == "0" ? "-" : "",
                                   VigSegmentaria = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000675") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000675").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000675").Valor == "0" ? "-" : "",
                                   Solventes = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000676") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000676").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000676").Valor == "0" ? "-" : "",
                                   Posturas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000677") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000677").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000677").Valor == "0" ? "-" : "",
                                   PantallaPVD = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000678") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000678").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000678").Valor == "0" ? "-" : "",
                                   ViBTotal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000679") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000679").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000679").Valor == "0" ? "-" : "",
                                   MetalPesado = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000683") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000683").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000683").Valor == "0" ? "-" : "",
                                   Turnos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000684") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000684").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000684").Valor == "0" ? "-" : "",
                                   Otros = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000685") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000685").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000685").Valor == "0" ? "-" : "",
                                   Describir = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000686") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000686").Valor,
                                   InmunizacionTetano = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003190") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003190").Valor,
                                   InmunizacionInfluenza = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003191") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003191").Valor,
                                   InmunizacionHepatitisB = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003192") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003192").Valor,
                                   InmunizacionFiebreAmarilla = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003193") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003193").Valor,
                                   Talla = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007").Valor,
                                   Peso = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008").Valor,
                                   IMC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009").Valor,
                                   PerimetroAbdominal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000010").Valor,
                                   PerimetroCadera = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000011").Valor,
                                   IndiceCintura = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000012").Valor,
                                   ////PorcentajeGrasaCorporal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000013") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000013").Valor,
                                   PresionSistolica = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000001").Valor,
                                   PresionDiastolica = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000002").Valor,
                                   FrecuenciaCardiaca = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000003").Valor,
                                   Temperatura = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000004") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000004").Valor,
                                   FrecuenciaRespiratoria = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000005").Valor,
                                   SaturacionOxigeno = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000006").Valor,
                                   CabezaDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000687") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000687").Valor,
                                   CuelloDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000688") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000688").Valor,
                                   NarizDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000689") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000689").Valor,
                                   BocaAdmigdalaFaringeLaringeDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000690") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000690").Valor,
                                   MiembrosSuperioresDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000692") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000692").Valor,
                                   MiembrosInferioresDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000693") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000693").Valor,
                                   ReflejosOsteoTendinososDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000694") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000694").Valor,
                                   MarchaDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000695") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000695").Valor,
                                   ColumnaDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000696") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000696").Valor,
                                   AbdomenDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000697") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000697").Valor,
                                   DescripcionGeneral = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000665") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000665").Valor,
                                   AnillosInguinalesDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000698") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000698").Valor,
                                   HerniasDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000699") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000699").Valor,
                                   VaricesDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000700") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000700").Valor,
                                   OrganosGenitalesDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000701") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000701").Valor,
                                   GangliosDescripcion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000702") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000702").Valor,
                                   LenAtenMemoOrientIntelAfect = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003264") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003264").Valor,
                                   Gingivitis = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000185") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000185").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000185").Valor == "0" ? "NO" : "",
                                   AntecedentesLaborales = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003541") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003541").Valor,
                                   AntecedentesPatologicos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003542") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003542").Valor,


                                   Cocaina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000705" || o.IdCampo == "N009-MF000003740") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000705" || o.IdCampo == "N009-MF000003740").ValorName,
                                   Marihuana = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001294" || o.IdCampo == "N009-MF000003739") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001294" || o.IdCampo == "N009-MF000003739").ValorName,
                                   Magnesio = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003230") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003230").ValorName,

                                   //AptitudTrabajarNo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-editas") != null ? varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547").ValorName : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548").ValorName,//aqui mete un campo
                                   VisionCercaScod = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547" || o.IdCampo == "N009-MF000003569" || o.IdCampo == "N009-MF000003614") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547" || o.IdCampo == "N009-MF000003569" || o.IdCampo == "N009-MF000003614").ValorName,
                                   VisionCercaScoi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548" || o.IdCampo == "N009-MF000003570" || o.IdCampo == "N009-MF000003615") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548" || o.IdCampo == "N009-MF000003570" || o.IdCampo == "N009-MF000003615").ValorName,
                                   Area = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003549" || o.IdCampo == "N009-MF000003571" || o.IdCampo == "N009-MF000003616") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003549" || o.IdCampo == "N009-MF000003571" || o.IdCampo == "N009-MF000003616").ValorName,
                                   VisonCercaCcoi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF0000035450" || o.IdCampo == "N009-MF000003572" || o.IdCampo == "N009-MF000003617") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003550" || o.IdCampo == "N009-MF000003572" || o.IdCampo == "N009-MF000003617").ValorName,
                                   VisionLejosScod = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003551" || o.IdCampo == "N009-MF000003565" || o.IdCampo == "N009-MF000003618") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003551" || o.IdCampo == "N009-MF000003565" || o.IdCampo == "N009-MF000003618").ValorName,
                                   VisionLejosScoi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003552" || o.IdCampo == "N009-MF000003566" || o.IdCampo == "N009-MF000003619") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003552" || o.IdCampo == "N009-MF000003566" || o.IdCampo == "N009-MF000003619").ValorName,
                                   VisionLejosCcod = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553" || o.IdCampo == "N009-MF000003567" || o.IdCampo == "N009-MF000003620") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553" || o.IdCampo == "N009-MF000003567" || o.IdCampo == "N009-MF000003620").ValorName,
                                   VisonLejosCcoi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554" || o.IdCampo == "N009-MF000003568" || o.IdCampo == "N009-MF000003621") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554" || o.IdCampo == "N009-MF000003568" || o.IdCampo == "N009-MF000003621").ValorName,

                                   VisonColores = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003555") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003555").ValorName,
                                   EnfermedadesOculares = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003556") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003556").Valor,
                                   ReflejosPupilares = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003557") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003557").ValorName,
                                   AptitudTrabajarSi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003558") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003558").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003558").Valor == "0" ? "NO" : "",

                                   //Marca = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000082") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000082").Valor,
                                   //Modelo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000083") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000083").Valor,
                                   Calibracion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000084") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000084").Valor,
                                   ProtectoresAuditivos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000100") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000100").ValorName,
                                   ApreciacionRuido = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000098") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000098").ValorName,
                                   TiempoExpPond = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001303") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001303").Valor,
                                   CambiosAltitud = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001299") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001299").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001299").Valor == "0" ? "NO" : "",
                                   ViajesFrecuentesAltura = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001300") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001300").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001300").Valor == "0" ? "NO" : "",
                                   HorasDescansoExamen = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001302") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001302").Valor,
                                   TiempoTrabajo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001378") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001378").Valor,
                                   SorderaDismAud = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000092") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000092").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000092").Valor == "0" ? "NO" : "",
                                   Zumbido = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000093") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000093").Valor == "1" ? "SI" : "NO",
                                   VertigosMareos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000094") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000094").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000094").Valor == "0" ? "NO" : "",
                                   OtalgiaDolorOido = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000096") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000096").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000096").Valor == "0" ? "NO" : "",
                                   SecrecionOticaInfeccion = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000099") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000099").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000099").Valor == "0" ? "NO" : "",
                                   OtrosSint = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001301") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001301").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001301").Valor == "0" ? "NO" : "",
                                   OtrosOidos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001310") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001310").Valor,
                                   InfeccionesAuditivas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000091") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000091").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000091").Valor == "0" ? "NO" : "",
                                   InfeccionesOrofaringeas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000089") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000089").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000089").Valor == "0" ? "NO" : "",
                                   Resfrios = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001307") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001307").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001307").Valor == "0" ? "NO" : "",
                                   AccidentesTraumaticoAuditivo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001306") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001306").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001306").Valor == "0" ? "NO" : "",
                                   USoMedicamentosOtotoxicos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000090") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000090").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000090").Valor == "0" ? "NO" : "",
                                   EnfermedadTiroidea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001304") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001304").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001304").Valor == "0" ? "NO" : "",
                                   //Tec = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001305") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001305").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001305").Valor == "0" ? "NO" : "",
                                   ConsumoTabaco = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000095") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000095").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000095").Valor == "0" ? "NO" : "",
                                   ServicioMilitar = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001309") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001309").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001309").Valor == "0" ? "NO" : "",
                                   HobbiesCexposARuido = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001308") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001308").Valor == "1" ? "SI" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001308").Valor == "0" ? "NO" : "",
                                   OidoDerecho = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000178") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000178").Valor,
                                   OidoIzquierdo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000179") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000179").Valor,
                                   TxtVaOd125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000043") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000043").Valor,
                                   TxtVaOd250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000044") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000044").Valor,
                                   TxtVaOd500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001").Valor,
                                   TxtVaOd1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002").Valor,
                                   TxtVaOd2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003").Valor,
                                   TxtVaOd3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004").Valor,
                                   TxtVaOd4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005").Valor,
                                   TxtVaOd6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006").Valor,
                                   TxtVaOd8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007").Valor,
                                   TxtVoOd125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000045") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000045").Valor,
                                   TxtVoOd250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000046") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000046").Valor,
                                   TxtVoOd500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008").Valor,
                                   TxtVoOd1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009").Valor,
                                   TxtVoOd2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010").Valor,
                                   TxtVoOd3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011").Valor,
                                   TxtVoOd4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012").Valor,
                                   TxtVoOd6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013").Valor,
                                   TxtVoOd8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000014") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000014").Valor,
                                   TxtVaOi125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000047") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000047").Valor,
                                   TxtVaOi250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000048") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000048").Valor,
                                   TxtVaOi500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015").Valor,
                                   TxtVaOi1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016").Valor,
                                   TxtVaOi2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017").Valor,
                                   TxtVaOi3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018").Valor,
                                   TxtVaOi4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019").Valor,
                                   TxtVaOi6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020").Valor,
                                   TxtVaOi8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021").Valor,
                                   TxtVoOi125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000049") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000049").Valor,
                                   TxtVoOi250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000050") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000050").Valor,
                                   TxtVoOi500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022").Valor,
                                   TxtVoOi1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023").Valor,
                                   TxtVoOi2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024").Valor,
                                   TxtVoOi3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025").Valor,
                                   TxtVoOi4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026").Valor,
                                   TxtVoOi6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027").Valor,
                                   TxtVoOi8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000028") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000028").Valor,
                                   TxtEmOd125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000051") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000051").Valor,
                                   TxtEmOd250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000052") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000052").Valor,
                                   TxtEmOd500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000029") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000029").Valor,
                                   TxtEmOd1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000030") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000030").Valor,
                                   TxtEmOd2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000059") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000059").Valor,
                                   TxtEmOd3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000060") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000060").Valor,
                                   TxtEmOd4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000055") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000055").Valor,
                                   TxtEmOd6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000056") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000056").Valor,
                                   TxtEmOd8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000057") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000057").Valor,
                                   TxtEmOi125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000053") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000053").Valor,
                                   TxtEmOi250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000054") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000054").Valor,
                                   TxtEmOi500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000058") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000058").Valor,
                                   TxtEmOi1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000037") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000037").Valor,
                                   TxtEmOi2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000038") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000038").Valor,
                                   TxtEmOi3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000039") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000039").Valor,
                                   TxtEmOi4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000040") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000040").Valor,
                                   TxtEmOi6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000041") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000041").Valor,
                                   TxtEmOi8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000042") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000042").Valor,
                                   TxtAnOd125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000061") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000061").Valor,
                                   TxtAnOd250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000062") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000062").Valor,
                                   TxtAnOd500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000063") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000063").Valor,
                                   TxtAnOd1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000064") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000064").Valor,
                                   TxtAnOd2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000065") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000065").Valor,
                                   TxtAnOd3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000066") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000066").Valor,
                                   TxtAnOd4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000067") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000067").Valor,
                                   TxtAnOd6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000068") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000068").Valor,
                                   TxtAnOd8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000069") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000069").Valor,
                                   TxtAnOi125 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000070") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000070").Valor,
                                   TxtAnOi250 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000071") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000071").Valor,
                                   TxtAnOi500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000052") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000052").Valor,
                                   TxtAnOi1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000073") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000073").Valor,
                                   TxtAnOi2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000074") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000074").Valor,
                                   TxtAnOi3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000075") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000075").Valor,
                                   TxtAnOi4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000076") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000076").Valor,
                                   TxtAnOi6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000077") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000077").Valor,
                                   TxtAnOi8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000078") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000078").Valor,
                                   Cvf = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000286") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000286").Valor,
                                   ExposLaboralQuimicos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000287") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000287").Valor,
                                   Vef1Cvf = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000169") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000169").Valor,
                                   Cero = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000221") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000221").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000221").Valor == "0" ? " - " : "",
                                   CeroCero = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000222") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000222").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000222").Valor == "0" ? " - " : "",
                                   CeroUno = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000223") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000223").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000223").Valor == "0" ? " - " : "",
                                   UnoCero = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000220") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000220").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000220").Valor == "0" ? " - " : "",
                                   UnoUno = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000720") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000720").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000720").Valor == "0" ? " - " : "",
                                   UnoDos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000721") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000721").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000721").Valor == "0" ? " - " : "",
                                   DosUno = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000722") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000722").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000722").Valor == "0" ? " - " : "",
                                   DosDos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000723") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000723").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000723").Valor == "0" ? " - " : "",
                                   DosTres = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000724") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000724").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000724").Valor == "0" ? " - " : "",
                                   TresUno = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000725") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000725").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000725").Valor == "0" ? " - " : "",
                                   TresDos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000726") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000726").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000726").Valor == "0" ? " - " : "",
                                   TresMas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000727") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000727").Valor == "1" ? "X" : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000727").Valor == "0" ? " - " : "",
                                   FrecuenciaCardiaca_ = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003119") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003119").Valor,
                                   RitmoCardiaco = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003120" || o.IdCampo == "N009-MF000003129") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003120" || o.IdCampo == "N009-MF000003129").Valor,
                                   IntervaloPR = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003121" || o.IdCampo == "N009-MF000003130") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003121" || o.IdCampo == "N009-MF000003130").Valor,
                                   ComplejoQRS = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003122") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003122").Valor,
                                   IntervaloQTC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003123" || o.IdCampo == "N009-MF000003131") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003123" || o.IdCampo == "N009-MF000003131").Valor,
                                   EjeCardiaco = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003124" || o.IdCampo == "N009-MF000003132") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003124" || o.IdCampo == "N009-MF000003132").Valor,
                                   HallazgoInformeElectricoCardiaco = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 5).Select(s => s.v_DiseasesName)),
                                   Hemoglobina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001874" || o.IdCampo == "N009-MF000000265") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001874" || o.IdCampo == "N009-MF000000265").Valor,
                                   Hematocrito = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001876" || o.IdCampo == "N009-MF000000266") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001876" || o.IdCampo == "N009-MF000000266").Valor,
                                   Hematies = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001878") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001878").Valor,
                                   LeucocitosTotales = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001890") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001890").Valor,
                                   Plaquetas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001886") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001886").Valor,
                                   Basofilos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001900") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001900").Valor,
                                   Eosinofilos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001894") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001894").Valor,
                                   Monocitos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001902") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001902").Valor,
                                   Linfocitos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001892") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001892").Valor,
                                   NeutrofilosSegementados = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001896") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001896").Valor,
                                   Mielocitos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001888") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001888").Valor,
                                   Juveniles = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003205") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003205").Valor,
                                   Abastonados = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003207") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003207").Valor,
                                   Segmentados = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001898") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001898").Valor,
                                   CCVolumenCorpuscularMedio = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001880") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001880").Valor,
                                   CCHBCorpuscular = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001882") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001882").Valor,
                                   Glucosa = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000261") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000261").Valor,
                                   ProteinasTotales = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001792") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001792").Valor,
                                   Albumina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001796") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001796").Valor,
                                   TGO = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001798" || o.IdCampo == "N009-MF000004318") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001798" || o.IdCampo == "N009-MF000004318").Valor,
                                   TGP = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001800" || o.IdCampo == "N009-MF000004320") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001800" || o.IdCampo == "N009-MF000004320").Valor,
                                   FosfatasaAlcalina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001802") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001802").Valor,
                                   GammaGlutamilTranspeptidasa = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001804") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001804").Valor,
                                   BilirrubinaTotal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001806") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001806").Valor,
                                   BilirrubinaDirecta = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001808") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001808").Valor,
                                   //BilirrubinaIndirecta = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001810") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001810").Valor,
                                   ColesterolTotal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001904" || o.IdCampo == "N009-MF000001086") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001904" || o.IdCampo == "N009-MF000001086").Valor,
                                   Tec = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001073") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001073").Valor,
                                   Trigliceridos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001906" || o.IdCampo == "N009-MF000001296") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001906" || o.IdCampo == "N009-MF000001296").Valor,
                                   ColesterolHDL = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000254") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000254").Valor,
                                   ColesterolLDL = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001073") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001073").Valor,
                                   CreatininaSuero = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000518") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000518").Valor,
                                   Color = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000444") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000444").ValorName,
                                   Aspecto = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001041") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001041").ValorName,
                                   Densidad = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001043") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001043").Valor,
                                   ReaccionPH = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001045") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001045").Valor,
                                   SangreOrina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001315") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001315").ValorName,
                                   Nitritos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001055") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001055").Valor,
                                   Proteinas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001053") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001053").ValorName,

                                   HabitosNocivosDrogas = Habitos_Personales.Find(p => p.PersonId == a.PersonId) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Frequency,
                                   HabitosNocivosAlcohol = Habitos_Personales.Find(p => p.PersonId == a.PersonId) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Frequency,
                                   HabitosNocivosTabaco = Habitos_Personales.Find(p => p.PersonId == a.PersonId) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "c" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Frequency,
                                   Alergias = Habitos_Personales.Find(p => p.PersonId == a.PersonId) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //
                                   GlucosaExamenCompletoOrina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001313") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001313").ValorName,
                                   PorcentajeGrasaCorporal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003486") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003486").ValorName,


                                   RecomendacionesConcatenadas = string.Join(", ", Reco.FindAll(p => p.ServiceId == a.ServiceId).Select(s => s.Name)),
                                   RestriccionConcatenadas = string.Join(", ", Restri.FindAll(p => p.ServiceId == a.ServiceId).Select(s => s.Name)),
                                   BilirrubinaIndirecta = string.Join(", ", Dxa.FindAll(p => p.ServiceId == a.ServiceId).Select(s => s.v_DiseasesName)),


                                   AntecedentesFamiliares = Habitos_Personales.Find(p => p.PersonId == a.PersonId) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaAntecedentesFamiliares == null ? " " : AntecedentesFamiliaresConcatenados(Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaAntecedentesFamiliares),
                                   AntecedentesPersonales = Habitos_Personales.Find(p => p.PersonId == a.PersonId) == null ? "NIEGA" : Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaPersonalMedical == null ? "NIEGA" : AntecedentesPatologicosConcatenados(Habitos_Personales.Find(p => p.PersonId == a.PersonId).ListaPersonalMedical),
                                   ConclusionLabo = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 1).Select(s => s.v_DiseasesName)),
                                   ConclusionRayosX = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 6).Select(s => s.v_DiseasesName)),//dxs.FindAll(p => p.ServiceId == a.ServiceId).Select(s => s.v_DiseasesName).ToString(), 
                                   ConclusionEspirometrica = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 16).Select(s => s.v_DiseasesName)),
                                   ConclusionesEKG = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 5).Select(s => s.v_DiseasesName)),
                                   //ConclusionesEKG = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 5).Select(s => s.v_DiseasesName)),
                                   Dx1 = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 2).Select(s => s.v_DiseasesName)),//Odonto
                                   Dx2 = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 7).Select(s => s.v_DiseasesName)),//Psico
                                   Dx3 = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 10).Select(s => s.v_DiseasesName)),//Triaje
                                   Dx4 = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 11).Select(s => s.v_DiseasesName)),//Medicina
                                   Dx5 = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 14).Select(s => s.v_DiseasesName)),//Oftalmo
                                   ConclusionesRx = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 15).Select(s => s.v_DiseasesName)),//Audio
                                   Dx6 = string.Join("/ ", dxs.FindAll(p => p.ServiceId == a.ServiceId && p.CategoriaId == 22).Select(s => s.v_DiseasesName)),//Psicosen

                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private object DevolverAntecedentesPersonales(List<string> PersonIds)
        {
            throw new NotImplementedException();
        }

        public List<MatrizLaZanja> ReporteMatrizLaZanja(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service

                                    join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                    from B in B_join.DefaultIfEmpty()

                                    join C in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.person on A.v_PersonId equals D.v_PersonId
                                    join E in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = D.i_MaritalStatusId.Value, b = 101 }
                                        equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = D.i_LevelOfId.Value, b = 108 }
                                           equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 154 }
                                          equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 155 }
                                        equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.organization on new { a = B.v_CustomerOrganizationId }
                                            equals new { a = J.v_OrganizationId } into J_join
                                    from J in J_join.DefaultIfEmpty()

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin
                                    select new MatrizLaZanja
                                    {
                                        ServiceId = A.v_ServiceId,
                                        PersonId = D.v_PersonId,
                                        ProtocolId = B.v_ProtocolId,
                                        v_CustomerOrganizationId = B.v_CustomerOrganizationId,
                                        v_CustomerLocationId = B.v_CustomerLocationId,

                                        ApellidosNombres = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                        Procedencia = D.v_Procedencia,
                                        Sexo = E.v_Value1,
                                        Empresa = J.v_Name,
                                        Dni = D.v_DocNumber,
                                        PuestoTrabajo = D.v_CurrentOccupation,
                                        TipoExamen = C.v_Value1,
                                        FechaDigitacion = A.d_ServiceDate.Value,
                                        FechaExamenOcupacional = A.d_ServiceDate.Value,
                                        FechaNacimiento = D.d_Birthdate.Value,
                                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.PersonId);
                        ServicioIds.Add(item.ServiceId);
                    }

                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var sql = (from a in objEntity.ToList()

                               select new MatrizLaZanja
                               {
                                   ServiceId = a.ServiceId,
                                   PersonId = a.PersonId,
                                   ProtocolId = a.ProtocolId,
                                   v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                                   v_CustomerLocationId = a.v_CustomerLocationId,


                                   ApellidosNombres = a.ApellidosNombres,
                                   Procedencia = a.Procedencia,
                                   Sexo = a.Sexo,
                                   Empresa = a.Empresa,
                                   Dni = a.Dni,
                                   PuestoTrabajo = a.PuestoTrabajo,
                                   TipoExamen = a.TipoExamen,
                                   FechaDigitacion = a.FechaDigitacion,
                                   FechaExamenOcupacional = a.FechaExamenOcupacional,
                                   FechaNacimiento = a.FechaNacimiento,
                                   Edad = GetAge(a.FechaNacimiento.Value),
                                   Talla = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007").Valor,
                                   Peso = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008").Valor,
                                   Imc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009").Valor,
                                   Pas = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000001").Valor,
                                   Pad = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000002").Valor,
                                   Fc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000003").Valor,
                                   Fr = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000005").Valor,
                                   SatO2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000006").Valor,
                                   VlCcOd = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547").ValorName,
                                   VlCcOi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003549") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003549").ValorName,
                                   VlScOd = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003551") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003551").ValorName,
                                   VlScOi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003552") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003552").ValorName,
                                   VisionCromatica = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003555") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003555").ValorName,
                                   
                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<MatrizGoldFields> ReporteMatrizGoldFields(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service

                                    join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                    from B in B_join.DefaultIfEmpty()

                                    join C in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.person on A.v_PersonId equals D.v_PersonId

                                    join E in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = D.i_MaritalStatusId.Value, b = 101 }
                                        equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = D.i_LevelOfId.Value, b = 108 }
                                           equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 154 }
                                          equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 155 }
                                        equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.organization on new { a = B.v_CustomerOrganizationId }
                                            equals new { a = J.v_OrganizationId } into J_join
                                    from J in J_join.DefaultIfEmpty()

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    join L in dbContext.datahierarchy on new { a = D.i_DocTypeId.Value, b = 106 }
                                    equals new { a = L.i_ItemId, b = L.i_GroupId }  // TIPO DOCUMENTO

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin
                                    select new MatrizGoldFields
                                    {
                                        ServiceId = A.v_ServiceId,
                                        PersonId = D.v_PersonId,
                                        ProtocolId = B.v_ProtocolId,
                                        v_CustomerOrganizationId = B.v_CustomerOrganizationId,
                                        v_CustomerLocationId = B.v_CustomerLocationId,
                                        Condicion = "",
                                        FechaDigitacion = A.d_ServiceDate.Value,
                                        Empresa = J.v_Name,
                                        Apellidos = D.v_FirstLastName + " " + D.v_SecondLastName,
                                        Nombres = D.v_FirstName ,
                                        FechaNacimiento = D.d_Birthdate.Value,
                                        Sexo = E.v_Value1,
                                        TipoDocumentoIdentidad =L.v_Value1,
                                        NumeroDocumentoIdentidad = D.v_DocNumber,
                                        PuestoTrabajo = D.v_CurrentOccupation,
                                        FonoContacto = "",
                                        CentroMedico = "San Lorenzo",
                                        TipoExamen = C.v_Value1,
                                        FechaExamenMedico = A.d_ServiceDate.Value,

                                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.PersonId);
                        ServicioIds.Add(item.ServiceId);
                    }

                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var sql = (from a in objEntity.ToList()

                               select new MatrizGoldFields
                               {
                                   ServiceId = a.ServiceId,
                                   PersonId = a.PersonId,
                                   ProtocolId = a.ProtocolId,
                                   v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                                   v_CustomerLocationId = a.v_CustomerLocationId,
                                   Condicion = "",
                                   FechaDigitacion = a.FechaDigitacion,
                                   Empresa = a.Empresa,
                                   Apellidos = a.Apellidos,
                                   Nombres = a.Nombres,
                                   FechaNacimiento = a.FechaNacimiento,
                                   Sexo = a.Sexo,
                                   TipoDocumentoIdentidad = a.TipoDocumentoIdentidad,
                                   NumeroDocumentoIdentidad = a.NumeroDocumentoIdentidad,
                                   PuestoTrabajo = a.PuestoTrabajo,
                                   FonoContacto = a.FonoContacto,
                                   CentroMedico = a.CentroMedico,
                                   TipoExamen = a.TipoExamen,
                                   FechaExamenMedico = a.FechaExamenMedico,
                                   Talla = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007").Valor,
                                   Peso = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008").Valor,
                                   Imc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009").Valor,
                                   SCOjoDer = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547").ValorName,
                                   SCOjoIzq = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548").ValorName,
                                   CCOjoDer = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553").ValorName,
                                   CCOjoIzq = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554").ValorName,
                                   _500Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001").Valor,
                                   _1000Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002").Valor,
                                   _2000Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003").Valor,
                                   _3000Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004").Valor,
                                   _4000Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005").Valor,
                                   _6000Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006").Valor,
                                   _8000Hz_Od = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007").Valor,
                                   _500Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008").Valor,
                                   _1000Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009").Valor,
                                   _2000Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010").Valor,
                                   _3000Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011").Valor,
                                   _4000Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012").Valor,
                                   _6000Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013").Valor,
                                   _8000Hz_Oi = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000014") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000014").Valor,
                                   _500Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015").Valor,
                                   _1000Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016").Valor,
                                   _2000Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017").Valor,
                                   _3000Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018").Valor,
                                   _4000Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019").Valor,
                                   _6000Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020").Valor,
                                   _8000Hz_1 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021").Valor,
                                   _500Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022").Valor,
                                   _1000Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023").Valor,
                                   _2000Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024").Valor,
                                   _3000Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025").Valor,
                                   _4000Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026").Valor,
                                   _6000Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027").Valor,
                                   _8000Hz_2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000028") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000028").Valor,
                                   Fcv = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000286") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000286").Valor,
                                   Fev1Fvc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000287") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000287").Valor,
                                   
                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<MatrizSolucionesManteIntegrales> ReporteMatrizSolucManteIntegra(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service

                                    join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                    from B in B_join.DefaultIfEmpty()

                                    join C in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.person on A.v_PersonId equals D.v_PersonId
                                    join E in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = D.i_MaritalStatusId.Value, b = 101 }
                                        equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = D.i_LevelOfId.Value, b = 108 }
                                           equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 154 }
                                          equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 155 }
                                        equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.organization on new { a = B.v_CustomerOrganizationId }
                                            equals new { a = J.v_OrganizationId } into J_join
                                    from J in J_join.DefaultIfEmpty()

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    join L in dbContext.systemparameter on new { a = A.i_AptitudeStatusId.Value, b = 124 }
                                    equals new { a = L.i_ParameterId, b = L.i_GroupId }  // ESTADO APTITUD ESO     

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin
                                    select new MatrizSolucionesManteIntegrales
                                    {
                                        ServiceId = A.v_ServiceId,
                                        PersonId = D.v_PersonId,
                                        ProtocolId = B.v_ProtocolId,
                                        v_CustomerOrganizationId = B.v_CustomerOrganizationId,
                                        v_CustomerLocationId = B.v_CustomerLocationId,
                                        TipoEmo = C.v_Value1,
                                        Dni = D.v_DocNumber,
                                        NumCelular = D.v_TelephoneNumber,
                                        Apellidos = D.v_FirstLastName + " " + D.v_SecondLastName,
                                        Nombres = D.v_FirstName,
                                        FechaNacimiento = D.d_Birthdate.Value,
                                        Genero = E.v_Value1,
                                        ClinicaProveedora = "San Lorenzo",
                                        PuestoTrabajo = D.v_CurrentOccupation,
                                        Area = K.v_Name,
                                        Proyecto ="",
                                        FechaEvaluacion = A.d_ServiceDate.Value,
                                        Aptitud = L.v_Value1,
                                        GrupoFactorSanguineo = H.v_Value1 + " " + I.v_Value1,
                                        Procedencia = D.v_Procedencia                                       

                                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.PersonId);
                        ServicioIds.Add(item.ServiceId);
                    }

                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var sql = (from a in objEntity.ToList()

                        select new MatrizSolucionesManteIntegrales
                        {
                            ServiceId = a.ServiceId,
                            PersonId = a.PersonId,
                            ProtocolId = a.ProtocolId,
                            v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                            v_CustomerLocationId = a.v_CustomerLocationId,
                            TipoEmo = a.TipoEmo,
                            Dni = a.Dni,
                            NumCelular = a.NumCelular,
                            Apellidos = a.Apellidos,
                            Nombres = a.Nombres,
                            FechaNacimiento = a.FechaNacimiento,
                            Edad = GetAge(a.FechaNacimiento.Value),
                            Genero = a.Genero,
                            ClinicaProveedora = a.ClinicaProveedora,
                            PuestoTrabajo = a.PuestoTrabajo,
                            Area = a.Area,
                            Proyecto = a.Proyecto,
                            FechaEvaluacion = a.FechaEvaluacion,
                            Aptitud = a.Aptitud,
                            GrupoFactorSanguineo = a.GrupoFactorSanguineo,
                            Procedencia = a.Procedencia,

                            Talla = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007").Valor,
                            Peso = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008").Valor,
                            Imc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009").Valor,
                            PerimetroAbdominal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000010").Valor,
                            Cadera = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000011").Valor,
                            Icc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000012").Valor,
                            OftalmoVCODSC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547").ValorName,
                            OftalmoVCOISC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548").ValorName,
                            OftalmoVLOICC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003549") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003549").ValorName,
                            OftalmoVLODSC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003551") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003551").ValorName,
                            OftalmoVLOISC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003552") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003552").ValorName,
                            OftalmoVCODCC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553").ValorName,
                            OftalmoVCOICC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554").ValorName,
                            VisionColores = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003555") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003555").ValorName,
                            OiVa500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001").Valor,
                            OiVa1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002").Valor,
                            OiVa2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003").Valor,
                            OiVa3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004").Valor,
                            OiVa4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005").Valor,
                            OiVa6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006").Valor,
                            OiVa8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007").Valor,
                            OdVa500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008").Valor,
                            OdVa1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009").Valor,
                            OdVa2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010").Valor,
                            OdVa3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011").Valor,
                            OdVa4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012").Valor,
                            OdVa6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013").Valor,
                            OdVa8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000014") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000014").Valor,
                            OiVo500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015").Valor,
                            OiVo1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016").Valor,
                            OiVo2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017").Valor,
                            OiVo3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018").Valor,
                            OiVo4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019").Valor,
                            OiVo6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020").Valor,
                            OiVo8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021").Valor,
                            OdVo500 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022").Valor,
                            OdVo1000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023").Valor,
                            OdVo2000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024").Valor,
                            OdVo3000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025").Valor,
                            OdVo4000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026").Valor,
                            OdVo6000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027").Valor,
                            OdVo8000 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000028") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000028").Valor,
                            EspirometriaCvf = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000286") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000286").Valor,
                            EspirometriaFec1Fvc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000287") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000287").Valor,
                            Hemoglobina = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001874") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001874").Valor,
                            Hematocrito = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001876") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001876").Valor,
                            Glucosa = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000261") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000261").Valor,
                            ColesterolTotal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001904") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001904").Valor,
                            Trigliceridos = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001906") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001906").Valor,
                            Hdl = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000254") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000000254").Valor,
                            Ldl = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001073") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001073").Valor,
                            
                        }).ToList();
                    return sql;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<MatrizMiBanco> ReporteMatrizMiBanco(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    List<string> ServicioIds = new List<string>();
                    List<string> PersonIds = new List<string>();

                    var objEntity = from A in dbContext.service

                                    join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                    from B in B_join.DefaultIfEmpty()

                                    join C in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                                    from C in C_join.DefaultIfEmpty()

                                    join D in dbContext.person on A.v_PersonId equals D.v_PersonId
                                    join E in dbContext.systemparameter on new { a = B.i_EsoTypeId.Value, b = 118 }
                                        equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                                    from E in E_join.DefaultIfEmpty()

                                    join F in dbContext.systemparameter on new { a = D.i_MaritalStatusId.Value, b = 101 }
                                        equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                                    from F in F_join.DefaultIfEmpty()

                                    join G in dbContext.datahierarchy on new { a = D.i_LevelOfId.Value, b = 108 }
                                           equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                    from G in G_join.DefaultIfEmpty()

                                    join H in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 154 }
                                          equals new { a = H.i_ParameterId, b = H.i_GroupId } into H_join
                                    from H in H_join.DefaultIfEmpty()

                                    join I in dbContext.systemparameter on new { a = D.i_LevelOfId.Value, b = 155 }
                                        equals new { a = I.i_ParameterId, b = I.i_GroupId } into I_join
                                    from I in I_join.DefaultIfEmpty()

                                    join J in dbContext.organization on new { a = B.v_CustomerOrganizationId }
                                            equals new { a = J.v_OrganizationId } into J_join
                                    from J in J_join.DefaultIfEmpty()

                                    join K in dbContext.area on A.v_AreaId equals K.v_AreaId into K_join
                                    from K in K_join.DefaultIfEmpty()

                                    join L in dbContext.servicecomponent on A.v_ServiceId equals L.v_ServiceId

                                    join me in dbContext.systemuser on L.i_ApprovedUpdateUserId equals me.i_SystemUserId into me_join
                                    from me in me_join.DefaultIfEmpty()

                                    join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                    from pme in pme_join.DefaultIfEmpty()

                                    join p in dbContext.person on me.v_PersonId equals p.v_PersonId

                                    where A.d_ServiceDate >= FechaInicio && A.d_ServiceDate <= FechaFin &&
                                          (L.v_ComponentId == Constants.EXAMEN_FISICO_ID || L.v_ComponentId == Constants.EXAMEN_FISICO_7C_ID)
                                    select new MatrizMiBanco
                                    {
                                        ServiceId = A.v_ServiceId,
                                        PersonId = D.v_PersonId,
                                        ProtocolId = B.v_ProtocolId,
                                        v_CustomerOrganizationId = B.v_CustomerOrganizationId,
                                        v_CustomerLocationId = B.v_CustomerLocationId,
                                        v_EmployerOrganizationId = B.v_EmployerOrganizationId,
                                        Dni = D.v_DocNumber,
                                        Protocolo = B.v_Name,
                                        TipoEmo = C.v_Value1,
                                        Empresa = J.v_Name,
                                        ApellidoPaterno = D.v_FirstLastName,
                                        ApellidoMaterno = D.v_SecondLastName,
                                        Nombre = D.v_FirstName,
                                        Genero = E.v_Value1,
                                        Localidad = D.v_Procedencia,
                                        FechaNacimiento = D.d_Birthdate.Value,
                                        FechaIngreso = A.d_ServiceDate.Value,
                                        Area = K.v_Name,
                                        Puesto = D.v_CurrentOccupation,
                                        GrupoSanguineo = H.v_Value1 + " " + I.v_Value1,
                                        MedicoFirmante = p.v_FirstLastName + " " + p.v_SecondLastName + " " + p.v_FirstName

                                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        objEntity = objEntity.Where(pstrFilterExpression);
                    }

                    foreach (var item in objEntity)
                    {
                        PersonIds.Add(item.PersonId);
                        ServicioIds.Add(item.ServiceId);
                    }
                    var varValores = DevolverValorCampoPorServicioMejorado(ServicioIds);
                    var Habitos_Personales = DevolverHabitos_Personales(PersonIds);
                    var sql = (from a in objEntity.ToList()

                               select new MatrizMiBanco
                               {
                                   ServiceId = a.ServiceId,
                                   PersonId = a.PersonId,
                                   ProtocolId = a.ProtocolId,
                                   v_CustomerOrganizationId = a.v_CustomerOrganizationId,
                                   v_CustomerLocationId = a.v_CustomerLocationId,
                                   Dni = a.Dni,
                                   Protocolo = a.Protocolo,
                                   TipoEmo = a.TipoEmo,
                                   Empresa = a.Empresa,
                                   ApellidoPaterno = a.ApellidoPaterno,
                                   ApellidoMaterno = a.ApellidoMaterno,
                                   Nombre = a.Nombre,
                                   Genero = a.Genero,
                                   Localidad = a.Localidad,
                                   FechaNacimiento = a.FechaNacimiento,
                                   FechaIngreso = a.FechaIngreso,
                                   Area = a.Area,
                                   Puesto = a.Puesto,
                                   GrupoSanguineo = a.GrupoSanguineo,
                                   MedicoFirmante = a.MedicoFirmante,
                                   FPsicoTiempoTrabajo = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001838") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000001838").Valor,
                                   Talla = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000007").Valor,
                                   Peso = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000008").Valor,
                                   Imc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000009").Valor,
                                   PerimetoAbdominal = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000010").Valor,
                                   PerimetoCadera = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000011").Valor,
                                   ICC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000012").Valor,
                                   PAS = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000001").Valor,
                                   PAD = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000002").Valor,
                                   Fc = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000003").Valor,
                                   Temperatura = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000004") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000004").Valor,
                                   Fr = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000005").Valor,
                                   SatO2 = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-MF000000006").Valor,
                                   OjoDerechoSC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003547").ValorName,
                                   OjoIzquierdoSC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003548").ValorName,
                                   VLejosODCC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003553").ValorName,
                                   VLejosOICC = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N009-MF000003554").ValorName,
                                   OD125Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000043") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000043").Valor,
                                   OD250Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000044") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000044").Valor,
                                   OD500Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000001").Valor,
                                   OD1000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000002").Valor,
                                   OD2000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000003").Valor,
                                   OD3000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000004").Valor,
                                   OD4000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000005").Valor,
                                   OD6000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000006").Valor,
                                   OD8000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000007").Valor,
                                   OD250Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000046") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000046").Valor,
                                   OD500Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000008").Valor,
                                   OD1000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000009").Valor,
                                   OD2000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000010").Valor,
                                   OD3000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000011").Valor,
                                   OD4000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000012").Valor,
                                   OD6000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000013").Valor,
                                   OI125Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000047") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000047").Valor,
                                   OI250Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000048") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000048").Valor,
                                   OI500Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000015").Valor,
                                   OI1000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000016").Valor,
                                   OI2000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000017").Valor,
                                   OI3000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000018").Valor,
                                   OI4000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000019").Valor,
                                   OI6000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000020").Valor,
                                   OI8000Aerea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000021").Valor,
                                   OI250Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000050") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000050").Valor,
                                   OI500Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000022").Valor,
                                   OI1000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000023").Valor,
                                   OI2000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000024").Valor,
                                   OI3000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000025").Valor,
                                   OI4000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000026").Valor,
                                   OI6000Osea = varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027") == null ? " " : varValores.Find(p => p.ServicioId == a.ServiceId).CampoValores.Find(o => o.IdCampo == "N002-AUD00000027").Valor,
                                   
                               }

                               ).ToList();
                    return sql;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Reportes

        public List<OsteomuscularNuevo> ReportOsteoMuscularNuevo(string pstrserviceId, string pstrComponentId, string idComponentReport)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join E in dbContext.servicecomponent on new { a = pstrserviceId, b = pstrComponentId }
                                                                        equals new { a = E.v_ServiceId, b = E.v_ComponentId }

                                 // Usuario Medico Evaluador / Medico Aprobador ****************************
                                 join me in dbContext.systemuser on E.i_ApprovedUpdateUserId equals me.i_SystemUserId into me_join
                                 from me in me_join.DefaultIfEmpty()

                                 join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                 from pme in pme_join.DefaultIfEmpty()
                                 //**********************************************************************************************

                                 join I in dbContext.protocol on A.v_ProtocolId equals I.v_ProtocolId into I_join
                                 from I in I_join.DefaultIfEmpty()

                                 join J in dbContext.organization on I.v_EmployerOrganizationId equals J.v_OrganizationId

                                 join L in dbContext.systemparameter on new { a = I.i_EsoTypeId.Value, b = 118 }
                                                 equals new { a = L.i_ParameterId, b = L.i_GroupId } into L_join
                                 from L in L_join.DefaultIfEmpty()

                                 join Z in dbContext.person on me.v_PersonId equals Z.v_PersonId

                                 where A.v_ServiceId == pstrserviceId

                                 select new OsteomuscularNuevo
                                 {//rrr
                                     IdServicio = A.v_ServiceId,
                                     NOMBRE_PACIENTE = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                                     PUESTO_TRABAJO = B.v_CurrentOccupation,
                                     EMPRESA_CLIENTE = J.v_Name,
                                     FechaNacimiento = B.d_Birthdate.Value,
                                     i_SEXO = B.i_SexTypeId.Value,
                                     NroDNI = B.v_DocNumber,
                                     TIPOESO = I.i_EsoTypeId.Value,
                                     FirmaGraba = pme.b_SignatureImage,
                                     FirmaTrabajador = B.b_FingerPrintImage,
                                     HuellaTrabajadr = B.b_RubricImage,
                                     FECHA_SERVICIO = A.d_ServiceDate.Value
                                 });

                var MedicalCenter = GetInfoMedicalCenter();
                var OsteoMuscular = new ServiceBL().ValoresComponentesUserControl (pstrserviceId, idComponentReport);

                var xxx = OsteoMuscular.Find(p => p.v_ComponentFieldId == "N009-MF000000838");
                var sql = (from a in objEntity.ToList()
                          
                           select new OsteomuscularNuevo
                           {
                               IdServicio = a.IdServicio,
                               FECHA_SERVICIO = a.FECHA_SERVICIO,
                               NOMBRE_PACIENTE = a.NOMBRE_PACIENTE,
                               PUESTO_TRABAJO = a.PUESTO_TRABAJO,
                               EMPRESA_CLIENTE = a.EMPRESA_CLIENTE,
                               FechaNacimiento = a.FechaNacimiento,
                               EDAD = GetAge(a.FechaNacimiento.Value),
                               FirmaGraba = a.FirmaGraba,
                                FirmaTrabajador = a.FirmaTrabajador,
                                HuellaTrabajadr = a.HuellaTrabajadr,
                                i_SEXO = a.i_SEXO,
                               SEXO = a.i_SEXO.ToString(),
                               MANIPULACION_LEVANTAR_CARGAS = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_LEVANTAR_CARGAS) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_LEVANTAR_CARGAS).v_Value1,
                               MANIPULACION_LEVANTAR_CARGAS_DESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_LEVANTAR_CARGAS_DESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_LEVANTAR_CARGAS_DESCRIPCION).v_Value1,
                               
                               OSTEO_MUSCULAR_TAREAS_CARGA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_TAREAS_CARGA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_TAREAS_CARGA).v_Value1,
                               MANIPULACION_EMPUJAR_CARGA_DESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_EMPUJAR_CARGA_DESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_EMPUJAR_CARGA_DESCRIPCION).v_Value1,
                             
                               MANIPULACION_JALAR_CARGAS = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_JALAR_CARGAS) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_JALAR_CARGAS).v_Value1,
                               PMANIPULACIÓN_JALAR_CARGAS_DESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PMANIPULACIÓN_JALAR_CARGAS_DESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PMANIPULACIÓN_JALAR_CARGAS_DESCRIPCION).v_Value1,

                               LEVANTAMIENTO_ENCIMA_DEL_HOMBRO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LEVANTAMIENTO_ENCIMA_DEL_HOMBRO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LEVANTAMIENTO_ENCIMA_DEL_HOMBRO).v_Value1,
                               POSTURA_SEDENTARIA_DESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_SEDENTARIA_DESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_SEDENTARIA_DESCRIPCION).v_Value1,
                               PESOS_SUPERIORES_A_25KGDESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PESOS_SUPERIORES_A_25KGDESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PESOS_SUPERIORES_A_25KGDESCRIPCION).v_Value1,
                               PESOS_SUPERIORES_A_25KG = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PESOS_SUPERIORES_A_25KG) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PESOS_SUPERIORES_A_25KG).v_Value1,
                               LEVANTAMIENTO_POR_ENCIMA_DELHOMBRODESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LEVANTAMIENTO_POR_ENCIMA_DELHOMBRODESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LEVANTAMIENTO_POR_ENCIMA_DELHOMBRODESCRIPCION).v_Value1,
                               MANIPULACION_DE_VALVULAS_ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_DE_VALVULAS_) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_DE_VALVULAS_).v_Value1,
                               MANIPULACION_DE_VALVULAS_DESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_DE_VALVULAS_DESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MANIPULACION_DE_VALVULAS_DESCRIPCION).v_Value1,
                               POSTURA_FORZADA__ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_FORZADA__) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_FORZADA__).v_Value1,
                               POSTURA_FORZADA_DESCRIPCION_ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_FORZADA_DESCRIPCION_) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_FORZADA_DESCRIPCION_).v_Value1,
                               POSTURA_SEDENTARIA__ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_SEDENTARIA__) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.POSTURA_SEDENTARIA__).v_Value1,
                               MOVIMIENTOS_REPETITIVOS__ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MOVIMIENTOS_REPETITIVOS__) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MOVIMIENTOS_REPETITIVOS__).v_Value1,
                               MOVIMIENTOS_REPETITIVOS_DESCRIPCION_ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MOVIMIENTOS_REPETITIVOS_DESCRIPCION_) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MOVIMIENTOS_REPETITIVOS_DESCRIPCION_).v_Value1,
                               SINTOMAS = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.SINTOMAS) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.SINTOMAS).v_Value1,
                               CADERA_POBRE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_POBRE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_POBRE).v_Value1,

                               CADERA_PUNTOS = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PUNTOS) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PUNTOS).v_Value1,
                               CADRA_OBSERVACIONES = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADRA_OBSERVACIONES) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADRA_OBSERVACIONES).v_Value1,
                               MUSLO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO).v_Value1Name,
                               MUSLO_EXCELENTE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_EXCELENTE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_EXCELENTE).v_Value1,
                               _MUSLO_PROMEDIO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants._MUSLO_PROMEDIO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants._MUSLO_PROMEDIO).v_Value1,
                               MUSLO_REGULAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_REGULAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_REGULAR).v_Value1,
                               MUSLO_POBRE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_POBRE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_POBRE).v_Value1,
                               MUSLO_PUNTOS = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_PUNTOS) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_PUNTOS).v_Value1,
                               MUSLO_OBSERVACIONES = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_OBSERVACIONES) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_OBSERVACIONES).v_Value1,
                               ABDOMEN_LAT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT).v_Value1,

                               ABDOMEN_LAT_EXCELENTE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_EXCELENTE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_EXCELENTE).v_Value1,
                               ABDOMEN_LAT_PROMEDIO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PROMEDIO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PROMEDIO).v_Value1,
                               ABDOMEN_LAT_REGULAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_REGULAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_REGULAR).v_Value1,
                               ABDOMEN_LAT_POBRE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_POBRE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_POBRE).v_Value1,
                               ABDOMEN_LAT_PUNTOS = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PUNTOS) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PUNTOS).v_Value1,
                               ABDOMEN_LAT_OBSERVACIONES = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_OBSERVACIONES) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_OBSERVACIONES).v_Value1,
                               TOTAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOTAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOTAL).v_Value1,
                               ABDOMEN = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN).v_Value1,
                               CADERA_EXCELENTE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_EXCELENTE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_EXCELENTE).v_Value1,
                               PUNTOS_ABDOMEN = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PUNTOS_ABDOMEN) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PUNTOS_ABDOMEN).v_Value1,

                               CADERA_PROMEDIO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PROMEDIO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PROMEDIO).v_Value1,
                               ABDOMEN_EXCELENTE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_EXCELENTE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_EXCELENTE).v_Value1,
                               ABDOMEN_PROMEDIO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_PROMEDIO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_PROMEDIO).v_Value1,
                               ABDOMEN_REGULAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_REGULAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_REGULAR).v_Value1,
                               ABDOMEN_POBRE = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_POBRE) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_POBRE).v_Value1,
                               CADERA_REGULAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_REGULAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_REGULAR).v_Value1,
                               ABDOMEN_OBSERVACIONES = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_OBSERVACIONES) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_OBSERVACIONES).v_Value1,
                               CADERA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA).v_Value1,
                               FLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FLEXION).v_Value1,
                               HOMBRO_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFLEXION).v_Value1,

                               HOMBRO_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQEXTENSION).v_Value1,
                               HOMBRO_IZQROTDER = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTDER) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTDER).v_Value1,
                               HOMBRO_IZQROTIZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTIZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTIZQ).v_Value1,
                               HOMBRO_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFUERZA).v_Value1,
                               HOMBRO_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQDOLOR).v_Value1,
                               CODO_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCH).v_Value1,
                               CODO_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHABDUCC).v_Value1,
                               CODO_DCHFELXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFELXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFELXION).v_Value1,
                               CODO_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHEXTENSION).v_Value1,
                               CODO_DCHROTDER = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTDER) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTDER).v_Value1,

                               CODO_DCHROTIZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTIZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTIZQ).v_Value1,
                               CODO_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHDOLOR).v_Value1,
                               CODO_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFUERZA).v_Value1,
                               CODO_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQ).v_Value1,
                               CODO_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQABDUCC).v_Value1,
                               CODO_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFLEXION).v_Value1,
                               CODO_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQEXTENSION).v_Value1,
                               CODO_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROTEXT).v_Value1,
                               CODO_IZQROT_INT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROT_INT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROT_INT).v_Value1,
                               CODO_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFUERZA).v_Value1,

                               CODO_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQDOLOR).v_Value1,
                               MUNIECA_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIECA_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIECA_DCH).v_Value1,
                               MUNIEECA_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHABDUCC).v_Value1,
                               MUNIEECA_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFLEXION).v_Value1,
                               MUNIEECA_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHEXTENSION).v_Value1,
                               MUNIEECA_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTEXT).v_Value1,
                               MUNIEECA_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTINT).v_Value1,
                               MUNIEECA_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFUERZA).v_Value1,
                               MUNIEECA_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHDOLOR).v_Value1,
                               MUNIEECA_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQ).v_Value1,

                               MUNIEECA_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQABDUCC).v_Value1,
                               MUNIEECA_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFLEXION).v_Value1,
                               MUNIEECA_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQEXTENSION).v_Value1,
                               MUNIEECA_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTEXT).v_Value1,
                               MUNIEECA_IZQROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTINT).v_Value1,
                               MUNIEECA_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFUERZA).v_Value1,
                               MUNIEECA_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQDOLOR).v_Value1,
                               CADERA_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCH).v_Value1,
                               CADERA_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHABDUCC).v_Value1,
                               CADERA_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFLEXION).v_Value1,

                               CADERA_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHEXTENSION).v_Value1,
                               CADERA_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTEXT).v_Value1,
                               CADERA_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTINT).v_Value1,
                               CADERA_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFUERZA).v_Value1,
                               CADERA_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHDOLOR).v_Value1,
                               CADERA_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQ).v_Value1,
                               CADERA_IZQABDUC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQABDUC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQABDUC).v_Value1,
                               CADERA_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFLEXION).v_Value1,
                               CADERA_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTEXT).v_Value1,
                               CADERA_IZQROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTINT).v_Value1,


                               CADERA_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQEXTENSION).v_Value1,
                               CADERA_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFUERZA).v_Value1,
                               CADERA_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQDOLOR).v_Value1,
                               RODILLA_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCH).v_Value1,
                               RODILLA_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHABDUCC).v_Value1,
                               RODILLA_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFLEXION).v_Value1,
                               RODILLA_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHEXTENSION).v_Value1,
                               RODILLA_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTEXT).v_Value1,
                               RODILLA_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTINT).v_Value1,
                               RODILLA_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFUERZA).v_Value1,

                               RODILLA_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHDOLOR).v_Value1,
                               RODILLA_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQ).v_Value1,
                               RODILLA_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQABDUCC).v_Value1,
                               RODILLA_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFLEXION).v_Value1,
                               RODILLA_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQEXTENSION).v_Value1,
                               RODILLA_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQROTEXT).v_Value1,
                               RODILLA_IZQINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQINT).v_Value1,
                               RODILLA_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFUERZA).v_Value1,
                               RODILLA_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQDOLOR).v_Value1,
                               TOBILLO_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCH).v_Value1,

                               TOBILLO_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHABDUCC).v_Value1,
                               TOBILLO_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFLEXION).v_Value1,
                               TOBILLO_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHEXTENSION).v_Value1,
                               TOBILLO_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTEXT).v_Value1,
                               TOBILLO_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTINT).v_Value1,
                               TOBILLO_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFUERZA).v_Value1,
                               TOBILLO_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHDOLOR).v_Value1,
                               TOBILLO_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQ).v_Value1,
                               TOBILLO_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQABDUCC).v_Value1,
                               TOBILLO_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFLEXION).v_Value1,

                               TOBILLO_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTEXT).v_Value1,
                               TOBILLO_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQEXTENSION).v_Value1,
                               TOBILLO_IZQROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTINT).v_Value1,
                               TOBILLO_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFUERZA).v_Value1,
                               TOBILLO_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQDOLOR).v_Value1,
                               HOMBRO_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC).v_Value1,
                               HOMBRO_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC).v_Value1,
                               HOMBRO_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH).v_Value1,
                               HOMBRO_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION).v_Value1,
                               HOMBRO_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTEXT).v_Value1,

                               HOMBRO_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR).v_Value1,
                               HOMBRO_DCHFUERZATONO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO).v_Value1,
                               HOMBRO_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION).v_Value1,
                               HOMBRO_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ).v_Value1,
                               HOMBRO_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT).v_Value1,
                               OBSERVACION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OBSERVACION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OBSERVACION).v_Value1,
                               TINEL_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_DERECHO).v_Value1,
                               TINEL_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TINEL_IZQUIERDO).v_Value1,
                               LASEGUE_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_DERECHO).v_Value1,
                               LASEGUE_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LASEGUE_IZQUIERDO).v_Value1,

                               FINKELSTEIN_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FINKELSTEIN_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FINKELSTEIN_DERECHO).v_Value1,
                               ADAM_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_DERECHO).v_Value1,
                               FINKELSTEIN_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FINKELSTEIN_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FINKELSTEIN_IZQUIERDO).v_Value1,
                               ADAM_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ADAM_IZQUIERDO).v_Value1,
                               PHALEN_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_DERECHO).v_Value1,
                               PHALEN_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PHALEN_IZQUIERDO).v_Value1,
                               PIE_CAVO_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_CAVO_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_CAVO_DERECHO).v_Value1,
                               PIE_CAVO_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_CAVO_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_CAVO_IZQUIERDO).v_Value1,
                               PIE_PLANO_DERECHO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_DERECHO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_DERECHO).v_Value1,
                               PIE_PLANO_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_IZQUIERDO).v_Value1,

                               CERVICAL_AP = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_AP) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_AP).v_Value1,
                               DORSAL_AP = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_AP) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_AP).v_Value1,
                               LUMBAR_AP = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_AP) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_AP).v_Value1,
                               DORSAL_LATERAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LATERAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LATERAL).v_Value1,
                               LUMBAR_LATERAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_LATERAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_LATERAL).v_Value1,
                               CERVICAL_LATERALIZACION_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_DERECHA).v_Value1,
                               CERVICAL_LATERALIZACION_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_IZQUIERDA).v_Value1,
                               DORSAL_LUMBAR_LATERAL_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_IZQUIERDA).v_Value1,
                               DORSAL_LUMBAR_ROACION_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_DERECHA).v_Value1,
                               CERVICAL_EXTENXION__ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_EXTENXION__) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_EXTENXION__).v_Value1,

                               DORSAL_LUMBAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR).v_Value1,
                               DORSAL_LUMBAR_EXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_EXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_EXTENSION).v_Value1,
                               CERVICAL_ROTACION_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_DERECHA).v_Value1,
                               CERVICAL_ROTACION_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_IZQUIERDA).v_Value1,
                               CERVICAL_IRRADIACION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_IRRADIACION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_IRRADIACION).v_Value1Name,
                               DORSAL_LUMBAR_LATERAL_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_DERECHA).v_Value1Name,
                               CERVICAL_FLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_FLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_FLEXION).v_Value1,
                               DORSAL_LUMBAR_IRRADIACION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION).v_Value1,
                               DORSAL_LUMBAR_ROACION_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA).v_Value1,
                               COLUMNA_CERVICAL_CONTRACTURA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL_CONTRACTURA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL_CONTRACTURA).v_Value1,

                               COLUMNA_LUMBAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR).v_Value1,
                               COLUMNA_DORSAL_CONTRACTURA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL_CONTRACTURA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL_CONTRACTURA).v_Value1,
                               COLUMNA_LUMBAR_CONTACTURA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR_CONTACTURA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR_CONTACTURA).v_Value1,
                               COLUMNA_DORSAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL).v_Value1,
                               COLUMNA_CERVICAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL).v_Value1,
                               DESCRIPCION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DESCRIPCION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DESCRIPCION).v_Value1,
                               b_Logo = MedicalCenter.b_Image,
                               EmpresaPropietaria = MedicalCenter.v_Name,
                               EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                               EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                               EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                               NroDNI = a.NroDNI,
                               TIPOESO = a.TIPOESO,
                               Recomendaciones = GetRecomendationByServiceIdAndComponentConcatec(a.IdServicio, Constants.OSTEO_MUSCULAR_ID_1),
                               DxCIE10 = GetDisgnosticsCIE10ByServiceIdAndComponentConcatec(a.IdServicio, Constants.OSTEO_MUSCULAR_ID_1)
                           }).ToList();

                return sql;
            }
            catch (Exception)
            {

                throw;
            }
        }

  
        #endregion

        //Alberto

        public List<PacientList> GetPacientsPagedAndFilteredByPErsonId(ref OperationResult pobjOperationResult, int? pintPageIndex, int pintResultsPerPage, string pstrPErsonId)
        {
            //mon.IsActive = true;
            try
            {
                int intId = -1;
              
                var Id = intId.ToString();
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.pacient
                             join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where  A.v_PersonId ==pstrPErsonId
                             select new PacientList
                             {
                                 v_PersonId = A.v_PersonId,
                                 v_FirstName = B.v_FirstName,
                                 v_FirstLastName = B.v_FirstLastName,
                                 v_SecondLastName = B.v_SecondLastName,
                                 v_AdressLocation = B.v_AdressLocation,
                                 v_TelephoneNumber = B.v_TelephoneNumber,
                                 v_Mail = B.v_Mail,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,
                                 i_DepartmentId = B.i_DepartmentId,
                                 i_ProvinceId = B.i_ProvinceId,
                                 i_DistrictId = B.i_DistrictId,
                                 i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId,
                                 v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
                                 i_TypeOfInsuranceId = B.i_TypeOfInsuranceId,
                                 i_NumberLivingChildren = B.i_NumberLivingChildren,
                                 i_NumberDependentChildren = B.i_NumberDependentChildren,
                                 i_NumberLiveChildren = B.i_NumberLiveChildren,
                                 i_NumberDeadChildren = B.i_NumberDeadChildren,
                                 v_DocNumber = B.v_DocNumber

                             }).Concat
                            (from A in dbContext.pacient
                             join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where B.v_DocNumber == Id && B.i_IsDeleted == 0
                             select new PacientList
                             {
                                 v_PersonId = A.v_PersonId,
                                 v_FirstName = B.v_FirstName,
                                 v_FirstLastName = B.v_FirstLastName,
                                 v_SecondLastName = B.v_SecondLastName,
                                 v_AdressLocation = B.v_AdressLocation,
                                 v_TelephoneNumber = B.v_TelephoneNumber,
                                 v_Mail = B.v_Mail,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,
                                 i_DepartmentId = B.i_DepartmentId,
                                 i_ProvinceId = B.i_ProvinceId,
                                 i_DistrictId = B.i_DistrictId,
                                 i_ResidenceInWorkplaceId = B.i_ResidenceInWorkplaceId,
                                 v_ResidenceTimeInWorkplace = B.v_ResidenceTimeInWorkplace,
                                 i_TypeOfInsuranceId = B.i_TypeOfInsuranceId,
                                 i_NumberLivingChildren = B.i_NumberLivingChildren,
                                 i_NumberDependentChildren = B.i_NumberDependentChildren,
                                 i_NumberLiveChildren = B.i_NumberLiveChildren,
                                 i_NumberDeadChildren = B.i_NumberDeadChildren,
                                 v_DocNumber = B.v_DocNumber
                             }).OrderBy("v_FirstLastName").Take(pintResultsPerPage);

                List<PacientList> objData = query.ToList();
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

        public Sigesoft.Node.WinClient.BE.Ninioo DevolverNinio(string pstrServiceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 join b in dbContext.ninio on a.v_PersonId equals b.v_PersonId
                                 select new Sigesoft.Node.WinClient.BE.Ninioo
                                 {
                                     v_NombreCuidador = b.v_NombreCuidador,
                                     v_EdadCuidador = b.v_EdadCuidador,
                                     v_DniCuidador = b.v_DniCuidador,
                                     v_NombreMadre= b.v_NombreMadre,
                                     v_NombrePadre=b.v_NombrePadre,
                                     v_EdadMadre=b.v_EdadMadre,
                                     v_EdadPadre=b.v_EdadPadre,
                                     v_DniMadre=b.v_DniMadre,
                                     v_DniPadre=b.v_DniPadre,
                                     i_TipoAfiliacionMadre = b.i_TipoAfiliacionMadre,
                                     i_TipoAfiliacionPadre = b.i_TipoAfiliacionPadre,
                                     v_CodigoAfiliacionMadre=b.v_CodigoAfiliacionMadre,
                                     v_CodigoAfiliacionPadre=b.v_CodigoAfiliacionPadre,
                                     i_GradoInstruccionMadre=b.i_GradoInstruccionMadre,
                                     i_GradoInstruccionPadre=b.i_GradoInstruccionPadre,
                                     v_OcupacionMadre=b.v_OcupacionMadre,
                                     v_OcupacionPadre=b.v_OcupacionPadre,
                                     i_EstadoCivilIdMadre1=b.i_EstadoCivilIdMadre1,
                                     i_EstadoCivilIdPadre=b.i_EstadoCivilIdPadre,
                                     v_ReligionMadre=b.v_ReligionMadre,
                                     v_ReligionPadre=b.v_ReligionPadre,
                                     v_PatologiasGestacion=b.v_PatologiasGestacion,
                                     v_nEmbarazos=b.v_nEmbarazos,
                                     v_nAPN=b.v_nAPN,
                                     v_LugarAPN=b.v_LugarAPN,
                                     v_ComplicacionesParto=b.v_ComplicacionesParto,
                                     v_Atencion=b.v_Atencion,
                                     v_EdadGestacion=b.v_EdadGestacion,
                                     v_Peso=b.v_Peso,
                                     v_Talla=b.v_Talla,
                                     v_PerimetroCefalico=b.v_PerimetroCefalico,
                                     v_PerimetroToracico=b.v_PerimetroToracico,
                                     v_EspecificacionesNac=b.v_EspecificacionesNac,
                                     v_LME=b.v_LME,
                                     v_Mixta=b.v_Mixta,
                                     v_Artificial=b.v_Artificial,
                                     v_InicioAlimentacionComp=b.v_InicioAlimentacionComp,
                                     v_AlergiasMedicamentos=b.v_AlergiasMedicamentos,
                                     v_OtrosAntecedentes=b.v_OtrosAntecedentes,
                                     v_EspecificacionesAgua=b.v_EspecificacionesAgua,
                                     v_EspecificacionesDesague=b.v_EspecificacionesDesague,
                                     v_TiempoHospitalizacion=b.v_TiempoHospitalizacion,
                                     v_QuienTuberculosis = b.v_QuienTuberculosis,
                                     v_QuienAsma = b.v_QuienAsma,
                                     v_QuienVIH = b.v_QuienVIH,
                                     v_QuienDiabetes = b.v_QuienDiabetes,
                                     v_QuienEpilepsia = b.v_QuienEpilepsia,
                                     v_QuienAlergias = b.v_QuienAlergias,
                                     v_QuienViolenciaFamiliar = b.v_QuienViolenciaFamiliar,
                                     v_QuienAlcoholismo = b.v_QuienAlcoholismo,
                                     v_QuienDrogadiccion = b.v_QuienDrogadiccion,
                                     v_QuienHeptitisB = b.v_QuienHeptitisB,
                                     i_QuienTuberculosis = b.i_QuienTuberculosis,
                                     i_QuienAsma = b.i_QuienAsma,
                                     i_QuienVIH = b.i_QuienVIH,
                                     i_QuienDiabetes = b.i_QuienDiabetes,
                                     i_QuienEpilepsia = b.i_QuienEpilepsia,
                                     i_QuienAlergias = b.i_QuienAlergias,
                                     i_QuienViolenciaFamiliar = b.i_QuienViolenciaFamiliar,
                                     i_QuienAlcoholismo = b.i_QuienAlcoholismo,
                                     i_QuienDrogadiccion = b.i_QuienDrogadiccion,
                                     i_QuienHeptitisB = b.i_QuienHeptitisB
                                 }).ToList();

                var result = (from a in objEntity
                              select new Sigesoft.Node.WinClient.BE.Ninioo
                              {
                                  v_NombreCuidador = a.v_NombreCuidador,
                                  v_EdadCuidador = a.v_EdadCuidador,
                                  v_DniCuidador = a.v_DniCuidador,
                                  v_NombreMadre =a.v_NombreMadre,
                                  v_NombrePadre =a.v_NombrePadre,
                                  v_EdadMadre=a.v_EdadMadre,
                                  v_EdadPadre=a.v_EdadPadre,
                                  v_DniMadre=a.v_DniMadre,
                                  v_DniPadre=a.v_DniPadre,
                                  i_TipoAfiliacionMadre = a.i_TipoAfiliacionMadre,
                                  i_TipoAfiliacionPadre = a.i_TipoAfiliacionPadre,
                                  v_CodigoAfiliacionMadre=a.v_CodigoAfiliacionMadre,
                                  v_CodigoAfiliacionPadre=a.v_CodigoAfiliacionPadre,
                                  i_GradoInstruccionMadre=a.i_GradoInstruccionMadre,
                                  i_GradoInstruccionPadre=a.i_GradoInstruccionPadre,
                                  v_OcupacionMadre=a.v_OcupacionMadre,
                                  v_OcupacionPadre=a.v_OcupacionPadre,
                                  i_EstadoCivilIdMadre1=a.i_EstadoCivilIdMadre1,
                                  i_EstadoCivilIdPadre=a.i_EstadoCivilIdPadre,
                                  v_ReligionMadre=a.v_ReligionMadre,
                                  v_ReligionPadre=a.v_ReligionPadre,
                                  v_PatologiasGestacion=a.v_PatologiasGestacion,
                                  v_nEmbarazos=a.v_nEmbarazos,
                                  v_nAPN=a.v_nAPN,
                                  v_LugarAPN=a.v_LugarAPN,
                                  v_ComplicacionesParto=a.v_ComplicacionesParto,
                                  v_Atencion=a.v_Atencion,
                                  v_EdadGestacion=a.v_EdadGestacion,
                                  v_Peso=a.v_Peso,
                                  v_Talla=a.v_Talla,
                                  v_PerimetroCefalico=a.v_PerimetroCefalico,
                                  v_PerimetroToracico=a.v_PerimetroToracico,
                                  v_EspecificacionesNac=a.v_EspecificacionesNac,
                                  v_LME=a.v_LME,
                                  v_Mixta=a.v_Mixta,
                                  v_Artificial=a.v_Artificial,
                                  v_InicioAlimentacionComp=a.v_InicioAlimentacionComp,
                                  v_AlergiasMedicamentos=a.v_AlergiasMedicamentos,
                                  v_OtrosAntecedentes=a.v_OtrosAntecedentes,
                                  v_EspecificacionesAgua=a.v_EspecificacionesAgua,
                                  v_EspecificacionesDesague=a.v_EspecificacionesDesague,
                                  v_TiempoHospitalizacion=a.v_TiempoHospitalizacion,
                                  v_QuienTuberculosis=a.v_QuienTuberculosis,
                                  v_QuienAsma = a.v_QuienAsma,
                                  v_QuienVIH = a.v_QuienVIH,
                                  v_QuienDiabetes = a.v_QuienDiabetes,
                                  v_QuienEpilepsia = a.v_QuienEpilepsia,
                                  v_QuienAlergias = a.v_QuienAlergias,
                                  v_QuienViolenciaFamiliar = a.v_QuienViolenciaFamiliar,
                                  v_QuienAlcoholismo = a.v_QuienAlcoholismo,
                                  v_QuienDrogadiccion = a.v_QuienDrogadiccion,
                                  v_QuienHeptitisB = a.v_QuienHeptitisB,
                                  i_QuienTuberculosis = a.i_QuienTuberculosis,
                                  i_QuienAsma = a.i_QuienAsma,
                                  i_QuienVIH = a.i_QuienVIH,
                                  i_QuienDiabetes = a.i_QuienDiabetes,
                                  i_QuienEpilepsia = a.i_QuienEpilepsia,
                                  i_QuienAlergias = a.i_QuienAlergias,
                                  i_QuienViolenciaFamiliar = a.i_QuienViolenciaFamiliar,
                                  i_QuienAlcoholismo = a.i_QuienAlcoholismo,
                                  i_QuienDrogadiccion = a.i_QuienDrogadiccion,
                                  i_QuienHeptitisB = a.i_QuienHeptitisB
                              }
                        ).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Embarazo> GetEmbarazos(string personId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.embarzo
                                 join b in dbContext.person on a.v_PersonId equals b.v_PersonId
                            where a.v_PersonId == personId && a.i_IsDeleted == 0
                            select new Embarazo
                            {
                                v_PersonId=a.v_PersonId,
                                v_EmbarazoId=a.v_PersonId,
                                v_Anio = a.v_Anio,
                                v_Cpn=a.v_Cpn,
                                v_Complicacion=a.v_Complicacion,
                                v_Parto=a.v_Parto,
                                v_PesoRn=a.v_PesoRn,
                                v_Puerpio=a.v_Puerpio,
                                v_ObservacionesGestacion=a.v_ObservacionesGestacion
                            };

                List<Embarazo> objData = query.ToList();
                return objData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Sigesoft.Node.WinClient.BE.Adolescente DevolverAdolescente(string pstrServiceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 join b in dbContext.adolescente on a.v_PersonId equals b.v_PersonId
                select new Sigesoft.Node.WinClient.BE.Adolescente
                                 {
                                     v_NombreCuidador = b.v_NombreCuidador,
                                     v_EdadCuidador = b.v_EdadCuidador,
                                     v_DniCuidador = b.v_DniCuidador,
                                     v_ViveCon = b.v_ViveCon,
                                     v_EdadInicioTrabajo = b.v_EdadInicioTrabajo,
                                     v_TipoTrabajo = b.v_TipoTrabajo,
                                     v_NroHorasTv = b.v_NroHorasTv,
                                     v_NroHorasJuegos = b.v_NroHorasJuegos,
                                     v_MenarquiaEspermarquia = b.v_MenarquiaEspermarquia,
                                     v_EdadInicioRS = b.v_EdadInicioRS,
                                     v_Observaciones = b.v_Observaciones
                                }).ToList();
      
                    var result = (from a in objEntity
                              select new Sigesoft.Node.WinClient.BE.Adolescente
                              {
                                  v_NombreCuidador = a.v_NombreCuidador,
                                  v_EdadCuidador = a.v_EdadCuidador,
                                  v_DniCuidador = a.v_DniCuidador,
                                  v_ViveCon = a.v_ViveCon,
                                  v_EdadInicioTrabajo = a.v_EdadInicioTrabajo,
                                  v_TipoTrabajo = a.v_TipoTrabajo,
                                  v_NroHorasTv = a.v_NroHorasTv,
                                  v_NroHorasJuegos = a.v_NroHorasJuegos,
                                  v_MenarquiaEspermarquia = a.v_MenarquiaEspermarquia,
                                  v_EdadInicioRS = a.v_EdadInicioRS,
                                  v_Observaciones=a.v_Observaciones
                              }
                            ).FirstOrDefault();
                return result;
            }
            catch(Exception)
            {
                return null;
            }
        
        }

        public Sigesoft.Node.WinClient.BE.Adulto DevolverAdulto(string pstrServiceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 join b in dbContext.adulto on a.v_PersonId equals b.v_PersonId
                                 select new Sigesoft.Node.WinClient.BE.Adulto
                                 {
                                     v_NombreCuidador = b.v_NombreCuidador,
                                     v_EdadCuidador = b.v_EdadCuidador,
                                     v_DniCuidador = b.v_DniCuidador,
                                     v_MedicamentoFrecuente = b.v_MedicamentoFrecuente,
                                     v_ReaccionAlergica = b.v_ReaccionAlergica,
                                     v_InicioRS = b.v_InicioRS,
                                     v_NroPs = b.v_NroPs,
                                     v_FechaUR = b.v_FechaUR,
                                     v_RC = b.v_RC,
                                     v_Parto = b.v_Parto,
                                     v_Prematuro = b.v_Prematuro,
                                     v_Aborto = b.v_Aborto,
                                     v_OtrosAntecedentes=b.v_OtrosAntecedentes,
                                     v_DescripcionAntecedentes = b.v_DescripcionAntecedentes,
                                     v_FlujoVaginal = b.v_FlujoVaginal,
                                     v_ObservacionesEmbarazo = b.v_ObservacionesEmbarazo

                                 }).ToList();

                var result = (from a in objEntity
                              select new Sigesoft.Node.WinClient.BE.Adulto
                              {
                                  v_NombreCuidador = a.v_NombreCuidador,
                                  v_EdadCuidador = a.v_EdadCuidador,
                                  v_DniCuidador = a.v_DniCuidador,
                                  v_MedicamentoFrecuente = a.v_MedicamentoFrecuente,
                                  v_ReaccionAlergica = a.v_ReaccionAlergica,
                                  v_InicioRS = a.v_InicioRS,
                                  v_NroPs = a.v_NroPs,
                                  v_FechaUR = a.v_FechaUR,
                                  v_RC=a.v_RC,
                                  v_Parto = a.v_Parto,
                                  v_Prematuro = a.v_Prematuro,
                                  v_Aborto = a.v_Aborto,
                                  v_OtrosAntecedentes = a.v_OtrosAntecedentes,
                                  v_DescripcionAntecedentes = a.v_DescripcionAntecedentes,
                                  v_FlujoVaginal = a.v_FlujoVaginal,
                                  v_ObservacionesEmbarazo = a.v_ObservacionesEmbarazo
                              }
                        ).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Sigesoft.Node.WinClient.BE.AdultoMayor DevolverAdultoMayor(string pstrServiceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 join b in dbContext.adultomayor on a.v_PersonId equals b.v_PersonId
                                 select new Sigesoft.Node.WinClient.BE.AdultoMayor
                                 {
                                     v_NombreCuidador = b.v_NombreCuidador,
                                     v_EdadCuidador = b.v_EdadCuidador,
                                     v_DniCuidador = b.v_DniCuidador,

                                     v_MedicamentoFrecuente = b.v_MedicamentoFrecuente,
                                     v_ReaccionAlergica = b.v_ReaccionAlergica,
                                     v_InicioRS = b.v_InicioRS,
                                     v_NroPs = b.v_NroPs,
                                     v_FechaUR = b.v_FechaUR,
                                     v_RC = b.v_RC,
                                     v_Parto = b.v_Parto,
                                     v_Prematuro = b.v_Prematuro,
                                     v_Aborto = b.v_Aborto,
                                     v_DescripciónAntecedentes = b.v_DescripciónAntecedentes,
                                     v_FlujoVaginal = b.v_FlujoVaginal,
                                     v_ObservacionesEmbarazo = b.v_ObservacionesEmbarazo
                                 }).ToList();

                var result = (from a in objEntity
                              select new Sigesoft.Node.WinClient.BE.AdultoMayor
                              {
                                  v_NombreCuidador = a.v_NombreCuidador,
                                  v_EdadCuidador = a.v_EdadCuidador,
                                  v_DniCuidador = a.v_DniCuidador,
                                  v_MedicamentoFrecuente = a.v_MedicamentoFrecuente,
                                  v_ReaccionAlergica = a.v_ReaccionAlergica,
                                  v_InicioRS = a.v_InicioRS,
                                  v_NroPs = a.v_NroPs,
                                  v_FechaUR = a.v_FechaUR,
                                  v_RC = a.v_RC,
                                  v_Parto = a.v_Parto,
                                  v_Prematuro = a.v_Prematuro,
                                  v_Aborto = a.v_Aborto,
                                  v_DescripciónAntecedentes=a.v_DescripciónAntecedentes,
                                  v_FlujoVaginal=a.v_FlujoVaginal,
                                  v_ObservacionesEmbarazo=a.v_ObservacionesEmbarazo
                              }
                        ).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Sigesoft.Node.WinClient.BE.PacientList DevolverDatosPaciente(string pstrServiceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 join b in dbContext.person on a.v_PersonId equals b.v_PersonId

                                 join c in dbContext.systemparameter on new { a = b.i_SexTypeId.Value, b = 100 }
                                    equals new { a = c.i_ParameterId, b = c.i_GroupId }  // GENERO
                                 join d in dbContext.protocol on a.v_ProtocolId equals d.v_ProtocolId


                                 join E in dbContext.datahierarchy on new { a = b.i_DepartmentId.Value, b = 113 }
                                                       equals new { a = E.i_ItemId, b = E.i_GroupId } into E_join
                                 from E in E_join.DefaultIfEmpty()

                                 join F in dbContext.datahierarchy on new { a = b.i_ProvinceId.Value, b = 113 }
                                                       equals new { a = F.i_ItemId, b = F.i_GroupId } into F_join
                                 from F in F_join.DefaultIfEmpty()

                                 join G in dbContext.datahierarchy on new { a = b.i_DistrictId.Value, b = 113 }
                                                       equals new { a = G.i_ItemId, b = G.i_GroupId } into G_join
                                 from G in G_join.DefaultIfEmpty()

                                 join H in dbContext.person on a.v_PersonId equals H.v_PersonId into H_join
                                 from H in H_join.DefaultIfEmpty()

                                 join I in dbContext.datahierarchy on new { a = H.i_DepartmentId.Value, b = 113 }
                                                       equals new { a = I.i_ItemId, b = I.i_GroupId } into I_join
                                 from I in I_join.DefaultIfEmpty()

                                 join J in dbContext.datahierarchy on new { a = H.i_ProvinceId.Value, b = 113 }
                                                       equals new { a = J.i_ItemId, b = J.i_GroupId } into J_join
                                 from J in J_join.DefaultIfEmpty()

                                 join K in dbContext.datahierarchy on new { a = H.i_DistrictId.Value, b = 113 }
                                                       equals new { a = K.i_ItemId, b = K.i_GroupId } into K_join
                                 from K in K_join.DefaultIfEmpty()

                                 join M in dbContext.systemparameter on new { a = H.i_MaritalStatusId.Value, b = 101 }
                                              equals new { a = M.i_ParameterId, b = M.i_GroupId } into M_join
                                 from M in M_join.DefaultIfEmpty()

                                 join N in dbContext.datahierarchy on new { a = H.i_LevelOfId.Value, b = 108 }
                                                 equals new { a = N.i_ItemId, b = N.i_GroupId } into N_join
                                 from N in N_join.DefaultIfEmpty()

                                 join P in dbContext.systemparameter on new { a = b.i_BloodGroupId.Value, b = 154 }
                                                 equals new { a = P.i_ParameterId, b = P.i_GroupId } into P_join
                                 from P in P_join.DefaultIfEmpty()

                                 join Q in dbContext.systemparameter on new { a = b.i_BloodFactorId.Value, b = 155 }
                                                 equals new { a = Q.i_ParameterId, b = Q.i_GroupId } into Q_join
                                 from Q in Q_join.DefaultIfEmpty()

                                 join r in dbContext.servicecomponent on a.v_ServiceId equals r.v_ServiceId

                                 // Empresa / Sede Cliente ******************************************************
                                 join oc in dbContext.organization on new { a = d.v_CustomerOrganizationId }
                                         equals new { a = oc.v_OrganizationId } into oc_join
                                 from oc in oc_join.DefaultIfEmpty()

                                 join z in dbContext.organization on new { a = d.v_EmployerOrganizationId }
                                         equals new { a = z.v_OrganizationId } into z_join
                                 from z in z_join.DefaultIfEmpty()

                                 join lc in dbContext.location on new { a = d.v_CustomerOrganizationId, b = d.v_CustomerLocationId }
                                       equals new { a = lc.v_OrganizationId, b = lc.v_LocationId } into lc_join
                                 from lc in lc_join.DefaultIfEmpty()

                                 //**********************************************************************************
                                 join O in dbContext.systemparameter on new { a = 134, b = a.i_MacId.Value }
                                                      equals new { a = O.i_GroupId, b = O.i_ParameterId } into O_join
                                 from O in O_join.DefaultIfEmpty()
                                 join J4 in dbContext.systemparameter on new { ItemId = a.i_AptitudeStatusId.Value, groupId = 124 }
                                      equals new { ItemId = J4.i_ParameterId, groupId = J4.i_GroupId } into J4_join
                                 from J4 in J4_join.DefaultIfEmpty()

                                 join su in dbContext.systemuser on a.i_UpdateUserMedicalAnalystId.Value equals su.i_SystemUserId into su_join
                                 from su in su_join.DefaultIfEmpty()

                                 join pr in dbContext.professional on su.v_PersonId equals pr.v_PersonId into pr_join
                                 from pr in pr_join.DefaultIfEmpty()

                                 join su1 in dbContext.systemuser on a.i_UpdateUserOccupationalMedicaltId.Value equals su1.i_SystemUserId into su1_join
                                 from su1 in su1_join.DefaultIfEmpty()

                                 join pr2 in dbContext.professional on su1.v_PersonId equals pr2.v_PersonId into pr2_join
                                 from pr2 in pr2_join.DefaultIfEmpty()

                                 where a.v_ServiceId == pstrServiceId && a.i_IsDeleted == 0
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {
                                     Trabajador = b.v_FirstLastName + " " + b.v_SecondLastName + " " + b.v_FirstName,
                                     d_Birthdate = b.d_Birthdate.Value,
                                     //
                                     v_PersonId = b.v_PersonId,
                                      
                                     v_FirstLastName = b.v_FirstLastName,
                                     v_SecondLastName=b.v_SecondLastName,
                                     v_FirstName = b.v_FirstName,
                                     v_BirthPlace = b.v_BirthPlace,
                                     v_DepartamentName = I.v_Value1,
                                     v_ProvinceName = J.v_Value1,
                                     v_DistrictName = K.v_Value1,
                                     v_AdressLocation = b.v_AdressLocation,
                                     GradoInstruccion = N.v_Value1,
                                     v_CentroEducativo = b.v_CentroEducativo,
                                     v_MaritalStatus = M.v_Value1,
                                     v_BloodGroupName = P.v_Value1,
                                     v_BloodFactorName = Q.v_Value1,
                                     v_IdService = a.v_ServiceId,
                                     v_OrganitationName = oc.v_Name,
                                     i_NumberLivingChildren = b.i_NumberLivingChildren,
                                     FechaCaducidad = a.d_GlobalExpirationDate,
                                     FechaActualizacion = a.d_UpdateDate,
                                     N_Informe = r.v_ServiceComponentId,
                                     v_Religion = b.v_Religion,
                                     v_Nacionalidad=b.v_Nacionalidad,
                                     v_ResidenciaAnterior = b.v_ResidenciaAnterior,
                                     i_DocTypeId = b.i_DocTypeId,
                                     v_OwnerName = b.v_OwnerName,
                                     v_Employer = z.v_Name,
                                     v_ContactName = b.v_ContactName,
                                     i_Relationship = b.i_Relationship,
                                     v_EmergencyPhone = b.v_EmergencyPhone,
                                     //
                                     Genero = c.v_Value1,
                                     i_SexTypeId = b.i_SexTypeId,
                                     v_DocNumber = b.v_DocNumber,
                                     v_TelephoneNumber = b.v_TelephoneNumber,
                                     Empresa = oc.v_Name,
                                     Sede = lc.v_Name,
                                     v_CurrentOccupation = b.v_CurrentOccupation,
                                     FechaServicio = a.d_ServiceDate.Value,
                                     i_MaritalStatusId = b.i_MaritalStatusId,
                                     // Antecedentes ginecologicos
                                     d_PAP = a.d_PAP.Value,
                                     d_Mamografia = a.d_Mamografia.Value,
                                     v_CiruGine = a.v_CiruGine,
                                     v_Gestapara = a.v_Gestapara,
                                     v_Menarquia = a.v_Menarquia,
                                     v_Findings = a.v_Findings,
                                     d_Fur = a.d_Fur,
                                     v_CatemenialRegime = a.v_CatemenialRegime,
                                     i_MacId = a.i_MacId,
                                     v_Mac = O.v_Value1,
                                     v_Story = a.v_Story,
                                     Aptitud = J4.v_Value1,
                                     b_FirmaAuditor = pr.b_SignatureImage,
                                     FirmaTrabajador = b.b_RubricImage,
                                     HuellaTrabajador = b.b_FingerPrintImage,
                                     FirmaDoctorAuditor = pr2.b_SignatureImage,
                                 }
                                ).ToList();
                var DatosMedicoMedicinaEvaluador = ObtenerDatosMedicoMedicina(pstrServiceId, Constants.EXAMEN_FISICO_ID, Constants.EXAMEN_FISICO_7C_ID);
                //var DatosMedicoMedicinaAuditor = ObtenerDatosMedicoMedicinaAuditor(pstrServiceId, Constants.EXAMEN_FISICO_ID, Constants.EXAMEN_FISICO_7C_ID);
                
                var result = (from a in objEntity
                              select new Sigesoft.Node.WinClient.BE.PacientList
                              {
                                  Trabajador = a.Trabajador,
                                  d_Birthdate = a.d_Birthdate,
                                  v_ContactName = a.v_ContactName,
                                  v_EmergencyPhone = a.v_EmergencyPhone,
                                  i_Relationship = a.i_Relationship,
                                  //
                                  v_PersonId=a.v_PersonId,
                                  v_FirstLastName = a.v_FirstLastName,
                                  v_SecondLastName = a.v_SecondLastName,
                                  v_FirstName = a.v_FirstName,
                                  v_BirthPlace = a.v_BirthPlace,
                                  v_DepartamentName = a.v_DepartamentName,
                                  v_ProvinceName = a.v_ProvinceName,
                                  v_DistrictName = a.v_DistrictName,
                                  GradoInstruccion = a.GradoInstruccion,
                                  v_MaritalStatus=a.v_MaritalStatus,
                                  v_BloodGroupName = a.v_BloodGroupName,
                                  v_BloodFactorName=a.v_BloodFactorName,
                                  v_AdressLocation = a.v_AdressLocation,
                                  v_IdService=a.v_IdService,
                                  v_OrganitationName = a.v_OrganitationName,
                                  i_NumberLivingChildren = a.i_NumberLivingChildren,
                                  v_CentroEducativo = a.v_CentroEducativo,
                                  FechaCaducidad = a.FechaCaducidad,
                                  FechaActualizacion=a.FechaActualizacion,
                                  N_Informe = a.N_Informe,
                                  v_Religion=a.v_Religion,
                                  v_Nacionalidad=a.v_Nacionalidad,
                                  v_ResidenciaAnterior=a.v_ResidenciaAnterior,
                                  i_DocTypeId = a.i_DocTypeId,
                                  v_OwnerName = a.v_OwnerName,
                                  v_Employer = a.v_Employer,
                                  i_MaritalStatusId = a.i_MaritalStatusId,
                                  //
                                  Edad = GetAge(a.d_Birthdate.Value),
                                  Genero = a.Genero,
                                  i_SexTypeId = a.i_SexTypeId,
                                  v_DocNumber = a.v_DocNumber,
                                  v_TelephoneNumber = a.v_TelephoneNumber,
                                  Empresa = a.Empresa,
                                  Sede = a.Sede,
                                  v_CurrentOccupation = a.v_CurrentOccupation,
                                  FechaServicio = a.FechaServicio,
                                  MedicoGrabaMedicina = DatosMedicoMedicinaEvaluador == null ? "" : DatosMedicoMedicinaEvaluador.ApellidosDoctor + " " + DatosMedicoMedicinaEvaluador.NombreDoctor,
                                  // Antecedentes ginecologicos
                                  d_PAP = a.d_PAP,
                                  d_Mamografia = a.d_Mamografia,
                                  v_CiruGine = a.v_CiruGine,
                                  v_Gestapara = a.v_Gestapara,
                                  v_Menarquia = a.v_Menarquia,
                                  v_Findings = a.v_Findings,
                                  d_Fur = a.d_Fur,
                                  v_CatemenialRegime = a.v_CatemenialRegime,
                                  i_MacId = a.i_MacId,
                                  v_Mac = a.v_Mac,
                                  v_Story = a.v_Story,
                                  Aptitud = a.Aptitud,
                                  b_FirmaEvaluador = DatosMedicoMedicinaEvaluador== null?null:DatosMedicoMedicinaEvaluador.FirmaMedicoMedicina,
                                  b_FirmaAuditor = a.b_FirmaAuditor,
                                  FirmaTrabajador = a.FirmaTrabajador,
                                  HuellaTrabajador = a.HuellaTrabajador,
                                  FirmaDoctorAuditor = a.FirmaDoctorAuditor
                              }
                            ).FirstOrDefault();


                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DatosDoctorMedicina ObtenerDatosMedicoMedicina(string pstrServiceId, string p1, string p2)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objEntity = (from E in dbContext.servicecomponent

                             join me in dbContext.systemuser on E.i_ApprovedUpdateUserId equals me.i_SystemUserId into me_join
                             from me in me_join.DefaultIfEmpty()

                             join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                             from pme in pme_join.DefaultIfEmpty()

                             join a in dbContext.person on me.v_PersonId equals a.v_PersonId

                             where E.v_ServiceId == pstrServiceId &&
                             (E.v_ComponentId == p1 || E.v_ComponentId == p2)
                             select new DatosDoctorMedicina
                             {
                                 FirmaMedicoMedicina = pme.b_SignatureImage,
                                 ApellidosDoctor = a.v_FirstLastName + " " + a.v_SecondLastName,
                                 DireccionDoctor = a.v_AdressLocation,
                                 NombreDoctor = a.v_FirstName,
                                 CMP = pme.v_ProfessionalCode,

                             }).FirstOrDefault();

            return objEntity;
        }

        private DatosDoctorMedicina ObtenerDatosMedicoMedicinaAuditor(string pstrServiceId, string p1, string p2)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objEntity = (from E in dbContext.servicecomponent

                             join me in dbContext.systemuser on E.i_AuditorUpdateUserId equals me.i_SystemUserId into me_join
                             from me in me_join.DefaultIfEmpty()

                             join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                             from pme in pme_join.DefaultIfEmpty()

                             join a in dbContext.person on me.v_PersonId equals a.v_PersonId

                             where E.v_ServiceId == pstrServiceId &&
                             (E.v_ComponentId == p1 || E.v_ComponentId == p2)
                             select new DatosDoctorMedicina
                             {
                                 FirmaMedicoMedicina = pme.b_SignatureImage,
                                 ApellidosDoctor = a.v_FirstLastName + " " + a.v_SecondLastName,
                                 DireccionDoctor = a.v_AdressLocation,
                                 NombreDoctor = a.v_FirstName,
                                 CMP = pme.v_ProfessionalCode,

                             }).FirstOrDefault();

            return objEntity;
        }
        public List<Sigesoft.Node.WinClient.BE.NoxiousHabitsList> DevolverHabitosNoscivos(string pstrPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var q4 = (from A in dbContext.noxioushabits
                      where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId
                      select new Sigesoft.Node.WinClient.BE.NoxiousHabitsList
                      {
                          v_NoxiousHabitsName = A.v_Comment,
                          //i_FrequencyId = A.i_FrequencyId,
                          i_TypeHabitsId = A.i_TypeHabitsId.Value
                      }).ToList();

            return q4;

        }

        public List<Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList> DevolverAntecedentesFamiliares(string pstrPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var q4 = (from A in dbContext.familymedicalantecedents
                      where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId
                      select new Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList
                      {
                          i_TypeFamilyId = A.i_TypeFamilyId.Value,
                          v_Comment = A.v_Comment,
                      }).ToList();

            return q4;
        }

        public List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList> DevolverAntecedentesPersonales(string pstrPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var q4 = (from A in dbContext.personmedicalhistory
                      join B in dbContext.diseases on A.v_DiseasesId equals B.v_DiseasesId
                      where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId
                      select new Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList
                      {
                          v_DiseasesName = B.v_Name,
                          //v_FechaInicio = "",
                          v_DiagnosticDetail = A.v_DiagnosticDetail,
                          v_DiseasesId = B.v_DiseasesId,
                          d_StartDate = A.d_StartDate
                      }).ToList();

            return q4;
        }

        public List<Sigesoft.Node.WinClient.BE.HistoryList> DevolverAntecedentesOcupacionales(string pstrPersonId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var q4 = (from A in dbContext.history
                      where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId
                      select new Sigesoft.Node.WinClient.BE.HistoryList
                      {
                          v_Organization = A.v_Organization,
                          v_TypeActivity = A.v_TypeActivity,
                          v_workstation = A.v_workstation,
                          //v_Fechas = A.v_Fechas,
                          //v_Exposicion = A.v_Exposicion,
                          //v_TiempoTrabajo = A.v_TiempoTrabajo,
                          //v_Epps = A.v_Epps

                      }).ToList();

            return q4;
        }

        public List<PuestoList> GetAllPuestos()
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.person
                             where  A.i_IsDeleted == 0
                             select new PuestoList
                             {
                                 PuestoId = A.v_CurrentOccupation,
                                 Puesto = A.v_CurrentOccupation
                             }).ToList();


                var objData = query.AsEnumerable().
                            GroupBy(g => g.Puesto)
                                        .Select(s => s.First());

                List<PuestoList> x = objData.ToList().FindAll(p => p.Puesto != "" || p.Puesto != null );
                return x;
            }
            catch (Exception ex)
            { return null;
            }
        }

        public List<CCostoList> GetAllCCosto()
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.service
                             where A.i_IsDeleted == 0
                             select new CCostoList
                             {
                                 CCostoId = A.v_centrocosto,
                                 CCosto = A.v_centrocosto
                             }).ToList();


                var objData = query.AsEnumerable().
                            GroupBy(g => g.CCosto)
                                        .Select(s => s.First());

                List<CCostoList> x = objData.ToList().FindAll(p => p.CCosto != "" || p.CCosto != null);
                return x;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public List<PersonList_2> LlenarPerson(ref OperationResult objOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int isNotDeleted = (int)SiNo.NO;
                var query = (from A in dbContext.person
                    join P in dbContext.pacient on A.v_PersonId equals P.v_PersonId
                             where A.i_IsDeleted == isNotDeleted
                    select new PersonList_2
                    {
                        v_name = A.v_FirstLastName + " " + A.v_SecondLastName + " " + A.v_FirstName + " | " + A.v_PersonId,
                        v_personId = A.v_PersonId
                    }).ToList();


                var objData = query.AsEnumerable().
                    GroupBy(g => g.v_name)
                    .Select(s => s.First());

                List<PersonList_2> x = objData.ToList();
                objOperationResult.Success = 1;
                return x;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }



        public object LlenarDxsTramas(ref OperationResult objOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int isYesDeleted = (int)SiNo.SI;
                var query = (from A in dbContext.cie10
                    where A.i_IsDeleted == isYesDeleted
                          select new ListaDxsTramas
                          {
                              v_Name = A.v_CIE10Description1,
                              v_CIE10Id = A.v_CIE10Id
                    }).ToList();
                var objData = query.AsEnumerable().
                    GroupBy(g => g.v_Name)
                    .Select(s => s.First());
                List<ListaDxsTramas> x = objData.ToList();
                objOperationResult.Success = 1;
                return x;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }

        }

        public object LlenarListaProc(ref OperationResult objOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int isNotDeleted = (int)SiNo.NO;
                var query = (from A in dbContext.systemparameter
                             where A.i_IsDeleted == isNotDeleted && A.i_GroupId==348
                    select new ListaProcedimientos
                    {
                        v_Value1 = A.v_Value1.ToUpper(),
                        i_ParameterId = A.i_ParameterId
                    }).ToList();
                var objData = query.AsEnumerable().
                    GroupBy(g => g.v_Value1)
                    .Select(s => s.First());
                List<ListaProcedimientos> x = objData.ToList();
                objOperationResult.Success = 1;
                return x;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public object LlenarListaUps(ref OperationResult objOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int isNotDeleted = (int)SiNo.NO;
                var query = (from A in dbContext.systemparameter
                    where A.i_IsDeleted == isNotDeleted && A.i_GroupId == 349
                    select new ListaUpsEspecialidades
                    {
                        v_Value1 = A.v_Value1,
                        i_ParameterId = A.i_ParameterId
                    }).ToList();
                var objData = query.AsEnumerable().
                    GroupBy(g => g.v_Value1)
                    .Select(s => s.First());
                List<ListaUpsEspecialidades> x = objData.ToList();
                objOperationResult.Success = 1;
                return x;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
    }
}

