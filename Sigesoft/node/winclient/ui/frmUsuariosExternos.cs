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
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.UI.Configuration;
  
namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmUsuariosExternos : Form
    {
        string strFilterExpression;
        public frmUsuariosExternos()
        {
            InitializeComponent();
        }

        private void frmUsuariosExternos_Load(object sender, EventArgs e)
        {
            LoadCombox();
        }

        private void LoadCombox()
        {
            OperationResult objOperationResult = new OperationResult();
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);

            ddlProtocolId.Enabled = true;
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetUsuariosExternos(ref objOperationResult, ""), DropDownListAction.All);
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);
            
        }

        private void ddlCustomerOrganization_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }

            ddlProtocolId.Enabled = true;

            OperationResult objOperationResult = new OperationResult();

            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);          
            
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            var frm = new frmExternalUserEditSinProtocol("New", null, null, "");
            frm.ShowDialog();

            if (frm.DialogResult != DialogResult.OK)
                return;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerOrganizationId==" + "\"" + id3[1] + "\"");
            }

            if (ddlProtocolId.SelectedValue.ToString() != "-1") Filters.Add("v_ProtocolId==" + "\"" + ddlProtocolId.SelectedValue + "\"");

            if (ddlUsuario.SelectedValue.ToString() != "-1") Filters.Add("i_SystemUserId==" +  ddlUsuario.SelectedValue );


            // Create the Filter Expression
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            BindGrid();
        }

        private void BindGrid()
        {

            var objData = GetData(0, null, "", strFilterExpression);
 
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdData.Rows.Count > 0)
                grdData.Rows[0].Selected = true;
        }

        private List<ProtocolSystemUSerExternalList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceBL serviceBL = new ServiceBL();
            var _objData = serviceBL.GetProtocolSystemExternalList(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ServiceBL oServiceBL = new ServiceBL();
             int UsuarioId = int.Parse(grdData.Selected.Rows[0].Cells["i_SystemUserId"].Value.ToString());
             int IsDeleted = int.Parse(grdData.Selected.Rows[0].Cells["i_IsDeleted"].Value.ToString());
            oServiceBL.ActualizarEstadoUsuarioExterno(UsuarioId,IsDeleted, Globals.ClientSession.GetAsList());

            if (IsDeleted == 1)
            {
                MessageBox.Show("Usuario Activado.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Usuario Desactivado.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int UsuarioId = int.Parse(grdData.Selected.Rows[0].Cells["i_SystemUserId"].Value.ToString());
            string PersonId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            string ProtocolId = grdData.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            var frm = new frmExternalUserEditSinProtocol("Edit", PersonId, UsuarioId, ProtocolId);
            frm.ShowDialog();
        }

        private void grdData_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells["i_IsDeleted"].Value.ToString() == "1")
            {
                e.Row.Appearance.BackColor = Color.Red;
                e.Row.Appearance.BackColor2 = Color.White;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }
        }

    }
}
