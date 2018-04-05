using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using Sigesoft.Common;
using System.Web;

namespace Sigesoft.Server.WebClientAdmin.BLL
{
    public static class Utils
    {
        //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

        public static int GetNextSecuentialId(int pintNodeId, int pintTableId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            secuential objSecuential = (from a in dbContext.secuential
                                        where a.i_TableId == pintTableId && a.i_NodeId == pintNodeId
                                        select a).SingleOrDefault();

            // Actualizar el campo con el nuevo valor a efectos de reservar el ID autogenerado para evitar colisiones entre otros nodos
            if (objSecuential != null)
            {
                objSecuential.i_SecuentialId = objSecuential.i_SecuentialId + 1;
            }
            else
            {
                objSecuential = new secuential();
                objSecuential.i_NodeId = pintNodeId;
                objSecuential.i_TableId = pintTableId;
                objSecuential.i_SecuentialId = 0;
                dbContext.AddTosecuential(objSecuential);
            }

            dbContext.SaveChanges();

            return objSecuential.i_SecuentialId.Value;
        }

        public static int GetNextSecuentialIdMejorado(int pintNodeId, int pintTableId, int Rango)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            secuential objSecuential = (from a in dbContext.secuential
                                        where a.i_TableId == pintTableId && a.i_NodeId == pintNodeId
                                        select a).SingleOrDefault();

            // Actualizar el campo con el nuevo valor a efectos de reservar el ID autogenerado para evitar colisiones entre otros nodos
            if (objSecuential != null)
            {
                objSecuential.i_SecuentialId = objSecuential.i_SecuentialId + Rango;
            }
            else
            {
                objSecuential = new secuential();
                objSecuential.i_NodeId = pintNodeId;
                objSecuential.i_TableId = pintTableId;
                objSecuential.i_SecuentialId = 0;
                dbContext.AddTosecuential(objSecuential);
            }

            dbContext.SaveChanges();

            return objSecuential.i_SecuentialId.Value - Rango;
        }

        #region KeyValueDTO

