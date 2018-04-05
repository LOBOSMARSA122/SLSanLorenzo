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
    public partial class frmComisionMedico : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        string strFilterExpression;

        public frmComisionMedico()
        {
            InitializeComponent();
        }

        private void frmComisionMedico_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult1, ""), DropDownListAction.All);

        }

        private void ddlUsuario_SelectedValueChanged(object sender, EventArgs e)
        {
            ProfessionalBL oProfessionalBL = new ProfessionalBL();
            OperationResult objOperationResult = new OperationResult();
            SystemUserList oSystemUserList = new SystemUserList();

            if (ddlUsuario.SelectedValue == null)
                return;

            if (ddlUsuario.SelectedValue.ToString() == "-1")
            {
                lblNombreProfesional.Text = "Nombres y Apellidos del Profesional";
                return;
            }

            oSystemUserList = oProfessionalBL.GetSystemUserName(ref objOperationResult, int.Parse(ddlUsuario.SelectedValue.ToString()));

            lblNombreProfesional.Text = oSystemUserList.v_PersonName;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (uvReporte.Validate(true, false).IsValid)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    List<string> Filters = new List<string>();
                    DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                    DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                    if (ddlUsuario.SelectedValue.ToString() != "-1")
                        Filters.Add("i_InsertUserId==" + ddlUsuario.SelectedValue);
                    
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
                    var objData = _serviceBL.ComisionAuxiliar(pdatBeginDate, pdatEndDate, strFilterExpression);

                    grdData.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                    txtTotal.Text = objData.Sum(p => p.Comision).ToString();
                    if (grdData.Rows.Count > 0)
                        grdData.Rows[0].Selected = true;

                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Comisión de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            //NombreArchivo = "Matriz de datos";
            //}

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
