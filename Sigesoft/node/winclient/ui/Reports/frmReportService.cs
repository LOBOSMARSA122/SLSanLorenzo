using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    
    public partial class frmReportService : Form
    {
        string strFilterExpression;
        ServiceBL _objServiceBL = new ServiceBL();
        public frmReportService()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddServiceStatusId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceStatusId==" + ddServiceStatusId.SelectedValue);
            //if (ddlOrganizationId.SelectedValue.ToString() != "-1") Filters.Add("v_CustomerOrganizationId==" + "\"" + ddlOrganizationId.SelectedValue + "\"");
            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }

            if (ddlEmployerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlEmployerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_EmployerOrganizationId==" + "\"" + id3[0] + "\"&&v_EmployerLocationId==" + "\"" + id3[1] + "\"");
            }

            if (ddlWorkerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlWorkerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_WorkingOrganizationId==" + "\"" + id3[0] + "\"&&v_WorkingLocationId==" + "\"" + id3[1] + "\"");
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
            var rp = new Reports.crService();
            var aptitudeCertificate = objData;

             DataSet ds = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ToDataTable(aptitudeCertificate);
            dt.TableName = "Service";
            ds.Tables.Add(dt);
            rp.SetDataSource(ds);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }

        private List<ServiceList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = _objServiceBL.GetServicesPagedAndFilteredReport(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void frmReportService_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //Utils.LoadDropDownList(ddlOrganizationId, "Value1", "Id", BLL.Utils.GetOrganization(ref objOperationResult), DropDownListAction.All);

            var dataList = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 125, null).FindAll(p => p.Id != "1");

            Utils.LoadDropDownList(ddServiceStatusId, "Value1", "Id", dataList, DropDownListAction.All);
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));

            var customerOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);

            Utils.LoadDropDownList(ddlCustomerOrganization,
             "Value1",
             "Id",
             customerOrganization,
             DropDownListAction.Select);     

            var employerOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            Utils.LoadDropDownList(ddlEmployerOrganization,
             "Value1",
             "Id",
             employerOrganization,
             DropDownListAction.Select);

            var workingOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            Utils.LoadDropDownList(ddlWorkerOrganization,
             "Value1",
             "Id",
             workingOrganization,
             DropDownListAction.Select); 
 
            ddServiceStatusId.SelectedValue = ((int)Common.ServiceStatus.Culminado).ToString();
        }

        private void ddlCustomerOrganization_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void dtpDateTimeStar_Validating(object sender, CancelEventArgs e)
        {
            if (dtpDateTimeStar.Value.Date > dptDateTimeEnd.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Desde no puede ser Mayor a la fecha Hasta.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dptDateTimeEnd_Validating(object sender, CancelEventArgs e)
        {
            if (dptDateTimeEnd.Value.Date < dtpDateTimeStar.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Hasta no puede ser Menor a la fecha Desde.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

       

    }
              
}
