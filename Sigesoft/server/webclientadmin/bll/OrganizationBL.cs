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
    public class OrganizationBL
    {

        public List<KeyValueDTO> GetAllOrganizations(ref OperationResult pobjOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.organization
                            join b in dbContext.location on a.v_OrganizationId equals   b.v_OrganizationId
                            where a.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_OrganizationId+ "|"+ b.v_LocationId,
                                Value1 = a.v_Name + " / " + b.v_Name,
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

        public List<KeyValueDTO> GetAllOrganizationsOnly(ref OperationResult pobjOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.organization
                            where a.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_OrganizationId ,
                                Value1 = a.v_Name,
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

        //public List<OrganizationList> GetOrganizationsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        //{
        //    //mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        var query = from A in dbContext.Organizations
        //                    join B in dbContext.SystemParameters on new { a = A.i_OrganizationTypeId.Value, b = 103 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
        //                    join C in dbContext.DataHierarchies on new { a = A.i_SectorTypeId.Value, b = 104 } equals new { a = C.i_ItemId, b = C.i_GroupId }
        //                    join J1 in dbContext.SystemUsers on new { i_InsertUserId = A.i_InsertUserId.Value }
        //                                                    equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
        //                    from J1 in J1_join.DefaultIfEmpty()

        //                    join J2 in dbContext.SystemUsers on new { i_UpdateUserId = A.i_UpdateUserId.Value }
        //                                                    equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
        //                    from J2 in J2_join.DefaultIfEmpty()
        //                    select new OrganizationList
        //                    {
        //                        i_OrganizationId = A.i_OrganizationId,
        //                        i_OrganizationTypeId = (int)A.i_OrganizationTypeId,
        //                        v_OrganizationTypeIdName = B.v_Value1,
        //                        i_SectorTypeId = (int)A.i_SectorTypeId,
        //                        v_SectorTypeIdName = C.v_Value1,
        //                        v_IdentificationNumber = A.v_IdentificationNumber,
        //                        v_Name = A.v_Name,
        //                        v_Address = A.v_Address,
        //                        v_PhoneNumber = A.v_PhoneNumber,
        //                        v_Mail = A.v_Mail,
        //                        v_CreationUser = J1.v_UserName,
        //                        v_UpdateUser = J2.v_UserName,
        //                        d_CreationDate = A.d_InsertDate,
        //                        d_UpdateDate = A.d_UpdateDate,
        //                        i_IsDeleted = A.i_IsDeleted
        //                    };

        //        if (!string.IsNullOrEmpty(pstrFilterExpression))
        //        {
        //            query = query.Where(pstrFilterExpression);
        //        }
        //        if (!string.IsNullOrEmpty(pstrSortExpression))
        //        {
        //            query = query.OrderBy(pstrSortExpression);
        //        }
        //        if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
        //        {
        //            int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
        //            query = query.Skip(intStartRowIndex);
        //        }
        //        if (pintResultsPerPage.HasValue)
        //        {
        //            query = query.Take(pintResultsPerPage.Value);
        //        }

        //        List<OrganizationList> objData = query.ToList();
        //        pobjOperationResult.Success = 1;
        //        return objData;

        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return null;
        //    }
        //}

        //public int GetOrganizationsCount(ref OperationResult pobjOperationResult, string filterExpression)
        //{
        //    mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        var query = from a in dbContext.Organizations select a;

        //        if (!string.IsNullOrEmpty(filterExpression))
        //            query = query.Where(filterExpression);

        //        int intResult = query.Count();

        //        pobjOperationResult.Success = 1;
        //        return intResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return 0;
        //    }
        //}

        //public OrganizationDto GetOrganization(ref OperationResult pobjOperationResult, int pintOrganizationId)
        //{
        //    mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        OrganizationDto objDtoEntity = null;

        //        var objEntity = (from a in dbContext.Organizations
        //                         where a.i_OrganizationId == pintOrganizationId
        //                         select a).FirstOrDefault();

        //        if (objEntity != null)
        //            objDtoEntity = OrganizationAssembler.ToDTO(objEntity);

        //        pobjOperationResult.Success = 1;
        //        return objDtoEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return null;
        //    }
        //}

        //public void AddOrganization(ref OperationResult pobjOperationResult, OrganizationDto pobjDtoEntity, List<string> ClientSession)
        //{
        //    mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        Organization objEntity = OrganizationAssembler.ToEntity(pobjDtoEntity);

        //        objEntity.d_InsertDate = DateTime.Now;
        //        objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
        //        objEntity.i_IsDeleted = 0;
        //        // Autogeneramos el Pk de la tabla
        //        int SecuentialId = new Utils().GetNextSecuentialId(1, 5);
        //        objEntity.i_OrganizationId = SecuentialId;

        //        dbContext.AddToOrganizations(objEntity);
        //        dbContext.SaveChanges();

        //        pobjOperationResult.Success = 1;
        //        // Llenar entidad Log
        //        new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.CREACION, "ORGANIZACIÓN", "i_OrganizationId=" + objEntity.i_OrganizationId.ToString(), Constants.Success.Ok, null);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        // Llenar entidad Log
        //        new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.CREACION, "ORGANIZACIÓN", "i_OrganizationId=" + pobjDtoEntity.i_OrganizationId.ToString(), Constants.Success.Failed, ex.Message);
        //        return;
        //    }
        //}

        //public void UpdateOrganization(ref OperationResult pobjOperationResult, OrganizationDto pobjDtoEntity, List<string> ClientSession)
        //{
        //    mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

        //        // Obtener la entidad fuente
        //        var objEntitySource = (from a in dbContext.Organizations
        //                               where a.i_OrganizationId == pobjDtoEntity.i_OrganizationId
        //                               select a).FirstOrDefault();

        //        // Crear la entidad con los datos actualizados
        //        pobjDtoEntity.d_UpdateDate = DateTime.Now;
        //        pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
        //        Organization objEntity = OrganizationAssembler.ToEntity(pobjDtoEntity);

        //        // Copiar los valores desde la entidad actualizada a la Entidad Fuente
        //        dbContext.Organizations.ApplyCurrentValues(objEntity);

        //        // Guardar los cambios
        //        dbContext.SaveChanges();

        //        pobjOperationResult.Success = 1;
        //        // Llenar entidad Log
        //        new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.ACTUALIZACION, "ORGANIZACIÓN", "i_OrganizationId=" + objEntity.i_OrganizationId.ToString(), Constants.Success.Ok, null);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        // Llenar entidad Log
        //        new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.ACTUALIZACION, "ORGANIZACIÓN", "i_OrganizationId=" + pobjDtoEntity.i_OrganizationId.ToString(), Constants.Success.Failed, ex.Message);
        //        return;
        //    }
        //}

        //public void DeleteOrganization(ref OperationResult pobjOperationResult, int pintOrganizationId, List<string> ClientSession)
        //{
        //    mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

        //        // Obtener la entidad fuente
        //        var objEntitySource = (from a in dbContext.Organizations
        //                               where a.i_OrganizationId == pintOrganizationId
        //                               select a).FirstOrDefault();

        //        // Crear la entidad con los datos actualizados
        //        objEntitySource.d_UpdateDate = DateTime.Now;
        //        objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
        //        objEntitySource.i_IsDeleted = 1;

        //        // Guardar los cambios
        //        dbContext.SaveChanges();

        //        pobjOperationResult.Success = 1;
        //        // Llenar entidad Log
        //        new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.ELIMINACION, "ORGANIZACIÓN", "", Constants.Success.Ok, null);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        // Llenar entidad Log
        //        new Utils().SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], Constants.LogEventType.ELIMINACION, "ORGANIZACIÓN", "", Constants.Success.Failed, ex.Message);
        //        return;
        //    }
        //}

        //public List<OrganizationDto> GetOrganizationsBySystemUserIdAndNodeId(ref OperationResult pobjOperationResult, int pintNodeId, int pintPersonId, int pintSystemNodeId)
        //{
        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        List<OrganizationDto> objListOrganizationDto;
        //        objListOrganizationDto = (from a in dbContext.SystemUserContexProfiles
        //                                  join o in dbContext.Organizations on a.i_OrganizationId equals o.i_OrganizationId
        //                                  where a.i_NodeId == pintNodeId && a.i_PersonId == pintPersonId && a.i_SystemUserNodeId == pintSystemNodeId
        //                                  select new OrganizationDto
        //                                  {
        //                                      i_OrganizationId = Convert.ToInt32(a.i_OrganizationId),
        //                                      v_Name = o.v_Name
        //                                  }).Distinct().ToList();
        //        pobjOperationResult.Success = 1;
        //        return objListOrganizationDto;

        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return null;
        //    }

        //}   

        public List<KeyValueDTO> GetProtocolsByOrganizationForCombo(ref OperationResult pobjOperationResult, string v_CustomerOrganizationId, string v_CustomerLocationId, string pstrSortExpression)
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

        public string AddLocation(ref OperationResult pobjOperationResult, locationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                location objEntity = locationAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 14), "OL");
                objEntity.v_LocationId = NewId;

                dbContext.AddTolocation(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "SEDE", "v_LocationId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "SEDE", "v_LocationId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);

                return null;
            }
        }

        public List<Sigesoft.Node.WinClient.BE.WarehouseList> GetWarehousePagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.warehouse
                            join B in dbContext.location on A.v_LocationId equals B.v_LocationId
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                             equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J4 in dbContext.datahierarchy on new { ItemId = A.i_CostCenterId.Value, groupId = 110 }
                                                           equals new { ItemId = J4.i_ItemId, groupId = J4.i_GroupId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new Sigesoft.Node.WinClient.BE.WarehouseList
                            {
                                v_OrganizationId = A.v_OrganizationId,
                                v_WarehouseId = A.v_WarehouseId,
                                v_LocationId = A.v_LocationId,
                                v_LocationIdName = B.v_Name,
                                i_CostCenterId = A.i_CostCenterId.Value,
                                v_CenterCostoName = J4.v_Value1,
                                v_Name = A.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted
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

                List<Sigesoft.Node.WinClient.BE.WarehouseList> objData = query.ToList();
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

        public void DeleteLocation(ref OperationResult pobjOperationResult, string pstrLocationId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.location
                                       where a.v_LocationId == pstrLocationId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "SEDE", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "SEDE", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public List<KeyValueDTO> GetJoinOrganizationAndLocation(ref OperationResult pobjOperationResult,int pintSystemUserId)
        {
            //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                //var query = (from n in dbContext.node
                //             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                //             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                //                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                //             from J1 in j1_join.DefaultIfEmpty()
                //             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                //             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             //where n.i_NodeId == pintNodeId &&
                             //      n.i_IsDeleted == 0 &&
                             //      a.i_IsDeleted == 0
                var query = (from a in dbContext.protocolsystemuser
                             join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                             join c in dbContext.organization on b.v_CustomerOrganizationId equals c.v_OrganizationId
                             join d in dbContext.location on b.v_CustomerLocationId equals d.v_LocationId
                             where (a.i_SystemUserId == pintSystemUserId) &&
                             (a.i_IsDeleted == 0)
                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = c.v_Name,
                                 v_LocationName = d.v_Name,
                                 v_LocationId = d.v_LocationId,
                                 v_OrganizationId = c.v_OrganizationId,
                                 //i_NodeId = J1.i_NodeId,
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

                var y = q.GroupBy(g => g.Value1)
                        .Select(s => s.First());

                List<KeyValueDTO> KeyValueDTO = y.OrderBy(p => p.Value1).ToList();

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

        public List<KeyValueDTO> GetJoinOrganizationAndLocationALL(ref OperationResult pobjOperationResult)
        {
            //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                //var query = (from n in dbContext.node
                //             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                //             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                //                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                //             from J1 in j1_join.DefaultIfEmpty()
                //             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                //             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                //where n.i_NodeId == pintNodeId &&
                //      n.i_IsDeleted == 0 &&
                //      a.i_IsDeleted == 0
                var query = (from a in dbContext.protocolsystemuser
                             join b in dbContext.protocol on a.v_ProtocolId equals b.v_ProtocolId
                             join c in dbContext.organization on b.v_CustomerOrganizationId equals c.v_OrganizationId
                             join d in dbContext.location on b.v_CustomerLocationId equals d.v_LocationId
                             where 
                             (a.i_IsDeleted == 0)
                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = c.v_Name,
                                 v_LocationName = d.v_Name,
                                 v_LocationId = d.v_LocationId,
                                 v_OrganizationId = c.v_OrganizationId,
                                 //i_NodeId = J1.i_NodeId,
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

                var y = q.GroupBy(g => g.Value1)
                        .Select(s => s.First());

                List<KeyValueDTO> KeyValueDTO = y.OrderBy(p => p.Value1).ToList();

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

        public List<KeyValueDTO> GetProtocolsByOrganizationForCombo_(ref OperationResult pobjOperationResult, string v_CustomerOrganizationId, string v_CustomerLocationId, int? pintTipoESOId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.protocol
                            where (a.v_CustomerOrganizationId == v_CustomerOrganizationId) &&
                            (a.v_CustomerLocationId == v_CustomerLocationId) &&
                            (a.i_IsDeleted == 0) &&
                            (a.i_EsoTypeId == pintTipoESOId)
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

        #region Sedes
        public List<Sigesoft.Node.WinClient.BE.LocationList> GetLocationPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.location
                            join B in dbContext.organization on A.v_OrganizationId equals B.v_OrganizationId

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                          equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new Sigesoft.Node.WinClient.BE.LocationList
                            {
                                v_LocationId = A.v_LocationId,
                                v_OrganizationId = A.v_OrganizationId,
                                v_OrganizationName = B.v_Name,
                                v_Name = A.v_Name,
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

                List<Sigesoft.Node.WinClient.BE.LocationList> objData = query.ToList();
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

      
        public locationDto GetLocation(ref OperationResult pobjOperationResult, string pstrLocationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                locationDto objDtoEntity = null;

                var objEntity = (from a in dbContext.location
                                 where a.v_LocationId == pstrLocationId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = locationAssembler.ToDTO(objEntity);

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

        public string UpdateLocation(ref OperationResult pobjOperationResult, locationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.location
                                       where a.v_LocationId == pobjDtoEntity.v_LocationId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                location objEntity = locationAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.location.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
            return objEntity.v_LocationId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SEDE", "v_LocationId=" + pobjDtoEntity.v_LocationId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

     
        #endregion

        #region GESO

        public List<Sigesoft.Node.WinClient.BE.GroupOccupationList> GetGroupOccupationPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.groupoccupation
                            join B in dbContext.location on A.v_LocationId equals B.v_LocationId
                            join C in dbContext.organization on B.v_OrganizationId equals C.v_OrganizationId

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0 && C.v_OrganizationId == pstrOrganizationId

                            select new Sigesoft.Node.WinClient.BE.GroupOccupationList
                            {
                                v_OrganizationId = B.v_OrganizationId,
                                v_GroupOccupationId = A.v_GroupOccupationId,
                                v_LocationId = A.v_LocationId,
                                v_LocationName = B.v_Name,
                                v_Name = A.v_Name,
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

                List<Sigesoft.Node.WinClient.BE.GroupOccupationList> objData = query.ToList();
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

        public int GetGroupOccupationCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.groupoccupation select a;

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

        public groupoccupationDto GetGroupOccupation(ref OperationResult pobjOperationResult, string pstrGroupOccupationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                groupoccupationDto objDtoEntity = null;

                var objEntity = (from a in dbContext.groupoccupation
                                 where a.v_GroupOccupationId == pstrGroupOccupationId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = groupoccupationAssembler.ToDTO(objEntity);

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

        public void AddGroupOccupation(ref OperationResult pobjOperationResult, groupoccupationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                groupoccupation objEntity = groupoccupationAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 13), "OG"); ;
                objEntity.v_GroupOccupationId = NewId;

                dbContext.AddTogroupoccupation(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "GESO", "v_GroupOccupationId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "GESO", "v_GroupOccupationId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateGroupOccupation(ref OperationResult pobjOperationResult, groupoccupationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.groupoccupation
                                       where a.v_GroupOccupationId == pobjDtoEntity.v_GroupOccupationId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                groupoccupation objEntity = groupoccupationAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.groupoccupation.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "GESO", "v_GroupOccupationId=" + objEntity.v_GroupOccupationId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "GESO", "v_GroupOccupationId=" + pobjDtoEntity.v_GroupOccupationId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteGroupOccupation(ref OperationResult pobjOperationResult, string pstrGroupOccupationId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.groupoccupation
                                       where a.v_GroupOccupationId == pstrGroupOccupationId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "GESO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "GESO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }


        #endregion

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

                        //// Graba almacenes
                        //OperationResult objOperationResult1 = new OperationResult();

                        //if (pobjWarehouseList != null)
                        //{
                        //    AddWarehouse(ref objOperationResult1, pobjWarehouseList, ClientSession);
                        //}

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

                        //// Graba almacenes
                        //OperationResult objOperationResult1 = new OperationResult();

                        //if (pobjWarehouseList != null)
                        //{
                        //    AddWarehouse(ref objOperationResult1, pobjWarehouseList, ClientSession);
                        //}

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

        #region OrdenReportes
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


        #endregion

        public List<KeyValueDTO> GetEmpresasPorUsuarioExterno(int pintSystemUserId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from c in dbContext.protocolsystemuser
                            join d in dbContext.protocol on c.v_ProtocolId equals d.v_ProtocolId
                            join a in dbContext.organization on d.v_CustomerOrganizationId equals a.v_OrganizationId
                            join b in dbContext.location on a.v_OrganizationId equals b.v_OrganizationId
                            where a.i_IsDeleted == 0
                            select new KeyValueDTO
                            {
                                Id = a.v_OrganizationId + "|" + b.v_LocationId,
                                Value1 = a.v_Name + " / " + b.v_Name,
                            };

                List<KeyValueDTO> objDataList = query.OrderBy(p => p.Value1).Distinct().ToList();

               
                return objDataList;

            }
            catch (Exception ex)
            {
          
                return null;
            }
        }

        #region Mayquel

        public List<OrdenReportes> GetAllOrdenReporteNuevo(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.systemparameter
                            where A.i_IsDeleted == 0 && A.i_GroupId == 250
                            select new OrdenReportes
                            {
                                b_Seleccionar = false,
                                v_ComponenteId = A.v_Value2,
                                v_NombreReporte = A.v_Value1,
                                i_Orden = A.i_Sort.Value,
                                //v_NombreCrystal = "",
                                //i_NombreCrystalId = -1
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

                List<OrdenReportes> objData = query.ToList();
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
               
        public void InsertOrderReport(ref OperationResult pobjOperationResult, List<systemparameterDto> ListReport, List<string> ClientSession)
        {
            try
            {
                
                foreach (systemparameterDto value in ListReport) {
                    
                    systemparameterDto pobjDtoEntity = new systemparameterDto();
                    pobjDtoEntity = value;

                    SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                   // systemparameterDto idcomponent = new systemparameterDto();

                    var idcomponente = (from a in dbContext.systemparameter
                                           where a.v_Value2 == pobjDtoEntity.v_Value2 && a.v_Value1 == pobjDtoEntity.v_Value1
                                           select a).FirstOrDefault();

                    // Obtener la entidad fuente
                    var objEntitySource = (from a in dbContext.systemparameter
                                           where a.v_Value2 == pobjDtoEntity.v_Value2 && a.i_ParameterId == idcomponente.i_ParameterId
                                           select a).FirstOrDefault();
                    
                    // Crear la entidad con los datos actualizados
                    pobjDtoEntity.d_UpdateDate = DateTime.Now;
                    pobjDtoEntity.i_IsDeleted = 0;
                    pobjDtoEntity.i_ParameterId = idcomponente.i_ParameterId;
                    pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    systemparameter objEntity = systemparameterAssembler.ToEntity(pobjDtoEntity);

                    // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                    dbContext.systemparameter.ApplyCurrentValues(objEntity);

                    // Guardar los cambios
                    dbContext.SaveChanges();

                    pobjOperationResult.Success = 1;
                    // Llenar entidad Log
                    //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "GESO", "" , Success.Ok, null);
                    
                    }
                return;
                }
                
            catch (Exception ex)
            {
                ex.Message.ToString();
                //pobjOperationResult.Success = 0;
                //pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "GESO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }
        #endregion

    }
}
