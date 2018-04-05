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
    public partial class frmParametersManagement : Form
    {
        SystemParameterBL _objBL = new SystemParameterBL();
        string strFilterExpression;

        public frmParametersManagement()
        {
            InitializeComponent();
            grdData.AutoGenerateColumns = false;
        }

        private void frmAdministracion_Load(object sender, EventArgs e)
        {
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
            //_Util.SetFormActionsInSession("FRM001");
            //btnNew.Enabled = _Util.IsActionEnabled("FRM001_ADD");
            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtParameterIdFilter.Text)) Filters.Add("i_ParameterId==" + txtParameterIdFilter.Text.Trim());
            if (!string.IsNullOrEmpty(txtDescriptionFilter.Text)) Filters.Add("v_Value1.Contains(\"" + txtDescriptionFilter.Text.Trim() + "\")");
            Filters.Add("i_GroupId==0 && i_IsDeleted==0");
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

            // Utilitario para obtener los tamaños de las columnas de la grilla
            Clipboard.SetText(Utils.GetGridColumnsDetail(grdData));
        }

        private void BindGrid()
        {

            var objData = GetData(0, 50, "i_GroupId ASC, i_ParameterId ASC", strFilterExpression);
            
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private SystemParameterList[] GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objBL.GetSystemParametersPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, 0).ToArray();

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEdicionRegistro frm = new frmEdicionRegistro();
            frm.ShowDialog();
        }

    }
}
