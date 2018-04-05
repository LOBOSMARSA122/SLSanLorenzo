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
    public partial class frmDiagnosticsByOfficeDetail : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        string strFilterExpression;
        string _IdEmpresaClienete;

        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        List<string> _componentIds;

        public frmDiagnosticsByOfficeDetail()
        {
            InitializeComponent();
        }

        private void LoadComboboxes()
        {
            ddlComponentId.SelectedValueChanged -= ddlComponentId_SelectedValueChanged;

            OperationResult objOperationResult = new OperationResult();

            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));

            Utils.LoadDropDownList(ddlComponentId, "Value1", "Id", groupComponentList, DropDownListAction.All);

        
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);
            if (Globals.ClientSession.i_SystemUserId == 31)
            {
                ddlCustomerOrganization.Enabled = false;
                ddlCustomerOrganization.SelectedValue = "N009-OO000000108|N009-OL000000115";
            }

            ddlComponentId.SelectedValueChanged += ddlComponentId_SelectedValueChanged;

          
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

                //if (ddlComponentId.SelectedValue.ToString() != "-1")
                //    Filters.Add("v_ComponentId==" + "\"" + ddlComponentId.SelectedValue + "\"");

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

        private void frmDiagnosticsByOfficeDetail_Load(object sender, EventArgs e)
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

            LoadComboboxes();
        }

        private void ShowReport(DateTime? beginDate, DateTime? endDate)
        {

            List<TrabajadoresConcatenados> oListaTrabajadoresConcatenados = new List<TrabajadoresConcatenados>();
            TrabajadoresConcatenados oTrabajadoresConcatenados = new TrabajadoresConcatenados();

            // Mostrar reporte
            var Cabecera = _serviceBL.CabeceraReporte(_IdEmpresaClienete);
            var dataList = _serviceBL.ReportDiagnosticsByOfficeDetail(beginDate, endDate, strFilterExpression, _componentIds != null ? _componentIds.ToArray() : null);

            var Diagnosticos = _serviceBL.ReportDiagnosticsByOfficeDetailDetallado(beginDate, endDate, strFilterExpression, _componentIds != null ? _componentIds.ToArray() : null);

            if (dataList.Count != 0)
            {
                var ListaTrabajadores = Diagnosticos.FindAll(p => p.v_DiseasesName == dataList[0].v_DiseasesName);
                var ConcatTrabajadores = string.Join("/ ", ListaTrabajadores.Select(p => p.Trabajador));

                oTrabajadoresConcatenados.Dx = dataList[0].v_DiseasesName;
                oTrabajadoresConcatenados.CantidadTrabajadores = dataList[0].NroHallazgos.ToString();
                oTrabajadoresConcatenados.Trabajadores = ConcatTrabajadores;
                oListaTrabajadoresConcatenados.Add(oTrabajadoresConcatenados);
            }

            var rp = new Reports.crDiagnosticsByOfficeDetail();
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataList);
            dt.TableName = "dtDiagnosticsByOffice";
            ds1.Tables.Add(dt);

            DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Cabecera);
            dt1.TableName = "dtDiagnosticsByOfficeCabecera";
            ds1.Tables.Add(dt1);

            DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(oListaTrabajadoresConcatenados);
            dt2.TableName = "dtDetalle";
            ds1.Tables.Add(dt2);
           

            rp.SetDataSource(ds1);

            crvDiagnosticsByOfficeDetail.ReportSource = rp;
            crvDiagnosticsByOfficeDetail.Show();
        }

        private void ddlComponentId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlComponentId.SelectedIndex == 0) // Todos 
            {
                _componentIds = null;
                return;
            }
          
            _componentIds = new List<string>();

            var eee = (KeyValueDTO)ddlComponentId.SelectedItem;

            if (eee.Value4.ToString() == "-1")
            {
                _componentIds.Add(eee.Value2);
            }
            else
            {
                _componentIds = _componentListTemp.FindAll(p => p.Value4 == eee.Value4)
                                                           .Select(s => s.Value2)
                                                           .OrderBy(p => p).ToList();
            }

        }
      
    }
}
