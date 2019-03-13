using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FineUI;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BE.Custom;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.UI.ExternalUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI.WebControls.Expressions;
using System.Web.UI.WebControls.WebParts;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmMedicina : System.Web.UI.Page
    {
        ServiceBL _serviceBL = new ServiceBL();
        HistoryBL _HistoryBL = new HistoryBL();
        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        FileInfoDto fileInfo = null;
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = new List<Node.WinClient.BE.ServiceComponentFieldsList>();
        string _ruta;
        DataSet dsGetRepo = null;
        ReportDocument rp;
        DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
        private Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _tmpTotalDiagnostic = null;
        List<string> _filesNameToMerge = new List<string>();
        List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _listMedicina = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
        ServiceComponentMultimediaFile _ServiceComponentMultimediaFile = new ServiceComponentMultimediaFile();
        public string cadenaUrlFoto = null;
        public string JsFunction { get; set; }
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
                btnAlturaReporte.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=AlturaEstructural");
                btnNuevoAntecedenteOcupacional.OnClientClick = WindowAddAntecedenteOcupacional.GetSaveStateReference(hfRefresh.ClientID) + WindowAddAntecedenteOcupacional.GetShowReference("../Consultorios/frmAntecedenteOcupacional.aspx?Mode=New");
                btnNuevoAntecedentePersonal.OnClientClick = WindowAddAntecedentePersonal.GetSaveStateReference(hfRefresh.ClientID) + WindowAddAntecedentePersonal.GetShowReference("../Consultorios/frmAntecedentePersonal.aspx?Mode=New");

                btnAgregarAntecedenteFamiliar.OnClientClick = WindowAddAntecedenteFamiliar.GetSaveStateReference(hfRefresh.ClientID) + WindowAddAntecedenteFamiliar.GetShowReference("../Consultorios/frmAntecedenteFamiliar.aspx?Mode=New");
                btnAgregarHabitoNocivo.OnClientClick = WindowAddHabitoNocivo.GetSaveStateReference(hfRefresh.ClientID) + WindowAddHabitoNocivo.GetShowReference("../Consultorios/frmHabitosNocivos.aspx?Mode=New");
                
                btnReporte312.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=312");
                btnReporteAltura18C.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=AlturaCI");
                btnReporteOsteo.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=OsteoCI");
                btnReporteOsteoCI.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=OsteoCI");
                btnReporteTamizajeCI.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=TamizajeCI");
                btn7D_CI.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=7D");
                btnReporteSintomatico.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Sintomatico");
                btnReporte16.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Anexo16");

                btnCertificadoAptitud.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Certificado");
                btnInformeCI.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=InformeCI");
                btnAddRecomendacion.OnClientClick = winEditReco.GetSaveStateReference(hfRefresh.ClientID) + winEditReco.GetShowReference("../Auditar/FRM033B.aspx?Mode=New");
                btnAddRestriccion.OnClientClick = winEditRestri.GetSaveStateReference(hfRefresh.ClientID) + winEditRestri.GetShowReference("../Auditar/FRM033E.aspx?Mode=New");
            

                btnNewDiagnosticos.OnClientClick = WindowAddDX.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDX.GetShowReference("../Auditar/FRM033C.aspx?Mode=New");
                btnNewDiagnosticosFrecuente.OnClientClick = WindowAddDXFrecuente.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDXFrecuente.GetShowReference("../Auditar/FRM033G.aspx?Mode=New");
                int RoleId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString());
                btnDescargar.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("DescargarAdjunto.aspx?Consultorio=312");
                btnCambiarFechaServicio.OnClientClick = WinFechaServicio.GetSaveStateReference(hfRefresh.ClientID) + WinFechaServicio.GetShowReference("../Consultorios/FRMCAMBIOFECHASERVICIO.aspx");
                var ComponentesPermisoLectura = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId(9, RoleId).FindAll(p => p.i_Read == 1);
                List<string> ListaComponentesPermisoLectura = new List<string>();
                foreach (var item in ComponentesPermisoLectura)
                {
                    ListaComponentesPermisoLectura.Add(item.v_ComponentId);
                }
                Session["ComponentesPermisoLectura"] = ListaComponentesPermisoLectura;

                TabAnexo312.Hidden = true;
                TabFototipo.Hidden = true;
                TabAnexo16.Hidden = true;
                TabOsteomuscular.Hidden = true;
                TabOsteomuscularInternacional.Hidden = true;
                TabAlturaEstructural.Hidden = true;
                TabAltura18_Internacional.Hidden = true;
                Tab7D.Hidden = true;
                TabSintomaticoRespiratorio.Hidden = true;
                TabTamizajeDermatologico.Hidden = true;
                TabDermatologico_Internacional.Hidden = true;

                TabAnexo312.Attributes.Add("Tag", "N002-ME000000022");

                txtTalla.Attributes.Add("Tag", "N002-MF000000007"); 
                txtPeso.Attributes.Add("Tag", "N002-MF000000008");
                txtImc.Attributes.Add("Tag", "N002-MF000000009"); 
                txtIcc.Attributes.Add("Tag", "N002-MF000000012"); 
                txtfres.Attributes.Add("Tag", "N002-MF000000005");
                txtFcar.Attributes.Add("Tag", "N002-MF000000003"); 
                txtParterial.Attributes.Add("Tag", "N002-MF000000001");
                txtParterialDiatolica.Attributes.Add("Tag", "N002-MF000000002");
                txtTemp.Attributes.Add("Tag", "N002-MF000000004");
                txtPcadera.Attributes.Add("Tag", "N002-MF000000011");
                txtPadb.Attributes.Add("Tag", "N002-MF000000010"); 
                txtGcorporal.Attributes.Add("Tag", "N002-MF000000013");
                txtSatO2.Attributes.Add("Tag", "N002-MF000000006");

                ddlPiel.Attributes.Add("Tag", "N002-MF000000131");
                txtHallazgoPiel.Attributes.Add("Tag", "N009-MF000000601");
                ddlCabello.Attributes.Add("Tag", "N002-MF000000132");
                txtHallazgoCabello.Attributes.Add("Tag", "N009-MF000000602");
                ddlOjosAnexos.Attributes.Add("Tag", "N002-MF000000122");
                txtOjosAnexos.Attributes.Add("Tag", "N009-MF000000604");
                ddlOidos.Attributes.Add("Tag", "N002-MF000000121");
                txtOidos.Attributes.Add("Tag", "N009-MF000000603");
                ddlNariz.Attributes.Add("Tag", "N002-MF000000129");
                txtNariz.Attributes.Add("Tag", "N009-MF000000605");
                ddlBoca.Attributes.Add("Tag", "N002-MF000000136");
                txtBoca.Attributes.Add("Tag", "N009-MF000000606");
                ddlFaringe.Attributes.Add("Tag", "N002-MF000000126");
                txtFaringe.Attributes.Add("Tag", "N009-MF000000607");
                ddlCuello.Attributes.Add("Tag", "N002-MF000000135");
                txtCuello.Attributes.Add("Tag", "N009-MF000000608");
                ddlApaRespiratorio.Attributes.Add("Tag", "N002-MF000000123");
                txtApaRespira.Attributes.Add("Tag", "N009-MF000000609");
                ddlApaCardiovascular.Attributes.Add("Tag", "N002-MF000000127");
                txtApaCardiovas.Attributes.Add("Tag", "N009-MF000000610");
                ddlApaDigestivo.Attributes.Add("Tag", "N002-MF000000133");
                txtApaDigestivo.Attributes.Add("Tag", "N009-MF000000611");
                ddlApaGenito.Attributes.Add("Tag", "N002-MF000000124");
                txtApaGenito.Attributes.Add("Tag", "N009-MF000000612");
                ddlApaLocomotor.Attributes.Add("Tag", "N002-MF000000137");
                txtApaLocomotor.Attributes.Add("Tag", "N009-MF000000613");
                ddlMarcha.Attributes.Add("Tag", "N002-MF000000125");
                txtMarcha.Attributes.Add("Tag", "N009-MF000000614");
                ddlColumna.Attributes.Add("Tag", "N002-MF000000128");
                txtColumna.Attributes.Add("Tag", "N009-MF000000615");
                ddlExtreSuper.Attributes.Add("Tag", "N002-MF000000130");
                txtExtreSuperio.Attributes.Add("Tag", "N009-MF000000616");
                ddlExtreInfe.Attributes.Add("Tag", "N002-MF000000134");
                txtExtreInfer.Attributes.Add("Tag", "N009-MF000000617");
                ddlLinfaticos.Attributes.Add("Tag", "N002-MF000000176");
                txtLinfaticos.Attributes.Add("Tag", "N009-MF000000618");
                ddlSistNerviso.Attributes.Add("Tag", "N002-MF000000177");
                txtSistemaNervisos.Attributes.Add("Tag", "N009-MF000000619");
                ddlEctoscopia.Attributes.Add("Tag", "N009-MF000000003");
                txtExtoscopia.Attributes.Add("Tag", "N009-MF000000620");
                ddlEstadoMental.Attributes.Add("Tag", "N009-MF000000624");
                txtEstadoMental.Attributes.Add("Tag", "N009-MF000000625");
                txtResumen.Attributes.Add("Tag", "N002-MF000000138");
                chkPersonaSana.Attributes.Add("Tag", "N009-MF000002135");





                
                TabAnexo16.Attributes.Add("Tag", "N009-ME000000052");
                txtTalla_16.Attributes.Add("Tag", "N002-MF000000007");
                txtPeso_16.Attributes.Add("Tag", "N002-MF000000008");
                txtImc_16.Attributes.Add("Tag", "N002-MF000000009");
                txtIcc_16.Attributes.Add("Tag", "N002-MF000000012");
                txtfres_16.Attributes.Add("Tag", "N002-MF000000005");
                txtFcar_16.Attributes.Add("Tag", "N002-MF000000003");
                txtParterial_16.Attributes.Add("Tag", "N002-MF000000001");
                txtPresionArterialDiastolica.Attributes.Add("Tag", "N002-MF000000002");
                txtTemp_16.Attributes.Add("Tag", "N002-MF000000004");
                txtPcadera_16.Attributes.Add("Tag", "N002-MF000000011");
                txtPadb_16.Attributes.Add("Tag", "N002-MF000000010");
                txtGcorporal_16.Attributes.Add("Tag", "N002-MF000000013");
                txtSatO2_16.Attributes.Add("Tag", "N002-MF000000006");

                ddlCabeza_16.Attributes.Add("Tag", "N009-MF000000626");
                txtHallazgoCabeza_16.Attributes.Add("Tag", "N009-MF000000687");
                ddlCuello_16.Attributes.Add("Tag", "N009-MF000000627");
                txtCuello_16.Attributes.Add("Tag", "N009-MF000000688");
                ddlNariz_16.Attributes.Add("Tag", "N009-MF000000628");
                txtNariz_16.Attributes.Add("Tag", "N009-MF000000689");
                ddlBAFL_16.Attributes.Add("Tag", "N009-MF000000629");
                txtBAFL_16.Attributes.Add("Tag", "N009-MF000000690");
                ddlReflejosPupilares_16.Attributes.Add("Tag", "N009-MF000000648");
                txtReflejosPupilares_16.Attributes.Add("Tag", "N009-MF000000691");
                ddlExtreSuper_16.Attributes.Add("Tag", "N009-MF000000649");
                txtExtreSuper_16.Attributes.Add("Tag", "N009-MF000000692");
                ddlExtreInfe_16.Attributes.Add("Tag", "N009-MF000000650");
                txtExtreInfe_16.Attributes.Add("Tag", "N009-MF000000693");
                ddlReflejosOsteo_16.Attributes.Add("Tag", "N009-MF000000651");
                txtReflejosOsteo_16.Attributes.Add("Tag", "N009-MF000000694");
                ddlMarcha_16.Attributes.Add("Tag", "N009-MF000000652");
                txtMarcha_16.Attributes.Add("Tag", "N009-MF000000695");
                ddlColumnaVertebral_16.Attributes.Add("Tag", "N009-MF000000653");
                txtColumnaVertebral_16.Attributes.Add("Tag", "N009-MF000000696");
                ddlAbdomen_16.Attributes.Add("Tag", "N009-MF000000654");
                txtAbdomen_16.Attributes.Add("Tag", "N009-MF000000697");
                ddlAnillosAngui_16.Attributes.Add("Tag", "N009-MF000000655");
                txtAnillosAngui_16.Attributes.Add("Tag", "N009-MF000000698");
                ddlHernias_16.Attributes.Add("Tag", "N009-MF000000656");
                txtHernias_16.Attributes.Add("Tag", "N009-MF000000699");
                ddlVarices_16.Attributes.Add("Tag", "N009-MF000000657");
                txtVarices_16.Attributes.Add("Tag", "N009-MF000000700");
                ddlOrganosGenitales_16.Attributes.Add("Tag", "N009-MF000000658");
                txtOrganosGenitales_16.Attributes.Add("Tag", "N009-MF000000701");
                ddlGanglios_16.Attributes.Add("Tag", "N009-MF000000659");
                txtGanglios_16.Attributes.Add("Tag", "N009-MF000000702");
                rdoPulmonNormal.Attributes.Add("Tag", "N009-MF000000660");
                rdoPulmonAnormal.Attributes.Add("Tag", "N009-MF000000661");
                txtPulmonDescripcion.Attributes.Add("Tag", "N009-MF000000662");
                rdoTactoRectalNormal.Attributes.Add("Tag", "N009-MF000000663");
                rdoTactoRectalAnormal.Attributes.Add("Tag", "N009-MF000000664");
                txtTactoRectalDescripcion.Attributes.Add("Tag", "N009-MF000000665");
                rdoTactoRectalNoRealizo.Attributes.Add("Tag", "N009-MF000000666");
                txtResumen_16.Attributes.Add("Tag", "N009-MF000000703");
                chkPersonaSana_16.Attributes.Add("Tag", "N009-MF000002137");
                //chkruido.Attributes.Add("Tag", "N009-MF000000667");
                //chkcancerigenos.Attributes.Add("Tag", "N009-MF000000668");
                //chktemperaturas.Attributes.Add("Tag", "N009-MF000000669");
                //chkcargas.Attributes.Add("Tag", "N009-MF000000670");
                //chkpolvo.Attributes.Add("Tag", "N009-MF000000671");
                //chkmutagenicos.Attributes.Add("Tag", "N009-MF000000672");
                //chkbiologicos.Attributes.Add("Tag", "N009-MF000000673");
                //chkmov_repet.Attributes.Add("Tag", "N009-MF000000674");
                //chkvib_segmentaria.Attributes.Add("Tag", "N009-MF000000675");
                //chksolventes.Attributes.Add("Tag", "N009-MF000000676");
                //chkposturas.Attributes.Add("Tag", "N009-MF000000677");
                //chkpantalla_pvd.Attributes.Add("Tag", "N009-MF000000678");
                //chkvib_total.Attributes.Add("Tag", "N009-MF000000679");
                //chkmetal_pesado.Attributes.Add("Tag", "N009-MF000000683");
                //chkturnos.Attributes.Add("Tag", "N009-MF000000684");
                //chkotros.Attributes.Add("Tag", "N009-MF000000685");
                //txtdescribir_otros.Attributes.Add("Tag", "N009-MF000000686");




                TabOsteomuscular.Attributes.Add("Tag", "N002-ME000000046");
                ddlManiLevanCargas.Attributes.Add("Tag", "N009-MF000001226");
                ddlPRFManiLevanCargas.Attributes.Add("Tag", "N009-MF000001221");

                ddlManiEmpujarCargas.Attributes.Add("Tag", "N009-MF000001227");
                ddlPRFManiEmpujarCargas.Attributes.Add("Tag", "N009-MF000001222");

                ddlManiJalarCargas.Attributes.Add("Tag", "N009-MF000001220");
                ddlPRFManiJalarCargas.Attributes.Add("Tag", "N009-MF000001224");

                ddlPesoMayor25.Attributes.Add("Tag", "N009-MF000001225");
                ddlPRFPesoMayor25.Attributes.Add("Tag", "N009-MF000001223");

                ddlEncimaHombro.Attributes.Add("Tag", "N009-MF000000048");
                ddlPRFEncimaHombro.Attributes.Add("Tag", "N009-MF000001228");

                ddlManiValvulas.Attributes.Add("Tag", "N009-MF000001229");
                ddlPRFManiValvulas.Attributes.Add("Tag", "N009-MF000001230");

                ddlMoviRepetitivos.Attributes.Add("Tag", "N009-MF000001415");
                ddlPRFMoviRepetitivos.Attributes.Add("Tag", "N009-MF000001416");

                ddlMoviForzada.Attributes.Add("Tag", "N009-MF000001412");
                ddlPRFMoviForzada.Attributes.Add("Tag", "N009-MF000001413");

                ddlPosturaForzada.Attributes.Add("Tag", "N009-MF000001414");
                ddlPRFPosturaForzada.Attributes.Add("Tag", "N009-MF000000540");

                txtsintomas.Attributes.Add("Tag", "N009-MF000000549");

                ddlHD1.Attributes.Add("Tag", "N009-MF000000836");
                ddlHD2.Attributes.Add("Tag", "N009-MF000000835");
                ddlHD3.Attributes.Add("Tag", "N009-MF000000838");
                ddlHD4.Attributes.Add("Tag", "N009-MF000000843");
                ddlHD5.Attributes.Add("Tag", "N009-MF000000840");
                ddlHD6.Attributes.Add("Tag", "N009-MF000000845");
                ddlHD7.Attributes.Add("Tag", "N009-MF000000842");
                ddlHD8.Attributes.Add("Tag", "N009-MF000000841");

                ddlHI1.Attributes.Add("Tag", "N009-MF000000844");
                ddlHI2.Attributes.Add("Tag", "N009-MF000000850");
                ddlHI3.Attributes.Add("Tag", "N009-MF000001908");
                ddlHI4.Attributes.Add("Tag", "N009-MF000001909");
                ddlHI5.Attributes.Add("Tag", "N009-MF000001910");
                ddlHI6.Attributes.Add("Tag", "N009-MF000001911");
                ddlHI7.Attributes.Add("Tag", "N009-MF000001912");
                ddlHI8.Attributes.Add("Tag", "N009-MF000001913");

                ddlCD1.Attributes.Add("Tag", "N009-MF000001914");
                ddlCD2.Attributes.Add("Tag", "N009-MF000001915");
                ddlCD3.Attributes.Add("Tag", "N009-MF000001916");
                ddlCD4.Attributes.Add("Tag", "N009-MF000001917");
                ddlCD5.Attributes.Add("Tag", "N009-MF000001918");
                ddlCD6.Attributes.Add("Tag", "N009-MF000001919");
                ddlCD7.Attributes.Add("Tag", "N009-MF000001921");
                ddlCD8.Attributes.Add("Tag", "N009-MF000001920");

                ddlCI1.Attributes.Add("Tag", "N009-MF000001922");
                ddlCI2.Attributes.Add("Tag", "N009-MF000001923");
                ddlCI3.Attributes.Add("Tag", "N009-MF000001924");
                ddlCI4.Attributes.Add("Tag", "N009-MF000001925");
                ddlCI5.Attributes.Add("Tag", "N009-MF000001926");
                ddlCI6.Attributes.Add("Tag", "N009-MF000001927");
                ddlCI7.Attributes.Add("Tag", "N009-MF000001928");
                ddlCI8.Attributes.Add("Tag", "N009-MF000001929");

                ddlMuneD1.Attributes.Add("Tag", "N009-MF000001930");
                ddlMuneD2.Attributes.Add("Tag", "N009-MF000001931");
                ddlMuneD3.Attributes.Add("Tag", "N009-MF000001932");
                ddlMuneD4.Attributes.Add("Tag", "N009-MF000001933");
                ddlMuneD5.Attributes.Add("Tag", "N009-MF000001934");
                ddlMuneD6.Attributes.Add("Tag", "N009-MF000001935");
                ddlMuneD7.Attributes.Add("Tag", "N009-MF000001936");
                ddlMuneD8.Attributes.Add("Tag", "N009-MF000001937");

                ddlMuneI1.Attributes.Add("Tag", "N009-MF000001938");
                ddlMuneI2.Attributes.Add("Tag", "N009-MF000001939");
                ddlMuneI3.Attributes.Add("Tag", "N009-MF000001940");
                ddlMuneI4.Attributes.Add("Tag", "N009-MF000001941");
                ddlMuneI5.Attributes.Add("Tag", "N009-MF000001942");
                ddlMuneI6.Attributes.Add("Tag", "N009-MF000001943");
                ddlMuneI7.Attributes.Add("Tag", "N009-MF000001944");
                ddlMuneI8.Attributes.Add("Tag", "N009-MF000001945");

                ddlCaderaD1.Attributes.Add("Tag", "N009-MF000001946");
                ddlCaderaD2.Attributes.Add("Tag", "N009-MF000001947");
                ddlCaderaD3.Attributes.Add("Tag", "N009-MF000001948");
                ddlCaderaD4.Attributes.Add("Tag", "N009-MF000001949");
                ddlCaderaD5.Attributes.Add("Tag", "N009-MF000001950");
                ddlCaderaD6.Attributes.Add("Tag", "N009-MF000001951");
                ddlCaderaD7.Attributes.Add("Tag", "N009-MF000001952");
                ddlCaderaD8.Attributes.Add("Tag", "N009-MF000001953");

                ddlCaderaI1.Attributes.Add("Tag", "N009-MF000001954");
                ddlCaderaI2.Attributes.Add("Tag", "N009-MF000001955");
                ddlCaderaI3.Attributes.Add("Tag", "N009-MF000001956");
                ddlCaderaI4.Attributes.Add("Tag", "N009-MF000001959");
                ddlCaderaI5.Attributes.Add("Tag", "N009-MF000001957");
                ddlCaderaI6.Attributes.Add("Tag", "N009-MF000001958");
                ddlCaderaI7.Attributes.Add("Tag", "N009-MF000001960");
                ddlCaderaI8.Attributes.Add("Tag", "N009-MF000001961");

                ddlRodillaD1.Attributes.Add("Tag", "N009-MF000001962");
                ddlRodillaD2.Attributes.Add("Tag", "N009-MF000001963");
                ddlRodillaD3.Attributes.Add("Tag", "N009-MF000001964");
                ddlRodillaD4.Attributes.Add("Tag", "N009-MF000001965");
                ddlRodillaD5.Attributes.Add("Tag", "N009-MF000001966");
                ddlRodillaD6.Attributes.Add("Tag", "N009-MF000001967");
                ddlRodillaD7.Attributes.Add("Tag", "N009-MF000001968");
                ddlRodillaD8.Attributes.Add("Tag", "N009-MF000001969");

                ddlRodillaI1.Attributes.Add("Tag", "N009-MF000001970");
                ddlRodillaI2.Attributes.Add("Tag", "N009-MF000001971");
                ddlRodillaI3.Attributes.Add("Tag", "N009-MF000001972");
                ddlRodillaI4.Attributes.Add("Tag", "N009-MF000001973");
                ddlRodillaI5.Attributes.Add("Tag", "N009-MF000001974");
                ddlRodillaI6.Attributes.Add("Tag", "N009-MF000001975");
                ddlRodillaI7.Attributes.Add("Tag", "N009-MF000001976");
                ddlRodillaI8.Attributes.Add("Tag", "N009-MF000001977");

                ddlTobilloD1.Attributes.Add("Tag", "N009-MF000001978");
                ddlTobilloD2.Attributes.Add("Tag", "N009-MF000001979");
                ddlTobilloD3.Attributes.Add("Tag", "N009-MF000001980");
                ddlTobilloD4.Attributes.Add("Tag", "N009-MF000001981");
                ddlTobilloD5.Attributes.Add("Tag", "N009-MF000001982");
                ddlTobilloD6.Attributes.Add("Tag", "N009-MF000001983");
                ddlTobilloD7.Attributes.Add("Tag", "N009-MF000001984");
                ddlTobilloD8.Attributes.Add("Tag", "N009-MF000001985");

                ddlTobilloI1.Attributes.Add("Tag", "N009-MF000001986");
                ddlTobilloI2.Attributes.Add("Tag", "N009-MF000001987");
                ddlTobilloI3.Attributes.Add("Tag", "N009-MF000001988");
                ddlTobilloI4.Attributes.Add("Tag", "N009-MF000001990");
                ddlTobilloI5.Attributes.Add("Tag", "N009-MF000001989");
                ddlTobilloI6.Attributes.Add("Tag", "N009-MF000001991");
                ddlTobilloI7.Attributes.Add("Tag", "N009-MF000001992");
                ddlTobilloI8.Attributes.Add("Tag", "N009-MF000001993");

                txtobservacionOsteo1.Attributes.Add("Tag", "N009-MF000001994");

                ddlLasegueDere.Attributes.Add("Tag", "N009-MF000000064");
                ddlLasegueIzq.Attributes.Add("Tag", "N009-MF000000065");
                ddladam_derecho.Attributes.Add("Tag", "N009-MF000000144");
                ddladam_izquierdo.Attributes.Add("Tag", "N009-MF000000536");
                ddlphalen_derecho.Attributes.Add("Tag", "N009-MF000000059");
                ddlphalen_izquierdo.Attributes.Add("Tag", "N009-MF000000060");
                ddltinel_derecho.Attributes.Add("Tag", "N009-MF000000062");
                ddltinel_izquierdo.Attributes.Add("Tag", "N009-MF000000063");
                ddlfinkelstein_derecho.Attributes.Add("Tag", "N009-MF000000066");
                ddlfinkelstein_izquierdo.Attributes.Add("Tag", "N009-MF000000051");
                ddlpie_cavo_derecho.Attributes.Add("Tag", "N009-MF000000546");
                ddlpie_cavo_izquierdo.Attributes.Add("Tag", "N009-MF000000547");
                ddlpie_plano_derecho.Attributes.Add("Tag", "N009-MF000000548");
                ddlpie_plano_izquierdo.Attributes.Add("Tag", "N009-MF000000544");

                ddlcervical1.Attributes.Add("Tag", "N009-MF000001995");
                ddldorsal1.Attributes.Add("Tag", "N009-MF000000551");
                ddllumbar1.Attributes.Add("Tag", "N009-MF000000541");

                ddldorsal2.Attributes.Add("Tag", "N009-MF000000542");
                ddllumbar2.Attributes.Add("Tag", "N009-MF000000543");

                ddlcervical2.Attributes.Add("Tag", "N009-MF000000545");
                ddlcervical_extenxion.Attributes.Add("Tag", "N009-MF000000052");
                ddlcervical_lateralizacion_derecha.Attributes.Add("Tag", "N009-MF000000537");
                ddlcervical_lateralizacion_izquierda.Attributes.Add("Tag", "N009-MF000000538");
                ddlcervical_rotacion_derecha.Attributes.Add("Tag", "N009-MF000000055");
                ddlcervical_rotacion_izquierda.Attributes.Add("Tag", "N009-MF000000056");
                ddlcervical_irradiacion.Attributes.Add("Tag", "N009-MF000000057");

                ddldorsallumbar.Attributes.Add("Tag", "N009-MF000000053");
                ddldorsallumbar_extension.Attributes.Add("Tag", "N009-MF000000054");
                ddldorsallumbar_lateral_derecha.Attributes.Add("Tag", "N009-MF000000058");
                ddldorsallumbar_lateral_izquierda.Attributes.Add("Tag", "N009-MF000000061");
                ddldorsallumbar_roacion_derecha.Attributes.Add("Tag", "N009-MF000000172");
                ddldorsallumbar_roacion_izquierda.Attributes.Add("Tag", "N009-MF000000833");
                ddldorsallumbar_irradiacion.Attributes.Add("Tag", "N009-MF000000174");

                //chkColumnaCervicalApofisis.Attributes.Add("Tag", "N009-MF000000832");
               ddlColumnaCervicalApofisis.Attributes.Add("Tag", "N009-MF000000832");
                //chkColumnaCervicalContractura.Attributes.Add("Tag", "N009-MF000000166");
               ddlColumnaCervicalContractura.Attributes.Add("Tag", "N009-MF000000166");
                //chkColumnaDorsalApofisis.Attributes.Add("Tag", "N009-MF000000171");
               ddlColumnaDorsalApofisis.Attributes.Add("Tag", "N009-MF000000171");
                //chkColumnaDorsalContractura.Attributes.Add("Tag", "N009-MF000000169");
               ddlColumnaDorsalContractura.Attributes.Add("Tag", "N009-MF000000169");
                //chkColumnaLumbarApofisis.Attributes.Add("Tag", "N009-MF000000167");
               ddlColumnaLumbarApofisis.Attributes.Add("Tag", "N009-MF000000167");
               // chkColumnaLumbarContractura.Attributes.Add("Tag", "N009-MF000000170"); 
               ddlColumnaLumbarContractura.Attributes.Add("Tag", "N009-MF000000170"); 
                ddlaptitudOsteo.Attributes.Add("Tag", "N009-MF000000621");

                chkevaluacion_normal.Attributes.Add("Tag", "N009-MF000002136");
                txtdescripcion.Attributes.Add("Tag", "N009-MF000000232");


                rbAbdomen1.Attributes.Add("Tag", "N009-OTS00000001");
                rbAbdomen2.Attributes.Add("Tag", "N009-OTS00000001");
                rbAbdomen3.Attributes.Add("Tag", "N009-OTS00000001");
                rbAbdomen4.Attributes.Add("Tag", "N009-OTS00000001");
                txtAbdomenPuntos.Attributes.Add("Tag", "N009-OTS00000002");
                txtAbdomenObs.Attributes.Add("Tag", "N009-OTS00000003");

                rbCadera1.Attributes.Add("Tag", "N009-OTS00000004");
                rbCadera2.Attributes.Add("Tag", "N009-OTS00000004");
                rbCadera3.Attributes.Add("Tag", "N009-OTS00000004");
                rbCadera4.Attributes.Add("Tag", "N009-OTS00000004");
                txtCaderaPuntos.Attributes.Add("Tag", "N009-OTS00000005");
                txtCaderaObs.Attributes.Add("Tag", "N009-OTS00000006");

                rbMuslo1.Attributes.Add("Tag", "N009-OTS00000007");
                rbMuslo2.Attributes.Add("Tag", "N009-OTS00000007");
                rbMuslo3.Attributes.Add("Tag", "N009-OTS00000007");
                rbMuslo4.Attributes.Add("Tag", "N009-OTS00000007");
                txtMusloPuntos.Attributes.Add("Tag", "N009-OTS00000008");
                txtMusloObs.Attributes.Add("Tag", "N009-OTS00000009");

                rbLateral1.Attributes.Add("Tag", "N009-OTS00000010");
                rbLateral2.Attributes.Add("Tag", "N009-OTS00000010");
                rbLateral3.Attributes.Add("Tag", "N009-OTS00000010");
                rbLateral4.Attributes.Add("Tag", "N009-OTS00000010");
                txtLateralPuntos.Attributes.Add("Tag", "N009-OTS00000011");
                txtLateralObs.Attributes.Add("Tag", "N009-OTS00000012");

                rbHombroA180_1.Attributes.Add("Tag", "N009-OTS00000013");
                rbHombroA180_2.Attributes.Add("Tag", "N009-OTS00000013");
                rbHombroA180_3.Attributes.Add("Tag", "N009-OTS00000013");

                txtAbduccionHombro180Puntos.Attributes.Add("Tag", "N009-OTS00000014");
                chkHombro180SI.Attributes.Add("Tag", "N009-OTS00000015");
                //rbAbduccion180DolorNO.Attributes.Add("N009-OTS00000015");

                rbHombroB1801_1.Attributes.Add("Tag", "N009-OTS00000016");
                rbHombroB1801_2.Attributes.Add("Tag", "N009-OTS00000016");
                rbHombroB1801_3.Attributes.Add("Tag", "N009-OTS00000016");

                txtAduccionHombro60Puntos.Attributes.Add("Tag", "N009-OTS00000017");
                chkHombro60SI.Attributes.Add("Tag", "N009-OTS00000018");
                //rbAbduccion60DolorNO.Attributes.Add("N009-OTS00000018");

                rbHombro90_1.Attributes.Add("Tag", "N009-OTS00000019");
                rbHombro90_2.Attributes.Add("Tag", "N009-OTS00000019");
                rbHombro90_3.Attributes.Add("Tag", "N009-OTS00000019");

                txtRotacionHombro90Puntos.Attributes.Add("Tag", "N009-OTS00000020");
                chkHombro90SI.Attributes.Add("Tag", "N009-OTS00000021");
                //rbRotacion090DolorNO.Attributes.Add("N009-OTS00000021");

                rbHombroInternal_1.Attributes.Add("Tag", "N009-OTS00000022");
                rbHombroInternal_2.Attributes.Add("Tag", "N009-OTS00000022");
                rbHombroInternal_3.Attributes.Add("Tag", "N009-OTS00000022");

                txtInternaHombroPuntos.Attributes.Add("Tag", "N009-OTS00000023");
                chkHombroInternoSI.Attributes.Add("Tag", "N009-OTS00000024");
                //rbRotacionExtIntDolorNO.Attributes.Add("N009-OTS00000024");


                txtObservaciones.Attributes.Add("Tag", "N009-OTS00000025");

                txtTotal1.Attributes.Add("Tag", "N009-OTS00000026");

                txtTotal2.Attributes.Add("Tag", "N009-OTS00000027");


                TabAlturaEstructural.Attributes.Add("Tag", "N009-ME000000015");
                ddlAnteTEC.Attributes.Add("Tag", "N009-MF000000781");
                //chkantecedente_de_tec_no.Attributes.Add("Tag", "N009-MF000000782");
                txtAnteTEC.Attributes.Add("Tag", "N009-MF000000783");
                ddlConvulsiones.Attributes.Add("Tag", "N009-MF000000785");
                //chkconvulsiones__epilepsia_no.Attributes.Add("Tag", "N009-MF000000786");
                txtConvulsiones.Attributes.Add("Tag", "N009-MF000000787");
                ddlMareosMioclo.Attributes.Add("Tag", "N009-MF000000789");
                //chkmareos__mioclonias__actasia_no.Attributes.Add("Tag", "N009-MF000000790");
                txtMareosMioclo.Attributes.Add("Tag", "N009-MF000000791");
                ddlAgarofobia.Attributes.Add("Tag", "N009-MF000000793");
                //chkagorafobia_no.Attributes.Add("Tag", "N009-MF000000794");
                txtAgarofobia.Attributes.Add("Tag", "N009-MF000000795");
                ddlAcrofobia.Attributes.Add("Tag", "N009-MF000000797");
                //chkacrofobia_no.Attributes.Add("Tag", "N009-MF000000798");
                txtAcrofobia.Attributes.Add("Tag", "N009-MF000000799");
                ddlInsuficiCardiaca.Attributes.Add("Tag", "N009-MF000000801");
                //chkinsuficiencia_cardiaca_no.Attributes.Add("Tag", "N009-MF000000802");
                txtInsuficiCardiaca.Attributes.Add("Tag", "N009-MF000000803");
                ddlEstereopsiaAlterada.Attributes.Add("Tag", "N009-MF000000805");
                //chkestereopsia_alterada_no.Attributes.Add("Tag", "N009-MF000000806");
                txtEstereopsiaAlterada.Attributes.Add("Tag", "N009-MF000000807");

                chkNistagmusEspontaneo.Attributes.Add("Tag", "N009-MF000000026");
                chkNistagmusProvocado.Attributes.Add("Tag", "N009-MF000000027");
                chkPrimerosAuxi.Attributes.Add("Tag", "N009-MF000000028");
                chkTrabjNivel.Attributes.Add("Tag", "N009-MF000000029");
                ddlTimpanos.Attributes.Add("Tag", "N009-MF000000030");
                ddlEquilibrio.Attributes.Add("Tag", "N009-MF000000031");
                ddlSustentacion.Attributes.Add("Tag", "N009-MF000000036");
                ddlCaminarRecta.Attributes.Add("Tag", "N009-MF000000035");
                ddlCaminarOjosVendados.Attributes.Add("Tag", "N009-MF000000034");
                ddlCaminarPuntaTalon.Attributes.Add("Tag", "N009-MF000000032");
                ddlRotarSilla.Attributes.Add("Tag", "N009-MF000000033");
                ddlAdiadocoquinesiaDirecta.Attributes.Add("Tag", "N009-MF000000037");
                ddlAdiadocoquinesiaCruzada.Attributes.Add("Tag", "N009-MF000000038");
                ddlAptitudAltura18.Attributes.Add("Tag", "N009-MF000000039");
                txtResultadoAltura18.Attributes.Add("Tag", "N009-MF000000357");


                Tab7D.Attributes.Add("Tag", "N002-ME000000045");

            txtActividadRealizar_7D.Attributes.Add("Tag", "N002-MF000000306");
            ddlProblemasCardiacos_7D.Attributes.Add("Tag", "N002-MF000000307");
            ddlProblemasOftal_7D.Attributes.Add("Tag", "N002-MF000000308");
            ddlProblemasDigestivos_7D.Attributes.Add("Tag", "N002-MF000000309");
            ddlDesprdenes_7D.Attributes.Add("Tag", "N002-MF000000310");
            ddlProblemasRespi_7D.Attributes.Add("Tag", "N002-MF000000311");
            ddlNeurologicos_7D.Attributes.Add("Tag", "N002-MF000000312");
            ddlCirugia_7D.Attributes.Add("Tag", "N002-MF000000313");
            ddlInfecciones_7D.Attributes.Add("Tag", "N002-MF000000314");
            ddlHipertension_7D.Attributes.Add("Tag", "N002-MF000000315");
            ddlObesidad_7D.Attributes.Add("Tag", "N002-MF000000316");
            ddlDiabetes_7D.Attributes.Add("Tag", "N002-MF000000317");
            ddlApnea_7D.Attributes.Add("Tag", "N002-MF000000318");
            ddlEmbarazo_7D.Attributes.Add("Tag", "N002-MF000000319");
            ddlAlergias_7D.Attributes.Add("Tag", "N002-MF000000320");
            ddlAnemia_7D.Attributes.Add("Tag", "N002-MF000000321");

            txtUsoMedicacion_7D.Attributes.Add("Tag", "N002-MF000000322");
            txtOtraCondicion_7D.Attributes.Add("Tag", "N002-MF000000325");
            ddlAptitud_7D.Attributes.Add("Tag", "N002-MF000000323");
            txtObservacion_7D.Attributes.Add("Tag", "N009-MF000000230");

                //sINTOMATICO
                TabSintomaticoRespiratorio.Attributes.Add("Tag", "N009-ME000000116");
                //ddlSintomaticoTuberculosis.Attributes.Add("Tag", "N009-MF000002015");
                //ddlSintomaticoRecibioTto.Attributes.Add("Tag", "N009-MF000002080");
                //ddlSintomaticoBajaPeso.Attributes.Add("Tag", "N009-MF000002081");
                //ddlSintomaticoTos.Attributes.Add("Tag", "N009-MF000002082");
                //ddlSintomaticoExpecto.Attributes.Add("Tag", "N009-MF000002083");
                //ddlSintomaticoSudoracion.Attributes.Add("Tag", "N009-MF000002084");
                //ddlSintomaticoFamiliares.Attributes.Add("Tag", "N009-MF000002085");
                //ddlSintomaticoSospecha.Attributes.Add("Tag", "N009-MF000002086");
                //txtSintomaticoObservacion.Attributes.Add("Tag", "N009-MF000002087");
                //ddlddlSintomaticoTosSintomaticoCertifica.Attributes.Add("Tag", "N009-MF000002088");
                //ddlSintomaticoRequiere.Attributes.Add("Tag", "N009-MF000002089");
                //txtSintomaticoResultRX.Attributes.Add("Tag", "N009-MF000002090");
                //txtSintomaticoBK1.Attributes.Add("Tag", "N009-MF000002091");
                //txtSintomaticoBK2.Attributes.Add("Tag", "N009-MF000002092");
                ddlSintomaticoTuberculosis.Attributes.Add("Tag", "N009-MF000002015");
                ddlSintomaticoTos15dias.Attributes.Add("Tag", "N009-MF000002016");
                ddlSintomaticoBajaPeso.Attributes.Add("Tag", "N009-MF000002017");
                ddlSintomaticoSudoracion.Attributes.Add("Tag", "N009-MF000002018");
                ddlSintomaticoExpecto.Attributes.Add("Tag", "N009-MF000002019");
                ddlSintomaticoFamiliares.Attributes.Add("Tag", "N009-MF000002020");
                ddlSintomaticoSospecha.Attributes.Add("Tag", "N009-MF000002021");
                txtSintomaticoObservacion.Attributes.Add("Tag", "N009-MF000002022");
                ddlSintomaticoConclusion.Attributes.Add("Tag", "N009-MF000002023");
                //txtSintomaticoResultRX.Attributes.Add("Tag", "N009-MF000002026");
                txtSintomaticoBK1.Attributes.Add("Tag", "N009-MF000002024");
                txtSintomaticoBK2.Attributes.Add("Tag", "N009-MF000002025");




                //TAMIZAJE DERMATOLOGICO (revisar porque está mal)
                TabTamizajeDermatologico.Attributes.Add("Tag", "N009-ME000000044");
                //ddlAlergiasDermicas.Attributes.Add("Tag", "N009-MF000001983");
                //ddlAlergiasMedicamentosas.Attributes.Add("Tag", "N009-MF000001984");
                //ddlEnfPropiaPiel.Attributes.Add("Tag", "N009-MF000001985");
                //ddlLupusEritematoso.Attributes.Add("Tag", "N009-MF000001987");
                //ddlEnfermedadTiroidea.Attributes.Add("Tag", "N009-MF000001988");
                //ddlArtritisReumatoide.Attributes.Add("Tag", "N009-MF000001989");
                //ddlHepatopatias.Attributes.Add("Tag", "N009-MF000001990");
                //ddlPsoriasis.Attributes.Add("Tag", "N009-MF000001991");
                //ddlSindromeOvario.Attributes.Add("Tag", "N009-MF000001992");
                //ddlDiabetesMellitus.Attributes.Add("Tag", "N009-MF000001993");
                //ddlOtrasEnfermedadesSistemicas.Attributes.Add("Tag", "N009-MF000001994");
                //ddlMacula.Attributes.Add("Tag", "N009-MF000001996");
                //ddlVesicula.Attributes.Add("Tag", "N009-MF000001997");
                //ddlNodulo.Attributes.Add("Tag", "N009-MF000001998");
                //ddlPurpura.Attributes.Add("Tag", "N009-MF000001999");
                //ddlPapula.Attributes.Add("Tag", "N009-MF000002000");
                //ddlAmpolla.Attributes.Add("Tag", "N009-MF000002001");
                //ddlPlaca.Attributes.Add("Tag", "N009-MF000002002");
                //ddlComedones.Attributes.Add("Tag", "N009-MF000002003");
                //ddlTuberculo.Attributes.Add("Tag", "N009-MF000002004");
                //ddlPustula.Attributes.Add("Tag", "N009-MF000002005");
                //ddlQuiste.Attributes.Add("Tag", "N009-MF000002006");
                //ddlTelangiectasia.Attributes.Add("Tag", "N009-MF000002007");

                //ddlEscama.Attributes.Add("Tag", "N009-MF000002009");
                //ddlPetequia.Attributes.Add("Tag", "N009-MF00000201");
                //ddlAngioedema.Attributes.Add("Tag", "N009-MF000002011");
                //ddlTumor.Attributes.Add("Tag", "N009-MF000002012");
                //ddlHabon.Attributes.Add("Tag", "N009-MF000002013");
                //ddlEquimosis.Attributes.Add("Tag", "N009-MF000002014");
                //ddlDiscromias.Attributes.Add("Tag", "N009-MF000002015");
                //ddlEscamas.Attributes.Add("Tag", "N009-MF000002017");

                //ddlEscaras.Attributes.Add("Tag", "N009-MF000002018");
                //ddlFisura.Attributes.Add("Tag", "N009-MF000002019");
                //ddlExcoriaciones.Attributes.Add("Tag", "N009-MF000002020");
                //ddlCostras.Attributes.Add("Tag", "N009-MF000002021");
                //ddlCicatrices.Attributes.Add("Tag", "N009-MF000002022");
                ////ddlAtrofia.Attributes.Add("Tag", "N009-MF000002023");
                //ddlLiquenificacion.Attributes.Add("Tag", "N009-MF000002024");
                //ddlEsclerosis.Attributes.Add("Tag", "N009-MF000002026");

                //ddlUlceras.Attributes.Add("Tag", "N009-MF000002057");
                //ddlErosion.Attributes.Add("Tag", "N009-MF000002058");

                //txtDescribirAnamnesis.Attributes.Add("Tag", "N009-MF000001986");
                //txtDescribirEnfermedades.Attributes.Add("Tag", "N009-MF000001995");
                //txtOtrosLesionesPrimarias.Attributes.Add("Tag", "N009-MF000002016");
                //txtOtrosLesionesSecundarias.Attributes.Add("Tag", "N009-MF000002029");


                //18 internacional
                //ALTURA 1.8
                TabAltura18_Internacional.Attributes.Add("Tag", "N005-ME000000117");

                txtAltura1_8Activiad_Internacional.Attributes.Add("Tag", "N005-MF000002056");
                txtAltura1_8Trabajos_Internacional.Attributes.Add("Tag", "N005-MF000002057");
                txtAltura1_8Cardiovasc_Internacional.Attributes.Add("Tag", "N005-MF000002058");
                txtAltura1_8Quirurg_Internacional.Attributes.Add("Tag", "N005-MF000002059");
                txtAltura1_8Fobias_Internacional.Attributes.Add("Tag", "N005-MF000002060");
                txtAltura1_8AntecedAlcohol_Internacional.Attributes.Add("Tag", "N005-MF000002061");
                txtAltura1_8FarmacoActual_Internacional.Attributes.Add("Tag", "N005-MF000002062");
                txtAltura1_8AntecedPsiquiat_Internacional.Attributes.Add("Tag", "N005-MF000002063");
                txtAltura1_8ExamenCardiovasc_Internacional.Attributes.Add("Tag", "N005-MF000002064");
                txtAltura1_8ExamenRespiratorio_Internacional.Attributes.Add("Tag", "N005-MF000002065");
                txtAltura1_8ExamenNervioso_Internacional.Attributes.Add("Tag", "N005-MF000002066");
                ddlAltura1_8Nistagmus_Internacional.Attributes.Add("Tag", "N005-MF000002067");
                txtAltura1_8Manifestaciones_Internacional.Attributes.Add("Tag", "N005-MF000002068");
                ddlAltura1_8PrimerosAux_Internacional.Attributes.Add("Tag", "N005-MF000002069");
                ddlAltura1_8Timpanos_Internacional.Attributes.Add("Tag", "N005-MF000002070");
                ddlAltura1_8Equilibrio_Internacional.Attributes.Add("Tag", "N005-MF000002071");
                ddlAltura1_8Sustentacion_Internacional.Attributes.Add("Tag", "N005-MF000002072");
                ddlAltura1_8Caminar_Internacional.Attributes.Add("Tag", "N005-MF000002073");
                ddlAltura1_8Adiadococinesia_Internacional.Attributes.Add("Tag", "N005-MF000002074");
                ddlAltura1_8IndiceNariz_Internacional.Attributes.Add("Tag", "N005-MF000002075");
                ddlAltura1_8RecibioCurso_Internacional.Attributes.Add("Tag", "N005-MF000002076");
                ddlAltura1_8Aptitud_Internacional.Attributes.Add("Tag", "N005-MF000002077");
                txtAltura1_8Observaciones_Internacional.Attributes.Add("Tag", "N005-MF000002078");

                //txtMultimediaFileId_Inter.Attributes.Add("Tag", Constants.txt_MULTIMEDIA_FILE_FOTO_TIPO);
                //txtServiceComponentMultimediaId_Inter.Attributes.Add("Tag", Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_FOTO_TIPO);

                #region OsteomuscularInternacional
                //Setear OsteoMuscular

                TabOsteomuscularInternacional.Attributes.Add("Tag", "N005-ME000000046");
                chkEvaluacionNormalOsteo_CI.Attributes.Add("Tag", "N009-MF000002819");

                ddlNuca1_Inter.Attributes.Add("Tag", "N005-MF000001471");
                ddlNuca2_Inter.Attributes.Add("Tag", "N005-MF000001472");
                ddlNuca3_Inter.Attributes.Add("Tag", "N005-MF000001475");
                //Hombro
                ddlHombroDerecho1_Inter.Attributes.Add("Tag", "N005-MF000001473");
                ddlHombroDerecho2_Inter.Attributes.Add("Tag", "N005-MF000001477");
                ddlHombroDerecho3_Inter.Attributes.Add("Tag", "N005-MF000001478");

                ddlHombroIzquierdo1_Inter.Attributes.Add("Tag", "N005-MF000001474");
                ddlHombroIzquierdo2_Inter.Attributes.Add("Tag", "N005-MF000001476");
                ddlHombroIzquierdo3_Inter.Attributes.Add("Tag", "N005-MF000001479");

                ddlAmbosHombros1_Inter.Attributes.Add("Tag", "N005-MF000001480");
                ddlAmbosHombros2_Inter.Attributes.Add("Tag", "N005-MF000001481");
                ddlAmbosHombros3_Inter.Attributes.Add("Tag", "N005-MF000001521");

                //Codo
                ddlCodoDerecho1_Inter.Attributes.Add("Tag", "N005-MF000001520");
                ddlCodoDerecho2_Inter.Attributes.Add("Tag", "N005-MF000001522");
                ddlCodoDerecho3_Inter.Attributes.Add("Tag", "N005-MF000001525");

                ddlCodoIzquierdo1_Inter.Attributes.Add("Tag", "N005-MF000001519");
                ddlCodoIzquierdo2_Inter.Attributes.Add("Tag", "N005-MF000001523");
                ddlCodoIzquierdo3_Inter.Attributes.Add("Tag", "N005-MF000001524");

                ddlAmboscodos1_Inter.Attributes.Add("Tag", "N005-MF000001526");
                ddlAmboscodos2_Inter.Attributes.Add("Tag", "N005-MF000001527");
                ddlAmboscodos3_Inter.Attributes.Add("Tag", "N005-MF000001530");

                //Muñecas
                ddlManosDerecha1_Inter.Attributes.Add("Tag", "N005-MF000001528");
                ddlManosDerecha2_Inter.Attributes.Add("Tag", "N005-MF000001532");
                ddlManosDerecha3_Inter.Attributes.Add("Tag", "N005-MF000001533");

                ddlManosIzquierda1_Inter.Attributes.Add("Tag", "N005-MF000001529");
                ddlManosIzquierda2_Inter.Attributes.Add("Tag", "N005-MF000001531");
                ddlManosIzquierda3_Inter.Attributes.Add("Tag", "N005-MF000001534");

                ddlAmbasManos1_Inter.Attributes.Add("Tag", "N005-MF000001535");
                ddlAmbasManos2_Inter.Attributes.Add("Tag", "N005-MF000001536");
                ddlAmbasManos3_Inter.Attributes.Add("Tag", "N005-MF000001485");

                //Dorsal
                ddlColumnadorsal1_Inter.Attributes.Add("Tag", "N005-MF000001486");
                ddlColumnadorsal2_Inter.Attributes.Add("Tag", "N005-MF000001487");
                ddlColumnadorsal3_Inter.Attributes.Add("Tag", "N005-MF000001482");

                //Lumbar
                ddlColumnaLumbar1_Inter.Attributes.Add("Tag", "N005-MF000001491");
                ddlColumnaLumbar2_Inter.Attributes.Add("Tag", "N005-MF000001492");
                ddlColumnaLumbar3_Inter.Attributes.Add("Tag", "N005-MF000001493");

                //Caderas
                ddlCaderaDerecha1_Inter.Attributes.Add("Tag", "N005-MF000001489");
                ddlCaderaDerecha2_Inter.Attributes.Add("Tag", "N005-MF000001483");
                ddlCaderaDerecha3_Inter.Attributes.Add("Tag", "N005-MF000001490");

                ddlCaderaIzquierda1_Inter.Attributes.Add("Tag", "N005-MF000001970");
                ddlCaderaIzquierda2_Inter.Attributes.Add("Tag", "N005-MF000001971");
                ddlCaderaIzquierda3_Inter.Attributes.Add("Tag", "N005-MF000001972");

                //Rodillas
                ddlRodillaDerecha1_Inter.Attributes.Add("Tag", "N005-MF000001488");
                ddlRodillaDerecha2_Inter.Attributes.Add("Tag", "N005-MF000001484");
                ddlRodillaDerecha3_Inter.Attributes.Add("Tag", "N005-MF000001496");

                ddlRodillaIzquierda1_Inter.Attributes.Add("Tag", "N005-MF000001497");
                ddlRodillaIzquierda2_Inter.Attributes.Add("Tag", "N005-MF000001498");
                ddlRodillaIzquierda3_Inter.Attributes.Add("Tag", "N005-MF000001500");

                //Tobillos
                ddlTobillosDerecho1_Inter.Attributes.Add("Tag", "N005-MF000001973");
                ddlTobillosDerecho2_Inter.Attributes.Add("Tag", "N005-MF000001974");
                ddlTobillosDerecho3_Inter.Attributes.Add("Tag", "N005-MF000001975");

                ddlTobillosIzquierdo1_Inter.Attributes.Add("Tag", "N005-MF000001976");
                ddlTobillosIzquierdo2_Inter.Attributes.Add("Tag", "N005-MF000001977");
                ddlTobillosIzquierdo3_Inter.Attributes.Add("Tag", "N005-MF000001978");

                ///
                ddlCervical_Inter.Attributes.Add("Tag", "N005-MF000001501");
                ddlDorsalEjeLateral_Inter.Attributes.Add("Tag", "N005-MF000001518");
                ddlDorsal_Inter.Attributes.Add("Tag", "N005-MF000001504");
                ddlLumbarEjeLateral_Inter.Attributes.Add("Tag", "N005-MF000001494");
                ddlLumbar_Inter.Attributes.Add("Tag", "N005-MF000001505");

                ///MOVILIDAD DOLOR
                ddlCervicalFlexion_Inter.Attributes.Add("Tag", "N005-MF000001502");
                ddlCervicalExtension_Inter.Attributes.Add("Tag", "N005-MF000001506");
                ddlCervicalLatDere_Inter.Attributes.Add("Tag", "N005-MF000001503");
                ddlCervicalLatIzq_Inter.Attributes.Add("Tag", "N005-MF000001470");
                ddlCervicalRotaDere_Inter.Attributes.Add("Tag", "N005-MF000001499");
                ddlCervicalRotaIzq_Inter.Attributes.Add("Tag", "N005-MF000001495");
                ddlCervicalIrradiacion_Inter.Attributes.Add("Tag", "N005-MF000001508");

                ddlDorsoFlexion_Inter.Attributes.Add("Tag", "N005-MF000001509");
                ddlDorsoExtension_Inter.Attributes.Add("Tag", "N005-MF000001510");
                ddlDorsoLateDere_Inter.Attributes.Add("Tag", "N005-MF000001512");
                ddlDorsoLateIzq_Inter.Attributes.Add("Tag", "N005-MF000001513");
                ddlDorsoRotaDere_Inter.Attributes.Add("Tag", "N005-MF000001514");
                ddlDorsoRotaIzq_Inter.Attributes.Add("Tag", "N005-MF000001515");
                ddlDorsoIrradiacion_Inter.Attributes.Add("Tag", "N005-MF000001516");

                ddlLasegueDere_Inter.Attributes.Add("Tag", "N005-MF000001517");
                ddlLasegueIzq_Inter.Attributes.Add("Tag", "N005-MF000001511");
                ddlSchoberDere_Inter.Attributes.Add("Tag", "N005-MF000001469");
                ddlSchoberIzq_Inter.Attributes.Add("Tag", "N005-MF000001468");


                chkColumnaCervicalApofisis_Inter.Attributes.Add("Tag", "N005-MF000001226");
                chkColumnaCervicalContractura_Inter.Attributes.Add("Tag", "N005-MF000001221");
                chkColumnaDorsalApofisis_Inter.Attributes.Add("Tag", "N005-MF000001227");
                chkColumnaDorsalContractura_Inter.Attributes.Add("Tag", "N005-MF000001222");
                chkColumnaLumbarApofisis_Inter.Attributes.Add("Tag", "N005-MF000001220");
                chkColumnaLumbarContractura_Inter.Attributes.Add("Tag", "N005-MF000001224");


                //1
                ddlHD1_Inter.Attributes.Add("Tag", "N005-MF000001223");
                ddlHD2_Inter.Attributes.Add("Tag", "N005-MF000001228");
                ddlHD3_Inter.Attributes.Add("Tag", "N005-MF000001229");
                ddlHD4_Inter.Attributes.Add("Tag", "N005-MF000001230");
                ddlHD5_Inter.Attributes.Add("Tag", "N005-MF000001415");
                ddlHD6_Inter.Attributes.Add("Tag", "N005-MF000001416");
                ddlHD7_Inter.Attributes.Add("Tag", "N005-MF000001412");
                ddlHD8_Inter.Attributes.Add("Tag", "N005-MF000001413");


                //2
                ddlHI1_Inter.Attributes.Add("Tag", "N005-MF000001414");
                ddlHI2_Inter.Attributes.Add("Tag", "N005-MF000000547");
                ddlHI3_Inter.Attributes.Add("Tag", "N005-MF000000548");
                ddlHI4_Inter.Attributes.Add("Tag", "N005-MF000000544");
                ddlHI5_Inter.Attributes.Add("Tag", "N005-MF000000545");
                ddlHI6_Inter.Attributes.Add("Tag", "N005-MF000000144");
                ddlHI7_Inter.Attributes.Add("Tag", "N005-MF000000536");
                ddlHI8_Inter.Attributes.Add("Tag", "N005-MF000000064");

                //3
                ddlCD1_Inter.Attributes.Add("Tag", "N005-MF000000065");
                ddlCD2_Inter.Attributes.Add("Tag", "N005-MF000000059");
                ddlCD3_Inter.Attributes.Add("Tag", "N005-MF000000060");
                ddlCD4_Inter.Attributes.Add("Tag", "N005-MF000000062");
                ddlCD5_Inter.Attributes.Add("Tag", "N005-MF000000063");
                ddlCD6_Inter.Attributes.Add("Tag", "N005-MF000000813");
                ddlCD7_Inter.Attributes.Add("Tag", "N005-MF000000153");
                ddlCD8_Inter.Attributes.Add("Tag", "N005-MF000000819");

                //4
                ddlCI1_Inter.Attributes.Add("Tag", "N005-MF000000822");
                ddlCI2_Inter.Attributes.Add("Tag", "N005-MF000000820");
                ddlCI3_Inter.Attributes.Add("Tag", "N005-MF000000823");
                ddlCI4_Inter.Attributes.Add("Tag", "N005-MF000000821");
                ddlCI5_Inter.Attributes.Add("Tag", "N005-MF000000156");
                ddlCI6_Inter.Attributes.Add("Tag", "N005-MF000000154");
                ddlCI7_Inter.Attributes.Add("Tag", "N005-MF000000155");
                ddlCI8_Inter.Attributes.Add("Tag", "N005-MF000000146");

                //5
                ddlMuneD1_Inter.Attributes.Add("Tag", "N005-MF000000808");
                ddlMuneD2_Inter.Attributes.Add("Tag", "N005-MF000000152");
                ddlMuneD3_Inter.Attributes.Add("Tag", "N005-MF000000809");
                ddlMuneD4_Inter.Attributes.Add("Tag", "N005-MF000000157");
                ddlMuneD5_Inter.Attributes.Add("Tag", "N005-MF000000812");
                ddlMuneD6_Inter.Attributes.Add("Tag", "N005-MF000000815");
                ddlMuneD7_Inter.Attributes.Add("Tag", "N005-MF000000816");
                ddlMuneD8_Inter.Attributes.Add("Tag", "N005-MF000000814");

                //6
                ddlMuneI1_Inter.Attributes.Add("Tag", "N005-MF000000147");
                ddlMuneI2_Inter.Attributes.Add("Tag", "N005-MF000000829");
                ddlMuneI3_Inter.Attributes.Add("Tag", "N005-MF000000168");
                ddlMuneI4_Inter.Attributes.Add("Tag", "N005-MF000000833");
                ddlMuneI5_Inter.Attributes.Add("Tag", "N005-MF000000174");
                ddlMuneI6_Inter.Attributes.Add("Tag", "N005-MF000000831");
                ddlMuneI7_Inter.Attributes.Add("Tag", "N005-MF000000173");
                ddlMuneI8_Inter.Attributes.Add("Tag", "N005-MF000000832");

                //7
                ddlCaderaD1_Inter.Attributes.Add("Tag", "N005-MF000000171");
                ddlCaderaD2_Inter.Attributes.Add("Tag", "N005-MF000000169");
                ddlCaderaD3_Inter.Attributes.Add("Tag", "N005-MF000000167");
                ddlCaderaD4_Inter.Attributes.Add("Tag", "N005-MF000000170");
                ddlCaderaD5_Inter.Attributes.Add("Tag", "N005-MF000000837");
                ddlCaderaD6_Inter.Attributes.Add("Tag", "N005-MF000000839");
                ddlCaderaD7_Inter.Attributes.Add("Tag", "N005-MF000000836");
                ddlCaderaD8_Inter.Attributes.Add("Tag", "N005-MF000000835");

                //8
                ddlCaderaI1_Inter.Attributes.Add("Tag", "N005-MF000000838");
                ddlCaderaI2_Inter.Attributes.Add("Tag", "N005-MF000000845");
                ddlCaderaI3_Inter.Attributes.Add("Tag", "N005-MF000000842");
                ddlCaderaI4_Inter.Attributes.Add("Tag", "N005-MF000000843");
                ddlCaderaI5_Inter.Attributes.Add("Tag", "N005-MF000000841");
                ddlCaderaI6_Inter.Attributes.Add("Tag", "N005-MF000000844");
                ddlCaderaI7_Inter.Attributes.Add("Tag", "N005-MF000000850");
                ddlCaderaI8_Inter.Attributes.Add("Tag", "N005-MF000000621");

                //9
                ddlTobilloD1_Inter.Attributes.Add("Tag", "N005-MF000000773");
                ddlTobilloD2_Inter.Attributes.Add("Tag", "N005-MF000000825");
                ddlTobilloD3_Inter.Attributes.Add("Tag", "N005-MF000000826");
                ddlTobilloD4_Inter.Attributes.Add("Tag", "N005-MF000000827");
                ddlTobilloD5_Inter.Attributes.Add("Tag", "N005-MF000000828");
                ddlTobilloD6_Inter.Attributes.Add("Tag", "N005-MF000000830");
                ddlTobilloD7_Inter.Attributes.Add("Tag", "N005-MF000000145");
                ddlTobilloD8_Inter.Attributes.Add("Tag", "N005-MF000000818");

                //10
                ddlTobilloI1_Inter.Attributes.Add("Tag", "N005-MF000000824");
                ddlTobilloI2_Inter.Attributes.Add("Tag", "N005-MF000000158");
                ddlTobilloI3_Inter.Attributes.Add("Tag", "N005-MF000000810");
                ddlTobilloI4_Inter.Attributes.Add("Tag", "N005-MF000000159");
                ddlTobilloI5_Inter.Attributes.Add("Tag", "N005-MF000000160");
                ddlTobilloI6_Inter.Attributes.Add("Tag", "N005-MF000000846");
                ddlTobilloI7_Inter.Attributes.Add("Tag", "N005-MF000000811");
                ddlTobilloI8_Inter.Attributes.Add("Tag", "N005-MF000000817");

                //11
                ddlRodillaD1_Inter.Attributes.Add("Tag", "N005-MF000000207");
                ddlRodillaD2_Inter.Attributes.Add("Tag", "N005-MF000001104");
                ddlRodillaD3_Inter.Attributes.Add("Tag", "N005-MF000001103");
                ddlRodillaD4_Inter.Attributes.Add("Tag", "N005-MF000001102");
                ddlRodillaD5_Inter.Attributes.Add("Tag", "N005-MF000000048");
                ddlRodillaD6_Inter.Attributes.Add("Tag", "N005-MF000000546");
                ddlRodillaD7_Inter.Attributes.Add("Tag", "N005-MF000000543");
                ddlRodillaD8_Inter.Attributes.Add("Tag", "N005-MF000000055");

                //12
                ddlRodillaI1_Inter.Attributes.Add("Tag", "N005-MF000000056");
                ddlRodillaI2_Inter.Attributes.Add("Tag", "N005-MF000000057");
                ddlRodillaI3_Inter.Attributes.Add("Tag", "N005-MF000000053");
                ddlRodillaI4_Inter.Attributes.Add("Tag", "N005-MF000000054");
                ddlRodillaI5_Inter.Attributes.Add("Tag", "N005-MF000000066");
                ddlRodillaI6_Inter.Attributes.Add("Tag", "N005-MF000000051");
                ddlRodillaI7_Inter.Attributes.Add("Tag", "N005-MF000000058");
                ddlRodillaI8_Inter.Attributes.Add("Tag", "N005-MF000000061");


                ddlPhalenDerecha_Inter.Attributes.Add("Tag", "N005-MF000000847");
                ddlPhalenIzquierda_Inter.Attributes.Add("Tag", "N005-MF000000848");
                ddlTinelDerecha_Inter.Attributes.Add("Tag", "N005-MF000000849");
                ddlTinelIzquierda_Inter.Attributes.Add("Tag", "N005-MF000000172");
                ddlCodoDerecho_Inter.Attributes.Add("Tag", "N005-MF000000166");
                ddlCodoIzquierdo_Inter.Attributes.Add("Tag", "N005-MF000000834");
                ddlPieDerecho_Inter.Attributes.Add("Tag", "N005-MF000000840");
                ddlPieIzquierdo_Inter.Attributes.Add("Tag", "N005-MF000000539");


                txtAmpliar_Inter.Attributes.Add("Tag", "N005-MF000001980");
                txtConclusion_Inter.Attributes.Add("Tag", "N005-MF000000232");
                #endregion

                #region Dermatologico Internacional
                //TAMIZAJE DERMATOLOGICO
                TabDermatologico_Internacional.Attributes.Add("Tag", "N005-ME000000116");

                chkSinDermatopatias.Attributes.Add("Tag", "N009-MF000002818");
                ddlAlergiasDermicas_Inter.Attributes.Add("Tag", "N005-MF000001983");
                ddlAlergiasMedicamentosas_Inter.Attributes.Add("Tag", "N005-MF000001984");
                ddlEnfPropiaPiels_Inter.Attributes.Add("Tag", "N005-MF000001985");
                ddlLupusEritematosos_Inter.Attributes.Add("Tag", "N005-MF000001987");
                ddlEnfermedadTiroideas_Inter.Attributes.Add("Tag", "N005-MF000001988");
                ddlArtritisReumatoides_Inter.Attributes.Add("Tag", "N005-MF000001989");
                ddlHepatopatiass_Inter.Attributes.Add("Tag", "N005-MF000001990");
                ddlPsoriasiss_Inter.Attributes.Add("Tag", "N005-MF000001991");
                ddlSindromeOvarios_Inter.Attributes.Add("Tag", "N005-MF000001992");
                ddlDiabetesMellituss_Inter.Attributes.Add("Tag", "N005-MF000001993");
                ddlOtrasEnfermedadesSistemicass_Inter.Attributes.Add("Tag", "N005-MF000001994");
                ddlMaculas_Inter.Attributes.Add("Tag", "N005-MF000001996");
                ddlVesiculas_Inter.Attributes.Add("Tag", "N005-MF000001997");
                ddlNodulos_Inter.Attributes.Add("Tag", "N005-MF000001998");
                ddlPurpuras_Inter.Attributes.Add("Tag", "N005-MF000001999");
                ddlPapulas_Inter.Attributes.Add("Tag", "N005-MF000002000");
                ddlAmpollas_Inter.Attributes.Add("Tag", "N005-MF000002001");
                ddlPlacas_Inter.Attributes.Add("Tag", "N005-MF000002002");
                ddlComedoness_Inter.Attributes.Add("Tag", "N005-MF000002003");
                ddlTuberculos_Inter.Attributes.Add("Tag", "N005-MF000002004");
                ddlPustulas_Inter.Attributes.Add("Tag", "N005-MF000002005");
                ddlQuistes_Inter.Attributes.Add("Tag", "N005-MF000002006");
                ddlTelangiectasias_Inter.Attributes.Add("Tag", "N005-MF000002007");

                ddlEscamas_Inter.Attributes.Add("Tag", "N005-MF000002009");
                ddlPetequias_Inter.Attributes.Add("Tag", "N005-MF000002010");
                ddlAngioedemas_Inter.Attributes.Add("Tag", "N005-MF000002011");
                ddlTumors_Inter.Attributes.Add("Tag", "N005-MF000002012");
                ddlHabons_Inter.Attributes.Add("Tag", "N005-MF000002013");
                ddlEquimosiss_Inter.Attributes.Add("Tag", "N005-MF000002014");
                ddlDiscromiass_Inter.Attributes.Add("Tag", "N005-MF000002015");
                ddlEscamass_Inter.Attributes.Add("Tag", "N005-MF000002017");

                ddlEscarass_Inter.Attributes.Add("Tag", "N005-MF000002018");
                ddlFisuras_Inter.Attributes.Add("Tag", "N005-MF000002019");
                ddlExcoriacioness_Inter.Attributes.Add("Tag", "N005-MF000002020");
                ddlCostrass_Inter.Attributes.Add("Tag", "N005-MF000002021");
                ddlCicatricess_Inter.Attributes.Add("Tag", "N005-MF000002022");
                ddlAtrofias_Inter.Attributes.Add("Tag", "N005-MF000002023");
                ddlLiquenificacions_Inter.Attributes.Add("Tag", "N005-MF000002024");
                ddlEsclerosiss_Inter.Attributes.Add("Tag", "N005-MF000002026");

                ddlUlcerass_Inter.Attributes.Add("Tag", "N005-MF000002027");
                ddlErosions_Inter.Attributes.Add("Tag", "N005-MF000002028");

                txtDescribirAnamnesiss_Inter.Attributes.Add("Tag", "N005-MF000001986");
                txtDescribirEnfermedadess_Inter.Attributes.Add("Tag", "N005-MF000001995");
                txtOtrosLesionesPrimariass_Inter.Attributes.Add("Tag", "N005-MF000002016");
                txtOtrosLesionesSecundariass_Inter.Attributes.Add("Tag", "N005-MF000002029");
                #endregion


                #region Fototipos
                TabFototipo.Attributes.Add("Tag", "N009-ME000000411");
                //txtLinkImage.Attributes.Add("Tag", "N009-MF000003204");
                #endregion

                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1);  //  DateTime.Parse("12/11/2016");
                dpFechaFin.SelectedDate = DateTime.Now; //  DateTime.Parse("12/11/2016"); 
                LoadCombos();

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
                    ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString(); ;
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
            ddlConsultorio.SelectedValue = "11";
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

        protected void CargarRegistro()
        {
            TabAnexo312.Hidden = true;
            TabFototipo.Hidden = true;
            TabAnexo16.Hidden = true;
            TabOsteomuscular.Hidden = true;
            TabOsteomuscularInternacional.Hidden = true;
            TabAlturaEstructural.Hidden = true;
            TabAltura18_Internacional.Hidden = true;
            Tab7D.Hidden = true;
            TabSintomaticoRespiratorio.Hidden = true;
            TabTamizajeDermatologico.Hidden = true;
            TabDermatologico_Internacional.Hidden = true;

            //Pintar los examenes correpondientes por servicio
            int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());


            LoadCombos312();
            ObtenerDatosAnexo312(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            //TabAnexo312.Hidden = false;

            if (ProfesionId == (int)TipoProfesional.Auditor)
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());
                foreach (var item in ListaComponentes)
                {                 
                    if (item.ComponentId == TabAnexo16.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombos16();
                        ObtenerDatos16(Session["ServiceId"].ToString(), Session["PersonId"].ToString());

                        TabAnexo16.Hidden = false;
                    }
                    else if (item.ComponentId == TabOsteomuscular.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosOsteo();
                        ObtenerDatosOsteomuscular(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabOsteomuscular.Hidden = false;
                    }
                    else if (item.ComponentId == TabOsteomuscularInternacional.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosOsteoInternacional();
                        ObtenerDatosOsteomuscularInternacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabOsteomuscularInternacional.Hidden = false;
                    }
                    else if (item.ComponentId == TabAlturaEstructural.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosAltura18();
                        ObtenerDatosAltura18(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabAlturaEstructural.Hidden = false;
                    }
                    else if (item.ComponentId == TabAltura18_Internacional.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosAltura18Internacional();
                        ObtenerDatosAltura18Internacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabAltura18_Internacional.Hidden = false;
                    }
                    else if (item.ComponentId == Tab7D.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombos7D();
                        ObtenerDatos7D(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        Tab7D.Hidden = false;
                    }
                    else if (item.ComponentId == TabSintomaticoRespiratorio.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosSintomaticoRespiratorio();
                        ObtenerDatosSintomaticoRespiratorio(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabSintomaticoRespiratorio.Hidden = false;
                    }
                    else if (item.ComponentId == TabTamizajeDermatologico.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosDermatologico();
                        ObtenerDatosDermatologicos(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabTamizajeDermatologico.Hidden = false;
                    }
                    else if (item.ComponentId == TabDermatologico_Internacional.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosDermatologicoInternacional();
                        ObtenerDatosDermatologicosInternacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabDermatologico_Internacional.Hidden = false;
                    }
                    else if (item.ComponentId == TabFototipo.Attributes.GetValue("Tag").ToString())
                    {
                        OperationResult objOperationResult = new OperationResult();
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        LoadcombosFototipo();
                        //ObtenerDatosFototipo(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabFototipo.Hidden = false;
                        //var dataMultimedia = _multimediaFileBL.GetMultimediaFileByPersonId(ref objOperationResult, Session["PersonId"].ToString());
                        
                        
                    }

                }
            }
            else
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());

                var ListaComponenentesConPermiso = (List<string>)Session["ComponentesPermisoLectura"];

                foreach (var item in ListaComponentes)
                {
                    if (item.ComponentId == TabAnexo312.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.EXAMEN_FISICO_ID);
                        if (Resultado != null)
                        {
                            LoadCombos312();
                            ObtenerDatosAnexo312(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabAnexo312.Hidden = false;
                        }

                    }
                    else if (item.ComponentId == TabAnexo16.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.EXAMEN_FISICO_7C_ID);
                        if (Resultado != null)
                        {
                            LoadCombos16();
                            ObtenerDatos16(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabAnexo16.Hidden = false;
                        }

                    }
                    else if (item.ComponentId == TabOsteomuscular.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.OSTEO_MUSCULAR_ID_1);
                        if (Resultado != null)
                        {
                            LoadCombosOsteo();
                            ObtenerDatosOsteomuscular(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabOsteomuscular.Hidden = false;
                        }

                    }
                    else if (item.ComponentId == TabOsteomuscularInternacional.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == "N005-ME000000046");
                        if (Resultado != null)
                        {
                            LoadCombosOsteoInternacional();
                            ObtenerDatosOsteomuscularInternacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabOsteomuscularInternacional.Hidden = false;
                        }

                    }

                    else if (item.ComponentId == TabAlturaEstructural.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.ALTURA_ESTRUCTURAL_ID);
                        if (Resultado != null)
                        {
                            LoadCombosAltura18();
                            ObtenerDatosAltura18(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabAlturaEstructural.Hidden = false;
                        }

                    }

                    else if (item.ComponentId == TabAltura18_Internacional.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == "N005-ME000000117");
                        if (Resultado != null)
                        {
                            LoadCombosAltura18Internacional();
                            ObtenerDatosAltura18Internacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabAltura18_Internacional.Hidden = false;
                        }

                    }

                    else if (item.ComponentId == Tab7D.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.ALTURA_7D_ID);
                        if (Resultado != null)
                        {
                            LoadCombos7D();
                            ObtenerDatos7D(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            Tab7D.Hidden = false;
                        }

                    }
                    else if (item.ComponentId == TabSintomaticoRespiratorio.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.SINTOMATICO_ID);
                        if (Resultado != null)
                        {
                            LoadCombosSintomaticoRespiratorio();
                            ObtenerDatosSintomaticoRespiratorio(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabSintomaticoRespiratorio.Hidden = false;
                        }

                    }
                    else if (item.ComponentId == TabTamizajeDermatologico.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.TAMIZAJE_DERMATOLOGIO_ID);
                        if (Resultado != null)
                        {
                            LoadCombosDermatologico();
                            ObtenerDatosDermatologicos(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabTamizajeDermatologico.Hidden = false;
                        }
                    }
                    else if (item.ComponentId == TabDermatologico_Internacional.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == "N005-ME000000116");
                        if (Resultado != null)
                        {
                            LoadCombosDermatologicoInternacional();
                            ObtenerDatosDermatologicosInternacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabDermatologico_Internacional.Hidden = false;
                        }
                    }
                    else if (item.ComponentId == TabFototipo.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == "N009-ME000000411");
                        if (Resultado != null)
                        {
                            OperationResult objOperationResult = new OperationResult();
                            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                            LoadcombosFototipo();
                            TabFototipo.Hidden = false;
                        }                       
                    }
                }
            }
        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {            
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();
            int index = e.RowIndex;
            Session["index"] = index;
            var dataKeys = grdData.DataKeys[index];
            Session["ServiceId"] = dataKeys[0] == null ? "" : dataKeys[0].ToString();
            Session["PersonId"] = dataKeys[1] == null ? "" : dataKeys[1].ToString();
            Session["i_AptitudeStatusId"] = dataKeys[3] == null ? "" : dataKeys[3].ToString();
            Session["i_EsoTypeId"] = dataKeys[4] == null ? "" : dataKeys[4].ToString();

            Session["v_ExploitedMineral"] =  dataKeys[5] == null?"": dataKeys[5].ToString();
            Session["i_AltitudeWorkId"] = dataKeys[6] == null ? "" : dataKeys[6].ToString();
            Session["i_PlaceWorkId"] = dataKeys[7] == null ? "" : dataKeys[7].ToString();
            Session["d_ServiceDate"] = dataKeys[10] == null ? "" : dataKeys[10].ToString();

            var genero = dataKeys[2] == null ? "" : dataKeys[2].ToString();
            if (genero.ToUpper() == "FEMENINO")
            {
                PanelGinecologico.Enabled = true;
                PanelGinecologico_16.Enabled = true;
            }
            else
            {
                PanelGinecologico.Enabled = false;
                PanelGinecologico_16.Enabled = false;  
            }
            txtEmpresaClienteCabecera.Text =dataKeys[14]== null?"": dataKeys[14].ToString();
            txtActividadEmpresaClienteCabecera.Text = dataKeys[16] == null ? "" : dataKeys[16].ToString();
            txtTrabajadorCabecera.Text = dataKeys[8] == null ? "" : dataKeys[8].ToString();
            txtDNICabecera.Text = dataKeys[9] == null ? "" : dataKeys[9].ToString();
            Session["DniTrabajador"] = txtDNICabecera.Text;
            txtFechaCabecera.Text = dataKeys[10] == null ? "" : ((DateTime)dataKeys[10]).ToString("dd/MM/yyyy");
            Session["FechaServicio"] = dataKeys[10] == null ? "" : ((DateTime)dataKeys[10]).ToString("ddMMyyyy");
            txtTipoExamenCabecera.Text = dataKeys[11] == null ? "" : dataKeys[11].ToString();
            txtGeneroCabecera.Text = dataKeys[2] == null ? "" : dataKeys[2].ToString();
            txtPuestoCabecera.Text = dataKeys[12] == null ? "" : dataKeys[12].ToString();

            DateTime? FechaNacimiento = DateTime.Parse(dataKeys[13].ToString());
            txtEdadCabecera.Text = dataKeys[13] == null ? "" : new ServiceBL().GetAge(FechaNacimiento.Value).ToString();// dataKeys[13].ToString();                               

            //Mostrar
            grdComponentes.DataSource=  _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(ddlConsultorio.SelectedValue.ToString()), Session["ServiceId"].ToString());
            grdComponentes.DataBind();

            CargarRegistro();
            Utils.LoadDropDownList(ddlGrabarUsuarioFototipo, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabaAltura, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGraba7D, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabaSintomatico, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabaDermatologico, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabaOsteo, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabaAnexo16, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

            ddlGrabarUsuarioFototipo.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabaAltura.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGraba7D.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabaSintomatico.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabaDermatologico.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabaOsteo.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabar.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
            ddlUsuarioGrabaAnexo16.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();

            ddlUsuarioGrabar.Enabled = false;
            ddlGrabarUsuarioFototipo.Enabled = false;
            ddlUsuarioGrabaAltura.Enabled = false;
            ddlUsuarioGraba7D.Enabled = false;
            ddlUsuarioGrabaSintomatico.Enabled = false;
            ddlUsuarioGrabaDermatologico.Enabled = false;
            ddlUsuarioGrabaOsteo.Enabled = false;
            ddlUsuarioGrabaAnexo16.Enabled = false;
            if (grdData.Rows.Count > 0)
            {
                Accordion1.Enabled = true;
            }
            else
            {
                Accordion1.Enabled = false;
            }
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            //LlenarLista();ç
            
            _ServiceComponentMultimediaFile = _serviceBL.ExtraerUrlImagen(Session["ServiceId"].ToString(), Session["ServicioComponentIdMedicina"].ToString());
            if (_ServiceComponentMultimediaFile == null)
            {
                texturl.Text = "";
                txtMultimediaFileId_Inter.Text = "";
                txtServiceComponentMultimediaId_Inter.Text = "";
            }
            else {
                texturl.Text = Encoding.UTF8.GetString(_ServiceComponentMultimediaFile.b_file);
                txtMultimediaFileId_Inter.Text = _ServiceComponentMultimediaFile.v_MultimediaFileId.ToString();
                txtServiceComponentMultimediaId_Inter.Text = _ServiceComponentMultimediaFile.v_ServiceComponetMultimediaId.ToString();
            }
            
           
        }

        private void LlenarLista()
        {
            List<MyListWeb> lista = new List<MyListWeb>();
            int selectedCount = grdData.SelectedRowIndexArray.Length;
            for (int i = 0; i < selectedCount; i++)
            {
                int rowIndex = grdData.SelectedRowIndexArray[i];

                var dataKeys = grdData.DataKeys[rowIndex];

                lista.Add(new MyListWeb
                {
                    IdServicio = dataKeys[0].ToString()
                });
            }
            Session["objListaCambioFecha"] = lista;

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
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId,i_ServiceComponentStatusId", strFilterExpression);
            grdData.DataBind();
        }

        private List<ServiceList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();

            //var _objData = _serviceBL.GetAllServices_(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));

            List<string> ComponentesId = new List<string>();
            ComponentesId.Add("N002-ME000000022");
            ComponentesId.Add("N009-ME000000052");
            ComponentesId.Add("N002-ME000000046");
            ComponentesId.Add("N009-ME000000015");
            ComponentesId.Add("N002-ME000000045");
            ComponentesId.Add("N009-ME000000044");
            ComponentesId.Add("N009-ME000000116");
            ComponentesId.Add("N005-ME000000117");
            
            var _objData = _serviceBL.GetAllServices_Consultorio(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1), ComponentesId.ToArray());
       
            if (_objData.Count == 0)
            {
                TabAnexo312.Hidden = true;
                TabAnexo16.Hidden = true;
                TabOsteomuscular.Hidden = true;
                TabAlturaEstructural.Hidden = true;
                Tab7D.Hidden = true;
                TabSintomaticoRespiratorio.Hidden = true;
                TabTamizajeDermatologico.Hidden = true;
                TabFototipo.Hidden = true;
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

        protected void winEditExaAdicional_Close(object sender, WindowCloseEventArgs e)
        {
            grdData.SelectedRowIndex = int.Parse(Session["Index"].ToString());
            CargarRegistro();
        }
        
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlDepartamento.SelectedValue == null) return;
            if (ddlDepartamento.SelectedValue.ToString() == "-1")
            {
                Utils.LoadDropDownList(ddlProvincia, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            }
            else
            {
                Utils.LoadDropDownList(ddlProvincia, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamento.SelectedValue.ToString())), DropDownListAction.Select);
            }
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlProvincia.SelectedValue == null) return;
            if (ddlDepartamento.SelectedValue.ToString() == "-1")
            {
                Utils.LoadDropDownList(ddlDistrito, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);
            }
            else
            {
                Utils.LoadDropDownList(ddlDistrito, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvincia.SelectedValue.ToString())), DropDownListAction.Select);
            }
        }

        protected void WinFechaServicio_Close(object sender, WindowCloseEventArgs e)
        {
            btnFilter_Click(sender, e);
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

        #region Anexo 312

        protected void ddlDepartamento_16_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlDepartamento_16.SelectedValue == null) return;
            if (ddlDepartamento_16.SelectedValue.ToString() == "-1")
            {
                Utils.LoadDropDownList(ddlProvincia_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            }
            else
            {
                Utils.LoadDropDownList(ddlProvincia_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamento_16.SelectedValue.ToString())), DropDownListAction.Select);
            }
        }

        protected void ddlProvincia_16_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlProvincia_16.SelectedValue == null) return;
            if (ddlDepartamento_16.SelectedValue.ToString() == "-1")
            {
                Utils.LoadDropDownList(ddlDistrito_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);
            }
            else
            {
                Utils.LoadDropDownList(ddlDistrito_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvincia_16.SelectedValue.ToString())), DropDownListAction.Select);
            }
        }              

        private void ObtenerDatosAnexo312(string pServiceId, string pPersonId)
        {

            OperationResult objOperationResult = new OperationResult();
            #region MyRegion
          
            var Obj312 = _serviceBL.RetornarDatosEmpresaFiliacion(pServiceId, pPersonId);

            //Datos de la empresa
            txtRazonSocial.Text = Obj312[0].EmpresaCliente == null ? "" : Obj312[0].EmpresaCliente.ToString().ToUpper();
            txtRucEmpresa.Text = Obj312[0].RucEmpresaCliente == null ? "" : Obj312[0].RucEmpresaCliente.ToString().ToUpper();
            txtActividadEconomica.Text = Obj312[0].ActividadEconomicaEmpresaCliente == null ? "" : Obj312[0].ActividadEconomicaEmpresaCliente.ToString().ToUpper();
            txtLugarTrabajo.Text = Obj312[0].LugarTrabajo == null ? "" : Obj312[0].LugarTrabajo.ToString().ToUpper();
            txtPuestoTrabajo.Text = Obj312[0].PuestoTrabajo == null ? "" : Obj312[0].PuestoTrabajo.ToString().ToUpper();
            txtNombreCompletoTrabajador.Text = Obj312[0].Trabajador == null ? "" : Obj312[0].Trabajador.ToString().ToUpper();

            //Filiación      
            dpcFechaNacimiento.SelectedDate = Obj312[0].FechaNacimiento.Value;
            txtEdad.Text = _serviceBL.GetAge(Obj312[0].FechaNacimiento.Value).ToString();
            ddlTipoDocumento.SelectedValue = Obj312[0].TipoDocumentoId == null ? "-1" : Obj312[0].TipoDocumentoId.Value.ToString();
            txtNroDocumento.Text = Obj312[0].NroDocumento == null ? "" : Obj312[0].NroDocumento.ToString();
            txtDireccionFiscal.Text = Obj312[0].DireccionFiscal == null ? "" : Obj312[0].DireccionFiscal.ToString().ToUpper();
            ddlDepartamento.SelectedValue = Obj312[0].DepartamentoId == null ? "-1" : Obj312[0].DepartamentoId.Value.ToString();
            Utils.LoadDropDownList(ddlProvincia, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamento.SelectedValue.ToString())), DropDownListAction.Select);
            ddlProvincia.SelectedValue = Obj312[0].ProvinciaId == null ? "-1" : Obj312[0].ProvinciaId.Value.ToString();
            Utils.LoadDropDownList(ddlDistrito, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvincia.SelectedValue.ToString())), DropDownListAction.Select);
            ddlDistrito.SelectedValue = Obj312[0].DistritoId == null ? "-1" : Obj312[0].DistritoId.Value.ToString();
            ddlResideSiNo.SelectedValue = Obj312[0].ResideLugarTrabajo == null ? "0" : Obj312[0].ResideLugarTrabajo.Value.ToString();
            txtTiempoResidencia.Text = Obj312[0].TiempoResidencia == null ? "---" : Obj312[0].TiempoResidencia.ToString();
            txtLugarNacimiento.Text = Obj312[0].v_BirthPlace == null ? "---" : Obj312[0].v_BirthPlace.ToString();
            txtTelefonos.Text = Obj312[0].Telefono == null ? "" : Obj312[0].Telefono.ToString();
            ddlTipoSeguro.SelectedValue = Obj312[0].TipoSeguroId == null ? "-1" : Obj312[0].TipoSeguroId.Value.ToString();
            txtCorreoElectronico.Text = Obj312[0].Email == null ? "" : Obj312[0].Email.ToString().ToUpper();
            ddlEstadoCivil.SelectedValue = Obj312[0].EstadoCivilId == null ? "-1" : Obj312[0].EstadoCivilId.Value.ToString();
            ddlGradoInstruccion.SelectedValue = Obj312[0].GradoInstruccionId == null ? "-1" : Obj312[0].GradoInstruccionId.Value.ToString();
            txtNroHijosVivos.Text = Obj312[0].HijosVivos == null ? "" : Obj312[0].HijosVivos.Value.ToString();
            txtNroHijosMuertos.Text = Obj312[0].HijosMuertos == null ? "" : Obj312[0].HijosMuertos.Value.ToString();
            txtAnamnesis.Text = Obj312[0].Anamnesis == null ? "PACIENTE APARENTEMENTE SANO" : Obj312[0].Anamnesis.ToString();
            txtMenarquia.Text = Obj312[0].v_Menarquia;
            txtGestacion.Text = Obj312[0].v_Gestapara;
            txtFum.Text = Obj312[0].v_FechaUltimaRegla;
            ddlMac.SelectedValue = Obj312[0].i_MacId.ToString();
            txtRegimenCatamenial.Text = Obj312[0].v_CatemenialRegime;
            txtCirugiaGineco.Text = Obj312[0].v_CiruGine;
            txtUltimoPAP.Text = Obj312[0].v_FechaUltimoPAP;
            txtResultadoPAP.Text = Obj312[0].v_ResultadoMamo;
            txtUltimaMamo.Text = Obj312[0].v_FechaUltimaMamo;
            txtResultadoMamo.Text = Obj312[0].v_ResultadosPAP;
            ddlTipoEvaluacion.SelectedValue = Session["i_EsoTypeId"].ToString();
            ddlAptitud.SelectedValue = Obj312[0].i_StatusAptitud.ToString();
            txtComentarioAptitud.Text = Obj312[0].Comentario == null ? "" : Obj312[0].Comentario.ToString();
            //txtComentarioMedico.Text = Obj312[0].ComentarioMedico == null ? "" : Obj312[0].ComentarioMedico.ToString();
            //txtRestriccionesAptitud.Text = Obj312[0].Restricciones == null ? "" : Obj312[0].Restricciones.ToString();

            grdAntecedentesOcupacionales.DataSource = Obj312[0].ListaHistoriaOcupacional;
            grdAntecedentesOcupacionales.DataBind();

            grdAntecedentesPersonales.DataSource = Obj312[0].ListaMedicosPersonales;
            grdAntecedentesPersonales.DataBind();

            grdHabitosNocivos.DataSource = Obj312[0].ListaHabitosNosivos;
            grdHabitosNocivos.DataBind();

            grdAntecedenteFamiliar.DataSource = Obj312[0].ListaMedicosFamiliares;
            grdAntecedenteFamiliar.DataBind();

            //Evaluación Triaje
            var oExamenTriaje = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 10);
            var oExamenFisico = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 11);
            Session["ServicioComponentIdMedicina"] = oExamenFisico[0].ServicioComponentId;
            var objExamenTriaje = _serviceBL.GetServiceComponentFields_AMC312(oExamenTriaje == null ? "" : oExamenTriaje[0].ServicioComponentId, oExamenFisico == null ? "" : oExamenFisico[0].ServicioComponentId, pServiceId);
            Session["ComponenteseMedicina"] = objExamenTriaje;
            
            Session["ServicioComponentIdTriaje"] = oExamenTriaje == null ? null : oExamenTriaje[0].ServicioComponentId;
            Session["ServicioComponentIdFisico"] = oExamenFisico == null ? null : oExamenFisico[0].ServicioComponentId;
            if (objExamenTriaje.Count != 0)
            {
                SearchControlAndLoadData(TabAnexo312, oExamenFisico[0].ServicioComponentId, objExamenTriaje);
                #region Campos de auditoria
                var datosAuditoria = HistoryBL.CamposAuditoria(Session["ServicioComponentIdMedicina"].ToString());
                if (datosAuditoria != null)
                {
                    txtMedicina1Auditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtMedicina1AuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtMedicina1AuditorActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtMedicina1Evaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtMedicina1EvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtMedicina1EvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtMedicinaInformador.Text = datosAuditoria.UserNameInformadorInsert;
                    txtMedicinaInformadorInserta.Text = datosAuditoria.FechaHoraInformadorInsert;
                    txtMedicinaInformadorActualiza.Text = datosAuditoria.FechaHoraInformadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);               
                SearchControlAndClean(TabAnexo312, _tmpServiceComponentsForBuildMenuList);

                txtMedicina1Auditor.Text = "";
                txtMedicina1AuditorInsercion.Text = "";
                txtMedicina1AuditorActualizacion.Text = "";

                txtMedicina1Evaluador.Text = "";
                txtMedicina1EvaluadorInserta.Text = "";
                txtMedicina1EvaluadorActualizacion.Text = "";

                txtMedicinaInformador.Text = "";
                txtMedicinaInformadorInserta.Text = "";
                txtMedicinaInformadorActualiza.Text = "";
            }
            #endregion
        }
               
        private void LoadCombos312()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo135 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 135);
            var Combo118 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118);
            // 312
            Utils.LoadDropDownList(ddlTipoDocumento , "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 106), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDepartamento, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDepartamento(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProvincia, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDistrito, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlResideSiNo, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTipoSeguro, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 188), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEstadoCivil, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 101), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlGradoInstruccion, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 108), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMac, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 134), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlPiel, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCabello, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOjosAnexos, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOidos, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNariz, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBoca, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFaringe, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCuello, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlApaRespiratorio, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlApaCardiovascular, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlApaDigestivo, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlApaGenito, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlApaLocomotor, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMarcha, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumna, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtreSuper, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtreInfe, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLinfaticos, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSistNerviso, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEctoscopia, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEstadoMental, "Value1", "Id", Combo135, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTipoEvaluacion, "Value1", "Id", Combo118, DropDownListAction.Select);
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();
      
            Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
                
        }

        protected void WindowAddAntecedenteOcupacional_Close(object sender, WindowCloseEventArgs e)
        {           
            grdAntecedentesOcupacionales.DataSource = _HistoryBL.ListarGrillaHistoriaOcupacional(Session["PersonId"].ToString());
            grdAntecedentesOcupacionales.DataBind();
        }

        protected void WindowAddAntecedentePersonal_Close(object sender, WindowCloseEventArgs e)
        {
            grdAntecedentesPersonales.DataSource = _HistoryBL.ListarGrillaMedicoPersonal(Session["PersonId"].ToString());
            grdAntecedentesPersonales.DataBind();
        }

        protected void txtTalla_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text != "" && txtTalla.Text != "")
            {
                decimal Peso = decimal.Parse(txtPeso.Text.ToString());
                decimal Talla = decimal.Parse(txtTalla.Text.ToString());

                txtImc.Text = ((Peso / Talla) / Talla).ToString("#.##");
            }
        }

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text != "" && txtTalla.Text != "")
            {
                decimal Peso = decimal.Parse(txtPeso.Text.ToString());
                decimal Talla = decimal.Parse(txtTalla.Text.ToString());

                txtImc.Text = ((Peso / Talla) / Talla).ToString("#.##");
            }
        }

        protected void txtPadb_TextChanged(object sender, EventArgs e)
        {
            if (txtPadb.Text != "" && txtPcadera.Text != "")
            {
                decimal PerAbd = decimal.Parse(txtPadb.Text.ToString());
                decimal PerCad = decimal.Parse(txtPcadera.Text.ToString());

                txtIcc.Text = (PerAbd / PerCad).ToString("#.##");
            }
        }

        protected void txtPcadera_TextChanged(object sender, EventArgs e)
        {
            if (txtPadb.Text != "" && txtPcadera.Text != "")
            {
                decimal PerAbd = decimal.Parse(txtPadb.Text.ToString());
                decimal PerCad = decimal.Parse(txtPcadera.Text.ToString());

                txtIcc.Text = (PerAbd / PerCad).ToString("#.##");
            }
        }

        protected void WindowAddAntecedenteFamiliar_Close(object sender, WindowCloseEventArgs e)
        {
            grdAntecedenteFamiliar.DataSource = _HistoryBL.ListarGrillaFamiliars(Session["PersonId"].ToString());
            grdAntecedenteFamiliar.DataBind();
        }

        protected void WindowAddHabitoNocivo_Close(object sender, WindowCloseEventArgs e)
        {
            grdHabitosNocivos.DataSource = _HistoryBL.ListarGrillaNocivos(Session["PersonId"].ToString());
            grdHabitosNocivos.DataBind();
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
        }

        protected void grdAntecedentesPersonales_RowCommand(object sender, GridCommandEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "DeleteRegistro")
            {
                string PersonalMedicalId = grdAntecedentesPersonales.DataKeys[grdAntecedentesPersonales.SelectedRowIndex][0].ToString();
                _HistoryBL.DeleteMedicoPersonal(ref objOperationResult, PersonalMedicalId, ((ClientSession)Session["objClientSession"]).GetAsList());

                grdAntecedentesPersonales.DataSource = _HistoryBL.ListarGrillaMedicoPersonal(Session["PersonId"].ToString());
                grdAntecedentesPersonales.DataBind();
            }
        }

        protected void grdHabitosNocivos_RowCommand(object sender, GridCommandEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "DeleteRegistro")
            {
                string NoxiousHabitsId = grdHabitosNocivos.DataKeys[grdHabitosNocivos.SelectedRowIndex][0].ToString();
                _HistoryBL.DeleteNocivos(ref objOperationResult, NoxiousHabitsId, ((ClientSession)Session["objClientSession"]).GetAsList());

                grdHabitosNocivos.DataSource = _HistoryBL.ListarGrillaNocivos(Session["PersonId"].ToString());
                grdHabitosNocivos.DataBind();
            }
        }

        protected void btnGrabarAnexo312_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            personDto objpersonDto = new personDto();
            PacientBL objPacientBL = new PacientBL();
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            if (ddlUsuarioGrabar.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }

            //Actualizar Datos del Filiación
            objpersonDto = objPacientBL.GetPerson(ref objOperationResult, Session["PersonId"].ToString());

            objpersonDto.d_Birthdate = dpcFechaNacimiento.SelectedDate;
            objpersonDto.i_DocTypeId = int.Parse(ddlTipoDocumento.SelectedValue.ToString());
            objpersonDto.v_DocNumber = txtNroDocumento.Text;
            objpersonDto.v_AdressLocation = txtDireccionFiscal.Text;
            objpersonDto.i_DepartmentId = int.Parse(ddlDepartamento.SelectedValue.ToString());
            objpersonDto.i_ProvinceId = int.Parse(ddlProvincia.SelectedValue.ToString());
            objpersonDto.i_DistrictId = int.Parse(ddlDistrito.SelectedValue.ToString());
            objpersonDto.i_ResidenceInWorkplaceId = int.Parse(ddlResideSiNo.SelectedValue.ToString());
            objpersonDto.v_ResidenceTimeInWorkplace = txtTiempoResidencia.Text;
            objpersonDto.v_TelephoneNumber = txtTelefonos.Text;
            objpersonDto.i_TypeOfInsuranceId = int.Parse(ddlTipoSeguro.SelectedValue.ToString());
            objpersonDto.v_Mail = txtCorreoElectronico.Text;
            objpersonDto.i_MaritalStatusId = int.Parse(ddlEstadoCivil.SelectedValue.ToString());
            objpersonDto.i_LevelOfId = int.Parse(ddlGradoInstruccion.SelectedValue.ToString());
            objpersonDto.i_NumberLiveChildren = txtNroHijosVivos.Text == "" ? (int?)null : int.Parse(txtNroHijosVivos.Text.ToString());
            objpersonDto.i_NumberDeadChildren = txtNroHijosMuertos.Text == "" ? (int?)null : int.Parse(txtNroHijosMuertos.Text.ToString());
            objpersonDto.v_BirthPlace = txtLugarNacimiento.Text;
            //Grabar Datos Filiacion
            objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, ((ClientSession)Session["objClientSession"]).GetAsList(), txtNroDocumento.Text, txtNroDocumento.Text);


            //Grabar Antecedentes Ginecológicos
            serviceDto serviceDTO = new serviceDto();

            serviceDTO.v_ServiceId = Session["ServiceId"].ToString(); ;
            serviceDTO.v_MainSymptom = "";
            serviceDTO.i_TimeOfDisease = (int?)null;
            serviceDTO.i_TimeOfDiseaseTypeId = (int?)null;
            serviceDTO.v_Story = txtAnamnesis.Text;
            serviceDTO.i_DreamId = -1;
            serviceDTO.i_UrineId = -1;
            serviceDTO.i_DepositionId = -1;
            serviceDTO.v_Findings = "";
            serviceDTO.i_AppetiteId = -1;
            serviceDTO.i_ThirstId = -1;
            serviceDTO.i_HasSymptomId = (int?)null;
            serviceDTO.d_PAP = (DateTime?)null;
            serviceDTO.d_Mamografia = (DateTime?)null;
            serviceDTO.v_Findings = "";
            serviceDTO.v_Menarquia = txtMenarquia.Text;
            serviceDTO.v_Gestapara = txtGestacion.Text;
            //serviceDTO.v_FechaUltimaRegla = txtFum.Text;
            serviceDTO.i_MacId = int.Parse(ddlMac.SelectedValue.ToString());
            serviceDTO.v_CatemenialRegime = txtRegimenCatamenial.Text;
            serviceDTO.v_CiruGine = txtCirugiaGineco.Text;
            serviceDTO.v_FechaUltimoPAP = txtUltimoPAP.Text;
            serviceDTO.v_ResultadosPAP = txtResultadoPAP.Text;
            serviceDTO.v_FechaUltimaMamo = txtUltimaMamo.Text;
            serviceDTO.v_ResultadoMamo = txtResultadoMamo.Text;
            serviceDTO.i_AptitudeStatusId = int.Parse(Session["i_AptitudeStatusId"].ToString());
            _serviceBL.UpdateAnamnesis(ref objOperationResult, serviceDTO, ((ClientSession)Session["objClientSession"]).GetAsList());

            SearchControlAndSetValues(Panel8, Session["ServicioComponentIdTriaje"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdTriaje"].ToString());


                   
            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];
         
            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N002-MF000000009") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtImc.Text, "N002-MF000000009", "double");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }             
            }
            if (Componentes.Find(p => p.v_ComponentFieldsId == "N002-MF000000012") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtIcc.Text, "N002-MF000000012", "double");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }    
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N002-MF000000001") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtParterial.Text, "N002-MF000000001", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N002-MF000000002") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtParterialDiatolica.Text, "N002-MF000000002", "int");
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
           
            Session["_serviceComponentFieldsList"] = null;
            _serviceComponentFieldsList = new List<Node.WinClient.BE.ServiceComponentFieldsList>();
            SearchControlAndSetValues(Panel9, Session["ServicioComponentIdMedicina"].ToString());

             result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());
             Componentes = null;
             Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];
             if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002135") != null)
             {
                 var DXAautomatico = SearchDxSugeridoOfSystem(chkPersonaSana.Checked ? "1" : "0", "N009-MF000002135", "int");
                 if (DXAautomatico != null)
                 {
                     var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                     if (Result1 == null)
                     {
                         l.Add(DXAautomatico);
                     }
                 }
             }


            //Gabar Dx
             #region Capturar [Comentarios, estado, procedencia de un exmanen componente]

             var serviceComponentDto = new servicecomponentDto();
             serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
             //Obtener fecha de Actualizacion
             var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

             serviceComponentDto.v_ComponentId = "N002-ME000000002|N002-ME000000001|N002-ME000000022";
             serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
             serviceComponentDto.d_UpdateDate = FechaUpdate;
             #endregion

            //obtener el usuario antiguo
             Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
             // Grabar Dx por examen componente mas sus restricciones
                     
            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabar.SelectedValue.ToString());

            //if (int.Parse(ddlUsuarioGrabar.SelectedValue.ToString()) != ((ClientSession)Session["objClientSession"]).i_SystemUserId)
            //{
            //  //  //Obtener el rol del usuario que graba
            //  //var oSystemUser =  new SecurityBL().GetSystemUser(ref objOperationResult, int.Parse(ddlUsuarioGrabar.SelectedValue.ToString()));
            //  //  ((ClientSession)Session["objClientSession"]).i_ProfesionId = oSystemUser.i_

            //}

             _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                    l,
                                                    serviceComponentDto,
                                                    ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                    true);




             ////Grabar Auditoría
             //var kvpUsuarios = new List<KeyValuePair<string, int>>();
             //int SystemUserId = int.Parse(((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString());
             //kvpUsuarios.Add(new KeyValuePair<string, int>(Session["ServicioComponentIdMedicina"].ToString(), SystemUserId));
             //HistoryBL.SetAuditoryDataOnServiceComponent(ref objOperationResult, kvpUsuarios,11);

             //Mostrar Auditoria
             var datosAuditoria = HistoryBL.CamposAuditoria(Session["ServicioComponentIdMedicina"].ToString());
             if (datosAuditoria != null)
             {
                 txtMedicina1Auditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                 txtMedicina1AuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                 txtMedicina1AuditorActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                 txtMedicina1Evaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                 txtMedicina1EvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                 txtMedicina1EvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                 txtMedicinaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                 txtMedicinaInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                 txtMedicinaInformadorActualiza.Text = datosAuditoria.FechaHoraEvaluadorEdit;
             }


            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                Session["_serviceComponentFieldsList"] = null;
                Alert.ShowInTop("Se grabó correctamente", MessageBoxIcon.Information);
                //var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
                //Llenar Datos
                txtTalla_18.Text = txtTalla.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID).ServiceComponentFieldValues[0].v_Value1;
                txtPeso_18.Text = txtPeso.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID).ServiceComponentFieldValues[0].v_Value1;
                txtIMC_18.Text = txtImc.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID).ServiceComponentFieldValues[0].v_Value1;
                //string PAS = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID).ServiceComponentFieldValues[0].v_Value1;
                //string PAD = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID).ServiceComponentFieldValues[0].v_Value1;
                txtPA_18.Text = txtParterial.Text + " - " + txtParterialDiatolica.Text;
                txtFreRes_18.Text = txtfres.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).ServiceComponentFieldValues[0].v_Value1;
                txtFreCar_18.Text = txtFcar.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).ServiceComponentFieldValues[0].v_Value1;

                //Llenar Datos 
                txtAltura18TALLA_Internacional.Text = txtTalla.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID).ServiceComponentFieldValues[0].v_Value1;
                txtAltura18PESO_Internacional.Text = txtPeso.Text;//ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID).ServiceComponentFieldValues[0].v_Value1;
                txtAltura18PS_Internacional.Text = txtParterial.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID).ServiceComponentFieldValues[0].v_Value1;
                txtAltura18PD_Internacional.Text = txtParterialDiatolica.Text;// ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID).ServiceComponentFieldValues[0].v_Value1;
                txtAltura18FR_Internacional.Text = txtfres.Text;//ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).ServiceComponentFieldValues[0].v_Value1;
                txtAltura18FC_Internacional.Text = txtFcar.Text;//ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).ServiceComponentFieldValues[0].v_Value1;

                //Llenar Datos  7D
                txtFc_7D.Text = txtFcar.Text;
                txtPAS_7D.Text = txtParterial.Text;
                txtFR_7D.Text = txtfres.Text;
                txtIMC_7D.Text = txtImc.Text;
                txtSAT_7D.Text = txtSatO2.Text;

                ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());

                //Mostrar
                grdComponentes.DataSource = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(ddlConsultorio.SelectedValue.ToString()), Session["ServiceId"].ToString());
                grdComponentes.DataBind();
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
                Session["_serviceComponentFieldsList"] = null;
            }

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(Session["UsuarioLogueado"].ToString());
          
        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void fileDoc_FileSelected(object sender, EventArgs e)
        {
            string Ruta = WebConfigurationManager.AppSettings["Ruta312"].ToString();
            string Dni = Session["DniTrabajador"].ToString();
            string Fecha = Session["FechaServicio"].ToString();
            string Consultorio = "MEDICINA";
            string Ext = fileDoc.FileName.Substring(fileDoc.FileName.Length - 3, 3);
            fileDoc.SaveAs(Ruta + Dni + "-" + Fecha + "-" + Consultorio + "." + Ext);

            Alert.ShowInTop("El archivo subió correctamente", MessageBoxIcon.Information);
            fileDoc.Text = "";
        }

        protected void grdAntecedenteFamiliar_RowCommand(object sender, GridCommandEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "DeleteRegistro")
            {
                string FamilyMedicalAntecedentsId = grdAntecedenteFamiliar.DataKeys[grdAntecedenteFamiliar.SelectedRowIndex][0].ToString();
                _HistoryBL.DeleteFamiliars(ref objOperationResult, FamilyMedicalAntecedentsId, ((ClientSession)Session["objClientSession"]).GetAsList());

                grdAntecedenteFamiliar.DataSource = _HistoryBL.ListarGrillaFamiliars(Session["PersonId"].ToString());
                grdAntecedenteFamiliar.DataBind();
            }
        }

        protected void WindowAddPeligrosEPP_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void xxx_Close(object sender, WindowCloseEventArgs e)
        {

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

        #endregion

        #region Tamizaje Dermatológico
        private void LoadcombosFototipo()
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();
            Utils.LoadDropDownList(ddlGrabarUsuarioFototipo, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }
        private void LoadCombosDermatologico()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            //TAMIZAJE DERMATOLÓGICO
            Utils.LoadDropDownList(ddlAlergiasDermicas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAlergiasMedicamentosas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEnfPropiaPiel, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLupusEritematoso, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEnfermedadTiroidea, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlArtritisReumatoide, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHepatopatias, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPsoriasis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSindromeOvario, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDiabetesMellitus, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlOtrasEnfermedadesSistemicas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMacula, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlVesicula, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNodulo, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPurpura, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPapula, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmpolla, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPlaca, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlComedones, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTuberculo, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlPustula, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlQuiste, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTelangiectasia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEscama, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPetequia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAngioedema, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTumor, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHabon, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEquimosis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEscamas, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlEscaras, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFisura, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExcoriaciones, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCostras, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCicatrices, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAtrofia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLiquenificacion, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEsclerosis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUlceras, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlErosion, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDiscromias, "Value1", "Id", Combo111, DropDownListAction.Select);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaDermatologico, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

        }

        private void ObtenerDatosDermatologicos(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N009-MF000001983") != null)
            {
                SearchControlAndLoadData(TabTamizajeDermatologico, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabTamizajeDermatologico, _tmpServiceComponentsForBuildMenuList);
            }
        }
        
        protected void btnTamizajeDermatologico_Click(object sender, EventArgs e)
        {
           

            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabTamizajeDermatologico, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());


      
            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
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
           
        }

        #endregion
        #region Altura Estructural

        private void LoadCombosAltura18()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo221 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 221);
            var Combo163 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 163);

            Utils.LoadDropDownList(ddlAnteTEC, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlConvulsiones, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMareosMioclo, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAgarofobia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAcrofobia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInsuficiCardiaca, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEstereopsiaAlterada, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTimpanos, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEquilibrio, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSustentacion, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaminarRecta, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaminarOjosVendados, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaminarPuntaTalon, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRotarSilla, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAdiadocoquinesiaDirecta, "Value1", "Id", Combo221, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAdiadocoquinesiaCruzada, "Value1", "Id", Combo221, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAptitudAltura18, "Value1", "Id", Combo163, DropDownListAction.Select);
        }         

        private void ObtenerDatosAltura18(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            //Llenar Datos
            txtTalla_18.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID).ServiceComponentFieldValues[0].v_Value1;
            txtPeso_18.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID).ServiceComponentFieldValues[0].v_Value1;
            txtIMC_18.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID).ServiceComponentFieldValues[0].v_Value1;
            string PAS = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID).ServiceComponentFieldValues[0].v_Value1;
            string PAD = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID).ServiceComponentFieldValues[0].v_Value1;
            txtPA_18.Text = PAS + " - " + PAD;
            txtFreRes_18.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).ServiceComponentFieldValues[0].v_Value1;
            txtFreCar_18.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).ServiceComponentFieldValues[0].v_Value1;
             
            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N009-MF000000781") != null)
            {                
                SearchControlAndLoadData(TabAlturaEstructural, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabAlturaEstructural, _tmpServiceComponentsForBuildMenuList);
            }
        }

        protected void btnAlturaEsctructural_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabAlturaEstructural, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
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
        }

        #endregion

        #region Osteomuscular

        private void LoadCombosOsteo()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo253 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 253);
            var Combo183 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 183);
            var Combo229 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 229);
            var Combo203 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 203);
            var Combo163 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 163);
            var Combo160 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 160);
            var Combo225 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 225);
            var Combo248 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 248);
            //OSTEOMUSCULAR
            Utils.LoadDropDownList(ddlManiLevanCargas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManiEmpujarCargas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManiJalarCargas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPesoMayor25, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEncimaHombro, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManiValvulas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMoviRepetitivos, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMoviForzada, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPosturaForzada, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlPRFManiLevanCargas, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFManiEmpujarCargas, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFManiJalarCargas, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFPesoMayor25, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFEncimaHombro, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFManiValvulas, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFMoviRepetitivos, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFMoviForzada, "Value1", "Id", Combo225, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPRFPosturaForzada, "Value1", "Id", Combo225, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHD1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHI1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI8, "Value1", "Id", Combo248, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlCD1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD8, "Value1", "Id", Combo248, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlCI1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMuneD1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMuneI1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaD1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaI1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobilloD1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobilloI1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaD1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaI1, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI3, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI4, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI5, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI6, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI7, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI8, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlcervical1, "Value1", "Id", Combo183, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsal1, "Value1", "Id", Combo183, DropDownListAction.Select);
            Utils.LoadDropDownList(ddllumbar1, "Value1", "Id", Combo183, DropDownListAction.Select);
            
            Utils.LoadDropDownList(ddldorsal2, "Value1", "Id", Combo229, DropDownListAction.Select);
            Utils.LoadDropDownList(ddllumbar2, "Value1", "Id", Combo229, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlcervical2, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlcervical_extenxion, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlcervical_lateralizacion_derecha, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlcervical_lateralizacion_izquierda, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlcervical_rotacion_derecha, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlcervical_rotacion_izquierda, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlcervical_irradiacion, "Value1", "Id", Combo248, DropDownListAction.Select);

            Utils.LoadDropDownList(ddldorsallumbar, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsallumbar_extension, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsallumbar_lateral_derecha, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsallumbar_lateral_izquierda, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsallumbar_roacion_derecha, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsallumbar_roacion_izquierda, "Value1", "Id", Combo248, DropDownListAction.Select);
            Utils.LoadDropDownList(ddldorsallumbar_irradiacion, "Value1", "Id", Combo248, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlLasegueDere, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLasegueIzq, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddladam_derecho, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddladam_izquierdo, "Value1", "Id", Combo203, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlpie_cavo_derecho, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlpie_cavo_izquierdo, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlpie_plano_derecho, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlpie_plano_izquierdo, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlphalen_derecho, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlphalen_izquierdo, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddltinel_derecho, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddltinel_izquierdo, "Value1", "Id", Combo203, DropDownListAction.Select);

             Utils.LoadDropDownList(ddlfinkelstein_derecho, "Value1", "Id", Combo203, DropDownListAction.Select);
             Utils.LoadDropDownList(ddlfinkelstein_izquierdo, "Value1", "Id", Combo203, DropDownListAction.Select);

             Utils.LoadDropDownList(ddlaptitudOsteo, "Value1", "Id", Combo163, DropDownListAction.Select);

             Utils.LoadDropDownList(ddlColumnaCervicalApofisis, "Value1", "Id", Combo111, DropDownListAction.Select);
             Utils.LoadDropDownList(ddlColumnaCervicalContractura, "Value1", "Id", Combo111, DropDownListAction.Select);

             Utils.LoadDropDownList(ddlColumnaDorsalApofisis, "Value1", "Id", Combo111, DropDownListAction.Select);
             Utils.LoadDropDownList(ddlColumnaDorsalContractura, "Value1", "Id", Combo111, DropDownListAction.Select);
             Utils.LoadDropDownList(ddlColumnaLumbarApofisis, "Value1", "Id", Combo111, DropDownListAction.Select);
             Utils.LoadDropDownList(ddlColumnaLumbarContractura, "Value1", "Id", Combo111, DropDownListAction.Select);

            
                
        }
    
        private void ObtenerDatosOsteomuscular(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();

            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N009-MF000001226") != null)
            {
                SearchControlAndLoadData(TabOsteomuscular, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
            }
           else
           {
                //para obtener valores por defecto
               var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
               SearchControlAndClean(TabOsteomuscular, _tmpServiceComponentsForBuildMenuList);
               CargarValoresDefectoOsteoUC();
             
           }
        }
        private void ObtenerDatosFototipo(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();

            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N009-ME000000411") != null)
            {
                SearchControlAndLoadData(TabFototipo, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
            }
            else
            {
                //para obtener valores por defecto
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabFototipo, _tmpServiceComponentsForBuildMenuList);
                CargarValoresDefectoOsteoUC();

            }
        }
        private void CargarValoresDefectoOsteoUC()
        {
            rbAbdomen1.Checked = true;
            txtAbdomenPuntos.Text = "1";
        }

        protected void btnOsteomuscular_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabOsteomuscular, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
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
        }

        #region MyRegion
          protected void rbAbdomen1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomen1.Checked)
            {
                txtAbdomenPuntos.Text = "1";
                calcularTotal1();
            }          
        }

        protected void rbCadera1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCadera1.Checked)
            {
                txtCaderaPuntos.Text = "1";
                calcularTotal1();
            }            
        }

        protected void rbMuslo1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMuslo1.Checked)
            {
                txtMusloPuntos.Text = "1";
                calcularTotal1();
            }
        }

        protected void rbLateral1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLateral1.Checked)
            {
                txtLateralPuntos.Text = "1";
                calcularTotal1();
            }
        }

        protected void rbAbdomen2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomen2.Checked)
            {
                txtAbdomenPuntos.Text = "2";
                calcularTotal1();
            }
        }

        protected void rbCadera2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCadera2.Checked)
            {
                txtCaderaPuntos.Text = "2";
                calcularTotal1();
            }
        }

        protected void rbMuslo2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMuslo2.Checked)
            {
                txtMusloPuntos.Text = "2";
                calcularTotal1();
            }           
        }

        protected void rbLateral2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLateral2.Checked)
            {
                txtLateralPuntos.Text = "2";
                calcularTotal1();
            }           
        }

        protected void rbAbdomen3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomen3.Checked)
            {
                txtAbdomenPuntos.Text = "3";
                calcularTotal1();
            }
        }

        protected void rbCadera3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCadera3.Checked)
            {
                txtCaderaPuntos.Text = "3";
                calcularTotal1();
            }
        }

        protected void rbMuslo3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMuslo3.Checked)
            {
                txtMusloPuntos.Text = "3";
                calcularTotal1();
            }
        }

        protected void rbLateral3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLateral3.Checked)
            {
                txtLateralPuntos.Text = "3";
                calcularTotal1();
            }
        }

        protected void rbAbdomen4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbdomen4.Checked)
            {
                txtAbdomenPuntos.Text = "4";
                calcularTotal1();
            }
        }

        protected void rbCadera4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCadera4.Checked)
            {
                txtCaderaPuntos.Text = "4";
                calcularTotal1();
            }
        }

        protected void rbMuslo4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMuslo4.Checked)
            {
                txtMusloPuntos.Text = "4";
                calcularTotal1();
            }
        }

        protected void rbLateral4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLateral4.Checked)
            {
                txtLateralPuntos.Text = "4";
                calcularTotal1();
            }
        }

        protected void rbHombroA180_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroA180_1.Checked)
            {
                txtAbduccionHombro180Puntos.Text = "1";
                calcularTotal2();
            }
        }

        protected void rbHombroB1801_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroB1801_1.Checked)
            {
                txtAduccionHombro60Puntos.Text = "1";
                calcularTotal2();
            }
        }

        protected void rbHombro90_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombro90_1.Checked)
            {
                txtRotacionHombro90Puntos.Text = "1";
                calcularTotal2();
            }
        }

        protected void rbHombroInternal_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroInternal_1.Checked)
            {
                txtInternaHombroPuntos.Text = "1";
                calcularTotal2();
            }
        }

        protected void rbHombroA180_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroA180_2.Checked)
            {
                txtAbduccionHombro180Puntos.Text = "2";
                calcularTotal2();
            }
        }

        protected void rbHombroB1801_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroB1801_2.Checked)
            {
                txtAduccionHombro60Puntos.Text = "2";
                calcularTotal2();
            }
        }

        protected void rbHombro90_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombro90_2.Checked)
            {
                txtRotacionHombro90Puntos.Text = "2";
                calcularTotal2();
            }
        }

        protected void rbHombroInternal_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroInternal_2.Checked)
            {
                txtInternaHombroPuntos.Text = "2";
                calcularTotal2();
            }
        }

        protected void rbHombroA180_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroA180_3.Checked)
            {
                txtAbduccionHombro180Puntos.Text = "3";
                calcularTotal2();
            }
        }

        protected void rbHombroB1801_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroB1801_3.Checked)
            {
                txtAduccionHombro60Puntos.Text = "3";
                calcularTotal2();
            }
        }

        protected void rbHombro90_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombro90_3.Checked)
            {
                txtRotacionHombro90Puntos.Text = "3";
                calcularTotal2();
            }
        }

        protected void rbHombroInternal_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHombroInternal_3.Checked)
            {
                txtInternaHombroPuntos.Text = "3";
                calcularTotal2();
            }
        }

        private void calcularTotal1()
        {
            int p1 = txtAbdomenPuntos.Text == "" ? 0 : int.Parse(txtAbdomenPuntos.Text.ToString());
            int p2 = txtCaderaPuntos.Text == "" ? 0 : int.Parse(txtCaderaPuntos.Text.ToString());
            int p3 = txtMusloPuntos.Text == "" ? 0 : int.Parse(txtMusloPuntos.Text.ToString());
            int p4 = txtLateralPuntos.Text == "" ? 0 : int.Parse(txtLateralPuntos.Text.ToString());
            txtTotal1.Text = (p1 + p2 + p3 + p4).ToString();

        }

        private void calcularTotal2()
        {
            int p1 = txtAbduccionHombro180Puntos.Text == "" ? 0 : int.Parse(txtAbduccionHombro180Puntos.Text.ToString());
            int p2 = txtAduccionHombro60Puntos.Text == "" ? 0 : int.Parse(txtAduccionHombro60Puntos.Text.ToString());
            int p3 = txtRotacionHombro90Puntos.Text == "" ? 0 : int.Parse(txtRotacionHombro90Puntos.Text.ToString());
            int p4 = txtInternaHombroPuntos.Text == "" ? 0 : int.Parse(txtInternaHombroPuntos.Text.ToString());
            txtTotal2.Text = (p1 + p2 + p3 + p4).ToString();

        }

        #endregion
        #endregion

        #region OsteomuscularInternacional

        private void LoadCombosOsteoInternacional()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo253 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 253);
            var Combo183 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 183);
            var Combo229 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 229);
            var Combo203 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 203);
            var Combo254 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 254);
            var Combo160 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 160);
            var Combo265 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 265);
            
            //OSTEOMUSCULAR
            Utils.LoadDropDownList(ddlNuca1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNuca2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNuca3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHombroDerecho1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroDerecho2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroDerecho3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHombroIzquierdo1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroIzquierdo2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroIzquierdo3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAmbosHombros1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbosHombros2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbosHombros3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCodoDerecho1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoDerecho2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoDerecho3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCodoIzquierdo1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoIzquierdo2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoIzquierdo3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAmboscodos1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmboscodos2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmboscodos3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlManosDerecha1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosDerecha2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosDerecha3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlManosIzquierda1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosIzquierda2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosIzquierda3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAmbasManos1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbasManos2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbasManos3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlColumnadorsal1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnadorsal2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnadorsal3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlColumnaLumbar1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnaLumbar2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnaLumbar3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaDerecha1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaDerecha2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaDerecha3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaIzquierda1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaIzquierda2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaIzquierda3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaDerecha1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaDerecha2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaDerecha3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaIzquierda1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaIzquierda2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaIzquierda3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobillosDerecho1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosDerecho2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosDerecho3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobillosIzquierdo1_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosIzquierdo2_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosIzquierdo3_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);



            Utils.LoadDropDownList(ddlHD1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHI1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlCD1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlCI1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMuneD1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMuneI1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaD1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaI1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobilloD1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobilloI1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaD1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaI1_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI2_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI3_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI4_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI5_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI6_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI7_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI8_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCervical_Inter, "Value1", "Id", Combo183, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsal_Inter, "Value1", "Id", Combo183, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLumbar_Inter, "Value1", "Id", Combo183, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlDorsalEjeLateral_Inter, "Value1", "Id", Combo229, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLumbarEjeLateral_Inter, "Value1", "Id", Combo229, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCervicalFlexion_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalExtension_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalLatDere_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalLatIzq_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalRotaDere_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalRotaIzq_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalIrradiacion_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDorsoFlexion_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoExtension_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoLateDere_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoLateIzq_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoRotaDere_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoRotaIzq_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoIrradiacion_Inter, "Value1", "Id", Combo160, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlLasegueDere_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLasegueIzq_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSchoberDere_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSchoberIzq_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCodoDerecho_Inter, "Value1", "Id", Combo265, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoIzquierdo_Inter, "Value1", "Id", Combo265, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPieDerecho_Inter, "Value1", "Id", Combo265, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPieIzquierdo_Inter, "Value1", "Id", Combo265, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlPhalenDerecha_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPhalenIzquierda_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTinelDerecha_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTinelIzquierda_Inter, "Value1", "Id", Combo203, DropDownListAction.Select);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaOsteo, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }

        private void ObtenerDatosOsteomuscularInternacional(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();

            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            var x = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N005-MF000001471");
            if (x != null)
            {
                SearchControlAndLoadData(TabOsteomuscularInternacional, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
                #region Campos de auditoria
                var scId = _serviceBL.ObtenerScId(pServiceId, "N005-ME000000046");
                var datosAuditoria = HistoryBL.CamposAuditoria(scId);
                if (datosAuditoria != null)
                {
                    txtOsteoMuscularAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtOsteoMuscularAuditorInsertar.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtOsteoMuscularAuditorEditar.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txttxtOsteoMuscularEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txttxtOsteoMuscularEvaluadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txttxtOsteoMuscularEvaluadorEvaluar.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txttxtOsteoMuscularInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txttxtOsteoMuscularInformadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txttxtOsteoMuscularInformadorEvaluar.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabOsteomuscularInternacional, _tmpServiceComponentsForBuildMenuList);

                txtOsteoMuscularAuditor.Text = "";
                txtOsteoMuscularAuditorInsertar.Text = "";
                txtOsteoMuscularAuditorEditar.Text = "";

                txttxtOsteoMuscularEvaluador.Text = "";
                txttxtOsteoMuscularEvaluadorInsertar.Text = "";
                txttxtOsteoMuscularEvaluadorEvaluar.Text = "";

                txttxtOsteoMuscularInformador.Text = "";
                txttxtOsteoMuscularInformadorInsertar.Text = "";
                txttxtOsteoMuscularInformadorEvaluar.Text = "";
            }
        }
        
        protected void btnOsteoInternacional_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabaOsteo.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabOsteomuscularInternacional, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002819") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkEvaluacionNormalOsteo_CI.Checked?"1":"0", "N009-MF000002819", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N005-ME000000046";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabaOsteo.SelectedValue.ToString());


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   l,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);


            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N005-ME000000046");
            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtOsteoMuscularAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtOsteoMuscularAuditorInsertar.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtOsteoMuscularAuditorEditar.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txttxtOsteoMuscularEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txttxtOsteoMuscularEvaluadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txttxtOsteoMuscularEvaluadorEvaluar.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txttxtOsteoMuscularInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txttxtOsteoMuscularInformadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txttxtOsteoMuscularInformadorEvaluar.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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


        #region Tamizaje Dermatológico Internacional

        private void LoadCombosDermatologicoInternacional()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            //TAMIZAJE DERMATOLÓGICO
            Utils.LoadDropDownList(ddlAlergiasDermicas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAlergiasMedicamentosas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEnfPropiaPiels_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLupusEritematosos_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEnfermedadTiroideas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlArtritisReumatoides_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHepatopatiass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPsoriasiss_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSindromeOvarios_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDiabetesMellituss_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlOtrasEnfermedadesSistemicass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMaculas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlVesiculas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNodulos_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPurpuras_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPapulas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmpollas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPlacas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlComedoness_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTuberculos_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlPustulas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlQuistes_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTelangiectasias_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEscamas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPetequias_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAngioedemas_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTumors_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHabons_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEquimosiss_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEscamass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlEscarass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFisuras_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExcoriacioness_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCostrass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCicatricess_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAtrofias_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLiquenificacions_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEsclerosiss_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUlcerass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlErosions_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDiscromiass_Inter, "Value1", "Id", Combo111, DropDownListAction.Select);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaDermatologico, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);

        }

        private void ObtenerDatosDermatologicosInternacional(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N005-MF000001983") != null)
            {
                SearchControlAndLoadData(TabDermatologico_Internacional, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
                #region Campos de auditoria
                //Obtener scId
                var scId = _serviceBL.ObtenerScId(pServiceId, "N005-ME000000116");
                var datosAuditoria = HistoryBL.CamposAuditoria(scId);
                if (datosAuditoria != null)
                {
                    txtDermatoloCIAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtDermatoloCIAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtDermatoloCIActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtDermatoloCIEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtDermatoloCIEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtDermatoloCIEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtDermatoloCIInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtDermatoloCIInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtDermatoloCIInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabDermatologico_Internacional, _tmpServiceComponentsForBuildMenuList);

                txtDermatoloCIAuditor.Text = "";
                txtDermatoloCIAuditorInsercion.Text = "";
                txtDermatoloCIActualizacion.Text = "";

                txtDermatoloCIEvaluador.Text = "";
                txtDermatoloCIEvaluadorInserta.Text = "";
                txtDermatoloCIEvaluadorActualizacion.Text = "";

                txtDermatoloCIInformador.Text = "";
                txtDermatoloCIInformadorInserta.Text = "";
                txtDermatoloCIInformadorActualizacion.Text = "";
            }
        }


        protected void btnDermatologicoInternacional_Click(object sender, EventArgs e)
        {

            if (ddlUsuarioGrabaDermatologico.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabDermatologico_Internacional, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());
            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002818") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkSinDermatopatias.Checked?"1":"0", "N009-MF000002818", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N005-ME000000116";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabaDermatologico.SelectedValue.ToString());


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   l,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);

            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N005-ME000000116");

            var datosAuditoria = HistoryBL.CamposAuditoria(scId.ToString());
            if (datosAuditoria != null)
            {
                txtDermatoloCIAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtDermatoloCIAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtDermatoloCIActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtDermatoloCIEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtDermatoloCIEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtDermatoloCIEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtDermatoloCIInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtDermatoloCIInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtDermatoloCIInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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

        #region Sintomático Respiratorio

        void LoadCombosSintomaticoRespiratorio()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);

            Utils.LoadDropDownList(ddlSintomaticoTuberculosis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoTos15dias, "Value1", "Id", Combo111, DropDownListAction.Select);            
            //Utils.LoadDropDownList(ddlSintomaticoRecibioTto, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoBajaPeso, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlSintomaticoTos, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoExpecto, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoSudoracion, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoFamiliares, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoSospecha, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSintomaticoConclusion, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlSintomaticoRequiere, "Value1", "Id", Combo111, DropDownListAction.Select);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaSintomatico, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }

        private void ObtenerDatosSintomaticoRespiratorio(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];

            //Obtener Resultados para mostar
            txtSintomaticoResultRX.Text = _serviceBL.GetDiagnosticByServiceIdAndCategory(pServiceId, 11) == "" ? "NO SE HAN REGISTRADO DATOS" : _serviceBL.GetDiagnosticByServiceIdAndCategory(pServiceId, 6);


            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N009-MF000002015") != null)
            {
                SearchControlAndLoadData(TabSintomaticoRespiratorio, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
                #region Campos de auditoria
                var scId = _serviceBL.ObtenerScId(pServiceId, "N009-ME000000116");
                var datosAuditoria = HistoryBL.CamposAuditoria(scId);
                if (datosAuditoria != null)
                {
                    txtSintomaticoAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtSintomaticoAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtSintomaticoAuditorActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtSintomaticoEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtSintomaticoEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtSintomaticoEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtSintomaticoInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtSintomaticoInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtSintomaticoInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabSintomaticoRespiratorio, _tmpServiceComponentsForBuildMenuList);

                txtSintomaticoAuditor.Text = "";
                txtSintomaticoAuditorInsercion.Text = "";
                txtSintomaticoAuditorActualizacion.Text = "";

                txtSintomaticoEvaluador.Text = "";
                txtSintomaticoEvaluadorInserta.Text = "";
                txtSintomaticoEvaluadorActualizacion.Text = "";

                txtSintomaticoInformador.Text = "";
                txtSintomaticoInformadorInserta.Text = "";
                txtSintomaticoInformadorActualizacion.Text = "";

            }
        }

        protected void btnSintomaticoRespiratorio_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabaSintomatico.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Warning);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabSintomaticoRespiratorio, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002023") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(ddlSintomaticoConclusion.SelectedValue.ToString(), "N009-MF000002023", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N009-ME000000116";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabaSintomatico.SelectedValue.ToString());


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   l,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);

            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N009-ME000000116");
            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtSintomaticoAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtSintomaticoAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtSintomaticoAuditorActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtSintomaticoEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtSintomaticoEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtSintomaticoEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtSintomaticoInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtSintomaticoInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtSintomaticoInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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

        #region Anexo 16

        private void LoadCombos16()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo218 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 135);
            var Combo100 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 100);
            var Combo208 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 208);
            var Combo204 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 204);
            


            // 16
            Utils.LoadDropDownList(ddlTipoDocumento_16, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 106), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDepartamento_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDepartamento(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProvincia_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDistrito_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlResideSiNo_16, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTipoSeguro_16, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 188), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEstadoCivil_16, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 101), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlGradoInstruccion_16, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 108), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMac_16, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 134), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCabeza_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCuello_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNariz_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBAFL_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlReflejosPupilares_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtreSuper_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlExtreInfe_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlReflejosOsteo_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMarcha_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnaVertebral_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAbdomen_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAnillosAngui_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHernias_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlVarices_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrganosGenitales_16, "Value1", "Id", Combo218, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlGanglios_16, "Value1", "Id", Combo218, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlGenero_16, "Value1", "Id", Combo100, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlLugarLabor_16, "Value1", "Id", Combo208, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltitudLabor_16, "Value1", "Id", Combo204, DropDownListAction.Select);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaAnexo16, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }

        private void ObtenerDatos16(string pServiceId, string pPersonId)
        {

            OperationResult objOperationResult = new OperationResult();
            #region MyRegion

            var Obj16 = _serviceBL.RetornarDatosEmpresaFiliacion(pServiceId, pPersonId);

            txtMineralesExplotados_16.Text = Session["v_ExploitedMineral"].ToString();       
            ddlLugarLabor_16.SelectedValue = Session["i_AltitudeWorkId"] == null ? "-1" : Session["i_AltitudeWorkId"].ToString();
            ddlAltitudLabor_16.SelectedValue = Session["i_PlaceWorkId"] == null ? "-1" : Session["i_PlaceWorkId"].ToString();

            //Datos de la empresa
            txtRazonSocial_16.Text = Obj16[0].EmpresaCliente == null ? "" : Obj16[0].EmpresaCliente.ToString().ToUpper();
            txtRucEmpresa_16.Text = Obj16[0].RucEmpresaCliente == null ? "" : Obj16[0].RucEmpresaCliente.ToString().ToUpper();
            txtActividadEconomica_16.Text = Obj16[0].ActividadEconomicaEmpresaCliente == null ? "" : Obj16[0].ActividadEconomicaEmpresaCliente.ToString().ToUpper();
            txtLugarTrabajo_16.Text = Obj16[0].LugarTrabajo == null ? "" : Obj16[0].LugarTrabajo.ToString().ToUpper();
            txtPuestoTrabajo_16.Text = Obj16[0].PuestoTrabajo == null ? "" : Obj16[0].PuestoTrabajo.ToString().ToUpper();
            txtNombreCompletoTrabajador_16.Text = Obj16[0].Trabajador == null ? "" : Obj16[0].Trabajador.ToString().ToUpper();

            //Filiación      
            dpcFechaNacimiento_16.SelectedDate = Obj16[0].FechaNacimiento.Value;
            txtEdad_16.Text = _serviceBL.GetAge(Obj16[0].FechaNacimiento.Value).ToString();
            ddlTipoDocumento_16.SelectedValue = Obj16[0].TipoDocumentoId == null ? "-1" : Obj16[0].TipoDocumentoId.Value.ToString();
            txtNroDocumento_16.Text = Obj16[0].NroDocumento == null ? "" : Obj16[0].NroDocumento.ToString();
            txtDireccionFiscal_16.Text = Obj16[0].DireccionFiscal == null ? "" : Obj16[0].DireccionFiscal.ToString().ToUpper();
            txtLugarNacimiento_16.Text = Obj16[0].v_BirthPlace == null ? "" : Obj16[0].v_BirthPlace.ToString().ToUpper();
            ddlDepartamento_16.SelectedValue = Obj16[0].DepartamentoId == null ? "-1" : Obj16[0].DepartamentoId.Value.ToString();
            Utils.LoadDropDownList(ddlProvincia_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamento.SelectedValue.ToString())), DropDownListAction.Select);
            ddlProvincia_16.SelectedValue = Obj16[0].ProvinciaId == null ? "-1" : Obj16[0].ProvinciaId.Value.ToString();
            Utils.LoadDropDownList(ddlDistrito_16, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvincia.SelectedValue.ToString())), DropDownListAction.Select);
            ddlDistrito_16.SelectedValue = Obj16[0].DistritoId == null ? "-1" : Obj16[0].DistritoId.Value.ToString();
            ddlResideSiNo_16.SelectedValue = Obj16[0].ResideLugarTrabajo == null ? "0" : Obj16[0].ResideLugarTrabajo.Value.ToString();
            txtTiempoResidencia_16.Text = Obj16[0].TiempoResidencia == null ? "" : Obj16[0].TiempoResidencia.ToString();
            txtTelefonos_16.Text = Obj16[0].Telefono == null ? "" : Obj16[0].Telefono.ToString();
            ddlTipoSeguro_16.SelectedValue = Obj16[0].TipoSeguroId == null ? "-1" : Obj16[0].TipoSeguroId.Value.ToString();
            txtCorreoElectronico_16.Text = Obj16[0].Email == null ? "" : Obj16[0].Email.ToString().ToUpper();
            ddlEstadoCivil_16.SelectedValue = Obj16[0].EstadoCivilId == null ? "-1" : Obj16[0].EstadoCivilId.Value.ToString();
            ddlGradoInstruccion_16.SelectedValue = Obj16[0].GradoInstruccionId == null ? "-1" : Obj16[0].GradoInstruccionId.Value.ToString();
            txtNroHijosVivos_16.Text = Obj16[0].HijosVivos == null ? "" : Obj16[0].HijosVivos.Value.ToString();
            txtNroHijosMuertos_16.Text = Obj16[0].HijosMuertos == null ? "" : Obj16[0].HijosMuertos.Value.ToString();
            txtAnamnesis_16.Text = Obj16[0].Anamnesis == null ? "PACINETE APARENTEMENTE SANO." : Obj16[0].Anamnesis.ToString();
            txtMenarquia_16.Text = Obj16[0].v_Menarquia;
            txtGestacion_16.Text = Obj16[0].v_Gestapara;
            txtFum_16.Text = Obj16[0].v_FechaUltimaRegla;
            ddlMac_16.SelectedValue = Obj16[0].i_MacId.ToString();
            txtRegimenCatamenial_16.Text = Obj16[0].v_CatemenialRegime;
            txtCirugiaGineco_16.Text = Obj16[0].v_CiruGine;
            txtUltimoPAP_16.Text = Obj16[0].v_FechaUltimoPAP;
            txtResultadoPAP_16.Text = Obj16[0].v_ResultadoMamo;
            txtUltimaMamo_16.Text = Obj16[0].v_FechaUltimaMamo;
            txtResultadoMamo_16.Text = Obj16[0].v_ResultadosPAP;
            ddlGenero_16.SelectedValue = Obj16[0].i_GeneroId == null ? "-1" : Obj16[0].i_GeneroId.Value.ToString();
            //ddlAptitud.SelectedValue = Obj16[0].i_StatusAptitud.ToString();
            //txtComentarioAptitud.Text = Obj16[0].Comentario == null ? "" : Obj16[0].Comentario.ToString();
            //txtComentarioMedico.Text = Obj16[0].ComentarioMedico == null ? "" : Obj16[0].ComentarioMedico.ToString();
            //txtRestriccionesAptitud.Text = Obj16[0].Restricciones == null ? "" : Obj16[0].Restricciones.ToString();

            grdAntecedentesOcupacionales_16.DataSource = Obj16[0].ListaHistoriaOcupacional;
            grdAntecedentesOcupacionales_16.DataBind();

            grdAntecedentesPersonales_16.DataSource = Obj16[0].ListaMedicosPersonales;
            grdAntecedentesPersonales_16.DataBind();

            grdHabitosNocivos_16.DataSource = Obj16[0].ListaHabitosNosivos;
            grdHabitosNocivos_16.DataBind();

            grdAntecedenteFamiliar_16.DataSource = Obj16[0].ListaMedicosFamiliares;
            grdAntecedenteFamiliar_16.DataBind();

           
            var oExamenTriaje = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 10);
            var oExamenFisico7C = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 11);
            var objExamenTriaje = _serviceBL.GetServiceComponentFields_AMC312(oExamenTriaje == null ? "" : oExamenTriaje[0].ServicioComponentId, oExamenFisico7C == null ? "" : oExamenFisico7C[0].ServicioComponentId, pServiceId);

            Session["ServicioComponentIdTriaje"] = oExamenTriaje == null ? null : oExamenTriaje[0].ServicioComponentId;
            Session["ServicioComponentIdFisico"] = oExamenFisico7C == null ? null : oExamenFisico7C[0].ServicioComponentId;
            if (objExamenTriaje.Count != 0)
            {
                //SearchControlAndLoadData(TabAnexo16, oExamenFisico7C[0].ServicioComponentId, objExamenTriaje);
                SearchControlAndLoadData(TabAnexo16, Session["ServicioComponentIdMedicina"].ToString(), (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"]);
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabAnexo16, _tmpServiceComponentsForBuildMenuList);
            }
            #endregion
        }

        protected void btngrabarAnexo16_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabaAnexo16.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }

            OperationResult objOperationResult = new OperationResult();
            personDto objpersonDto = new personDto();
            PacientBL objPacientBL = new PacientBL();
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            //Actualizar Datos del Filiación
            objpersonDto = objPacientBL.GetPerson(ref objOperationResult, Session["PersonId"].ToString());

            objpersonDto.d_Birthdate = dpcFechaNacimiento.SelectedDate;
            objpersonDto.i_DocTypeId = int.Parse(ddlTipoDocumento.SelectedValue.ToString());
            objpersonDto.v_DocNumber = txtNroDocumento.Text;
            objpersonDto.v_AdressLocation = txtDireccionFiscal.Text;
            objpersonDto.i_DepartmentId = int.Parse(ddlDepartamento.SelectedValue.ToString());
            objpersonDto.i_ProvinceId = int.Parse(ddlProvincia.SelectedValue.ToString());
            objpersonDto.i_DistrictId = int.Parse(ddlDistrito.SelectedValue.ToString());
            objpersonDto.i_ResidenceInWorkplaceId = int.Parse(ddlResideSiNo.SelectedValue.ToString());
            objpersonDto.v_ResidenceTimeInWorkplace = txtTiempoResidencia.Text;
            objpersonDto.v_TelephoneNumber = txtTelefonos.Text;
            objpersonDto.i_TypeOfInsuranceId = int.Parse(ddlTipoSeguro.SelectedValue.ToString());
            objpersonDto.v_Mail = txtCorreoElectronico.Text;
            objpersonDto.i_MaritalStatusId = int.Parse(ddlEstadoCivil.SelectedValue.ToString());
            objpersonDto.i_LevelOfId = int.Parse(ddlGradoInstruccion.SelectedValue.ToString());
            objpersonDto.i_NumberLiveChildren = txtNroHijosVivos.Text == "" ? (int?)null : int.Parse(txtNroHijosVivos.Text.ToString());
            objpersonDto.i_NumberDeadChildren = txtNroHijosMuertos.Text == "" ? (int?)null : int.Parse(txtNroHijosMuertos.Text.ToString());
            objpersonDto.v_BirthPlace = txtLugarNacimiento_16.Text;
            //Grabar Datos Filiacion
            objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, ((ClientSession)Session["objClientSession"]).GetAsList(), txtNroDocumento.Text, txtNroDocumento.Text);


            //Grabar Antecedentes Ginecológicos
            serviceDto serviceDTO = new serviceDto();

            serviceDTO.v_ServiceId = Session["ServiceId"].ToString(); ;
            serviceDTO.v_MainSymptom = "";
            serviceDTO.i_TimeOfDisease = (int?)null;
            serviceDTO.i_TimeOfDiseaseTypeId = (int?)null;
            serviceDTO.v_Story = txtAnamnesis.Text;
            serviceDTO.i_DreamId = -1;
            serviceDTO.i_UrineId = -1;
            serviceDTO.i_DepositionId = -1;
            serviceDTO.v_Findings = "";
            serviceDTO.i_AppetiteId = -1;
            serviceDTO.i_ThirstId = -1;
            serviceDTO.i_HasSymptomId = (int?)null;
            serviceDTO.d_PAP = (DateTime?)null;
            serviceDTO.d_Mamografia = (DateTime?)null;
            serviceDTO.v_Findings = "";
            serviceDTO.v_Menarquia = txtMenarquia.Text;
            serviceDTO.v_Gestapara = txtGestacion.Text;
            //serviceDTO.v_FechaUltimaRegla = txtFum.Text;
            serviceDTO.i_MacId = int.Parse(ddlMac.SelectedValue.ToString());
            serviceDTO.v_CatemenialRegime = txtRegimenCatamenial.Text;
            serviceDTO.v_CiruGine = txtCirugiaGineco.Text;
            serviceDTO.v_FechaUltimoPAP = txtUltimoPAP.Text;
            serviceDTO.v_ResultadosPAP = txtResultadoPAP.Text;
            serviceDTO.v_FechaUltimaMamo = txtUltimaMamo.Text;
            serviceDTO.v_ResultadoMamo = txtResultadoMamo.Text;
            serviceDTO.i_AptitudeStatusId = int.Parse(Session["i_AptitudeStatusId"].ToString());
            _serviceBL.UpdateAnamnesis(ref objOperationResult, serviceDTO, ((ClientSession)Session["objClientSession"]).GetAsList());

            SearchControlAndSetValues(Panel21, Session["ServicioComponentIdTriaje"].ToString());

            var oTriaje = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdTriaje"].ToString());


            SearchControlAndSetValues(TabAnexo16, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002137") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkPersonaSana_16.Checked ?"1":"0", "N009-MF000002137", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N009-ME000000052";
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


         var datosAuditoria = HistoryBL.CamposAuditoria(Session["ServicioComponentIdMedicina"].ToString());
            if (datosAuditoria != null)
            {
                txtAnexo16Auditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtAnexo16AuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtAnexo16Actualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtAnexo16Evaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtAnexo16EvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtAnexo16EvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtAnexo16Informador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtAnexo16InformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtAnexo16InformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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

        #region 7 D

        void LoadCombos7D()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo163 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 163);

            Utils.LoadDropDownList(ddlAnemia_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCirugia_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDesprdenes_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDiabetes_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHipertension_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEmbarazo_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNeurologicos_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInfecciones_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProblemasCardiacos_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlObesidad_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProblemasRespi_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProblemasOftal_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProblemasDigestivos_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlApnea_7D, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAlergias_7D, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAptitud_7D, "Value1", "Id", Combo163, DropDownListAction.Select);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGraba7D, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }

        private void ObtenerDatos7D(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
            //Llenar Datos
            txtFc_7D.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).ServiceComponentFieldValues[0].v_Value1;
            string PAS = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID).ServiceComponentFieldValues[0].v_Value1;
            string PAD = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID).ServiceComponentFieldValues[0].v_Value1;
            txtPAS_7D.Text = PAS + " - " + PAD;
            txtFR_7D.Text = txtPcadera.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).ServiceComponentFieldValues[0].v_Value1;
            txtIMC_7D.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID).ServiceComponentFieldValues[0].v_Value1;
            txtSAT_7D.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_SAT_O2_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_SAT_O2_ID).ServiceComponentFieldValues[0].v_Value1;

            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N002-MF000000306") != null)
            {             
                SearchControlAndLoadData(Tab7D, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);
                #region Campos de auditoria
                //Obtener scId
                var scId = _serviceBL.ObtenerScId(pServiceId, "N002-ME000000045");
                var datosAuditoria = HistoryBL.CamposAuditoria(scId);
                if (datosAuditoria != null)
                {
                    txt7DAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txt7DAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txt7DActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txt7DEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txt7DEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txt7DEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txt7DInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txt7DInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txt7DInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(Tab7D, _tmpServiceComponentsForBuildMenuList);
                txt7DAuditor.Text = "";
                txt7DAuditorInsercion.Text = "";
                txt7DActualizacion.Text = "";

                txt7DEvaluador.Text = "";
                txt7DEvaluadorInserta.Text = "";
                txt7DEvaluadorActualizacion.Text = "";

                txt7DInformador.Text = "";
                txt7DInformadorInserta.Text = "";
                txt7DInformadorActualizacion.Text = "";
            }
        }

        protected void btn7D_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGraba7D.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(Tab7D, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdMedicina"].ToString());
            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N002-MF000000323") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(ddlAptitud_7D.SelectedValue.ToString(), "N002-MF000000323", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N002-ME000000045";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGraba7D.SelectedValue.ToString());


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   l,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);

            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N002-ME000000045");

            var datosAuditoria = HistoryBL.CamposAuditoria(scId.ToString());
            if (datosAuditoria != null)
            {
                txt7DAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txt7DAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txt7DActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txt7DEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txt7DEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txt7DEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txt7DInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txt7DInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txt7DInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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
                        if (ComponentFieldId == "N009-MF000003204" || ComponentFieldId == "N009-ME000000411")
                        {
                            bool llega = true;
                        }
                        ((TextBox)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

                    }
                }

                if (ctrl is NumberBox)
                {
                    if (((NumberBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((NumberBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((NumberBox)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

                    }
                }

                if (ctrl is CheckBox)
                {
                    //if ("N009-OTS00000015" == ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString())
                    //{
                    //    var x = ListaValores.Find(p => p.v_ComponentFieldsId == "N009-OTS00000015");
                    //    var y = x.ServiceComponentFieldValues;
                    //    var z = y[0].v_Value1;
                    //}

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
                        string Value = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;                        
                        if (Value != null)
                        {
                            SetearRadioButtonEvaluacionMusculo(ComponentFieldId, Value);
                        }
                        //((RadioButton)ctrl).Checked = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? false : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                    }
                }

                if (ctrl.HasControls())
                    SearchControlAndLoadData(ctrl, ServiceComponentId, ListaValores);

            }
        }

        private void SetearRadioButtonEvaluacionMusculo(string pstrComponentFieldId, string pstrValue)
        {
            if (pstrComponentFieldId == "N009-MF000000660" )
            {
                if (pstrValue != "0")
                {
                    rdoPulmonNormal.Checked = true;
                }
            }
            if (pstrComponentFieldId == "N009-MF000000661")
            {
                if (pstrValue != "0")
                {
                    rdoPulmonAnormal.Checked = true;
                }
            }
            switch (pstrComponentFieldId)
            {
                case "N009-MF000000663":
                    if (pstrValue != "0")
                    {
                        rdoTactoRectalNormal.Checked = true;
                    }
                    break;
                case "N009-MF000000664":
                    if (pstrValue != "0")
                    {
                        rdoTactoRectalAnormal.Checked = true;
                    }
                    break;
                case "N009-MF000000666":
                    if (pstrValue != "0")
                    {
                        rdoTactoRectalNoRealizo.Checked = true;
                    }
                    break;
                default:
                    break;
            }
            

            if (pstrComponentFieldId == Constants.UC_OSTEO_FLEXIBILIDAD)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbAbdomen1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbAbdomen2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbAbdomen3.Checked = true;
                }
                else if (pstrValue.ToString() == "4")
                {
                    rbAbdomen4.Checked = true;
                }
            }

            if (pstrComponentFieldId == Constants.UC_OSTEO_CADERA)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbCadera1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbCadera2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbCadera3.Checked = true;
                }
                else if (pstrValue.ToString() == "4")
                {
                    rbCadera4.Checked = true;
                }
            }
            if (pstrComponentFieldId == Constants.UC_OSTEO_MUSLO)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbMuslo1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbMuslo2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbMuslo3.Checked = true;
                }
                else if (pstrValue.ToString() == "4")
                {
                    rbMuslo4.Checked = true;
                }
            }

            if (pstrComponentFieldId == Constants.UC_OSTEO_ABDOMEN)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbLateral1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbLateral2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbLateral3.Checked = true;
                }
                else if (pstrValue.ToString() == "4")
                {
                    rbLateral4.Checked = true;
                }
            }

            if (pstrComponentFieldId == Constants.UC_OSTEO_ABD_180)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbHombroA180_1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbHombroA180_2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbHombroA180_3.Checked = true;
                }
            }

            if (pstrComponentFieldId == Constants.UC_OSTEO_ABD_60)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbHombroB1801_1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbHombroB1801_2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbHombroB1801_3.Checked = true;
                }
            }

            if (pstrComponentFieldId == Constants.UC_OSTEO_ABD_90)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbHombro90_1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbHombro90_2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbHombro90_3.Checked = true;
                }
            }

            if (pstrComponentFieldId == Constants.UC_OSTEO_ROTACION)
            {
                if (pstrValue.ToString() == "1")
                {
                    rbHombroInternal_1.Checked = true;
                }
                else if (pstrValue.ToString() == "2")
                {
                    rbHombroInternal_2.Checked = true;
                }
                else if (pstrValue.ToString() == "3")
                {
                    rbHombroInternal_3.Checked = true;
                }
            }

            
        }

        private void SearchControlAndSetValues(Control ctrlContainer, string pstrServiceComponentId)
        {
            if (pstrServiceComponentId == "N009-MF000002135")
            {
                
            }
           
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
                if (ctrl is NumberBox)
                {
                    if (((NumberBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        var x = ((NumberBox)ctrl).Text;
                        var y = ((NumberBox)ctrl).Attributes.GetValue("Tag");

                        serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

                        serviceComponentFields.v_ComponentFieldsId = ((NumberBox)ctrl).Attributes.GetValue("Tag").ToString();
                        serviceComponentFields.v_ServiceComponentId = pstrServiceComponentId;

                        _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                        serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                        serviceComponentFieldValues.v_Value1 = ((NumberBox)ctrl).Text;
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
                    if ("N009-OTS00000015" == ((CheckBox)ctrl).Attributes.GetValue("Tag").ToString())
                    {
                    }

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

                        if (componentFieldId == Constants.UC_OSTEO_FLEXIBILIDAD)
                        {
                            if (rbAbdomen1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbAbdomen2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbAbdomen3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                            else if (rbAbdomen4.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "4";
                            }
                        }
                        if (componentFieldId == Constants.UC_OSTEO_CADERA)
                        {
                            if (rbCadera1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbCadera2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbCadera3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                            else if (rbCadera4.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "4";
                            }
                        }
                        if (componentFieldId == Constants.UC_OSTEO_MUSLO)
                        {
                            if (rbMuslo1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbMuslo2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbMuslo3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                            else if (rbMuslo4.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "4";
                            }
                        }

                        if (componentFieldId == Constants.UC_OSTEO_ABDOMEN)
                        {
                            if (rbLateral1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbLateral2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbLateral3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                            else if (rbLateral4.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "4";
                            }
                        }

                        if (componentFieldId == Constants.UC_OSTEO_ABD_180)
                        {
                            if (rbHombroA180_1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbHombroA180_2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbHombroA180_3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                        }

                        if (componentFieldId == Constants.UC_OSTEO_ABD_60)
                        {
                            if (rbHombroB1801_1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbHombroB1801_2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbHombroB1801_3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                        }

                        if (componentFieldId == Constants.UC_OSTEO_ABD_90)
                        {
                            if (rbHombro90_1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbHombro90_2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbHombro90_3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                        }

                        if (componentFieldId == Constants.UC_OSTEO_ROTACION)
                        {
                            if (rbHombroInternal_1.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "1";
                            }
                            else if (rbHombroInternal_2.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "2";
                            }
                            else if (rbHombroInternal_3.Checked)
                            {
                                serviceComponentFieldValues.v_Value1 = "3";
                            }
                        }

                        //if (componentFieldId == Constants.UC_OSTEO_ABD_180_SINO)
                        //{
                        //    if (item.v_Value1.ToString() == "1")
                        //    {
                        //        rbAbduccion180DolorSI.Checked = true;
                        //    }
                        //    else if (item.v_Value1.ToString() == "2")
                        //    {
                        //        rbAbduccion180DolorNO.Checked = true;
                        //    }
                        //}

                        //if (componentFieldId == Constants.UC_OSTEO_ABD_60_SINO)
                        //{
                        //    if (item.v_Value1.ToString() == "1")
                        //    {
                        //        rbAbduccion60DolorSI.Checked = true;
                        //    }
                        //    else if (item.v_Value1.ToString() == "2")
                        //    {
                        //        rbAbduccion60DolorNO.Checked = true;
                        //    }
                        //}

                        //if (componentFieldId == Constants.UC_OSTEO_ABD_90_SINO)
                        //{
                        //    if (item.v_Value1.ToString() == "1")
                        //    {
                        //        rbRotacion090DolorSI.Checked = true;
                        //    }
                        //    else if (item.v_Value1.ToString() == "2")
                        //    {
                        //        rbRotacion090DolorNO.Checked = true;
                        //    }
                        //}

                        //if (componentFieldId == Constants.UC_OSTEO_ROTACION_SINO)
                        //{
                        //    if (item.v_Value1.ToString() == "1")
                        //    {
                        //        rbRotacionExtIntDolorSI.Checked = true;
                        //    }
                        //    else if (item.v_Value1.ToString() == "2")
                        //    {
                        //        rbRotacionExtIntDolorNO.Checked = true;
                        //    }
                        //}


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

                if (ctrl is NumberBox)
                {
                    if (((NumberBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((NumberBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((NumberBox)ctrl).Text = ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId) == null ? "" : ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId).v_DefaultText;

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

        #region Antecedente Ocupacionales
        protected void grdAntecedentesOcupacionales_RowCommand(object sender, GridCommandEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "DeleteRegistro")
            {
                string HistoryId = grdAntecedentesOcupacionales.DataKeys[grdAntecedentesOcupacionales.SelectedRowIndex][0].ToString();
                _HistoryBL.DeleteHistory(ref objOperationResult, HistoryId, ((ClientSession)Session["objClientSession"]).GetAsList());

                grdAntecedentesOcupacionales.DataSource = _HistoryBL.ListarGrillaHistoriaOcupacional(Session["PersonId"].ToString());
                grdAntecedentesOcupacionales.DataBind();
            }
        }
        #endregion

        #region Acordion
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
            
        #region Altura Estructural Internacional

        private void LoadCombosAltura18Internacional()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo163 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 163);

            Utils.LoadDropDownList(ddlAltura1_8Nistagmus_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8PrimerosAux_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8Timpanos_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8Equilibrio_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8Sustentacion_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8Caminar_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8Adiadococinesia_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8IndiceNariz_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8RecibioCurso_Internacional, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltura1_8Aptitud_Internacional, "Value1", "Id", Combo163, DropDownListAction.Select);
             SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabaAltura, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }

        private void ObtenerDatosAltura18Internacional(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var ComponentesMedicina = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponenteseMedicina"];
          
            //Llenar Datos 
            txtAltura18TALLA_Internacional.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID).ServiceComponentFieldValues[0].v_Value1;
            txtAltura18PESO_Internacional.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID).ServiceComponentFieldValues[0].v_Value1;
            txtAltura18PS_Internacional.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID).ServiceComponentFieldValues[0].v_Value1;
            txtAltura18PD_Internacional.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAD_ID).ServiceComponentFieldValues[0].v_Value1;
            txtAltura18FR_Internacional.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).ServiceComponentFieldValues[0].v_Value1;
            txtAltura18FC_Internacional.Text = ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID) == null ? "" : ComponentesMedicina.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).ServiceComponentFieldValues[0].v_Value1;
       
            //Obtener Resultados para mostar
            txtElectrocardioAltura_CI.Text = _serviceBL.GetDiagnosticByServiceIdAndComponent(pServiceId, Constants.ELECTROCARDIOGRAMA_ID) == "" ? "NO SE HAN REGISTRADO DATOS" : _serviceBL.GetDiagnosticByServiceIdAndComponent(pServiceId, Constants.ELECTROCARDIOGRAMA_ID);

            var xColesterol = _serviceBL.ValoresComponente(pServiceId, Constants.COLESTEROL_ID);
            var xTriglicerido = _serviceBL.ValoresComponente(pServiceId, Constants.TRIGLICERIDOS_ID);
            txtColesterolAltura_CI.Text = xColesterol.Count == 0 || xColesterol.Find(p => p.v_ComponentFieldId == Constants.COLESTEROL_COLESTEROL_TOTAL_ID) == null ? string.Empty : xColesterol.Find(p => p.v_ComponentFieldId == Constants.COLESTEROL_COLESTEROL_TOTAL_ID).v_Value1;
            txtTrigliceridoAltura_CI.Text = xTriglicerido.Count == 0 || xTriglicerido.Find(p => p.v_ComponentFieldId == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS) == null ? string.Empty : xTriglicerido.Find(p => p.v_ComponentFieldId == Constants.TRIGLICERIDOS_BIOQUIMICA_TRIGLICERIDOS).v_Value1;
            if (ComponentesMedicina.Find(p => p.v_ComponentFieldsId == "N005-MF000002063") != null)
            {              
                SearchControlAndLoadData(TabAltura18_Internacional, Session["ServicioComponentIdMedicina"].ToString(), ComponentesMedicina);

                #region Campos de auditoria
                //Obtener scId
                var scId = _serviceBL.ObtenerScId(pServiceId, "N005-ME000000117");
                var datosAuditoria = HistoryBL.CamposAuditoria(scId);
                if (datosAuditoria != null)
                {
                    txtAlturaCIAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtAlturaCIAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtAlturaCIActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtAlturaCIEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtAlturaCIEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtAlturaCIEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtAlturaCIInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtAlturaCIInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtAlturaCIInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabAltura18_Internacional, _tmpServiceComponentsForBuildMenuList);

                txtAlturaCIAuditor.Text = "";
                txtAlturaCIAuditorInsercion.Text = "";
                txtAlturaCIActualizacion.Text = "";

                txtAlturaCIEvaluador.Text = "";
                txtAlturaCIEvaluadorInserta.Text = "";
                txtAlturaCIEvaluadorActualizacion.Text = "";

                txtAlturaCIInformador.Text = txtAlturaCIEvaluador.Text = "";
                txtAlturaCIInformadorInserta.Text = txtAlturaCIEvaluador.Text = "";
                txtAlturaCIInformadorActualizacion.Text = txtAlturaCIEvaluador.Text = "";

            }
        }

        protected void btnAltura18_Internacional_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabaAltura.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabAltura18_Internacional, Session["ServicioComponentIdMedicina"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                       Session["ServicioComponentIdMedicina"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N005-MF000002077") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(ddlAltura1_8Aptitud_Internacional.SelectedValue.ToString(), "N005-MF000002077", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdMedicina"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdMedicina"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N005-ME000000117";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            //obtener el usuario antiguo
            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabaAltura.SelectedValue.ToString());


             _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   l,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);

             //Obtener scId
             var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N005-ME000000117");

            var datosAuditoria = HistoryBL.CamposAuditoria(scId.ToString());
            if (datosAuditoria != null)
            {
                txtAlturaCIAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtAlturaCIAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtAlturaCIActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtAlturaCIEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtAlturaCIEvaluadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtAlturaCIEvaluadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtAlturaCIInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtAlturaCIInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtAlturaCIInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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

        #region Reporte 312     

        private void GenerateAnexo312(string pathFile, string ServicioId, string PacienteId)
        {
            PacientBL _pacientBL = new PacientBL();
            HistoryBL _historyBL = new HistoryBL();
            Session["ValidacionReporte312"] = true;
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(PacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var _DataService = _serviceBL.GetServiceReport(ServicioId);
            if (_DataService == null)
            {
                Session["ValidacionReporte312"] = false;
                 return;
            }
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(PacienteId);

            var Antropometria = _serviceBL.ValoresComponente(ServicioId, Constants.ANTROPOMETRIA_ID);
            var FuncionesVitales = _serviceBL.ValoresComponente(ServicioId, Constants.FUNCIONES_VITALES_ID);
            var ExamenFisico = _serviceBL.ValoresComponente(ServicioId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(ServicioId, Constants.OFTALMOLOGIA_ID);
            var Psicologia = _serviceBL.ValoresExamenComponete(ServicioId, Constants.PSICOLOGIA_ID, 195);
            var OIT = _serviceBL.ValoresExamenComponete(ServicioId, Constants.OIT_ID, 211);
            var RX = _serviceBL.ValoresExamenComponete(ServicioId, Constants.RX_TORAX_ID, 135);
            var Laboratorio = _serviceBL.ValoresComponente(ServicioId, Constants.INFORME_LABORATORIO_ID);
            //var Audiometria = _serviceBL.ValoresComponente(ServicioId, Constants.AUDIOMETRIA_ID);
            var Audiometria = _serviceBL.GetDiagnosticForAudiometria(ServicioId, Constants.AUDIOMETRIA_ID);
            var Espirometria = _serviceBL.ValoresExamenComponete(ServicioId, Constants.ESPIROMETRIA_ID, 210);
            var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(ServicioId);
            var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(ServicioId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(ServicioId);
            var ValoresDxLab = _serviceBL.ValoresComponenteAMC_(ServicioId, 1);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var TestIhihara = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ISHIHARA_ID);
            var TestEstereopsis = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ESTEREOPSIS_ID);


            FichaMedicaOcupacional312_CI.CreateFichaMedicalOcupacional312Report(_DataService,
                        filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                        _listMedicoPersonales, _listaHabitoNocivos, Antropometria, FuncionesVitales,
                        ExamenFisico, Oftalmologia, Psicologia, OIT, RX, Laboratorio, Audiometria, Espirometria,
                        _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter, TestIhihara, TestEstereopsis,
                        pathFile);

        }

        protected void lnkAnexo312_Click(object sender, EventArgs e)
        {            
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];            
            string path;
            path = _ruta+ Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_312;

            GenerateAnexo312(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());


            if (!bool.Parse(Session["ValidacionReporte312"].ToString()))
            {
                Alert.Show("Grabe el examen Anexo 312 para poder generar el reporte.");
            }
            else
            {
                Download(Session["ServiceId"].ToString() + "-312.pdf", path+".pdf");
            }

        }

        #endregion

        #region Reporte Osteo CI
        protected void lnkOsteomuscular_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000046";

            GenerateOsteomuscular(_ruta, Session["ServiceId"].ToString());

            var x = ((List<string>)Session["filesNameToMerge"]).ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = _ruta + Session["ServiceId"].ToString() + "-N005-ME000000046_MERGE.pdf";
            _mergeExPDF.Execute();

            Download(Session["ServiceId"].ToString() + "-N005-ME000000046_MERGE.pdf", path + "_MERGE.pdf");

        }

        private void GenerateOsteomuscular(string ruta, string serviceId)
        {
            var OSTEOMUCULAR_CI_ID = new ServiceBL().GetMusculoEsqueletico_ClinicaInternacional(serviceId, "N005-ME000000046");
            dsGetRepo = new DataSet();
            DataTable dt_OSTEOMUSCULAR_CI_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OSTEOMUCULAR_CI_ID);
            dt_OSTEOMUSCULAR_CI_ID.TableName = "DataTable1";
            dsGetRepo.Tables.Add(dt_OSTEOMUSCULAR_CI_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico2();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + "-2.pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico3();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + "-3.pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }
        #endregion

        #region "Reporte Altura CI"

        protected void lnkAltura18CI_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000117";

            GenerateAltura18_CI(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N005-ME000000117.pdf", path + ".pdf");
        }

        private void GenerateAltura18_CI(string _ruta, string p)
        {
            var ALTURA_18_ID = new ServiceBL().GetAlturaEstructural_CI(p, "N005-ME000000117");

            dsGetRepo = new DataSet();

            DataTable dtALTURA_18_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ALTURA_18_ID);
            dtALTURA_18_ID.TableName = "dtAltura18_CI";
            dsGetRepo.Tables.Add(dtALTURA_18_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAltura1_8_Inter();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + "N005-ME000000117" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            //Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }
        #endregion  

        #region Reporte 7D
        protected void lnk7D_CI_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000045";

            Generate7D(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N002-ME000000045.pdf", path + ".pdf");
        }

        private void Generate7D(string _ruta, string p)
        {
            var AscensoAlturas = new ServiceBL().ReportAscensoGrandesAlturas(p, Constants.ALTURA_7D_ID);
            var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(p, Constants.FUNCIONES_VITALES_ID);
            var Antropometria = new ServiceBL().ReportAntropometria(p, Constants.ANTROPOMETRIA_ID);

            dsGetRepo = new DataSet("Anexo7D");

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AscensoAlturas);
            dt.TableName = "dtAnexo7D";
            dsGetRepo.Tables.Add(dt);

            DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales);
            dt1.TableName = "dtFuncionesVitales";
            dsGetRepo.Tables.Add(dt1);

            DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria);
            dt2.TableName = "dtAntropometria";
            dsGetRepo.Tables.Add(dt2);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAnexo7D();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.ALTURA_7D_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }
        #endregion
       
        #region Reporte Tamizaje
        protected void lnkTamizajeCI_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000116";

            GenerateTamizaje(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N005-ME000000116.pdf", path + ".pdf");
        }

        private void GenerateTamizaje(string _ruta, string p)
        {
            var TAMIZAJE_DERMATOLOGIO_ID = new ServiceBL().ReportTamizajeDermatologico_CI(p, Constants.TAMIZAJE_DERMATOLOGIO_ID_CI);

            dsGetRepo = new DataSet();
            DataTable dt_TAMIZAJE_DERMATOLOGIO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TAMIZAJE_DERMATOLOGIO_ID);
            dt_TAMIZAJE_DERMATOLOGIO_ID.TableName = "dtEvaDermatologicoCI";
            dsGetRepo.Tables.Add(dt_TAMIZAJE_DERMATOLOGIO_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEvaluacionDermatologicaCI();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.TAMIZAJE_DERMATOLOGIO_ID_CI + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
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

        #region Fototipo
        //protected void btngrabarFototipo_Click(object sender, EventArgs e)
        //{
            

        //    if (ddlGrabarUsuarioFototipo.SelectedValue == "-1")
        //    {
        //        Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
        //        return;
        //    }

        //    OperationResult objOperationResult = new OperationResult();
        //    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //    var img = txtLinkImage.Text;
        //    byte[] array =  encoding.GetBytes(img);

        //    fileInfo = new FileInfoDto();

        //    fileInfo.PersonId = Session["PersonId"].ToString();
        //    var serviceComponentId = _serviceBL.GetServiceComponentId(Session["ServiceId"].ToString(), "N009-ME000000411");
        //    fileInfo.ServiceComponentId = serviceComponentId[0].v_ServiceComponentId;
        //    fileInfo.FileName = "IMAGEN FOTOTIPO";
        //    fileInfo.Description = "IMAGEN PROVENIENTE DE MEDICINA FOTOTIPO";
        //    fileInfo.ByteArrayFile = array;

        //    if (txtMultimediaId.Text != "")
        //    {
        //        fileInfo.MultimediaFileId = txtMultimediaId.Text;
        //        _multimediaFileBL.UpdateMultimediaFileComponent(ref objOperationResult, fileInfo, ((ClientSession)Session["objClientSession"]).GetAsList());
        //    }
        //    else
        //    {
        //        _multimediaFileBL.AddMultimediaFileComponent(ref objOperationResult, fileInfo, ((ClientSession)Session["objClientSession"]).GetAsList());
        //    }
            
            
            

        //}
        #endregion

        protected void ddlPiel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPiel.SelectedValue.ToString() == "2")
            {
                txtHallazgoPiel.Text = "";
            }
        }

        protected void ddlCabello_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCabello.SelectedValue.ToString() == "2")
            {
                txtHallazgoCabello.Text = "";
            }
        }

        protected void ddlOjosAnexos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOjosAnexos.SelectedValue.ToString() == "2")
            {
                txtOjosAnexos.Text = "";
            }
        }

        protected void ddlOidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOidos.SelectedValue.ToString() == "2")
            {
                txtOidos.Text = "";
            }
        }

        protected void ddlNariz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNariz.SelectedValue.ToString() == "2")
            {
                txtNariz.Text = "";
            }
        }

        protected void ddlBoca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBoca.SelectedValue.ToString() == "2")
            {
                txtBoca.Text = "";
            }
        }

        protected void ddlFaringe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFaringe.SelectedValue.ToString() == "2")
            {
                txtFaringe.Text = "";
            }
        }

        protected void ddlCuello_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCuello.SelectedValue.ToString() == "2")
            {
                txtCuello.Text = "";
            }
        }

        protected void ddlApaRespiratorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApaRespiratorio.SelectedValue.ToString() == "2")
            {
                txtApaRespira.Text = "";
            }
        }

        protected void ddlApaCardiovascular_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApaCardiovascular.SelectedValue.ToString() == "2")
            {
                txtApaCardiovas.Text = "";
            }
        }

        protected void ddlApaDigestivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApaDigestivo.SelectedValue.ToString() == "2")
            {
                txtApaDigestivo.Text = "";
            }
        }

        protected void ddlApaGenito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApaGenito.SelectedValue.ToString() == "2")
            {
                txtApaGenito.Text = "";
            }
        }

        protected void ddlApaLocomotor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApaLocomotor.SelectedValue.ToString() == "2")
            {
                txtApaLocomotor.Text = "";
            }
        }

        protected void ddlMarcha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMarcha.SelectedValue.ToString() == "2")
            {
                txtMarcha.Text = "";
            }
        }

        protected void ddlColumna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlColumna.SelectedValue.ToString() == "2")
            {
                txtColumna.Text = "";
            }
        }

        protected void ddlExtreSuper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExtreSuper.SelectedValue.ToString() == "2")
            {
                txtExtreSuperio.Text = "";
            }
        }

        protected void ddlExtreInfe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExtreInfe.SelectedValue.ToString() == "2")
            {
                txtExtreInfer.Text = "";
            }
        }

        protected void ddlLinfaticos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLinfaticos.SelectedValue.ToString() == "2")
            {
                txtLinfaticos.Text = "";
            }
        }

        protected void ddlSistNerviso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSistNerviso.SelectedValue.ToString() == "2")
            {
                txtSistemaNervisos.Text = "";
            }
        }

        protected void ddlEctoscopia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEctoscopia.SelectedValue.ToString() == "2")
            {
                txtExtoscopia.Text = "";
            }
        }

        protected void ddlEstadoMental_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstadoMental.SelectedValue.ToString() == "2")
            {
                txtEstadoMental.Text = "";
            }
        }
        
        protected void btngrabarfoto_Click(object sender, EventArgs e)
        {
            
            
            byte[] urlfoto = Encoding.ASCII.GetBytes(texturl.Text);

            string[] FOTO = SavePrepared(txtMultimediaFileId_Inter.Text, txtServiceComponentMultimediaId_Inter.Text, Session["PersonId"].ToString(), Session["ServicioComponentIdMedicina"].ToString(), "IMAGEN MEDICINA", "IMAGEN PROVENIENTE DEL UC MEDICINA", urlfoto);

            if (FOTO != null)    // GRABAR
            {
                txtMultimediaFileId_Inter.Text = FOTO[0];
                txtServiceComponentMultimediaId_Inter.Text = FOTO[1];

                var medicina = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>()
                {                                
                    new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList { v_ComponentFieldId = txtMultimediaFileId_Inter.ID, v_Value1 = txtMultimediaFileId_Inter.Text },                
                    new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList { v_ComponentFieldId = txtServiceComponentMultimediaId_Inter.ID, v_Value1 = txtServiceComponentMultimediaId_Inter.Text }
                };

                _listMedicina.AddRange(medicina);
            }
        }

        private string[] SavePrepared(string multimediaFileId, string serviceComponentMultimediaId, string personId, string serviceComponentId, string fileName, string description, byte[] chartImagen)
        {
            string[] FOTO = null;

            fileInfo = new FileInfoDto();

            fileInfo.PersonId = personId;
            fileInfo.ServiceComponentId = serviceComponentId;
            fileInfo.FileName = fileName;
            fileInfo.Description = description;
            fileInfo.ByteArrayFile = chartImagen;

            OperationResult operationResult = null;

            if (string.IsNullOrEmpty(multimediaFileId))     // GRABAR
            {
                // Grabar
                operationResult = new OperationResult();
                FOTO = _multimediaFileBL.AddMultimediaFileComponent(ref operationResult, fileInfo, ((ClientSession)Session["objClientSession"]).GetAsList());               
                txtMultimediaFileId_Inter.Text=FOTO[0];
                txtServiceComponentMultimediaId_Inter.Text = FOTO[1];
                // Analizar el resultado de la operación
                if (operationResult.Success != 1)
                {
                    //MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else {
                    Alert.ShowInTop("Se grabó correctamente", MessageBoxIcon.Information);
                }
            }
            else        // ACTUALIZAR
            {
                operationResult = new OperationResult();
                fileInfo.MultimediaFileId = multimediaFileId;
                fileInfo.ServiceComponentMultimediaId = serviceComponentMultimediaId;
                _multimediaFileBL.UpdateMultimediaFileComponent(ref operationResult, fileInfo, ((ClientSession)Session["objClientSession"]).GetAsList());
                // Analizar el resultado de la operación
                if (operationResult.Success != 1)
                {
                    //MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                else {
                    Alert.ShowInTop("Se grabó correctamente", MessageBoxIcon.Information);
                }
            }
            return FOTO;

        }
    }
}