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
    public class NodeBL
    {
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

        public nodeorganizationlocationwarehouseprofileDto GetNodeOrganizationLocationWarehouseProfile(ref OperationResult pobjOperationResult, string pstrWarehouseId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                nodeorganizationlocationwarehouseprofileDto objDtoEntity = null;

                var objEntity = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                                 where a.v_WarehouseId == pstrWarehouseId && a.i_IsDeleted ==0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = nodeorganizationlocationwarehouseprofileAssembler.ToDTO(objEntity);

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

        public nodeDto GetNodeByNodeIdReport(int pintNodeId)
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

                return objDtoEntity;
            }
            catch (Exception ex)
            {
                
                return null;
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
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from j1 in j1_join.DefaultIfEmpty()
                             join b in dbContext.organization on j1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             orderby b.v_Name, c.v_Name
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

        public void UpdateNodeOrganizationChangeStatusAll(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId, string pstrLocationId, int pintIsDeleted, List<string> ClientSession)
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
                                                   a.v_OrganizationId == pstrOrganizationId &&
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

    }
}
