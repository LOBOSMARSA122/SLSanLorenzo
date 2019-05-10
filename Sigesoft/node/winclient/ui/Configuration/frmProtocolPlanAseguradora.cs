using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.Contasol.Integration.Contasol;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmProtocolPlanAseguradora : Form
    {
        private string _protocolId;
        private string _aseguradoraId;
        PlanBl _objPlanBl;
        OperationResult objOperationResult1 = new OperationResult();
        BindingList<planDto> _gridDataSouce;
        List<planDto> _listToDelete;
        BindingList<planDto> OldListPlan = new BindingList<planDto>();

        #region GetChanges

        //private List<Campos> SetChangeProtocolComponent(BindingList<planDto> NewListPlan)
        //{
        //    var old = _OldProtocolcomponentListForcomentary;
        //    var nuevo = _tmpProtocolcomponentList;
        //    bool cambios = false;
        //    List<Campos> ComentaryProtComponent = new List<Campos>();
        //    foreach (var itemOld in old)
        //    {
        //        string cadena = _protocolBL.GetComentaryUpdateByProtocolComponentId(itemOld.v_ProtocolComponentId);


        //        cadena += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
        //        var itemNew = nuevo.Find(x => x.v_ProtocolComponentId == itemOld.v_ProtocolComponentId);

        //        if (cambios)
        //        {
        //            Campos _Campos = new Campos();
        //            _Campos.ValorCampo = cadena;
        //            _Campos.NombreCampo = itemOld.v_ProtocolComponentId;
        //            ComentaryProtComponent.Add(_Campos);
        //        }


        //    }

        //    return ComentaryProtComponent;

        //}


        #endregion
        


        public frmProtocolPlanAseguradora(string pstrProtocolId, string pstrAseguradoraId, string pstrNombreProtocolo)
        {
            InitializeComponent();
            Text = "Plan de: " + pstrNombreProtocolo;
            _listToDelete = new List<planDto>();
            _objPlanBl = new PlanBl();
            _protocolId = pstrProtocolId;
            _aseguradoraId = pstrAseguradoraId;
        }

        public void frmProtocolPlanAseguradora_Load(object sender, EventArgs e)
        {
            try
            {
                objOperationResult1 = new OperationResult();
                int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
                var dataListOrganization = BLL.Utils.GetOrganization(ref objOperationResult1);
                cboEmpresa.DataSource = dataListOrganization;
                cboEmpresa.DisplayMember = "Value1";
                cboEmpresa.ValueMember = "Id";
                if (!string.IsNullOrEmpty(_aseguradoraId))
                    cboEmpresa.Value = _aseguradoraId;
                else
                    cboEmpresa.SelectedIndex = 0;
                _gridDataSouce = _objPlanBl.ObtenerPlanesPorProtocolo(ref objOperationResult1, _protocolId);
                OldListPlan = _gridDataSouce;
                grd.DataSource = _gridDataSouce;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            objOperationResult1 = new OperationResult();
            try
            {
                frmAddPlan frm = new frmAddPlan(_protocolId, _aseguradoraId, _aseguradoraId);
                frm.ShowDialog();

                _gridDataSouce = _objPlanBl.ObtenerPlanesPorProtocolo(ref objOperationResult1, _protocolId);
                grd.DataSource = _gridDataSouce;
                //_gridDataSouce.AddNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grd_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                if (e.Cell == null) return;
                var item = (planDto)e.Cell.Row.ListObject;
                item.Editado = true;

                if (e.Cell.Column.Key.Equals("EsDeducible"))
                {
                    var estado = Convert.ToBoolean(e.Cell.EditorResolved.Value.ToString());
                    e.Cell.Row.Cells["EsCoaseguro"].SetValue(!estado, false);
                }

                if (e.Cell.Column.Key.Equals("EsCoaseguro"))
                {
                    var estado = Convert.ToBoolean(e.Cell.EditorResolved.Value.ToString());
                    e.Cell.Row.Cells["EsDeducible"].SetValue(!estado, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grd_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                var f = new frmLineaSelector();
                f.ShowDialog();
                var linea = f.LineaSeleccionada;
                if (linea == null) return;
                if (!_gridDataSouce.Any(p => p.v_IdUnidadProductiva != null && p.v_IdUnidadProductiva.Equals(linea.IdLinea)))
                {
                    var fila = grd.ActiveRow;
                    var item = (planDto)fila.ListObject;
                    fila.Cells["v_IdUnidadProductiva"].SetValue(linea.IdLinea, false);
                    fila.Cells["NombreLinea"].SetValue(linea.Nombre, false);
                    fila.Cells["v_OrganizationSeguroId"].SetValue(_aseguradoraId, false);
                    fila.Cells["v_ProtocoloId"].SetValue(_protocolId, false);
                    item.Editado = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                objOperationResult1 = new OperationResult();
                var data = grd.Rows.Select(p => (planDto)p.ListObject).ToList();
                foreach (var item in data)
                {
                    if (!item.EsCoaseguro && !item.EsDeducible)
                    {
                        MessageBox.Show("Por favor revise todos los tipos de descuento.!");
                        return;
                    }

                    if ((item.d_Importe ?? 0) < 0)
                    {
                        MessageBox.Show("Por favor revise todos los importes de descuento.!");
                        return;
                    }

                    item.i_EsCoaseguro = item.EsCoaseguro ? 1 : 0;
                    item.i_EsDeducible = item.EsDeducible ? 1 : 0;

                    _aseguradoraId = cboEmpresa.Value.ToString()/*.Split('|')[0]*/;
                    item.v_OrganizationSeguroId = _aseguradoraId.Trim();
                }

                //SetChangeProtocolComponent(data);
                _objPlanBl.UpdatePlan(_aseguradoraId, _protocolId, data, _listToDelete);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboEmpresa_ValueChanged(object sender, EventArgs e)
        {
            if (cboEmpresa.Value == null)
            {
                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
                _aseguradoraId = cboEmpresa.Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var msg = MessageBox.Show("¿Seguro de eliminar?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (msg == System.Windows.Forms.DialogResult.No) return;
            if (grd.ActiveRow == null) return;
            var item = (planDto)grd.ActiveRow.ListObject;
            _listToDelete.Add(item);
            _gridDataSouce.Remove(item);
        }

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            frmAddLineSAM frm = new frmAddLineSAM();
            frm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
