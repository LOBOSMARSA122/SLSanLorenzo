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
    public partial class frmConsultarExamen : Form
    {
        string strFilterExpression;
        MedicalExamBL _objMedicalExamBL = new MedicalExamBL();
        MedicalExamFieldsBL _objMedicalExamFieldsBL = new MedicalExamFieldsBL();
        //string strMedicalExamId;
        //string _ComponentName;
        //int _RowIndexgrdDataMedicalExamFields;
        public frmConsultarExamen()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }
        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }
        private void frmConsultarExamen_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }
            #endregion
            OperationResult objOperationResult = new OperationResult();

            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
            //Llenado de combos

            Utils.LoadDropDownList(ddlCategoryId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 116, null), DropDownListAction.Select);

            //Utils.LoadComboTreeBoxList(ddlCategoryId, BLL.Utils.GetSystemParameterForComboTreeBox(ref objOperationResult, 116, null), DropDownListAction.All);         

            btnFilter_Click(sender, e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (ddlCategoryId.SelectedValue.ToString() != "-1") Filters.Add("i_CategoryId==" + ddlCategoryId.SelectedValue);
            if (!string.IsNullOrEmpty(txtExamen.Text)) Filters.Add("v_Name.Contains(\"" + txtExamen.Text.Trim() + "\")");
            Filters.Add("i_IsDeleted==0");
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

            this.BindGridMedicalExam();
        }
        private void BindGridMedicalExam()
        {
            var objData = GetData(0, null, "v_Name ASC", strFilterExpression);

            grdDataMedicalExam.DataSource = objData;

            if (grdDataMedicalExam.Rows.Count > 0)
                grdDataMedicalExam.Rows[0].Selected = true;
        }
        private List<MedicalExamList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objMedicalExamBL.GetMedicalExamPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }       
    }
}
