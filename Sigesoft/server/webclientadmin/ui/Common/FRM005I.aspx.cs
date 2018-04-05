using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using FineUI;

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRM005I : System.Web.UI.Page
    {
        NodeBL _objNodeBL = new NodeBL();

        #region Properties

        public int NodeId
        {
            get
            {
                if (Request.QueryString["nodeId"] != null)
                {
                    string nodeId = Request.QueryString["nodeId"].ToString();
                    if (!string.IsNullOrEmpty(nodeId))
                    {
                        return Convert.ToInt32(nodeId);
                    }
                }

                return 0;
            }
        }

        public string Mode
        {
            get
            {
                if (Request.QueryString["Mode"] != null)
                {
                    string mode = Request.QueryString["Mode"].ToString();
                    if (!string.IsNullOrEmpty(mode))
                    {
                        return mode;
                    }
                }

                return string.Empty;
            }
        }  

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
              
            }
        }

        private void LoadData()
        {
            LoadComboBox();

            if (Mode == "New")
            {

            }
            else if (Mode == "Edit")
            {
                OperationResult objOperationResult1 = new OperationResult();
                var node = _objNodeBL.GetPharmacyWarehouseByNode(ref objOperationResult1, NodeId);
                ddlOrganization.SelectedValue = string.IsNullOrEmpty(node.v_PharmacyWarehouseId) ? "-1" : node.v_PharmacyWarehouseId;

                Session["node"] = node;
            }       

        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult1 = new OperationResult();
            var orgLocationWarehouse = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetPharmacyWarehouse(ref objOperationResult1, NodeId);

            Sigesoft.Server.WebClientAdmin.UI.Utils.LoadDropDownList(ddlOrganization,
                "Value1",
                "Id",
                orgLocationWarehouse,
                DropDownListAction.Select);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {          
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                
            }
            else if (Mode == "Edit")
            {
                if (Session["node"] != null)
                {
                    // Get the entity from the session
                    nodeDto objEntity = (nodeDto)Session["node"];

                    // Populate the entity
                    objEntity.i_NodeId = NodeId;
                    objEntity.v_PharmacyWarehouseId = ddlOrganization.SelectedValue.ToString();

                    // Save the data
                    _objNodeBL.UpdatePharmacyWarehouseByNode(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
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

    }
}