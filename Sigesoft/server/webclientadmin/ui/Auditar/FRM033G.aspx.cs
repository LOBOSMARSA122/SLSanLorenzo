using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033G : System.Web.UI.Page
    {
        ServiceBL oServiceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }    
        }

        private void loadCombo()
        {
            OperationResult objOperationResult = new OperationResult();

            if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 30)
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

                if (results.Count > 0)
                {
                    ddlConsultorio.SelectedIndex = 1;
                    var CategoriaId = ddlConsultorio.SelectedValue;

                    //Obtener los componetes de ese servicio en función de la categoría seleccionada

                    var Examenes = oServiceBL.DevolverExamenesPorCategoria(Session["ServiceId"].ToString(), int.Parse(CategoriaId));

                    Utils.LoadDropDownList(ddlExamen, "Value1", "Id", Examenes, DropDownListAction.Select);

                    if (Examenes.Count > 0)
                    {
                        ddlExamen.SelectedIndex = 1;
                    }
                }
                else
                {
                    Utils.LoadDropDownList(ddlExamen, "Value1", "Id", null, DropDownListAction.Select);
                }
               

              

            }
            else if (((ClientSession)Session["objClientSession"]).i_ProfesionId == 31)
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

                //ddlConsultorio.Enabled = false;
                Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", groupComponentList, DropDownListAction.All);
               
            }
        }

        protected void ddlConsultorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Obtener Componentes del consultorio en función de su protoclo
            var CategoriaId = ddlConsultorio.SelectedValue;

            //Obtener los componetes de ese servicio en función de la categoría seleccionada

            var Examenes = oServiceBL.DevolverExamenesPorCategoria(Session["ServiceId"].ToString(), int.Parse(CategoriaId));

            Utils.LoadDropDownList(ddlExamen, "Value1", "Id", Examenes, DropDownListAction.Select);

            if (Examenes.Count > 0)
            {
                ddlExamen.SelectedIndex = 1;
            }

        }

        protected void grdCie10_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            int index = e.RowIndex;
            var dataKeys = grdCie10.DataKeys[index];
            Session["Cie10Id"] = dataKeys[4];
            Session["DiseasesId"] = dataKeys[2];
            Session["v_DxFrecuenteId"] = dataKeys[3];

            //Obtener las Recomendaciones y restricciones
            if (Session["v_DxFrecuenteId"] != null)
            {
                txtRecomendacion1.Text = oServiceBL.ObtenerRecomendacionesPorDxFrecuente(Session["v_DxFrecuenteId"].ToString(), 1);
                txtRestriccion1.Text = oServiceBL.ObtenerRecomendacionesPorDxFrecuente(Session["v_DxFrecuenteId"].ToString(), 2);
            }
            else
            {
                txtRecomendacion1.Text = "";
                txtRestriccion1.Text = "";

                txtDxActualizado.Text = dataKeys[1].ToString();
            }
      

        }

        protected void grdCie10_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdCie10.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnCie10_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtDx.Text)) Filters.Add("v_CIE10Idv_Name.Contains(\"" + txtDx.Text.Trim() + "\")");
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
            grdCie10.PageIndex = 0;
            this.BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            OperationResult objOperationResult = new OperationResult();
            highlightRows.Text = "";
            txtRecomendacion1.Text = "";
            txtRestriccion1.Text = "";
            txtDxActualizado.Text = "";
            grdCie10.DataSource = oServiceBL.GetDxFrecuentes(ref objOperationResult, grdCie10.PageIndex, grdCie10.PageSize, "v_CIE10Id", strFilterExpression);
            grdCie10.DataBind();
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();
            List<dxfrecuentedetalleDto> ListadxfrecuentedetalleDto = new List<dxfrecuentedetalleDto>();
                     
            if (ddlExamen.SelectedValue == null || ddlExamen.SelectedValue == "-1")
            {
                Alert.Show("Por favor seleccione un examen.");
                return; 
            }

            if (Session["v_DxFrecuenteId"] != null) // Grabar un Dx frecuente
            {
                //Grabar el Dx en el servicio
                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _diagnosticRepository = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
                List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> _ListadiagnosticRepository = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

                List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendaciones = new List<Node.WinClient.BE.RecomendationList>();
                Sigesoft.Node.WinClient.BE.RecomendationList Recomendacion = null;


                List<Sigesoft.Node.WinClient.BE.RestrictionList> Restricciones = new List<Node.WinClient.BE.RestrictionList>();
                Sigesoft.Node.WinClient.BE.RestrictionList Restriccion = null;



                _diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                _diagnosticRepository.v_DiseasesId = Session["DiseasesId"].ToString();
                _diagnosticRepository.i_AutoManualId = 1;
                _diagnosticRepository.i_PreQualificationId = 1;
                _diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                _diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;

                _diagnosticRepository.v_ServiceId = Session["ServiceId"].ToString();
                _diagnosticRepository.v_ComponentId = ddlExamen.SelectedValue.ToString();  //_componentId;
                _diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                _diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                #region Recomendación
                if (txtRecomendacion1.Text.Trim() != "")
                {
                    //Verficiar si la recomendación se encuentra en la bd
                    MasterRecommendationRestricctionBL oMasterRecommendationRestricctionBL = new MasterRecommendationRestricctionBL();
                    var masterrecommendation = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRecomendacion1.Text.Trim());

                    if (masterrecommendation == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRecomendacion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 1;
                        //Crear Recomendación
                        var MasterRecommendationId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                        Recomendacion = new Node.WinClient.BE.RecomendationList();

                        Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                        Recomendacion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                        Recomendacion.v_MasterRecommendationId = MasterRecommendationId;
                        Recomendacion.i_RecordType = (int)RecordType.Temporal;
                        Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Recomendaciones.Add(Recomendacion);
                    }
                    else
                    {
                        var ListaReco = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente(Session["v_DxFrecuenteId"].ToString(), 1);

                        foreach (var item in ListaReco)
                        {
                            Recomendacion = new Node.WinClient.BE.RecomendationList();

                            Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                            Recomendacion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                            Recomendacion.v_MasterRecommendationId = item.v_MasterRecommendationRestricctionId;
                            Recomendacion.i_RecordType = (int)RecordType.Temporal;
                            Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                            Recomendaciones.Add(Recomendacion);
                        }
                    }
                }

                #endregion

                #region Restricciones

                if (txtRestriccion1.Text.Trim() != "")
                {
                    //Verficiar si la recomendación se encuentra en la bd
                    MasterRecommendationRestricctionBL oMasterRecommendationRestricctionBL = new MasterRecommendationRestricctionBL();
                    var masterrestriccion = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRestriccion1.Text.Trim());
                    if (masterrestriccion == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRestriccion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 2;
                        //Crear Restricción
                        var MasterRestriccionId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                        Restriccion = new Node.WinClient.BE.RestrictionList();

                        Restriccion.v_ServiceId = Session["ServiceId"].ToString();
                        Restriccion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                        Restriccion.v_MasterRestrictionId = MasterRestriccionId;
                        Restriccion.i_RecordType = (int)RecordType.Temporal;
                        Restriccion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Restricciones.Add(Restriccion);
                    }
                    else
                    {
                        var ListaRestri = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente(Session["v_DxFrecuenteId"].ToString(), 2);

                        foreach (var item1 in ListaRestri)
                        {
                            Restriccion = new Node.WinClient.BE.RestrictionList();

                            Restriccion.v_ServiceId = Session["ServiceId"].ToString();
                            Restriccion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                            Restriccion.v_MasterRestrictionId = item1.v_MasterRecommendationRestricctionId;
                            Restriccion.i_RecordType = (int)RecordType.Temporal;
                            Restriccion.i_RecordStatus = (int)RecordStatus.Agregado;
                            Restricciones.Add(Restriccion);
                        }
                    }
                }

                #endregion

                _diagnosticRepository.Restrictions = Restricciones;
                _diagnosticRepository.Recomendations = Recomendaciones;

                _ListadiagnosticRepository.Add(_diagnosticRepository);

                oServiceBL.AddDiagnosticRepository(ref objOperationResult, _ListadiagnosticRepository, null, ((ClientSession)Session["objClientSession"]).GetAsList(), true);

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
            else if (Session["DiseasesId"] == null)
            {

                #region Crear el Dx Frecuente

                objDiseaseDto.v_CIE10Id = Session["Cie10Id"].ToString();
                objDiseaseDto.v_Name = txtDxActualizado.Text;

                objDiseaseDto1 = oServiceBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);
                string DxFrecuneteId = null;
                if (objDiseaseDto1 == null)
                {
                    //Creo el Diseases
                    objDiseaseDto.v_DiseasesId = oServiceBL.AddDiseases(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                    //Crear nuevo Dx Frecuente y lo amarro al diseasesId creado
                    DxFrecuenteBL oDxFrecuenteBL = new DxFrecuenteBL();
                    dxfrecuenteDto odxfrecuenteDto = new dxfrecuenteDto();
                    odxfrecuenteDto.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                    odxfrecuenteDto.v_CIE10Id = Session["Cie10Id"].ToString();
                    DxFrecuneteId = oDxFrecuenteBL.AddDxFrecuente(ref objOperationResult, odxfrecuenteDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                    //Agregar Recomendaciones a Dx Frecuente
                    dxfrecuentedetalleDto odxfrecuentedetalleDto = new dxfrecuentedetalleDto();

                    //Verficiar si la recomendación se encuentra en la bd
                    MasterRecommendationRestricctionBL oMasterRecommendationRestricctionBL = new MasterRecommendationRestricctionBL();
                    var masterrecommendation = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRecomendacion1.Text.Trim());
                    var MasterRecommendationId = "";
                    if (masterrecommendation == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRecomendacion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 1;
                        //Crear Recomendación
                        MasterRecommendationId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                    }
                    odxfrecuentedetalleDto.v_DxFrecuenteId = DxFrecuneteId;
                    odxfrecuentedetalleDto.v_MasterRecommendationRestricctionId = MasterRecommendationId;
                    ListadxfrecuentedetalleDto.Add(odxfrecuentedetalleDto);


                    odxfrecuentedetalleDto = new dxfrecuentedetalleDto();
                    //Agregar Restricciones a Dx Frecuente
                    var masterrestriccion = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRestriccion1.Text.Trim());
                    var MasterRestriccionId = "";
                    if (masterrestriccion == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRestriccion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 2;
                        //Crear Restricción
                        MasterRestriccionId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                    }
                    odxfrecuentedetalleDto.v_DxFrecuenteId = DxFrecuneteId;
                    odxfrecuentedetalleDto.v_MasterRecommendationRestricctionId = MasterRestriccionId;
                    ListadxfrecuentedetalleDto.Add(odxfrecuentedetalleDto);
                    //Agrego el detalle de los dxfrecuentes creados
                    foreach (var item in ListadxfrecuentedetalleDto)
                    {
                        oDxFrecuenteBL.AddDxFrecuenteDetalle(ref objOperationResult, item, ((ClientSession)Session["objClientSession"]).GetAsList());
                    }
               
              




                }
                else
                {
                    Alert.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                #region Grabo el dx al servicio

                //Grabar el Dx en el servicio
                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _diagnosticRepository = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
                List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> _ListadiagnosticRepository = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

                List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendaciones = new List<Node.WinClient.BE.RecomendationList>();
                Sigesoft.Node.WinClient.BE.RecomendationList Recomendacion = null;


                List<Sigesoft.Node.WinClient.BE.RestrictionList> Restricciones = new List<Node.WinClient.BE.RestrictionList>();
                Sigesoft.Node.WinClient.BE.RestrictionList Restriccion = null;



                _diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                _diagnosticRepository.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                _diagnosticRepository.i_AutoManualId = 1;
                _diagnosticRepository.i_PreQualificationId = 1;
                _diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                _diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;

                _diagnosticRepository.v_ServiceId = Session["ServiceId"].ToString();
                _diagnosticRepository.v_ComponentId = ddlExamen.SelectedValue.ToString();  //_componentId;
                _diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                _diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                #region Recomendación
                if (txtRecomendacion1.Text.Trim() != "")
                {
                    //Verficiar si la recomendación se encuentra en la bd
                    MasterRecommendationRestricctionBL oMasterRecommendationRestricctionBL = new MasterRecommendationRestricctionBL();
                    var masterrecommendation = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRecomendacion1.Text.Trim());

                    if (masterrecommendation == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRecomendacion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 1;
                        //Crear Recomendación
                        var MasterRecommendationId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                        Recomendacion = new Node.WinClient.BE.RecomendationList();

                        Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                        Recomendacion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                        Recomendacion.v_MasterRecommendationId = MasterRecommendationId;
                        Recomendacion.i_RecordType = (int)RecordType.Temporal;
                        Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Recomendaciones.Add(Recomendacion);
                    }
                    else
                    {
                        var ListaReco = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente(DxFrecuneteId, 1);

                        foreach (var item in ListaReco)
                        {
                            Recomendacion = new Node.WinClient.BE.RecomendationList();

                            Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                            Recomendacion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                            Recomendacion.v_MasterRecommendationId = item.v_MasterRecommendationRestricctionId;
                            Recomendacion.i_RecordType = (int)RecordType.Temporal;
                            Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                            Recomendaciones.Add(Recomendacion);
                        }
                    }
                }

                #endregion

                #region Restricciones

                if (txtRestriccion1.Text.Trim() != "")
                {
                    //Verficiar si la recomendación se encuentra en la bd
                    MasterRecommendationRestricctionBL oMasterRecommendationRestricctionBL = new MasterRecommendationRestricctionBL();
                    var masterrestriccion = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRestriccion1.Text.Trim());
                    if (masterrestriccion == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRestriccion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 2;
                        //Crear Restricción
                        var MasterRestriccionId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                        Restriccion = new Node.WinClient.BE.RestrictionList();

                        Restriccion.v_ServiceId = Session["ServiceId"].ToString();
                        Restriccion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                        Restriccion.v_MasterRestrictionId = MasterRestriccionId;
                        Restriccion.i_RecordType = (int)RecordType.Temporal;
                        Restriccion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Restricciones.Add(Restriccion);
                    }
                    else
                    {
                        var ListaRestri = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente(DxFrecuneteId, 2);

                        foreach (var item1 in ListaRestri)
                        {
                            Restriccion = new Node.WinClient.BE.RestrictionList();

                            Restriccion.v_ServiceId = Session["ServiceId"].ToString();
                            Restriccion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                            Restriccion.v_MasterRestrictionId = item1.v_MasterRecommendationRestricctionId;
                            Restriccion.i_RecordType = (int)RecordType.Temporal;
                            Restriccion.i_RecordStatus = (int)RecordStatus.Agregado;
                            Restricciones.Add(Restriccion);
                        }
                    }
                }

                #endregion

                _diagnosticRepository.Restrictions = Restricciones;
                _diagnosticRepository.Recomendations = Recomendaciones;

                _ListadiagnosticRepository.Add(_diagnosticRepository);

                oServiceBL.AddDiagnosticRepository(ref objOperationResult, _ListadiagnosticRepository, null, ((ClientSession)Session["objClientSession"]).GetAsList(), true);

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
                #endregion

            }
            else if (Session["DiseasesId"] != null)
            {
                string DxFrecuneteId = null;
                objDiseaseDto = oServiceBL.GetDiseases(ref  objOperationResult, Session["DiseasesId"].ToString());

                objDiseaseDto.v_CIE10Id = Session["Cie10Id"].ToString();
                objDiseaseDto.v_Name = txtDxActualizado.Text;
                oServiceBL.UpdateDiseases(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                #region Crear el Dx Frecuente
                //Crear nuevo Dx Frecuente y lo amarro al diseasesId creado
                DxFrecuenteBL oDxFrecuenteBL = new DxFrecuenteBL();
                dxfrecuenteDto odxfrecuenteDto = new dxfrecuenteDto();
                odxfrecuenteDto.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                odxfrecuenteDto.v_CIE10Id = Session["Cie10Id"].ToString();
                DxFrecuneteId = oDxFrecuenteBL.AddDxFrecuente(ref objOperationResult, odxfrecuenteDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                //Agregar Recomendaciones a Dx Frecuente
                dxfrecuentedetalleDto odxfrecuentedetalleDto = new dxfrecuentedetalleDto();

                //Verficiar si la recomendación se encuentra en la bd
                MasterRecommendationRestricctionBL oMasterRecommendationRestricctionBL = new MasterRecommendationRestricctionBL();
                var masterrecommendation = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRecomendacion1.Text.Trim());
                var MasterRecommendationId = "";
                if (masterrecommendation == null)
                {
                    masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                    omasterrecommendationrestricctionDto.v_Name = txtRecomendacion1.Text.Trim();
                    omasterrecommendationrestricctionDto.i_TypifyingId = 1;
                    //Crear Recomendación
                    MasterRecommendationId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                }
                odxfrecuentedetalleDto.v_DxFrecuenteId = DxFrecuneteId;
                odxfrecuentedetalleDto.v_MasterRecommendationRestricctionId = MasterRecommendationId;
                ListadxfrecuentedetalleDto.Add(odxfrecuentedetalleDto);


                odxfrecuentedetalleDto = new dxfrecuentedetalleDto();
                //Agregar Restricciones a Dx Frecuente
                var masterrestriccion = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRestriccion1.Text.Trim());
                var MasterRestriccionId = "";
                if (masterrestriccion == null)
                {
                    masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                    omasterrecommendationrestricctionDto.v_Name = txtRestriccion1.Text.Trim();
                    omasterrecommendationrestricctionDto.i_TypifyingId = 2;
                    //Crear Restricción
                    MasterRestriccionId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                }
                odxfrecuentedetalleDto.v_DxFrecuenteId = DxFrecuneteId;
                odxfrecuentedetalleDto.v_MasterRecommendationRestricctionId = MasterRestriccionId;
                ListadxfrecuentedetalleDto.Add(odxfrecuentedetalleDto);
                //Agrego el detalle de los dxfrecuentes creados
                foreach (var item in ListadxfrecuentedetalleDto)
                {
                    oDxFrecuenteBL.AddDxFrecuenteDetalle(ref objOperationResult, item, ((ClientSession)Session["objClientSession"]).GetAsList());
                }
                #endregion

                #region Grabo el dx al servicio

                //Grabar el Dx en el servicio
                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _diagnosticRepository = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();
                List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> _ListadiagnosticRepository = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

                List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendaciones = new List<Node.WinClient.BE.RecomendationList>();
                Sigesoft.Node.WinClient.BE.RecomendationList Recomendacion = null;


                List<Sigesoft.Node.WinClient.BE.RestrictionList> Restricciones = new List<Node.WinClient.BE.RestrictionList>();
                Sigesoft.Node.WinClient.BE.RestrictionList Restriccion = null;



                _diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                _diagnosticRepository.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                _diagnosticRepository.i_AutoManualId = 1;
                _diagnosticRepository.i_PreQualificationId = 1;
                _diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                _diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;

                _diagnosticRepository.v_ServiceId = Session["ServiceId"].ToString();
                _diagnosticRepository.v_ComponentId = ddlExamen.SelectedValue.ToString();  //_componentId;
                _diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                _diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                #region Recomendación
                if (txtRecomendacion1.Text.Trim() != "")
                {
                    //Verficiar si la recomendación se encuentra en la bd
                     masterrecommendation = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRecomendacion1.Text.Trim());

                    if (masterrecommendation == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRecomendacion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 1;
                        //Crear Recomendación
                        MasterRecommendationId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                        Recomendacion = new Node.WinClient.BE.RecomendationList();

                        Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                        Recomendacion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                        Recomendacion.v_MasterRecommendationId = MasterRecommendationId;
                        Recomendacion.i_RecordType = (int)RecordType.Temporal;
                        Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Recomendaciones.Add(Recomendacion);
                    }
                    else
                    {
                        var ListaReco = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente(DxFrecuneteId, 1);

                        foreach (var item in ListaReco)
                        {
                            Recomendacion = new Node.WinClient.BE.RecomendationList();

                            Recomendacion.v_ServiceId = Session["ServiceId"].ToString();
                            Recomendacion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                            Recomendacion.v_MasterRecommendationId = item.v_MasterRecommendationRestricctionId;
                            Recomendacion.i_RecordType = (int)RecordType.Temporal;
                            Recomendacion.i_RecordStatus = (int)RecordStatus.Agregado;
                            Recomendaciones.Add(Recomendacion);
                        }
                    }
                }

                #endregion

                #region Restricciones

                if (txtRestriccion1.Text.Trim() != "")
                {
                    //Verficiar si la recomendación se encuentra en la bd
                  
                     masterrestriccion = oMasterRecommendationRestricctionBL.GetMasterRecommendationRestricctionByName(txtRestriccion1.Text.Trim());
                    if (masterrestriccion == null)
                    {
                        masterrecommendationrestricctionDto omasterrecommendationrestricctionDto = new masterrecommendationrestricctionDto();
                        omasterrecommendationrestricctionDto.v_Name = txtRestriccion1.Text.Trim();
                        omasterrecommendationrestricctionDto.i_TypifyingId = 2;
                        //Crear Restricción
                         MasterRestriccionId = oMasterRecommendationRestricctionBL.AddMasterRecommendationRestricction(ref objOperationResult, omasterrecommendationrestricctionDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                        Restriccion = new Node.WinClient.BE.RestrictionList();

                        Restriccion.v_ServiceId = Session["ServiceId"].ToString();
                        Restriccion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                        Restriccion.v_MasterRestrictionId = MasterRestriccionId;
                        Restriccion.i_RecordType = (int)RecordType.Temporal;
                        Restriccion.i_RecordStatus = (int)RecordStatus.Agregado;
                        Restricciones.Add(Restriccion);
                    }
                    else
                    {
                        var ListaRestri = oServiceBL.ObtenerListaRecomendacionesPorDxFrecuente(DxFrecuneteId, 2);

                        foreach (var item1 in ListaRestri)
                        {
                            Restriccion = new Node.WinClient.BE.RestrictionList();

                            Restriccion.v_ServiceId = Session["ServiceId"].ToString();
                            Restriccion.v_ComponentId = ddlExamen.SelectedValue.ToString();
                            Restriccion.v_MasterRestrictionId = item1.v_MasterRecommendationRestricctionId;
                            Restriccion.i_RecordType = (int)RecordType.Temporal;
                            Restriccion.i_RecordStatus = (int)RecordStatus.Agregado;
                            Restricciones.Add(Restriccion);
                        }
                    }
                }

                #endregion

                _diagnosticRepository.Restrictions = Restricciones;
                _diagnosticRepository.Recomendations = Recomendaciones;

                _ListadiagnosticRepository.Add(_diagnosticRepository);

                oServiceBL.AddDiagnosticRepository(ref objOperationResult, _ListadiagnosticRepository, null, ((ClientSession)Session["objClientSession"]).GetAsList(), true);

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
                #endregion


            }
        }

        protected void WindowCie10_Close(object sender, EventArgs e)
        {

        }

        protected void grdCie10_RowDataBound(object sender, GridRowEventArgs e)
        {
            Sigesoft.Node.WinClient.BE.DxFrecuenteList row = (Sigesoft.Node.WinClient.BE.DxFrecuenteList)e.DataItem;
            if (row.v_DxFrecuenteId != null)
            {
                highlightRows.Text += e.RowIndex.ToString() + ",";

            }
        }
    }
}