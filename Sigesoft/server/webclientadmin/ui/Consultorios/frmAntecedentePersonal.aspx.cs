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
    public partial class frmAntecedentePersonal : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        HistoryBL _objHistoryBL = new HistoryBL();
        personmedicalhistoryDto _objpersonmedicalhistoryDto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                btnOtroDx.OnClientClick = WindowAddDX.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDX.GetShowReference("CIE10.aspx");
               
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

                //Llenar combo ItemParameter Tree
                ddlDx.DataTextField = "Description";
                ddlDx.DataValueField = "Description2";
                ddlDx.DataSimulateTreeLevelField = "Level";
                ddlDx.DataEnableSelectField = "EnabledSelect";
                List<DataForTreeView> t = _objDataHierarchyBL.GetSystemParameterForComboTree(ref objOperationResult, 147);
                ddlDx.DataSource = t;
                ddlDx.DataBind();
                this.ddlDx.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));

                Utils.LoadDropDownList(ddlTipoDx, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 139), DropDownListAction.Select);
                LoadData();
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();

            string PersonMedicalHistoryId = "";
            string v_DiseasesName = "";

            if (Request.QueryString["v_PersonMedicalHistoryId"] != null)
                PersonMedicalHistoryId = Request.QueryString["v_PersonMedicalHistoryId"].ToString();

            if (Request.QueryString["v_DiseasesName"] != null)
                v_DiseasesName = Request.QueryString["v_DiseasesName"].ToString();

            if (Mode == "New")
            {
                ddlTipoDx.SelectedValue = ((int)TipoDx.Enfermedad_Comun).ToString();
                ddlDx.SelectedValue = "-1";
                dtcFechaInicio.SelectedDate = DateTime.Now;
                txtDetalle.Text = "";
                txtDiasDescanso.Text = "";
                chkSoloAnio.Checked = true;
            }
            else if (Mode == "Edit")
            {
                _objpersonmedicalhistoryDto = new personmedicalhistoryDto();

                _objpersonmedicalhistoryDto = _objHistoryBL.GetpersonmedicalhistoryDto(ref objOperationResult, PersonMedicalHistoryId);
                Session["objEntity"] = _objpersonmedicalhistoryDto;

                ddlTipoDx.SelectedValue = _objpersonmedicalhistoryDto.i_TypeDiagnosticId.Value.ToString();
                ddlDx.SelectedValue = _objpersonmedicalhistoryDto.v_DiseasesId;
                if (ddlDx.SelectedValue == "-1")
                {
                    ddlDx.SelectedValue = ".OTROS";
                    txtOtroDx.Text = v_DiseasesName;
                }
                dtcFechaInicio.SelectedDate = _objpersonmedicalhistoryDto.d_StartDate;
                txtDetalle.Text = _objpersonmedicalhistoryDto.v_DiagnosticDetail;
                txtDiasDescanso.Text = _objpersonmedicalhistoryDto.v_TreatmentSite;
                chkSoloAnio.Checked = _objpersonmedicalhistoryDto.i_SoloAnio == 1 ? true : false;
            }

        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();
            string PersonId = "";
            if (Request.QueryString["v_PersonId"] != null)
                PersonId = Request.QueryString["v_PersonId"].ToString();
               

                if (Mode == "New")
                {
                    personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();

                    personmedicalhistoryDtoDto.i_TypeDiagnosticId = int.Parse(ddlTipoDx.SelectedValue.ToString());
                    personmedicalhistoryDtoDto.v_PersonId = Session["PersonId"].ToString();
                    if (ddlDx.SelectedValue == ".OTROS")
                    {
                        personmedicalhistoryDtoDto.v_DiseasesId = Session["OtroDxId"].ToString();
                    }
                    else
                    {
                        personmedicalhistoryDtoDto.v_DiseasesId = ddlDx.SelectedValue.ToString();
                    }
                    personmedicalhistoryDtoDto.d_StartDate = dtcFechaInicio.SelectedDate;
                    personmedicalhistoryDtoDto.v_DiagnosticDetail =txtDetalle.Text;
                    personmedicalhistoryDtoDto.v_TreatmentSite = txtDiasDescanso.Text;
                    personmedicalhistoryDtoDto.i_AnswerId =1;
                    personmedicalhistoryDtoDto.i_SoloAnio = chkSoloAnio.Checked == true ? 1 : 0;
                    _objHistoryBL.AddMedicoPersonal(ref objOperationResult, personmedicalhistoryDtoDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                }
                else if (Mode == "Edit")
                {
                    personmedicalhistoryDto personmedicalhistoryDtoDto = (personmedicalhistoryDto)Session["objEntity"];

                    personmedicalhistoryDtoDto.i_TypeDiagnosticId = int.Parse(ddlTipoDx.SelectedValue.ToString());

                    if (ddlDx.SelectedValue == ".OTROS")
                    {
                        personmedicalhistoryDtoDto.v_DiseasesId = Session["OtroDxId"].ToString();
                    }
                    else
                    {
                        personmedicalhistoryDtoDto.v_DiseasesId = ddlDx.SelectedValue.ToString();
                    }
                    
                    personmedicalhistoryDtoDto.d_StartDate = dtcFechaInicio.SelectedDate;
                    personmedicalhistoryDtoDto.v_DiagnosticDetail = txtDetalle.Text;
                    personmedicalhistoryDtoDto.v_TreatmentSite = txtDiasDescanso.Text;
                    personmedicalhistoryDtoDto.i_AnswerId = 1;
                    personmedicalhistoryDtoDto.i_SoloAnio = chkSoloAnio.Checked == true ? 1 : 0;
                    _objHistoryBL.UpdateMedicoPersonal(ref objOperationResult, personmedicalhistoryDtoDto, ((ClientSession)Session["objClientSession"]).GetAsList());

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

        protected void ddlDx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDx.SelectedValue == ".OTROS")
            {
                btnOtroDx.Enabled = true;
            }
            else
            {
                btnOtroDx.Enabled = false;
            }
        }

        protected void WindowAddDX_Close(object sender, WindowCloseEventArgs e)
        {
            txtOtroDx.Text = Session["OtroDx"].ToString();
        }

        protected void btnOtroDx_Click(object sender, EventArgs e)
        {

        }
    }
}