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
    public partial class frmReporteGerencia : Form
    {
        public frmReporteGerencia()
        {
            InitializeComponent();
        }

        private void frmReporteGerencia_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);
        }

        private void ddlServiceTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedValue.ToString() == "-1")
            {
                ddlMasterServiceId.SelectedValue = "-1";
                ddlMasterServiceId.Enabled = false;
                return;
            }

            ddlMasterServiceId.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, int.Parse(ddlServiceTypeId.SelectedValue.ToString()), Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
          
        }

        private void ddlMasterServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMasterServiceId.SelectedValue == null)
                return;

            if (ddlMasterServiceId.SelectedValue.ToString() == "-1")
            {
                ddlEsoType.SelectedValue = "-1";
                ddlEsoType.Enabled = false;
                return;
            }

            OperationResult objOperationResult = new OperationResult();
            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)Common.MasterService.Eso).ToString() ||
                ddlMasterServiceId.SelectedValue.ToString() == "12")
            {

                ddlEsoType.Enabled = true;
            }
            else
            {
                ddlEsoType.SelectedValue = "-1";
                ddlEsoType.Enabled = false;
            } 
        }

        string strFilterExpression;
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cboTipoEmpresa.Text == "--Seleccionar--")
            {
                MessageBox.Show("Debe Seleccionar Tipo de Empresa", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Get the filters from the UI
            List<string> Filters = new List<string>();
       
            if (ddlMasterServiceId.SelectedValue.ToString() != "-1") Filters.Add("i_MasterServiceId==" + ddlMasterServiceId.SelectedValue);
            if (ddlServiceTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceTypeId==" + ddlServiceTypeId.SelectedValue);
            if (ddlEsoType.SelectedValue.ToString() != "-1") Filters.Add("i_EsoTypeId==" + ddlEsoType.SelectedValue);
        
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

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);

            var rp = new Reports.crReporteGerencia();
            DataSet ds1 = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(objData);
            dt.TableName = "dtReporteGerencia";
            ds1.Tables.Add(dt);          

            rp.SetDataSource(ds1);

            crvGerencia.ReportSource = rp;
            crvGerencia.Show();
        }

        private List<ReporteGerencia> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
            var modo = "";
            if (rbCantidad.Checked) modo = "Cantidad";
            else modo = "Valorizado";

            var _objData = new ServiceBL().ReporteGerencia(ref objOperationResult, pstrFilterExpression, pdatBeginDate, pdatEndDate, modo, cboTipoEmpresa.Text);
                       
            return _objData;
        }
    }
}
