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
                SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_LUNARES, txtNroLunares.Text);
                SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_MANCHAS, txtNroManchas.Text);
                SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_PECAS, txtNroPecas.Text);
                SaveValueControlForInterfacingEso(Constants.FOTO_TIPO_CICATRICES, txtNroCicatrices.Text);

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
            g = panel1.CreateGraphics();
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

            _imgArray = dataSource.Find(p => p.v_ComponentFieldId == null).fotoTipo;

            Stream stream = new MemoryStream(_imgArray);
            panel1.BackgroundImage = Image.FromStream(stream); //.FromFile(@"C:\Users\dev1\Documents\42708422-06032018-FOTO TIPO.IPO.jpg");
            image = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height, PixelFormat.Format32bppArgb);
            
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
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));
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
            txtNroLunares.Name = "N009-FOT00000001";
            txtNroManchas.Name = "N009-FOT00000002";
            txtNroPecas.Name = "N009-FOT00000003";
            txtNroCicatrices.Name = "N009-FOT00000004";
        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:32874/frmCanvasFotipo.aspx");  
        }

        private Bitmap _canvas;
        private void ResizeCanvas()
        {
            Bitmap tmp = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppRgb);
            using (Graphics g = Graphics.FromImage(tmp))
            {
                g.Clear(Color.White);
                if (_canvas != null)
                {
                    g.DrawImage(_canvas, 0, 0);
                    _canvas.Dispose();
                }
            }
            _canvas = tmp;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);
            //e.Graphics.DrawString("sss", Font, new SolidBrush(ForeColor), ClientRectangle); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (Point point in _points)
            {
                using (Pen Haitham = new Pen(Color.Silver, 2))
                {
                    g.FillRectangle(Haitham.Brush, new Rectangle(point.X, point.Y, 50, 50));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _points = new List<Point>();
            Pen Haitham = new Pen(Color.AliceBlue, 2);
            g.FillRectangle(Haitham.Brush, new Rectangle(0, 0, 260, 209));
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            _points.Add(new Point(e.X, e.Y));
            Invalidate(); 
        }
    }
}
