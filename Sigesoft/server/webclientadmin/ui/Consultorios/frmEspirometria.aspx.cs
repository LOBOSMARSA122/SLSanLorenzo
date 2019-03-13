using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FineUI;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.UI.ExternalUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmEspirometria : System.Web.UI.Page
    {
        string _ruta;
        DataSet dsGetRepo = null;
        ReportDocument rp;
        DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
        List<string> _filesNameToMerge = new List<string>();

        ServiceBL _serviceBL = new ServiceBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = new List<Node.WinClient.BE.ServiceComponentFieldsList>();
        private Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _tmpTotalDiagnostic = null;
      
        #region Cargar Inicial

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.ToString()) == 30)
                {
                    AccordionPane2.Visible = false;
                }
                else
                {
                    AccordionPane2.Visible = true;
                }
                btnNewDiagnosticos.OnClientClick = WindowAddDX.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDX.GetShowReference("../Auditar/FRM033C.aspx?Mode=New");
                btnNewDiagnosticosFrecuente.OnClientClick = WindowAddDXFrecuente.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDXFrecuente.GetShowReference("../Auditar/FRM033G.aspx?Mode=New");
                btnReporteEspiro.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Espiro");
                btnCertificadoAptitud.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Certificado");
               
                btnDescargar.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("DescargarAdjunto.aspx?Consultorio=ESPIRO");

                int RoleId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString());
                var ComponentesPermisoLectura = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId(9, RoleId).FindAll(p => p.i_Read == 1);
                List<string> ListaComponentesPermisoLectura = new List<string>();
                foreach (var item in ComponentesPermisoLectura)
                {
                    ListaComponentesPermisoLectura.Add(item.v_ComponentId);
                }
                Session["ComponentesPermisoLectura"] = ListaComponentesPermisoLectura;
                TabEspirometria.Hidden = true;

                TabEspirometria.Attributes.Add("Tag", "N002-ME000000031");

                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1);  //  DateTime.Parse("12/11/2016");
                dpFechaFin.SelectedDate = DateTime.Now; //  DateTime.Parse("12/11/2016"); 
                LoadCombos();

                chkPre1.Attributes.Add("Tag", "N002-MF000000199");
                chkPre2.Attributes.Add("Tag", "N002-MF000000200");
                chkPre3.Attributes.Add("Tag", "N002-MF000000201");
                chkPre4.Attributes.Add("Tag", "N002-MF000000202");
                chkPre5.Attributes.Add("Tag", "N002-MF000000203");
                chkhemoptisis.Attributes.Add("Tag","N009-MF000000112");
                chkinfarto_reciente.Attributes.Add("Tag","N009-MF000000116");
                chkpneumotorax.Attributes.Add("Tag", "N009-MF000000113");
                chkinestabilidad_cv.Attributes.Add("Tag", "N009-MF000000574");
                chktraqueostomia.Attributes.Add("Tag", "N009-MF000000114");
                chkfiebre_nausea.Attributes.Add("Tag", "N009-MF000000117");
                chksonda_pleural.Attributes.Add("Tag", "N009-MF000000115");
                chkembarazo_avanzado.Attributes.Add("Tag", "N009-MF000000120");
                chkaneurisma_cerebral.Attributes.Add("Tag", "N009-MF000000118");
                chkembarazo_complicado.Attributes.Add("Tag", "N009-MF000000121");
                chkembolia_pulmonar.Attributes.Add("Tag", "N009-MF000000119");
                chk1_tuvo_una_infeccion.Attributes.Add("Tag", "N002-MF000000207");
                chk2_tuvo_infeccion.Attributes.Add("Tag", "N002-MF000000284");
                chk3_uso_aerosoles.Attributes.Add("Tag", "N002-MF000000204");
                chk4_ha_usado_algun_medicamento.Attributes.Add("Tag", "N002-MF000000206");
                chk5_ha_fumado_cualquier.Attributes.Add("Tag", "N002-MF000000208");
                chk6_realizo_algun_ejercicio.Attributes.Add("Tag", "N002-MF000000205");
                chk7_ingirio_alimentos.Attributes.Add("Tag", "N002-MF000000285");

                //ddlorigen_etnico.Attributes.Add("Tag","N009-MF000000622");
                //ddltabaquismo.Attributes.Add("Tag","N009-MF000000623");
                //txttiempo_de_trabajo.Attributes.Add("Tag","N009-MF000001038");
                txtcvf.Attributes.Add("Tag","N002-MF000000286");
                txtdescripcion_cvf.Attributes.Add("Tag","N009-MF000000578");
                txtvef_1.Attributes.Add("Tag","N002-MF000000287");
                txtdescripcion_vef_1.Attributes.Add("Tag","N009-MF000000579");
                txtvef1_cvf.Attributes.Add("Tag","N002-MF000000169");
                txtdescripcion_vef1_cvf.Attributes.Add("Tag","N009-MF000000580");
                txtfet.Attributes.Add("Tag","N002-MF000000170");
                txtdescripcion_fet.Attributes.Add("Tag","N009-MF000000581");
                txtfef_2575.Attributes.Add("Tag","N002-MF000000171");
                txtdescripcion_f_2575.Attributes.Add("Tag","N009-MF000000582");
                txtpef.Attributes.Add("Tag","N002-MF000000168");
                txtdescripcion_pef.Attributes.Add("Tag","N009-MF000000583");
                txtmef.Attributes.Add("Tag","N009-MF000000575");
                txtdescripcion_mef.Attributes.Add("Tag","N009-MF000000584");
                txtr_50.Attributes.Add("Tag","N009-MF000000576");
                txtdescripcion_r_50.Attributes.Add("Tag","N009-MF000000585");
                txtmvv_ind.Attributes.Add("Tag","N009-MF000000577");
                txtdescripcion_mvvind.Attributes.Add("Tag","N009-MF000000586");
                //txtedad_pulmorar_estimada.Attributes.Add("Tag","N009-MF000000600");
                chkespirometria_normal.Attributes.Add("Tag","N009-MF000002131");
                chkpatron_obstructivo.Attributes.Add("Tag","N009-MF000002154");
                chkpatron_restrictivo.Attributes.Add("Tag","N009-MF000002155");


                int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());
                OperationResult objOperationResult = new OperationResult();
                SystemParameterBL oSystemParameterBL = new SystemParameterBL();
                Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

                if (ProfesionId != (int)TipoProfesional.Auditor)
                {
                    btnGrabarAptitud.Enabled = false;
                    ddlUsuarioGrabar.Enabled = false;
                    ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                }
                else
                {
                    ddlUsuarioGrabar.Enabled = false;
                    ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                }



            }
        }

        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();
            int NodoWin = int.Parse(WebConfigurationManager.AppSettings["NodoWin"].ToString());
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(NodoWin, int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString()));
            var _componentListTemp = _serviceBL.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
            // Remover los componentes que no estan asignados al rol del usuario
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));


            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", results, DropDownListAction.Select);
            ddlConsultorio.SelectedValue = "16";
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlFirmaAuditor, "Value1", "Id", oSystemParameterBL.GetProfessionalAuditores(ref objOperationResult, ""), DropDownListAction.Select);
        }

        #endregion

        #region Bandeja Consultorio

        protected void grdData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "LlamarPaciente")
            {
                int index = e.RowIndex;
                var dataKeys = grdData.DataKeys[index];
                servicecomponentDto objservicecomponentDto = null;
                foreach (var item in _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(dataKeys[10].ToString()), Session["ServiceId"].ToString()))
                {
                    objservicecomponentDto = new servicecomponentDto();
                    objservicecomponentDto.v_ServiceComponentId = item.ToString();
                    objservicecomponentDto.i_QueueStatusId = 2;// (int)Common.QueueStatusId.LLAMANDO;
                    _serviceBL.UpdateServiceComponentOfficeLlamando(objservicecomponentDto);
                }
            }
        }

        protected void grdData_RowDataBound(object sender, FineUI.GridRowEventArgs e)
        {

        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            TabEspirometria.Hidden = true;
            int index = e.RowIndex;
            var dataKeys = grdData.DataKeys[index];
            Session["ServiceId"] = dataKeys[0] == null ? "" : dataKeys[0].ToString();
            Session["PersonId"] = dataKeys[1] == null ? "" : dataKeys[1].ToString();
            Session["i_AptitudeStatusId"] = dataKeys[3] == null ? "" : dataKeys[3].ToString();
            Session["i_EsoTypeId"] = dataKeys[4] == null ? "" : dataKeys[4].ToString();

            Session["v_ExploitedMineral"] = dataKeys[5] == null ? "" : dataKeys[5].ToString();
            Session["i_AltitudeWorkId"] = dataKeys[6] == null ? "" : dataKeys[6].ToString();
            Session["i_PlaceWorkId"] = dataKeys[7] == null ? "" : dataKeys[7].ToString();

            Session["d_ServiceDate"] = dataKeys[10] == null ? "" : dataKeys[10].ToString();


            txtEmpresaClienteCabecera.Text = dataKeys[14] == null ? "" : dataKeys[14].ToString();
            txtActividadEmpresaClienteCabecera.Text = dataKeys[16] == null ? "" : dataKeys[16].ToString();
            txtTrabajadorCabecera.Text = dataKeys[8] == null ? "" : dataKeys[8].ToString();
            txtDNICabecera.Text = dataKeys[9] == null ? "" : dataKeys[9].ToString();
            txtFechaCabecera.Text = dataKeys[10] == null ? "" : ((DateTime)dataKeys[10]).ToString("dd/MM/yyyy");
            txtTipoExamenCabecera.Text = dataKeys[11] == null ? "" : dataKeys[11].ToString();
            txtGeneroCabecera.Text = dataKeys[2] == null ? "" : dataKeys[2].ToString();
            txtPuestoCabecera.Text = dataKeys[12] == null ? "" : dataKeys[12].ToString();
            Session["DniTrabajador"] = txtDNICabecera.Text;
            Session["FechaServicio"] = dataKeys[10] == null ? "" : ((DateTime)dataKeys[10]).ToString("ddMMyyyy");
            DateTime? FechaNacimiento = DateTime.Parse(dataKeys[13].ToString());
            txtEdadCabecera.Text = dataKeys[13] == null ? "" : new ServiceBL().GetAge(FechaNacimiento.Value).ToString();// dataKeys[13].ToString();
            ddlAptitud.SelectedValue = dataKeys[3] == null ? "-1" : dataKeys[3].ToString();
            txtComentarioAptitud.Text = dataKeys[14] == null ? "" : dataKeys[14].ToString();
            //Mostrar
            grdComponentes.DataSource = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(ddlConsultorio.SelectedValue.ToString()), Session["ServiceId"].ToString());
            grdComponentes.DataBind();
            //Pintar los examenes correpondientes por servicio
            int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());
            if (ProfesionId == (int)TipoProfesional.Auditor)
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());
                foreach (var item in ListaComponentes)
                {
                    if (item.ComponentId == TabEspirometria.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosEspirometria();
                        ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                        ddlUsuarioGrabar.Enabled = false;
                        ObtenerDatosEspirometria(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabEspirometria.Hidden = false;
                    }
                }
            }
            else
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());

                var ListaComponenentesConPermiso = (List<string>)Session["ComponentesPermisoLectura"];

                foreach (var item in ListaComponenentesConPermiso)
                {
                    if (item == TabEspirometria.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.ESPIROMETRIA_ID);
                        if (Resultado != null)
                        {
                            LoadCombosEspirometria();
                            ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                            ddlUsuarioGrabar.Enabled = false;
                            ObtenerDatosEspirometria(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabEspirometria.Hidden = false;
                        }
                    }
                }
            }
            if (grdData.Rows.Count > 0)
            {
                Accordion1.Enabled = true;
            }
            else
            {
                Accordion1.Enabled = false;
            }
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            

        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlConsultorio.SelectedValue.ToString() != "-1") Filters.Add("i_CategoryId==" + ddlConsultorio.SelectedValue);
            if (!string.IsNullOrEmpty(txtTrabajador.Text)) Filters.Add("v_Pacient.Contains(\"" + txtTrabajador.Text.ToUpper().Trim() + "\")");

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
            Session["strFilterExpression"] = strFilterExpression;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            grdData.DataBind();
        }

        private List<ServiceList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();

            //var _objData = _serviceBL.GetAllServices_(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));
            List<string> ComponentesId = new List<string>();
            ComponentesId.Add("N002-ME000000031");
            var _objData = _serviceBL.GetAllServices_Consultorio(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1), ComponentesId.ToArray());
          
            if (_objData.Count == 0)
            {
                TabEspirometria.Hidden = true;
            }
            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            #region ESPECIALISTA
            if (int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.ToString()) == 30)
            {
                _objData = _objData.FindAll(p => p.i_SystemUserEspecialistaId == int.Parse(((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString()));
            }
            #endregion          

            return _objData;
        }

        protected void grdDx_RowDataBound(object sender, GridRowEventArgs e)
        {
            DiagnosticRepositoryList row = (DiagnosticRepositoryList)e.DataItem;
            if (row.i_FinalQualificationId == 2)
            {
                highlightRows.Text += e.RowIndex.ToString() + ",";
            }
        }
        #endregion

        #region Métodos Globales

        private void SearchControlAndLoadData(Control ctrlContainer, string ServiceComponentId, List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> ListaValores)
        {
            
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is DropDownList)
                {
                    if (((DropDownList)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((DropDownList)ctrl).Attributes.GetValue("Tag").ToString();
                        ((DropDownList)ctrl).SelectedValue = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;
                    }
                }

                if (ctrl is TextArea)
                {
                    if (((TextArea)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((TextArea)ctrl).Attributes.GetValue("Tag").ToString();
                        ((TextArea)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

                    }
                }

                if (ctrl is TextBox)
                {
                    if (((TextBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((TextBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((TextBox)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

                    }
                }
                //("Tag", "N002-MF000000200");
                if (ctrl is CheckBox)
                {
                    if (((CheckBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((CheckBox)ctrl).Checked = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? false : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                    }
                }

                if (ctrl.HasControls())
                    SearchControlAndLoadData(ctrl, ServiceComponentId, ListaValores);

            }
        }

        private void SearchControlAndSetValues(Control ctrlContainer, string pstrServiceComponentId)
        {
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList serviceComponentFields = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList serviceComponentFieldValues = null;
            OperationResult objOperationResult = new OperationResult();
            int Contador = 0;
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                Contador++;
                if (ctrl is TextBox)
                {
                    if (((TextBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        var x = ((TextBox)ctrl).Text;
                        var y = ((TextBox)ctrl).Attributes.GetValue("Tag");

                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

                        serviceComponentFields.v_ComponentFieldsId = ((TextBox)ctrl).Attributes.GetValue("Tag").ToString();
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;

                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        serviceComponentFieldValues.v_Value1 = ((TextBox)ctrl).Text;
                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                        // Agregar a mi lista
                        _serviceComponentFieldsList.Add(serviceComponentFields);

                    }

                }
                if (ctrl is TextArea)
                {
                    if (((TextArea)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        var x = ((TextArea)ctrl).Text;
                        var y = ((TextArea)ctrl).Attributes.GetValue("Tag");

                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

                        serviceComponentFields.v_ComponentFieldsId = ((TextArea)ctrl).Attributes.GetValue("Tag").ToString();
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;

                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        serviceComponentFieldValues.v_Value1 = ((TextArea)ctrl).Text;
                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                        // Agregar a mi lista
                        _serviceComponentFieldsList.Add(serviceComponentFields);

                    }

                }

                if (ctrl is DropDownList)
                {
                    if (((DropDownList)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        var x = ((DropDownList)ctrl);
                        var y = ((DropDownList)ctrl).Attributes.GetValue("Tag");

                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

                        serviceComponentFields.v_ComponentFieldsId = ((DropDownList)ctrl).Attributes.GetValue("Tag").ToString();
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;

                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        serviceComponentFieldValues.v_Value1 = ((DropDownList)ctrl).SelectedValue.ToString();
                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                        // Agregar a mi lista
                        _serviceComponentFieldsList.Add(serviceComponentFields);
                    }
                }

                if (ctrl is CheckBox)
                {
                    if (((CheckBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        var y = ((CheckBox)ctrl).Attributes.GetValue("Tag");

                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

                        serviceComponentFields.v_ComponentFieldsId = ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString();
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;

                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        serviceComponentFieldValues.v_Value1 = ((CheckBox)ctrl).Checked == true ? "1" : "0";
                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                        // Agregar a mi lista
                        _serviceComponentFieldsList.Add(serviceComponentFields);
                    }
                }

                if (ctrl is RadioButton)
                {
                    if (((RadioButton)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();
                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
                        var componentFieldId = ((RadioButton)ctrl).Attributes.GetValue("Tag").ToString();

                        serviceComponentFields.v_ComponentFieldsId = componentFieldId;
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;
                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        //serviceComponentFieldValues.v_Value1 = ((CheckBox)ctrl).Checked == true ? "1" : "0";
                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                        // Agregar a mi lista
                        _serviceComponentFieldsList.Add(serviceComponentFields);

                    }
                }

                if (ctrl.HasControls())
                    SearchControlAndSetValues(ctrl, pstrServiceComponentId);
            }

            Session["_serviceComponentFieldsList"] = _serviceComponentFieldsList;


        }

        private void SearchControlAndClean(Control ctrlContainer, List<Sigesoft.Node.WinClient.BE.ComponentFieldsList> ListaValoresPorDefecto)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is DropDownList)
                {
                    if (((DropDownList)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((DropDownList)ctrl).Attributes.GetValue("Tag").ToString();
                        ((DropDownList)ctrl).SelectedValue = ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId) == null ? "-1" : ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId).v_DefaultText;
                    }

                }
                if (ctrl is TextArea)
                {
                    if (((TextArea)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((TextArea)ctrl).Attributes.GetValue("Tag").ToString();
                        ((TextArea)ctrl).Text = ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId) == null ? "" : ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId).v_DefaultText;

                    }
                }

                if (ctrl is TextBox)
                {
                    if (((TextBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((TextBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((TextBox)ctrl).Text = ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId) == null ? "" : ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId).v_DefaultText;

                    }
                }

                if (ctrl is CheckBox)
                {
                    if (((CheckBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((CheckBox)ctrl).Checked = ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId) == null ? false : ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId).v_DefaultText == "0" ? false : true;

                    }
                }

                if (ctrl.HasControls())
                    SearchControlAndClean(ctrl, ListaValoresPorDefecto);

            }
        }

        private Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList SearchDxSugeridoOfSystem(string valueToAnalyze, string pComponentFieldsId, string dataTypeControlToParse)
        {
            Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList diagnosticRepository = null;
            string matchValId = null;
            bool exitLoop = false;
            OperationResult objOperationResult = new OperationResult();

            var componentField = _serviceBL.ConfiguracionDxAutomatico(Session["serviceId"].ToString()).FindAll(p => p.v_ComponentFieldsId == pComponentFieldsId);

            if (componentField != null)
            {
                if (componentField != null)
                {
                    int generoId = txtGeneroCabecera.Text == "MASCULINO" ? 1 : 2;
                    var x = componentField.FindAll(p => p.i_GenderId == generoId || p.i_GenderId == -1);
                    foreach (Sigesoft.Node.WinClient.BE.ComponentFieldValues val in x)
                    {
                        switch ((Operator2Values)val.i_OperatorId)
                        {
                            #region Analizar valor ingresado x el medico contra una serie de valores k se obtinen desde la BD

                            case Operator2Values.X_esIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) == int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) == double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_noesIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) != int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) != double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A:
                                // X >= 40.0 (Obesidad clase III)
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorque_B:

                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < A && X <= B 
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    var parse = double.Parse(valueToAnalyze);
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            default:
                                //MessageBox.Show("valor no encontrado " + valueToAnalyze);
                                break;

                            #endregion
                        }

                        if (exitLoop)
                        {
                            #region CREAR / AGREGAR DX (automático)

                            matchValId = val.v_ComponentFieldValuesId;

                            // Si el valor analizado se encuentra en el rango de valores NORMALES, 
                            // entonces NO se genera un DX (automático).
                            if (val.v_DiseasesId == null)
                                break;

                            val.Recomendations.ForEach(item => { item.v_RecommendationId = Guid.NewGuid().ToString(); });
                            val.Restrictions.ForEach(item => { item.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString(); });
                            // Insertar DX sugerido (automático) a la bolsa de DX 
                            diagnosticRepository = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
                            diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                            diagnosticRepository.v_DiseasesId = val.v_DiseasesId;
                            diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;

                            diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;
                            if (val.v_CIE10 == "Z000" || val.v_CIE10 == "Z001")
                            {
                                diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Normal;
                                diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Descartado;
                            }
                            else
                            {
                                diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                                diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                            }




                            diagnosticRepository.v_ServiceId = Session["serviceId"].ToString();
                            diagnosticRepository.v_ComponentId = val.v_ComponentId;
                            diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            //diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            //diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            // ID enlace DX automatico para grabar valores dinamicos
                            diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            diagnosticRepository.v_ComponentFieldsId = pComponentFieldsId;
                            diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            #endregion
                            break;
                        }

                    }
                }
            }

            return diagnosticRepository;

        }

        private string GetDataTypeControl(int ControlId)
        {
            string dataType = null;

            switch ((ControlType)ControlId)
            {
                case ControlType.NumeroEntero:
                    dataType = "int";
                    break;
                case ControlType.NumeroDecimal:
                    dataType = "double";
                    break;
                case ControlType.SiNoCheck:
                    dataType = "int";
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.SiNoCombo:
                    dataType = "int";
                    break;
                case ControlType.Lista:
                    dataType = "int";
                    break;
                default:
                    break;
            }

            return dataType;
        }

        private List<Sigesoft.Node.WinClient.BE.RestrictionList> RefreshRestrictionList(List<Sigesoft.Node.WinClient.BE.RestrictionList> prestrictions)
        {
            var restrictionsList = new List<Sigesoft.Node.WinClient.BE.RestrictionList>();

            foreach (var item in prestrictions)
            {
                // Agregar restricciones (Automáticas) a la Lista mas lo que ya tiene
                Sigesoft.Node.WinClient.BE.RestrictionList restriction = new Sigesoft.Node.WinClient.BE.RestrictionList();

                restriction.v_RestrictionByDiagnosticId = item.v_RestrictionByDiagnosticId;
                restriction.v_ServiceId = Session["serviceId"].ToString();
                restriction.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                restriction.v_MasterRestrictionId = item.v_MasterRestrictionId;
                restriction.v_RestrictionName = item.v_RestrictionName;
                restriction.i_RecordStatus = (int)RecordStatus.Agregado;
                restriction.i_RecordType = (int)RecordType.Temporal;
                restriction.v_ComponentId = item.v_ComponentId;

                restrictionsList.Add(restriction);
            }

            return restrictionsList;
        }

        private List<Sigesoft.Node.WinClient.BE.RecomendationList> RefreshRecomendationList(List<Sigesoft.Node.WinClient.BE.RecomendationList> precomendations)
        {
            var recomendationsList = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();

            foreach (var item in precomendations)
            {
                // Agregar restricciones a la Lista mas lo que ya tiene
                Sigesoft.Node.WinClient.BE.RecomendationList recomendation = new Sigesoft.Node.WinClient.BE.RecomendationList();

                recomendation.v_RecommendationId = item.v_RecommendationId;
                recomendation.v_ServiceId = Session["serviceId"].ToString();
                recomendation.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                recomendation.v_RecommendationId = item.v_RecommendationId;
                recomendation.v_MasterRecommendationId = item.v_MasterRecommendationId;  // ID -> RECOME / RESTRIC (BOLSA CONFIG POR M. MENDEZ)
                recomendation.v_RecommendationName = item.v_RecommendationName;
                recomendation.i_RecordStatus = (int)RecordStatus.Agregado;
                recomendation.i_RecordType = (int)RecordType.Temporal;
                recomendation.v_ComponentId = item.v_ComponentId;

                recomendationsList.Add(recomendation);
            }

            return recomendationsList;
        }

        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(sFilePath);
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }      
      
        #endregion          

        #region Acordion

        protected void grdDx_RowClick(object sender, GridRowClickEventArgs e)
        {
            int index = e.RowIndex;
            Session["indexgrdDx"] = index;
            var dataKeys = grdDx.DataKeys[index];
            Session["DiagnosticRepositoryId"] = dataKeys[0].ToString();
            Session["ComponentId"] = dataKeys[1].ToString();
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
        }

        protected void grdDx_RowCommand(object sender, GridCommandEventArgs e)
        {
            int index = e.RowIndex;
            var dataKeys = grdDx.DataKeys[index];
            //Session["ServiceId"] = dataKeys[1].ToString();
            if (e.CommandName == "DeleteAction")
            {
                DeleteItemDiagnostico(dataKeys[0].ToString());
                ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());

            }
        }

        protected void ActualizaGrillasDx(string ServiceId, string PersonId)
        {

            OperationResult objOperationResult = new OperationResult();
            var ListaDx = _serviceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, ServiceId);
            Session["GrillaDx"] = ListaDx;
            var ListaComponentes = (List<string>)Session["ComponentesPermisoLectura"];
            highlightRows.Text = "";
            grdDx.DataSource = ListaDx.FindAll(p => ListaComponentes.Contains(p.v_ComponentId));
            grdDx.DataBind();

            if (ListaDx.Count != 0)
            {
                grdDx.SelectedRowIndex = 0;
                ActualizaGrillasRecoYRestri(ListaDx[0].v_DiagnosticRepositoryId);
            }
            else
            {
                ActualizaGrillasRecoYRestri(null);
            }

            if (Session["indexgrdDx"] != null)
            {
                grdDx.SelectedRowIndex = int.Parse(Session["indexgrdDx"].ToString());

            }

        }

        private void DeleteItemDiagnostico(string pDiagnosticoRepositoryId)
        {
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _serviceBL.EliminarDxByDiagnosticRepository(pDiagnosticoRepositoryId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }

        protected void ActualizaGrillasRecoYRestri(string DiagnosticRepositoryId)
        {
            OperationResult objOperationResult = new OperationResult();
            _tmpTotalDiagnostic = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
            if (DiagnosticRepositoryId == null)
            {
                grdRecomendaciones.DataSource = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();
                grdRecomendaciones.DataBind();

                grdRestricciones.DataSource = new List<Sigesoft.Node.WinClient.BE.RestrictionList>();
                grdRestricciones.DataBind();
            }
            else
            {

                _tmpTotalDiagnostic = _serviceBL.GetServiceComponentTotalDiagnostics(ref objOperationResult, DiagnosticRepositoryId);

                grdRecomendaciones.DataSource = _tmpTotalDiagnostic.Recomendations;
                grdRecomendaciones.DataBind();

                grdRestricciones.DataSource = _tmpTotalDiagnostic.Restrictions;
                grdRestricciones.DataBind();
            }

        }

        protected void btnGrabarAptitud_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlFirmaAuditor.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar firma de auditor", MessageBoxIcon.Warning);
                return;
            }
            //Actualizar Aptitud del Servicio
            _serviceBL.ActualizarAptitudServicio(ref objOperationResult, Session["ServiceId"].ToString(), int.Parse(ddlAptitud.SelectedValue.ToString()), ((ClientSession)Session["objClientSession"]).GetAsList(), txtComentarioAptitud.Text, "", "", int.Parse(ddlFirmaAuditor.SelectedValue.ToString()));

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                Alert.ShowInTop("Se grabó correctamente", MessageBoxIcon.Information);
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
        }

        protected void WindowAddDX_Close(object sender, WindowCloseEventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
        }

        protected void WindowAddDXFrecuente_Close(object sender, WindowCloseEventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
        }

        #endregion

        #region Recomendaciones y restricciones

        protected void winEditReco_Close(object sender, WindowCloseEventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
        }

        protected void winEditRestri_Close(object sender, WindowCloseEventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasRecoYRestri(Session["DiagnosticRepositoryId"].ToString());
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
            _serviceBL.EliminarRecomendacion(ref objOperationResult, pRecomenedationId);
        }

        private void DeleteItemRestriccion(string pRestriccionId)
        {
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _serviceBL.EliminarRestriccion(ref objOperationResult, pRestriccionId);
        }

        #endregion


        protected void btnGrabarEspirometria_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabar.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
          
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabEspirometria, Session["ServicioComponentIdEspiro"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdEspiro"].ToString());


            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002131") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkespirometria_normal.Checked ? "1" : "0", "N009-MF000002131", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002154") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkpatron_obstructivo.Checked ? "1" : "0", "N009-MF000002154", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002155") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkpatron_restrictivo.Checked ? "1" : "0", "N009-MF000002155", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            

            #endregion
            //Gabar Dx
            #region Capturar [Comentarios, estado, procedencia de un exmanen componente]

            var serviceComponentDto = new servicecomponentDto();
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdEspiro"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdEspiro"].ToString()).d_UpdateDate;
            serviceComponentDto.v_Comment = "";
            //grabar estado del examen según profesión del usuario
            int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());

            if (ProfesionId == 30) // evaluador
            {
                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;

            }
            else if (ProfesionId == 31)//auditor
            {
                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Auditado;

            }
            //serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceStatus.Culminado;

            serviceComponentDto.i_ExternalInternalId = 1;
            serviceComponentDto.i_IsApprovedId = 1;

            serviceComponentDto.v_ComponentId = "N002-ME000000031";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion


            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabar.SelectedValue.ToString());


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   l,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);


            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N002-ME000000031");
          
            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtEspirometriaAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtEspirometriaAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtEspirometriaAuditorModificacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtEspirometriaEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtEspirometriaEvaluadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtEspirometriaEvaluadorModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtEspirometriaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtEspirometriaInformadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtEspirometriaInformadorModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
            }

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                //Mostrar
                grdComponentes.DataSource = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(ddlConsultorio.SelectedValue.ToString()), Session["ServiceId"].ToString());
                grdComponentes.DataBind();
                Session["_serviceComponentFieldsList"] = null;
                Alert.ShowInTop("Se grabó correctamente", MessageBoxIcon.Information);
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(Session["UsuarioLogueado"].ToString());
          
        }

        private void LoadCombosEspirometria()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            //Utils.LoadDropDownList(ddlPre1, "Value1", "Id", Combo111, DropDownListAction.Select);
 

 
            //Utils.LoadDropDownList(ddlPre2, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlPre3, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlPre4, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlPre5, "Value1", "Id", Combo111, DropDownListAction.Select);

            //Utils.LoadDropDownList(ddlhemoptisis, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlpneumotorax, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddltraqueostomia, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlsonda_pleural, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlaneurisma_cerebral, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlembolia_pulmonar, "Value1", "Id", Combo111, DropDownListAction.Select);

            //Utils.LoadDropDownList(ddlinfarto_reciente, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlinestabilidad_cv, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlfiebre_nausea, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlembarazo_avanzado, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlembarazo_complicado, "Value1", "Id", Combo111, DropDownListAction.Select);

            //Utils.LoadDropDownList(ddl1_tuvo_una_infeccion, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddl2_tuvo_infeccion, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddl3_uso_aerosoles, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddl4_ha_usado_algun_medicamento, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddl5_ha_fumado_cualquier, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddl6_realizo_algun_ejercicio, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddl7_ingirio_alimentos, "Value1", "Id", Combo111, DropDownListAction.Select);
 

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

        }

        private void ObtenerDatosEspirometria(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var oExamenElectro = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 16);
            Session["ServicioComponentIdEspiro"] = oExamenElectro[0].ServicioComponentId;
            var objExamenElectro = _serviceBL.GetServiceComponentFields(oExamenElectro == null ? "" : oExamenElectro[0].ServicioComponentId, pServiceId);
            Session["ComponentesEspiro"] = objExamenElectro;
            if (objExamenElectro.ToList().Count != 0)
            {
                SearchControlAndLoadData(TabEspirometria, Session["ServicioComponentIdEspiro"].ToString(), (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesEspiro"]);
                #region Campos auditoria
                var datosAuditoria = HistoryBL.CamposAuditoria(oExamenElectro[0].ServicioComponentId);
                if (datosAuditoria != null)
                {
                    txtEspirometriaAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtEspirometriaAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtEspirometriaAuditorModificacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtEspirometriaEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtEspirometriaEvaluadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtEspirometriaEvaluadorModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtEspirometriaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtEspirometriaInformadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtEspirometriaInformadorModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabEspirometria, _tmpServiceComponentsForBuildMenuList);

                txtEspirometriaAuditor.Text = "";
                txtEspirometriaAuditorInsercion.Text = "";
                txtEspirometriaAuditorModificacion.Text = "";

                txtEspirometriaEvaluador.Text = "";
                txtEspirometriaEvaluadorInsercion.Text = "";
                txtEspirometriaEvaluadorModificacion.Text = "";

                txtEspirometriaInformador.Text = txtEspirometriaEvaluadorModificacion.Text = "";
                txtEspirometriaInformadorInsercion.Text = txtEspirometriaEvaluadorModificacion.Text = "";
                txtEspirometriaInformadorModificacion.Text = txtEspirometriaEvaluadorModificacion.Text = "";

            }



        }

        protected void fileDoc_FileSelected(object sender, EventArgs e)
        {
            string Ruta = WebConfigurationManager.AppSettings["ImgESPIROOrigen"].ToString();
            string Dni = Session["DniTrabajador"].ToString();
            string Fecha = Session["FechaServicio"].ToString();
            string Consultorio = "ESPIROMETRÍA";
            string Ext = fileDoc.FileName.Substring(fileDoc.FileName.Length - 3, 3);
            fileDoc.SaveAs(Ruta + Dni + "-" + Fecha + "-" + Consultorio + "." + Ext);
            Alert.ShowInTop("El archivo subió correctamente", MessageBoxIcon.Information);
            fileDoc.Text = "";
        }

        protected void lnkEspirometria_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000031";

            GenerateEspirometria(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N002-ME000000031.pdf", path + ".pdf");
        }

        private void GenerateEspirometria(string _ruta, string p)
        {
            var ESPIROMETRIA_ID = new ServiceBL().GetReportCuestionarioEspirometria(p, Constants.ESPIROMETRIA_ID);

            dsGetRepo = new DataSet();
            DataTable dt_ESPIROMETRIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ESPIROMETRIA_ID);
            dt_ESPIROMETRIA_ID.TableName = "dtCuestionarioEspirometria";
            dsGetRepo.Tables.Add(dt_ESPIROMETRIA_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCuestionarioEspirometria();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.ESPIROMETRIA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            fileDoc.Text = "";
        }

     
    }
}