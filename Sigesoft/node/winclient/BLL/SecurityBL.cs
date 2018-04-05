using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Security.Cryptography;
using System.Net;
using System.Net.NetworkInformation;

namespace Sigesoft.Node.WinClient.BLL
{
    public class SecurityBL
    {
        #region SystemUSer

        #region Authentication
        /// <summary>
        /// Valida si el susuario esta registrado en el sistema
        /// </summary>
        /// <param name="pobjOperationResult"></param>
        /// <param name="pstrUser"></param>
        /// <param name="pstrPassword"></param>
        /// <param name="pintNode"></param>
        /// <returns></returns>
        public SystemUserList ValidateSystemUser(ref OperationResult pobjOperationResult, int pintNodeId, string pstrUser, string pstrPassword)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                SystemUserList objSystemUserDto = null;
                objSystemUserDto = (from a in dbContext.systemuser
                                    join b in dbContext.professional on a.v_PersonId equals b.v_PersonId
                                    where a.v_UserName == pstrUser
                                    && a.v_Password == pstrPassword
                                    select new SystemUserList
                                    {
                                        i_SystemUserId = a.i_SystemUserId,
                                        v_UserName = a.v_UserName,
                                        v_PersonId = a.v_PersonId,
                                        i_RolVentaId = a.i_RolVentaId,
                                        i_ProfesionId = b.i_ProfessionId
                                    }).FirstOrDefault();

                if (objSystemUserDto == null)
                {
                    pobjOperationResult.AdditionalInformation = "El password es incorrecto, el usuario no existe, o el usuario está desahibilitado";
                    pobjOperationResult.ReturnValue = "NOTEXIST";

                    // Llenar entidad Log
                    LogBL.SaveLog(pintNodeId.ToString(), null, null, LogEventType.ACCESOSSISTEMA,
                    null, string.Format("Descripción={0}", pstrUser),
                    Success.Failed, pobjOperationResult.AdditionalInformation);
                }
                else
                {
                    pobjOperationResult.ReturnValue = "EXIST";
                    // Llenar entidad Log
                    //LogBL.SaveLog(pintNodeId.ToString(), null, objSystemUserDto.i_SystemUserId.ToString(), LogEventType.ACCESOSSISTEMA,
                    //null, string.Format("Id={0} / Descripción={1}", objSystemUserDto.i_SystemUserId.ToString(), objSystemUserDto.v_UserName),
                    //Success.Ok, null);
                }

                pobjOperationResult.Success = 1;
                return objSystemUserDto;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                //LogBL.SaveLog(pintNodeId.ToString(), null, null, LogEventType.ACCESOSSISTEMA,
                //null, string.Format("Descripción={0}", pstrUser),
                //Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }

        }
        #endregion

        #region Authorization

        /// <summary>
        /// Retorna información de su profile de un usuario por nodo y empresa para el armado de su menú
        /// </summary>
        /// <param name="pobjOperationResult"></param>
        /// <param name="pintNodeId"></param>
        /// <param name="pintOrganizationId"></param>
        /// <returns></returns>
        //public List<AutorizationList> GetAuthorization(ref OperationResult pobjOperationResult, int pintNodeId, int pintSystemUserId)
        //{
        //    //mon.IsActive = true;
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

        //        var query = (from rnp in dbContext.rolenodeprofile
        //                     join rn in dbContext.rolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
        //                                             equals new { a = rn.i_NodeId, b = rn.i_RoleId } into rn_join
        //                     from rnj in rn_join.DefaultIfEmpty()

        //                     join surn in dbContext.systemuserrolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
        //                                            equals new { a = surn.i_NodeId, b = surn.i_RoleId } into surn_join
        //                     from surnj in surn_join.DefaultIfEmpty()

