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
    public partial class FRM005DAA : System.Web.UI.Page
    {
        NodeBL _nodeBL = new NodeBL();         

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

        public string RoleNodeComponentId
        {
            get
            {
                if (Request.QueryString["roleNodeComponentId"] != null)
                {
                    string roleNodeComponentId = Request.QueryString["roleNodeComponentId"].ToString();
                    if (!string.IsNullOrEmpty(roleNodeComponentId))
                    {
                        return roleNodeComponentId;
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
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void LoadData()
        {          
            OperationResult objOperationResult = new OperationResult();

            var dataListComponents = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetComponentsFilter(ref objOperationResult);
            Utils.LoadDropDownList(ddlComponent, "Value1", "Id", dataListComponents, DropDownListAction.Select);

            if (Mode == "New")
            {
                   
            }
            else if (Mode == "Edit")
            {
                ddlComponent.Enabled = false;

                var roleNodeComponentProfile = _nodeBL.GetRoleNodeComponentProfile(ref objOperationResult, RoleNodeComponentId);

                ddlComponent.SelectedValue = roleNodeComponentProfile.v_ComponentId;
                chkRead.Checked = Convert.ToBoolean(roleNodeComponentProfile.i_Read);
                chkWrite.Checked = Convert.ToBoolean(roleNodeComponentProfile.i_Write);

                if (roleNodeComponentProfile.i_Dx != -1)
                {
                    chkDx.Checked = Convert.ToBoolean(roleNodeComponentProfile.i_Dx);
                }
                else
                {
                    chkDx.Enabled = false;
                }

                if (roleNodeComponentProfile.i_Approved != -1)
                {
                    chkApproved.Checked = Convert.ToBoolean(roleNodeComponentProfile.i_Approved);
                }
                else
                {
                    chkApproved.Enabled = false;
                }

                Session["sroleNodeComponentProfile"] = roleNodeComponentProfile;

            }

        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult1 = new OperationResult();

            if (Mode == "New")
            {                         
                var roleNodeComponentProfile = new rolenodecomponentprofileDto();

                roleNodeComponentProfile.i_NodeId = NodeId;
                roleNodeComponentProfile.i_RoleId = RoleId;
                roleNodeComponentProfile.v_ComponentId = ddlComponent.SelectedValue;
                roleNodeComponentProfile.i_Read = Convert.ToInt32(chkRead.Checked);
                roleNodeComponentProfile.i_Write = Convert.ToInt32(chkWrite.Checked);
                roleNodeComponentProfile.i_Dx = !chkDx.Enabled ? -1 : Convert.ToInt32(chkDx.Checked);
                roleNodeComponentProfile.i_Approved = !chkApproved.Enabled ? -1 : Convert.ToInt32(chkApproved.Checked);

                _nodeBL.AddRoleNodeComponentProfile(ref objOperationResult1,
                                                    roleNodeComponentProfile, 
                                                    ((ClientSession)Session["objClientSession"]).GetAsList());


                if (objOperationResult1.ErrorMessage != null)
                {
                    Alert.ShowInTop(string.Format("<font color='red'> {0} </font> ya se encuentra registrado. Por favor elija otro.", ddlComponent.SelectedText));
                    return;
                }

                if (objOperationResult1.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult1.ExceptionMessage);
                    return;
                }

            }
            else if (Mode == "Edit")
            {
                var roleNodeComponentProfile = Session["sroleNodeComponentProfile"] as rolenodecomponentprofileDto;

                roleNodeComponentProfile.v_RoleNodeComponentId = RoleNodeComponentId;
                roleNodeComponentProfile.i_Read = Convert.ToInt32(chkRead.Checked);
                roleNodeComponentProfile.i_Write = Convert.ToInt32(chkWrite.Checked);
                roleNodeComponentProfile.i_Dx = !chkDx.Enabled ? -1 : Convert.ToInt32(chkDx.Checked);
                roleNodeComponentProfile.i_Approved = !chkApproved.Enabled ? -1 : Convert.ToInt32(chkApproved.Checked);

                _nodeBL.UpdateRoleNodeComponentProfile(ref objOperationResult1,
                                                        roleNodeComponentProfile, 
                                                        ((ClientSession)Session["objClientSession"]).GetAsList());

                if (objOperationResult1.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult1.ExceptionMessage);
                    return;
                }

            }

            // Cerrar página actual y hacer postback en el padre para actualizar
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlComponent.SelectedIndex == 0)
                return;

            OperationResult objOperationResult1 = new OperationResult();
            string componentId = ddlComponent.SelectedValue;
            var data = new MedicalExamBL().GetMedicalExam(ref objOperationResult1, componentId);

            if (data != null)
            {
                chkDx.Enabled = Convert.ToBoolean(data.i_DiagnosableId);
                chkApproved.Enabled = Convert.ToBoolean(data.i_IsApprovedId); 
            }                

            if (objOperationResult1.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult1.ExceptionMessage);
                return;
            }

        }

    }
}