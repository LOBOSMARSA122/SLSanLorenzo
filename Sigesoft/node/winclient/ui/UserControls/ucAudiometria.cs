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

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucAudiometria : UserControl
    {
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

        #region "--------------------Declarations -------------------"

        ServiceComponentFieldValuesList _audiometria;
        List<ServiceComponentFieldValuesList> _listAudiometria = new List<ServiceComponentFieldValuesList>();
        string _valueOld = String.Empty;
        bool _isChangueValueControl = false;
        //Dictionary<string, TextBox> dictionary = new Dictionary<string, TextBox>();
        List<AudiometriaDataForGraphic> AudioODList = new List<AudiometriaDataForGraphic>();
        List<AudiometriaDataForGraphic> AudioOIList = new List<AudiometriaDataForGraphic>();

        List<RecomendationList> _recomendations = null;
        List<RestrictionList> _restrictions = null;
        List<DiagnosticRepositoryList> _dx = null;
        FileInfoDto fileInfo = null;
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        private byte[] _file = null;

        #endregion

        #region "--------------- Properties --------------------"

        public List<ServiceComponentFieldValuesList> DataSource
        {
            get
            {
                SaveImagenAudiograma();
                return _listAudiometria;
            }
            set
            {
                if (value != _listAudiometria)
                {
                    ClearValueControl();
                    _listAudiometria = value;
                    SearchControlAndFill(value);
                }
            }
        }

        public void ClearValueControl()
        {
            _isChangueValueControl = false;

            txt_VA_OD_125.Text = "";
            txt_VA_OD_250.Text = "";
            txt_VA_OD_500.Text = "";
            txt_VA_OD_1000.Text = "";
            txt_VA_OD_2000.Text = "";
            txt_VA_OD_3000.Text = "";
            txt_VA_OD_4000.Text = "";
            txt_VA_OD_6000.Text = "";
            txt_VA_OD_8000.Text = "";

            //Vía Ósea OD
            txt_VO_OD_125.Text = "";
            txt_VO_OD_250.Text = "";
            txt_VO_OD_500.Text = "";
            txt_VO_OD_1000.Text = "";
            txt_VO_OD_2000.Text = "";
            txt_VO_OD_3000.Text = "";
            txt_VO_OD_4000.Text = "";
            txt_VO_OD_6000.Text = "";
            txt_VO_OD_8000.Text = "";

            //Vía Aérea OI
            txt_VA_OI_125.Text = "";
            txt_VA_OI_250.Text = "";
            txt_VA_OI_500.Text = "";
            txt_VA_OI_1000.Text = "";
            txt_VA_OI_2000.Text = "";
            txt_VA_OI_3000.Text = "";
            txt_VA_OI_4000.Text = "";
            txt_VA_OI_6000.Text = "";
            txt_VA_OI_8000.Text = "";

            //Vía Ósea OI
            txt_VO_OI_125.Text = "";
            txt_VO_OI_250.Text = "";
            txt_VO_OI_500.Text = "";
            txt_VO_OI_1000.Text = "";
            txt_VO_OI_2000.Text = "";
            txt_VO_OI_3000.Text = "";
            txt_VO_OI_4000.Text = "";
            txt_VO_OI_6000.Text = "";
            txt_VO_OI_8000.Text = "";

            //Enamascaramiento OD 
            txt_EM_OD_125.Text = "";
            txt_EM_OD_250.Text = "";
            txt_EM_OD_500.Text = "";
            txt_EM_OD_1000.Text = "";
            txt_EM_OD_2000.Text = "";
            txt_EM_OD_3000.Text = "";
            txt_EM_OD_4000.Text = "";
            txt_EM_OD_6000.Text = "";
            txt_EM_OD_8000.Text = "";

            //Enamascaramiento OI
            txt_EM_OI_125.Text = "";
            txt_EM_OI_250.Text = "";
            txt_EM_OI_500.Text = "";
            txt_EM_OI_1000.Text = "";
            txt_EM_OI_2000.Text = "";
            txt_EM_OI_3000.Text = "";
            txt_EM_OI_4000.Text = "";
            txt_EM_OI_6000.Text = "";
            txt_EM_OI_8000.Text = "";

        }

        public bool IsChangeValueControl { get { return _isChangueValueControl; } }

        public string ServiceComponentFieldsId { get; set; }
        public string ServiceComponentId { get; set; }
        public string ServiceComponentFieldValuesId { get; set; }

        public string PersonId { get; set; }



        #endregion

        #region "---------------- Custom Entities ------------"

        private class Audio
        {
            public string TagGrupo { get; set; }
            public int TagOrden { get; set; }
        }

        public class AudiometriaDataForGraphic
        {
            public string SeriesName { get; set; }
            public double? _125Hz { get; set; }
            public double? _250Hz { get; set; }
            public double? _500Hz { get; set; }
            public double? _1000Hz { get; set; }
            public double? _2000Hz { get; set; }
            public double? _3000Hz { get; set; }
            public double? _4000Hz { get; set; }
            public double? _6000Hz { get; set; }
            public double? _8000Hz { get; set; }
        }

        public class AudiometriaLine25
        {
            public string SeriesName { get; set; }
            public double? _125Hz { get; set; }
            public double? _250Hz { get; set; }
            public double? _500Hz { get; set; }
            public double? _1000Hz { get; set; }
            public double? _2000Hz { get; set; }
            public double? _3000Hz { get; set; }
            public double? _4000Hz { get; set; }
            public double? _6000Hz { get; set; }
            public double? _8000Hz { get; set; }
        }

        public class AudiometriaLine50
        {
            public string SeriesName { get; set; }
            public double? _125Hz { get; set; }
            public double? _250Hz { get; set; }
            public double? _500Hz { get; set; }
            public double? _1000Hz { get; set; }
            public double? _2000HZ { get; set; }
            public double? _3000Hz { get; set; }
            public double? _4000Hz { get; set; }
            public double? _6000Hz { get; set; }
            public double? _8000Hz { get; set; }
        }

        #endregion

        #region "---------------- Constructor ---------------------------"

        public ucAudiometria()
        {
            InitializeComponent();

            //Vía Aérea OD
            txt_VA_OD_125.Name = Constants.txt_VA_OD_125;
            Audio o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 43;
            txt_VA_OD_125.Tag = o;

            txt_VA_OD_250.Name = Constants.txt_VA_OD_250;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 44;
            txt_VA_OD_250.Tag = o;

            txt_VA_OD_500.Name = Constants.txt_VA_OD_500;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 1;
            txt_VA_OD_500.Tag = o;

            txt_VA_OD_1000.Name = Constants.txt_VA_OD_1000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 2;
            txt_VA_OD_1000.Tag = o;

            txt_VA_OD_2000.Name = Constants.txt_VA_OD_2000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 3;
            txt_VA_OD_2000.Tag = o;

            txt_VA_OD_3000.Name = Constants.txt_VA_OD_3000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 4;
            txt_VA_OD_3000.Tag = o;

            txt_VA_OD_4000.Name = Constants.txt_VA_OD_4000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 5;
            txt_VA_OD_4000.Tag = o;

            txt_VA_OD_6000.Name = Constants.txt_VA_OD_6000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 6;
            txt_VA_OD_6000.Tag = o;

            txt_VA_OD_8000.Name = Constants.txt_VA_OD_8000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 7;
            txt_VA_OD_8000.Tag = o;


            //Vía Ósea OD
            txt_VO_OD_125.Name = Constants.txt_VO_OD_125;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 45;
            txt_VO_OD_125.Tag = o;

            txt_VO_OD_250.Name = Constants.txt_VO_OD_250;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 46;
            txt_VO_OD_250.Tag = o;

            txt_VO_OD_500.Name = Constants.txt_VO_OD_500;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 8;
            txt_VO_OD_500.Tag = o;

            txt_VO_OD_1000.Name = Constants.txt_VO_OD_1000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 9;
            txt_VO_OD_1000.Tag = o;

            txt_VO_OD_2000.Name = Constants.txt_VO_OD_2000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 10;
            txt_VO_OD_2000.Tag = o;

            txt_VO_OD_3000.Name = Constants.txt_VO_OD_3000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 11;
            txt_VO_OD_3000.Tag = o;

            txt_VO_OD_4000.Name = Constants.txt_VO_OD_4000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 12;
            txt_VO_OD_4000.Tag = o;

            txt_VO_OD_6000.Name = Constants.txt_VO_OD_6000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 13;
            txt_VO_OD_6000.Tag = o;

            txt_VO_OD_8000.Name = Constants.txt_VO_OD_8000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 14;
            txt_VO_OD_8000.Tag = o;


            //Vía Aérea OI
            txt_VA_OI_125.Name = Constants.txt_VA_OI_125;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 47;
            txt_VA_OI_125.Tag = o;

            txt_VA_OI_250.Name = Constants.txt_VA_OI_250;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 48;
            txt_VA_OI_250.Tag = o;

            txt_VA_OI_500.Name = Constants.txt_VA_OI_500;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 15;
            txt_VA_OI_500.Tag = o;

            txt_VA_OI_1000.Name = Constants.txt_VA_OI_1000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 16;
            txt_VA_OI_1000.Tag = o;

            txt_VA_OI_2000.Name = Constants.txt_VA_OI_2000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 17;
            txt_VA_OI_2000.Tag = o;

            txt_VA_OI_3000.Name = Constants.txt_VA_OI_3000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 18;
            txt_VA_OI_3000.Tag = o;

            txt_VA_OI_4000.Name = Constants.txt_VA_OI_4000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 19;
            txt_VA_OI_4000.Tag = o;

            txt_VA_OI_6000.Name = Constants.txt_VA_OI_6000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 20;
            txt_VA_OI_6000.Tag = o;

            txt_VA_OI_8000.Name = Constants.txt_VA_OI_8000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 21;
            txt_VA_OI_8000.Tag = o;


            //Vía Ósea OI
            txt_VO_OI_125.Name = Constants.txt_VO_OI_125;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 49;
            txt_VO_OI_125.Tag = o;

            txt_VO_OI_250.Name = Constants.txt_VO_OI_250;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 50;
            txt_VO_OI_250.Tag = o;

            txt_VO_OI_500.Name = Constants.txt_VO_OI_500;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 22;
            txt_VO_OI_500.Tag = o;

            txt_VO_OI_1000.Name = Constants.txt_VO_OI_1000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 23;
            txt_VO_OI_1000.Tag = o;

            txt_VO_OI_2000.Name = Constants.txt_VO_OI_2000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 24;
            txt_VO_OI_2000.Tag = o;

            txt_VO_OI_3000.Name = Constants.txt_VO_OI_3000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 25;
            txt_VO_OI_3000.Tag = o;

            txt_VO_OI_4000.Name = Constants.txt_VO_OI_4000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 26;
            txt_VO_OI_4000.Tag = o;

            txt_VO_OI_6000.Name = Constants.txt_VO_OI_6000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 27;
            txt_VO_OI_6000.Tag = o;

            txt_VO_OI_8000.Name = Constants.txt_VO_OI_8000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 28;
            txt_VO_OI_8000.Tag = o;

            //ENMASCARAMIENTO OD
            txt_EM_OD_125.Name = Constants.txt_EM_OD_125;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 51;
            txt_EM_OD_125.Tag = o;

            txt_EM_OD_250.Name = Constants.txt_EM_OD_250;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 52;
            txt_EM_OD_250.Tag = o;

            txt_EM_OD_500.Name = Constants.txt_EM_OD_500;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 29;
            txt_EM_OD_500.Tag = o;

            txt_EM_OD_1000.Name = Constants.txt_EM_OD_1000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 30;
            txt_EM_OD_1000.Tag = o;

            txt_EM_OD_2000.Name = Constants.txt_EM_OD_2000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 31;
            txt_EM_OD_2000.Tag = o;

            txt_EM_OD_3000.Name = Constants.txt_EM_OD_3000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 32;
            txt_EM_OD_3000.Tag = o;

            txt_EM_OD_4000.Name = Constants.txt_EM_OD_4000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 33;
            txt_EM_OD_4000.Tag = o;

            txt_EM_OD_6000.Name = Constants.txt_EM_OD_6000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 34;
            txt_EM_OD_6000.Tag = o;

            txt_EM_OD_8000.Name = Constants.txt_EM_OD_8000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 35;
            txt_EM_OD_8000.Tag = o;

            //ENMASCARAMIENTO OI
            txt_EM_OI_125.Name = Constants.txt_EM_OI_125;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 53;
            txt_EM_OI_125.Tag = o;

            txt_EM_OI_250.Name = Constants.txt_EM_OI_250;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 54;
            txt_EM_OI_250.Tag = o;

            txt_EM_OI_500.Name = Constants.txt_EM_OI_500;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 36;
            txt_EM_OI_500.Tag = o;

            txt_EM_OI_1000.Name = Constants.txt_EM_OI_1000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 37;
            txt_EM_OI_1000.Tag = o;

            txt_EM_OI_2000.Name = Constants.txt_EM_OI_2000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 38;
            txt_EM_OI_2000.Tag = o;

            txt_EM_OI_3000.Name = Constants.txt_EM_OI_3000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 39;
            txt_EM_OI_3000.Tag = o;

            txt_EM_OI_4000.Name = Constants.txt_EM_OI_4000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 40;
            txt_EM_OI_4000.Tag = o;

            txt_EM_OI_6000.Name = Constants.txt_EM_OI_6000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 41;
            txt_EM_OI_6000.Tag = o;

            txt_EM_OI_8000.Name = Constants.txt_EM_OI_8000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 42;
            txt_EM_OI_8000.Tag = o;



            //ANACUSIA OD
            txt_AN_OD_125.Name = Constants.txt_AN_OD_125;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 55;
            txt_AN_OD_125.Tag = o;

            txt_AN_OD_250.Name = Constants.txt_AN_OD_250;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 56;
            txt_AN_OD_250.Tag = o;

            txt_AN_OD_500.Name = Constants.txt_AN_OD_500;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 57;
            txt_AN_OD_500.Tag = o;

            txt_AN_OD_1000.Name = Constants.txt_AN_OD_1000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 58;
            txt_AN_OD_1000.Tag = o;

            txt_AN_OD_2000.Name = Constants.txt_AN_OD_2000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 59;
            txt_AN_OD_2000.Tag = o;

            txt_AN_OD_3000.Name = Constants.txt_AN_OD_3000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 60;
            txt_AN_OD_3000.Tag = o;

            txt_AN_OD_4000.Name = Constants.txt_AN_OD_4000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 61;
            txt_AN_OD_4000.Tag = o;

            txt_AN_OD_6000.Name = Constants.txt_AN_OD_6000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 62;
            txt_AN_OD_6000.Tag = o;

            txt_AN_OD_8000.Name = Constants.txt_AN_OD_8000;
            o = new Audio();
            o.TagGrupo = "OD";
            o.TagOrden = 63;
            txt_AN_OD_8000.Tag = o;




            //ANACUSIA OI
            txt_AN_OI_125.Name = Constants.txt_AN_OI_125;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 64;
            txt_AN_OI_125.Tag = o;

            txt_AN_OI_250.Name = Constants.txt_AN_OI_250;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 65;
            txt_AN_OI_250.Tag = o;

            txt_AN_OI_500.Name = Constants.txt_AN_OI_500;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 66;
            txt_AN_OI_500.Tag = o;

            txt_AN_OI_1000.Name = Constants.txt_AN_OI_1000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 67;
            txt_AN_OI_1000.Tag = o;

            txt_AN_OI_2000.Name = Constants.txt_AN_OI_2000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 68;
            txt_AN_OI_2000.Tag = o;

            txt_AN_OI_3000.Name = Constants.txt_AN_OI_3000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 69;
            txt_AN_OI_3000.Tag = o;

            txt_AN_OI_4000.Name = Constants.txt_AN_OI_4000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 70;
            txt_AN_OI_4000.Tag = o;

            txt_AN_OI_6000.Name = Constants.txt_AN_OI_6000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 71;
            txt_AN_OI_6000.Tag = o;

            txt_AN_OI_8000.Name = Constants.txt_AN_OI_8000;
            o = new Audio();
            o.TagGrupo = "OI";
            o.TagOrden = 72;
            txt_AN_OI_8000.Tag = o;



































            // DX AUTO => OCUPACIONAL / CLINICO

            txtDxOcupacionalOD.Name = Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OD;
            //o = new Audio();
            //o.TagGrupo = "OD";
            //o.TagOrden = 29;
            //txtDxOcupacionalOD.Tag = o;

            txtDxOcupacionalOI.Name = Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OI;
            //o = new Audio();
            //o.TagGrupo = "OI";
            //o.TagOrden = 30;
            //txtDxOcupacionalOI.Tag = o;

            txtDxClinico_OD.Name = Constants.txt_AUD_DX_CLINICO_AUTO_OD;
            //o = new Audio();
            //o.TagGrupo = "OD";
            //o.TagOrden = 31;
            //txtDxClinico_OD.Tag = o;

            txtDxClinico_OI.Name = Constants.txt_AUD_DX_CLINICO_AUTO_OI;
            //o = new Audio();
            //o.TagGrupo = "OI";
            //o.TagOrden = 32;
            //txtDxClinico_OI.Tag = o;

            //
            txtMultimediaFileId_OD.Name = Constants.txt_MULTIMEDIA_FILE_OD;
            txtMultimediaFileId_OI.Name = Constants.txt_MULTIMEDIA_FILE_OI;

            //
            txtServiceComponentMultimediaId_OD.Name = Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_OD;
            txtServiceComponentMultimediaId_OI.Name = Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_OI;

            SearchControlAndSetEvents(this);
        }

        #endregion

        #region "----------------------- Private Methods -----------------------"

        private void ShowGraphicOI()
        {
            double? Vacio = null;
            AudioOIList = new List<AudiometriaDataForGraphic>() 
            { 
                #region Fill Entities for Graphic

                 //b=(checkBox1.Checked==true ? true : false);
                new AudiometriaDataForGraphic { SeriesName = "Via Aerea OI", 
                                                _125Hz  = txt_VA_OI_125.Text == string.Empty  ? Vacio :double.Parse(txt_VA_OI_125.Text), 
                                                _250Hz = txt_VA_OI_250.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_250.Text),
                                                _500Hz  = txt_VA_OI_500.Text == string.Empty  ? Vacio :double.Parse(txt_VA_OI_500.Text), 
                                                _1000Hz = txt_VA_OI_1000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_1000.Text),
                                                _2000Hz = txt_VA_OI_2000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_2000.Text),
                                                _3000Hz = txt_VA_OI_3000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_3000.Text),
                                                _4000Hz = txt_VA_OI_4000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_4000.Text),
                                                _6000Hz = txt_VA_OI_6000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_6000.Text),
                                                _8000Hz = txt_VA_OI_8000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OI_8000.Text)
                                              },

                new AudiometriaDataForGraphic { SeriesName = "Via Osea OI", 
                                                _125Hz  = txt_VO_OI_125.Text == string.Empty  ? Vacio : double.Parse(txt_VO_OI_125.Text) , 
                                                _250Hz = txt_VO_OI_250.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_250.Text),
                                                _500Hz  = txt_VO_OI_500.Text == string.Empty  ? Vacio : double.Parse(txt_VO_OI_500.Text) , 
                                                _1000Hz = txt_VO_OI_1000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_1000.Text),
                                                _2000Hz = txt_VO_OI_2000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_2000.Text),
                                                _3000Hz = txt_VO_OI_3000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_3000.Text),
                                                _4000Hz = txt_VO_OI_4000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_4000.Text),
                                                _6000Hz = txt_VO_OI_6000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_6000.Text),
                                                _8000Hz = txt_VO_OI_8000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OI_8000.Text)
                                              },        

                new AudiometriaDataForGraphic { SeriesName = "Enmascaramiento OI", 
                                                 _125Hz  = txt_EM_OI_125.Text == string.Empty  ? Vacio : double.Parse(txt_EM_OI_125.Text) , 
                                                _250Hz = txt_EM_OI_250.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_250.Text),
                                                _500Hz  = txt_EM_OI_500.Text == string.Empty  ? Vacio : double.Parse(txt_EM_OI_500.Text) , 
                                                _1000Hz = txt_EM_OI_1000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_1000.Text),
                                                _2000Hz = txt_EM_OI_2000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_2000.Text),
                                                _3000Hz = txt_EM_OI_3000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_3000.Text),
                                                _4000Hz = txt_EM_OI_4000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_4000.Text),
                                                _6000Hz = txt_EM_OI_6000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_6000.Text),
                                                _8000Hz = txt_EM_OI_8000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OI_8000.Text)
                                              }      ,
              new AudiometriaDataForGraphic { SeriesName = "Anacusia OI", 
                                                _125Hz = txt_AN_OI_125.Text == string.Empty  ? Vacio : double.Parse(txt_AN_OI_125.Text), 
                                                _250Hz = txt_AN_OI_250.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_250.Text),
                                                _500Hz  = txt_AN_OI_500.Text == string.Empty  ? Vacio : double.Parse(txt_AN_OI_500.Text) , 
                                                _1000Hz = txt_AN_OI_1000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_1000.Text),
                                                _2000Hz = txt_AN_OI_2000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_2000.Text),
                                                _3000Hz = txt_AN_OI_3000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_3000.Text),
                                                _4000Hz = txt_AN_OI_4000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_4000.Text),
                                                _6000Hz = txt_AN_OI_6000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_6000.Text),
                                                _8000Hz = txt_AN_OI_8000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OI_8000.Text)
                                              }    
                #endregion       
            };

            List<AudiometriaLine25> LineList25 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 25

                new AudiometriaLine25 { SeriesName = "Line25",
                                      _500Hz = 25,
                                      _1000Hz = 25,
                                      _2000Hz = 25,
                                      _3000Hz = 25,
                                      _4000Hz = 25,
                                      _6000Hz = 25,
                                      _8000Hz = 25
                                    }
                #endregion
            };

            List<AudiometriaLine25> LineList50 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 50

                new AudiometriaLine25 { SeriesName = "Line50",
                                      _500Hz = 0,
                                      _1000Hz = 0,
                                      _2000Hz = 0,
                                      _3000Hz = 0,
                                      _4000Hz = 0,
                                      _6000Hz = 0,
                                      _8000Hz = 0
                                    }
                #endregion
            };

            chart2.Series.Clear();

            var propertyInfo = AudioOIList.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine25 = LineList25.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine50 = LineList50.GetType().GetGenericArguments()[0].GetProperties();

            foreach (var row in AudioOIList)
            {
                #region Set cruz.png / mayorazul.png

                // for each Row, add a new series
                string seriesName = row.SeriesName;
                chart2.Series.Add(seriesName);
                chart2.Series[seriesName].ChartType = SeriesChartType.Line;
                chart2.Series[seriesName].BorderWidth = 2;
                chart2.Series[seriesName].ShadowOffset = 2;

                //if (seriesName == "Via Aerea OI")
                //{
                //    chart2.Series[seriesName].MarkerImage = Application.StartupPath + @"\Resources\cruz.png";
                //    chart2.Series[seriesName].Color = Color.Blue;
                //}
                //else if (seriesName == "Via Osea OI")
                //{
                //    chart2.Series[seriesName].MarkerImage = Application.StartupPath + @"\Resources\mayorazul.png";
                //    chart2.Series[seriesName].Color = Color.Transparent;
                //}

                for (int colIndex = 1; colIndex < propertyInfo.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfo[colIndex].Name;
                    object YVal = AudioOIList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    //if (YVal.ToString() != "0")
                    //{
                    chart2.Series[seriesName].Points.AddXY(columnName, YVal);
                    //}

                    if (YVal == null)
                    {
                        
                    }
                    //else if (YVal.ToString() == "0")
                    //{
                    //    chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = "";
                    //    chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Blue;
                    //}
                    else
                    {
                        if (seriesName == "Via Aerea OI")
                        {
                            chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\cruz.png";                            
                            chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Blue;
                            chart2.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
                        }
                        else if (seriesName == "Via Osea OI")
                        {
                            chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\mayorazul.png";
                            chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Enmascaramiento OI")
                        {
                            chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\CorcheteAzul.png";
                            chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Anacusia OI")
                        {
                            chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\cuadradoazul.png";
                            chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                    }

                }

                #endregion
            }

            foreach (var row in LineList25)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                chart2.Series.Add(seriesName);
                chart2.Series[seriesName].ChartType = SeriesChartType.Line;
                chart2.Series[seriesName].BorderWidth = 1;
                //chart2.Series[seriesName].ShadowOffset = 2;
                chart2.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine25.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine25[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    chart2.Series[seriesName].Points.AddXY(columnName, 25);
                }
            }

            foreach (var row in LineList50)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                chart2.Series.Add(seriesName);
                chart2.Series[seriesName].ChartType = SeriesChartType.Line;
                chart2.Series[seriesName].BorderWidth = 1;
                //chart2.Series[seriesName].ShadowOffset = 2;
                chart2.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine50.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine50[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    chart2.Series[seriesName].Points.AddXY(columnName, 55);
                }
            }
        }

        private void ShowGraphicOD()
        {                 
            double? Vacio = null;
            AudioODList = new List<AudiometriaDataForGraphic>() 
            { 
                #region Fill Entities for Graphic
		 	           
               
                 //b=(checkBox1.Checked==true ? true : false);
    
                new AudiometriaDataForGraphic { SeriesName = "Via Aerea OD", 
                                                _125Hz  = txt_VA_OD_125.Text == string.Empty  ? Vacio :double.Parse(txt_VA_OD_125.Text), 
                                                _250Hz = txt_VA_OD_250.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_250.Text),
                                                _500Hz  = txt_VA_OD_500.Text == string.Empty  ? Vacio :double.Parse(txt_VA_OD_500.Text), 
                                                _1000Hz = txt_VA_OD_1000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_1000.Text),
                                                _2000Hz = txt_VA_OD_2000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_2000.Text),
                                                _3000Hz = txt_VA_OD_3000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_3000.Text),
                                                _4000Hz = txt_VA_OD_4000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_4000.Text),
                                                _6000Hz = txt_VA_OD_6000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_6000.Text),
                                                _8000Hz = txt_VA_OD_8000.Text == string.Empty ? Vacio : double.Parse(txt_VA_OD_8000.Text)
                                              },
                new AudiometriaDataForGraphic { SeriesName = "Via Osea OD", 
                                                _125Hz  = txt_VO_OD_125.Text == string.Empty  ? Vacio : double.Parse(txt_VO_OD_125.Text) , 
                                                _250Hz = txt_VO_OD_250.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_250.Text),
                                                _500Hz  = txt_VO_OD_500.Text == string.Empty  ? Vacio : double.Parse(txt_VO_OD_500.Text) , 
                                                _1000Hz = txt_VO_OD_1000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_1000.Text),
                                                _2000Hz = txt_VO_OD_2000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_2000.Text),
                                                _3000Hz = txt_VO_OD_3000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_3000.Text),
                                                _4000Hz = txt_VO_OD_4000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_4000.Text),
                                                _6000Hz = txt_VO_OD_6000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_6000.Text),
                                                _8000Hz = txt_VO_OD_8000.Text == string.Empty ? Vacio : double.Parse(txt_VO_OD_8000.Text)
                                              },
            new AudiometriaDataForGraphic { SeriesName = "Enmascaramiento OD", 
                                                _125Hz = txt_EM_OD_125.Text == string.Empty  ? Vacio : double.Parse(txt_EM_OD_125.Text), 
                                                _250Hz = txt_EM_OD_250.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_250.Text),
                                                _500Hz  = txt_EM_OD_500.Text == string.Empty  ? Vacio : double.Parse(txt_EM_OD_500.Text) , 
                                                _1000Hz = txt_EM_OD_1000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_1000.Text),
                                                _2000Hz = txt_EM_OD_2000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_2000.Text),
                                                _3000Hz = txt_EM_OD_3000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_3000.Text),
                                                _4000Hz = txt_EM_OD_4000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_4000.Text),
                                                _6000Hz = txt_EM_OD_6000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_6000.Text),
                                                _8000Hz = txt_EM_OD_8000.Text == string.Empty ? Vacio : double.Parse(txt_EM_OD_8000.Text)
                                              },    

            new AudiometriaDataForGraphic { SeriesName = "Anacusia OD", 
                                                _125Hz = txt_AN_OD_125.Text == string.Empty  ? Vacio : double.Parse(txt_AN_OD_125.Text), 
                                                _250Hz = txt_AN_OD_250.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_250.Text),
                                                _500Hz  = txt_AN_OD_500.Text == string.Empty  ? Vacio : double.Parse(txt_AN_OD_500.Text) , 
                                                _1000Hz = txt_AN_OD_1000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_1000.Text),
                                                _2000Hz = txt_AN_OD_2000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_2000.Text),
                                                _3000Hz = txt_AN_OD_3000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_3000.Text),
                                                _4000Hz = txt_AN_OD_4000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_4000.Text),
                                                _6000Hz = txt_AN_OD_6000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_6000.Text),
                                                _8000Hz = txt_AN_OD_8000.Text == string.Empty ? Vacio : double.Parse(txt_AN_OD_8000.Text)
                                              }    
                #endregion     
            };

            List<AudiometriaLine25> LineList25 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 25

                new AudiometriaLine25 { SeriesName = "Line25",
                                       _125Hz = 25,
                                      _250Hz = 25, 
                                      _500Hz = 25,
                                      _1000Hz = 25,
                                      _2000Hz = 25,
                                      _3000Hz = 25,
                                      _4000Hz = 25,
                                      _6000Hz = 25,
                                      _8000Hz = 25
                                    }
                #endregion
            };

            List<AudiometriaLine25> LineList50 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 50

                new AudiometriaLine25 { SeriesName = "Line50",
                                       _125Hz = 55,
                                      _250Hz = 55,
                                      _500Hz = 55,
                                      _1000Hz = 55,
                                      _2000Hz = 55,
                                      _3000Hz = 55,
                                      _4000Hz = 55,
                                      _6000Hz = 55,
                                      _8000Hz = 55
                                    }
                #endregion
            };

            chart1.Series.Clear();

            var propertyInfo = AudioODList.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine25 = LineList25.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine50 = LineList50.GetType().GetGenericArguments()[0].GetProperties();

            foreach (var row in AudioODList)
            {
                #region Set circulo.png / menorrojo.png

                // for each Row, add a new series
                string seriesName = row.SeriesName;
                chart1.Series.Add(seriesName);
                chart1.Series[seriesName].ChartType = SeriesChartType.Line;
                chart1.Series[seriesName].BorderWidth = 2;
                chart1.Series[seriesName].ShadowOffset = 2;

                //if (seriesName == "Via Aerea OD")
                //{
                //    chart1.Series[seriesName].MarkerImage = Application.StartupPath + @"\Resources\circulo.png";
                //    chart1.Series[seriesName].Color = Color.Red;
                //}
                //else if (seriesName == "Via Osea OD")
                //{
                //    chart1.Series[seriesName].MarkerImage = Application.StartupPath + @"\Resources\menorrojo.png";
                //    chart1.Series[seriesName].Color = Color.Transparent;
                //}
                //_chart.Series[0].BorderDashStyle = ChartDashStyle.Dash;

                for (int colIndex = 1; colIndex < propertyInfo.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfo[colIndex].Name;
                    object YVal = AudioODList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    //if (YVal.ToString() != "0")
                    //{
                    chart1.Series[seriesName].Points.AddXY(columnName, YVal);
                    //}

                    if (YVal == null)
                    {
                        
                    }
                   //else if (YVal.ToString() == "0")
                   // {
                   //     chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = "";
                   //     chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Red;
                   // }
                    else
                    {
                        if (seriesName == "Via Aerea OD")
                        {
                            chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\circulo.png";
                            chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Red;
                            chart1.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
                        }
                        else if (seriesName == "Via Osea OD")
                        {
                            chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\menorrojo.png";
                            chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Enmascaramiento OD")
                        {
                            chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\CorcheteRojo.png";
                            chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Anacusia OD")
                        {
                            chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = Application.StartupPath + @"\Resources\triangulorojo.png";
                            chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                    }

                }

                #endregion
            }

            foreach (var row in LineList25)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                chart1.Series.Add(seriesName);
                chart1.Series[seriesName].ChartType = SeriesChartType.Line;
                chart1.Series[seriesName].BorderWidth = 1;
                //chart1.Series[seriesName].ShadowOffset = 2;
                chart1.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine25.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine25[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    chart1.Series[seriesName].Points.AddXY(columnName, 25);
                }
            }

            foreach (var row in LineList50)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                chart1.Series.Add(seriesName);
                chart1.Series[seriesName].ChartType = SeriesChartType.Line;
                chart1.Series[seriesName].BorderWidth = 1;
                //chart1.Series[seriesName].ShadowOffset = 2;
                chart1.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine50.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine50[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    chart1.Series[seriesName].Points.AddXY(columnName, 55);
                }
            }

        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    var field = (TextBox)ctrl;
                    var tag = (Audio)field.Tag;

                    if (tag != null)
                    {
                        if (tag.TagGrupo == "OD")
                        {
                            ctrl.TextChanged += new EventHandler(txt_TextChanged_OD);
                            //ctrl.KeyPress += new KeyPressEventHandler(txt_KeyPress_OD);
                            ctrl.Validating += new CancelEventHandler(txt_Validating_OD);
                            ctrl.Enter += new EventHandler(txt_Enter_OD);
                            ((TextBox)ctrl).Leave += new EventHandler(txt_Leave_OD);

                        }
                        else if (tag.TagGrupo == "OI")
                        {
                            ctrl.TextChanged += new EventHandler(txt_TextChanged_OI);
                            //ctrl.KeyPress += new KeyPressEventHandler(txt_KeyPress_OI);
                            ctrl.Validating += new CancelEventHandler(txt_Validating_OI);
                            ctrl.Enter += new EventHandler(txt_Enter_OI);
                            ((TextBox)ctrl).Leave += new EventHandler(txt_Leave_OI);

                        }
                    }
                    //agrego al diccionario los textbox para tenerlo en memoria
                    //dictionary.Add(ctrl.Name, (TextBox)ctrl);
                }

                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);
            }
        }

        private void SearchControlAndFill(List<ServiceComponentFieldValuesList> DataSource)
        {
            if (DataSource == null || DataSource.Count == 0) return;
            // Ordenar Lista Datasource
            var DataSourceOrdenado = DataSource.OrderBy(p => p.v_ComponentFieldId).ToList();

            // recorrer la lista que viene de la BD
            foreach (var item in DataSourceOrdenado)
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

        private void Average()
        {
            double promFC = 0;
            double promNFC = 0;

            promFC = Math.Abs(
                                ((txt_VA_OD_500.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_500.Text))
                               + (txt_VA_OD_1000.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_1000.Text))
                               + (txt_VA_OD_2000.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_2000.Text))
                               + (txt_VA_OD_3000.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_3000.Text)))
                               -
                                ((txt_VA_OI_500.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_500.Text))
                               + (txt_VA_OI_1000.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_1000.Text))
                               + (txt_VA_OI_2000.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_2000.Text))
                               + (txt_VA_OI_3000.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_3000.Text)))
                            ) / 4;

            promNFC = Math.Abs(
                                ((txt_VA_OD_4000.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_4000.Text))
                               + (txt_VA_OD_6000.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_6000.Text))
                               + (txt_VA_OD_8000.Text == string.Empty ? 0 : int.Parse(txt_VA_OD_8000.Text)))
                               -
                               ((txt_VA_OI_4000.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_4000.Text))
                               + (txt_VA_OI_6000.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_6000.Text))
                               + (txt_VA_OI_8000.Text == string.Empty ? 0 : int.Parse(txt_VA_OI_8000.Text)))
                            ) / 3;
        }

        private void CallMe()
        {
            //DxAutomatic();
        }

        // Alejandro
        private DiagnosticRepositoryList SearchDxSugeridoOfSystem(string dx, string oido, string componentFieldsId)
        {
            DiagnosticRepositoryList diagnosticRepository = null;
            // Buscar reco / res asociadas a un dx

            var newDxName = string.Empty;

            if (dx == "Otras Alteraciones No debidas a ruído.")
            {
                string diseasesName = string.Empty;

                if (oido == "Oído Derecho")
                {
                    diseasesName = txtDxClinico_OD.Text;
                }
                else if (oido == "Oído Izquierdo")
                {
                    diseasesName = txtDxClinico_OI.Text;
                }

                newDxName = string.Format("Otras Alteraciones No debidas a ruído {0} - {1}", oido, diseasesName);
            }
            else
            {
                newDxName = string.Format("{0} {1}", dx, oido);
            }


            var diagnostic = GetDxByName(newDxName);

            if (diagnostic != null)
            {
                // Insertar DX sugerido (automático) a la bolsa de DX 
                diagnosticRepository = new DiagnosticRepositoryList();

                diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                diagnosticRepository.v_DiseasesId = diagnostic.v_DiseasesId;
                diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;
                diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;
                diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.SinCalificar;
                diagnosticRepository.v_ServiceId = "";
                diagnosticRepository.v_ComponentId = Constants.AUDIOMETRIA_ID;
                diagnosticRepository.v_DiseasesName = diagnostic.v_DiseasesName;
                diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                diagnosticRepository.v_RestrictionsName = string.Join(", ", diagnostic.Restrictions.Select(p => p.v_RestrictionName));
                diagnosticRepository.v_RecomendationsName = string.Join(", ", diagnostic.Recomendations.Select(p => p.v_RecommendationName));
                diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";

                // ID enlace DX automatico para grabar valores dinamicos
                //diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                diagnosticRepository.v_ComponentFieldsId = componentFieldsId;
                diagnosticRepository.Recomendations = diagnostic.Recomendations;
                diagnosticRepository.Restrictions = diagnostic.Restrictions;
                diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                diagnosticRepository.i_RecordType = (int)RecordType.Temporal;


                //diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);
            }
            else
            {

            }

            return diagnosticRepository;
        }

        // Alejandro
        private void LoadDxAndRecoAndRes()
        {

            _recomendations = new List<RecomendationList>()
            {
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000340", v_DiseasesId = Constants.NORMOACUSIA_UC_AUDIOMETRIA_OIDO_IZQUIERDO, v_RecommendationName = "Control Audiológico Anual" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000340", v_DiseasesId = Constants.NORMOACUSIA_UC_AUDIOMETRIA_OIDO_DERECHO, v_RecommendationName = "Control Audiológico Anual" },

                new RecomendationList { v_MasterRecommendationId = "N009-MR000000331", v_DiseasesId = Constants.TRAUMA_ACUSTICO_LEVE_OD, v_RecommendationName = "Uso de protección auditiva en zonas de ruido superior al LMP" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000331", v_DiseasesId = Constants.TRAUMA_ACUSTICO_AVANZADO_OD, v_RecommendationName = "Uso de protección auditiva en zonas de ruido superior al LMP" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000332", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_LEVE_OD, v_RecommendationName = "Uso estricto de protección auditiva en zonas de ruido superior al NA" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000333", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_MODERADA_OD, v_RecommendationName = "Uso estricto de protección auditiva en zonas de ruido superior al NA Control audiométrico en 6 meses" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000333", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_AVANZADA_OD, v_RecommendationName = "Uso estricto de protección auditiva en zonas de ruido superior al NA Control audiométrico en 6 meses" },

                new RecomendationList { v_MasterRecommendationId = "N009-MR000000331", v_DiseasesId = Constants.TRAUMA_ACUSTICO_LEVE_OI, v_RecommendationName = "Uso de protección auditiva en zonas de ruido superior al LMP" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000331", v_DiseasesId = Constants.TRAUMA_ACUSTICO_AVANZADO_OI, v_RecommendationName = "Uso de protección auditiva en zonas de ruido superior al LMP" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000332", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_LEVE_OI, v_RecommendationName = "Uso estricto de protección auditiva en zonas de ruido superior al NA" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000333", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_MODERADA_OI, v_RecommendationName = "Uso estricto de protección auditiva en zonas de ruido superior al NA Control audiométrico en 6 meses" },
                new RecomendationList { v_MasterRecommendationId = "N009-MR000000333", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_AVANZADA_OI, v_RecommendationName = "Uso estricto de protección auditiva en zonas de ruido superior al NA Control audiométrico en 6 meses" }
            };

            _restrictions = new List<RestrictionList>()
            {             
                new RestrictionList { v_MasterRestrictionId = "N009-MR000000334", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_LEVE_OD, v_RestrictionName = "No Trabajar en zonas donde el ruido supere el LMP" },
                new RestrictionList { v_MasterRestrictionId = "N009-MR000000335", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_MODERADA_OD, v_RestrictionName = "No Trabajar en zonas donde el ruido supere el LMP, No laborar en embarcaciones" },
                new RestrictionList { v_MasterRestrictionId = "N009-MR000000336", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_AVANZADA_OD, v_RestrictionName = "No Trabajar en zonas donde el ruido supere el LMP, No laborar en embarcaciones, No conducir vehículos automotores" },

                new RestrictionList { v_MasterRestrictionId = "N009-MR000000334", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_LEVE_OI, v_RestrictionName = "No Trabajar en zonas donde el ruido supere el LMP" },
                new RestrictionList { v_MasterRestrictionId = "N009-MR000000335", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_MODERADA_OI, v_RestrictionName = "No Trabajar en zonas donde el ruido supere el LMP, No laborar en embarcaciones" },
                new RestrictionList { v_MasterRestrictionId = "N009-MR000000336", v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_AVANZADA_OI, v_RestrictionName = "No Trabajar en zonas donde el ruido supere el LMP, No laborar en embarcaciones, No conducir vehículos automotores" }
            };

            _dx = new List<DiagnosticRepositoryList>()
            {
                new DiagnosticRepositoryList { v_DiseasesId = Constants.NORMOACUSIA_UC_AUDIOMETRIA_OIDO_IZQUIERDO, v_DiseasesName = "Normoacusia Oído Izquierdo" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.NORMOACUSIA_UC_AUDIOMETRIA_OIDO_DERECHO, v_DiseasesName = "Normoacusia Oído Derecho" },
              
                new DiagnosticRepositoryList { v_DiseasesId = Constants.TRAUMA_ACUSTICO_LEVE_OD, v_DiseasesName = "Trauma Acústico Leve Oído Derecho" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.TRAUMA_ACUSTICO_AVANZADO_OD, v_DiseasesName = "Trauma Acústico Avanzado Oído Derecho" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_LEVE_OD, v_DiseasesName = "Hipoacusia Inducida por Ruido Leve Oído Derecho" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_MODERADA_OD, v_DiseasesName = "Hipoacusia Inducida por Ruido Moderada Oído Derecho" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_AVANZADA_OD, v_DiseasesName = "Hipoacusia Inducida por Ruido Avanzada Oído Derecho" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.TRAUMA_ACUSTICO_LEVE_OI, v_DiseasesName = "Trauma Acústico Leve Oído Izquierdo" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.TRAUMA_ACUSTICO_AVANZADO_OI, v_DiseasesName = "Trauma Acústico Avanzado Oído Izquierdo" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_LEVE_OI, v_DiseasesName = "Hipoacusia Inducida por Ruido Leve Oído Izquierdo" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_MODERADA_OI, v_DiseasesName = "Hipoacusia Inducida por Ruido Moderada Oído Izquierdo" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.HIPOACUSIA_INDUCIDA_POR_RUIDO_AVANZADA_OI, v_DiseasesName = "Hipoacusia Inducida por Ruido Avanzada Oído Izquierdo" },

                 // Otras alt oido derecho
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_AUDICIÓN_INFRANORMAL_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Audición Infranormal - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_AUDICIÓN_INFRANORMAL_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Audición Infranormal - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_AUDICIÓN_INFRANORMAL_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Audición Infranormal - Mixta" },
                
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_LIGERA_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Ligera - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_LIGERA_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Ligera - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_LIGERA_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Ligera - Mixta" },
               
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_1ER_GRADO_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Mediana 1ER Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_1ER_GRADO_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Mediana 1ER Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_1ER_GRADO_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Mediana 1ER Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_2DO_GRADO_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Mediana 2DO Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_2DO_GRADO_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Mediana 2DO Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_2DO_GRADO_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Mediana 2DO Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_1ER_GRADO_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Severa 1ER Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_1ER_GRADO_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Severa 1ER Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_1ER_GRADO_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Severa 1ER Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_2DO_GRADO_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Severa 2DO Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_2DO_GRADO_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Severa 2DO Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_2DO_GRADO_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Severa 2DO Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_PROFUNDA_CONDUCTIVA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Profunda - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_PROFUNDA_NEUROSENSORIAL_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Profunda - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_PROFUNDA_MIXTA_OD, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Derecho - Deficiencia Auditiva Profunda - Mixta " },

                // Otras alt oido izquierdo
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_AUDICIÓN_INFRANORMAL_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Audición Infranormal - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_AUDICIÓN_INFRANORMAL_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Audición Infranormal - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_AUDICIÓN_INFRANORMAL_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Audición Infranormal - Mixta" },
                
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_LIGERA_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Ligera - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_LIGERA_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Ligera - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_LIGERA_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Ligera - Mixta" },
               
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_1ER_GRADO_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Mediana 1ER Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_1ER_GRADO_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Mediana 1ER Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_1ER_GRADO_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Mediana 1ER Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_2DO_GRADO_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Mediana 2DO Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_2DO_GRADO_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Mediana 2DO Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_MEDIANA_2DO_GRADO_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Mediana 2DO Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_1ER_GRADO_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Severa 1ER Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_1ER_GRADO_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Severa 1ER Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_1ER_GRADO_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Severa 1ER Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_2DO_GRADO_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Severa 2DO Grado - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_2DO_GRADO_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Severa 2DO Grado - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_SEVERA_2DO_GRADO_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Severa 2DO Grado - Mixta" },

                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_PROFUNDA_CONDUCTIVA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Profunda - Conductiva" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_PROFUNDA_NEUROSENSORIAL_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Profunda - Neuro Sensorial" },
                new DiagnosticRepositoryList { v_DiseasesId = Constants.OTRAS_ALTERACIONES_NDR_DEFICIENCIA_AUDITIVA_PROFUNDA_MIXTA_OI, v_DiseasesName = "Otras Alteraciones No debidas a ruído Oído Izquierdo - Deficiencia Auditiva Profunda - Mixta " },
            };

        }

        // Alejandro
        private DiagnosticRepositoryList GetDxByName(string dx)
        {
            var sql = (from d in _dx
                       join rec in _recomendations on d.v_DiseasesId equals rec.v_DiseasesId into l_rec
                       join rec in _restrictions on d.v_DiseasesId equals rec.v_DiseasesId into l_res
                       where d.v_DiseasesName == dx
                       select new DiagnosticRepositoryList
                       {
                           v_DiseasesId = d.v_DiseasesId,
                           v_DiseasesName = d.v_DiseasesName,
                           Recomendations = l_rec.ToList(),
                           Restrictions = l_res.ToList()
                       }).FirstOrDefault();

            if (sql != null)
            {
                sql.Recomendations.ForEach(p => { p.v_ComponentId = Constants.AUDIOMETRIA_ID; p.i_RecordStatus = (int)RecordStatus.Agregado; p.i_RecordType = (int)RecordType.Temporal; });
                sql.Restrictions.ForEach(p => { p.v_ComponentId = Constants.AUDIOMETRIA_ID; p.i_RecordStatus = (int)RecordStatus.Agregado; p.i_RecordType = (int)RecordType.Temporal; });
            }

            return sql;
        }

        private void DxAutomatic()
        {
            string dxklo, dxcli, hipo, hipo2, dxklo2, dxcli2;
            //int [,] vector = new int[8,7];
            double[,] vector = new double[8, 7];


            dxklo = "";
            dxcli = "";
            hipo = "";
            hipo2 = "";
            dxklo2 = "";
            dxcli2 = "";

            //try
            //{

            #region Llenado de Vector
            vector[0, 0] = int.Parse(txt_VA_OD_500.Text == "" ? "-11" : txt_VA_OD_500.Text);
            vector[0, 1] = int.Parse(txt_VA_OD_1000.Text == "" ? "-11" : txt_VA_OD_1000.Text);
            vector[0, 2] = int.Parse(txt_VA_OD_2000.Text == "" ? "-11" : txt_VA_OD_2000.Text);
            vector[0, 3] = int.Parse(txt_VA_OD_3000.Text == "" ? "-11" : txt_VA_OD_3000.Text);
            vector[0, 4] = int.Parse(txt_VA_OD_4000.Text == "" ? "-11" : txt_VA_OD_4000.Text);
            vector[0, 5] = int.Parse(txt_VA_OD_6000.Text == "" ? "-11" : txt_VA_OD_6000.Text);
            vector[0, 6] = int.Parse(txt_VA_OD_8000.Text == "" ? "-11" : txt_VA_OD_8000.Text);

            vector[1, 0] = int.Parse(txt_VA_OI_500.Text == "" ? "-11" : txt_VA_OI_500.Text);
            vector[1, 1] = int.Parse(txt_VA_OI_1000.Text == "" ? "-11" : txt_VA_OI_1000.Text);
            vector[1, 2] = int.Parse(txt_VA_OI_2000.Text == "" ? "-11" : txt_VA_OI_2000.Text);
            vector[1, 3] = int.Parse(txt_VA_OI_3000.Text == "" ? "-11" : txt_VA_OI_3000.Text);
            vector[1, 4] = int.Parse(txt_VA_OI_4000.Text == "" ? "-11" : txt_VA_OI_4000.Text);
            vector[1, 5] = int.Parse(txt_VA_OI_6000.Text == "" ? "-11" : txt_VA_OI_6000.Text);
            vector[1, 6] = int.Parse(txt_VA_OI_8000.Text == "" ? "-11" : txt_VA_OI_8000.Text);

            vector[2, 0] = int.Parse(txt_VO_OD_500.Text == "" ? "-11" : txt_VO_OD_500.Text);
            vector[2, 1] = int.Parse(txt_VO_OD_1000.Text == "" ? "-11" : txt_VO_OD_1000.Text);
            vector[2, 2] = int.Parse(txt_VO_OD_2000.Text == "" ? "-11" : txt_VO_OD_2000.Text);
            vector[2, 3] = int.Parse(txt_VO_OD_3000.Text == "" ? "-11" : txt_VO_OD_3000.Text);
            vector[2, 4] = int.Parse(txt_VO_OD_4000.Text == "" ? "-11" : txt_VO_OD_4000.Text);
            vector[2, 5] = int.Parse(txt_VO_OD_6000.Text == "" ? "-11" : txt_VO_OD_6000.Text);
            vector[2, 6] = int.Parse(txt_VO_OD_8000.Text == "" ? "-11" : txt_VO_OD_8000.Text);

            vector[3, 0] = int.Parse(txt_VO_OI_500.Text == "" ? "-11" : txt_VO_OI_500.Text);
            vector[3, 1] = int.Parse(txt_VO_OI_1000.Text == "" ? "-11" : txt_VO_OI_1000.Text);
            vector[3, 2] = int.Parse(txt_VO_OI_2000.Text == "" ? "-11" : txt_VO_OI_2000.Text);
            vector[3, 3] = int.Parse(txt_VO_OI_3000.Text == "" ? "-11" : txt_VO_OI_3000.Text);
            vector[3, 4] = int.Parse(txt_VO_OI_4000.Text == "" ? "-11" : txt_VO_OI_4000.Text);
            vector[3, 5] = int.Parse(txt_VO_OI_6000.Text == "" ? "-11" : txt_VO_OI_6000.Text);
            vector[3, 6] = int.Parse(txt_VO_OI_8000.Text == "" ? "-11" : txt_VO_OI_8000.Text);
            #endregion

            #region Cálculos de Matriz

            //Diferencia OD
            for (int i = 0; i <= 6; i++)
            {
                if (vector[2, i] != -11)
                {
                    if (vector[0, i] > 25)
                    { vector[4, i] = vector[0, i] - vector[2, i]; }
                    else
                    { vector[4, i] = 0; }
                }
                else
                { vector[4, i] = 0; }
            }

            //Diferencia OI
            for (int i = 0; i <= 6; i++)
            {
                if (vector[3, i] != -11)
                {
                    if (vector[1, i] > 25)
                    { vector[5, i] = vector[1, i] - vector[3, i]; }
                    else
                    { vector[5, i] = 0; }
                }
                else
                { vector[5, i] = 0; }
            }

            //Hipoacusia OD
            for (int i = 0; i <= 6; i++)
            {
                if (vector[0, i] > 25)
                {
                    if (vector[2, i] != -11)
                    {
                        if (vector[2, i] > 25)
                        {
                            if (vector[4, i] > 10)
                            { vector[6, i] = 1000; } //MIXTA
                            else
                            { vector[6, i] = 2000; } //NEUROSENSORIAL
                        }
                        else
                        { vector[6, i] = 3000; } //CONDUCTIVA
                    }
                    else
                    { vector[6, i] = 4000; } //ND
                }
                else
                { vector[6, i] = 0; } //NORMAL
            }

            //Hipoacusia OI
            for (int i = 0; i <= 6; i++)
            {
                if (vector[1, i] > 25)
                {
                    if (vector[3, i] != -11)
                    {
                        if (vector[3, i] > 25)
                        {
                            if (vector[5, i] > 10)
                            { vector[7, i] = 1000; } //MIXTA
                            else
                            { vector[7, i] = 2000; } //NEURO SENSORIAL
                        }
                        else
                        { vector[7, i] = 3000; } //CONDUCTIVA
                    }
                    else
                    { vector[7, i] = 4000; } //ND
                }
                else
                { vector[7, i] = 0; } //NORMAL
            }

            #endregion

            #region DX Clínico OD

            if ((vector[0, 0] != -11) && (vector[0, 1] != -11) && (vector[0, 2] != -11) && (vector[0, 3] != -11) && (vector[0, 4] != -11) && (vector[0, 5] != -11) && (vector[0, 6] != -11))
            {
                if ((vector[0, 0] <= 20) && (vector[0, 1] <= 20) && (vector[0, 2] <= 20) && (vector[0, 3] <= 20) && (vector[0, 4] <= 20) && (vector[0, 5] <= 20) && (vector[0, 6] <= 20))
                { dxcli = "Normoacusia"; }
                else
                {
                    if ((vector[0, 0] + vector[0, 1] + vector[0, 2] + vector[0, 4]) / 4 <= 20)
                    { dxcli = "Audición Infranormal"; }
                    else
                    {
                        if ((vector[0, 0] + vector[0, 1] + vector[0, 2] + vector[0, 4]) / 4 <= 40)
                        { dxcli = "Deficiencia Auditiva Ligera"; }
                        else
                        {
                            if ((vector[0, 0] + vector[0, 1] + vector[0, 2] + vector[0, 4]) / 4 <= 55)
                            { dxcli = "Deficiencia Auditiva Mediana 1ER Grado"; }
                            else
                            {
                                if ((vector[0, 0] + vector[0, 1] + vector[0, 2] + vector[0, 4]) / 4 <= 70)
                                { dxcli = "Deficiencia Auditiva Mediana 2DO Grado"; }
                                else
                                {
                                    if ((vector[0, 0] + vector[0, 1] + vector[0, 2] + vector[0, 4]) / 4 <= 80)
                                    { dxcli = "Deficiencia Auditiva Severa 1ER Grado"; }
                                    else
                                    {
                                        if ((vector[0, 0] + vector[0, 1] + vector[0, 2] + vector[0, 4]) / 4 <= 90)
                                        { dxcli = "Deficiencia Auditiva Severa 2DO Grado"; }
                                        else
                                        { dxcli = "Deficiencia Auditiva Profunda"; }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            { dxcli = "No Hay Valores que Procesar"; }

            if (((vector[6, 0] == 1000) || (vector[6, 1] == 1000) || (vector[6, 2] == 1000) || (vector[6, 3] == 1000) || (vector[6, 4] == 1000) || (vector[6, 5] == 1000) || (vector[6, 6] == 1000)) ||
                (((vector[6, 0] == 2000) || (vector[6, 1] == 2000) || (vector[6, 2] == 2000) || (vector[6, 3] == 2000) || (vector[6, 4] == 2000) || (vector[6, 5] == 2000) || (vector[6, 6] == 2000)) &&
                ((vector[6, 0] == 3000) || (vector[6, 1] == 3000) || (vector[6, 2] == 3000) || (vector[6, 3] == 3000) || (vector[6, 4] == 3000) || (vector[6, 5] == 3000) || (vector[6, 6] == 3000))))
            { hipo = "Mixta"; }
            else
            {
                if ((vector[6, 0] == 2000) || (vector[6, 1] == 2000) || (vector[6, 2] == 2000) || (vector[6, 3] == 2000) || (vector[6, 4] == 2000) || (vector[6, 5] == 2000) || (vector[6, 6] == 2000))
                { hipo = "Neuro Sensorial"; }
                else
                {
                    if ((vector[6, 0] == 3000) || (vector[6, 1] == 3000) || (vector[6, 2] == 3000) || (vector[6, 3] == 3000) || (vector[6, 4] == 3000) || (vector[6, 5] == 3000) || (vector[6, 6] == 3000))
                    { hipo = "Conductiva"; }
                    else
                    { hipo = "No Determinado"; }
                }
            }

            #endregion

            #region DX Clínico OI

            if ((vector[1, 0] != -11) && (vector[1, 1] != -11) && (vector[1, 2] != -11) && (vector[1, 3] != -11) && (vector[1, 4] != -11) && (vector[1, 5] != -11) && (vector[1, 6] != -11))
            {
                if ((vector[1, 0] <= 20) && (vector[1, 1] <= 20) && (vector[1, 2] <= 20) && (vector[1, 3] <= 20) && (vector[1, 4] <= 20) && (vector[1, 5] <= 20) && (vector[1, 6] <= 20))
                { dxcli2 = "Normoacusia"; }
                else
                {
                    if ((vector[1, 0] + vector[1, 1] + vector[1, 2] + vector[1, 4]) / 4 <= 20)
                    { dxcli2 = "Audición Infranormal"; }
                    else
                    {
                        if ((vector[1, 0] + vector[1, 1] + vector[1, 2] + vector[1, 4]) / 4 <= 40)
                        { dxcli2 = "Deficiencia Auditiva Ligera"; }
                        else
                        {
                            if ((vector[1, 0] + vector[1, 1] + vector[1, 2] + vector[1, 4]) / 4 <= 55)
                            { dxcli2 = "Deficiencia Auditiva Mediana 1ER Grado"; }
                            else
                            {
                                if ((vector[1, 0] + vector[1, 1] + vector[1, 2] + vector[1, 4]) / 4 <= 70)
                                { dxcli2 = "Deficiencia Auditiva Mediana 2DO Grado"; }
                                else
                                {
                                    if ((vector[1, 0] + vector[1, 1] + vector[1, 2] + vector[1, 4]) / 4 <= 80)
                                    { dxcli2 = "Deficiencia Auditiva Severa 1ER Grado"; }
                                    else
                                    {
                                        if ((vector[1, 0] + vector[1, 1] + vector[1, 2] + vector[1, 4]) / 4 <= 90)
                                        { dxcli2 = "Deficiencia Auditiva Severa 2DO Grado"; }
                                        else
                                        { dxcli2 = "Deficiencia Auditiva Profunda"; }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            { dxcli2 = "No Hay Valores que Procesar"; }

            if (((vector[7, 0] == 1000) || (vector[7, 1] == 1000) || (vector[7, 2] == 1000) || (vector[7, 3] == 1000) || (vector[7, 4] == 1000) || (vector[7, 5] == 1000) || (vector[7, 6] == 1000)) ||
                (((vector[7, 0] == 2000) || (vector[7, 1] == 2000) || (vector[7, 2] == 2000) || (vector[7, 3] == 2000) || (vector[7, 4] == 2000) || (vector[7, 5] == 2000) || (vector[7, 6] == 2000)) &&
                ((vector[7, 0] == 3000) || (vector[7, 1] == 3000) || (vector[7, 2] == 3000) || (vector[7, 3] == 3000) || (vector[7, 4] == 3000) || (vector[7, 5] == 3000) || (vector[7, 6] == 3000))))
            { hipo2 = "Mixta"; }
            else
            {
                if ((vector[7, 0] == 2000) || (vector[7, 1] == 2000) || (vector[7, 2] == 2000) || (vector[7, 3] == 2000) || (vector[7, 4] == 2000) || (vector[7, 5] == 2000) || (vector[7, 6] == 2000))
                { hipo2 = "Neuro Sensorial"; }
                else
                {
                    if ((vector[7, 0] == 3000) || (vector[7, 1] == 3000) || (vector[7, 2] == 3000) || (vector[7, 3] == 3000) || (vector[7, 4] == 3000) || (vector[7, 5] == 3000) || (vector[7, 6] == 3000))
                    { hipo2 = "Conductiva"; }
                    else
                    { hipo2 = "No Determinado"; }
                }
            }

            #endregion

            #region DX Ocupacional Kx OD

            if ((vector[0, 0] != -11) && (vector[0, 1] != -11) && (vector[0, 2] != -11) && (vector[0, 3] != -11) && (vector[0, 4] != -11) && (vector[0, 5] != -11) && (vector[0, 6] != -11))
            {
                if ((((vector[6, 0] == 2000) || (vector[6, 1] == 2000) || (vector[6, 2] == 2000) || (vector[6, 3] == 2000) || (vector[6, 4] == 2000) || (vector[6, 5] == 2000) || (vector[6, 6] == 2000)) ||
                    ((vector[6, 0] == 1000) || (vector[6, 1] == 1000) || (vector[6, 2] == 1000) || (vector[6, 3] == 1000) || (vector[6, 4] == 1000) || (vector[6, 5] == 1000) || (vector[6, 6] == 1000))) &&
                    ((Math.Abs(vector[0, 0] - vector[1, 0]) < 15) && (Math.Abs(vector[0, 1] - vector[1, 1]) < 15) && (Math.Abs(vector[0, 2] - vector[1, 2]) < 15) && (Math.Abs(vector[0, 3] - vector[1, 3]) < 15) && (Math.Abs(vector[0, 4] - vector[1, 4]) < 15) && (Math.Abs(vector[0, 5] - vector[1, 5]) < 15) && (Math.Abs(vector[0, 6] - vector[1, 6]) < 15)))
                {
                    if (((vector[0, 0] <= 25) && (vector[0, 1] <= 25) && (vector[0, 2] <= 25) && (vector[0, 3] <= 25)) &&
                        ((vector[0, 4] > 25) || (vector[0, 5] > 25)) &&
                        ((vector[0, 4] > vector[0, 6]) || (vector[0, 5] > vector[0, 6])) &&
                        ((vector[0, 4] < 55) && (vector[0, 5] < 55)))
                    { dxklo = "Trauma Acústico Leve"; }
                    else
                    {
                        if (((vector[0, 0] <= 25) && (vector[0, 1] <= 25) && (vector[0, 2] <= 25) && (vector[0, 3] <= 25)) &&
                            ((vector[0, 4] > 25) || (vector[0, 5] > 25)) &&
                            ((vector[0, 4] > vector[0, 6]) || (vector[0, 5] > vector[0, 6])) &&
                            ((vector[0, 4] > 55) || (vector[0, 5] > 55)))
                        { dxklo = "Trauma Acústico Avanzado"; }
                        else
                        {
                            if (((vector[0, 0] > 25) || (vector[0, 1] > 25) || (vector[0, 2] > 25) || (vector[0, 3] > 25)) &&
                                ((vector[0, 0] <= 25) || (vector[0, 1] <= 25) || (vector[0, 2] <= 25) || (vector[0, 3] <= 25)))
                            { dxklo = "Hipoacusia Inducida por Ruido Leve"; }
                            else
                            {
                                if (((vector[0, 0] > 25) && (vector[0, 1] > 25) && (vector[0, 2] > 25) && (vector[0, 3] > 25)) &&
                                    ((vector[0, 0] <= 55) && (vector[0, 1] <= 55) && (vector[0, 2] <= 55) && (vector[0, 3] <= 55)))
                                { dxklo = "Hipoacusia Inducida por Ruido Moderada"; }
                                else
                                {
                                    if (((vector[0, 0] > 25) && (vector[0, 1] > 25) && (vector[0, 2] > 25) && (vector[0, 3] > 25)) &&
                                        ((vector[0, 0] > 55) || (vector[0, 1] > 55) || (vector[0, 2] > 55) || (vector[0, 3] > 55)))
                                    { dxklo = "Hipoacusia Inducida por Ruido Avanzada"; }
                                    else
                                    { dxklo = "Otras Alteraciones No debidas a ruído."; }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if ((vector[0, 0] > 25) || (vector[0, 1] > 25) || (vector[0, 2] > 25) || (vector[0, 3] > 25) || (vector[0, 4] > 25) || (vector[0, 5] > 25) || (vector[0, 6] > 25))
                    { dxklo = "Otras Alteraciones No debidas a ruído."; }
                    else
                    { dxklo = "Normoacusia"; }
                }
            }
            else
            { dxklo = "No hay Datos"; }

            #endregion

            #region DX Ocupacional Kx ID

            if ((vector[1, 0] != -11) && (vector[1, 1] != -11) && (vector[1, 2] != -11) && (vector[1, 3] != -11) && (vector[1, 4] != -11) && (vector[1, 5] != -11) && (vector[1, 6] != -11))
            {
                if ((((vector[7, 0] == 2000) || (vector[7, 1] == 2000) || (vector[7, 2] == 2000) || (vector[7, 3] == 2000) || (vector[7, 4] == 2000) || (vector[7, 5] == 2000) || (vector[7, 6] == 2000)) ||
                    ((vector[7, 0] == 1000) || (vector[7, 1] == 1000) || (vector[7, 2] == 1000) || (vector[7, 3] == 1000) || (vector[7, 4] == 1000) || (vector[7, 5] == 1000) || (vector[7, 6] == 1000))) &&
                    ((Math.Abs(vector[0, 0] - vector[1, 0]) < 15) && (Math.Abs(vector[0, 1] - vector[1, 1]) < 15) && (Math.Abs(vector[0, 2] - vector[1, 2]) < 15) && (Math.Abs(vector[0, 3] - vector[1, 3]) < 15) && (Math.Abs(vector[0, 4] - vector[1, 4]) < 15) && (Math.Abs(vector[0, 5] - vector[1, 5]) < 15) && (Math.Abs(vector[0, 6] - vector[1, 6]) < 15)))
                {
                    if (((vector[1, 0] <= 25) && (vector[1, 1] <= 25) && (vector[1, 2] <= 25) && (vector[1, 3] <= 25)) &&
                        ((vector[1, 4] > 25) || (vector[1, 5] > 25)) &&
                        ((vector[1, 4] > vector[1, 6]) || (vector[1, 5] > vector[1, 6])) &&
                        ((vector[1, 4] < 55) && (vector[1, 5] < 55)))
                    { dxklo2 = "Trauma Acústico Leve"; }
                    else
                    {
                        if (((vector[1, 0] <= 25) && (vector[1, 1] <= 25) && (vector[1, 2] <= 25) && (vector[1, 3] <= 25)) &&
                            ((vector[1, 4] > 25) || (vector[1, 5] > 25)) &&
                            ((vector[1, 4] > vector[1, 6]) || (vector[0, 5] > vector[1, 6])) &&
                            ((vector[1, 4] > 55) || (vector[1, 5] > 55)))
                        { dxklo2 = "Trauma Acústico Avanzado"; }
                        else
                        {
                            if (((vector[1, 0] > 25) || (vector[1, 1] > 25) || (vector[1, 2] > 25) || (vector[1, 3] > 25)) &&
                                ((vector[1, 0] <= 25) || (vector[1, 1] <= 25) || (vector[1, 2] <= 25) || (vector[1, 3] <= 25)))
                            { dxklo2 = "Hipoacusia Inducida por Ruido Leve"; }
                            else
                            {
                                if (((vector[1, 0] > 25) && (vector[1, 1] > 25) && (vector[1, 2] > 25) && (vector[1, 3] > 25)) &&
                                    ((vector[1, 0] <= 55) && (vector[1, 1] <= 55) && (vector[1, 2] <= 55) && (vector[1, 3] <= 55)))
                                { dxklo2 = "Hipoacusia Inducida por Ruido Moderada"; }
                                else
                                {
                                    if (((vector[1, 0] > 25) && (vector[1, 1] > 25) && (vector[1, 2] > 25) && (vector[1, 3] > 25)) &&
                                        ((vector[1, 0] > 55) || (vector[1, 1] > 55) || (vector[1, 2] > 55) || (vector[1, 3] > 55)))
                                    { dxklo2 = "Hipoacusia Inducida por Ruido Avanzada"; }
                                    else
                                    { dxklo2 = "Otras Alteraciones No debidas a ruído."; }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if ((vector[1, 0] > 25) || (vector[1, 1] > 25) || (vector[1, 2] > 25) || (vector[1, 3] > 25) || (vector[1, 4] > 25) || (vector[1, 5] > 25) || (vector[1, 6] > 25))
                    { dxklo2 = "Otras Alteraciones No debidas a ruído."; }
                    else
                    { dxklo2 = "Normoacusia"; }
                }
            }
            else
            { dxklo2 = "No hay Datos"; }

            #endregion


            txtDxClinico_OD.Text = dxcli + " - " + hipo;
            txtDxClinico_OI.Text = dxcli2 + " - " + hipo2;

            txtDxOcupacionalOD.Text = dxklo;
            txtDxOcupacionalOI.Text = dxklo2;


            // capturar dx automatico + recomendaciones y restricciones   

            List<DiagnosticRepositoryList> dxList = null;

            var dxOD = SearchDxSugeridoOfSystem(txtDxOcupacionalOD.Text, "Oído Derecho", Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OD);
            var dxOI = SearchDxSugeridoOfSystem(txtDxOcupacionalOI.Text, "Oído Izquierdo", Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OI);

            if (dxOD != null || dxOI != null)
            {
                dxList = new List<DiagnosticRepositoryList>();
            }

            if (dxOD != null)
            {
                dxList.Add(dxOD);
            }

            if (dxOI != null)
            {
                dxList.Add(dxOI);
            }

            List<string> listCf = new List<string>() { Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OD, Constants.txt_AUD_DX_OCUPACIONAL_AUTO_OI };

            // Disparar evento
            OnAfterValueChange(new AudiometriaAfterValueChangeEventArgs { PackageSynchronization = dxList, ListcomponentFieldsId = listCf });

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        private void SaveImagenAudiograma()
        {
            var chartAudiogramaOD = new MemoryStream();
            chart1.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);

            var chartAudiogramaOI = new MemoryStream();
            chart2.SaveImage(chartAudiogramaOI, ChartImageFormat.Png);

            string[] IDs = null;

            IDs = SavePrepared(txtMultimediaFileId_OD.Text, txtServiceComponentMultimediaId_OD.Text, PersonId, ServiceComponentId, "IMAGEN AUDIOGRAMA OD", "IMAGEN PROVENIENTE DEL UC AUDIOMETRIA OD", chartAudiogramaOD.ToArray());

            if (IDs != null)  // GRABAR
            {
                txtMultimediaFileId_OD.Text = IDs[0];
                txtServiceComponentMultimediaId_OD.Text = IDs[1];

                var audiograma = new List<ServiceComponentFieldValuesList>()
                {                
                    new ServiceComponentFieldValuesList { v_ComponentFieldId = txtMultimediaFileId_OD.Name, v_Value1 = txtMultimediaFileId_OD.Text },                 
                    new ServiceComponentFieldValuesList { v_ComponentFieldId = txtServiceComponentMultimediaId_OD.Name, v_Value1 = txtServiceComponentMultimediaId_OD.Text },                
                };

                _listAudiometria.AddRange(audiograma);

            }

            IDs = SavePrepared(txtMultimediaFileId_OI.Text, txtServiceComponentMultimediaId_OI.Text, PersonId, ServiceComponentId, "IMAGEN AUDIOGRAMA OI", "IMAGEN PROVENIENTE DEL UC AUDIOMETRIA OI", chartAudiogramaOI.ToArray());

            if (IDs != null)    // GRABAR
            {
                txtMultimediaFileId_OI.Text = IDs[0];
                txtServiceComponentMultimediaId_OI.Text = IDs[1];

                var audiograma = new List<ServiceComponentFieldValuesList>()
                {                                
                    new ServiceComponentFieldValuesList { v_ComponentFieldId = txtMultimediaFileId_OI.Name, v_Value1 = txtMultimediaFileId_OI.Text },                
                    new ServiceComponentFieldValuesList { v_ComponentFieldId = txtServiceComponentMultimediaId_OI.Name, v_Value1 = txtServiceComponentMultimediaId_OI.Text }
                };

                _listAudiometria.AddRange(audiograma);
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

        #endregion

        #region "----------------------- Private Events -----------------------"

        private void ucAudiometria_Load(object sender, EventArgs e)
        {
            LoadDxAndRecoAndRes();
            ShowGraphicOD();
            ShowGraphicOI();
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            ShowGraphicOD();
        }

        private void txt_Leave_OD(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            //if (!_isChangueValueControl)
            //{
            if (_valueOld != senderCtrl.Text)
            {
                _isChangueValueControl = true;
            }
            else
            {
                _isChangueValueControl = false;
            }
            //}
        }

        private void txt_Leave_OI(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            //if (!_isChangueValueControl)
            //{
            if (_valueOld != senderCtrl.Text)
            {
                _isChangueValueControl = true;
            }
            else
            {
                _isChangueValueControl = false;
            }
            //}
        }

        private void txt_Enter_OD(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            _valueOld = senderCtrl.Text;
        }

        private void txt_Enter_OI(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            _valueOld = senderCtrl.Text;
        }

        private void txt_TextChanged_OD(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            if (senderCtrl.Text == "") senderCtrl.Text = null;
            if (!IsNumeric(senderCtrl.Text)) return;
            _audiometria = new ServiceComponentFieldValuesList();
            int var_rango;

            if (!IsNumeric(senderCtrl.Text)) return;
          
            if (senderCtrl.Text != string.Empty && !senderCtrl.Text.Contains("No Determinado"))
            {
                var res = int.TryParse(senderCtrl.Text, out var_rango);

                if (var_rango >= -10 && var_rango <= 120)
                {
                    //e.Cancel = false;
                }
                else
                {
                    //e.Cancel = true;
                    senderCtrl.Select(0, 3);
                    MessageBox.Show("El valor : " + senderCtrl.Text + " está fuera del rango [-10 - 120]. ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var_rango = -100;   // valor que significa vacio para el sistema
            }



            _listAudiometria.RemoveAll(p => p.v_ComponentFieldId == senderCtrl.Name);

            _audiometria.v_ComponentFieldId = senderCtrl.Name;
            _audiometria.v_Value1 = var_rango == -100 ? string.Empty : var_rango.ToString(); // senderCtrl.Text;

            _listAudiometria.Add(_audiometria);

            DataSource = _listAudiometria;

            ShowGraphicOD();
            //CallMe();



        }

        private void txt_TextChanged_OI(object sender, System.EventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
            if (senderCtrl.Text == "") senderCtrl.Text = null;
            if (!IsNumeric(senderCtrl.Text)) return;
            _audiometria = new ServiceComponentFieldValuesList();
            int var_rango;

            if (senderCtrl.Text != string.Empty && !senderCtrl.Text.Contains("No Determinado"))
            {

                var res = int.TryParse(senderCtrl.Text, out var_rango);

                if (var_rango >= -10 && var_rango <= 120)
                {
                    //e.Cancel = false;
                }
                else
                {
                    //e.Cancel = true;
                    senderCtrl.Select(0, 3);
                    MessageBox.Show("El valor : " + senderCtrl.Text + " está fuera del rango (-10 ; 120). ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var_rango = -100;
            }


            _listAudiometria.RemoveAll(p => p.v_ComponentFieldId == senderCtrl.Name);

            _audiometria.v_ComponentFieldId = senderCtrl.Name;
            _audiometria.v_Value1 = var_rango == -100 ? string.Empty : var_rango.ToString();  //senderCtrl.Text;

            _listAudiometria.Add(_audiometria);

            DataSource = _listAudiometria;

            ShowGraphicOI();
            //CallMe();


        }

        private void txt_KeyPress_OD(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txt_KeyPress_OI(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void txt_Validating_OD(object sender, CancelEventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
   
            if (senderCtrl.Text != "")
            {
                if (!IsNumeric(senderCtrl.Text))
                {
                    e.Cancel = true;
                    MessageBox.Show("El valor : " + senderCtrl.Text + " no es un valor numérico ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
            
            int var_rango;
            if (senderCtrl.Text != string.Empty)
            {
                var_rango = int.Parse(senderCtrl.Text);
            }
            else
            {
                var_rango = 0;
            }

            if (var_rango >= -10 && var_rango <= 120)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("El valor : " + senderCtrl.Text + " está fuera del rango (-10-120). ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Validating_OI(object sender, CancelEventArgs e)
        {
            TextBox senderCtrl = (TextBox)sender;
       
            if (senderCtrl.Text != "")
            {
                if (!IsNumeric(senderCtrl.Text))
                {
                    e.Cancel = true;
                    MessageBox.Show("El valor : " + senderCtrl.Text + " no es un valor numérico ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            int var_rango;
            if (senderCtrl.Text != string.Empty)
            {
                var_rango = int.Parse(senderCtrl.Text);
            }
            else
            {
                var_rango = 0;
            }

            if (var_rango >= -10 && var_rango <= 120)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("El valor : " + senderCtrl.Text + " está fuera del rango (-10-120). ", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        public bool IsNumeric(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
    }
}
