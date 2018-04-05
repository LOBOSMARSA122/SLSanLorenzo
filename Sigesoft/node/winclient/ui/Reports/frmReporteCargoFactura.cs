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
    public partial class frmReporteCargoFactura : Form
    {
        string Mes = "";
        string Fecha = "";
        string NroFactura = "";
        string NroTrabajadores = "";
        string Empresa = "";

        public frmReporteCargoFactura(string pMes, string pFecha, string pNroFactura, string pNroTrabajadores, string pEmpresa)
        {
            InitializeComponent();
            Mes = pMes;
            Fecha = pFecha;
            NroFactura = pNroFactura;
            NroTrabajadores = pNroTrabajadores;
            Empresa = pEmpresa;
        }

        private void frmReporteCargoFactura_Load(object sender, EventArgs e)
        {
            ReporteCargoFactura oReporteCargoFactura = new ReporteCargoFactura();
            List<ReporteCargoFactura> Lista = new List<ReporteCargoFactura>();
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            var rp = new Reports.crCargoFactura();
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenterSede();

            var Parametro =  BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 259, null);

            oReporteCargoFactura.LogoEmpresaPropietaria = MedicalCenter.b_Image;
            oReporteCargoFactura.RazonSocialEmpresaPropietaria = MedicalCenter.v_Name;
            oReporteCargoFactura.FechaActual =  DateTime.Now.ToString("dd/MMMM/yyyy");
            oReporteCargoFactura.SedeEmpresaPropietaria = MedicalCenter.v_Sede;
            oReporteCargoFactura.RazonSocialEmpresaCliente = Empresa;
            oReporteCargoFactura.Mes = Mes;
            oReporteCargoFactura.Fecha = Fecha;
            oReporteCargoFactura.NroFactura = NroFactura;
            oReporteCargoFactura.NroTrabajadores = NroTrabajadores;
            oReporteCargoFactura.EmailRepresentanteLegalEP = MedicalCenter.v_Mail;
            oReporteCargoFactura.EmailContactoEP = MedicalCenter.v_EmailContacto;
            oReporteCargoFactura.Parametro = Parametro[0].Value1 + " - " + Parametro[0].Value2;
            oReporteCargoFactura.AnioMes = DateTime.Now.ToString("yyyy/MM");
            oSystemParameterBL.ActualizarValorParametro(256, int.Parse(Parametro[0].Value2.ToString()));

            Lista.Add(oReporteCargoFactura);

            DataSet ds = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
            dt.TableName = "dtCargoFactura";
            ds.Tables.Add(dt);
            rp.SetDataSource(ds);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();
        }
    }
}
