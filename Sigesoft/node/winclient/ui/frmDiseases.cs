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
    public partial class frmDiseases : Form
    {
        MedicalExamFieldValuesBL _objMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
        public  DiseasesList _objDiseasesList = new DiseasesList();
        string strFilterExpression;
        public    string strEnfermedad;

        public frmDiseases()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (ultraValidator2.Validate(true, false).IsValid)
            {
                // Get the filters from the UI
                List<string> Filters = new List<string>();

                if (rbCode.Checked == true)
                {
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_CIE10Id.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_Name.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_CIE10Description1.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
                }
              
                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " || ";
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
            var objData = GetData(0, null, "v_Name ASC, v_DiseasesId ASC", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<DiseasesList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
           

            var _objData = _objMedicalExamFieldValuesBL.GetDiseasesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();
            if (grdData.Selected.Rows == null) return;
            if (grdData.Selected.Rows.Count == 0) return;

            if (grdData.Selected.Rows[0].Cells[0].Value == null)
            {
                objDiseaseDto.v_CIE10Id = grdData.Selected.Rows[0].Cells[1].Value.ToString();
                objDiseaseDto.v_Name = grdData.Selected.Rows[0].Cells[2].Value.ToString();

                objDiseaseDto1 = _objMedicalExamFieldValuesBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);

                if (objDiseaseDto1 == null)
                {
                    objDiseaseDto.v_DiseasesId = _objMedicalExamFieldValuesBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());
                }
                else
                {
                    MessageBox.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }              

                _objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                _objDiseasesList.v_CIE10Id = objDiseaseDto.v_CIE10Id;
                _objDiseasesList.v_Name = objDiseaseDto.v_Name;
            }
            else
            {
                _objDiseasesList.v_DiseasesId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
                _objDiseasesList.v_CIE10Id = grdData.Selected.Rows[0].Cells[1].Value.ToString();
                _objDiseasesList.v_Name = grdData.Selected.Rows[0].Cells[2].Value.ToString();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void grdData_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows == null) return;
            if (grdData.Selected.Rows.Count == 0) return;

            btnUpdateandSelect.Enabled = true;
            if (grdData.Selected.Rows[0].Cells[2].Value != null)
            {
                txtDiseases.Text = grdData.Selected.Rows[0].Cells[2].Value.ToString();
            }
            else
            {
                txtDiseases.Text = "No tiene Nombre";
            }
            
        }

        private void btnUpdateandSelect_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();
            if (btnUpdateandSelect.Enabled == false) return;

            if (ultraValidator1.Validate(true, false).IsValid)
            {
                if (grdData.Selected.Rows[0].Cells[0].Value != null)
                {
                    objDiseaseDto = _objMedicalExamFieldValuesBL.GetDiseases(ref  objOperationResult, grdData.Selected.Rows[0].Cells[0].Value.ToString());

                    objDiseaseDto.v_CIE10Id = grdData.Selected.Rows[0].Cells[1].Value.ToString();
                    objDiseaseDto.v_Name = txtDiseases.Text;
                    _objMedicalExamFieldValuesBL.UpdateDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());

                    _objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                    _objDiseasesList.v_CIE10Id = objDiseaseDto.v_CIE10Id;
                    _objDiseasesList.v_Name = objDiseaseDto.v_Name;
                }
                else
                {
                    objDiseaseDto.v_CIE10Id = grdData.Selected.Rows[0].Cells[1].Value.ToString();
                    objDiseaseDto.v_Name = txtDiseases.Text;


                    objDiseaseDto1 = _objMedicalExamFieldValuesBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);

                    if (objDiseaseDto1 == null)
                    {
                        objDiseaseDto.v_DiseasesId = _objMedicalExamFieldValuesBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());
                    }
                    else
                    {
                        MessageBox.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }              


                    //objDiseaseDto.v_DiseasesId=  _objMedicalExamFieldValuesBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());

                    _objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                    _objDiseasesList.v_CIE10Id = grdData.Selected.Rows[0].Cells[1].Value.ToString();
                    _objDiseasesList.v_Name = txtDiseases.Text;
                }

                //strEnfermedad = objDiseaseDto.v_DiseasesId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }         

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {          
        }

        private void txtDiseases_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnUpdateandSelect_Click(sender, e);
            }
        }

        private void txtDiseasesFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnFilter_Click(sender, e);
            }
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

        private void frmDiseases_Load(object sender, EventArgs e)
        {

            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
        }
        

    }
}
