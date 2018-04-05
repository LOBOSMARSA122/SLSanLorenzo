using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM036A : System.Web.UI.Page
    {
        SystemParameterBL _oSystemParameterBL = new SystemParameterBL();
        ServiceBL _oServiceBL = new ServiceBL();
        private ProtocolBL _protocolBL = new ProtocolBL();
        private protocolDto _protocolDTO = null;
        private List<protocolcomponentDto> _protocolcomponentListDTO = null;
        private List<protocolcomponentDto> _protocolcomponentListDTODelete = null;
        private List<protocolcomponentDto> _protocolcomponentListDTOUpdate = null;
        //private List<ProtocolComponentList> _tmpProtocolcomponentList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                LoadCombos();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }    
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();

            if (Mode == "New")
            {
                Session["ListaCompletaComponentes"] = null;
                BindGridNew();
            }
            else if (Mode == "Edit")
            {
                string ProtocolId= null;
                string idOrgInter = "-1";

                if (Request.QueryString["v_ProtocolId"] != null)
                    ProtocolId = Request.QueryString["v_ProtocolId"].ToString();
                Session["ProtocolId"] = ProtocolId;
                _protocolDTO = _protocolBL.GetProtocol(ref objOperationResult, ProtocolId);

                // cabecera del protocolo
                txtProtocolName.Text = _protocolDTO.v_Name;
                cbEsoType.SelectedValue = _protocolDTO.i_EsoTypeId.ToString();
                cbOrganization.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_EmployerOrganizationId, _protocolDTO.v_EmployerLocationId);
       

                if (_protocolDTO.v_WorkingOrganizationId != "-1" && _protocolDTO.v_WorkingLocationId != "-1")
                {
                    idOrgInter = string.Format("{0}|{1}", _protocolDTO.v_WorkingOrganizationId, _protocolDTO.v_WorkingLocationId);
                }

                cbIntermediaryOrganization.SelectedValue = idOrgInter;
                cbOrganizationInvoice.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_CustomerOrganizationId, _protocolDTO.v_CustomerLocationId);

                LoadcbGESO();
                cbGeso.SelectedValue = _protocolDTO.v_GroupOccupationId;               
                cbServiceType.SelectedValue = _protocolDTO.i_MasterServiceTypeId.ToString();
                LoadcbServiceType();
                cbService.SelectedValue = _protocolDTO.i_MasterServiceId.ToString();
                txtCostCenter.Text = _protocolDTO.v_CostCenter;
           
                // Componentes del protocolo
                var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, ProtocolId);
                dataListPc.Sort((y, x) => x.v_CategoryName.CompareTo(y.v_CategoryName));
                //Obtener Lista Completa de Componentes

                var ListaCompletaComponentes = GetData(grdData.PageIndex, grdData.PageSize, "v_CategoryName,v_Name,AtSchool", "");
                ListaCompletaComponentes.Sort((y, x) => x.v_CategoryName.CompareTo(y.v_CategoryName));
             
               foreach (var Componentes in ListaCompletaComponentes)
               {
                   foreach (var ProtocoloComponente in dataListPc)
                   {
                       if (ProtocoloComponente.v_ComponentId == Componentes.v_ComponentId)
                       {
                           Componentes.AtSchool = true;
                           Componentes.r_Price = ProtocoloComponente.r_Price;
                           Componentes.Adicional = ProtocoloComponente.i_isAdditional == 0 ? false : true;
                           Componentes.Condicional = ProtocoloComponente.i_IsConditionalId == 0 ? false : true ;
                           Componentes.i_OperatorId = int.Parse(ProtocoloComponente.i_OperatorId.ToString());
                           Componentes.i_Age = ProtocoloComponente.i_Age;
                           Componentes.i_GenderId = int.Parse(ProtocoloComponente.i_GenderId.ToString());
                           //System.Web.UI.WebControls.DropDownList ddlGender = (System.Web.UI.WebControls.DropDownList)row.FindControl("ddlGender");
                           //Componentes.
                       }                       
                   }
               }
               // obligatorio para que los controles se dibujen en orden adecuado
             
               ListaCompletaComponentes.Sort((y,x) => x.AtSchool.CompareTo(y.AtSchool));

               Session["ListaCompletaComponentes"] = ListaCompletaComponentes;
               grdData.DataSource = ListaCompletaComponentes;
               grdData.DataBind();
            }
        }

        private void BindGridNew()
        {
            string strFilterExpression = "";
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_CategoryName,v_Name", strFilterExpression);
            grdData.DataBind();
        }

        private List<Sigesoft.Node.WinClient.BE.MedicalExamList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataList = _oServiceBL.GetMedicalExamPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);
            
            return dataList;
        }       
        
        private void LoadCombos()
        {

          
            // Llenado de combos
            // Tipos de eso
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", _oSystemParameterBL.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbEsoType, "Value1", "Id", _oSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.Select);

            // Lista de empresas por nodo
            int nodeId = 10;
            OperationResult objOperationResult1 = new OperationResult();
            var dataListOrganization = _oSystemParameterBL.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization1 = _oSystemParameterBL.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization2 = _oSystemParameterBL.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);

            Utils.LoadDropDownList(cbOrganization,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbIntermediaryOrganization,
               "Value1",
               "Id",
               dataListOrganization1,
               DropDownListAction.Select);

            Utils.LoadDropDownList(cbOrganizationInvoice,
              "Value1",
              "Id",
              dataListOrganization2,
              DropDownListAction.Select);

            //Llenado de los tipos de servicios [Emp/Part]
            Utils.LoadDropDownList(cbServiceType, "Value1", "Id", _oSystemParameterBL.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1, null), DropDownListAction.Select);
            // combo servicio
            Utils.LoadDropDownList(cbService, "Value1", "Id", _oSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, -1), DropDownListAction.Select);

        }

        //protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "CheckBox3")
        //    {
        //        CheckBoxField fieldCondicional = (CheckBoxField)grdData.FindColumn("CheckBoxField4");

        //        bool checkState = fieldCondicional.GetCheckedState(e.RowIndex);

        //        System.Web.UI.WebControls.DropDownList ddlOperador = (System.Web.UI.WebControls.DropDownList)grdData.Rows[e.RowIndex].FindControl("ddlOperador");
        //        if (checkState)
        //        {
        //            ddlOperador.Enabled = true;
        //            i_Age.Enabled = true;
        //            ddlGender.Enabled = true;
        //        }
        //        else
        //        {
        //            ddlOperador.Enabled = false;
        //            i_Age.Enabled = false;
        //            ddlGender.Enabled = false;
        //        }
        //    }
        //}

        protected void grdData_PageIndexChange(object sender, GridPageEventArgs e)
        {

        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            _protocolcomponentListDTO = new List<protocolcomponentDto>();

            _protocolDTO = new protocolDto();
            var id = cbOrganization.SelectedValue.ToString().Split('|');
            var id1 = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
            var id2 = cbIntermediaryOrganization.SelectedValue.ToString().Split('|');

            _protocolDTO.v_Name = txtProtocolName.Text;
            _protocolDTO.v_EmployerOrganizationId = id[0];
            _protocolDTO.v_EmployerLocationId = id[1];
            _protocolDTO.i_EsoTypeId = int.Parse(cbEsoType.SelectedValue.ToString());
            _protocolDTO.v_GroupOccupationId = cbGeso.SelectedValue.ToString();
            _protocolDTO.v_CustomerOrganizationId = id1[0];
            _protocolDTO.v_CustomerLocationId = id1[1];
            _protocolDTO.v_WorkingOrganizationId = id2[0];
            _protocolDTO.v_WorkingLocationId = cbIntermediaryOrganization.SelectedValue.ToString() != "-1" ? id2[1] : "-1";
            _protocolDTO.i_MasterServiceId = int.Parse(cbService.SelectedValue.ToString());
            _protocolDTO.v_CostCenter = txtCostCenter.Text;
            _protocolDTO.i_MasterServiceTypeId = int.Parse(cbServiceType.SelectedValue.ToString());
            _protocolDTO.i_HasVigency = 0;
            _protocolDTO.i_ValidInDays = 0;
            _protocolDTO.i_IsActive =1;
            _protocolDTO.v_NombreVendedor = "";

            if (Mode == "New")
            {
                if (IsExistsProtocolName())
                {
                    Alert.Show("Este protocolo ya existe");
                    return;
                }

                CheckBoxField field1 = (CheckBoxField)grdData.FindColumn("CheckBoxField2");

                for (int i = 0; i < grdData.Rows.Count; i++)
                {
                    if (field1.GetCheckedState(i) == true)
                    {
                        _protocolDTO.v_ProtocolId = null;
                        GridRow row = grdData.Rows[i];
                        System.Web.UI.WebControls.TextBox txtPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("r_Price");


                         protocolcomponentDto protocolComponent = new protocolcomponentDto();

                         protocolComponent.v_ComponentId = grdData.Rows[i].Values[5];
                         protocolComponent.r_Price = float.Parse(txtPrice.Text.ToString());// float.Parse(grdData.Rows[i].Values[4].ToString());
                         //protocolComponent.i_OperatorId = -1;
                         //protocolComponent.i_Age = 0;
                         //protocolComponent.i_GenderId = 3;
                         //protocolComponent.i_IsAdditional = 0;
                         //protocolComponent.i_IsConditionalId =0;
                         System.Web.UI.WebControls.TextBox txtEdad = (System.Web.UI.WebControls.TextBox)row.FindControl("i_Age");
                         CheckBoxField fieldAdicional = (CheckBoxField)grdData.FindColumn("CheckBoxField3");
                         bool ChecAdicional = fieldAdicional.GetCheckedState(i);
                         CheckBoxField fieldCondicional = (CheckBoxField)grdData.FindColumn("CheckBoxField4");
                         bool ChecCondicional = fieldCondicional.GetCheckedState(i);

                         protocolComponent.i_Age = int.Parse(txtEdad.Text.ToString());
                         protocolComponent.i_IsAdditional = ChecAdicional == true ? 1 : 0;
                         protocolComponent.i_IsConditionalId = ChecCondicional == true ? 1 : 0;

                         System.Web.UI.WebControls.DropDownList ddlOperador = (System.Web.UI.WebControls.DropDownList)grdData.Rows[i].FindControl("ddlOperador");
                         System.Web.UI.WebControls.DropDownList ddlGender = (System.Web.UI.WebControls.DropDownList)grdData.Rows[i].FindControl("ddlGender");




                         protocolComponent.i_GenderId = int.Parse(ddlGender.SelectedValue.ToString());
                         protocolComponent.i_OperatorId = int.Parse(ddlOperador.SelectedValue.ToString());

                         protocolComponent.i_IsConditionalIMC =0;
                         protocolComponent.r_Imc = 0;

                         _protocolcomponentListDTO.Add(protocolComponent);
                    }
                }

                _protocolBL.AddProtocol(ref objOperationResult, _protocolDTO, _protocolcomponentListDTO, ((ClientSession)Session["objClientSession"]).GetAsList());


            }
            else if (Mode == "Edit")
            {
                _protocolDTO.v_ProtocolId = Session["ProtocolId"].ToString();
                //Eliminar Fisicamente registros de protocolcomponent
                _protocolBL.EliminarProtocolComponentByProtocolId(ref objOperationResult, Session["ProtocolId"].ToString());

                //Grabar de nuevo la entidad
                CheckBoxField field1 = (CheckBoxField)grdData.FindColumn("CheckBoxField2");

                for (int i = 0; i < grdData.Rows.Count; i++)
                {
                    if (field1.GetCheckedState(i) == true)
                    {
                        protocolcomponentDto protocolComponent = new protocolcomponentDto();
                        GridRow row = grdData.Rows[i];

                        System.Web.UI.WebControls.TextBox txtPrice = (System.Web.UI.WebControls.TextBox)row.FindControl("r_Price");
                        System.Web.UI.WebControls.TextBox txtEdad = (System.Web.UI.WebControls.TextBox)row.FindControl("i_Age");
                        CheckBoxField fieldAdicional = (CheckBoxField)grdData.FindColumn("CheckBoxField3");
                        bool ChecAdicional = fieldAdicional.GetCheckedState(i);
                        CheckBoxField fieldCondicional = (CheckBoxField)grdData.FindColumn("CheckBoxField4");
                        bool ChecCondicional = fieldCondicional.GetCheckedState(i);

                        System.Web.UI.WebControls.DropDownList ddlOperador = (System.Web.UI.WebControls.DropDownList)grdData.Rows[i].FindControl("ddlOperador");
                        System.Web.UI.WebControls.DropDownList ddlGender = (System.Web.UI.WebControls.DropDownList)grdData.Rows[i].FindControl("ddlGender");

                        

                        protocolComponent.v_ComponentId = grdData.Rows[i].Values[5];
                        protocolComponent.r_Price = float.Parse(txtPrice.Text.ToString());
                        protocolComponent.i_Age = int.Parse(txtEdad.Text.ToString());
                        protocolComponent.i_IsAdditional = ChecAdicional == true ? 1:0;
                        protocolComponent.i_IsConditionalId = ChecCondicional == true ? 1 : 0;





                        protocolComponent.i_GenderId = int.Parse(ddlGender.SelectedValue.ToString());
                        protocolComponent.i_OperatorId = int.Parse(ddlOperador.SelectedValue.ToString());


                        protocolComponent.i_IsConditionalIMC = 0;
                        protocolComponent.r_Imc = 0;

                        _protocolcomponentListDTO.Add(protocolComponent);
                    }
                }

                _protocolBL.AddProtocol(ref objOperationResult, _protocolDTO, _protocolcomponentListDTO, ((ClientSession)Session["objClientSession"]).GetAsList());



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

        private bool IsExistsProtocolName()
        {
            // validar
            OperationResult objOperationResult = new OperationResult();
            return _protocolBL.IsExistsProtocolName(ref objOperationResult, txtProtocolName.Text);
        }

        private void LoadcbGESO()
        {
            var index = cbOrganizationInvoice.SelectedIndex;

            if (index == 0 || index == -1)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(cbGeso, "Value1", "Id", _oSystemParameterBL.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
                return;
            }

            var dataList = cbOrganization.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];
            string idLoc = dataList[1];

            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", _oSystemParameterBL.GetGESOByOrgIdAndLocationId(ref objOperationResult1, idOrg, idLoc), DropDownListAction.Select);
        }

        protected void cbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbServiceType();
           
        }

        private void LoadcbServiceType()
        {
            if (cbServiceType.SelectedIndex == 0 || cbServiceType.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbServiceType.SelectedValue.ToString());
            Utils.LoadDropDownList(cbService, "Value1", "Id", _oSystemParameterBL.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        protected void grdData_RowDataBound(object sender, GridRowEventArgs e)
        {
            System.Web.UI.WebControls.DropDownList ddlOperador = (System.Web.UI.WebControls.DropDownList)grdData.Rows[e.RowIndex].FindControl("ddlOperador");
            System.Web.UI.WebControls.DropDownList ddlGender = (System.Web.UI.WebControls.DropDownList)grdData.Rows[e.RowIndex].FindControl("ddlGender");

            Sigesoft.Node.WinClient.BE.MedicalExamList row = (Sigesoft.Node.WinClient.BE.MedicalExamList)e.DataItem;

            int Operador = Convert.ToInt32(row.i_OperatorId);
            ddlOperador.SelectedValue = Operador.ToString();

            int Gender = Convert.ToInt32(row.i_GenderId);
            ddlGender.SelectedValue = Gender.ToString();


            bool AtSchool = row.AtSchool;

            if (AtSchool)
            {
                highlightRows.Text += e.RowIndex.ToString() + ",";
            }

        }

        protected void cbOrganizationInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbOrganization.SelectedIndex = cbOrganizationInvoice.SelectedIndex;
            cbIntermediaryOrganization.SelectedIndex = cbOrganizationInvoice.SelectedIndex;
            LoadcbGESO();
        }

        protected void grdData_Sort(object sender, GridSortEventArgs e)
        {
            BindGridWithSort(e.SortField, e.SortDirection);
        }

        private void BindGridWithSort(string sortField, string sortDirection)
        {
            //if (Session["ListaCompletaComponentes"] == null) return;
            //var x = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable((List<Sigesoft.Node.WinClient.BE.MedicalExamList>)Session["ListaCompletaComponentes"]); 
            //DataTable table = x;

            //DataView view1 = table.DefaultView;
            //view1.Sort = String.Format("{0} {1}", sortField, sortDirection);


            //grdData.DataSource = view1;
            //grdData.DataBind();
        }

    }
}