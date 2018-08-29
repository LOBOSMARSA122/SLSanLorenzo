using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmServiceOrderEdit : Form
    {
        string _ProtocolId;
        string _Mode;
        string _ServiceOrderId;
        ServiceOrderBL _oServiceOrderBL = new ServiceOrderBL();
        serviceorderDto _oserviceorderDto = new serviceorderDto();
        ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId;

        List<ServiceOrderDetailList> _TempServiceOrderDetail = null;
        ServiceOrderDetailList _objServiceOrderDetailList = null;
        List<serviceorderdetailDto> _serviceorderdetailDto = null;

        public frmServiceOrderEdit(string pstrServiceOrderId,string pstrProtocolId ,string pstrMode)
        {
            InitializeComponent();
            _Mode = pstrMode;
            _ServiceOrderId = pstrServiceOrderId;
            _ProtocolId = pstrProtocolId;
        }

        private void frmServiceOrderEdit_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<ProtocolComponentList> oProtocolComponentList = new List<BE.ProtocolComponentList>();
            ProtocolBL oProtocolBL = new ProtocolBL();
            ProtocolList objProtocol = new ProtocolList();
            float CostoTotal = 0;
            

            Utils.LoadDropDownList(ddlStatusOrderServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 194, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbLineaCredito, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 122, null), DropDownListAction.Select);

            if (_Mode == "New")
            {
                txtNroTrabajadores.Select();
                int Year = DateTime.Now.Year;
                int Month = DateTime.Now.Month;
                int intNodeId = int.Parse(Globals.ClientSession.GetAsList()[0]);
                txtNroDocument.Text = GenerarCorrelativo(Year, Month, Sigesoft.Node.WinClient.BLL.Utils.GetNextSecuentialNoSave(intNodeId, 101)) + "-" + intNodeId;
                txtDateTime.Text = DateTime.Now.Date.ToString();

                ddlStatusOrderServiceId.SelectedValue = ((int)Common.ServiceOrderStatus.Iniciado).ToString();
                if (_ProtocolId != "")
                {
                      oProtocolComponentList = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId);


                var x = oProtocolComponentList.FindAll(P => P.r_Price != 0); // eliminamos los componentes con precio 0
                foreach (var item in x)
                {
                    CostoTotal += (float)item.r_Price;
                   
                }

                //grdData.DataSource = x;
                txtTotal.Text = CostoTotal.ToString();
                //txtTotalTrabajadores.Text = 

                objProtocol = oProtocolBL.GetProtocolById(ref objOperationResult, _ProtocolId);

                txtProtocolName.Text = objProtocol.v_Protocol;
                txtOrganitation.Text = objProtocol.v_Organization;
                txtContact.Text = objProtocol.v_ContacName;
                txtAdress.Text = objProtocol.v_Address;
                txttypeProtocol.Text = objProtocol.v_EsoType;
                }

                this.Height = 517;
                groupBox1.Height = 47;
                this.groupBox2.Location = new System.Drawing.Point(13, 98);
            }
            else
            {
                _oserviceorderDto = _oServiceOrderBL.GetServiceOrder(ref objOperationResult, _ServiceOrderId);
                
                //txtNroTrabajadores.Text = _oserviceorderDto.i_NumberOfWorker.ToString();
                txtNroDocument.Text = _oserviceorderDto.v_CustomServiceOrderId;
                txtComentary.Text = _oserviceorderDto.v_Comentary;
                //txtCostoTotal.Text = _oserviceorderDto.r_TotalCost.ToString();
                txtDateTime.Text = _oserviceorderDto.d_InsertDate.Value.Date.ToString();
                txtTotal.Text = _oserviceorderDto.r_TotalCost.ToString();
                txtTotalTrabajadores.Text = _oserviceorderDto.i_NumberOfWorker.ToString();
                if (_oserviceorderDto.d_DeliveryDate == null)
                {
                    dtpDelirevy.Checked = false;
                }
                else
                {
                    dtpDelirevy.Checked = true;
                    dtpDelirevy.Value = (DateTime)_oserviceorderDto.d_DeliveryDate;
                }

                if (_oserviceorderDto.i_MostrarPrecio == 1 || _oserviceorderDto.i_MostrarPrecio == null)
                {
                    rbSi.Checked = true;
                    rbNo.Checked = false;
                }
                else
                {
                    rbNo.Checked = true;
                    rbSi.Checked = false;
                }

                if (_oserviceorderDto.i_EsProtocoloEspecial == 1 )
                {
                    chkProtocoloEspecial.Checked = true;
                }
                else
                {
                    chkProtocoloEspecial.Checked = false;
                }

                ddlStatusOrderServiceId.SelectedValue = _oserviceorderDto.i_ServiceOrderStatusId.ToString();
                cbLineaCredito.SelectedValue = _oserviceorderDto.i_LineaCreditoId.ToString();

               // oProtocolComponentList = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId);

               //var x = oProtocolComponentList.FindAll(P => P.r_Price != 0); // eliminamos los componentes con precio 0
               //foreach (var item in x)
               // {
               //     CostoTotal += (float)item.r_Price;
               // }

               //grdData.DataSource = x;
                

                //objProtocol = oProtocolBL.GetProtocolById(ref objOperationResult, _ProtocolId);

                //txtProtocolName.Text = objProtocol.v_Protocol;
                //txtOrganitation.Text = objProtocol.v_Organization;
                //txtContact.Text = objProtocol.v_ContacName;
                //txtAdress.Text = objProtocol.v_Address;
                //txttypeProtocol.Text = objProtocol.v_EsoType;
                //txtTotal.Text = 


                _TempServiceOrderDetail = _oServiceOrderBL.GetServiceOrderPagedAndFiltered(ref objOperationResult, 0, null, "", "v_ServiceOrderId==" + "\"" + _oserviceorderDto.v_ServiceOrderId + "\"");
              
                
                grdData1.DataSource = _TempServiceOrderDetail;

                _ProtocolId = _TempServiceOrderDetail[0].v_ProtocolId;

               
            }
        }

        private string GenerarCorrelativo(int Year, int Month, int Secuential)
        {
            return string.Format("N° {0}{1}{2}", Year, Month, Secuential.ToString("000000"));
        }

        private void btnSearchProtocol_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ProtocolBL oProtocolBL = new ProtocolBL();
            //List<ProtocolComponentList> ProtocolComponentList = new List<BE.ProtocolComponentList>();
  

            Configuration.frmProtocolManagement frm = new Configuration.frmProtocolManagement("View", -1, -1);
            frm.ShowDialog();
            if (frm._pstrProtocolId != null)
            {
                _ProtocolId = frm._pstrProtocolId;
            }

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            if (_ProtocolId == null) return;

            ProtocolBL _objProtocoltBL = new ProtocolBL();
            protocolDto objProtocolDto = new protocolDto();
            ProtocolList objProtocol = new ProtocolList();
            objProtocol = _objProtocoltBL.GetProtocolById(ref objOperationResult, _ProtocolId);

            txtProtocolName.Text = objProtocol.v_Protocol;
            txtOrganitation.Text =  objProtocol.v_Organization;
            txtContact.Text = objProtocol.v_ContacName;
            txtAdress.Text = objProtocol.v_Address;
            txttypeProtocol.Text = objProtocol.v_EsoType;

            //ProtocolComponentList = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId);

            //foreach (var item in ProtocolComponentList)
            //{
            //    CostoTotal += (float)item.r_Price;
            //}

            //grdData.DataSource = ProtocolComponentList;
            //txtTotal.Text = CostoTotal.ToString();
            //Calcular();
            txtNroTrabajadores.Focus();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {           
            
            OperationResult objOperationResult = new OperationResult();
            _serviceorderdetailDto = new List<serviceorderdetailDto>();

            if (ultraValidator1.Validate(true, false).IsValid)
            {
                if (ddlStatusOrderServiceId.SelectedValue.ToString() == "-1")
                {
                    MessageBox.Show("Por favor seleccione un Estado.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbLineaCredito.SelectedValue.ToString() == "-1")
                {
                    MessageBox.Show("Por favor seleccione una Línea de Crédito.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            
                if (_Mode == "New")
                {
                    _oserviceorderDto.v_CustomServiceOrderId = txtNroDocument.Text;
                    _oserviceorderDto.v_Description = "";
                    _oserviceorderDto.v_Comentary = "";
                    _oserviceorderDto.i_NumberOfWorker = int.Parse(txtTotalTrabajadores.Text.ToString());
                    _oserviceorderDto.r_TotalCost = float.Parse(txtTotal.Text.ToString());
                    _oserviceorderDto.v_Comentary = txtComentary.Text;


                    if (dtpDelirevy.Checked == false)
                    {
                        _oserviceorderDto.d_DeliveryDate = null;
                    }
                    else
                    {
                        _oserviceorderDto.d_DeliveryDate = dtpDelirevy.Value;
                    }      
                    _oserviceorderDto.i_ServiceOrderStatusId = int.Parse(ddlStatusOrderServiceId.SelectedValue.ToString());
                    _oserviceorderDto.i_LineaCreditoId = int.Parse(cbLineaCredito.SelectedValue.ToString());
                    _oserviceorderDto.i_MostrarPrecio = rbSi.Checked ? 1 : 0;
                    _oserviceorderDto.i_EsProtocoloEspecial = chkProtocoloEspecial.Checked ? 1 : 0;
                    
                    if (_TempServiceOrderDetail == null || _TempServiceOrderDetail.Count == 0)
                    {
                        _serviceorderdetailDto = null;
                        MessageBox.Show("El detalle no puede estar vacio.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        foreach (var item in _TempServiceOrderDetail)
                        {
                            serviceorderdetailDto oserviceorderdetailDto = new serviceorderdetailDto();
                            oserviceorderdetailDto.v_ProtocolId = item.v_ProtocolId;
                            oserviceorderdetailDto.r_ProtocolPrice = item.r_ProtocolPrice;
                            oserviceorderdetailDto.i_NumberOfWorkerProtocol = item.i_NumberOfWorkerProtocol;
                            oserviceorderdetailDto.r_Total = item.r_Total;
                            _serviceorderdetailDto.Add(oserviceorderdetailDto);
                        }                       
                    }

                 _ServiceOrderId=   _oServiceOrderBL.AddServiceOrder(ref objOperationResult, _oserviceorderDto, _serviceorderdetailDto, Globals.ClientSession.GetAsList());

                }
                else if (_Mode == "Edit")
                {
                    _oserviceorderDto.v_ServiceOrderId = _ServiceOrderId;
                    _oserviceorderDto.v_CustomServiceOrderId = txtNroDocument.Text;
                    _oserviceorderDto.v_Description = "";
                    _oserviceorderDto.v_Comentary = "";
                    _oserviceorderDto.i_NumberOfWorker = int.Parse(txtTotalTrabajadores.Text.ToString());
                    _oserviceorderDto.r_TotalCost = float.Parse(txtTotal.Text.ToString());
                    _oserviceorderDto.v_Comentary = txtComentary.Text;
                    _oserviceorderDto.i_ServiceOrderStatusId = int.Parse(ddlStatusOrderServiceId.SelectedValue.ToString());

                    _oserviceorderDto.i_LineaCreditoId = int.Parse(cbLineaCredito.SelectedValue.ToString());
                    _oserviceorderDto.i_MostrarPrecio = rbSi.Checked ? 1 : 0;
                    _oserviceorderDto.i_EsProtocoloEspecial = chkProtocoloEspecial.Checked ? 1 : 0;
                    if (dtpDelirevy.Checked == false)
                    {
                        _oserviceorderDto.d_DeliveryDate = null;
                    }
                    else
                    {
                        _oserviceorderDto.d_DeliveryDate = dtpDelirevy.Value;
                    }

                    if (_TempServiceOrderDetail == null || _TempServiceOrderDetail.Count == 0)
                    {
                        _serviceorderdetailDto = null;
                        MessageBox.Show("El detalle no puede estar vacio.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (var item in _TempServiceOrderDetail)
                    {
                        serviceorderdetailDto oserviceorderdetailDto = new serviceorderdetailDto();
                        oserviceorderdetailDto.v_ServiceOrderDetailId = item.v_ServiceOrderDetailId;
                        oserviceorderdetailDto.v_ProtocolId = item.v_ProtocolId;
                        oserviceorderdetailDto.r_ProtocolPrice = item.r_ProtocolPrice;
                        oserviceorderdetailDto.i_NumberOfWorkerProtocol = item.i_NumberOfWorkerProtocol;
                        oserviceorderdetailDto.r_Total = item.r_Total;
                        _serviceorderdetailDto.Add(oserviceorderdetailDto);
                    }          
       
                    _oServiceOrderBL.UpdateService(ref objOperationResult, _oserviceorderDto, _serviceorderdetailDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    btnReportePDF.Enabled = true;
                    btnSendEmail.Enabled = true;
                   
                    //this.Close();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }
                 
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtNroTrabajadores_TextChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void Calcular()
        {
            float NTrabajadores = 0;
            float TotalProtocolo = 0;

            NTrabajadores = txtNroTrabajadores.Text.ToString() == String.Empty ? 0 : float.Parse(txtNroTrabajadores.Text.ToString());
            TotalProtocolo = txtTotal.Text.ToString() == string.Empty ? 0 : float.Parse(txtTotal.Text.ToString());
            //txtCostoTotal.Text = (TotalProtocolo * NTrabajadores).ToString();
        }

        private void ddlStatusOrderServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatusOrderServiceId.SelectedValue.ToString() == ((int)ServiceOrderStatus.Observado).ToString())
            {
                this.Height = 547;
                groupBox1.Height = 75;
                txtComentary.Visible = true;
                label6.Visible = true;
                this.groupBox2.Location = new System.Drawing.Point(13, 125);
            }
            else
            {
                this.Height = 517;
                groupBox1.Height = 47;
                txtComentary.Visible = false;
                label6.Visible = false;
                this.groupBox2.Location = new System.Drawing.Point(13, 98);
                txtComentary.Text = "";
            }
        }

        private void txtNroTrabajadores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtNroTrabajadores.Text.Length; i++)
            {
                if (txtNroTrabajadores.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }


            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceOrderBL oServiceOrderlBL = new ServiceOrderBL();
            ProtocolBL oProtocolBL = new ProtocolBL();
            ProtocolList oProtocolList = new ProtocolList();
            int CantidadTrabajadores = 0;
            #region
            if (txtProtocolName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Por favor seleccione un Protocolo.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNroTrabajadores.Text.Trim() == "" || txtNroTrabajadores.Text.Trim() == "0")
            {
                MessageBox.Show("El N° de Trabajadores no puede ser 0 o vacío", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CantidadTrabajadores.ToString() != "")
            {
                CantidadTrabajadores = int.Parse(txtNroTrabajadores.Text.ToString() );
            }
           

            float CostoTotal = 0;
            int TotalTrabajadores = 0;


            #endregion
            if (_TempServiceOrderDetail == null)
            {
                _TempServiceOrderDetail = new List<ServiceOrderDetailList>();
            }

            grdData1.DataSource = new ServiceOrderDetailList();
            _objServiceOrderDetailList = new ServiceOrderDetailList();

            oProtocolList =  oProtocolBL.GetProtocolById(ref objOperationResult, _ProtocolId);
            //Buscar si un producto ya esta en la Grilla
            var findResult = _TempServiceOrderDetail.Find(p => p.v_ProtocolId == _ProtocolId);

            if (findResult == null)
            {
                _objServiceOrderDetailList.v_ProtocolId = _ProtocolId;
                _objServiceOrderDetailList.v_ProtocolName = oProtocolList.v_Name;
                _objServiceOrderDetailList.i_NumberOfWorkerProtocol = CantidadTrabajadores;
                _objServiceOrderDetailList.r_ProtocolPrice = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId).Sum(s => s.r_Price);
                _objServiceOrderDetailList.r_Total = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId).Sum(s => s.r_Price) * CantidadTrabajadores;                
                _TempServiceOrderDetail.Add(_objServiceOrderDetailList);
                grdData1.DataSource = _TempServiceOrderDetail;
            }
            else
            {
                var findIndex = _TempServiceOrderDetail.FindIndex(p => p.v_ProtocolId == _ProtocolId);

                _objServiceOrderDetailList.v_ProtocolId = _ProtocolId;
                _objServiceOrderDetailList.v_ProtocolName = oProtocolList.v_Name;
                _objServiceOrderDetailList.i_NumberOfWorkerProtocol = CantidadTrabajadores;
                _objServiceOrderDetailList.r_ProtocolPrice = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId).Sum(s => s.r_Price);
                _objServiceOrderDetailList.r_Total = oProtocolBL.GetProtocolComponents(ref objOperationResult, _ProtocolId).Sum(s => s.r_Price) * CantidadTrabajadores;   
                _TempServiceOrderDetail.Add(_objServiceOrderDetailList);
                _TempServiceOrderDetail.RemoveAt(findIndex);
                grdData1.DataSource = _TempServiceOrderDetail;
            }

            grdData1.Refresh();

            //Limpiar controles

            txtProtocolName.Text = string.Empty;
            txtOrganitation.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txttypeProtocol.Text = string.Empty;
            txtNroTrabajadores.Text = string.Empty;
            


            //_TempServiceOrderDetail = oServiceOrderlBL.GetServiceOrderDetailList(_ProtocolId);

            foreach (var item in _TempServiceOrderDetail)
            {
                CostoTotal += (float)item.r_Total;
                TotalTrabajadores += (int)item.i_NumberOfWorkerProtocol;
            }

            //grdData.DataSource = _TempServiceOrderDetail;
            txtTotal.Text = CostoTotal.ToString();
            txtTotalTrabajadores.Text = TotalTrabajadores.ToString();
            //Calcular();
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmReporteCotizacionConsolidado(_ServiceOrderId);
            frm.ShowDialog();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnReportePDF_Click(object sender, EventArgs e)
        {
            ProtocolBL oProtocolBL = new ProtocolBL();
            List<ServiceOrderPdf> Lista = new List<ServiceOrderPdf>();
            ServiceOrderPdf oServiceOrderPdf;

            DialogResult Result = MessageBox.Show("¿Desea publicar a la WEB?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            OperationResult objOperationResult = new OperationResult();

            List<ProtocolComponentList> ListaComponentes = new List<ProtocolComponentList>();
            SecurityBL oSecurityBL = new SecurityBL();

            ServiceOrderDetailPdf oServiceOrderDetailPdf;
            List<ServiceOrderDetailPdf> ListaServiceOrderDetailPdf = new List<ServiceOrderDetailPdf>();

            SystemUserList oSystemUserList = new SystemUserList();

            //saveFileDialog1.FileName = "Orden de Servicio";
            //saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    this.Enabled = false;
                    var MedicalCenter = _serviceBL.GetInfoMedicalCenterSede();
                    var pEmpresaCliente = new ProtocolBL().GetEmpresaByProtocoloId(_ProtocolId)[0].v_Name;
                    var _DataService = oProtocolBL.GetProtocolById(ref objOperationResult, _ProtocolId);

                    foreach (var Protocolo in _TempServiceOrderDetail)
                    {

                        oServiceOrderPdf = new ServiceOrderPdf();
                        //Llenar cabecera 
                        var oProtocolo = oProtocolBL.GetProtocolById(ref objOperationResult, Protocolo.v_ProtocolId);
                        oServiceOrderPdf.v_ServiceOrderId = _ServiceOrderId;
                        oServiceOrderPdf.EmpresaCliente = oProtocolo.v_OrganizationInvoice + " / " + oProtocolo.v_GroupOccupation + " / " + oProtocolo.v_EsoType;
                        
                        //Llenar Detalle
                        ListaComponentes = oProtocolBL.GetProtocolComponents(ref objOperationResult, Protocolo.v_ProtocolId);
                        ListaServiceOrderDetailPdf = new List<ServiceOrderDetailPdf>();
                        foreach (var Componente in ListaComponentes)
                        {
                            oServiceOrderDetailPdf = new ServiceOrderDetailPdf();
                            oServiceOrderDetailPdf.v_ServiceOrderDetailId = _TempServiceOrderDetail.Find(p => p.v_ProtocolId == Protocolo.v_ProtocolId).v_ServiceOrderDetailId;
                            oServiceOrderDetailPdf.v_ServiceOrderId = _ServiceOrderId;
                            oServiceOrderDetailPdf.v_ComponentId = Componente.v_ComponentId;
                            oServiceOrderDetailPdf.Componente = Componente.v_ComponentName;
                            oServiceOrderDetailPdf.v_Precio = Componente.r_Price;
                            ListaServiceOrderDetailPdf.Add(oServiceOrderDetailPdf);
                        }
                        oServiceOrderPdf.DetalleServiceOrder = ListaServiceOrderDetailPdf;
                        oServiceOrderPdf.TotalProtocolo = ListaServiceOrderDetailPdf.Sum(s => s.v_Precio);
                        Lista.Add(oServiceOrderPdf);
                    }

                    //obtener profesion del usuario
                   var SystemUserId = Globals.ClientSession.i_SystemUserId;

                   oSystemUserList= oSecurityBL.GetSystemUserAndProfesional(ref objOperationResult, SystemUserId);

                   string ruta = Common.Utils.GetApplicationConfigValue("rutaCotizacion").ToString();



                   if (chkProtocoloEspecial.Checked)
                   {
                       OrdenServicioPromocion.CrearOrdenServicio(rbSi.Checked ? true : false, Lista, MedicalCenter, pEmpresaCliente, DateTime.Parse(txtDateTime.Text).ToString("dd 'd'e MMMM 'd'e yyyy"), oSystemUserList.Profesion + ". " + oSystemUserList.v_PersonName, ruta + _ServiceOrderId + ".pdf");

                   }
                   else
                   {
                       OrdenServicio.CrearOrdenServicio(rbSi.Checked ? true : false, Lista, MedicalCenter, pEmpresaCliente, _ServiceOrderId, DateTime.Parse(txtDateTime.Text).ToString("dd 'd'e MMMM 'd'e yyyy"), oSystemUserList.Profesion + ". " + oSystemUserList.v_PersonName, ruta + _ServiceOrderId + ".pdf");
                   }

                    this.Enabled = true;
                //}
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {

        }

        private void btnSendEmail_Click_1(object sender, EventArgs e)
        {
            ServiceOrderBL oServiceOrderBL = new ServiceOrderBL();
            List<string> ListaProtocolos = new List<string>();
            var Lista = oServiceOrderBL.ObtenerProtocolos(_ServiceOrderId);
            foreach (var item in Lista)
            {
                ListaProtocolos.Add(item.Value1);
            }
            frmEmailOrdenServicioCotizacion frm = new frmEmailOrdenServicioCotizacion(_ServiceOrderId, ListaProtocolos);
            frm.ShowDialog();
        }

    }
}
