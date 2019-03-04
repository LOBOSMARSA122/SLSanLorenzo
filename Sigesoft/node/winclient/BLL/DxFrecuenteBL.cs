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
    public class DxFrecuenteBL
    {
        #region DxFrecuente

        public string AddDxFrecuente(ref OperationResult pobjOperationResult, dxfrecuenteDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                dxfrecuente objEntity = dxfrecuenteAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 301), "HG"); ;
                objEntity.v_DxFrecuenteId = NewId;

                dbContext.AddTodxfrecuente(objEntity);
                dbContext.SaveChanges();
           
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

        public List<DxFrecuenteList> GetDxFrecuentes(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.dxfrecuente
                            join B in dbContext.dxfrecuentedetalle on A.v_DxFrecuenteId equals B.v_DxFrecuenteId
                            join C in dbContext.diseases on A.v_DiseasesId equals C.v_DiseasesId
                            join D in dbContext.masterrecommendationrestricction on B.v_MasterRecommendationRestricctionId equals D.v_MasterRecommendationRestricctionId
                             where A.i_IsDeleted == 0
                            select new DxFrecuenteList
                          {
                              v_DxFrecuenteId = A.v_DxFrecuenteId,
                              v_DiseasesId = A.v_DiseasesId,
                              v_DiseasesName =  C.v_Name,
                              v_CIE10Id = A.v_CIE10Id,
                              v_DxFrecuenteDetalleId = B.v_DxFrecuenteDetalleId,
                              v_MasterRecommendationRestricctionId = B.v_MasterRecommendationRestricctionId,
                              v_MasterRecommendationRestricctionName = D.v_Name,
                              i_Tipo = D.i_TypifyingId.Value
                          };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }

                List<DxFrecuenteList> objData = query.ToList();

                List<DxFrecuenteList> QueryFinal = new List<DxFrecuenteList>();


                //Transformar Data
                //Agrupar Data para DXS
                var DxFrecunetes = (objData.GroupBy(g => new { g.v_DiseasesId })
                               .Select(s => s.First()).ToList()).FindAll(p => p.v_DiseasesName != null);

                DxFrecuenteList oDxFrecuente;
                List<DxFrecuenteDetalleList> ListaDxFrecuenteDetalleList;

                foreach (var Dx in DxFrecunetes)
                {
                    oDxFrecuente = new DxFrecuenteList();
                    oDxFrecuente.v_DxFrecuenteId = Dx.v_DxFrecuenteId;
                    oDxFrecuente.v_DiseasesId = Dx.v_DiseasesId;
                    oDxFrecuente.v_DiseasesName = Dx.v_DiseasesName;
                    oDxFrecuente.v_CIE10Id = Dx.v_CIE10Id;

                    var ListaRecomendacionesRestricciones = objData.FindAll(p => p.v_DxFrecuenteId == Dx.v_DxFrecuenteId);
                    ListaDxFrecuenteDetalleList = new List<DxFrecuenteDetalleList>();
                    DxFrecuenteDetalleList oDxFrecuenteDetalleList;
                    foreach (var item in ListaRecomendacionesRestricciones)
                    {
                        oDxFrecuenteDetalleList = new DxFrecuenteDetalleList();
                        oDxFrecuenteDetalleList.v_DxFrecuenteDetalleId = item.v_DxFrecuenteDetalleId;
                        oDxFrecuenteDetalleList.v_DxFrecuenteId = item.v_DxFrecuenteId;
                        oDxFrecuenteDetalleList.v_MasterRecommendationRestricctionId = item.v_MasterRecommendationRestricctionId;
                        oDxFrecuenteDetalleList.i_Tipo = item.i_Tipo;
                        if (item.i_Tipo == 1)
                        {
                            oDxFrecuenteDetalleList.v_RecomendacionName = item.v_MasterRecommendationRestricctionName;
                        }
                        else
                        {
                            oDxFrecuenteDetalleList.v_RestriccionName = item.v_MasterRecommendationRestricctionName;
                        }

                        ListaDxFrecuenteDetalleList.Add(oDxFrecuenteDetalleList);
                    }
                    oDxFrecuente.DxFrecuenteDetalle = ListaDxFrecuenteDetalleList;

                    QueryFinal.Add(oDxFrecuente);
                }


                pobjOperationResult.Success = 1;
                return QueryFinal;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
        #endregion

        #region DxFrecuenteDetalle
        public void AddDxFrecuenteDetalle(ref OperationResult pobjOperationResult, dxfrecuentedetalleDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                dxfrecuentedetalle objEntity = dxfrecuentedetalleAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 302), "HZ"); 
                objEntity.v_DxFrecuenteDetalleId = NewId;

                dbContext.AddTodxfrecuentedetalle(objEntity);
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

       
        #endregion

        public class DiagnosticCustom
        {
            public string v_DiagnosticRepositoryId { get; set; }
            public string v_ServiceId { get; set; }
            public string v_DiseaseId { get; set; }
            public string v_DiseaseName { get; set; }
        }

        public List<DiagnosticRepositoryJerarquizada> getDataService(string serviceId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var diagnostics = (from ccc in dbContext.diagnosticrepository
                join ddd in dbContext.diseases on ccc.v_DiseasesId equals ddd.v_DiseasesId
                where ccc.v_ServiceId == serviceId &&
                      ccc.i_IsDeleted == 0 && ccc.i_FinalQualificationId != 4
                select new DiagnosticCustom
                {
                    v_DiagnosticRepositoryId = ccc.v_DiagnosticRepositoryId,
                    v_DiseaseName = ddd.v_Name,
                    v_ServiceId = ccc.v_ServiceId,
                    v_DiseaseId = ddd.v_DiseasesId
                }).ToList();

            diagnostics = diagnostics.GroupBy(g => g.v_DiseaseId).Select(s => s.First()).ToList();
            var list = new List<DiagnosticRepositoryJerarquizada>();

            foreach (var dx in diagnostics)
            {
                var oDiagnosticRepositoryJerarquizada = new DiagnosticRepositoryJerarquizada();
                oDiagnosticRepositoryJerarquizada.v_DiseasesName = dx.v_DiseaseName;
                oDiagnosticRepositoryJerarquizada.v_RecomendationsName =
                    new ServiceBL().GetRecommendationByServiceIdAndDiagnostic(dx.v_ServiceId,
                        dx.v_DiagnosticRepositoryId);

                oDiagnosticRepositoryJerarquizada.v_RestricctionName =
                    new ServiceBL().GetResstrictionByServiceIdAndDiagnostic(dx.v_ServiceId,
                        dx.v_DiagnosticRepositoryId);

                list.Add(oDiagnosticRepositoryJerarquizada);
            }

            return list;
        }
    }
}
