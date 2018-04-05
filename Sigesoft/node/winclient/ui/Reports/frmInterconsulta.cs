using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System.IO;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmInterconsulta : Form
    {
        private string _serviceId;
        private string _Altitud;
        private string _Especialidad;
        private string _Labor;
        private string _Solicita;
        private string _Observaciones;
        private List<DxCie10> _Lista;
        private MergeExPDF _mergeExPDF = new MergeExPDF();

        public frmInterconsulta(string pstServiceId, string pstrAltitud, string pstrEspecialidad, string pstrLabor, string pstrSolicita, List<DxCie10> Lista, string pstrObservaciones)
        {
            InitializeComponent();
            _serviceId = pstServiceId;
            _Altitud = pstrAltitud;
            _Especialidad = pstrEspecialidad;
            _Labor = pstrLabor;
            _Solicita = pstrSolicita;
            _Observaciones = pstrObservaciones;
            _Lista = Lista;
        }

        private void frmInterconsulta_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void ShowReport()
        {
            List<string> _filesNameToMerge = new List<string>();
            OperationResult objOperationResult = new OperationResult();

            string ruta = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();
            var aptitudeCertificate = new ServiceBL().GetReportInterconsulta(_serviceId, _Altitud, _Especialidad, _Labor, _Solicita, _Observaciones);

            //Verificar si el reporte existe en el repositorio
            string findReportInter = ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + ".pdf";
            string[] files = Directory.GetFiles(ruta, "*.pdf");
            int strIndex = 0;
            for (int i = 0; i < files.Length; i++)
            {
                strIndex = files[i].IndexOf(findReportInter);
                if (strIndex >= 0)
                {
                    File.Copy(findReportInter, ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + "_antiguo.pdf");
                    _filesNameToMerge.Add(ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + "_antiguo.pdf");
                }
            }

            //Crear el nuevo reporte Interconsulta
            var rp = new Reports.crReporteInterconsulta();
            string RutaReportInterNew = ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + "_nuevo.pdf";

            DataSet ds1 = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
            dt.TableName = "dtInterconsulta";
            ds1.Tables.Add(dt);

            DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(_Lista);
            dt1.TableName = "dtDxs";
            ds1.Tables.Add(dt1);

            rp.SetDataSource(ds1);
            rp.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, RutaReportInterNew);

            //crystalReportViewer1.ReportSource = rp;
            //crystalReportViewer1.Show();

            //Agregar el nuevo reporte a la lista para unir PDFs
            _filesNameToMerge.Add(RutaReportInterNew);

            //Fusionar los reportes de la lista en un solo PDF
            var x = _filesNameToMerge.ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + ".pdf";
            _mergeExPDF.Execute();
            _mergeExPDF.RunFile();

            //Borrar temporal creadas
            System.IO.File.Delete(ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + "_nuevo.pdf");
            System.IO.File.Delete(ruta + _serviceId + "-" + aptitudeCertificate[0].Paciente + "_antiguo.pdf");

        }
        //private void ShowReport()
        //{
        //    OperationResult objOperationResult = new OperationResult();

        //    var rp = new Reports.crReporteInterconsulta();

        //    var aptitudeCertificate = new ServiceBL().GetReportInterconsulta(_serviceId, _Altitud, _Especialidad, _Labor, _Solicita, _Observaciones);
        //    DataSet ds1 = new DataSet();

        //    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
        //    dt.TableName = "dtInterconsulta";
        //    ds1.Tables.Add(dt);

        //    DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(_Lista);
        //    dt1.TableName = "dtDxs";
        //    ds1.Tables.Add(dt1);

        //    string ruta = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();

        //    string RutaGrabado = ruta + _serviceId + "-" + aptitudeCertificate [0].Paciente+ ".pdf";      
        //    rp.SetDataSource(ds1);
        //    rp.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, RutaGrabado);

        //    crystalReportViewer1.ReportSource = rp;
        //    crystalReportViewer1.Show();
           

        //}
    }
}
