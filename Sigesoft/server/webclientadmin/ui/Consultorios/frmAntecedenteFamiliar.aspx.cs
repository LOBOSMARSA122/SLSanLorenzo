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
    public partial class frmAntecedenteFamiliar : System.Web.UI.Page
    {
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        familymedicalantecedentsDto _objfamilymedicalantecedentsDto;
        HistoryBL _objHistoryBL = new HistoryBL();
        SystemParameterBL _objBL = new SystemParameterBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                btnOtroDx.OnClientClick = WindowAddDX.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDX.GetShowReference("CIE10.aspx");
               
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

                //Llenar combo ItemParameter Tree
                ddlDx.DataTextField = "Description";
                ddlDx.DataValueField = "Id";
                ddlDx.DataSimulateTreeLevelField = "Level";
                ddlDx.DataEnableSelectField = "EnabledSelect";
                List<DataForTreeView> t = _objDataHierarchyBL.GetSystemParameterForComboTree_(ref objOperationResult, 149);
                ddlDx.DataSource = t;
                ddlDx.DataBind();
                this.ddlDx.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));

                LoadData();
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();

            string FamilyMedicalAntecedentsId = "";

            if (Request.QueryString["v_FamilyMedicalAntecedentsId"] != null)
                FamilyMedicalAntecedentsId = Request.QueryString["v_FamilyMedicalAntecedentsId"].ToString();

            if (Mode == "New")
            {
                ddlDx.SelectedValue = "-1";
                txtComentario.Text = "";

            }
            else if (Mode == "Edit")
            {
                _objfamilymedicalantecedentsDto = new familymedicalantecedentsDto();

                _objfamilymedicalantecedentsDto = _objHistoryBL.GetfamilymedicalantecedentsDto(ref objOperationResult, FamilyMedicalAntecedentsId);
                Session["objEntity"] = _objfamilymedicalantecedentsDto;

                ddlDx.SelectedValue = _objfamilymedicalantecedentsDto.i_TypeFamilyId.ToString();
                txtComentario.Text = _objfamilymedicalantecedentsDto.v_Comment;
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
                familymedicalantecedentsDto personmedicalhistoryDtoDto = new familymedicalantecedentsDto();
                var x = ddlDx.SelectedText.ToString().Split('|');
                //Obtener Disease 
                systemparameterDto objEntity = _objBL.GetSystemParameter(ref objOperationResult, 149, int.Parse(x[1].ToString()));



                personmedicalhistoryDtoDto.v_PersonId = Session["PersonId"].ToString();

                personmedicalhistoryDtoDto.i_TypeFamilyId = int.Parse(x[1].ToString());

                if (ddlDx.SelectedValue == "80" || ddlDx.SelectedValue == "81" || ddlDx.SelectedValue == "82" || ddlDx.SelectedValue == "83" || ddlDx.SelectedValue == "84")
                {
                    personmedicalhistoryDtoDto.v_DiseasesId = Session["OtroDxId"].ToString();
                }
                else
                {
                    personmedicalhistoryDtoDto.v_DiseasesId = objEntity.v_Value1;
                }
                
                personmedicalhistoryDtoDto.v_Comment = txtComentario.Text;

                _objHistoryBL.AddFamiliar(ref objOperationResult, personmedicalhistoryDtoDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            else if (Mode == "Edit")
            {
                familymedicalantecedentsDto personmedicalhistoryDtoDto = (familymedicalantecedentsDto)Session["objEntity"];

                var x = ddlDx.SelectedText.ToString().Split('|');

                //Obtener Disease 
                systemparameterDto objEntity = _objBL.GetSystemParameter(ref objOperationResult, 149, int.Parse(x[1].ToString()));


                //personmedicalhistoryDtoDto.v_DiseasesId = objEntity.v_Value1;
                if (ddlDx.SelectedValue == "80" || ddlDx.SelectedValue == "81" || ddlDx.SelectedValue == "82" || ddlDx.SelectedValue == "83" || ddlDx.SelectedValue == "84")
                {
                    personmedicalhistoryDtoDto.v_DiseasesId = Session["OtroDxId"].ToString();
                }
                else
                {
                    personmedicalhistoryDtoDto.v_DiseasesId = objEntity.v_Value1;
                }

                personmedicalhistoryDtoDto.v_Comment = txtComentario.Text;
                personmedicalhistoryDtoDto.i_TypeFamilyId = int.Parse(x[1].ToString());

                _objHistoryBL.UpdateFamiliar(ref objOperationResult, personmedicalhistoryDtoDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            Session["GrupoFamiliarId"] = null;
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

        protected void WindowAddDX_Close(object sender, WindowCloseEventArgs e)
        {
            txtOtroDx.Text = Session["OtroDx"].ToString();
        }

        protected void ddlDx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDx.SelectedValue == "80" || ddlDx.SelectedValue == "81" || ddlDx.SelectedValue == "82" || ddlDx.SelectedValue == "83" || ddlDx.SelectedValue == "84")
            {
                btnOtroDx.Enabled = true;
            }
            else
            {
                btnOtroDx.Enabled = false;
            }
        }

        protected void btnOtroDx_Click(object sender, EventArgs e)
        {

        }

       
    }
}