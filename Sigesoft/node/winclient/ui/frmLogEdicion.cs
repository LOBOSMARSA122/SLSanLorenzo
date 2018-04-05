using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLogEdicion : Form
    {
        LogBL _objBL = new LogBL();
        string LogId;

        public frmLogEdicion(string pstrLogId)
        {
            InitializeComponent();
            this.Text = this.Text + "(" + pstrLogId + ")";
            LogId = pstrLogId;
        }

        private void frmLogEdicion_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            //Llenado de combos
            Utils.LoadDropDownList(ddlEventTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 102,null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlSuccess, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111,null), DropDownListAction.All);
      
            // Get the Entity Data
            LogList objEntity = _objBL.GetLog(ref objOperationResult, LogId);
            
            txtLogId.Text = objEntity.v_LogId;
            ddlEventTypeId.SelectedValue = objEntity.i_EventTypeId.ToString();
            ddlSuccess.SelectedValue = objEntity.i_Success.ToString();
            txtUserName.Text = objEntity.v_SystemUserName;
            txtExpirationDate.Value = objEntity.d_Date;
            txtProcessEntity.Text = objEntity.v_ProcessEntity;
            txtElementItem.Text = objEntity.v_ElementItem;
            txtError.Text = objEntity.v_ErrorException;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }      
    }
}
