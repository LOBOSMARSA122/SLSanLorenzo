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
using System.Diagnostics;



namespace Sigesoft.Node.WinClient.UI
{
    public partial class Reportes : Form
    {
        HistoryBL _historyBL = new HistoryBL();
        ServiceBL _serviceBL = new ServiceBL();
        private SaveFileDialog saveFileDialog3 = new SaveFileDialog();
        private string _serviceId;
        private string _pacientId;
        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            #region Simular sesion
            ClientSession objClientSession = new ClientSession();
            objClientSession.i_SystemUserId = 1;
            objClientSession.v_UserName = "sa";
            objClientSession.i_CurrentExecutionNodeId = 9;
            objClientSession.v_CurrentExecutionNodeName = "SALUS";
            //_ClientSession.i_CurrentOrganizationId = 57;
            objClientSession.v_PersonId = "N000-P0000000001";

            // Pasar el objeto de sesión al gestor de objetos globales
            Globals.ClientSession = objClientSession;
            #endregion     

            //_serviceId = "N009-SR000000395";
            //_pacientId = "N009-PP000000208";

            //_serviceId = "N009-SR000000478";
            //_pacientId = "N009-PR000000055";

            //_serviceId = "N009-SR000000477";
            //_pacientId = "N009-PR000000067";

            //_serviceId = "N009-SR000000476";
            //_pacientId = "N009-PR000000055";

            //_serviceId = "N009-SR000000475";
            //_pacientId = "N009-PR000000067";

            //_serviceId = "N009-SR000000441";
            //_pacientId = "N009-PR000000045";

            //_serviceId = "N009-SR000000357";
            //_pacientId = "N009-PP000000213";

            //_serviceId = "N009-SR000000352";
            //_pacientId = "N009-PP000000208";

            _serviceId = "N009-SR000001230";
            //_pacientId = "N009-PP000000208";

            //_serviceId = "N009-SR000000751";

            //DateTime FechaInicio = DateTime.Parse("07/21/2014");
            //DateTime FechaFin = DateTime.Parse("07/23/2014");


            //grdData.DataSource = _serviceBL.ReporteAseguradora(FechaInicio, FechaFin);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmAnexo7D(_serviceId, Constants.ALTURA_7D_ID);
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmOdontograma(_serviceId, Constants.ODONTOGRAMA_ID);
            //frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmHistoriaOcupacional(_serviceId);
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmRadiologico(_serviceId, Constants.RX_TORAX_ID);
            //frm.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmOsteomuscular(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmInformeRadiograficoOIT(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            //frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmEstudioElectrocardiografico(_serviceId, Constants.PRUEBA_ESFUERZO_ID);
            //frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var frm = new Reports.frmTamizajeDermatologico(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            frm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //var frm = new Reports.frmOdontograma(_serviceId, Constants.ODONTOGRAMA_ID);
            //frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog3.FileName = string.Format("{0} 7C", "Alberto Merchan Cosme");
            saveFileDialog3.Filter = "Files (*.pdf;)|*.pdf;";
            _serviceId = txtServicio.Text.Trim();
            _pacientId = txtPersona.Text.Trim();
            if (saveFileDialog3.ShowDialog() == DialogResult.OK)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    var _Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
                    var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
                    var _DataService = _serviceBL.GetServiceReport(_serviceId);
                    var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
                    var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
                 
                    var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);
                    var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
                    var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
                    var CuadroVacio = Common.Utils.BitmapToByteArray(Resources.CuadradoVacio);
                    var CuadroCheck = Common.Utils.BitmapToByteArray(Resources.CuadradoCheck);
                    var Pulmones = Common.Utils.BitmapToByteArray(Resources.MisPulmones);
                    var Audiometria = _serviceBL.ValoresComponenteOdontogramaValue1(_serviceId, Constants.AUDIOMETRIA_ID);
                    var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    ReportPDF.CreateAnexo7C(_DataService,filiationData, _Valores, _listMedicoPersonales, _listaPatologicosFamiliares, _listaHabitoNocivos, CuadroVacio, CuadroCheck, Pulmones,_PiezasCaries, _PiezasAusentes, Audiometria,diagnosticRepository,MedicalCenter, saveFileDialog3.FileName);
                    this.Enabled = true;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmConsentimiento(_serviceId);
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmEvaluacionGinecologica(_serviceId,Constants.GINECOLOGIA_ID);
            frm.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            var frm = new Reports.frmInformeEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);
            frm.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            var frm = new Reports.frmCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);
            frm.ShowDialog();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            var frm = new Reports.frmEvaluacionPsicolaboralPersonal(_serviceId, Constants.EVALUACION_PSICOLABORAL);
            frm.ShowDialog();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //var frm = new Reports.frmOsteo(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            //frm.ShowDialog();
        }

        PacientBL _pacientBL = new PacientBL();

        private void btnLaboratorio_Click(object sender, EventArgs e)
        {
            saveFileDialog3.FileName = string.Format("{0} Laboratorio", "Alberto Merchan Cosme");
            saveFileDialog3.Filter = "Files (*.pdf;)|*.pdf;";
            _serviceId = txtServicio.Text.Trim();
            _pacientId = txtPersona.Text.Trim();
        
            if (saveFileDialog3.ShowDialog() == DialogResult.OK)
            {              
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                    var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

                    var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);

                    LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, saveFileDialog3.FileName);
                    RunFile(saveFileDialog3.FileName); 
                }
            }
        }


        public void RunFile(string fileName)
        {
            Process proceso = Process.Start(fileName);
            //proceso.WaitForExit();
            proceso.Close();

        }

        private void btnCustionarioNordico_Click(object sender, EventArgs e)
        {
            ServiceBL o = new ServiceBL();
            o.GetReportCuestionarioNordico("N009-SR000002065", "");
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
           var x=  Common.Utils.DevolverLetrasMayusculas(txtCadena.Text);

           txtResultado.Text = x;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            bool Imported = true;
            List<RolCuotaDetalleList> _TempRolCuotaDetalleList;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.xls;*.xlsx)|*.xls;*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _TempRolCuotaDetalleList = new List<RolCuotaDetalleList>();

                var Ext = Path.GetExtension(openFileDialog1.FileName).ToUpper();

                if (Ext == ".XLSX" || Ext == ".XLS")
                {
                    Infragistics.Documents.Excel.Workbook workbook1 = Infragistics.Documents.Excel.Workbook.Load(openFileDialog1.FileName);

                    Infragistics.Documents.Excel.Worksheet worksheet1 = workbook1.Worksheets["PLANTILLA"];

                    RolCuotaDetalleList TemRolCuotaDetalleList;
                    int i = 4;
                    int ii = 4;

                    while (worksheet1.Rows[i].Cells[0].Value != null)
                    {
                        TemRolCuotaDetalleList = new RolCuotaDetalleList();
                        //ID PRODUCTO
                        if (worksheet1.Rows[i].Cells[0].Value != null)
                        {
                            string x = Common.Utils.DevolverLetrasMayusculas(worksheet1.Rows[i].Cells[0].Value.ToString());

                            TemRolCuotaDetalleList.v_IdProducto = x;
                            Imported = true;
                        }
                        else
                        {                           
                            i++;
                            continue;
                        }
                        i++;
                        _TempRolCuotaDetalleList.Add(TemRolCuotaDetalleList);
                        ultraGrid1.DataSource = _TempRolCuotaDetalleList;
                        lblContador.Text = _TempRolCuotaDetalleList.Count().ToString();
                    }




                }
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.ultraGrid1, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