        public static List<KeyValueDTO> GetGESO(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGESOList;
                DataGESOList = (from a in dbContext.groupoccupation
                                join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                                join c in dbContext.organization on b.v_OrganizationId equals c.v_OrganizationId
                                where c.v_OrganizationId == pstrOrganizationId && a.i_IsDeleted == 0
                                select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_GroupOccupationId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataGESOList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetGESOByOrgIdAndLocationId(ref OperationResult pobjOperationResult, string pstrOrganizationId, string locationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGESOList;

                DataGESOList = (from a in dbContext.groupoccupation
                                join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                                join c in dbContext.organization on b.v_OrganizationId equals c.v_OrganizationId
                                where c.v_OrganizationId == pstrOrganizationId
                                      && b.v_LocationId == locationId
                                      && a.i_IsDeleted == 0

                                select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_GroupOccupationId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataGESOList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
        
        public static List<KeyValueDTO> GetOrganizationByNodeId(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;
            
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeorganizationprofile
                             join b in dbContext.organization on a.v_OrganizationId equals b.v_OrganizationId
                             where a.i_NodeId == pintNodeId && a.i_IsDeleted == 0
                             select new KeyValueDTO
                             {
                                 Id = a.v_OrganizationId,
                                 Value1 = b.v_Name
                             });

                List<KeyValueDTO> OrganizationList = query.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return OrganizationList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetLocationByNodeIdAndOrganizationId(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeorganizationlocationprofile
                             join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                             where a.i_NodeId == pintNodeId && a.v_OrganizationId == pstrOrganizationId && a.i_IsDeleted == 0
                             select new KeyValueDTO
                             {
                                 Id = a.v_LocationId,
                                 Value1 = b.v_Name
                             });

                List<KeyValueDTO> LocationList = query.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return LocationList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetWarehouseByNodeIdAndOrganizationIdAndLocationId(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId, string pstrLocationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                             join b in dbContext.warehouse on a.v_WarehouseId equals b.v_WarehouseId
                             where a.i_NodeId == pintNodeId && 
                                   a.v_OrganizationId == pstrOrganizationId && 
                                   a.v_LocationId == pstrLocationId && 
                                   a.i_IsDeleted == 0
                             select new KeyValueDTO
                             {
                                 Id = a.v_WarehouseId,
                                 Value1 = b.v_Name
                             });

                List<KeyValueDTO> WarehouseList = query.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return WarehouseList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetWarehouseNotInRestricted(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId, string pstrLocationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeorganizationlocationwarehouseprofile
                             join b in dbContext.warehouse on a.v_WarehouseId equals b.v_WarehouseId
                             where a.i_NodeId == pintNodeId &&
                                   a.v_OrganizationId == pstrOrganizationId &&
                                   a.v_LocationId == pstrLocationId &&
                                   a.i_IsDeleted == 0
                             select new KeyValueDTO
                             {
                                 Id = a.v_WarehouseId,
                                 Value1 = b.v_Name
                             });

                // Excluir almacenes restringidos
                var queryNotIn = (from a in query.ToList()
                                 where !(from r in dbContext.restrictedwarehouseprofile    
                                         select r.v_WarehouseId).Contains(a.Id)                               
                                 select a);

                List<KeyValueDTO> WarehouseList = queryNotIn.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return WarehouseList;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetSystemParameterForCombo(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                            select a;


                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ParameterId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2
                            }).ToList();

                pobjOperationResult.Success = 1;
                return query2;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
        
        public static List<KeyValueDTO> GetJoinOrganizationAndLocationAndWarehouse(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from J1 in j1_join.DefaultIfEmpty()
                             join J2 in dbContext.nodeorganizationlocationwarehouseprofile on new { a = a.i_NodeId, b = a.v_OrganizationId, c = a.v_LocationId }
                                                      equals new { a = J2.i_NodeId, b = J2.v_OrganizationId , c = J2.v_LocationId } into j2_join
                             from J2 in j2_join.DefaultIfEmpty()
                             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             join J3 in dbContext.warehouse on new { a = J2.v_WarehouseId}
                                                      equals new { a = J3.v_WarehouseId} into j3_join
                             from J3 in j3_join.DefaultIfEmpty()
                             where n.i_NodeId == pintNodeId && 
                                   n.i_IsDeleted == 0 &&
                                   a.i_IsDeleted == 0 &&
                                   J2.i_IsDeleted == 0
                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = b.v_Name,
                                 v_LocationName = c.v_Name,
                                 v_LocationId = c.v_LocationId,
                                 v_OrganizationId = b.v_OrganizationId,
                                 i_NodeId = J1.i_NodeId,
                                 v_WarehouseName = J3.v_Name,
                                 v_WarehouseId = J3.v_WarehouseId
                             }
                          );            

                var q = from a in query.ToList()
                         select new KeyValueDTO
                         {                            
                             Id = string.Format("{0}|{1}|{2}|{3}", a.i_NodeId, a.v_OrganizationId, a.v_LocationId, a.v_WarehouseId),
                             Value1 = string.Format("Empresa: {0} / Sede: {1} / Almacén: {2}", 
                                      a.v_OrganizationName, 
                                      a.v_LocationName, 
                                      a.v_WarehouseName)                                   
                         };

                List<KeyValueDTO> WarehouseList = q.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return WarehouseList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetPharmacyWarehouse(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from J1 in j1_join.DefaultIfEmpty()
                             join J2 in dbContext.nodeorganizationlocationwarehouseprofile on new { a = a.i_NodeId, b = a.v_OrganizationId, c = a.v_LocationId }
                                                      equals new { a = J2.i_NodeId, b = J2.v_OrganizationId, c = J2.v_LocationId } into j2_join
                             from J2 in j2_join.DefaultIfEmpty()
                             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             join J3 in dbContext.warehouse on new { a = J2.v_WarehouseId }
                                                      equals new { a = J3.v_WarehouseId } into j3_join
                             from J3 in j3_join.DefaultIfEmpty()
                             where n.i_NodeId == pintNodeId &&
                                   n.i_IsDeleted == 0 &&
                                   a.i_IsDeleted == 0 &&
                                   J2.i_IsDeleted == 0
                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = b.v_Name,
                                 v_LocationName = c.v_Name,
                                 v_LocationId = c.v_LocationId,
                                 v_OrganizationId = b.v_OrganizationId,
                                 i_NodeId = J1.i_NodeId,
                                 v_WarehouseName = J3.v_Name,
                                 v_WarehouseId = J3.v_WarehouseId
                             }
                          );

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id = a.v_WarehouseId,
                            Value1 = string.Format("Empresa: {0} / Sede: {1} / Almacén: {2}",
                                     a.v_OrganizationName,
                                     a.v_LocationName,
                                     a.v_WarehouseName)
                        };

