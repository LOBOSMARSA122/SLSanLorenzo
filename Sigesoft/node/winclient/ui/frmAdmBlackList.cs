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
    public partial class frmAdmBlackList : Form
    {
        BlackListBL _objBlackListBL = new BlackListBL();
        List<BlackListList> _BlackList = new List<BlackListList>();
    
        string strFilterExpression;
        public frmAdmBlackList()
        {
            InitializeComponent();
        }

        private void frmAdmBlackList_Load(object sender, EventArgs e)
        {
             OperationResult objOperationResult = new OperationResult();

             ultraGrid2.DisplayLayout.Bands[0].Columns["paciente"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

             ultraGrid2.DisplayLayout.Bands[0].Columns["v_Comment"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
             ultraGrid2.DisplayLayout.Bands[0].Columns["d_DateRegister"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
             ultraGrid2.DisplayLayout.Bands[0].Columns["d_DateDetection"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
             ultraGrid2.DisplayLayout.Bands[0].Columns["Img"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

             DateTime? pdatBeginDate;
             DateTime? pdatEndDate;
              pdatBeginDate = dtpDateTimeStar.Value.Date;
              pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
             _BlackList = _objBlackListBL.GetBlackListPagedAndFiltered(ref objOperationResult, 0, null, "", "", pdatBeginDate, pdatEndDate);
            ultraGrid2.DataSource = _BlackList;
        }

        private void btnRemoveBalckList_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            foreach (var item in _BlackList)
            {
                if (item.seleccion == true)
                {
                    _objBlackListBL.DeleteBlackList(ref objOperationResult, item.v_BlackListPerson, Globals.ClientSession.GetAsList());
                }               
            }

            if (objOperationResult.Success == 1)  // Operación sin error
            {
                MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ultraGrid2.DataSource = _objBlackListBL.GetBlackListPagedAndFiltered(ref objOperationResult, 0, null, "", "", dtpDateTimeStar.Value.Date, dptDateTimeEnd.Value.Date);
            }
            else  // Operación con error
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("Paciente.Contains(\"" + txtPacient.Text.Trim() + "\")" + " ||" + "v_NroDocumento.Contains(\"" + txtPacient.Text.Trim() + "\")");

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

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);

            ultraGrid2.DataSource = objData;          
        }

        private List<BlackListList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            _BlackList = _objBlackListBL.GetBlackListPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dtpDateTimeStar.Value.Date, dptDateTimeEnd.Value.Date);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _BlackList;
        }

        private void ultraGrid2_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            if (e.Row.Cells["i_Status"].Value == null)
                return;

            if ((int)e.Row.Cells["i_Status"].Value == (int)StatusBlackListPerson.Detectado)
            {
                e.Row.Cells["Img"].Value = Resources.accept;
                e.Row.Cells["Img"].ToolTipText = "Detectado";
            }
        }
    }
}
