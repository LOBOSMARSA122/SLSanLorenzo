using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System.IO;
using System.Drawing.Imaging;
using System.Web.UI;
using UserControl = System.Windows.Forms.UserControl;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucFotoTipo : UserControl
    {
        private List<Point> _points = new List<Point>();
        Graphics g;
        FileInfoDto fileInfo = null;
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        bool _isChangueValueControl = false;
        List<ServiceComponentFieldValuesList> _listOfAtencionAdulto1 = new List<ServiceComponentFieldValuesList>();
        ServiceComponentFieldValuesList _UserControlValores = null;
        private Bitmap image;
        private byte[] _imgArray = null;

        #region "------------- Public Events -------------"
        /// <summary>
        /// Se desencadena cada vez que se cambia un valor del examen de Audiometria.
        /// </summary>
        public event EventHandler<AudiometriaAfterValueChangeEventArgs> AfterValueChange;
        protected void OnAfterValueChange(AudiometriaAfterValueChangeEventArgs e)
        {
            if (AfterValueChange != null)
                AfterValueChange(this, e);
        }
        #endregion

        #region "--------------- Properties --------------------"
        public string PersonId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceComponentId { get; set; }

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                SaveImagenFotoTipo();
                //SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_LUNARES, txtNroLunares.Text);
                //SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_MANCHAS, txtNroManchas.Text);
                //SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_PECAS, txtNroPecas.Text);
                //SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_CICATRICES, txtNroCicatrices.Text);

                return _listOfAtencionAdulto1.FindAll(p => p.v_ComponentFieldId != null);
            }
            set
            {
                if (value != _listOfAtencionAdulto1)
                {
                    ClearValueControl();
                    _listOfAtencionAdulto1 = value;
                    SearchControlAndFill(value);
                }
            }
        }

        public void ClearValueControl()
        {
            _isChangueValueControl = false;
        }

        public bool IsChangeValueControl { get { return _isChangueValueControl; } }
        #endregion

        public ucFotoTipo()
        {
            InitializeComponent();
            txtMultimediaFileId.Name = Constants.txt_MULTIMEDIA_FILE_FOTO_TIPO;
            txtServiceComponentMultimediaId.Name = Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_FOTO_TIPO;
        }

        private void SaveValueControlForInterfacingEso(string name, string value)
        {
            #region Capturar Valor del campo

            _listOfAtencionAdulto1.RemoveAll(p => p.v_ComponentFieldId == name);

            _UserControlValores = new ServiceComponentFieldValuesList();

            _UserControlValores.v_ComponentFieldId = name;
            _UserControlValores.v_Value1 = value;
            _UserControlValores.v_ComponentId = Constants.FOTO_TIPO_ID;

            _listOfAtencionAdulto1.Add(_UserControlValores);

            DataSource = _listOfAtencionAdulto1;

            #endregion
        }

        private void SearchControlAndFill(List<ServiceComponentFieldValuesList> dataSource)
        {
            if (dataSource == null || dataSource.Count == 0) return;
            // Ordenar Lista Datasource
            var dataSourceOrdenado = dataSource.FindAll(x => x.v_ComponentFieldId != null).OrderBy(p => p.v_ComponentFieldId).ToList();

            
            // recorrer la lista que viene de la BD
            foreach (var item in dataSourceOrdenado)
            {
                var matchedFields = this.Controls.Find(item.v_ComponentFieldId, true);

                if (matchedFields.Length > 0)
                {
                    var field = matchedFields[0];

                    if (field is TextBox)
                    {
                        if (field.Name == item.v_ComponentFieldId)
                        {
                            ((TextBox)field).Text = item.v_Value1;
                        }
                    }
                }
            }
        }

        private void SaveImagenFotoTipo()
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); //you could ave in BPM, PNG  etc format.
            byte[] picArr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(picArr, 0, picArr.Length);
            ms.Close();
            string[] IDs = null;

            IDs = SavePrepared(txtMultimediaFileId.Text, txtServiceComponentMultimediaId.Text, PersonId, ServiceComponentId, "FOTO TIPO", "IMAGEN PROVENIENTE DE FOTOTIPO", picArr);

            if (IDs != null) //GRABAR
            {
                txtMultimediaFileId.Text = IDs[0];
                txtServiceComponentMultimediaId.Text = IDs[1];

                var audiograma = new List<ServiceComponentFieldValuesList>()
                {                
                    new ServiceComponentFieldValuesList { v_ComponentFieldId = txtMultimediaFileId.Name, v_Value1 = txtMultimediaFileId.Text },                 
                    new ServiceComponentFieldValuesList { v_ComponentFieldId = txtServiceComponentMultimediaId.Name, v_Value1 = txtServiceComponentMultimediaId.Text },                
                };

                _listOfAtencionAdulto1.AddRange(audiograma);
            }
        }

        private string[] SavePrepared(string multimediaFileId, string serviceComponentMultimediaId, string personId, string serviceComponentId, string fileName, string description, byte[] chartImagen)
        {

            string[] IDs = null;

            fileInfo = new FileInfoDto();

            fileInfo.PersonId = personId;
            fileInfo.ServiceComponentId = serviceComponentId;
            fileInfo.FileName = fileName;
            fileInfo.Description = description;
            fileInfo.ByteArrayFile = chartImagen;

            OperationResult operationResult = null;

            if (string.IsNullOrEmpty(multimediaFileId))     // GRABAR
            {
                // Grabar
                operationResult = new OperationResult();
                IDs = _multimediaFileBL.AddMultimediaFileComponent(ref operationResult, fileInfo, Globals.ClientSession.GetAsList());

                // Analizar el resultado de la operación
                if (operationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else        // ACTUALIZAR
            {
                operationResult = new OperationResult();
                fileInfo.MultimediaFileId = multimediaFileId;
                fileInfo.ServiceComponentMultimediaId = serviceComponentMultimediaId;
                _multimediaFileBL.UpdateMultimediaFileComponent(ref operationResult, fileInfo, Globals.ClientSession.GetAsList());

                // Analizar el resultado de la operación
                if (operationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return IDs;

        }
        
        void MainFormLoad(object sender, EventArgs e)
        {
            //txtNroLunares.Name = "N009-FOT00000001";
            //txtNroManchas.Name = "N009-FOT00000002";
            //txtNroPecas.Name = "N009-FOT00000003";
            //txtNroCicatrices.Name = "N009-FOT00000004";
        }

        private void btnPintar_Click(object sender, EventArgs e)
        {
            DrawMethod(Brushes.Blue);
            pictureBox1.Image = _btm;
        }


        int k = 0;
        Point sp = new Point(0, 0);
        Point ep = new Point(0, 0);
        private int cX, cY, x, y, dX, dY;
        int figura;
        Color color;

        private Pen _p;
        private Rectangle _r;
        private Bitmap _btm;
        private Graphics _g;
        public void DrawMethod(Brush brush)
        {
            _p = new Pen(brush);
            _r = new Rectangle(0,0,100,100);
            _btm = new Bitmap(_r.Width + 1, _r.Height + 1);
            _g = Graphics.FromImage(_btm);
            _g.DrawRectangle(_p,_r);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
                
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }



    }
}
