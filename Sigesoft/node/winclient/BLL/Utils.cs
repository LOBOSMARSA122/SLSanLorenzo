using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Linq.Dynamic;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace Sigesoft.Node.WinClient.BLL
{

    public static class Utils
    {

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


        public static int GetNextSecuentialNoSave(int pintNodeId, int pintTableId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            secuential objSecuential = (from a in dbContext.secuential
                                        where a.i_TableId == pintTableId && a.i_NodeId == pintNodeId
                                        select a).SingleOrDefault();
            return objSecuential.i_SecuentialId.Value;
        }

        #region KeyValueDTO

        public static List<KeyValueDTO> GetProtocolsByOrganizationForCombo(ref OperationResult pobjOperationResult, string v_CustomerOrganizationId, string v_CustomerLocationId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.protocol
                            where (a.v_CustomerOrganizationId == v_CustomerOrganizationId) &&
                            (a.v_CustomerLocationId == v_CustomerLocationId) &&
                            (a.i_IsDeleted == 0)
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_ProtocolId");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.v_ProtocolId,
                                Value1 = x.v_Name
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


        public static List<KeyValueDTO> GetSystemParameterForCombo(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

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

        public static List<KeyValueDTO> GetSystemParameterForComboAndParameterId(ref OperationResult pobjOperationResult, int pintGroupId, int pintParameterId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId &&
                            a.i_ParentParameterId == pintParameterId &&
                            a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

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

        public static List<KeyValueDTOForTree> GetSystemParameterForComboTreeBox(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTOForTree
                            {
                                Id = x.i_ParameterId.ToString(),
                                ParentId = x.i_ParentParameterId.ToString(),
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

        public static List<KeyValueDTOForTree> GetDataHierarchyForComboTreeBox(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTOForTree
                            {
                                Id = x.i_ItemId.ToString(),
                                ParentId = x.i_ParentItemId.ToString(),
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

        public static List<KeyValueDTO> GetSystemParameterByParentForCombo(ref OperationResult pobjOperationResult, int i_ParentGroupId, int i_ParentParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataSystemParameterList;
                DataSystemParameterList = (from a in dbContext.systemparameter
                                           where a.i_ParentParameterId == i_ParentParameterId && a.i_IsDeleted == 0
                                           select new KeyValueDTO
                                           {
                                               Id = a.i_ParameterId.ToString(),
                                               Value1 = a.v_Value1,
                                               Value2 = a.v_Value2
                                           }).ToList();

                pobjOperationResult.Success = 1;
                return DataSystemParameterList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetSystemParameterByParentForComboAntecedentesFamiliares(ref OperationResult pobjOperationResult, int i_ParentParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            join J3 in dbContext.diseases on new { a = a.v_Value1 }
                                                         equals new { a = J3.v_DiseasesId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()
                            where a.i_ParentParameterId == i_ParentParameterId && a.i_IsDeleted == 0 && a.i_GroupId == 149
                            select new KeyValueDTO
                            {
                                Value4 = a.i_ParameterId,
                                Value1 = J3.v_Name,
                                Value2 = a.v_Value1,
                            };


                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.Value4.ToString(),
                                Value1 = x.Value1,
                                Value2 = x.Value2
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


        public static List<KeyValueDTO> GetSystemParameterByParentIdForCombo(ref OperationResult pobjOperationResult, int pintGroupId, int pintParentParameterId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId &&
                            a.i_ParentParameterId == pintParentParameterId &&
                            a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

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

        public static List<KeyValueDTO> GetAllOrganizations(ref OperationResult pobjOperationResult)
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

        public static List<KeyValueDTO> GetProduct(ref OperationResult pobjOperationResult, string pstrFilterExpression)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.product
                             //join b in dbContext.product on a.v_ProductId equals b.v_ProductId
                             select new KeyValueDTO
                             {
                                 Id = a.v_ProductId,
                                 Value1 = "Producto : " + a.v_Name + " / Marca : " + a.v_Brand + " / Modelo : " + a.v_Model + " / Nro. Serie : " + a.v_SerialNumber
                             }).Take(15);
                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
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

        public static List<KeyValueDTO> GetProductWarehouse(ref OperationResult pobjOperationResult, string pstrFilterExpression)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.productwarehouse
                            join b in dbContext.product on a.v_ProductId equals b.v_ProductId
                            join eee in dbContext.datahierarchy on new { a = b.i_MeasurementUnitId.Value, b = 105 } // Unid medida
                                                              equals new { a = eee.i_ItemId, b = eee.i_GroupId } into J8_join
                            from eee in J8_join.DefaultIfEmpty()
                            where eee.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_ProductId,
                                Value1 = "Producto : " + b.v_Name + " / Marca : " + b.v_Brand + " / Modelo : " + b.v_Model + " / Nro. Serie : " + b.v_SerialNumber,
                                Value2 = a.v_WarehouseId,
                                Value3 = b.v_Presentation + " " + eee.v_Value1,
                                Value4 = (int)a.r_StockActual.Value
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }

                var query1 = query.AsEnumerable()
                             .Select(x => new KeyValueDTO
                             {
                                 Id = x.Id,
                                 Value1 = x.Value1 + "  (Stock Actual : " + x.Value4 + ")",
                                 Value2 = x.Value2,
                                 Value3 = x.Value3,
                                 Value4 = x.Value4
                                 //"Producto : " + a.v_Name + " / Marca : " + a.v_Brand +  " / Modelo : " + a.v_Model + " / Nro. Serie : " + a.v_SerialNumber
                             }).ToList().Take(15);

                List<KeyValueDTO> objDataList = query1.OrderBy(p => p.Value1).ToList();
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

        public static List<KeyValueDTO> GetDataHierarchyForCombo(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
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

        public static List<KeyValueDTO> GetDataHierarchyForComboAndItemId(ref OperationResult pobjOperationResult, int pintGroupId, int pintItemId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId &&
                            a.i_ParentItemId == pintItemId &&
                            a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
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

        public static List<KeyValueDTO> GetArea(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataAreaList;
                DataAreaList = (from a in dbContext.area
                                join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                                join c in dbContext.organization on b.v_OrganizationId equals c.v_OrganizationId
                                where c.v_OrganizationId == pstrOrganizationId && a.i_IsDeleted == 0
                                select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_AreaId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataAreaList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetGES(ref OperationResult pobjOperationResult, string pstrAreaId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGESList;
                DataGESList = (from a in dbContext.ges
                               join b in dbContext.area on a.v_AreaId equals b.v_AreaId
                               join c in dbContext.location on b.v_LocationId equals c.v_LocationId
                               join d in dbContext.organization on c.v_OrganizationId equals d.v_OrganizationId
                               where a.i_IsDeleted == 0 && a.v_AreaId == pstrAreaId
                               select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_GesId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataGESList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

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

        public static List<KeyValueDTO> GetMedicalExamGrupo(ref OperationResult pobjOperationResult, string pstrComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGroupList;
                DataGroupList = (from a in dbContext.componentfields
                                 where a.v_Group != null && a.v_ComponentId == pstrComponentId
                                 select new KeyValueDTO
                                 {
                                     Value1 = a.v_Group
                                 }
                                ).Distinct().ToList();

                pobjOperationResult.Success = 1;
                return DataGroupList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetVendedor(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGroupList;
                DataGroupList = (from a in dbContext.protocol
                                 where a.v_NombreVendedor != null
                                 select new KeyValueDTO
                                 {
                                     Value1 = a.v_NombreVendedor
                                 }
                                ).Distinct().ToList();

                pobjOperationResult.Success = 1;
                return DataGroupList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetJoinOrganizationAndLocation(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from J1 in j1_join.DefaultIfEmpty()
                             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             where n.i_NodeId == pintNodeId &&
                                   n.i_IsDeleted == 0 &&
                                   a.i_IsDeleted == 0

                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = b.v_Name,
                                 v_LocationName = c.v_Name,
                                 v_LocationId = c.v_LocationId,
                                 v_OrganizationId = b.v_OrganizationId,
                                 i_NodeId = J1.i_NodeId,
                             }
                          );

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id = string.Format("{0}|{1}", a.v_OrganizationId, a.v_LocationId),
                            Value1 = string.Format("{0} / Sede: {1} ",
                                     a.v_OrganizationName,
                                     a.v_LocationName
                                    )
                        };

                List<KeyValueDTO> KeyValueDTO = q.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return KeyValueDTO;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }


        public static List<KeyValueDTO> GetJoinOrganization(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from J1 in j1_join.DefaultIfEmpty()
                             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             where n.i_NodeId == pintNodeId &&
                                   n.i_IsDeleted == 0 &&
                                   a.i_IsDeleted == 0

                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = b.v_Name,
                                 v_LocationName = c.v_Name,
                                 v_LocationId = c.v_LocationId,
                                 v_OrganizationId = b.v_OrganizationId,
                                 i_NodeId = J1.i_NodeId,
                             }
                          );

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id = string.Format("{0}", a.v_OrganizationId),
                            Value1 = string.Format("{0} ",
                                     a.v_OrganizationName
                                    )
                        };

                List<KeyValueDTO> KeyValueDTO = q.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return KeyValueDTO;

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
                                     where a.i_IsDeleted == 0 && a.i_ComponentTypeId == 1
                                     && a.i_CategoryId != -1
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

        public static List<KeyValueDTO> GetComponentsByCategoryId(ref OperationResult pobjOperationResult, int pintCategoryId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataComponentList;
                DataComponentList = (from a in dbContext.component
                                     where a.i_IsDeleted == 0 && a.i_ComponentTypeId == 1 && a.i_CategoryId == pintCategoryId
                                     && a.i_CategoryId != -1
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


        public static List<KeyValueDTO> GetOrganization(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataComponentList;
                DataComponentList = (from a in dbContext.organization
                                     where a.i_IsDeleted == 0
                                     select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Value1 = x.v_Name,
                                               Id = x.v_OrganizationId
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

        public static List<KeyValueDTO> GetAllComponents(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var DataComponentList = (from a in dbContext.component
                                         join B in dbContext.systemparameter on new { a = a.i_CategoryId.Value, b = 116 } equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                                         from B in B_join.DefaultIfEmpty()
                                         where a.i_IsDeleted == 0 &&
                                               a.i_ComponentTypeId == 1
                                         select new KeyValueDTO
                                         {
                                             Value4 = a.i_CategoryId.Value,//i_CategoryId
                                             Value1 = a.i_CategoryId.Value == -1 ? a.v_Name : B.v_Value1, //CategoryName
                                             Value2 = a.v_ComponentId, // ComponentId
                                             Value3 = a.v_Name, // v_Name
                                             //Id = a.v_ComponentId
                                         }).ToList();


                List<KeyValueDTO> objData = DataComponentList.ToList();
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

        public static List<KeyValueDTO> GetAllComponentsByRoleNodeId(int nodeId, int roleId)
        {
            //mon.IsActive = true;
            ///
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var DataComponentList = (from a in dbContext.component
                                         join C in dbContext.rolenodecomponentprofile on a.v_ComponentId equals C.v_ComponentId
                                         join B in dbContext.systemparameter on new { a = a.i_CategoryId.Value, b = 116 } equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                                         from B in B_join.DefaultIfEmpty()
                                         where (a.i_IsDeleted == 0) &&
                                               (a.i_ComponentTypeId == 1) &&
                                               (C.i_NodeId == nodeId) &&
                                               (C.i_RoleId == roleId) &&
                                               (C.i_IsDeleted == (int)SiNo.NO)
                                         select new KeyValueDTO
                                         {
                                             Value4 = a.i_CategoryId.Value,//i_CategoryId
                                             Value1 = a.i_CategoryId.Value == -1 ? a.v_Name : B.v_Value1, //CategoryName
                                             Value2 = a.v_ComponentId, // ComponentId
                                             Value3 = a.v_Name, // v_Name
                                             //Id = a.v_ComponentId
                                         }).ToList();


                List<KeyValueDTO> objData = DataComponentList.ToList();

                return objData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<KeyValueDTO> GetServiceType(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeserviceprofile
                             join b in dbContext.systemparameter on new { a = a.i_ServiceTypeId.Value, b = 119 } equals new { a = b.i_ParameterId, b = b.i_GroupId }
                             where a.i_NodeId == pintNodeId && a.i_IsDeleted == 0
                             select b).AsEnumerable().Distinct()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ParameterId.ToString(),
                                Value1 = x.v_Value1
                            }).ToList();
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

        public static List<KeyValueDTO> GetMasterService(ref OperationResult pobjOperationResult, int? pintServiceTypeId, int nodeId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.nodeserviceprofile
                             join b in dbContext.systemparameter on new { a = a.i_MasterServiceId.Value, b = 119 } equals new { a = b.i_ParameterId, b = b.i_GroupId }
                             where a.i_ServiceTypeId == pintServiceTypeId && a.i_IsDeleted == 0 && a.i_NodeId == nodeId
                             select b).AsEnumerable().Distinct()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ParameterId.ToString(),
                                Value1 = x.v_Value1
                            }).ToList();
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

        public static List<KeyValueDTO> GetJoinOrganizationAndLocationNotInRestricted(ref OperationResult pobjOperationResult, int pintNodeId)
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
                                 i_NodeId = J1.i_NodeId
                             }
                          ).Distinct();

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id = string.Format("{0}|{1}|{2}", a.i_NodeId, a.v_OrganizationId, a.v_LocationId),
                            Value1 = string.Format("Empresa: {0} / Sede: {1} ",
                                     a.v_OrganizationName,
                                     a.v_LocationName)
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

        public static List<KeyValueDTO> GetWarehouseNotInRestricted(ref OperationResult pobjOperationResult, int pintNodeId, string pstrOrganizationId, string pstrLocationId)
        {
            //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

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
                                          where r.i_IsDeleted == 0
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

        public static List<KeyValueDTO> GetProtocolByLocation(ref OperationResult pobjOperationResult, string pstrLocationId, int pintEsoTypeId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.protocol
                             where a.v_EmployerLocationId == pstrLocationId && a.i_EsoTypeId == pintEsoTypeId && a.i_IsDeleted == 0
                             select a).AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.v_ProtocolId.ToString(),
                                Value1 = x.v_Name
                            }).ToList();
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

        public static List<KeyValueDTO> GetFieldsByComponent(ref OperationResult pobjOperationResult, string ComponentId, string Mode)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.componentfields
                            join b in dbContext.componentfield on a.v_ComponentFieldId equals b.v_ComponentFieldId
                            where a.v_ComponentId == ComponentId && a.i_IsDeleted == 0
                            select b;


                var query1 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.v_ComponentFieldId,
                                Value1 = x.v_TextLabel + "  " + "(" + x.v_ComponentFieldId + ")"

                            }).ToList();

                List<KeyValueDTO> objDataList = query1.OrderBy(p => p.Value1).ToList();
                if (Mode == "Total")
                {
                    objDataList.Insert(0, new KeyValueDTO { Id = "GENERO_1", Value1 = "GÉNERO_1" + "(" + "  masculino = 0 ; femenino = 1" + ")" });
                    objDataList.Insert(1, new KeyValueDTO { Id = "GENERO_2", Value1 = "GÉNERO_2" + "(" + "  masculino = 1 ; femenino = 0" + ")" });
                    objDataList.Insert(2, new KeyValueDTO { Id = "EDAD", Value1 = "EDAD" });
                }

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

        public static List<KeyValueDTO> GetAllRestrictionForCombo(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.masterrecommendationrestricction
                             where a.i_TypifyingId == (int)Typifying.Restricciones &&
                             a.i_IsDeleted == 0
                             select new KeyValueDTO
                             {
                                 Id = a.v_MasterRecommendationRestricctionId,
                                 Value1 = a.v_Name
                             }).ToList();

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

        public static List<KeyValueDTO> GetDataHierarchyForComboDepartamento(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParentItemId == -1
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
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

        public static List<KeyValueDTO> GetDataHierarchyForComboProvincia(ref OperationResult pobjOperationResult, int pintGroupId, int? pintParentItemId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParentItemId == pintParentItemId
                            select a;


                query = query.OrderBy("v_Value1");

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
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

        public static List<KeyValueDTO> GetDataHierarchyForComboDistrito(ref OperationResult pobjOperationResult, int pintGroupId, int? pintParentItemId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParentItemId == pintParentItemId
                            select a;


                query = query.OrderBy("v_Value1");

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
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

        public static List<KeyValueDTO> GetProfessional(ref OperationResult pobjOperationResult, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemuser
                            where a.i_IsDeleted == 0 && a.i_SystemUserTypeId == 1
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_UserName");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_SystemUserId.ToString(),
                                Value1 = x.v_UserName
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

        public static List<KeyValueDTO> GetUsuariosExternos(ref OperationResult pobjOperationResult, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemuser
                            where a.i_IsDeleted == 0 && a.i_SystemUserTypeId == 2
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_UserName");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_SystemUserId.ToString(),
                                Value1 = x.v_UserName
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

        public static List<KeyValueDTO> GetAllComponentsByCategory(ref OperationResult pobjOperationResult, int pintCategoryId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var DataComponentList = (from a in dbContext.component
                                         where a.i_IsDeleted == 0 &&
                                               a.i_CategoryId == pintCategoryId
                                         select new KeyValueDTO
                                         {
                                             Id = a.v_ComponentId,
                                             Value1 = a.v_Name
                                         }).ToList();


                List<KeyValueDTO> objData = DataComponentList.ToList();
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
        #endregion

        #region FormActions

        public static List<KeyValueDTO> SetFormActionsInSession(string pstrFormCode, int pintCurrentExecutionNodeId, int pintRoleId, int pintSystemUserId)
        {
            try
            {
                SecurityBL _objSecurityBL = new SecurityBL();
                // Obtener acciones de un formulario especifico
                OperationResult objOperationResult = new OperationResult();
                List<KeyValueDTO> formActions = _objSecurityBL.GetFormAction(ref objOperationResult,
                                                                                pintCurrentExecutionNodeId,
                                                                                pintRoleId,
                                                                                pintSystemUserId,
                                                                                pstrFormCode.Trim());
                return formActions;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static bool IsActionEnabled(string pstrActionCode, List<KeyValueDTO> FormActions)
        {
            List<KeyValueDTO> objFormAction = FormActions;

            if (objFormAction != null)
            {
                bool isExists = objFormAction.Exists(p => p.Value2.Equals(pstrActionCode.Trim()));

                if (isExists)
                    return true;
            }

            return false;
        }


        public static List<KeyValueDTOCheck> GetSystemParameterForComboCheck(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTOCheck
                            {
                                Id = x.i_ParameterId.ToString(),
                                Value1 = x.v_Value1
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

        #endregion

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static List<KeyValueDTO> GetAreaSede(ref OperationResult pobjOperationResult, string pstrOrganizationId, string pstrSedeId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataAreaList;
                DataAreaList = (from a in dbContext.area
                                join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                                join c in dbContext.organization on b.v_OrganizationId equals c.v_OrganizationId
                                where c.v_OrganizationId == pstrOrganizationId && a.i_IsDeleted == 0 && b.v_LocationId == pstrSedeId
                                select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_AreaId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataAreaList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public static List<KeyValueDTO> GetDataHierarchyByParentIdForCombo(ref OperationResult pobjOperationResult, int pintGroupId, int pintParentParameterId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId &&
                            a.i_ParentItemId == pintParentParameterId &&
                            a.i_IsDeleted == 0
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
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

        public static List<KeyValueDTO> ListLocationForCombo(string pstrEmpresaId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.location
                            where a.v_OrganizationId == pstrEmpresaId && a.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_OrganizationId,
                                Value1 = a.v_Name
                            };

                List<KeyValueDTO> objDataList = query.OrderBy(p => p.Value1).ToList();

                return objDataList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }


}
