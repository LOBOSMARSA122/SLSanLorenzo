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
    public partial class frmLaboratorio : System.Web.UI.Page
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
                btnReporteLab.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Lab");
                btnCertificadoAptitud.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Certificado");
                btnNewDiagnosticosFrecuente.OnClientClick = WindowAddDXFrecuente.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDXFrecuente.GetShowReference("../Auditar/FRM033G.aspx?Mode=New");
                int RoleId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString());
                var ComponentesPermisoLectura = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId(9, RoleId).FindAll(p => p.i_Read == 1);
                List<string> ListaComponentesPermisoLectura = new List<string>();
                foreach (var item in ComponentesPermisoLectura)
                {
                    ListaComponentesPermisoLectura.Add(item.v_ComponentId);
                }
                Session["ComponentesPermisoLectura"] = ListaComponentesPermisoLectura;
                TabLaboratorio.Hidden = true;
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1);  //  DateTime.Parse("12/11/2016");
                dpFechaFin.SelectedDate = DateTime.Now; //  DateTime.Parse("12/11/2016"); 
                LoadCombos();
                TabLaboratorio.Attributes.Add("Tag", "N001-ME000000000");

                PanelExamencompletodeorina.Hidden = true;
                PanelExamencompletodeorina.Attributes.Add("Tag", "N009-ME000000046");
                ddlCOLOR_EX_ORINA.Attributes.Add("Tag", "N009-MF000000444");

                ddlASPECTO_EX_ORINA.Attributes.Add("Tag", "N009-MF000001041");

                txtDENSIDAD_EX_ORINA.Attributes.Add("Tag", "N009-MF000001043");
                txtDENSIDAD_EX_ORINA_DESEABLE.Attributes.Add("Tag", "N009-MF000003224");
                txtPH_EX_ORINA_DESEABLE.Attributes.Add("Tag", "N009-MF000003225");
                txtPH_EX_ORINA.Attributes.Add("Tag", "N009-MF000001045");

                txtCELULAS_EPITELIALES_EX_ORINA.Attributes.Add("Tag", "N009-MF000001059");



                txtLEUCOCITOS_EX_ORINA.Attributes.Add("Tag", "N009-MF000001061");
                txtHEMATIES_EX_ORINA.Attributes.Add("Tag", "N009-MF000001063");
                ddlCRISTALES_DEOX_CALCIO.Attributes.Add("Tag", "N009-MF000001065");
                ddlCRISTALES_DEUR_AMORFOS.Attributes.Add("Tag", "N009-MF000003242");
                ddlCRISTALES_FOSF_AMORFOS.Attributes.Add("Tag", "N009-MF000003243");
                ddlCRISTALES_FOSF_TRIPLES.Attributes.Add("Tag", "N009-MF000003244");
                ddlGERMENES_EX_ORINA.Attributes.Add("Tag", "N009-MF000001067");

                txtCILINDROS_HILAINOS.Attributes.Add("Tag", "N009-MF000001069");
                txtCILINDROS_GRANULOSOS.Attributes.Add("Tag", "N009-MF000003245");
                txtCILINDROS_LEUCOCITARIOS.Attributes.Add("Tag", "N009-MF000003246");
                txtCILINDROS_HEMATICOS.Attributes.Add("Tag", "N009-MF000003247");

                txtPIOCITOS.Attributes.Add("Tag", "N009-MF000001047");

                ddlFILAMENTO_MUCOIDE_EX_ORINA.Attributes.Add("Tag", "N009-MF000001071");
                ddlLEVADURAS.Attributes.Add("Tag", "N009-MF000003437");
                ddlSANGRE_EX_ORINA.Attributes.Add("Tag", "N009-MF000001315");
                ddlUROBILINOGENO_EX_ORINA.Attributes.Add("Tag", "N009-MF000001049");
                txtBILIRRUBINA_EX_ORINA.Attributes.Add("Tag", "N009-MF000003439");
                txtLEUCOCITOS_EX_ORINA_QUIMICO.Attributes.Add("Tag", "N009-MF000003438");
                ddlPROTEINAS_EX_ORINA.Attributes.Add("Tag", "N009-MF000001053");
                ddlNITRITOS_EX_ORINA.Attributes.Add("Tag", "N009-MF000001055");
                ddlC_CETONICOS_EX_ORINA.Attributes.Add("Tag", "N009-MF000001057");
                ddlGLUCOSA_EX_ORINA.Attributes.Add("Tag", "N009-MF000001313");
                ddlPIGMENTOS_EX_ORINA.Attributes.Add("Tag", "N009-MF000001051");
                ddlACIDO_ASCORBIC_EX_ORINA.Attributes.Add("Tag", "N009-MF000001071");

                rbPatologico.Attributes.Add("Tag", "N009-MF000003210");
                rbNoPatologico.Attributes.Add("Tag", "N009-MF000003211");


                PanelCadmio.Hidden= true;
                PanelCadmio.Attributes.Add("Tag", "N009-ME000000429");    
                txtCadmio.Attributes.Add("Tag", "N009-MF000003228");                 
                txtCadmioDeseable.Attributes.Add("Tag","N009-MF000003229");

                PanelCadmiOOrina.Hidden = true;
                PanelCadmiOOrina.Attributes.Add("Tag", "N009-ME000000409");
                txtCADMIO_ORINA.Attributes.Add("Tag", "N009-MF000003106");
                txtCADMIO_ORINA_DESEABLE.Attributes.Add("Tag", "N009-MF000003107");

                PanelReaccionesSerologicas.Hidden = true;
                PanelReaccionesSerologicas.Attributes.Add("Tag", "N009-ME000000003");
                ddlREACCIONES_SEROLOGICAS.Attributes.Add("Tag", "N009-MF000000269");

                PanelFactorRematoideo.Hidden = true;
                PanelFactorRematoideo.Attributes.Add("Tag", "N009-ME000000432");
                ddlFACTOR_REMATOIDEO.Attributes.Add("Tag", "N009-MF000003240");

                PanelCultivoHeces.Hidden = true;
                PanelCultivoHeces.Attributes.Add("Tag", "N009-ME000000427");
                ddlSALMONELA_TYPHI.Attributes.Add("Tag", "N009-MF000003226");

                PanelExamenSereadoHeces.Hidden = true;
                PanelExamenSereadoHeces.Attributes.Add("Tag", "N009-ME000000428");
                txtEXAM_SEREADO_HECES.Attributes.Add("Tag", "N009-MF000003227");

                PanelDatoCieloAzul.Hidden = true;
                PanelDatoCieloAzul.Attributes.Add("Tag", "N009-ME000000454");
                txtIDENTIFICACION.Attributes.Add("Tag", "N009-MF000003253");
                dpFECH_EMISION.Attributes.Add("Tag", "N009-MF000003254");
                txtMEDICO.Attributes.Add("Tag", "N009-MF000003255");

                PanelCoprocultCieloAzul.Hidden = true;
                PanelCoprocultCieloAzul.Attributes.Add("Tag", "N009-ME000000453");
                txtDESCRIPCION.Attributes.Add("Tag", "N009-MF000003665");

                PanelDosajeAlcohol.Hidden = true;
                PanelDosajeAlcohol.Attributes.Add("Tag", "N009-ME000000041");
                ddlDOSAJE_ALCOHOL.Attributes.Add("Tag", "N009-MF000000397");

                PanelHemograma.Hidden = true;
                PanelHemograma.Attributes.Add("Tag", "N009-ME000000113");                 
                //txtHEMOGLOBINA_CONST_CORP.Attributes.Add("Tag","N009-MF000001874");
                //txtHEMOGLOBINA_DESEABLE_CONST_CORP.Attributes.Add("Tag","N009-MF000001875");
                //txtHEMATOCRITO_CONST_CORP.Attributes.Add("Tag", "N009-MF000001876");
                //txtHEMATOCRITO_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001877");
                //txtHEMATIES_CONST_CORP.Attributes.Add("Tag", "N009-MF000001878");
                //txtHEMATIES_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001879");
                //txtVOLUMEN_CORPUSCULAR_MEDIO_CONST_CORP.Attributes.Add("Tag", "N009-MF000001880");
                //txtVOL_CORP_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001881");
                //txtHB_CORPUSCULAR_MEDIO_CONST_CORP.Attributes.Add("Tag", "N009-MF000001882");
                //txtHB_CORP_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001883");
                //txtCC_HB_CORPUSCULAR_CONST_CORP.Attributes.Add("Tag", "N009-MF000001884");
                //txtCC_HB_CORP_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001885");
                //txtPLAQUETAS_CONST_CORP.Attributes.Add("Tag", "N009-MF000001886");
                //txtPLAQUETAS_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001887");
                //txtVOL_PLAQUETARIO_MEDIO_CONST_CORP.Attributes.Add("Tag", "N009-MF000001888");
                //txtVOL_PLAQUETARIO_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001889");
                //txtLEUCOCITOS_TOTALES_CONST_CORP.Attributes.Add("Tag", "N009-MF000001890");
                //txtLEUCOCITOS_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001891");
                //txtLINFOCITOS_CONST_CORP.Attributes.Add("Tag", "N009-MF000001892");
                //txtLINFOCITOS_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001893");
                //txtMID_BAS_EOS_MON_CONST_CORP.Attributes.Add("Tag", "N009-MF000001894");
                //txtMID_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001895");
                //txtNEUTROFILOS_SEMENTADOS_CONST_CORP.Attributes.Add("Tag", "N009-MF000001896");
                //txtNEUTROFILOS_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001897");
                //txtLINFOCITOS_10_9_CONST_CORP.Attributes.Add("Tag", "N009-MF000001898");
                //txtLINFOCITOS_10_9_DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001899");
                //txtMID_BAS_EOS_MON_10_9_CONST_CORP.Attributes.Add("Tag", "N009-MF000001900");
                //txtMID_B_E_M_10_9DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001901");
                //txtNEUTROFILOS_10_9_CONST_CORP.Attributes.Add("Tag", "N009-MF000001902");
                //txtNEUTROFILOS_10_9DESEABLE_CONST_CORP.Attributes.Add("Tag", "N009-MF000001903");

                txtHEMOGLOBINA_HEM_COM.Attributes.Add("Tag", "N009-MF000001874");
                txtHEMOGLOBINA_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001875");
                txtHEMATOCRITO_HEM_COM.Attributes.Add("Tag", "N009-MF000001876");
                txtHEMATOCRITO_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001877");
                txtHEMATIES_HEM_COM.Attributes.Add("Tag", "N009-MF000001878");
                txtHEMATIES_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001879");
                txtLEUCOCITOS_HEM_COM.Attributes.Add("Tag", "N009-MF000001890");
                txtLEUCOCITOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001891");
                txtRECUENTO_DE_PLAQUETAS_HEM_COM.Attributes.Add("Tag", "N009-MF000001886");
                txtPLAQUETAS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001887");
                txtABASTONADO_HEM_COM.Attributes.Add("Tag", "N009-MF000003207");
                txtABASTONADOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000003208");
                txtSEGMENTADOS_HEM_COM.Attributes.Add("Tag", "N009-MF000001898");
                txtSEGMENTADOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001899");
                txtEOSINOFILOS_HEM_COM.Attributes.Add("Tag", "N009-MF000001894");
                txtEOSINOFILOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001895");
                txtBASOFILOS_HEM_COM.Attributes.Add("Tag", "N009-MF000001900");
                txtBASOFILOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001901");
                txtMONOCITOS_HEM_COM.Attributes.Add("Tag", "N009-MF000001902");
                txtMONOCITOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001903");
                txtLINFOCITOS_HEM_COM.Attributes.Add("Tag", "N009-MF000001892");
                txtLINFOCITOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001893");
                txtNEUTROFILOS.Attributes.Add("Tag", "N009-MF000001903");
                txtNEUTROFILOS_DESEABLE.Attributes.Add("Tag", "N009-MF000001892");
                txtMIELOCITOS.Attributes.Add("Tag", "N009-MF000001893");
                txtMIELOCITOS_DESEABLE.Attributes.Add("Tag", "N009-MF000001903");
                txtJUVENILES.Attributes.Add("Tag", "N009-MF000001892");
                txtJUVENILES_DESEABLE.Attributes.Add("Tag", "N009-MF000001893");
                txtVOL_CORPUSCULAR.Attributes.Add("Tag", "N009-MF000001880");
                txtVOL_CORPUSCULAR_DESEABLE.Attributes.Add("Tag", "N009-MF000001881");
                txtHB_CORPPUSCULAR_MEDIO.Attributes.Add("Tag", "N009-MF000001882");
                txtHB_CORPPUSCULAR_MEDIO_DESEABLE.Attributes.Add("Tag", "N009-MF000001883");
                txtCC_CORPUSCULAR.Attributes.Add("Tag", "N009-MF000001884");
                txtCC_CORPUSCULAR_DESEABLE.Attributes.Add("Tag", "N009-MF000001885");

                ParasitoSeriado.Hidden = true;
                ParasitoSeriado.Attributes.Add("Tag", "N009-ME000000049");
                COLOR_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000000515");
                CONSISTENCIA_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001329");
                RESTOS_ALIMENTICIOS_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001330");
                SANGRE_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001331");
                MOCO_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001340");
                HUEVOS_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001342");
                QUISTES_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001343");
                TROFOZOITOS_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001344");
                HEMATIES_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001345");
                LEUCOCITOS_Heces_Primera_Muestra.Attributes.Add("Tag", "N009-MF000001346");
                COLOR_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001347");
                CONSISTENCIA_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001348");
                RESTOS_ALIMENTICIOS_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001349");
                SANGRE_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001350");
                MOCO_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001351");
                QUISTES_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001353");
                HUEVOS_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001354");
                TROFOZOITOS_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001355");
                HEMATIES_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001356");
                LEUCOCITOS_Heces_Segunda_Muestra.Attributes.Add("Tag", "N009-MF000001357");
                COLOR_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001358");
                CONSISTENCIA_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001359");
                RESTOS_ALIMENTICIOS_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001360");
                SANGRE_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001361");
                MOCO_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001362");
                QUISTES_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001364");
                HUEVOS_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001365");
                TROFOZOITOS_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001366");
                HEMATIES_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001367");
                LEUCOCITOS_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001368");
                RESULTADOS_Heces_Tercera_Muestra.Attributes.Add("Tag", "N009-MF000001370");

                ParasitoSimple.Hidden = true;
                ParasitoSimple.Attributes.Add("Tag", "N009-ME000000010");
                txtCOLOR_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000000260");
                ddlCONSISTENCIA_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001325");
                ddlRESTOS_ALIMENTICIOS_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001326");
                ddlSANGRE_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001327");
                ddlMOCO_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001328");
                ddlQUISTES_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001333");
                ddlHUEVOS_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001334");
                ddlTROFOZOITOS_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001335");
                ddlHEMATIES_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001336");
                ddlLEUCOCITOS_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001337");
                txtRESULTADOS_PAR_SIMPLE.Attributes.Add("Tag", "N009-MF000001339");

                PanelAglutinacionesenlamina.Hidden = true;
                PanelAglutinacionesenlamina.Attributes.Add("Tag", "N009-ME000000025");
                txtTIFICO_O.Attributes.Add("Tag", "N009-MF000001318");
                txtTIFICO_H.Attributes.Add("Tag", "N009-MF000001320");
                txtPARATIFICO_A.Attributes.Add("Tag", "N009-MF000000445");
                txtPARATIFICO_B.Attributes.Add("Tag", "N009-MF000001323");
                txtBRUCELLA.Attributes.Add("Tag", "N009-MF000001431");

                PanelToxiCocaMari.Hidden = true;
                PanelToxiCocaMari.Attributes.Add("Tag", "N009-ME000000053");
                MUESTRA_COC_MAR.Attributes.Add("Tag", "N009-MF000001374");
                METODO_COC_MAR.Attributes.Add("Tag", "N009-MF000001375");
                COCAINA_COC_MAR.Attributes.Add("Tag", "N009-MF000000705");
                MARIHUANA_COC_MAR.Attributes.Add("Tag", "N009-MF000001294");
                LOTE_COCAINA.Attributes.Add("Tag", "N009-MF000002799");
                LOTE_MARIHUANA.Attributes.Add("Tag", "N009-MF000002814");
                
                PerfilHepatico.Hidden = true;
                PerfilHepatico.Attributes.Add("Tag", "N009-ME000000096");
                PROTEINAS_TOTALES_PERF_HEP.Attributes.Add("Tag", "N009-MF000001792");
                PROTEINAS_TOTALES_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001793");
                ALBUMINA_PERF_HEP.Attributes.Add("Tag", "N009-MF000001794");
                ALBUMINA_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001795");
                GLOBULINA_PERF_HEP.Attributes.Add("Tag", "N009-MF000001796");
                GLOBULINA_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001797");
                TGO_PERF_HEP.Attributes.Add("Tag", "N009-MF000001798");
                TGO_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001799");
                TGP_PERF_HEP.Attributes.Add("Tag", "N009-MF000001800");
                TGP_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001801");
                FOSFATASA_ALCALINA_PERF_HEP.Attributes.Add("Tag", "N009-MF000001802");
                FOSFATASA_ALCALINA_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001803");
                GGTP_PERF_HEP.Attributes.Add("Tag", "N009-MF000001804");
                GGTP_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001805");
                BILIRRUBINA_TOTAL_PERF_HEP.Attributes.Add("Tag", "N009-MF000001806");
                BILIRRUBINA_TOTAL_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001807");
                BILIRRUBINA_DIRECTA_PERF_HEP.Attributes.Add("Tag", "N009-MF000001808");
                BILIRRUBINA_DIRECTA_DESEABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001809");
                BILIRRUBINA_INDIRECTA_PERF_HEP.Attributes.Add("Tag", "N009-MF000001810");
                BILIRRUBINA_INDIRECTA_DESABLE_PERF_HEP.Attributes.Add("Tag", "N009-MF000001811");

                PerfilLipidico.Hidden = true;
                PerfilLipidico.Attributes.Add("Tag", "N009-ME000000114");
                COLESTEROL_TOTAL_PERF_LIP.Attributes.Add("Tag", "N009-MF000001904");
                COLESTEROL_TOTAL_DESEABLE_PERF_LIP.Attributes.Add("Tag", "N009-MF000001905");
                COLESTEROL_HDL_PERF_LIP.Attributes.Add("Tag", "N009-MF000000254");
                COLESTEROL_LDL_PERF_LIP.Attributes.Add("Tag", "N009-MF000001073");
                COLESTEROL_HDL_DESEABLE_PERF_LIP.Attributes.Add("Tag", "N009-MF000000414");
                COLESTEROL_LDL_DESEABLE_PERF_LIP.Attributes.Add("Tag", "N009-MF000001074");
                COLESTEROL_VLDL_PERF_LIP.Attributes.Add("Tag", "N009-MF000001075");
                COLESTEROL_VLDL_DESEABLE_PERF_LIP.Attributes.Add("Tag", "N009-MF000001076");
                TRIGLICERIDOS_PERF_LIP.Attributes.Add("Tag", "N009-MF000001906");
                TRIGLICERIDOS_DESEABLE_PERF_LIP.Attributes.Add("Tag", "N009-MF000001907");

                PanelGrupoyfactorsanguineo.Hidden = true;
                PanelGrupoyfactorsanguineo.Attributes.Add("Tag", "N009-ME000000000");
                ddlGRUPO_SANGUINEO.Attributes.Add("Tag", "N009-MF000000262");
                ddlFACTOR_RH.Attributes.Add("Tag", "N009-MF000000263");


                PanelHemoglobinaHematocrito.Hidden = true;
                PanelHemoglobinaHematocrito.Attributes.Add("Tag", "N009-ME000000006");
                txtHEMOGLOBINA.Attributes.Add("Tag", "N009-MF000000265");
                txtHEMOGLOBINA_DESEABLE.Attributes.Add("Tag", "N009-MF000000420");
                txtHEMATOCRITO.Attributes.Add("Tag", "N009-MF000000266");
                txtHEMATOCRITO_DESEABLE.Attributes.Add("Tag", "N009-MF000000421");

                PanelVdrl.Hidden = true;
                PanelVdrl.Attributes.Add("Tag", "N009-ME000000003");
                ddl_VDRL.Attributes.Add("Tag", "N009-MF000000269");


                PanelHavigmhepatitisa.Hidden = true;
                PanelHavigmhepatitisa.Attributes.Add("Tag", "N009-ME000000004");
                txtHAV_IGM_HEPATITIS_A.Attributes.Add("Tag", "N009-MF000000264");
                txtHAV_IGM_DESEABLE.Attributes.Add("Tag", "N009-MF000001289");

                PanelExamendeelisahiv.Hidden = true;
                PanelExamendeelisahiv.Attributes.Add("Tag", "N009-ME000000030");
                txtEXAMEN_DE_ELISA.Attributes.Add("Tag", "N009-MF000000257");
                txtELISA_DESEABLE.Attributes.Add("Tag", "N009-MF000001288");

                PanelAcidourico.Hidden = true;
                PanelAcidourico.Attributes.Add("Tag", "N009-ME000000086");
                txtACIDO_URICO.Attributes.Add("Tag", "N009-MF000001425");
                txtACIDO_URICO_DESEABLE.Attributes.Add("Tag", "N009-MF000001426");

                PanelAntigenoprostatico.Hidden = true;
                PanelAntigenoprostatico.Attributes.Add("Tag", "N009-ME000000009");
                txtANTIGENO_PROSTATICO.Attributes.Add("Tag", "N009-MF000000443");
                txtPAS_DESEABLE.Attributes.Add("Tag", "N009-MF000001287");



                PanelColesteroltotal.Hidden = true;
                PanelColesteroltotal.Attributes.Add("Tag", "N009-ME000000016");
                txtCOLESTEROL_TOTAL.Attributes.Add("Tag", "N009-MF000001086");
                txtCOLESTEROL_TOTAL_DESEABLE.Attributes.Add("Tag", "N009-MF000001087");

                PanelTrigliceridos.Hidden = true;
                PanelTrigliceridos.Attributes.Add("Tag", "N009-ME000000017");
                txtTRIGLICERIDOS.Attributes.Add("Tag", "N009-MF000001296");
                txtTRIGLICERIDOS_DESEABLE.Attributes.Add("Tag", "N009-MF000001297");

                PanelGlucosa.Hidden = true;
                PanelGlucosa.Attributes.Add("Tag", "N009-ME000000008");
                txtGLUCOSA.Attributes.Add("Tag", "N009-MF000000261");
                txtGLUCOSA_DESEABLE.Attributes.Add("Tag", "N009-MF000000418");

                PanelCreatinina.Hidden = true;
                PanelCreatinina.Attributes.Add("Tag", "N009-ME000000028");
                txtCREATININA_EN_SUERO.Attributes.Add("Tag", "N009-MF000000518");
                txtCREATININA_DESEABLE.Attributes.Add("Tag", "N009-MF000000519");

                PanelUrea.Hidden = true;
                PanelUrea.Attributes.Add("Tag", "N009-ME000000073");
                txtUREA.Attributes.Add("Tag", "N009-MF000000253");
                txtUREA_DESEABLE.Attributes.Add("Tag", "N009-MF000000256");

                PanelTgo.Hidden = true;
                PanelTgo.Attributes.Add("Tag", "N009-ME000000054");
                txtTGO.Attributes.Add("Tag", "N009-MF000000706");
                txtTGO_DESEABLE.Attributes.Add("Tag", "N009-MF000001292");
                
                PanelTgp.Hidden = true;
                PanelTgp.Attributes.Add("Tag", "N009-ME000000055");
                txtTGP.Attributes.Add("Tag", "N009-MF000000707");
                txtTGP_DESEABLE.Attributes.Add("Tag", "N009-MF000001293");

                PanelSubunidadbetacualitativo.Hidden = true;
                PanelSubunidadbetacualitativo.Attributes.Add("Tag", "N009-ME000000027");
                txtSUB_UNIDAD_BETA.Attributes.Add("Tag", "N009-MF000000270");
                txtVALOR_DESEABLE.Attributes.Add("Tag", "N009-MF000001436");

                PanelBkDirecto.Hidden = true;
                PanelBkDirecto.Attributes.Add("Tag", "N009-ME000000081");
                MUESTRA_BK_DIRECTO.Attributes.Add("Tag", "N009-MF000001371");
                COLORACION_BK_DIRECTO.Attributes.Add("Tag", "N009-MF000001372");
                RESULTADOS_BK_DIRECTO.Attributes.Add("Tag", "N009-MF000001373");

                PanelHepatitisC.Hidden = true;
                PanelHepatitisC.Attributes.Add("Tag", "N009-ME000000005");
                txtINMUNO_ENZIMA_HEPATITIS_C.Attributes.Add("Tag", "N009-MF000000267");
                txtINMUNO_ENZIMA_DESEABLE.Attributes.Add("Tag", "N009-MF000001290");
                //txtOBSERVACION_HEPATITIS_C.Attributes.Add("Tag", "N009-MF000001435");

                PanelBenzeno.Hidden = true;
                PanelBenzeno.Attributes.Add("Tag", "N009-ME000000087");
                RESULTADOS_BENZENO.Attributes.Add("Tag", "N009-MF000001427");
                BENZENO_DESEABLE.Attributes.Add("Tag", "N009-MF000001428");

                PanelMicroalbuminuria.Hidden = true;
                PanelMicroalbuminuria.Attributes.Add("Tag", "N009-ME000000087");
                MICROALBUMINURIA.Attributes.Add("Tag", "N009-MF000002027");
                DESEABLE_MICROALBUMINURIA.Attributes.Add("Tag", "N009-MF000002028");

                PanelHCV.Hidden = true;
                PanelHCV.Attributes.Add("Tag", "N009-ME000000120");
                RESULTADO_HCV.Attributes.Add("Tag", "N009-MF000002053");
                DESEABLE_HCV.Attributes.Add("Tag", "N009-MF000002054");

                PanelHBsAg.Hidden = true;
                PanelHBsAg.Attributes.Add("Tag", "N009-ME000000121");
                RESULTADO_HBsAg.Attributes.Add("Tag", "N009-MF000002055");
                DESEABLE_HBsAg.Attributes.Add("Tag", "N009-MF000002056");

                PanelKOH.Hidden = true;
                PanelKOH.Attributes.Add("Tag", "N009-ME000000122");
                MUESTRA_KOH.Attributes.Add("Tag", "N009-MF000002057");
                RESULTADO_KOH.Attributes.Add("Tag", "N009-MF000002058");

                PanelInsulinaBasal.Hidden = true;
                PanelInsulinaBasal.Attributes.Add("Tag", "N009-ME000000125");
                INSULINA_BASAL.Attributes.Add("Tag", "N009-MF000002067");
                DESEABLE_INSULINA_BASAL.Attributes.Add("Tag", "N009-MF000002068");

                PanelTiempodecoagulacion.Hidden = true;
                PanelTiempodecoagulacion.Attributes.Add("Tag", "N009-ME000000124");
                txtLabTIempoCoagulacion.Attributes.Add("Tag", "N009-MF000002065");
                txtLabTIempoCoagulacionDeseable.Attributes.Add("Tag", "N009-MF000002066");

                PanelTiempodesangria.Hidden = true;
                PanelTiempodesangria.Attributes.Add("Tag", "N009-ME000000124");
                txtLabTiempoSangria.Attributes.Add("Tag", "N009-MF000002063");
                txtLabTiempoSangriaDeseable.Attributes.Add("Tag", "N009-MF000002064");

                PanelHisopadoNaso.Hidden = true;
                PanelHisopadoNaso.Attributes.Add("Tag", "N009-ME000000118");
                txtNasoFarTipoMuestra.Attributes.Add("Tag", "N009-MF000002029");
                txtNasoFarLevadura.Attributes.Add("Tag", "N009-MF000002031");
                txtNasoFarAntibiograma.Attributes.Add("Tag", "N009-MF000002032");
                txtNasoFarFrotisGram.Attributes.Add("Tag", "N009-MF000002030");


                PanelHisopadoFaringeo.Hidden = true;
                PanelHisopadoFaringeo.Attributes.Add("Tag", "N009-ME000000095");
                txtCOLOR_HISOP_FAR.Attributes.Add("Tag", "N009-MF000001791");
                txtASPECTO_HISOP_FAR.Attributes.Add("Tag", "N009-MF000002048");
                txtLEUCOCITOS_HISOP_FAR.Attributes.Add("Tag", "N009-MF000002049");
                txtHEMATIES_HISOP_FAR.Attributes.Add("Tag", "N009-MF000002050");
                txtCELULAS_EPITELIALES_HISOP_FAR.Attributes.Add("Tag", "N009-MF000002051");
                txtBACTERIAS_GRAM_HISOP_FAR.Attributes.Add("Tag", "N009-MF000002052");

                //PanelHemogramaCompleto.Hidden = true;
                //PanelHemogramaCompleto.Attributes.Add("Tag", "N009-ME000000045");
                //txtHEMOGLOBINA_HEM_COM.Attributes.Add("Tag", "N009-MF000001282");
                //txtHEMOGLOBINA_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001283");
                //txtHEMATIES_HEM_COM.Attributes.Add("Tag", "N009-MF000000422");
                //txtHEMATIES_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000423");
                //txtLEUCOCITOS_HEM_COM.Attributes.Add("Tag", "N009-MF000000424");
                //txtLEUCOCITOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000425");
                //txtRECUENTO_DE_PLAQUETAS_HEM_COM.Attributes.Add("Tag", "N009-MF000001284");
                //txtPLAQUETAS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000001285");
                //txtABASTONADO_HEM_COM.Attributes.Add("Tag", "N009-MF000000426");
                //txtABASTONADOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000427");
                //txtSEGMENTADOS_HEM_COM.Attributes.Add("Tag", "N009-MF000000428");
                //txtSEGMENTADOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000429");
                //txtEOSINOFILOS_HEM_COM.Attributes.Add("Tag", "N009-MF000000430");
                //txtEOSINOFILOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000431");
                //txtBASOFILOS_HEM_COM.Attributes.Add("Tag", "N009-MF000000432");
                //txtBASOFILOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000433");
                //txtMONOCITOS_HEM_COM.Attributes.Add("Tag", "N009-MF000000434");
                //txtMONOCITOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000435");
                //txtLINFOCITOS_HEM_COM.Attributes.Add("Tag", "N009-MF000000436");
                //txtLINFOCITOS_DESEABLE_HEM_COM.Attributes.Add("Tag", "N009-MF000000437");
                //txtCONCLUCIONES_HEM_COM.Attributes.Add("Tag", "N009-MF000000516");

                PanelInformeLaboratorio.Hidden = true;
                PanelInformeLaboratorio.Attributes.Add("Tag", "N001-ME000000000");
                txtOBSERVACIONES_LABORATORIO.Attributes.Add("Tag", "N009-MF000000272");
                chkLABORATORIO_NORMAL.Attributes.Add("Tag", "N009-MF000002132");

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
                    ddlUsuarioGrabar.Enabled = true;
                    ddlUsuarioGrabar.SelectedValue = "-1";
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
            ddlConsultorio.SelectedValue = "1";
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

            DateTime? FechaNacimiento = DateTime.Parse(dataKeys[13].ToString());
            txtEdadCabecera.Text = dataKeys[13] == null ? "" : new ServiceBL().GetAge(FechaNacimiento.Value).ToString();// dataKeys[13].ToString();
            ddlAptitud.SelectedValue = dataKeys[3] == null ? "-1" : dataKeys[3].ToString();
            txtComentarioAptitud.Text = dataKeys[14] == null ? "" : dataKeys[14].ToString();

            var lGridComponentes = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(ddlConsultorio.SelectedValue.ToString()), Session["ServiceId"].ToString());

            string ListaComponentesGrabar = "";
            foreach (var item in lGridComponentes)
            {
                ListaComponentesGrabar += item.v_ComponentId + "|" ;
            }
            Session["ListaComponentesGrabar"] = ListaComponentesGrabar.Substring(0,ListaComponentesGrabar.Length-1);

            grdComponentes.DataSource = lGridComponentes;
            grdComponentes.DataBind();

            //Pintar los examenes correpondientes por servicio
            int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());
            if (ProfesionId == (int)TipoProfesional.Auditor)
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());
                foreach (var item in ListaComponentes)
                {
                    if (item.ComponentId == TabLaboratorio.Attributes.GetValue("Tag").ToString())
                    {
                        
                        LoadCombosLaboratorio();
                        ObtenerDatosLaboratorio(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabLaboratorio.Hidden = false;
                        
                    }
                }
            }
            else
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());

                var ListaComponenentesConPermiso = (List<string>)Session["ComponentesPermisoLectura"];

                foreach (var item in ListaComponenentesConPermiso)
                {
                    if (item == TabLaboratorio.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.ESPIROMETRIA_ID);
                        if (Resultado != null)
                        {
                            LoadCombosLaboratorio();
                            ObtenerDatosLaboratorio(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabLaboratorio.Hidden = false;
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

        private void ObtenerDatosLaboratorio(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var oExamenLaboratorio = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 1);
            Session["ComponentIdESO"] = oExamenLaboratorio[0].ComponentId;
            Session["ServicioComponentIdLab"] = oExamenLaboratorio[0].ServicioComponentId;
            var objExamenElectro = _serviceBL.GetServiceComponentFields(oExamenLaboratorio == null ? "" : oExamenLaboratorio[0].ServicioComponentId, pServiceId);
            Session["ComponentesLab"] = objExamenElectro;
            var Examenes = _serviceBL.DevolverExamenesPorCategoria(Session["ServiceId"].ToString(), 1);
            SearchControlAndShow(TabLaboratorio, Examenes);
            if (objExamenElectro.ToList().Count != 0)
            {
                SearchControlAndLoadData(TabLaboratorio, Session["ServicioComponentIdLab"].ToString(), (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesLab"]);
                #region Campos auditoria
                var datosAuditoria = HistoryBL.CamposAuditoria(oExamenLaboratorio[0].ServicioComponentId);
                if (datosAuditoria != null)
                {
                    txtLabAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtLabAuditorInsertar.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtLabAuditorEditar.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtLabEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtLabEvaluadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtLabEvaluadorEvaluar.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtLabInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtLabInformadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtLabInformadorActualiza.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }
                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabLaboratorio, _tmpServiceComponentsForBuildMenuList);

                txtLabAuditor.Text = "";
                txtLabAuditorInsertar.Text = "";
                txtLabAuditorEditar.Text = "";

                txtLabEvaluador.Text = "";
                txtLabEvaluadorInsertar.Text = "";
                txtLabEvaluadorEvaluar.Text = "";

                txtLabInformador.Text = "";
                txtLabInformadorInsertar.Text = "";
                txtLabInformadorActualiza.Text = "";
            }
        }

        private void LoadCombosLaboratorio()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo232 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 232);
            var Combo233 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 233);
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo152 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 152);
            var Combo154 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 154);
            var Combo155 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 155);

            var Combo257 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 257);
            var Combo258 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 258);
            var Combo268 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 268);
            var Combo259 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 259);
            var Combo264 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 264);
            var Combo261 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 261);

            var Combo196 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 196);
            var Combo260 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 260);
            var Combo265 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 265);
            var Combo262 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 262);
            var Combo263 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 263);
            var Combo266 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 266);
            var Combo279 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 279);

            Utils.LoadDropDownList(CONSISTENCIA_Heces_Primera_Muestra, "Value1", "Id", Combo232, DropDownListAction.Select);
            Utils.LoadDropDownList(RESTOS_ALIMENTICIOS_Heces_Primera_Muestra, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(SANGRE_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(MOCO_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(HUEVOS_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(QUISTES_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(TROFOZOITOS_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(HEMATIES_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(LEUCOCITOS_Heces_Primera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);

            Utils.LoadDropDownList(CONSISTENCIA_Heces_Segunda_Muestra, "Value1", "Id", Combo232, DropDownListAction.Select);
            Utils.LoadDropDownList(RESTOS_ALIMENTICIOS_Heces_Segunda_Muestra, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(SANGRE_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(MOCO_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(QUISTES_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(HUEVOS_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(TROFOZOITOS_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(HEMATIES_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(LEUCOCITOS_Heces_Segunda_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);

            Utils.LoadDropDownList(CONSISTENCIA_Heces_Tercera_Muestra, "Value1", "Id", Combo232, DropDownListAction.Select);
            Utils.LoadDropDownList(RESTOS_ALIMENTICIOS_Heces_Tercera_Muestra, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(SANGRE_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(MOCO_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(QUISTES_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(HUEVOS_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(TROFOZOITOS_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(HEMATIES_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(LEUCOCITOS_Heces_Tercera_Muestra, "Value1", "Id", Combo233, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCONSISTENCIA_PAR_SIMPLE, "Value1", "Id", Combo232, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRESTOS_ALIMENTICIOS_PAR_SIMPLE, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSANGRE_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMOCO_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlQUISTES_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHUEVOS_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTROFOZOITOS_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHEMATIES_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLEUCOCITOS_PAR_SIMPLE, "Value1", "Id", Combo233, DropDownListAction.Select);

            Utils.LoadDropDownList(COCAINA_COC_MAR, "Value1", "Id", Combo152, DropDownListAction.Select);
            Utils.LoadDropDownList(MARIHUANA_COC_MAR, "Value1", "Id", Combo152, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlGRUPO_SANGUINEO, "Value1", "Id", Combo154, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFACTOR_RH, "Value1", "Id", Combo155, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCOLOR_EX_ORINA, "Value1", "Id", Combo257, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlASPECTO_EX_ORINA, "Value1", "Id", Combo258, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCRISTALES_DEOX_CALCIO, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCRISTALES_DEUR_AMORFOS, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCRISTALES_FOSF_AMORFOS, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCRISTALES_FOSF_TRIPLES, "Value1", "Id", Combo268, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlGERMENES_EX_ORINA, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFILAMENTO_MUCOIDE_EX_ORINA, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLEVADURAS, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSANGRE_EX_ORINA, "Value1", "Id", Combo259, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUROBILINOGENO_EX_ORINA, "Value1", "Id", Combo264, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPROTEINAS_EX_ORINA, "Value1", "Id", Combo261, DropDownListAction.Select);
            Utils.LoadDropDownList(ddl_VDRL, "Value1", "Id", Combo196, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlNITRITOS_EX_ORINA, "Value1", "Id", Combo260, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlC_CETONICOS_EX_ORINA, "Value1", "Id", Combo265, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlGLUCOSA_EX_ORINA, "Value1", "Id", Combo262, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPIGMENTOS_EX_ORINA, "Value1", "Id", Combo263, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlACIDO_ASCORBIC_EX_ORINA, "Value1", "Id", Combo266, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlFACTOR_REMATOIDEO, "Value1", "Id", Combo196, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSALMONELA_TYPHI, "Value1", "Id", Combo155, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlREACCIONES_SEROLOGICAS, "Value1", "Id", Combo196, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDOSAJE_ALCOHOL, "Value1", "Id", Combo279, DropDownListAction.Select);
            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
             
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

            var _objData = _serviceBL.GetAllServices_(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));
            if (_objData.Count == 0)
            {

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
                        ((DatePicker)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;
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

        private void SearchControlAndShow(Control ctrlContainer, List<KeyValueDTO> objExamenLaboratorio)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is Panel)
                {
                    
                    if (((Panel)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentId = ((Panel)ctrl).Attributes.GetValue("Tag").ToString();
                        var x = objExamenLaboratorio.Find(p => p.Id == ComponentId);
                        bool llega = false;
                        if (ComponentId == "N009-ME000000002")
                        {                  
                            llega = true;
                        }
                        ((Panel)ctrl).Hidden = x == null ? true : false;

                    }
                }


                if (ctrl.HasControls())
                    SearchControlAndShow(ctrl, objExamenLaboratorio);

            }
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

        protected void btnGrabarLaboratorio_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabar.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabLaboratorio, Session["ServicioComponentIdLab"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdLab"].ToString());


            #region Dx Automaticos Laboratorio Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002132") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkLABORATORIO_NORMAL.Checked ? "1" : "0", "N009-MF000002132", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }


            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000000265") != null)
            {
                if (txtHEMOGLOBINA.Text != "")
                {
                    var DXAautomatico = SearchDxSugeridoOfSystem(txtHEMOGLOBINA.Text, "N009-MF000000265", "int");
                    if (DXAautomatico != null)
                    {
                        var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                        if (Result1 == null)
                        {
                            l.Add(DXAautomatico);
                        }
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000000261") != null)
            {
                if (txtGLUCOSA.Text != "")
                {
                    var DXAautomatico = SearchDxSugeridoOfSystem(txtGLUCOSA.Text, "N009-MF000000261", "int");
                    if (DXAautomatico != null)
                    {
                        var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                        if (Result1 == null)
                        {
                            l.Add(DXAautomatico);
                        }
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001086") != null)
            {
                if (txtCOLESTEROL_TOTAL.Text != "")
                {
                    var DXAautomatico = SearchDxSugeridoOfSystem(txtCOLESTEROL_TOTAL.Text, "N009-MF000001086", "int");
                    if (DXAautomatico != null)
                    {
                        var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                        if (Result1 == null)
                        {
                            l.Add(DXAautomatico);
                        }
                    }
                }
            }
            


            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001296") != null && txtTRIGLICERIDOS.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtTRIGLICERIDOS.Text, "N009-MF000001296", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001282") != null && txtHEMOGLOBINA_HEM_COM.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtHEMOGLOBINA_HEM_COM.Text, "N009-MF000001282", "double");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001061") != null && txtLEUCOCITOS_EX_ORINA.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtLEUCOCITOS_EX_ORINA.Text, "N009-MF000001061", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001063") != null && txtHEMATIES_EX_ORINA.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtHEMATIES_EX_ORINA.Text, "N009-MF000001063", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001874") != null && txtHEMOGLOBINA_HEM_COM.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(txtHEMOGLOBINA_HEM_COM.Text, "N009-MF000001874", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001904") != null && COLESTEROL_TOTAL_PERF_LIP.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(COLESTEROL_TOTAL_PERF_LIP.Text, "N009-MF000001904", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000001906") != null && TRIGLICERIDOS_PERF_LIP.Text != "")
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(TRIGLICERIDOS_PERF_LIP.Text, "N009-MF000001906", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdLab"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdLab"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = Session["ListaComponentesGrabar"].ToString();//"N009-ME000000002";
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
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), Session["ComponentIdESO"].ToString());

            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtLabAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtLabAuditorInsertar.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtLabAuditorEditar.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtLabEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtLabEvaluadorInsertar.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtLabEvaluadorEvaluar.Text = datosAuditoria.FechaHoraEvaluadorEdit;
            }

            //Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                //Mostrar
                var lGridComponente = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, int.Parse(ddlConsultorio.SelectedValue.ToString()), Session["ServiceId"].ToString());
                grdComponentes.DataSource = lGridComponente;
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

        protected void lnkLab_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_LABORATORIO_CLINICO;

            GenerateLaboratorio(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());

            Download(Session["ServiceId"].ToString() + "-ILAB_CLINICO.pdf", path + ".pdf");
  
        }

        private void GenerateLaboratorio(string pathFile, string ServicioId, string PacienteId)
        {
            PacientBL _pacientBL = new PacientBL();
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);

            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);

            
        }
    }
}