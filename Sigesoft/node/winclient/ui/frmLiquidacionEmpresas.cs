using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLiquidacionEmpresas : Form
    {
        private DateTime? _fInicio;
        private DateTime? _fFin;
        private string _empresa = null;
        public frmLiquidacionEmpresas(DateTime? dtpDateTimeStar, DateTime? dptDateTimeEnd, string empresa)
        {
            InitializeComponent();
            _fInicio = dtpDateTimeStar;
            _fFin = dptDateTimeEnd;
            _empresa = empresa;

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            MedicamentoBl oMedicamentoBl = new MedicamentoBl();
            if (rbEstadoCuentaEmpresa.Checked)
            {
                if (_empresa != null)
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                    {
                        this.Enabled = false;

                        var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                        OperationResult objOperationResult = new OperationResult();

                        DateTime? fechaInicio = _fInicio.Value.Date;
                        DateTime? fechaFin = _fFin.Value.Date.AddDays(1);

                        string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                        string fechaFin_1 = fechaFin.ToString().Split(' ')[0];

                        var lista = new ServiceBL().GetListaLiquidacionByEmpresa_Name(ref objOperationResult, fechaInicio, fechaFin, _empresa);
                        
                        string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                        string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                        string nombre = "Liquidaciones de EMPRESA - CSL";

                        
                        List<string> facturas = new List<string>();
                        foreach (var item in lista)
                        {
                            var obj = item.detalle.FindAll(p =>p.v_NroFactura != "").ToList();
                            foreach (var item_1 in obj)
                            {
                                facturas.Add(item_1.v_NroFactura);
                            }
                        }

                        var listFacturaCobranza = new List<FacturaCobranza>();
                        foreach (var nroFactura in facturas)
                        {
                            var oFacturaCobranza = new FacturaCobranza();
                            var obj = oMedicamentoBl.ObtnerNroFacturaCobranza(nroFactura);

                            oFacturaCobranza.v_IdVenta = obj.v_IdVenta;
                            oFacturaCobranza.FechaCreacion = obj.FechaCreacion;
                            oFacturaCobranza.FechaVencimiento = obj.FechaVencimiento;
                            oFacturaCobranza.NetoXCobrar = obj.NetoXCobrar;
                            oFacturaCobranza.TotalPagado = obj.TotalPagado;
                            oFacturaCobranza.DocuemtosReferencia = obj.DocuemtosReferencia;
                            oFacturaCobranza.NroComprobante = obj.NroComprobante;       
                            listFacturaCobranza.Add(oFacturaCobranza);
                        }

                      
                        var empresa_info = new ServiceBL().GetOrganizationEmpresa(ref objOperationResult, _empresa);


                        Liquidacion_EMPRESA_DETALLE.CreateLiquidacion_EMPRESAS_DETALLE(ruta + nombre + ".pdf", MedicalCenter, lista, fechaInicio_1, fechaFin_1, empresa_info, listFacturaCobranza);
                        this.Enabled = true;
                    }
                }
                else
                {
                    btnGenerar.Enabled = false;
                }
            }
            else if (rbCuentasXCobrar.Checked)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                    OperationResult objOperationResult = new OperationResult();

                    DateTime? fechaInicio = _fInicio.Value.Date;
                    DateTime? fechaFin = _fFin.Value.Date.AddDays(1);
                    var deudoras = new ServiceBL().GetListaLiquidacionByEmpresa(ref objOperationResult, fechaInicio, fechaFin);

                    List<string> deudores = new List<string>();
                    foreach (var item in deudoras)
                    {
                        var empDeud = oMedicamentoBl.EmpresaDeudora(item.v_Ruc);
                        string ruc = "";
                        foreach (var item_1 in empDeud)
                        {
                            if (item_1.NetoXCobrar != 0)
	                        {
                                if (item.v_Ruc != ruc)
                                {
                                    deudores.Add(item.v_Ruc);
                                }
		                        ruc = item.v_Ruc;
	                        }
                        }
                    }

                    var listaLiquidacionEmpresa = new List<LiquidacionEmpresa>();

                    var listFacturaCobranza = new List<FacturaCobranza>();
                    foreach (var ruc in deudores)
                    {
                        var obj = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio, fechaFin, ruc);

                        List<string> facturas = new List<string>();

                        foreach (var item in obj)
                        {
                            var obj_1 = item.detalle.FindAll(p => p.v_NroFactura != "").ToList();
                            foreach (var item_1 in obj_1)
                            {
                                facturas.Add(item_1.v_NroFactura);
                            }
                        }

                        
                        foreach (var nroFactura in facturas)
                        {
                            var oFacturaCobranza = new FacturaCobranza();
                            var obj_2 = oMedicamentoBl.ObtnerNroFacturaCobranza(nroFactura);

                            oFacturaCobranza.v_IdVenta = obj_2.v_IdVenta;
                            oFacturaCobranza.FechaCreacion = obj_2.FechaCreacion;
                            oFacturaCobranza.FechaVencimiento = obj_2.FechaVencimiento;
                            oFacturaCobranza.NetoXCobrar = obj_2.NetoXCobrar;
                            oFacturaCobranza.TotalPagado = obj_2.TotalPagado;
                            oFacturaCobranza.DocuemtosReferencia = obj_2.DocuemtosReferencia;
                            oFacturaCobranza.NroComprobante = obj_2.NroComprobante;
                            listFacturaCobranza.Add(oFacturaCobranza);
                        }

                    }

                    string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                    string fechaFin_1 = fechaFin.ToString().Split(' ')[0];
                    var lista = new ServiceBL().GetListaLiquidacionByEmpresa(ref objOperationResult, fechaInicio, fechaFin);

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Cuentas X Cobrar - CSL";
                    //query para validar si la empresa es deudora ARNOLD
                    
                    Liquidacion_EMO_EMPRESAS.CreateLiquidacion_EMO_EMPRESAS(ruta + nombre + ".pdf", MedicalCenter, lista, fechaInicio_1, fechaFin_1);

                    this.Enabled = true;
                }
            }
            else if (rbResumenCuentasXCobrar.Checked)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                    OperationResult objOperationResult = new OperationResult();


                    DateTime? fechaFin = DateTime.Now;
                    DateTime? fechaInicio = DateTime.Now.AddDays(-30);
                    var lista_1 = new ServiceBL().GetListaLiquidacionByEmpresa(ref objOperationResult, fechaInicio, fechaFin);

                    DateTime? _fechaFin = DateTime.Now.AddDays(-31);
                    // DateTime? _fechaInicio = DateTime.Now.AddDays(-325);
                    DateTime? _fechaInicio = new DateTime(2018, 1, 1, 0, 0, 0);
                    var lista_2 = new ServiceBL().GetListaLiquidacionByEmpresa(ref objOperationResult, _fechaInicio, _fechaFin);

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Liquidaciones de EMPRESA - CSL";

                    string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                    string fechaFin_1 = fechaFin.ToString().Split(' ')[0];
                    LiquidacionCuentasPorCobrar.CreateLiquidacionCuentasPorCobrar(ruta + nombre + ".pdf", MedicalCenter, lista_1, fechaInicio_1, fechaFin_1, lista_2);
                    this.Enabled = true;
                }
            }
            
            else if (rbLiqPendFacturar.Checked)
            {

            }
            else {
                btnGenerar.Enabled = false;
            }
        }

        private void rbEstadoCuentaEmpresa_CheckedChanged(object sender, EventArgs e)
        {
            btnGenerar.Enabled = true;
        }

        private void rbCuentasXCobrar_CheckedChanged(object sender, EventArgs e)
        {
            btnGenerar.Enabled = true;
        }

        private void rbResumenCuentasXCobrar_CheckedChanged(object sender, EventArgs e)
        {
            btnGenerar.Enabled = true;
        }

        private void rbLiqPendFacturar_CheckedChanged(object sender, EventArgs e)
        {
            btnGenerar.Enabled = true;
        }
    }
}
