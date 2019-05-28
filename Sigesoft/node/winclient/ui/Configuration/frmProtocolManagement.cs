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

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmProtocolManagement : Form
    {
        private string _Mode;
        private int _pintServiceId;
        private int _pintServiceTypeId;
        public  string _pstrProtocolId;

        #region Declarations
        private ProtocolBL _protocolBL = new ProtocolBL();
        private string _protocolId;
     
        #endregion

        public frmProtocolManagement(string pstrMode, int pintServiceTypeId,int pintServiceId)
        {
            InitializeComponent();
            _Mode = pstrMode;
            if (_Mode == "View")
            {
                _pintServiceId = pintServiceId;
                _pintServiceTypeId = pintServiceTypeId;
                 //cmProtocol.Enabled = false;
                 chkIsActive.Enabled = false;

                 btnNuevo.Enabled = false;
                 btnEditar.Enabled = false;
                 btnClon.Enabled = false;
                 btnGenerarOS.Enabled = false;
                 lblCostoTotal.Visible = false;
                 cbServiceType.Enabled = true;
                 cbService.Enabled = true;

                 //cbMasterService.SelectedValue = pintServiceId.ToString();
                 ////cbMasterService.Enabled = false;
            }
        }

        public frmProtocolManagement()
        {
            InitializeComponent();           
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {           
            BindGrid();
        }

        private string BuildFilterExpression()
        {
            // Get the filters from the UI
            string filterExpression = string.Empty;

            List<string> Filters = new List<string>();

            if (!string.IsNullOrEmpty(txtProtocolName.Text)) Filters.Add("v_Protocol.Contains(\"" + txtProtocolName.Text.Trim() + "\")");
            if (cbOrganization.SelectedValue.ToString() != "-1")
            {
                var id1 = cbOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_OrganizationId==" + "\"" + id1[0] + "\"&&v_LocationId==" + "\"" + id1[1] + "\"");
            }
            if (cbEsoType.SelectedValue.ToString() != "-1") Filters.Add("i_EsoTypeId==" + int.Parse(cbEsoType.SelectedValue.ToString()));
            if (cbGeso.SelectedValue.ToString() != "-1") Filters.Add("v_GroupOccupationId==" + "\"" + cbGeso.SelectedValue + "\"");
            if (cbIntermediaryOrganization.SelectedValue.ToString() != "-1")
            {
                var id2 = cbIntermediaryOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_WorkingOrganizationId==" + "\"" + id2[0] + "\"&&v_WorkingLocationId==" + "\"" + id2[1] + "\"");
            }
            if (cbOrganizationInvoice.SelectedValue.ToString() != "-1")
            {
                var id3 = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
                Filters.Add("v_OrganizationInvoiceId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }

            if (cbServiceType.SelectedValue.ToString() != "-1")
            {
                Filters.Add("i_ServiceTypeId==" + int.Parse(cbServiceType.SelectedValue.ToString()));
            }

            if (cbService.SelectedValue.ToString() != "-1")
            {           
                Filters.Add("i_MasterServiceId==" + int.Parse(cbService.SelectedValue.ToString()));
            }
            
            Filters.Add("i_IsActive ==" + Convert.ToInt32(chkIsActive.Checked).ToString());
           
            //Filters.Add("i_IsDeleted==0");
            // Create the Filter Expression
            filterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    filterExpression = filterExpression + item + " && ";
                }
                filterExpression = filterExpression.Substring(0, filterExpression.Length - 4);
            }

            return filterExpression;
        }

        private void New_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(string.Empty, "New");
            frm.ShowDialog();
          
            // Refrescar grilla
            btnFilter_Click(sender, e);
            
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(_protocolId, "Edit");
            frm.ShowDialog();
          
            // Refrescar grilla
            btnFilter_Click(sender, e);
            
        }

        private void Clonar_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(_protocolId, "Clon");
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }     

        private void BindGrid()
        {
            var dataList = GetData(0, null, "v_Protocol ASC", BuildFilterExpression());

            if (dataList != null )
            {
                if (dataList.Count != 0)
                {                  
                    grd.DataSource = dataList;                   
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
                }
                else
                {
                    grd.DataSource = dataList;                  
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", 0);

                }

                grdProtocolComponent.DataSource = new List<ProtocolComponentList>();
                lblCostoTotal.Text = "";
            }
          
        }

        private List<ProtocolList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataList = _protocolBL.GetProtocolPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression,txtComponente.Text);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataList;
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }
        
        private void frmProtocolManagement_Load(object sender, EventArgs e)
        {
            this.Show();
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }

            #endregion

            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.All);
            LoadComboBox();
            BindGrid();

            if (grd.Rows.Count != 0)          
                grd.Rows[0].Selected = true;

  
           
        }

        private void LoadComboBox()
        {
            // Llenado de combos
            // Tipos de eso
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

            // Lista de empresas por nodo
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            OperationResult objOperationResult1 = new OperationResult();
            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);

            Utils.LoadDropDownList(cbOrganization,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.All);

            Utils.LoadDropDownList(cbIntermediaryOrganization,
               "Value1",
               "Id",
               dataListOrganization1,
               DropDownListAction.All);

            Utils.LoadDropDownList(cbOrganizationInvoice,
              "Value1",
              "Id",
              dataListOrganization2,
              DropDownListAction.All);

            //Llenado de los tipos de servicios [Emp/Part]
            Utils.LoadDropDownList(cbServiceType, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1, null), DropDownListAction.All);
            // combo servicio
            Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.All);
           
            if (_Mode == "View")
            {          
                Utils.LoadDropDownList(cbServiceType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 119, null), DropDownListAction.All);
                cbServiceType.SelectedValue = _pintServiceTypeId.ToString();

                Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 119, null), DropDownListAction.All);
                cbService.SelectedValue = _pintServiceId.ToString();
               
                cbServiceType.Enabled = false;
                cbService.Enabled = false;
            }
            else
            {
                //Utils.LoadDropDownList(cbMasterService, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 119, null), DropDownListAction.All);

            }

           
        }

        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbGESO();
           
        }

        private void LoadcbGESO()
        {
            var index = cbOrganization.SelectedIndex;
            if (index == 0)
                return;

            var dataList = cbOrganization.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];

            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, idOrg), DropDownListAction.All);
        }

        private void grd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grd.Rows[row.Index].Selected = true;
                    _protocolId = row.Cells["v_ProtocolId"].Value.ToString();
                    cmProtocol.Items["Edit"].Enabled = true;
                    cmProtocol.Items["Clonar"].Enabled = true;                                  
                }
                else
                {
                    cmProtocol.Items["Edit"].Enabled = false;
                    cmProtocol.Items["Clonar"].Enabled = false;
                }

            } 
        }

        private void grd_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_Mode == "View")
            {
                if (grd.Selected.Rows.Count == 0) 
                    return;

                _pstrProtocolId = grd.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                
            }

        }

        private void cbMasterService_TextChanged(object sender, EventArgs e)
        {
            if (cbServiceType.SelectedIndex == 0 || cbServiceType.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbServiceType.SelectedValue.ToString());
            Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.All);

        }

        private void grd_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grd.Selected.Rows.Count != 0)
            {
                string protocolName = grd.Selected.Rows[0].Cells["v_Protocol"].Value.ToString();
                float Total = 0;
                _protocolId = grd.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

                gbProtocolComponents.Text = string.Format("Comp. del Prot. < {0} >", protocolName);

                // Cargar componentes de un protocolo seleccionado
                OperationResult objOperationResult = new OperationResult();
                var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, _protocolId);

                grdProtocolComponent.DataSource = dataListPc;             

                lblRecordCountProtocolComponents.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());

                foreach (var item in dataListPc)
                {
                    Total = Total + item.r_Price.Value; 
                }
                lblCostoTotal.Text = Total.ToString();
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void grd_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(string.Empty, "New");
            frm.ShowDialog();

            // Refrescar grilla
            btnFilter_Click(sender, e);
            BindGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(_protocolId, "Edit");
            frm.ShowDialog();

            // Refrescar grilla
            btnFilter_Click(sender, e);
            BindGrid();
            grdProtocolComponent.DataSource = new List<ProtocolComponentList>();
            lblCostoTotal.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(_protocolId, "Clon");
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerarOS_Click(object sender, EventArgs e)
        {
            frmServiceOrderEdit frm = new frmServiceOrderEdit("", _protocolId, "New");
            frm.ShowDialog();

        }

        private void txtProtocolName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BindGrid();
            }
        }

        private void btnUsuariosExternos_Click(object sender, EventArgs e)
        {
            var frm = new frmExternalUserEditSinProtocol("New", null, null, _protocolId);
            frm.ShowDialog();

            if (frm.DialogResult != DialogResult.OK)
                return;
        }

        private void gbProtocolComponents_Enter(object sender, EventArgs e)
        {

        }

        private void btnPlanes_Click(object sender, EventArgs e)
        {
            try
            {
                if(grd.ActiveRow == null) return;
                var pId = grd.ActiveRow.Cells["v_ProtocolId"].Value.ToString();
                var aseg = grd.ActiveRow.Cells["AseguradoraId"].Value != null ? grd.ActiveRow.Cells["AseguradoraId"].Value.ToString() : string.Empty;
                var nombre = grd.ActiveRow.Cells["v_Protocol"].Value.ToString();
                var f = new frmProtocolPlanAseguradora(pId, aseg, nombre);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void verCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string commentary = _protocolBL.GetComentaryUpdateByProtocolId(_protocolId);
            if (commentary == "")
            {
                MessageBox.Show("Aún no se han realizado cambios.", "AVISO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var frm = new frmViewChanges(commentary);
            frm.ShowDialog();
        }
               
    }
}
