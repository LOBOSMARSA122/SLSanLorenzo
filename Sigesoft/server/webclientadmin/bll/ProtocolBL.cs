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
    public class ProtocolBL
    {
        public  List<KeyValueDTO> GetProtocolBySystemUser(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocolsystemuser
                            join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                            where (a.i_SystemUserId == pintSystemUserId) &&
                            (a.i_IsDeleted == 0)
                            select new KeyValueDTO {
                                Id = b.v_ProtocolId,
                                Value1 = b.v_Name
                            }).Distinct().ToList();
             
                pobjOperationResult.Success = 1;
                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public string GetNameOrganizationCustomer(string pstrProtocolId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocol
                            join b in dbContext.organization on a.v_CustomerOrganizationId equals b.v_OrganizationId
                             where (a.v_ProtocolId == pstrProtocolId) &&
                            (a.i_IsDeleted == 0)
                            select new KeyValueDTO {
                                Id = b.v_OrganizationId,
                                Value1 = b.v_Name
                            }).FirstOrDefault();

                return query.Value1; 

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<OrganizationShortList> GetOrganizationCustumerByProtocolSystemUser(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocolsystemuser
                             join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                             join c in dbContext.organization on b.v_CustomerOrganizationId equals  c.v_OrganizationId
                             where (a.i_SystemUserId == pintSystemUserId) &&
                             (a.i_IsDeleted == 0)
                             select new OrganizationShortList
                             {
                               CustomerOrganizationName = c.v_Name,
                               IdEmpresaCliente = c.v_OrganizationId
                             }).ToList();

                var y = query.GroupBy(g => g.CustomerOrganizationName)
                         .Select(s => s.First());

                pobjOperationResult.Success = 1;
                return y.ToList();
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<OrganizationShortList> GetOrganizationCustumer(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocolsystemuser
                             join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                             join c in dbContext.organization on b.v_CustomerOrganizationId equals c.v_OrganizationId
                             where 
                             (a.i_IsDeleted == 0)
                             select new OrganizationShortList
                             {
                                 CustomerOrganizationName = c.v_Name,
                                 IdEmpresaCliente = c.v_OrganizationId
                             }).ToList();

                var y = query.GroupBy(g => g.CustomerOrganizationName)
                         .Select(s => s.First());

                pobjOperationResult.Success = 1;
                return y.ToList();
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
        
        public List<OrganizationShortList> DevolverTodasEmpresas(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocol
                             join b in dbContext.organization on a.v_CustomerOrganizationId equals  b.v_OrganizationId
                             where (a.i_IsDeleted == 0)
                             select new OrganizationShortList
                             {
                                 CustomerOrganizationName = b.v_Name,
                                 IdEmpresaCliente = a.v_CustomerOrganizationId
                             }).ToList();

                var y = query.GroupBy(g => g.CustomerOrganizationName)
                         .Select(s => s.First());

                pobjOperationResult.Success = 1;
                return y.ToList();
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public OrganizationShortList GetOrganizationCustumerByProtocolSystemUser_(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocolsystemuser
                             join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                             join c in dbContext.organization on b.v_CustomerOrganizationId equals c.v_OrganizationId
                             where (a.i_SystemUserId == pintSystemUserId) &&
                             (a.i_IsDeleted == 0)
                             select new OrganizationShortList
                             {
                                 CustomerOrganizationName = c.v_Name,
                                 IdEmpresaCliente = c.v_OrganizationId
                             }).FirstOrDefault();

                pobjOperationResult.Success = 1;
                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
        
        public int ObtenerRol(int pintNodeId, int pintSystemUserId)
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
                             from surn in surn_join.DefaultIfEmpty()
                            
                             select new 
                             {
                                 i_RoleId = surn.i_RoleId
                             }).FirstOrDefault();


            
                return query.i_RoleId;
            }
            catch (Exception ex)
            {
             
                return 0;
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

        public List<Sigesoft.Node.WinClient.BE.ProtocolComponentList> GetProtocolComponents(ref OperationResult pobjOperationResult, string pstrProtocolId)
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

                                 select new Sigesoft.Node.WinClient.BE.ProtocolComponentList
                                 {
                                     v_ComponentId = a.v_ComponentId,
                                     v_ComponentName = b.v_Name,
                                     //v_ServiceComponentStatusName = K.v_Value1,
                                     v_ProtocolComponentId = a.v_ProtocolComponentId,
                                     r_Price = a.r_Price,
                                     v_Operator = E.v_Value1,
                                     i_Age = a.i_Age,
                                     v_Gender = H.v_Value1,
                                     i_IsConditionalIMC = a.i_IsConditionalIMC,
                                     r_Imc = a.r_Imc,
                                     v_IsConditional = a.i_IsConditionalId == 1 ? "Si" : "No",
                                     i_isAdditional = a.i_IsAdditional,
                                     v_ComponentTypeName = I.v_Value1,
                                     i_RecordStatus = (int)RecordStatus.Grabado,
                                     i_RecordType = (int)RecordType.NoTemporal,
                                     i_GenderId = a.i_GenderId,
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

        public ProtocolSystemUserList ObtenerEmpresaPorSystemUserId(int pstrSystemUserId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.protocolsystemuser
                             join B in dbContext.protocol on A.v_ProtocolId equals B.v_ProtocolId
                          
                             where A.i_SystemUserId == pstrSystemUserId && A.i_IsDeleted == 0
                             select new ProtocolSystemUserList
                             {
                                 EmpresaId = B.v_CustomerOrganizationId + "|" + B.v_CustomerLocationId,
                                 ProtocoloId = A.v_ProtocolId
                             }).FirstOrDefault();

                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<protocolsystemuserDto> ObtenerPermisosPorProtocoloId(string pstrProtocolId, int pintSystemUserId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.protocolsystemuser
                             where A.v_ProtocolId == pstrProtocolId && A.i_IsDeleted == 0 && A.i_SystemUserId == pintSystemUserId
                             select new protocolsystemuserDto
                             {
                                 i_ApplicationHierarchyId = A.i_ApplicationHierarchyId
                             }).ToList();

                return query;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminarFisicamentePermisosPorUsuario(ref OperationResult pobjOperationResult, int pintSystemUserId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var ListobjEntity = (from a in dbContext.protocolsystemuser
                                     where a.i_SystemUserId == pintSystemUserId
                                     select a).ToList();


                foreach (var item in ListobjEntity)
                {
                    dbContext.DeleteObject(item);
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;

            }

            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.ProtocolList> DevolverProtocolosPorEmpresa(string pstrEmpresaCliente, string pstrLocationClienteId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.protocol
                             where A.v_CustomerOrganizationId == pstrEmpresaCliente && A.v_CustomerLocationId == pstrLocationClienteId && A.i_IsDeleted == 0
                             select new Sigesoft.Node.WinClient.BE.ProtocolList
                             {
                                 v_ProtocolId = A.v_ProtocolId,
                                 v_Name = A.v_Name
                             }).ToList();
                return query;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.ProtocolList> DevolverProtocolosPorEmpresaOnly(string pstrEmpresaCliente)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.protocol
                             where A.v_CustomerOrganizationId == pstrEmpresaCliente && A.i_IsDeleted == 0
                             select new Sigesoft.Node.WinClient.BE.ProtocolList
                             {
                                 v_ProtocolId = A.v_ProtocolId,
                                 v_Name =  A.v_Name
                             }).ToList();
                return query;
            }
            catch (Exception)
            {

                throw;
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

        public string ObtenerProtocoloIdPorCodigoProtocolo(string pstrCodigoProtocolo)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocol
                             where (a.v_Name == pstrCodigoProtocolo) &&
                            (a.i_IsDeleted == 0)
                             select new
                             {
                                 ProtocoloId = a.v_ProtocolId
                             }).FirstOrDefault();

                return query.ProtocoloId;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public string AddSystemUserExternal(ref OperationResult pobjOperationResult, List<protocolsystemuserDto> ListProtocolSystemUser, List<string> ClientSession, int? pintsystemUserId)
        {
            string newId = string.Empty;
            person objEntity1 = null;

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
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONA", "v_PersonId=" + objEntity1.v_PersonId, Success.Failed, ex.Message);
            }

            return newId;
        }

        public string AddOrganization(ref OperationResult pobjOperationResult, organizationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                organization objEntity = organizationAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = 9;
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 5), "OO");
                objEntity.v_OrganizationId = NewId;


                dbContext.AddToorganization(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ORGANIZACIÓN", "i_OrganizationId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }
            
        public void EliminarProtocolComponentByProtocolId(ref OperationResult pobjOperationResult, string pstrProtocolId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var ListobjEntity = (from a in dbContext.protocolcomponent
                                     where a.v_ProtocolId == pstrProtocolId
                                     select a).ToList();


                foreach (var item in ListobjEntity)
                {
                    dbContext.DeleteObject(item);
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;

            }

            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return;
            }
        }

        public string AddProtocol(ref OperationResult pobjOperationResult, protocolDto pobjProtocol, List<protocolcomponentDto> pobjProtocolComponent, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId0 = null;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                protocol objEntity = protocolAssembler.ToEntity(pobjProtocol);

                int intNodeId = 9;

                if (pobjProtocol.v_ProtocolId == null)
                {
                    objEntity.d_InsertDate = DateTime.Now;
                    objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity.i_IsDeleted = 0;
                    // Autogeneramos el Pk de la tabla

                    NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 20), "PR");
                    objEntity.v_ProtocolId = NewId0;

                    dbContext.AddToprotocol(objEntity);
                    dbContext.SaveChanges();
                }
                else
                {
                    // Obtener la entidad fuente
                    var objEntitySource = (from a in dbContext.protocol
                                           where a.v_ProtocolId == pobjProtocol.v_ProtocolId
                                           select a).FirstOrDefault();

                    // Crear la entidad con los datos actualizados
                    pobjProtocol.i_IsDeleted = 0;
                    pobjProtocol.d_UpdateDate = DateTime.Now;
                    pobjProtocol.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                    var objStrongEntity = protocolAssembler.ToEntity(pobjProtocol);

                    // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                    dbContext.protocol.ApplyCurrentValues(objStrongEntity);
                }

                // Grabar detalle del protocolo
                foreach (var item in pobjProtocolComponent)
                {
                    protocolcomponent objEntity1 = protocolcomponentAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 21), "PC");
                    objEntity1.v_ProtocolComponentId = NewId1;
                    objEntity1.v_ProtocolId = pobjProtocol.v_ProtocolId == null ? NewId0 : pobjProtocol.v_ProtocolId;

                    dbContext.AddToprotocolcomponent(objEntity1);
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;


            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log

            }

            return NewId0;
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

        public List<Sigesoft.Node.WinClient.BE.ProtocolList> GetProtocolPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
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

                            where A.i_IsDeleted == 0
                            select new Sigesoft.Node.WinClient.BE.ProtocolList
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
                                v_OrganizationId = F.v_OrganizationId + "|" + I.v_LocationId,
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
                                i_IsActive = A.i_IsActive
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

                List<Sigesoft.Node.WinClient.BE.ProtocolList> objData = query.ToList();
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

        public organizationDto GetOrganization(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                organizationDto objDtoEntity = null;

                var objEntity = (from a in dbContext.organization
                                 where a.v_OrganizationId == pstrOrganizationId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = organizationAssembler.ToDTO(objEntity);

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

        public int GetEmpresasCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.organization select a;

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


        public int GetProtocolCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.protocol select a;

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

        public List<Sigesoft.Node.WinClient.BE.OrganizationList> GetOrganizationsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.organization
                            join B in dbContext.systemparameter on new { a = A.i_OrganizationTypeId.Value, b = 103 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            //join C in dbContext.datahierarchy on new { a = A.i_SectorTypeId.Value, b = 104 } equals new { a = C.i_ItemId, b = C.i_GroupId }
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0
                            select new Sigesoft.Node.WinClient.BE.OrganizationList
                            {
                                v_OrganizationId = A.v_OrganizationId,
                                i_OrganizationTypeId = (int)A.i_OrganizationTypeId,
                                v_OrganizationTypeIdName = B.v_Value1,
                                i_SectorTypeId = (int)A.i_SectorTypeId,
                                //v_SectorTypeIdName = C.v_Value1,
                                v_Contacto = A.v_Contacto,
                                v_EmailContacto = A.v_EmailContacto,
                                v_IdentificationNumber = A.v_IdentificationNumber,
                                v_Name = A.v_Name,
                                v_Address = A.v_Address,
                                v_PhoneNumber = A.v_PhoneNumber,
                                v_Mail = A.v_Mail,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,
                                v_SectorName = A.v_SectorName,
                                v_SectorCodigo = A.v_SectorCodigo
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

                List<Sigesoft.Node.WinClient.BE.OrganizationList> objData = query.ToList();
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

        public List<Sigesoft.Node.WinClient.BE.OrdenReportes> GetOrdenReportes(ref OperationResult pobjOperationResult, string pstrEmpresaPlantillaId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.ordenreporte
                            where A.v_OrganizationId == pstrEmpresaPlantillaId
                            select new Sigesoft.Node.WinClient.BE.OrdenReportes
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
     
        public void UpdateOrganization(ref OperationResult pobjOperationResult, organizationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.organization
                                       where a.v_OrganizationId == pobjDtoEntity.v_OrganizationId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                organization objEntity = organizationAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.organization.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ORGANIZACIÓN", "i_OrganizationId=" + objEntity.v_OrganizationId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ORGANIZACIÓN", "i_OrganizationId=" + pobjDtoEntity.v_OrganizationId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
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
