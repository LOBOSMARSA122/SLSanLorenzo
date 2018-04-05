using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmEnvioDeArchivos : Form
    {
        List<CalendarListEmail> Lista = new List<CalendarListEmail>();
        frmWaiting _frmWaiting = new frmWaiting("Enviando Notificación");
        private List<string> _filesNameToMerge = new List<string>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public class RunWorkerAsyncPackage
        {
            public string NombreEmpresaCliente { get; set; }
        }

        public frmEnvioDeArchivos()
        {
            InitializeComponent();
        }

        private void frmEnvioDeArchivos_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<EmailList> ListaEmails = new List<EmailList>();
            EmailBL oEmailBL = new EmailBL();

            txtLabel.Select();
            txtLabel.DataSource = oEmailBL.LlenarEmail(ref objOperationResult);
            txtLabel.DisplayMember = "v_Email";
            txtLabel.ValueMember = "v_Email";

            txtLabel.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtLabel.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtLabel.DropDownWidth = 550;
            //this.txtLabel.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

            txtLabel.DisplayLayout.Bands[0].Columns[0].Width = 10;
            txtLabel.DisplayLayout.Bands[0].Columns[1].Width = 350;
            var clientOrganization = BLL.Utils.GetJoinOrganization(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var Path1 = Application.StartupPath;
            if (ultraValidator1.Validate(true, false).IsValid)
            {
                if (txtLabel.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese Correo Electrónico.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtSubject.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un Asunto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (rbCotizacion.Checked)
                {
                    ServiceBL oServiceBL = new ServiceBL();
                    ProtocolBL oProtocolBL = new ProtocolBL();
                    ReportDocument rp;
                    DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();
                    var MedicalCenter = oServiceBL.GetInfoMedicalCenterSede();
                
                    DataSet ds = new DataSet();
                    var Valores = new ServiceBL().ObtenerCabeceraCotizacion(ddlCustomerOrganization.SelectedValue.ToString());
                    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Valores);
                    dt.TableName = "dtCartaCotizacion";
                    ds.Tables.Add(dt);

                    //Reporte 1
                    rp = new Reports.crReporteCotizacion01();
                    rp.SetDataSource(ds);
                   
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + "Cotizacion1.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    //Reporte 2
                    rp = new Reports.crReporteCotizacion02();
                    rp.SetDataSource(ds);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + "Cotizacion2.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    //Reporte 3
                    rp = new Reports.crReporteCotizacion03();
                    rp.SetDataSource(ds);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + "Cotizacion3.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    //Reporte 4
                    rp = new Reports.crReporteCotizacion04();
                    rp.SetDataSource(ds);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + "Cotizacion4.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    var l = oProtocolBL.CotizacionProtocoloPorEmpresaClienteId(ddlCustomerOrganization.SelectedValue.ToString());
                    HojaCotizacion.CrearHojaCotizacion(l, ddlCustomerOrganization.Text, MedicalCenter, ruta + ddlCustomerOrganization.Text+ ".pdf");
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta + ddlCustomerOrganization.Text )));
                                    
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path1 + @"\Archivos\Archivo2"));

                    var x = _filesNameToMerge.ToList();
                    _mergeExPDF.FilesName = x;
                    _mergeExPDF.DestinationFile = ruta + ddlCustomerOrganization.Text + " " + DateTime.Now.Date.ToString("dd MMMM") + ".pdf"; ;
                    _mergeExPDF.Execute();
                  
                }
                else if (rbCarta.Checked)
                {
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();

                    ReportDocument rp;
                    DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                    DataSet ds = new DataSet();
                    var Valores = new ServiceBL().ObtenerCabeceraCotizacion(ddlCustomerOrganization.SelectedValue.ToString());
                    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Valores);
                    dt.TableName = "dtCartaCotizacion";
                    ds.Tables.Add(dt);

                    rp = new Reports.crCartaPresentación();
                    rp.SetDataSource(ds);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + "Carta Presentación " + ddlCustomerOrganization.Text+ ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path1 + @"\Archivos\Archivo1"));

                    var x = _filesNameToMerge.ToList();
                    _mergeExPDF.FilesName = x;
                    _mergeExPDF.DestinationFile = ruta + "Carta Presentación " + ddlCustomerOrganization.Text + " " + DateTime.Now.Date.ToString("dd MMMM") + ".pdf";
                    _mergeExPDF.Execute();
                }
                //else if (rbDocumentos.Checked)
                //{
                //    List<string> ArchivoAdjunto = new List<string>();
                //    ArchivoAdjunto.Add(Path1 + @"\Archivos\Acreditacion.pdf");
                //    ArchivoAdjunto.Add(Path1 + @"\Archivos\Recomendaciones.pdf");
                //    ArchivoAdjunto.Add(Path1 + @"\Archivos\Informacion.pdf");
                //    ArchivoAdjunto.Add(Path1 + @"\Archivos\Mapa.pdf");

                //    ArchivoAdjunto.Add(Path1 + @"\Archivos\PLANTILLA.xlsx");
                //    ArchivoAdjunto.Add(Path1 + @"\Archivos\FICHA CLIENTE.xlsx");
                //}
                EmailBL oEmailBL = new EmailBL();
                emailDto oemailDto = new emailDto();
                oemailDto.v_Email = txtLabel.Text;
                OperationResult objOperationResult = new OperationResult();
                oEmailBL.AddEmail(ref objOperationResult, oemailDto);
                this.Enabled = false;
                _frmWaiting.Show(this);
              RunWorkerAsyncPackage packageForSave = new RunWorkerAsyncPackage();
              packageForSave.NombreEmpresaCliente = ddlCustomerOrganization.Text;
              backgroundWorker1.RunWorkerAsync(packageForSave);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void CloseErrorfrmWaiting()
        {
            if (_frmWaiting.InvokeRequired)
            {
                this.Invoke(new Action(CloseErrorfrmWaiting));
            }
            else
            {
                this.Enabled = true;
                _frmWaiting.Visible = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            _frmWaiting.Visible = false;

            if ((bool)e.Result == true)
            {
                MessageBox.Show("Su correo ha sido enviado correctamente.", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            try
            {
                RunWorkerAsyncPackage packageForSave = (RunWorkerAsyncPackage)e.Argument;

                // Obtener los Parametros necesarios para el envio de notificación
                var configEmail = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 161, "i_ParameterId");

                string smtp = configEmail[0].Value1.ToLower();
                int port = int.Parse(configEmail[1].Value1);
                string from = configEmail[2].Value1.ToLower();
                string fromPassword = configEmail[4].Value1;
                string subject = configEmail[6].Value1;
                bool enableSsl = Convert.ToBoolean(int.Parse(configEmail[3].Value1));
                string cabecera = "";

                var html = Common.Utils.GetMyTable(Lista, x => x.v_EntryTimeCM, x => x.v_Pacient, x => x.v_NumberDocument, x => x.v_ProtocolName, x => x.v_ServiceTypeName, x => x.v_EsoTypeName);
                string body = "<table><tr><td></td>" + txtBody.Text + "</tr><tr><td></td></tr></table>";
                string message = string.Format(cabecera + body + html);
                e.Result = true;
                // Enviar notificación de usuario y clave via email
                var Path = Application.StartupPath;
                List<string> ArchivoAdjunto = new List<string>();

                if (rbCotizacion.Checked)
                {
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();
                    ArchivoAdjunto.Add(ruta + packageForSave.NombreEmpresaCliente + " " + DateTime.Now.Date.ToString("dd MMMM") + ".pdf");
                }
                else if (rbCarta.Checked)
                {
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();
                    //ArchivoAdjunto.Add(ruta + "Carta Presentación " + ddlCustomerOrganization.Text+ ".pdf");
                    ArchivoAdjunto.Add(ruta + "Carta Presentación " + packageForSave.NombreEmpresaCliente + " " + DateTime.Now.Date.ToString("dd MMMM") + ".pdf");
                }
                else if (rbDocumentos.Checked)
                {
                    var Path1 = Application.StartupPath;
                    ArchivoAdjunto.Add(Path1 + @"\Archivos\Acreditacion.pdf");
                    ArchivoAdjunto.Add(Path1 + @"\Archivos\Recomendaciones.pdf");
                    ArchivoAdjunto.Add(Path1 + @"\Archivos\Informacion.pdf");
                    ArchivoAdjunto.Add(Path1 + @"\Archivos\Mapa.pdf");

                    ArchivoAdjunto.Add(Path1 + @"\Archivos\PLANTILLA.xlsx");
                    ArchivoAdjunto.Add(Path1 + @"\Archivos\FICHA CLIENTE.xlsx");
                }

                Common.Utils.SendMessage(smtp, port, enableSsl, true, from, fromPassword, txtLabel.Text.Trim(), "", txtSubject.Text, message, ArchivoAdjunto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Verifique su conexion de internet y/o cable de red,\n es posible que este desconectado.", "Error al enviar notificación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Result = false;
                CloseErrorfrmWaiting();
            }
        }

    }
}
