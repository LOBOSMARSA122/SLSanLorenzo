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
    public partial class frmDiagnosticsByGroupOccupation : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        string strFilterExpression;
        string _IdEmpresaClienete;

        public frmDiagnosticsByGroupOccupation()
        {
            InitializeComponent();
        }

        private void frmDiagnosticsByGroupOccupation_Load(object sender, EventArgs e)
        {
            //#region Simular sesion
            //ClientSession objClientSession = new ClientSession();
            //objClientSession.i_SystemUserId = 1;
            //objClientSession.v_UserName = "sa";
            //objClientSession.i_CurrentExecutionNodeId = 9;
            //objClientSession.v_CurrentExecutionNodeName = "SALUS";
            ////_ClientSession.i_CurrentOrganizationId = 57;
            //objClientSession.v_PersonId = "N000-P0000000001";

            //// Pasar el objeto de sesión al gestor de objetos globales
            //Globals.ClientSession = objClientSession;
            //#endregion

            cbTop.SelectedIndex = 5;

            OperationResult objOperationResult = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
            if (Globals.ClientSession.i_SystemUserId == 31)
            {
                ddlCustomerOrganization.Enabled = false;
                ddlCustomerOrganization.SelectedValue = "N009-OO000000108|N009-OL000000115";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (uvReporte.Validate(true, false).IsValid)
            {
                List<string> Filters = new List<string>();
                DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                    _IdEmpresaClienete = id3[0];
                }

                if (ddlProtocolId.SelectedValue.ToString() != "-1")
                    Filters.Add("IdProtocolId==" + "\"" + ddlProtocolId.SelectedValue + "\"");

                if (cbGeso.SelectedValue.ToString() != "-1")
                    Filters.Add("v_GroupOccupationId==" + "\"" + cbGeso.SelectedValue.ToString() + "\"");


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

                ShowReport(pdatBeginDate, pdatEndDate);
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ShowReport(DateTime? beginDate, DateTime? endDate)
        {
            List<TrabajadoresConcatenados> oListaTrabajadoresConcatenados = new List<TrabajadoresConcatenados>();
            TrabajadoresConcatenados oTrabajadoresConcatenados = new TrabajadoresConcatenados();

            // Mostrar reporte
            var Cabecera = _serviceBL.CabeceraReporte(_IdEmpresaClienete);
            var dataList = _serviceBL.ReportDiagnosticsByGroupOccupation(beginDate, endDate, strFilterExpression, int.Parse(cbTop.Text));

            var Diagnosticos = _serviceBL.ReportDiagnosticsByGroupOccupationDetallado(beginDate, endDate, strFilterExpression, int.Parse(cbTop.Text));


            var ListaTrabajadores = Diagnosticos.FindAll(p => p.v_DiseasesName == dataList[0].v_DiseasesName);
            var ConcatTrabajadores = string.Join("/ ", ListaTrabajadores.Select(p => p.Trabajador));

            oTrabajadoresConcatenados.Dx = dataList.Count() == 0 ? "" : dataList[0].v_DiseasesName;
            oTrabajadoresConcatenados.CantidadTrabajadores = dataList.Count() == 0 ? "" : dataList[0].NroHallazgos.ToString();
            oTrabajadoresConcatenados.Trabajadores = ConcatTrabajadores;
            oListaTrabajadoresConcatenados.Add(oTrabajadoresConcatenados);

            var rp = new Reports.crDiagnosticsByGroupOccupation();
            DataSet ds1 = new DataSet();



            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataList);
            dt.TableName = "dtDiagnosticsByGroupOccupation";
            ds1.Tables.Add(dt);

            DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Cabecera);
            dt1.TableName = "dtDiagnosticsByGroupOccupationCabecera";
            ds1.Tables.Add(dt1);

            DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(oListaTrabajadoresConcatenados);
            dt2.TableName = "dtDetalle";
            ds1.Tables.Add(dt2);

            rp.SetDataSource(ds1);

            crvDiagnosticsByGroupOccupation.ReportSource = rp;
            crvDiagnosticsByGroupOccupation.Show();
        }

        private void LoadcbGESO()
        {
            var index = ddlCustomerOrganization.SelectedIndex;

            if (index == 0 || index == -1)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
                return;
            }

            var dataList = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];
            string idLoc = dataList[1];

            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESOByOrgIdAndLocationId(ref objOperationResult1, idOrg, idLoc), DropDownListAction.Select);
        }

        private void ddlCustomerOrganization_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                cbGeso.Enabled = false;
                return;
            }

            ddlProtocolId.Enabled = true;
            cbGeso.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.Select);

            LoadcbGESO();
        }

        
    }
}
