using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;


namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmHabitosNocivos : System.Web.UI.Page
    {
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        HistoryBL _objHistoryBL = new HistoryBL();
        noxioushabitsDto _objnoxioushabitsDto;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();

                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

                //Llenar combo ItemParameter Tree
                ddlTipoHabito.DataTextField = "Description";
                ddlTipoHabito.DataValueField = "Id";
                ddlTipoHabito.DataSimulateTreeLevelField = "Level";
                ddlTipoHabito.DataEnableSelectField = "EnabledSelect";
                List<DataForTreeView> t = _objDataHierarchyBL.GetSystemParameterForComboTree(ref objOperationResult, 148);
                ddlTipoHabito.DataSource = t;
                ddlTipoHabito.DataBind();
                this.ddlTipoHabito.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));
                Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id", null, DropDownListAction.Select);
                LoadData();
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();

            string NoxiousHabitsId = "";

            if (Request.QueryString["v_NoxiousHabitsId"] != null)
                NoxiousHabitsId = Request.QueryString["v_NoxiousHabitsId"].ToString();

            if (Mode == "New")
            {
                ddlFrecuencia.SelectedValue = "-1";
                ddlTipoHabito.SelectedValue = "-1";
                txtComentario.Text = "";
            }
            else if (Mode == "Edit")
            {
                _objnoxioushabitsDto = new noxioushabitsDto();
                _objnoxioushabitsDto = _objHistoryBL.GetnoxioushabitsDto(ref objOperationResult, NoxiousHabitsId);
                Session["objEntity"] = _objnoxioushabitsDto;

                string ValorFrecuencia = "-1";

                ddlTipoHabito.SelectedValue = _objnoxioushabitsDto.i_TypeHabitsId.ToString();
                if (ddlTipoHabito.SelectedText.ToUpper() == "CONSUMO DE ALCOHOL" || ddlTipoHabito.SelectedText.ToUpper() == "CONSUMO DE DROGAS")
                {
                    var o =  BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 205, null);
                    Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id",o, DropDownListAction.Select);

                    ValorFrecuencia = o.Find(p => p.Value1.ToUpper() == _objnoxioushabitsDto.v_Frequency).Id;
                }
                else if (ddlTipoHabito.SelectedText.ToUpper() == "TABAQUISMO")
                {
                    var o = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 206, null);
                    Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id",o, DropDownListAction.Select);
                    ValorFrecuencia = o.Find(p => p.Value1.ToUpper() == _objnoxioushabitsDto.v_Frequency).Id;
                }
                else if (ddlTipoHabito.SelectedText.ToUpper() == "ACTIVIDAD FÍSICA")
                {
                    var o = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 223, null);
                    Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id", o, DropDownListAction.Select);
                    ValorFrecuencia = o.Find(p => p.Value1.ToUpper() == _objnoxioushabitsDto.v_Frequency).Id;
                }

                ddlFrecuencia.SelectedValue = ValorFrecuencia;
                txtComentario.Text = _objnoxioushabitsDto.v_Comment;

            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceBL oServiceBL = new ServiceBL();
            List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendaciones = new List<Node.WinClient.BE.RecomendationList>();
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> _ListadiagnosticRepository = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

            Sigesoft.Node.WinClient.BE.RecomendationList Recomendacion = null;
            Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList diagnosticRepository = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
              
            string Mode = Request.QueryString["Mode"].ToString();

            if (Mode == "New")
            {
                //Verificar si el hábito ya existe en la grilla
               var Result =  _objHistoryBL.ListarGrillaNocivos(Session["PersonId"].ToString()).Find(p => p.i_TypeHabitsId == int.Parse(ddlTipoHabito.SelectedValue.ToString()));

               if (Result != null)
               {
                    Alert.ShowInTop("Este hábito nocivo ya se encuentra registrado, revíselo y edítelo.");
                    return;
               }
                noxioushabitsDto onoxioushabitsDto = new noxioushabitsDto();
                onoxioushabitsDto.i_TypeHabitsId = int.Parse(ddlTipoHabito.SelectedValue.ToString());
                onoxioushabitsDto.v_Frequency = ddlFrecuencia.SelectedText.ToString();
                onoxioushabitsDto.v_PersonId = Session["PersonId"].ToString();
                onoxioushabitsDto.v_Comment = txtComentario.Text;

                _objHistoryBL.AddNocivo(ref objOperationResult, onoxioushabitsDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (ddlFrecuencia.SelectedText.ToString() =="FUMADOR")
                {
                      diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                diagnosticRepository.v_DiseasesId = "N009-DD000000519";
                diagnosticRepository.i_AutoManualId = 1;
                diagnosticRepository.i_PreQualificationId = 1;
                diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;

                diagnosticRepository.v_ServiceId = Session["ServiceId"].ToString();
                diagnosticRepository.v_ComponentId = "N002-ME000000022";  //_componentId;
                diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                var ListaReco = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente("N002-HG000000229", 1);

                foreach (var item in ListaReco)
                {
                    Recomendacion = new Node.WinClient.BE.RecomendationList();

                    Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                    Recomendacion.v_ComponentId = "N002-ME000000022";
                    Recomendacion.v_MasterRecommendationId = item.v_MasterRecommendationRestricctionId;
                    Recomendacion.i_RecordType = (int)RecordType.Temporal;
                    Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                    Recomendaciones.Add(Recomendacion);
                }

                diagnosticRepository.Recomendations = Recomendaciones;
                _ListadiagnosticRepository.Add(diagnosticRepository);
                oServiceBL.AddDiagnosticRepository(ref objOperationResult, _ListadiagnosticRepository, null, ((ClientSession)Session["objClientSession"]).GetAsList(), true);
                    
                }
            }
            else if (Mode == "Edit")
            {
                noxioushabitsDto onoxioushabitsDto = (noxioushabitsDto)Session["objEntity"];

                onoxioushabitsDto.i_TypeHabitsId = int.Parse(ddlTipoHabito.SelectedValue.ToString());
                onoxioushabitsDto.v_Frequency = ddlFrecuencia.SelectedText.ToString();
                onoxioushabitsDto.v_Comment = txtComentario.Text;

                _objHistoryBL.UpdateNocivo(ref objOperationResult, onoxioushabitsDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (ddlFrecuencia.SelectedText.ToString() == "FUMADOR")
                {
                    diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    diagnosticRepository.v_DiseasesId = "N009-DD000000519";
                    diagnosticRepository.i_AutoManualId = 1;
                    diagnosticRepository.i_PreQualificationId = 1;
                    diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                    diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;

                    diagnosticRepository.v_ServiceId = Session["ServiceId"].ToString();
                    diagnosticRepository.v_ComponentId = "N002-ME000000022";  //_componentId;
                    diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                    diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                    var ListaReco = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente("N002-HG000000229", 1);

                    foreach (var item in ListaReco)
                    {
                        Recomendacion = new Node.WinClient.BE.RecomendationList();

                        Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                        Recomendacion.v_ComponentId = "N002-ME000000022";
                        Recomendacion.v_MasterRecommendationId = item.v_MasterRecommendationRestricctionId;
                        Recomendacion.i_RecordType = (int)RecordType.Temporal;
                        Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Recomendaciones.Add(Recomendacion);
                    }

                    diagnosticRepository.Recomendations = Recomendaciones;
                    _ListadiagnosticRepository.Add(diagnosticRepository);
                    oServiceBL.AddDiagnosticRepository(ref objOperationResult, _ListadiagnosticRepository, null, ((ClientSession)Session["objClientSession"]).GetAsList(), true);
                }
            }
            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                // Cerrar página actual y hacer postback en el padre para actualizar
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
        }

        protected void ddlTipoHabito_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlTipoHabito.SelectedText.ToUpper() == "CONSUMO DE ALCOHOL" || ddlTipoHabito.SelectedText.ToUpper() == "CONSUMO DE DROGAS")
            {
                Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 205, null), DropDownListAction.Select);
            }
            else if (ddlTipoHabito.SelectedText.ToUpper() == "TABAQUISMO")
            {
                Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 213, null), DropDownListAction.Select);
            }
            else if (ddlTipoHabito.SelectedText.ToUpper() == "ACTIVIDAD FÍSICA")
            {
                Utils.LoadDropDownList(ddlFrecuencia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 223, null), DropDownListAction.Select);
            }
        }
    }
}