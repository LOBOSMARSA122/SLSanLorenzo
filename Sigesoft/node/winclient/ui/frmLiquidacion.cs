using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Sigesoft.Node.Contasol.Integration;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLiquidacion : Form
    {
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();
        private OperationResult _objOperationResult = new OperationResult();
        private PacientBL _pacientBL = new PacientBL();
        private int seleccionado = -1;
        private int factura = -1;
        private string empresaId = null;
        public frmLiquidacion()
        {
            InitializeComponent();
        }

        private void frmLiquidacion_Load(object sender, EventArgs e)
        {
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            OperationResult objOperationResult1 = new OperationResult();
            OperationResult objOperationResult = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var clientOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var clientOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);

            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
            Utils.LoadDropDownList(ddlEmployerOrganization, "Value1", "Id", clientOrganization1, DropDownListAction.All);
            Utils.LoadDropDownList(cbbSubContratas, "Value1", "Id", clientOrganization2, DropDownListAction.All);

            Utils.LoadDropDownList(cbbEstadoLiq, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 327, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbbFac, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 328, null), DropDownListAction.Select);
            //
            btnLiqd1.Enabled = false;
            btnCarta.Enabled = false;
            btnRepEmp.Enabled = false;
            btnEliminarLiquidacion.Enabled = false;
            //
            UltraGridColumn c = grdData.DisplayLayout.Bands[1].Columns["b_Seleccionar"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtCCosto.Text)) Filters.Add("CCosto.Contains(\"" + txtCCosto.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtNroLiquidacion.Text)) Filters.Add("v_NroLiquidacion.Contains(\"" + txtNroLiquidacion.Text.Trim() + "\")");
    
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

            if (cbbSubContratas.SelectedValue.ToString() != "-1")
            {
                var id3 = cbbSubContratas.SelectedValue.ToString().Split('|');
                Filters.Add("v_WorkingOrganizationId ==" + "\"" + id3[0] + "\"&&v_WorkingLocationId ==" + "\"" + id3[1] + "\"");
            }

            //if (cbbEstadoLiq.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceStatusId==" + cbbEstadoLiq.SelectedValue);

            int seleccionado = cbbEstadoLiq.SelectedIndex;

            btnLiqd1.Enabled = false;
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

            var selectedTab = tabControl1.SelectedTab.Name;

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                if (tabControl1.SelectedTab.Name == "tpESO")
                {
                    BindGrid();
                    btnLiqd1.Enabled = false;
                    btnRepEmp.Enabled = true;
                }
                else if (tabControl1.SelectedTab.Name == "tpEmpresa")
                {
                    BindGridEmpresa();
                    btnLiqd1.Enabled = false;
                    btnRepEmp.Enabled = true;
                }
            };
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdData.Rows.Count > 0)
            {
                grdData.Rows[0].Selected = true;
                btnExportarExcel.Enabled = true;
            }

        }

        private void BindGridEmpresa()
        {
            var objData = GetDataEmpresa(0, null, "", strFilterExpression);
            grdEmpresa.DataSource = objData;

            //lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdEmpresa.Rows.Count > 0)
            {
                txtDebe.Text = objData[0].Total_Debe;
                txtPago.Text = objData[0].Total_Pago;
                txtTotal.Text = objData[0].Total_Total;
                grdEmpresa.Rows[0].Selected = true;
                //btnExportarExcel.Enabled = true;
            }

        }

        private List<LiquidacionEmpresa> GetDataEmpresa(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = _serviceBL.ListaLiquidacionByEmpresa(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }



        private List<Liquidacion> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
            seleccionado = cbbEstadoLiq.SelectedIndex;
            factura = cbbFac.SelectedIndex;
            var _objData = _serviceBL.ListaLiquidacion(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, seleccionado, factura);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }


        private void btnEditarServicio_Click(object sender, EventArgs e)
        {
            var band = this.grdData.DisplayLayout.Bands[1];

            var ids = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                       where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                       select row).ToList().Select(p => p.Cells["v_ServiceId"].Value.ToString()).ToArray();

            var idProtocolId = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                       where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                       select row).ToList().Select(p => p.Cells["v_ProtocolId"].Value.ToString()).ToArray().FirstOrDefault();

            var personId = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                                where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                                select row).ToList().Select(p => p.Cells["v_PersonId"].Value.ToString()).ToArray().FirstOrDefault();


            if (ids.Length > 1)
            {
                MessageBox.Show("Solo puede seleccionar un registro a la vez", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ids.Length == 0 )
            {
                MessageBox.Show("Seleccione un registro", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var frm = new frmEditarServicio(ids[0], idProtocolId, personId);
            frm.ShowDialog();
            
            


        }

        private void btnGenerarLiq_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdData.DisplayLayout.Bands[1];

            var ids = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                                               where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                                               select row).ToList().Select(p => p.Cells["v_ServiceId"].Value.ToString()).ToArray();

            if (ids.Length == 0)
            {
                MessageBox.Show("Seleccionar Registros", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            var frm = new frmSelectOrganization(ids, Globals.ClientSession.GetAsList());
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void btnLiberarRegistro_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grdData.DisplayLayout.Bands[1];

            var ids = (from row in band.GetRowEnumerator(GridRowType.DataRow).Cast<UltraGridRow>()
                       where Convert.ToBoolean(row.Cells["b_Seleccionar"].Value) == true
                       select row).ToList().Select(p => p.Cells["v_ServiceId"].Value.ToString()).ToArray();

            if (ids.Length == 0)
            {
                MessageBox.Show("Seleccionar Registros", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            };

            foreach (var id in ids)
            {
                _serviceBL.GenerarLiberar(id, Globals.ClientSession.GetAsList());
            }

            MessageBox.Show("Actualizado", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnFilter_Click(sender, e);
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }     
        }

        private void grdData_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;
                }
                else
                {
                    e.Cell.Value = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tpESO")
            {
                var liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                var serviceID = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                var protocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacionID);
                    
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Liquidación N° " + liquidacionID + " - CSL";

                    string idLiq = "";
                    foreach (var item in lista)
                    {
                        foreach (var item_1 in item.Detalle)
                        {
                            idLiq = item_1.v_NroLiquidacion;
                            break;
                        }
                        break;
                    }
                    var traerEmpresa = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, idLiq);
                    string idEmpresa = traerEmpresa.v_OrganizationId;
                    var obtenerInformacionEmpresas = new ServiceBL().GetOrganizationId(ref _objOperationResult, idEmpresa);
                    Liquidacion_EMO.CreateLiquidacion_EMO(ruta + nombre + ".pdf", MedicalCenter, lista, obtenerInformacionEmpresas);
                    this.Enabled = true;
                }
            }
            else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            {
                var liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacionID);

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Liquidación N° " + liquidacionID + " - CSL";

                    var traerEmpresa = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, liquidacionID);
                    string idEmpresa = traerEmpresa.v_OrganizationId;
                    var obtenerInformacionEmpresas = new ServiceBL().GetOrganizationId(ref _objOperationResult, idEmpresa);
                    Liquidacion_EMO.CreateLiquidacion_EMO(ruta + nombre + ".pdf", MedicalCenter, lista, obtenerInformacionEmpresas);
                    this.Enabled = true;
                }
            }
            
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdData.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    btnLiqd1.Enabled = false;
                    btnCarta.Enabled = false;
                    btnEliminarLiquidacion.Enabled = false;
                    btnRepEmp.Enabled = true;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    if (grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value == null) return;
                    var liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                    if (liquidacionID == null || liquidacionID =="")
                    {
                        btnLiqd1.Enabled = false;
                        btnCarta.Enabled = false;
                        btnEliminarLiquidacion.Enabled = false;
                        btnRepEmp.Enabled = true;
                    }
                    else
                    {
                        btnLiqd1.Enabled = true;
                        btnCarta.Enabled = true;
                        btnEliminarLiquidacion.Enabled = false;
                        btnRepEmp.Enabled = true;
                    }
                }
                else
                {
                    btnLiqd1.Enabled = false;
                    btnCarta.Enabled = false;
                    btnRepEmp.Enabled = true;
                    btnEliminarLiquidacion.Enabled = false;
                }

            }
            
        }

        private void ddlCustomerOrganization_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null,null);
            }
        }

        private void ddlEmployerOrganization_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void cbbSubContratas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtNroLiquidacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtCCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dtpDateTimeStar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void dptDateTimeEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void btnCarta_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tpESO")
            {
                var liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                var serviceID = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                var protocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacionID);

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Carta N° " + liquidacionID + " - CSL";


                    int systemUser = 205;
                    var datosGrabo = _serviceBL.DevolverDatosUsuarioFirma(systemUser);

                    string idLiq = "";
                    foreach (var item in lista)
                    {
                        foreach (var item_1 in item.Detalle)
                        {
                            idLiq = item_1.v_NroLiquidacion;
                            break;
                        }
                        break;
                    }
                    var liquidacion = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, idLiq);
                    string N_Fact = liquidacion.v_NroFactura;
                    var empresa = new ServiceBL().GetOrganizationId(ref _objOperationResult, liquidacion.v_OrganizationId);
                    Liquidacion_Carta.CreateLiquidacion_Carta(ruta + nombre + ".pdf", MedicalCenter, lista, empresa, datosGrabo, N_Fact);
                    this.Enabled = true;
                }
            }
            else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            {
                var liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacionID);

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Carta N° " + liquidacionID + " - CSL";


                    int systemUser = 205;
                    var datosGrabo = _serviceBL.DevolverDatosUsuarioFirma(systemUser);

                    var liquidacion = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, liquidacionID);
                    string N_Fact = liquidacion.v_NroFactura;
                    var empresa = new ServiceBL().GetOrganizationId(ref _objOperationResult, liquidacion.v_OrganizationId);
                    Liquidacion_Carta.CreateLiquidacion_Carta(ruta + nombre + ".pdf", MedicalCenter, lista, empresa, datosGrabo, N_Fact);
                    this.Enabled = true;
                }
            }
        }

        private void grdEmpresa_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdEmpresa.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    
                    btnLiqd1.Enabled = false;
                    btnCarta.Enabled = false;
                    btnRepEmp.Enabled = true;
                    btnEliminarLiquidacion.Enabled = false;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    if (grdEmpresa.Selected.Rows.Count == 0) return;
                    //if (grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value == null) return;
                    var liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                    
                    if (liquidacionID == null || liquidacionID == "")
                    {
                        btnLiqd1.Enabled = false;
                        btnCarta.Enabled = false;
                        btnRepEmp.Enabled = true;
                        btnEliminarLiquidacion.Enabled = false;
                    }
                    else
                    {
                        var traeLiqu = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, liquidacionID);
                        if (traeLiqu.v_NroFactura == null || traeLiqu.v_NroFactura == "")
                        {
                            btnEliminarLiquidacion.Enabled = true;
                        }
                        else
                        {
                            btnEliminarLiquidacion.Enabled = false;
                        }
                        btnLiqd1.Enabled = true;
                        btnCarta.Enabled = true;
                        btnRepEmp.Enabled = true;
                        
                    }
                }
                else
                {
                    btnLiqd1.Enabled = false;
                    btnCarta.Enabled = false;
                    btnRepEmp.Enabled = false;
                    btnEliminarLiquidacion.Enabled = false;
                }
            }
        }

        private void grdEmpresa_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
          
        }

        private void btnExportclinico_Click(object sender, EventArgs e)
        {

        }

        private void btnExportAramark_Click(object sender, EventArgs e)
        {

        }

        private void brnRepEmp_Click(object sender, EventArgs e)
        {
            //var hospitId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            empresaId = grdEmpresa.Selected.Rows[0].Cells["v_OrganizationName"].Value.ToString();

            frmLiquidacionEmpresas reportsEmpresas = new frmLiquidacionEmpresas(dtpDateTimeStar.Value.Date, dptDateTimeEnd.Value.Date, empresaId);
            reportsEmpresas.ShowDialog();

            
        }

        private void cbbEstadoLiq_SelectedIndexChanged(object sender, EventArgs e)
        {
            seleccionado = cbbEstadoLiq.SelectedIndex;
            if (seleccionado == 2)
            {
                cbbFac.Enabled = false;
            }
            else
            {
                cbbFac.Enabled = true;
            }
            
        }

        private void cbbFac_SelectedIndexChanged(object sender, EventArgs e)
        {
            factura = cbbFac.SelectedIndex;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tpESO")
            {
                txtCCosto.Text="";
                txtCCosto.Enabled = true;
                ddlCustomerOrganization.Enabled = true;
                ddlEmployerOrganization.Enabled = true;
                cbbSubContratas.Enabled = true;
                cbbEstadoLiq.Enabled = true;
                cbbFac.Enabled = true;
            }
            else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            {
                txtCCosto.Text = "";
                ddlCustomerOrganization.SelectedValue = "-1";
                ddlEmployerOrganization.SelectedValue = "-1";
                cbbSubContratas.SelectedValue = "-1";
                cbbEstadoLiq.SelectedValue = "-1";
                cbbFac.SelectedValue = "-1";

                txtCCosto.Enabled = false;
                ddlCustomerOrganization.Enabled = false;
                ddlEmployerOrganization.Enabled = false;
                cbbSubContratas.Enabled = false;
                cbbEstadoLiq.Enabled = false;
                cbbFac.Enabled = false;
            }
        }

        private void btnEliminarLiquidacion_Click(object sender, EventArgs e)
        {
            var liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();

            var liquidaciones = new ServiceBL().GetListaLiquidacion(ref _objOperationResult, liquidacionID);
            int n = 0;
            foreach (var item in liquidaciones)
            {
                n = item.Detalle.Count();
                break;
            }
            

            DialogResult Result = MessageBox.Show("¿Desea eliminar liquidación N° " + liquidacionID + "?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                if (n > 0)
                {
                    MessageBox.Show("NO SE PUEDE BORRAR REGISTRO POR QUE SE ENCUENTRA ENLAZADO A UNO O VARIOS SERVICIOS.", "¡ATENCIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var traeLiqu = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, liquidacionID);

                    traeLiqu.v_LiquidacionId = liquidacionID;
                    traeLiqu.i_IsDeleted = 1;
                    new ServiceBL().UpdateLiquidacion(ref _objOperationResult, traeLiqu, Globals.ClientSession.GetAsList());
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
