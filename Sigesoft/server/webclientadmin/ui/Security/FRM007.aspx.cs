using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class FRM007 : System.Web.UI.Page
    {
        ApplicationHierarchyBL _objBL = new ApplicationHierarchyBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM007A.aspx?Mode=New");
                BindGrid();
            }           
        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            grdData.DataSource = GetData( "v_Description ASC");
            grdData.DataBind();
        }

        private List<DtvForGrwAppHierarchy> GetData(string pstrSortExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            //Llenar _objData para Grilla Treeview

            List<DtvForGrwAppHierarchy> _objData = _objBL.GetApplicationHierarchyForGridView(ref objOperationResult).ToList();

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                DeleteItem();
                BindGrid();

            }
            if (e.CommandName == "ClonAction")
            {
                ClonAction();
                BindGrid();
            }
        }

        private void DeleteItem()
        {
            // Obtener los IDs de la fila seleccionada
            int _i_ApplicationHierarchyId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);
                       
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _objBL.DeleteApplicationHierarchy(ref objOperationResult, _i_ApplicationHierarchyId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }

        private void ClonAction()
        {
            applicationhierarchyDto objEntity = new applicationhierarchyDto();
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada
            int _i_ApplicationHierarchyId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);
            objEntity = _objBL.GetApplicationHierarchy(ref objOperationResult, _i_ApplicationHierarchyId);
            objEntity.v_Description = objEntity.v_Description + "_Copia";
            // Save the data                  
            _objBL.AddApplicationHierarchy(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
        }
    }
}