                List<KeyValueDTO> WarehouseList = q.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return WarehouseList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetAllNodeForCombo(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               
                var query = from a in dbContext.node 
                            where a.i_IsDeleted == 0
                            select a;                               
                           
                 var q = from a in query.ToList()
                         select new KeyValueDTO
                         {
                             Id = a.i_NodeId.ToString(),
                             Value1 = a.v_Description
                         };

                List<KeyValueDTO> NodeList = q.OrderBy(p => p.Value1).ToList();
                pobjOperationResult.Success = 1;
                return NodeList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }

        }

        public static List<KeyValueDTO> GetRoleByNodeIdForCombo(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.rolenode
                             join J2 in dbContext.systemparameter on new { a = a.i_RoleId, b = 115 }
                                         equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where a.i_NodeId == pintNodeId &&
                             a.i_IsDeleted == 0
                             select new 
                             { 
                                i_RoleId = a.i_RoleId,
                                v_RoleName = J2.v_Value1
                             });

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id = a.i_RoleId.ToString(),
                            Value1 = a.v_RoleName
                        };

                List<KeyValueDTO> NodeList = q.OrderBy(p => p.Value1).ToList();
                pobjOperationResult.Success = 1;
                return NodeList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }

        }

        public static List<KeyValueDTO> GetAllOrganizationsForCombo(ref OperationResult pobjOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.organization
                            where a.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_OrganizationId,
                                Value1 = a.v_Name
                            };

                List<KeyValueDTO> objDataList = query.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;

                return objDataList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetLocationByOrganizationIdForCombo(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.location
                            where a.v_OrganizationId == pstrOrganizationId && 
                                  a.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_LocationId,
                                Value1 = a.v_Name
                            };

                List<KeyValueDTO> objDataList = query.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;

                return objDataList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetComponents(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataComponentList;
                DataComponentList = (from a in dbContext.component
                                     where a.i_IsDeleted == 0                                                                        
                                     select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Value1 = x.v_Name,
                                               Id = x.v_ComponentId
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataComponentList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetComponentsByAttentionInArea(ref OperationResult pobjOperationResult, string pstrAttentionInAreaId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataComponentList;
                DataComponentList = (from a in dbContext.attentioninareacomponent
                                     where a.i_IsDeleted == 0 && a.v_AttentionInAreaId == pstrAttentionInAreaId
                                     select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Value1 = x.v_ComponentId,
                                               Id = x.v_ComponentId
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataComponentList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetComponentsFilter(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataComponentList;
                DataComponentList = (from a in dbContext.component
                                     where a.i_IsDeleted == 0 &&
                                     a.i_ComponentTypeId == (int?)ComponentType.Examen
                                     select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Value1 = x.v_Name,
                                               Id = x.v_ComponentId
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataComponentList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetSystemParameterValueById(int pintGroupId, int pstrParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParameterId == pstrParameterId 
                            select a;



                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ParameterId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2
                            }).ToList();

                return query2;
            }
            catch (Exception ex)
            {              
                return null;
            }
        }
        

        #endregion

        #region TreeView

        public static List<KeyValueDTO> GetWarehouseByOrganizationAndLocationForTreeView(ref OperationResult pobjOperationResult, string pstrOrganizationId, string pstrLocationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from c in dbContext.organization 
                             join d in dbContext.location on c.v_OrganizationId equals d.v_OrganizationId
                             join e in dbContext.warehouse on d.v_LocationId equals e.v_LocationId
                             where c.v_OrganizationId == pstrOrganizationId && 
                                   d.v_LocationId == pstrLocationId &&
                                   c.i_IsDeleted == 0
                             select new RestrictedWarehouseProfileList
                             {                             
                                 v_WarehouseId = e.v_WarehouseId,                             
                                 v_OrganizationName = c.v_Name,
                                 v_LocationName = d.v_Name,
                                 v_WarehouseName = e.v_Name,                                                        
                             });
                             

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id =  a.v_WarehouseId,
                            Value1 = string.Format("{0} / {1} / {2}",
                                     a.v_OrganizationName,
                                     a.v_LocationName,
                                     a.v_WarehouseName)

                        };

                List<KeyValueDTO> WarehouseList = q.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return WarehouseList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetWarehouseForShowTreeView(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId, string pstrLocationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeorganizationlocationwarehouseprofile                          
                             where a.i_NodeId == pintNodeId &&
                                   a.v_OrganizationId == pstrOrganizationId &&
                                   a.v_LocationId == pstrLocationId &&
                                   a.i_IsDeleted == 0
                             select new KeyValueDTO
                             {
                                 Id = a.v_WarehouseId                                
                             });

                List<KeyValueDTO> KeyValueDTOlist = query.OrderBy(p => p.Id).ToList();

                pobjOperationResult.Success = 1;
                return KeyValueDTOlist;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

       
        #endregion

        #region Functions to Cache

        /// <summary>
        /// Agrega los Paginas de un Perfil al Cache
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <param name="dsPaginas">Paginas</param>
        /// <param name="context">El HTTPContext que se esta ejecutando</param>
        public static void AddPaginasToCache(string key, List<AutorizationList> Pages, System.Web.HttpContext context)
        {
            try
            {
                if (context.Cache.Get(key) != null)
                    context.Cache.Remove(key); //remuevo si lo encuentra para reemplazarlo

                context.Cache.Add(key,
                    Pages, null,
                    DateTime.Now.AddMonths(1),
                    TimeSpan.Zero,
                    System.Web.Caching.CacheItemPriority.High, null); //agrego al cache
            }
            catch (Exception )
            {
                //throw ex;
            }
        }

        #endregion

        #region FormActions

        public static void SetFormActionsInSession(string pstrFormCode)
        {
            try
            {
                if (HttpContext.Current.Session["objClientSession"] != null)
                {
                    var objClientSession = (ClientSession)HttpContext.Current.Session["objClientSession"];

                    SecurityBL _objSecurityBL = new SecurityBL();
                    // Obtener acciones de un formulario especifico
                    OperationResult objOperationResult = new OperationResult();
                    List<KeyValueDTO> formActions = _objSecurityBL.GetFormAction(ref objOperationResult,
                                                                objClientSession.i_CurrentExecutionNodeId,
                                                                objClientSession.i_SystemUserId,
                                                                pstrFormCode.Trim());

                    // Guardar las acciones
                    HttpContext.Current.Session["objFormAction"] = formActions;                
                }           
            }
            catch (Exception)
            {
                throw;            
            }
            
        }

        public static bool IsActionEnabled(string pstrActionCode)
        {
            if (HttpContext.Current.Session["objFormAction"] != null)
            {
                List<KeyValueDTO> objFormAction = HttpContext.Current.Session["objFormAction"] as List<KeyValueDTO>;

                if (objFormAction != null)
                {
                    bool isExists = objFormAction.Exists(p => p.Value2.Equals(pstrActionCode.Trim()));

                    if (isExists)
                        return true;
                }
            }

            return false;
        }

        #endregion

        public static List<KeyValueDTO> GetLocation(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataLocationList;
                DataLocationList = (from a in dbContext.location
                                    where a.v_OrganizationId == pstrOrganizationId && a.i_IsDeleted == 0
                                    select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_LocationId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataLocationList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

    }

    public class TransactionUtils
    {
        /// <summary>
        /// Crea una nueva instancia de un bloque Transaccional con las configuraciones óptimas para su uso.
        /// </summary>
        /// <returns></returns>
        public static TransactionScope CreateTransactionScope()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);

        }

        /// <summary>
        /// Metodo que sirve para cambiar el tiempo limite de las transacciones en el machine.config
        /// </summary>
        /// <param name="timeOut"></param>
        public static void OverrideTransactionScopeMaximumTimeout(TimeSpan timeOut)
        {
            var oSystemType = typeof(TransactionManager);
            System.Reflection.FieldInfo oCachedMaxTimeout = oSystemType.GetField(@"_cachedMaxTimeout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Reflection.FieldInfo oMaximumTimeout = oSystemType.GetField(@"_maximumTimeout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            if (oCachedMaxTimeout != null) oCachedMaxTimeout.SetValue(null, true);
            if (oMaximumTimeout != null) oMaximumTimeout.SetValue(null, timeOut);
        }
    }
}