        //                     join ah in dbContext.applicationhierarchy on rnp.i_ApplicationHierarchyId equals ah.i_ApplicationHierarchyId
        //                     where surnj.i_NodeId == pintNodeId &&
        //                           surnj.i_SystemUserId == pintSystemUserId &&
        //                           ah.i_ApplicationHierarchyTypeId != 3 &&
        //                           surnj.i_IsDeleted == 0
        //                     select new AutorizationList
        //                     {
        //                         I_ApplicationHierarchyId = rnp.i_ApplicationHierarchyId,
        //                         I_ApplicationHierarchyTypeId = ah.i_ApplicationHierarchyTypeId,
        //                         V_Description = ah.v_Description,
        //                         I_ParentId = ah.i_ParentId,
        //                         V_Form = ah.v_Form == null ? string.Empty : ah.v_Form
        //                     }
        //                     ).Concat(from a in dbContext.systemusergobalprofile
        //                              join b in dbContext.applicationhierarchy on a.i_ApplicationHierarchyId equals b.i_ApplicationHierarchyId
        //                              where a.i_SystemUserId == pintSystemUserId &&
        //                                    b.i_ApplicationHierarchyTypeId != 3 &&
        //                                    b.i_IsDeleted == 0
        //                              select new AutorizationList
        //                              {
        //                                  I_ApplicationHierarchyId = a.i_ApplicationHierarchyId,
        //                                  I_ApplicationHierarchyTypeId = b.i_ApplicationHierarchyTypeId,
        //                                  V_Description = b.v_Description,
        //                                  I_ParentId = b.i_ParentId,
        //                                  V_Form = b.v_Form == null ? string.Empty : b.v_Form
        //                              });

        //        List<AutorizationList> objAutorizationList = query.AsEnumerable()
        //                                                    .OrderBy(p => p.I_ApplicationHierarchyId)
        //                                                    .GroupBy(x => x.I_ApplicationHierarchyId)
        //                                                    .Select(group => group.First())
        //                                                    .ToList();

        //        pobjOperationResult.Success = 1;
        //        return objAutorizationList;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
        //        return null;
        //    }

        //}
        
        public List<AutorizationList> GetAuthorization(ref OperationResult pobjOperationResult, int pintNodeId, int pintSystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from rnp in dbContext.rolenodeprofile
                             join rn in dbContext.rolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
                                                     equals new { a = rn.i_NodeId, b = rn.i_RoleId } into rn_join
                             from rnj in rn_join.DefaultIfEmpty()

                             join surn in dbContext.systemuserrolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
                                                    equals new { a = surn.i_NodeId, b = surn.i_RoleId } into surn_join
                             from surnj in surn_join.DefaultIfEmpty()

                             join ah in dbContext.applicationhierarchy on rnp.i_ApplicationHierarchyId equals ah.i_ApplicationHierarchyId

                             join fff in dbContext.systemparameter on new { a = surnj.i_RoleId, b = 115 } // ROLES DEL SISTEMA
                                                                   equals new { a = fff.i_ParameterId, b = fff.i_GroupId } into J5_join
                             from fff in J5_join.DefaultIfEmpty()
                             
                             where (surnj.i_NodeId == pintNodeId) &&
                                   (surnj.i_SystemUserId == pintSystemUserId) &&
                                   (ah.i_ApplicationHierarchyTypeId == 2 || ah.i_ApplicationHierarchyTypeId == 1) &&
                                   (surnj.i_IsDeleted == 0) && (rnp.i_IsDeleted == 0) &&                                                                   
                                   (ah.i_TypeFormId == (int)TypeForm.Windows) && (ah.i_IsDeleted == 0)
                             select new AutorizationList
                             {
                                 I_ApplicationHierarchyId = rnp.i_ApplicationHierarchyId,
                                 I_ApplicationHierarchyTypeId = ah.i_ApplicationHierarchyTypeId,
                                 V_Description = ah.v_Description,
                                 I_ParentId = ah.i_ParentId,
                                 V_Form = ah.v_Form == null ? string.Empty : ah.v_Form,
                                 V_RoleName = fff.v_Value1,
                                 i_RoleId = fff.i_ParameterId
                             }
                             ).Concat(from a in dbContext.systemusergobalprofile
                                      join b in dbContext.applicationhierarchy on a.i_ApplicationHierarchyId equals b.i_ApplicationHierarchyId                                       
                                      where (a.i_SystemUserId == pintSystemUserId) &&
                                            (b.i_ApplicationHierarchyTypeId == 1 || b.i_ApplicationHierarchyTypeId == 2) &&
                                            (b.i_IsDeleted == 0) && (a.i_IsDeleted == 0) &&                                      
                                            (b.i_TypeFormId == (int)TypeForm.Windows)
                                      select new AutorizationList
                                      {
                                          I_ApplicationHierarchyId = a.i_ApplicationHierarchyId,
                                          I_ApplicationHierarchyTypeId = b.i_ApplicationHierarchyTypeId,
                                          V_Description = b.v_Description,
                                          I_ParentId = b.i_ParentId,
                                          V_Form = b.v_Form == null ? string.Empty : b.v_Form,
                                          V_RoleName = "",
                                          i_RoleId = null
                                      });

