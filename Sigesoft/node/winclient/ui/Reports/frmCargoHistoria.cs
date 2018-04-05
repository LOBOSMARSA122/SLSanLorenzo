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
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmCargoHistoria : Form
    {
        crConsolidadoCargoHistorias rp = null;
        List<ReporteCargoHistorias> ListaCargoHistorias = new List<ReporteCargoHistorias>();
        ReporteCargoHistorias oReporteCargoHistorias = null;
        OperationResult objOperationResult = new OperationResult();
        SystemParameterBL oSystemParameterBL = new SystemParameterBL();
        public frmCargoHistoria(List<ServiceGridJerarquizadaList> ListaServicio)
        {
            InitializeComponent();
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();
            int Correlativo = 1;
            var Parametro = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 259, null);
            ListaServicio.Sort((x, y) => x.d_ServiceDate.Value.CompareTo(y.d_ServiceDate.Value));
            foreach (var item in ListaServicio)
            {
                oReporteCargoHistorias = new ReporteCargoHistorias();
                oReporteCargoHistorias.LogoEmpresaPropietaria = MedicalCenter.b_Image;
                oReporteCargoHistorias.RazonSocialEmpresaPropietaria = MedicalCenter.v_Name;
                oReporteCargoHistorias.FechaActual = DateTime.Now.ToString();
                oReporteCargoHistorias.SedeEmpresaPropietaria = MedicalCenter.v_Sede;
                oReporteCargoHistorias.RazonSocialEmpresaCliente = item.v_OrganizationName;
                oReporteCargoHistorias.Correlativo = Correlativo.ToString();
                oReporteCargoHistorias.FechaHoraServicio = item.d_ServiceDate.ToString();
                oReporteCargoHistorias.Paciente = item.v_Pacient;
                oReporteCargoHistorias.TipoEso = item.v_EsoTypeName;
                oReporteCargoHistorias.Aptitud = item.v_AptitudeStatusName;
                oReporteCargoHistorias.EmailRepresentanteLegalEP = MedicalCenter.v_Mail;
                oReporteCargoHistorias.EmailContactoEP = MedicalCenter.v_EmailContacto;
                oReporteCargoHistorias.Parametro = Parametro[0].Value1 + " - " + Parametro[0].Value2;
                oReporteCargoHistorias.AnioMes = DateTime.Now.ToString("yyyy/MM");
                oSystemParameterBL.ActualizarValorParametro(256, int.Parse(Parametro[0].Value2.ToString()));
                Correlativo++;
                ListaCargoHistorias.Add(oReporteCargoHistorias);
            }

        }

        private void frmCargoHistoria_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            
                OperationResult objOperationResult = new OperationResult();
                rp = new Reports.crConsolidadoCargoHistorias();
                //var rp = new Reports.crCargoHistoria();
              
                var aptitudeCertificate = ListaCargoHistorias;
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

                dt.TableName = "dtCargoHistorias";

                ds1.Tables.Add(dt);

                //rp.SetDataSource(ds1);

                //crystalReportViewer1.ReportSource = rp;
                //crystalReportViewer1.Show();

                rp.Subreports["crCargoHistoria.rpt"].SetDataSource(ds1);
                rp.Hoja1.SectionFormat.EnableSuppress = false;

                //rp.Subreports["crCargoHistoria2.rpt"].SetDataSource(ds1);
                //rp.Hoja2.SectionFormat.EnableSuppress = false;

                crystalReportViewer1.EnableDrillDown = false;
                var Path = Application.StartupPath;
                rp.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Path + @"\ReporteCargoHistoria.pdf");
                crystalReportViewer1.ReportSource = rp;
                crystalReportViewer1.Show();
        

        }
    }
}
