using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE.Custom;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Sigesoft.Node.Contasol.Integration;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmTramasSusalud : Form
    {
        string strFilterExpression;
        object lista;
        object listaUps;
        object listaproc;
        List<TramasList> _objData = new List<TramasList>();

        List<ServiceList> _objDataLista = new List<ServiceList>();

        TramasBL _objTramasBL = new TramasBL();
        public frmTramasSusalud()
        {
            InitializeComponent();
            OperationResult objOperationResult = new OperationResult();
            PacientBL _PacientBL = new PacientBL();
            using (new LoadingClass.PleaseWait(this.Location, "Data CIE10..."))
            {
                lista = _PacientBL.LlenarDxsTramas(ref objOperationResult);
            };
            using (new LoadingClass.PleaseWait(this.Location, "Data UPS..."))
            {
                listaUps = _PacientBL.LlenarListaUps(ref objOperationResult);
            };
            using (new LoadingClass.PleaseWait(this.Location, "Data Procedimientos..."))
            {
                listaproc = _PacientBL.LlenarListaProc(ref objOperationResult);
            };
            
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var fecha= grService.Selected.Rows[0].Cells["fechaservicio"].Value.ToString();
            DateTime parsedDate = DateTime.Parse(fecha);
            var genero= grService.Selected.Rows[0].Cells["genero"].Value.ToString();
            var edad= grService.Selected.Rows[0].Cells["edad"].Value.ToString();
            string tabName = utcSusalud.SelectedTab.Text;
            frmRegistroEmAmHos frmRegistroEm = new frmRegistroEmAmHos(tabName, string.Empty, "New", parsedDate, genero, edad, lista, listaUps, listaproc);
            frmRegistroEm.Text = "Registrar: " + tabName;
            if (tabName == "Ambulatorio" || tabName == "Emergencia" || tabName == "Partos")
            {
                frmRegistroEm.Size = new Size(638, 196);
            }
            else if (tabName == "Hospitalización")
            {
                frmRegistroEm.Size = new Size(638, 236);
            }
            else if (tabName == "Procedimientos / Cirugía")
            {
                frmRegistroEm.Size = new Size(638, 300);
            }
            frmRegistroEm.Show();
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public void btnFilter_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (dtpDateTimeStar.Value > dptDateTimeEnd.Value)
            {
                MessageBox.Show("La Fecha inicial no puede ser mayor a la final:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_DiseasesName.Contains(\"" + txtPacient.Text.Trim() + "\")");

            //Filters.Add("i_IsDeleted == 0");
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
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void BindGrid()
        {
            var objDataService = GetDataServices(0, null, "v_ServiceId ASC", strFilterExpression);
            grService.DataSource = objDataService;
            lblServices.Text = string.Format("Se encontraron {0} registros.", objDataService.Count());
            this.grService.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;

            //
            string tabName = utcSusalud.SelectedTab.Text;

            if (tabName == "Ambulatorio")
            {
                var objData = GetData(0, null, "d_FechaIngreso ASC", strFilterExpression);
                grAmbulatorio.DataSource = objData;

                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                if (objData.Count() >= 1)
                {
                    btnExportAmbulatorio.Enabled = true;
                }
                else
                {
                    btnExportAmbulatorio.Enabled = false;
                }

                this.grAmbulatorio.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            }
            else if (tabName == "Emergencia")
            {
                var objData = GetData(0, null, "d_FechaIngreso ASC", strFilterExpression);
                grEmergencia.DataSource = objData;

                lblRecordCount1.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                if (objData.Count() >= 1)
                {
                    btnExportEmergencia.Enabled = true;
                }
                else
                {
                    btnExportEmergencia.Enabled = false;
                }

                this.grEmergencia.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            }
            else if (tabName == "Hospitalización")
            {
                var objData = GetData(0, null, "d_FechaIngreso ASC", strFilterExpression);
                grHospitalizacion.DataSource = objData;

                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                if (objData.Count() >= 1)
                {
                    btnExportHospitalizacion.Enabled = true;
                }
                else
                {
                    btnExportHospitalizacion.Enabled = false;
                }

                this.grHospitalizacion.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            }
            else if (tabName == "Procedimientos / Cirugía")
            {
                var objData = GetData(0, null, "d_FechaIngreso ASC", strFilterExpression);
                grProcedimientosCirugia.DataSource = objData;

                lblRecordCount3.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                if (objData.Count() >= 1)
                {
                    btnExportProcedimientosCirugias.Enabled = true;
                }
                else
                {
                    btnExportProcedimientosCirugias.Enabled = false;
                }

                this.grProcedimientosCirugia.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            }
            else if (tabName == "Partos")
            {
                var objData = GetData(0, null, "d_FechaIngreso ASC", strFilterExpression);
                grPartos.DataSource = objData;

                lblRecordCount3.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                if (objData.Count() >= 1)
                {
                    btnExportartos.Enabled = true;
                }
                else
                {
                    btnExportartos.Enabled = false;
                }

                this.grPartos.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            }
            
        }

        private List<TramasList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            string tabName = utcSusalud.SelectedTab.Text;

            if (tabName == "Ambulatorio")
            {
                _objData = _objTramasBL.GettramasPageAndFilteredAmbulatorio(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Emergencia")
            {
                _objData = _objTramasBL.GettramasPageAndFilteredEmergencia(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Partos")
            {
                _objData = _objTramasBL.GettramasPageAndFilteredPartos(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Hospitalización")
            {
                _objData = _objTramasBL.GettramasPageAndFilteredHospitalizacion(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Procedimientos / Cirugía")
            {
                _objData = _objTramasBL.GettramasPageAndFilteredProcedimientosCirugia(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private List<ServiceList> GetDataServices(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {

            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
            //_objDataLista = new List<ServiceList>();
            string tabName = utcSusalud.SelectedTab.Text;

            if (tabName == "Ambulatorio")
            {
                _objDataLista = new ServiceBL().GetServiceForTramasPageAndFilteredAmbulatorio(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Emergencia")
            {
                _objDataLista = new ServiceBL().GetServiceForTramasPageAndFilteredEmergencia(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Partos")
            {
                _objDataLista = new ServiceBL().GetServiceForTramasPageAndFilteredPartos(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Hospitalización")
            {
                _objDataLista = new ServiceBL().GetServiceForTramasPageAndFilteredHospitalizacion(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }
            else if (tabName == "Procedimientos / Cirugía")
            {
                _objDataLista = new ServiceBL().GetServiceForTramasPageAndFilteredProcedimientosCirugias(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            }

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objDataLista;
        }
        private void btnExportAmbulatorio_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Datos Ambulatorio del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text + "-tramas";
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grAmbulatorio, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnExportEmergencia_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Datos Emergencia del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text + "-tramas";
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grEmergencia, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnExportHospitalizacion_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Datos Hospitalización del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text + "-tramas";
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grHospitalizacion, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnExportProcedimientosCirugias_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Datos Procedimientos/Cirugía del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text + "-tramas";
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grProcedimientosCirugia, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnExportartos_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Datos Partos del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text + "-tramas";
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grPartos, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnGenerar_Click_1(object sender, EventArgs e)
        {
            frmExportTramas frm = new frmExportTramas();
            frm.Show();
        }

        private void frmTramasSusalud_Load(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGenerar.Enabled = true;

            btnExportAmbulatorio.Enabled = false;
            btnExportEmergencia.Enabled = false;
            btnExportHospitalizacion.Enabled = false;
            btnExportProcedimientosCirugias.Enabled = false;
            btnExportartos.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                string tramaId = null;

                string tabName = utcSusalud.SelectedTab.Text;

                if (tabName == "Ambulatorio")
                {
                    tramaId = grAmbulatorio.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Emergencia")
                {
                    tramaId = grEmergencia.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Hospitalización")
                {
                    tramaId = grHospitalizacion.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Procedimientos / Cirugía")
                {
                    tramaId = grProcedimientosCirugia.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Partos")
                {
                    tramaId = grPartos.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }


                //string tabName = utcSusalud.SelectedTab.Text;
                frmRegistroEmAmHos frmRegistroEm = new frmRegistroEmAmHos(tabName, tramaId, "Edit", DateTime.Now, string.Empty, string.Empty, lista, listaUps, listaproc);
                frmRegistroEm.Text = "Editar: " + tabName;
                if (tabName == "Ambulatorio" || tabName == "Emergencia" || tabName == "Partos")
                {
                    frmRegistroEm.Size = new Size(638, 196);
                }
                else if (tabName == "Hospitalización")
                {
                    frmRegistroEm.Size = new Size(638, 236);
                }
                else if (tabName == "Procedimientos / Cirugía")
                {
                    frmRegistroEm.Size = new Size(638, 300);
                }
                frmRegistroEm.Show();
                btnAgregar.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
           }
            catch (Exception exception)
            {
                MessageBox.Show("SELECCIONE UNA TRAMA A EDITAR", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFilter_Click(sender, e);
            }
            //btnFilter_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                string tramaId = null;

                string tabName = utcSusalud.SelectedTab.Text;

                if (tabName == "Ambulatorio")
                {
                    tramaId = grAmbulatorio.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Emergencia")
                {
                    tramaId = grEmergencia.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Hospitalización")
                {
                    tramaId = grHospitalizacion.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Procedimientos / Cirugía")
                {
                    tramaId = grProcedimientosCirugia.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }
                else if (tabName == "Partos")
                {
                    tramaId = grPartos.Selected.Rows[0].Cells["v_TramaId"].Value.ToString();
                }

                DialogResult Result = MessageBox.Show("¿Está seguro de eliminar TRAMA?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    _objTramasBL.DeleteTrama(tramaId, Globals.ClientSession.GetAsList());
                }
                btnAgregar.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show("SELECCIONE UNA TRAMA A ELIMINAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFilter_Click(sender, e);
            }
        }

        private void grService_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnAgregar.Enabled = true;
        }

        private void utcSusalud_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void grAmbulatorio_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void grEmergencia_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void grHospitalizacion_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void grProcedimientosCirugia_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void grPartos_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        
    }
}
