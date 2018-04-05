using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using FineUI;

namespace Sigesoft.Server.WebClientAdmin.UI.Security
{
    public partial class FRM002B : System.Web.UI.Page
    {
        #region Declarations

        SecurityBL _objSecurityBL = new SecurityBL();
        ApplicationHierarchyBL _objApplicationHierarchyBL = new ApplicationHierarchyBL();
        List<systemusergobalprofileDto> _listSystemUserGlobalProfileDtoAdd = new List<systemusergobalprofileDto>();
        List<systemusergobalprofileDto> _listSystemUserGlobalProfileDtoDelete = new List<systemusergobalprofileDto>();
        List<systemusergobalprofileDto> _listSystemUserGlobalProfileDtoUpdate = new List<systemusergobalprofileDto>();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["objlistSystemUserGlobalProfileDtoDelete"] = _listSystemUserGlobalProfileDtoDelete;
                Session["objlistSystemUserGlobalProfileDtoUpdate"] = _listSystemUserGlobalProfileDtoUpdate;
                LoadTreeGlobalPermissions();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();               

            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult1 = new OperationResult();
            int nodeId = -1;
            string personId = string.Empty;
            int systemUserId = -1;

            if (Request.QueryString["systemUserId"] != null)
                systemUserId = int.Parse(Request.QueryString["systemUserId"].ToString());

            if (hfMode.Text == "New")
            {
                // Graba permisos globales
                AccessInsertGlobal(systemUserId, tvGlobalPermissions.Nodes);

            }
            else if (hfMode.Text == "Edit")
            {
                if (Request.QueryString["nodeId"] != null)
                    nodeId = int.Parse(Request.QueryString["nodeId"].ToString());
                if (Request.QueryString["personId"] != null)
                    personId = Request.QueryString["personId"].ToString();

                // Actualiza Permisos globales
                AccessUpdateGlobal(systemUserId, tvGlobalPermissions.Nodes);
                // Elimina Permisos globales
                AccessDeleteGlobal(systemUserId, tvGlobalPermissions.Nodes);

                var objlistSystemUserGlobalProfileDtoUpdate = Session["objlistSystemUserGlobalProfileDtoUpdate"] as List<systemusergobalprofileDto>;
                var objlistSystemUserGlobalProfileDtoDelete = Session["objlistSystemUserGlobalProfileDtoDelete"] as List<systemusergobalprofileDto>;

                OperationResult objOperationResult = new OperationResult();
                _objSecurityBL.UpdateSystemUserGlobalProfiles(ref objOperationResult,
                                                                objlistSystemUserGlobalProfileDtoUpdate,
                                                                objlistSystemUserGlobalProfileDtoDelete,
                                                                ((ClientSession)Session["objClientSession"]).GetAsList());

            }

            // Cerrar página actual y hacer postback en el padre para actualizar
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

            //Operación con error
            //Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objCommonOperationResult.ExceptionMessage);
            // Se queda en el formulario.


        }

        private void LoadData()
        {
            int systemUserId = -1;
            if (Request.QueryString["systemUserId"] != null)
                systemUserId = int.Parse(Request.QueryString["systemUserId"].ToString());

            // Verificar si el usuario tiene permisos asignados
            OperationResult objSecurityOperationResult1 = new OperationResult();
            OperationResult objSecurityOperationResult2 = new OperationResult();

            var filterExpression = string.Format("i_SystemUserId={0}", systemUserId);
            int count1 = _objSecurityBL.GetSystemUserGlobalProfileCount(ref objSecurityOperationResult1, filterExpression);
           

            if (count1 > 0)
                hfMode.Text = "Edit";
            else
                hfMode.Text = "New";

            if (hfMode.Text == "New")
            {
                // Additional logic here.

            }
            else if (hfMode.Text == "Edit")
            {
                // Get the Entity Data
                OperationResult objOperationResult = new OperationResult();
                var objEntity = _objSecurityBL.GetSystemUser(ref objOperationResult, systemUserId);

                // Cargar permisos Globales
                OperationResult objCommonOperationResultGlobal = new OperationResult();
                var objGlobalAuthorization = _objSecurityBL.GetSystemUserGlobalProfiles(ref objCommonOperationResultGlobal, systemUserId);

                // Marcar (CkeckBox) los permisos en el Tree Contextual
                foreach (var item in objGlobalAuthorization)
                {
                    SearchNode(tvGlobalPermissions.Nodes, item.I_ApplicationHierarchyId, true);
                } 

            }
        }

        protected void tvGlobalPermissions_NodeCheck(object sender, FineUI.TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    tvGlobalPermissions.CheckAllNodes(e.Node.Nodes);
                }

