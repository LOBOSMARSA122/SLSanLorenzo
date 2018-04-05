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
namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmComisionVendedor : Form
    {
        string _FacturacionId;
        string _EmpresaCliente;
        string _EmpresaSede;
        string _NombreEmpresaCliente;
        DateTime? _FechaInicio;
        DateTime? _FechaFin;

        public frmComisionVendedor(string pstrFacturacionId, string pstrEmpresaCliente, string pstrEmpresaSede, DateTime pdtFechaInicio, DateTime pdtFechaFin, string pstrNombreEmpresaCliente)
        {
            _FacturacionId = pstrFacturacionId;
            _EmpresaCliente = pstrEmpresaCliente;
            _EmpresaSede = pstrEmpresaSede;
            _FechaInicio = pdtFechaInicio;
            _FechaFin = pdtFechaFin;
            _NombreEmpresaCliente = pstrNombreEmpresaCliente;
            InitializeComponent();
        }

        private void frmComisionVendedor_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            var DetalleFactura = new FacturacionBL().ComisionVendedor("", _EmpresaCliente, _EmpresaSede, _FechaInicio.Value.Date, _FechaFin.Value.Date, -1);

            List<ComisionVendedorList> Lista = new List<ComisionVendedorList>();
            ComisionVendedorList oComisionVendedorList ;

            foreach (var item in DetalleFactura)
            {
                //Verificar si es protocolo con comisión
                oComisionVendedorList = new ComisionVendedorList();
                bool Result = new ServiceBL().EsProtocoloComision(item.IdService);

                if (Result)
                {
                    oComisionVendedorList.v_ServicioId = item.IdService;

                    //Obtener datos del protocolo 
                    var Protocolo=   new ProtocolBL().GetProtocolByService(item.IdService);

                    oComisionVendedorList.Protocolo = Protocolo.v_Name;
                    oComisionVendedorList.Comision = Protocolo.Comision;
                    oComisionVendedorList.Costo = item.Total;
                    oComisionVendedorList.CostoComision = oComisionVendedorList.Costo.Value.ToString() + " - " + oComisionVendedorList.Comision.Value.ToString() + "%";
                    oComisionVendedorList.Vendedor = Protocolo.v_NombreVendedor;
                    oComisionVendedorList.SubTotal = oComisionVendedorList.Costo.Value * oComisionVendedorList.Comision.Value / 100;

                    Lista.Add(oComisionVendedorList);
                }

                if (Lista.Count > 0)
                {
                    txtNombreVendedor.Text = Lista[0].Vendedor.ToString();
                    txtEmpresaCliente.Text = _NombreEmpresaCliente;

                    txtTotalPagar.Text = Lista.Sum(p => p.SubTotal).ToString();
                    grdData.DataSource = Lista;
                }
               
               

            }
        }
    }
}
