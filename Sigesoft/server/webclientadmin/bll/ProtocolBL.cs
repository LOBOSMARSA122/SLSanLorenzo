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

        //public List<OrdenReportes> GetOrdenExcel(ref OperationResult pobjOperationResult, string organizationId)
        //{
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();



        //        var Lista = (from ord in dbContext.ordenexcel
        //            where ord.v_OrganizationId == organizationId
        //            select new OrdenReportes
        //            {
        //                b_Seleccionar = true,
        //                v_OrdenReporteId = ord.v_OrdenExcelId,
        //                v_ColumnaId = ord.v_KeyColumna,
        //                v_CampoId = ord.v_NombreColumna,
        //                i_Orden = ord.i_Orden.Value,
        //            }).ToList();

        //        return Lista;

        //    }
        //    catch (Exception e)
        //    {
        //        pobjOperationResult.ExceptionMessage = e.Message;
        //        return null;
        //    }
        //}

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

        public List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones> DevolverJerarquiaDxMejoradoConDescartados(List<string> ServicioIds)
        {
            try
            {
                int isDeleted = (int)SiNo.NO;
                int definitivo = (int)FinalQualification.Definitivo;
                int presuntivo = (int)FinalQualification.Presuntivo;
                int descartado = (int)FinalQualification.Descartado;

                List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones> ListaTotalJerarquizada = new List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones>();
                Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones ListaJerarquizadaDxRecomendaciones = new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones();
                List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList> ListaDxRecomendacionesPorServicio;

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
                                            select new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList
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
                                                     select new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList
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
                                             select new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones
                                             {
                                                 ServicioId = A.v_ServiceId
                                             }).ToList();

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.DetalleDxRecomendaciones = ListaDxRecoTodos.FindAll(p => p.ServicioId == a.ServicioId);
                    });



                    foreach (var item in ListaJerarquizada)
                    {
                        ListaJerarquizadaDxRecomendaciones = new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones();
                        ListaDxRecomendacionesPorServicio = new List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList>();

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
                                ListaDxRecomendacionesPorServicio.Add(new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList());
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
                                ListaDxRecomendacionesPorServicio.Add(new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList());
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

        public List<Sigesoft.Node.WinClient.BE.JerarquiaServicioCamposValores> DevolverValorCampoPorServicioMejorado(List<string> ListaServicioIds)
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
                                select new Sigesoft.Node.WinClient.BE.ValorComponenteList
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
                                 select new Sigesoft.Node.WinClient.BE.ValorComponenteList
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

                                  select new Sigesoft.Node.WinClient.BE.ValorComponenteList
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
                                         select new Sigesoft.Node.WinClient.BE.JerarquiaServicioCamposValores
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

        public List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones> DevolverJerarquiaDxMejoradoSinDescartados(List<string> ServicioIds)
        {
            try
            {
                int isDeleted = (int)SiNo.NO;
                int definitivo = (int)FinalQualification.Definitivo;
                int presuntivo = (int)FinalQualification.Presuntivo;
                int descartado = (int)FinalQualification.Descartado;

                List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones> ListaTotalJerarquizada = new List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones>();
                Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones ListaJerarquizadaDxRecomendaciones = new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones();
                List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList> ListaDxRecomendacionesPorServicio;

                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                    var ListaDxRecoTodos = (from ccc in dbContext.diagnosticrepository
                                            join bbb in dbContext.component on ccc.v_ComponentId equals bbb.v_ComponentId into J7_join
                                            from bbb in J7_join.DefaultIfEmpty()
                                            join ddd in dbContext.diseases on ccc.v_DiseasesId equals ddd.v_DiseasesId  // Diagnosticos 
                                            join eee in dbContext.service on ccc.v_ServiceId equals eee.v_ServiceId
                                            where (ccc.i_IsDeleted == isDeleted) &&
                                                (ccc.i_FinalQualificationId == definitivo ||
                                                ccc.i_FinalQualificationId == presuntivo)
                                                && ServicioIds.Contains(eee.v_ServiceId)
                                            //&& eee.d_ServiceDate < FeFin && eee.d_ServiceDate > FeIni
                                            orderby eee.v_ServiceId
                                            select new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList
                                            {
                                                ServicioId = eee.v_ServiceId,
                                                Descripcion = ddd.v_Name,
                                                IdCampo = ccc.v_ComponentFieldId,
                                                Tipo = "D",
                                                IdComponente = bbb.v_ComponentId,
                                                IdDeseases = ddd.v_DiseasesId,
                                                i_FinalQualiticationId = ccc.i_FinalQualificationId,
                                                DiseasesName = ddd.v_Name,
                                                i_DiagnosticTypeId = ccc.i_DiagnosticTypeId
                                            }).Union(from ccc in dbContext.recommendation
                                                     join ddd in dbContext.masterrecommendationrestricction on ccc.v_MasterRecommendationId equals ddd.v_MasterRecommendationRestricctionId  // Diagnosticos      
                                                     join eee in dbContext.service on ccc.v_ServiceId equals eee.v_ServiceId
                                                     where ccc.i_IsDeleted == isDeleted
                                                       && ServicioIds.Contains(eee.v_ServiceId)
                                                     orderby eee.v_ServiceId
                                                     select new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList
                                                     {
                                                         ServicioId = eee.v_ServiceId,
                                                         Descripcion = ddd.v_Name,
                                                         IdCampo = "sin nada",
                                                         Tipo = "R",
                                                         IdComponente = "sin nada",
                                                         IdDeseases = "sin nada",
                                                         i_FinalQualiticationId = 0,
                                                         DiseasesName = "sin nada",
                                                         i_DiagnosticTypeId = 0
                                                     }).ToList();



                    var ListaJerarquizada = (from A in dbContext.service
                                             where ServicioIds.Contains(A.v_ServiceId)
                                             //A.d_ServiceDate < FeFin && A.d_ServiceDate > FeIni
                                             select new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones
                                             {
                                                 ServicioId = A.v_ServiceId
                                             }).ToList();

                    ListaJerarquizada.ForEach(a =>
                    {
                        a.DetalleDxRecomendaciones = ListaDxRecoTodos.FindAll(p => p.ServicioId == a.ServicioId);
                    });



                    foreach (var item in ListaJerarquizada)
                    {
                        ListaJerarquizadaDxRecomendaciones = new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendaciones();
                        ListaDxRecomendacionesPorServicio = new List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList>();

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
                                ListaDxRecomendacionesPorServicio.Add(new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList());
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
                                ListaDxRecomendacionesPorServicio.Add(new Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList());
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

        public List<Sigesoft.Node.WinClient.BE.Antecedentes> DevolverHabitos_Personales(List<string> PersonIds)
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
                                                  select new Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList
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
                                                       select new Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList
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
                                               select new Sigesoft.Node.WinClient.BE.NoxiousHabitsList
                                               {
                                                   v_PersonId = A.v_PersonId,
                                                   i_TypeHabitsId = A.i_TypeHabitsId.Value,
                                                   v_Frequency = A.v_Frequency,
                                                   v_Comment = A.v_Comment
                                               }).ToList();


                    var ListaJerarquizada = (from A in dbContext.person
                                             where PersonIds.Contains(A.v_PersonId)

                                             select new Sigesoft.Node.WinClient.BE.Antecedentes
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

        public List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> ValoresComponenteOdontogramaValue1(string pstrServiceId, string pstrComponentId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            try
            {
                List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> serviceComponentFieldValues = (from A in dbContext.service
                                                                                     join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                                                                                     join C in dbContext.servicecomponentfields on B.v_ServiceComponentId equals C.v_ServiceComponentId
                                                                                     join D in dbContext.servicecomponentfieldvalues on C.v_ServiceComponentFieldsId equals D.v_ServiceComponentFieldsId

                                                                                     where (A.v_ServiceId == pstrServiceId)
                                                                                           && (B.v_ComponentId == pstrComponentId)
                                                                                           && (B.i_IsDeleted == 0)
                                                                                           && (C.i_IsDeleted == 0)

                                                                                     select new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList
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
        public string AntecedentesFamiliaresConcatenados(List<Sigesoft.Node.WinClient.BE.FamilyMedicalAntecedentsList> Lista)
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
        public List<Sigesoft.Node.WinClient.BE.MatrizExcel> ReporteMatrizExcel(DateTime? FechaInicio, DateTime? FechaFin, string pstrCustomerOrganizationId, string pstrFilterExpression)
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
                                    select new Sigesoft.Node.WinClient.BE.MatrizExcel
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

                               let UsaLentesNO = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CORRECTORES_OCULARES_NO_ID).ValorName : ""
                               let UsaLentesSI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CORRECTORES_OCULARES_SI_ID).ValorName :
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_DESCRIPCION_LAB).Valor : "---"

                               let IshiharaNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VISION_COLORES).ValorName :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_TEST_ISHIHARA).ValorName : "NO APLICA"

                               let IshiharaAnormal = ""

                               let EstereopsisNormal = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_TEST_VISION_ESTEREOSCOPICA).ValorName :
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FLY_TEST).ValorName : "NO APLICA"
                               let EstereopsisAnormal = ""

                               let Refraccion = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_REFRACCION).Valor : "NO APLICA"

                               let FondoOjo = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_FONDO_OJO).Valor : "NO APLICA"


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
                               let DxTGC2 = TGCD2x == null ? "No Aplica" : TGCD2x.Count == 0 ? "Normal" : string.Join("/ ", TGCD2x.Select(p => p.Descripcion))//TGCD2x.Count == 0 ? null : TGCD2x != null ? string.Join("/ ", TGCD2x.Select(p => p.Descripcion)) : "Normal"






                               let HDLDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_HDL_BIOQUIMICA_COLESTEROL_HDL)
                               let DxHDL = HDLDx == null ? "No Aplica" : HDLDx.Count == 0 ? "Normal" : string.Join("/ ", HDLDx.Select(p => p.Descripcion))

                               let LDLDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL)
                               let DxLDL = LDLDx == null ? "No Aplica" : LDLDx.Count == 0 ? "Normal" : string.Join("/ ", LDLDx.Select(p => p.Descripcion))

                               let VLDLx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL)
                               let DxVLDL = VLDLx == null ? "No Aplica" : VLDLx.Count == 0 ? "Normal" : string.Join("/ ", VLDLx.Select(p => p.Descripcion))


                               //let OrinaDx = varDxConDescartados.Find(p => p.ServicioId == a.IdServicio).DetalleDxRecomendaciones.FindAll(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID)
                               //let DxOrina = OrinaDx != null ? string.Join("/ ", OrinaDx.Select(p => p.Descripcion)) : "Normal"

                               let TGO1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGO_ID && o.IdCampo == Constants.TGO_BIOQUIMICA_TGO).Valor
                               let TGO2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGO_ID).Valor

                               let TGP1 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TGP_ID && o.IdCampo == Constants.TGP_BIOQUIMICA_TGP).Valor
                               let TGP2 = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_HEPATICO_ID && o.IdCampo == Constants.PERFIL_HEPATICO_TGP_ID).Valor

                               select new Sigesoft.Node.WinClient.BE.MatrizExcel
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

                                   VisionCercaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_DERECHO_ID).Valor :
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOD).ValorName :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOD).ValorName : "NO APLICA",

                                   VisionCercaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_SC_LEJOS_OJO_IZQUIERDO_ID).Valor :
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCSCOI).ValorName :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCSCOI).ValorName : "NO APLICA",

                                   AgudezaVisualLejosOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_DERECHO).Valor :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOD).ValorName :
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOD).ValorName : "NO APLICA",

                                   AgudezaVisualLejosOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_SC_OJO_IZQUIERDO).Valor :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLSCOI).ValorName :
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLSCOI).ValorName : "NO APLICA",


                                   VisionCercaCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_DERECHO_ID).Valor :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOD).ValorName :
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOD).ValorName : "NO APLICA",

                                   VisionCercaCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_LEJOS_OJO_IZQUIERDO_ID).Valor :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VCCCOI).ValorName :
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VCCCOI).ValorName : "NO APLICA",

                                   AgudezaVisualLejosCorregidaOD = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_AGUDEZA_VISUAL_CERCA_CC_OJO_DERECHO).Valor :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOD).ValorName :
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOD).ValorName : "NO APLICA",

                                   AgudezaVisualLejosCorregidaOI = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.OFTALMOLOGIA_ID && o.IdCampo == Constants.OFTALMOLOGIA_CC_CERCA_OJO_IZQUIERDO_ID).Valor :
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID) != null ?
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_VLCCOI).ValorName :
                                               varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID) != null ?
                                                   varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID && o.IdCampo == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_VLCCOI).ValorName : "NO APLICA",

                                   Refraccion = Refraccion,
                                   FondoOjo = FondoOjo,

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
                                   Leucocitos = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_LEUCOCITOS).Valor,
                                   DxLeucocitos = DxLeucocitos == null ? "NO APLICA" : DxLeucocitos == "" ? "NORMAL" : DxLeucocitos,
                                   Hemoglobina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_HEMOGLOBINA).Valor,

                                   DxHemoglobina = DxHemoglobina == null ? "NO APLICA" : DxHemoglobina == "" ? "NORMAL" : DxHemoglobina,
                                   Eosi = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_FORMULA_LEUCOCITARIA_EOSINOFILOS).Valor,
                                   RecuentoPlaquetas = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID && o.IdCampo == Constants.HEMOGRAMA_COMPLETO_PLAQUETAS).Valor,

                                   DxHemograma = DxHemograma == null ? "NO APLICA" : DxHemograma == "" ? "NORMAL" : DxHemograma,
                                   Glucosa = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.GLUCOSA_ID && o.IdCampo == Constants.OFTALMOLOGIA_DESCRIPCION).Valor,
                                   DxGlucosa = DxGlucosa == null ? "NO APLICA" : DxGlucosa == "" ? "NORMAL" : DxGlucosa,
                                   Colesterol = Colesterol1,
                                   DxColesterol = DxColesterol1 == "" ? "NO APLICA" : DxColesterol1 == "" ? "NORMAL" : DxColesterol1,
                                   Colesterolv2 = Colesterol2,
                                   DxColesterolLipidico = DxColesterol2 == "" ? "NO APLICA" : DxColesterol2 == "" ? "NORMAL" : DxColesterol2,

                                   Hdl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_HDL).Valor,
                                   DxHdl = DxHDL,// == "SinDx" ? "NORMAL" : DxHDL == "" ? "NORMAL" : DxHDL,
                                   Ldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_LDL_BIOQUIMICA_COLESTEROL_LDL).Valor,
                                   DxLdl = DxLDL == null ? "NO APLICA" : DxLDL == "" ? "NORMAL" : DxLDL,
                                   Vldl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.PERFIL_LIPIDICO && o.IdCampo == Constants.COLESTEROL_VLDL_BIOQUIMICA_COLESTEROL_VLDL).Valor,
                                   DxVldl = DxVLDL == null ? "NO APLICA" : DxVLDL == "" ? "NORMAL" : DxVLDL,
                                   Trigliceridos = Trigli1 == "" ? "NO APLICA" : Trigli1,
                                   DxTgc = DxTGC1 == null ? "NO APLICA" : DxTGC1 == "" ? "NORMAL" : DxTGC1,

                                   Trigliceridos2 = Trigli2 == "" ? "NO APLICA" : Trigli2,
                                   DxTgc2 = DxTGC2 == null ? "NO APLICA" : DxTGC2 == "" ? "NORMAL" : DxTGC2,

                                   Urea = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.UREA_ID && o.IdCampo == Constants.UREA_BIOQUIMICA_UREA).Valor,
                                   Creatina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.CREATININA_ID && o.IdCampo == Constants.CREATININA_BIOQUIMICA_CREATININA).Valor,

                                   Tgo = TGO1 + TGO2,
                                   Tgp = TGP1 + TGP2,
                                   Leuc = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_LEUCOCITOS).Valor,
                                   Hemat = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID && o.IdCampo == Constants.EXAMEN_COMPLETO_ORINA_MICROSCOPICO_HEMATIES).Valor,

                                   Marihuana = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) != null ?
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_MARIHUANA).ValorName :
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T && o.IdCampo == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T_MARIHUANA).ValorName : "NO APLICA",

                                   Cocaina = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID) != null ?
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID && o.IdCampo == Constants.COCAINA_MARIHUANA_TOXICOLOGICOS_COCAINA).ValorName :
                                       varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T) != null ?
                                           varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T && o.IdCampo == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T_COCAINA).ValorName : "NO APLICA",


                                   Vdrl = varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID) == null ? "NO APLICA" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).ValorName == "" ? "SIN DATOS" : varValores.Find(p => p.ServicioId == a.IdServicio).CampoValores.Find(o => o.IdComponente == Constants.VDRL_ID && o.IdCampo == Constants.LABORATORIO_VDRL_ID).ValorName,
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
                                   MotivoAptitud = a.MotivoAptitud,
                                   //ComentarioAptitud = a.AptitudId != (int)AptitudeStatus.NoApto ? a.MotivoAptitud : "",
                                   Evaluador = a.Evaluador,
                                   CMP = a.CMP,
                                   Restricciones = ConcatenateRestrictionByService(a.IdServicio)
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


        public string ObtenerDxMedicos(List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList> ListaDxOcupacionales, int Posicion)
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
                    if (CantidadRegistros - 1 >= Posicion)
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
        public int GetAge(DateTime FechaNacimiento)
        {
            return int.Parse((DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1).ToString());

        }

        public string GetCantidadAusentes(string pstrServiceId, string pstrComponentId, string pstrFieldId)
        {
            try
            {
                string retornar = "0";
                string[] componentId = null;
                ServiceBL oServiceBL = new ServiceBL();
                List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> oServiceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();

                oServiceComponentFieldValuesList = oServiceBL.ValoresComponenteOdontograma1(pstrServiceId, pstrComponentId);
                var xx = oServiceComponentFieldValuesList.Count() == 0 || ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)) == null ? string.Empty : ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;

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
        public string AntecedentesPatologicosConcatenados(List<Sigesoft.Node.WinClient.BE.PersonMedicalHistoryList> Lista)
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
        public string GetCantidadCaries(string pstrServiceId, string pstrComponentId, string pstrFieldId)
        {
            try
            {
                string Retornar = "0";
                string[] componentId = null;
                ServiceBL oServiceBL = new ServiceBL();
                List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> oServiceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();

                oServiceComponentFieldValuesList = oServiceBL.ValoresComponenteOdontograma1(pstrServiceId, pstrComponentId);
                var xx = oServiceComponentFieldValuesList.Count() == 0 || ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)) == null ? string.Empty : ((Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList)oServiceComponentFieldValuesList.Find(p => p.v_ComponentFieldId == pstrFieldId)).v_Value1;

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

        public string ObtenerDxOcupacionales(List<Sigesoft.Node.WinClient.BE.DiagnosticosRecomendacionesList> ListaDxOcupacionales, int Posicion)
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
                    if (CantidadRegistros - 1 >= Posicion)
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

    }
}
