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
using System.Media;
using System.Configuration;
using Microsoft.Speech.Synthesis;
using System.Xml;
using WMPLib;
using System.Threading;

namespace Sigesoft.Node.WinQueueVisor.UI
{
    public partial class frmTV : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        List<string> _lstBackGroundVideoFiles;
        string PathVideo = ConfigurationManager.AppSettings["PathVideo"];
        string NroTv = ConfigurationManager.AppSettings["TV"];
        int TiempoRetraso = int.Parse(ConfigurationManager.AppSettings["TiempoRetraso"].ToString());
        string PathSound = ConfigurationManager.AppSettings["PathSound"];
        int TimerIntervale =int.Parse(ConfigurationManager.AppSettings["TimerIntervale"].ToString());
        string CallFormat = ConfigurationManager.AppSettings["CallFormat"];

        public frmTV()
        {
            InitializeComponent();           
        }

        private void frmTV_Load(object sender, EventArgs e)
        {
            try
            {             
                synth.SetOutputToDefaultAudioDevice();

                // Obtener los videos de la carpeta \Video
                string strFolder = Common.Utils.GetApplicationExecutingFolder() + @"\" + PathVideo + "";
                string strSupportedExtensions = "*.wmv,*.mp4,*.avi"; // Solo Videos
                _lstBackGroundVideoFiles = Common.Utils.GetFolderFiles(strFolder, strSupportedExtensions);

                WMPLib.IWMPPlaylist PlayList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("Video");

                foreach (var item in _lstBackGroundVideoFiles)
                {
                    WMPLib.IWMPMedia zMP3File = axWindowsMediaPlayer1.newMedia(item);
                    PlayList.appendItem(zMP3File);
                }
                axWindowsMediaPlayer1.currentPlaylist = PlayList;
                axWindowsMediaPlayer1.settings.setMode("loop", true);
                axWindowsMediaPlayer1.settings.volume = 10;
                //axWindowsMediaPlayer1.uiMode = "none";

                timer1.Start();
                timer1.Interval = TimerIntervale;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            CalendarBL objCalendarBL = new CalendarBL();
            ServiceBL objServiceBL = new ServiceBL();
            servicecomponentDto objservicecomponentDto = new servicecomponentDto();
            List<CalendarList> objCalendarList = new List<CalendarList>();

            objCalendarList = objCalendarBL.GetCallPacientPagedAndFilteredGroupByCategoryId(ref objOperationResult, 0, null, "", "");

            if (objCalendarList == null)
                return;

            grdData.DataSource = objCalendarList;
            
            foreach (var item in objCalendarList)
            {
                if (item.i_QueueStatusId == (int)QueueStatusId.LLAMANDO && item.i_Iscalling == (int)SiNo.NO )
                {
                    string Repetitions = ConfigurationManager.AppSettings["Repetitions"];

                    for (int i = 0; i < int.Parse(Repetitions.ToString()); i++)
                    {
                        WindowsMediaPlayer wplayer = new WindowsMediaPlayer();

                        wplayer.URL = PathSound;
                       
                        wplayer.controls.play();
                       
                        string VTrabajador;

                        if (item.i_Gender == (int)Common.Gender.MASCULINO)
                        {
                            VTrabajador = "Trabajador";
                        }
                        else
                        {
                            VTrabajador = "Trabajadora";
                        }

                        if (item.v_NameOffice == "--Seleccionar--")
                        {
                            synth.SpeakAsync(string.Format(CallFormat, item.v_Pacient, item.v_OfficeNumber, item.v_CategoryName, VTrabajador));       
                        }
                        else
                        {
                            synth.SpeakAsync(string.Format(CallFormat, item.v_Pacient, item.v_OfficeNumber + "," + item.v_NameOffice));       
                        }
                                                               
                    
                    
                    }    
                }
            }

            List<CalendarList> x = objCalendarBL.GetCallPacientPagedAndFiltered(ref objOperationResult, 0, null, "", "");

            foreach (var item in x)
            {
                //Se actualiza el Flag de IsCalling para saber que el paciente ya ha sido llamado una vez
                string ServiceComponentId = item.v_ServiceComponentId;

                //if (NroTv =="1")
                //{
                    objServiceBL.UpdateServiceComponentVisor(ref objOperationResult, ServiceComponentId, (int)SiNo.SI);
                //}
                //else if (NroTv == "2")
                //{
                //    objServiceBL.UpdateServiceComponentVisor_(ref objOperationResult, ServiceComponentId, (int)SiNo.SI);
                //}

               
            }

        }

        private void grdData_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            if ( int.Parse(e.Row.Cells["i_QueueStatusId"].Value.ToString()) == (int)Common.QueueStatusId.LLAMANDO)
            {
                e.Row.Appearance.BackColor2 = Color.Pink;
            }
            else if (int.Parse(e.Row.Cells["i_QueueStatusId"].Value.ToString()) == (int)Common.QueueStatusId.OCUPADO)
            {
                e.Row.Appearance.BackColor2 = Color.Green;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            btnRefresh_Click(sender, e);
        }

     
    }
}
