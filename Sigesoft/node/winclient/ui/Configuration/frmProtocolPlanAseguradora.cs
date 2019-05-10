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

        private List<Campos> SetChangeProtocolComponent(List<planDto> NewListPlan)
        {
            //var nuevo = _tmpProtocolcomponentList;
            
            List<Campos> ListPlan = new List<Campos>();
            foreach (var itemOld in OldListPlan)
            {
                bool cambios = false;
                string comentario = _objPlanBl.GetComentaryUpdateByPlanId(itemOld.i_PlanId);

                comentario += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
                var itemNew = NewListPlan.Find(x => x.i_PlanId == itemOld.i_PlanId);
                
                if (itemOld.i_EsCoaseguro != itemNew.i_EsCoaseguro)
                {
                    var valor = itemOld.i_EsCoaseguro == 0 && itemOld.i_EsCoaseguro != null ? "NO" : "SI";
                    comentario += "EsCoaseguro:" + valor + "|";
                    cambios = true;
                }

                if (itemOld.d_Importe != itemNew.d_Importe)
                {
                    comentario += "Importe:" + itemOld.d_Importe + "|";
                    cambios = true;
                }

                if (itemOld.d_ImporteCo != itemNew.d_ImporteCo)
                {
                    comentario += "ImporteCo:" + itemOld.d_ImporteCo + "|";
                    cambios = true;
                }

                if (itemOld.i_EsDeducible != itemNew.i_EsDeducible)
                {
                    var valor = itemOld.i_EsDeducible == 0 && itemOld.i_EsDeducible != null ? "NO" : "SI";
                    comentario += "EsDeducible:" + valor + "|";
                    cambios = true;
                }

                if (itemOld.NombreLinea != itemNew.NombreLinea)
                {
                    comentario += "UnidadProductiva:" + itemOld.NombreLinea + "|";
                    cambios = true;
                }



                if (cambios)
                {
                    Campos _Campos = new Campos();
                    _Campos.ValorCampo = comentario;
                    _Campos.NombreCampo = itemOld.i_PlanId.ToString();
                    ListPlan.Add(_Campos);
                }
            }

            return ListPlan;
        }


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
                //Obtengo la data antes de que se actualize
                OldListPlan = _objPlanBl.ObtenerPlanesPorProtocolo(ref objOperationResult1, _protocolId);

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

                var cambiados = SetChangeProtocolComponent(data);

                foreach (var plan in cambiados)
                {
                    var itemData = data.Find(y => y.i_PlanId == Int32.Parse(plan.NombreCampo));
                    if (itemData != null)
                    {
                        data.Find(y => y.i_PlanId == Int32.Parse(plan.NombreCampo)).v_ComentaryUpdate = plan.ValorCampo;
                    }
                }

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
