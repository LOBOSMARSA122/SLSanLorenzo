using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmFactura : Form
    {
        string _FacturacionId;
        string _EmpresaCliente;
        string _EmpresaSede;
        DateTime? _FechaInicio;
        DateTime? _FechaFin;
        string TotalesenLetras;
        int _TipoReporte;
        string[] _ArrayServicios;
        int _TipoFactura;

        public frmFactura(string pstrFacturacionId, string pstrEmpresaCliente, string pstrEmpresaSede,DateTime pdtFechaInicio, DateTime pdtFechaFin, int pintTipoReporte, String[]ArryServicios, int pintTipoFactura)
        {
            _FacturacionId = pstrFacturacionId;
            _EmpresaCliente = pstrEmpresaCliente;
            _EmpresaSede = pstrEmpresaSede;
            _FechaInicio = pdtFechaInicio;
            _FechaFin = pdtFechaFin;
            _TipoReporte = pintTipoReporte;
            _ArrayServicios = ArryServicios;
            _TipoFactura = pintTipoFactura;
            InitializeComponent();
        }

        private void frmFactura_Load(object sender, EventArgs e)
        {
            ShowReport();
        }


        private void ShowReport()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                if (_TipoFactura ==1)
                {
                    var rp = new Reports.crFactura();

                    var CabeceraFactura = new FacturacionBL().CabeceraFactura(_FacturacionId);
                    var DetalleFactura = new FacturacionBL().LlenarGrillaSigesfot("", _EmpresaCliente, _EmpresaSede, _FechaInicio.Value.Date, _FechaFin.Value.Date, -1, _TipoReporte, _ArrayServicios);

                    DataSet ds1 = new DataSet();
                    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CabeceraFactura);
                    dt.TableName = "dtFacturacion";
                    ds1.Tables.Add(dt);

                    DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(DetalleFactura);
                    dt1.TableName = "dtFacturacionDetalle";
                    ds1.Tables.Add(dt1);

                    decimal TotalFacturado = CabeceraFactura[0].Total;
                    TotalesenLetras = Common.Utils.ConvertLetter(TotalFacturado.ToString(), "0") + " Soles " + ".";

                    rp.SetDataSource(ds1);


                    ParameterFieldDefinitions crParameterFieldDefinitions;
                    ParameterFieldDefinition crParameterFieldDefinition;
                    ParameterValues crParameterValues;
                    ParameterDiscreteValue crParameterDiscreteValue;


                    crParameterValues = new ParameterValues();
                    crParameterDiscreteValue = new ParameterDiscreteValue();
                    crParameterDiscreteValue.Value = TotalesenLetras; // TextBox con el valor del Parametro
                    crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
                    crParameterFieldDefinition = crParameterFieldDefinitions["TotalLetras"];


                    crParameterValues = crParameterFieldDefinition.CurrentValues;
                    crParameterValues.Clear();
                    crParameterValues.Add(crParameterDiscreteValue);
                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                    crystalReportViewer2.ReportSource = rp;
                    crystalReportViewer2.Show();
                }

                else if (_TipoFactura ==2)
                {
                    var rp = new Reports.Factura2();

                    var CabeceraFactura = new FacturacionBL().CabeceraFactura_(_FacturacionId);
                    var DetalleFactura = new FacturacionBL().LlenarGrillaSigesfot("", _EmpresaCliente, _EmpresaSede, _FechaInicio.Value.Date, _FechaFin.Value.Date, -1, _TipoReporte, _ArrayServicios);

                    DataSet ds1 = new DataSet();
                    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CabeceraFactura);
                    dt.TableName = "dtFacturacion";
                    ds1.Tables.Add(dt);

                    DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(DetalleFactura);
                    dt1.TableName = "dtFacturacionDetalle";
                    ds1.Tables.Add(dt1);

                    rp.SetDataSource(ds1);

                    crystalReportViewer2.ReportSource = rp;
                    crystalReportViewer2.Show();
                }



            }
            catch (Exception)
            {

                throw;
            }


        }

        private void crystalReportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}
