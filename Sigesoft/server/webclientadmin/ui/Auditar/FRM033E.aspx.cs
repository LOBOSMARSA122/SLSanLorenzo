using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033E : System.Web.UI.Page
    {
        ServiceBL _oServiceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadCombo();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

            }
        }

        private void LoadData()
        {
            //setear dato de aptitud
            if (Request.QueryString["v_RestrictionId"] != null)
                Session["v_RestrictionId"] = Request.QueryString["v_RestrictionId"].ToString();

            if (Request.QueryString["v_MasterRestrictionId"] != null)
                Session["v_MasterRestrictionId"] = Request.QueryString["v_MasterRestrictionId"].ToString();

            if (Session["v_MasterRestrictionId"] != null)
            {
                ddlRecomendaciones.SelectedValue = Session["v_MasterRestrictionId"].ToString();
            }

        }

        private void LoadCombo()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlRecomendaciones, "v_Name", "v_MasterRecommendationRestricctionId", _oServiceBL.DevolverTodasRecomendacionesRestricciones(2, ""), DropDownListAction.All);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (Session["v_MasterRestrictionId"] != null)
            {
                //Actualizar Aptitud del Servicio
                _oServiceBL.ActualizarRestricctionServicio(ref objOperationResult, Session["v_RestrictionId"].ToString(), ddlRecomendaciones.SelectedValue.ToString(), ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            else
            {
                restrictionDto orestricctionDto = new restrictionDto();
                orestricctionDto.v_ServiceId = Session["ServiceId"].ToString();
                orestricctionDto.v_DiagnosticRepositoryId = Session["DiagnosticRepositoryId"].ToString();
                orestricctionDto.v_ComponentId = Session["ComponentId"].ToString();
                orestricctionDto.v_MasterRestrictionId = ddlRecomendaciones.SelectedValue;
                _oServiceBL.AgregarRestriccion(ref objOperationResult, orestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());
            }
            Session["v_MasterRestrictionId"] = null;
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

        protected void ddlRecomendaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRecomendaciones.SelectedValue != "-1")
            {
                lblName.Text = ddlRecomendaciones.SelectedText;
            }

        }

        protected void txtRecoBus_TextChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlRecomendaciones, "v_Name", "v_MasterRecommendationRestricctionId", _oServiceBL.DevolverTodasRecomendacionesRestricciones(2, txtRecoBus.Text), DropDownListAction.All);

        }

        protected void winEdit_Close(object sender, EventArgs e)
        {

        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            masterrecommendationrestricctionDto _masterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
            // Populate the entity                    
            _masterrecommendationrestricctionDto.v_Name = txtNuevaRecomendacion.Text.Trim();
            _masterrecommendationrestricctionDto.i_TypifyingId = 2;
            // Save the data
            var Resultado = _oServiceBL.AddMasterRecommendationRestricction(ref objOperationResult, _masterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            if (Resultado != null)
            {
                Alert.ShowInTop("La recomendación se grabó correctamente");
            }
        }
    }
}