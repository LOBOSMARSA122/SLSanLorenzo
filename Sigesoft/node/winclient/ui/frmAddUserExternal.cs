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

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAddUserExternal : Form
    {
        List<ProtocolSystemUSerExternalList> _TempProtocolSystemUSerExternalList = new List<ProtocolSystemUSerExternalList>();
        string _ProtocolId;
        string _EmpresaId;

        public frmAddUserExternal(string pstrProtocolId)
        {
              OperationResult objOperationResult = new OperationResult();
            ProtocolBL objProtocolBL = new ProtocolBL();
            var obj =objProtocolBL.GetProtocolById(ref objOperationResult, pstrProtocolId);
            _EmpresaId = obj.v_CustomerOrganizationId;
            _ProtocolId = pstrProtocolId;

            InitializeComponent();
        }

        private void frmAddUserExternal_Load(object sender, EventArgs e)
        {
            LoadTreeDangers();
        }

        private void LoadTreeDangers()
        {
            OperationResult objOperationResult = new OperationResult();
            ProtocolBL objProtocolBL = new ProtocolBL();
            List<SystemUserList> objSystemUserList = new List<SystemUserList>();

            treeViewUserExternal.Nodes.Clear();
            TreeNode nodePrimary = null;

            objSystemUserList = objProtocolBL.GetAllSystemUserExternal();

            foreach (var item in objSystemUserList)
            {
                    #region Add Main Nodes
                    //case "-1": // 1. Add Main nodes:
                        nodePrimary = new TreeNode();
                        nodePrimary.Text = item.v_UserName;
                        nodePrimary.Name = item.i_SystemUserId.ToString();
                        treeViewUserExternal.Nodes.Add(nodePrimary);
                        //break;
                    #endregion
                   
            }
            treeViewUserExternal.ExpandAll();
        }

        private TreeNode SelectChildrenRecursive(TreeNode tn, string searchValue)
        {
            if (tn.Name == searchValue)
            {
                return tn;
            }
            else
            {
                //tn.Collapse();
            }
            if (tn.Nodes.Count > 0)
            {
                TreeNode objNode = new TreeNode();

                foreach (TreeNode tnC in tn.Nodes)
                {
                    objNode = SelectChildrenRecursive(tnC, searchValue);
                    if (objNode != null) return objNode;
                }
            }
            return null;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            ProtocolSystemUSerExternalList objProtocolSystemUSerExternalList = new ProtocolSystemUSerExternalList();
            if (_TempProtocolSystemUSerExternalList == null)
                _TempProtocolSystemUSerExternalList = new List<ProtocolSystemUSerExternalList>();
            int SystemUserID = int.Parse(treeViewUserExternal.SelectedNode.Name.ToString());
            //Busco en la lista temporal si ya se agrego el item seleccionado
            var findResult = _TempProtocolSystemUSerExternalList.Find(p => p.i_SystemUserId == SystemUserID);
            
            if (findResult == null)
                {

                //Verificar si el usuario se encuentra en otra empresa
                    //Verificar cuantos protocolos està asigando este usuario
                    var ListaProtocolos = new ProtocolBL().GetProtocolbySystemUserId(SystemUserID);

                    ////Verificar si el usuario esta en una empresa diferente
                    //if (ListaProtocolos.FindAll(p => p.v_CustomerOrganizationId != _EmpresaId).Count() > 0)
                    //{
                    //    MessageBox.Show("El usuario: " + treeViewUserExternal.SelectedNode.Text + " ya se encuentra asiganado a la empresa: " + ListaProtocolos[0].v_CustomerOrganizationName, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //} 
                    objProtocolSystemUSerExternalList.v_UserName = treeViewUserExternal.SelectedNode.Text;
                    objProtocolSystemUSerExternalList.i_SystemUserId = int.Parse(treeViewUserExternal.SelectedNode.Name.ToString());
                //objProtocolSystemUSerExternalList.v_ProtocolId = 
                    _TempProtocolSystemUSerExternalList.Add(objProtocolSystemUSerExternalList);
                }
            else{
                 MessageBox.Show("Este Usuario ya se encuentra en la lista", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            var dataList = _TempProtocolSystemUSerExternalList;
            grdDataUserExternal.DataSource = new ProtocolSystemUSerExternalList();
            grdDataUserExternal.DataSource = dataList;
            grdDataUserExternal.Refresh();

            if (grdDataUserExternal.Rows.Count() > 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
               OperationResult objOperationResult = new OperationResult();
            foreach (var item in _TempProtocolSystemUSerExternalList)
            {
                List<protocolsystemuserDto> x = new ProtocolBL().GetAllSystemUserExternalBySystemUserId(item.i_SystemUserId);

                for (int i = 0; i <  x.Count(); i++)
                {
                    x[i].v_ProtocolId = _ProtocolId;
                }

                new ProtocolBL().AddProtocolSystemUser(ref objOperationResult, x, item.i_SystemUserId, Globals.ClientSession.GetAsList(),false);

                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else  // Operación con error
                {
                    if (objOperationResult.ErrorMessage != null)
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _TempProtocolSystemUSerExternalList.RemoveAt(int.Parse(grdDataUserExternal.Rows[0].Index.ToString()));
          
            grdDataUserExternal.DataSource = new ProtocolSystemUSerExternalList();
            grdDataUserExternal.DataSource = _TempProtocolSystemUSerExternalList;
            grdDataUserExternal.Refresh();
            btnDelete.Enabled = false;


            if (grdDataUserExternal.Rows.Count() > 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }

        }

        private void grdDataUserExternal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                }
            }
        }
                     
    }
}
