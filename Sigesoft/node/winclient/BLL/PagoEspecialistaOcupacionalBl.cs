using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class PagoEspecialistaOcupacionalBl
    {
        public List<PagoEspecialistaOcupacional> LoadGrid(DateTime? fInicio, DateTime? fFin, int systemUserId, int flagPagado)
        {
            try
            {
                var list = new List<PagoEspecialistaOcupacional>();
                var dbContext = new SigesoftEntitiesModel();

                var specialistConfigurations = dbContext.medico.Where(p => p.i_MasterServiceId == (int)MasterService.Eso && p.i_MasterServiceTypeId == (int)ServiceType.Empresarial && p.i_SystemUserId == systemUserId && p.i_IsDeleted == 0);
                
                var servicecomponents = (from a in dbContext.servicecomponent
                                        join b in dbContext.service on a.v_ServiceId equals b.v_ServiceId
                                        join c in dbContext.protocol on b.v_ProtocolId equals c.v_ProtocolId
                                        join f in dbContext.organization on c.v_EmployerOrganizationId equals f.v_OrganizationId
                                        join d in dbContext.person on b.v_PersonId equals d.v_PersonId
                                        join g in dbContext.organization on  c.v_CustomerOrganizationId  equals  g.v_OrganizationId
                                        join h in dbContext.organization on c.v_WorkingOrganizationId equals h.v_OrganizationId

                                         where a.i_ApprovedUpdateUserId == systemUserId && a.i_IsDeleted == 0 && b.i_PagoEspecialista == flagPagado
                                        select new
                                        {
                                            serviceId = a.v_ServiceId,
                                            protocolId = b.v_ProtocolId,
                                            EmployerId = c.v_EmployerOrganizationId,
                                            EmployerName = f.v_Name,
                                            CustomerId = c.v_CustomerOrganizationId,
                                            CustomerName = g.v_Name,
                                            WorkingId = c.v_WorkingOrganizationId,
                                            WorkingName = h.v_Name,
                                            Protocol = c.v_Name,
                                            Trabajador = d.v_FirstName + " " + d.v_FirstLastName + " " + d.v_SecondLastName,
                                            d_ServiceDate = b.d_ServiceDate
                                        }).ToList();

                servicecomponents = servicecomponents.FindAll(b => b.d_ServiceDate >= fInicio && b.d_ServiceDate <= fFin).ToList();

                foreach (var item in specialistConfigurations)
                {
                    var oPagoEspecialistaOcupacional = new PagoEspecialistaOcupacional();

                    if (item.v_WorkingOrganizationId == "-1" && item.v_EmployerOrganizationId == "-1")
                    {
                        var objServicesComponents = servicecomponents.FindAll(p => p.CustomerId == item.v_CustomerOrganizationId).ToList();
                        if (objServicesComponents.Count != 0)
                        {
                            oPagoEspecialistaOcupacional.Minera = objServicesComponents[0].CustomerName;
                            oPagoEspecialistaOcupacional.Contrata = objServicesComponents[0].EmployerName;
                            oPagoEspecialistaOcupacional.Terceros = objServicesComponents[0].WorkingName;
                            oPagoEspecialistaOcupacional.NroAtenciones = objServicesComponents.Count;
                            oPagoEspecialistaOcupacional.Precio = item.r_Price;
                            oPagoEspecialistaOcupacional.Total = item.r_Price * objServicesComponents.Count;

                            var listServices = new List<ServiciosEspecialistasOcupacional>();
                            foreach (var obj in objServicesComponents)
                            {
                                var oServiciosEspecialistasOcupacional = new ServiciosEspecialistasOcupacional();
                                oServiciosEspecialistasOcupacional.ServiceId = obj.serviceId;
                                oServiciosEspecialistasOcupacional.Trabajador = obj.Trabajador;
                                oServiciosEspecialistasOcupacional.Protocolo = obj.Protocol;
                                listServices.Add(oServiciosEspecialistasOcupacional);
                            }

                            oPagoEspecialistaOcupacional.ServiceIds = string.Join("|", listServices.Select(s => s.ServiceId));

                            oPagoEspecialistaOcupacional.Servicios = listServices;
                        }
                    }
                    else if (item.v_WorkingOrganizationId == "-1")
                    {
                        var objServicesComponents = servicecomponents.FindAll(p => p.CustomerId == item.v_CustomerOrganizationId && p.EmployerId == item.v_EmployerOrganizationId).ToList();
                        if (objServicesComponents.Count != 0)
                        {
                            oPagoEspecialistaOcupacional.Minera = objServicesComponents[0].CustomerName;
                            oPagoEspecialistaOcupacional.Contrata = objServicesComponents[0].EmployerName;
                            oPagoEspecialistaOcupacional.Terceros = objServicesComponents[0].WorkingName;
                            oPagoEspecialistaOcupacional.NroAtenciones = objServicesComponents.Count;
                            oPagoEspecialistaOcupacional.Precio = item.r_Price;
                            oPagoEspecialistaOcupacional.Total = item.r_Price * objServicesComponents.Count;

                            var listServices = new List<ServiciosEspecialistasOcupacional>();
                            foreach (var obj in objServicesComponents)
                            {
                                var oServiciosEspecialistasOcupacional = new ServiciosEspecialistasOcupacional();
                                oServiciosEspecialistasOcupacional.ServiceId = obj.serviceId;
                                oServiciosEspecialistasOcupacional.Trabajador = obj.Trabajador;
                                oServiciosEspecialistasOcupacional.Protocolo = obj.Protocol;
                                listServices.Add(oServiciosEspecialistasOcupacional);
                            }
                            oPagoEspecialistaOcupacional.ServiceIds = string.Join("|", listServices.Select(s => s.ServiceId));
                            oPagoEspecialistaOcupacional.Servicios = listServices;

                        }
                    }
                    else
                    {
                        var objServicesComponents = servicecomponents.FindAll(p => p.CustomerId == item.v_CustomerOrganizationId && p.EmployerId == item.v_EmployerOrganizationId && p.WorkingId == item.v_WorkingOrganizationId).ToList();
                        if (objServicesComponents.Count != 0)
                        {
                            oPagoEspecialistaOcupacional.Minera = objServicesComponents[0].CustomerName;
                            oPagoEspecialistaOcupacional.Contrata = objServicesComponents[0].EmployerName;
                            oPagoEspecialistaOcupacional.Terceros = objServicesComponents[0].WorkingName;
                            oPagoEspecialistaOcupacional.NroAtenciones = objServicesComponents.Count;
                            oPagoEspecialistaOcupacional.Precio = item.r_Price;
                            oPagoEspecialistaOcupacional.Total = item.r_Price * objServicesComponents.Count;

                            var listServices = new List<ServiciosEspecialistasOcupacional>();
                            foreach (var obj in objServicesComponents)
                            {
                                var oServiciosEspecialistasOcupacional = new ServiciosEspecialistasOcupacional();
                                oServiciosEspecialistasOcupacional.ServiceId = obj.serviceId;
                                oServiciosEspecialistasOcupacional.Trabajador = obj.Trabajador;
                                oServiciosEspecialistasOcupacional.Protocolo = obj.Protocol;
                                listServices.Add(oServiciosEspecialistasOcupacional);
                            }
                            oPagoEspecialistaOcupacional.ServiceIds = string.Join("|", listServices.Select(s => s.ServiceId));
                            oPagoEspecialistaOcupacional.Servicios = listServices;
                        }
                    }
                    if (oPagoEspecialistaOcupacional.NroAtenciones !=0 )
                    list.Add(oPagoEspecialistaOcupacional);

                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }
        public List<PagoEspecialistaOcupacional> LoadGrid_(DateTime? fInicio, DateTime? fFin, int systemUserId, int flagPagado)
        {
            try
            {
                var list = new List<PagoEspecialistaOcupacional>();
                var dbContext = new SigesoftEntitiesModel();

                var specialistConfigurations = dbContext.medico.Where(p => p.i_MasterServiceId == (int)MasterService.Eso && p.i_MasterServiceTypeId == (int)ServiceType.Empresarial && p.i_SystemUserId == systemUserId && p.i_IsDeleted == 0).ToList();

                var servicecomponents = (from a in dbContext.servicecomponent
                                         join b in dbContext.service on a.v_ServiceId equals b.v_ServiceId
                                         join c in dbContext.protocol on b.v_ProtocolId equals c.v_ProtocolId
                                         join f in dbContext.organization on c.v_EmployerOrganizationId equals f.v_OrganizationId
                                         join d in dbContext.person on b.v_PersonId equals d.v_PersonId
                                         join g in dbContext.organization on c.v_CustomerOrganizationId equals g.v_OrganizationId
                                         join h in dbContext.organization on c.v_WorkingOrganizationId equals h.v_OrganizationId

                                         where a.i_ApprovedUpdateUserId == systemUserId && a.i_IsDeleted == 0 && b.i_PagoEspecialista == null
                                         select new
                                         {
                                             serviceId = a.v_ServiceId,
                                             protocolId = b.v_ProtocolId,
                                             EmployerId = c.v_EmployerOrganizationId,
                                             EmployerName = f.v_Name,
                                             CustomerId = c.v_CustomerOrganizationId,
                                             CustomerName = g.v_Name,
                                             WorkingId = c.v_WorkingOrganizationId,
                                             WorkingName = h.v_Name,
                                             Protocol = c.v_Name,
                                             Trabajador = d.v_FirstName + " " + d.v_FirstLastName + " " + d.v_SecondLastName,
                                             d_ServiceDate = b.d_ServiceDate
                                         }).ToList();

                servicecomponents = servicecomponents.FindAll(b => b.d_ServiceDate >= fInicio && b.d_ServiceDate <= fFin).ToList();

                foreach (var item in specialistConfigurations)
                {
                    var oPagoEspecialistaOcupacional = new PagoEspecialistaOcupacional();

                    if (item.v_WorkingOrganizationId == "-1" && item.v_EmployerOrganizationId == "-1")
                    {
                        var objServicesComponents = servicecomponents.FindAll(p => p.CustomerId == item.v_CustomerOrganizationId).ToList();
                        if (objServicesComponents.Count != 0)
                        {
                            oPagoEspecialistaOcupacional.Minera = objServicesComponents[0].CustomerName;
                            oPagoEspecialistaOcupacional.Contrata = objServicesComponents[0].EmployerName;
                            oPagoEspecialistaOcupacional.Terceros = objServicesComponents[0].WorkingName;
                            oPagoEspecialistaOcupacional.NroAtenciones = objServicesComponents.Count;
                            oPagoEspecialistaOcupacional.Precio = item.r_Price;
                            oPagoEspecialistaOcupacional.Total = item.r_Price * objServicesComponents.Count;

                            var listServices = new List<ServiciosEspecialistasOcupacional>();
                            foreach (var obj in objServicesComponents)
                            {
                                var oServiciosEspecialistasOcupacional = new ServiciosEspecialistasOcupacional();
                                oServiciosEspecialistasOcupacional.ServiceId = obj.serviceId;
                                oServiciosEspecialistasOcupacional.Trabajador = obj.Trabajador;
                                oServiciosEspecialistasOcupacional.Protocolo = obj.Protocol;
                                listServices.Add(oServiciosEspecialistasOcupacional);
                            }

                            oPagoEspecialistaOcupacional.ServiceIds = string.Join("|", listServices.Select(s => s.ServiceId));

                            oPagoEspecialistaOcupacional.Servicios = listServices;
                        }
                    }
                    else if (item.v_WorkingOrganizationId == "-1")
                    {
                        var objServicesComponents = servicecomponents.FindAll(p => p.CustomerId == item.v_CustomerOrganizationId && p.EmployerId == item.v_EmployerOrganizationId).ToList();
                        if (objServicesComponents.Count != 0)
                        {
                            oPagoEspecialistaOcupacional.Minera = objServicesComponents[0].CustomerName;
                            oPagoEspecialistaOcupacional.Contrata = objServicesComponents[0].EmployerName;
                            oPagoEspecialistaOcupacional.Terceros = objServicesComponents[0].WorkingName;
                            oPagoEspecialistaOcupacional.NroAtenciones = objServicesComponents.Count;
                            oPagoEspecialistaOcupacional.Precio = item.r_Price;
                            oPagoEspecialistaOcupacional.Total = item.r_Price * objServicesComponents.Count;

                            var listServices = new List<ServiciosEspecialistasOcupacional>();
                            foreach (var obj in objServicesComponents)
                            {
                                var oServiciosEspecialistasOcupacional = new ServiciosEspecialistasOcupacional();
                                oServiciosEspecialistasOcupacional.ServiceId = obj.serviceId;
                                oServiciosEspecialistasOcupacional.Trabajador = obj.Trabajador;
                                oServiciosEspecialistasOcupacional.Protocolo = obj.Protocol;
                                listServices.Add(oServiciosEspecialistasOcupacional);
                            }
                            oPagoEspecialistaOcupacional.ServiceIds = string.Join("|", listServices.Select(s => s.ServiceId));
                            oPagoEspecialistaOcupacional.Servicios = listServices;

                        }
                    }
                    else
                    {
                        var objServicesComponents = servicecomponents.FindAll(p => p.CustomerId == item.v_CustomerOrganizationId && p.EmployerId == item.v_EmployerOrganizationId && p.WorkingId == item.v_WorkingOrganizationId).ToList();
                        if (objServicesComponents.Count != 0)
                        {
                            oPagoEspecialistaOcupacional.Minera = objServicesComponents[0].CustomerName;
                            oPagoEspecialistaOcupacional.Contrata = objServicesComponents[0].EmployerName;
                            oPagoEspecialistaOcupacional.Terceros = objServicesComponents[0].WorkingName;
                            oPagoEspecialistaOcupacional.NroAtenciones = objServicesComponents.Count;
                            oPagoEspecialistaOcupacional.Precio = item.r_Price;
                            oPagoEspecialistaOcupacional.Total = item.r_Price * objServicesComponents.Count;

                            var listServices = new List<ServiciosEspecialistasOcupacional>();
                            foreach (var obj in objServicesComponents)
                            {
                                var oServiciosEspecialistasOcupacional = new ServiciosEspecialistasOcupacional();
                                oServiciosEspecialistasOcupacional.ServiceId = obj.serviceId;
                                oServiciosEspecialistasOcupacional.Trabajador = obj.Trabajador;
                                oServiciosEspecialistasOcupacional.Protocolo = obj.Protocol;
                                listServices.Add(oServiciosEspecialistasOcupacional);
                            }
                            oPagoEspecialistaOcupacional.ServiceIds = string.Join("|", listServices.Select(s => s.ServiceId));
                            oPagoEspecialistaOcupacional.Servicios = listServices;
                        }
                    }
                    if (oPagoEspecialistaOcupacional.NroAtenciones != 0)
                        list.Add(oPagoEspecialistaOcupacional);

                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public bool Pay(List<string> serviceIds)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    var dbContext = new SigesoftEntitiesModel();

                    foreach (var serviceId in serviceIds)
                    {
                        var ids = serviceId.Split('|');
                        foreach (var id in ids)
                        {
                            var obj = dbContext.service.FirstOrDefault(p => p.v_ServiceId == id);
                            if (obj != null) obj.i_PagoEspecialista = 1;
                            dbContext.SaveChanges();
                        }
                    }
                    ts.Complete();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
