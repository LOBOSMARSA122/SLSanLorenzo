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
    public partial class frmRayosX : System.Web.UI.Page
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

                btnDescargarRX.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("DescargarAdjunto.aspx?Consultorio=RX");
                btnDescargarOIT.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("DescargarAdjunto.aspx?Consultorio=OIT");

                btnReporteRX.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=RX");
                btnReporteOIT.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=OIT");
                btnCertificadoAptitud.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Certificado");
               
                int RoleId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString());
                var ComponentesPermisoLectura = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId(9, RoleId).FindAll(p => p.i_Read == 1);
                List<string> ListaComponentesPermisoLectura = new List<string>();
                foreach (var item in ComponentesPermisoLectura)
                {
                    ListaComponentesPermisoLectura.Add(item.v_ComponentId);
                }
                Session["ComponentesPermisoLectura"] = ListaComponentesPermisoLectura;
                TabRayosX.Hidden = true;
                TabOIT.Hidden = true;

                TabRayosX.Attributes.Add("Tag", "N002-ME000000032");
                TabOIT.Attributes.Add("Tag", "N009-ME000000062");

                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1);  //  DateTime.Parse("12/11/2016");
                dpFechaFin.SelectedDate = DateTime.Now; //  DateTime.Parse("12/11/2016"); 
                LoadCombos();
                
                //rx                   
                txtRXNroPlaca.Attributes.Add("Tag", "N009-MF000001788");
                txtRXVertices.Attributes.Add("Tag", "N009-MF000000590");
                txtRXCamposPulmonares.Attributes.Add("Tag", "N009-MF000000591");
                txtRXHilios.Attributes.Add("Tag", "N009-MF000000592");
                txtRXSenosDiafrag.Attributes.Add("Tag", "N009-MF000000593");
                txtRXSenosCardiofre.Attributes.Add("Tag", "N009-MF000000883");
                txtRXMediastinos.Attributes.Add("Tag", "N009-MF000000594");
                txtRXSiluetaCard.Attributes.Add("Tag", "N009-MF000000595");
                txtRXInidice.Attributes.Add("Tag", "N009-MF000000884");
                txtRXPartesBlandas.Attributes.Add("Tag", "N009-MF000000886");
                chkPlacaNormal.Attributes.Add("Tag", "N009-MF000002134");



                //oit
                txtOITCodigoPlaca.Attributes.Add("Tag", "N002-MF000000211");
                txtOITFechaLectura.Attributes.Add("Tag", "N009-MF000000587");
                txtOITFechaToma.Attributes.Add("Tag", "N009-MF000000588");
                ddlOITCalidad.Attributes.Add("Tag", "N002-MF000000184");
                ddlPerfilPlacaPleurales.Attributes.Add("Tag", "N009-MF000002102");
                ddlFrentePlacaPleurales.Attributes.Add("Tag", "N009-MF000002103");
                ddlDiafragmaPlacaPleurales.Attributes.Add("Tag", "N009-MF000002104");
                ddlOtrosPlacaPleurales.Attributes.Add("Tag", "N009-MF000002105");
                ddlPerfilCalcifica.Attributes.Add("Tag", "N009-MF000002110");
                ddlFrenteCalcifica.Attributes.Add("Tag", "N009-MF000002111");
                ddlDiafragmaCalcifica.Attributes.Add("Tag", "N009-MF000002112");
                ddlOtrosCalcifica.Attributes.Add("Tag", "N009-MF000002113");
                ddlExtensionDerPlacas.Attributes.Add("Tag", "N009-MF000002114");
                ddlExtensionIzqPlacas.Attributes.Add("Tag", "N009-MF000002115");
                ddlObliAngulo.Attributes.Add("Tag", "N009-MF000002118");
                ddlAnchoDerPlacas.Attributes.Add("Tag", "N009-MF000002116");
                ddlAnchoIzqPlacas.Attributes.Add("Tag", "N009-MF000002117");
                ddlPerfilEngrosa.Attributes.Add("Tag", "N009-MF000002120");
                ddlFrenteEngrosa.Attributes.Add("Tag", "N009-MF000002121");
                ddlPerfilCalcificaEngrosa.Attributes.Add("Tag", "N009-MF000002122");
                ddlFrenteCalcificaEngrosa.Attributes.Add("Tag", "N009-MF000002123");
                ddlExtensionEngrosaDer.Attributes.Add("Tag", "N009-MF000002124");
                ddlExtensionEngrosaIzq.Attributes.Add("Tag", "N009-MF000002125");
                ddlAnchoEngrosaDer.Attributes.Add("Tag", "N009-MF000002126");
                ddlAnchoEngrosaIzq.Attributes.Add("Tag", "N009-MF000002127");
                txtOITComentarios.Attributes.Add("Tag", "N009-MF000000589");
                //ddlOITCausas.Attributes.Add("Tag", "N002-MF000000210");
                ChckNinguna.Attributes.Add("Tag", "N009-MF000003196");
                ChckSobreExp.Attributes.Add("Tag", "N009-MF000003197");
                ChckSubExp.Attributes.Add("Tag", "N009-MF000003198");
                ChckPosCent.Attributes.Add("Tag", "N009-MF000003201");
                ChckBajaInsp.Attributes.Add("Tag", "N009-MF000003199");
                ChckEscapula.Attributes.Add("Tag", "N009-MF000003200");
                ChckArtefact.Attributes.Add("Tag", "N009-MF000003202");
                ChckOtros.Attributes.Add("Tag", "N009-MF000003203");
                txtLectVertice.Attributes.Add("Tag", "N009-MF000002501");
                txtLectSenos.Attributes.Add("Tag", "N009-MF000002496");
                txtLectCampos.Attributes.Add("Tag", "N009-MF000002497");
                txtLectMediast.Attributes.Add("Tag", "N009-MF000002498");
                txtLectHilios.Attributes.Add("Tag", "N009-MF000002499");
                txtLectSilueta.Attributes.Add("Tag", "N009-MF000002500");
                chkOITSuperiorDer.Attributes.Add("Tag", "N009-MF000000218");
                chkOITSuperiorIzq.Attributes.Add("Tag", "N009-MF000000219");
                chkOITMedioDer.Attributes.Add("Tag", "N009-MF000000220");
                chkOITMedioIzq.Attributes.Add("Tag", "N009-MF000000221");
                chkOITInferiorDer.Attributes.Add("Tag", "N009-MF000000222");
                chkOITInferiorIzq.Attributes.Add("Tag", "N009-MF000000223");
                chkOIT0_.Attributes.Add("Tag", "N002-MF000000221");
                chkOIT0_0.Attributes.Add("Tag", "N002-MF000000222");
                chkOIT0_1.Attributes.Add("Tag", "N002-MF000000223");
                chkOIT1_0.Attributes.Add("Tag", "N002-MF000000220");
                chkOIT1_1.Attributes.Add("Tag", "N009-MF000000720");
                chkOIT1_2.Attributes.Add("Tag", "N009-MF000000721");
                chkOIT2_1.Attributes.Add("Tag", "N009-MF000000722");
                chkOIT2_2.Attributes.Add("Tag", "N009-MF000000723");
                chkOIT2_3.Attributes.Add("Tag", "N009-MF000000724");
                chOIT3_2.Attributes.Add("Tag", "N009-MF000000725");
                chOIT3_3.Attributes.Add("Tag", "N009-MF000000726");
                chOIT3_.Attributes.Add("Tag", "N009-MF000000727");
                chkOITPrimariap.Attributes.Add("Tag", "N009-MF000000733");
                chkOITPrimarias.Attributes.Add("Tag", "N009-MF000000734");
                chkSecundariap.Attributes.Add("Tag", "N009-MF000000736");
                chkSecundarias.Attributes.Add("Tag", "N009-MF000000737");
                chkOITPrimariaq.Attributes.Add("Tag", "N009-MF000000742");
                chkOITPrimariat.Attributes.Add("Tag", "N009-MF000000744");
                chkOITSecundariaq.Attributes.Add("Tag", "N009-MF000000746");
                chkOITSecundariat.Attributes.Add("Tag", "N009-MF000000748");
                chkOITPrimariar.Attributes.Add("Tag", "N009-MF000000749");
                chkOITPrimariau.Attributes.Add("Tag", "N009-MF000000751");
                chkOITSecundariar.Attributes.Add("Tag", "N009-MF000000753");
                chkOITSecundariau.Attributes.Add("Tag", "N009-MF000000755");
                chkOITOpacidades0.Attributes.Add("Tag", "N009-MF000000756");
                chkOITOpacidadesA.Attributes.Add("Tag", "N009-MF000000757");
                chkOITOpacidadesB.Attributes.Add("Tag", "N009-MF000000758");
                chkOITOpacidadesC.Attributes.Add("Tag", "N009-MF000000759");
                rdoAnormalidadesSI.Attributes.Add("Tag", "N009-MF000003194");
                rdoAnormalidadesNO.Attributes.Add("Tag", "N009-MF000000761");
                rdoSimboloSi.Attributes.Add("Tag", "N009-MF000003195");
                rdoSimboloNo.Attributes.Add("Tag", "N009-MF000000760");
                chkOITaa.Attributes.Add("Tag", "N009-MF000000762");
                chkOITat.Attributes.Add("Tag", "N009-MF000000763");
                chkOITax.Attributes.Add("Tag", "N009-MF000000764");
                chkOITbu.Attributes.Add("Tag", "N009-MF000000765");
                chkOITca.Attributes.Add("Tag", "N009-MF000000766");
                chkOITcg.Attributes.Add("Tag", "N009-MF000000767");
                chkOITcn.Attributes.Add("Tag", "N009-MF000000768");
                chkOITco.Attributes.Add("Tag", "N009-MF000000770");
                chkOITcp.Attributes.Add("Tag", "N009-MF000000771");
                chkOITcv.Attributes.Add("Tag", "N009-MF000000772");
                chkOITdi.Attributes.Add("Tag", "N009-MF000001017");
                chkOITef.Attributes.Add("Tag", "N009-MF000001018");
                chkOITem.Attributes.Add("Tag", "N009-MF000001019");
                chkOITes.Attributes.Add("Tag", "N009-MF000001020");
                chkOITfr.Attributes.Add("Tag", "N009-MF000001022");
                chkOIThi.Attributes.Add("Tag", "N009-MF000001023");
                chkOITho.Attributes.Add("Tag", "N009-MF000001024");
                chkOITid.Attributes.Add("Tag", "N009-MF000001025");
                chkOITih.Attributes.Add("Tag", "N009-MF000001026");
                chkOITkl.Attributes.Add("Tag", "N009-MF000001027");
                chkOITme.Attributes.Add("Tag", "N009-MF000001028");
                chkOITpa.Attributes.Add("Tag", "N009-MF000001030");
                chkOITpb.Attributes.Add("Tag", "N009-MF000001031");
                chkOITpi.Attributes.Add("Tag", "N009-MF000001032");
                chkOITpx.Attributes.Add("Tag", "N009-MF000001033");
                chkOITra.Attributes.Add("Tag", "N009-MF000001034");
                chkOITrp.Attributes.Add("Tag", "N009-MF000001035");
                chkOITtb.Attributes.Add("Tag", "N009-MF000001036");
                chkOITod.Attributes.Add("Tag", "N009-MF000001037");
                txtOITComentariosod.Attributes.Add("Tag", "N009-MF000001039");
                chkSinNeumoconi.Attributes.Add("Tag", "N009-MF000002142");

                int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());
                OperationResult objOperationResult = new OperationResult();
                SystemParameterBL oSystemParameterBL = new SystemParameterBL();
                Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

                //if (ProfesionId != (int)TipoProfesional.Auditor)
                //{
                //    btnGrabarAptitud.Enabled = false;
                //    ddlUsuarioGrabar.Enabled = false;
                //    ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                //}
                //else
                //{
                //    ddlUsuarioGrabar.Enabled = true;
                //    ddlUsuarioGrabar.SelectedValue = "-1";
                //}

                ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                ddlUsuarioGrabar.Enabled = false;

                ddlUsuarioGrabaOIT.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                ddlUsuarioGrabaOIT.Enabled = false;               
            }
            if (chkOIT0_0.Checked == true)
            {
                BlockFormayTamaño(false);
            }
            else
            {
                BlockFormayTamaño(true);
            }
        }

        protected void ChkProfusion_OnCheckedChanged(object sender, EventArgs e)
        {       
                CheckBox chk = (CheckBox)sender;
                if (chk.Checked == true)
                {         
                    chkOIT0_.Checked = false;
                    chkOIT0_0.Checked = false;
                    chkOIT0_1.Checked = false;
                    chkOIT1_0.Checked = false;
                    chkOIT1_1.Checked = false;                           
                    chkOIT1_2.Checked = false;                            
                    chkOIT2_1.Checked = false;                            
                    chkOIT2_2.Checked = false;                            
                    chkOIT2_3.Checked = false;                            
                    chOIT3_2.Checked = false;                           
                    chOIT3_3.Checked = false;                           
                    chOIT3_.Checked = false;                           
                    chk.Checked = true;             
                }                      
            if (chkOIT0_0.Checked == true)                        
            {                           
                BlockFormayTamaño(false);                      
            }                       
            else                       
            {                           
                BlockFormayTamaño(true);                       
            }                         
        }

        protected void BlockFormayTamaño(bool condition) {
            chkOITPrimariap.Enabled = condition;
            chkOITPrimarias.Enabled = condition;
            chkSecundariap.Enabled = condition;
            chkSecundarias.Enabled = condition;
            chkOITPrimariaq.Enabled = condition;
            chkOITPrimariat.Enabled = condition;
            chkOITSecundariaq.Enabled = condition;
            chkOITSecundariat.Enabled = condition;
            chkOITPrimariar.Enabled = condition;
            chkOITPrimariau.Enabled = condition;
            chkOITSecundariar.Enabled = condition;
            chkOITSecundariau.Enabled = condition;
        }

        protected void RbnAnormalidadesPleurales_OnCheckedChanged(object sender, EventArgs e) {
                RadioButton rbn = (RadioButton)sender;
                if (rbn.Checked == true)
                {
                    rdoAnormalidadesSI.Checked = false;
                    rdoAnormalidadesSI.Checked = false;
                    rbn.Checked = true;
                }

                if (rdoAnormalidadesSI.Checked == true)
                {
                    BlockPlacasPleurales(true);
                }
                else if (rdoAnormalidadesNO.Checked == true) 
                {
                    BlockPlacasPleurales(false);
                }                
        }

        protected void BlockPlacasPleurales(bool condition) {
            ddlPerfilPlacaPleurales.Enabled = condition;
            ddlFrentePlacaPleurales.Enabled = condition;
            ddlDiafragmaPlacaPleurales.Enabled = condition;
            ddlOtrosPlacaPleurales.Enabled = condition;
            ddlPerfilCalcifica.Enabled = condition;
            ddlFrenteCalcifica.Enabled = condition;
            ddlDiafragmaCalcifica.Enabled = condition;
            ddlOtrosCalcifica.Enabled = condition;
            ddlExtensionDerPlacas.Enabled = condition;
            ddlExtensionIzqPlacas.Enabled = condition;
            ddlObliAngulo.Enabled = condition;
            ddlAnchoDerPlacas.Enabled = condition;
            ddlAnchoIzqPlacas.Enabled = condition;

            ddlPerfilEngrosa.Enabled = condition;
            ddlFrenteEngrosa.Enabled = condition;
            ddlPerfilCalcificaEngrosa.Enabled = condition;
            ddlFrenteCalcificaEngrosa.Enabled = condition;
            ddlExtensionEngrosaDer.Enabled = condition;
            ddlExtensionEngrosaIzq.Enabled = condition;
            ddlAnchoEngrosaDer.Enabled = condition;
            ddlAnchoEngrosaIzq.Enabled = condition;
        }

        protected void RbnSimbolos_OnCheckedChanged(object sender, EventArgs e) {
            RadioButton rbn = (RadioButton)sender;
            if (rbn.Checked == true)
            {
                rdoSimboloSi.Checked = false;
                rdoSimboloNo.Checked = false;
                rbn.Checked = true;
            }
            if (rdoSimboloSi.Checked==true)
            {
                BlockSimbolos(true);
            }
            else if (rdoSimboloNo.Checked==true)
            {
                BlockSimbolos(false);
            }
        }

        protected void BlockSimbolos(bool condition)
        {
            chkOITaa.Enabled = condition;
            chkOITat.Enabled = condition;
            chkOITax.Enabled = condition;
            chkOITbu.Enabled = condition;
            chkOITca.Enabled = condition;
            chkOITcg.Enabled = condition;
            chkOITcn.Enabled = condition;
            chkOITco.Enabled = condition;
            chkOITcp.Enabled = condition;
            chkOITcv.Enabled = condition;
            chkOITdi.Enabled = condition;
            chkOITef.Enabled = condition;
            chkOITem.Enabled = condition;
            chkOITes.Enabled = condition;
            chkOITfr.Enabled = condition;
            chkOIThi.Enabled = condition;
            chkOITho.Enabled = condition;
            chkOITid.Enabled = condition;
            chkOITih.Enabled = condition;
            chkOITkl.Enabled = condition;
            chkOITme.Enabled = condition;
            chkOITpa.Enabled = condition;
            chkOITpb.Enabled = condition;
            chkOITpi.Enabled = condition;
            chkOITpx.Enabled = condition;
            chkOITra.Enabled = condition;
            chkOITrp.Enabled = condition;
            chkOITtb.Enabled = condition;
            chkOITod.Enabled = condition;           
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
            ddlConsultorio.SelectedValue = "6";
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
            TabRayosX.Hidden = true;
            TabOIT.Hidden = true;

            grdRecomendaciones.DataSource = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();
            grdRecomendaciones.DataBind();

            grdRestricciones.DataSource = new List<Sigesoft.Node.WinClient.BE.RestrictionList>();
            grdRestricciones.DataBind();


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
            txtFechaCabecera.Text = dataKeys[10] == null ? "" : ((DateTime)dataKeys[10]).ToString("ddMMyyyy");
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
                     if (item.ComponentId == TabRayosX.Attributes.GetValue("Tag").ToString())
                     {
                         LoadCombosRayosX();
                         ObtenerDatosRX(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                         TabRayosX.Hidden = true;
                     }
                     else if (item.ComponentId == TabOIT.Attributes.GetValue("Tag").ToString())
                     {
                         LoadCombosOIT();
                         ObtenerDatosOIT(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                         TabOIT.Hidden = false;
                     }
                 }
            }
            else
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());

                var ListaComponenentesConPermiso = (List<string>)Session["ComponentesPermisoLectura"];

                foreach (var item in ListaComponenentesConPermiso)
                {
                    if (item == TabRayosX.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.RX_TORAX_ID);
                        if (Resultado != null)
                        {
                            LoadCombosRayosX();
                            ObtenerDatosRX(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabRayosX.Hidden = true;
                        }
                    }

                    else if (item == TabOIT.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.OIT_ID);
                        if (Resultado != null)
                        {
                            LoadCombosOIT();
                            ObtenerDatosOIT(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabOIT.Hidden = false;
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
            ComponentesId.Add("N002-ME000000032");
            ComponentesId.Add("N009-ME000000062");
            var _objData = _serviceBL.GetAllServices_Consultorio(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1), ComponentesId.ToArray());
                       
            if (_objData.Count == 0)
            {
                TabRayosX.Hidden = true;
                TabOIT.Hidden = true;
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

                if (ctrl is CheckBox)
                {
                    if (((CheckBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((CheckBox)ctrl).Checked = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? false : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                    }
                }

                if (ctrl is RadioButton)
                {
                    if (((RadioButton)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((RadioButton)ctrl).Attributes.GetValue("Tag").ToString();
                        ((RadioButton)ctrl).Checked = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? false : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                    }
                }

                if (ctrl is DatePicker)
                {
                    if (((DatePicker)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        string ComponentFieldId = ((DatePicker)ctrl).Attributes.GetValue("Tag").ToString();
                        var r = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;
                        DateTime? Fecha = null;
                        if (r != "")
                        {
                            Fecha = DateTime.Parse(r.ToString());
                        }
                  
                        ((DatePicker)ctrl).SelectedDate = Fecha;
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

                if (ctrl is  DatePicker)
                {
                    if (((DatePicker)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        var x = ((DatePicker)ctrl);
                        var y = ((DatePicker)ctrl).Attributes.GetValue("Tag");

                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

                        serviceComponentFields.v_ComponentFieldsId = ((DatePicker)ctrl).Attributes.GetValue("Tag").ToString();
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;

                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        serviceComponentFieldValues.v_Value1 = ((DatePicker)ctrl).SelectedDate.ToString();
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

        #region RX

        private void LoadCombosRayosX()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();
      
            Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabar.Enabled = false;
        }

        private void ObtenerDatosRX(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var oExamenRx = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 6);
            Session["ServicioComponentIdRayosX"] = oExamenRx[0].ServicioComponentId;
            var objExamenRx = _serviceBL.GetServiceComponentFields(oExamenRx[0].ServicioComponentId, pServiceId);
            Session["ComponentesRayosX"] = objExamenRx;
            var ComponentesRayosX = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesRayosX"];    
            var ComponentesRx = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesRayosX"];
            if (ComponentesRx.Find(p => p.v_ComponentFieldsId == "N009-MF000000590") != null)
            {
                SearchControlAndLoadData(TabRayosX, Session["ServicioComponentIdRayosX"].ToString(), ComponentesRx);
                #region Campos de Auditoria

                var scId = _serviceBL.ObtenerScId(pServiceId, "N002-ME000000032");

                var datosAuditoria = HistoryBL.CamposAuditoria(oExamenRx[0].ServicioComponentId);
                if (datosAuditoria != null)
                {
                    txtRadiografiaAuditoria.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtRadiografiaAuditoriaInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtRadiografiaAuditoriaModificacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtRadiografiaEvaluacion.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtRadiografiaEvaluacionInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtRadiografiaEvaluacionModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtRadiografiaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtRadiografiaInformadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtRadiografiaInformadorModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                else
                {
                    txtRadiografiaAuditoria.Text = "";
                    txtRadiografiaAuditoriaInsercion.Text = "";
                    txtRadiografiaAuditoriaModificacion.Text = "";

                    txtRadiografiaEvaluacion.Text = "";
                    txtRadiografiaEvaluacionInsercion.Text = "";
                    txtRadiografiaEvaluacionModificacion.Text = "";

                    txtRadiografiaInformador.Text = "";
                    txtRadiografiaInformadorInsercion.Text = "";
                    txtRadiografiaInformadorModificacion.Text = "";

                }

                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabRayosX, _tmpServiceComponentsForBuildMenuList);

                txtRadiografiaAuditoria.Text = "";
                txtRadiografiaAuditoriaInsercion.Text = "";
                txtRadiografiaAuditoriaModificacion.Text = "";

                txtRadiografiaEvaluacion.Text = "";
                txtRadiografiaEvaluacionInsercion.Text = "";
                txtRadiografiaEvaluacionModificacion.Text = "";

                txtRadiografiaInformador.Text = "";
                txtRadiografiaInformadorInsercion.Text = "";
                txtRadiografiaInformadorModificacion.Text = "";
            }

        }

        protected void btnGrabarRayosX_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabar.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }

            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabRayosX, Session["ServicioComponentIdRayosX"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdRayosX"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002134") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkPlacaNormal.Checked ? "1" : "0", "N009-MF000002134", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdRayosX"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdRayosX"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N002-ME000000032";
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

            #region Auditoria

            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N002-ME000000032");

            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtRadiografiaAuditoria.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtRadiografiaAuditoriaInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtRadiografiaAuditoriaModificacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtRadiografiaEvaluacion.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtRadiografiaEvaluacionInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtRadiografiaEvaluacionModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtRadiografiaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtRadiografiaInformadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtRadiografiaInformadorModificacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
            }
            #endregion
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


        #endregion

        #region OIT

        private void LoadCombosOIT()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo165 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 165);
            var Combo166 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 166);
            var Combo158 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 158);
            var Combo159 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 159);
            var Combo172 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 172);

            Utils.LoadDropDownList(ddlOITCalidad, "Value1", "Id", Combo165, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPerfilPlacaPleurales, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFrentePlacaPleurales, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDiafragmaPlacaPleurales, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOtrosPlacaPleurales, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPerfilCalcifica, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFrenteCalcifica, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDiafragmaCalcifica, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOtrosCalcifica, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtensionDerPlacas, "Value1", "Id", Combo159, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtensionIzqPlacas, "Value1", "Id", Combo159, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlObliAngulo, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAnchoDerPlacas, "Value1", "Id", Combo172, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAnchoIzqPlacas, "Value1", "Id", Combo172, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPerfilEngrosa, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFrenteEngrosa, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPerfilCalcificaEngrosa, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFrenteCalcificaEngrosa, "Value1", "Id", Combo158, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtensionEngrosaDer, "Value1", "Id", Combo159, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtensionEngrosaIzq, "Value1", "Id", Combo159, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAnchoEngrosaDer, "Value1", "Id", Combo172, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAnchoEngrosaIzq, "Value1", "Id", Combo172, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlOITCausas, "Value1", "Id", Combo166, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlOITCausas, "Value1", "Id", Combo166, DropDownListAction.Select);
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaOIT, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            ddlUsuarioGrabaOIT.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
          
        }

        private void ObtenerDatosOIT(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();

            var oExamenOIT = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 6);
            Session["ServicioComponentIdOIT"] = oExamenOIT[0].ServicioComponentId;
            var objExamenOIT = _serviceBL.GetServiceComponentFields(oExamenOIT[0].ServicioComponentId, pServiceId);
            Session["ComponentesOIT"] = objExamenOIT;
            var ComponentesOIT = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesOIT"];
            
            if (ComponentesOIT.Find(p => p.v_ComponentFieldsId == "N002-MF000000211") != null)
            {
                SearchControlAndLoadData(TabOIT, Session["ServicioComponentIdOIT"].ToString(), ComponentesOIT);
                #region Campos de Auditoria
                //Obtener scId
                var scId = _serviceBL.ObtenerScId(pServiceId, "N009-ME000000062");
                var datosAuditoria = HistoryBL.CamposAuditoria(scId);
                if (datosAuditoria != null)
                {
                    txtOITAuditoria.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtOITAuditoriaInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtOITAuditoriaModificacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtOITEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtOITEvaluadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtOITEvaluadorActualizador.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }

                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabOIT, _tmpServiceComponentsForBuildMenuList);

                txtOITAuditoria.Text = "";
                txtOITAuditoriaInsercion.Text = "";
                txtOITAuditoriaModificacion.Text = "";

                txtOITEvaluador.Text = "";
                txtOITEvaluadorInsercion.Text = "";
                txtOITEvaluadorActualizador.Text = "";
            }
        }

        protected void btnGrabarOIT_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabaOIT.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }

            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
       
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabOIT, Session["ServicioComponentIdOIT"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdOIT"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002142") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkSinNeumoconi.Checked ? "1" : "0", "N009-MF000002142", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdOIT"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdOIT"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N009-ME000000062";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabaOIT.SelectedValue.ToString());

            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                 l,
                                                 serviceComponentDto,
                                                 ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                 true);

            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N009-ME000000062");


            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtOITAuditoria.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtOITAuditoriaInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtOITAuditoriaModificacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtOITEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtOITEvaluadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtOITEvaluadorActualizador.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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
            var ListaDx = _serviceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, ServiceId).FindAll(p => p.v_ComponentName =="RAYOS X");
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

        protected void fileDocRX_FileSelected(object sender, EventArgs e)
        {
            string Ruta = WebConfigurationManager.AppSettings["ImgRxOrigen"].ToString();
            string Dni = Session["DniTrabajador"].ToString();
            string Fecha = Session["FechaServicio"].ToString();
            string Consultorio = "ConsultorioRX";
            string Ext = fileDocRX.FileName.Substring(fileDocRX.FileName.Length - 3, 3);
            fileDocRX.SaveAs(Ruta + Dni + "-" + Fecha + "-" + Consultorio + "." + Ext);
            Alert.ShowInTop("El archivo subió correctamente", MessageBoxIcon.Information);
        }

        protected void FileInforme_FileSelected(object sender, EventArgs e)
        {
            string Ruta = WebConfigurationManager.AppSettings["ImgRxInforme"];
            string Dni = Session["DniTrabajador"].ToString();
            string Fecha = Session["FechaServicio"].ToString();
            string Consultorio = "ConsultorioRX";
            string Ext = FileInforme.FileName.Substring(FileInforme.FileName.Length - 3, 3);
            FileInforme.SaveAs(Ruta + Dni + "-" + Fecha + "-" + Consultorio + "." + Ext);
            Alert.ShowInTop("El archivo subió correctamente", MessageBoxIcon.Information);
        }
        

        protected void fileDocOIT_FileSelected(object sender, EventArgs e)
        {
            string Ruta = WebConfigurationManager.AppSettings["ImgOITOrigen"].ToString();
            string Dni = Session["DniTrabajador"].ToString();
            string Fecha = Session["FechaServicio"].ToString();
            string Consultorio = "ConsultorioOIT";
            string Ext = fileDocOIT.FileName.Substring(fileDocOIT.FileName.Length - 3, 3);
            fileDocOIT.SaveAs(Ruta + Dni + "-" + Fecha + "-" + Consultorio + "." + Ext);
            Alert.ShowInTop("El archivo subió correctamente", MessageBoxIcon.Information);
        }

        protected void lnkRayosx_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000032";

            GenerateRX(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N002-ME000000032.pdf", path + ".pdf");
        }

        private void GenerateRX(string _ruta, string p)
        {
            var RX_TORAX_ID = new ServiceBL().ReportRadiologico(p, Constants.RX_TORAX_ID);
            dsGetRepo = new DataSet();

            DataTable dt_RX_TORAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(RX_TORAX_ID);

            dt_RX_TORAX_ID.TableName = "dtRadiologico";

            dsGetRepo.Tables.Add(dt_RX_TORAX_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiologico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.RX_TORAX_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

        protected void lnkOIT_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N009-ME000000062";

            GenerateOIT(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N009-ME000000062.pdf", path + ".pdf");
        }

        private void GenerateOIT(string _ruta, string p)
        {
            var OIT_ID = new ServiceBL().ReportInformeRadiografico(p, Constants.OIT_ID);

            dsGetRepo = new DataSet();
            DataTable dt_OIT_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OIT_ID);
            dt_OIT_ID.TableName = "dtInformeRadiografico";
            dsGetRepo.Tables.Add(dt_OIT_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiograficoOIT();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.OIT_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

              
       
    }
}