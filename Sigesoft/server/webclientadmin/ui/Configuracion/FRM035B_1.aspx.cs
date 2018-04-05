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

namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM035B_1 : System.Web.UI.Page
    {
        OrganizationBL oOrganizationBL = new OrganizationBL();
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
            if (Request.QueryString["v_LocationId"] != null)
                Session["v_LocationId"] = Request.QueryString["v_LocationId"].ToString();

            if (Request.QueryString["v_OrganizationId"] != null)
                Session["v_OrganizationId"] = Request.QueryString["v_OrganizationId"].ToString();


            if (Request.QueryString["v_Name"] != null)
                txtSede.Text = Request.QueryString["v_Name"].ToString();
        }


        private List<nodeorganizationlocationwarehouseprofileDto> InsertWarehouse(string pstrLocationId)
        {
            OperationResult objOperationResult = new OperationResult();
            List<nodeorganizationlocationwarehouseprofileDto> objwarehouseListAdd = new List<nodeorganizationlocationwarehouseprofileDto>();

            string pstrFilterExpression = "v_LocationId ==" + "\"" + pstrLocationId + "\"";
            var _objData = oOrganizationBL.GetWarehousePagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);

            // Datos de Almacén
            foreach (var item in _objData)
            {
                // Datos de Almacen
                nodeorganizationlocationwarehouseprofileDto objWarehouse = new nodeorganizationlocationwarehouseprofileDto();
                objWarehouse.i_NodeId = 9;
                objWarehouse.v_OrganizationId = item.v_OrganizationId;
                objWarehouse.v_LocationId = item.v_LocationId;
                objWarehouse.v_WarehouseId = item.v_WarehouseId;

                objwarehouseListAdd.Add(objWarehouse);

            }

            return objwarehouseListAdd.Count == 0 ? null : objwarehouseListAdd;
        }


        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            var locationId = "";
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                locationDto objEntity = new locationDto();

                // Populate the entity
                objEntity.v_OrganizationId = Session["v_OrganizationId"].ToString();
                objEntity.v_Name = txtSede.Text;
              
                    // Save the data                  
               locationId=   oOrganizationBL.AddLocation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

               //Rutina para Asignar la Empresa creada automaticamente al nodo actual
               NodeOrganizationLoactionWarehouseList objNodeOrganizationLoactionWarehouseList = new NodeOrganizationLoactionWarehouseList();
               List<nodeorganizationlocationwarehouseprofileDto> objnodeorganizationlocationwarehouseprofileDto = new List<nodeorganizationlocationwarehouseprofileDto>();

               //Llenar Entidad Empresa/sede
               objNodeOrganizationLoactionWarehouseList.i_NodeId = 9;
               objNodeOrganizationLoactionWarehouseList.v_OrganizationId = Session["v_OrganizationId"].ToString();
               objNodeOrganizationLoactionWarehouseList.v_LocationId = locationId;

               //Llenar Entidad Almacén
               var objInsertWarehouseList = InsertWarehouse(locationId);


               oOrganizationBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult, objNodeOrganizationLoactionWarehouseList, objInsertWarehouseList, ((ClientSession)Session["objClientSession"]).GetAsList());


            }
            else if (Mode == "Edit")
            {
                locationDto objEntity = new locationDto();

                // Populate the entity
                objEntity.v_OrganizationId = Session["v_OrganizationId"].ToString();
                objEntity.v_LocationId = Session["v_LocationId"].ToString();
                objEntity.v_Name = txtSede.Text;
                oOrganizationBL.UpdateLocation(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
            }


            

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                // Cerrar página actual y hacer postback en el padre para actualizar
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
        }
    }
}