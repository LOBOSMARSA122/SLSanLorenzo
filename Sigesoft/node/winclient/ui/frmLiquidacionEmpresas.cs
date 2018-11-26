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
                        DateTime? fechaFin = _fFin.Value.Date;

                        string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                        string fechaFin_1 = fechaFin.ToString().Split(' ')[0];

                        //var lista = new ServiceBL().GetListaLiquidacionByEmpresa_Name(ref objOperationResult, fechaInicio, fechaFin, _empresa);
                        
                        string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                        List<LiquidacionEmpresa> ListaLiquidacion = new List<LiquidacionEmpresa>();
                        var empresa_info = new ServiceBL().GetOrganizationEmpresa(ref objOperationResult, _empresa);

                        string nombre = "Liquidaciones de EMPRESA " + empresa_info.v_Name;

                        var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                        var liquidacionEmpresa = new LiquidacionEmpresa();

                        liquidacionEmpresa.v_OrganizationName = empresa_info.v_Name;
                        liquidacionEmpresa.v_Ruc = empresa_info.v_IdentificationNumber;
                        liquidacionEmpresa.v_AddressLocation = empresa_info.v_Address;
                        liquidacionEmpresa.v_TelephoneNumber = empresa_info.v_PhoneNumber;
                        liquidacionEmpresa.v_ContactName = empresa_info.v_ContacName;

                        var detalleEmpresaDeuda = oMedicamentoBl.EmpresaDeudora(empresa_info.v_IdentificationNumber);
                        foreach (var detalle_facturas in detalleEmpresaDeuda)
                        {
                            var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                            liquidacionDetalleEmpresa.v_IdVenta = detalle_facturas.v_IdVenta;
                            liquidacionDetalleEmpresa.FechaCreacion = detalle_facturas.FechaCreacion;
                            liquidacionDetalleEmpresa.FechaVencimiento = detalle_facturas.FechaVencimiento;
                            liquidacionDetalleEmpresa.NetoXCobrar = detalle_facturas.NetoXCobrar;
                            liquidacionDetalleEmpresa.TotalPagado = detalle_facturas.TotalPagado;
                            liquidacionDetalleEmpresa.DocuemtosReferencia = detalle_facturas.DocuemtosReferencia;
                            liquidacionDetalleEmpresa.NroComprobante = detalle_facturas.NroComprobante;
                            liquidacionDetalleEmpresa.Condicion = detalle_facturas.Condicion;
                            listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                        }
                        liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                        ListaLiquidacion.Add(liquidacionEmpresa);


                        Liquidacion_EMPRESA_DETALLE.CreateLiquidacion_EMPRESAS_DETALLE(ruta + nombre + ".pdf", MedicalCenter, fechaInicio_1, fechaFin_1, empresa_info, ListaLiquidacion);
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
                            if (item_1.Condicion == "DEBE")
	                        {
                                if (item.v_Ruc != ruc)
                                {
                                    deudores.Add(item.v_Ruc);
                                }
		                        ruc = item.v_Ruc;
	                        }
                        }
                    }

                    List<LiquidacionEmpresa> ListaLiquidacion = new List<LiquidacionEmpresa>();
                   
                    foreach (var ruc in deudores)
                    {
                        //var obj = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio, ruc);
                        var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc);

                        var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                        var liquidacionEmpresa = new LiquidacionEmpresa();

                        liquidacionEmpresa.v_OrganizationName = empresa.v_Name;
                        liquidacionEmpresa.v_Ruc = empresa.v_IdentificationNumber;
                        liquidacionEmpresa.v_AddressLocation = empresa.v_Address;
                        liquidacionEmpresa.v_TelephoneNumber = empresa.v_PhoneNumber;
                        liquidacionEmpresa.v_ContactName = empresa.v_ContacName;

                        var detalleEmpresaDeuda = oMedicamentoBl.EmpresaDeudora(ruc);
                        foreach (var detalle_facturas in detalleEmpresaDeuda)
                        {
                            var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                            //var obj_2 = oMedicamentoBl.ObtnerNroFacturaCobranza(N_fac);
                            liquidacionDetalleEmpresa.v_IdVenta = detalle_facturas.v_IdVenta;
                            liquidacionDetalleEmpresa.FechaCreacion = detalle_facturas.FechaCreacion;
                            liquidacionDetalleEmpresa.FechaVencimiento = detalle_facturas.FechaVencimiento;
                            liquidacionDetalleEmpresa.NetoXCobrar = detalle_facturas.NetoXCobrar;
                            liquidacionDetalleEmpresa.TotalPagado = detalle_facturas.TotalPagado;
                            liquidacionDetalleEmpresa.DocuemtosReferencia = detalle_facturas.DocuemtosReferencia;
                            liquidacionDetalleEmpresa.NroComprobante = detalle_facturas.NroComprobante;
                            liquidacionDetalleEmpresa.Condicion = detalle_facturas.Condicion;
                            listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                        }
                        liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                        ListaLiquidacion.Add(liquidacionEmpresa);

                    }

                    string fechaInicio_1 = fechaInicio.ToString().Split(' ')[0];
                    string fechaFin_1 = fechaFin.ToString().Split(' ')[0];

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Cuentas X Cobrar - CSL";
                    //query para validar si la empresa es deudora ARNOLD

                    Liquidacion_EMO_EMPRESAS.CreateLiquidacion_EMO_EMPRESAS(ruta + nombre + ".pdf", MedicalCenter, ListaLiquidacion, fechaInicio_1, fechaFin_1);

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

                    DateTime? inicioDeudas = new DateTime(2018, 1, 1, 0, 0, 0);
                    DateTime? FinDeudas = DateTime.Now;
                    var deudoras = new ServiceBL().GetListaLiquidacionByEmpresa(ref objOperationResult, inicioDeudas, FinDeudas);

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

                    List<LiquidacionEmpresa> ListaLiquidacion_1 = new List<LiquidacionEmpresa>();
                    
                    foreach (var ruc in deudores)
                    {
                        DateTime? fechaFin_L1 = DateTime.Now;
                        DateTime? fechaInicio_L1 = DateTime.Now.AddDays(-30);
                        var lista_1 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L1, fechaFin_L1, ruc);

                        List<string> facturas_1 = new List<string>();
                        foreach (var item in lista_1)
                        {
                            var obj_1 = item.detalle.FindAll(p => p.v_NroFactura != "").ToList();
                            foreach (var item_1 in obj_1)
                            {
                                facturas_1.Add(item_1.v_NroFactura);
                            }
                        }

                        foreach (var item in lista_1)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;
                            var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                            if (facturas_1.Count() != 0)
                            {
                                foreach (var N_fac in facturas_1)
                                {
                                    var obj_2 = oMedicamentoBl.ObtnerNroFacturaCobranza(N_fac);
                                    liquidacionDetalleEmpresa.v_IdVenta = obj_2.v_IdVenta;
                                    liquidacionDetalleEmpresa.FechaCreacion = obj_2.FechaCreacion;
                                    liquidacionDetalleEmpresa.FechaVencimiento = obj_2.FechaVencimiento;
                                    liquidacionDetalleEmpresa.NetoXCobrar = obj_2.NetoXCobrar;
                                    liquidacionDetalleEmpresa.TotalPagado = obj_2.TotalPagado;
                                    liquidacionDetalleEmpresa.DocuemtosReferencia = obj_2.DocuemtosReferencia;
                                    liquidacionDetalleEmpresa.NroComprobante = obj_2.NroComprobante;
                                    listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                                }
                            }
                            else
                            {
                                liquidacionDetalleEmpresa.v_IdVenta = "";
                                liquidacionDetalleEmpresa.FechaCreacion = new DateTime(2018, 1, 1, 0, 0, 0);
                                liquidacionDetalleEmpresa.FechaVencimiento = new DateTime(2018, 1, 1, 0, 0, 0);
                                liquidacionDetalleEmpresa.NetoXCobrar = 0;
                                liquidacionDetalleEmpresa.TotalPagado = 0;
                                liquidacionDetalleEmpresa.DocuemtosReferencia = "- - -";
                                liquidacionDetalleEmpresa.NroComprobante = "- - -";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_1.Add(liquidacionEmpresa);
                        }
                    }

                    List<LiquidacionEmpresa> ListaLiquidacion_2 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in deudores)
                    {
                        DateTime? fechaFin_L2 = DateTime.Now.AddDays(-31);
                        DateTime? fechaInicio_L2 = new DateTime(2018, 1, 1, 0, 0, 0);
                        var lista_2 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L2, fechaFin_L2, ruc);

                        List<string> facturas_1 = new List<string>();
                        foreach (var item in lista_2)
                        {
                            var obj_1 = item.detalle.FindAll(p => p.v_NroFactura != "").ToList();
                            foreach (var item_1 in obj_1)
                            {
                                facturas_1.Add(item_1.v_NroFactura);
                            }
                        }

                        foreach (var item in lista_2)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;
                            var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                            if (facturas_1.Count() != 0)
                            {
                                foreach (var N_fac in facturas_1)
                                {
                                    var obj_2 = oMedicamentoBl.ObtnerNroFacturaCobranza(N_fac);
                                    liquidacionDetalleEmpresa.v_IdVenta = obj_2.v_IdVenta;
                                    liquidacionDetalleEmpresa.FechaCreacion = obj_2.FechaCreacion;
                                    liquidacionDetalleEmpresa.FechaVencimiento = obj_2.FechaVencimiento;
                                    liquidacionDetalleEmpresa.NetoXCobrar = obj_2.NetoXCobrar;
                                    liquidacionDetalleEmpresa.TotalPagado = obj_2.TotalPagado;
                                    liquidacionDetalleEmpresa.DocuemtosReferencia = obj_2.DocuemtosReferencia;
                                    liquidacionDetalleEmpresa.NroComprobante = obj_2.NroComprobante;
                                    listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                                }
                            }
                            else {
                                liquidacionDetalleEmpresa.v_IdVenta = "";
                                liquidacionDetalleEmpresa.FechaCreacion = new DateTime(2018, 1, 1, 0, 0, 0);
                                liquidacionDetalleEmpresa.FechaVencimiento = new DateTime(2018, 1, 1, 0, 0, 0);
                                liquidacionDetalleEmpresa.NetoXCobrar = 0;
                                liquidacionDetalleEmpresa.TotalPagado = 0;
                                liquidacionDetalleEmpresa.DocuemtosReferencia = "- - -";
                                liquidacionDetalleEmpresa.NroComprobante = "- - -";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_2.Add(liquidacionEmpresa);
                        }
                    }
                    
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "Liquidaciones de EMPRESAS - CSL";

                    DateTime? fechaFin_1 = DateTime.Now;
                    DateTime? fechaInicio_1 = DateTime.Now.AddDays(-30);
                    string fechaInicio_2 = fechaInicio_1.ToString().Split(' ')[0];
                    string fechaFin_2 = fechaFin_1.ToString().Split(' ')[0];
                    LiquidacionCuentasPorCobrar.CreateLiquidacionCuentasPorCobrar(ruta + nombre + ".pdf", MedicalCenter, ListaLiquidacion_1, fechaInicio_2, fechaFin_2, ListaLiquidacion_2);
                    this.Enabled = true;
                }
            }
            
            else if (rbLiqPendFacturar.Checked)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;

                    var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                    OperationResult objOperationResult = new OperationResult();

                    DateTime? inicioDeudas = new DateTime(2018, 1, 1, 0, 0, 0);
                    DateTime? FinDeudas = DateTime.Now;
                    var deudoras = new ServiceBL().GetListaLiquidacionByEmpresa_SinFacturar(ref objOperationResult, inicioDeudas, FinDeudas);

                    List<string> deudores = new List<string>();
                    string ruc_emp = "";
                    decimal _debe_calculo = 0;
                    foreach (var item in deudoras)
                    {
                        foreach (var item_1 in item.detalle)
                        {
                            _debe_calculo += (decimal)item_1.d_Debe + (decimal)item_1.d_Pago;
                        }
                        if (_debe_calculo !=0)
                        {
                            deudores.Add(item.v_Ruc);
                        }
                        //ruc_emp = item.v_Ruc;
                        _debe_calculo = 0;
                    }

                    List<LiquidacionEmpresa> ListaLiquidacion_1 = new List<LiquidacionEmpresa>();

                    foreach (var ruc in deudores)
                    {
                        DateTime? fechaFin_L1 = DateTime.Now;
                        DateTime? fechaInicio_L1 = DateTime.Now.AddDays(-30);
                        var lista_1 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L1, fechaFin_L1, ruc);

                        List<string> facturas_1 = new List<string>();
                        foreach (var item in lista_1)
                        {
                            var obj_1 = item.detalle.FindAll(p => p.v_NroFactura == "").ToList();
                            foreach (var item_1 in obj_1)
                            {
                                facturas_1.Add(item_1.v_NroFactura);
                            }
                        }

                        foreach (var item in lista_1)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;
                            var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                            if (facturas_1.Count() != 0)
                            {
                                foreach (var detalle in item.detalle)
                                {
                                    liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                    liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                    liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                    liquidacionDetalleEmpresa.v_NroFactura = "";
                                    listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                                }
                            }
                            else
                            {
                                liquidacionDetalleEmpresa.d_Debe = 0;
                                liquidacionDetalleEmpresa.d_Pago = 0;
                                liquidacionDetalleEmpresa.d_Total = 0;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }

                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_1.Add(liquidacionEmpresa);
                        }
                    }

                    List<LiquidacionEmpresa> ListaLiquidacion_2 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in deudores)
                    {
                        DateTime? fechaFin_L2 = DateTime.Now.AddDays(-31);
                        DateTime? fechaInicio_L2 = new DateTime(2018, 1, 1, 0, 0, 0);
                        var lista_2 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L2, fechaFin_L2, ruc);

                        List<string> facturas_1 = new List<string>();
                        foreach (var item in lista_2)
                        {
                            var obj_1 = item.detalle.FindAll(p => p.v_NroFactura == "").ToList();
                            foreach (var item_1 in obj_1)
                            {
                                facturas_1.Add(item_1.v_NroFactura);
                            }
                        }

                        foreach (var item in lista_2)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;
                            var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                            if (facturas_1.Count() != 0)
                            {
                                foreach (var detalle in item.detalle)
                                {
                                    liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                    liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                    liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                    liquidacionDetalleEmpresa.v_NroFactura = "";
                                    listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                                }
                            }
                            else
                            {
                                liquidacionDetalleEmpresa.d_Debe = 0;
                                liquidacionDetalleEmpresa.d_Pago = 0;
                                liquidacionDetalleEmpresa.d_Total = 0;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }

                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_2.Add(liquidacionEmpresa);
                        }
                    }

                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "EMPRESAS POR FACTURAR - CSL";

                    DateTime? fechaFin_1 = DateTime.Now;
                    DateTime? fechaInicio_1 = DateTime.Now.AddDays(-30);
                    string fechaInicio_2 = fechaInicio_1.ToString().Split(' ')[0];
                    string fechaFin_2 = fechaFin_1.ToString().Split(' ')[0];
                    Liquidaciones_Pendientes_Facturass.CreateLiquidaciones_Pendientes_Facturass(ruta + nombre + ".pdf", MedicalCenter, ListaLiquidacion_1, fechaInicio_2, fechaFin_2, ListaLiquidacion_2);
                    this.Enabled = true;
                }
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