                List<AutorizationList> objAutorizationList = query.AsEnumerable()
                                                            .OrderBy(p => p.I_ApplicationHierarchyId)
                                                            .GroupBy(x => x.I_ApplicationHierarchyId)
                                                            .Select(group => group.First())
                                                            .ToList();

                pobjOperationResult.Success = 1;
                return objAutorizationList;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }

        }

        #endregion

        #region FormActions

        public List<KeyValueDTO> GetFormAction(ref OperationResult pobjOperationResult, int pintNodeId, int pintRoleId, int pintSystemUserId, string pstrFormCode)
        {
            //var dd = "frmEso_ANADX_ADDDX".Substring(0, "frmEso_ANADX_ADDDX".IndexOf('_'));

            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                //var query1 = (from rnp in dbContext.rolenodeprofile
                //             join rn in dbContext.rolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
                //                                     equals new { a = rn.i_NodeId, b = rn.i_RoleId } into rn_join
                //             from rnj in rn_join.DefaultIfEmpty()

                //             join surn in dbContext.systemuserrolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
                //                                    equals new { a = surn.i_NodeId, b = surn.i_RoleId } into surn_join
                //             from surnj in surn_join.DefaultIfEmpty()

                //             join ah in dbContext.applicationhierarchy on rnp.i_ApplicationHierarchyId equals ah.i_ApplicationHierarchyId
                //             where (surnj.i_NodeId == pintNodeId) &&
                //                   (surnj.i_RoleId == pintRoleId) &&
                //                   (surnj.i_SystemUserId == pintSystemUserId) &&
                //                   (ah.i_ApplicationHierarchyTypeId == 3) &&   // solo acciones                                 
                //                   (ah.v_Code.Contains(pstrFormCode)) &&
                //                   //(ah.v_Code.Substring(0 - 1, (int)ah.v_Code.ToUpper().IndexOf("_".ToUpper()) + 1) == pstrFormCode) &&
                //                   (rnp.i_IsDeleted == 0)
                //             select new KeyValueDTO
                //             {
                //                 Value1 = ah.v_Description,
                //                 Value2 = ah.v_Code
                //             }).ToList();


                var query = (from rnp in dbContext.rolenodeprofile
                             join rn in dbContext.rolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
                                                     equals new { a = rn.i_NodeId, b = rn.i_RoleId } into rn_join
                             from rnj in rn_join.DefaultIfEmpty()

                             join surn in dbContext.systemuserrolenode on new { a = rnp.i_NodeId, b = rnp.i_RoleId }
                                                    equals new { a = surn.i_NodeId, b = surn.i_RoleId } into surn_join
                             from surnj in surn_join.DefaultIfEmpty()

                             join ah in dbContext.applicationhierarchy on rnp.i_ApplicationHierarchyId equals ah.i_ApplicationHierarchyId
                             where (surnj.i_NodeId == pintNodeId) &&
                                   (surnj.i_RoleId == pintRoleId) &&
                                   (surnj.i_SystemUserId == pintSystemUserId) &&
                                   (ah.i_ApplicationHierarchyTypeId == 3) &&   // solo acciones                                 
                                   (ah.v_Code.Contains(pstrFormCode)) &&
                                   //(ah.v_Code.Substring(0 - 1, (int)ah.v_Code.ToUpper().IndexOf("_".ToUpper()) + 1) == pstrFormCode) &&
                                   (rnp.i_IsDeleted == 0)
                             select new KeyValueDTO
                             {
                                 Value1 = ah.v_Description,
                                 Value2 = ah.v_Code
                             }
                             ).Concat(from a in dbContext.systemusergobalprofile
                                      join b in dbContext.applicationhierarchy on a.i_ApplicationHierarchyId equals b.i_ApplicationHierarchyId
                                      where (a.i_SystemUserId == pintSystemUserId) &&
                                            (b.i_ApplicationHierarchyTypeId == 3) &&  // solo acciones
                                            (b.v_Code.Contains(pstrFormCode)) &&
                                            //(b.v_Code.Substring(0 - 1, (int)b.v_Code.ToUpper().IndexOf("_".ToUpper()) + 1) == pstrFormCode) &&
                                            (a.i_IsDeleted == 0)
                                      select new KeyValueDTO
                                      {
                                          Value1 = b.v_Description,
                                          Value2 = b.v_Code
                                      });

                List<KeyValueDTO> objFormAction = query.OrderBy(P => P.Value1).ToList();
                pobjOperationResult.Success = 1;
                return objFormAction;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
        
        #endregion

        #region Crud
        public List<SystemUserList> GetSystemUserPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from su1 in dbContext.systemuser
                             where su1.i_IsDeleted == 0
                             join su2 in dbContext.systemuser on new { i_InsertUserId = su1.i_InsertUserId.Value }
                                                           equals new { i_InsertUserId = su2.i_SystemUserId } into su2_join
                             from su2 in su2_join.DefaultIfEmpty()

                             join su3 in dbContext.systemuser on new { i_UpdateUserId = su1.i_UpdateUserId.Value }
                                                           equals new { i_UpdateUserId = su3.i_SystemUserId } into su3_join
                             from su3 in su3_join.DefaultIfEmpty()

                             select new SystemUserList
                             {
                                 i_SystemUserId = su1.i_SystemUserId,
                                 v_PersonId = su1.v_PersonId,
                                 v_UserName = su1.v_UserName,
                                 v_Password = su1.v_Password,
                                 v_SecretQuestion = su1.v_SecretQuestion,
                                 v_SecretAnswer = su1.v_SecretAnswer,
                                 i_IsDeleted = su1.i_IsDeleted,
                                 i_InsertUserId = su1.i_InsertUserId,
                                 d_InsertDate = su1.d_InsertDate,
                                 i_UpdateUserId = su1.i_UpdateUserId,
                                 d_UpdateDate = su1.d_UpdateDate,
                                 v_InsertUser = su2.v_UserName,
                                 v_UpdateUser = su3.v_UserName

                             }
                            );



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

                List<SystemUserList> objData = query.ToList();
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

        public int GetSystemUserCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.systemuser select a;

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

        public systemuserDto GetSystemUser(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                systemuserDto objDtoEntity = null;

                var objEntity = (from a in dbContext.systemuser
                                 where a.i_SystemUserId == pintSystemUserId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = systemuserAssembler.ToDTO(objEntity);

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

        public int AddSystemUSer(ref OperationResult pobjOperationResult, systemuserDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            int SecuentialId = 0;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //systemuser objEntity = systemuserAssembler.ToEntity(pobjDtoEntity);
                systemuser objEntity = systemuserAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                SecuentialId = Utils.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 9);
                objEntity.i_SystemUserId = SecuentialId;

                dbContext.AddTosystemuser(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                   "USUARIO", string.Format("Id={0} / Descripción={1}", SecuentialId, objEntity.v_UserName), Success.Ok, null);
                return SecuentialId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                  "USUARIO", string.Format("Id={0}", SecuentialId), Success.Failed, pobjOperationResult.ExceptionMessage);
                return 0;
            }

        }

        public void UpdateSystemUSer(ref OperationResult pobjOperationResult, systemuserDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.systemuser
                                       where a.i_SystemUserId == pobjDtoEntity.i_SystemUserId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                systemuser objEntity = systemuserAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.systemuser.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                  "USUARIO", string.Format("Id={0} / Descripción={1}", pobjDtoEntity.i_SystemUserId, pobjDtoEntity.v_UserName), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                 "USUARIO", string.Format("Id={0}", pobjDtoEntity.i_SystemUserId), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteSystemUSer(ref OperationResult pobjOperationResult, int pintSystemUSerId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            systemuser objEntitySource = null;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                objEntitySource = (from a in dbContext.systemuser
                                   where a.i_SystemUserId == pintSystemUSerId
                                   select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = int.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                 "USUARIO", string.Format("Id={0} / Descripción={1}", pintSystemUSerId, objEntitySource.v_UserName), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
               "USUARIO", string.Format("Id={0}", pintSystemUSerId), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public SystemUserList GetSystemUserAndProfesional(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                SystemUserList objDtoEntity = null;

                var objEntity = (from a in dbContext.systemuser
                                 join b in dbContext.professional on a.v_PersonId equals b.v_PersonId
                                 join c in dbContext.person on a.v_PersonId equals c.v_PersonId
                                 join K in dbContext.systemparameter on new { a = b.i_ProfessionId.Value, b = 115 } equals new { a = K.i_ParameterId, b = K.i_GroupId } into K_join
                                 from K in K_join.DefaultIfEmpty()
                                 where a.i_SystemUserId == pintSystemUserId
                                 select new SystemUserList
                                 {
                                     v_PersonName = c.v_FirstName + " " + c.v_FirstLastName + " " + c.v_SecondLastName,
                                     Profesion = K.v_Value1
                                 }

                                ).FirstOrDefault();

              

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

        #region Utils

        public static string Encrypt(string pData)
        {
            UnicodeEncoding parser = new UnicodeEncoding();
            byte[] _original = parser.GetBytes(pData);
            MD5CryptoServiceProvider Hash = new MD5CryptoServiceProvider();
            byte[] _encrypt = Hash.ComputeHash(_original);
            return Convert.ToBase64String(_encrypt);
        }

        #endregion

        #endregion

        #region SystemUserGlobalProfile

        private void DeleteSystemUserGlobalProfiles(ref OperationResult pobjOperationResult, List<systemusergobalprofileDto> ListSystemUserGlobalProfileDto, List<string> ClientSession)
        {

            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in ListSystemUserGlobalProfileDto)
                {
                    // Obtener la entidad a eliminar
                    var objEntitySource = (from a in dbContext.systemusergobalprofile
                                           where a.i_ApplicationHierarchyId == item.i_ApplicationHierarchyId &&
                                                 a.i_SystemUserId == item.i_SystemUserId
                                           select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    objEntitySource.d_UpdateDate = DateTime.Now;
                    objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySource.i_IsDeleted = 1;
                }

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }

        }

        public void AddSystemUserGlobalProfiles(ref OperationResult pobjOperationResult, List<systemusergobalprofileDto> ListSystemUserGlobalProfileDto, int pintUserPersonId, List<string> ClientSession, bool pbRegisterLog)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in ListSystemUserGlobalProfileDto)
                {

                    var objEntitySource = (from a in dbContext.systemusergobalprofile
                                           where a.i_SystemUserId == item.i_SystemUserId &&
                                                   a.i_ApplicationHierarchyId == item.i_ApplicationHierarchyId
                                           select a).FirstOrDefault();

                    if (objEntitySource != null)
                    {
                        if (objEntitySource.i_IsDeleted == 1)  // Registro macado como eliminado
                        {
                            // dar de alta el registro existente
                            objEntitySource.d_UpdateDate = DateTime.Now;
                            objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                            objEntitySource.i_IsDeleted = 0;
                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        // Grabar como nuevo
                        systemusergobalprofile objEntity = systemusergobalprofileAssembler.ToEntity(item);

                        objEntity.d_InsertDate = DateTime.Now;
                        objEntity.i_InsertUserId = pintUserPersonId;
                        objEntity.i_IsDeleted = 0;
                        dbContext.AddTosystemusergobalprofile(objEntity);
                        dbContext.SaveChanges();
                    }
                }


                pobjOperationResult.Success = 1;
                if (pbRegisterLog == true)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                       "SystemUserGlobalProfile", string.Format("SystemUserId={0}", pintUserPersonId), Success.Ok, null);
                }

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                       "SystemUserGlobalProfile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public List<AutorizationList> GetSystemUserGlobalProfiles(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<AutorizationList> oAutorizationList;

                oAutorizationList = (from a in dbContext.systemusergobalprofile
                                     join b in dbContext.applicationhierarchy on a.i_ApplicationHierarchyId equals b.i_ApplicationHierarchyId
                                     where a.i_SystemUserId == pintSystemUserId &&
                                           a.i_IsDeleted == 0
                                     select new AutorizationList
                                     {
                                         I_ApplicationHierarchyId = (int)a.i_ApplicationHierarchyId,
                                         V_Description = b.v_Description,
                                         I_ParentId = b.i_ParentId
                                     }).ToList();
                return oAutorizationList;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void DeleteSystemUserGlobalProfile(ref OperationResult pobjOperationResult, int pintSystemUserId, List<string> ClientSession)
        {

            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad a eliminar
                var objEntitySource = (from a in dbContext.systemusergobalprofile
                                       where a.i_SystemUserId == pintSystemUserId
                                       select a);

                foreach (var item in objEntitySource)
                {
                    dbContext.DeleteObject(item);
                }

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                   "SystemUserGlobalProfile", string.Format("SystemUserId={0}", pintSystemUserId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                      "SystemUserGlobalProfile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public int GetSystemUserGlobalProfileCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.systemusergobalprofile select a;

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

        public void UpdateSystemUserGlobalProfiles(ref OperationResult pobjOperationResult, List<systemusergobalprofileDto> ListSystemUserGlobalProfileUpdate, List<systemusergobalprofileDto> ListSystemUserGlobalProfileDelete, List<string> ClientSession)
        {
            OperationResult _OperationResult1 = new OperationResult();
            OperationResult _OperationResult2 = new OperationResult();

            try
            {
                if (ListSystemUserGlobalProfileUpdate.Count != 0)
                {
                    AddSystemUserGlobalProfiles(ref _OperationResult1, ListSystemUserGlobalProfileUpdate, int.Parse(ClientSession[2]), ClientSession, false);
                }

                if (ListSystemUserGlobalProfileDelete.Count != 0)
                {
                    DeleteSystemUserGlobalProfiles(ref _OperationResult2, ListSystemUserGlobalProfileDelete, ClientSession);
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                   "SystemUserGlobalProfile", string.Format("SystemUserId={0}", ClientSession[2].ToString()), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                    "SystemUserGlobalProfile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        #endregion

        #region RestrictedWarehouseProfile

        public int GetRestrictedWarehouseProfileCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.restrictedwarehouseprofile select a;

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

        public void AddRestrictedWarehouseProfile(ref OperationResult pobjOperationResult, restrictedwarehouseprofileDto RestrictedWarehouseProfile, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.restrictedwarehouseprofile
                                       where a.i_SystemUserId == RestrictedWarehouseProfile.i_SystemUserId &&
                                             a.i_NodeId == RestrictedWarehouseProfile.i_NodeId &&
                                             a.v_WarehouseId == RestrictedWarehouseProfile.v_WarehouseId
                                       select a).FirstOrDefault();

                if (objEntitySource != null)
                {
                    if (objEntitySource.i_IsDeleted == 0)   // Registro ya esta grabado
                    {
                        // validar que no se vuelva a registrar datos ya existentes
                        pobjOperationResult.ErrorMessage = "Este Almacén para este Nodo / Empresa / Sede ya existe agregue otro por favor.)";
                        pobjOperationResult.Success = 1;
                        return;
                    }
                    else if (objEntitySource.i_IsDeleted == 1)  // Registro macado como eliminado
                    {
                        // Actualizar registro (dar de alta al registro ya existente "no volver a insertar")
                        OperationResult objOperationResult2 = new OperationResult();
                        UpdateRestrictedWarehouseProfile(ref objOperationResult2,
                                                      RestrictedWarehouseProfile.i_NodeId,
                                                      RestrictedWarehouseProfile.i_SystemUserId,
                                                      RestrictedWarehouseProfile.v_WarehouseId,
                                                      ClientSession);
                        pobjOperationResult = objOperationResult2;
                        return;
                    }
                }
                else
                {
                    // Grabar nuevo registro
                    restrictedwarehouseprofile objEntity;

                    objEntity = restrictedwarehouseprofileAssembler.ToEntity(RestrictedWarehouseProfile);
                    objEntity.d_InsertDate = DateTime.Now;
                    objEntity.i_InsertUserId = int.Parse(ClientSession[2]);
                    objEntity.i_IsDeleted = 0;

                    dbContext.AddTorestrictedwarehouseprofile(objEntity);

                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;

                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                        "restrictedwarehouseProfile", string.Empty, Success.Ok, null);

                    return;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                  "restrictedwarehouseprofile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public List<RestrictedWarehouseProfileList> GetRestrictedWarehouseProfileForGridView(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.restrictedwarehouseprofile
                             join b in dbContext.warehouse on a.v_WarehouseId equals b.v_WarehouseId
                             join c in dbContext.location on b.v_LocationId equals c.v_LocationId
                             join d in dbContext.organization on c.v_OrganizationId equals d.v_OrganizationId
                             join e in dbContext.node on a.i_NodeId equals e.i_NodeId
                             where a.i_SystemUserId == pintSystemUserId &&
                                    a.i_IsDeleted == 0
                             select new RestrictedWarehouseProfileList
                             {
                                 i_NodeId = a.i_NodeId,
                                 v_WarehouseId = a.v_WarehouseId,
                                 v_OrganizationName = d.v_Name,
                                 v_WarehouseName = b.v_Name,
                                 v_OrganizationId = d.v_OrganizationId,
                                 i_SystemUserId = a.i_SystemUserId,
                                 v_LocationId = c.v_LocationId,
                                 v_LocationName = c.v_Name,
                                 v_NodeName = e.v_Description
                             }).ToList();

                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void DeleteRestrictedWarehouseProfile(ref OperationResult pobjOperationResult, int pintNodeId, int pintSystemUserId, string pstrWarehouseId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.restrictedwarehouseprofile
                                       where a.i_SystemUserId == pintSystemUserId &&
                                             a.i_NodeId == pintNodeId &&
                                             a.v_WarehouseId == pstrWarehouseId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }

        public void DeleteRestrictedWarehouseProfileBySystemUSerId(ref OperationResult pobjOperationResult, int pintSystemUserId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.restrictedwarehouseprofile
                                       where a.i_SystemUserId == pintSystemUserId
                                       select a);

                //if (objEntitySource != null)

                foreach (var item in objEntitySource)
                {
                    // Crear la entidad con los datos actualizados
                    item.d_UpdateDate = DateTime.Now;
                    item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    item.i_IsDeleted = 1;
                }

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                   "RestrictedWarehouseProfile", string.Format("SystemUserId={0}", pintSystemUserId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                "RestrictedWarehouseProfile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        private void UpdateRestrictedWarehouseProfile(ref OperationResult pobjOperationResult, int pintNodeId, int pintSystemUserId, string pstrWarehouseId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.restrictedwarehouseprofile
                                       where a.i_SystemUserId == pintSystemUserId &&
                                             a.i_NodeId == pintNodeId &&
                                             a.v_WarehouseId == pstrWarehouseId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 0;

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                   "RestrictedWarehouseProfile", string.Format("SystemUserId={0}", pintSystemUserId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                "RestrictedWarehouseProfile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        #endregion

        #region SystemUserRoleNode

        public List<RoleNodeList> GetSystemUserRoleNodeForGridView(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.systemuserrolenode
                             join e in dbContext.node on a.i_NodeId equals e.i_NodeId
                             join J2 in dbContext.systemparameter on new { a = a.i_RoleId, b = 115 }
                                        equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where a.i_SystemUserId == pintSystemUserId &&
                                   a.i_IsDeleted == 0
                             select new RoleNodeList
                             {
                                 i_NodeId = a.i_NodeId,
                                 i_RoleId = a.i_RoleId,
                                 v_NodeName = e.v_Description,
                                 v_RoleName = J2.v_Value1,
                                 i_SystemUserId = a.i_SystemUserId
                             }).ToList();

                pobjOperationResult.Success = 1;
                return query;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        private bool IsSystemUserAssignedToNodeRol(ref OperationResult pobjOperationResult, int pintSystemUserId, int pintNodeId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            // validar que un usuario solo pueda tener asignado un nodo rol
            var query = (from a in dbContext.systemuserrolenode
                         join b in dbContext.systemuser on a.i_SystemUserId equals b.i_SystemUserId
                         join c in dbContext.node on a.i_NodeId equals c.i_NodeId
                         join J2 in dbContext.systemparameter on new { a = a.i_RoleId, b = 115 }
                              equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                         from J2 in J2_join.DefaultIfEmpty()
                         where a.i_SystemUserId == pintSystemUserId &&
                               a.i_NodeId == pintNodeId &&
                               a.i_IsDeleted == 0
                         select new
                         {
                             v_UserName = b.v_UserName,
                             v_NodeName = c.v_Description,
                             v_RoleName = J2.v_Value1,
                         }).FirstOrDefault();

            if (query != null)
            {
                pobjOperationResult.ErrorMessage = string.Format("El Nodo / Rol: <font color='red'> {0} </font> ya se encuentra asignado al usuario <font color='red'> {1} </font> Por favor elija otro.",
                                                    query.v_NodeName + " " + query.v_RoleName,
                                                    query.v_UserName);

                pobjOperationResult.Success = 1;
                return true;
            }

            return false;
        }

        public void AddSystemUserRoleNode(ref OperationResult pobjOperationResult, systemuserrolenodeDto pobjSystemUserRoleNode, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                OperationResult objOperationResult5 = new OperationResult();
                if (IsSystemUserAssignedToNodeRol(ref objOperationResult5,
                                                    pobjSystemUserRoleNode.i_SystemUserId,
                                                    pobjSystemUserRoleNode.i_NodeId))
                {
                    pobjOperationResult = objOperationResult5;
                    return;
                }

                var objEntitySource = (from a in dbContext.systemuserrolenode
                                       where a.i_SystemUserId == pobjSystemUserRoleNode.i_SystemUserId &&
                                             a.i_NodeId == pobjSystemUserRoleNode.i_NodeId &&
                                             a.i_RoleId == pobjSystemUserRoleNode.i_RoleId
                                       select a).FirstOrDefault();

                if (objEntitySource != null)
                {
                    if (objEntitySource.i_IsDeleted == 1)  // Registro macado como eliminado
                    {
                        // Actualizar registro (dar de alta al registro ya existente "no volver a insertar")
                        OperationResult objOperationResult2 = new OperationResult();
                        UpdateSystemUserRoleNode(ref objOperationResult2,
                                                      pobjSystemUserRoleNode.i_NodeId,
                                                      pobjSystemUserRoleNode.i_SystemUserId,
                                                      pobjSystemUserRoleNode.i_RoleId,
                                                      ClientSession);
                        pobjOperationResult = objOperationResult2;
                        return;
                    }
                }
                else
                {
                    // Grabar nuevo registro
                    systemuserrolenode objEntity;

                    objEntity = systemuserrolenodeAssembler.ToEntity(pobjSystemUserRoleNode);
                    objEntity.d_InsertDate = DateTime.Now;
                    objEntity.i_InsertUserId = int.Parse(ClientSession[2]);
                    objEntity.i_IsDeleted = 0;

                    dbContext.AddTosystemuserrolenode(objEntity);

                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;

                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                        "SystemUserRoleNode", string.Empty, Success.Ok, null);

                    return;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                  "SystemUserRoleNode", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        private void UpdateSystemUserRoleNode(ref OperationResult pobjOperationResult, int pintNodeId, int pintSystemUserId, int pintRoleId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.systemuserrolenode
                                       where a.i_SystemUserId == pintSystemUserId &&
                                             a.i_NodeId == pintNodeId &&
                                             a.i_RoleId == pintRoleId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 0;

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                   "SystemUserRoleNode", string.Format("SystemUserId={0}", pintSystemUserId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                "SystemUserRoleNode", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public int GetSystemUserRoleNodeCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.systemuserrolenode select a;

                //string _filterEx = "i_IsDeleted==0";
                //query = query.Where(_filterEx);

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

        public void DeleteSystemUserRoleNode(ref OperationResult pobjOperationResult, int pintNodeId, int pintSystemUserId, int pintRoleId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.systemuserrolenode
                                       where a.i_SystemUserId == pintSystemUserId &&
                                             a.i_NodeId == pintNodeId &&
                                             a.i_RoleId == pintRoleId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                   "SystemUserRoleNode", string.Format("SystemUserId={0}", pintSystemUserId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION,
                "SystemUserRoleNode", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        #endregion

        public string GetIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName()); <-- Obsolete
            IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }
    }
}
