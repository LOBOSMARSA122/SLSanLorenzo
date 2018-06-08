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
                                     i_NumberDeadChildren = B.i_NumberDeadChildren
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



                                 where s.v_ServiceId == serviceId
                                 select new PacientList
                                 {
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
                                     FirmaDoctor = pr1.b_SignatureImage      ,
                                     v_ExaAuxResult = s.v_ExaAuxResult,
                                     FirmaDoctorAuditor = pr2.b_SignatureImage,
                                     GESO = F.v_Name,
                                     i_AptitudeStatusId = s.i_AptitudeStatusId,
                                     v_MaritalStatus = H.v_Value1,
                                     //EmpresaClienteId = ow.v_OrganizationId
                                     
                                 });

             
                var sql = (from a in objEntity.ToList()
                         
                           select new PacientList
                            {
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
                                //EmpresaClienteId = a.EmpresaClienteId
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
                                     v_ExaAuxResult = s.v_ExaAuxResult

                                 });


                var sql = (from a in objEntity.ToList()

                           select new PacientList
                           {
                               FirmaDoctor = a.FirmaDoctor,
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
        public List<ReportAlturaEstructural> GetAlturaEstructural(string pstrserviceId, string pstrComponentId)
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
                                 

                                 where A.v_ServiceId == pstrserviceId
                                 select new ReportAlturaEstructural
                                 {
                                     EmpresaCliente = D1.v_Name,
                                     v_ComponentId = E.v_ServiceComponentId,
                                     v_ServiceId = A.v_ServiceId,
                                     NombrePaciente = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                     EmpresaTrabajadora = D.v_Name,
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
                var valores = new ServiceBL().ValoresComponente(pstrserviceId, Constants.ALTURA_ESTRUCTURAL_ID);
                var sql = (from a in objEntity.ToList()
                           select new ReportAlturaEstructural
                            {
                                EmpresaCliente = a.EmpresaCliente,
                                v_ComponentId = a.v_ComponentId,
                                v_ServiceId = a.v_ServiceId,
                               ServicioId = a.ServicioId,
                               NombrePaciente = a.NombrePaciente,
                               EmpresaTrabajadora =a.EmpresaTrabajadora,
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
                                EmpresaPropietariaDireccion = MedicalCenter.v_Address,
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

                                ODSCLEJOS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000637") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000637").v_Value1,

                                OI_SC_LEJOS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000638") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000638").v_Value1,

                                OD_CC_LEJOS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000639") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000639").v_Value1,

                                OI_CC_LEJOS  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000647") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000647").v_Value1,

                                OD_AE_LEJOS2  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002078") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002078").v_Value1,

                                OI_AE_LEJOS2  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002079") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002079").v_Value1,

                                SC_LEJOSOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000234") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000234").v_Value1,

                                SCLEJOSOJOIZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000230") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000230").v_Value1,

                                CCLEJOSOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000231") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000231").v_Value1,

                                CCLEJOSOJ_IZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000236") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000236").v_Value1,

                                AELEJOSOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002080") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002080").v_Value1,

                                AELEJOSOJOIZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002081") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002081").v_Value1,

                                SCCERCAOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000233") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000233").v_Value1,

                                S_CCERCAOJOIZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000227") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000227").v_Value1,

                                CCCERCAOJODERECHO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000235") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000235").v_Value1,

                                CCCERCAOJOIZQUIERDO  = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000646") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000646").v_Value1,

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
                                DESCRIPCION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000261") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000261").v_Value1,

                                TIEMPO = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000258") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000258").v_Value1,

                                RECUPERACION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002093") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002093").v_Value1,




                                CAMPIMETRIAOD = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002094") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002094").v_Value1,

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
                                 join C1 in dbContext.organization on B.v_CustomerOrganizationId equals C1.v_OrganizationId into C1_join
                                 from C1 in C1_join.DefaultIfEmpty()
                      
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

                                 let varDpto = dep.v_Value1 == null ? "" : dep.v_Value1
                                 let varProv = prov.v_Value1 == null ? "" : prov.v_Value1
                                 let varDistri = distri.v_Value1 == null ? "" : distri.v_Value1

                                 where A.v_ServiceId == pstrServiceId

                                 select new ReportConsentimiento
                                 {
                                     NombreTrabajador = P1.v_FirstName + " " + P1.v_FirstLastName +  " " + P1.v_SecondLastName,
                                     NroDocumento = P1.v_DocNumber,
                                     Ocupacion = P1.v_CurrentOccupation,
                                     Empresa = C.v_Name,
                                     Contratista = C1.v_Name,
                                     FirmaTrabajador = P1.b_RubricImage,
                                     HuellaTrabajador = P1.b_FingerPrintImage,
                                     LugarProcedencia = varDistri + "-" + varProv + "-" + varDpto, // Santa Anita - Lima - Lima
                                     v_AdressLocation = p.v_AdressLocation,
                                     d_ServiceDate = A.d_ServiceDate,

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


                                   DxExaMedicoGeneral = ExaMedGeneral== "" ? "NO APLICA" :ExaMedGeneral,
                                   DxMusculoEsqueletico = ExaMusculoEsqueletico == "" ? "NO APLICA" : ExaMusculoEsqueletico,



                                   EvAltura180 = EvaAltura == "" ? "NO APLICA" : EvaAltura,// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor,
                                   Exa7D = Exa7D == "" ? "NO APLICA" : Exa7D,//eva varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).ValorName,
                                   EvaNeurologica = EvaNeurologica== "" ? "NO APLICA" :EvaNeurologica, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor,
                                   TamizajeDermatologico = TamizajeDer== "" ? "NO APLICA" :TamizajeDer, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor,



                                   DxRadiografiaTorax = RadioTorax== "" ? "NO APLICA" :RadioTorax,
                                   DxRadiografiaOIT = RadioOIT== "" ? "NO APLICA" :RadioOIT,
                                   InidceNeumoconiosis = Y== "" ? "NO APLICA" :Y,

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
                                   Dxaudiometria = AudiometriaDxs== "" ? "NO APLICA" :AudiometriaDxs,


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
                                 
                                   
                                   
                                   
                                   
                                   DxOdontologia = Odontograma== "" ? "NO APLICA" : Odontograma,
                                   DxElectrocardiograma = Electrocardiograma== "" ? "NO APLICA" :Electrocardiograma ,
                                   PruebaEsfuerzo = PbaEsfuerzo== "" ? "NO APLICA" :PbaEsfuerzo ,

                                   AreaCognitiva = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor,
                                   AreaEmocional = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor,
                                   AreaPersonal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor,
                                   AptitudPsicologica = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor,
                                   DxPsicologia = Psicologia== "" ? "NO APLICA" :Psicologia ,

                                   GrupoFactor = Grupo + " - " + Factor,
                                   Leucocitos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor,
                                   DxLeucocitos = DxLeucocitos ==null?"NO APLICA" :DxLeucocitos== "" ? "NORMAL" : DxLeucocitos,
                                   Hemoglobina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor,

                                   DxHemoglobina = DxHemoglobina == null ? "NO APLICA" : DxHemoglobina == "" ? "NORMAL" : DxHemoglobina,
                                   Eosi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor,
                                   RecuentoPlaquetas = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor,

                                   DxHemograma = DxHemograma == null ? "NO APLICA" : DxHemograma == "" ? "NORMAL" : DxHemograma,
                                   Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor,
                                   DxGlucosa = DxGlucosa == null ? "NO APLICA" : DxGlucosa == "" ? "NORMAL" : DxGlucosa,
                                   Colesterol = Colesterol1 ,
                                   DxColesterol = DxColesterol1 == "" ? "NO APLICA" : DxColesterol1 == "" ? "NORMAL" : DxColesterol1,
                                   Colesterolv2 = Colesterol2,
                                   DxColesterolLipidico = DxColesterol2 == "" ? "NO APLICA" : DxColesterol2 == "" ? "NORMAL" : DxColesterol2,

                                   Hdl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor,
                                   DxHdl =   DxHDL,// == "SinDx" ? "NORMAL" : DxHDL == "" ? "NORMAL" : DxHDL,
                                   Ldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor,
                                   DxLdl = DxLDL == null ? "NO APLICA" : DxLDL == "" ? "NORMAL" : DxLDL,
                                   Vldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor,
                                   DxVldl = DxVLDL == null ? "NO APLICA" : DxVLDL == "" ? "NORMAL" : DxVLDL,
                                   Trigliceridos = Trigli1== "" ? "NO APLICA" :Trigli1  ,
                                   DxTgc = DxTGC1 == null ? "NO APLICA" : DxTGC1 == "" ? "NORMAL" : DxTGC1,

                                    Trigliceridos2 = Trigli2== "" ? "NO APLICA" :Trigli2  ,
                                   DxTgc2 = DxTGC2 == null ? "NO APLICA" : DxTGC2 == "" ? "NORMAL" : DxTGC2,

                                   Urea = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor,
                                   Creatina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor,

                                   Tgo = TGO1 + TGO2,
                                   Tgp = TGP1 + TGP2,
                                   Leuc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor,
                                   Hemat = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor,

                                   Marihuana = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).ValorName,
                                   Cocaina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).ValorName,

                                   Vdrl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor,
                                   Colinesterasa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == "N009-ME000000042") == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == "N009-ME000000042" && o.IdCampo == "N009-MF000000393").Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == "N009-ME000000042" && o.IdCampo == "N009-MF000000393").Valor,

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
                                        Discapacitado = "???????????????",
                                        Discapacitado_ID = 0000000000000000,
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
                                   IMC_CIE10 = "????????",
                                   IMC_Obs = "",
                                   Cintura = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor),
                                   Cadera = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor),
                                   ICC = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor),
                                   Sistolica = 0000000000,
                                   Sistolica_CIE10 = "?????????",
                                   Sistolica_Obs = "",
                                   Diastolica = 000000000,
                                   Diastolica_CIE10 = "????????",
                                   Diastolica_Obs = "?????????",
                                   FC = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor),
                                   FR = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? 0 : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor == "" ? 0 : decimal.Parse(varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor),
                                   ///////////////////// OFTALMO //////////////////////////
                                   Sin_Corr_Cerca_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor,
                                   Sin_Corr_Cerca_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000230").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor,
                                   Sin_Corr_Lejos_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor,
                                   Sin_Corr_Lejos_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor,
                                   Corr_Cerca_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor,
                                   Corr_Cerca_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor,
                                   Corr_Lejos_OD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor,
                                   Corr_Lejos_OI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor,
                                   OD_CIE10 = "????????",
                                   OD_Obs = "",
                                   OI_CIE10 = "???????????",
                                   OI_Obs = "",
                                   Discro = "??????????",
                                   Discro_CIE10 = "???????",
                                   Discro_Obs = "",
                                   /////////////////// AUDIOMETRIA ////////////////////////
                                   Otoscopia_OD = "?????????????",
                                   Otoscopia_OI = "?????????????",

                                   Oido_Der_125 = "?????????????",
                                   Oido_Der_250 = "?????????????",
                                   Oido_Der_500 = "?????????????",
                                   Oido_Der_750 = "?????????????",
                                   Oido_Der_1000 = "?????????????",
                                   Oido_Der_1500 = "?????????????",
                                   Oido_Der_2000 = "?????????????",
                                   Oido_Der_3000 = "?????????????",
                                   Oido_Der_4000 = "?????????????",
                                   Oido_Der_6000 = "?????????????",
                                   Oido_Der_8000 = "?????????????",

                                   Oido_Izq_125 = "?????????????",
                                   Oido_Izq_250 = "?????????????",
                                   Oido_Izq_500 = "?????????????",
                                   Oido_Izq_750 = "?????????????",
                                   Oido_Izq_1000 = "?????????????",
                                   Oido_Izq_1500 = "?????????????",
                                   Oido_Izq_2000 = "?????????????",
                                   Oido_Izq_3000 = "?????????????",
                                   Oido_Izq_4000 = "?????????????",
                                   Oido_Izq_6000 = "?????????????",
                                   Oido_Izq_8000 = "?????????????",

                                   Audiometria_D_CIE10 = "?????????????",
                                   Audiometria_D_Obs = "",
                                   Audiometria_I_CIE10 = "?????????????",
                                   Audiometria_I_Obs = "",
                                   ///////////////// LABORATORIO //////////////////////////
                                   Grupo_Sanguineo = Grupo,
                                   Grupo_Sanguineo_ID = 0000000000,
                                   Factor_RH = Factor,
                                   Factor_RH_ID = 00000000000000,
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
                                   Rayos_X_CIE10 = "???????????????",
                                   Rayos_X_Obs = "",
                                   ///////////////////// PSICOLOGIA //////////////////////
                                   Psico_CIE10 = Psicologia,
                                   Psico_Obs = "",
                                   ////////////////////////////////////////////////////////


                                   //IdServicio = a.IdServicio,
                                   //IdTrabajador = a.IdTrabajador,

                                   //NombreCompleto = a.NombreCompleto,
                                   //Dni = a.Dni,
                                   //LugarNacimiento = a.LugarNacimiento,
                                   //FechaNacimiento = a.FechaNacimiento,
                                   //Edad = GetAge(a.FechaNacimiento.Value),
                                   //RangoEdad = GetGrupoEtario(GetAge(a.FechaNacimiento.Value)),
                                   //Sexo = a.Sexo,
                                   //Domicilio = a.Domicilio,
                                   //Ubigueo = a.Ubigueo,
                                   //EstadoCivil = a.EstadoCivil,
                                   //NroHijos = a.NroHijos,
                                   //NivelEstudio = a.NivelEstudio,
                                   //Telefono = a.Telefono,
                                   //EmpresaSede = a.EmpresaSede,
                                   //TipoExamen = a.TipoExamen,
                                   //Grupo = a.Grupo,
                                   //PuestoPostula = a.PuestoPostula,
                                   //Area = a.Area,
                                   //Proveedor = MedicalCenter.v_Name,
                                   //FechaExamen = a.FechaExamen,

                                   //v_Menarquia = a.v_Menarquia,
                                   //d_Fur = a.d_Fur,
                                   //v_CatemenialRegime = a.v_CatemenialRegime,
                                   //d_PAP = a.d_PAP,
                                   //v_FechaUltimaMamo = a.v_FechaUltimaMamo,
                                   //v_Gestapara = a.v_Gestapara,
                                   //v_Mac = a.v_Mac,
                                   //v_CiruGine = a.v_CiruGine,
                                   //v_ResultadosPAP = a.v_ResultadosPAP,
                                   //v_ResultadoMamo = a.v_ResultadoMamo,
                                   //Pa = ValorPAS + " / " + ValorPAD,
                                   //DxNutricional = Nutrcion,
                                   //AnteGinecologicos = a.Sexo == "M" ? "No Aplica" : a.v_Menarquia + " / " + a.d_Fur + " / " + a.v_CatemenialRegime + " / " + a.d_PAP + " / " + a.v_FechaUltimaMamo + " / " + a.v_Gestapara + " / " + a.v_Mac + " / " + a.v_CiruGine + " / " + a.v_ResultadosPAP + " / " + a.v_ResultadoMamo,
                                   //AntePatologicos = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " " : AntecedentesPatologicosConcatenados(Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical),
                                   //AnteFamiliares = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " " : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaAntecedentesFamiliares == null ? " " : AntecedentesFamiliaresConcatenados(Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaAntecedentesFamiliares),

                                   //Alergias = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000633").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //HipertensionArterial = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000436") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000436").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //AnteQuirurgicos = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000637") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000637").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //Gastritis = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000401") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000401").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //DiabetesMellitus = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000642") == null ? " NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000642").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //Tuberculosis = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000540") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000540").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //Cancer = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000638") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000638").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //Convulsiones = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000639") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000639").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //AsmaBronquial = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000599") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_DiseasesId == "N009-DD000000599").i_Answer.ToString() == "1" ? "SI" : "NO",
                                   //Otros = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical == null ? " b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_GroupName == "ENFERMEDADES OTROS") == null ? "NO" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaPersonalMedical.Find(p => p.v_GroupName == "ENFERMEDADES OTROS").i_Answer.ToString() == "1" ? "SI" : "NO",











                                   //DxExaMedicoGeneral = ExaMedGeneral == "" ? "NO APLICA" : ExaMedGeneral,
                                   //DxMusculoEsqueletico = ExaMusculoEsqueletico == "" ? "NO APLICA" : ExaMusculoEsqueletico,



                                   //EvAltura180 = EvaAltura == "" ? "NO APLICA" : EvaAltura,// varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_ESTRUCTURAL_ID && o.IdCampo == Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID).Valor,
                                   //Exa7D = Exa7D == "" ? "NO APLICA" : Exa7D,//eva varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ALTURA_7D_ID && o.IdCampo == Constants.ASCENSO_GRANDES_ALTURAS_APTO_ASCENDER_GRANDES_ALTURAS_ID).ValorName,
                                   //EvaNeurologica = EvaNeurologica == "" ? "NO APLICA" : EvaNeurologica, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EVA_NEUROLOGICA_ID && o.IdCampo == Constants.EVA_NEUROLOGICA_CONCLUSION_ID).Valor,
                                   //TamizajeDermatologico = TamizajeDer == "" ? "NO APLICA" : TamizajeDer, //varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TAMIZAJE_DERMATOLOGIO_ID && o.IdCampo == Constants.TAMIZAJE_DERMATOLOGIO_DESCRIPCION1_ID).Valor,



                                   //DxRadiografiaTorax = RadioTorax == "" ? "NO APLICA" : RadioTorax,
                                   //DxRadiografiaOIT = RadioOIT == "" ? "NO APLICA" : RadioOIT,
                                   //InidceNeumoconiosis = Y == "" ? "NO APLICA" : Y,

                                   //OD_VA_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_125).v_Value1,
                                   //OD_VA_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_250).v_Value1,
                                   //OD_VA_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_500).v_Value1,
                                   //OD_VA_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_1000).v_Value1,
                                   //OD_VA_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_2000).v_Value1,
                                   //OD_VA_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_3000).v_Value1,
                                   //OD_VA_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_4000).v_Value1,
                                   //OD_VA_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_6000).v_Value1,
                                   //OD_VA_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OD_8000).v_Value1,

                                   //OI_VA_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_125).v_Value1,
                                   //OI_VA_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_250).v_Value1,
                                   //OI_VA_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_500).v_Value1,
                                   //OI_VA_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_1000).v_Value1,
                                   //OI_VA_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_2000).v_Value1,
                                   //OI_VA_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_3000).v_Value1,
                                   //OI_VA_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_4000).v_Value1,
                                   //OI_VA_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_6000).v_Value1,
                                   //OI_VA_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VA_OI_8000).v_Value1,


                                   //OD_VO_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_125).v_Value1,
                                   //OD_VO_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_250).v_Value1,
                                   //OD_VO_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_500).v_Value1,
                                   //OD_VO_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_1000).v_Value1,
                                   //OD_VO_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_2000).v_Value1,
                                   //OD_VO_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_3000).v_Value1,
                                   //OD_VO_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_4000).v_Value1,
                                   //OD_VO_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_6000).v_Value1,
                                   //OD_VO_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OD_8000).v_Value1,


                                   //OI_VO_125 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_125) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_125).v_Value1,
                                   //OI_VO_250 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_250) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_250).v_Value1,
                                   //OI_VO_500 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_500).v_Value1,
                                   //OI_VO_1000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_1000).v_Value1,
                                   //OI_VO_2000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_2000).v_Value1,
                                   //OI_VO_3000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_3000).v_Value1,
                                   //OI_VO_4000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_4000).v_Value1,
                                   //OI_VO_6000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_6000).v_Value1,
                                   //OI_VO_8000 = AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000) == null ? "" : AudiometriaValores.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.txt_VO_OI_8000).v_Value1,
                                   //Dxaudiometria = AudiometriaDxs == "" ? "NO APLICA" : AudiometriaDxs,






                                   //DxEspirometria = Espirometria == "" ? "NO APLICA" : Espirometria,

                                   ////---Oftalmología
                                   //UsaLentes = UsaLentesSI + UsaLentesNO,
                                   //VisionCercaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000234").Valor,
                                   //VisionCercaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000230").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor,

                                   //AgudezaVisualLejosOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000233").Valor,
                                   //AgudezaVisualLejosOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000227").Valor,
                                   //VisionCercaCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000231").Valor,
                                   //VisionCercaCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000236").Valor,
                                   //AgudezaVisualLejosCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N002-MF000000235").Valor,
                                   //AgudezaVisualLejosCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == "N009-MF000000646").Valor,

                                   //TestIshihara = IshiharaNormal + IshiharaAnormal,
                                   //Estereopsis = EstereopsisNormal + EstereopsisAnormal,
                                   //DxOftalmología = new ServiceBL().GetDiagnosticByServiceIdAndCategoryId(a.IdServicio, 14),
                                   ////-----------------------


                                   //DxOdontologia = Odontograma == "" ? "NO APLICA" : Odontograma,
                                   //DxElectrocardiograma = Electrocardiograma == "" ? "NO APLICA" : Electrocardiograma,
                                   //PruebaEsfuerzo = PbaEsfuerzo == "" ? "NO APLICA" : PbaEsfuerzo,

                                   //DxPsicologia = Psicologia == "" ? "NO APLICA" : Psicologia,

                                   //GrupoFactor = Grupo + " - " + Factor,
                                   //DxLeucocitos = DxLeucocitos == "" ? "NO APLICA" : DxLeucocitos,
                                   //DxHemoglobina = DxHemoglobina == "" ? "NO APLICA" : DxHemoglobina,
                                   //DxHemograma = DxHemograma == "" ? "NO APLICA" : DxHemograma,
                                   //DxGlucosa = DxGlucosa == "" ? "NO APLICA" : DxGlucosa,
                                   //Colesterol = Colesterol1 + " " + Colesterol2,
                                   //DxColesterol = DxColesterol == "" ? "NO APLICA" : DxColesterol,
                                   //DxHdl = DxHDL == "" ? "NO APLICA" : DxHDL,
                                   //DxLdl = DxLDL == "" ? "NO APLICA" : DxLDL,
                                   //DxVldl = DxVLDL == "" ? "NO APLICA" : DxVLDL,
                                   //Trigliceridos = Trigli1 == "" ? "NO APLICA" : Trigli1,
                                   //DxTgc = DxTGC == "" ? "NO APLICA" : DxTGC,

                                   //Tgo = TGO1 + TGO2,
                                   //Tgp = TGP1 + TGP2,













                                   //AreaCognitiva = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_AREA_COGNITIVA_ID).Valor,
                                   //AreaEmocional = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_emocianal_ID).Valor,
                                   ////AreaPersonal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.EXAMEN_MENTAL_area_personal_ID).Valor,
                                   //AptitudPsicologica = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PSICOLOGIA_ID && o.IdCampo == Constants.PSICOLOGIA_APTITUD_PSICOLOGICA_ID).Valor,

                                   //Leucocitos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor,

                                   //Hemoglobina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor,

                                   //Eosi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor,
                                   //RecuentoPlaquetas = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor,

                                   //Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor,

                                   //Hdl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor,

                                   //Ldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_LDL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_LDL_ID && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_LDL_ID && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor,

                                   //Vldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.COLESTEROL_VLDL_ID && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor,

                                   //Urea = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor,
                                   //Creatina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor,

                                   //NroPiezasCaries = GetCantidadCaries(a.IdServicio, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID),
                                   //NroPiezasAusentes = GetCantidadAusentes(a.IdServicio, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID),

                                   //Cabello = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).Valor,
                                   //Ojos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).Valor,
                                   //Oidos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).Valor,
                                   //Nariz = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).Valor,

                                   //Boca = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).Valor,
                                   //Cuello = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).Valor,

                                   //Torax = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).Valor,
                                   //Cardiovascular = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).Valor,
                                   //Abdomen = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).Valor,

                                   //ApGenitourinario = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).Valor,
                                   //Locomotor = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).Valor,
                                   //Marcha = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).Valor,

                                   //Columna = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).Valor,
                                   //ExtremidadesSuperiores = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).Valor,
                                   //ExtremidadesInferiores = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).Valor,
                                   //SistemaLinfatico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).Valor,
                                   //Neurologico = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).Valor,

                                   //Cabeza7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).Valor,
                                   //Cuello7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CUELLO_DESCRIPCION).Valor,
                                   //Nariz7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_NARIZ_DESCRIPCION).Valor,

                                   //Boca7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_BOCA_ADMIGDALA_FARINGE_LARINGE_DESCRIPCION).Valor,
                                   //ReflejosPupilares7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_PUPILARES_DESCRIPCION).Valor,
                                   //MiembrosSuperiores7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_SUPERIORES_DESCRIPCION).Valor,
                                   //MiembrosInferiores7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MIEMBROS_INFERIORES_DESCRIPCION).Valor,


                                   //ReflejosOsteotendiosos7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_REFLEJOS_OSTEO_TENDINOSOS_DESCRIPCION).Valor,
                                   //Marcha7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_MARCHA_DESCRIPCION).Valor,
                                   //Columna7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_COLUMNA_DESCRIPCION).Valor,
                                   //Abdomen7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ABDOMEN_DESCRIPCION).Valor,

                                   //AnillosIInguinales7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMENFISICO_ANILLOS_INGUINALES_DESCRIPCION).Valor,
                                   //Hernias7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_HERNIAS_DESCRIPCION).Valor,
                                   //Varices7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_VARICES_DESCRIPCION).Valor,
                                   //Genitales7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_ORGANOS_GENITALES_DESCRIPCION).Valor,
                                   //Ganclios7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_GANGLIOS_DESCRIPCION).Valor,
                                   //Pulmones7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_PULMONES_DESCRIPCION).Valor,
                                   //TactoRectal7C = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_7C_ID && o.IdCampo == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_TACTO_RECTAL_DESCRIPCION).Valor,

                                   //Fr = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).Valor,
                                   //Fc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.FUNCIONES_VITALES_ID && o.IdCampo == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).Valor,
                                   //PerAbdominal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).Valor,
                                   //PerCadera = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).Valor,
                                   //Icc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).Valor,
                                   //Peso = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_PESO_ID).Valor,
                                   //Talla = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_TALLA_ID).Valor,
                                   //Imc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ANTROPOMETRIA_ID && o.IdCampo == Constants.ANTROPOMETRIA_IMC_ID).Valor,
                                   //Sintomatologia = a.Sintomatologia,
                                   //PielAnexos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_FISICO_ID && o.IdCampo == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).Valor,

                                   //ActividadFisica = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Frequency,
                                   //ActividadFisicaDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.ActividadFisica).v_Comment,
                                   //ConsumoDrogas = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Frequency,
                                   //ConsumoDrogasDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Drogas).v_Comment,
                                   //ConsumoAlcohol = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Frequency,
                                   //ConsumoAlcoholDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? " a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Alcohol).v_Comment,
                                   //ConsumoTabaco = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Frequency,
                                   //ConsumoTabacoDetalle = Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador) == null ? "a" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos == null ? "b" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco) == null ? "" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Comment == "" ? "---" : Habitos_Personales.Find(p => p.PersonId == a.IdTrabajador).ListaHabitos.Find(p => p.i_TypeHabitsId == (int)TypeHabit.Tabaco).v_Comment,


                                   //Fvc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_CVF).Valor,
                                   //Fev1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1).Valor,
                                   //Fev1_Fvc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_VEF_1_CVF).Valor,
                                   //Fev25_75 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.ESPIROMETRIA_ID && o.IdCampo == Constants.ESPIROMETRIA_FUNCION_RESPIRATORIA_ABS_FEF_25_75).Valor,

                                   //Leuc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor,
                                   //Hemat = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor,

                                   //Marihuana = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).ValorName,
                                   //Cocaina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).ValorName,

                                   //Vdrl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor == "" ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).Valor,

                                   //DxOcu1 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 0),
                                   //DxOcu2 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 1),
                                   //DxOcu3 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 2),
                                   //DxOcu4 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 3),
                                   //DxOcu5 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 4),
                                   //DxOcu6 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 5),
                                   //DxOcu7 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 6),
                                   //DxOcu8 = ObtenerDxOcupacionales(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 7),

                                   //DxMed1 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 0),
                                   //DxMed2 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 1),
                                   //DxMed3 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 2),
                                   //DxMed4 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 3),
                                   //DxMed5 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 4),
                                   //DxMed6 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 5),
                                   //DxMed7 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 6),
                                   //DxMed8 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 7),
                                   //DxMed9 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 8),
                                   //DxMed10 = ObtenerDxMedicos(varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones, 9),

                                   //Reco1 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[18].Descripcion,
                                   //Reco2 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[19].Descripcion,
                                   //Reco3 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[20].Descripcion,
                                   //Reco4 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[21].Descripcion,
                                   //Reco5 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[22].Descripcion,
                                   //Reco6 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[23].Descripcion,
                                   //Reco7 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[24].Descripcion,
                                   //Reco8 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[25].Descripcion,
                                   //Reco9 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[26].Descripcion,
                                   //Reco10 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[27].Descripcion,
                                   //Reco11 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[28].Descripcion,
                                   //Reco12 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[29].Descripcion,
                                   //Reco13 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[30].Descripcion,
                                   //Reco14 = varDx.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones[31].Descripcion,

                                   //AptitudId = a.AptitudId,
                                   //AptitudMedica = a.AptitudMedica,
                                   //MotivoAptitud = a.AptitudId == (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   //ComentarioAptitud = a.AptitudId != (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   //Evaluador = a.Evaluador,
                                   //CMP = a.CMP,
                                   //Restricciones = ConcatenateRestrictionByService(a.IdServicio)
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

        #region Reportes

        public List<OsteomuscularNuevo> ReportOsteoMuscularNuevo(string pstrserviceId, string pstrComponentId)
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


                var sql = (from a in objEntity.ToList()
                           let OsteoMuscular = new ServiceBL().ValoresComponente(pstrserviceId, pstrComponentId)
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
                               CADERA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA).v_Value1Name,
                               FLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.FLEXION).v_Value1Name,
                               HOMBRO_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFLEXION).v_Value1Name,

                               HOMBRO_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQEXTENSION).v_Value1Name,
                               HOMBRO_IZQROTDER = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTDER) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTDER).v_Value1Name,
                               HOMBRO_IZQROTIZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTIZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQROTIZQ).v_Value1Name,
                               HOMBRO_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQFUERZA).v_Value1Name,
                               HOMBRO_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQDOLOR).v_Value1Name,
                               CODO_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCH).v_Value1Name,
                               CODO_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHABDUCC).v_Value1Name,
                               CODO_DCHFELXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFELXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFELXION).v_Value1Name,
                               CODO_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHEXTENSION).v_Value1Name,
                               CODO_DCHROTDER = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTDER) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTDER).v_Value1Name,

                               CODO_DCHROTIZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTIZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHROTIZQ).v_Value1Name,
                               CODO_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHDOLOR).v_Value1Name,
                               CODO_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_DCHFUERZA).v_Value1Name,
                               CODO_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQ).v_Value1Name,
                               CODO_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQABDUCC).v_Value1Name,
                               CODO_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFLEXION).v_Value1Name,
                               CODO_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQEXTENSION).v_Value1Name,
                               CODO_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROTEXT).v_Value1Name,
                               CODO_IZQROT_INT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROT_INT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQROT_INT).v_Value1Name,
                               CODO_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQFUERZA).v_Value1Name,

                               CODO_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CODO_IZQDOLOR).v_Value1Name,
                               MUNIECA_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIECA_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIECA_DCH).v_Value1Name,
                               MUNIEECA_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHABDUCC).v_Value1Name,
                               MUNIEECA_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFLEXION).v_Value1Name,
                               MUNIEECA_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHEXTENSION).v_Value1Name,
                               MUNIEECA_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTEXT).v_Value1Name,
                               MUNIEECA_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHROTINT).v_Value1Name,
                               MUNIEECA_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHFUERZA).v_Value1Name,
                               MUNIEECA_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_DCHDOLOR).v_Value1Name,
                               MUNIEECA_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQ).v_Value1Name,

                               MUNIEECA_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQABDUCC).v_Value1Name,
                               MUNIEECA_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFLEXION).v_Value1Name,
                               MUNIEECA_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQEXTENSION).v_Value1Name,
                               MUNIEECA_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTEXT).v_Value1Name,
                               MUNIEECA_IZQROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQROTINT).v_Value1Name,
                               MUNIEECA_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQFUERZA).v_Value1Name,
                               MUNIEECA_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUNIEECA_IZQDOLOR).v_Value1Name,
                               CADERA_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCH).v_Value1Name,
                               CADERA_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHABDUCC).v_Value1Name,
                               CADERA_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFLEXION).v_Value1Name,

                               CADERA_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHEXTENSION).v_Value1Name,
                               CADERA_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTEXT).v_Value1Name,
                               CADERA_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHROTINT).v_Value1Name,
                               CADERA_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHFUERZA).v_Value1Name,
                               CADERA_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_DCHDOLOR).v_Value1Name,
                               CADERA_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQ).v_Value1Name,
                               CADERA_IZQABDUC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQABDUC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQABDUC).v_Value1Name,
                               CADERA_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFLEXION).v_Value1Name,
                               CADERA_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTEXT).v_Value1Name,
                               CADERA_IZQROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQROTINT).v_Value1Name,


                               CADERA_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQEXTENSION).v_Value1Name,
                               CADERA_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQFUERZA).v_Value1Name,
                               CADERA_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_IZQDOLOR).v_Value1Name,
                               RODILLA_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCH).v_Value1Name,
                               RODILLA_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHABDUCC).v_Value1Name,
                               RODILLA_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFLEXION).v_Value1Name,
                               RODILLA_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHEXTENSION).v_Value1Name,
                               RODILLA_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTEXT).v_Value1Name,
                               RODILLA_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHROTINT).v_Value1Name,
                               RODILLA_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHFUERZA).v_Value1Name,

                               RODILLA_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_DCHDOLOR).v_Value1Name,
                               RODILLA_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQ).v_Value1Name,
                               RODILLA_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQABDUCC).v_Value1Name,
                               RODILLA_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFLEXION).v_Value1Name,
                               RODILLA_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQEXTENSION).v_Value1Name,
                               RODILLA_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQROTEXT).v_Value1Name,
                               RODILLA_IZQINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQINT).v_Value1Name,
                               RODILLA_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQFUERZA).v_Value1Name,
                               RODILLA_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.RODILLA_IZQDOLOR).v_Value1Name,
                               TOBILLO_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCH).v_Value1Name,

                               TOBILLO_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHABDUCC).v_Value1Name,
                               TOBILLO_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFLEXION).v_Value1Name,
                               TOBILLO_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHEXTENSION).v_Value1Name,
                               TOBILLO_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTEXT).v_Value1Name,
                               TOBILLO_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHROTINT).v_Value1Name,
                               TOBILLO_DCHFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHFUERZA).v_Value1Name,
                               TOBILLO_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_DCHDOLOR).v_Value1Name,
                               TOBILLO_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQ).v_Value1Name,
                               TOBILLO_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQABDUCC).v_Value1Name,
                               TOBILLO_IZQFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFLEXION).v_Value1Name,

                               TOBILLO_IZQROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTEXT).v_Value1Name,
                               TOBILLO_IZQEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQEXTENSION).v_Value1Name,
                               TOBILLO_IZQROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQROTINT).v_Value1Name,
                               TOBILLO_IZQFUERZA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFUERZA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQFUERZA).v_Value1Name,
                               TOBILLO_IZQDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOBILLO_IZQDOLOR).v_Value1Name,
                               HOMBRO_IZQABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC).v_Value1Name,
                               HOMBRO_DCHABDUCC = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC).v_Value1Name,
                               HOMBRO_DCH = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH).v_Value1Name,
                               HOMBRO_DCHFLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION).v_Value1Name,
                               HOMBRO_DCHROTEXT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTEXT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTEXT).v_Value1Name,

                               HOMBRO_DCHDOLOR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR).v_Value1Name,
                               HOMBRO_DCHFUERZATONO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO).v_Value1Name,
                               HOMBRO_DCHEXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION).v_Value1Name,
                               HOMBRO_IZQ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ).v_Value1Name,
                               HOMBRO_DCHROTINT = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT).v_Value1Name,
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
                               PIE_PLANO_IZQUIERDO = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_IZQUIERDO) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PIE_PLANO_IZQUIERDO).v_Value1Name,

                               CERVICAL_AP = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_AP) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_AP).v_Value1,
                               DORSAL_AP = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_AP) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_AP).v_Value1,
                               LUMBAR_AP = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_AP) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_AP).v_Value1,
                               DORSAL_LATERAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LATERAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LATERAL).v_Value1,
                               LUMBAR_LATERAL = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_LATERAL) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.LUMBAR_LATERAL).v_Value1,
                               CERVICAL_LATERALIZACION_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_DERECHA).v_Value1Name,
                               CERVICAL_LATERALIZACION_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_LATERALIZACION_IZQUIERDA).v_Value1Name,
                               DORSAL_LUMBAR_LATERAL_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_IZQUIERDA).v_Value1Name,
                               DORSAL_LUMBAR_ROACION_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_DERECHA).v_Value1Name,
                               CERVICAL_EXTENXION__ = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_EXTENXION__) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_EXTENXION__).v_Value1Name,

                               DORSAL_LUMBAR = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR).v_Value1Name,
                               DORSAL_LUMBAR_EXTENSION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_EXTENSION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_EXTENSION).v_Value1Name,
                               CERVICAL_ROTACION_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_DERECHA).v_Value1Name,
                               CERVICAL_ROTACION_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_ROTACION_IZQUIERDA).v_Value1Name,
                               CERVICAL_IRRADIACION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_IRRADIACION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_IRRADIACION).v_Value1Name,
                               DORSAL_LUMBAR_LATERAL_DERECHA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_DERECHA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_LATERAL_DERECHA).v_Value1Name,
                               CERVICAL_FLEXION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_FLEXION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CERVICAL_FLEXION).v_Value1Name,
                               DORSAL_LUMBAR_IRRADIACION = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION).v_Value1Name,
                               DORSAL_LUMBAR_ROACION_IZQUIERDA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA).v_Value1Name,
                               COLUMNA_CERVICAL_CONTRACTURA = OsteoMuscular.Count == 0 || OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL_CONTRACTURA) == null ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL_CONTRACTURA).v_Value1Name,

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
                                 // Empresa / Sede Cliente ******************************************************
                                 join oc in dbContext.organization on new { a = d.v_CustomerOrganizationId }
                                         equals new { a = oc.v_OrganizationId } into oc_join
                                 from oc in oc_join.DefaultIfEmpty()

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
                                 where a.v_ServiceId == pstrServiceId && a.i_IsDeleted == 0
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {
                                     Trabajador = b.v_FirstLastName + " " + b.v_SecondLastName + " " + b.v_FirstName,
                                     d_Birthdate = b.d_Birthdate.Value,
                                     Genero = c.v_Value1,
                                     i_SexTypeId = b.i_SexTypeId,
                                     v_DocNumber = b.v_DocNumber,
                                     v_TelephoneNumber = b.v_TelephoneNumber,
                                     Empresa = oc.v_Name,
                                     Sede = lc.v_Name,
                                     v_CurrentOccupation = b.v_CurrentOccupation,
                                     FechaServicio = a.d_ServiceDate.Value,
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
                                     b_FirmaAuditor = pr.b_SignatureImage
                                 }
                                ).ToList();
                var DatosMedicoMedicinaEvaluador = ObtenerDatosMedicoMedicina(pstrServiceId, Constants.EXAMEN_FISICO_ID, Constants.EXAMEN_FISICO_7C_ID);
                //var DatosMedicoMedicinaAuditor = ObtenerDatosMedicoMedicinaAuditor(pstrServiceId, Constants.EXAMEN_FISICO_ID, Constants.EXAMEN_FISICO_7C_ID);
                
                var result = (from a in objEntity
                              select new Sigesoft.Node.WinClient.BE.PacientList
                              {
                                  Trabajador = a.Trabajador,
                                  d_Birthdate = a.d_Birthdate,
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
                                  b_FirmaAuditor = a.b_FirmaAuditor
                              }
                            ).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private DatosDoctorMedicina ObtenerDatosMedicoMedicina(string pstrServiceId, string p1, string p2)
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

    }
}

