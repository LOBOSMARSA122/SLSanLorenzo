using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FineUI;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRM005F : System.Web.UI.Page
    {
        SystemParameterBL _systemParameterBL = new SystemParameterBL();

        #region Declarations

        SecurityBL _objSecurityBL = new SecurityBL();

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

        //public int SystemUserId
        //{
        //    get
        //    {
        //        if (Request.QueryString["systemUserId"] != null)
        //        {
        //            string systemUserId = Request.QueryString["systemUserId"].ToString();
        //            if (!string.IsNullOrEmpty(systemUserId))
        //            {
        //                return Convert.ToInt32(systemUserId);
        //            }
        //        }

        //        return -1;
        //    }
        //}

        public string NodeName
        {
            get
            {
                if (Request.QueryString["nodeName"] != null)
                {
                    string nodeName = Request.QueryString["nodeName"].ToString();
                    if (!string.IsNullOrEmpty(nodeName))
                    {
                        return nodeName;
                    }
                }

                return string.Empty;
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
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult1 = new OperationResult();

            txtNode.Text = NodeName;

            //Llenar combo ItemParameter Tree
            ddlMasterService.DataTextField = "Description";
            ddlMasterService.DataValueField = "Id";
            ddlMasterService.DataSimulateTreeLevelField = "Level";
            ddlMasterService.DataEnableSelectField = "EnabledSelect";
            List<DataForTreeViewSP> t = _systemParameterBL.GetSystemParameterForComboTreeView(ref objOperationResult1, 119).ToList();
            ddlMasterService.DataSource = t;
            ddlMasterService.DataBind();
          
            ddlMasterService.Items.Insert(0, new FineUI.ListItem(Constants.Select, Constants.SelectValue));        

            if (Mode == "New")
            {

            }
            else if (Mode == "Edit")
            {
                // cargar grid 
                Loadgrd();
            }

        }

        protected void grd_RowCommand(object sender, GridCommandEventArgs e)
        {
            // Eliminar 
            if (e.CommandName == "DeleteAction")
            {
                // Obtener los IDs de la fila seleccionada
                int nodeId = Convert.ToInt32(grd.DataKeys[e.RowIndex][0]);
                int masterServiceId =  Convert.ToInt32(grd.DataKeys[e.RowIndex][1]);
               
                // Borrar permisos de contexto asociados al usuario actual
                OperationResult objOperationResult = new OperationResult();
                _objSecurityBL.DeleteNodeServiceProfile(ref objOperationResult,
                                                                nodeId,
                                                                masterServiceId,
                                                                ((ClientSession)Session["objClientSession"]).GetAsList());
                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                    return;
                }
               
                Loadgrd();
            }
        }

        protected void grd_PageIndexChange(object sender, GridPageEventArgs e)
        {
            grd.PageIndex = e.NewPageIndex;
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {

        }

        private void Loadgrd()
        {
            OperationResult objOperationResult = new OperationResult();
            var objEntity = _objSecurityBL.GetNodeServiceProfileForGridView(ref objOperationResult, NodeId);

            if (objEntity.Count > 0)
            {
                grd.DataSource = objEntity;
                grd.DataBind();
            }

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                return;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // grabar
            OperationResult objOperationResult = new OperationResult();
            
            var objEntityDTO = new nodeserviceprofileDto();

            objEntityDTO.i_ServiceTypeId = null;
            objEntityDTO.i_MasterServiceId = int.Parse(ddlMasterService.SelectedValue);
            objEntityDTO.i_NodeId = NodeId;

            _objSecurityBL.AddNodeServiceProfile(ref objOperationResult,
                                                        objEntityDTO,
                                                        ((ClientSession)Session["objClientSession"]).GetAsList());

            if (objOperationResult.ErrorMessage != null)
            {
                Alert.ShowInTop(string.Format("<font color='red'> {0} </font> ya se encuentra registrado. Por favor elija otro.", ddlMasterService.SelectedText));
                return;
            }

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                return;
            }
           

            // cargar grid 
            Loadgrd();
        }
    }
}