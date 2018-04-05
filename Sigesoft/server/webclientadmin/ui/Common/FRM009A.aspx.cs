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

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class FRM009A : System.Web.UI.Page
    {
        Utils UtilComboBox = new Utils();
        LogBL _objLogBL = new LogBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        NodeBL _objNodeBL = new NodeBL();
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
            OperationResult objOperationResult = new OperationResult();
            //Llenado de combos
            Utils.LoadDropDownList(ddlNodeId, "v_Description", "i_NodeId", _objNodeBL.GetAllNode(ref objOperationResult), DropDownListAction.All);
            Utils.LoadDropDownList(ddlEventTypeId, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 102), DropDownListAction.All);
            Utils.LoadDropDownList(ddlSuccess, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111), DropDownListAction.All);
            string Mode = Request.QueryString.ToString();
            string LogId =  null;
            if (Request.QueryString["v_LogId"] != null)
                LogId = Request.QueryString["v_LogId"].ToString();
            // Get the Entity Data
            LogList objEntity = _objLogBL.GetLog(ref objOperationResult,LogId);

            // Save the entity on the session
            Session["objEntity"] = objEntity;

            txtLogId.Text = objEntity.v_LogId;
            ddlNodeId.SelectedValue = objEntity.i_NodeId.ToString();
            ddlEventTypeId.SelectedValue = objEntity.i_EventTypeId.ToString();
            ddlSuccess.SelectedValue = objEntity.i_Success.ToString();
            txtUserName.Text = objEntity.v_SystemUserName;
            txtExpirationDate.SelectedDate = objEntity.d_Date;
            txtProcessEntity.Text = objEntity.v_ProcessEntity;
            txtElementItem.Text = objEntity.v_ElementItem;
            txtError.Text = objEntity.v_ErrorException;
            }      
    }
}