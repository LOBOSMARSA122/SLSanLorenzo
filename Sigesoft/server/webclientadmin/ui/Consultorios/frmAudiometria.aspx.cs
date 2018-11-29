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
    public partial class frmAudiometria : System.Web.UI.Page
    {
        string _ruta;
        DataSet dsGetRepo = null;
        ReportDocument rp;
        DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
        private Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _tmpTotalDiagnostic = null;
        List<string> _filesNameToMerge = new List<string>();

        ServiceBL _serviceBL = new ServiceBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = new List<Node.WinClient.BE.ServiceComponentFieldsList>();
        Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList _audiometria;
        List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _listAudiometria = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
        List<AudiometriaDataForGraphic> AudioODList = new List<AudiometriaDataForGraphic>();
        List<AudiometriaDataForGraphic> AudioOIList = new List<AudiometriaDataForGraphic>();

        private MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
        FileInfoDto fileInfo = null;
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
                btnReporteAudioCI.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=AudioCI");
                btnCertificadoAptitud.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("frmVisorReporte.aspx?Mode=Certificado");
              
                int RoleId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString());
                var ComponentesPermisoLectura = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId(9, RoleId).FindAll(p => p.i_Read == 1);
                List<string> ListaComponentesPermisoLectura = new List<string>();
                foreach (var item in ComponentesPermisoLectura)
                {
                    ListaComponentesPermisoLectura.Add(item.v_ComponentId);
                }
                Session["ComponentesPermisoLectura"] = ListaComponentesPermisoLectura;

                TabAudiometria.Hidden = true;
                TabAudiometriaInternacional.Hidden = true;

                TabAudiometria.Attributes.Add("Tag", "N002-ME000000005");

                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1);  //  DateTime.Parse("12/11/2016");
                dpFechaFin.SelectedDate = DateTime.Now; //  DateTime.Parse("12/11/2016"); 
                LoadCombos();

                #region Ids Desarrollo
                txtMarca.Attributes.Add("Tag", "N009-MF000000082");
                txtModelo.Attributes.Add("Tag", "N009-MF000000083");
                txtCalibracion.Attributes.Add("Tag", "N009-MF000000084");

                ddlsi_hizo_cambios.Attributes.Add("Tag", "N009-MF000001299");
                ddlestuvo_expuesto.Attributes.Add("Tag", "N009-MF000001300");
                ddlpresenta_algun.Attributes.Add("Tag", "N009-MF000001301");
                ddldurmio_mal_la_noche.Attributes.Add("Tag", "N009-MF000001302");
                ddlconsumio_alcohol.Attributes.Add("Tag", "N009-MF000001303");

                txttiempo_de_trabajo.Attributes.Add("Tag", "N009-MF000001378");
                chkRinitisa.Attributes.Add("Tag", "N009-MF000000089");
                //chkOtitisa.Attributes.Add("Tag", "N009-MF000000091");
                //ddlMedicamentos.Attributes.Add("Tag", "N009-MF000000087");
                chkuso_de_medicamentos.Attributes.Add("Tag", "N009-MF000000090");
                ddlDiabetes.Attributes.Add("Tag", "N009-MF000000095");
                ddlMeningitis.Attributes.Add("Tag", "N009-MF000000097");
                ddlDislipidemia.Attributes.Add("Tag", "N009-MF000000100");
                ddlSarampion.Attributes.Add("Tag", "N009-MF000000098");
                //ddlSordera.Attributes.Add("Tag", "N009-MF000000101");
                ddlTiroida.Attributes.Add("Tag", "N009-MF000001304");
                //ddlTec.Attributes.Add("Tag", "N009-MF000001305");
                chkSorderaFamiliara.Attributes.Add("Tag", "N009-MF000001306");
                chkSustQuimicasa.Attributes.Add("Tag", "N009-MF000001307");
                chkUsoMP3.Attributes.Add("Tag", "N009-MF000001308");
                chkTiro.Attributes.Add("Tag", "N009-MF000001309");
                //chkotros.Attributes.Add("Tag", "N009-MF000001310");
                chksordera.Attributes.Add("Tag", "N009-MF000000092");
                chkacuferos.Attributes.Add("Tag", "N009-MF000000093");
                chkvertigos.Attributes.Add("Tag", "N009-MF000000094");
                chkotalgia.Attributes.Add("Tag", "N009-MF000000096");
                chksecrecion_otica.Attributes.Add("Tag", "N009-MF000000099");
                txtOidoDerecho.Attributes.Add("Tag", "N002-MF000000178");
                txtOidoIzquierdo.Attributes.Add("Tag", "N002-MF000000179");
                chknormoacusia_bilateral.Attributes.Add("Tag", "N009-MF000002128");
                chknormoacusia_od.Attributes.Add("Tag", "N009-MF000002129");
                chknormoacusia_oi.Attributes.Add("Tag", "N009-MF000002130");
                chkRuidoExtra.Attributes.Add("Tag", "N009-MF000000100");

                //OD
                txtOD_VA_125.Attributes.Add("Tag", Constants.txt_VA_OD_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_125).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_250.Attributes.Add("Tag", Constants.txt_VA_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_250).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_500.Attributes.Add("Tag", Constants.txt_VA_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_500).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_1000.Attributes.Add("Tag", Constants.txt_VA_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_2000.Attributes.Add("Tag", Constants.txt_VA_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_3000.Attributes.Add("Tag", Constants.txt_VA_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_4000.Attributes.Add("Tag", Constants.txt_VA_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_6000.Attributes.Add("Tag", Constants.txt_VA_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VA_8000.Attributes.Add("Tag", Constants.txt_VA_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOD_VO_125.Attributes.Add("Tag", Constants.txt_VO_OD_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_125).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_250.Attributes.Add("Tag", Constants.txt_VO_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_250).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_500.Attributes.Add("Tag", Constants.txt_VO_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_500).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_1000.Attributes.Add("Tag", Constants.txt_VO_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_2000.Attributes.Add("Tag", Constants.txt_VO_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_3000.Attributes.Add("Tag", Constants.txt_VO_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_4000.Attributes.Add("Tag", Constants.txt_VO_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_6000.Attributes.Add("Tag", Constants.txt_VO_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_VO_8000.Attributes.Add("Tag", Constants.txt_VO_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOD_EM_125.Attributes.Add("Tag", Constants.txt_EM_OD_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_125).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_250.Attributes.Add("Tag", Constants.txt_EM_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_250).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_500.Attributes.Add("Tag", Constants.txt_EM_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_500).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_1000.Attributes.Add("Tag", Constants.txt_EM_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_2000.Attributes.Add("Tag", Constants.txt_EM_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_3000.Attributes.Add("Tag", Constants.txt_EM_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_4000.Attributes.Add("Tag", Constants.txt_EM_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_6000.Attributes.Add("Tag", Constants.txt_EM_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_EM_8000.Attributes.Add("Tag", Constants.txt_EM_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOD_AN_125.Attributes.Add("Tag", Constants.txt_AN_OD_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_125).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_250.Attributes.Add("Tag", Constants.txt_AN_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_250).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_500.Attributes.Add("Tag", Constants.txt_AN_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_500).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_1000.Attributes.Add("Tag", Constants.txt_AN_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_2000.Attributes.Add("Tag", Constants.txt_AN_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_3000.Attributes.Add("Tag", Constants.txt_AN_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_4000.Attributes.Add("Tag", Constants.txt_AN_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_6000.Attributes.Add("Tag", Constants.txt_AN_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOD_AN_8000.Attributes.Add("Tag", Constants.txt_AN_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OD_8000).ServiceComponentFieldValues[0].v_Value1;


                //OI
                txtOI_VA_125.Attributes.Add("Tag", Constants.txt_VA_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_125).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_250.Attributes.Add("Tag", Constants.txt_VA_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_250).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_500.Attributes.Add("Tag", Constants.txt_VA_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_500).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_1000.Attributes.Add("Tag", Constants.txt_VA_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_2000.Attributes.Add("Tag", Constants.txt_VA_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_3000.Attributes.Add("Tag", Constants.txt_VA_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_4000.Attributes.Add("Tag", Constants.txt_VA_OI_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_6000.Attributes.Add("Tag", Constants.txt_VA_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VA_8000.Attributes.Add("Tag", Constants.txt_VA_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOI_VO_125.Attributes.Add("Tag", Constants.txt_VO_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_125).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_250.Attributes.Add("Tag", Constants.txt_VO_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_250).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_500.Attributes.Add("Tag", Constants.txt_VO_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_500).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_1000.Attributes.Add("Tag", Constants.txt_VO_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_2000.Attributes.Add("Tag", Constants.txt_VO_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_3000.Attributes.Add("Tag", Constants.txt_VO_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_4000.Attributes.Add("Tag", Constants.txt_VO_OI_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_6000.Attributes.Add("Tag", Constants.txt_VO_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_VO_8000.Attributes.Add("Tag", Constants.txt_VO_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOI_EM_125.Attributes.Add("Tag", Constants.txt_EM_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_125).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_250.Attributes.Add("Tag", Constants.txt_EM_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_250).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_500.Attributes.Add("Tag", Constants.txt_EM_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_500).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_1000.Attributes.Add("Tag", Constants.txt_EM_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_2000.Attributes.Add("Tag", Constants.txt_EM_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_3000.Attributes.Add("Tag", Constants.txt_EM_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_4000.Attributes.Add("Tag", Constants.txt_EM_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000txt_EM_OI_3000txt_EM_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_6000.Attributes.Add("Tag", Constants.txt_EM_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_EM_8000.Attributes.Add("Tag", Constants.txt_EM_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOI_AN_125.Attributes.Add("Tag", Constants.txt_AN_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_125).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_250.Attributes.Add("Tag", Constants.txt_AN_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_250).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_500.Attributes.Add("Tag", Constants.txt_AN_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_500).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_1000.Attributes.Add("Tag", Constants.txt_AN_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_1000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_2000.Attributes.Add("Tag", Constants.txt_AN_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_2000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_3000.Attributes.Add("Tag", Constants.txt_AN_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_3000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_4000.Attributes.Add("Tag", Constants.txt_AN_OI_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_4000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_6000.Attributes.Add("Tag", Constants.txt_AN_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_6000).ServiceComponentFieldValues[0].v_Value1;
                txtOI_AN_8000.Attributes.Add("Tag", Constants.txt_AN_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_AN_OI_8000).ServiceComponentFieldValues[0].v_Value1;

                txtMultimediaFileId_OD.Attributes.Add("Tag", Constants.txt_MULTIMEDIA_FILE_OD);
                txtMultimediaFileId_OI.Attributes.Add("Tag", Constants.txt_MULTIMEDIA_FILE_OI);
                txtServiceComponentMultimediaId_OD.Attributes.Add("Tag", Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_OD);
                txtServiceComponentMultimediaId_OI.Attributes.Add("Tag", Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_OI);
                #endregion


                TabAudiometriaInternacional.Attributes.Add("Tag", "N002-ME000000005");
                #region Ids Internacional
                ddlAU_Condicion.Attributes.Add("Tag", "N005-MF000001378");
                txtAU_Observaciones.Attributes.Add("Tag", "N005-MF000000178");

                chkSupuracion.Attributes.Add("Tag", "N005-MF000000089");


                txtAnioServicioMilitar.Attributes.Add("Tag", "N005-MF000001876");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001876") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001876").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaServicioMilitar.Attributes.Add("Tag", "N005-MF000001879");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001879") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001879").ServiceComponentFieldValues[0].v_Value1;

                txtAnioDeportesAereos.Attributes.Add("Tag", "N005-MF000001880");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001880") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001880").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaDeportesAereos.Attributes.Add("Tag", "N005-MF000001881");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001881") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001881").ServiceComponentFieldValues[0].v_Value1;

                txtAnioDeporteSubmarino.Attributes.Add("Tag", "N005-MF000001882");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001882") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001882").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaDeporteSubmarino.Attributes.Add("Tag", "N005-MF000001883");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001883") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001883").ServiceComponentFieldValues[0].v_Value1;

                txtAnioManipulacionArmas.Attributes.Add("Tag", "N005-MF000001884");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001884") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001884").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaManipulacionArmas.Attributes.Add("Tag", "N005-MF000001885");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001885") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001885").ServiceComponentFieldValues[0].v_Value1;

                txtAnioExposicionMusica.Attributes.Add("Tag", "N005-MF000001886");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001886") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001886").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaExposicionMusica.Attributes.Add("Tag", "N005-MF000001887");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001887") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001887").ServiceComponentFieldValues[0].v_Value1;

                txtAnioUsoaudifnos.Attributes.Add("Tag", "N005-MF000001888");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001888") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001888").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaUsoaudifnos.Attributes.Add("Tag", "N005-MF000001889");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001889") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001889").ServiceComponentFieldValues[0].v_Value1;

                txtAnioMotociclismo.Attributes.Add("Tag", "N005-MF000001890");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001890") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001890").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaMotociclismo.Attributes.Add("Tag", "N005-MF000001891");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001891") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001891").ServiceComponentFieldValues[0].v_Value1;

                txtAnioOtro.Attributes.Add("Tag", "N005-MF000001892");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001892") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001892").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaOtro.Attributes.Add("Tag", "N005-MF000001893");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001893") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001893").ServiceComponentFieldValues[0].v_Value1;


                txtIntensidad.Attributes.Add("Tag", "N005-MF000000179");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000179") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000179").ServiceComponentFieldValues[0].v_Value1;
                txtHoras.Attributes.Add("Tag", "N005-MF000001894");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001894") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001894").ServiceComponentFieldValues[0].v_Value1;

                txtActualAnio.Attributes.Add("Tag", "N005-MF000001933");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001933") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001933").ServiceComponentFieldValues[0].v_Value1;
                txtActualOD.Attributes.Add("Tag", "N005-MF000001934");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001934") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001934").ServiceComponentFieldValues[0].v_Value1;
                txtActualOI.Attributes.Add("Tag", "N005-MF000001935");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001935") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001935").ServiceComponentFieldValues[0].v_Value1;
                txtMenoscaboAuditivo.Attributes.Add("Tag", "N005-MF000001979");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001979") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001979").ServiceComponentFieldValues[0].v_Value1;

                txtCalibracion_Inter.Attributes.Add("Tag", "N005-MF000000084");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000084") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000084").ServiceComponentFieldValues[0].v_Value1;
                txtMarca_Inter.Attributes.Add("Tag", "N005-MF000000082");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000082") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000082").ServiceComponentFieldValues[0].v_Value1;
                txtModelo_Inter.Attributes.Add("Tag", "N005-MF000000083");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000083") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000083").ServiceComponentFieldValues[0].v_Value1;
                txtNivelRuidoAmbiental.Attributes.Add("Tag", "N005-MF000001874");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001874") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001874").ServiceComponentFieldValues[0].v_Value1;


                ddlTipoRuido.Attributes.Add("Tag", "N005-MF000001307");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001307") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001307").ServiceComponentFieldValues[0].v_Value1;
                ddlHorasPorDia.Attributes.Add("Tag", "N005-MF000001308");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001308") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001308").ServiceComponentFieldValues[0].v_Value1;
                ddlTapones.Attributes.Add("Tag", "N005-MF000001309");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001309") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001309").ServiceComponentFieldValues[0].v_Value1;
                ddlOrejeras.Attributes.Add("Tag", "N005-MF000001310");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001310") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001310").ServiceComponentFieldValues[0].v_Value1;
                ddlAmbos.Attributes.Add("Tag", "N005-MF000001895");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001895") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001895").ServiceComponentFieldValues[0].v_Value1;


                //OD
                txtOD_VA_125_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_125);

                txtOD_VA_250_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_250).ServiceComponentFieldValues[0].v_Value1;


                txtOD_VA_500_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_500).ServiceComponentFieldValues[0].v_Value1;


                txtOD_VA_1000_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_1000).ServiceComponentFieldValues[0].v_Value1;
               

                txtOD_VA_2000_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_2000).ServiceComponentFieldValues[0].v_Value1;
                

                txtOD_VA_3000_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_3000).ServiceComponentFieldValues[0].v_Value1;
                

                txtOD_VA_4000_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_4000).ServiceComponentFieldValues[0].v_Value1;
                

                txtOD_VA_6000_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_6000).ServiceComponentFieldValues[0].v_Value1;
                

                txtOD_VA_8000_Inter.Attributes.Add("Tag", Constants.txt_VA_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VA_8000).ServiceComponentFieldValues[0].v_Value1;



                txtOD_VO_125_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_125).ServiceComponentFieldValues[0].v_Value1;
            

                txtOD_VO_250_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_250).ServiceComponentFieldValues[0].v_Value1;
               

                txtOD_VO_500_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_500).ServiceComponentFieldValues[0].v_Value1;
               

                txtOD_VO_1000_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_1000).ServiceComponentFieldValues[0].v_Value1;
              

                txtOD_VO_2000_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_2000).ServiceComponentFieldValues[0].v_Value1;
               

                txtOD_VO_3000_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_3000).ServiceComponentFieldValues[0].v_Value1;
                

                txtOD_VO_4000_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_4000).ServiceComponentFieldValues[0].v_Value1;
                

                txtOD_VO_6000_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_6000).ServiceComponentFieldValues[0].v_Value1;
               
                txtOD_VO_8000_Inter.Attributes.Add("Tag", Constants.txt_VO_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_VO_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOD_EM_125_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_125).ServiceComponentFieldValues[0].v_Value1;


                txtOD_EM_250_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_250).ServiceComponentFieldValues[0].v_Value1;
          
                txtOD_EM_500_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_500).ServiceComponentFieldValues[0].v_Value1;
              
                txtOD_EM_1000_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_1000).ServiceComponentFieldValues[0].v_Value1;
           
                txtOD_EM_2000_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_2000).ServiceComponentFieldValues[0].v_Value1;
          

                txtOD_EM_3000_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_3000).ServiceComponentFieldValues[0].v_Value1;
             

                txtOD_EM_4000_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_4000).ServiceComponentFieldValues[0].v_Value1;
            

                txtOD_EM_6000_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_6000).ServiceComponentFieldValues[0].v_Value1;
               

                txtOD_EM_8000_Inter.Attributes.Add("Tag", Constants.txt_EM_OD_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txtOD_EM_8000).ServiceComponentFieldValues[0].v_Value1;
               

                //OI
                txtOI_VA_125_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_125).ServiceComponentFieldValues[0].v_Value1;
          
                txtOI_VA_250_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_250).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_VA_500_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_500).ServiceComponentFieldValues[0].v_Value1;
         
                txtOI_VA_1000_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_1000).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_VA_2000_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_2000).ServiceComponentFieldValues[0].v_Value1;
        
                txtOI_VA_3000_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_3000).ServiceComponentFieldValues[0].v_Value1;
         
                txtOI_VA_4000_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_4000).ServiceComponentFieldValues[0].v_Value1;
               
                txtOI_VA_6000_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_6000).ServiceComponentFieldValues[0].v_Value1;
                
                txtOI_VA_8000_Inter.Attributes.Add("Tag", Constants.txt_VA_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOI_VO_125_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_125).ServiceComponentFieldValues[0].v_Value1;
     
                txtOI_VO_250_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_250).ServiceComponentFieldValues[0].v_Value1;
                
                txtOI_VO_500_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_500).ServiceComponentFieldValues[0].v_Value1;
         
                txtOI_VO_1000_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_1000).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_VO_2000_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_2000).ServiceComponentFieldValues[0].v_Value1;
         
                txtOI_VO_3000_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_3000).ServiceComponentFieldValues[0].v_Value1;
             
                txtOI_VO_4000_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_4000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_4000).ServiceComponentFieldValues[0].v_Value1;
               
                txtOI_VO_6000_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_6000).ServiceComponentFieldValues[0].v_Value1;
                
                txtOI_VO_8000_Inter.Attributes.Add("Tag", Constants.txt_VO_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_8000).ServiceComponentFieldValues[0].v_Value1;

                txtOI_EM_125_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_125);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_125).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_EM_250_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_250);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_250).ServiceComponentFieldValues[0].v_Value1;
               
                txtOI_EM_500_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_500);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_500).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_EM_1000_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_1000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_1000).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_EM_2000_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_2000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_2000).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_EM_3000_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_EM_4000_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_3000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000txt_EM_OI_3000txt_EM_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_4000).ServiceComponentFieldValues[0].v_Value1;
                
                txtOI_EM_6000_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_6000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_6000).ServiceComponentFieldValues[0].v_Value1;
              
                txtOI_EM_8000_Inter.Attributes.Add("Tag", Constants.txt_EM_OI_8000);//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_8000).ServiceComponentFieldValues[0].v_Value1;
             
           

                txtMultimediaFileId_OD_Inter.Attributes.Add("Tag", Constants.txt_MULTIMEDIA_FILE_OD);
                txtMultimediaFileId_OI_Inter.Attributes.Add("Tag", Constants.txt_MULTIMEDIA_FILE_OI);
                txtServiceComponentMultimediaId_OD_Inter.Attributes.Add("Tag", Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_OD);
                txtServiceComponentMultimediaId_OI_Inter.Attributes.Add("Tag", Constants.txt_SERVICE_COMPONENT_MULTIMEDIA_OI);


                chkVertigo.Attributes.Add("Tag", "N005-MF000000090");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000090") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000090").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkOtitisa.Attributes.Add("Tag", "N009-MF000000091");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000091") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000091").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkParatodiditis.Attributes.Add("Tag", "N005-MF000000092");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000092") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000092").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkMeningitis.Attributes.Add("Tag", "N005-MF000000093");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000093") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000093").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkGolpesCefalicos.Attributes.Add("Tag", "N005-MF000000094");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000094") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000094").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkParalisisFacial.Attributes.Add("Tag", "N005-MF000000095");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000095") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000095").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkTTOANTITBC.Attributes.Add("Tag", "N005-MF000000096");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000096") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000096").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkTTOOtotoxicos.Attributes.Add("Tag", "N005-MF000000097");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000097") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000097").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkConsumoMedicamento.Attributes.Add("Tag", "N005-MF000000098");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000098") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000098").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkExposicionSolventes.Attributes.Add("Tag", "N005-MF000000099");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000099") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000099").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;

                //chkRuidoExtra.Attributes.Add("Tag", "N005-MF000000100");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000100") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000100").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkRuidoLaboral.Attributes.Add("Tag", "N005-MF000000101");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000101") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000000101").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkServicioMilitar.Attributes.Add("Tag", "N005-MF000001299");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001299") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001299").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkDeportesAereos.Attributes.Add("Tag", "N005-MF000001300");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001300") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001300").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkDeportesSubmarinos.Attributes.Add("Tag", "N005-MF000001301");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001301") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001301").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkManipulacionArmas.Attributes.Add("Tag", "N005-MF000001302");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001302") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001302").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkExposicionMusica.Attributes.Add("Tag", "N005-MF000001303");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001303") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001303").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkUsoAudifonos.Attributes.Add("Tag", "N005-MF000001304");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001304") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001304").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkMotociclismo.Attributes.Add("Tag", "N005-MF000001305");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001305") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001305").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkOtro.Attributes.Add("Tag", "N005-MF000001306");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001306") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N005-MF000001306").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;


                chkNormoBilateral.Attributes.Add("Tag", "N009-MF000002815");
                chkNormoOD.Attributes.Add("Tag", "N009-MF000002816");
                chkNormoOI.Attributes.Add("Tag", "N009-MF000002817");
                #endregion

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
            ddlConsultorio.SelectedValue = "15";
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);

            SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            Utils.LoadDropDownList(ddlFirmaAuditor, "Value1", "Id", oSystemParameterBL.GetProfessionalAuditores(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUsuarioGrabarAudio, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
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
            TabAudiometria.Hidden = true;
            TabAudiometriaInternacional.Hidden = true;
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
                    if (item.ComponentId == TabAudiometria.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosAudiometria();
                        ddlUsuarioGrabarAudio.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                        ddlUsuarioGrabarAudio.Enabled = false;
                        ObtenerDatosAudiometria(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabAudiometria.Hidden = false;
                    }

                    else if (item.ComponentId == TabAudiometriaInternacional.Attributes.GetValue("Tag").ToString())
                    {
                        LoadCombosAudiometriaInternacional();
                        ObtenerDatosAudiometriaInternacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                        TabAudiometriaInternacional.Hidden = false;
                    }
                    TabAudiometria.Hidden = false;
                    TabAudiometriaInternacional.Hidden = false;
                }
            }
            else
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());

                var ListaComponenentesConPermiso = (List<string>)Session["ComponentesPermisoLectura"];

                foreach (var item in ListaComponenentesConPermiso)
                {
                    if (item == TabAudiometria.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.AUDIOMETRIA_ID);
                        if (Resultado != null)
                        {
                            LoadCombosAudiometria();
                            ddlUsuarioGrabarAudio.SelectedValue = ((ClientSession)Session["objClientSession"]).i_SystemUserId.ToString();
                            ddlUsuarioGrabarAudio.Enabled = false;
                            ObtenerDatosAudiometria(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabAudiometria.Hidden = false;
                        }

                    }

                    else if (item == TabAudiometriaInternacional.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == "N002-ME000000005");
                        if (Resultado != null)
                        {
                            LoadCombosAudiometriaInternacional();
                            ObtenerDatosAudiometriaInternacional(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                            TabAudiometriaInternacional.Hidden = false;
                        }
                        TabAudiometria.Hidden = false;
                        TabAudiometriaInternacional.Hidden = false;
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
            ComponentesId.Add("N002-ME000000005");
            ComponentesId.Add("N002-ME000000005");
            var _objData = _serviceBL.GetAllServices_Consultorio(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1), ComponentesId.ToArray());
            if (_objData.Count == 0)
            {
                TabAudiometria.Hidden = true;
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

                if (ctrl is NumberBox)
                {
                    if (((NumberBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((NumberBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((NumberBox)ctrl).Text = ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId) == null ? "" : ListaValores.Find(p => p.v_ComponentFieldsId == ComponentFieldId).ServiceComponentFieldValues[0].v_Value1;

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
                if (ctrl.HasControls())
                    SearchControlAndSetValues(ctrl, pstrServiceComponentId);
            }

            Session["_serviceComponentFieldsList"] = _serviceComponentFieldsList;


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

        private void SearchControlAndClean(Control ctrlContainer, List<Sigesoft.Node.WinClient.BE.ComponentFieldsList> ListaValoresPorDefecto)
        {
            //var r = N005-MF000000236
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
                if (ctrl is NumberBox)
                {
                    if (((NumberBox)ctrl).Attributes.GetValue("Tag") != null)
                    {
                        //AMC
                        string ComponentFieldId = ((NumberBox)ctrl).Attributes.GetValue("Tag").ToString();
                        ((NumberBox)ctrl).Text = ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId) == null ? "" : ListaValoresPorDefecto.Find(p => p.v_ComponentFieldId == ComponentFieldId).v_DefaultText;

                    }
                }
                if (ctrl.HasControls())
                    SearchControlAndClean(ctrl, ListaValoresPorDefecto);

            }
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

        private void LoadCombosAudiometria()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo253 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 253);
            var Combo191 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 191);

            //Utils.LoadDropDownList(ddlsi_hizo_cambios, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlestuvo_expuesto, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlpresenta_algun, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddldurmio_mal_la_noche, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlconsumio_alcohol, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlRinitis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSarampion, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(chkOtitisa, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(chkuso_de_medicamentos, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlSordera, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlMedicamentos, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlTec, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlDiabetes, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlSorderaFamiliar, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlMeningitis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDislipidemia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTiroida, "Value1", "Id", Combo111, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlSustQuimicas, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(chkRuidoExtra, "Value1", "Id", Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSarampion, "Value1", "Id", Combo191, DropDownListAction.Select);
           
        }

        private void LoadCombosAudiometriaInternacional()
        {
            OperationResult objOperationResult = new OperationResult();

            //AUDIOMETRÍA
            var Combo267 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 267);
            var Combo268 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 268);
            var Combo269 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 269);
            var Combo266 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 266);

            Utils.LoadDropDownList(ddlTipoRuido, "Value1", "Id", Combo267, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHorasPorDia, "Value1", "Id", Combo268, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTapones, "Value1", "Id", Combo269, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrejeras, "Value1", "Id", Combo269, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbos, "Value1", "Id", Combo269, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAU_Condicion, "Value1", "Id", Combo266, DropDownListAction.Select);
        }

        private void ObtenerDatosAudiometria(string pServiceId, string pPersonId)
        {
            var oExamenAudiometria = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 15);
            Session["ServicioComponentIdAudiometria"] = oExamenAudiometria[0].ServicioComponentId;
            var objExamenAudiometria = _serviceBL.GetServiceComponentFields(oExamenAudiometria == null ? "" : oExamenAudiometria[0].ServicioComponentId, pServiceId);
            Session["ComponentesAudiometria"] = objExamenAudiometria;
            if (Session["ComponentesAudiometria"] != null)
            {
                SearchControlAndLoadData(TabAudiometria, Session["ServicioComponentIdAudiometria"].ToString(), (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesAudiometria"]);
            }
        }

        private void ObtenerDatosAudiometriaInternacional(string pServiceId, string pPersonId)
        {
            OperationResult objOperationResult = new OperationResult();
            var oExamenAudiometria = _serviceBL.ObtenerIdsParaImportacionExcel(new List<string> { pServiceId }, 15);
            Session["ServicioComponentIdAudiometria"] = oExamenAudiometria[0].ServicioComponentId;
            var objExamenAudiometria = _serviceBL.GetServiceComponentFields(oExamenAudiometria == null ? "" : oExamenAudiometria[0].ServicioComponentId, pServiceId);
            Session["ComponentesAudiometria"] = objExamenAudiometria;
            if (objExamenAudiometria.ToList().Count != 0)
            {
                SearchControlAndLoadData(TabAudiometriaInternacional, Session["ServicioComponentIdAudiometria"].ToString(), (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["ComponentesAudiometria"]);
                #region Campos de Auditoria

                var datosAuditoria = HistoryBL.CamposAuditoria(oExamenAudiometria[0].ServicioComponentId);
                if (datosAuditoria != null)
                {
                    txtAudiometriaAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                    txtAudiometriaAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                    txtAudiometriaAuditorActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                    txtAudiometriaEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtAudiometriaEvaluadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtAudiometriaEvaluadorEvaluacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                    txtAudiometriaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                    txtAudiometriaInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                    txtAudiometriaInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
                }

                #endregion
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabAudiometriaInternacional, _tmpServiceComponentsForBuildMenuList);

                txtAudiometriaAuditor.Text = "";
                txtAudiometriaAuditorInsercion.Text = "";
                txtAudiometriaAuditorActualizacion.Text = "";

                txtAudiometriaEvaluador.Text = "";
                txtAudiometriaEvaluadorInsercion.Text = "";
                txtAudiometriaEvaluadorEvaluacion.Text = "";

                txtAudiometriaInformador.Text = "";
                txtAudiometriaInformadorInserta.Text = "";
                txtAudiometriaInformadorActualizacion.Text = "";
            }

        }

        private void SaveImagenAudiograma_CI()
        {
            var chartAudiogramaOD = new MemoryStream();
            Chart1.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);
            Chart1.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);
            

            var chartAudiogramaOI = new MemoryStream();
            Chart2.SaveImage(chartAudiogramaOI, ChartImageFormat.Png);

            string[] IDs = null;

            IDs = SavePrepared(txtMultimediaFileId_OD_Inter.Text, txtServiceComponentMultimediaId_OD_Inter.Text, Session["PersonId"].ToString(), Session["ServicioComponentIdAudiometria"].ToString(), "IMAGEN AUDIOGRAMA OD", "IMAGEN PROVENIENTE DEL UC AUDIOMETRIA OD", (byte[])Session["ImagenOD"]);

            if (IDs != null)  // GRABAR
            {
                txtMultimediaFileId_OD_Inter.Text = IDs[0];
                txtServiceComponentMultimediaId_OD_Inter.Text = IDs[1];

                var audiograma = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>()
                {                
                    new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList { v_ComponentFieldId = txtMultimediaFileId_OD_Inter.ID, v_Value1 = txtMultimediaFileId_OD_Inter.Text },                 
                    new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList { v_ComponentFieldId = txtServiceComponentMultimediaId_OD_Inter.ID, v_Value1 = txtServiceComponentMultimediaId_OD_Inter.Text },                
                };

                _listAudiometria.AddRange(audiograma);

            }

            IDs = SavePrepared(txtMultimediaFileId_OI_Inter.Text, txtServiceComponentMultimediaId_OI_Inter.Text, Session["PersonId"].ToString(), Session["ServicioComponentIdAudiometria"].ToString(), "IMAGEN AUDIOGRAMA OI", "IMAGEN PROVENIENTE DEL UC AUDIOMETRIA OI", (byte[])Session["ImagenOI"]);

            if (IDs != null)    // GRABAR
            {
                txtMultimediaFileId_OI_Inter.Text = IDs[0];
                txtServiceComponentMultimediaId_OI_Inter.Text = IDs[1];

                var audiograma = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>()
                {                                
                    new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList { v_ComponentFieldId = txtMultimediaFileId_OI_Inter.ID, v_Value1 = txtMultimediaFileId_OI_Inter.Text },                
                    new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList { v_ComponentFieldId = txtServiceComponentMultimediaId_OI_Inter.ID, v_Value1 = txtServiceComponentMultimediaId_OI_Inter.Text }
                };

                _listAudiometria.AddRange(audiograma);
            }


        }

        private string[] SavePrepared(string multimediaFileId, string serviceComponentMultimediaId, string personId, string serviceComponentId, string fileName, string description, byte[] chartImagen)
        {
            string[] IDs = null;
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
                IDs = _multimediaFileBL.AddMultimediaFileComponent(ref operationResult, fileInfo, ((ClientSession)Session["objClientSession"]).GetAsList());

                // Analizar el resultado de la operación
                if (operationResult.Success != 1)
                {
                    //MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
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
            }

            return IDs;

        }

        protected void btnAudiometria_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabarAudio.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }

            SaveImagenAudiograma_CI();

            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabAudiometria, Session["ServicioComponentIdAudiometria"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),
                                                      Session["ServicioComponentIdAudiometria"].ToString());
            ShowGraphicOD();
            ShowGraphicOI();

            //Gabar Dx
            #region Capturar [Comentarios, estado, procedencia de un exmanen componente]

            var serviceComponentDto = new servicecomponentDto();
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdAudiometria"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdAudiometria"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N002-ME000000005";
            serviceComponentDto.v_ServiceId = Session["ServiceId"].ToString();
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            Session["UsuarioLogueado"] = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            // Grabar Dx por examen componente mas sus restricciones

            ((ClientSession)Session["objClientSession"]).i_SystemUserId = int.Parse(ddlUsuarioGrabarAudio.SelectedValue.ToString());


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                   null,
                                                   serviceComponentDto,
                                                   ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                   true);
      
            
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

        protected void aspButtonOD_Click(object sender, EventArgs e)
        {
            //VA OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_125;
            _audiometria.v_Value1 = txtOD_VA_125.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_250;
            _audiometria.v_Value1 = txtOD_VA_250.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_500;
            _audiometria.v_Value1 = txtOD_VA_500.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_1000;
            _audiometria.v_Value1 = txtOD_VA_1000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_2000;
            _audiometria.v_Value1 = txtOD_VA_2000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_3000;
            _audiometria.v_Value1 = txtOD_VA_3000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_4000;
            _audiometria.v_Value1 = txtOD_VA_4000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_6000;
            _audiometria.v_Value1 = txtOD_VA_6000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_8000;
            _audiometria.v_Value1 = txtOD_VA_8000.Text;
            _listAudiometria.Add(_audiometria);






            //VO OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_125;
            _audiometria.v_Value1 = txtOD_VO_125.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_250;
            _audiometria.v_Value1 = txtOD_VO_250.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_500;
            _audiometria.v_Value1 = txtOD_VO_500.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_1000;
            _audiometria.v_Value1 = txtOD_VO_1000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_2000;
            _audiometria.v_Value1 = txtOD_VO_2000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_3000;
            _audiometria.v_Value1 = txtOD_VO_3000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_4000;
            _audiometria.v_Value1 = txtOD_VO_4000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_6000;
            _audiometria.v_Value1 = txtOD_VO_6000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_8000;
            _audiometria.v_Value1 = txtOD_VO_8000.Text;
            _listAudiometria.Add(_audiometria);









            //EM OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_125;
            _audiometria.v_Value1 = txtOD_EM_125.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_250;
            _audiometria.v_Value1 = txtOD_EM_250.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_500;
            _audiometria.v_Value1 = txtOD_EM_500.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_1000;
            _audiometria.v_Value1 = txtOD_EM_1000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_2000;
            _audiometria.v_Value1 = txtOD_EM_2000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_3000;
            _audiometria.v_Value1 = txtOD_EM_3000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_4000;
            _audiometria.v_Value1 = txtOD_EM_4000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_6000;
            _audiometria.v_Value1 = txtOD_EM_6000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_8000;
            _audiometria.v_Value1 = txtOD_EM_8000.Text;
            _listAudiometria.Add(_audiometria);

            ShowGraphicOD();
            ShowGraphicOI();

            var chartAudiogramaOD = new MemoryStream();
            Chart1.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);
            Session["ImagenOD"] = chartAudiogramaOD.ToArray();

            var chartAudiogramaOI = new MemoryStream();
            Chart2.SaveImage(chartAudiogramaOI, ChartImageFormat.Png);
            Session["ImagenOI"] = chartAudiogramaOI.ToArray();
        }

        protected void aspButtonOI_Click(object sender, EventArgs e)
        {


            //VA OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_125;
            _audiometria.v_Value1 = txtOI_VA_125.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_250;
            _audiometria.v_Value1 = txtOI_VA_250.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_500;
            _audiometria.v_Value1 = txtOI_VA_500.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_1000;
            _audiometria.v_Value1 = txtOI_VA_1000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_2000;
            _audiometria.v_Value1 = txtOI_VA_2000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_3000;
            _audiometria.v_Value1 = txtOI_VA_3000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_4000;
            _audiometria.v_Value1 = txtOI_VA_4000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_6000;
            _audiometria.v_Value1 = txtOI_VA_6000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_8000;
            _audiometria.v_Value1 = txtOI_VA_8000.Text;
            _listAudiometria.Add(_audiometria);






            //VO OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_125;
            _audiometria.v_Value1 = txtOI_VO_125.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_250;
            _audiometria.v_Value1 = txtOI_VO_250.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_500;
            _audiometria.v_Value1 = txtOI_VO_500.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_1000;
            _audiometria.v_Value1 = txtOI_VO_1000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_2000;
            _audiometria.v_Value1 = txtOI_VO_2000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_3000;
            _audiometria.v_Value1 = txtOI_VO_3000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_4000;
            _audiometria.v_Value1 = txtOI_VO_4000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_6000;
            _audiometria.v_Value1 = txtOI_VO_6000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_8000;
            _audiometria.v_Value1 = txtOI_VO_8000.Text;
            _listAudiometria.Add(_audiometria);









            //EM OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_125;
            _audiometria.v_Value1 = txtOI_EM_125.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_250;
            _audiometria.v_Value1 = txtOI_EM_250.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_500;
            _audiometria.v_Value1 = txtOI_EM_500.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_1000;
            _audiometria.v_Value1 = txtOI_EM_1000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_2000;
            _audiometria.v_Value1 = txtOI_EM_2000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_3000;
            _audiometria.v_Value1 = txtOI_EM_3000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_4000;
            _audiometria.v_Value1 = txtOI_EM_4000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_6000;
            _audiometria.v_Value1 = txtOI_EM_6000.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_8000;
            _audiometria.v_Value1 = txtOI_EM_8000.Text;
            _listAudiometria.Add(_audiometria);

            ShowGraphicOD();
            ShowGraphicOI();

            var chartAudiogramaOD = new MemoryStream();
            Chart1.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);
            Session["ImagenOD"] = chartAudiogramaOD.ToArray();

            var chartAudiogramaOI = new MemoryStream();
            Chart2.SaveImage(chartAudiogramaOI, ChartImageFormat.Png);
            Session["ImagenOI"] = chartAudiogramaOI.ToArray();
        }

        public class AudiometriaDataForGraphic
        {
            public string SeriesName { get; set; }
            public double? _125Hz { get; set; }
            public double? _250Hz { get; set; }
            public double? _500Hz { get; set; }
            public double? _1000Hz { get; set; }
            public double? _2000Hz { get; set; }
            public double? _3000Hz { get; set; }
            public double? _4000Hz { get; set; }
            public double? _6000Hz { get; set; }
            public double? _8000Hz { get; set; }
        }

        public class AudiometriaLine25
        {
            public string SeriesName { get; set; }
            public double? _125Hz { get; set; }
            public double? _250Hz { get; set; }
            public double? _500Hz { get; set; }
            public double? _1000Hz { get; set; }
            public double? _2000Hz { get; set; }
            public double? _3000Hz { get; set; }
            public double? _4000Hz { get; set; }
            public double? _6000Hz { get; set; }
            public double? _8000Hz { get; set; }
        }

        public class AudiometriaLine50
        {
            public string SeriesName { get; set; }
            public double? _125Hz { get; set; }
            public double? _250Hz { get; set; }
            public double? _500Hz { get; set; }
            public double? _1000Hz { get; set; }
            public double? _2000HZ { get; set; }
            public double? _3000Hz { get; set; }
            public double? _4000Hz { get; set; }
            public double? _6000Hz { get; set; }
            public double? _8000Hz { get; set; }
        }

        private void ShowGraphicOD()
        {
            Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -10;
            Chart1.ChartAreas["ChartArea1"].AxisY.Interval = 10;
            Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
            Chart1.ChartAreas["ChartArea1"].AxisY.IsReversed = true;
            double? Vacio = null;
            AudioODList = new List<AudiometriaDataForGraphic>() 
            { 
                #region Fill Entities for Graphic
		 	           
               
                 //b=(checkBox1.Checked==true ? true : false);
                new AudiometriaDataForGraphic { SeriesName = "Via Aerea OD", 
                                                _125Hz  = txtOD_VA_125.Text == string.Empty  ? Vacio :double.Parse(txtOD_VA_125.Text), 
                                                _250Hz = txtOD_VA_250.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_250.Text),
                                                _500Hz  = txtOD_VA_500.Text == string.Empty  ? Vacio :double.Parse(txtOD_VA_500.Text), 
                                                _1000Hz = txtOD_VA_1000.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_1000.Text),
                                                _2000Hz = txtOD_VA_2000.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_2000.Text),
                                                _3000Hz = txtOD_VA_3000.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_3000.Text),
                                                _4000Hz = txtOD_VA_4000.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_4000.Text),
                                                _6000Hz = txtOD_VA_6000.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_6000.Text),
                                                _8000Hz = txtOD_VA_8000.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_8000.Text)
                                              },
                new AudiometriaDataForGraphic { SeriesName = "Via Osea OD", 
                                                _125Hz  = txtOD_VO_125.Text == string.Empty  ? Vacio : double.Parse(txtOD_VO_125.Text) , 
                                                _250Hz = txtOD_VO_250.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_250.Text),
                                                _500Hz  = txtOD_VO_500.Text == string.Empty  ? Vacio : double.Parse(txtOD_VO_500.Text) , 
                                                _1000Hz = txtOD_VO_1000.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_1000.Text),
                                                _2000Hz = txtOD_VO_2000.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_2000.Text),
                                                _3000Hz = txtOD_VO_3000.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_3000.Text),
                                                _4000Hz = txtOD_VO_4000.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_4000.Text),
                                                _6000Hz = txtOD_VO_6000.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_6000.Text),
                                                _8000Hz = txtOD_VO_8000.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_8000.Text)
                                              },
            new AudiometriaDataForGraphic { SeriesName = "Enmascaramiento OD", 
                                                _125Hz = txtOD_EM_125.Text == string.Empty  ? Vacio : double.Parse(txtOD_EM_125.Text), 
                                                _250Hz = txtOD_EM_250.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_250.Text),
                                                _500Hz  = txtOD_EM_500.Text == string.Empty  ? Vacio : double.Parse(txtOD_EM_500.Text) , 
                                                _1000Hz = txtOD_EM_1000.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_1000.Text),
                                                _2000Hz = txtOD_EM_2000.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_2000.Text),
                                                _3000Hz = txtOD_EM_3000.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_3000.Text),
                                                _4000Hz = txtOD_EM_4000.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_4000.Text),
                                                _6000Hz = txtOD_EM_6000.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_6000.Text),
                                                _8000Hz = txtOD_EM_8000.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_8000.Text)
                                              },   

            new AudiometriaDataForGraphic { SeriesName = "Anacusia OD", 
                                                _125Hz = txtOD_AN_125.Text == string.Empty  ? Vacio : double.Parse(txtOD_AN_125.Text), 
                                                _250Hz = txtOD_AN_250.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_250.Text),
                                                _500Hz  = txtOD_AN_500.Text == string.Empty  ? Vacio : double.Parse(txtOD_AN_500.Text) , 
                                                _1000Hz = txtOD_AN_1000.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_1000.Text),
                                                _2000Hz = txtOD_AN_2000.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_2000.Text),
                                                _3000Hz = txtOD_AN_3000.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_3000.Text),
                                                _4000Hz = txtOD_AN_4000.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_4000.Text),
                                                _6000Hz = txtOD_AN_6000.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_6000.Text),
                                                _8000Hz = txtOD_AN_8000.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_8000.Text)
                                              }    
                #endregion     
            };

            List<AudiometriaLine25> LineList25 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 25

                new AudiometriaLine25 { SeriesName = "Line25",
                                       _125Hz = 25,
                                      _250Hz = 25, 
                                      _500Hz = 25,
                                      _1000Hz = 25,
                                      _2000Hz = 25,
                                      _3000Hz = 25,
                                      _4000Hz = 25,
                                      _6000Hz = 25,
                                      _8000Hz = 25
                                    }
                #endregion
            };

            List<AudiometriaLine25> LineList50 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 50

                new AudiometriaLine25 { SeriesName = "Line50",
                                       _125Hz = 55,
                                      _250Hz = 55,
                                      _500Hz = 55,
                                      _1000Hz = 55,
                                      _2000Hz = 55,
                                      _3000Hz = 55,
                                      _4000Hz = 55,
                                      _6000Hz = 55,
                                      _8000Hz = 55
                                    }
                #endregion
            };

            Chart1.Series.Clear();

            var propertyInfo = AudioODList.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine25 = LineList25.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine50 = LineList50.GetType().GetGenericArguments()[0].GetProperties();


            foreach (var row in AudioODList)
            {
                #region Set circulo.png / menorrojo.png

                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart1.Series.Add(seriesName);
                //Chart1.Series[seriesName].ChartType = SeriesChartType.Line;
                Chart1.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart1.Series[seriesName].BorderWidth = 2;
                Chart1.Series[seriesName].ShadowOffset = 2;

                for (int colIndex = 1; colIndex < propertyInfo.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfo[colIndex].Name;
                    object YVal = AudioODList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    if (YVal != null)
                    {
                        Chart1.Series[seriesName].Points.AddXY(columnName, double.Parse(YVal.ToString()));
                    }
                    else
                    {
                        Chart1.Series[seriesName].Points.AddXY(columnName, YVal);
                    }



                    if (YVal == null)
                    {

                    }
                    else
                    {
                        if (seriesName == "Via Aerea OD")
                        {
                            Chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = "circulo.png"; //Application.StartupPath + @"\Resources\circulo.png";
                            Chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Red;
                            Chart1.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
                        }
                        else if (seriesName == "Via Osea OD")
                        {
                            Chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = "menorrojo.png";// Application.StartupPath + @"\Resources\menorrojo.png";
                            Chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Enmascaramiento OD")
                        {
                            Chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = "CorcheteRojo.png";// Application.StartupPath + @"\Resources\CorcheteRojo.png";
                            Chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Anacusia OD")
                        {
                            Chart1.Series[seriesName].Points[colIndex - 1].MarkerImage = "flechaRoja.png";
                            Chart1.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                    }

                }

                #endregion
            }

            foreach (var row in LineList25)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart1.Series.Add(seriesName);
                Chart1.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart1.Series[seriesName].BorderWidth = 1;
                Chart1.Series[seriesName].ShadowOffset = 2;
                Chart1.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine25.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine25[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart1.Series[seriesName].Points.AddXY(columnName, 25);
                }
            }

            foreach (var row in LineList50)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart1.Series.Add(seriesName);
                Chart1.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart1.Series[seriesName].BorderWidth = 1;
                Chart1.Series[seriesName].ShadowOffset = 2;
                Chart1.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine50.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine50[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart1.Series[seriesName].Points.AddXY(columnName, 50);
                }
            }

        }

        private void ShowGraphicOI()
        {
            Chart2.ChartAreas["ChartArea1"].AxisY.Minimum = -10;
            Chart2.ChartAreas["ChartArea1"].AxisY.Interval = 10;
            Chart2.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
            Chart2.ChartAreas["ChartArea1"].AxisY.IsReversed = true;
            double? Vacio = null;
            AudioOIList = new List<AudiometriaDataForGraphic>() 
            { 
                #region Fill Entities for Graphic
		 	           
               
                 //b=(checkBox1.Checked==true ? true : false);
                new AudiometriaDataForGraphic { SeriesName = "Via Aerea OI", 
                                                _125Hz  = txtOI_VA_125.Text == string.Empty  ? Vacio :double.Parse(txtOI_VA_125.Text), 
                                                _250Hz = txtOI_VA_250.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_250.Text),
                                                _500Hz  = txtOI_VA_500.Text == string.Empty  ? Vacio :double.Parse(txtOI_VA_500.Text), 
                                                _1000Hz = txtOI_VA_1000.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_1000.Text),
                                                _2000Hz = txtOI_VA_2000.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_2000.Text),
                                                _3000Hz = txtOI_VA_3000.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_3000.Text),
                                                _4000Hz = txtOI_VA_4000.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_4000.Text),
                                                _6000Hz = txtOI_VA_6000.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_6000.Text),
                                                _8000Hz = txtOI_VA_8000.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_8000.Text)
                                              },
                new AudiometriaDataForGraphic { SeriesName = "Via Osea OI", 
                                                _125Hz  = txtOI_VO_125.Text == string.Empty  ? Vacio : double.Parse(txtOI_VO_125.Text) , 
                                                _250Hz = txtOI_VO_250.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_250.Text),
                                                _500Hz  = txtOI_VO_500.Text == string.Empty  ? Vacio : double.Parse(txtOI_VO_500.Text) , 
                                                _1000Hz = txtOI_VO_1000.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_1000.Text),
                                                _2000Hz = txtOI_VO_2000.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_2000.Text),
                                                _3000Hz = txtOI_VO_3000.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_3000.Text),
                                                _4000Hz = txtOI_VO_4000.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_4000.Text),
                                                _6000Hz = txtOI_VO_6000.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_6000.Text),
                                                _8000Hz = txtOI_VO_8000.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_8000.Text)
                                              },
            new AudiometriaDataForGraphic { SeriesName = "Enmascaramiento OI", 
                                                _125Hz = txtOI_EM_125.Text == string.Empty  ? Vacio : double.Parse(txtOI_EM_125.Text), 
                                                _250Hz = txtOI_EM_250.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_250.Text),
                                                _500Hz  = txtOI_EM_500.Text == string.Empty  ? Vacio : double.Parse(txtOI_EM_500.Text) , 
                                                _1000Hz = txtOI_EM_1000.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_1000.Text),
                                                _2000Hz = txtOI_EM_2000.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_2000.Text),
                                                _3000Hz = txtOI_EM_3000.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_3000.Text),
                                                _4000Hz = txtOI_EM_4000.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_4000.Text),
                                                _6000Hz = txtOI_EM_6000.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_6000.Text),
                                                _8000Hz = txtOI_EM_8000.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_8000.Text)
                                              }   , 

            new AudiometriaDataForGraphic { SeriesName = "Anacusia OI", 
                                                _125Hz = txtOI_AN_125.Text == string.Empty  ? Vacio : double.Parse(txtOI_AN_125.Text), 
                                                _250Hz = txtOI_AN_250.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_250.Text),
                                                _500Hz  = txtOI_AN_500.Text == string.Empty  ? Vacio : double.Parse(txtOI_AN_500.Text) , 
                                                _1000Hz = txtOI_AN_1000.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_1000.Text),
                                                _2000Hz = txtOI_AN_2000.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_2000.Text),
                                                _3000Hz = txtOI_AN_3000.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_3000.Text),
                                                _4000Hz = txtOI_AN_4000.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_4000.Text),
                                                _6000Hz = txtOI_AN_6000.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_6000.Text),
                                                _8000Hz = txtOI_AN_8000.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_8000.Text)
                                              }    
                #endregion     
            };

            List<AudiometriaLine25> LineList25 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 25

                new AudiometriaLine25 { SeriesName = "Line25",
                                       _125Hz = 25,
                                      _250Hz = 25, 
                                      _500Hz = 25,
                                      _1000Hz = 25,
                                      _2000Hz = 25,
                                      _3000Hz = 25,
                                      _4000Hz = 25,
                                      _6000Hz = 25,
                                      _8000Hz = 25
                                    }
                #endregion
            };

            List<AudiometriaLine25> LineList50 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 50

                new AudiometriaLine25 { SeriesName = "Line50",
                                       _125Hz = 55,
                                      _250Hz = 55,
                                      _500Hz = 55,
                                      _1000Hz = 55,
                                      _2000Hz = 55,
                                      _3000Hz = 55,
                                      _4000Hz = 55,
                                      _6000Hz = 55,
                                      _8000Hz = 55
                                    }
                #endregion
            };

            Chart2.Series.Clear();

            var propertyInfo = AudioODList.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine25 = LineList25.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine50 = LineList50.GetType().GetGenericArguments()[0].GetProperties();


            foreach (var row in AudioOIList)
            {
                #region Set circulo.png / menorrojo.png

                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart2.Series.Add(seriesName);
                //Chart2.Series[seriesName].ChartType = SeriesChartType.Line;
                Chart2.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart2.Series[seriesName].BorderWidth = 2;
                Chart2.Series[seriesName].ShadowOffset = 2;

                for (int colIndex = 1; colIndex < propertyInfo.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfo[colIndex].Name;
                    object YVal = AudioOIList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    if (YVal != null)
                    {
                        Chart2.Series[seriesName].Points.AddXY(columnName, double.Parse(YVal.ToString()));
                    }
                    else
                    {
                        Chart2.Series[seriesName].Points.AddXY(columnName, YVal);
                    }



                    if (YVal == null)
                    {

                    }
                    else
                    {
                        if (seriesName == "Via Aerea OI")
                        {
                            //Chart2.Series[seriesName].MarkerImage = "Face.bmp";
                            //var x = Server.MapPath("~/images/icons/circulo.bmp");
                            Chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = "cruz.png"; //Application.StartupPath + @"\Resources\circulo.png";
                            Chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Blue;
                            Chart2.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
                        }
                        else if (seriesName == "Via Osea OI")
                        {
                            Chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = "mayorazul.png";// Application.StartupPath + @"\Resources\menorrojo.png";
                            Chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Enmascaramiento OI")
                        {
                            Chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = "CorcheteAzul.png";// Application.StartupPath + @"\Resources\CorcheteRojo.png";
                            Chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Anacusia OI")
                        {
                            Chart2.Series[seriesName].Points[colIndex - 1].MarkerImage = "flechaAzul.png"; 
                            Chart2.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                    }

                }

                #endregion
            }

            foreach (var row in LineList25)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart2.Series.Add(seriesName);
                Chart2.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart2.Series[seriesName].BorderWidth = 1;
                Chart2.Series[seriesName].ShadowOffset = 2;
                Chart2.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine25.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine25[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart2.Series[seriesName].Points.AddXY(columnName, 25);
                }
            }

            foreach (var row in LineList50)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart2.Series.Add(seriesName);
                Chart2.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart2.Series[seriesName].BorderWidth = 1;
                Chart2.Series[seriesName].ShadowOffset = 2;
                Chart2.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine50.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine50[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart2.Series[seriesName].Points.AddXY(columnName, 50);
                }
            }

        }

        protected void btnGrabarAudiometriaInternacional_Click(object sender, EventArgs e)
        {
            if (ddlUsuarioGrabar.SelectedValue == "-1")
            {
                Alert.ShowInTop("Seleccionar Firma de usuario", MessageBoxIcon.Information);
                return;
            }
            SaveImagenAudiograma_CI();
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            OperationResult objOperationResult = new OperationResult();
            SearchControlAndSetValues(TabAudiometriaInternacional, Session["ServicioComponentIdAudiometria"].ToString());

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                       (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"],
                                                      ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                       Session["PersonId"].ToString(),   
                                                       Session["ServicioComponentIdAudiometria"].ToString());

            #region Dx Automaticos Triaje Internacional
            var Componentes = (List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>)Session["_serviceComponentFieldsList"];

            var GrillaDx = (List<DiagnosticRepositoryList>)Session["GrillaDx"];

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002815") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkNormoBilateral.Checked?"1":"0", "N009-MF000002815", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }

            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002816") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkNormoOD.Checked ? "1" : "0", "N009-MF000002816", "int");
                if (DXAautomatico != null)
                {
                    var Result1 = GrillaDx.Find(p => p.v_DiseasesName == DXAautomatico.v_DiseasesName);
                    if (Result1 == null)
                    {
                        l.Add(DXAautomatico);
                    }
                }
            }
            if (Componentes.Find(p => p.v_ComponentFieldsId == "N009-MF000002817") != null)
            {
                var DXAautomatico = SearchDxSugeridoOfSystem(chkNormoOI.Checked ? "1" : "0", "N009-MF000002817", "int");
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
            serviceComponentDto.v_ServiceComponentId = Session["ServicioComponentIdAudiometria"].ToString();
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, Session["ServicioComponentIdAudiometria"].ToString()).d_UpdateDate;
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

            serviceComponentDto.v_ComponentId = "N002-ME000000005";
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

            ShowGraphicOD();
            ShowGraphicOI();

            //Obtener scId
            var scId = _serviceBL.ObtenerScId(Session["ServiceId"].ToString(), "N002-ME000000005");
            //Mostrar Auditoria
            var datosAuditoria = HistoryBL.CamposAuditoria(scId);
            if (datosAuditoria != null)
            {
                txtAudiometriaAuditor.Text = datosAuditoria.UserNameAuditoriaInsert;
                txtAudiometriaAuditorInsercion.Text = datosAuditoria.FechaHoraAuditoriaInsert;
                txtAudiometriaAuditorActualizacion.Text = datosAuditoria.FechaHoraAuditoriaEdit;

                txtAudiometriaEvaluador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtAudiometriaEvaluadorInsercion.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtAudiometriaEvaluadorEvaluacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;

                txtAudiometriaInformador.Text = datosAuditoria.UserNameEvaluadorInsert;
                txtAudiometriaInformadorInserta.Text = datosAuditoria.FechaHoraEvaluadorInsert;
                txtAudiometriaInformadorActualizacion.Text = datosAuditoria.FechaHoraEvaluadorEdit;
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

        protected void aspButtonOI_Inter_Click(object sender, EventArgs e)
        {
            //VA OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_125;
            _audiometria.v_Value1 = txtOI_VA_125_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_250;
            _audiometria.v_Value1 = txtOI_VA_250_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_500;
            _audiometria.v_Value1 = txtOI_VA_500_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_1000;
            _audiometria.v_Value1 = txtOI_VA_1000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_2000;
            _audiometria.v_Value1 = txtOI_VA_2000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_3000;
            _audiometria.v_Value1 = txtOI_VA_3000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_4000;
            _audiometria.v_Value1 = txtOI_VA_4000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_6000;
            _audiometria.v_Value1 = txtOI_VA_6000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OI_8000;
            _audiometria.v_Value1 = txtOI_VA_8000_Inter.Text;
            _listAudiometria.Add(_audiometria);






            //VO OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_125;
            _audiometria.v_Value1 = txtOI_VO_125_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_250;
            _audiometria.v_Value1 = txtOI_VO_250_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_500;
            _audiometria.v_Value1 = txtOI_VO_500_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_1000;
            _audiometria.v_Value1 = txtOI_VO_1000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_2000;
            _audiometria.v_Value1 = txtOI_VO_2000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_3000;
            _audiometria.v_Value1 = txtOI_VO_3000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_4000;
            _audiometria.v_Value1 = txtOI_VO_4000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_6000;
            _audiometria.v_Value1 = txtOI_VO_6000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OI_8000;
            _audiometria.v_Value1 = txtOI_VO_8000_Inter.Text;
            _listAudiometria.Add(_audiometria);









            //EM OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_125;
            _audiometria.v_Value1 = txtOI_EM_125_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_250;
            _audiometria.v_Value1 = txtOI_EM_250_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_500;
            _audiometria.v_Value1 = txtOI_EM_500_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_1000;
            _audiometria.v_Value1 = txtOI_EM_1000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_2000;
            _audiometria.v_Value1 = txtOI_EM_2000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_3000;
            _audiometria.v_Value1 = txtOI_EM_3000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_4000;
            _audiometria.v_Value1 = txtOI_EM_4000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_6000;
            _audiometria.v_Value1 = txtOI_EM_6000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OI_8000;
            _audiometria.v_Value1 = txtOI_EM_8000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            ShowGraphicOD_Inter();
            ShowGraphicOI_Inter();

            var chartAudiogramaOD = new MemoryStream();
            Chart1_Inter.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);
            Session["ImagenOD"] = chartAudiogramaOD.ToArray();

            var chartAudiogramaOI = new MemoryStream();
            Chart2_Inter.SaveImage(chartAudiogramaOI, ChartImageFormat.Png);
            Session["ImagenOI"] = chartAudiogramaOI.ToArray();
        }

        protected void aspButtonOD_Inter_Click(object sender, EventArgs e)
        {
            //VA OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_125;
            _audiometria.v_Value1 = txtOD_VA_125_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_250;
            _audiometria.v_Value1 = txtOD_VA_250_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_500;
            _audiometria.v_Value1 = txtOD_VA_500_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_1000;
            _audiometria.v_Value1 = txtOD_VA_1000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_2000;
            _audiometria.v_Value1 = txtOD_VA_2000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_3000;
            _audiometria.v_Value1 = txtOD_VA_3000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_4000;
            _audiometria.v_Value1 = txtOD_VA_4000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_6000;
            _audiometria.v_Value1 = txtOD_VA_6000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VA_OD_8000;
            _audiometria.v_Value1 = txtOD_VA_8000_Inter.Text;
            _listAudiometria.Add(_audiometria);






            //VO OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_125;
            _audiometria.v_Value1 = txtOD_VO_125_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_250;
            _audiometria.v_Value1 = txtOD_VO_250_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_500;
            _audiometria.v_Value1 = txtOD_VO_500_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_1000;
            _audiometria.v_Value1 = txtOD_VO_1000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_2000;
            _audiometria.v_Value1 = txtOD_VO_2000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_3000;
            _audiometria.v_Value1 = txtOD_VO_3000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_4000;
            _audiometria.v_Value1 = txtOD_VO_4000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_6000;
            _audiometria.v_Value1 = txtOD_VO_6000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_VO_OD_8000;
            _audiometria.v_Value1 = txtOD_VO_8000_Inter.Text;
            _listAudiometria.Add(_audiometria);









            //EM OD
            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_125;
            _audiometria.v_Value1 = txtOD_EM_125_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_250;
            _audiometria.v_Value1 = txtOD_EM_250_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_500;
            _audiometria.v_Value1 = txtOD_EM_500_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_1000;
            _audiometria.v_Value1 = txtOD_EM_1000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_2000;
            _audiometria.v_Value1 = txtOD_EM_2000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_3000;
            _audiometria.v_Value1 = txtOD_EM_3000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_4000;
            _audiometria.v_Value1 = txtOD_EM_4000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_6000;
            _audiometria.v_Value1 = txtOD_EM_6000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            _audiometria = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();
            _audiometria.v_ComponentFieldId = Constants.txt_EM_OD_8000;
            _audiometria.v_Value1 = txtOD_EM_8000_Inter.Text;
            _listAudiometria.Add(_audiometria);

            ShowGraphicOD_Inter();
            ShowGraphicOI_Inter();

            var chartAudiogramaOD = new MemoryStream();
            Chart1_Inter.SaveImage(chartAudiogramaOD, ChartImageFormat.Png);
            Session["ImagenOD"] = chartAudiogramaOD.ToArray();

            var chartAudiogramaOI = new MemoryStream();
            Chart2_Inter.SaveImage(chartAudiogramaOI, ChartImageFormat.Png);
            Session["ImagenOI"] = chartAudiogramaOI.ToArray();
        }

        private void ShowGraphicOD_Inter()
        {
            Chart1_Inter.ChartAreas["ChartArea1_Inter"].AxisY.Minimum = -10;
            Chart1_Inter.ChartAreas["ChartArea1_Inter"].AxisY.Interval = 10;
            Chart1_Inter.ChartAreas["ChartArea1_Inter"].AxisY.Maximum = 100;
            Chart1_Inter.ChartAreas["ChartArea1_Inter"].AxisY.IsReversed = true;
            double? Vacio = null;
            AudioODList = new List<AudiometriaDataForGraphic>() 
            { 
                #region Fill Entities for Graphic
		 	           
               
                 //b=(checkBox1.Checked==true ? true : false);
                new AudiometriaDataForGraphic { SeriesName = "Via Aerea OD", 
                                                _125Hz  = txtOD_VA_125_Inter.Text == string.Empty  ? Vacio :double.Parse(txtOD_VA_125_Inter.Text), 
                                                _250Hz = txtOD_VA_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_250_Inter.Text),
                                                _500Hz  = txtOD_VA_500_Inter.Text == string.Empty  ? Vacio :double.Parse(txtOD_VA_500_Inter.Text), 
                                                _1000Hz = txtOD_VA_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_1000_Inter.Text),
                                                _2000Hz = txtOD_VA_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_2000_Inter.Text),
                                                _3000Hz = txtOD_VA_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_3000_Inter.Text),
                                                _4000Hz = txtOD_VA_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_4000_Inter.Text),
                                                _6000Hz = txtOD_VA_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_6000_Inter.Text),
                                                _8000Hz = txtOD_VA_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VA_8000_Inter.Text)
                                              },
                new AudiometriaDataForGraphic { SeriesName = "Via Osea OD", 
                                                _125Hz  = txtOD_VO_125_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOD_VO_125_Inter.Text) , 
                                                _250Hz = txtOD_VO_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_250_Inter.Text),
                                                _500Hz  = txtOD_VO_500_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOD_VO_500_Inter.Text) , 
                                                _1000Hz = txtOD_VO_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_1000_Inter.Text),
                                                _2000Hz = txtOD_VO_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_2000_Inter.Text),
                                                _3000Hz = txtOD_VO_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_3000_Inter.Text),
                                                _4000Hz = txtOD_VO_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_4000_Inter.Text),
                                                _6000Hz = txtOD_VO_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_6000_Inter.Text),
                                                _8000Hz = txtOD_VO_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_VO_8000_Inter.Text)
                                              },
            new AudiometriaDataForGraphic { SeriesName = "Enmascaramiento OD", 
                                                _125Hz = txtOD_EM_125_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOD_EM_125_Inter.Text), 
                                                _250Hz = txtOD_EM_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_250_Inter.Text),
                                                _500Hz  = txtOD_EM_500_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOD_EM_500_Inter.Text) , 
                                                _1000Hz = txtOD_EM_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_1000_Inter.Text),
                                                _2000Hz = txtOD_EM_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_2000_Inter.Text),
                                                _3000Hz = txtOD_EM_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_3000_Inter.Text),
                                                _4000Hz = txtOD_EM_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_4000_Inter.Text),
                                                _6000Hz = txtOD_EM_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_6000_Inter.Text),
                                                _8000Hz = txtOD_EM_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_EM_8000_Inter.Text)
                                              },   

            //new AudiometriaDataForGraphic { SeriesName = "Anacusia OD", 
            //                                    _125Hz = txtOD_AN_125_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOD_AN_125_Inter.Text), 
            //                                    _250Hz = txtOD_AN_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_250_Inter.Text),
            //                                    _500Hz  = txtOD_AN_500_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOD_AN_500_Inter.Text) , 
            //                                    _1000Hz = txtOD_AN_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_1000_Inter.Text),
            //                                    _2000Hz = txtOD_AN_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_2000_Inter.Text),
            //                                    _3000Hz = txtOD_AN_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_3000_Inter.Text),
            //                                    _4000Hz = txtOD_AN_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_4000_Inter.Text),
            //                                    _6000Hz = txtOD_AN_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_6000_Inter.Text),
            //                                    _8000Hz = txtOD_AN_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOD_AN_8000_Inter.Text)
            //                                  }    
                #endregion     
            };

            List<AudiometriaLine25> LineList25 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 25

                new AudiometriaLine25 { SeriesName = "Line25",
                                       _125Hz = 25,
                                      _250Hz = 25, 
                                      _500Hz = 25,
                                      _1000Hz = 25,
                                      _2000Hz = 25,
                                      _3000Hz = 25,
                                      _4000Hz = 25,
                                      _6000Hz = 25,
                                      _8000Hz = 25
                                    }
                #endregion
            };

            List<AudiometriaLine25> LineList50 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 50

                new AudiometriaLine25 { SeriesName = "Line50",
                                       _125Hz = 55,
                                      _250Hz = 55,
                                      _500Hz = 55,
                                      _1000Hz = 55,
                                      _2000Hz = 55,
                                      _3000Hz = 55,
                                      _4000Hz = 55,
                                      _6000Hz = 55,
                                      _8000Hz = 55
                                    }
                #endregion
            };

            Chart1_Inter.Series.Clear();

            var propertyInfo = AudioODList.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine25 = LineList25.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine50 = LineList50.GetType().GetGenericArguments()[0].GetProperties();


            foreach (var row in AudioODList)
            {
                #region Set circulo.png / menorrojo.png

                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart1_Inter.Series.Add(seriesName);
                //Chart1_Inter.Series[seriesName].ChartType = SeriesChartType.Line;
                Chart1_Inter.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart1_Inter.Series[seriesName].BorderWidth = 2;
                Chart1_Inter.Series[seriesName].ShadowOffset = 2;

                for (int colIndex = 1; colIndex < propertyInfo.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfo[colIndex].Name;
                    object YVal = AudioODList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    if (YVal != null)
                    {
                        Chart1_Inter.Series[seriesName].Points.AddXY(columnName, double.Parse(YVal.ToString()));
                    }
                    else
                    {
                        Chart1_Inter.Series[seriesName].Points.AddXY(columnName, YVal);
                    }



                    if (YVal == null)
                    {

                    }
                    else
                    {
                        if (seriesName == "Via Aerea OD")
                        {
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "circulo.png"; //Application.StartupPath + @"\Resources\circulo.png";
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Red;
                            Chart1_Inter.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
                        }
                        else if (seriesName == "Via Osea OD")
                        {
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "menorrojo.png";// Application.StartupPath + @"\Resources\menorrojo.png";
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Enmascaramiento OD")
                        {
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "CorcheteRojo.png";// Application.StartupPath + @"\Resources\CorcheteRojo.png";
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Anacusia OD")
                        {
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "flechaRoja.png";
                            Chart1_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                    }

                }

                #endregion
            }

            foreach (var row in LineList25)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart1_Inter.Series.Add(seriesName);
                Chart1_Inter.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart1_Inter.Series[seriesName].BorderWidth = 1;
                Chart1_Inter.Series[seriesName].ShadowOffset = 2;
                Chart1_Inter.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine25.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine25[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart1_Inter.Series[seriesName].Points.AddXY(columnName, 25);
                }
            }

            foreach (var row in LineList50)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart1_Inter.Series.Add(seriesName);
                Chart1_Inter.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart1_Inter.Series[seriesName].BorderWidth = 1;
                Chart1_Inter.Series[seriesName].ShadowOffset = 2;
                Chart1_Inter.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine50.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine50[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart1_Inter.Series[seriesName].Points.AddXY(columnName, 50);
                }
            }

        }

        private void ShowGraphicOI_Inter()
        {
            Chart2_Inter.ChartAreas["ChartArea1_Inter"].AxisY.Minimum = -10;
            Chart2_Inter.ChartAreas["ChartArea1_Inter"].AxisY.Interval = 10;
            Chart2_Inter.ChartAreas["ChartArea1_Inter"].AxisY.Maximum = 100;
            Chart2_Inter.ChartAreas["ChartArea1_Inter"].AxisY.IsReversed = true;
            double? Vacio = null;
            AudioOIList = new List<AudiometriaDataForGraphic>() 
            { 
                #region Fill Entities for Graphic
		 	           
               
                 //b=(checkBox1.Checked==true ? true : false);
                new AudiometriaDataForGraphic { SeriesName = "Via Aerea OI", 
                                                _125Hz  = txtOI_VA_125_Inter.Text == string.Empty  ? Vacio :double.Parse(txtOI_VA_125_Inter.Text), 
                                                _250Hz = txtOI_VA_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_250_Inter.Text),
                                                _500Hz  = txtOI_VA_500_Inter.Text == string.Empty  ? Vacio :double.Parse(txtOI_VA_500_Inter.Text), 
                                                _1000Hz = txtOI_VA_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_1000_Inter.Text),
                                                _2000Hz = txtOI_VA_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_2000_Inter.Text),
                                                _3000Hz = txtOI_VA_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_3000_Inter.Text),
                                                _4000Hz = txtOI_VA_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_4000_Inter.Text),
                                                _6000Hz = txtOI_VA_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_6000_Inter.Text),
                                                _8000Hz = txtOI_VA_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VA_8000_Inter.Text)
                                              },
                new AudiometriaDataForGraphic { SeriesName = "Via Osea OI", 
                                                _125Hz  = txtOI_VO_125_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOI_VO_125_Inter.Text) , 
                                                _250Hz = txtOI_VO_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_250_Inter.Text),
                                                _500Hz  = txtOI_VO_500_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOI_VO_500_Inter.Text) , 
                                                _1000Hz = txtOI_VO_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_1000_Inter.Text),
                                                _2000Hz = txtOI_VO_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_2000_Inter.Text),
                                                _3000Hz = txtOI_VO_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_3000_Inter.Text),
                                                _4000Hz = txtOI_VO_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_4000_Inter.Text),
                                                _6000Hz = txtOI_VO_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_6000_Inter.Text),
                                                _8000Hz = txtOI_VO_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_VO_8000_Inter.Text)
                                              },
            new AudiometriaDataForGraphic { SeriesName = "Enmascaramiento OI", 
                                                _125Hz = txtOI_EM_125_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOI_EM_125_Inter.Text), 
                                                _250Hz = txtOI_EM_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_250_Inter.Text),
                                                _500Hz  = txtOI_EM_500_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOI_EM_500_Inter.Text) , 
                                                _1000Hz = txtOI_EM_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_1000_Inter.Text),
                                                _2000Hz = txtOI_EM_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_2000_Inter.Text),
                                                _3000Hz = txtOI_EM_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_3000_Inter.Text),
                                                _4000Hz = txtOI_EM_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_4000_Inter.Text),
                                                _6000Hz = txtOI_EM_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_6000_Inter.Text),
                                                _8000Hz = txtOI_EM_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_EM_8000_Inter.Text)
                                              }   , 

            //new AudiometriaDataForGraphic { SeriesName = "Anacusia OI", 
            //                                    _125Hz = txtOI_AN_125_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOI_AN_125_Inter.Text), 
            //                                    _250Hz = txtOI_AN_250_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_250_Inter.Text),
            //                                    _500Hz  = txtOI_AN_500_Inter.Text == string.Empty  ? Vacio : double.Parse(txtOI_AN_500_Inter.Text) , 
            //                                    _1000Hz = txtOI_AN_1000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_1000_Inter.Text),
            //                                    _2000Hz = txtOI_AN_2000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_2000_Inter.Text),
            //                                    _3000Hz = txtOI_AN_3000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_3000_Inter.Text),
            //                                    _4000Hz = txtOI_AN_4000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_4000_Inter.Text),
            //                                    _6000Hz = txtOI_AN_6000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_6000_Inter.Text),
            //                                    _8000Hz = txtOI_AN_8000_Inter.Text == string.Empty ? Vacio : double.Parse(txtOI_AN_8000_Inter.Text)
            //                                  }    
                #endregion     
            };

            List<AudiometriaLine25> LineList25 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 25

                new AudiometriaLine25 { SeriesName = "Line25",
                                       _125Hz = 25,
                                      _250Hz = 25, 
                                      _500Hz = 25,
                                      _1000Hz = 25,
                                      _2000Hz = 25,
                                      _3000Hz = 25,
                                      _4000Hz = 25,
                                      _6000Hz = 25,
                                      _8000Hz = 25
                                    }
                #endregion
            };

            List<AudiometriaLine25> LineList50 = new List<AudiometriaLine25>()
            {
                #region Fill Entities for Graphic Line 50

                new AudiometriaLine25 { SeriesName = "Line50",
                                       _125Hz = 55,
                                      _250Hz = 55,
                                      _500Hz = 55,
                                      _1000Hz = 55,
                                      _2000Hz = 55,
                                      _3000Hz = 55,
                                      _4000Hz = 55,
                                      _6000Hz = 55,
                                      _8000Hz = 55
                                    }
                #endregion
            };

            Chart2_Inter.Series.Clear();

            var propertyInfo = AudioODList.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine25 = LineList25.GetType().GetGenericArguments()[0].GetProperties();
            var propertyInfoLine50 = LineList50.GetType().GetGenericArguments()[0].GetProperties();


            foreach (var row in AudioOIList)
            {
                #region Set circulo.png / menorrojo.png

                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart2_Inter.Series.Add(seriesName);
                //Chart2_Inter.Series[seriesName].ChartType = SeriesChartType.Line;
                Chart2_Inter.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart2_Inter.Series[seriesName].BorderWidth = 2;
                Chart2_Inter.Series[seriesName].ShadowOffset = 2;

                for (int colIndex = 1; colIndex < propertyInfo.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfo[colIndex].Name;
                    object YVal = AudioOIList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    if (YVal != null)
                    {
                        Chart2_Inter.Series[seriesName].Points.AddXY(columnName, double.Parse(YVal.ToString()));
                    }
                    else
                    {
                        Chart2_Inter.Series[seriesName].Points.AddXY(columnName, YVal);
                    }



                    if (YVal == null)
                    {

                    }
                    else
                    {
                        if (seriesName == "Via Aerea OI")
                        {
                            //Chart2_Inter.Series[seriesName].MarkerImage = "Face.bmp";
                            //var x = Server.MapPath("~/images/icons/circulo.bmp");
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "cruz.png"; //Application.StartupPath + @"\Resources\circulo.png";
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Blue;
                            Chart2_Inter.Series[seriesName].BorderDashStyle = ChartDashStyle.Dash;
                        }
                        else if (seriesName == "Via Osea OI")
                        {
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "mayorazul.png";// Application.StartupPath + @"\Resources\menorrojo.png";
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Enmascaramiento OI")
                        {
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "CorcheteAzul.png";// Application.StartupPath + @"\Resources\CorcheteRojo.png";
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                        else if (seriesName == "Anacusia OI")
                        {
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].MarkerImage = "flechaAzul.png";
                            Chart2_Inter.Series[seriesName].Points[colIndex - 1].Color = Color.Transparent;
                        }
                    }

                }

                #endregion
            }

            foreach (var row in LineList25)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart2_Inter.Series.Add(seriesName);
                Chart2_Inter.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart2_Inter.Series[seriesName].BorderWidth = 1;
                Chart2_Inter.Series[seriesName].ShadowOffset = 2;
                Chart2_Inter.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine25.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine25[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart2_Inter.Series[seriesName].Points.AddXY(columnName, 25);
                }
            }

            foreach (var row in LineList50)
            {
                // for each Row, add a new series
                string seriesName = row.SeriesName;
                Chart2_Inter.Series.Add(seriesName);
                Chart2_Inter.Series[seriesName].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Line", true);
                Chart2_Inter.Series[seriesName].BorderWidth = 1;
                Chart2_Inter.Series[seriesName].ShadowOffset = 2;
                Chart2_Inter.Series[seriesName].Color = Color.Black;
                for (int colIndex = 1; colIndex < propertyInfoLine50.Length; colIndex++)
                {
                    // for each column (column 1 and onward), add the value as a point
                    string columnName = propertyInfoLine50[colIndex].Name;
                    //object YVal = AudioViaAereaList.GetType().GetGenericArguments()[0].GetProperty(columnName).GetValue(row, null);

                    Chart2_Inter.Series[seriesName].Points.AddXY(columnName, 50);
                }
            }

        }

        protected void lnkAudiometriaCI_Click(object sender, EventArgs e)
        {
            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            MergeExPDF _mergeExPDF = new MergeExPDF();
            string path;
            path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000005";

            GenerateAudimetriaCI(_ruta, Session["ServiceId"].ToString());

            Download(Session["ServiceId"].ToString() + "-N002-ME000000005.pdf", path + ".pdf");
        }

        private void GenerateAudimetriaCI(string _ruta, string p)
        {
            var serviceBL = new ServiceBL();
            var dsAudiometria = new DataSet();
            var dxList_CI = serviceBL.GetDiagnosticRepositoryByComponent(p, "N002-ME000000005");
            if (dxList_CI.Count == 0)
            {
                DiagnosticRepositoryList oDiagnosticRepositoryList = new DiagnosticRepositoryList();
                List<DiagnosticRepositoryList> Lista = new List<DiagnosticRepositoryList>();
                oDiagnosticRepositoryList.v_ServiceId = "Sin Id";
                oDiagnosticRepositoryList.v_DiseasesName = "Sin Alteración";
                oDiagnosticRepositoryList.v_DiagnosticRepositoryId = "Sin Id";
                Lista.Add(oDiagnosticRepositoryList);
                var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
                dtDx.TableName = "dtDiagnostic";
                dsAudiometria.Tables.Add(dtDx);
            }
            else
            {
                List<DiagnosticRepositoryList> ListaDxsAudio = new List<DiagnosticRepositoryList>();
                DiagnosticRepositoryList oDiagnosticRepositoryList;
                foreach (var item in dxList_CI)
                {
                    oDiagnosticRepositoryList = new DiagnosticRepositoryList();

                    oDiagnosticRepositoryList.v_DiseasesName = item.v_DiseasesName;
                    oDiagnosticRepositoryList.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                    oDiagnosticRepositoryList.v_ServiceId = item.v_ServiceId;
                    ListaDxsAudio.Add(oDiagnosticRepositoryList);
                }


                var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ListaDxsAudio);
                dtDx.TableName = "dtDiagnostic";
                dsAudiometria.Tables.Add(dtDx);
            }


            var recom_CI = dxList_CI.SelectMany(s1 => s1.Recomendations).ToList();
            if (recom_CI.Count == 0)
            {
                Sigesoft.Node.WinClient.BE.RecomendationList oRecomendationList = new Sigesoft.Node.WinClient.BE.RecomendationList();
                List<Sigesoft.Node.WinClient.BE.RecomendationList> Lista = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();

                oRecomendationList.v_ServiceId = "Sin Id";
                oRecomendationList.v_RecommendationName = "Sin Recomendaciones";
                oRecomendationList.v_DiagnosticRepositoryId = "Sin Id";
                Lista.Add(oRecomendationList);
                var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
                dtReco.TableName = "dtRecomendation";
                dsAudiometria.Tables.Add(dtReco);

            }
            else
            {
                var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom_CI);
                dtReco.TableName = "dtRecomendation";
                dsAudiometria.Tables.Add(dtReco);
            }


            //-------******************************************************************************************

            var audioUserControlList_CI = serviceBL.ReportAudiometriaUserControl(p, "N002-ME000000005");
            var audioCabeceraList_CI = serviceBL.ReportAudiometria_CI(p, "N002-ME000000005");
            var dtAudiometriaUserControl_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList_CI);
            var dtCabecera_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList_CI);

            dtCabecera_CI.TableName = "dtAudiometria";
            dtAudiometriaUserControl_CI.TableName = "dtAudiometriaUserControl";

            dsAudiometria.Tables.Add(dtCabecera_CI);
            dsAudiometria.Tables.Add(dtAudiometriaUserControl_CI);
                     
            rp = new AdministradorServicios.crFichaAudiometria_inter();
         
            rp.SetDataSource(dsAudiometria);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + "N002-ME000000005" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

        protected void winEdit1_Close(object sender, WindowCloseEventArgs e)
        {

        }

    }
}