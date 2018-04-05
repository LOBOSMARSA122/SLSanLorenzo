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
    public class NodeBL
    {
        //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

        public nodeDto GetNodeByNodeId(ref OperationResult pobjOperationResult, int pintNodeId)
        {           
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                nodeDto objDtoEntity = null;

                var objEntity = (from a in dbContext.node
                                 where a.i_NodeId == pintNodeId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = nodeAssembler.ToDTO(objEntity);

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
        
        public ICollection<nodeDto> GetAllNode(ref OperationResult pobjOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.node where a.i_IsDeleted == 0 select a;

                ICollection<nodeDto> objData = nodeAssembler.ToDTOs(query.OrderBy(p => p.v_Description).ToArray());

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

        public List<NodeList> GetNodePagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join sp1 in dbContext.systemparameter on new { i_ParameterId = n.i_NodeTypeId.Value }
                                                                   equals new { i_ParameterId = sp1.i_ParameterId } into sp1_join
                             from sp1 in sp1_join.DefaultIfEmpty()

                             join su2 in dbContext.systemuser on new { i_InsertUserId = n.i_InsertUserId.Value }
                                                           equals new { i_InsertUserId = su2.i_SystemUserId } into su2_join
                             from su2 in su2_join.DefaultIfEmpty()

                             join su3 in dbContext.systemuser on new { i_UpdateUserId = n.i_UpdateUserId.Value }
                                                           equals new { i_UpdateUserId = su3.i_SystemUserId } into su3_join
                             from su3 in su3_join.DefaultIfEmpty()
                             where n.i_IsDeleted == 0 &&
                                 //su2.i_IsDeleted == 0 && 
                                 //su3.i_IsDeleted == 0 && 
                                   sp1.i_GroupId == 105     // Tipo de Nodo

                             select new NodeList
                             {
                                 i_NodeId = n.i_NodeId,
                                 v_Description = n.v_Description,
                                 v_GeografyLocationId = n.v_GeografyLocationId,
                                 v_GeografyLocationDescription = n.v_GeografyLocationDescription,
                                 v_NodeType = sp1.v_Value1,
                                 d_BeginDate = n.d_BeginDate,
                                 d_EndDate = n.d_EndDate,
                                 d_InsertDate = n.d_InsertDate,
                                 d_UpdateDate = n.d_UpdateDate,
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

                List<NodeList> objData = query.ToList();
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

        public int GetNodeCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.node select a;

                string _filterEx = "i_IsDeleted==0";
                query = query.Where(_filterEx);

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

        public void AddNode(ref OperationResult pobjOperationResult, nodeDto pobjDtoEntity, List<string> ClientSession)
        {
            int SecuentialId = 0;
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                node objEntity = nodeAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                SecuentialId = Utils.GetNextSecuentialId(1, 4);
                objEntity.i_NodeId = SecuentialId;

                dbContext.AddTonode(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "NODO", "i_NodeId=" + objEntity.i_NodeId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "NODO", "i_NodeId=" + pobjDtoEntity.i_NodeId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateNode(ref OperationResult pobjOperationResult, nodeDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.node
                                       where a.i_NodeId == pobjDtoEntity.i_NodeId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                node objEntity = nodeAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.node.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "NODO", "i_NodeId=" + objEntity.i_NodeId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "NODO", "i_NodeId=" + pobjDtoEntity.i_NodeId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteNode(ref OperationResult pobjOperationResult, int pintNodeId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.node
                                       where a.i_NodeId == pintNodeId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "NODO", "i_NodeId=" + objEntitySource.i_NodeId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "NODO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        #region NodeOrganization

        private bool IsWarehouseAssignedToNode(ref OperationResult pobjOperationResult, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseList)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            // Validar que un almacen solo sea asignado a un solo nodo
            foreach (var item in pobjWarehouseList)
            {
                var query = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                             join b in dbContext.node on a.i_NodeId equals b.i_NodeId
                             join c in dbContext.warehouse on a.v_WarehouseId equals c.v_WarehouseId
                             where a.v_WarehouseId == item.v_WarehouseId &&
                             a.i_IsDeleted == 0
                             select new 
                             {
                                v_NodeName = b.v_Description,
                                v_WarehouseName = c.v_Name
                             }).FirstOrDefault();

                if (query != null)
                {
                    pobjOperationResult.ErrorMessage = string.Format("El Almacén <font color='red'> {0} </font> ya se encuentra asignado al nodo <font color='red'> {1} </font>. Por favor elija otro.", 
                                                                                    query.v_WarehouseName,
                                                                                    query.v_NodeName);
                    pobjOperationResult.Success = 1;
                    return true;
                }
            }

            return false;
        }
        
        public void AddNodeOrganizationLoactionWarehouse(ref OperationResult pobjOperationResult, NodeOrganizationLoactionWarehouseList pobjNodeOrgLocationWarehouse, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseList, List<string> ClientSession)
        {
            //mon.IsActive = true;
           
            nodeorganizationprofile objNodeorganizationprofile = new nodeorganizationprofile();
            nodeorganizationlocationprofile objNodeorganizationlocationprofile = new nodeorganizationlocationprofile();

            try
            {
                           
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                OperationResult objOperationResult5 = new OperationResult();

                if (pobjWarehouseList != null)
                {
                    if (IsWarehouseAssignedToNode(ref objOperationResult5, pobjWarehouseList))
                    {
                        pobjOperationResult = objOperationResult5;
                        return;
                    }
                }

                var objEntitySource = (from a in dbContext.nodeorganizationlocationprofile
                                       join c in dbContext.nodeorganizationlocationprofile on a.i_NodeId equals c.i_NodeId
                                        where a.i_NodeId == pobjNodeOrgLocationWarehouse.i_NodeId &&
                                        a.v_OrganizationId == pobjNodeOrgLocationWarehouse.v_OrganizationId &&
                                        c.v_LocationId == pobjNodeOrgLocationWarehouse.v_LocationId
                                        select a).FirstOrDefault();

                if (objEntitySource != null)
                {
                    // Actualizar registro (dar de alta al registro ya existente "no volver a insertar")
                    OperationResult objOperationResult2 = new OperationResult();

                    UpdateNodeOrganizationChangeStatusAll(ref objOperationResult2,
                                                            pobjNodeOrgLocationWarehouse.i_NodeId,
                                                            pobjNodeOrgLocationWarehouse.v_OrganizationId,
                                                            pobjNodeOrgLocationWarehouse.v_LocationId,
                                                            0,
                                                            ClientSession);
                 
                    pobjOperationResult = objOperationResult2;
                }
                else
                {
                    var query = (from a in dbContext.nodeorganizationlocationprofile                                         
                                where a.i_NodeId == pobjNodeOrgLocationWarehouse.i_NodeId &&
                                a.v_OrganizationId == pobjNodeOrgLocationWarehouse.v_OrganizationId                                         
                                select a).FirstOrDefault();

                    // Grabar nuevo

                    if (query == null)
                    {                       
                        #region Nodeorganization
                        // Grabar nodo / empresa
                        objNodeorganizationprofile.d_InsertDate = DateTime.Now;
                        objNodeorganizationprofile.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objNodeorganizationprofile.i_IsDeleted = 0;
                        objNodeorganizationprofile.i_NodeId = pobjNodeOrgLocationWarehouse.i_NodeId;
                        objNodeorganizationprofile.v_OrganizationId = pobjNodeOrgLocationWarehouse.v_OrganizationId;

                        dbContext.AddTonodeorganizationprofile(objNodeorganizationprofile);
                        dbContext.SaveChanges();
                        #endregion

                        #region Nodeorganizationlocation
                        // Grabar nodo / empresa / sede

                        objNodeorganizationlocationprofile.d_InsertDate = DateTime.Now;
                        objNodeorganizationlocationprofile.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objNodeorganizationlocationprofile.i_IsDeleted = 0;
                        objNodeorganizationlocationprofile.i_NodeId = pobjNodeOrgLocationWarehouse.i_NodeId;
                        objNodeorganizationlocationprofile.v_OrganizationId = pobjNodeOrgLocationWarehouse.v_OrganizationId;
                        objNodeorganizationlocationprofile.v_LocationId = pobjNodeOrgLocationWarehouse.v_LocationId;

                        dbContext.AddTonodeorganizationlocationprofile(objNodeorganizationlocationprofile);
                        dbContext.SaveChanges();
                        #endregion

                        #region Add Warehouse

                        // Graba almacenes
                        OperationResult objOperationResult1 = new OperationResult();

                        if (pobjWarehouseList != null)
                        {
                            AddWarehouse(ref objOperationResult1, pobjWarehouseList, ClientSession);
                        }

                        #endregion
                    }
                    else
                    {
                        #region Nodeorganizationlocation
                        // Grabar nodo / empresa / sede

                        objNodeorganizationlocationprofile.d_InsertDate = DateTime.Now;
                        objNodeorganizationlocationprofile.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objNodeorganizationlocationprofile.i_IsDeleted = 0;
                        objNodeorganizationlocationprofile.i_NodeId = pobjNodeOrgLocationWarehouse.i_NodeId;
                        objNodeorganizationlocationprofile.v_OrganizationId = pobjNodeOrgLocationWarehouse.v_OrganizationId;
                        objNodeorganizationlocationprofile.v_LocationId = pobjNodeOrgLocationWarehouse.v_LocationId;

                        dbContext.AddTonodeorganizationlocationprofile(objNodeorganizationlocationprofile);
                        dbContext.SaveChanges();
                        #endregion

                        #region Add Warehouse

                        // Graba almacenes
                        OperationResult objOperationResult1 = new OperationResult();

                        if (pobjWarehouseList != null)
                        {
                            AddWarehouse(ref objOperationResult1, pobjWarehouseList, ClientSession);
                        }

                        #endregion
                    }
                    
                }
                         
                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EMPRESA / SEDE / ALMACÉN", "v_OrganizationId=" + objNodeorganizationprofile.v_OrganizationId, Success.Ok, null);
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EMPRESA / SEDE / ALMACÉN", "v_OrganizationId=" + objNodeorganizationprofile.v_OrganizationId, Success.Failed, pobjOperationResult.ExceptionMessage);
            }
           
        }

        public void UpdateNodeOrganizationLoactionWarehouse(ref OperationResult pobjOperationResult, NodeOrganizationLoactionWarehouseList pobjNodeOrgLocationWarehouse, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseListAdd, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseListDel, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Warehouse -> Add / Del

                // Eliminar Almacenes              
                if (pobjWarehouseListDel != null)
                {
                    OperationResult objOperationResult1 = new OperationResult();
                    DeleteWarehouse(ref objOperationResult1, pobjWarehouseListDel, ClientSession);
                }

                // Graba almacenes
                if (pobjWarehouseListAdd != null)
                {
                    OperationResult objOperationResult3 = new OperationResult();
                    if (IsWarehouseAssignedToNode(ref objOperationResult3, pobjWarehouseListAdd))
                    {
                        pobjOperationResult = objOperationResult3;
                        return;
                    }

                    OperationResult objOperationResult2 = new OperationResult();
                    AddWarehouse(ref objOperationResult2, pobjWarehouseListAdd, ClientSession);
                }

                #endregion
                              
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EMPRESA / SEDE / ALMACÉN", "v_OrganizationId=" + pobjNodeOrgLocationWarehouse.v_OrganizationId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EMPRESA / SEDE / ALMACÉN", "v_OrganizationId=" + pobjNodeOrgLocationWarehouse.v_OrganizationId, Success.Failed, pobjOperationResult.ExceptionMessage);
            }
        }
        
        public List<NodeOrganizationLoactionWarehouseList> GetNodeOrganization(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationName)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId}
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from j1 in j1_join.DefaultIfEmpty()
                             join b in dbContext.organization on j1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             orderby b.v_Name,c.v_Name
                             where n.i_NodeId == pintNodeId && n.i_IsDeleted == 0 &&
                             //j1.i_IsDeleted == 0 &&
                             a.i_IsDeleted == 0 &&
                             b.v_Name.Contains(pstrOrganizationName)
                             select new NodeOrganizationLoactionWarehouseList
                             {
                                v_OrganizationName = b.v_Name,
                                v_LocationName = c.v_Name,
                                v_LocationId = c.v_LocationId,
                                v_OrganizationId = b.v_OrganizationId,
                                i_NodeId = n.i_NodeId,   
                             }
                           );

                var q = (from a in query.ToList()
                         select new NodeOrganizationLoactionWarehouseList
                         {
                             v_OrganizationName = a.v_OrganizationName,
                             v_LocationName = a.v_LocationName,
                             v_LocationId = a.v_LocationId,
                             v_OrganizationId = a.v_OrganizationId,
                             i_NodeId = a.i_NodeId,
                             v_WarehouseName = dd(a.i_NodeId, a.v_OrganizationId, a.v_LocationId)
                         });

                List<NodeOrganizationLoactionWarehouseList> objData = q.ToList();

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

        private string dd(int pintNodeId, string pstrOrganizationId, string pstrLocationId)
        { 
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              var qry = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                         join b in dbContext.warehouse on a.v_WarehouseId equals b.v_WarehouseId
                         where a.i_NodeId == pintNodeId &&
                         a.v_OrganizationId == pstrOrganizationId &&
                         a.v_LocationId == pstrLocationId &&
                         a.i_IsDeleted == 0
                         select new
                         {
                             v_Warehouse = b.v_Name
                         }).ToList();

              return string.Join(", ", qry.Select(p => p.v_Warehouse));
        }

        private void DeleteWarehouse(ref OperationResult pobjOperationResult, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseList, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in pobjWarehouseList)
                {
                    var objEntitySource = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                                           where a.i_NodeId == item.i_NodeId &&
                                                 a.v_OrganizationId == item.v_OrganizationId &&
                                                 a.v_LocationId == item.v_LocationId &&
                                                 a.v_WarehouseId == item.v_WarehouseId
                                           select a).FirstOrDefault();

                    objEntitySource.d_UpdateDate = DateTime.Now;
                    objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objEntitySource.i_IsDeleted = 1;

                    // Guardar los cambios
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        private void AddWarehouse(ref OperationResult pobjOperationResult, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseList, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                nodeorganizationlocationwarehouseprofile objnodeorganizationlocationwarehouseprofile = null;
          
                // Grabar almacén
                foreach (var item in pobjWarehouseList)
                {
                    var objEntitySource = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                                           where a.i_NodeId == item.i_NodeId &&
                                                 a.v_OrganizationId == item.v_OrganizationId &&
                                                 a.v_LocationId == item.v_LocationId &&
                                                 a.v_WarehouseId == item.v_WarehouseId
                                           select a).FirstOrDefault();

                    if (objEntitySource != null)
                    {
                        objEntitySource.d_UpdateDate = DateTime.Now;
                        objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource.i_IsDeleted = 0;

                        // Guardar los cambios
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        objnodeorganizationlocationwarehouseprofile = nodeorganizationlocationwarehouseprofileAssembler.ToEntity(item);
                        objnodeorganizationlocationwarehouseprofile.d_InsertDate = DateTime.Now;
                        objnodeorganizationlocationwarehouseprofile.i_InsertUserId = int.Parse(ClientSession[2]);
                        objnodeorganizationlocationwarehouseprofile.i_IsDeleted = 0;

                        dbContext.AddTonodeorganizationlocationwarehouseprofile(objnodeorganizationlocationwarehouseprofile);
                        dbContext.SaveChanges();
                    }
                                 
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateNodeOrganizationChangeStatusAll(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId,string pstrLocationId, int pintIsDeleted, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region nodeOrganization
                // Obtener la entidad fuente
                var objnodeOrganization = (from a in dbContext.nodeorganizationprofile
                                           where a.i_NodeId == pintNodeId &&
                                           a.v_OrganizationId == pstrOrganizationId
                                           select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objnodeOrganization.d_UpdateDate = DateTime.Now;
                objnodeOrganization.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objnodeOrganization.i_IsDeleted = pintIsDeleted;
                
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region nodeOrganizationLocation
                // Obtener la entidad fuente
                var objnodeOrganizationLocation = (from a in dbContext.nodeorganizationlocationprofile
                                                   where a.i_NodeId == pintNodeId &&
                                                   a.v_OrganizationId == pstrOrganizationId  &&
                                                   a.v_LocationId == pstrLocationId
                                                   select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objnodeOrganizationLocation.d_UpdateDate = DateTime.Now;
                objnodeOrganizationLocation.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objnodeOrganizationLocation.i_IsDeleted = pintIsDeleted;
               
                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Warehouse
                            
                // Obtener la entidad fuente
                var objWarehouse = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                                                   where a.i_NodeId == pintNodeId &&
                                                   a.v_OrganizationId == pstrOrganizationId &&
                                                   a.v_LocationId == pstrLocationId
                                                   select a).ToList();
                if (objWarehouse != null)
                {
                    foreach (var item in objWarehouse)
                    {
                        item.d_UpdateDate = DateTime.Now;
                        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        item.i_IsDeleted = pintIsDeleted;                    
                    }

                    // Guardar los cambios
                    dbContext.SaveChanges();
                }
                #endregion

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EMPRESA / SEDE / ALMACÉN", "v_OrganizationId=" + pstrOrganizationId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EMPRESA / SEDE / ALMACÉN", "v_OrganizationId=" + pstrOrganizationId, Success.Failed, pobjOperationResult.ExceptionMessage);
            }
        }

        public int GetNodeOrganizationCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from a in dbContext.nodeorganizationprofile
                             join c in dbContext.nodeorganizationlocationprofile on a.i_NodeId equals c.i_NodeId
                             select new 
                             { 
                                i_NodeId = a.i_NodeId, 
                                v_OrganizationId = a.v_OrganizationId,
                                v_LocationId = c.v_LocationId,
                                i_IsDeleted = a.i_IsDeleted
                             });

                string filterEx = "i_IsDeleted==0";
                query = query.Where(filterEx);

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

        public int GetNodeOrganizationLocationWarehouseProfileCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.nodeorganizationlocationwarehouseprofile select a;

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
      
        #endregion

        #region NodeRole

        public List<RoleNodeList> GetRoleNode(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.rolenode
                             join b in dbContext.node on a.i_NodeId equals b.i_NodeId
                             join J2 in dbContext.systemparameter on new { a = a.i_RoleId, b = 115 }
                                                                       equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where a.i_NodeId == pintNodeId && a.i_IsDeleted == 0                                
                             select new RoleNodeList
                             {
                                i_NodeId = b.i_NodeId,
                                i_RoleId = a.i_RoleId,
                                v_RoleName = J2.v_Value1,
                                v_NodeName = b.v_Description
                             }
                           );

                List<RoleNodeList> objData = query.ToList();

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

        public void UpdateRoleNodeChangeStatus(ref OperationResult pobjOperationResult, int pintNodeId, int pintRoleId, int pintIsDeleted, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region RoleNode
                // Obtener la entidad fuente
                var objrolenode = (from a in dbContext.rolenode
                                    where a.i_NodeId == pintNodeId &&
                                    a.i_RoleId == pintRoleId
                                    select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objrolenode.d_UpdateDate = DateTime.Now;
                objrolenode.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objrolenode.i_IsDeleted = pintIsDeleted;

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion   

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "Nodo / Rol", "i_NodeId=" + pintNodeId + "i_RoleId=" + pintRoleId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "Nodo / Rol", "i_NodeId=" + pintNodeId + "i_RoleId=" + pintRoleId, Success.Failed, ex.Message);
            }
        }

        public void DeleteRoleAll(ref OperationResult pobjOperationResult, int pintNodeId, int pintRoleId, int pintIsDeleted, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region RoleNode
                // Obtener la entidad fuente
                var objrolenode = (from a in dbContext.rolenode
                                   where a.i_NodeId == pintNodeId &&
                                   a.i_RoleId == pintRoleId
                                   select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objrolenode.d_UpdateDate = DateTime.Now;
                objrolenode.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objrolenode.i_IsDeleted = pintIsDeleted;

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region RoleNodeProfile
                // Obtener la entidad fuente
                var objroleNodeProfile = (from a in dbContext.rolenodeprofile
                                          where a.i_NodeId == pintNodeId &&
                                          a.i_RoleId == pintRoleId
                                          select a).ToList();

                if (objroleNodeProfile != null)
                {
                    foreach (var item in objroleNodeProfile)
                    {
                        item.d_UpdateDate = DateTime.Now;
                        item.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        item.i_IsDeleted = pintIsDeleted;
                    }

                    // Guardar los cambios
                    dbContext.SaveChanges();
                }
                #endregion

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "Nodo / Rol", "i_NodeId=" + pintNodeId + "i_RoleId=" + pintRoleId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "Nodo / Rol", "i_NodeId=" + pintNodeId + "i_RoleId=" + pintRoleId, Success.Failed, pobjOperationResult.ExceptionMessage);
            }
        }
      
        public int GetRoleNodeCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.rolenode select a;

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

        #endregion

        #region RoleNodeProfile

        public void UpdateRoleNodeProfileChangeStatus(ref OperationResult pobjOperationResult, int pintNodeId, int pintRoleId, int pintApplicationHierarchyId, int pintIsDeleted, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();         

                #region RoleNodeProfile
                // Obtener la entidad fuente
                var objroleNodeProfile = (from a in dbContext.rolenodeprofile
                                          where a.i_NodeId == pintNodeId &&
                                          a.i_RoleId == pintRoleId &&
                                          a.i_ApplicationHierarchyId == pintApplicationHierarchyId
                                          select a).FirstOrDefault();

                if (objroleNodeProfile != null)
                {
                    objroleNodeProfile.d_UpdateDate = DateTime.Now;
                    objroleNodeProfile.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objroleNodeProfile.i_IsDeleted = pintIsDeleted;
                   
                    // Guardar los cambios
                    dbContext.SaveChanges();
                }
                #endregion

                pobjOperationResult.Success = 1;
                //// Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "Nodo / Rol", "i_NodeId=" + pintNodeId + "i_RoleId=" + pintRoleId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "Nodo / Rol", "i_NodeId=" + pintNodeId + "i_RoleId=" + pintRoleId, Success.Failed, pobjOperationResult.ExceptionMessage);
            }
        }

        private void DeleteRoleNodeProfile(ref OperationResult pobjOperationResult, List<rolenodeprofileDto> ListRoleNodeProfileDto, List<string> ClientSession)
        {

            //mon.IsActive = true;

            try 
	        {	        
                 SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                foreach (var item in ListRoleNodeProfileDto)
                {
                    // Obtener la entidad fuente
                    var objroleNodeProfile = (from a in dbContext.rolenodeprofile
                                              where a.i_NodeId == item.i_NodeId &&
                                              a.i_RoleId == item.i_RoleId &&
                                              a.i_ApplicationHierarchyId == item.i_ApplicationHierarchyId
                                              select a).FirstOrDefault();

                    objroleNodeProfile.d_UpdateDate = DateTime.Now;
                    objroleNodeProfile.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    objroleNodeProfile.i_IsDeleted = 1;
                }
           
                 pobjOperationResult.Success = 1;

                // Guardar los cambios
                dbContext.SaveChanges();
            }
	        catch (Exception ex)
	        {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
		      
	        }

        }

        public void AddRoleNodeProfile(ref OperationResult pobjOperationResult, rolenodeDto pobjRoleNode, List<rolenodeprofileDto> ListRoleNodeProfile, List<string> ClientSession, bool pbRegisterLog)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region RoleNode

                if (pobjRoleNode != null)
                {
                    var objrolenode = (from a in dbContext.rolenode
                                       where a.i_NodeId == pobjRoleNode.i_NodeId &&
                                       a.i_RoleId == pobjRoleNode.i_RoleId
                                       select a).FirstOrDefault();

                    if (objrolenode != null)
                    {
                        if (objrolenode.i_IsDeleted == 1)
                        {
                            OperationResult objOperationResult = new OperationResult();
                            UpdateRoleNodeChangeStatus(ref objOperationResult, pobjRoleNode.i_NodeId, pobjRoleNode.i_RoleId, 0, ClientSession);
                        }
                    }
                    else
                    {
                        // Grabar como nuevo
                        var objRoleNode = rolenodeAssembler.ToEntity(pobjRoleNode);

                        objRoleNode.d_InsertDate = DateTime.Now;
                        objRoleNode.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objRoleNode.i_IsDeleted = 0;
                        dbContext.AddTorolenode(objRoleNode);
                        dbContext.SaveChanges();
                    }
                }

                #endregion

                #region RoleNodeProfile

                foreach (var item in ListRoleNodeProfile)
                {
                    var objroleNodeProfile = (from a in dbContext.rolenodeprofile
                                              where a.i_NodeId == item.i_NodeId &&
                                              a.i_RoleId == item.i_RoleId &&
                                              a.i_ApplicationHierarchyId == item.i_ApplicationHierarchyId
                                              select a).FirstOrDefault();

                    if (objroleNodeProfile != null)
                    {
                        if (objroleNodeProfile.i_IsDeleted == 1)
                        {
                            OperationResult objOperationResult = new OperationResult();
                            UpdateRoleNodeProfileChangeStatus(ref objOperationResult, item.i_NodeId, item.i_RoleId,item.i_ApplicationHierarchyId, 0, ClientSession); 
                        }
                    }
                    else
                    {
                        // Grabar como nuevo
                        var objEntity = rolenodeprofileAssembler.ToEntity(item);

                        objEntity.d_InsertDate = DateTime.Now;
                        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objEntity.i_IsDeleted = 0;
                        dbContext.AddTorolenodeprofile(objEntity);
                        dbContext.SaveChanges();
                    }                  
                }

                #endregion
              
                pobjOperationResult.Success = 1;

                if (pbRegisterLog == true)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "Nodo / Rol", "i_NodeId=" + pobjRoleNode.i_NodeId + "i_RoleId=" + pobjRoleNode.i_RoleId, Success.Ok, null);
                }

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "Nodo / Rol", "i_NodeId=" + pobjRoleNode.i_NodeId + "i_RoleId=" + pobjRoleNode.i_RoleId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        public int GetRoleNodeProfileCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.rolenodeprofile select a;

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

        public void UpdateRoleNodeProfile(ref OperationResult pobjOperationResult, List<rolenodeprofileDto> ListRoleNodeProfileUpdate, List<rolenodeprofileDto> ListRoleNodeProfileDelete, List<string> ClientSession)
        {
            OperationResult operationResult1 = new OperationResult();
            OperationResult operationResult2 = new OperationResult();

            try
            {
                if (ListRoleNodeProfileUpdate != null)
                {
                    AddRoleNodeProfile(ref operationResult1,null, ListRoleNodeProfileUpdate, ClientSession, false);
                }

                if (ListRoleNodeProfileDelete != null)
                {
                    DeleteRoleNodeProfile(ref operationResult2, ListRoleNodeProfileDelete, ClientSession);
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "NodoRolProfile", string.Empty, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "NodoRolProfile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public List<KeyValueDTO> GetRoleNodeProfile(ref OperationResult pobjOperationResult, int pintNodeId, int pintRoleId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.rolenodeprofile
                             join b in dbContext.applicationhierarchy on a.i_ApplicationHierarchyId equals b.i_ApplicationHierarchyId                          
                             where a.i_NodeId == pintNodeId && a.i_RoleId == pintRoleId &&
                             b.i_IsDeleted == 0 && a.i_IsDeleted == 0
                             select new 
                             {
                                 Id = b.i_ApplicationHierarchyId,
                                 Value1 = b.v_Description
                             }
                           ).ToList();

                var q = (from a in query
                         select new KeyValueDTO
                        {
                            Id = a.Id.ToString(),
                            Value1 = a.Value1
                        }).ToList();

                pobjOperationResult.Success = 1;
                return q;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        #endregion

        #region RoleNodeComponentProfile

        public List<RoleNodeComponentProfileList> GetRoleNodeComponentProfileForGridView(ref OperationResult pobjOperationResult, int pintNodeId, int pintRoleId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.rolenodecomponentprofile
                             join b in dbContext.component on a.v_ComponentId equals b.v_ComponentId
                             where a.i_NodeId == pintNodeId && 
                             a.i_RoleId == pintRoleId && 
                             a.i_IsDeleted == 0
                             select new RoleNodeComponentProfileList
                             {
                                 v_RoleNodeComponentId = a.v_RoleNodeComponentId,
                                 i_NodeId = a.i_NodeId,
                                 i_RoleId = a.i_RoleId,
                                 v_ComponentName = b.v_Name,
                                 v_Read = a.i_Read == 1 ? "Si" : "No",
                                 v_Write = a.i_Write == 1 ? "Si" : "No",
                                 v_Dx = a.i_Dx == 1 ? "Si" : "No",
                                 v_Approved = a.i_Approved == 1 ? "Si" : "No"
                             }
                           );

                List<RoleNodeComponentProfileList> objData = query.ToList();

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

        public void DeleteRoleNodeComponentProfile(ref OperationResult pobjOperationResult, string pstrRoleNodeComponentId, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.rolenodecomponentprofile
                                       where a.v_RoleNodeComponentId == pstrRoleNodeComponentId                                            
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

        public void AddRoleNodeComponentProfile(ref OperationResult pobjOperationResult, rolenodecomponentprofileDto RoleNodeComponentProfile, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.rolenodecomponentprofile
                                       where a.i_NodeId == RoleNodeComponentProfile.i_NodeId &&
                                             a.i_RoleId == RoleNodeComponentProfile.i_RoleId &&
                                             a.v_ComponentId == RoleNodeComponentProfile.v_ComponentId                                           
                                       select a).FirstOrDefault();

                if (objEntitySource != null)
                {
                    if (objEntitySource.i_IsDeleted == 0)   // Registro ya esta grabado
                    {
                        // validar que no se vuelva a registrar datos ya existentes
                        pobjOperationResult.ErrorMessage = "Este Componente ya existe para este Nodo / Rol, agregue otro por favor.)";
                        pobjOperationResult.Success = 1;
                        return;
                    }
                    else if (objEntitySource.i_IsDeleted == 1)  // Registro macado como eliminado
                    {
                        // Actualizar registro (dar de alta al registro ya existente "no volver a insertar")
                        OperationResult objOperationResult2 = new OperationResult();

                        RoleNodeComponentProfile.v_RoleNodeComponentId = objEntitySource.v_RoleNodeComponentId;

                        UpdateStatusRoleNodeComponentProfile(ref objOperationResult2,
                                                 RoleNodeComponentProfile,                                               
                                                 ClientSession);

                        pobjOperationResult = objOperationResult2;
                        return;
                    }
                }
                else
                {
                    // Autogeneramos el Pk de la tabla
                    int intNodeId = int.Parse(ClientSession[0]);
                    var newId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 26), "RC"); ;

                    // Grabar nuevo registro
                    rolenodecomponentprofile objEntity;

                    objEntity = rolenodecomponentprofileAssembler.ToEntity(RoleNodeComponentProfile);
                    objEntity.v_RoleNodeComponentId = newId;                  
                    objEntity.d_InsertDate = DateTime.Now;
                    objEntity.i_InsertUserId = int.Parse(ClientSession[2]);
                    objEntity.i_IsDeleted = 0;

                    dbContext.AddTorolenodecomponentprofile(objEntity);

                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;

                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                        "rolenodecomponentprofile", string.Empty, Success.Ok, null);

                    return;
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION,
                  "rolenodecomponentprofile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }

        }

        private void UpdateStatusRoleNodeComponentProfile(ref OperationResult pobjOperationResult, rolenodecomponentprofileDto RoleNodeComponentProfile, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.rolenodecomponentprofile
                                       where a.v_RoleNodeComponentId == RoleNodeComponentProfile.v_RoleNodeComponentId
                                       select a).FirstOrDefault();
               
                RoleNodeComponentProfile.d_InsertDate = objEntitySource.d_InsertDate;
                RoleNodeComponentProfile.i_InsertUserId = objEntitySource.i_InsertUserId;
                RoleNodeComponentProfile.d_UpdateDate = DateTime.Now;
                RoleNodeComponentProfile.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                RoleNodeComponentProfile.i_IsDeleted = 0;

                var objStrongEntity = rolenodecomponentprofileAssembler.ToEntity(RoleNodeComponentProfile);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.rolenodecomponentprofile.ApplyCurrentValues(objStrongEntity);   

                // Guardar los cambios
                dbContext.SaveChanges();
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                   "rolenodecomponentprofile", string.Format("v_RoleNodeComponentId={0}", RoleNodeComponentProfile.v_RoleNodeComponentId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION,
                "rolenodecomponentprofile", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public rolenodecomponentprofileDto GetRoleNodeComponentProfile(ref OperationResult pobjOperationResult, string pstrRoleNodeComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                rolenodecomponentprofileDto dtoEntity = null;

                var query = (from a in dbContext.rolenodecomponentprofile
                             where a.v_RoleNodeComponentId == pstrRoleNodeComponentId                                                      
                             select a).FirstOrDefault();

                if (query != null)                                            
                     dtoEntity = rolenodecomponentprofileAssembler.ToDTO(query);

                pobjOperationResult.Success = 1;
                return dtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void UpdateRoleNodeComponentProfile(ref OperationResult pobjOperationResult, rolenodecomponentprofileDto RoleNodeComponentProfile, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();            

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.rolenodecomponentprofile
                                       where a.v_RoleNodeComponentId == RoleNodeComponentProfile.v_RoleNodeComponentId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                RoleNodeComponentProfile.d_UpdateDate = DateTime.Now;
                RoleNodeComponentProfile.i_UpdateUserId = Int32.Parse(ClientSession[2]);               

                var objStrongEntity = rolenodecomponentprofileAssembler.ToEntity(RoleNodeComponentProfile);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.rolenodecomponentprofile.ApplyCurrentValues(objStrongEntity);             
              
                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "rolenodecomponentprofile", "v_RoleNodeComponentId=" + RoleNodeComponentProfile.v_RoleNodeComponentId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "rolenodecomponentprofile", "v_RoleNodeComponentId=" + RoleNodeComponentProfile.v_RoleNodeComponentId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }
        

        #endregion

        #region Attention in Area

        public List<AttentionInAreaList> GetAttentionInAreaByNodePagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.attentioninarea
                             join B in dbContext.node on A.i_NodeId equals B.i_NodeId
                             //join C in dbContext.component on A.v_ComponentId equals C.v_ComponentId
                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                             equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where A.i_IsDeleted == 0

                             select new AttentionInAreaList
                             {
                                 v_AttentionInAreaId = A.v_AttentionInAreaId,
                                 i_NodeId = A.i_NodeId,
                                 v_NodeName = B.v_Description,
                                 //v_ComponentId = A.v_ComponentId,
                                 //v_ComponentName = C.v_Name,
                                 v_Name = A.v_Name,
                                 v_OfficeNumber = A.v_OfficeNumber,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate
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

                List<AttentionInAreaList> objData = query.ToList();
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

        public int GetAttentionInAreaByNodeCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.attentioninarea select a;

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

        public attentioninareaDto GetAttentionInAreaByNode(ref OperationResult pobjOperationResult, string pstrAttentionInAreaId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                attentioninareaDto objDtoEntity = null;

                var objEntity = (from a in dbContext.attentioninarea
                                 where a.v_AttentionInAreaId == pstrAttentionInAreaId 
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = attentioninareaAssembler.ToDTO(objEntity);

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

        public string AddAttentionInAreaByNode(ref OperationResult pobjOperationResult, attentioninareaDto pobjAttentionInArea, List<attentioninareacomponentDto> pobjAttentionInAreaComponentAdd  ,List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            string AttentioninAreaComponentId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();              

                #region AttentionInArea
                attentioninarea objEntity = attentioninareaAssembler.ToEntity(pobjAttentionInArea);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 26), "AA"); ;
                objEntity.v_AttentionInAreaId = NewId;
                objEntity.i_NodeId = intNodeId;

                dbContext.AddToattentioninarea(objEntity);
                dbContext.SaveChanges();

                #endregion

                #region AttentionInAreaComponent

                foreach (var item in pobjAttentionInAreaComponentAdd)
                {
                    attentioninareacomponent objEntity1 = new attentioninareacomponent();
                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    objEntity1.v_AttentionInAreaId = NewId;
                    objEntity1.v_ComponentId = item.v_ComponentId;
                    // Autogeneramos el Pk de la tabla                 

                    AttentioninAreaComponentId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 47), "AC"); ;
                    objEntity1.v_AttentioninAreaComponentId = AttentioninAreaComponentId;

                    dbContext.AddToattentioninareacomponent(objEntity1);
                    dbContext.SaveChanges();
                }


                #endregion

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + NewId.ToString() + " / Descripción = " + objEntity.v_Name, Success.Ok, null);

                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);               
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + NewId.ToString() + " / Descripción = " + pobjAttentionInArea.v_Name, Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public void UpdateAttentionInAreaByNode(ref OperationResult pobjOperationResult, attentioninareaDto pobjDtoEntity, List<attentioninareacomponentDto> pobjAddAttentionAreaComponentList, List<attentioninareacomponentDto> objDeleteAttentionAreaComponentList, string pstrAttentionInAreaId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region AttentionInArea
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.attentioninarea
                                       where a.v_AttentionInAreaId == pobjDtoEntity.v_AttentionInAreaId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                attentioninarea objEntity = attentioninareaAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.attentioninarea.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                //Eliminar Componentes

                if (objDeleteAttentionAreaComponentList != null)
                {
                    OperationResult objOperationResult1 = new OperationResult();

                    foreach (var item in objDeleteAttentionAreaComponentList)
                    {

                        var x = GetAttentionInAreaComponentByComponentId(ref objOperationResult1, item.v_ComponentId);

                        DeleteAttentionInAreaComponent(ref objOperationResult1, x.v_AttentioninAreaComponentId, ClientSession);
                    }
                    
                }

                //Grabar Componentes

                if (pobjAddAttentionAreaComponentList != null)
                {
                    OperationResult objOperationResult2 = new OperationResult();

                    foreach (var item in pobjAddAttentionAreaComponentList)
                    {
                        attentioninareacomponentDto oattentioninareacomponentDto = new attentioninareacomponentDto();

                        oattentioninareacomponentDto.v_ComponentId = item.v_ComponentId;
                        oattentioninareacomponentDto.v_AttentionInAreaId = pstrAttentionInAreaId;

                        var x = GetAttentionInAreaComponentByComponentId(ref objOperationResult2, item.v_ComponentId);

                        if (x != null)
                        {
                            x.i_IsDeleted = 0;
                            UpdateAttentionInAreaComponent(ref objOperationResult2, x, ClientSession);
                        }
                        else
                        {
                            AddAttentionInAreaComponent(ref objOperationResult2, oattentioninareacomponentDto, ClientSession);
                        }
                        
                    }
                    
                }
                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + objEntity.v_AttentionInAreaId.ToString() + " / Descripción = " + objEntity.v_Name, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + pobjDtoEntity.v_AttentionInAreaId.ToString() + " / Descripción = " + pobjDtoEntity.v_Name, Success.Failed, ex.Message);
                return;
            }
        }

        public void DeleteAttentionInAreaByNode(ref OperationResult pobjOperationResult, string pstrAttentionInAreaId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.attentioninarea
                                       where a.v_AttentionInAreaId == pstrAttentionInAreaId 
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "", Success.Failed, ex.Message);
                return;
            }
        }



        public List<AttentionInAreaComponentList> GetAttentionInAreaComponentByAttentionInAreaIdPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.attentioninareacomponent                           
                             where A.i_IsDeleted == 0

                             select new AttentionInAreaComponentList
                             {
                                v_AttentioninAreaComponentId = A.v_AttentioninAreaComponentId,
                                v_AttentionInAreaId = A.v_AttentionInAreaId,
                                v_ComponentId = A.v_ComponentId,
                                i_IsDeleted = A.i_IsDeleted.Value
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

                List<AttentionInAreaComponentList> objData = query.ToList();
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

        public attentioninareacomponentDto GetAttentionInAreaComponent(ref OperationResult pobjOperationResult, string pstrAttentioninAreaComponentId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                attentioninareacomponentDto objDtoEntity = null;

                var objEntity = (from a in dbContext.attentioninareacomponent
                                 where a.v_AttentioninAreaComponentId == pstrAttentioninAreaComponentId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = attentioninareacomponentAssembler.ToDTO(objEntity);

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

        public void AddAttentionInAreaComponent(ref OperationResult pobjOperationResult, attentioninareacomponentDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                attentioninareacomponent objEntity = attentioninareacomponentAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 47), "AC"); ;
                objEntity.v_AttentioninAreaComponentId = NewId;

                dbContext.AddToattentioninareacomponent(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + NewId.ToString() + " / Descripción = " + objEntity.v_Name, Success.Ok, null);

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + NewId.ToString() + " / Descripción = " + pobjDtoEntity.v_Name, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateAttentionInAreaComponent(ref OperationResult pobjOperationResult, attentioninareacomponentDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.attentioninareacomponent
                                       where a.v_AttentioninAreaComponentId == pobjDtoEntity.v_AttentioninAreaComponentId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                attentioninareacomponent objEntity = attentioninareacomponentAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.attentioninareacomponent.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + objEntity.v_AttentionInAreaId.ToString() + " / Descripción = " + objEntity.v_Name, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "v_AttentionInAreaId=" + pobjDtoEntity.v_AttentionInAreaId.ToString() + " / Descripción = " + pobjDtoEntity.v_Name, Success.Failed, ex.Message);
                return;
            }
        }

        public void DeleteAttentionInAreaComponent(ref OperationResult pobjOperationResult, string pstrAttentioninAreaComponentId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.attentioninareacomponent
                                       where a.v_AttentioninAreaComponentId == pstrAttentioninAreaComponentId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ATENCIÓN EN ÁREA", "", Success.Failed, ex.Message);
                return;
            }
        }


        public attentioninareacomponentDto GetAttentionInAreaComponentByComponentId(ref OperationResult pobjOperationResult, string pstrComponentId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                attentioninareacomponentDto objDtoEntity = null;

                var objEntity = (from a in dbContext.attentioninareacomponent
                                 where a.v_ComponentId == pstrComponentId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = attentioninareacomponentAssembler.ToDTO(objEntity);

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

        #endregion

        #region Almacén Farmacia

        public nodeDto GetPharmacyWarehouseByNode(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                nodeDto objDtoEntity = null;

                var objEntity = (from a in dbContext.node
                                 where a.i_NodeId == pintNodeId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = nodeAssembler.ToDTO(objEntity);

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

        public void UpdatePharmacyWarehouseByNode(ref OperationResult pobjOperationResult, nodeDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.node
                                       where a.i_NodeId == pobjDtoEntity.i_NodeId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                node objEntity = nodeAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.node.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ALMACÉN FARMACIA", "i_NodeId=" + objEntity.i_NodeId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ALMACÉN FARMACIA", "i_NodeId=" + pobjDtoEntity.i_NodeId.ToString() , Success.Failed, ex.Message);
                return;
            }
        }

        #endregion
    }
}
