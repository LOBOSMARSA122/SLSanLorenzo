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
  public  class MedicalExamFieldsBL
    {
        public List<MedicalExamFieldsList> GetMedicalExamFieldsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.componentfields
                            join B in dbContext.componentfield on A.v_ComponentFieldId equals B.v_ComponentFieldId
                            join C in dbContext.systemparameter on new { a = B.i_IsRequired.Value, b = 111 } equals new { a = C.i_ParameterId, b = C.i_GroupId }
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new MedicalExamFieldsList
                            {
                                v_MedicalExamFieldId = B.v_ComponentFieldId,
                                v_ComponentId = A.v_ComponentId,
                                v_TextLabel = B.v_TextLabel,
                                i_LabelWidth = B.i_LabelWidth.Value,
                                v_DefaultText = B.v_DefaultText,
                                i_ControlId = B.i_ControlId.Value,
                                i_GroupId = B.i_GroupId.Value,
                                i_ItemId = B.i_ItemId.Value,
                                i_ControlWidth = B.i_WidthControl.Value,
                                i_HeightControl = B.i_HeightControl.Value,
                                i_MaxLenght = B.i_MaxLenght.Value,
                                i_IsRequired = B.i_IsRequired.Value,
                                v_IsRequired = C.v_Value1,
                                i_IsCalculate = B.i_IsCalculate.Value,
                                i_Order = B.i_Order.Value,
                                i_MeasurementUnitId = B.i_MeasurementUnitId.Value,
                                r_ValidateValue1 = B.r_ValidateValue1.Value,
                                r_ValidateValue2 = B.r_ValidateValue2.Value,
                                v_Group  =A.v_Group ,

                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted.Value,
                                v_ComponentFieldId = B.v_ComponentFieldId
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

                List<MedicalExamFieldsList> objData = query.ToList();
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

        public string AddMedicalExamFields(ref OperationResult pobjOperationResult, componentfieldDto pobjDtoEntity, componentfieldsDto pobjDtoEntitys, string pstrComponentId,List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                 var query1 = (from a in dbContext.componentfield
                               join b in dbContext.componentfields on a.v_ComponentFieldId equals b.v_ComponentFieldId
                               where a.v_TextLabel.ToUpper() == pobjDtoEntity.v_TextLabel.ToUpper() && b.v_ComponentId == pobjDtoEntitys.v_ComponentId && b.v_Group == pobjDtoEntitys.v_Group
                                select a).ToList();
                 if (query1.Count > 0)
                 {
                     pobjOperationResult.ErrorMessage = "Nombre del componente duplicado";
                     return "0";
                 }
                #region ComponentField
                componentfield objEntity = componentfieldAssembler.ToEntity(pobjDtoEntity);


                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 18), "MF");

                objEntity.v_ComponentFieldId = NewId;

                

                dbContext.AddTocomponentfield(objEntity);
                dbContext.SaveChanges();
                #endregion
                
                #region ComponentFields
                componentfields objEntitys = componentfieldsAssembler.ToEntity(pobjDtoEntitys);
                objEntitys.v_ComponentFieldId = NewId;
                objEntitys.v_ComponentId = pstrComponentId;
                objEntitys.d_InsertDate = DateTime.Now;
                objEntitys.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntitys.i_IsDeleted = 0;

                dbContext.AddTocomponentfields(objEntitys);
                dbContext.SaveChanges();

                #endregion

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EXAMEN MÉDICO CAMPO", "v_MedicalExamFieldsId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EXAMEN MÉDICO CAMPO", "v_MedicalExamFieldsId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return "0";
            }
        }

        public componentfieldDto GetMedicalExamFields(ref OperationResult pobjOperationResult, string pstrMedicalExamFieldId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentfieldDto objDtoEntity = null;

                var objEntity = (from a in dbContext.componentfield
                                 where a.v_ComponentFieldId == pstrMedicalExamFieldId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = componentfieldAssembler.ToDTO(objEntity);

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

        public void UpdateMedicalExamField(ref OperationResult pobjOperationResult, componentfieldDto pobjDtoEntity, string pstComponentId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                
                // Obtener la entidad fuente componentfield
                var objEntitySource = (from a in dbContext.componentfield
                                       where a.v_ComponentFieldId == pobjDtoEntity.v_ComponentFieldId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                componentfield objEntity = componentfieldAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.componentfield.ApplyCurrentValues(objEntity);

                dbContext.componentfield.ApplyCurrentValues(objEntity);
                // Guardar los cambios
                dbContext.SaveChanges();

                // Obtener la entidad fuente componentfields
                var objEntitySource1 = (from a in dbContext.componentfields
                                       where a.v_ComponentFieldId == pobjDtoEntity.v_ComponentFieldId && a.v_ComponentId == pstComponentId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource1.d_UpdateDate = DateTime.Now;
                objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EXAMEN MÉDICO CAMPO", "v_MedicalExamFieldsId=" + objEntity.v_ComponentFieldId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EXAMEN MÉDICO CAMPO", "v_MedicalExamFieldsId=" + pobjDtoEntity.v_ComponentFieldId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteMedicalExamFields(ref OperationResult pobjOperationResult, string pstrComponentId, string pstrMedicalExamFieldId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query1 = (from a in dbContext.componentfields
                              where a.v_ComponentFieldId == pstrMedicalExamFieldId && a.i_IsDeleted == 0
                              select a).ToList();

                if (query1.Count() > 1)
                {
                    // Obtener la entidad fuente
                    var objEntitySource = (from a in dbContext.componentfields
                                           where a.v_ComponentFieldId == pstrMedicalExamFieldId && a.v_ComponentId == pstrComponentId
                                           select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    objEntitySource.d_UpdateDate = DateTime.Now;
                    objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySource.i_IsDeleted = 1;
                }
                else
                {
                    // Obtener la entidad fuente
                    var objEntitySource = (from a in dbContext.componentfields
                                           where a.v_ComponentFieldId == pstrMedicalExamFieldId && a.v_ComponentId == pstrComponentId
                                           select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    objEntitySource.d_UpdateDate = DateTime.Now;
                    objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySource.i_IsDeleted = 1;

                    // Obtener la entidad fuente
                    var objEntitySource1 = (from a in dbContext.componentfield
                                           where a.v_ComponentFieldId == pstrMedicalExamFieldId 
                                           select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    objEntitySource.d_UpdateDate = DateTime.Now;
                    objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySource.i_IsDeleted = 1;
                }
             
                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "EXAMEN MÉDICO CAMPO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "EXAMEN MÉDICO CAMPO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public List<SearchComponentFieldsList> FillComponentFieldList(ref OperationResult pobjOperationResult)
        {
            try 
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.componentfield
                            join B in dbContext.componentfields on A.v_ComponentFieldId equals B.v_ComponentFieldId
                            join C in dbContext.component on B.v_ComponentId equals C.v_ComponentId
                            where A.i_IsDeleted == 0
                            select new SearchComponentFieldsList
                            {                               
                                Nombre = A.v_TextLabel,
                                Grupo = B.v_Group,
                                Componente = C.v_Name

                            };
                List<SearchComponentFieldsList> objData = query.ToList();
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

        #region ComponentFields

        public List<ComponentFieldsList> GetComponentFieldsagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.componentfields
                            join B in dbContext.componentfield on A.v_ComponentFieldId equals B.v_ComponentFieldId
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                             equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0 

                            select new ComponentFieldsList
                            {
                                v_ComponentFieldId = A.v_ComponentFieldId,
                                v_ComponentId = A.v_ComponentId,
                                v_Group = A.v_Group,
                                v_TextLabel = B.v_TextLabel,
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

                List<ComponentFieldsList> objData = query.ToList();
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

        public componentfieldsDto GetComponentFields(ref OperationResult pobjOperationResult, string pstrComponentFieldId, string pstrComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentfieldsDto objDtoEntity = null;

                var objEntity = (from a in dbContext.componentfields
                                 where a.v_ComponentFieldId == pstrComponentFieldId && a.v_ComponentId == pstrComponentId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = componentfieldsAssembler.ToDTO(objEntity);

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

        public void AddComponentFields(ref OperationResult pobjOperationResult, List<componentfieldsDto> pobjcomponentfieldsAdd, List<componentfieldsDto> pobjcomponentfieldsUpdate, List<componentfieldsDto> pobjcomponentfieldsDelete, List<string> ClientSession)
        {
            try
            {
                OperationResult objOperationResult =  new OperationResult();
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                int intNodeId = int.Parse(ClientSession[0]);
                #region Add Component Fields
                foreach (var item in pobjcomponentfieldsAdd)
                {
                    componentfields objEntity1 = new componentfields();
                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;


                    objEntity1.v_ComponentFieldId = item.v_ComponentFieldId;
                    objEntity1.v_ComponentId = item.v_ComponentId;
                    objEntity1.v_Group = item.v_Group;

                    //Verificar si se encuentra en la BD eliminado
                    var objEntitySource1 = (from a in dbContext.componentfields
                                            where a.v_ComponentFieldId == item.v_ComponentFieldId && a.v_ComponentId == item.v_ComponentId
                                            select a).FirstOrDefault();

                    if (objEntitySource1 == null)
                    {
                        dbContext.AddTocomponentfields(objEntity1);
                    }
                    else
                    {
                        objEntitySource1.i_IsDeleted = 0;

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        
                    }
                   
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Update Component Fields
                if (pobjcomponentfieldsUpdate != null)
                {
                    // Actualizar Componentes del protocolo
                    foreach (var item in pobjcomponentfieldsUpdate)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.componentfields
                                                where a.v_ComponentFieldId == item.v_ComponentFieldId && a.v_ComponentId == item.v_ComponentId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados

                        objEntitySource1.v_Group = item.v_Group;
                        objEntitySource1.i_IsDeleted = 0;

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    }
                }
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Delete Component Fields
                if (pobjcomponentfieldsDelete != null)
                {
                    foreach (var item in pobjcomponentfieldsDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.componentfields
                                                where a.v_ComponentFieldId == item.v_ComponentFieldId  && a.v_ComponentId == item.v_ComponentId
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
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "CAMPOS DEL COMPONENTE", "v_ComponentId=" + objEntity1.v_ComponentId + "v_ComponentFieldId= " + objEntity1.v_ComponentFieldId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "CAMPOS DEL COMPONENTE", "v_ComponentId= No generado ,v_ComponentFieldId= No generado ", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public bool UpdateComponentFields(ref OperationResult pobjOperationResult, componentfieldsDto pobjDtoEntity, string pstrTextLableOld, string pstrTextLableNew, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                if (pstrTextLableOld.ToUpper().Trim() != pstrTextLableNew.ToUpper().Trim())
                {
                    var query1 = (from a in dbContext.componentfield
                                  join b in dbContext.componentfields on a.v_ComponentFieldId equals b.v_ComponentFieldId
                                  where a.v_TextLabel.ToUpper() == pstrTextLableNew.ToUpper() && b.v_ComponentId == pobjDtoEntity.v_ComponentId && b.v_Group == pobjDtoEntity.v_Group
                                  select a).ToList();
                    if (query1.Count > 0)
                    {
                        pobjOperationResult.ErrorMessage = "Nombre del componente duplicado";
                        return false;
                    }
                }             

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.componentfields
                                       where a.v_ComponentFieldId == pobjDtoEntity.v_ComponentFieldId && a.v_ComponentId == pobjDtoEntity.v_ComponentId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                componentfields objEntity = componentfieldsAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.componentfields.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "CAMPOS DEL COMPONENTE", "v_ComponentId=" + objEntity.v_ComponentId + "v_ComponentFieldId= " + objEntity.v_ComponentFieldId, Success.Ok, null);
                return true;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "CAMPOS DEL COMPONENTE", "v_ComponentId= No generado ,v_ComponentFieldId= No generado ", Success.Failed, pobjOperationResult.ExceptionMessage);
                return false;
            }
        }

        public void DeleteComponentFields(ref OperationResult pobjOperationResult, string pstrComponentFieldId, string pstrComponentId , List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.componentfields
                                       where a.v_ComponentFieldId == pstrComponentFieldId && a.v_ComponentId == pstrComponentId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "CAMPOS DEL COMPONENTE", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "CAMPOS DEL COMPONENTE", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        
        #endregion

    }
}
