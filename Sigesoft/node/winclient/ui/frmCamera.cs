using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebCam_Capture;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCamera : Form
    {
        public frmCamera()
        {
            InitializeComponent();
        }
        WebCam webcam;
       public Image _Image;
        private void frmCamera_Load(object sender, EventArgs e)
        {
            try
            {
                webcam = new WebCam();
                webcam.InitializeWebCam(ref imgVideo);
                if (webcam == null)
                {
                    MessageBox.Show("No se encontró cámara instalada en el equipo.");
                }
                else
                {
                    webcam.Start();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
               
        }

        class WebCam
        {
            private WebCamCapture webcam;
            private System.Windows.Forms.PictureBox _FrameImage;
            private int FrameNumber = 30;
            public void InitializeWebCam(ref System.Windows.Forms.PictureBox ImageControl)
            {
                webcam = new WebCamCapture();
                webcam.FrameNumber = ((ulong)(0ul));
                webcam.TimeToCapture_milliseconds = FrameNumber;
                webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
                _FrameImage = ImageControl;
            }

            void webcam_ImageCaptured(object source, WebcamEventArgs e)
            {
                _FrameImage.Image = e.WebCamImage;
            }

            public void Start()
            {
                try
                {
                    webcam.TimeToCapture_milliseconds = FrameNumber;
                    webcam.Start(0);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            
            }

            public void Stop()
            {
                webcam.Stop();
            }

            public void Continue()
            {
                // change the capture time frame
                webcam.TimeToCapture_milliseconds = FrameNumber;

                // resume the video capture from the stop
                webcam.Start(this.webcam.FrameNumber);
            }

            public void ResolutionSetting()
            {
                webcam.Config();
            }

            public void AdvanceSetting()
            {
                webcam.Config2();
            }

        }

        private void bntStart_Click(object sender, EventArgs e)
        {
            webcam.Start();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
           _Image = imgVideo.Image;
           webcam.Stop();
           this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            webcam.Stop();
        }

        private void bntVideoSource_Click(object sender, EventArgs e)
        {
            webcam.AdvanceSetting();
        }
    }
}
