using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Threading;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.BLL
{
   public class OrganizationBL
    {

        public string GetComentaryUpdateByOrganizationId(string organizationId)
        {
            using (var dbContext = new SigesoftEntitiesModel())
            {
                return dbContext.organization.FirstOrDefault(p => p.v_OrganizationId == organizationId).v_ComentaryUpdate;
            }
        }

       public bool OrganizacionExiste(string ruc)
       {
           try
           {
               using (var dbContext = new SigesoftEntitiesModel())
               {
                   return dbContext.organization.Any(p => p.v_IdentificationNumber.Equals(ruc) && p.i_IsDeleted == 0);
               }
           }
           catch 
           {
               return false;
           }
       }

       public bool OrganizacionExisteByName(string name)
       {
           try
           {
               var nameOrganization = name.Split('/').ToArray()[0].Trim();
               using (var dbContext = new SigesoftEntitiesModel())
               {
                   return dbContext.organization.Any(p => p.v_Name.Equals(nameOrganization) && p.i_IsDeleted == 0);
               }
           }
           catch
           {
               return false;
           }
       }

       public List<OrganizationList> GetOrganizationsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
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
                           where A.i_IsDeleted ==0
                           select new OrganizationList
                           {
                               b_Seleccionar = false,
                               v_OrganizationId = A.v_OrganizationId,
                               i_OrganizationTypeId = (int)A.i_OrganizationTypeId,
                               v_OrganizationTypeIdName = B.v_Value1,
                               i_SectorTypeId = (int)A.i_SectorTypeId,                              
                               v_EmailContacto = A.v_EmailContacto,
                               v_IdentificationNumber = A.v_IdentificationNumber,
                               v_Name = A.v_Name,                        
                               v_CreationUser = J1.v_UserName,
                               v_UpdateUser = J2.v_UserName,
                               d_CreationDate = A.d_InsertDate,
                               d_UpdateDate = A.d_UpdateDate,
                               i_IsDeleted = A.i_IsDeleted,
                               v_SectorName = A.v_SectorName,
                               v_SectorCodigo = A.v_SectorCodigo,
                               v_ContacName = A.v_ContacName + " / " + A.v_Mail,
                               v_Contacto = A.v_Contacto + " / " + A.v_EmailContacto,
                               v_ContactoMedico = A.v_ContactoMedico + " / " + A.v_EmailMedico,
                               v_Address = A.v_Address,
                               v_Observation = A.v_Observation,
                               v_PhoneNumber = A.v_PhoneNumber
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

               List<OrganizationList> objData = query.ToList();
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
               int intNodeId = int.Parse(ClientSession[0]);
               NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 5), "OO");
               objEntity.v_OrganizationId = NewId;
           

               dbContext.AddToorganization(objEntity);
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ORGANIZACIÓN", "i_OrganizationId=" + NewId.ToString(), Success.Ok, null);
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

       public List<EmpresaMigracion> EmpresasSalus()
       {
            //SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            //var list = new List<EmpresaMigracion>();
            //var query =  dbContext.empresassalus().ToList();

            //var empresas = query.GroupBy(g => g.v_Name).Select(s => s.First()).ToList();

            //  foreach (var empresa in empresas)
            //  {
            //      var oEmpresaMigracion = new EmpresaMigracion();

            //      oEmpresaMigracion.i_OrganizationTypeId = empresa.i_OrganizationTypeId;
            //      oEmpresaMigracion.v_IdentificationNumber = empresa.v_IdentificationNumber;
            //      oEmpresaMigracion.i_SectorTypeId = empresa.i_SectorTypeId;
            //      oEmpresaMigracion.v_Name = empresa.v_Name;
            //      oEmpresaMigracion.v_Address = empresa.v_Address;
            //      oEmpresaMigracion.v_PhoneNumber = empresa.v_PhoneNumber;
            //      oEmpresaMigracion.v_Mail = empresa.v_Mail;
            //      oEmpresaMigracion.v_ContacName = empresa.v_ContacName;
            //      oEmpresaMigracion.v_Observation = empresa.v_Observation;

            //      var sedes = empresas.FindAll(p => p.v_Name == oEmpresaMigracion.v_Name).ToList();
            //      var listSedes = new List<SedeMigracion>();

            //      foreach (var sede in sedes)
            //      {
            //          var oSedeMigracion = new SedeMigracion();
            //          oSedeMigracion.Sede = sede.Sede;
                      

            //          var gesos = sedes.FindAll(p => p.Sede == sede.Sede).ToList();
            //          var listGesos = new List<GesoMigracion>();
            //          foreach (var geso in gesos)
            //          {
            //              var oGesoMigracion = new GesoMigracion();
            //              oGesoMigracion.Geso = geso.GESO;
            //              listGesos.Add(oGesoMigracion);
            //          }

            //          oSedeMigracion.Gesos = listGesos;
            //          listSedes.Add(oSedeMigracion);

            //      }



            //      oEmpresaMigracion.Sedes = listSedes;

            //      list.Add(oEmpresaMigracion);
            //  }

            //  return list;
            return null;
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

       public void DeleteOrganization(ref OperationResult pobjOperationResult, string pstrOrganizationId, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.organization
                                      where a.v_OrganizationId == pstrOrganizationId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               objEntitySource.d_UpdateDate = DateTime.Now;
               objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               objEntitySource.i_IsDeleted = 1;

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ORGANIZACIÓN", "", Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ORGANIZACIÓN", "", Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
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
                                      join c in dbContext.nodeorganizationlocationwarehouseprofile on a.i_NodeId equals c.i_NodeId
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

       public void AddWarehouse(ref OperationResult pobjOperationResult, List<nodeorganizationlocationwarehouseprofileDto> pobjWarehouseList, List<string> ClientSession)
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

       public List<OrdenReportes> GetAllOrdenReporteNuevo(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
       {
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = from  A in dbContext.systemparameter                          
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


        #region OrdenReportes
       public List<OrdenReportes> GetOrdenReportes(ref OperationResult pobjOperationResult,string pstrEmpresaPlantillaId)
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

       public List<ServiceComponentList> GetOrdenReportes_(ref OperationResult pobjOperationResult, string pstrEmpresaPlantillaId)
       {
           //mon.IsActive = true;
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = from A in dbContext.ordenreporte
                           //join B in dbContext.component on A.v_ComponenteId equals B.v_ComponentId into B_join
                           //from B in B_join.DefaultIfEmpty()
                   where A.v_OrganizationId == pstrEmpresaPlantillaId
                        select new ServiceComponentList
                   {
                       v_ComponentId = A.v_ComponenteId,
                       v_ComponentName = A.v_NombreReporte,
                       i_Orden = A.i_Orden.Value,
                       //v_NombreReporte = A.v_NombreReporte,
                       v_NombreCrystal = A.v_NombreCrystal,
                       i_NombreCrystalId = A.i_NombreCrystalId.Value,
                       //i_CategoryId = B.i_CategoryId
                   };

               List<ServiceComponentList> objData = query.ToList();
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

                   dbContext.SaveChanges();
               }

               

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

        public organizationDto GetDataOrganizationByServiceiId(string serviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objOrganization = (from org in dbContext.organization
                                       join prot in dbContext.protocol on org.v_OrganizationId equals prot.v_EmployerOrganizationId
                                       join ser in dbContext.service on prot.v_ProtocolId equals ser.v_ProtocolId
                                       where ser.v_ServiceId == serviceId && ser.i_IsDeleted == 0
                                       select new organizationDto
                                       {
                                           v_Name = org.v_Name,
                                           v_Address = org.v_Address,
                                           v_PhoneNumber = org.v_PhoneNumber,
                                       }).FirstOrDefault();
                return objOrganization;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public organizationDto GetDataAseguradoraByServiceiId(string serviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var objOrganization = (from org in dbContext.organization
                    join prot in dbContext.protocol on org.v_OrganizationId equals prot.v_AseguradoraOrganizationId
                    join ser in dbContext.service on prot.v_ProtocolId equals ser.v_ProtocolId
                    where ser.v_ServiceId == serviceId && ser.i_IsDeleted == 0
                    select new organizationDto
                    {
                        v_Name = org.v_Name,
                        v_IdentificationNumber = org.v_IdentificationNumber,
                    }).FirstOrDefault();
                return objOrganization;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
