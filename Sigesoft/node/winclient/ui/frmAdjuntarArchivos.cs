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
using Infragistics.Win.UltraWinGrid;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdjuntarArchivos : Form
    {
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        List<string> _servicios = new List<string>();
        List<Rutas> _rutas = new List<Rutas>();

        ServiceBL oServiceBL = new ServiceBL();

        string _ServicioId = "";
        string _RutaLarga = "";
        string _Paciente = "";
        string _PersonId = "";
        string _ProtocolId = "";
        private byte[] _file = null;
        PictureBox pbFile = new PictureBox();


        public frmAdjuntarArchivos(List<string> pstrServicios)
        {
            InitializeComponent();
            _servicios = pstrServicios;

            //Llenar Grilla
            _rutas = oServiceBL.LlenarGrillaArchivosAdjunto(_servicios);
            grdData.DataSource = _rutas;



        }
         List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();



        private void frmAdjuntarArchivos_Load(object sender, EventArgs e)
        {
              ddlComponentId.SelectedValueChanged -= ddlComponentId_SelectedValueChanged;

            OperationResult objOperationResult = new OperationResult();

          //var ListaComponentes =    _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);
          //Utils.LoadDropDownList(ddlComponentId, "Value3", "Value2", ListaComponentes, DropDownListAction.Select);

            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = oServiceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);

            //*********************************************


            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));


            // Remover los componentes que no estan asignados al rol del usuario
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2)).FindAll(p => p.Value4 == 15 || p.Value4 == 5 || p.Value4 == 18 || p.Value4 == 16 || p.Value4 == 1 || p.Value4 == 7 || p.Value4 == 6);
            //var dd = groupComponentList.FindAll(p => componentProfile.FindAll(o => o.v_ComponentId == p.Value2));

            Utils.LoadDropDownList(ddlComponentId, "Value1", "Value4", results, DropDownListAction.Select);

          ddlComponentId.SelectedValueChanged += ddlComponentId_SelectedValueChanged;
        }

          private void ddlComponentId_SelectedValueChanged(object sender, EventArgs e)
        {

        }

          private void btnGuardar_Click(object sender, EventArgs e)
          {
              if (uvPacient.Validate(true, false).IsValid)
              {
                   OperationResult operationResult = new OperationResult();
                  FileInfoDto fileInfo = null;

                  //Verificar si algun registro no tiene ruta asignada

                  foreach (var item in _rutas)
                  {
                      if (item.RutaLarga =="")
                      {
                          if (MessageBox.Show("El trabajador " + item.Paciente + " no tiene un archivo asignado, ¿Desea Continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.No)
                          {
                              return;
                          };                        
                      }
                  }

                  foreach (var item in _rutas)
                  {
                     //Obtener CategoriaId
                      var oKeyValues= (KeyValueDTO)ddlComponentId.SelectedItem;
                      var CategoriaId = oKeyValues.Value4;

                      //Obtener lista de componentes de un protocolo por su categoria
                      ProtocolBL oProtocolBL = new ProtocolBL();
                      var ListaComponentesCategoria = oProtocolBL.GetProtocolComponents(ref operationResult, item.ProtocolId).FindAll(p => p.i_CategoryId == CategoriaId);

                      var OrdenDescListaComponentesCategoria = ListaComponentesCategoria.OrderBy(o => o.v_ComponentId).ToList();

                      

                      //var eee = (KeyValueDTO)ddlComponentId.SelectedItem;

                      //Obtener el ServicecomponentId

                      var oserviceComponent = oServiceBL.GetServiceComponentByServiceIdAndComponentId(item.ServicioId, OrdenDescListaComponentesCategoria[0].v_ComponentId);
                      
                      if (oserviceComponent != null)
                      {
                         
                          string serviceComponentId = oserviceComponent.v_ServiceComponentId;


                          if (item.RutaLarga != "")
                          {
                              
                            var fileSize = Convert.ToInt32(Convert.ToSingle(Common.Utils.GetFileSizeInMegabytes(item.RutaLarga)));
                            if (fileSize > 7)
                            {
                                MessageBox.Show("La imagen que está tratando de subir es damasiado grande.\nEl tamaño maximo es de 7 MB.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }


                            // Seteaar propiedades del control PictutreBox
                            LoadFileNoLock(item.RutaLarga);   


                              fileInfo = new FileInfoDto();

                              fileInfo.Id = null;
                              fileInfo.PersonId = item.PersonId;
                              fileInfo.ServiceComponentId = serviceComponentId;
                              fileInfo.FileName = item.RutaCorta;
                              fileInfo.Description = "";
                              fileInfo.ByteArrayFile = _file;
                              fileInfo.ThumbnailFile = Common.Utils.imageToByteArray1(pbFile.Image);
                              fileInfo.Action = (int)ActionForm.Add;

                              // Grabar

                              _multimediaFileBL.AddMultimediaFileComponent(ref operationResult, fileInfo, Globals.ClientSession.GetAsList());
                          }
                      }       
                  }


                  MessageBox.Show("Se adjuntaron los archivos correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  this.DialogResult = DialogResult.OK;

              }
              else
              {
                  MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

              }
          }

          private void btnExit_Click(object sender, EventArgs e)
          {

          }

          private void btnArchivo1_Click(object sender, EventArgs e)
          {
              openFileDialog1.FileName = string.Empty;
              //openFileDialog1.Filter = "Image Files (*.jpg;*.gif;*.jpeg;*.png)|*.jpg;*.gif;*.jpeg;*.png";

              if (openFileDialog1.ShowDialog() == DialogResult.OK)
              {
                  if (!IsValidImageSize(openFileDialog1.FileName))
                      return;

                  txtFileName1.Text = Path.GetFileName(openFileDialog1.FileName);

                  Rutas oRutas = new Rutas();
                  _rutas.RemoveAll(p => p.ServicioId == _ServicioId);

                  oRutas.ServicioId = _ServicioId;
                  oRutas.Paciente = _Paciente;
                  oRutas.RutaCorta = Path.GetFileName(openFileDialog1.FileName);
                  oRutas.RutaLarga = openFileDialog1.FileName;
                  oRutas.PersonId = _PersonId;
                  oRutas.ProtocolId = _ProtocolId;
                  _rutas.Add(oRutas);

                  grdData.DataSource = new List<Rutas>();
                  grdData.DataSource = _rutas;
                  txtFileName1.Text = "";

              }
          }

          private bool IsValidImageSize(string pfilePath)
          {
              using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
              {
                  Image original = Image.FromStream(fs);

                  if (original.Width > Constants.WIDTH_MAX_SIZE_IMAGE || original.Height > Constants.HEIGHT_MAX_SIZE_IMAGE)
                  {
                      MessageBox.Show("La imagen que está tratando de subir es damasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      return false;
                  }
              }
              return true;
          }

          private void btnAgregar_Click(object sender, EventArgs e)
          {
              Rutas oRutas = new Rutas();
              _rutas.RemoveAll(p => p.ServicioId == _ServicioId);

              oRutas.ServicioId = _ServicioId;
              oRutas.Paciente = _Paciente;
              oRutas.RutaCorta = Path.GetFileName(openFileDialog1.FileName);
              oRutas.RutaLarga = openFileDialog1.FileName;
              oRutas.PersonId = _PersonId;
              oRutas.ProtocolId = _ProtocolId;
              _rutas.Add(oRutas);

              grdData.DataSource =  new List<Rutas>();
              grdData.DataSource = _rutas;
              txtFileName1.Text = "";
              
          }

          private void grdData_MouseDown(object sender, MouseEventArgs e)
          {
                Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);
           

            if (uiElement == null || uiElement.Parent == null)
                return;

            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));
            if (e.Button == MouseButtons.Left)
            {
                if (row != null)
                {
                    _ServicioId = grdData.Selected.Rows[0].Cells["ServicioId"].Value.ToString();
                    _RutaLarga = grdData.Selected.Rows[0].Cells["RutaLarga"].Value.ToString();
                    _Paciente = grdData.Selected.Rows[0].Cells["Paciente"].Value.ToString();
                    _PersonId = grdData.Selected.Rows[0].Cells["PersonId"].Value.ToString();
                    _ProtocolId = grdData.Selected.Rows[0].Cells["ProtocolId"].Value.ToString();
                    btnArchivo1.Enabled = true;
                    btnAgregar.Enabled = true;
                }
            }
            else
            {
                btnArchivo1.Enabled = false;
                btnAgregar.Enabled = false;
            }
          }

          private void LoadFileNoLock(string pfilePath)
          {
            

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
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Gray;
                          _file = Common.Utils.imageToByteArray1(imageOriginal);
                          break;
                      case FileExtension.GIF:
                          imageOriginal = new Bitmap(pfilePath);
                          pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Gray;
                          _file = Common.Utils.imageToByteArray1(imageOriginal);
                          break;
                      case FileExtension.JPEG:
                          imageOriginal = new Bitmap(pfilePath);
                          pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Gray;
                          _file = Common.Utils.imageToByteArray1(imageOriginal);
                          break;
                      case FileExtension.PNG:
                          imageOriginal = new Bitmap(pfilePath);
                          pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Gray;
                          _file = Common.Utils.imageToByteArray1(imageOriginal);
                          break;
                      case FileExtension.BMP:
                          imageOriginal = new Bitmap(pfilePath);
                          pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Gray;
                          _file = Common.Utils.imageToByteArray1(imageOriginal);
                          break;
                      case FileExtension.XLS:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Excel.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Green;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.XLSX:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Excel.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Green;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.DOC:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "word.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.DodgerBlue;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.DOCX:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "word.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.DodgerBlue;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.PDF:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Pdf.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.DarkRed;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.PPT:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "ppt.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.DarkOrange;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.PPTX:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "ppt.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.DarkOrange;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.TXT:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "txt.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Gray;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.AVI:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "avi.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Black;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.MPG:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mpg.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Black;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.MPEG:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mpeg.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Black;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.MOV:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mov.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Black;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.WMV:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wmv.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Black;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.FLV:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "flv.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.Black;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.MP3:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mp3.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.MediumPurple;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.MP4:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mp4.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.MediumPurple;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.WMA:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wma.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.MediumPurple;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      case FileExtension.WAV:
                          pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wav.png");
                          //lblExt.Text = fe.ToString();
                          //pnlPreviewTitle.BackColor = Color.MediumPurple;
                          _file = File.ReadAllBytes(pfilePath);
                          break;
                      default:
                          break;

                      #endregion
                  }

                  if (imageOriginal != null) imageOriginal.Dispose();

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }

          }

          private void ddlComponentId_SelectedValueChanged_1(object sender, EventArgs e)
          {
              if (ddlComponentId.SelectedValue == null) return;
               OperationResult operationResult = new OperationResult();

               string[] examenForPrint = new string[] 
                { 
                      Constants.AUDIOMETRIA_ID,
                      Constants.ELECTROCARDIOGRAMA_ID,
                      Constants.EVA_CARDIOLOGICA_ID,
                      Constants.PRUEBA_ESFUERZO_ID,
                      Constants.ELECTROENCEFALOGRAMA_ID,
                      Constants.ESPIROMETRIA_ID,
                      Constants.INFORME_LABORATORIO_ID,
                      Constants.ADJUNTOS_PSICOLOGIA,
                      Constants.ECOGRAFIA_ABDOMINAL_ID,
                      Constants.INFORME_ECOGRAFICO_PROSTATA_ID,
                      Constants.ECOGRAFIA_RENAL_ID,
                      Constants.RX_TORAX_ID,
                      Constants.HISTORIA_CLINICA_PSICOLOGICA_ID,
                      Constants.OIT_ID
                };


               var results = BLL.Utils.GetAllComponentsByCategory(ref operationResult, int.Parse(ddlComponentId.SelectedValue.ToString()))
                   .FindAll(p => examenForPrint.Contains(p.Id));

               Utils.LoadDropDownList(cboComponente, "Value1", "Id", results, DropDownListAction.Select);


          }
                     
    }
}
