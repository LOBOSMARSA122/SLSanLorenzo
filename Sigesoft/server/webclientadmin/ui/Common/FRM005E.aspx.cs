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

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRM005E : System.Web.UI.Page
    {
        #region Declarations

        SecurityBL _objSecurityBL = new SecurityBL();
        NodeBL _objNodeBL = new NodeBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ApplicationHierarchyBL _objApplicationHierarchyBL = new ApplicationHierarchyBL();
        List<rolenodeprofileDto> _listRoleNodeProfileDtoAdd = new List<rolenodeprofileDto>();
        List<rolenodeprofileDto> _listRoleNodeProfileDtoDelete = null;
        List<rolenodeprofileDto> _listRoleNodeProfileDtoUpdate = new List<rolenodeprofileDto>();

        #endregion

        #region Properties

        public int NodeId
        {
            get
            {
                if (Request.QueryString["nodeId"] != null)
                {
                    string nodeId = Request.QueryString["nodeId"].ToString();
                    if (!string.IsNullOrEmpty(nodeId))
                    {
                        return Convert.ToInt32(nodeId);
                    }
                }

                return 0;
            }
        }

        public int RoleId
        {
            get
            {
                if (Request.QueryString["roleId"] != null)
                {
                    string roleId = Request.QueryString["roleId"].ToString();
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        return Convert.ToInt32(roleId);
                    }
                }

                return 0;
            }
        }

        public string Mode
        {
            get
            {
                if (Request.QueryString["Mode"] != null)
                {
                    string mode = Request.QueryString["Mode"].ToString();
                    if (!string.IsNullOrEmpty(mode))
                    {
                        return mode;
                    }
                }

                return string.Empty;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadTreePermissions();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            
            OperationResult objOperationResult1 = new OperationResult();

            if (hfMode.Text == "New" )
            {
                // Graba permisos globales
                AccessInsertGlobal(tvContextPermissions.Nodes);

            }
            else if (hfMode.Text == "Edit")
            {
                // Actualiza Permisos globales
                AccessUpdateGlobal(tvContextPermissions.Nodes);
                // Elimina Permisos globales
                AccessDeleteGlobal(tvContextPermissions.Nodes);

                var sobjRoleNodeProfileDtoUpdate = Session["sobjRoleNodeProfileDtoUpdate"] as List<rolenodeprofileDto>;
                var sobjRoleNodeProfileDtoDelete = Session["sobjRoleNodeProfileDtoDelete"] as List<rolenodeprofileDto>;

                OperationResult objOperationResult = new OperationResult();
                _objNodeBL.UpdateRoleNodeProfile(ref objOperationResult,
                                                    sobjRoleNodeProfileDtoUpdate,
                                                    sobjRoleNodeProfileDtoDelete,
                                                    ((ClientSession)Session["objClientSession"]).GetAsList());


                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                    return;
                }


            }

            // Cerrar página actual y hacer postback en el padre para actualizar
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

            //Operación con error
            //Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objCommonOperationResult.ExceptionMessage);
            // Se queda en el formulario.


        }

        private void LoadData()
        {

            LoadComboBox();

            // Verificar si el usuario tiene permisos asignados
            OperationResult objSecurityOperationResult1 = new OperationResult();
           
            if (Mode == "New")
            {
                hfMode.Text = "New";
            }
            else if (Mode == "Edit")
            {
                ddlRole.SelectedValue = RoleId.ToString();
                ddlRole.Enabled = false;

                var filterExpression = string.Format("i_NodeId=={0}&&i_RoleId=={1}&&i_IsDeleted==0", NodeId, RoleId);
                int count1 = _objNodeBL.GetRoleNodeProfileCount(ref objSecurityOperationResult1, filterExpression);

                if (count1 == 0)
                {
                    // Additional logic here.
                    hfMode.Text = "New";
                }
                else
                {
                    hfMode.Text = "Edit";
                    // Cargar permisos Globales
                    OperationResult objCommonOperationResultGlobal = new OperationResult();
                    var objRoleNodeProfile = _objNodeBL.GetRoleNodeProfile(ref objCommonOperationResultGlobal, NodeId, RoleId);

                    if (objRoleNodeProfile != null)
                    {
                        // Marcar (CkeckBox) los permisos en el Tree Contextual
                        foreach (var item in objRoleNodeProfile)
                        {
                            SearchNode(tvContextPermissions.Nodes, int.Parse(item.Id), true);
                        } 
                    }
                    
                }
            }
           
        }

        protected void tvGlobalPermissions_NodeCheck(object sender, FineUI.TreeCheckEventArgs e)
        {
            if (e.Checked)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    tvContextPermissions.CheckAllNodes(e.Node.Nodes);
                }

                if (e.Node.ParentNode != null)
                {
                    if (!e.Node.ParentNode.Checked)
                    {
                        Utils.NodeParentsCheck(e.Node);                      
                    }

                    if (e.Node.Checked == false)
                    {
                        Utils.NodeParentsUnCheck(e.Node.ParentNode);
                    }
                }
            }
            else
            {
                tvContextPermissions.UncheckAllNodes(e.Node.Nodes);

                if (e.Node.ParentNode == null) return;

                if (e.Node.Checked == false)
                {
                    Utils.NodeParentsUnCheck(e.Node.ParentNode);
                }
            }

        }     

        private void LoadTreePermissions()
        {
            OperationResult objOperationResult = new OperationResult();
            var listApplicationHierarchy = _objApplicationHierarchyBL.GetApplicationHierarchyByScopeId(ref objOperationResult, (int)Scope.Contextual);

            Utils.loadTreeMenu(tvContextPermissions, listApplicationHierarchy);                  

        }      

        private void AccessInsertGlobal(FineUI.TreeNodeCollection pNodes)
        {
            foreach (FineUI.TreeNode n in pNodes)
            {
                InsertGlobal(n);
            }

            //Grabar
            OperationResult objOperationResult = new OperationResult();

            rolenodeDto objRoleNodeDto = new rolenodeDto();

            objRoleNodeDto.i_NodeId = NodeId;
            objRoleNodeDto.i_RoleId = int.Parse(ddlRole.SelectedValue);

            var sobjRoleNodeProfileDtoAdd = Session["sobjRoleNodeProfileDtoAdd"] as List<rolenodeprofileDto>;

            _objNodeBL.AddRoleNodeProfile(ref objOperationResult,
                                        objRoleNodeDto,
                                        sobjRoleNodeProfileDtoAdd,                                                       
                                        ((ClientSession)Session["objClientSession"]).GetAsList(),
                                        true);


            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                return;
            }

        }

        private void AccessUpdateGlobal(FineUI.TreeNodeCollection pNodes)
        {
            foreach (FineUI.TreeNode n in pNodes)
            {
                UpdateGlobal(n);
            }
        }

        private void AccessDeleteGlobal(FineUI.TreeNodeCollection pNodes)
        {
            foreach (FineUI.TreeNode n in pNodes)
            {
                DeleteGlobal(n);
            }
        }

        private void InsertGlobal(FineUI.TreeNode treeNode)
        {
            if (treeNode.Checked == true)
            {
                var applicationHierarchyId = Convert.ToInt32(treeNode.NodeID);
                var optionText = treeNode.Text;

                rolenodeprofileDto objRolenodeprofileDTO = new rolenodeprofileDto();
                objRolenodeprofileDTO.i_NodeId = NodeId;
                objRolenodeprofileDTO.i_RoleId = int.Parse(ddlRole.SelectedValue);
                objRolenodeprofileDTO.i_ApplicationHierarchyId = applicationHierarchyId;           
                objRolenodeprofileDTO.i_InsertUserId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
              
                // Cargar lista de permisos a grabar
                _listRoleNodeProfileDtoAdd.Add(objRolenodeprofileDTO);
            }

            foreach (FineUI.TreeNode tn in treeNode.Nodes)
            {
                InsertGlobal(tn);
            }

            Session["sobjRoleNodeProfileDtoAdd"] = _listRoleNodeProfileDtoAdd;
        }

        private void UpdateGlobal(FineUI.TreeNode treeNode)
        {
            if (treeNode.Checked == true)
            {
                if (treeNode.CommandName != "1")
                {
                    var applicationHierarchyId = Convert.ToInt32(treeNode.NodeID);
                 
                    rolenodeprofileDto objRoleNodeProfileDto = new rolenodeprofileDto();

                    objRoleNodeProfileDto.i_NodeId = NodeId;
                    objRoleNodeProfileDto.i_RoleId = RoleId;
                    objRoleNodeProfileDto.i_ApplicationHierarchyId = applicationHierarchyId;               
                    objRoleNodeProfileDto.i_InsertUserId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
                    
                    // Cargar lista de permisos a actualizar
                    _listRoleNodeProfileDtoUpdate.Add(objRoleNodeProfileDto);
                }
            }

            foreach (FineUI.TreeNode tn in treeNode.Nodes)
            {
                UpdateGlobal(tn);
            }

            Session["sobjRoleNodeProfileDtoUpdate"] = _listRoleNodeProfileDtoUpdate.Count == 0 ? null : _listRoleNodeProfileDtoUpdate;
        }

        private void DeleteGlobal(FineUI.TreeNode treeNode)
        {

            if (_listRoleNodeProfileDtoDelete == null)
            {
                _listRoleNodeProfileDtoDelete = new List<rolenodeprofileDto>();
            }

            if (!treeNode.Checked)
            {
                if (treeNode.CommandName == "1")
                {
                    _listRoleNodeProfileDtoDelete.Add(new rolenodeprofileDto
                    {
                        i_NodeId = NodeId,
                        i_RoleId = RoleId,
                        i_ApplicationHierarchyId = Convert.ToInt32(treeNode.NodeID),                      
                    });
                }
            }

            foreach (FineUI.TreeNode tn in treeNode.Nodes)
            {
                DeleteGlobal(tn);
            }

            Session["sobjRoleNodeProfileDtoDelete"] = _listRoleNodeProfileDtoDelete.Count == 0 ? null : _listRoleNodeProfileDtoDelete;
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

        private void LoadComboBox()
        {
            OperationResult objOperationResult1 = new OperationResult();
            var roleNode = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult1, 115);
            Sigesoft.Server.WebClientAdmin.UI.Utils.LoadDropDownList(ddlRole, "Value1", "Id", roleNode, DropDownListAction.Select);  
        }

        private bool IsValidNodeRole()
        {
            // Validar existencia de un nodo
            OperationResult objOperationResult6 = new OperationResult();
            int roleId = int.Parse(ddlRole.SelectedValue);

            string filterExpression = string.Format("i_NodeId=={0}&&i_RoleId=={1}&&i_IsDeleted==0", NodeId, roleId);
            var recordCount = _objNodeBL.GetRoleNodeCount(ref objOperationResult6, filterExpression);

            if (recordCount != 0)
            {
                Alert.ShowInTop(string.Format("El rol de <font color='red'> {0} </font> ya se encuentra registrado.<br> Por favor elija otro Rol.", ddlRole.SelectedText));
                return false;
            }
            return true;
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                if (!tvContextPermissions.Enabled)
                    tvContextPermissions.Enabled = true;

                // Validar nodo-rol
                if (!IsValidNodeRole())
                {
                    tvContextPermissions.Enabled = false;
                    return;
                }
            }
        }

        //protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        //{

        //}
    }
}