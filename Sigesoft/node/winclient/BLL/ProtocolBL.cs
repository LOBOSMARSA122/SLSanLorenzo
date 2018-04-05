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
    public class ProtocolBL
    {
        //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

        public List<ProtocolList> GetProtocolPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrComponente)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.protocol
                            join B in dbContext.organization on A.v_EmployerOrganizationId equals B.v_OrganizationId
                            join C in dbContext.location on A.v_EmployerLocationId equals C.v_LocationId
                            join D in dbContext.groupoccupation on A.v_GroupOccupationId equals D.v_GroupOccupationId                  
                            join E in dbContext.systemparameter on new { a = A.i_EsoTypeId.Value, b = 118 }
                                                                equals new { a = E.i_ParameterId, b = E.i_GroupId } into J3_join
                            from E in J3_join.DefaultIfEmpty()

                            join F in dbContext.organization on A.v_CustomerOrganizationId equals F.v_OrganizationId

                            join I in dbContext.location on A.v_CustomerLocationId equals I.v_LocationId

                            join G in dbContext.organization on A.v_WorkingOrganizationId equals G.v_OrganizationId into J4_join
                            from G in J4_join.DefaultIfEmpty()

                            join J in dbContext.location on A.v_WorkingLocationId equals J.v_LocationId into J6_join
                            from J in J6_join.DefaultIfEmpty()

                            join H in dbContext.systemparameter on new { a = A.i_MasterServiceId.Value, b = 119 }
                                                                equals new { a = H.i_ParameterId, b = H.i_GroupId } into J5_join
                            from H in J5_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                          equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join L in dbContext.protocolcomponent on A.v_ProtocolId equals L.v_ProtocolId
                            join K in dbContext.component on L.v_ComponentId equals K.v_ComponentId

                            where A.i_IsDeleted == 0
                            select new ProtocolList
                            {
                                v_ProtocolId = A.v_ProtocolId,
                                v_Protocol = A.v_Name,
                                v_Organization = B.v_Name + " / " + C.v_Name,
                                v_Location = C.v_Name,
                                v_EsoType = E.v_Value1,
                                v_GroupOccupation = D.v_Name,
                                v_OrganizationInvoice = F.v_Name + " / " + I.v_Name,
                                v_CostCenter = A.v_CostCenter,
                                v_IntermediaryOrganization = G.v_Name + " / " + J.v_Name,
                                i_ServiceTypeId = A.i_MasterServiceTypeId.Value,
                                v_MasterServiceName = H.v_Value1,
                                i_MasterServiceId = A.i_MasterServiceId.Value,
                                v_OrganizationId = B.v_OrganizationId,
                                i_EsoTypeId = A.i_EsoTypeId,
                                v_WorkingOrganizationId = G.v_OrganizationId,
                                v_OrganizationInvoiceId = F.v_OrganizationId,
                                v_GroupOccupationId = D.v_GroupOccupationId,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                v_LocationId = A.v_EmployerLocationId,
                                v_CustomerLocationId = A.v_CustomerLocationId,
                                v_WorkingLocationId = A.v_WorkingLocationId,
                                i_IsActive = A.i_IsActive,
                                v_ComponenteNombre = K.v_Name,
                                AseguradoraId = A.v_AseguradoraOrganizationId
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

                List<ProtocolList> objData = query.ToList();

                pobjOperationResult.Success = 1;

                if (pstrComponente == "")
                {
                    var ee = objData.GroupBy(x => x.v_Protocol)
                          .Select(group => group.First());

                    return ee.ToList();
                }
                else
                {
                    return objData.FindAll(u => u.v_ComponenteNombre.Contains(pstrComponente.ToUpper())).ToList();
                }

               

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }

        }

        //public List<ProtocolList> DevolverFiltroDx(List<ProtocolList> l, string pstrComponente)
        //{
        //    List<ProtocolList> lServicios = new List<ProtocolList>();

        //    if (pstrComponente == "")
        //    {
        //        return l;
        //    }
        //    else
        //    {
        //        foreach (var item in l)
        //        {
        //            //var x = item.v_Protocol.ToList();
        //            var users = item.(u => u.v_Protocol.Contains(pstrComponente.ToUpper())).ToList();
        //            if (users.Count() > 0)
        //            {
        //                lServicios.Add(item);
        //            }
        //        }
        //        return lServicios;
        //    }


        //}
     

        public string AddProtocol(ref OperationResult pobjOperationResult, protocolDto pobjProtocol, List<protocolcomponentDto> pobjProtocolComponent, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId0 = null;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                protocol objEntity = protocolAssembler.ToEntity(pobjProtocol);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 20), "PR");
                objEntity.v_ProtocolId = NewId0;

                dbContext.AddToprotocol(objEntity);
                dbContext.SaveChanges();

                // Grabar detalle del protocolo
                foreach (var item in pobjProtocolComponent)
                {
                    protocolcomponent objEntity1 = protocolcomponentAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 21), "PC");
                    objEntity1.v_ProtocolComponentId = NewId1;
                    objEntity1.v_ProtocolId = NewId0;

                    dbContext.AddToprotocolcomponent(objEntity1);
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PROTOCOLO", "v_ProtocolId=" + NewId0.ToString(), Success.Ok, null);
                
                
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PROTOCOLO", "v_ProtocolId=" + NewId0.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               
            }

            return NewId0;
        }

        public void UpdateProtocol(ref OperationResult pobjOperationResult, protocolDto pobjProtocol, List<protocolcomponentDto> pobjProtocolComponentAdd, List<protocolcomponentDto> pobjProtocolComponentUpdate, List<protocolcomponentDto> pobjProtocolComponentDelete, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Actualizar Protocolo

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.protocol
                                       where a.v_ProtocolId == pobjProtocol.v_ProtocolId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjProtocol.d_UpdateDate = DateTime.Now;
                pobjProtocol.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                var objStrongEntity = protocolAssembler.ToEntity(pobjProtocol);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.protocol.ApplyCurrentValues(objStrongEntity);

                #endregion

                #region Create Protocol Component

                int intNodeId = int.Parse(ClientSession[0]);

                // Grabar Componentes del protocolo
                foreach (var item in pobjProtocolComponentAdd)
                {

                    protocolcomponent objEntity1 = protocolcomponentAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 21), "PC");
                    objEntity1.v_ProtocolComponentId = NewId1;
                    objEntity1.v_ProtocolId = pobjProtocol.v_ProtocolId;

                    dbContext.AddToprotocolcomponent(objEntity1);

                }

                #endregion

                #region Update Protocol Component

                if (pobjProtocolComponentUpdate != null)
                {
                    // Actualizar Componentes del protocolo
                    foreach (var item in pobjProtocolComponentUpdate)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.protocolcomponent
                                                where a.v_ProtocolComponentId == item.v_ProtocolComponentId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados

                        //objEntitySource1.v_ComponentId = item.v_ComponentId;
                        objEntitySource1.r_Price = item.r_Price;
                        objEntitySource1.i_OperatorId = item.i_OperatorId;
                        objEntitySource1.i_Age = item.i_Age;
                        objEntitySource1.i_GenderId = item.i_GenderId;
                        objEntitySource1.i_IsAdditional = item.i_IsAdditional;
                        objEntitySource1.i_IsConditionalId = item.i_IsConditionalId;
                        objEntitySource1.i_IsConditionalIMC = item.i_IsConditionalIMC;
                        objEntitySource1.i_GrupoEtarioId = item.i_GrupoEtarioId;
                        objEntitySource1.r_Imc = item.r_Imc;
                        objEntitySource1.i_IsDeleted = 0;

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    }
                }

                #endregion

                #region Delete Protocol Component

                if (pobjProtocolComponentDelete != null)
                {
                    // Eliminar Componentes del protocolo
                    foreach (var item in pobjProtocolComponentDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.protocolcomponent
                                                where a.v_ProtocolComponentId == item.v_ProtocolComponentId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                    }
                }

                #endregion

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PROTOCOLO / COMPONENTES", "v_ProtocolId=" + pobjProtocol.v_ProtocolId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PROTOCOLO / COMPONENTES", "v_ProtocolId=" + pobjProtocol.v_ProtocolId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }
        
        public List<ProtocolComponentList> GetProtocolComponents(ref OperationResult pobjOperationResult, string pstrProtocolId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              
               var objEntity = (from a in dbContext.protocolcomponent
                                join b in dbContext.component on a.v_ComponentId equals b.v_ComponentId
                                join fff in dbContext.systemparameter on new { a = b.i_CategoryId.Value, b = 116 } // CATEGORIA DEL EXAMEN
                                                                                         equals new { a = fff.i_ParameterId, b = fff.i_GroupId } into J5_join
                                from fff in J5_join.DefaultIfEmpty()

                                join E in dbContext.systemparameter on new { a = a.i_OperatorId.Value, b = 117 }
                                                               equals new { a = E.i_ParameterId, b = E.i_GroupId } into J1_join
                                from E in J1_join.DefaultIfEmpty()

                                join H in dbContext.systemparameter on new { a = a.i_GenderId.Value, b = 130 }  // Genero condicional
                                                                   equals new { a = H.i_ParameterId, b = H.i_GroupId } into J2_join
                                from H in J2_join.DefaultIfEmpty()

                                join I in dbContext.systemparameter on new { a = b.i_ComponentTypeId.Value, b = 126 }  // Tipo componente
                                                                  equals new { a = I.i_ParameterId, b = I.i_GroupId } into J3_join
                                from I in J3_join.DefaultIfEmpty()
                                 where a.v_ProtocolId == pstrProtocolId
                                 && a.i_IsDeleted == 0

                                 select new ProtocolComponentList
                                 { 
                                    v_ComponentId = a.v_ComponentId,
                                    v_ComponentName = b.v_Name,
                                    v_Porcentajes = fff.v_Field,
                                    //v_ServiceComponentStatusName = K.v_Value1,
                                    v_ProtocolComponentId = a.v_ProtocolComponentId,
                                    r_Price = a.r_Price,
                                    v_Operator = E.v_Value1,
                                    i_Age = a.i_Age,
                                    v_Gender = H.v_Value1,  
                                    i_IsConditionalIMC = a.i_IsConditionalIMC,
                                    r_Imc =a.r_Imc,
                                    v_IsConditional = a.i_IsConditionalId == 1 ? "Si" : "No",      
                                    i_isAdditional = a.i_IsAdditional,
                                    v_ComponentTypeName = I.v_Value1,                                   
                                    i_RecordStatus = (int)RecordStatus.Grabado,
                                    i_RecordType = (int)RecordType.NoTemporal,
                                    i_GenderId = a.i_GenderId,
                                    i_GrupoEtarioId = a.i_GrupoEtarioId,
                                    i_IsConditionalId = a.i_IsConditionalId,
                                    i_OperatorId = a.i_OperatorId,
                                    i_IsDeleted = a.i_IsDeleted,
                                    d_CreationDate = a.d_InsertDate,
                                    v_CategoryName = fff.v_Value1,
                                    i_CategoryId = b.i_CategoryId
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

        public ProtocolComponentList GetProtocolComponent(ref OperationResult pobjOperationResult, string pstrProtocolComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.protocolcomponent
                                 join b in dbContext.component on a.v_ComponentId equals b.v_ComponentId
                                 join E in dbContext.systemparameter on new { a = a.i_OperatorId.Value, b = 117 }
                                                                    equals new { a = E.i_ParameterId, b = E.i_GroupId } into J1_join
                                 from E in J1_join.DefaultIfEmpty()

                                 join H in dbContext.systemparameter on new { a = a.i_GenderId.Value, b = 100 }
                                                                    equals new { a = H.i_ParameterId, b = H.i_GroupId } into J2_join
                                 from H in J2_join.DefaultIfEmpty()
                                 where a.v_ProtocolComponentId == pstrProtocolComponentId
                                 select new ProtocolComponentList
                                 {
                                     v_ComponentId = a.v_ComponentId,
                                     v_ComponentName = b.v_Name,
                                     v_ProtocolComponentId = a.v_ProtocolComponentId,
                                     r_Price = a.r_Price,
                                     v_Operator = E.v_Value1,
                                     i_OperatorId = a.i_OperatorId,
                                     i_GenderId = a.i_GenderId,
                                     i_Age = a.i_Age,
                                     v_Gender = H.v_Value1,
                                     i_IsConditionalIMC = a.i_IsConditionalIMC,
                                     r_Imc = a.r_Imc,
                                     i_IsConditionalId = a.i_IsConditionalId,
                                     v_IsConditional = a.i_IsConditionalId == 1 ? "Si" : "No"
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

        public ProtocolComponentList GetProtocolComponentByProtocol(ref OperationResult pobjOperationResult,string pstrProtocolId, string pstrComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.protocolcomponent
                                 join b in dbContext.component on a.v_ComponentId equals b.v_ComponentId
                                 join E in dbContext.systemparameter on new { a = a.i_OperatorId.Value, b = 117 }
                                                                    equals new { a = E.i_ParameterId, b = E.i_GroupId } into J1_join
                                 from E in J1_join.DefaultIfEmpty()

                                 join H in dbContext.systemparameter on new { a = a.i_GenderId.Value, b = 100 }
                                                                    equals new { a = H.i_ParameterId, b = H.i_GroupId } into J2_join
                                 from H in J2_join.DefaultIfEmpty()
                                 where a.v_ComponentId == pstrComponentId && a.v_ProtocolId == pstrProtocolId
                                 select new ProtocolComponentList
                                 {
                                     v_ComponentId = a.v_ComponentId,
                                     v_ComponentName = b.v_Name,
                                     v_ProtocolComponentId = a.v_ProtocolComponentId,
                                     r_Price = a.r_Price,
                                     v_Operator = E.v_Value1,
                                     i_OperatorId = a.i_OperatorId,
                                     i_GenderId = a.i_GenderId,
                                     i_Age = a.i_Age,
                                     v_Gender = H.v_Value1,
                                     i_IsConditionalIMC = a.i_IsConditionalIMC,
                                     r_Imc = a.r_Imc,
                                     i_IsConditionalId = a.i_IsConditionalId,
                                     v_IsConditional = a.i_IsConditionalId == 1 ? "Si" : "No"
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

        public protocolDto GetProtocol(ref OperationResult pobjOperationResult, string pstrProtocolId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                protocolDto objDtoEntity = null;

                var objEntity = (from a in dbContext.protocol
                                 where a.v_ProtocolId == pstrProtocolId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = protocolAssembler.ToDTO(objEntity);

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

        public bool IsExistsProtocol(ref OperationResult pobjOperationResult, string pstrProtocolId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.service
                                 where a.v_ProtocolId == pstrProtocolId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                {
                    return true;
                }

                pobjOperationResult.Success = 1;
                
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }

            return false;
        }

        public bool IsExistsProtocolName(ref OperationResult pobjOperationResult, string pstrProtocolName)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.protocol
                                 where a.v_Name.ToUpper() == pstrProtocolName
                                 select a).FirstOrDefault();

                if (objEntity != null)
                {
                    return true;
                }

                pobjOperationResult.Success = 1;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }

            return false;
        }

        public ProtocolList GetProtocolById(ref OperationResult pobjOperationResult, string pstrProtocolId)
        {
             try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var objProtocol= (from A in dbContext.protocol
                            join B in dbContext.organization on A.v_EmployerOrganizationId equals B.v_OrganizationId
                            join C in dbContext.location on A.v_EmployerLocationId equals C.v_LocationId
                            join D in dbContext.groupoccupation on A.v_GroupOccupationId equals D.v_GroupOccupationId
                            join E in dbContext.systemparameter on new { a = A.i_EsoTypeId.Value, b = 118 } equals new { a = E.i_ParameterId, b = E.i_GroupId }
                            join F in dbContext.organization on A.v_CustomerOrganizationId equals F.v_OrganizationId                            
                            join H in dbContext.systemparameter on new { a = A.i_MasterServiceId.Value, b = 119 } equals new { a = H.i_ParameterId, b = H.i_GroupId }
                            join I in dbContext.location on A.v_CustomerLocationId equals I.v_LocationId
                      
                            join J in dbContext.location on A.v_EmployerLocationId equals J.v_LocationId
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                          equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J3 in dbContext.organization on new { a = A.v_WorkingOrganizationId }
                                    equals new { a = J3.v_OrganizationId} into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                            join J4 in dbContext.area on new { a = C.v_LocationId }
                                    equals new { a = J4.v_LocationId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            join J5 in dbContext.ges on new { a = J4.v_AreaId }
                                equals new { a = J5.v_AreaId } into J5_join
                            from J5 in J5_join.DefaultIfEmpty()

                            join J6 in dbContext.occupation on new { a = J5.v_GesId, b = D.v_GroupOccupationId }
                                        equals new { a = J6.v_GesId, b = J6.v_GroupOccupationId } into J6_join
                            from J6 in J6_join.DefaultIfEmpty()

                            //join J7 in dbContext.groupoccupation on new { a = J4.v_AreaId }
                            //    equals new { a = J7.v_AreaId } into J7_join
                            //from J5 in J5_join.DefaultIfEmpty()

                            where (A.v_ProtocolId == pstrProtocolId) &&  (A.i_IsDeleted == 0)
                            select new  ProtocolList
                            {
                                v_Name =A.v_Name,
                                v_ProtocolId = A.v_ProtocolId, //id Protocolo
                                v_Protocol = A.v_Name,// monbre protocolo
                                v_Organization = B.v_Name + " / "+ J.v_Name, // nombre organizacion
                                v_Location = C.v_Name, // nombre de sede
                                v_EsoType = E.v_Value1, // Esoa, Esor
                               
                                v_OrganizationInvoice = F.v_Name, // empresa que factura
                                v_CostCenter = A.v_CostCenter, // centro de costo
                                v_IntermediaryOrganization = J3.v_Name + " / " + I.v_Name, // empresa intermediaria
                                
                                v_MasterServiceName = H.v_Value1, // Eso o no Eo
                                v_OrganizationId = B.v_OrganizationId,
                                i_EsoTypeId = E.i_ParameterId, // Id de (Esoa, Esor, Espo)
                                v_WorkingOrganizationId = J3.v_OrganizationId,
                                v_OrganizationInvoiceId = F.v_OrganizationId,
                                v_GroupOccupationId = D.v_GroupOccupationId,
                                v_Ges = J5.v_Name,
                                v_GroupOccupation = D.v_Name, // nombre GESO
                                v_Occupation = J6.v_Name,
                                i_ServiceTypeId = A.i_MasterServiceTypeId.Value,
                                i_MasterServiceId = A.i_MasterServiceId.Value,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                v_ContacName = F.v_ContacName,
                                v_Address = F.v_Address,
                                v_CustomerOrganizationId = A.v_CustomerOrganizationId
                            }).FirstOrDefault();
                ProtocolList objData = objProtocol;
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

        public bool ValidateComponentElimination(ref OperationResult pobjOperationResult, string pstrComponentId)
        {
            bool Bool = false;
             SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
             var objEntity = (from a in dbContext.protocol
                                join b in dbContext.protocolcomponent on a.v_ProtocolId equals b.v_ProtocolId
                                 where b.v_ComponentId == pstrComponentId && a.i_IsDeleted ==0
                                 select a).FirstOrDefault();
             if (objEntity != null) Bool= true;
             pobjOperationResult.Success = 1;
             return Bool;
        }

        public ProtocolList GetProtocolByIdReport(string pstrProtocolId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objProtocol = (from A in dbContext.protocol
                                   join B in dbContext.organization on A.v_EmployerOrganizationId equals B.v_OrganizationId
                                   join C in dbContext.location on A.v_EmployerLocationId equals C.v_LocationId
                                   join D in dbContext.groupoccupation on A.v_GroupOccupationId equals D.v_GroupOccupationId
                                   join E in dbContext.systemparameter on new { a = A.i_EsoTypeId.Value, b = 118 } equals new { a = E.i_ParameterId, b = E.i_GroupId }
                                   join F in dbContext.organization on A.v_CustomerOrganizationId equals F.v_OrganizationId
                                   join H in dbContext.systemparameter on new { a = A.i_MasterServiceId.Value, b = 119 } equals new { a = H.i_ParameterId, b = H.i_GroupId }
                                   join I in dbContext.location on A.v_CustomerLocationId equals I.v_LocationId

                                   join J in dbContext.location on A.v_EmployerLocationId equals J.v_LocationId
                                   join K in dbContext.datahierarchy on new { a = B.i_SectorTypeId.Value, b = 104 } equals new { a = K.i_ItemId, b = K.i_GroupId }
                                   join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                 equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                   from J1 in J1_join.DefaultIfEmpty()

                                   join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                                   equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                                   from J2 in J2_join.DefaultIfEmpty()

                                   join J3 in dbContext.organization on new { a = A.v_WorkingOrganizationId }
                                           equals new { a = J3.v_OrganizationId } into J3_join
                                   from J3 in J3_join.DefaultIfEmpty()

                                   join J4 in dbContext.area on new { a = C.v_LocationId }
                                           equals new { a = J4.v_LocationId } into J4_join
                                   from J4 in J4_join.DefaultIfEmpty()

                                   join J5 in dbContext.ges on new { a = J4.v_AreaId }
                                       equals new { a = J5.v_AreaId } into J5_join
                                   from J5 in J5_join.DefaultIfEmpty()

                                   join J6 in dbContext.occupation on new { a = J5.v_GesId, b = D.v_GroupOccupationId }
                                               equals new { a = J6.v_GesId, b = J6.v_GroupOccupationId } into J6_join
                                   from J6 in J6_join.DefaultIfEmpty()
                                   where A.v_ProtocolId == pstrProtocolId && A.i_IsDeleted == 0
                                   select new ProtocolList
                                   {
                                       v_ProtocolId = A.v_ProtocolId, //id Protocolo
                                       v_Protocol = A.v_Name,// monbre protocolo
                                       v_Organization = B.v_Name + " / " + J.v_Name, // nombre organizacion
                                       v_SectorTypeName = K.v_Value1,
                                       v_OrganizationAddress = B.v_Address,
                                       v_Location = C.v_Name, // nombre de sede
                                       v_EsoType = E.v_Value1, // Esoa, Esor

                                       v_OrganizationInvoice = F.v_Name, // empresa que factura
                                       v_CostCenter = A.v_CostCenter, // centro de costo
                                       v_IntermediaryOrganization = J3.v_Name + " / " + I.v_Name, // empresa intermediaria

                                       v_MasterServiceName = H.v_Value1, // Eso o no Eo
                                       v_OrganizationId = B.v_OrganizationId,
                                       i_EsoTypeId = E.i_ParameterId, // Id de (Esoa, Esor, Espo)
                                       v_WorkingOrganizationId = J3.v_OrganizationId,
                                       v_OrganizationInvoiceId = F.v_OrganizationId,
                                       v_GroupOccupationId = D.v_GroupOccupationId,
                                       v_Ges = J5.v_Name,
                                       v_GroupOccupation = D.v_Name, // nombre GESO
                                       v_Occupation = J6.v_Name,
                                       i_ServiceTypeId = A.i_MasterServiceTypeId.Value,
                                       i_MasterServiceId = A.i_MasterServiceId.Value,
                                       v_CreationUser = J1.v_UserName,
                                       v_UpdateUser = J2.v_UserName,
                                       d_CreationDate = A.d_InsertDate,
                                       d_UpdateDate = A.d_UpdateDate,
                                       v_ContacName = F.v_ContacName,
                                       v_Address = F.v_Address
                                   }).FirstOrDefault();
                ProtocolList objData = objProtocol;
                return objData;
            }

            catch (Exception ex)
            {
                return null;
            }

        }

        public ProtocolList GetProtocolByService(string pstrServicoId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.service
                                 join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                                 where a.v_ServiceId == pstrServicoId
                                 select new ProtocolList
                                 {
                                     v_Name = b.v_Name,
                                     Comision = b.i_ValidInDays,
                                     v_NombreVendedor = b.v_NombreVendedor
                                 }).FirstOrDefault();


                return objEntity;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        #region ProtocolSystemUser

        public List<SystemUserList> GetSystemUserExternalPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from su1 in dbContext.systemuser
                             join B in dbContext.protocolsystemuser on su1.i_SystemUserId equals B.i_SystemUserId
                             join C in dbContext.person on su1.v_PersonId equals C.v_PersonId
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
                                 v_UpdateUser = su3.v_UserName,
                                 v_PersonName = C.v_FirstName + " " + C.v_FirstLastName + " " + C.v_SecondLastName,
                                 v_DocNumber = C.v_DocNumber,
                                 d_ExpireDate = su1.d_ExpireDate,
                                 v_ProtocolId = B.v_ProtocolId
                             }
                            ).Distinct();



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

        public List<KeyValueDTO> GetExternalPermisionForChekedListByTypeId(ref OperationResult pobjOperationResult, int pintExternalUserFunctionalityTypeId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from ah in dbContext.applicationhierarchy
                             where ah.i_IsDeleted == 0 &&
                             ah.i_ExternalUserFunctionalityTypeId == pintExternalUserFunctionalityTypeId
                             select new
                             {
                                 Id = ah.i_ApplicationHierarchyId,
                                 Value1 = ah.v_Description
                             });

                var query1 = (from a in query.AsEnumerable()
                              select new KeyValueDTO
                              {
                                  Id = a.Id.ToString(),
                                  Value1 = a.Value1
                              });


                List<KeyValueDTO> obj = query1.OrderBy(P => P.Value1).ToList();
                pobjOperationResult.Success = 1;
                return obj;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<KeyValueDTO> GetExternalPermisionByProtocolIdAndSystemUserId(ref OperationResult pobjOperationResult, string pstrProtocolId, int? pintSystemUserId, int pintExternalUserFunctionalityTypeId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from psu in dbContext.protocolsystemuser
                             join ah in dbContext.applicationhierarchy on psu.i_ApplicationHierarchyId equals ah.i_ApplicationHierarchyId
                             where psu.v_ProtocolId == pstrProtocolId &&
                                   psu.i_SystemUserId == pintSystemUserId.Value &&
                                   ah.i_ExternalUserFunctionalityTypeId == pintExternalUserFunctionalityTypeId &&
                                   psu.i_IsDeleted == 0
                             select new
                             {
                                 Id = ah.i_ApplicationHierarchyId,
                                 Value1 = ah.v_Description
                             });

                var query1 = (from a in query.AsEnumerable()
                              select new KeyValueDTO
                              {
                                  Id = a.Id.ToString(),
                                  Value1 = a.Value1
                              });

                List<KeyValueDTO> objFormAction = query1.OrderBy(P => P.Value1).ToList();
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

        public string AddSystemUserExternal(ref OperationResult pobjOperationResult, personDto pobjPerson, professionalDto pobjProfessional, systemuserDto pobjSystemUser, List<protocolsystemuserDto> ListProtocolSystemUser, List<string> ClientSession)
        {
            //mon.IsActive = true;
            int SecuentialId = -1;
            string newId = string.Empty;
            person objEntity1 = null;
            int? systemUserId = null;

            OperationResult objOperationResult = new OperationResult();

            try
            {
                #region Validations
                // Validar el DNI de la persona
                if (pobjPerson != null)
                {
                    OperationResult objOperationResult6 = new OperationResult();
                    string strfilterExpression1 = string.Format("v_DocNumber==\"{0}\"&&i_Isdeleted==0", pobjPerson.v_DocNumber);
                    var _recordCount1 = new PacientBL().GetPersonCount(ref objOperationResult6, strfilterExpression1);

                    if (_recordCount1 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El número de documento " + pobjPerson.v_DocNumber + " ya se encuentra registrado. Por favor ingrese otro número de documento.";
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
                    pobjProfessional.v_PersonId = objEntity1.v_PersonId;
                    new PacientBL().AddProfessional(ref objOperationResult, pobjProfessional, ClientSession);
                }

                // Grabar Usuario
                if (pobjSystemUser != null)
                {
                    pobjSystemUser.v_PersonId = objEntity1.v_PersonId;
                    pobjSystemUser.i_SystemUserTypeId = (int)SystemUserTypeId.External;
                    systemUserId = new SecurityBL().AddSystemUSer(ref objOperationResult, pobjSystemUser, ClientSession);
                }

                #region GRABA ProtocolSystemUser

                if (ListProtocolSystemUser != null)
                {
                    AddProtocolSystemUser(ref objOperationResult, ListProtocolSystemUser, systemUserId, ClientSession, true);
                }

                #endregion

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

        public string AddSystemUserExternal_(ref OperationResult pobjOperationResult, List<protocolsystemuserDto> ListProtocolSystemUser, List<string> ClientSession, int? pintsystemUserId)
        {
            //mon.IsActive = true;
            int SecuentialId = -1;
            string newId = string.Empty;
            person objEntity1 = null;
            int? systemUserId = 0;

            OperationResult objOperationResult = new OperationResult();

            try
            {

                #region GRABA ProtocolSystemUser

                if (ListProtocolSystemUser != null)
                {
                    AddProtocolSystemUser(ref objOperationResult, ListProtocolSystemUser, pintsystemUserId, ClientSession, true);
                }

                #endregion
              
                pobjOperationResult.Success = 1;
           
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

        public string UpdateSystemUserExternal(ref OperationResult pobjOperationResult, bool pbIsChangeDocNumber, personDto pobjPerson, professionalDto pobjProfessional, bool pbIsChangeUserName, systemuserDto pobjSystemUser, List<protocolsystemuserDto> ListProtocolSystemUserPermisoUpdate, List<protocolsystemuserDto> ListProtocolSystemUserPermisoDelete, List<protocolsystemuserDto> ListProtocolSystemUserNotificacionUpdate, List<protocolsystemuserDto> ListProtocolSystemUserNotificacionDelete, List<string> ClientSession)
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
                        pobjOperationResult.ErrorMessage = "El nombre de usuario " + pobjSystemUser.v_UserName + " ya se encuentra registrado. Por favor ingrese otro nombre de usuario.";
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
                    var _recordCount1 = new PacientBL().GetPersonCount(ref objOperationResult6, strfilterExpression1);

                    if (_recordCount1 != 0)
                    {
                        pobjOperationResult.ErrorMessage = "El número de documento " + pobjPerson.v_DocNumber + " ya se encuentra registrado. Por favor ingrese otro número de documento.";
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
                    new PacientBL().UpdateProfessional(ref pobjOperationResult, pobjProfessional, ClientSession);
                }

                // Actualiza Usuario
                if (pobjSystemUser != null)
                {
                    pobjSystemUser.i_SystemUserTypeId = (int)SystemUserTypeId.External;
                    new SecurityBL().UpdateSystemUSer(ref pobjOperationResult, pobjSystemUser, ClientSession);
                }

                if (ListProtocolSystemUserPermisoUpdate != null)
                {
                    AddProtocolSystemUser(ref pobjOperationResult, ListProtocolSystemUserPermisoUpdate, null, ClientSession, false);
                }

                if (ListProtocolSystemUserPermisoDelete != null)
                {
                    DeleteProtocolSystemUser(ref pobjOperationResult, ListProtocolSystemUserPermisoDelete, ClientSession);
                }

                if (ListProtocolSystemUserNotificacionUpdate != null)
                {
                    AddProtocolSystemUser(ref pobjOperationResult, ListProtocolSystemUserNotificacionUpdate, null, ClientSession, false);
                }

                if (ListProtocolSystemUserNotificacionDelete != null)
                {
                    DeleteProtocolSystemUser(ref pobjOperationResult, ListProtocolSystemUserNotificacionDelete, ClientSession);
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PROTOCOL SYSTEM USER", null, Success.Ok, null);
                return "1";
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PROTOCOL SYSTEM USER", null, Success.Failed, ex.Message);
                return "-1";
            }
        }

        public void AddProtocolSystemUser(ref OperationResult pobjOperationResult, List<protocolsystemuserDto> ListProtocolSystemUserDto, int? pintSystemUserId, List<string> ClientSession, bool pbRegisterLog)
        {
            int SecuentialId = -1;
            string newId;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in ListProtocolSystemUserDto)
                {
                    // Autogeneramos el Pk de la tabla
                    SecuentialId = Utils.GetNextSecuentialId(Int32.Parse(ClientSession[0]), 44);
                    newId = Common.Utils.GetNewId(int.Parse(ClientSession[0]), SecuentialId, "PU");

                    // Grabar como nuevo
                    var objEntity = protocolsystemuserAssembler.ToEntity(item);

                    objEntity.v_ProtocolSystemUserId = newId;
                    if (pintSystemUserId == null)
                        objEntity.i_SystemUserId = item.i_SystemUserId;
                    else
                        objEntity.i_SystemUserId = pintSystemUserId.Value;
                    objEntity.d_InsertDate = DateTime.Now;
                    objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity.i_IsDeleted = 0;
                    dbContext.AddToprotocolsystemuser(objEntity);
                }

                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;

                if (pbRegisterLog == true)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                       "ProtocolSystemUser", null, Success.Ok, null);
                }

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                       "ProtocolSystemUser", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        private void DeleteProtocolSystemUser(ref OperationResult pobjOperationResult, List<protocolsystemuserDto> ListProtocolSystemUser, List<string> ClientSession)
        {

            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in ListProtocolSystemUser)
                {
                    // Obtener la entidad a eliminar
                    var objEntitySource = (from a in dbContext.protocolsystemuser
                                           where a.i_SystemUserId == item.i_SystemUserId &&
                                                 a.v_ProtocolId == item.v_ProtocolId &&
                                                 a.i_ApplicationHierarchyId == item.i_ApplicationHierarchyId &&
                                                 a.i_IsDeleted == 0
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

        public List<SystemUserList> GetAllSystemUserExternal()
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from su1 in dbContext.systemuser
                             join B in dbContext.protocolsystemuser on su1.i_SystemUserId equals B.i_SystemUserId
                             join C in dbContext.person on su1.v_PersonId equals C.v_PersonId
                             where su1.i_IsDeleted == 0
                             select new SystemUserList
                             {
                                 v_ProtocolSystemUserId = B.v_ProtocolSystemUserId,
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
                                 v_PersonName = C.v_FirstName + " " + C.v_FirstLastName + " " + C.v_SecondLastName,
                                 v_DocNumber = C.v_DocNumber,
                                 d_ExpireDate = su1.d_ExpireDate,
                                 v_ProtocolId = B.v_ProtocolId
                             }
                            );

                var objData = query.AsEnumerable()
                       .GroupBy(x => x.i_SystemUserId)
                       .Select(group => group.First());
                List<SystemUserList> objData1 = objData.ToList();
                return objData1;
            }
            catch (Exception ex)
            {
               
                return null;
            }


        }

        public List<protocolsystemuserDto> GetAllSystemUserExternalBySystemUserId(int pintSystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.protocolsystemuser
                             where A.i_IsDeleted == 0 && A.i_SystemUserId == pintSystemUserId 
                             select new protocolsystemuserDto
                             {
                                 v_ProtocolSystemUserId = A.v_ProtocolSystemUserId,
                                 i_SystemUserId = A.i_SystemUserId,
                                 v_ProtocolId = A.v_ProtocolId,
                                 i_ApplicationHierarchyId = A.i_ApplicationHierarchyId,
                             }
                            ).ToList();



                var y = query.GroupBy(g => new {g.i_ApplicationHierarchyId })
                              .Select(s => s.First());



                return y.ToList();
            }
            catch (Exception ex)
            {

                return null;
            }


        }

        #endregion

        #region Validaciones

        public bool IsExistscomponentfieldsInCurrentProtocol(ref OperationResult pobjOperationResult, string[] pobjComponenttIdToComparerList, string pstrComponentIdToFind)
        {
            bool IsExists = false;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                // Obtener campos de una lista de componente pertenecientes a un mismo protocolo
                var query1 = (from A in dbContext.componentfields
                              where (A.i_IsDeleted == 0) && (pobjComponenttIdToComparerList.Contains(A.v_ComponentId))
                              orderby A.v_ComponentFieldId 
                              select new ComponentFieldsList
                              {
                                  v_ComponentFieldId = A.v_ComponentFieldId,
                                  v_ComponentId = A.v_ComponentId
                              }).ToList();

                // Obtener campos del componente concurrente
                var query2 = (from A in dbContext.componentfields
                              where (A.i_IsDeleted == 0) && (A.v_ComponentId == pstrComponentIdToFind)
                              orderby A.v_ComponentFieldId
                              select new ComponentFieldsList
                              {
                                  v_ComponentFieldId = A.v_ComponentFieldId,
                                  v_ComponentId = A.v_ComponentId
                              }).ToList();

                // Buscar los campos del componente concurrente obtenido en la lista 
                // de campos pertenecientes a un mismo protocolo            
                IsExists = query2.Exists(s => query1.Any(a => a.v_ComponentFieldId == s.v_ComponentFieldId));
                var IsExists_ = query2.Where(s => query1.Any(a => a.v_ComponentFieldId == s.v_ComponentFieldId)).ToList();
                return IsExists;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);               
            }

            return IsExists;
        }

        public SiNo IsExistsFormula(ref OperationResult pobjOperationResult, string[] pobjComponenttIdToComparerList, string pstrComponentId)
        {
            SiNo rpta = SiNo.NONE;
            string[] source = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener campos formula del componente concurrente (que se quiere agregar)
                var fieldsFormulaFromCurrentComponent = (from A in dbContext.componentfield
                                                         join B in dbContext.componentfields on A.v_ComponentFieldId equals B.v_ComponentFieldId
                                                         where (A.i_IsDeleted == 0) && (B.v_ComponentId == pstrComponentId) && (A.i_IsCalculate == (int)SiNo.SI)
                                                         orderby A.v_ComponentFieldId
                                                         select new ComponentFieldsList
                                                         {
                                                             v_ComponentFieldId = B.v_ComponentFieldId,
                                                             v_ComponentId = B.v_ComponentId,
                                                             v_Formula = A.v_Formula,
                                                             v_TextLabel = A.v_TextLabel
                                                         }).ToList();

                // Si se encuentra un campo (formula) EN EL COMPONENTE ACTUAL QUE SE QUIERE AGREGAR 
                if (fieldsFormulaFromCurrentComponent.Count != 0)
                {                    
                    ArrayList fieldsFormulate = new ArrayList();       

                    foreach (ComponentFieldsList item in fieldsFormulaFromCurrentComponent)
                    {
                        // Obtener Campos fuente participantes en el calculo DE UNA FORMULA
                        string[] sourceFields = Common.Utils.GetTextFromExpressionInCorchete(item.v_Formula);                     
                        fieldsFormulate.AddRange(sourceFields);
                    }

                    // convertir arrayList a un string[] nativo para utilizarlo en los querys linq
                    source = Array.ConvertAll(fieldsFormulate.ToArray(), o => (string)o);

                    // Obtener datos de campos fuentes ejem: [N002-MF000000008]/Pow([N002-MF000000007],2)] para mostrar mensajes
                    var componentFieldsSourceInfoForMessage = (from A in dbContext.componentfield
                                                               join B in dbContext.componentfields on A.v_ComponentFieldId equals B.v_ComponentFieldId
                                                               join C in dbContext.component on B.v_ComponentId equals C.v_ComponentId
                                                               where (A.i_IsDeleted == 0) && (source.Contains(A.v_ComponentFieldId))
                                                               select new ComponentFieldsList
                                                               {
                                                                   v_ComponentFieldId = B.v_ComponentFieldId,
                                                                   v_ComponentId = B.v_ComponentId,
                                                                   v_Formula = A.v_Formula,
                                                                   v_TextLabel = A.v_TextLabel,
                                                                   v_ComponentName = C.v_Name
                                                               }).ToList();

                    if (componentFieldsSourceInfoForMessage.Count != 0)
                    {
                        sb.Append("El campo formula " + fieldsFormulaFromCurrentComponent[0].v_TextLabel);
                        sb.Append(" depende de los campos " + string.Join(", ", componentFieldsSourceInfoForMessage.Select(p => p.v_TextLabel)));
                        sb.Append(" que están en los componentes " + string.Join(", ", componentFieldsSourceInfoForMessage.Select(p => p.v_ComponentName)));
                        sb.Append(" Por favor agrege previamente los componentes " + string.Join(", ", componentFieldsSourceInfoForMessage.Select(p => p.v_ComponentName)));
                        sb.Append(" al protocolo.");
                        pobjOperationResult.ReturnValue = sb.ToString();
                    }
                    else
                    {
                        if (fieldsFormulate.Count == 0)
                        {
                             pobjOperationResult.ReturnValue = "Campo Formula vacia o invalida";
                        }
                    }

                    // Obtener campos de LOS componentes DEL protocolo actual, mediante una lista de IDs
                    var componentFieldsFromCurrentProtocol = (from A in dbContext.componentfields
                                                              where (A.i_IsDeleted == 0) &&
                                                              (pobjComponenttIdToComparerList.Contains(A.v_ComponentId))
                                                              orderby A.v_ComponentFieldId
                                                              select new ComponentFieldsList
                                                              {
                                                                  v_ComponentFieldId = A.v_ComponentFieldId,
                                                                  v_ComponentId = A.v_ComponentId
                                                              }).ToList();

                    // Obtener campos del componente concurrente
                    var componentFieldsFromCurrentComponent = (from A in dbContext.componentfields
                                                               where (A.i_IsDeleted == 0) && (A.v_ComponentId == pstrComponentId)
                                                               orderby A.v_ComponentFieldId
                                                               select new ComponentFieldsList
                                                               {
                                                                   v_ComponentFieldId = A.v_ComponentFieldId,
                                                                   v_ComponentId = A.v_ComponentId
                                                               }).ToList();

                    // Buscar coincidencias      
                    var _IsExists___ = componentFieldsFromCurrentComponent.Where(s => fieldsFormulate.Contains(s.v_ComponentFieldId)).ToList();
                    // versus entre [campos de comp del prot actual] contra [campos fuentes encontrados]
                    var IsExists___ = componentFieldsFromCurrentProtocol.Where(s => fieldsFormulate.Contains(s.v_ComponentFieldId)).ToList();
                  
                    if (IsExists___.Count != 0 || _IsExists___.Count != 0)
                    {
                        if (IsExists___.Count == fieldsFormulate.Count || _IsExists___.Count != 0)
                        {
                            rpta = SiNo.SI;
                        }
                        else
                        {
                            rpta = SiNo.NO;
                        }
                       
                    }
                    else
                    {
                        rpta = SiNo.NO;
                    }     
             
                    return rpta;                      
                }
               
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }

            return rpta;
        }

        #endregion

        public List<ProtocolSystemUSerExternalList> GetProtocolbySystemUserId(int pintSystemUserId)
        {
            try
            {

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objEntity = (from a in dbContext.protocolsystemuser
                                 join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                                 join c in dbContext.organization on b.v_CustomerOrganizationId equals c.v_OrganizationId
                                 where a.i_IsDeleted == 0 && a.i_SystemUserId == pintSystemUserId
                                 select new ProtocolSystemUSerExternalList
                                 {                                  
                                    v_ProtocolId =a.v_ProtocolId,
                                    v_CustomerOrganizationId = b.v_CustomerOrganizationId,
                                    v_CustomerOrganizationName = c.v_Name
                                 }).Distinct().ToList();

                return objEntity;

            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public List<ProtocolList> GetProtocolosByEmpresaId(ref OperationResult pobjOperationResult, string pstrEmpredaId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<ProtocolList> objEntity = null;

                objEntity = (from a in dbContext.protocol
                             where a.v_CustomerOrganizationId == pstrEmpredaId
                             select new ProtocolList
                             {
                                 v_ProtocolId = a.v_ProtocolId,
                                 v_Name = a.v_Name
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


        public List<OrganizationList> GetEmpresaByProtocoloId(string pstrProtocolId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<OrganizationList> objEntity = null;

                objEntity = (from a in dbContext.protocol
                             join b in dbContext.organization on a.v_CustomerOrganizationId equals  b.v_OrganizationId
                             where a.v_ProtocolId == pstrProtocolId
                             select new OrganizationList
                             {                                
                                 v_Name = b.v_Name
                             }).ToList();


                return objEntity;
            }
            catch (Exception ex)
            {               
                return null;
            }
        }

        public List<ObtenerIdsImporacion> ObtenerIdsParaImportacion(List<string> ServiceIds, int CategoriaId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<ObtenerIdsImporacion> objEntity = null;

                objEntity = (from a in dbContext.servicecomponent
                             join b in dbContext.component on a.v_ComponentId equals b.v_ComponentId
                             join c in dbContext.service on a.v_ServiceId equals c.v_ServiceId
                             join d in dbContext.person on c.v_PersonId equals d.v_PersonId
                             where b.i_CategoryId == CategoriaId && ServiceIds.Contains(c.v_ServiceId)
                             orderby b.i_UIIndex 
                             select new ObtenerIdsImporacion
                             {
                                ServicioId = c.v_ServiceId,
                                ServicioComponentId = a.v_ServiceComponentId,
                                ComponentId = a.v_ComponentId,
                                PersonId = c.v_PersonId,
                                Paciente = d.v_FirstLastName + " " + d.v_SecondLastName + " " + d.v_FirstName, 
                                DNI = d.v_DocNumber,
                                CategoriaId = b.i_CategoryId.Value,
                                i_UIIndex = b.i_UIIndex.Value
                             }).ToList();



                var objData = objEntity.AsEnumerable()
                           .GroupBy(x => new { x.CategoriaId, x.ServicioId })
                           .Select(group => group.First())
                           .OrderBy(o => o.i_UIIndex);



                return objData.ToList();
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }

        public List<ObtenerIdsImporacion> ObtenerIdsParaImportacion_(List<string> ServiceIds, int CategoriaId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<ObtenerIdsImporacion> objEntity = null;

                objEntity = (from a in dbContext.servicecomponent
                             join b in dbContext.component on a.v_ComponentId equals b.v_ComponentId
                             join c in dbContext.service on a.v_ServiceId equals c.v_ServiceId
                             join d in dbContext.person on c.v_PersonId equals d.v_PersonId
                             where b.i_CategoryId == CategoriaId && ServiceIds.Contains(c.v_ServiceId)
                             orderby b.i_UIIndex descending
                             select new ObtenerIdsImporacion
                             {
                                 ServicioId = c.v_ServiceId,
                                 ServicioComponentId = a.v_ServiceComponentId,
                                 ComponentId = a.v_ComponentId,
                                 PersonId = c.v_PersonId,
                                 Paciente = d.v_FirstLastName + " " + d.v_SecondLastName + " " + d.v_FirstName,
                                 DNI = d.v_DocNumber,
                                 CategoriaId = b.i_CategoryId.Value,
                                 i_UIIndex = b.i_UIIndex.Value
                             }).ToList();



                var objData = objEntity.AsEnumerable()
                           .GroupBy(x => new { x.CategoriaId, x.ServicioId })
                           .Select(group => group.First())
                           .OrderBy(o => o.i_UIIndex);



                return objData.ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<ProtocolList> DevolverProtocolosPorEmpresa(string pstrEmpresaCliente)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.protocol
                             where A.v_CustomerOrganizationId == pstrEmpresaCliente && A.i_IsDeleted == 0
                             select new Sigesoft.Node.WinClient.BE.ProtocolList
                             {
                                 v_ProtocolId = A.v_ProtocolId
                             }).ToList();
                return query;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CotizacionProtocolo> CotizacionProtocoloPorEmpresaClienteId(string pstrEmpresaClienteId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                OperationResult objOperationResult = new OperationResult();

                List<CotizacionProtocolo> lFinal = new List<CotizacionProtocolo>();
                CotizacionProtocolo oCotizacionProtocolo = null;

                var lProtocolos = (from A in dbContext.protocol
                             where A.v_CustomerOrganizationId == pstrEmpresaClienteId && A.i_IsDeleted == 0 
                             select new Sigesoft.Node.WinClient.BE.CotizacionProtocolo
                             {
                                 ProtocoloId = A.v_ProtocolId,
                                 ProtocoloNombre = A.v_Name
                             }).ToList();

                foreach (var item in lProtocolos)
                {
                    //Obtener Detalle de Protocolo
                    oCotizacionProtocolo = new CotizacionProtocolo();
                    oCotizacionProtocolo.ProtocoloId = item.ProtocoloId;
                    oCotizacionProtocolo.ProtocoloNombre = item.ProtocoloNombre;

                   var lDetalle = GetProtocolComponents(ref objOperationResult, item.ProtocoloId).FindAll(p => p.r_Price !=0);

                   CotizacionProtocoloDetalle CotizacionProtocoloDetalle = null;
                   List<CotizacionProtocoloDetalle> lCotizacionProtocoloDetalle = new List<CotizacionProtocoloDetalle>();
                   foreach (var item1 in lDetalle)
                   {
                       CotizacionProtocoloDetalle = new BE.CotizacionProtocoloDetalle();
                       CotizacionProtocoloDetalle.ComponenteId = item1.v_ComponentId;
                       CotizacionProtocoloDetalle.ComponenteNombre = item1.v_ComponentName;
                       CotizacionProtocoloDetalle.costo = item1.r_Price;
                       lCotizacionProtocoloDetalle.Add(CotizacionProtocoloDetalle);
                   }
                   oCotizacionProtocolo.Detalle = lCotizacionProtocoloDetalle;

                    lFinal.Add(oCotizacionProtocolo);
                }
                return lFinal;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<OrdenReportes> GetOrdenReportes(ref OperationResult pobjOperationResult, string pstrEmpresaPlantillaId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.ordenreporte
                            where A.v_OrganizationId == pstrEmpresaPlantillaId
                            select new OrdenReportes
                            {
                                b_Seleccionar = true,
                                v_OrdenReporteId = A.v_OrdenReporteId,
                                v_ComponenteId = A.v_ComponenteId,
                                v_NombreReporte = A.v_NombreReporte,
                                i_Orden = A.i_Orden.Value,
                                v_NombreCrystal = A.v_NombreCrystal,
                                i_NombreCrystalId = A.i_NombreCrystalId
                            };

                List<Sigesoft.Node.WinClient.BE.OrdenReportes> objData = query.ToList();
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

        public void AddOrdenReportes(ref OperationResult pobjOperationResult, List<ordenreporteDto> pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in pobjDtoEntity)
                {
                    ordenreporte objEntity = ordenreporteAssembler.ToEntity(item);
                    // Autogeneramos el Pk de la tabla
                    int intNodeId = int.Parse(ClientSession[0]);
                    NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 210), "OZ");
                    objEntity.v_OrdenReporteId = NewId;
                    dbContext.AddToordenreporte(objEntity);
                }

                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
            }
        }

        public void DeleteOrdenReportes(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var Lista = (from a in dbContext.ordenreporte
                             where a.v_OrganizationId == pstrOrganizationId
                             select a).ToList();

                foreach (var item in Lista)
                {
                    dbContext.ordenreporte.DeleteObject(item);
                }
                dbContext.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
