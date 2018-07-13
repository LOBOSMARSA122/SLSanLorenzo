using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Threading.Tasks;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System.Globalization;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class frmFileUploadEdit : Form
    {
        #region Declarations

        private string _fileName;
        private string _fileNameOld;
        private string _filePath;
        private ActionForm _action;
        private byte[] _bytes;
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        FileExtension fe;
        private byte[] _file = null;
        #endregion

        #region Properties

        /// <summary>
        /// Obtiene el nombre de archivo seleccionado.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Obtiene la ruta completa del archivo seleccionado.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }

        /// <summary>
        ///  Obtiene o establece una DTOEntity
        ///  utilizada para transporte de datos entre formularios.
        /// </summary>
        public FileInfoDto FileEntity { get; set; }

        /// <summary>
        /// Establece una Accion
        /// </summary>
        public ActionForm Action
        {
            set { _action = value; }
        }
      
        /// <summary>
        /// Obtiene o establece el valor del progress bar.
        /// </summary>
        public int? Progress
        {
            get
            {
                if (prgFileUpload.Style == ProgressBarStyle.Marquee)
                {
                    return null;
                }
                else
                {
                    return prgFileUpload.Value;
                }
            }

            set
            {
                if (value == null)
                {
                    prgFileUpload.Style = ProgressBarStyle.Marquee;
                    prgFileUpload.Value = 100;

                    lblPercent.Visible = false;
                }
                else
                {
                    prgFileUpload.Style = ProgressBarStyle.Continuous;
                    prgFileUpload.Value = value.Value;

                    lblPercent.Text = string.Format("{0}%", value);
                    lblPercent.Visible = true;
                }
            }
        }

        /// <summary>
        /// ID tabla person
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// ID tabla multimediafile
        /// </summary>
        public string MultimediaFileId { get; set; }

        public string ServiceComponentId { get; set; }

        public string Dni { get; set; }

        public string Fecha { get; set; }
        public string Consultorio { get; set; }
        

        #endregion

        public frmFileUploadEdit()
        {
            InitializeComponent();
        }

        private void frmFileUploadA_Load(object sender, EventArgs e)
        {
            lblExt.Text = string.Empty;
            lblFileSize.Text = string.Empty;

            switch (_action)
            {
                case ActionForm.Edit:
                    txtFileName.Text = FileEntity.FileName;
                    _fileNameOld = FileEntity.FileName;
                    txtDescripcion.Text = FileEntity.Description;
                    pbFile.Image = Common.Utils.byteArrayToImage(FileEntity.ThumbnailFile);
               
                    string fileExt = FileEntity.FileName.Substring(FileEntity.FileName.LastIndexOf('.') + 1).ToUpper();
                    
                    bool enumParseResult = Enum.TryParse(fileExt, true, out fe);
                    LoadDataFileFormat(fe);
                    break;
            }

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileSize = Convert.ToInt32(Convert.ToSingle(Common.Utils.GetFileSizeInMegabytes(openFileDialog1.FileName)));

                if (fileSize > 7)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.\nEl tamaño maximo es de 7 MB.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                prgFileUpload.Visible = true;
                // Seteaar propiedades del control PictutreBox
                LoadFileNoLock(openFileDialog1.FileName);             
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName;
                txtDescripcion.Clear();
                txtDescripcion.Focus();              
            }
            else
            {
                return;
            }
        }   

        private void LoadFileNoLock(string pfilePath)
        {               
            // Destruyo la posible imagen existente en el control           
            ImageDisposing();
             
            string fileExt = pfilePath.Substring(pfilePath.LastIndexOf('.') + 1).ToUpper();
                    
            FileExtension fe;
            bool enumParseResult = Enum.TryParse(fileExt, true, out fe);

            if (enumParseResult)
            {                                          
                SetImagenInfo(fe, pfilePath);            
            }
            else
            {
                MessageBox.Show("El archivo que está intentando subir no es soportado por el sistema.\nPor favor seleccione otro tipo de archivo", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }       
            
        }

        private void LoadDataFileFormat(FileExtension fe)
        {
            switch (fe) 
            {
                case FileExtension.JPG:
                    pnlPreviewTitle.BackColor = Color.Gray;
                    break;
                case FileExtension.GIF:
                    pnlPreviewTitle.BackColor = Color.Gray;
                    break;
                case FileExtension.JPEG:
                    pnlPreviewTitle.BackColor = Color.Gray;
                    break;
                case FileExtension.PNG:
                    pnlPreviewTitle.BackColor = Color.Gray;
                    break;
                case FileExtension.BMP:
                    pnlPreviewTitle.BackColor = Color.Gray;
                    break;
                case FileExtension.XLS:
                    pnlPreviewTitle.BackColor = Color.Green;
                    break;
                case FileExtension.XLSX:
                    pnlPreviewTitle.BackColor = Color.Green;
                    break;
                case FileExtension.DOC:
                    pnlPreviewTitle.BackColor = Color.DodgerBlue;
                    break;
                case FileExtension.DOCX:
                    pnlPreviewTitle.BackColor = Color.DodgerBlue;
                    break;
                case FileExtension.PDF:
                    pnlPreviewTitle.BackColor = Color.DarkRed;
                    break;
                case FileExtension.PPT:
                    pnlPreviewTitle.BackColor = Color.DarkOrange;
                    break;
                case FileExtension.PPTX:
                    pnlPreviewTitle.BackColor = Color.DarkOrange;
                    break;
                case FileExtension.TXT:
                    pnlPreviewTitle.BackColor = Color.Gray;
                    break;
                case FileExtension.AVI:
                    pnlPreviewTitle.BackColor = Color.Black;
                    break;
                case FileExtension.MPG:
                    pnlPreviewTitle.BackColor = Color.Black;
                    break;
                case FileExtension.MPEG:
                    pnlPreviewTitle.BackColor = Color.Black;
                    break;
                case FileExtension.MOV:
                    pnlPreviewTitle.BackColor = Color.Black;
                    break;
                case FileExtension.WMV:
                    pnlPreviewTitle.BackColor = Color.Black;
                    break;
                case FileExtension.FLV:
                    pnlPreviewTitle.BackColor = Color.Black;
                    break;
                case FileExtension.MP3:
                    pnlPreviewTitle.BackColor = Color.MediumPurple;
                    break;
                case FileExtension.MP4:
                    pnlPreviewTitle.BackColor = Color.MediumPurple;
                    break;
                case FileExtension.WMA:
                    pnlPreviewTitle.BackColor = Color.MediumPurple;
                    break;
                case FileExtension.WAV:
                    pnlPreviewTitle.BackColor = Color.MediumPurple;
                    break;
                default:
                    break;
            }

            lblExt.Text = fe.ToString();
        }

        private void SetImagenInfo(FileExtension fe, string pfilePath)
        {               
            Bitmap imageOriginal = null;

            try
            {
                switch (fe)
                {
                    #region Establecer -> Imagen / extension / color de borde

                    case FileExtension.JPG:
                        imageOriginal = new Bitmap(pfilePath);                      
                        pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);                                          
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Common.Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.GIF:
                        imageOriginal = new Bitmap(pfilePath);
                        pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Common.Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.JPEG:
                        imageOriginal = new Bitmap(pfilePath);
                        pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);                     
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Common.Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.PNG:
                        imageOriginal = new Bitmap(pfilePath);
                        pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Common.Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.BMP:
                        imageOriginal = new Bitmap(pfilePath);
                        pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Common.Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.XLS:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Excel.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Green;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.XLSX:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Excel.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Green;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.DOC:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "word.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.DodgerBlue;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.DOCX:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "word.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.DodgerBlue;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.PDF:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Pdf.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.DarkRed;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.PPT:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "ppt.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.DarkOrange;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.PPTX:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "ppt.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.DarkOrange;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.TXT:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "txt.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Gray;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.AVI:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "avi.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MPG:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mpg.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MPEG:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mpeg.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MOV:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mov.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.WMV:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wmv.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.FLV:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "flv.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MP3:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mp3.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MP4:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mp4.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.WMA:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wma.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.WAV:
                        pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wav.png");
                        lblExt.Text = fe.ToString();
                        pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    default:
                        break;

                    #endregion
                }

                if (imageOriginal != null) imageOriginal.Dispose();

                lblFileSize.Text = Common.Utils.GetFileSizeInMegabytes(pfilePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
           
        }

        private void DeleteImage()
        {
            pnlPreviewTitle.BackColor = Color.Transparent;
            lblExt.Text = string.Empty;
            lblFileSize.Text = string.Empty;
            ImageDisposing();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (pbFile.Image == null)
            {
                 MessageBox.Show("Seleccione archivo por favor.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);               
                 return;
            }

            OperationResult operationResult = new OperationResult();
            FileInfoDto fileInfo = null;
            DirectoryInfo rutaOrigen = null;
            string ext = "";
            switch (_action)
            {
                case ActionForm.Add:

                    fileInfo = new FileInfoDto();
                   
                    fileInfo.Id = null;
                    fileInfo.PersonId = PersonId;
                    fileInfo.ServiceComponentId = ServiceComponentId;
                    fileInfo.FileName = FileName;
                    fileInfo.Description = txtDescripcion.Text;                  
                    //fileInfo.ByteArrayFile = _file;
                   
                    if (Consultorio == "ESPIROMETRÍA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen").ToString());
                    }
                    else if (Consultorio == "RAYOS X")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString());
                    }
                    else if (Consultorio == "CARDIOLOGÍA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgEKGOrigen").ToString());
                    }
                    else if (Consultorio == "LABORATORIO")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgLABOrigen").ToString());
                    }
                    else if (Consultorio == "PSICOLOGÍA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgPSICOOrigen").ToString());
                    }
                    else if (Consultorio == "OFTALMOLOGÍA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgOftalmoOrigen").ToString());
                    }
                    else if (Consultorio == "MEDICINA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgMedicinaOrigen").ToString());
                    }
                    if (rutaOrigen == null)
                    {
                        MessageBox.Show("No se ha configurado una ruta para subir el archivo.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        ext = Path.GetExtension(_filePath);
                        File.Copy(_filePath, rutaOrigen + Dni + "-" + Fecha + "-" + Consultorio + ext);
                        fileInfo.FileName = Dni + "-" + Fecha + "-" + Consultorio + ext;
                        fileInfo.ThumbnailFile = Common.Utils.imageToByteArray1(pbFile.Image);
                        fileInfo.Action = (int)ActionForm.Add;

                        // Grabar

                        _multimediaFileBL.AddMultimediaFileComponent(ref operationResult, fileInfo, Globals.ClientSession.GetAsList());

                        // Analizar el resultado de la operación
                        if (operationResult.Success != 1)
                        {
                            MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }             
                    }

                   
                   
   
                    break;
                case ActionForm.Edit:                  
                    fileInfo = new FileInfoDto();

                    fileInfo.Id = null;
                    fileInfo.MultimediaFileId = FileEntity.MultimediaFileId;
                    fileInfo.ServiceComponentMultimediaId = FileEntity.ServiceComponentMultimediaId;
                    rutaOrigen = null;
                    if (Consultorio == "ESPIROMETRÍA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen").ToString());
                    }
                    else if (Consultorio == "RAYOS X")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString());
                    }
                    else if (Consultorio == "CARDIOLOGÍA")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgEKGOrigen").ToString());
                    }
                    else if (Consultorio == "LABORATORIO")
                    {
                        rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgLABOrigen").ToString());
                    }
                    if (rutaOrigen == null)
                    {
                        MessageBox.Show("No se ha configurado una ruta para subir el archivo.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Eliminar el archivo antiguo
                    if (System.IO.File.Exists(rutaOrigen + _fileNameOld))
                    {
                        System.IO.File.Delete(rutaOrigen + _fileNameOld);
                    }
                    else
                    {
                        MessageBox.Show("El archivo ah sido eliminado de la carpeta de origen", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //fileInfo.FileName = txtFileName.Text;
                    fileInfo.Description = txtDescripcion.Text;

                    ext = Path.GetExtension(_filePath);
                    File.Copy(_filePath, rutaOrigen + Dni + "-" + Fecha + "-" + Consultorio + ext);
                    fileInfo.FileName = Dni + "-" + Fecha + "-" + Consultorio + ext;

                    //if (_file != null)
                    //    fileInfo.ByteArrayFile = _file;

                    fileInfo.ThumbnailFile = Common.Utils.imageToByteArray1(pbFile.Image);
                    fileInfo.Action = (int)ActionForm.Edit;

                    _multimediaFileBL.UpdateMultimediaFileComponent(ref operationResult, fileInfo, Globals.ClientSession.GetAsList());
                   
                    // Analizar el resultado de la operación
                    if (operationResult.Success != 1)
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    } 

                    break;
            }

            ImageDisposing();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ImageDisposing();
        }
    
        private void frmFileUploadA_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageDisposing();
        }

        /// <summary>
        /// liberar los recursos usado por el pictureBox (pbFile). es necesario para evitar errores de memoria insuficiente
        /// </summary>
        private void ImageDisposing()
        {
            if (pbFile.Image != null)
            {              
                pbFile.Image.Dispose();            
                pbFile.Image = null;

                if (_file != null)
                    _file = null;
            }
        }     

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteImage();
        }      

    }
}
