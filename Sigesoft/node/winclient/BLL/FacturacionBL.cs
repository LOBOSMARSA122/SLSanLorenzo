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
    public class FacturacionBL
    {
        public List<FacturacionList> GetFacturacionPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate, string TipoFecha)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.facturacion

                            // Empresa / Sede Cliente **************
                            join oc in dbContext.organization on new { a = A.v_EmpresaCliente }
                                    equals new { a = oc.v_OrganizationId } into oc_join
                            from oc in oc_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                 equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J4 in dbContext.datahierarchy on new { a = A.i_EstadoFacturacion.Value, b = 117 }
                                            equals new { a = J4.i_ItemId, b = J4.i_GroupId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                         
                            where A.i_IsDeleted == 0
                            select new FacturacionList
                            {
                                v_FacturacionId =  A.v_FacturacionId,
                                d_FechaRegistro = A.d_FechaRegistro.Value,
                                d_FechaCobro = A.d_FechaCobro,
                                v_NumeroFactura = A.v_NumeroFactura,
                                i_EstadoFacturacion = A.i_EstadoFacturacion.Value,
                                v_EstadoFacturacion =J4.v_Value1,
                                EmpresaClienteId =  oc.v_Name,
                                v_EmpresaCliente = A.v_EmpresaCliente,
                                v_EmpresaSede = A.v_EmpresaSede,
                                d_MontoTotal = A.d_MontoTotal.Value,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,

                              
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    if (TipoFecha == "F")
                    {
                        query = query.Where("d_FechaRegistro >= @0 && d_FechaRegistro <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                    }
                    else
                    {
                        query = query.Where("d_FechaCobro >= @0 && d_FechaCobro <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                    }
                  
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

                List<FacturacionList> objData = query.ToList();
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

        public List<ServicioFacturado> GetFacturacionPagedAndFilteredAMC(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate, string TipoFecha)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.facturacion
                            join B in dbContext.facturaciondetalle on A.v_FacturacionId equals B.v_FacturacionId
                              join C in dbContext.service on B.v_ServicioId equals C.v_ServiceId
                              join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                            // Empresa / Sede Cliente **************
                            join oc in dbContext.organization on new { a = A.v_EmpresaCliente }
                                    equals new { a = oc.v_OrganizationId } into oc_join
                            from oc in oc_join.DefaultIfEmpty()

                            //join lc in dbContext.location on new { a = A.v_EmpresaCliente, b = A.v_CustomerLocationId }
                            //    equals new { a = lc.v_OrganizationId, b = lc.v_LocationId } into lc_join
                            //from lc in lc_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                 equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J4 in dbContext.datahierarchy on new { a = A.i_EstadoFacturacion.Value, b = 117 }
                                            equals new { a = J4.i_ItemId, b = J4.i_GroupId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new FacturacionList
                            {
                                v_EmpresaCliente = oc.v_Name,
                                v_NumeroFactura = A.v_NumeroFactura,
                                v_EstadoFacturacion = J4.v_Value1,
                                d_MontoTotal = A.d_MontoTotal.Value,
                                d_FechaCobro = A.d_FechaCobro,

                                v_FacturacionId = A.v_FacturacionId,
                                d_FechaRegistro = A.d_FechaRegistro.Value,
                                i_EstadoFacturacion = A.i_EstadoFacturacion.Value,
                                EmpresaClienteId = oc.v_OrganizationId,
                                //v_EmpresaSede = lc.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,
                                d_Deducible = D.v_Deducible.Value
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    if (TipoFecha == "F")
                    {
                        query = query.Where("d_FechaRegistro >= @0 && d_FechaRegistro <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                    }
                    else
                    {
                        query = query.Where("d_FechaCobro >= @0 && d_FechaCobro <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                    }

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


                List<FacturacionList> objDataConSinDeducible = new  List<FacturacionList>();
                foreach (var item in query)
                {
                    if (item.d_Deducible != null && item.d_Deducible.ToString() != "0.00")
                    {
                        item.d_MontoTotal = item.d_MontoTotal - (item.d_Deducible * item.d_MontoTotal / 100);
                    }
                    else
                    {
                        item.d_MontoTotal = item.d_MontoTotal;
                    }

                    item.v_EmpresaCliente = item.v_EmpresaCliente;
                    item.v_NumeroFactura = item.v_NumeroFactura;
                    item.v_EstadoFacturacion = item.v_EstadoFacturacion;
                    item.d_FechaCobro = item.d_FechaCobro;
                    item.v_FacturacionId = item.v_FacturacionId;
                    item.d_FechaRegistro = item.d_FechaRegistro;
                    item.i_EstadoFacturacion = item.i_EstadoFacturacion;
                    item.EmpresaClienteId = item.EmpresaClienteId;
                    item.v_EmpresaSede = item.v_EmpresaSede;
                    item.v_CreationUser = item.v_CreationUser;
                    item.v_UpdateUser = item.v_UpdateUser;
                    item.d_CreationDate = item.d_CreationDate;
                    item.d_UpdateDate = item.d_UpdateDate;
                    item.i_IsDeleted = item.i_IsDeleted;
                    item.d_Deducible = item.d_Deducible;
                    objDataConSinDeducible.Add(item);
                }



                List<FacturacionList> objData = objDataConSinDeducible.ToList();
                List<FacturacionList> ListaDetalleAgrupada = objDataConSinDeducible.ToList();

                objData = objData.GroupBy(x => new { x.v_EmpresaCliente })
                                               .Select(group => group.First())
                                               .ToList();

                ListaDetalleAgrupada = objDataConSinDeducible.ToList().GroupBy(x => new { x.v_FacturacionId })
                                             .Select(group => group.First())
                                             .ToList();

                //Cargar la Cabecera
                List<ServicioFacturado> Lista1 = new List<ServicioFacturado>();
                ServicioFacturado obj1;
                foreach (var item in objData)
                {
                    obj1 = new ServicioFacturado();
                    obj1.EmpresaCliente = item.v_EmpresaCliente;

                    //cARGAR dETALLE
                    List<ServicioFacturadoDetalle> Lista2 = new List<ServicioFacturadoDetalle>();
                    ServicioFacturadoDetalle obj2;
                    var ListaDetalle = ListaDetalleAgrupada.ToList().FindAll(p => p.v_EmpresaCliente == item.v_EmpresaCliente);
                    if (ListaDetalle.Count != 0)
                    {
                        foreach (var item2 in ListaDetalle)
                        {
                            obj2 = new ServicioFacturadoDetalle();
                            obj2.v_FacturacionId = item2.v_FacturacionId;
                            obj2.NroFactura = item2.v_NumeroFactura;
                            obj2.EstadoFacturacion = item2.v_EstadoFacturacion;
                            obj2.MontoFacturado = (double)item2.d_MontoTotal;
                            obj2.FechaCobro = item2.d_FechaCobro;

                            obj2.v_CreationUser = item2.v_CreationUser;
                            obj2.v_UpdateUser = item2.v_UpdateUser;
                            obj2.d_CreationDate = item2.d_CreationDate;
                            obj2.d_UpdateDate = item2.d_UpdateDate;
                            Lista2.Add(obj2);
                        }
                    }
                    obj1.TotalCobrado = ListaDetalle.FindAll(p => p.i_EstadoFacturacion == 1).Sum(s => (double)s.d_MontoTotal);
                    obj1.TotalCobrar = ListaDetalle.FindAll(p => p.i_EstadoFacturacion == 2).Sum(s => (double)s.d_MontoTotal);
                    obj1.ServicioFacturadoDetalle = Lista2;

                    Lista1.Add(obj1);

                }





                pobjOperationResult.Success = 1;
                return Lista1;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public string AddFacturacion(ref OperationResult pobjOperationResult, facturacionDto pobjDtoEntity,List<facturaciondetalleDto> ListaFacturacionDetalle , List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            string NewIdDetalle = "(No generado)";
            try
            {
                ServiceBL oServiceBL = new ServiceBL();
                serviceDto oserviceDto = new serviceDto();


                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                facturacion objEntity = facturacionAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 200), "UI");
                objEntity.v_FacturacionId = NewId;

                dbContext.AddTofacturacion(objEntity);
                dbContext.SaveChanges();

                if (ListaFacturacionDetalle != null)
                {
                    foreach (var item in ListaFacturacionDetalle)
                    {
                        // Crear el detalle del movimiento
                        facturaciondetalle objDetailEntity = facturaciondetalleAssembler.ToEntity(item);

                        NewIdDetalle = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 200), "UU");
                        objDetailEntity.v_FacturacionDetalleId = NewIdDetalle;
                        objDetailEntity.v_FacturacionId = NewId;

                        objDetailEntity.v_ServicioId = item.v_ServicioId;
                        objDetailEntity.d_Monto = 0;

                        objDetailEntity.d_InsertDate = DateTime.Now;
                        objDetailEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                        objDetailEntity.i_IsDeleted = 0;



                        // Agregar el detalle del movimiento a la BD
                        dbContext.AddTofacturaciondetalle(objDetailEntity);
                        oserviceDto.v_ServiceId =  item.v_ServicioId;
                        oServiceBL.UpdateFlagFacturacion(oserviceDto,1);
                    }
                    // Guardar todo en la BD
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                    return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                 return null;
            }
        }
        
        public facturacionDto GetFacturacion(ref OperationResult pobjOperationResult, string pstrFacturacionId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                facturacionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.facturacion
                                 where a.v_FacturacionId == pstrFacturacionId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = facturacionAssembler.ToDTO(objEntity);

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

        public List<FacturacionDetalleList> GetListFacturacionDetalle(ref OperationResult pobjOperationResult, string pstrFacturacionId)
        {
            try
            {
                 SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                 var query = (from A in dbContext.facturaciondetalle
                              join C in dbContext.service on A.v_ServicioId  equals C.v_ServiceId
                              join D in dbContext.protocol on C.v_ProtocolId equals D.v_ProtocolId

                              join L in dbContext.systemparameter on new { a = D.i_EsoTypeId.Value, b = 118 }
                                equals new { a = L.i_ParameterId, b = L.i_GroupId } into L_join
                              from L in L_join.DefaultIfEmpty()

                              join go in dbContext.groupoccupation on D.v_GroupOccupationId equals go.v_GroupOccupationId into go_join
                              from go in go_join.DefaultIfEmpty()

                              join B in dbContext.person on C.v_PersonId equals B.v_PersonId
                              join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                              from J1 in J1_join.DefaultIfEmpty()

                              join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                              equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                              from J2 in J2_join.DefaultIfEmpty()
                   

                              where A.v_FacturacionId == pstrFacturacionId && A.i_IsDeleted == 0
                              select new FacturacionDetalleList
                              {
                                  v_FacturacionDetalleId =  A.v_FacturacionDetalleId,
                                  v_FacturacionId = A.v_FacturacionId,
                                  v_ServicioId =  A.v_ServicioId,
                                  d_Monto = A.d_Monto,
                                  d_ServiceDate = C.d_ServiceDate,
                                  Trabajador = B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_FirstName,
                                  v_CreationUser = J1.v_UserName,
                                  v_UpdateUser = J2.v_UserName,
                                  d_CreationDate = A.d_InsertDate,
                                  d_UpdateDate = A.d_UpdateDate,
                                  i_IsDeleted = A.i_IsDeleted.Value,
                                  TipoExamen =go.v_Name,
                                  Perfil = L.v_Value1,
                                  Igv = A.d_Monto *18/100,
                                  Total = A.d_Monto + (A.d_Monto * 18 / 100)

                              }).ToList();

                 FacturacionDetalleList oFacturacionDetalleList;
                 List<FacturacionDetalleList> _ListaFacturacionList = new List<FacturacionDetalleList>();
                 foreach (var item in query)
                 {
                      oFacturacionDetalleList = new FacturacionDetalleList();
                      oFacturacionDetalleList.v_ServicioId = item.v_ServicioId;
                      oFacturacionDetalleList.d_ServiceDate = item.d_ServiceDate;
                      oFacturacionDetalleList.Trabajador = item.Trabajador;
                      oFacturacionDetalleList.v_ProtocolId = item.v_ProtocolId;
                      oFacturacionDetalleList.Perfil = item.Perfil;
                      oFacturacionDetalleList.TipoExamen = item.TipoExamen;
                      var valor = new ServiceBL().GetServiceCostfloat(item.v_ServicioId);

                    oFacturacionDetalleList.d_Monto = decimal.Parse(valor.ToString());
                    oFacturacionDetalleList.Igv = oFacturacionDetalleList.d_Monto * 18/100;
                    oFacturacionDetalleList.Total = oFacturacionDetalleList.d_Monto + oFacturacionDetalleList.Igv;


                    _ListaFacturacionList.Add(oFacturacionDetalleList);
                
                 }

                 return _ListaFacturacionList;
            }
            catch (Exception ex)
            {
                
                  pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void UpdateFacturacion(ref OperationResult pobjOperationResult, facturacionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.facturacion
                                       where a.v_FacturacionId == pobjDtoEntity.v_FacturacionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.i_IsDeleted = 0;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                facturacion objEntity = facturacionAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.facturacion.ApplyCurrentValues(objEntity);

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

        public void DeleteFacturacion(ref OperationResult pobjOperationResult, string pstrFacturacionId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                List<FacturacionDetalleList> Lista = new List<FacturacionDetalleList>();
                    OperationResult objOperationResult = new OperationResult();
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ServiceBL oServiceBL = new ServiceBL();
                serviceDto oserviceDto = new serviceDto();


                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.facturacion
                                       where a.v_FacturacionId == pstrFacturacionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;


              Lista=  GetListFacturacionDetalle(ref objOperationResult, pstrFacturacionId);

              foreach (var item in Lista)
              {

                  DeleteFacturacionDetalle(ref objOperationResult, item.v_FacturacionDetalleId, ClientSession);
                  oserviceDto.v_ServiceId = item.v_ServicioId;
                  oServiceBL.UpdateFlagFacturacion(oserviceDto, 0);
              }
              
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }

        public void DeleteFacturacionDetalle(ref OperationResult pobjOperationResult, string FacturacionDetalleId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.facturaciondetalle
                                       where a.v_FacturacionDetalleId == FacturacionDetalleId
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

        public List<llenarConsultaSigesoft> LlenarGrillaSigesfot(string psrtDni, string pstrIdOrganization, string pstrIdLocation, DateTime FechaInico, DateTime FechaFin, int pintTipoExamen, int pintTipoReporte, string[] ArrayServicios)
        {
            //mon.IsActive = true;

            string[] ExamenesLaboratorio = new string[] 
            { 
                Constants.GRUPO_Y_FACTOR_SANGUINEO_ID,
                Constants.LABORATORIO_HEMATOCRITO_ID,
                Constants.VDRL_ID,
                Constants.HEPATITIS_A_ID,
                Constants.HEPATITIS_C_ID,
                Constants.LABORATORIO_HEMOGLOBINA_ID,
                Constants.GLUCOSA_ID,
                Constants.ANTIGENO_PROSTATICO_ID,
                Constants.PARASITOLOGICO_SIMPLE_ID,
                Constants.TEST_ESTEREOPSIS_ID,
                Constants.COLESTEROL_ID,
                Constants.TRIGLICERIDOS_ID,
                Constants.AGLUTINACIONES_LAMINA_ID,
                Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID,
                Constants.CREATININA_ID,
                Constants.EXAMEN_ELISA_ID,
                Constants.HEMOGRAMA_COMPLETO_ID,
                Constants.EXAMEN_COMPLETO_DE_ORINA_ID,
                Constants.PARASITOLOGICO_SERIADO_ID,
                Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID,
                Constants.TGO_ID,
                Constants.TGP_ID,
                Constants.PLOMO_SANGRE_ID,
                Constants.UREA_ID,
                Constants.COLESTEROL_HDL_ID,
                Constants.COLESTEROL_LDL_ID,
                Constants.COLESTEROL_VLDL_ID,
            };

            try
            {
                //SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //var query = null;//dbContext.llenargrillasigesoft(psrtDni, pstrIdOrganization, pstrIdLocation, FechaInico, FechaFin, pintTipoExamen).ToList();//.OrderBy(p => p.T_Id_Componente).ToList().FindAll(p => p.T_Total != 0);
                //List<llenarConsultaSigesoft> Lista = new List<llenarConsultaSigesoft>();
                //llenarConsultaSigesoft o;

                //foreach (llenargrillasigesoftResult cs in query)
                //{
                //    o = new llenarConsultaSigesoft();
                //    o.EmpresaCliente = cs.T_EmpresaCliente;
                //    o.Nombre_Componente = cs.T_Nombre_Completo;
                //    o.Geso = cs.T_GESO;
                //    o.IdService = cs.T_Id_Servicio;
                //    //o.TipoESO = cs.T_ESO;
                //    o.ProtocoloId = cs.T_IdProtocolo;
                //    o.Total = Math.Round(Decimal.Parse(cs.T_Total.Value.ToString()), 2);
                //    Lista.Add(o);
                //}


                //if (pintTipoReporte == 1)
                //{

                //    var ListaFiltrada = Lista.FindAll(p => ArrayServicios.Contains(p.IdService));
                //    var result_dt1 = (from r in ListaFiltrada.AsEnumerable()
                //                      group r by r.Nombre_Componente into dtGroup
                //                      select new llenarConsultaSigesoft
                //                      {
                //                          Nombre_Componente = dtGroup.Key,
                //                          Contador = dtGroup.Count(),
                //                          Total = dtGroup.Sum(r => r.Total)
                //                          //Total = dtGroup.Key.tot
                //                      }).ToList();

                //    return result_dt1;
                //}
                //else if (pintTipoReporte == 2)
                //{
                //    ServiceBL oServiceBL = new ServiceBL();

                //    var ListaFiltrada = Lista.FindAll(p => ArrayServicios.Contains(p.IdService));
                //    //var TipoESO = oServiceBL.DEvolverTipoESOConcatenado(ListaFiltrada);  
                //    var result_dt2 = (from r in ListaFiltrada.AsEnumerable()
                //                      group r by new { r.Geso, r.ProtocoloId, r.IdService, r.TipoESO } into dtGroup
                //                      select new llenarConsultaSigesoft
                //                      {
                //                          Nombre_Componente = "EXÁMENES OCUPACIONALES PERFIL: " + dtGroup.Key.Geso,
                //                          Contador = dtGroup.Count(),
                //                          Total = dtGroup.Sum(r => r.Total),
                //                          IdService = dtGroup.Key.IdService,
                //                          ProtocoloId = dtGroup.Key.ProtocoloId,
                //                          TipoESO = dtGroup.Key.TipoESO
                //                      }).ToList();


                //    var final = (from x in result_dt2
                //                 group x by new { x.ProtocoloId, x.Geso, x.Nombre_Componente,x.TipoESO } into g
                //                 select new llenarConsultaSigesoft
                //                 {
                //                     Nombre_Componente = g.Key.Nombre_Componente,  //"EXÁMENES OCUPACIONALES PERFIL: " + g.Key.Geso,
                //                     Contador = g.Count(),
                //                     Total = g.Sum(r => r.Total),
                //                     TipoESO =g.Key.TipoESO
                //                 }).ToList();


                //    return final;
                //}
                //else
                //{
                //    ServiceBL oServiceBL = new ServiceBL();
                //    var ListaFiltrada = Lista.FindAll(p => ArrayServicios.Contains(p.IdService));
                //    //Calcular el nro de servicios
                //    int Servicios = ListaFiltrada.GroupBy(x => new { x.IdService })                                            
                //                            .ToList().Count();

                //    var TipoESO = oServiceBL.DEvolverTipoESOConcatenado(ListaFiltrada);  
                //    var result_dt3 = (from r in ListaFiltrada.AsEnumerable()
                //                      group r by r.EmpresaCliente into dtGroup
                //                      select new llenarConsultaSigesoft
                //                      {
                //                          Nombre_Componente = "EXÁMENES MÉDICOS OCUPACIONALES ",
                //                          Contador = Servicios,
                //                          Total = dtGroup.Sum(r => r.Total),
                //                          TipoESO = TipoESO
                //                      }).ToList();

                //    return result_dt3;
                //}

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        
        public List<llenarConsultaSigesoft> ComisionVendedor(string psrtDni, string pstrIdOrganization, string pstrIdLocation, DateTime FechaInico, DateTime FechaFin, int pintTipoExamen)
        {
            //mon.IsActive = true;

            //string[] ExamenesLaboratorio = new string[] 
            //{ 
            //    Constants.GRUPO_Y_FACTOR_SANGUINEO_ID,
            //    Constants.LABORATORIO_HEMATOCRITO_ID,
            //    Constants.VDRL_ID,
            //    Constants.HEPATITIS_A_ID,
            //    Constants.HEPATITIS_C_ID,
            //    Constants.LABORATORIO_HEMOGLOBINA_ID,
            //    Constants.GLUCOSA_ID,
            //    Constants.ANTIGENO_PROSTATICO_ID,
            //    Constants.PARASITOLOGICO_SIMPLE_ID,
            //    Constants.TEST_ESTEREOPSIS_ID,
            //    Constants.COLESTEROL_ID,
            //    Constants.TRIGLICERIDOS_ID,
            //    Constants.AGLUTINACIONES_LAMINA_ID,
            //    Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID,
            //    Constants.CREATININA_ID,
            //    Constants.EXAMEN_ELISA_ID,
            //    Constants.HEMOGRAMA_COMPLETO_ID,
            //    Constants.EXAMEN_COMPLETO_DE_ORINA_ID,
            //    Constants.PARASITOLOGICO_SERIADO_ID,
            //    Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID,
            //    Constants.TGO_ID,
            //    Constants.TGP_ID,
            //    Constants.PLOMO_SANGRE_ID,
            //    Constants.UREA_ID,
            //    Constants.COLESTEROL_HDL_ID,
            //    Constants.COLESTEROL_LDL_ID,
            //    Constants.COLESTEROL_VLDL_ID,
            //};

            //try
            //{
            //    SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            //    //var query = dbContext.llenargrillasigesoft(psrtDni, pstrIdOrganization, pstrIdLocation, FechaInico, FechaFin, pintTipoExamen).ToList().OrderBy(p => p.T_Id_Componente).ToList();

            //    //List<devolvervalorescomponenteResult> TieneValores = dbContext.devolvervalorescomponente(FechaInico, FechaFin,1).ToList();
            //    List<llenarConsultaSigesoft> Lista = new List<llenarConsultaSigesoft>();
            //    llenarConsultaSigesoft o;

            //    foreach (llenargrillasigesoftResult cs in query)
            //    {
            //       o = new llenarConsultaSigesoft();
            //            o.IdService = cs.T_Id_Servicio;
            //            o.Nombre_Componente = cs.T_Nombre_Completo;
            //            o.Total = Math.Round(Decimal.Parse(cs.T_Total.Value.ToString()), 2);

            //            Lista.Add(o);

            //        #region MyRegion
            //        ////VERIFICAR SI ES UN COMPONENTE DE LABORATORIO
            //        //var EsLab = query.FindAll(p => ExamenesLaboratorio.Contains(o.IdComponente)).ToList();
            //        //if (EsLab != null && EsLab.Count() != 0)
            //        //{

            //        //    foreach (var item in TieneValores)
            //        //    {
            //        //        if (item.IDCAMPO == Constants.GLUCOSA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.GLUCOSA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.GLUCOSA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.LABORATORIO_HEMATOCRITO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.LABORATORIO_HEMATOCRITO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.LABORATORIO_HEMATOCRITO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.VDRL_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {

            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.VDRL_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.VDRL_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.HEPATITIS_A_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.HEPATITIS_A_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.HEPATITIS_A_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.HEPATITIS_C_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.HEPATITIS_C_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.HEPATITIS_C_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.LABORATORIO_HEMOGLOBINA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.LABORATORIO_HEMOGLOBINA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.LABORATORIO_HEMOGLOBINA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.ANTIGENO_PROSTATICO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.ANTIGENO_PROSTATICO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.ANTIGENO_PROSTATICO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.PARASITOLOGICO_SIMPLE_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.PARASITOLOGICO_SIMPLE_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.PARASITOLOGICO_SIMPLE_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.ACIDO_URICO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.ACIDO_URICO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.ACIDO_URICO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.COLESTEROL_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.COLESTEROL_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.COLESTEROL_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.TRIGLICERIDOS_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.TRIGLICERIDOS_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.TRIGLICERIDOS_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.AGLUTINACIONES_LAMINA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.AGLUTINACIONES_LAMINA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.AGLUTINACIONES_LAMINA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.CREATININA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.CREATININA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.CREATININA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.EXAMEN_ELISA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.EXAMEN_ELISA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.EXAMEN_ELISA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.HEMOGRAMA_COMPLETO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.HEMOGRAMA_COMPLETO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.EXAMEN_COMPLETO_DE_ORINA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.EXAMEN_COMPLETO_DE_ORINA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.PARASITOLOGICO_SERIADO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.PARASITOLOGICO_SERIADO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.PARASITOLOGICO_SERIADO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }

            //        //        else if (item.IDCAMPO == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.TGO_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.TGO_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.TGO_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.TGP_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.TGP_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.TGP_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.PLOMO_SANGRE_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.PLOMO_SANGRE_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.PLOMO_SANGRE_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.UREA_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.UREA_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.UREA_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.COLESTEROL_HDL_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.COLESTEROL_HDL_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.COLESTEROL_HDL_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.COLESTEROL_LDL_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.COLESTEROL_LDL_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.COLESTEROL_LDL_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }
            //        //        else if (item.IDCAMPO == Constants.COLESTEROL_VLDL_ID_REALIZADO && item.VALUE1 == "1" && item.v_ServiceId == o.IdService)
            //        //        {
            //        //            o = new llenarConsultaSigesoft();
            //        //            o.Nombre_Componente = query.Find(p => p.IdComponente == Constants.COLESTEROL_VLDL_ID).Nombre_Componente;
            //        //            o.Total = Math.Round(Decimal.Parse(query.Find(p => p.IdComponente == Constants.COLESTEROL_VLDL_ID).Total.ToString()));

            //        //            Lista.Add(o);
            //        //        }

            //            //}
            //        #endregion

            //    }

            //    var result_dt = (from r in Lista.AsEnumerable()
            //                     group r by r.IdService into dtGroup
            //                     select new llenarConsultaSigesoft
            //                     {
            //                         IdService = dtGroup.Key,
            //                         Contador = dtGroup.Count(),
            //                         Total = dtGroup.Sum(r => r.Total)
            //                     }).ToList();

                return null;

            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }

        public List<ImprimirFactura> CabeceraFactura(string pstrFacturacionId)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {
                   
                    var objEntity = (from A in dbContext.facturacion
                                     join B in dbContext.organization on A.v_EmpresaCliente equals B.v_OrganizationId
                                     //join C in dbContext.facturaciondetalle on A.v_FacturacionId equals C.v_FacturacionId
                                     //join D in dbContext.service on C.v_ServicioId equals D.v_ServiceId
                                     //join 
                                     where A.v_FacturacionId == pstrFacturacionId
                                     select new ImprimirFactura
                                     {                                        
                                         NumeroFactura = A.v_NumeroFactura,
                                         RazonSocial = B.v_Name,
                                         Direccion = B.v_Address,
                                         Ruc = B.v_IdentificationNumber,
                                         FechaFacturacion = A.d_FechaRegistro,
                                         Igv = A.d_Igv.Value,
                                         SubTotal =A.d_SubTotal.Value,
                                         Total =A.d_MontoTotal.Value,
                                         Detraccion = A.d_Detraccion.Value
                                     }).ToList();

                    return objEntity;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ImprimirFactura> CabeceraFactura_(string pstrFacturacionId)
        {
            try
            {
                using (SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel())
                {

                    var objEntity = (from A in dbContext.facturacion
                                     join B in dbContext.organization on A.v_EmpresaCliente equals B.v_OrganizationId
                                     join C in dbContext.facturaciondetalle on A.v_FacturacionId equals C.v_FacturacionId
                                     join D in dbContext.service on C.v_ServicioId equals D.v_ServiceId
                                     join E in dbContext.person on D.v_PersonId equals E.v_PersonId
                                     where A.v_FacturacionId == pstrFacturacionId
                                     select new ImprimirFactura
                                     {
                                         NumeroFactura = A.v_NumeroFactura,
                                         RazonSocial = B.v_Name,
                                         Direccion = B.v_Address,
                                         Ruc = B.v_IdentificationNumber,
                                         FechaFacturacion = A.d_FechaRegistro,
                                         Igv = A.d_Igv.Value,
                                         SubTotal = A.d_SubTotal.Value,
                                         Total = A.d_MontoTotal.Value,
                                         Detraccion = A.d_Detraccion.Value,
                                         Paciente = E.v_FirstName + " " + E.v_FirstLastName + " " + E.v_SecondLastName,
                                         Poliza = E.v_NroPoliza,
                                         FechaNacimiento = E.d_Birthdate,
                                         Deducible = E.v_Deducible.Value,
                                         TotalPagar = A.d_MontoTotal.Value - (A.d_MontoTotal.Value * E.v_Deducible.Value / 100)
                                     }).ToList();

                    return objEntity;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    
    }
}
