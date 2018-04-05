using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using Sigesoft.Common;
using System.Linq.Dynamic;


namespace Sigesoft.Server.WebClientAdmin.BLL
{
    public class DxFrecuenteBL
    {

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

        public List<Sigesoft.Node.WinClient.BE.DxFrecuenteList> GetDxFrecuentes(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.dxfrecuente
                            join B in dbContext.dxfrecuentedetalle on A.v_DxFrecuenteId equals B.v_DxFrecuenteId
                            join C in dbContext.diseases on A.v_DiseasesId equals C.v_DiseasesId
                            join D in dbContext.masterrecommendationrestricction on B.v_MasterRecommendationRestricctionId equals D.v_MasterRecommendationRestricctionId
                            where A.i_IsDeleted == 0
                            select new Sigesoft.Node.WinClient.BE.DxFrecuenteList
                            {
                                v_DxFrecuenteId = A.v_DxFrecuenteId,
                                v_DiseasesId = A.v_DiseasesId,
                                v_DiseasesName = C.v_Name,
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

                List<Sigesoft.Node.WinClient.BE.DxFrecuenteList> objData = query.ToList();

                List<Sigesoft.Node.WinClient.BE.DxFrecuenteList> QueryFinal = new List<Sigesoft.Node.WinClient.BE.DxFrecuenteList>();


                //Transformar Data
                //Agrupar Data para DXS
                var DxFrecunetes = (objData.GroupBy(g => new { g.v_DiseasesId })
                               .Select(s => s.First()).ToList()).FindAll(p => p.v_DiseasesName != null);

                Sigesoft.Node.WinClient.BE.DxFrecuenteList oDxFrecuente;
                List<Sigesoft.Node.WinClient.BE.DxFrecuenteDetalleList> ListaDxFrecuenteDetalleList;

                foreach (var Dx in DxFrecunetes)
                {
                    oDxFrecuente = new Sigesoft.Node.WinClient.BE.DxFrecuenteList();
                    oDxFrecuente.v_DxFrecuenteId = Dx.v_DxFrecuenteId;
                    oDxFrecuente.v_DiseasesId = Dx.v_DiseasesId;
                    oDxFrecuente.v_DiseasesName = Dx.v_DiseasesName;
                    oDxFrecuente.v_CIE10Id = Dx.v_CIE10Id;

                    var ListaRecomendacionesRestricciones = objData.FindAll(p => p.v_DxFrecuenteId == Dx.v_DxFrecuenteId);
                    ListaDxFrecuenteDetalleList = new List<Sigesoft.Node.WinClient.BE.DxFrecuenteDetalleList>();
                    Sigesoft.Node.WinClient.BE.DxFrecuenteDetalleList oDxFrecuenteDetalleList;
                    foreach (var item in ListaRecomendacionesRestricciones)
                    {
                        oDxFrecuenteDetalleList = new Sigesoft.Node.WinClient.BE.DxFrecuenteDetalleList();
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

      
    }
}
