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
    public partial class frmInterfaceSeguimiento : Form
    {
        string strFilterExpression = "";
        public frmInterfaceSeguimiento()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                List<string> Filters = new List<string>();
                DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                }

                if (ddlProtocolId.SelectedValue.ToString() != "-1")
                    Filters.Add("IdProtocolId==" + "\"" + ddlProtocolId.SelectedValue + "\"");

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

                var objData = new PacientBL().GetInterfaceSeguimiento(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdData.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
              
            }
        }

        private void frmInterfaceSeguimiento_Load(object sender, EventArgs e)
        {
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            OperationResult objOperationResult = new OperationResult();
            //var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, 9);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);

            if (Globals.ClientSession.i_SystemUserId == 31)
            {
                ddlCustomerOrganization.Enabled = false;
                ddlCustomerOrganization.SelectedValue = "N009-OO000000108|N009-OL000000115";
            }

            dtpDateTimeStar.Value = fecha1;
            dptDateTimeEnd.Value = DateTime.Today;
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddlCustomerOrganization_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }
            ddlProtocolId.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);

        }

        private void grdData_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnExport.Enabled = grdData.Rows.Count > 0;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.grdData.PerformAction(UltraGridAction.Copy, false, false);
        }

        private void grdData_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
          
        }
    }
}
