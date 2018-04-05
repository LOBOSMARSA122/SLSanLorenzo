using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration.Contasol;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.Contasol.Integration
{
    public class RecetaBl
    {
        public RecetaBl()
        {

        }

        public List<DiagnosticRepositoryList> GetHierarchycalData(ref OperationResult pobjOperationResult,
            List<DiagnosticRepositoryList> dataToIterate)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var diagnosticIds = dataToIterate.Select(p => p.v_DiagnosticRepositoryId).Distinct();
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var recetas = (from n in dbContext.receta.Where(p => diagnosticIds.Contains(p.v_DiagnosticRepositoryId)).ToList()
                                   join m in medicamentos on n.v_IdProductoDetalle equals m.IdProductoDetalle into mJoin
                                   from m in mJoin.DefaultIfEmpty()
                                   select new recetaDto
                                   {
                                       v_IdProductoDetalle = n.v_IdProductoDetalle,
                                       NombreMedicamento = m.NombreCompleto,
                                       d_Cantidad = n.d_Cantidad,
                                       i_IdReceta = n.i_IdReceta,
                                       t_FechaFin = n.t_FechaFin,
                                       v_DiagnosticRepositoryId = n.v_DiagnosticRepositoryId,
                                       v_Duracion = n.v_Duracion,
                                       v_Posologia = n.v_Posologia
                                   }).ToList();

                    if (recetas.Any())
                    {
                        var agrupado = recetas.GroupBy(g => g.v_DiagnosticRepositoryId);
                        foreach (var grupo in agrupado)
                        {
                            var parent = dataToIterate.FirstOrDefault(p => p.v_DiagnosticRepositoryId.Equals(grupo.Key));
                            if (parent != null)
                            {
                                parent.RecipeDetail.AddRange(grupo);
                            }
                        }
                    }

                    pobjOperationResult.Success = 1;
                    return dataToIterate;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.AddUpdateRecipe()";
                return null;
            }
        }

        public void AddUpdateRecipe(ref OperationResult pobjOperationResult, recetaDto pobjDto)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var entidad = dbContext.receta.FirstOrDefault(p => p.i_IdReceta == pobjDto.i_IdReceta);
                    if (entidad == null)
                    {
                        dbContext.receta.AddObject(pobjDto.ToEntity());
                    }
                    else
                    {
                        entidad = pobjDto.ToEntity();
                        dbContext.receta.ApplyCurrentValues(entidad);
                    }
                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.AddUpdateRecipe()";
            }
        }

        public void DeleteRecipe(ref OperationResult pobjOperationResult, int recipeId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var entidad = dbContext.receta.FirstOrDefault(p => p.i_IdReceta == recipeId);
                    if (entidad == null) throw new Exception("El objeto ya no existe!");
                    dbContext.receta.DeleteObject(entidad);
                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.DeleteRecipe()";
            }
        }

        public recetaDto GetRecipeById(ref OperationResult pobjOperationResult, int recipeId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();

                    var entidad = (from r in dbContext.receta.Where(p => p.i_IdReceta == recipeId).ToList()
                                   join m in medicamentos on r.v_IdProductoDetalle equals m.IdProductoDetalle into mJoin
                                   from m in mJoin.DefaultIfEmpty()
                                   where r.i_IdReceta == recipeId
                                   select new recetaDto
                                   {
                                       v_IdProductoDetalle = r.v_IdProductoDetalle,
                                       i_IdReceta = r.i_IdReceta,
                                       NombreMedicamento = m.NombreCompleto,
                                       d_Cantidad = r.d_Cantidad,
                                       t_FechaFin = r.t_FechaFin,
                                       v_DiagnosticRepositoryId = r.v_DiagnosticRepositoryId,
                                       v_Duracion = r.v_Duracion,
                                       v_Posologia = r.v_Posologia,
                                       v_IdUnidadProductiva = r.v_IdUnidadProductiva
                                   }).FirstOrDefault();

                    if (entidad == null) throw new Exception("El objeto ya no existe!");
                    pobjOperationResult.Success = 1;
                    return entidad;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaBl.DeleteRecipe()";
                return null;
            }
        }

        public List<recetadespachoDto> GetRecetaToReport(ref OperationResult pobjOperationResult, string serviceId)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    pobjOperationResult.Success = 1;
                    var medicamentos = MedicamentoDao.ObtenerContasolMedicamentos();
                    var consulta = (from r in dbContext.receta
                                    join d in dbContext.diagnosticrepository on r.v_DiagnosticRepositoryId equals d.v_DiagnosticRepositoryId into dJoin
                                    from d in dJoin.DefaultIfEmpty()
                                    join s in dbContext.service on d.v_ServiceId equals s.v_ServiceId into sJoin
                                    from s in sJoin.DefaultIfEmpty()
                                    join C in dbContext.organization on new { id = "N009-OO000000052" } equals new { id = C.v_OrganizationId } into C_join
                                    from C in C_join.DefaultIfEmpty()
                                    join p in dbContext.person on s.v_PersonId equals p.v_PersonId into pJoin
                                    from p in pJoin.DefaultIfEmpty()
                                    join su in dbContext.systemuser on new { idDoctor = s.i_UpdateUserMedicalAnalystId ?? (s.i_InsertUserMedicalAnalystId ?? 1) }
                                                            equals new { idDoctor = su.i_SystemUserId } into suJoin
                                    from su in suJoin.DefaultIfEmpty()
                                    //Aca el join no esta claro, falta explicacion.<----------------------------------------------------
                                    //join pp in dbContext.person on su.v_PersonId equals pp.v_PersonId into ppJoin
                                    //from pp in ppJoin.DefaultIfEmpty()
                                    //join pr in dbContext.professional on pp.v_PersonId equals pr.v_PersonId into prJoin
                                    //from pr in prJoin.DefaultIfEmpty()
                                    where s.v_ServiceId.Equals(serviceId)
                                    select new recetadespachoDto
                                    {
                                        RecetaId = r.i_IdReceta,
                                        CantidadRecetada = r.d_Cantidad ?? 0,
                                        //Medicamento = m.NombreCompleto,
                                        //Presentacion = m.Presentacion,
                                        //Ubicacion = m.Ubicacion,
                                        NombrePaciente = p.v_FirstLastName + " " + p.v_SecondLastName + " " + p.v_FirstName,
                                        FechaFin = r.t_FechaFin ?? DateTime.Now,
                                        Duracion = r.v_Duracion,
                                        Dosis = r.v_Posologia,
                                        //NombreMedico = pp.v_FirstLastName + " " + pp.v_SecondLastName + " " + pp.v_FirstName,
                                        //RubricaMedico = pr.b_SignatureImage,
                                        //MedicoNroCmp = pr.v_ProfessionalCode,
                                        NombreClinica = C.v_Name,
                                        DireccionClinica = C.v_Address,
                                        LogoClinica = C.b_Image,
                                        Despacho = (r.i_Lleva ?? 0) == 1,
                                        MedicinaId = r.v_IdProductoDetalle
                                    }).ToList();

                    foreach (var item in consulta)
                    {
                        var prod = medicamentos.FirstOrDefault(p => p.IdProductoDetalle.Equals(item.MedicinaId));
                        if (prod == null) continue;
                        item.Medicamento = prod.NombreCompleto;
                        item.Presentacion = prod.Presentacion;
                        item.Ubicacion = prod.Ubicacion;
                    }

                    return consulta;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.GetDespacho()";
                return null;
            }
        }

        public void UpdateDespacho(ref OperationResult pobjOperationResult, List<recetadespachoDto> data)
        {
            try
            {
                using (var dbContext = new SigesoftEntitiesModel())
                {
                    foreach (var item in data)
                    {
                        var e = dbContext.receta.FirstOrDefault(p => p.i_IdReceta == item.RecetaId);
                        if (e != null)
                        {
                            e.i_Lleva = item.Despacho ? 1 : 0;
                            dbContext.receta.ApplyCurrentValues(e);
                        }
                    }

                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "RecetaDespachoBl.UpdateDespacho()";
            }
        }
    }
}
