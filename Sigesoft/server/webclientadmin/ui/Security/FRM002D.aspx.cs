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

namespace Sigesoft.Server.WebClientAdmin.UI.Security
{
    public partial class FRM002D : System.Web.UI.Page
    {
        #region Declarations

        SecurityBL _objSecurityBL = new SecurityBL();

        #endregion

        #region Properties

        public int SystemUserId
        {
            get
            {
                if (Request.QueryString["systemUserId"] != null)
                {
                    string systemUserId = Request.QueryString["systemUserId"].ToString();
                    if (!string.IsNullOrEmpty(systemUserId))
                    {
                        return Convert.ToInt32(systemUserId);
                    }
                }

                return -1;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
            }
        }

        private void LoadData()
        {

            OperationResult objOperationResult1 = new OperationResult();

            ddlRole.Items.Insert(0, new FineUI.ListItem(Constants.Select, Constants.SelectValue));

            var nodeList = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetAllNodeForCombo(ref objOperationResult1);
            Sigesoft.Server.WebClientAdmin.UI.Utils.LoadDropDownList(ddlNode, "Value1", "Id", nodeList, DropDownListAction.Select);

            if (Mode == "New")
            {

            }
            else if (Mode == "Edit")
            {
                // cargar grid 
                Loadgrd();
            }

        }

        protected void grd_RowCommand(object sender, GridCommandEventArgs e)
        {
            // Eliminar 
            if (e.CommandName == "DeleteAction")
            {
                // Obtener los IDs de la fila seleccionada
                int nodeId = Convert.ToInt32(grd.DataKeys[e.RowIndex][0]);
                int roleId = int.Parse(grd.DataKeys[e.RowIndex][1].ToString());
                int systemUserId = Convert.ToInt32(grd.DataKeys[e.RowIndex][2]);

                // Borrar permisos de contexto asociados al usuario actual
                OperationResult objOperationResult = new OperationResult();
                _objSecurityBL.DeleteSystemUserRoleNode(ref objOperationResult,
                                                                nodeId,
                                                                systemUserId,
                                                                roleId,
                                                                ((ClientSession)Session["objClientSession"]).GetAsList());
                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                    return;
                }
                //else  // Operación con error
                //{
                //    Alert.ShowInTop(Constants.GenericErrorMessage, "ERROR!", MessageBoxIcon.Error);
                //    // Se queda en el formulario.
                //}

                Loadgrd();
            }
        }

        protected void grd_PageIndexChange(object sender, GridPageEventArgs e)
        {
            grd.PageIndex = e.NewPageIndex;
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {

        }

        private void Loadgrd()
        {
            OperationResult objOperationResult = new OperationResult();
            var objEntity = _objSecurityBL.GetSystemUserRoleNodeForGridView(ref objOperationResult, SystemUserId);

            if (objEntity.Count != 0)
            {
                grd.DataSource = objEntity;
                grd.DataBind();
            }
            else
            {
                grd.DataSource = null;
                grd.DataBind();
            }

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                return;
            }
            //else  // Operación con error
            //{
            //    Alert.ShowInTop(Constants.GenericErrorMessage, "ERROR!", MessageBoxIcon.Error);
            //    // Se queda en el formulario.
            //}
        }

        protected void ddlNode_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult1 = new OperationResult();
            var roleList = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetRoleByNodeIdForCombo(ref objOperationResult1, int.Parse(ddlNode.SelectedValue));

            Sigesoft.Server.WebClientAdmin.UI.Utils.LoadDropDownList(ddlRole,
                "Value1",
                "Id",
                roleList,
                DropDownListAction.Select);

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // grabar
            OperationResult objOperationResult = new OperationResult();

            var objEntityDTO = new systemuserrolenodeDto();

            objEntityDTO.i_SystemUserId = SystemUserId;
            objEntityDTO.i_RoleId = int.Parse(ddlRole.SelectedValue);
            objEntityDTO.i_NodeId = int.Parse(ddlNode.SelectedValue);

            _objSecurityBL.AddSystemUserRoleNode(ref objOperationResult,
                                                 objEntityDTO,
                                                 ((ClientSession)Session["objClientSession"]).GetAsList());

            if (objOperationResult.ErrorMessage != null)
            {
                Alert.ShowInTop(objOperationResult.ErrorMessage);
                return;
            }

            //if (objOperationResult.ErrorMessage != null)
            //{
            //    Alert.ShowInTop(string.Format("El Nodo Rol de <font color='red'> {0} </font> ya se encuentra registrado.<br> Por favor elija otro.", ddlRole.SelectedText));
            //    return;
            //}

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                return;
            }
            //else  // Operación con error
            //{
            //    Alert.ShowInTop(Constants.GenericErrorMessage, "ERROR!", MessageBoxIcon.Error);
            //    // Se queda en el formulario.
            //}

            // cargar grid 
            Loadgrd();
        }

    }
}