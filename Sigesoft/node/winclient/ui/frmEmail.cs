using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmEmail : Form
    {
        List<CalendarListEmail> Lista = new List<CalendarListEmail>();
        frmWaiting _frmWaiting = new frmWaiting("Enviando Notificación");
        string _EmpresaSede, _FechaInicio , _FechaFin;
        public frmEmail(List<CalendarListEmail> obj, string pstrEmpresaSede, string pstrFechaInicio, string pstrFechaFin)
        {
            Lista = obj;
            _FechaInicio = pstrFechaInicio;
            _FechaFin = pstrFechaFin;
            _EmpresaSede = pstrEmpresaSede;
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            try
            {
                // Obtener los Parametros necesarios para el envio de notificación
                var configEmail = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 161, "i_ParameterId");

                string smtp = configEmail[0].Value1.ToLower();
                int port = int.Parse(configEmail[1].Value1);
                string from = configEmail[2].Value1.ToLower();
                string fromPassword = configEmail[4].Value1;
                string subject = configEmail[6].Value1;
                bool enableSsl = Convert.ToBoolean(int.Parse(configEmail[3].Value1));
                string cabecera = "</BR><table><tr><td>EMPRESA / SEDE : " + _EmpresaSede + "  </td></tr><tr><td>SERVICIO: " + "Examen de Salud Ocupacional" + "</td></tr><tr><td>FECHA : " + " del " + _FechaInicio + "  al  " + _FechaFin + "</td></tr></table> </BR>";

                var html = Common.Utils.GetMyTable(Lista, x => x.v_EntryTimeCM, x => x.v_Pacient, x => x.v_NumberDocument, x => x.v_ProtocolName, x => x.v_ServiceTypeName,x => x.Restricciones, x => x.Observaciones ,x => x.v_EsoTypeName);
                string body = "<table><tr><td></td>" + txtBody.Text + "</tr><tr><td></td></tr></table>";
                string message = string.Format(cabecera + body + html);
                e.Result = true;
                // Enviar notificación de usuario y clave via email
                Common.Utils.SendMessage(smtp, port, enableSsl, true, from, fromPassword, txtLabel.Text.Trim(), "", txtSubject.Text, message, null);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
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

                EmailBL oEmailBL = new EmailBL();
                emailDto oemailDto = new emailDto();
                oemailDto.v_Email =  txtLabel.Text;
                OperationResult objOperationResult = new OperationResult();
                oEmailBL.AddEmail(ref objOperationResult, oemailDto);


                this.Enabled = false;
                _frmWaiting.Show(this);
                backgroundWorker1.RunWorkerAsync();
            }
            
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

        private void frmEmail_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<EmailList> ListaEmails = new List<EmailList>();
            EmailBL oEmailBL = new EmailBL();

            txtLabel.Select();
            var lista = oEmailBL.LlenarEmail(ref objOperationResult);
            txtLabel.DataSource = lista.Distinct();
            txtLabel.DisplayMember = "v_Email";
            txtLabel.ValueMember = "v_Email";

            txtLabel.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtLabel.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtLabel.DropDownWidth = 550;
            //this.txtLabel.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

            txtLabel.DisplayLayout.Bands[0].Columns[0].Width = 10;
            txtLabel.DisplayLayout.Bands[0].Columns[1].Width = 350;

           

        }
    }
}
