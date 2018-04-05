using System;
using System.Collections;
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
    public partial class frmCapturedFingerPrint : Form
    {
        private int FMatchType, fpcHandle;
        private bool FAutoIdentify;
        private string _fileName;
        private string _filePath;

        private Sigesoft.Common.ModoCargaImagen _modoCargaImagen;

        #region Properties

        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public string Mode { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion

        #region Constructor

        public frmCapturedFingerPrint()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods
     
        private void ShowHintInfo(String s)
        {
            if (s != "")
            {
                memoHint.AppendText(s + Environment.NewLine);
                //lblresult.Text = s;
            }
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

        private void ShowHintImageFirma(int iType)
        {
            if (iType == 0)
            {
                imgNoFirma.Visible = false;
                imgOkFirma.Visible = false;
                imgInfoFirma.Visible = false;
            }
            else if (iType == 1)
            {
                imgNoFirma.Visible = false;
                imgOkFirma.Visible = true;
                imgInfoFirma.Visible = false;
            }
            else if (iType == 2)
            {
                imgNoFirma.Visible = true;
                imgOkFirma.Visible = false;
                imgInfoFirma.Visible = false;
            }
            else if (iType == 3)
            {
                imgNoFirma.Visible = false;
                imgOkFirma.Visible = false;
                imgInfoFirma.Visible = true;
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
                ShowHintInfo("Sensor conectado");
                lblresult.Text = "Sensor de Huella conectado y Listo para iniciar registro";
                ShowHintImage(3);
                ZKFPEngX1.FPEngineVersion = "9";

                //Crear un espacio de caché de identificación de huellas dactilares y devuelve su identificador
                fpcHandle = ZKFPEngX1.CreateFPCacheDB();
                //EDSensorNum.Text = Convert.ToString(ZKFPEngX1.SensorCount);
                //EDSensorIndex.Text = Convert.ToString(ZKFPEngX1.SensorIndex);
                //EDSensorSN.Text = ZKFPEngX1.SensorSN;
                ZKFPEngX1.EnrollCount = 3;
                //button1.Enabled = true;
            }
            else
            {
                ShowHintInfo("Error al conectar el sensor de Huella");
            }
        }

        //desconectar
        private void DisconnectFingerPrint()
        {
            ZKFPEngX1.EndEngine();
            //btnInit.Enabled = true;
            //button1.Enabled = false;
        }

        //Comienzo de la inscripción de huellas dactilares
        private void EnrollFingerPrint(object sender, EventArgs e)
        {
            ZKFPEngX1.CancelEnroll();
            ZKFPEngX1.EnrollCount = 3;
            ZKFPEngX1.BeginEnroll();
            ShowHintInfo("Inicio de Registro");
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisconnectFingerPrint();
        }

        private void btnAutoverify_Click(object sender, EventArgs e)
        {
            FAutoIdentify = true;
            ZKFPEngX1.SetAutoIdentifyPara(FAutoIdentify, fpcHandle, 8);
            FMatchType = 2;
        }      

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {


                if (pbFingerPrint.Image == null)
                {
                    FingerPrintImage = null;
                }

                if (ZKFPEngX1.GetTemplate().ToString() != string.Empty)
                {
                    FingerPrintTemplate = (byte[])ZKFPEngX1.GetTemplate();

                    // Imagen de la huella
                    object image = null;
                    bool result = ZKFPEngX1.GetFingerImage(ref image);
                    
                    // Bajar el peso de la huella *******************************************             
                    FingerPrintImage = Common.Utils.byteArrayToByteArrayImageJpg((byte[])image);
                    //***********************************************************************

                }
                else
                {
                    if (_modoCargaImagen == Common.ModoCargaImagen.DesdeArchivo)
                    {
                        FingerPrintImage = Common.Utils.ImageToByteArrayImageJpg1(pbFingerPrint.Image);
                    }
                    
                }

                sigPlusNET1.SetImageXSize(500);
                sigPlusNET1.SetImageYSize(150);
                sigPlusNET1.SetJustifyMode(5);

                // Firma imagen          
                var myimage = sigPlusNET1.GetSigImage();
               
                RubricImage = Common.Utils.ImageToByteArrayImageJpg(myimage);              

                myimage.Dispose();

                if (sigPlusNET1.GetSigString() == "300D0A300D0A")
                {
                    MessageBox.Show("No se permite grabar sin firma", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Firma serializada en formato ASCII hex string
                RubricImageText = sigPlusNET1.GetSigString();

                //var str2 = BitConverter.ToString(RubricImage);
                //string base64ImageRepresentation = Convert.ToBase64String(RubricImage);
                //string re = System.Text.Encoding.UTF8.GetString(RubricImage);
                //var ss = ToHexString(RubricImage);

                DisconnectFingerPrint();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.Utils.ExceptionFormatter(ex), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void FingerPrintImageDisposing()
        {
            if (pbFingerPrint.Image != null)
            {
                pbFingerPrint.Image.Dispose();

                pbFingerPrint.Image = null;
            }
        }

        #endregion

        #region Private Events

        private void frmCapturedFingerPrint_Load(object sender, EventArgs e)
        {
            try
            {
                // Iniciar el componente huellero
                InitFingerPrint();

                // Iniciar el componente Firma
                sigPlusNET1.SetTabletState(1);

                lblResultFirma.Text = "Sensor de firma conectado y Listo para iniciar registro.";
                ShowHintImageFirma(3);

                FAutoIdentify = false;

                if (Mode == "New")
                {
                    EnrollFingerPrint(null, null);
                }
                else if (Mode == "Edit")
                {
                    if (FingerPrintImage == null) 
                        return;

                    pbFingerPrint.Image = Common.Utils.byteArrayToImage(FingerPrintImage);

                    if (RubricImageText == null) 
                        return;

                    sigPlusNET1.SetSigString(RubricImageText);

                    //btnEnroll_Click(null, null);
                }       
            }
            catch (Exception ex)
            {              
                MessageBox.Show(ex.Message);
            }
          
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

        //
        private void ZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {
                MessageBox.Show("Registro de Huella Dactilar Exitoso！ ", "ZK4500 Finger Print ", MessageBoxButtons.OK);
                //e.aTemplate = ZKFPEngX1.GetTemplate();
                //ZKFPEngX1.AddRegTemplateToFPCacheDB(fpcHandle, 1, e.aTemplate);

                ZKFPEngX1.AddRegTemplateStrToFPCacheDBEx(fpcHandle, 1, ZKFPEngX1.GetTemplateAsStringEx("9"), ZKFPEngX1.GetTemplateAsStringEx("10"));
                ShowHintInfo("Registro de Huella Dactilar Exitoso！");
                lblresult.Text = "Registro de Huella Dactilar Exitoso！";
                ShowHintImage(3);
               
            }
            else
            {
                ShowHintInfo("Error en Registro de Huella Dactilar");
                MessageBox.Show("Error en Registro de Huella Dactilar ", "ZK4500 Finger Print ", MessageBoxButtons.OK);
                lblresult.Text = "Error en Registro de Huella Dactilar！";
                ShowHintImage(2);
            }

        }

        //Obtener las huellas dactilares característica inicial, 0: buena característica de huellas 1: puntos de característica no es suficiente
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
            ShowHintInfo(strTemp);
        }

        // Eventos para la huella digital (en este ejemplo, sólo 1: N coincidente: el espacio de caché con la información de huellas dactilares de identificación de huellas digitales para comparación)
        private void ZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            int ID, i;
            int Score = new int();
            int ProcessNum = new int();
            ShowHintInfo("Adquiriendo fingerprint plantilla:");

            if (FMatchType == 1) // 1:1
            {
                //ZKFPEngX1.VerFingerFromStr(sRegTemp, sVerTemplate, False, ref regChange) 

            }

            if (FMatchType == 2) //1:N
            {
                if (!FAutoIdentify)
                {
                    // recommended value is 9 
                    Score = 9;
                   
                    ZKFPEngX1.FPEngineVersion = "9";

                    //var ss = (Array)ZKFPEngX1.GetTemplate();

                    //object bm = null;
                    //var img = ZKFPEngX1.GetFingerImage(ref bm);
                    //pictureBox1.Image = Common.Utils.byteArrayToImage((byte[])bm);

                    //bool b = ZKFPEngX1.SaveTemplate("________tplGiovanny____", ZKFPEngX1.GetTemplate());

                    

                    ID = ZKFPEngX1.IdentificationFromStrInFPCacheDB(fpcHandle, ZKFPEngX1.GetTemplateAsStringEx("9"), ref Score, ref ProcessNum);
                   
                    if (ID == -1)
                    {
                        ShowHintInfo("Error Identificando Puntuación! = " + Convert.ToString(Score));
                        ShowHintImage(2);
                    }
                    else
                    {
                        String strTemp = "Identificación exitosa!\n" + " Score =" + Convert.ToString(Score);
                        ShowHintInfo(strTemp);
                        ShowHintImage(1);
                    }
                    if (ID > 0)
                    {
                        lblresult.Text = "Verificación exitosa";
                    }
                    else
                    {
                        lblresult.Text = "Lo sentimos, Error en la verificación!";
                    }

                }
                else
                {
                    ID = 0;
                    Score = 0;
                    //  e.aTemplate datos para el tipo de objeto, que se aisló y se convierte en un entero
                    Array _ObjectArray = (Array)e.aTemplate;
                    int _ObjectCount = _ObjectArray.GetLength(0);
                    ID = Convert.ToInt32(_ObjectArray.GetValue(0));
                    Score = Convert.ToInt32(_ObjectArray.GetValue(1));

                    //FingerPrintTemplate = ((int[])e.aTemplate).Select(x => (byte)x).ToArray();

                    if (ID == -1)
                    {
                        lblresult.Text = "Error en Auto Identificación de Huella Dactilar!";
                        ShowHintInfo("Error en Auto Identificación de Huella Dactilar!");
                        ShowHintImage(2);
                    }
                    else
                    {
                        lblresult.Text = "Auto Identificación de Huella Dactilar exitosa!";
                        ShowHintInfo("Auto Identificación de Huella Dactilar exitosa! Puntuación =" + Convert.ToString(Score));
                        ShowHintImage(1);
                    }

                }

            }
        }

        private void ZKFPEngX1_OnFingerTouching(object sender, EventArgs e)
        {
            ShowHintInfo("Tocando");

        }

        private void ZKFPEngX1_OnFingerLeaving(object sender, EventArgs e)
        {
            ShowHintInfo("Soltando");
        }

        private void frmCapturedFingerPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectFingerPrint();
        }

        private void btnDelSignature_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearTablet();
        }

        private void btnDelFingerPrint_Click(object sender, EventArgs e)
        {
            if (_modoCargaImagen == Common.ModoCargaImagen.DesdeArchivo)
            {
                pbFingerPrint.Image = null;
            }
            else
            {
                if (ZKFPEngX1.IsRegister)
                {
                    FingerPrintImageDisposing();
                    ZKFPEngX1.CancelEnroll();
                    ZKFPEngX1.EnrollCount = 3;
                    ZKFPEngX1.BeginEnroll();
                    lblresult.Text = "Sensor de Huella conectado y Listo para iniciar registro.";
                    ShowHintImage(3);
                }
                else
                {
                    DialogResult rpta = MessageBox.Show("La huella ya esta verificada correctamente. \n¿ Desea volver a realizar el registro ? ", "ZK4500 Finger Print ", MessageBoxButtons.YesNo);

                    if (rpta == DialogResult.Yes)
                    {
                        FingerPrintImageDisposing();
                        ZKFPEngX1.CancelEnroll();
                        ZKFPEngX1.EnrollCount = 3;
                        ZKFPEngX1.BeginEnroll();
                        lblresult.Text = "Sensor de Huella conectado y Listo para iniciar registro.";
                        ShowHintImage(3);
                    }
                }

                //ZKFPEngX1.BeginCapture();
            }
           
          
        }

        #endregion               

        private void button5_Click(object sender, EventArgs e)
        {
            // Indica que la huella se va cargar desde un archivo de imagen externo.
            _modoCargaImagen = Common.ModoCargaImagen.DesdeArchivo;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.jpg;*.gif;*.jpeg;*.png)|*.jpg;*.gif;*.jpeg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!IsValidImageSize(openFileDialog1.FileName))
                    return;

                // Seteaar propiedades del control PictutreBox
                LoadFile(openFileDialog1.FileName, pbFingerPrint);
                //pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
               
                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName;

                var Ext = Path.GetExtension(_fileName);

                if (Ext == ".JPG" || Ext == ".GIF" || Ext == ".JPEG" || Ext == ".PNG" || Ext == "")
                {

                    System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(pbFingerPrint.Image);

                    Decimal Hv = 280;
                    Decimal Wv = 383;

                    Decimal k = -1;

                    Decimal Hi = bmp1.Height;
                    Decimal Wi = bmp1.Width;

                    Decimal Dh = -1;
                    Decimal Dw = -1;

                    Dh = Hi - Hv;
                    Dw = Wi - Wv;

                    if (Dh > Dw)
                    {
                        k = Hv / Hi;
                    }
                    else
                    {
                        k = Wv / Wi;
                    }

                    pbFingerPrint.Height = (int)(k * Hi);
                    pbFingerPrint.Width = (int)(k * Wi);
                }
            }
            else
            {
                return;
            }
        }

        private void LoadFile(string pfilePath, PictureBox pb)
        {
            Image img = pb.Image;

            // Destruyo la posible imagen existente en el control
            //
            if (img != null)
            {
                img.Dispose();
            }

            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);
                pb.Image = original;

            }
        }

        private bool IsValidImageSize(string pfilePath)
        {
            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);

                if (original.Width > Sigesoft.Common.Constants.WIDTH_MAX_SIZE_IMAGE || original.Height > Sigesoft.Common.Constants.HEIGHT_MAX_SIZE_IMAGE)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Indica que la firma se va cargar desde un archivo de imagen externo.
            _modoCargaImagen = Common.ModoCargaImagen.DesdeArchivo;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.jpg;*.gif;*.jpeg;*.png)|*.jpg;*.gif;*.jpeg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!IsValidImageSize(openFileDialog1.FileName))
                    return;

                // Seteaar propiedades del control PictutreBox
                LoadFile(openFileDialog1.FileName, pbFirma);
                //pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
                pbFirma.Visible = true;

                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName;

                var Ext = Path.GetExtension(_fileName);

                if (Ext == ".JPG" || Ext == ".GIF" || Ext == ".JPEG" || Ext == ".PNG" || Ext == "")
                {

                    System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(pbFirma.Image);

                    Decimal Hv = 280;
                    Decimal Wv = 383;

                    Decimal k = -1;

                    Decimal Hi = bmp1.Height;
                    Decimal Wi = bmp1.Width;

                    Decimal Dh = -1;
                    Decimal Dw = -1;

                    Dh = Hi - Hv;
                    Dw = Wi - Wv;

                    if (Dh > Dw)
                    {
                        k = Hv / Hi;
                    }
                    else
                    {
                        k = Wv / Wi;
                    }

                    pbFirma.Height = (int)(k * Hi);
                    pbFirma.Width = (int)(k * Wi);
                }
            }
            else
            {
                return;
            }
        }
    }
}
