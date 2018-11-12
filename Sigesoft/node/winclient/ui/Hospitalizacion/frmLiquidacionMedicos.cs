﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Infragistics.Win.UltraWinGrid;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmLiquidacionMedicos : Form
    {
        string strFilterExpression;
        public frmLiquidacionMedicos()
        {
            InitializeComponent();
        }

        private void frmLiquidacionMedicos_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessionalName(ref objOperationResult1), DropDownListAction.All);

            UltraGridColumn c = grdData.DisplayLayout.Bands[1].Columns["Select"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (ddlUsuario.SelectedValue.ToString() != "-1") Filters.Add("MedicoTratanteId==" + ddlUsuario.SelectedValue);

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
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            HospitalizacionBL o = new HospitalizacionBL();
            grdData.DataSource = o.LiquidacionMedicos(strFilterExpression, pdatBeginDate, pdatEndDate, chkPagados.Checked == true ? 1 : 0);

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdData.DisplayLayout.Bands[1];
            HospitalizacionBL o = new HospitalizacionBL();
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if ((bool)row.Cells["Select"].Value)
                {
                    string serviceId = row.Cells["v_ServiceId"].Value.ToString();
                    o.ActualizarPagoMedico(serviceId);
                }
            }

            btnFilter_Click(sender, e);
        }

        private void grdData_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "Select"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                    e.Cell.Value = true;
                else
                    e.Cell.Value = false;
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Liquidación de Médicos del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