                if (e.Node.ParentNode != null)
                {
                    if (!e.Node.ParentNode.Checked)
                    {
                        Utils.NodeParentsCheck(e.Node);
                        //Tree1.CheckAllNodes(e.Node.ParentNode.ParentNode.Nodes);
                    }

                    if (e.Node.Checked == false)
                    {
                        Utils.NodeParentsUnCheck(e.Node.ParentNode);
                    }
                }
            }
            else
            {
                tvGlobalPermissions.UncheckAllNodes(e.Node.Nodes);

                if (e.Node.ParentNode == null) return;

                if (e.Node.Checked == false)
                {
                    Utils.NodeParentsUnCheck(e.Node.ParentNode);
                }
            }

        }     

        private void LoadTreeGlobalPermissions()
        {
            OperationResult objOperationResult = new OperationResult();
            var listApplicationHierarchy = _objApplicationHierarchyBL.GetApplicationHierarchyByScopeId(ref objOperationResult, (int)Scope.Global);

            Utils.loadTreeMenu(tvGlobalPermissions, listApplicationHierarchy);           

        }      

        private void AccessInsertGlobal(int pSystemUserId, FineUI.TreeNodeCollection pNodes)
        {
            foreach (FineUI.TreeNode n in pNodes)
            {
                InsertGlobal(n, pSystemUserId);
            }

            //Grabar
            OperationResult objOperationResult = new OperationResult();
            _objSecurityBL.AddSystemUserGlobalProfiles(ref objOperationResult,
                                                        _listSystemUserGlobalProfileDtoAdd,
                                                        pSystemUserId,
                                                        ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                        true);

        }

        private void AccessUpdateGlobal(int pSystemUserId, FineUI.TreeNodeCollection pNodes)
        {
            foreach (FineUI.TreeNode n in pNodes)
            {
                UpdateGlobal(n, pSystemUserId);
            }
        }

        private void AccessDeleteGlobal(int pSystemUserId, FineUI.TreeNodeCollection pNodes)
        {
            foreach (FineUI.TreeNode n in pNodes)
            {
                DeleteGlobal(n, pSystemUserId);
            }
        }

        private void InsertGlobal(FineUI.TreeNode treeNode, int pSystemUserId)
        {
            if (treeNode.Checked == true)
            {
                var applicationHierarchyId = Convert.ToInt32(treeNode.NodeID);
                var optionText = treeNode.Text;

                systemusergobalprofileDto _systemUserGlobalProfileDTO = new systemusergobalprofileDto();
                _systemUserGlobalProfileDTO.i_ApplicationHierarchyId = applicationHierarchyId;
                _systemUserGlobalProfileDTO.i_SystemUserId = pSystemUserId;
                _systemUserGlobalProfileDTO.i_InsertUserId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
                _systemUserGlobalProfileDTO.d_InsertDate = DateTime.Now;

                // Cargar lista de permisos a grabar
                _listSystemUserGlobalProfileDtoAdd.Add(_systemUserGlobalProfileDTO);
            }

            foreach (FineUI.TreeNode tn in treeNode.Nodes)
            {
                InsertGlobal(tn, pSystemUserId);
            }
        }

        private void UpdateGlobal(FineUI.TreeNode treeNode, int pSystemUserId)
        {
            if (treeNode.Checked == true)
            {
                if (treeNode.CommandName != "1")
                {
                    var _applicationHierarchyId = Convert.ToInt32(treeNode.NodeID);
                    var _optionText = treeNode.Text;

                    systemusergobalprofileDto _systemUserGlobalProfileDTO = new systemusergobalprofileDto();

                    _systemUserGlobalProfileDTO.i_ApplicationHierarchyId = _applicationHierarchyId;
                    _systemUserGlobalProfileDTO.i_SystemUserId = pSystemUserId;
                    _systemUserGlobalProfileDTO.i_InsertUserId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
                    _systemUserGlobalProfileDTO.d_InsertDate = DateTime.Now;

                    // Cargar lista de permisos a actualizar
                    _listSystemUserGlobalProfileDtoUpdate.Add(_systemUserGlobalProfileDTO);
                }
            }

            foreach (FineUI.TreeNode tn in treeNode.Nodes)
            {
                UpdateGlobal(tn, pSystemUserId);
            }

            Session["objlistSystemUserGlobalProfileDtoUpdate"] = _listSystemUserGlobalProfileDtoUpdate;
        }

        private void DeleteGlobal(FineUI.TreeNode treeNode, int pSystemUserId)
        {
            if (!treeNode.Checked)
            {
                if (treeNode.CommandName == "1")
                {
                    _listSystemUserGlobalProfileDtoDelete.Add(new systemusergobalprofileDto
                    {
                        i_ApplicationHierarchyId = Convert.ToInt32(treeNode.NodeID),
                        i_SystemUserId = pSystemUserId,
                    });
                }
            }

            foreach (FineUI.TreeNode tn in treeNode.Nodes)
            {
                DeleteGlobal(tn, pSystemUserId);
            }

            Session["objlistSystemUserGlobalProfileDtoDelete"] = _listSystemUserGlobalProfileDtoDelete;
        }

        public void SearchNode(FineUI.TreeNodeCollection pNodes, int ApplicationHierarchyId, bool pStatus)
        {
            //Busca un nodo en el treeview y chekarlo
            foreach (FineUI.TreeNode sNode in pNodes)
            {
                if (sNode.NodeID.Trim() == ApplicationHierarchyId.ToString())
                {
                    sNode.Checked = pStatus;
                    sNode.CommandName = "1";
                    break;
                }
                SearchNode(sNode.Nodes, ApplicationHierarchyId, pStatus);
            }
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
           
        }

       
    }
}