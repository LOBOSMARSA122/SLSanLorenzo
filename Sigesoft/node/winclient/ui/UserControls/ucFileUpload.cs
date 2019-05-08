using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucFileUpload : UserControl
    {
        #region Declarations

        private List<FileInfoDto> _multimediaFiles = null;
        /// <summary>
        /// Flag para no disparar el evento SelectionChanged al cargar la propiedad DataSource del grid dgvFiles.
        /// </summary>
        private bool _isLoadingData; 
        private string _fileName;
        private string _filePath;
        private Action _Action;
        private int _currentRowIndex;
        private string _fileDescription;
        private byte[] _byteArrayFile;
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        private string _multimediaFileId;
        private string _serviceComponentMultimediaId;
        #endregion
      
        #region Properties 

        public List<FileInfoDto> DataSource
        {
            get { return _multimediaFiles; }
            set
            {
                _multimediaFiles = value;
                _isLoadingData = false;
                dgvFiles.DataSource = value;
                _isLoadingData = true;
            }
           
        }

        /// <summary>
        /// Gets or Sets the value of the progress bar.
        /// </summary>
        //public int? Progress
        //{
        //    get
        //    {
        //        if (progressBar1.Style == ProgressBarStyle.Marquee)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            return progressBar1.Value;
        //        }
        //    }

        //    set
        //    {
        //        if (value == null)
        //        {
        //            progressBar1.Style = ProgressBarStyle.Marquee;
        //            progressBar1.Value = 100;

        //            lblPercent.Visible = false;
        //        }
        //        else
        //        {
        //            progressBar1.Style = ProgressBarStyle.Continuous;
        //            progressBar1.Value = value.Value;

        //            lblPercent.Text = string.Format("{0}%", value);
        //            lblPercent.Visible = true;
        //        }
        //    }
        //}

        /// <summary>
        /// ID tabla person
        /// </summary>
        public string PersonId { get; set; }
        public string Dni { get; set; }
        public string Fecha { get; set; }
        public string ProtocolId { get; set; }
        public string Consultorio { get; set; }
        ///// <summary>
        ///// ID tabla multimediafile
        ///// </summary>
        //public string MultimediaFileId { get; set; }

        public string ServiceComponentId { get; set; }

        #endregion

        public ucFileUpload()
        {
            InitializeComponent();          
        }

        private void LoadDataGridView()
        {
            OperationResult operationResult = new OperationResult();
            _multimediaFiles = _multimediaFileBL.GetMultimediaFiles(ref operationResult, ServiceComponentId);

            // Analizar el resultado de la operación
            if (operationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvFiles.DataSource = _multimediaFiles;

            // setear nro de archivos agregados
            lblRecordCount.Text = string.Format("{0} Archivo(s) agregado(s)", dgvFiles.Rows.Count);

            // Limpiar grilla
            if (dgvFiles.RowCount > 0)
                dgvFiles.Rows[0].Selected = true;

        }

        private void ucFileUpload_Load(object sender, EventArgs e)
        {
            dgvFiles.AutoGenerateColumns = false;

            LoadDataGridView();
            _multimediaFiles = new List<FileInfoDto>();
        }

        private void dgvFiles_SelectionChanged(object sender, EventArgs e)
        {
            GetDataFromDataGridView();                  
        }

        private void GetDataFromDataGridView()
        {
            //if (!_isLoadingData) return;

            if (dgvFiles.SelectedRows.Count == 0)         
                return;

            ButtonsEnabled();

            // Setear variable global fila actual
            _currentRowIndex = dgvFiles.CurrentRow.Index;

            // Buscar Archivo y mostrar 
            _fileDescription = dgvFiles.SelectedRows[0].Cells["Description"].Value.ToString();
            _fileName = dgvFiles.SelectedRows[0].Cells["FileName"].Value.ToString();
            _byteArrayFile = (byte[])dgvFiles.SelectedRows[0].Cells["ThumbnailFile"].Value;
            _multimediaFileId = dgvFiles.SelectedRows[0].Cells["MultimediaFileId"].Value.ToString();
            _serviceComponentMultimediaId = dgvFiles.SelectedRows[0].Cells["ServiceComponentMultimediaId"].Value.ToString();

            if (_byteArrayFile != null)
                pbProductImage.Image = Sigesoft.Common.Utils.byteArrayToImage(_byteArrayFile);

        }     

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmFileUploadEdit frm = new frmFileUploadEdit();
            frm.PersonId = PersonId;
            frm.ServiceComponentId = ServiceComponentId;
            frm.Dni = Dni;
            frm.ProtocolId = ProtocolId;
            frm.Fecha = Fecha.Replace("/","");
            frm.Consultorio = Consultorio;
            frm.Action = ActionForm.Add;          
            
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            LoadDataGridView();


            //if (frm.FileName != null)
            //{
            //    if (CheckFileExistsInList(frm.FileName))
            //        return;
            //}

            //if (frm.FileEntity != null)
            //{
            //    var searchResult = _files.Find(p => p.FileName == frm.FileEntity.FileName);

            //    if (searchResult != null)   // se encontro una coincidencia
            //    {
            //        if (searchResult.Action == (int)ActionForm.Delete)
            //        {
            //            searchResult.Description = frm.FileEntity.Description;
            //            searchResult.ByteArrayFile = frm.FileEntity.ByteArrayFile;
            //            searchResult.Action = frm.FileEntity.Action;
            //        }
            //        else
            //        {
            //            MessageBox.Show("El archivo [" + searchResult.FileName + "] ya está agregado. corrija por favor.", "FileUpload::", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
                  
            //    }
            //    else  // ok se agrega con normalidad
            //    {
            //        _files.Add(frm.FileEntity);
            //    }              

            //    // Cargar grilla
            //    LoadDataGridView();
                _isLoadingData = true;
            //    ButtonsEnabled();
            //}
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmFileUploadEdit frm = new frmFileUploadEdit();
            frm.Action = ActionForm.Edit;

            frm.FileEntity = new FileInfoDto { 
                FileName = _fileName, 
                Description = _fileDescription, 
                ThumbnailFile = _byteArrayFile,
                MultimediaFileId = _multimediaFileId,
                //Dni = Dni,
                //Fecha = Fecha.Replace("/",""),
                //Consultorio = Consultorio,
                ServiceComponentMultimediaId = _serviceComponentMultimediaId
            };
            frm.Dni = Dni;
            frm.Fecha = Fecha.Replace("/", "");
            frm.Consultorio = Consultorio;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            LoadDataGridView();
           
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedRows.Count > 0)
            {
                string _fileName = dgvFiles.SelectedRows[0].Cells["FileName"].Value.ToString();
                DialogResult rp = MessageBox.Show("¿ Desea realmente remover el archivo " + _fileName + " ?", "FileUpload::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                DirectoryInfo rutaOrigen = null;
                if (rp == DialogResult.OK)
                {

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
                    if (System.IO.File.Exists(rutaOrigen + _fileName))
                    {
                        System.IO.File.Delete(rutaOrigen + _fileName);
                    }
                    else
                    {
                        MessageBox.Show("El archivo ah sido eliminado de la carpeta de origen", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    OperationResult operationResult = new OperationResult();
                    _multimediaFileBL.DeleteMultimediaFileComponent(ref operationResult, _multimediaFileId, Globals.ClientSession.GetAsList());

                    // Analizar el resultado de la operación
                    if (operationResult.Success != 1)
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ImageDisposing();
                    // Cargar grilla
                    LoadDataGridView();          
                    ButtonsEnabled();
                                  
                    // setear nro de archivos agregados
                    lblRecordCount.Text = string.Format("{0} Archivo(s) agregado(s)", dgvFiles.Rows.Count);
                }
            }
            else
            {
                MessageBox.Show("Debe de Seleccionar un registro.", "FileUpload::", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }           

        private void ButtonsEnabled()
        {
            btnEliminar.Enabled = btnModificar.Enabled = btnDescargar.Enabled = dgvFiles.SelectedRows.Count > 0;
        }

        private void pbProductImage_Click(object sender, EventArgs e)
        {
            if (_byteArrayFile == null) return;

            var frm = new frmPreviewImagePerson(_byteArrayFile, _fileName);
            frm.ShowDialog();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            #region MyRegion



            //// Analizar el resultado de la operación
            //if (operationResult.Success != 1)
            //{
            //    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //#region Download file
            //// 

            //string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //using (SaveFileDialog sfd = new SaveFileDialog())
            //{



            //    //Obtener la extensión del archivo
            //    string Ext = multimediaFile.FileName.Substring( multimediaFile.FileName.Length -3 ,3);

            //    sfd.Title = multimediaFile.dni + "-" + Fecha + "-" + multimediaFile.FileName + "." + Ext;
            //    sfd.FileName = mdoc +"\\"+ sfd.Title; 

            //        string path = sfd.FileName;
            //        File.WriteAllBytes(path, multimediaFile.ByteArrayFile);

            //        MessageBox.Show("El archivo se grabó correctamente en la carpeta MIS DOCUMENTOS.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}

            //#endregion
            #endregion

            try
            {
                OperationResult operationResult = new OperationResult();
                var multimediaFile = _multimediaFileBL.GetMultimediaFileById(ref operationResult, _multimediaFileId);
                //Fecha = multimediaFile.Fecha.Day.ToString().PadLeft(2, '0') + multimediaFile.Fecha.Month.ToString().PadLeft(2, '0') + multimediaFile.Fecha.Year.ToString();
                //string f = Fecha;
                DirectoryInfo rutaOrigen = null;
                string rutaDestino = null;
                DirectoryInfo ruta = null;
                //DateTime FechaServicio = DateTime.Parse(Fecha);
                string Fecha1 = Fecha;
                
                if (Consultorio == "RAYOS X")
                {
                    rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString());
                    ruta = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxDestino").ToString());
                    rutaDestino = Common.Utils.GetApplicationConfigValue("ImgRxDestino").ToString() + Dni + "-" + Fecha1;


                    FileInfo[] files = rutaOrigen.GetFiles();
                    int Contador = 1;
                    foreach (FileInfo file in files)
                    {
                        
                        if (file.ToString().Count() > 16)
                        {
                            if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha1)
                            {
                                string ext = Path.GetExtension(file.ToString());
                                file.CopyTo(rutaDestino + "-" + Contador + ext);
                                Contador++;
                            };
                        }
                    }
                }
                else if (Consultorio == "ELECTROCARDIOGRAMA")
                {
                    rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgEKGOrigen").ToString());
                    ruta = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgEKGDestino").ToString());
                    rutaDestino = Common.Utils.GetApplicationConfigValue("ImgEKGDestino").ToString() + Dni + "-" + Fecha1;


                    FileInfo[] files = rutaOrigen.GetFiles();
                    int Contador = 1;
                    foreach (FileInfo file in files)
                    {
                        if (file.ToString().Count() > 16)
                        {
                            if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha1)
                            {
                                string ext = Path.GetExtension(file.ToString());
                                file.CopyTo(rutaDestino + "-" + Contador + ext);
                                Contador++;
                            };
                        }
                    }
                }
                else if (Consultorio == "ESPIROMETRÍA")
                {
                    rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen").ToString());
                    ruta = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgESPIDestino").ToString());
                    rutaDestino = Common.Utils.GetApplicationConfigValue("ImgESPIDestino").ToString() + Dni + "-" + Fecha1;


                    FileInfo[] files = rutaOrigen.GetFiles();
                    int Contador = 1;
                    foreach (FileInfo file in files)
                    {
                       if (file.ToString().Count() > 16)
                        {
                            if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha1)
                            {
                                string ext = Path.GetExtension(file.ToString());
                                file.CopyTo(rutaDestino + "-" + Contador + ext);
                                Contador++;
                            };
                        }
                    }
                }

                else if (Consultorio == "LABORATORIO")
                {
                    rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgLABOrigen").ToString());
                    ruta = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgLABDestino").ToString());
                    rutaDestino = Common.Utils.GetApplicationConfigValue("ImgLABDestino").ToString() + Dni + "-" + Fecha1;


                    FileInfo[] files = rutaOrigen.GetFiles();
                    int Contador = 1;
                    foreach (FileInfo file in files)
                    {
                        if (file.ToString().Count() > 16)
                        {
                            if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha1)
                            {
                                string ext = Path.GetExtension(file.ToString());
                                file.CopyTo(rutaDestino + "-" + Contador + ext);
                                Contador++;
                            };
                        }
                    }
                }



                MessageBox.Show("Los archivos se copiaron correctamente en la siguiente ruta: " + Common.Utils.GetApplicationConfigValue("ImgRxDestino").ToString());
                System.Diagnostics.Process.Start(ruta.ToString());
                Clipboard.SetText(Dni + "-" + Fecha1);
        
            }
            catch (Exception ex)
            {
                MessageBox.Show("A ocurrido un error: ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }
            

        private void ImageDisposing()
        {
            if (pbProductImage.Image != null)
            {
                pbProductImage.Image.Dispose();
                pbProductImage.Image = null;

               
            }
        }     
       
    }
}
