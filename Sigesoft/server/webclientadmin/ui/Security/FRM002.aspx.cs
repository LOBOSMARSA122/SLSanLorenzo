using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;


namespace Sigesoft.Server.WebClientAdmin.UI.Security
{
    public partial class FRM002 : System.Web.UI.Page
    {
        #region Declarations
        SecurityBL _objSecurityBL = new SecurityBL();
        
        #endregion
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strFilterExpression"] = null;
                btnNew.OnClientClick = winEditUser.GetShowReference("FRM002A.aspx?Mode=New");
                Sigesoft.Server.WebClientAdmin.BLL.Utils.SetFormActionsInSession("FRM002");
                btnNew.Enabled = Sigesoft.Server.WebClientAdmin.BLL.Utils.IsActionEnabled("FRM002_ADD");

                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(ddlUSerType, "Value1", "Id", new SystemParameterBL().GetSystemParameterForCombo(ref objOperationResult, 132), DropDownListAction.All);

                BindGrid();
                var cond = true;
                cond = ((ClientSession)Session["objClientSession"]).i_SystemUserId == 11 ? false : true;
                if (cond == true)
                {
                    btnFilter_Click(txtUserNameFilter, e);
                }
                
             
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(txtUserNameFilter.Text))
            {
                filter = "v_UserName.Contains(\"" + txtUserNameFilter.Text.Trim() + "\")";
            }

            Session["strFilterExpression"] = filter;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            var objUserList = GetData(grdData.PageIndex, grdData.PageSize, "i_SystemUserId ASC", strFilterExpression);

            if (objUserList.Count > 0)
            {
                grdData.DataSource = objUserList;
                grdData.DataBind();
                var cond = true;
                cond = ((ClientSession)Session["objClientSession"]).i_SystemUserId == 11 ? false : true;
                if (cond == true)
                {
                    txtUserNameFilter.Text = ((ClientSession)Session["objClientSession"]).v_UserName;
                    txtUserNameFilter.Enabled = false;   
                }
                             
            }
            else
            {
                Alert.ShowInTop("Información de operación:" + System.Environment.NewLine + "No existen registros.");
            }

            
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = "i_IsDeleted=0" ;
            if (Session["strFilterExpression"] != null)
            {
                var ss = Convert.ToString(Session["strFilterExpression"]);
                if (ss.Length > 0)
                {
                    strFilterExpression += "&&" + ss; 
                }
                
            }
               
            return _objSecurityBL.GetSystemUserCount(ref objOperationResult, strFilterExpression);
           
        }

        private List<SystemUserList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            List<SystemUserList> objUserList = _objSecurityBL.GetSystemUserPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression,int.Parse(ddlUSerType.SelectedValue));         

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }
            //else  // Operación con error
            //{
            //    Alert.ShowInTop(Constants.GenericErrorMessage, "ERROR!", MessageBoxIcon.Error);
            //    // Se queda en el formulario.
            //}

            return objUserList;
          
        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                OperationResult objOperationResult = new OperationResult();
                // Obtener los IDs de la fila seleccionada
                int systemUserId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);
                _objSecurityBL.DeleteSystemUSer(ref objOperationResult, systemUserId, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                    return;
                }

                // Borrar permisos globales asociados al usuario actual
                OperationResult objOperationResult1 = new OperationResult();
                _objSecurityBL.DeleteSystemUserGlobalProfile(ref objOperationResult1, systemUserId, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (objOperationResult1.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                    return;
                }

                //// Borrar permisos contextuales asociados al usuario actual
                //OperationResult objOperationResult2 = new OperationResult();
                //_objSecurity.DeleteSystemUserContextProfileBySystemUserId(ref objOperationResult1, intSystemUserId, ((ClientSession)Session["objClientSession"]).GetAsList());

                // Borrar almacenes asociados al usuario actual
                OperationResult objOperationResult3 = new OperationResult();
                _objSecurityBL.DeleteRestrictedWarehouseProfileBySystemUSerId(ref objOperationResult3, systemUserId, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (objOperationResult3.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                    return;
                }

                BindGrid();
            }

        }

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
           
        }

        protected void winEditPermission_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void grdData_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            WindowField wfEdit1 = grdData.FindColumn("myWindowField") as WindowField;
            wfEdit1.Enabled = Sigesoft.Server.WebClientAdmin.BLL.Utils.IsActionEnabled("FRM002_EDIT");

            WindowField wfEdit2 = grdData.FindColumn("myWindowField3") as WindowField;
            wfEdit2.Enabled = Sigesoft.Server.WebClientAdmin.BLL.Utils.IsActionEnabled("FRM002_RESTRICTED_WAREHOUSE");

            WindowField wfEdit3 = grdData.FindColumn("myWindowField1") as WindowField;
            wfEdit3.Enabled = Sigesoft.Server.WebClientAdmin.BLL.Utils.IsActionEnabled("FRM002_ASSIGNED_GLOBALS_PERMISSIONS");

            WindowField wfEdit4 = grdData.FindColumn("myWindowField4") as WindowField;
            wfEdit4.Enabled = Sigesoft.Server.WebClientAdmin.BLL.Utils.IsActionEnabled("FRM002_ASSIGNED_ROLE");

            LinkButtonField lbfDelete = grdData.FindColumn("lbfAction2") as LinkButtonField;
            lbfDelete.Enabled = Sigesoft.Server.WebClientAdmin.BLL.Utils.IsActionEnabled("FRM002_DELETE");

        }

        protected void winEditUser_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

       

    }
}