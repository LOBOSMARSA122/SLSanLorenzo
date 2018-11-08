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
        public frmLiquidacion()
        {
            InitializeComponent();
        }

        private void frmLiquidacion_Load(object sender, EventArgs e)
        {
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            OperationResult objOperationResult1 = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var clientOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var clientOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);

            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
            Utils.LoadDropDownList(ddlEmployerOrganization, "Value1", "Id", clientOrganization1, DropDownListAction.All);
            Utils.LoadDropDownList(cbbSubContratas, "Value1", "Id", clientOrganization2, DropDownListAction.All);

            
            //
            btnLiqd1.Enabled = false;
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
                }
                else if (tabControl1.SelectedTab.Name == "tpEmpresa")
                {
                    BindGridEmpresa();
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

            var _objData = _serviceBL.ListaLiquidacion(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

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

                    var obtenerInformacionEmpresas = new ServiceBL().ObtenerInformacionEmpresas(serviceID);


                    Liquidacion_EMO.CreateLiquidacion_EMO(ruta + nombre + ".pdf", MedicalCenter, lista, obtenerInformacionEmpresas);
                    this.Enabled = true;
                }
            }
            else if (tabControl1.SelectedTab.Name == "tpEmpresa")
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    var lista = _serviceBL.GetListaLiquidacionByEmpresa(ref _objOperationResult);

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Liquidaciones de EMPRESA - CSL";

                    //var obtenerInformacionEmpresas = new ServiceBL().ObtenerInformacionEmpresas(serviceID);

                    Liquidacion_EMO_EMPRESAS.CreateLiquidacion_EMO_EMPRESAS(ruta + nombre + ".pdf", MedicalCenter, lista);
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
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    if (grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value == null) return;
                    var liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
                    if (liquidacionID == null || liquidacionID =="")
                    {
                        btnLiqd1.Enabled = false;
                    }
                    else {
                        btnLiqd1.Enabled = true;
                    }
                }
                else
                {
                    btnLiqd1.Enabled = false;
                }

            }
            foreach (UltraGridRow rowSelected in this.grdEmpresa.Selected.Rows)
            {
                
                    btnLiqd1.Enabled = true;
                
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
            var liquidacionID = grdData.Selected.Rows[0].Cells["v_NroLiquidacion"].Value.ToString();
            var serviceID = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            var protocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                var lista = _serviceBL.GetListaLiquidacion(ref _objOperationResult, liquidacionID);

                //var _DataService = _serviceBL.GetInfoEmpresaLiquidacion(serviceID);
               
                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();
               
                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Carta N° "+ liquidacionID + " - CSL";

                var obtenerInformacionEmpresas = new ServiceBL().ObtenerInformacionEmpresas(serviceID);
                int systemUser = 205;
                var datosGrabo = _serviceBL.DevolverDatosUsuarioFirma(systemUser);

                Liquidacion_Carta.CreateLiquidacion_Carta(ruta + nombre + ".pdf", MedicalCenter, lista, obtenerInformacionEmpresas, datosGrabo);
                this.Enabled = true;
            }
        }
    }
}
