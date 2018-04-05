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
    public partial class frmMovement : Form
    {
        string strFilterExpression;
        MovementBL _objLogisticBL = new MovementBL();

        public frmMovement()
        {
            InitializeComponent();
        }

        private void frmMovement_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            List<KeyValueDTO> _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmMovement", Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_SystemUserId);

            contextMenuMovement.Items["editarToolStripMenuItem"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmMovement_ADD", _formActions);
           

            contextMenuMovement.Items["editarToolStripMenuItem"].Enabled = true;
            strFilterExpression = null;
            //Utils.LoadDropDownList(ddlOrganizationLocationId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
           // comentado por el tiempo de Test
            Utils.LoadDropDownList(ddlOrganizationLocationId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocationNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlware, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, "-1", "-1"), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMovementTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 109, null), DropDownListAction.All);
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (uvMovement.Validate(true, false).IsValid)
            {
                // Get the filters from the UI
                List<string> Filters = new List<string>();
                if (ddlware.SelectedValue.ToString() != "-1") Filters.Add("v_WarehouseId==" + "\"" + ddlware.SelectedValue + "\"");
                if (ddlMovementTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_MovementTypeId==" + ddlMovementTypeId.SelectedValue );

                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }
                this.BindGrid();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);

            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<MovementList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date;

            var _objData = _objLogisticBL.GetMovementsListByWarehouseId(ref objOperationResult, ddlware.SelectedValue.ToString(), 0, null, "", strFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _objData;
        }

        private void LoadcbWarehouse()
        {
            OperationResult objOperationResult = new OperationResult();
            var index = ddlOrganizationLocationId.SelectedIndex;
            if (index == 0)
            {
                Utils.LoadDropDownList(ddlware, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, "-1", "-1"), DropDownListAction.Select);
                return;
            }
            var dataList = ddlOrganizationLocationId.SelectedValue.ToString().Split('|');
            string idOrg = dataList[1];
            string idLoc = dataList[2];

            Utils.LoadDropDownList(ddlware, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, idOrg, idLoc), DropDownListAction.Select);
        }

        private void ddlOrganizationLocationId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadcbWarehouse();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            frmWarehouseInput frm = new frmWarehouseInput(ddlware.Text,ddlware.SelectedValue.ToString(),null,"New");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void ddlware_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlware.Text.ToString() != "--Seleccionar--" && ddlware.Text.ToString() != "")
            {
                btnInput.Enabled = true;
                btnOutput.Enabled = true;
                //btnTransferedWarehouse.Enabled = true;
            }
            else
            {
                btnInput.Enabled = false;
                btnOutput.Enabled = false;
                //btnTransferedWarehouse.Enabled = false;
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string MovementId;
            MovementId = grdData.Selected.Rows[0].Cells[1].Value.ToString();

            int IsTransfer = int.Parse(grdData.Selected.Rows[0].Cells[9].Value.ToString());
            int  MovementTypeId = int.Parse(grdData.Selected.Rows[0].Cells[7].Value.ToString());

            if (MovementTypeId == 1 && IsTransfer !=19 && IsTransfer != 20)
            {
                frmWarehouseInput frm = new frmWarehouseInput(ddlware.Text.ToString(), ddlware.SelectedValue.ToString(), MovementId, "Edit");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    // Refrescar grilla
                    btnFilter_Click(sender, e);
                }
            }
            else if (MovementTypeId == 2 && IsTransfer != 20 && IsTransfer != 19)
            {
                frmWarehouseOutput frm = new frmWarehouseOutput(ddlware.Text.ToString(), ddlware.SelectedValue.ToString(), MovementId, "Edit");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    // Refrescar grilla
                    btnFilter_Click(sender, e);
                }
            }
            else if (((MovementTypeId == 2 || MovementTypeId == 1) && IsTransfer == 19) || ((MovementTypeId == 2 || MovementTypeId == 1) && IsTransfer == 20))
            {
                frmTransferBetweenWarehouses frm = new frmTransferBetweenWarehouses(ddlware.Text.ToString(), ddlware.SelectedValue.ToString(), MovementId, "Edit");
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    // Refrescar grilla
                    btnFilter_Click(sender, e);
                }
            }
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdData.Rows[row.Index].Selected = true;
                    contextMenuMovement.Items["editarToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    contextMenuMovement.Items["editarToolStripMenuItem"].Enabled = false;
                }
            } 
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            frmWarehouseOutput frm = new frmWarehouseOutput(ddlware.Text, ddlware.SelectedValue.ToString(), null, "New");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void btnTransferedWarehouse_Click(object sender, EventArgs e)
        {
            frmTransferBetweenWarehouses frm = new frmTransferBetweenWarehouses(ddlware.Text, ddlware.SelectedValue.ToString(), null, "New");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MovementBL objMovementBL = new MovementBL();
   
        }

    }
}
