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
using Sigesoft.Node.WinClient.BE.Custom;

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
                    //deudores = deudores.OrderBy(o => o).ToList();

                    List<string> nombres_Empresa = new List<string>();
                    foreach (var ruc_1 in deudores)
                    {
                        var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc_1);
                        nombres_Empresa.Add(empresa.v_Name);
                    }

                    nombres_Empresa = nombres_Empresa.OrderBy(o => o).ToList();
                    
                    List<string> rucs = new List<string>();
                    foreach (var item in nombres_Empresa)
                    {
                        var empresa = new ServiceBL().GetOrganizationEmpresa(ref objOperationResult, item);
                        rucs.Add(empresa.v_IdentificationNumber);
                    }

                    List<LiquidacionEmpresa> ListaLiquidacion = new List<LiquidacionEmpresa>();

                    foreach (var ruc in rucs)
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

                    List<string> nombres_Empresa = new List<string>();
                    foreach (var ruc_1 in deudores)
                    {
                        var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc_1);
                        nombres_Empresa.Add(empresa.v_Name);
                    }

                    nombres_Empresa = nombres_Empresa.OrderBy(o => o).ToList();

                    List<string> rucs = new List<string>();
                    foreach (var item in nombres_Empresa)
                    {
                        var empresa = new ServiceBL().GetOrganizationEmpresa(ref objOperationResult, item);
                        rucs.Add(empresa.v_IdentificationNumber);
                    }

                    List<LiquidacionEmpresa> ListaLiquidacion_1 = new List<LiquidacionEmpresa>();

                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaFin_L1 = DateTime.Now;
                        DateTime? fechaInicio_L1 = DateTime.Now.AddDays(-30);
                        var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc);
                        var lista_1 = oMedicamentoBl.EmpresaDeudora_Fechas_Fac(ruc, fechaInicio_L1, fechaFin_L1 );

                        var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                        var liquidacionEmpresa = new LiquidacionEmpresa();

                        liquidacionEmpresa.v_OrganizationName = empresa.v_Name;
                        liquidacionEmpresa.v_Ruc = empresa.v_IdentificationNumber;
                        liquidacionEmpresa.v_AddressLocation = empresa.v_Address;
                        liquidacionEmpresa.v_TelephoneNumber = empresa.v_PhoneNumber;
                        liquidacionEmpresa.v_ContactName = empresa.v_ContacName;

                        var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                        if (lista_1.Count() != 0)
                        {
                            foreach (var detalle_facturas in lista_1)
                            {
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

                    List<LiquidacionEmpresa> ListaLiquidacion_2 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaFin_L2 = DateTime.Now.AddDays(-31);
                        DateTime? fechaInicio_L2 = new DateTime(2018, 1, 1, 0, 0, 0);
                        var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc);
                        var lista_2 = oMedicamentoBl.EmpresaDeudora_Fechas_Fac(ruc, fechaInicio_L2.Value, fechaFin_L2.Value);

                        var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                        var liquidacionEmpresa = new LiquidacionEmpresa();

                        liquidacionEmpresa.v_OrganizationName = empresa.v_Name;
                        liquidacionEmpresa.v_Ruc = empresa.v_IdentificationNumber;
                        liquidacionEmpresa.v_AddressLocation = empresa.v_Address;
                        liquidacionEmpresa.v_TelephoneNumber = empresa.v_PhoneNumber;
                        liquidacionEmpresa.v_ContactName = empresa.v_ContacName;
                        
                        var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();

                        if (lista_2.Count() != 0)
                        {
                            foreach (var detalle_facturas in lista_2)
                            {
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
                        ListaLiquidacion_2.Add(liquidacionEmpresa); 
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

                    var deudoras = new ServiceBL().GetListaLiquidacionByEmpresa(ref objOperationResult, inicioDeudas, FinDeudas);

                    List<string> deudores = new List<string>();
                    foreach (var item in deudoras)
                    {
                        var empDeud = oMedicamentoBl.EmpresaDeudora(item.v_Ruc);
                        string ruc = "";
                        foreach (var item_1 in item.detalle)
                        {
                            if (item_1.v_NroFactura == "")
                            {
                                if (item.v_Ruc != ruc)
                                {
                                    deudores.Add(item.v_Ruc);
                                }
                                ruc = item.v_Ruc;
                            }
                        }
                    }

                    List<string> nombres_Empresa = new List<string>();
                    foreach (var ruc_1 in deudores)
                    {
                        var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc_1);
                        nombres_Empresa.Add(empresa.v_Name);
                    }

                    nombres_Empresa = nombres_Empresa.OrderBy(o => o).ToList();

                    List<string> rucs = new List<string>();
                    foreach (var item in nombres_Empresa)
                    {
                        var empresa = new ServiceBL().GetOrganizationEmpresa(ref objOperationResult, item);
                        rucs.Add(empresa.v_IdentificationNumber);
                    }
                    //var deudoras = new ServiceBL().GetListaLiquidaciones_Deudas(ref objOperationResult, inicioDeudas, FinDeudas);

                    //List<string> deudores = new List<string>();
                    //string idEmpresa = "";
                    //foreach (var item in deudoras)
                    //{
                    //    if (item.v_OrganizationId != idEmpresa)
                    //    {
                    //        deudores.Add(item.v_OrganizationId);
                    //    }
                    //    idEmpresa = item.v_OrganizationId;
                    //}
                    int años_atras = 2018;
                    if (DateTime.Now.Year != años_atras)
                    {
                        años_atras = DateTime.Now.Year-1;
                    }
                    else if (años_atras == 2018)
                    {
                        años_atras = años_atras - 1;
                    }
                    else
                    {
                        años_atras = 2018;
                    }
                    #region años atrás
                    List<LiquidacionEmpresa> ListaAños_Atras = new List<LiquidacionEmpresa>();

                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L1 = new DateTime(2018, 1, 1, 0, 0, 0);
                        DateTime? fechaFin_L1 = new DateTime(años_atras, 12, 31, 0, 0, 0);

                        var lista_anterior = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L1, fechaFin_L1, ruc);

                        foreach (var item in lista_anterior)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaAños_Atras.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion

                    int año_inicio = 2018;
                    if (DateTime.Now.Year != año_inicio)
                    {
                        año_inicio = DateTime.Now.Year;
                    }
                    else
                    {
                        año_inicio = 2018;
                    }
                    #region enero
                    List<LiquidacionEmpresa> ListaLiquidacion_1 = new List<LiquidacionEmpresa>();

                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L1 = new DateTime(año_inicio, 1, 1, 0, 0, 0);
                        DateTime? fechaFin_L1 = new DateTime(año_inicio, 1, 31, 0, 0, 0);
                        
                        var lista_1 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L1, fechaFin_L1, ruc);

                        foreach (var item in lista_1)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_1.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region febrero
                    List<LiquidacionEmpresa> ListaLiquidacion_2 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L2 = new DateTime(año_inicio, 2, 1, 0, 0, 0);
                        DateTime? fechaFin_L2 = new DateTime(año_inicio, 2, 28, 0, 0, 0);
                        
                        var lista_2 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L2, fechaFin_L2, ruc);

                        foreach (var item in lista_2)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_2.Add(liquidacionEmpresa);
                        }

                    }
                    #endregion
                    #region marzo
                    List<LiquidacionEmpresa> ListaLiquidacion_3 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L3 = new DateTime(año_inicio, 3, 1, 0, 0, 0);
                        DateTime? fechaFin_L3 = new DateTime(año_inicio, 3, 31, 0, 0, 0);
                        
                        var lista_3 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L3, fechaFin_L3, ruc);

                        foreach (var item in lista_3)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_3.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region abril
                    List<LiquidacionEmpresa> ListaLiquidacion_4 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L4 = new DateTime(año_inicio, 4, 1, 0, 0, 0);
                        DateTime? fechaFin_L4 = new DateTime(año_inicio, 4, 30, 0, 0, 0);
                        
                        var lista_4 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L4, fechaFin_L4, ruc);

                        foreach (var item in lista_4)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_4.Add(liquidacionEmpresa);
                        }

                    }
                    #endregion
                    #region mayo
                    List<LiquidacionEmpresa> ListaLiquidacion_5 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L5 = new DateTime(año_inicio, 5, 1, 0, 0, 0);
                        DateTime? fechaFin_L5 = new DateTime(año_inicio, 5, 31, 0, 0, 0);
                        
                        var lista_5 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L5, fechaFin_L5, ruc);

                        foreach (var item in lista_5)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_5.Add(liquidacionEmpresa);
                        }

                    }
                    #endregion
                    #region junio
                    List<LiquidacionEmpresa> ListaLiquidacion_6 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L6 = new DateTime(año_inicio, 6, 1, 0, 0, 0);
                        DateTime? fechaFin_L6 = new DateTime(año_inicio, 6, 30, 0, 0, 0);
                        
                        var lista_6 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L6, fechaFin_L6, ruc);

                        foreach (var item in lista_6)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_6.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region julio
                    List<LiquidacionEmpresa> ListaLiquidacion_7 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L7 = new DateTime(año_inicio, 7, 1, 0, 0, 0);
                        DateTime? fechaFin_L7 = new DateTime(año_inicio, 7, 31, 0, 0, 0);
                        
                        var lista_7 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L7, fechaFin_L7, ruc);

                        foreach (var item in lista_7)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_7.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region agosto
                    List<LiquidacionEmpresa> ListaLiquidacion_8 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L8 = new DateTime(año_inicio, 8, 1, 0, 0, 0);
                        DateTime? fechaFin_L8 = new DateTime(año_inicio, 8, 31, 0, 0, 0);
                        
                        var lista_8 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L8, fechaFin_L8, ruc);

                        foreach (var item in lista_8)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_8.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region setiembre
                    List<LiquidacionEmpresa> ListaLiquidacion_9 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L9 = new DateTime(año_inicio, 9, 1, 0, 0, 0);
                        DateTime? fechaFin_L9 = new DateTime(año_inicio, 9, 30, 0, 0, 0);
                        
                        var lista_9 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L9, fechaFin_L9, ruc);

                        foreach (var item in lista_9)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_9.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region octubre
                    List<LiquidacionEmpresa> ListaLiquidacion_10 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L10 = new DateTime(año_inicio, 10, 1, 0, 0, 0);
                        DateTime? fechaFin_L10 = new DateTime(año_inicio, 10, 31, 0, 0, 0);
                        
                        var lista_10 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L10, fechaFin_L10, ruc);

                        foreach (var item in lista_10)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_10.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region noviembre
                    List<LiquidacionEmpresa> ListaLiquidacion_11 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L11 = new DateTime(año_inicio, 11, 1, 0, 0, 0);
                        DateTime? fechaFin_L11 = new DateTime(año_inicio, 11, 30, 0, 0, 0);
                        
                        var lista_11 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L11, fechaFin_L11, ruc);

                        foreach (var item in lista_11)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                             var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);                                
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_11.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    #region diciembre
                    List<LiquidacionEmpresa> ListaLiquidacion_12 = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_L12 = new DateTime(año_inicio, 12, 1, 0, 0, 0);
                        DateTime? fechaFin_L12 = new DateTime(año_inicio, 12, 31, 0, 0, 0);
                        
                        var lista_12 = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_L12, fechaFin_L12, ruc);

                        foreach (var item in lista_12)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_12.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion

                    #region total
                    List<LiquidacionEmpresa> ListaLiquidacion_total = new List<LiquidacionEmpresa>();
                    foreach (var ruc in rucs)
                    {
                        DateTime? fechaInicio_LT = new DateTime(2018, 1, 1, 0, 0, 0);
                        DateTime? fechaFin_LT = DateTime.Now;
                        var lista_total = new ServiceBL().GetListaLiquidacionByEmpresa_Id(ref objOperationResult, fechaInicio_LT, fechaFin_LT, ruc);

                        foreach (var item in lista_total)
                        {
                            var listaLiquidacionEmpresaDetalle = new List<LiquidacionEmpresaDetalle>();
                            var liquidacionEmpresa = new LiquidacionEmpresa();

                            liquidacionEmpresa.v_OrganizationName = item.v_OrganizationName;
                            liquidacionEmpresa.v_Ruc = item.v_Ruc;
                            liquidacionEmpresa.v_AddressLocation = item.v_AddressLocation;
                            liquidacionEmpresa.v_TelephoneNumber = item.v_TelephoneNumber;
                            liquidacionEmpresa.v_ContactName = item.v_ContactName;

                            var detalles = item.detalle.FindAll(p => p.v_NroFactura == "");
                            foreach (var detalle in detalles)
                            {
                                var liquidacionDetalleEmpresa = new LiquidacionEmpresaDetalle();
                                liquidacionDetalleEmpresa.d_Debe = detalle.d_Debe;
                                liquidacionDetalleEmpresa.d_Pago = detalle.d_Pago;
                                liquidacionDetalleEmpresa.d_Total = detalle.d_Total;
                                liquidacionDetalleEmpresa.v_LiquidacionId = detalle.v_LiquidacionId;
                                liquidacionDetalleEmpresa.v_NroFactura = "";
                                listaLiquidacionEmpresaDetalle.Add(liquidacionDetalleEmpresa);
                            }
                            liquidacionEmpresa.detalle = listaLiquidacionEmpresaDetalle;
                            ListaLiquidacion_total.Add(liquidacionEmpresa);
                        }
                    }
                    #endregion
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                    string nombre = "EMPRESAS POR FACTURAR - CSL";

                    DateTime? fechaFin_1 = DateTime.Now;
                    DateTime? fechaInicio_1 = DateTime.Now.AddDays(-30);
                    string fechaInicio_2 = fechaInicio_1.ToString().Split(' ')[0];
                    string fechaFin_2 = fechaFin_1.ToString().Split(' ')[0];
                    Liquidaciones_Pendientes_Facturass.CreateLiquidaciones_Pendientes_Facturass(ruta + nombre + ".pdf", MedicalCenter, ListaLiquidacion_1, fechaInicio_2, fechaFin_2, ListaLiquidacion_2, ListaLiquidacion_3, ListaLiquidacion_4, ListaLiquidacion_5, ListaLiquidacion_6, ListaLiquidacion_7, ListaLiquidacion_8,
                        ListaLiquidacion_9, ListaLiquidacion_10, ListaLiquidacion_11, ListaLiquidacion_12, ListaLiquidacion_total, ListaAños_Atras);
                    this.Enabled = true;
                }
            }
            else if (rbLiqPendFacturarDETALLE.Checked)
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
                        foreach (var item_1 in item.detalle)
                        {
                            if (item_1.v_NroFactura == "")
                            {
                                if (item.v_Ruc != ruc)
                                {
                                    deudores.Add(item.v_Ruc);
                                }
                                ruc = item.v_Ruc;
                            }
                        }
                    }

                    //List<string> nombres_Empresa = new List<string>();
                    //foreach (var ruc_1 in deudores)
                    //{
                    //    var empresa = new ServiceBL().GetOrganizationRuc(ref objOperationResult, ruc_1);
                    //    nombres_Empresa.Add(empresa.v_Name);
                    //}

                    //nombres_Empresa = nombres_Empresa.OrderBy(o => o).ToList();

                    //List<string> rucs = new List<string>();
                    //foreach (var item in nombres_Empresa)
                    //{
                    //    var empresa = new ServiceBL().GetOrganizationEmpresa(ref objOperationResult, item);
                    //    rucs.Add(empresa.v_IdentificationNumber);
                    //}
                    var deudoraas = new ServiceBL().GetListaLiquidaciones_Deudas(ref objOperationResult, inicioDeudas, FinDeudas);

                    List<LiquidacionesConsolidado> ListaLiquidacion_detalle = new List<LiquidacionesConsolidado>();
                    foreach (var item in deudoraas)
                    {
                        var empresa = new ServiceBL().GetOrganizationId(ref objOperationResult, item.v_OrganizationId);
                        var Datos = new ServiceBL().GetLiquidacionConsolidada(ref objOperationResult, item.v_NroLiquidacion);
                        
                        var listaLiquidacionEmpresaDetalle = new List<LiquidacionesConsolidadoDetalle>();
                        var empresaLiquidacion = new LiquidacionesConsolidado();

                        empresaLiquidacion.v_OrganizationName = empresa.v_Name;
                        empresaLiquidacion.v_Ruc = empresa.v_IdentificationNumber;
                        empresaLiquidacion.v_AddressLocation = empresa.v_Address;
                        empresaLiquidacion.v_TelephoneNumber = empresa.v_PhoneNumber;
                        empresaLiquidacion.v_ContactName = empresa.v_ContacName;
                        empresaLiquidacion.v_NroLiquidacion = item.v_NroLiquidacion;
                        empresaLiquidacion.d_creaionLiq = item.Creacion_Liquidacion;
                        foreach (var detalle in Datos)
                        {
                            var empresaLiquidacionDetalle = new LiquidacionesConsolidadoDetalle();
                            empresaLiquidacionDetalle.v_Paciente = detalle.v_Paciente;
                            empresaLiquidacionDetalle.d_exam = detalle.d_exam;
                            empresaLiquidacionDetalle.d_price = detalle.d_price;
                            empresaLiquidacionDetalle.v_UsuarRecord = detalle.v_UsuarRecord;
                            empresaLiquidacionDetalle.v_CenterCost = detalle.v_CenterCost;
                            listaLiquidacionEmpresaDetalle.Add(empresaLiquidacionDetalle);
                        }
                        empresaLiquidacion.detalle = listaLiquidacionEmpresaDetalle;
                        ListaLiquidacion_detalle.Add(empresaLiquidacion);

                    }
                    ListaLiquidacion_detalle = ListaLiquidacion_detalle.OrderBy(o => o.v_OrganizationName).ToList();
                    //List<string> deudores = new List<string>();
                    //string idEmpresa = "";
                    //foreach (var item in deudoras)
                    //{
                    //    if (item.v_OrganizationId != idEmpresa)
                    //    {
                    //        deudores.Add(item.v_OrganizationId);
                    //    }
                    //    idEmpresa = item.v_OrganizationId;
                    //}
                   
                    string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                    string fecha = DateTime.Now.ToString().Split('/')[0] + "" + DateTime.Now.ToString().Split('/')[1] + "" + DateTime.Now.ToString().Split('/')[2];
                    string a = fecha.ToString();
                    string nombre = "EMPRESAS LIQUIDADAS POR FACTURAR DETALLE - CSL";

                    DateTime? fechaFin_1 = DateTime.Now;
                    DateTime? fechaInicio_1 = DateTime.Now.AddDays(-30);
                    string fechaInicio_2 = fechaInicio_1.ToString().Split(' ')[0];
                    string fechaFin_2 = fechaFin_1.ToString().Split(' ')[0];
                    Liquidaciones_Pendientes_Facturas_Detalle.CreateLiquidaciones_Pendientes_Facturas_Detalle(ruta + nombre + ".pdf", MedicalCenter, ListaLiquidacion_detalle, fechaInicio_2, fechaFin_2);
                    this.Enabled = true;
                }
            }
            else if (rbEmpresasSLSF.Checked)
            {
                GerenciaCreditoBl oGerenciaCreditoBl = new GerenciaCreditoBl();
                

                this.Enabled = false;

                #region total_sl
                List<GerenciaCredito> _listaSinLiquidar_Total = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Total1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_LT = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime? fechaFin_LT = DateTime.Now;

                _listaSinLiquidar_Total = oGerenciaCreditoBl.Filter(fechaInicio_LT.Value, fechaFin_LT.Value);
                _listaSinLiquidar_Total1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Total);

                #endregion

                List<string> deudores = new List<string>();

                foreach (var item in _listaSinLiquidar_Total1)
                {
                    foreach (var item1 in item.Tipos)
                    {
                        if (item1.Tipo == "SIN LIQUIDACION")
                        {
                            foreach (var item2 in item1.Empresas)
                            {
                                deudores.Add(item2.Empresa);
                            }
                        }
                        
                    }
                }

                


                #region años_atras
                List<GerenciaCredito> _listaSinLiquidar_AñosAtras = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_AñosAtras1 = new List<GerenciaTreeCredito>();


                List<GerenciaCredito> _listaSinLiquidar_Prueba = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Prueba1 = new List<GerenciaTreeCredito>();
                int años_atras = 2018;
                if (DateTime.Now.Year != años_atras)
                {
                    años_atras = DateTime.Now.Year - 1;
                }
                else if (años_atras == 2018)
                {
                    años_atras = años_atras - 1;
                }
                else
                {
                    años_atras = 2018;
                }
                DateTime? pdatBeginDate = new DateTime(2018, 1, 1, 0, 0, 0);
                DateTime? pdatEndDate = new DateTime(años_atras, 12, 31, 0, 0, 0);

                _listaSinLiquidar_AñosAtras = oGerenciaCreditoBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                _listaSinLiquidar_AñosAtras1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_AñosAtras);
                foreach (var item in deudores)
                {
                    _listaSinLiquidar_Prueba = oGerenciaCreditoBl.SinLiquidarXEmpresa(pdatBeginDate.Value, pdatEndDate.Value, item);
                    _listaSinLiquidar_Prueba1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Prueba);
                }
               

                #endregion

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                OperationResult objOperationResult = new OperationResult();

                int año_inicio = 2018;
                if (DateTime.Now.Year != año_inicio)
                {
                    año_inicio = DateTime.Now.Year;
                }
                else
                {
                    año_inicio = 2018;
                }
                #region enero_sl
                List<GerenciaCredito> _listaSinLiquidar_Enero = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Enero1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L1 = new DateTime(año_inicio, 1, 1, 0, 0, 0);
                DateTime? fechaFin_L1 = new DateTime(año_inicio, 1, 31, 0, 0, 0);

                _listaSinLiquidar_Enero = oGerenciaCreditoBl.Filter(fechaInicio_L1.Value, fechaFin_L1.Value);
                _listaSinLiquidar_Enero1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Enero);
                #endregion

                #region febrero_sl
                List<GerenciaCredito> _listaSinLiquidar_Febrero = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Febrero1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L2 = new DateTime(año_inicio, 2, 1, 0, 0, 0);
                DateTime? fechaFin_L2 = new DateTime(año_inicio, 2, 28, 0, 0, 0);

                _listaSinLiquidar_Febrero = oGerenciaCreditoBl.Filter(fechaInicio_L2.Value, fechaFin_L2.Value);
                _listaSinLiquidar_Febrero1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Febrero);
                #endregion

                #region marzo_sl
                List<GerenciaCredito> _listaSinLiquidar_Marzo = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Marzo1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L3 = new DateTime(año_inicio, 3, 1, 0, 0, 0);
                DateTime? fechaFin_L3 = new DateTime(año_inicio, 3, 31, 0, 0, 0);

                _listaSinLiquidar_Marzo = oGerenciaCreditoBl.Filter(fechaInicio_L3.Value, fechaFin_L3.Value);
                _listaSinLiquidar_Marzo1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Marzo);
                #endregion

                #region abril_sl
                List<GerenciaCredito> _listaSinLiquidar_Abril = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Abril1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L4 = new DateTime(año_inicio, 4, 1, 0, 0, 0);
                DateTime? fechaFin_L4 = new DateTime(año_inicio, 4, 30, 0, 0, 0);

                _listaSinLiquidar_Abril = oGerenciaCreditoBl.Filter(fechaInicio_L4.Value, fechaFin_L4.Value);
                _listaSinLiquidar_Abril1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Abril);
                #endregion

                #region mayo_sl
                List<GerenciaCredito> _listaSinLiquidar_Mayo = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Mayo1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L5 = new DateTime(año_inicio, 5, 1, 0, 0, 0);
                DateTime? fechaFin_L5 = new DateTime(año_inicio, 5, 31, 0, 0, 0);

                _listaSinLiquidar_Mayo = oGerenciaCreditoBl.Filter(fechaInicio_L5.Value, fechaFin_L5.Value);
                _listaSinLiquidar_Mayo1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Mayo);
                #endregion

                #region junio_sl
                List<GerenciaCredito> _listaSinLiquidar_Junio = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Junio1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L6 = new DateTime(año_inicio, 6, 1, 0, 0, 0);
                DateTime? fechaFin_L6 = new DateTime(año_inicio, 6, 30, 0, 0, 0);

                _listaSinLiquidar_Junio = oGerenciaCreditoBl.Filter(fechaInicio_L6.Value, fechaFin_L6.Value);
                _listaSinLiquidar_Junio1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Junio);
                #endregion

                #region julio_sl
                List<GerenciaCredito> _listaSinLiquidar_Julio = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Julio1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L7 = new DateTime(año_inicio, 7, 1, 0, 0, 0);
                DateTime? fechaFin_L7 = new DateTime(año_inicio, 7, 31, 0, 0, 0);

                _listaSinLiquidar_Julio = oGerenciaCreditoBl.Filter(fechaInicio_L7.Value, fechaFin_L7.Value);
                _listaSinLiquidar_Julio1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Julio);
                #endregion

                #region agosto_sl
                List<GerenciaCredito> _listaSinLiquidar_Agosto = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Agosto1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L8 = new DateTime(año_inicio, 8, 1, 0, 0, 0);
                DateTime? fechaFin_L8 = new DateTime(año_inicio, 8, 31, 0, 0, 0);

                _listaSinLiquidar_Agosto = oGerenciaCreditoBl.Filter(fechaInicio_L8.Value, fechaFin_L8.Value);
                _listaSinLiquidar_Agosto1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Agosto);
                #endregion

                #region setiembre_sl
                List<GerenciaCredito> _listaSinLiquidar_Setiembre = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Setiembre1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L9 = new DateTime(año_inicio, 9, 1, 0, 0, 0);
                DateTime? fechaFin_L9 = new DateTime(año_inicio, 9, 30, 0, 0, 0);

                _listaSinLiquidar_Setiembre = oGerenciaCreditoBl.Filter(fechaInicio_L9.Value, fechaFin_L9.Value);
                _listaSinLiquidar_Setiembre1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Setiembre);
                #endregion

                #region octubre_sl
                List<GerenciaCredito> _listaSinLiquidar_Octubre = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Octubre1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L10 = new DateTime(año_inicio, 10, 1, 0, 0, 0);
                DateTime? fechaFin_L10 = new DateTime(año_inicio, 10, 31, 0, 0, 0);

                _listaSinLiquidar_Octubre = oGerenciaCreditoBl.Filter(fechaInicio_L10.Value, fechaFin_L10.Value);
                _listaSinLiquidar_Octubre1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Octubre);
                #endregion

                #region noviembre_sl
                List<GerenciaCredito> _listaSinLiquidar_Noviembre = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Noviembre1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L11 = new DateTime(año_inicio, 11, 1, 0, 0, 0);
                DateTime? fechaFin_L11 = new DateTime(año_inicio, 11, 30, 0, 0, 0);

                _listaSinLiquidar_Noviembre = oGerenciaCreditoBl.Filter(fechaInicio_L11.Value, fechaFin_L11.Value);
                _listaSinLiquidar_Noviembre1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Noviembre);
                #endregion

                #region diciembre_sl
                List<GerenciaCredito> _listaSinLiquidar_Diciembre = new List<GerenciaCredito>();
                List<GerenciaTreeCredito> _listaSinLiquidar_Diciembre1 = new List<GerenciaTreeCredito>();
                DateTime? fechaInicio_L12 = new DateTime(año_inicio, 12, 1, 0, 0, 0);
                DateTime? fechaFin_L12 = new DateTime(año_inicio, 12, 31, 0, 0, 0);

                _listaSinLiquidar_Diciembre = oGerenciaCreditoBl.Filter(fechaInicio_L12.Value, fechaFin_L12.Value);
                _listaSinLiquidar_Diciembre1 = oGerenciaCreditoBl.ProcessDataTreeView(_listaSinLiquidar_Diciembre);
                #endregion

                

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "EMPRESAS SIN LIQUIDAR - CSL";

                DateTime? fechaFin_1 = DateTime.Now;
                DateTime? fechaInicio_1 = DateTime.Now.AddDays(-30);
                string fechaInicio_2 = fechaInicio_1.ToString().Split(' ')[0];
                string fechaFin_2 = fechaFin_1.ToString().Split(' ')[0];
                SinLiquidar_General.CreateEmpresasSinLiquidaciones_General(ruta + nombre + ".pdf", MedicalCenter, _listaSinLiquidar_Enero1, fechaInicio_2, fechaFin_2, _listaSinLiquidar_Febrero1, _listaSinLiquidar_Marzo1, _listaSinLiquidar_Abril1, _listaSinLiquidar_Mayo1, _listaSinLiquidar_Junio1, _listaSinLiquidar_Julio1, _listaSinLiquidar_Agosto1,
                   _listaSinLiquidar_Setiembre1, _listaSinLiquidar_Octubre1, _listaSinLiquidar_Noviembre1, _listaSinLiquidar_Diciembre1, _listaSinLiquidar_Total1, _listaSinLiquidar_AñosAtras1);
                this.Enabled = true;
            }
            else if (rbEmpresasDetalleSLSF.Checked)
            {
                this.Enabled = false;
                OperationResult objOperationResult = new OperationResult();

                //DateTime? inicioDeudas = _fInicio;
                //DateTime? FinDeudas = DateTime.Now;

                var NoLiquidados = new ServiceBL().NoLiquidados(ref objOperationResult, _fInicio, _fFin);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "EMPRESAS SIN LIQUIDAR - CSL";

                string fechaInicio_2 = _fInicio.ToString().Split(' ')[0];
                string fechaFin_2 = _fFin.ToString().Split(' ')[0];
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
                EmpresasSinLiquidarSF.CreateEmpresasSinLiquidar(ruta + nombre + ".pdf", MedicalCenter, fechaInicio_2, fechaFin_2, NoLiquidados);
                this.Enabled = true;

            }
            else
            {
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btnGenerar.Enabled = true;
        }

        private void frmLiquidacionEmpresas_Load(object sender, EventArgs e)
        {
            if (_empresa == "")
            {
                rbCuentasXCobrar.Checked = true;
                rbEstadoCuentaEmpresa.Enabled = false;
            }
            else
            {
                rbEstadoCuentaEmpresa.Checked = true;
                rbEstadoCuentaEmpresa.Enabled = true;
            }
        }
    }
}
