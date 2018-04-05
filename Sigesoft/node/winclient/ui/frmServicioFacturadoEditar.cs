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
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmServicioFacturadoEditar : Form
    {
        FacturacionBL oFacturacionBL = new FacturacionBL();
        List<FacturacionDetalleList> _ListaFacturacionDetalle = new List<FacturacionDetalleList>();
        facturacionDto ofacturacionDto = new facturacionDto(); 
        string _Mode;
        string _v_FacturacionId;
        string _EmpresaCliente;
        string _EmpresaSede;
        string _Descripcion = "";
        double _Descuento = 0;
        DateTime _FechaInicio;
        DateTime _FechaFin;
        public frmServicioFacturadoEditar(string pstrIdServicioFacturado, string pstrMode)
        {
            InitializeComponent();
            _Mode = pstrMode;
            _v_FacturacionId = pstrIdServicioFacturado;
        }

        private void frmServicioFacturadoEditar_Load(object sender, EventArgs e)
        {
            //loco
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(cboEstadoFacturacion, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 117, null), DropDownListAction.Select);

            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            Utils.LoadDropDownList(cbCustomerOrganization,
                "Value1",
                "Id",
                dataListOrganization1,
                DropDownListAction.Select);

              if (_Mode == "New")
            {
                // Additional logic here.

            }
              else if (_Mode == "Edit")
              {

                cbCustomerOrganization.Enabled = false;
                btnFilter.Enabled = false;
                btnImprimir.Enabled = true;
                btnConsolidado1.Enabled = true;
                btnConsolidado2.Enabled = true;
                btnConsolidado3.Enabled = true;
                btnComision.Enabled = true;
                //btnschedule.Enabled = true;

                ofacturacionDto=  oFacturacionBL.GetFacturacion(ref objOperationResult, _v_FacturacionId);
                dtpDateTimeStar.Value = (DateTime)ofacturacionDto.d_FechaRegistro;
                dtFechaCobro.Value = (DateTime)ofacturacionDto.d_FechaCobro;
                txtNroFactura.Text = ofacturacionDto.v_NumeroFactura;
                cboEstadoFacturacion.SelectedValue = ofacturacionDto.i_EstadoFacturacion.ToString();
                cbCustomerOrganization.SelectedValue = string.Format("{0}|{1}", ofacturacionDto.v_EmpresaCliente, ofacturacionDto.v_EmpresaSede);

               
               
                txtDescripcion.Text = ofacturacionDto.v_Descripcion;
                txtDescuento.Text = ofacturacionDto.d_Descuento.Value.ToString();
                _ListaFacturacionDetalle = oFacturacionBL.GetListFacturacionDetalle(ref objOperationResult, _v_FacturacionId);
                _FechaInicio = ofacturacionDto.d_FechaInicio== null? DateTime.Now: ofacturacionDto.d_FechaInicio.Value;
                _FechaFin = ofacturacionDto.d_FechaFin == null ? DateTime.Now : ofacturacionDto.d_FechaFin.Value;

                _EmpresaCliente = ofacturacionDto.v_EmpresaCliente;
                _EmpresaSede = ofacturacionDto.v_EmpresaSede;

                  grdData.DataSource = _ListaFacturacionDetalle;

                  decimal subTotal = 0;
                  decimal igv = 0;
                  decimal total = 0;
                  foreach (var item in grdData.Rows)
                  {
                      subTotal = subTotal + decimal.Parse(item.Cells["d_Monto"].Value.ToString());
                      igv = igv + decimal.Parse(item.Cells["igv"].Value.ToString());
                      total = total + decimal.Parse(item.Cells["total"].Value.ToString());

                  }
                  double detraccion =   double.Parse(total.ToString()) * 0.1;
                  txtDetraccion.Text = detraccion.ToString();
                  txtTotalFacturar.Text = total.ToString();// ofacturacionDto.d_MontoTotal.ToString();
                  txtIgv.Text = igv.ToString();// ofacturacionDto.d_Igv.ToString();
                  txtSubTotal.Text = subTotal.ToString();// ofacturacionDto.d_SubTotal.ToString();
              }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (uvValidar.Validate(true, false).IsValid)
            {
                var id2 = cbCustomerOrganization.SelectedValue.ToString().Split('|');

                frmBuscarServicioPendiente frm = new frmBuscarServicioPendiente(id2[0], id2[1]);
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    return;

                if (frm._ListaFacturacionList == null)
                    return;

                _ListaFacturacionDetalle = frm._ListaFacturacionList;

                //Obtener los protocolos de la lista _ListaFacturacionDetalle
              
                //var ListaProtocolos = _ListaFacturacionDetalle.GroupBy(g => g.v_ProtocolId)
                //                 .Select(s => s.First());


                double CostoTotal = 0;
                foreach (var item in _ListaFacturacionDetalle)
                {
                    int ContadorServicios = _ListaFacturacionDetalle.FindAll(p => p.v_ProtocolId == item.v_ProtocolId).ToList().Count();
                    double CostoProtocolo = double.Parse(item.d_Monto.Value.ToString());

                    CostoTotal += CostoProtocolo;
                }

                //Calcular el subTotal(CostoTotal) y el IGV
                double igv = (CostoTotal * 0.18);
                double subTotal = (CostoTotal) - igv;
                double total = CostoTotal + igv;
                CalcularTotales(total, CostoTotal, igv);

                DateTime? FechaInicio = frm._FechaInicio;
                DateTime? FechaFin = frm._FechaFin;

                _EmpresaCliente = id2[0];
                _EmpresaSede = id2[1];
                _FechaInicio = FechaInicio.Value.Date;
                _FechaFin = FechaFin.Value.Date;

                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _ListaFacturacionDetalle.Count());

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void CalcularTotales(double pTotal, double pSubTotal, double pIgv)
        {
            double Total = pTotal;
            double Igv;
            double SubTotal;

            if (Total > 700)
            {
                txtDetraccion.Text = (Total * 0.1).ToString();
            }
            else
            {
                txtDetraccion.Text = "0.00";
            }

            Igv = pIgv;
            SubTotal = pSubTotal;

            txtTotalFacturar.Text = (pSubTotal + Igv).ToString();
            txtIgv.Text = Igv.ToString();
            txtSubTotal.Text = SubTotal.ToString();
            grdData.DataSource = _ListaFacturacionDetalle;
        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            facturacionDto ofacturacionDto = new facturacionDto();

            facturaciondetalleDto ofacturaciondetalleDto;
            List<facturaciondetalleDto> ListaFacturacionDetalle = new List<facturaciondetalleDto>();
            if (uvValidarFormulario.Validate(true, false).IsValid)
            {

                DialogResult Result = MessageBox.Show("¿Desea aplicar un descuento?", "INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    frmDescuento frm = new frmDescuento();
                    frm.ShowDialog();

                    CalcularTotales(double.Parse(txtTotalFacturar.Text.ToString()) - double.Parse(frm._descuento.ToString()), double.Parse(txtSubTotal.Text.ToString()), double.Parse(txtIgv.Text.ToString()));
                    _Descripcion = frm._Descripcion;
                    _Descuento = double.Parse(frm._descuento.ToString());
                    txtDescripcion.Text = _Descripcion;
                    txtDescuento.Text = _Descuento.ToString(); ;
                }

            if (_Mode == "New")
            {
                ofacturacionDto.d_FechaRegistro = dtpDateTimeStar.Value;
                ofacturacionDto.d_FechaCobro = dtFechaCobro.Value;
                ofacturacionDto.v_NumeroFactura = txtNroFactura.Text;
                ofacturacionDto.i_EstadoFacturacion = int.Parse(cboEstadoFacturacion.SelectedValue.ToString());
                var id2 = cbCustomerOrganization.SelectedValue.ToString().Split('|');
                ofacturacionDto.v_EmpresaCliente = id2[0];
                ofacturacionDto.v_EmpresaSede = id2[1];

                ofacturacionDto.v_Descripcion = _Descripcion;
                ofacturacionDto.d_Descuento =_Descuento == null ?0:  decimal.Parse(_Descuento.ToString());
                ofacturacionDto.d_MontoTotal = decimal.Parse(txtTotalFacturar.Text.ToString());
                ofacturacionDto.d_Detraccion = decimal.Parse(txtDetraccion.Text.ToString());
                ofacturacionDto.d_Igv = decimal.Parse(txtIgv.Text.ToString());
                ofacturacionDto.d_SubTotal = decimal.Parse(txtSubTotal.Text.ToString());
                ofacturacionDto.d_FechaInicio = _FechaInicio;
                ofacturacionDto.d_FechaFin = _FechaFin;

                foreach (var item in _ListaFacturacionDetalle)
                {
                    ofacturaciondetalleDto = new facturaciondetalleDto();
                    ofacturaciondetalleDto.v_ServicioId = item.v_ServicioId;
                    ofacturaciondetalleDto.d_Monto = item.d_Monto;

                    ListaFacturacionDetalle.Add(ofacturaciondetalleDto);
                }

              _v_FacturacionId =  oFacturacionBL.AddFacturacion(ref objOperationResult, ofacturacionDto, ListaFacturacionDetalle, Globals.ClientSession.GetAsList());
                
            }
            else if (_Mode == "Edit")
            {
                facturacionDto ofacturacionDtoEdit = new facturacionDto();
                FacturacionBL oFacturacionBL = new FacturacionBL();

               ofacturacionDtoEdit= oFacturacionBL.GetFacturacion(ref objOperationResult, _v_FacturacionId);

                ofacturacionDto.v_FacturacionId = _v_FacturacionId;
                ofacturacionDto.d_FechaRegistro = dtpDateTimeStar.Value;
                ofacturacionDto.d_FechaCobro = dtFechaCobro.Value;
                ofacturacionDto.v_NumeroFactura = txtNroFactura.Text;
                ofacturacionDto.i_EstadoFacturacion = int.Parse(cboEstadoFacturacion.SelectedValue.ToString());
                ofacturacionDto.v_Descripcion = _Descripcion;
                ofacturacionDto.d_Descuento = decimal.Parse(_Descuento.ToString());
                ofacturacionDto.d_MontoTotal = decimal.Parse(txtTotalFacturar.Text.ToString());
                ofacturacionDto.d_Detraccion = decimal.Parse(txtDetraccion.Text.ToString());
                ofacturacionDto.d_Igv = decimal.Parse(txtIgv.Text.ToString());
                ofacturacionDto.d_SubTotal = decimal.Parse(txtSubTotal.Text.ToString());
                ofacturacionDto.d_FechaInicio = ofacturacionDtoEdit.d_FechaInicio;
                ofacturacionDto.d_FechaFin = ofacturacionDtoEdit.d_FechaFin;
                var id2 = cbCustomerOrganization.SelectedValue.ToString().Split('|');
                ofacturacionDto.v_EmpresaCliente = id2[0];
                ofacturacionDto.v_EmpresaSede = id2[1];
         

                foreach (var item in _ListaFacturacionDetalle)
                {
                    ofacturaciondetalleDto = new facturaciondetalleDto();
                    ofacturaciondetalleDto.v_ServicioId = item.v_ServicioId;
                    ofacturaciondetalleDto.d_Monto = item.d_Monto;

                    ListaFacturacionDetalle.Add(ofacturaciondetalleDto);
                }

                oFacturacionBL.UpdateFacturacion(ref objOperationResult, ofacturacionDto, Globals.ClientSession.GetAsList());
                

            }

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                btnImprimir.Enabled = true;
                btnComision.Enabled = true;
                btnConsolidado1.Enabled = true;
                btnConsolidado2.Enabled = true;
                btnConsolidado3.Enabled = true;
                _Mode = "Edit";
                MessageBox.Show("Se grabo correctamente", "SISTEMAS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
            }
            else  // Operación con error
            {
                btnImprimir.Enabled = false;
                btnComision.Enabled = false;
                btnConsolidado1.Enabled = false;
                btnConsolidado2.Enabled = false;
                btnConsolidado3.Enabled = false;
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

     
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            String[] CadenaServicios = new String[8000];

            for (int i = 0; i < _ListaFacturacionDetalle.Count; i++)
            {
                CadenaServicios[i] = _ListaFacturacionDetalle[i].v_ServicioId;
            }

            Reports.frmFactura frm = new Reports.frmFactura(_v_FacturacionId, _EmpresaCliente, _EmpresaSede, _FechaInicio, _FechaFin, 1, CadenaServicios,1);
            frm.ShowDialog();
        }

        private void btnComision_Click(object sender, EventArgs e)
        {

            var id2 = cbCustomerOrganization.SelectedValue.ToString().Split('|');
            frmComisionVendedor frm = new frmComisionVendedor(_v_FacturacionId, id2[0], id2[1], _FechaInicio, _FechaFin,cbCustomerOrganization.Text);
            frm.ShowDialog();
        }

        private void btnConsolidado1_Click(object sender, EventArgs e)
        {
            String[] CadenaServicios = new String[8000];

            for (int i = 0; i < _ListaFacturacionDetalle.Count; i++)
            {
                CadenaServicios[i] = _ListaFacturacionDetalle[i].v_ServicioId;
            }


            Reports.frmFactura frm = new Reports.frmFactura(_v_FacturacionId, _EmpresaCliente, _EmpresaSede, _FechaInicio, _FechaFin, 2, CadenaServicios,1);
            frm.ShowDialog();
        }

        private void btnConsolidado2_Click(object sender, EventArgs e)
        {
            String[] CadenaServicios = new String[8000];

            for (int i = 0; i < _ListaFacturacionDetalle.Count; i++)
            {
                CadenaServicios[i] = _ListaFacturacionDetalle[i].v_ServicioId;
            }
            Reports.frmFactura frm = new Reports.frmFactura(_v_FacturacionId, _EmpresaCliente, _EmpresaSede, _FechaInicio, _FechaFin, 3, CadenaServicios,1);
            frm.ShowDialog();
        }

        private void btnConsolidado3_Click(object sender, EventArgs e)
        {
            String[] CadenaServicios = new String[8000];

            for (int i = 0; i < _ListaFacturacionDetalle.Count; i++)
            {
                CadenaServicios[i] = _ListaFacturacionDetalle[i].v_ServicioId;
            }
            Reports.frmFactura frm = new Reports.frmFactura(_v_FacturacionId, _EmpresaCliente, _EmpresaSede, _FechaInicio, _FechaFin,1, CadenaServicios,2);
            frm.ShowDialog();
        }

        private void btnExportAramark_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (cbCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Reporte de facturación de" + cbCustomerOrganization.Text ;
        

            }
          

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdAramark.FileName = NombreArchivo;
            sfdAramark.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdAramark.ShowDialog() == DialogResult.OK)
            {
                this.ugeAramark.Export(this.grdData, sfdAramark.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnExportAramark.Enabled = grdData.Rows.Count > 0;
        }

        private void grdData_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            e.Layout.Bands[0].Columns["d_Monto"].Format = "#,##0.00";
            e.Layout.Bands[0].Columns["Igv"].Format = "#,##0.00";
            e.Layout.Bands[0].Columns["Total"].Format = "#,##0.00";

            // Sumarizado

            UltraGridColumn columnToSummarize = e.Layout.Bands[0].Columns["d_Monto"];
            SummarySettings summary = e.Layout.Bands[0].Summaries.Add("SubTotal", SummaryType.Sum, columnToSummarize);
            summary.DisplayFormat = "{0:C}";
            summary.Appearance.TextHAlign = HAlign.Right;
            summary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter;



            UltraGridColumn columnToSummarize2 = e.Layout.Bands[0].Columns["Igv"];
            SummarySettings summary2 = e.Layout.Bands[0].Summaries.Add("Igv", SummaryType.Sum, columnToSummarize2);
            summary2.DisplayFormat = "{0:C}";
            summary2.Appearance.TextHAlign = HAlign.Right;
            summary2.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter;

            UltraGridColumn columnToSummarize3 = e.Layout.Bands[0].Columns["Total"];
            SummarySettings summary3 = e.Layout.Bands[0].Summaries.Add("Total", SummaryType.Sum, columnToSummarize3);
            summary3.DisplayFormat = "{0:C}";
            summary3.Appearance.TextHAlign = HAlign.Right;
            summary3.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed | SummaryDisplayAreas.GroupByRowsFooter;

            e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
            e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = HAlign.Right;

            //e.Layout.Bands[0].SummaryFooterCaption = "Total:";
            e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            e.Layout.Override.SummaryFooterSpacingAfter = 5;
            e.Layout.Override.SummaryFooterSpacingBefore = 5;
        }
    }
}
