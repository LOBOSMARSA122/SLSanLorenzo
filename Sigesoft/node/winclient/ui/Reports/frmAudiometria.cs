using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;


namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmAudiometria : Form
    {
        private string _serviceId;
        private string _ComponentId;

        public frmAudiometria(string serviceId, string ComponentId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _ComponentId = ComponentId;
        }

        private void frmAudiometria_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crFichaAudiometria();

            var serviceBL = new ServiceBL();
            DataSet dsAudiometria = new DataSet();

            var dxList = serviceBL.GetDiagnosticRepositoryByComponent(_serviceId, Constants.AUDIOMETRIA_ID);
            var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dxList);
            dtDx.TableName = "dtDiagnostic";
            dsAudiometria.Tables.Add(dtDx);

            var recom = dxList.SelectMany(s => s.Recomendations).ToList();        

            var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom);
            dtReco.TableName = "dtRecomendation";
            dsAudiometria.Tables.Add(dtReco);

            //-------******************************************************************************************

            var audioUserControlList = serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.AUDIOMETRIA_ID);
            //aqui hay error corregir despues del cine
            var audioCabeceraList = serviceBL.ReportAudiometria(_serviceId, Constants.AUDIOMETRIA_ID);

            var dtAudiometriaUserControl = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList);

            var dtCabecera = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList);

            dtCabecera.TableName = "dtAudiometria";
            dtAudiometriaUserControl.TableName = "dtAudiometriaUserControl";

            dsAudiometria.Tables.Add(dtCabecera);
            dsAudiometria.Tables.Add(dtAudiometriaUserControl);

            rp.SetDataSource(dsAudiometria);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }

        private void frmAudiometria_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
        }
    }
}
