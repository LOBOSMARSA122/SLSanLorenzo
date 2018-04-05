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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033 : System.Web.UI.Page
    {
        private Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _tmpTotalDiagnostic = null;
        ServiceBL _ServiceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["ConsultorioId"] = null;
                btnNewDiagnosticos.OnClientClick = WindowAddDX.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDX.GetShowReference("FRM033C.aspx?Mode=New");
                btnAddRecomendacion.OnClientClick = winEditReco.GetSaveStateReference(hfRefresh.ClientID) + winEditReco.GetShowReference("FRM033B.aspx?Mode=New");
                btnAddRestriccion.OnClientClick = winEditRestri.GetSaveStateReference(hfRefresh.ClientID) + winEditRestri.GetShowReference("FRM033E.aspx?Mode=New");
                btnNewDiagnosticosFrecuente.OnClientClick = WindowAddDXFrecuente.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDXFrecuente.GetShowReference("FRM033G.aspx?Mode=New");
                btnNewExamenes.OnClientClick = winEdit3.GetSaveStateReference(hfRefresh.ClientID) + winEdit3.GetShowReference("../ExternalUser/FRM031E_.aspx");
                btnNewFichaOcupacional.OnClientClick = winEdit2.GetSaveStateReference(hfRefresh.ClientID) + winEdit2.GetShowReference("../ExternalUser/FRM031C.aspx");

                Session["btnNewFichaOcupacional"] = false;
                Session["btnNewExamenes"] = false;
                LoadCombos();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");
           
            }
        }

        private List<MyListWeb> LlenarLista()
        {
            List<MyListWeb> lista = new List<MyListWeb>();
            int selectedCount = grdData.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {
                btnNewFichaOcupacional.Enabled = (bool)Session["btnNewFichaOcupacional"];
                btnNewExamenes.Enabled = (bool)Session["btnNewExamenes"];

                if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 31)
                {
                    if (selectedCount > 1)
                    {
                        btnNewFichaOcupacional.Enabled = false;
                        btnNewExamenes.Enabled = false;
                    }
                    else
                    {
                        btnNewFichaOcupacional.Enabled = true;
                        btnNewExamenes.Enabled = true;
                        Session["btnNewFichaOcupacional"] = true;
                        Session["btnNewExamenes"] = true;
                    }
                   
                }

               else  if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 31)
                {
                    if (selectedCount > 1)
                    {
                        btnNewFichaOcupacional.Enabled = false;
                        btnNewExamenes.Enabled = false;
                    }
                    else
                    {
                        btnNewFichaOcupacional.Enabled = true;
                        btnNewExamenes.Enabled = true;
                        Session["btnNewFichaOcupacional"] = true;
                        Session["btnNewExamenes"] = true;
                    }

                }

            }
            else
            {
                btnNewFichaOcupacional.Enabled = false;
                btnNewExamenes.Enabled = false;
            }

            for (int i = 0; i < selectedCount; i++)
            {
                int rowIndex = grdData.SelectedRowIndexArray[i];

                var dataKeys = grdData.DataKeys[rowIndex];
                //for (int j = 0; j < dataKeys.Length; j++)
                //{
                //lista.Add( new MyListWeb< [0].ToString());
                lista.Add(new MyListWeb
                {
                    IdServicio = dataKeys[0].ToString(),
                    IdPaciente = dataKeys[1].ToString(),
                    EmpresaCliente = dataKeys[2].ToString(),
                });

                //}

            }

            Session["objLista"] = lista;

            return lista;
        }

        protected void winEditReco_Close(object sender, EventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            //grdDx.DataSource = _ServiceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, Session["ServiceId"].ToString());
            //grdDx.DataBind();
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
        }

        protected void WindowAddDXFrecuente_Close(object sender, EventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
        }

        protected void WindowAddDX_Close(object sender, EventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
        }

        protected void WindowAddReco_Close(object sender, EventArgs e)
        {

        }

        protected void WindowAddRestri_Close(object sender, EventArgs e)
        {

        }

        protected void Window1_Close(object sender, EventArgs e)
        {

        }        

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();
            OrganizationBL oOrganizationBL = new OrganizationBL();
            ServiceBL oServiceBL = new ServiceBL();

            Utils.LoadDropDownList(ddlEmpresaCliente, "Value1", "Id", oOrganizationBL.GetAllOrganizations(ref objOperationResult), DropDownListAction.All);

            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == (int)TipoProfesional.Evaluador)
            {
                //Llenar combo consultorio 
                int Nodo = 9;
                int RolId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.ToString());

                // Obtener permisos de cada examen de un rol especifico
                var componentProfile = oServiceBL.GetRoleNodeComponentProfileByRoleNodeId(Nodo, RolId);
              
                var _componentListTemp = oServiceBL.GetAllComponents(ref objOperationResult);

                Session["componentListTemp"] = _componentListTemp;
                var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

                List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

                groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
                // Remover los componentes que no estan asignados al rol del usuario
                var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));


                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", results, DropDownListAction.Select);
                ddlConsultorio.SelectedIndex = 1;
                Session["CategoriaId"] = ddlConsultorio.SelectedValue.ToString();
                MostrarOcultarBotonesGrilla(ddlConsultorio.SelectedValue.ToString());
            
            }
            else if (((ClientSession)Session["objClientSession"]).i_ProfesionId == (int)TipoProfesional.Auditor)
            {
                //Llenar combo consultorio 
                int Nodo = 9;
                int RolId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.ToString());

                // Obtener permisos de cada examen de un rol especifico
                var componentProfile = oServiceBL.GetRoleNodeComponentProfileByRoleNodeId(Nodo, RolId);

                var _componentListTemp = oServiceBL.GetAllComponents(ref objOperationResult);

                Session["componentListTemp"] = _componentListTemp;
                var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

                List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

                groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
                // Remover los componentes que no estan asignados al rol del usuario
                var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));

                //ddlConsultorio.Enabled = false;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", results, DropDownListAction.All);
            }
          
   

        }

        protected void grdDx_RowClick(object sender, GridRowClickEventArgs e)
        {
          
            int index = e.RowIndex;
            Session["indexgrdDx"] = index;
            var dataKeys = grdDx.DataKeys[index];
            Session["DiagnosticRepositoryId"] = dataKeys[0].ToString();
            Session["ComponentId"] = dataKeys[1].ToString();
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
        }

        protected void ActualizaGrillasRecoYRestri(string DiagnosticRepositoryId)
        {
            OperationResult objOperationResult = new OperationResult();

            _tmpTotalDiagnostic = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
            _tmpTotalDiagnostic = _ServiceBL.GetServiceComponentTotalDiagnostics_(ref objOperationResult, DiagnosticRepositoryId);

            if (_tmpTotalDiagnostic == null)
            {
                 _tmpTotalDiagnostic = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
            }
            //txtDx.Text = _tmpTotalDiagnostic.v_DiseasesName;

            grdRecomendaciones.DataSource = _tmpTotalDiagnostic.Recomendations;
            grdRecomendaciones.DataBind();

            grdRestricciones.DataSource = _tmpTotalDiagnostic.Restrictions;
            grdRestricciones.DataBind();
        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {
         
            int index = e.RowIndex;
            var dataKeys = grdData.DataKeys[index];
            Session["ServiceId"] = dataKeys[0].ToString();
            Session["PersonId"] = dataKeys[1].ToString();

            lblEdad.Text = "Edad: " + GetAge(DateTime.Parse(dataKeys[3].ToString())).ToString();
            lblGenero.Text = "Género: " + dataKeys[4].ToString();
            lblTipoEso.Text = "Tipo ESO: " + dataKeys[5].ToString();
            lblGrupoRiesgo.Text = "Grupo Riesgo: " + dataKeys[6].ToString() + " - "  +"Puesto Postula: " + dataKeys[7].ToString();
            //lblPuesto.Text = "Puesto Postula: " + dataKeys[7].ToString();

            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasRecoYRestri(null);
            LlenarLista();
        }

        public int GetAge(DateTime FechaNacimiento)
        {
            return int.Parse((DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1).ToString());

        }

        protected void ActualizaGrillasDx(string ServiceId,string PersonId)
        {
          
            OperationResult objOperationResult = new OperationResult();
            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 30)
            {
                List<string> ListaComponentes =  (List<string>)Session["ListaComponentes"];
                grdDx.DataSource = _ServiceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, ServiceId).FindAll(p => ListaComponentes.Contains(p.v_ComponentId));
                grdDx.DataBind();

            }
            else
            {
                grdDx.DataSource = _ServiceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, ServiceId);
                grdDx.DataBind();
            }
          

            //Llenar Grilla Antecedente
            grdAntecedentes.DataSource = _ServiceBL.GetAntecedentConsolidateForService(ref objOperationResult, PersonId);
            grdAntecedentes.DataBind();
            if (Session["indexgrdDx"] != null)
            {
                grdDx.SelectedRowIndex = int.Parse(Session["indexgrdDx"].ToString());
            }
            
        }        

        protected void btnFilter_Click(object sender, EventArgs e)
        {

            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 30)//Evaluador
            {
                if (ddlConsultorio.SelectedValue.ToString() == "-1")
                {
                     Alert.ShowInTop("Debe seleccionar un  consultorio");
                     return;
                }
            }
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlEmpresaCliente.SelectedValue.ToString() != "-1") Filters.Add("v_CustomerOrganizationId==\"" + ddlEmpresaCliente.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtTrabajador.Text)) Filters.Add("v_Pacient.Contains(\"" + txtTrabajador.Text.ToUpper().Trim() + "\")");
            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 31)//Evaluador
            {
                if (ddlConsultorio.SelectedValue.ToString() != "-1") Filters.Add("i_CategoryId==" + ddlConsultorio.SelectedValue);
            }
         

            string strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            // Save the Filter expression in the Session
            Session["strFilterExpressionBaneja"] = strFilterExpression;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString( Session["strFilterExpressionBaneja"]);
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            grdData.DataBind();
        }

        private List<ServiceList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();

            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 30)
            {
                grdData.Columns[0].Hidden = true;
                var _objData = _ServiceBL.GetServicesForAuditoriaEvaluador(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1), (List<string>)Session["ListaComponentes"]);

                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                }

                return _objData;
            }
            else
            {
                grdData.Columns[2].Hidden = true;
                grdData.Columns[3].Hidden = true;
                grdData.Columns[4].Hidden = true;
                grdData.Columns[5].Hidden = true;
               
                var _objData = _ServiceBL.GetServicesForAuditoriaAuditor(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1), (List<string>)Session["ListaComponentes"]);

                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                }

                return _objData;
            }
          
        }

        protected void grdRecomendaciones_RowCommand(object sender, GridCommandEventArgs e)
        {
            int index = e.RowIndex;
            var dataKeys = grdRecomendaciones.DataKeys[index];
            Session["ServiceId"] = dataKeys[1].ToString();
            if (e.CommandName == "DeleteAction")
            {
                DeleteItem(dataKeys[0].ToString());
                ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
            }
        }

        protected void grdRestricciones_RowCommand(object sender, GridCommandEventArgs e)
        {
            int index = e.RowIndex;
            var dataKeys = grdRestricciones.DataKeys[index];
            Session["ServiceId"] = dataKeys[1].ToString();
            if (e.CommandName == "DeleteAction")
            {
                DeleteItemRestriccion(dataKeys[0].ToString());
                ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
            }
        }

        private void DeleteItem(string pRecomenedationId)
        {          
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _ServiceBL.EliminarRecomendacion(ref objOperationResult, pRecomenedationId);
        }

        private void DeleteItemRestriccion(string pRestriccionId)
        {
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _ServiceBL.EliminarRestriccion(ref objOperationResult, pRestriccionId);
        }
    
        protected void winEdit3_Close(object sender, EventArgs e)
        {
            if (Session["EliminarArchivo"] != null)
            {
                File.Delete(Session["EliminarArchivo"].ToString());
            }

        }

        protected void winEditRestri_Close1(object sender, WindowCloseEventArgs e)
        {

            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
        }

        protected void ddlConsultorio_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == (int)TipoProfesional.Evaluador)
            {
                MostrarOcultarBotonesGrilla(ddlConsultorio.SelectedItem.Value.ToString());
            }
            else if (((ClientSession)Session["objClientSession"]).i_ProfesionId == (int)TipoProfesional.Auditor)
            {
                Session["CategoriaId"] = ddlConsultorio.SelectedItem.Value.ToString();
            }
         
          
        }

        void  MostrarOcultarBotonesGrilla( string pstrConsultorioValue)
        {
            Session["ConsultorioId"] = pstrConsultorioValue;
            List<KeyValueDTO> Lista = (List<KeyValueDTO>)Session["componentListTemp"];
            if (pstrConsultorioValue != "-1")
            {
                var Compornentes = Lista.FindAll(p => p.Value4 == Single.Parse(pstrConsultorioValue));

                List<string> ListaComponentes = new List<string>();

                foreach (var item in Compornentes)
                {
                    ListaComponentes.Add(item.Value2);
                }

                if (ListaComponentes.Find(p => p == Constants.ELECTROCARDIOGRAMA_ID) != null)
                {
                    grdData.Columns[3].Hidden = false;
                    grdData.Columns[4].Hidden = true;
                    grdData.Columns[5].Hidden = true;
                }

                else if (ListaComponentes.Find(p => p == Constants.RX_TORAX_ID) != null || ListaComponentes.Find(p => p == Constants.OIT_ID) != null)
                {
                    grdData.Columns[3].Hidden = true;
                    grdData.Columns[4].Hidden = false;
                    grdData.Columns[5].Hidden = false;
                }

                Session["ListaComponentes"] = ListaComponentes;
            }
           
          
        }

        protected void winreporte_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void winEdit2_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void EditarCardio_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void EditarRXTorax_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void EditarRXToraxOIT_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void winSubirArchivo_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
            Session["ServiceId"] = "";
            Session["PersonId"] = "";
            Session["DiagnosticRepositoryId"] = "";
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
        }

        protected void grdDx_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                DeleteItem();
                ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            }
        }

        private void DeleteItem()
        {
            // Obtener los IDs de la fila seleccionada
            string DiagnosticRepositoryId = grdDx.DataKeys[grdDx.SelectedRowIndex][0].ToString();
          
          
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _ServiceBL.EliminarDxByDiagnosticRepositoryId(DiagnosticRepositoryId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }
    }
}