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
    public class PacientBL
    {
        #region Person

        public byte[] getPhoto(string pstrPersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.person
                                 where a.v_PersonId == pstrPersonId
                                 select new
                                    {
                                        Foto = a.b_PersonImage
                                    }
                                 ).FirstOrDefault();

                return objEntity.Foto;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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
                        pobjOperationResult.ErrorMessage = "El número de documento  <font color='red'>" + pobjPerson.v_DocNumber + "</font> ya se encuentra registrado.<br> Por favor ingrese otro número de documento.";
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

        public int AddPersonUsuarioExterno(ref OperationResult pobjOperationResult, personDto pobjPerson, professionalDto pobjProfessional, systemuserDto pobjSystemUser, List<string> ClientSession)
        {
            //mon.IsActive = true;
            int SecuentialId = -1;
            string newId = string.Empty;
            person objEntity1 = null;
            int SistemuserId = 0;
            try
            {
                //#region Validations
                //// Validar el DNI de la persona
                //if (pobjPerson != null)
                //{
                //    OperationResult objOperationResult6 = new OperationResult();
                //    string strfilterExpression1 = string.Format("v_DocNumber==\"{0}\"&&i_Isdeleted==0", pobjPerson.v_DocNumber);
                //    var _recordCount1 = GetPersonCount(ref objOperationResult6, strfilterExpression1);

                //    if (_recordCount1 != 0)
                //    {
                //        pobjOperationResult.ErrorMessage = "El número de documento  <font color='red'>" + pobjPerson.v_DocNumber + "</font> ya se encuentra registrado.<br> Por favor ingrese otro número de documento.";
                //        return -1;
                //    }
                //}

                //// Validar existencia de UserName en la BD
                //if (pobjSystemUser != null)
                //{
                //    OperationResult objOperationResult7 = new OperationResult();
                //    string strfilterExpression2 = string.Format("v_UserName==\"{0}\"&&i_Isdeleted==0", pobjSystemUser.v_UserName);
                //    var _recordCount2 = new SecurityBL().GetSystemUserCount(ref objOperationResult7, strfilterExpression2);

                //    if (_recordCount2 != 0)
                //    {
                //        pobjOperationResult.ErrorMessage = "El nombre de usuario  <font color='red'>" + pobjSystemUser.v_UserName + "</font> ya se encuentra registrado.<br> Por favor ingrese otro nombre de usuario.";
                //        return -1;
                //    }
                //}
                //#endregion

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

                // Grabar Usuario
                if (pobjSystemUser != null)
                {
                    OperationResult objOperationResult3 = new OperationResult();
                    pobjSystemUser.v_PersonId = objEntity1.v_PersonId;
                    SistemuserId = new SecurityBL().AddSystemUSer(ref objOperationResult3, pobjSystemUser, ClientSession);
                }


                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_PersonId, Success.Ok, null);
                return SistemuserId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_PersonId, Success.Failed, ex.Message);
                return -1;
            }


        }


        public void UpdatePerson(ref OperationResult pobjOperationResult, bool pbIsChangeDocNumber, personDto pobjPerson, professionalDto pobjProfessional, bool pbIsChangeUserName, systemuserDto pobjSystemUser, List<string> ClientSession)
        {
            //mon.IsActive = true;
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
                        return;
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
                        return;
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
                    new SecurityBL().UpdateSystemUSer(ref objOperationResult3, pobjSystemUser, ClientSession);
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
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PERSONA", "v_PersonId=" + pobjPerson.v_PersonId, Success.Failed, ex.Message);
                return;
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

        //public void AddPersonOrganization(ref OperationResult pobjOperationResult, int PersonId, int OrganizationId, List<string> ClientSession)
        //{
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        personorganization objEntity = new personorganization();

        //        objEntity.i_PersonId = PersonId;
        //        objEntity.i_OrganizationId = OrganizationId;
        //        objEntity.d_InsertDate = DateTime.Now;
        //        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);

        //        dbContext.AddTopersonorganization(objEntity);
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
        //        //new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.CREACION, "PERSONA", "i_PersonId=" + objEntity1.i_PersonId, Constants.Success.Failed, ex.Message);
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

                //if (objEntity != null)
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
                OperationResult objOperationResult1 = new OperationResult();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.professional
                                       where a.v_PersonId == pobjDtoEntity.v_PersonId
                                       select a).FirstOrDefault();

                if (objEntitySource == null)
                {
                    // Grabar
                    AddProfessional(ref objOperationResult1, pobjDtoEntity, ClientSession);
                }
                else
                {
                    // Crear la entidad con los datos actualizados

                    pobjDtoEntity.d_UpdateDate = DateTime.Now;
                    pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                    professional objProfessionalTyped = professionalAssembler.ToEntity(pobjDtoEntity);

                    // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                    dbContext.professional.ApplyCurrentValues(objProfessionalTyped);

                    // Guardar los cambios
                    dbContext.SaveChanges();
                }              

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

        #region UserExternal

        public Sigesoft.Node.WinClient.BE.PacientList GetPacientReportEPS_Lab(string serviceId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from s in dbContext.service
                                 join pr in dbContext.protocol on s.v_ProtocolId equals pr.v_ProtocolId
                                 join pe in dbContext.person on s.v_PersonId equals pe.v_PersonId

                                 //************************************************************************************

                                 join ow in dbContext.organization on new { a = pr.v_WorkingOrganizationId }
                                     equals new { a = ow.v_OrganizationId } into ow_join
                                 from ow in ow_join.DefaultIfEmpty()

                                 join lw in dbContext.location on new { a = pr.v_WorkingOrganizationId, b = pr.v_WorkingLocationId }
                                     equals new { a = lw.v_OrganizationId, b = lw.v_LocationId } into lw_join
                                 from lw in lw_join.DefaultIfEmpty()

                                 join D in dbContext.systemparameter on new { a = pe.i_SexTypeId.Value, b = 100 }
                                     equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                                 from D in D_join.DefaultIfEmpty()

                                 where s.v_ServiceId == serviceId
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {
                                     v_FirstName = pe.v_FirstName,
                                     v_FirstLastName = pe.v_FirstLastName,
                                     v_SecondLastName = pe.v_SecondLastName,
                                     v_FullWorkingOrganizationName = ow.v_Name + " / " + lw.v_Name,
                                     v_CurrentOccupation = pe.v_CurrentOccupation,
                                     v_SexTypeName = D.v_Value1,
                                     d_ServiceDate = s.d_ServiceDate,
                                     d_Birthdate = pe.d_Birthdate,
                                     Trabajador = pe.v_FirstLastName + "_" + pe.v_SecondLastName + "_" + pe.v_FirstName
                                 }).ToList();
                objEntity[0].i_Age = GetAge(objEntity[0].d_Birthdate.Value);
                return objEntity.FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Sigesoft.Node.WinClient.BE.PacientList GetPacientReportEPS_Photo(string serviceId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from s in dbContext.service
                                 join pe in dbContext.person on s.v_PersonId equals pe.v_PersonId

                                 where s.v_ServiceId == serviceId
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {
                                     b_Photo = pe.b_PersonImage,
                                 }).ToList();

                return objEntity.FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Sigesoft.Node.WinClient.BE.PacientList GetPacientReportEPS_Oftalmo(string serviceId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //PacientList objDtoEntity = null;

                var objEntity = (from s in dbContext.service
                                 join pr in dbContext.protocol on s.v_ProtocolId equals pr.v_ProtocolId
                                 join pe in dbContext.person on s.v_PersonId equals pe.v_PersonId

                                 //************************************************************************************
                                 join B in dbContext.protocol on s.v_ProtocolId equals B.v_ProtocolId into B_join
                                 from B in B_join.DefaultIfEmpty()
                                 join C1 in dbContext.organization on B.v_EmployerOrganizationId equals C1.v_OrganizationId into C1_join
                                 from C1 in C1_join.DefaultIfEmpty()
                                 join C2 in dbContext.organization on B.v_CustomerOrganizationId equals C2.v_OrganizationId into C2_join
                                 from C2 in C2_join.DefaultIfEmpty()
                                 join C3 in dbContext.organization on B.v_WorkingOrganizationId equals C3.v_OrganizationId into C3_join
                                 from C3 in C3_join.DefaultIfEmpty()

                                 where s.v_ServiceId == serviceId
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {

                                     i_EsoTypeId = pr.i_EsoTypeId,
                                     empresa_ = C2.v_Name,
                                     contrata = C1.v_Name,
                                     subcontrata = C3.v_Name,
                                     FirmaTrabajador = pe.b_RubricImage,
                                     HuellaTrabajador = pe.b_FingerPrintImage,
                                 }).ToList();

                return objEntity.FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Sigesoft.Node.WinClient.BE.PacientList GetPacientReportEPS(string serviceId)
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


                                 //************************************************************************************
                                 join D in dbContext.systemparameter on new { a = pe.i_SexTypeId.Value, b = 100 }  
                                     equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                                 from D in D_join.DefaultIfEmpty()


                                 where s.v_ServiceId == serviceId
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {
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
                                     Trabajador=pe.v_FirstLastName + "_" + pe.v_SecondLastName +"_"+pe.v_FirstName
                                 });

                // Medico Examen fisico

                //var medico = (from sc in dbContext.servicecomponent
                //              join J1 in dbContext.systemuser on new { i_InsertUserId = sc.i_InsertUserId.Value }
                //                          equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                //              from J1 in J1_join.DefaultIfEmpty()
                //              join pe in dbContext.person on J1.v_PersonId equals pe.v_PersonId
                //              where (sc.v_ServiceId == serviceId) &&
                //                      (sc.v_ComponentId == Constants.EXAMEN_FISICO_ID)
                //              select pe.v_FirstName + " " + pe.v_FirstLastName).SingleOrDefault<string>();

                var sql = (from a in objEntity.ToList()
                           select new Sigesoft.Node.WinClient.BE.PacientList
                           {
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
                               Trabajador = a.Trabajador,
                               FirmaTrabajador = a.FirmaTrabajador,
                               HuellaTrabajador = a.HuellaTrabajador,
                               v_BloodGroupName = a.v_BloodGroupName,
                               v_BloodFactorName = a.v_BloodFactorName

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

        public List<Sigesoft.Node.WinClient.BE.ServiceList> GetMusculoEsqueletico(string pstrserviceId, string pstrComponentId)
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

                                 join F in dbContext.systemuser on E.i_InsertUserId equals F.i_SystemUserId
                                 join G in dbContext.professional on F.v_PersonId equals G.v_PersonId

                                 where A.v_ServiceId == pstrserviceId
                                 select new Sigesoft.Node.WinClient.BE.ServiceList
                                 {
                                     v_PersonId = A.v_PersonId,
                                     v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                     d_ServiceDate = A.d_ServiceDate,
                                     EmpresaTrabajo = D.v_Name,
                                     v_ServiceId = A.v_ServiceId,
                                     v_ComponentId = E.v_ServiceComponentId

                                 });

                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter();

                var sql = (from a in objEntity.ToList()

                           let OsteoMuscular = new ServiceBL().ValoresComponente(pstrserviceId, Constants.OSTEO_MUSCULAR_ID_1)
                           select new Sigesoft.Node.WinClient.BE.ServiceList
                           {
                               v_PersonId = a.v_PersonId,
                               v_Pacient = a.v_Pacient,
                               d_ServiceDate = a.d_ServiceDate,
                               EmpresaTrabajo = a.EmpresaTrabajo,
                               v_ServiceId = a.v_ServiceId,
                               v_ComponentId = a.v_ComponentId,

                               AbdomenExcelente = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_EXCELENTE).v_Value1,
                               AbdomenPromedio = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_PROMEDIO).v_Value1,
                               AbdomenRegular = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_REGULAR).v_Value1,
                               AbdomenPobre = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_POBRE).v_Value1,
                               AbdomenPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.PUNTOS_ABDOMEN).v_Value1,
                               AbdomenObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_OBSERVACIONES).v_Value1,
                               CaderaExcelente = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_EXCELENTE).v_Value1,
                               CaderaPromedio = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PROMEDIO).v_Value1,
                               CaderaRegular = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_REGULAR).v_Value1,
                               CaderaPobre = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_POBRE).v_Value1,
                               CaderaPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADERA_PUNTOS).v_Value1,
                               CaderaObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.CADRA_OBSERVACIONES).v_Value1,
                               MusloExcelente = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_EXCELENTE).v_Value1,
                               MusloPromedio = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants._MUSLO_PROMEDIO).v_Value1,
                               MusloRegular = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_REGULAR).v_Value1,
                               MusloPobre = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_POBRE).v_Value1,
                               MusloPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_PUNTOS).v_Value1,
                               MusloObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.MUSLO_OBSERVACIONES).v_Value1,
                               AbdomenLateralExcelente = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_EXCELENTE).v_Value1,
                               AbdomenLateralPromedio = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PROMEDIO).v_Value1,
                               AbdomenLateralRegular = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_REGULAR).v_Value1,
                               AbdomenLateralPobre = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_POBRE).v_Value1,
                               AbdomenLateralPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_PUNTOS).v_Value1,
                               AbdomenLateralObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.ABDOMEN_LAT_OBSERVACIONES).v_Value1,
                               AbduccionHombroNormalOptimo = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR).v_Value1,
                               AbduccionHombroNormalLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_NORMAL_LIMITADO_ID).v_Value1,
                               AbduccionHombroNormalMuyLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL_CONTRACTURA).v_Value1,
                               AbduccionHombroNormalPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_LUMBAR_CONTACTURA).v_Value1,
                               AbduccionHombroNormalObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_DORSAL).v_Value1Name,
                               AbduccionHombroOptimo = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_OPTIMO_ID).v_Value1,
                               AbduccionHombroLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_IRRADIACION).v_Value1,
                               AbduccionHombroMuyLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ADUCCION_HOMBRO_MUY_LIMITADO_ID).v_Value1,
                               AbduccionHombroPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.COLUMNA_CERVICAL).v_Value1,
                               AbduccionHombroObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DORSAL_LUMBAR_ROACION_IZQUIERDA).v_Value1Name,
                               RotacionExternaOptimo = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHABDUCC).v_Value1,
                               RotacionExternaLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCH).v_Value1,
                               RotacionExternaMuyLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ROTACION_EXTERNA_MUY_LIMITADO_ID).v_Value1,
                               RotacionExternaPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFLEXION).v_Value1,
                               RotacionExternaObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.OSTEO_MUSCULAR_ROTACION_EXTERNA_DOLOR_ID).v_Value1Name,
                               RotacionExternaHombroInternoOptimo = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHDOLOR).v_Value1,
                               RotacionExternaHombroInternoLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHFUERZATONO).v_Value1,
                               RotacionExternaHombroInternoMuyLimitado = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHEXTENSION).v_Value1,
                               RotacionExternaHombroInternoPuntos = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQ).v_Value1,
                               RotacionExternaHombroInternoObs = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_DCHROTINT).v_Value1Name,
                               Total1 = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.TOTAL).v_Value1,
                               Total2 = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.HOMBRO_IZQABDUCC).v_Value1,
                               AptitudMusculoEsqueletico = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.APTITUD).v_Value1,
                               Conclusiones = OsteoMuscular.Count == 0 ? string.Empty : OsteoMuscular.Find(p => p.v_ComponentFieldId == Constants.DESCRIPCION).v_Value1,

                               b_Logo = MedicalCenter.b_Image,
                               EmpresaPropietaria = MedicalCenter.v_Name,
                               EmpresaPropietariaDireccion = MedicalCenter.v_Address,
                               EmpresaPropietariaTelefono = MedicalCenter.v_PhoneNumber,
                               EmpresaPropietariaEmail = MedicalCenter.v_Mail,
                           }).ToList();

                return sql;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.ReportOftalmologia> GetOftalmologia(string pstrserviceId, string pstrComponentId)
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

                                 select new Sigesoft.Node.WinClient.BE.ReportOftalmologia
                                 {
                                     v_ComponentId = E.v_ComponentId,
                                     v_ServiceId = A.v_ServiceId,
                                     NombrePaciente = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                     EmprresaTrabajadora = D.v_Name,
                                     FechaServicio = A.d_ServiceDate.Value,
                                     FechaNacimiento = B.d_Birthdate.Value,
                                     PuestoTrabajo = B.v_CurrentOccupation,
                                     FirmaDoctor = pme.b_SignatureImage,
                                     FirmaTecnologo = prtec.b_SignatureImage,
                                     NombreTecnologo = petec.v_FirstLastName + " " + petec.v_SecondLastName + " " + petec.v_FirstName,
                                     //v_ServiceComponentId = E.v_ServiceComponentId
                                 });

                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter();


                var sql = (from a in objEntity.ToList()

                           let oftalmo = serviceBL.ValoresComponente(pstrserviceId, Constants.OFTALMOLOGIA_ID)
                           let FondoOjo = serviceBL.ValoresComponente(pstrserviceId, Constants.FONDO_OJO_ID)
                           let TestColores = serviceBL.ValoresComponente(pstrserviceId, Constants.TEST_ISHIHARA_ID)
                           let TestEsterepsis = serviceBL.ValoresComponente(pstrserviceId, Constants.TEST_ESTEREOPSIS_ID)
                           let Campimetria = serviceBL.ValoresComponente(pstrserviceId, Constants.CAMPIMETRIA_ID)
                           let Tonometria = serviceBL.ValoresComponente(pstrserviceId, Constants.TONOMETRIA_ID)



                           select new Sigesoft.Node.WinClient.BE.ReportOftalmologia
                           {
                               v_ServiceId = a.v_ServiceId,
                               NombrePaciente = a.NombrePaciente,
                               EmprresaTrabajadora = a.EmprresaTrabajadora,
                               FechaServicio = a.FechaServicio,
                               FechaNacimiento = a.FechaNacimiento,
                               Edad = GetAge(a.FechaNacimiento),
                               PuestoTrabajo = a.PuestoTrabajo,
                               USO_DE_CORRECTORES = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000172") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000172").v_Value1,
                               
                               SI = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000224") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000224").v_Value1,

                               NO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000719") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000719").v_Value1,

                               ULTIMAREFRACCION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000225") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000225").v_Value1,

                               DIABETES = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000176") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000176").v_Value1,

                               HIPERTENSION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000175") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000175").v_Value1,

                               SUSTQUIMICAS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000180") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000180").v_Value1,

                               EXPRADIACION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000182") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000182").v_Value1,

                               MIOPIA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000709") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000709").v_Value1,

                               CIRUGIAOCULAR = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000181") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000181").v_Value1,

                               TRAUMAOCULAR = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000178") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000178").v_Value1,

                               GLAUCOMA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000177") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000177").v_Value1,

                               ASTIGMATISMO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000179") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000179").v_Value1,

                               OTROSESPECIFICAR = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000710") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000710").v_Value1,

                               SINPATOLOGIAS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002092") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002092").v_Value1,

                               OTRASPATOLOGIA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002091") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002091").v_Value1,

                               PTOSISPALPEBRAL = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002084") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002084").v_Value1,

                               CONJUNTIVITIS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002085") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002085").v_Value1,

                               PTERIGIUM = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002086") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002086").v_Value1,

                               ESTRABISMO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002087") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002087").v_Value1,

                               TRANSCORNEA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002088") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002088").v_Value1,

                               CATARATAS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002089") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002089").v_Value1,

                               CHALAZION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002090") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002090").v_Value1,

                               ODSCLEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000637") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000637").v_Value1,

                               OI_SC_LEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000638") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000638").v_Value1,

                               OD_CC_LEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000639") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000639").v_Value1,

                               OI_CC_LEJOS = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000647") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000647").v_Value1,

                               OD_AE_LEJOS2 = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002078") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002078").v_Value1,

                               OI_AE_LEJOS2 = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002079") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002079").v_Value1,

                               SC_LEJOSOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000234") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000234").v_Value1,

                               SCLEJOSOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000230") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000230").v_Value1,

                               CCLEJOSOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000231") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000231").v_Value1,

                               CCLEJOSOJ_IZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000236") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000236").v_Value1,

                               AELEJOSOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002080") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002080").v_Value1,

                               AELEJOSOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002081") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002081").v_Value1,

                               SCCERCAOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000233") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000233").v_Value1,

                               S_CCERCAOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000227") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000227").v_Value1,

                               CCCERCAOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000235") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000235").v_Value1,

                               CCCERCAOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000646") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000646").v_Value1,

                               AECERCAOJODERECHO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002082") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002082").v_Value1,

                               AECERCAOJOIZQUIERDO = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002083") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002083").v_Value1,

                               NORMAL = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000711") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000711").v_Value1,

                               ANORMAL = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000712") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000000712").v_Value1,

                               DESCRIPCION = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000261") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N002-MF000000261").v_Value1,

                               EMETROPE = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002071") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002071").v_Value1,

                               PRESBICIACORREGIDA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002073") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002073").v_Value1,

                               AMETROPIACORREGIDA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002072") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002072").v_Value1,

                               PRESBICIANOCORREGIDA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002074") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002074").v_Value1,

                               AMETROPIANOCORREGIA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002075") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002075").v_Value1,

                               AMBLIOPIA = oftalmo.Count == 0 || oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002076") == null ? string.Empty : oftalmo.Find(p => p.v_ComponentFieldId == "N009-MF000002076").v_Value1,

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



                               NORMAL2 = TestColores.Count == 0 || TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000717") == null ? string.Empty : TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000717").v_Value1,

                               ANORMAL2 = TestColores.Count == 0 || TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000718") == null ? string.Empty : TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000718").v_Value1,

                               DESCRIPCION2 = TestColores.Count == 0 || TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000522") == null ? string.Empty : TestColores.Find(p => p.v_ComponentFieldId == "N009-MF000000522").v_Value1Name,




                               NORMAL3 = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000343") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000343").v_Value1,

                               ANORMAL3 = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000342") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000342").v_Value1,

                               ENCANDILAMIENTO = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000226") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000226").v_Value1,

                               TIEMPO = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000258") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N002-MF000000258").v_Value1,

                               RECUPERACION = TestEsterepsis.Count == 0 || TestEsterepsis.Find(p => p.v_ComponentFieldId == "N009-MF000002093") == null ? string.Empty : TestEsterepsis.Find(p => p.v_ComponentFieldId == "N009-MF000002093").v_Value1,




                               CAMPIMETRIAOD = Campimetria.Count == 0 || Campimetria.Find(p => p.v_ComponentFieldId == "N009-MF000002094") == null ? string.Empty : Campimetria.Find(p => p.v_ComponentFieldId == "N009-MF000002094").v_Value1,

                               CAMPIMETRIAOI = Campimetria.Count == 0 || Campimetria.Find(p => p.v_ComponentFieldId == "N009-MF000002095") == null ? string.Empty : Campimetria.Find(p => p.v_ComponentFieldId == "N009-MF000002095").v_Value1,


                               TONOMETRIAOD = Tonometria.Count == 0 || Tonometria.Find(p => p.v_ComponentFieldId == "N009-MF000002096") == null ? string.Empty : Tonometria.Find(p => p.v_ComponentFieldId == "N009-MF000002096").v_Value1,

                               TONOMETRIAOI = Tonometria.Count == 0 || Tonometria.Find(p => p.v_ComponentFieldId == "N009-MF000002097") == null ? string.Empty : Tonometria.Find(p => p.v_ComponentFieldId == "N009-MF000002097").v_Value1,

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
                               Recomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(14, pstrserviceId)

                           }).ToList();

                return sql;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.ReporteOftalmologiaAntiguo> GetReportOftalmologiaAntiguo(string pstrserviceId, string pstrComponentId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId

                                 join E in dbContext.servicecomponent on new { a = A.v_ServiceId, b = pstrComponentId }
                                                                      equals new { a = E.v_ServiceId, b = E.v_ComponentId }

                                 join J in dbContext.systemparameter on new { a = B.i_SexTypeId.Value, b = 100 }
                                                                        equals new { a = J.i_ParameterId, b = J.i_GroupId } into J_join // GENERO
                                 from J in J_join.DefaultIfEmpty()

                                 join E1 in dbContext.protocol on A.v_ProtocolId equals E1.v_ProtocolId

                                 join oc in dbContext.organization on new { a = E1.v_CustomerOrganizationId }
                                   equals new { a = oc.v_OrganizationId } into oc_join
                                 from oc in oc_join.DefaultIfEmpty()


                                 // Usuario Tecnologo *************************************
                                 join tec in dbContext.systemuser on E.i_UpdateUserTechnicalDataRegisterId equals tec.i_SystemUserId into tec_join
                                 from tec in tec_join.DefaultIfEmpty()

                                 join ptec in dbContext.professional on tec.v_PersonId equals ptec.v_PersonId into ptec_join
                                 from ptec in ptec_join.DefaultIfEmpty()
                                 // *******************************************************  

                                 // Usuario Medico Evaluador / Medico Aprobador ****************************
                                 join me in dbContext.systemuser on E.i_ApprovedUpdateUserId equals me.i_SystemUserId into me_join
                                 from me in me_join.DefaultIfEmpty()

                                 join pme in dbContext.professional on me.v_PersonId equals pme.v_PersonId into pme_join
                                 from pme in pme_join.DefaultIfEmpty()

                                 join Z in dbContext.person on me.v_PersonId equals Z.v_PersonId


                                 where A.v_ServiceId == pstrserviceId
                                 select new Sigesoft.Node.WinClient.BE.ReporteOftalmologiaAntiguo
                                 {

                                     Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                                     FechaNacimiento = B.d_Birthdate,
                                     DNI = B.v_DocNumber,
                                     v_ServiceId = A.v_ServiceId,
                                     FirmaMedico = pme.b_SignatureImage,
                                     EmpresaCliente = oc.v_Name,
                                     PuestoTrabajo = B.v_CurrentOccupation,
                                     FechaServicio = A.d_ServiceDate,
                                     FirmaTecnico = ptec.b_SignatureImage,
                                     LogoEmpresaCliente = oc.b_Image
                                 });

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                var sql = (from a in objEntity.ToList()
                           let EvaEnu = new ServiceBL().ValoresComponente(pstrserviceId, Constants.OFTALMOLOGIA_ID)

                           select new Sigesoft.Node.WinClient.BE.ReporteOftalmologiaAntiguo
                           {
                               Paciente = a.Paciente,
                               DNI = a.DNI,
                               v_ServiceId = a.v_ServiceId,
                               EmpresaCliente = a.EmpresaCliente,
                               Edad = GetAge(a.FechaNacimiento.Value),
                               FechaServicio = a.FechaServicio,
                               PuestoTrabajo = a.PuestoTrabajo,
                               USO_DE_CORRECTORES_SI = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000224") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000224").v_Value1,

                               USO_DE_CORRECTORES_NO = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000719") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000719").v_Value1,

                               ULTIMA_REFRACCION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000225") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000225").v_Value1,

                               ANTECEDENTE_DIABETES = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000176") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000176").v_Value1,

                               ANTECEDENTE_HIPERTENSION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000175") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000175").v_Value1,

                               ANTECEDENTE_SUST_QUIMICAS = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000180") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000180").v_Value1,

                               ANTECEDENTE_EXP_A_RADIACION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000182") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000182").v_Value1,

                               ANTECEDENTE_MIOPIA = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000709") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000709").v_Value1,

                               ANTECEDENTE_CIRUGIA_OCULAR = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000181") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000181").v_Value1,

                               ANTECEDENTE_TRAUMA_OCULAR = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000178") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000178").v_Value1,

                               ANTECEDENTE_GLAUCOMA = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000177") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000177").v_Value1,

                               ANTECEDENTE_ASTIGMATISMO = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000179") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000179").v_Value1,

                               ANTECEDENTE_OTROS = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000710") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000710").v_Value1,

                               PATOLOGIA_SIN_PATOLOGIAS = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002092") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002092").v_Value1,

                               PATOLOGIA_PTOSIS_PALPEBRAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002084") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002084").v_Value1,

                               PATOLOGIA_CONJUNTIVITIS = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002085") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002085").v_Value1,

                               PATOLOGIA_PTERIGIUM = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002086") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002086").v_Value1,

                               PATOLOGIA_ESTRABISMO = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002087") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002087").v_Value1,

                               PATOLOGIA_TRANS_DE_CORNEA = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002088") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002088").v_Value1,

                               PATOLOGIA_CATARATAS = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002089") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002089").v_Value1,

                               PATOLOGIA_CHALAZION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002090") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002090").v_Value1,

                               PATOLOGIA_OTRAS = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002091") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002091").v_Value1,

                               S_C_LEJOS_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000234") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000234").v_Value1,

                               S_C_LEJOS_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000230") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000230").v_Value1,

                               C_C_LEJOS_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000231") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000231").v_Value1,

                               C_C_LEJOS_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000236") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000236").v_Value1,

                               A_E_LEJOS_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002080") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002080").v_Value1,

                               A_E_LEJOS_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002081") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002081").v_Value1,

                               S_C_CERCA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000233") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000233").v_Value1,

                               S_C_CERCA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000227") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000227").v_Value1,

                               C_C_CERCA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000235") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000235").v_Value1,

                               C_C_CERCA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000646") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000646").v_Value1,

                               A_E_CERCA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002082") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002082").v_Value1,

                               A_E_CERCA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002083") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002083").v_Value1,

                               FONDO_OJO_MACULOPATIA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000251") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000251").v_Value1,

                               FONDO_OJO_MACULOPATIA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000252") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000252").v_Value1,

                               FONDO_OJO_NEURITIS_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000254") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000254").v_Value1,

                               FONDO_OJO_NEURITIS_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000255") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000255").v_Value1,

                               FONDO_OJO_RETINOPATIA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000524") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000524").v_Value1,

                               FONDO_OJO_RETINOPATIA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000525") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000525").v_Value1,

                               FONDO_OJO_ANGIOPATIA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000530") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000530").v_Value1,

                               FONDO_OJO_ANGIOPATIA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000531") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000531").v_Value1,

                               FONDO_OJO_ATROFIA_DE_N_O = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000532") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000532").v_Value1,

                               FONDO_OJO_ATROFIA_DE_NO_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000533") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000533").v_Value1,

                               FONDO_OJO_ATROFIA_DE_NO_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000534") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000534").v_Value1,

                               TEST_ISHIHARA_NORMAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000717") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000717").v_Value1,

                               TEST_ISHIHARA_ANORMAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000718") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000718").v_Value1,

                               TEST_ISHIHARA_DESCRIPCION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000522") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000522").v_Value1Name,

                               TEST_ESTEREOPSIS_NORMAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000343") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000343").v_Value1,

                               TEST_ESTEREOPSIS_ANORMAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000342") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000342").v_Value1,

                               TEST_ESTEREOPSIS_ENCANDILAMIENTO = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000226") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000226").v_Value1,

                               TEST_ESTEREOPSIS_TIEMPO = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000258") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000258").v_Value1,

                               TEST_ESTEREOPSIS_RECUPERACION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002093") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002093").v_Value1,

                               CAMPIMETRIA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002094") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002094").v_Value1,

                               CAMPIMETRIA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002095") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002095").v_Value1,

                               TONOMETRIA_O_D = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002096") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002096").v_Value1,

                               TONOMETRIA_O_I = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002097") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000002097").v_Value1,

                               REFLEJOS_PUPILARES_NORMAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000711") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000711").v_Value1,

                               REFLEJOS_PUPILARES_ANORMAL = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000712") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N009-MF000000712").v_Value1,

                               REFLEJOS_PUPILARES_DESCRIPCION = EvaEnu.Count == 0 || EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000261") == null ? string.Empty : EvaEnu.Find(p => p.v_ComponentFieldId == "N002-MF000000261").v_Value1,

                               LogoEmpresa = MedicalCenter.b_Image,
                               Dx = new ServiceBL().GetDiagnosticByServiceIdAndCategoryId(pstrserviceId, 14),
                               Recomendaciones = new ServiceBL().ConcatenateRecomendacionesByCategoria(14, pstrserviceId),
                               FirmaTecnico = a.FirmaTecnico,
                               FirmaMedico = a.FirmaMedico,
                               LogoEmpresaCliente = a.LogoEmpresaCliente

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
        public List<Sigesoft.Node.WinClient.BE.ServiceList> GetFichaPsicologicaOcupacional(string pstrserviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                                 join D in dbContext.organization on C.v_WorkingOrganizationId equals D.v_OrganizationId
                                 join E in dbContext.servicecomponent on new { a = A.v_ServiceId, b = "N002-ME000000033" }
                                                                        equals new { a = E.v_ServiceId, b = E.v_ComponentId }

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
                                 select new Sigesoft.Node.WinClient.BE.ServiceList
                                 {
                                     v_PersonId = A.v_PersonId,
                                     v_Pacient = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
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
                           select new Sigesoft.Node.WinClient.BE.ServiceList
                           {
                               v_ServiceId = a.v_ServiceId,
                               v_ServiceComponentId = a.v_ServiceComponentId,
                               v_PersonId = a.v_PersonId,
                               v_Pacient = a.v_Pacient,
                               i_Edad = GetAge(a.d_BirthDate.Value),
                               v_BirthPlace = a.v_BirthPlace,
                               i_DiaN = a.i_DiaN,
                               i_MesN = a.i_MesN,
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
        public string GetServiceComponentFielValue(string pstrServiceId, string pstrComponentId, string pstrFieldId, string Type, int pintParameter, string pstrConX)
        {
            try
            {
                ServiceBL oServiceBL = new ServiceBL();
                List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> oServiceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                string xx = "";
                if (Type == "NOCOMBO")
                {
                    oServiceComponentFieldValuesList = oServiceBL.ValoresComponente(pstrServiceId, pstrComponentId);
                    xx = oServiceComponentFieldValuesList.Count() == 0 ? string.Empty : ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;
                }
                else
                {
                    oServiceComponentFieldValuesList = oServiceBL.ValoresExamenComponete(pstrServiceId, pstrComponentId, pintParameter);
                    if (pstrConX == "SI")
                    {
                        xx = oServiceComponentFieldValuesList.Count() == 0 ? string.Empty : ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;
                    }
                    else
                    {
                        xx = oServiceComponentFieldValuesList.Count() == 0 ? string.Empty : ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1Name;
                    }

                }

                return xx;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.ReportAlturaEstructural> GetAlturaEstructural(string pstrserviceId, string pstrComponentId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                                 join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
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
                                 select new Sigesoft.Node.WinClient.BE.ReportAlturaEstructural
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
                                 });


                var serviceBL = new ServiceBL();
                var MedicalCenter = serviceBL.GetInfoMedicalCenter();

                var funcionesVitales = serviceBL.ReportFuncionesVitales(pstrserviceId, Constants.FUNCIONES_VITALES_ID);
                var antropometria = serviceBL.ReportAntropometria(pstrserviceId, Constants.ANTROPOMETRIA_ID);

                var sql = (from a in objEntity.ToList()
                           select new Sigesoft.Node.WinClient.BE.ReportAlturaEstructural
                           {
                               EmpresaCliente = a.EmpresaCliente,
                               v_ComponentId = a.v_ComponentId,
                               v_ServiceId = a.v_ServiceId,
                               ServicioId = a.ServicioId,
                               NombrePaciente = a.NombrePaciente,
                               EmpresaTrabajadora = a.EmpresaTrabajadora,
                               Fecha = a.Fecha,
                               FechaNacimiento = a.FechaNacimiento,
                               Edad = GetAge(a.FechaNacimiento),
                               PuestoTrabajo = a.PuestoTrabajo,

                               AntecedenteTecSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_SI_ID, "NOCOMBO", 0, "SI"),
                               AntecedenteTecNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_NO_ID, "NOCOMBO", 0, "SI"),
                               AntecedenteTecObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ANTECEDENTE_TEC_OBS_ID, "NOCOMBO", 0, "SI"),

                               ConvulsionesSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_SI_ID, "NOCOMBO", 0, "SI"),
                               ConvulsionesNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_NO_ID, "NOCOMBO", 0, "SI"),
                               ConvulsionesObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CONVULSIONES_EPILEPSIA_OBS_ID, "NOCOMBO", 0, "SI"),

                               MareosSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_MAREOS_SI_ID, "NOCOMBO", 0, "SI"),
                               MareosNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_MAREOS_NO_ID, "NOCOMBO", 0, "SI"),
                               MareosObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_MAREOS_OBS_ID, "NOCOMBO", 0, "SI"),

                               AgorafobiaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_SI_ID, "NOCOMBO", 0, "SI"),
                               AgorafobiaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_NO_ID, "NOCOMBO", 0, "SI"),
                               AgorafobiaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_AGORAFOBIA_OBS_ID, "NOCOMBO", 0, "SI"),

                               AcrofobiaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_SI_ID, "NOCOMBO", 0, "SI"),
                               AcrofobiaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_NO_ID, "NOCOMBO", 0, "SI"),
                               AcrofobiaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ACROFOBIA_OBS_ID, "NOCOMBO", 0, "SI"),

                               InsuficienciaCardiacaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_SI_ID, "NOCOMBO", 0, "SI"),
                               InsuficienciaCardiacaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_NO_ID, "NOCOMBO", 0, "SI"),
                               InsuficienciaCardiacaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_INSUFICIENCIA_CARDIACA_OBS_ID, "NOCOMBO", 0, "SI"),

                               EstereopsiaSI = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_SI_ID, "NOCOMBO", 0, "SI"),
                               EstereopsiaNO = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_NO_ID, "NOCOMBO", 0, "SI"),
                               EstereopsiaObs = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ESTEREOPSIA_OBS_ID, "NOCOMBO", 0, "SI"),

                               NistagmusEspontaneo = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_ESPONTANEO_ID, "NOCOMBO", 0, "SI"),
                               NistagmusProvocado = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_NISTAGMUS_PROVOCADO_ID, "NOCOMBO", 0, "SI"),

                               PrimerosAuxilios = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_PRIMEROS_AUXILIOS_ID, "NOCOMBO", 0, "SI"),
                               TrabajoNivelMar = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_TRABAJO_SOBRE_NIVEL_ID, "NOCOMBO", 0, "SI"),

                               Timpanos = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_TIMPANOS_ID, "NOCOMBO", 0, "SI"),
                               Equilibrio = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_EQUILIBRIO_ID, "NOCOMBO", 0, "SI"),
                               SustentacionPie20Seg = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_SUST_PIE_20_ID, "NOCOMBO", 0, "SI"),
                               CaminarLibre3Mts = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_RECTA_3_ID, "NOCOMBO", 0, "SI"),
                               CaminarLibreVendado3Mts = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_3_ID, "NOCOMBO", 0, "SI"),
                               CaminarLibreVendadoPuntaTalon3Mts = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_CAMINAR_LIBRE_OJOS_VENDADOS_PUNTA_TALON_3_ID, "NOCOMBO", 0, "SI"),
                               Rotar = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ROTAR_SILLA_GIRATORIA_ID, "NOCOMBO", 0, "SI"),
                               AdiadocoquinesiaDirecta = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_DIRECTA_ID, "NOCOMBO", 0, "SI"),
                               AdiadocoquinesiaCruzada = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ADIADOCOQUINESIA_CRUZADA_ID, "NOCOMBO", 0, "SI"),
                               Apto = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_APTO_ID, "NOCOMBO", 0, "SI"),
                               descripcion = GetServiceComponentFielValue(a.ServicioId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_DESCRIPCION_ID, "NOCOMBO", 0, "SI"),
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
                               NombreUsuarioGraba = a.NombreUsuarioGraba
                           }).ToList();

                return sql;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<FileInfoDto> GetMultimediaFileByPersonId(ref OperationResult pobjOperationResult, string psrPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.multimediafile
                                 where a.v_PersonId == psrPersonId
                                 && a.i_IsDeleted == 0
                                 select new FileInfoDto
                                 {
                                     MultimediaFileId = a.v_MultimediaFileId,
                                     FileName = a.v_FileName
                                     //ByteArrayFile = a.b_File
                                 }).ToList();

                pobjOperationResult.Success = 1;
                return objEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public FileInfoDto GetMultimediaFileById(ref OperationResult pobjOperationResult, string pstrMultimediaFileId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.multimediafile
                                 where a.v_MultimediaFileId == pstrMultimediaFileId
                                 && a.i_IsDeleted == 0
                                 select new FileInfoDto
                                 {
                                     MultimediaFileId = a.v_MultimediaFileId,
                                     FileName = a.v_FileName,
                                     ByteArrayFile = a.b_File
                                 }).FirstOrDefault();

                pobjOperationResult.Success = 1;
                return objEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
        #endregion

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

        public blacklistpersonDto GetBlackList(ref OperationResult pobjOperationResult, string pstrPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                blacklistpersonDto objDtoEntity = null;

                var objEntity = (from a in dbContext.blacklistperson
                                 where a.v_PersonId == pstrPersonId && a.i_IsDeleted == 0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = blacklistpersonAssembler.ToDTO(objEntity);

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
                noxioushabitsDto.v_Frequency = "NO FUMADOR";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 1;
                _noxioushabitsDto.Add(noxioushabitsDto);

                noxioushabitsDto = new noxioushabitsDto();
                noxioushabitsDto.v_Frequency = "NUNCA";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 2;
                _noxioushabitsDto.Add(noxioushabitsDto);

                noxioushabitsDto = new noxioushabitsDto();
                noxioushabitsDto.v_Frequency = "NUNCA";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 3;
                _noxioushabitsDto.Add(noxioushabitsDto);


                noxioushabitsDto = new noxioushabitsDto();
                noxioushabitsDto.v_Frequency = "ACTIVO";
                noxioushabitsDto.v_Comment = "";
                noxioushabitsDto.v_PersonId = NewId;
                noxioushabitsDto.i_TypeHabitsId = 4;
                _noxioushabitsDto.Add(noxioushabitsDto);


                objHistoryBL.AddNoxiousHabits(ref pobjOperationResult, _noxioushabitsDto, null, null, ClientSession);

                #endregion

                #region Creación de Médicos Familiares
                List<familymedicalantecedentsDto> _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                //Padre
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 65;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);

                //Madre
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 52;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                //Hermanos
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 39;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                //Esposos
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 26;
                familymedicalantecedentsDto.v_Comment = "";
                _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);

                //Hijos
                familymedicalantecedentsDto = new familymedicalantecedentsDto();
                familymedicalantecedentsDto.v_PersonId = NewId;
                familymedicalantecedentsDto.v_DiseasesId = "N009-DD000000649";
                familymedicalantecedentsDto.i_TypeFamilyId = 79;
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

        public string UpdatePacient(ref OperationResult pobjOperationResult, personDto pobjDtoEntity, List<string> ClientSession, string NumbreDocument, string _NumberDocument)
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
                    resultado = UpdatePerson_(ref pobjOperationResult, false, pobjDtoEntity, null, false, null, ClientSession);
                }
                else
                {
                    resultado = UpdatePerson_(ref pobjOperationResult, true, pobjDtoEntity, null, false, null, ClientSession);
                }



                if (resultado == "-1")
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

        public string UpdatePacient(ref OperationResult pobjOperationResult, List<personDto> pobjDtoEntity, List<string> ClientSession)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    foreach (var personDto in pobjDtoEntity.AsParallel())
                    {
                        personDto.i_IsDeleted = 0;
                        personDto.d_UpdateDate = DateTime.Now;
                        personDto.i_UpdateUserId = int.Parse(ClientSession[2]);
                        var objEntitySource = (dbContext.pacient.FirstOrDefault(a => a.v_PersonId.Equals(personDto.v_PersonId)));
                        if (objEntitySource != null)
                        {
                            var resultado = UpdatePerson_(ref pobjOperationResult, !personDto.v_DocNumber.Equals(objEntitySource.person.v_DocNumber), personDto, null, false, null, ClientSession);

                            if (resultado == "-1")
                            {
                                pobjOperationResult.Success = 0;
                                return resultado;
                            }

                            objEntitySource.d_UpdateDate = DateTime.Now;
                            objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                            dbContext.pacient.ApplyCurrentValues(objEntitySource);
                        }
                    }

                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                return "1";

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PACIENTE", "v_PacientId=" + pobjDtoEntity.v_PersonId, Success.Failed, ex.Message);
                return "-1";
            }
        }

        public string UpdatePerson_(ref OperationResult pobjOperationResult, bool pbIsChangeDocNumber, personDto pobjPerson, professionalDto pobjProfessional, bool pbIsChangeUserName, systemuserDto pobjSystemUser, List<string> ClientSession)
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
                pobjPerson.v_FirstName = objEntitySource.v_FirstName;
                pobjPerson.v_FirstLastName = objEntitySource.v_FirstLastName;
                pobjPerson.v_SecondLastName = objEntitySource.v_SecondLastName;
                pobjPerson.i_DocTypeId = objEntitySource.i_DocTypeId;
                pobjPerson.d_Birthdate = objEntitySource.d_Birthdate.Value;
                pobjPerson.i_SexTypeId = objEntitySource.i_SexTypeId;
                pobjPerson.v_CurrentOccupation = objEntitySource.v_CurrentOccupation;
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
               
                //Crear Instancia del Servicio y de Componente del Servicio
                objServiceDto.v_ProtocolId = pobjDtoEntity.v_ProtocolId;
                objServiceDto.v_PersonId = pstrPacientId;
                objServiceDto.i_MasterServiceId = pstrMasterServiceId;
                //Se setea el estado del servicio en "Por iniciar"
                objServiceDto.i_ServiceStatusId = (int)Common.ServiceStatus.Iniciado;
                //Se setea el estado de la aptitud en "Sin Aptitud" en caso sea un Eso
                objServiceDto.i_AptitudeStatusId = (int)Common.AptitudeStatus.SinAptitud;
                objServiceDto.d_ServiceDate = DateTime.Now;
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

                //objEntity.i_LineStatusId = (int)Common.LineStatus.EnCircuito;
                //objEntity.i_CalendarStatusId = (int)Common.CalendarStatus.Atendido;
                //objEntity.d_CircuitStartDate = DateTime.Now;
                objEntity.v_ServiceId = ServiceId;
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                //objEntity.d_EntryTimeCM = DateTime.Now;

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

        public personDto ValidarPersonaWeb(ref OperationResult pobjOperationResult, string pstrUsuario, string pstrPassword)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                personDto objDtoEntity = null;
                //Buscar si la persona existe
                var objEntity = (from a in dbContext.person
                                 where a.v_Password == pstrPassword
                                 select a).FirstOrDefault();
                //Buscar si es un usuario del sistema
                if (objEntity != null)
                {
                    var objEntity1 = (from a in dbContext.systemuser
                                      where a.v_PersonId == objEntity.v_PersonId
                                      select a).FirstOrDefault();
                    if (objEntity1 == null)
                    {
                        if (objEntity != null)
                            objDtoEntity = personAssembler.ToDTO(objEntity);

                        pobjOperationResult.Success = 1;
                        return objDtoEntity;
                    }
                    else
                    {
                        pobjOperationResult.Success = 1;
                        return null;
                    }
                }
                else
                {
                    pobjOperationResult.Success = 1;
                    return null;
                }


            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public void ActualizarContraseniaPaciente(ref OperationResult pobjOperationResult, string pPersonId, string NuevaContrasenia)
        {
            try
            {

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.person
                                       where a.v_PersonId == pPersonId
                                       select a).FirstOrDefault();


                objEntitySource.v_Password = NuevaContrasenia;

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                return;

            }
            catch (Exception ex)
            {

                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return;
            }
        }
        
        public int GetPacientes(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.pacient select a;

                if (!string.IsNullOrEmpty(filterExpression))
                    query = query.Where(filterExpression);

                int intResult = query.Count();

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

        public List<Sigesoft.Node.WinClient.BE.PacientList> GetPacientsPagedAndFiltered(ref OperationResult pobjOperationResult, int pintPageIndex, int pintResultsPerPage, string pstrFirstLastNameorDocNumber)
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
                             select new Sigesoft.Node.WinClient.BE.PacientList
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
                             select new Sigesoft.Node.WinClient.BE.PacientList
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

                List<Sigesoft.Node.WinClient.BE.PacientList> objData = query.ToList();
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

        public Sigesoft.Node.WinClient.BE.PacientList GetPacient(ref OperationResult pobjOperationResult, string pstrPacientId, string pstNroDocument)
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
                                 select new Sigesoft.Node.WinClient.BE.PacientList
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
                                     i_NroHermanos = B.i_NroHermanos.Value
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

        public List<Sigesoft.Node.WinClient.BE.ReportConsentimiento> GetReportConsentimiento(string pstrServiceId)
        {
            //mon.IsActive = true;
            var groupUbigeo = 113;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from A in dbContext.service
                                 join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId into B_join
                                 from B in B_join.DefaultIfEmpty()

                                 join C in dbContext.organization on B.v_WorkingOrganizationId equals C.v_OrganizationId into C_join
                                 from C in C_join.DefaultIfEmpty()

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

                                 select new Sigesoft.Node.WinClient.BE.ReportConsentimiento
                                 {
                                     NombreTrabajador = P1.v_FirstName + " " + P1.v_FirstLastName + " " + P1.v_SecondLastName,
                                     NroDocumento = P1.v_DocNumber,
                                     Ocupacion = P1.v_CurrentOccupation,
                                     Empresa = C.v_Name,
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
                           select new Sigesoft.Node.WinClient.BE.ReportConsentimiento
                           {
                               Fecha = DateTime.Now.ToShortDateString(),
                               Logo = MedicalCenter.b_Image,
                               NombreTrabajador = a.NombreTrabajador,
                               NroDocumento = a.NroDocumento,
                               Ocupacion = a.Ocupacion,
                               Empresa = a.Empresa,
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

        public Sigesoft.Node.WinClient.BE.PacientList DevolverDatosPaciente_Oftalmo(string pstrServiceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 join b in dbContext.person on a.v_PersonId equals b.v_PersonId
                                 where a.v_ServiceId == pstrServiceId && a.i_IsDeleted == 0
                                 select new Sigesoft.Node.WinClient.BE.PacientList
                                 {
                                     Trabajador = b.v_FirstLastName + " " + b.v_SecondLastName + " " + b.v_FirstName,
                                     d_Birthdate = b.d_Birthdate.Value,
                                     FechaServicio = a.d_ServiceDate.Value,
                                 }
                                ).ToList();
                objEntity[0].Edad = GetAge(objEntity[0].d_Birthdate.Value);

                return objEntity.FirstOrDefault();
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
                                  Genero = a.Genero,
                                  i_SexTypeId = a.i_SexTypeId,
                                  v_DocNumber = a.v_DocNumber,
                                  v_TelephoneNumber = a.v_TelephoneNumber,
                                  Empresa = a.Empresa,
                                  Sede = a.Sede,
                                  v_CurrentOccupation = a.v_CurrentOccupation,

                                  Trabajador = a.Trabajador,
                                  d_Birthdate = a.d_Birthdate,
                                  Edad = GetAge(a.d_Birthdate.Value),
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
                                  b_FirmaEvaluador = DatosMedicoMedicinaEvaluador == null ? null : DatosMedicoMedicinaEvaluador.FirmaMedicoMedicina,
                                  //b_FirmaAuditor = DatosMedicoMedicinaAuditor == null ? null : DatosMedicoMedicinaAuditor.FirmaMedicoMedicina
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

        private Sigesoft.Node.WinClient.BE.DatosDoctorMedicina ObtenerDatosMedicoMedicina(string pstrServiceId, string p1, string p2)
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
                             select new Sigesoft.Node.WinClient.BE.DatosDoctorMedicina
                             {
                                 FirmaMedicoMedicina = pme.b_SignatureImage,
                                 ApellidosDoctor = a.v_FirstLastName + " " + a.v_SecondLastName,
                                 DireccionDoctor = a.v_AdressLocation,
                                 NombreDoctor = a.v_FirstName,
                                 CMP = pme.v_ProfessionalCode,

                             }).FirstOrDefault();

            return objEntity;
        }

        private Sigesoft.Node.WinClient.BE.DatosDoctorMedicina ObtenerDatosMedicoMedicinaAuditor(string pstrServiceId, string p1, string p2)
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
                             select new Sigesoft.Node.WinClient.BE.DatosDoctorMedicina
                             {
                                 FirmaMedicoMedicina = pme.b_SignatureImage,
                                 ApellidosDoctor = a.v_FirstLastName + " " + a.v_SecondLastName,
                                 DireccionDoctor = a.v_AdressLocation,
                                 NombreDoctor = a.v_FirstName,
                                 CMP = pme.v_ProfessionalCode,

                             }).FirstOrDefault();

            return objEntity;
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
                                 v_DiseasesName = ddd.v_Name + "/" + ddd.v_CIE10Id,

                             }).ToList();


                return string.Join(", ", query.Select(p => p.v_DiseasesName));
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.OsteomuscularNuevo> ReportOsteoMuscularNuevo(string pstrserviceId, string pstrComponentId, string idComponentReport)
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

                                 select new Sigesoft.Node.WinClient.BE.OsteomuscularNuevo
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
                var OsteoMuscular = new ServiceBL().ValoresComponentesUserControl(pstrserviceId, idComponentReport);

                var xxx = OsteoMuscular.Find(p => p.v_ComponentFieldId == "N009-MF000000838");
                var sql = (from a in objEntity.ToList()

                           select new Sigesoft.Node.WinClient.BE.OsteomuscularNuevo
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

        public List<Sigesoft.Node.WinClient.BE.ReportAlturaEstructural> GetAlturaEstructural(string pstrserviceId, string pstrComponentId, string idComponentReport)
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
                                 select new Sigesoft.Node.WinClient.BE.ReportAlturaEstructural
                                 {
                                     EmpresaCliente = C2.v_Name, //general
                                     EmpresaTrabajadora = C1.v_Name, //contrata
                                     EmpresaPropietariaDireccion = CC.v_Name, //subcontrata
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
                           select new Sigesoft.Node.WinClient.BE.ReportAlturaEstructural
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

        public Sigesoft.Node.WinClient.BE.PacientList GetPacientReportEPSFirmaMedicoOcupacional(string serviceId)
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
                                 select new Sigesoft.Node.WinClient.BE.PacientList
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

                           select new Sigesoft.Node.WinClient.BE.PacientList
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
    }
}
