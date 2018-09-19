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
    public partial class frmEnviarCertificados : Form
    {
        List<String> _ListaServicios = new List<string>();
        List<CalendarListEmail> Lista = new List<CalendarListEmail>();
        frmWaiting _frmWaiting = new frmWaiting("Enviando Notificación");
        private List<string> _filesNameToMerge = new List<string>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        string ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
        string _EmpresaCliente;
        string _FechaInicio;
        string _FechaFin;
        ServiceBL _serviceBL = new ServiceBL();
        public class RunWorkerAsyncPackage
        {
            public string NombreEmpresaCliente { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFin { get; set; }
        }

        public frmEnviarCertificados(List<string> ListaServicioIds, string pEmpresaCliente, string FechaInicio, string FechaFin)
        {
            InitializeComponent();
            _ListaServicios = ListaServicioIds;
            _EmpresaCliente = pEmpresaCliente;
            _FechaInicio = FechaInicio;
            _FechaFin = FechaFin;
        }

        private void frmEnviarCertificados_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
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
        }

        private void btnOK_Click(object sender, EventArgs e)
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

            if (rb312.Checked)
            {
                foreach (var item in _ListaServicios)
                {
                    var o = ruta + item.ToString() + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                    _filesNameToMerge.Add(o);
                }
            
            }
            else if (rbEmpresarial.Checked)
            {                
                 foreach (var item in _ListaServicios)
                {
                    var o = ruta + item.ToString() + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + ".pdf";
                    _filesNameToMerge.Add(o);
                }
            }
            else if (rbSinDx.Checked)
            {
                foreach (var item in _ListaServicios)
                {
                    var o = ruta + item.ToString() + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + ".pdf";
                    _filesNameToMerge.Add(o);
                }
            }
            else if (rbToxicologico.Checked)
            {
                foreach (var item in _ListaServicios)
                {
                    var o = ruta + item.ToString() + "-" + Constants.TOXICOLOGICO_ID + ".pdf";
                    _filesNameToMerge.Add(o);
                }
            }
            else if (rbAltura18.Checked)
            {
                foreach (var item in _ListaServicios)
                {
                    var o = ruta + item.ToString() + "-" + Constants.ALTURA_ESTRUCTURAL_ID + ".pdf";
                    _filesNameToMerge.Add(o);
                }
            }
            else if (rbSomnolencia.Checked)
            {
                foreach (var item in _ListaServicios)
                {
                    var o = ruta + item.ToString() + "-" + Constants.SOMNOLENCIA_ID + ".pdf";
                    _filesNameToMerge.Add(o);
                }
            }


            var resultado = MessageBox.Show("¿Desea enviar certificados?", "¡ INFORMACIÓN !", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (resultado == DialogResult.Yes)
            {
                foreach (var item in _ListaServicios)
                {
                    _serviceBL.ActualizarEnvioCertificado(item, 1);
                }
            }

            var x = _filesNameToMerge.ToList();
            _mergeExPDF.FilesName = x;
            var EmpresaCliente = _EmpresaCliente.Replace("/", "_");
            _mergeExPDF.DestinationFile = ruta + "Certificados " + EmpresaCliente + " " + _FechaInicio + " " + _FechaFin + ".pdf"; 
            _mergeExPDF.Execute();


            EmailBL oEmailBL = new EmailBL();
            emailDto oemailDto = new emailDto();
            oemailDto.v_Email = txtLabel.Text;
            OperationResult objOperationResult = new OperationResult();
            oEmailBL.AddEmail(ref objOperationResult, oemailDto);
            this.Enabled = false;
            _frmWaiting.Show(this);

            RunWorkerAsyncPackage packageForSave = new RunWorkerAsyncPackage();
            packageForSave.NombreEmpresaCliente = _EmpresaCliente;
            packageForSave.FechaInicio = _FechaInicio;
            packageForSave.FechaFin = _FechaFin;
            backgroundWorker1.RunWorkerAsync(packageForSave);
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

                var EmpresaCliente = packageForSave.NombreEmpresaCliente.Replace("/", "_");
                ArchivoAdjunto.Add(ruta + "Certificados " + EmpresaCliente + " " + packageForSave.FechaInicio + " " + packageForSave.FechaFin + ".pdf");


                Common.Utils.SendMessage(smtp, port, enableSsl, true, from, fromPassword, txtLabel.Text.Trim(), "", txtSubject.Text, message, ArchivoAdjunto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Verifique su conexion de internet y/o cable de red,\n es posible que este desconectado.", "Error al enviar notificación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Result = false;
                CloseErrorfrmWaiting();
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
            this.Close();
        }
    }
}
