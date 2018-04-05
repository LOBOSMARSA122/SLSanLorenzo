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
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmControlVacunas : Form
    {
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();

        public frmControlVacunas()
        {
            InitializeComponent();
        }

        private void frmControlVacunas_Load(object sender, EventArgs e)
        {
            dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(-1);
            OperationResult objOperationResult = new OperationResult();
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);          
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);


            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";

            strFilterExpression = null;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_PacientDocument.Contains(\"" + txtPacient.Text.Trim() + "\")");

            var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
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

            this.BindGrid();
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);

            grdDataService.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdDataService.Rows.Count > 0)
                grdDataService.Rows[0].Selected = true;
        }

        private List<ControlVacunasList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);


            var _objData = _serviceBL.GetControlVacunasPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Reporte Vacunas de " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Reporte Vacunas de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataService, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdDataService_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnExport.Enabled = grdDataService.Rows.Count > 0;
        }

        private void grdDataService_InitializeRow(object sender, InitializeRowEventArgs e)
        {

            foreach (UltraGridRow rowSelected in this.grdDataService.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "2")
                {

                    DateTime dateValue;
                    Boolean Resultado = DateTime.TryParse(e.Row.Cells["v_Value1"].Value.ToString(),out dateValue);

                    if (Resultado)
                    {
                        DateTime FechaRegistro = DateTime.Parse(e.Row.Cells["v_Value1"].Value.ToString());

                        if (FechaRegistro > DateTime.Now.AddDays(-7) && FechaRegistro < DateTime.Now)
                        {
                             //Escojo 2 colores
                            e.Row.Appearance.BackColor = Color.Blue;
                            e.Row.Appearance.BackColor2 = Color.SkyBlue;
                            //Y doy el efecto degradado vertical
                            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                        }
                        else if (FechaRegistro > DateTime.Now)
                        {
                            //Escojo 2 colores
                            e.Row.Appearance.BackColor = Color.Red;
                            e.Row.Appearance.BackColor2 = Color.Pink;
                            //Y doy el efecto degradado vertical
                            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                        }
                    }

                }
            }
            ////Si el contenido de la columna Vip es igual a SI
            //if (e.Row.Cells["i_ServiceComponentStatusId"].Value.ToString() == ((int)Common.ServiceComponentStatus.Culminado).ToString())
            //{
            //    //Escojo 2 colores
            //    e.Row.Appearance.BackColor = Color.White;
            //    e.Row.Appearance.BackColor2 = Color.Cyan;
            //    //Y doy el efecto degradado vertical
            //    e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            //}
        }
    }
}
