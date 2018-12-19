using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using  Sigesoft.Common;

namespace Sigesoft.Node.WinClient.BLL
{
   public class SpecialistConfigurationBl
    {
        private HospitalizacionBL oHospitalizacionBL = new HospitalizacionBL();

        public bool SaveChange(List<SpecialistConfiguration> list,List<string> clientSession)
        {
            try
            {

                using (var ts = new TransactionScope())
                {
                    OperationResult objOperationResult = new OperationResult();
                    var dbContext = new SigesoftEntitiesModel();

                    var queryable = dbContext.medico.Where(p => p.i_MasterServiceId == (int)MasterService.Eso && p.i_MasterServiceTypeId == (int)ServiceType.Empresarial).ToList();
                    foreach (var rem in queryable)
                        dbContext.medico.DeleteObject(rem);

                   //var x = queryable.RemoveAll(p => p.i_MasterServiceId == (int)MasterService.Eso && p.i_MasterServiceTypeId == (int)ServiceType.Empresarial);

                    foreach (var item in list)
                    {
                        var omedicoDto = new medicoDto();

                        omedicoDto.i_MasterServiceId = (int)MasterService.Eso;
                        omedicoDto.i_SystemUserId =  int.Parse(item.i_SystemUserId);
                        omedicoDto.i_MasterServiceTypeId = (int)ServiceType.Empresarial;
                        omedicoDto.i_CategoryId = int.Parse(item.i_CategoryId);
                        omedicoDto.r_Price = decimal.Parse(item.Price.ToString());

                        omedicoDto.v_CustomerOrganizationId = item.v_CustomerOrganizationId;
                        omedicoDto.v_EmployerOrganizationId = item.v_EmployerOrganizationId;
                        omedicoDto.v_WorkingOrganizationId = item.v_WorkingOrganizationId;

                        oHospitalizacionBL.AddMedico(ref objOperationResult, omedicoDto, clientSession);
                    }
                    dbContext.SaveChanges();
                    ts.Complete();

                    return true;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BindingList<SpecialistConfiguration> LoadGrid()
        {
            try
            {
                var oList = new BindingList<SpecialistConfiguration>();
                
                var dbContext = new SigesoftEntitiesModel();

                var queryable = dbContext.medico.Where(p => p.i_MasterServiceId == (int)MasterService.Eso && p.i_MasterServiceTypeId == (int)ServiceType.Empresarial).ToList();

                foreach (var item in queryable)
                {
                    var oSpecialistConfiguration = new SpecialistConfiguration();
                    oSpecialistConfiguration.v_MedicoId = item.v_MedicoId;
                    oSpecialistConfiguration.Price = float.Parse(item.r_Price.ToString());
                    if (item.i_SystemUserId != null)
                        oSpecialistConfiguration.i_SystemUserId = item.i_SystemUserId.Value.ToString();

                    if (item.i_CategoryId != null)
                        oSpecialistConfiguration.i_CategoryId = item.i_CategoryId.Value.ToString();

                    oSpecialistConfiguration.v_CustomerOrganizationId = item.v_CustomerOrganizationId;
                    oSpecialistConfiguration.v_EmployerOrganizationId = item.v_EmployerOrganizationId;
                    oSpecialistConfiguration.v_WorkingOrganizationId = item.v_WorkingOrganizationId;

                    oList.Add(oSpecialistConfiguration);
                }
                return oList;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public bool DeleteRow(string medicoId)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();
                using (var ts = new TransactionScope())
                {
                    var queryable = dbContext.medico.FirstOrDefault(p => p.v_MedicoId == medicoId);
                    dbContext.medico.DeleteObject(queryable);
                    dbContext.SaveChanges();
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
