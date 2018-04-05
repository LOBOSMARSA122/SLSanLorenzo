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
    public partial class frmBuscarServicioPendiente : Form
    {
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();
        List<ServiceList> _ListaServiceList = new List<ServiceList>();
        public  List<FacturacionDetalleList> _ListaFacturacionList = new List<FacturacionDetalleList>();
        public DateTime? _FechaInicio;
        public DateTime? _FechaFin;
        string _EmpresaId;
        string _SedeId;

        public frmBuscarServicioPendiente(string pstrEmpresaId, string pstrSedeId)
        {
            InitializeComponent();
            _EmpresaId = pstrEmpresaId;
            _SedeId = pstrSedeId;
        }

        private void frmBuscarServicioPendiente_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            UltraGridColumn c = grdDataService.DisplayLayout.Bands[0].Columns["Seleccion"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            Utils.LoadDropDownList(cboTipoESO, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

            Utils.LoadDropDownList(cboGESO, "Value1", "Id", BLL.Utils.GetGESOByOrgIdAndLocationId(ref objOperationResult, _EmpresaId, _SedeId), DropDownListAction.All);
     
            //Utils.LoadDropDownList(cboSede, "Value1", "Id", BLL.Utils.ListLocationForCombo(_EmpresaId), DropDownListAction.All);

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (cboTipoESO.SelectedValue.ToString() != "-1") Filters.Add("i_EsoTypeId==" + cboTipoESO.SelectedValue);
            if (cboGESO.SelectedValue.ToString() != "-1") Filters.Add("v_GroupOccupationId==\"" + cboGESO.SelectedValue + "\"");
            Filters.Add("v_CustomerOrganizationId==" + "\"" + _EmpresaId + "\"&&v_CustomerLocationId==" + "\"" + _SedeId + "\"");
            Filters.Add("i_IsFac==0");

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
            }
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);

            grdDataService.DataSource = objData;
            _ListaServiceList = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdDataService.Rows.Count > 0)
                grdDataService.Rows[0].Selected = true;
        }

        private List<ServiceList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            _FechaInicio = pdatBeginDate;
            _FechaFin = pdatEndDate;
            var _objData = _serviceBL.BuscarServicionSinFacturarYCulmindados(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, null);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnAgregarFacturacion_Click(object sender, EventArgs e)
        {

            FacturacionDetalleList oFacturacionDetalleList;
        

            foreach (var item in grdDataService.Rows)
            {


                if ((bool)item.Cells["Seleccion"].Value)
                {
                    oFacturacionDetalleList = new FacturacionDetalleList();
                    oFacturacionDetalleList.v_ServicioId = item.Cells["v_ServiceId"].Value.ToString();
                    oFacturacionDetalleList.d_ServiceDate = DateTime.Parse(item.Cells["d_ServiceDate"].Value.ToString());
                    oFacturacionDetalleList.Trabajador = item.Cells["v_Pacient"].Value.ToString();
                    oFacturacionDetalleList.v_ProtocolId = item.Cells["v_ProtocolId"].Value.ToString();
                    oFacturacionDetalleList.Perfil = item.Cells["v_EsoTypeName"].Value.ToString();
                    oFacturacionDetalleList.TipoExamen = item.Cells["v_TipoExamen"].Value.ToString();
                    var valor = new ServiceBL().GetServiceCostfloat(item.Cells["v_ServiceId"].Value.ToString());

                    oFacturacionDetalleList.d_Monto = decimal.Parse(valor.ToString());
                    oFacturacionDetalleList.Igv = oFacturacionDetalleList.d_Monto * 18/100;
                    oFacturacionDetalleList.Total = oFacturacionDetalleList.d_Monto + oFacturacionDetalleList.Igv;


                    _ListaFacturacionList.Add(oFacturacionDetalleList);
                
                    
                }
             
            }

            this.DialogResult = DialogResult.OK;

        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "Seleccion"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;

                    //btnAgregarFacturacion.Enabled = true;
                }
                else
                {
                    e.Cell.Value = false;
                    //btnAgregarFacturacion.Enabled = false;
                }

            }
        }
    }
}
