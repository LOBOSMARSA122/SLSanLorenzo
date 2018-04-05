using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmRoadMap : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        CalendarBL _calendarBL = new CalendarBL(); 

        private string _serviceId;
        private string _calendarId;

        public frmRoadMap()
        {
            InitializeComponent();        
        }

        public frmRoadMap(string serviceId, string calendarId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _calendarId = calendarId;
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            // Cabecera
            var headerRoadMap = _calendarBL.GetHeaderRoadMap(_calendarId);

            // Detalle
            //var detailRoadMap = _serviceBL.GetServiceComponentsRoadMap(ref objOperationResult, _serviceId);
            var detailRoadMap = _serviceBL.GetServiceComponentsByCategoryExceptLab(ref objOperationResult, _serviceId);
            var rp = new Reports.crRoadMap();

            DataSet ds = new DataSet();

            DataTable dtHeader = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(headerRoadMap);
            DataTable dtDetail = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(detailRoadMap);
            
            dtHeader.TableName = "dtHeaderRoadMap";
            dtDetail.TableName = "dtDetailRoadMap";

            ds.Tables.Add(dtHeader);
            ds.Tables.Add(dtDetail);
            rp.SetDataSource(ds);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();
        }

        private void frmRoadMap_Load(object sender, EventArgs e)
        {
            ShowReport();
        }  

    }
}
