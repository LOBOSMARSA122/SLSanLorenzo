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

using System.Threading;
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

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
        string liquidacionID = null;
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
            Utils.LoadDropDownList(cboFacturacion, "Value1", "Id", BLL.Utils.GetOrganizationFacturacion(), DropDownListAction.Select);
            //
            btnLiqd1.Enabled = false;
            btnCarta.Enabled = false;
            btnRepEmp.Enabled = false;
            btnExportarExcel.Enabled = false;
            btnEliminarLiquidacion.Enabled = false;
            btnEditarServicio.Enabled = false;
            btnGenerarLiq.Enabled = false;
            btnLiberarRegistro.Enabled = false;
            btnNoLiquidados.Enabled = false;
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

            if (cboFacturacion.SelectedValue.ToString() != "-1")
            {
                var organizationId = cboFacturacion.SelectedValue.ToString();
                Filters.Add("v_OrganizationId==" + "\"" + organizationId +"\" ");
            }

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
            
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                if (tabControl1.SelectedTab.Name == "tpESO")
                {
                    BindGrid();
                    btnLiqd1.Enabled = false;
                    btnRepEmp.Enabled = true;

                    btnEditarServicio.Enabled = true;
                    btnGenerarLiq.Enabled = true;
                    btnLiberarRegistro.Enabled = true;
                    btnNoLiquidados.Enabled = true;
                }
                else if (tabControl1.SelectedTab.Name == "tpEmpresa")
                {
                    BindGridEmpresa();
                    btnLiqd1.Enabled = false;
                    btnRepEmp.Enabled = true;

                    btnEditarServicio.Enabled = false;
                    btnGenerarLiq.Enabled = false;
                    btnLiberarRegistro.Enabled = false;
                    btnNoLiquidados.Enabled = false;
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

                    var traerEmpresa = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, liquidacionID);
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
                    btnExportarExcel.Enabled = false;
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
                        btnExportarExcel.Enabled = false;
                        btnCarta.Enabled = false;
                        btnEliminarLiquidacion.Enabled = false;
                        btnRepEmp.Enabled = true;
                    }
                    else
                    {
                        btnLiqd1.Enabled = true;
                        btnCarta.Enabled = true;
                        btnExportarExcel.Enabled = true;
                        
                        btnEliminarLiquidacion.Enabled = false;
                        btnRepEmp.Enabled = true;

                        liquidacionID = liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                    }
                }
                else
                {
                    btnLiqd1.Enabled = false;
                    btnExportarExcel.Enabled = false;
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
                    btnExportarExcel.Enabled = false;
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
                        btnExportarExcel.Enabled = false;
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
                        btnExportarExcel.Enabled = true;
                        btnCarta.Enabled = true;
                        btnRepEmp.Enabled = true;

                        liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                        
                    }
                }
                else
                {
                    btnLiqd1.Enabled = false;
                    btnCarta.Enabled = false;
                    btnRepEmp.Enabled = false;
                    btnExportarExcel.Enabled = false;
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
            if (tabControl1.SelectedTab.Name == "tpESO")
            {
                saveFileDialog1.FileName = string.Empty;
                saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                    MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            {
                saveFileDialog1.FileName = string.Empty;
                saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.ultraGridExcelExporter1.Export(this.grdEmpresa, saveFileDialog1.FileName);
                    MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void brnRepEmp_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tpESO")
            {
                frmLiquidacionEmpresas reportsEmpresas = new frmLiquidacionEmpresas(dtpDateTimeStar.Value.Date, dptDateTimeEnd.Value.Date, string.Empty);
                reportsEmpresas.ShowDialog();
            }
            else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            {
                empresaId = grdEmpresa.Selected.Rows[0].Cells["v_OrganizationName"].Value.ToString();

                frmLiquidacionEmpresas reportsEmpresas = new frmLiquidacionEmpresas(dtpDateTimeStar.Value.Date, dptDateTimeEnd.Value.Date, empresaId);
                reportsEmpresas.ShowDialog();
            }
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
                btnNoLiquidados.Enabled = true;
                cbbFac.Enabled = true;

                btnEditarServicio.Enabled = true;
                btnGenerarLiq.Enabled = true;
                btnLiberarRegistro.Enabled = true;
                btnNoLiquidados.Enabled = true;

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
                btnNoLiquidados.Enabled = false;
                ddlCustomerOrganization.Enabled = false;
                ddlEmployerOrganization.Enabled = false;
                cbbSubContratas.Enabled = false;
                cbbEstadoLiq.Enabled = false;
                cbbFac.Enabled = false;

                btnEditarServicio.Enabled = false;
                btnGenerarLiq.Enabled = false;
                btnLiberarRegistro.Enabled = false;
                btnNoLiquidados.Enabled = false;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                OperationResult objOperationResult = new OperationResult();

                //DateTime? inicioDeudas = _fInicio;
                //DateTime? FinDeudas = DateTime.Now;

                var NoLiquidados = new ServiceBL().NoLiquidados(ref objOperationResult, dtpDateTimeStar.Value, dptDateTimeEnd.Value);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "EMPRESAS SIN LIQUIDAR - CSL";

                string fechaInicio_2 = dtpDateTimeStar.Value.ToString().Split(' ')[0];
                string fechaFin_2 = dptDateTimeEnd.Value.ToString().Split(' ')[0];
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                EmpresasSinLiquidarSF.CreateEmpresasSinLiquidar(ruta + nombre + ".pdf", MedicalCenter, fechaInicio_2, fechaFin_2, NoLiquidados);
                this.Enabled = true;
            }

        }

        private void grdData_AfterSortChange(object sender, Infragistics.Win.UltraWinGrid.BandEventArgs e)
        {

        }
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {

            try
            {
                btnExportarExcel.Enabled = false;

                if (tabControl1.SelectedTab.Name == "tpESO")
                {
                    liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                    creaExcel(sender, e, liquidacionID);
                }
                else if (tabControl1.SelectedTab.Name == "tpEmpresa")
                {
                    liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                    creaExcel(sender, e, liquidacionID);
                }
                btnExportarExcel.Enabled = true;
            }
            catch
            {

            }
        }

        public void creaExcel(object sender, EventArgs e, string liquidacion)
        {
            OperationResult objOperationResult = new OperationResult();
            
            //string liquidacionID = null;
            //string serviceID;
            //string protocolId;
            //if (tabControl1.SelectedTab.Name == "tpESO")
            //{
            //               
            //}
            //else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            //{
            //    
            //}
            string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

            var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacion);

            BackgroundWorker bw = sender as BackgroundWorker;

            Excel.Application excel = new Excel.Application();
            Excel._Workbook libro = null;
            Excel._Worksheet hoja = null;
            Excel.Range rango = null;

            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    //creamos un libro nuevo y la hoja con la que vamos a trabajar
                    libro = (Excel._Workbook)excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);

                    hoja = (Excel._Worksheet)libro.Worksheets.Add();
                    hoja.Application.ActiveWindow.DisplayGridlines = false;
                    hoja.Name = "LIQUIDACION N° " + liquidacionID;
                    ((Excel.Worksheet)excel.ActiveWorkbook.Sheets["Hoja1"]).Delete();   //Borro hoja que crea en el libro por defecto

                    //DatosEmpresa
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range("B2", "D5");
                    rango.Select();
                    rango.RowHeight = 25;
                    hoja.get_Range("B2", "D5").Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        200,
                        90);

                    montaCabeceras(3, ref hoja, liquidacion);

                    //DatosDinamicos
                    int fila = 11;
                    int count = 1;
                    int i = 0;
                    decimal sumatipoExm = 0;
                    decimal sumatipoExm_1 = 0;
                    decimal igvPerson = 0;
                    decimal _igvPerson = 0;
                    decimal subTotalPerson = 0;
                    decimal _subTotalPerson = 0;
                    decimal totalFinal = 0;
                    decimal totalFinal_1 = 0;
                    foreach (var lista1 in lista)
                    {
                        //Asignamos los datos a las celdas de la fila
                        hoja.Cells[fila + i, 2] = "TIPO EXAMEN: " + lista1.Esotype;
                        string x1 = "B" + (fila + i).ToString();
                        string y1 = "L" + (fila + i).ToString();
                        rango = hoja.Range[x1, y1];
                        rango.Merge(true);
                        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        rango.Interior.Color = Color.Gray;
                        rango.Font.Size = 14;
                        rango.RowHeight = 30;
                        rango.Font.Bold = true;
                        i++;

                        hoja.Cells[fila + i, 2] = "N°";
                        hoja.Cells[fila + i, 3] = "PACIENTE ";
                        hoja.Cells[fila + i, 4] = "EDAD ";
                        hoja.Cells[fila + i, 5] = "F. EXAMEN ";
                        hoja.Cells[fila + i, 6] = "DNI ";
                        hoja.Cells[fila + i, 7] = "CARGO ";
                        hoja.Cells[fila + i, 8] = "PERFIL ";
                        hoja.Cells[fila + i, 9] = "IGV ";
                        hoja.Cells[fila + i, 10] = "SUB TOTAL ";
                        hoja.Cells[fila + i, 11] = "TOTAL ";
                        hoja.Cells[fila + i, 12] = "REF./OBSE. ";
                        string x2 = "B" + (fila + i).ToString();
                        string y2 = "L" + (fila + i).ToString();
                        rango = hoja.Range[x2, y2];
                        rango.Borders.LineStyle = Excel.XlLineStyle.xlDash;
                        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        rango.RowHeight = 25;
                        rango.Font.Bold = true;
                        i++;
                        foreach (var item in lista1.Detalle)
                        {
                            hoja.Cells[fila + i, 2] = count + ".";
                            hoja.Cells[fila + i, 3] = item.Trabajador;
                            hoja.Cells[fila + i, 4] = item.Edad;
                            DateTime fecha = item.FechaExamen.Value.Date;
                            hoja.Cells[fila + i, 5] = fecha;
                            hoja.Cells[fila + i, 6] = item.NroDocumemto;
                            hoja.Cells[fila + i, 7] = item.Cargo;
                            hoja.Cells[fila + i, 8] = item.Perfil;
                            decimal _SubTotal = (decimal)item.Precio / (decimal)1.18;
                            _SubTotal = _SubTotal + (decimal)0.0000000000000000000000000000001;
                            _SubTotal = decimal.Round(_SubTotal, 2);
                            decimal _igv = _SubTotal * (decimal)0.18;
                            _igv = _igv + (decimal)0.00000000000000000000000000001;
                            _igv = decimal.Round(_igv, 2);
                            hoja.Cells[fila + i, 9] = _igv;
                            hoja.Cells[fila + i, 10] = _SubTotal;
                            decimal Precio = (decimal)item.Precio;
                            Precio = Precio + (decimal)0.0000000000000000000001;
                            Precio = decimal.Round(Precio, 2);
                            string[] _Pcadena = Precio.ToString().Split('.');
                            if (_Pcadena.Count() > 1)
                            {
                                hoja.Cells[fila + i, 11] = Precio;
                            }
                            else
                            {
                                hoja.Cells[fila + i, 11] = Precio.ToString() + ".00";
                            }
                            hoja.Cells[fila + i, 12] = item.CCosto;
                            string x_1 = "B" + (fila + i).ToString();
                            string y_1 = "H" + (fila + i).ToString();
                            hoja.get_Range(x_1, y_1).RowHeight = 25;
                            count++;
                            sumatipoExm += (decimal)item.Precio;
                            igvPerson += (decimal)_igv;
                            subTotalPerson += (decimal)_SubTotal;
                            i++;
                        }
                        sumatipoExm_1 = decimal.Round(sumatipoExm, 2);
                        _igvPerson = decimal.Round(igvPerson, 2);
                        _subTotalPerson = decimal.Round(subTotalPerson, 2);

                        hoja.Cells[fila + i, 2] = "TOTAL EXAMEN: " + lista1.Esotype + " = ";
                        string x3 = "B" + (fila + i).ToString();
                        string y3 = "H" + (fila + i).ToString();
                        rango = hoja.Range[x3, y3];
                        rango.Merge(true);
                        rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        rango.Font.Bold = true;
                        rango.Font.Size = 14;
                        hoja.Cells[fila + i, 9] = _igvPerson;
                        hoja.Cells[fila + i, 10] = _subTotalPerson;
                        hoja.Cells[fila + i, 11] = sumatipoExm_1;

                        i++;

                        sumatipoExm = 0;
                        igvPerson = 0;
                        subTotalPerson = 0;
                        totalFinal += (decimal)sumatipoExm_1;

                    }

                    totalFinal_1 = decimal.Round(totalFinal, 2);
                    decimal subTotalFinal = decimal.Round(totalFinal_1 / (decimal)1.18, 2);
                    decimal IGV = decimal.Round(subTotalFinal * (decimal)0.18, 2);

                    hoja.Cells[fila + i, 2] = "SUB TOTAL = ";
                    string x4 = "B" + (fila + i).ToString();
                    string y4 = "H" + (fila + i).ToString();
                    rango = hoja.Range[x4, y4];
                    rango.Merge(true);
                    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    rango.Font.Bold = true;
                    rango.Font.Size = 13;
                    hoja.Cells[fila + i, 11] = subTotalFinal;

                    i++;
                    hoja.Cells[fila + i, 2] = "IGV = ";
                    string x5 = "B" + (fila + i).ToString();
                    string y5 = "H" + (fila + i).ToString();
                    rango = hoja.Range[x5, y5];
                    rango.Merge(true);
                    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    rango.Font.Bold = true;
                    rango.Font.Size = 13;
                    hoja.Cells[fila + i, 11] = IGV;

                    i++;
                    hoja.Cells[fila + i, 2] = "TOTAL LIQUIDACIÓN = ";
                    string x6 = "B" + (fila + i).ToString();
                    string y6 = "H" + (fila + i).ToString();
                    rango = hoja.Range[x6, y6];
                    rango.Merge(true);
                    rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    rango.Font.Bold = true;
                    rango.Font.Size = 13;
                    hoja.Cells[fila + i, 11] = totalFinal_1;

                    i += 5;

                    string x7 = "B" + (fila + i).ToString();
                    string y7 = "L" + (fila + i).ToString();
                    rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(x7, y7);
                    rango.Select();
                    hoja.get_Range(x7, y7).Merge(true);

                    Microsoft.Office.Interop.Excel.Pictures oPictures2 = (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

                    hoja.Shapes.AddPicture(@"C:\Program Files (x86)\NetMedical\Banner\banner2.jpg",
                        Microsoft.Office.Core.MsoTriState.msoFalse,
                        Microsoft.Office.Core.MsoTriState.msoCTrue,
                        float.Parse(rango.Left.ToString()),
                        float.Parse(rango.Top.ToString()),
                        float.Parse(rango.Width.ToString()),
                        80);

                    libro.Saved = true;

                    libro.SaveAs(ruta + @"\" + "Liquidacion N° " + liquidacion + ".xlsx");

                    //bw.WorkerReportsProgress = true;
                    //bw.ReportProgress(100, ti);

                    libro.Close();
                    releaseObject(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject(excel);
                }
                Process.Start(ruta + @"\" + "Liquidacion N° " + liquidacion + ".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en creación/actualización de la Liquidación N° " + liquidacionID, MessageBoxButtons.OK, MessageBoxIcon.Error);
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    libro.Saved = true;
                    libro.SaveAs(ruta + @"\" + "Liquidacion N° " + liquidacion + "_fail.xlsx");

                    libro.Close();
                    releaseObject(libro);

                    excel.UserControl = false;
                    excel.Quit();
                    releaseObject(excel);
                }
                Process.Start(ruta + @"\" + "Liquidacion N° " + liquidacion + "_fail.xlsx");
            }

        }

        private void montaCabeceras(int fila, ref Excel._Worksheet hoja, string _liquidacion)
        {

            //string liquidacionID = null;

            //if (tabControl1.SelectedTab.Name == "tpESO")
            //{
            //    liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
            //}
            //else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            //{
            //    liquidacionID = grdEmpresa.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
            //}
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var traerEmpresa = new ServiceBL().ListaLiquidacionById(ref _objOperationResult, _liquidacion);
            string idEmpresa = traerEmpresa.v_OrganizationId;
            var obtenerInformacionEmpresas = new ServiceBL().GetOrganizationId(ref _objOperationResult, idEmpresa);
            try
            {
                Excel.Range rango;


                //** TITULO DEL LIBRO **
                ////hoja.Cells[1, 2] = MedicalCenter.b_Image;
                //hoja.get_Range("B1", "C1");

                hoja.Cells[6, 4] = "LIQUIDACIÓN DE EXAMENES MÉDICOS OCUPACIONALES N° " + liquidacionID;
                hoja.get_Range("B6", "L6").Merge(true);
                hoja.get_Range("B6", "L6").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("B6", "L6").Font.Bold = true;
                hoja.get_Range("B6", "L6").Font.Size = 18;
                hoja.get_Range("B6", "L6").RowHeight = 35;
                hoja.get_Range("B6", "L6").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[8, 2] = "EMPRESA A FACTURAR: ";
                hoja.Cells[8, 4] = obtenerInformacionEmpresas.v_Name;
                hoja.get_Range("B8", "C8").Merge(true);
                hoja.get_Range("D8", "L8").Merge(true);
                hoja.get_Range("B8", "C8").Font.Bold = true;
                hoja.get_Range("B8", "C8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
                hoja.get_Range("B8", "C8").RowHeight = 30;
                hoja.get_Range("D8", "L8").RowHeight = 30;
                hoja.get_Range("B8", "C8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("D8", "L8").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[9, 2] = "RUC: ";
                hoja.Cells[9, 4] = obtenerInformacionEmpresas.v_IdentificationNumber;
                hoja.get_Range("B9", "C9").Merge(true);
                hoja.get_Range("D9", "L9").Merge(true);
                hoja.get_Range("D9", "L9").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                hoja.get_Range("B9", "C9").Font.Bold = true;
                hoja.get_Range("B9", "C9").RowHeight = 30;
                hoja.get_Range("D9", "L9").RowHeight = 30;
                hoja.get_Range("B9", "C9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("D9", "L9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja.Cells[10, 2] = "DIRECCION: ";
                hoja.Cells[10, 4] = obtenerInformacionEmpresas.v_Address;
                hoja.get_Range("B10", "C10").Merge(true);
                hoja.get_Range("D10", "L10").Merge(true);
                hoja.get_Range("B10", "C10").Font.Bold = true;
                hoja.get_Range("B10", "C10").RowHeight = 30;
                hoja.get_Range("D10", "L10").RowHeight = 30;
                hoja.get_Range("B10", "C10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja.get_Range("D10", "L10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //Asigna borde
                rango = hoja.Range["B8", "L10"];
                rango.Borders.LineStyle = Excel.XlLineStyle.xlDot;

                //Modificamos los anchos de las columnas
                rango = hoja.Columns[1];
                rango.ColumnWidth = 3;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[2];
                rango.ColumnWidth = 5;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[3];
                rango.ColumnWidth = 40;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[4];
                rango.ColumnWidth = 7;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[5];
                rango.ColumnWidth = 12;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[6];
                rango.ColumnWidth = 10;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[7];
                rango.ColumnWidth = 30;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[8];
                rango.ColumnWidth = 40;
                rango.Cells.WrapText = true;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                rango = hoja.Columns[9];
                rango.ColumnWidth = 8;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#0.00";

                rango = hoja.Columns[10];
                rango.ColumnWidth = 12;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#0.00";

                rango = hoja.Columns[11];
                rango.ColumnWidth = 8;
                rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rango.NumberFormat = "#0.00";

                rango = hoja.Columns[12];
                rango.ColumnWidth = 20;
                rango.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de redondeo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Error mientras liberaba objecto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
