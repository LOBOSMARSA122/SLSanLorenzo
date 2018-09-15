﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLiquidacion : Form
    {
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();
        public frmLiquidacion()
        {
            InitializeComponent();
        }

        private void frmLiquidacion_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
            Utils.LoadDropDownList(ddlEmployerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

            UltraGridColumn c = grdData.DisplayLayout.Bands[1].Columns["b_Seleccionar"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtCCosto.Text)) Filters.Add("CCosto.Contains(\"" + txtCCosto.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtNroLiquidacion.Text)) Filters.Add("v_NroLiquidacion.Contains(\"" + txtNroLiquidacion.Text.Trim() + "\")");
    
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }

            if (ddlEmployerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlEmployerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_EmployerOrganizationId==" + "\"" + id3[0] + "\"&&v_EmployerLocationId==" + "\"" + id3[1] + "\"");
            }

          
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

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdData.Rows.Count > 0)
            {
                grdData.Rows[0].Selected = true;
                btnExportarExcel.Enabled = true;
            }

        }

        private List<Liquidacion> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = _serviceBL.ListaLiquidacion(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }


        private void btnEditarServicio_Click(object sender, EventArgs e)
        {
            var band = this.grdData.DisplayLayout.Bands[1];

            var ids = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                       where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                       select row).ToList().Select(p => p.Cells["v_ServiceId"].Value.ToString()).ToArray();

            var idProtocolId = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                       where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                       select row).ToList().Select(p => p.Cells["v_ProtocolId"].Value.ToString()).ToArray().FirstOrDefault();

            var personId = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                                where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                                select row).ToList().Select(p => p.Cells["v_PersonId"].Value.ToString()).ToArray().FirstOrDefault();


            if (ids.Length > 1)
            {
                MessageBox.Show("Solo puede seleccionar un registro a la vez", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new frmEditarServicio(ids[0], idProtocolId, personId);
            frm.ShowDialog();


        }

        private void btnGenerarLiq_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdData.DisplayLayout.Bands[1];

            var ids = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                                               where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                                               select row).ToList().Select(p => p.Cells["v_ServiceId"].Value.ToString()).ToArray();

            if (ids.Length == 0)
            {
                MessageBox.Show("Seleccionar Registros", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            foreach (var id in ids)
            {
                _serviceBL.GenerarLiquidacion(ids, Globals.ClientSession.GetAsList());
            }

            MessageBox.Show("Actualizado", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnFilter_Click(sender, e);
        }

        private void btnLiberarRegistro_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdData.DisplayLayout.Bands[1];

            var ids = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                       where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                       select row).ToList().Select(p => p.Cells["v_ServiceId"].Value.ToString()).ToArray();

            if (ids.Length == 0)
            {
                MessageBox.Show("Seleccionar Registros", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            foreach (var id in ids)
            {
                _serviceBL.GenerarLiberar(id, Globals.ClientSession.GetAsList());
            }

            MessageBox.Show("Actualizado", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnFilter_Click(sender, e);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }     
        }

        private void grdData_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;
                }
                else
                {
                    e.Cell.Value = false;
                }
            }
        }
    }
}