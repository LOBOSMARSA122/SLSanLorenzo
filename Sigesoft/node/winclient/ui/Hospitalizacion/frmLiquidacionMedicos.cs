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

            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                var x = row.Cells["Select"].Value;
            }
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
    }
}
