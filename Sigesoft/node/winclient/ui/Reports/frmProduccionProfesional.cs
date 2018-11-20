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

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmProduccionProfesional : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        string strFilterExpression;

        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
       
        public frmProduccionProfesional()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frmProduccionProfesional_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);

            Utils.LoadDropDownList(cbOrganization,
             "Value1",
             "Id",
             dataListOrganization,
             DropDownListAction.All);

            Utils.LoadDropDownList(cbIntermediaryOrganization,
               "Value1",
               "Id",
               dataListOrganization1,
               DropDownListAction.All);

            Utils.LoadDropDownList(cbOrganizationInvoice,
              "Value1",
              "Id",
              dataListOrganization2,
              DropDownListAction.All);

            Utils.LoadDropDownList(cbEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

            

            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));

            Utils.LoadDropDownList(ddlComponentId, "Value1", "Value4", groupComponentList, DropDownListAction.All);

            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult,""), DropDownListAction.All);

            if (Globals.ClientSession.i_SystemUserId == 31)
            {
                cbOrganizationInvoice.Enabled = false;
                cbOrganizationInvoice.SelectedValue = "N009-OO000000108|N009-OL000000115";
            }
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
            txtInfAdicional.Text = oSystemUserList.InfAdicional == "" || oSystemUserList.InfAdicional == null ? "0" : oSystemUserList.InfAdicional;
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

                    if (cbEsoType.SelectedValue.ToString() != "-1") Filters.Add("i_EsoTypeId==" + int.Parse(cbEsoType.SelectedValue.ToString()));
          
                    if (cbOrganization.SelectedValue.ToString() != "-1")
                    {
                        var id1 = cbOrganization.SelectedValue.ToString().Split('|');
                        Filters.Add("v_OrganizationId==" + "\"" + id1[0] + "\"&&v_LocationId==" + "\"" + id1[1] + "\"");
                    }

                    if (cbIntermediaryOrganization.SelectedValue.ToString() != "-1")
                    {
                        var id2 = cbIntermediaryOrganization.SelectedValue.ToString().Split('|');
                        Filters.Add("v_WorkingOrganizationId==" + "\"" + id2[0] + "\"&&v_WorkingLocationId==" + "\"" + id2[1] + "\"");
                    }
                    if (cbOrganizationInvoice.SelectedValue.ToString() != "-1")
                    {
                        var id3 = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
                        Filters.Add("v_OrganizationInvoiceId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                    }

                    if (ddlUsuario.SelectedValue.ToString() != "-1")
                        Filters.Add("i_ApprovedUpdateUserId==" + ddlUsuario.SelectedValue);

                    if (ddlComponentId.SelectedValue.ToString() != "0")
                        Filters.Add("i_CategoryId==" + ddlComponentId.SelectedValue);

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

                    //var objData = _serviceBL.ReporteProduccionProfesional(pdatBeginDate, pdatEndDate, cbOrganizationInvoice.SelectedValue.ToString(), strFilterExpression, ddlUsuario.Text, lblNombreProfesional.Text, ddlComponentId.Text, int.Parse(ddlComponentId.SelectedValue.ToString()), cbOrganizationInvoice.Text);
                    var objData = _serviceBL.ReporteProduccionProfesionalAMC(pdatBeginDate, pdatEndDate, cbOrganizationInvoice.SelectedValue.ToString(), strFilterExpression);
                   
                    grdData.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

                    if (grdData.Rows.Count > 0)
                        grdData.Rows[0].Selected = true;

                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            if (cbOrganizationInvoice.SelectedValue.ToString() != "-1")
            {
                var id3 = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }

            if (ddlUsuario.SelectedValue.ToString() != "-1")
                Filters.Add("i_ApprovedUpdateUserId==" + ddlUsuario.SelectedValue);

            if (ddlComponentId.SelectedValue.ToString() != "-1")
                Filters.Add("i_CategoryId==" + ddlComponentId.SelectedValue);


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

            var frm = new Reports.frmProduccionProfesionalImprimir(pdatBeginDate.Value, pdatEndDate.Value, cbOrganizationInvoice.SelectedValue.ToString(), strFilterExpression, ddlUsuario.Text, lblNombreProfesional.Text, ddlComponentId.Text, int.Parse(ddlComponentId.SelectedValue.ToString()), cbOrganizationInvoice.Text);
            frm.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ////"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            //if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            //{
            //    NombreArchivo = "Reporte Vacunas de " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            //    //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            //}
            //else
            //{
                NombreArchivo = "Reporte Producción Profesional de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
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

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnExport.Enabled = grdData.Rows.Count > 0;
        }

     
    }
}
