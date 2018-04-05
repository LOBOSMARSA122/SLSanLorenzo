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


namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM035C_1 : System.Web.UI.Page
    {
        OrganizationBL oOrganizationBL = new OrganizationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCombos();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

            }
        }
        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();

            if (Request.QueryString["v_OrganizationId"] != null)
                Session["v_OrganizationId"] = Request.QueryString["v_OrganizationId"].ToString();
            var x =BLL.Utils.GetLocation(ref objOperationResult, Session["v_OrganizationId"].ToString());
            Utils.LoadDropDownList(ddlLocation, "Value1", "Id", x, DropDownListAction.Select);
            if (x.Count > 0)
            {
                ddlLocation.SelectedIndex = 1;
            }
        }

        private void LoadData()
        {
            if (Request.QueryString["v_LocationId"] != null)
                Session["v_LocationId"] = Request.QueryString["v_LocationId"].ToString();
            
            if (Request.QueryString["v_GroupOccupationId"] != null)
                Session["v_GroupOccupationId"] = Request.QueryString["v_GroupOccupationId"].ToString();

            if (Request.QueryString["v_OrganizationId"] != null)
                Session["v_OrganizationId"] = Request.QueryString["v_OrganizationId"].ToString();

            if (Request.QueryString["v_Name"] != null)
                txtSede.Text = Request.QueryString["v_Name"].ToString();

            string Mode = Request.QueryString["Mode"].ToString();
            if (Mode == "Edit")
            {
                ddlLocation.SelectedValue = Request.QueryString["v_LocationId"].ToString();
            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                groupoccupationDto objEntity = new groupoccupationDto();

                // Populate the entity
                objEntity.v_LocationId = ddlLocation.SelectedValue.ToString();
                objEntity.v_Name = txtSede.Text;

                // Save the data                  
                oOrganizationBL.AddGroupOccupation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (txtSede2.Text.Trim()!="")
                {
                    objEntity = new groupoccupationDto();

                    // Populate the entity
                    objEntity.v_LocationId = ddlLocation.SelectedValue.ToString();
                    objEntity.v_Name = txtSede2.Text;

                    // Save the data                  
                    oOrganizationBL.AddGroupOccupation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

                }

                if (txtSede3.Text.Trim() != "")
                {
                    objEntity = new groupoccupationDto();

                    // Populate the entity
                    objEntity.v_LocationId = ddlLocation.SelectedValue.ToString();
                    objEntity.v_Name = txtSede3.Text;

                    // Save the data                  
                    oOrganizationBL.AddGroupOccupation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
                }

                if (txtSede4.Text.Trim() != "")
                {
                    objEntity = new groupoccupationDto();

                    // Populate the entity
                    objEntity.v_LocationId = ddlLocation.SelectedValue.ToString();
                    objEntity.v_Name = txtSede4.Text;

                    // Save the data                  
                    oOrganizationBL.AddGroupOccupation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
                }

                if (txtSede5.Text.Trim() != "")
                {
                    objEntity = new groupoccupationDto();

                    // Populate the entity
                    objEntity.v_LocationId = ddlLocation.SelectedValue.ToString();
                    objEntity.v_Name = txtSede5.Text;

                    // Save the data                  
                    oOrganizationBL.AddGroupOccupation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
                }

               
              
            }
            else if (Mode == "Edit")
            {
                groupoccupationDto objEntity = new groupoccupationDto();

                // Populate the entity
                objEntity.v_GroupOccupationId = Session["v_GroupOccupationId"].ToString();
                objEntity.v_LocationId = Session["v_LocationId"].ToString();
                objEntity.v_Name = txtSede.Text;
                oOrganizationBL.UpdateGroupOccupation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
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
    }
}