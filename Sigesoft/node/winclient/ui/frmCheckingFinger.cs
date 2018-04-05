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


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCheckingFinger : Form
    {

        private int FMatchType, fpcHandle;
        private bool FAutoIdentify;
        byte[] _FingerPrintImage;
        PacientBL objPacienteBL = new PacientBL();
        public string _PacientId = string.Empty;
        private string _personName;

        public frmCheckingFinger()
        {
            InitializeComponent();
        }

        private void frmCheckingFinger_Load(object sender, EventArgs e)
        {
            // Iniciar el componente huellero
            InitFingerPrint();
            FAutoIdentify = false;

            OperationResult objOperationResult = new OperationResult();
        
            var objPersonDto = objPacienteBL.GetPerson(ref objOperationResult, _PacientId);
            _personName = objPersonDto.v_FirstName + " " + objPersonDto.v_FirstLastName + " " + objPersonDto.v_SecondLastName;
            _FingerPrintImage = objPersonDto.b_FingerPrintTemplate;
              
          
        }


        //Show fingerprint image
        private void ZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            ShowHintImage(0);
            Graphics g = pbFingerPrint.CreateGraphics();
            Bitmap bmp = new Bitmap(pbFingerPrint.Width, pbFingerPrint.Height);
            g = Graphics.FromImage(bmp);
            int dc = g.GetHdc().ToInt32();
            ZKFPEngX1.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            pbFingerPrint.Image = bmp;
        }      

        private void ZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            String strTemp = "Fingerprint calidad";
            if (e.aQuality != 0)
            {
                strTemp = strTemp + " No buena";
                lblresult.Text = strTemp;
                ShowHintImage(2);
            }
            else
            {
                strTemp = strTemp + " Bueno";
            }
            if (ZKFPEngX1.EnrollIndex != 1)
            {
                if (ZKFPEngX1.IsRegister)
                {
                    if (ZKFPEngX1.EnrollIndex - 1 > 0)
                    {
                        strTemp = strTemp + '\n' + "Estado de Registro: pulse su dedo " + Convert.ToString(ZKFPEngX1.EnrollIndex - 1) + " veces!";
                        lblresult.Text = strTemp;
                        ShowHintImage(3);
                    }
                }
            }
          
        }

        private void ZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            string stmp = "";
            string Template = ZKFPEngX1.GetTemplateAsString();
            bool ddd = false;
            if (_FingerPrintImage == null)
            {
                MessageBox.Show("El trabajador no tiene registrado su huella digital", "!INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);            
                return;
            }
            stmp = System.Convert.ToBase64String(_FingerPrintImage);

            if (ZKFPEngX1.VerFingerFromStr(ref Template, stmp, false, ref ddd))
            {
                ShowHintImage(3);
                MessageBox.Show("Huella dáctilar correcta", "!INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;               
              
            }
            else
            {
                ShowHintImage(3);
                MessageBox.Show("Huella dáctilar incorrecta. Vuelva a intentar", "!ERROR DE VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pbFingerPrint.Image = null;
                //this.DialogResult = DialogResult.Cancel; 
           
            }
        }

        private void ZKFPEngX1_OnFingerTouching(object sender, EventArgs e)
        {
           
        }

        private void ZKFPEngX1_OnFingerLeaving(object sender, EventArgs e)
        {
         
        }

        private void frmCheckingFinger_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectFingerPrint();
        }
      

        //Show hint image
        private void ShowHintImage(int iType)
        {
            if (iType == 0)
            {
                imgNO.Visible = false;
                imgOK.Visible = false;
                imgInfo.Visible = false;
            }
            else if (iType == 1)
            {
                imgNO.Visible = false;
                imgOK.Visible = true;
                imgInfo.Visible = false;
            }
            else if (iType == 2)
            {
                imgNO.Visible = true;
                imgOK.Visible = false;
                imgInfo.Visible = false;
            }
            else if (iType == 3)
            {
                imgNO.Visible = false;
                imgOK.Visible = false;
                imgInfo.Visible = true;
            }

            this.Refresh();
        }

        // Initilization FingerPrint
        private void InitFingerPrint()
        {
            if (ZKFPEngX1.InitEngine() == 0)
            {
                //btnInit.Enabled = false;
                FMatchType = 2;              
                lblresult.Text = "Sensor de Huella conectado y Listo.";
                ShowHintImage(3);
                ZKFPEngX1.FPEngineVersion = "9";           
            }
            else
            {
                lblresult.Text = "Error al conectar el sensor de Huella";
            }
        }

        //desconectar
        private void DisconnectFingerPrint()
        {
            ZKFPEngX1.EndEngine();
            //btnInit.Enabled = true;
            //button1.Enabled = false;
        }
      

    }
}
