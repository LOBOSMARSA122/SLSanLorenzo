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

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmProduccionProfesionalImprimir : Form
    {
        private DateTime? _FechaInicio;
        private DateTime? _FechaFin;
        private string _EmpresaCliente;
        private string _FilterExpression;
        private string _Usuario;
        private string _Profesional;
        private string _Componente;
        private int _CategoriaId;
        private string _EmpresaClienteCabecera;

        public frmProduccionProfesionalImprimir(DateTime FechaInicio, DateTime FechaFin, string EmpresaCliente, string FilterExpression, string Usuario, string Profesional, string Compomente, int CategoriaId, string EmpresaClienteCabecera)
        {
            InitializeComponent();
            _FechaInicio = FechaInicio;
            _FechaFin = FechaFin;
            _EmpresaCliente = EmpresaCliente;
            _FilterExpression = FilterExpression;
            _Usuario = Usuario;
            _Profesional = Profesional;
            _Componente = Compomente;
            _CategoriaId = CategoriaId;
            _EmpresaClienteCabecera = EmpresaClienteCabecera;
        }

        private void frmProduccionProfesionalImprimir_Load(object sender, EventArgs e)
        {
              using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
       
        }
        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crProduccionProfesional();

            var aptitudeCertificate = new ServiceBL().ReporteProduccionProfesional(_FechaInicio, _FechaFin, _EmpresaCliente, _FilterExpression, _Usuario, _Profesional, _Componente, _CategoriaId, _EmpresaClienteCabecera);
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

            dt.TableName = "dtProduccionProfesional";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }

    }
}
