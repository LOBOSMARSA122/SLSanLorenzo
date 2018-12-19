﻿using System;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmPagoEspecialistaOcupacional : Form
    {
        PagoEspecialistaOcupacionalBl oPagoEspecialistaOcupacionalBl = new PagoEspecialistaOcupacionalBl();

        public FrmPagoEspecialistaOcupacional()
        {
            InitializeComponent();
        }

        private void FrmPagoEspecialistaOcupacional_Load(object sender, EventArgs e)
        {
            UltraGridColumn c = grdData.DisplayLayout.Bands[0].Columns["Select"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(cboSystemUser, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult1, ""), DropDownListAction.Select);
        }

        private void cboSystemUser_SelectedValueChanged(object sender, EventArgs e)
        {
            var oProfessionalBl = new ProfessionalBL();
            var objOperationResult = new OperationResult();

            if (cboSystemUser.SelectedValue == null)
                return;

            if (cboSystemUser.SelectedValue.ToString() == "-1")
            {
                lblNombreProfesional.Text = @"Nombres y Apellidos del Profesional";
                return;
            }

            var oSystemUserList = oProfessionalBl.GetSystemUserName(ref objOperationResult, int.Parse(cboSystemUser.SelectedValue.ToString()));
            lblNombreProfesional.Text = oSystemUserList.v_PersonName;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            grdData.DataSource = oPagoEspecialistaOcupacionalBl.LoadGrid(pdatBeginDate, pdatEndDate,int.Parse(cboSystemUser.SelectedValue.ToString()), chkPaid.Checked ? 1: 0);
        }

        private void grdData_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key == "Select")
            {
                if (e.Cell.Value.ToString() == "False")
                {
                    e.Cell.Value = true;
                    var rowIndex = e.Cell.Row.Index;
                    var total = decimal.Parse(grdData.Rows[rowIndex].Cells["Total"].Text);
                    txtTotalPagar.Text = (decimal.Parse(txtTotalPagar.Text) + total).ToString();
                }
                else
                {
                    e.Cell.Value = false;
                }

            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            var ids = grdData.Rows.Where(c => Convert.ToBoolean(c.Cells["Select"].Value.ToString()))
                .Select(p => p.Cells["ServiceIds"].Value.ToString()).ToList();

            if (oPagoEspecialistaOcupacionalBl.Pay(ids)) MessageBox.Show(@"Se Pagó correctamente", @"INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(@"Hubo un error en el grabado", @"INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnFilter_Click(sender, e);
        }
    }
}
