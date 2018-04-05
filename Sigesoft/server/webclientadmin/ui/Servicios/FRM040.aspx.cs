using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Sigesoft.Server.WebClientAdmin.UI.Servicios
{
    public partial class FRM040 : System.Web.UI.Page
    {
        ServiceBL _serviceBL = new ServiceBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        private Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList _tmpTotalDiagnostic = null;

        #region CARGA INICIAL
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                TabRx.Hidden = true;
                TabOIT.Hidden = true;
                TabLaboratorio.Hidden = true;


                TabAnexo312.Visible = false;
                TabOsteomuscular.Visible = false;
                TabAudiometria.Visible = false;
                TabPsicologia.Visible = false;
                TabDermatologico.Visible = false;
                TabEspirometria.Visible = false;
                TabOftalmologia.Visible = false;
                Tab7D.Visible = false;
                TabAltura18.Visible = false;
                TabSintomaticoRespiratorio.Visible = false;
              

                int RoleId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.Value.ToString());
                var ComponentesPermisoLectura = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId(9, RoleId).FindAll(p => p.i_Read == 1);
                List<string> ListaComponentesPermisoLectura = new List<string>();
                foreach (var item in ComponentesPermisoLectura)
                {
                    ListaComponentesPermisoLectura.Add(item.v_ComponentId);
                }
                Session["ComponentesPermisoLectura"] = ListaComponentesPermisoLectura;

                btnNewDiagnosticos.OnClientClick = WindowAddDX.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDX.GetShowReference("../Auditar/FRM033C.aspx?Mode=New");
                btnAddRecomendacion.OnClientClick = winEditReco.GetSaveStateReference(hfRefresh.ClientID) + winEditReco.GetShowReference("../Auditar/FRM033B.aspx?Mode=New");
                btnAddRestriccion.OnClientClick = winEditRestri.GetSaveStateReference(hfRefresh.ClientID) + winEditRestri.GetShowReference("../Auditar/FRM033E.aspx?Mode=New");
                btnNewDiagnosticosFrecuente.OnClientClick = WindowAddDXFrecuente.GetSaveStateReference(hfRefresh.ClientID) + WindowAddDXFrecuente.GetShowReference("../Auditar/FRM033G.aspx?Mode=New");
                btnReporteOftalmologia.OnClientClick = WindowReporte.GetSaveStateReference(hfRefresh.ClientID) + WindowReporte.GetShowReference("../Reportes/FRMVisorReporte.aspx");
                btnNewExamenes.OnClientClick = winEdit3.GetSaveStateReference(hfRefresh.ClientID) + winEdit3.GetShowReference("../ExternalUser/FRM031E.aspx");


                TabAnexo312.Attributes.Add("Tag", "N002-ME000000022");
                TabOsteomuscular.Attributes.Add("Tag", "N002-ME000000046");
                TabAudiometria.Attributes.Add("Tag", "N002-ME000000005");
                TabPsicologia.Attributes.Add("Tag", "N002-ME000000033");
                TabDermatologico.Attributes.Add("Tag", "N009-ME000000044");
                TabEspirometria.Attributes.Add("Tag", "N002-ME000000031");
                TabOftalmologia.Attributes.Add("Tag", "N002-ME000000028");
                TabRx.Attributes.Add("Tag", "N002-ME000000032");
                Tab7D.Attributes.Add("Tag", "N002-ME000000045");
                TabOIT.Attributes.Add("Tag", "N009-ME000000062");
                TabAltura18.Attributes.Add("Tag", "N009-ME000000015");
                TabSintomaticoRespiratorio.Attributes.Add("Tag", "N009-ME000000118");
                TabLaboratorio.Attributes.Add("Tag", "N009-ME000000002");


                ddlAU_Condicion.Attributes.Add("Tag", "N009-MF000001378");
                txtAU_Observaciones.Attributes.Add("Tag", "N002-MF000000178");

                chkSupuracion.Attributes.Add("Tag", "N009-MF000000089");


                txtAnioServicioMilitar.Attributes.Add("Tag", "N002-MF000001876");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001876") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001876").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaServicioMilitar.Attributes.Add("Tag", "N002-MF000001879");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001879") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001879").ServiceComponentFieldValues[0].v_Value1;

                txtAnioDeportesAereos.Attributes.Add("Tag", "N002-MF000001880");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001880") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001880").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaDeportesAereos.Attributes.Add("Tag", "N002-MF000001881");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001881") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001881").ServiceComponentFieldValues[0].v_Value1;

                txtAnioDeporteSubmarino.Attributes.Add("Tag", "N002-MF000001882");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001882") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001882").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaDeporteSubmarino.Attributes.Add("Tag", "N002-MF000001883");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001883") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001883").ServiceComponentFieldValues[0].v_Value1;

                txtAnioManipulacionArmas.Attributes.Add("Tag", "N002-MF000001884");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001884") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001884").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaManipulacionArmas.Attributes.Add("Tag", "N002-MF000001885");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001885") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001885").ServiceComponentFieldValues[0].v_Value1;

                txtAnioExposicionMusica.Attributes.Add("Tag", "N002-MF000001886");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001886") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001886").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaExposicionMusica.Attributes.Add("Tag", "N002-MF000001887");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001887") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001887").ServiceComponentFieldValues[0].v_Value1;

                txtAnioUsoaudifnos.Attributes.Add("Tag", "N002-MF000001888");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001888") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001888").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaUsoaudifnos.Attributes.Add("Tag", "N002-MF000001889");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001889") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001889").ServiceComponentFieldValues[0].v_Value1;

                txtAnioMotociclismo.Attributes.Add("Tag", "N002-MF000001890");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001890") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001890").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaMotociclismo.Attributes.Add("Tag", "N002-MF000001891");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001891") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001891").ServiceComponentFieldValues[0].v_Value1;

                txtAnioOtro.Attributes.Add("Tag", "N002-MF000001892");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001892") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001892").ServiceComponentFieldValues[0].v_Value1;
                txtFrecuenciaOtro.Attributes.Add("Tag", "N002-MF000001893");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001893") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001893").ServiceComponentFieldValues[0].v_Value1;


                txtIntensidad.Attributes.Add("Tag", "N002-MF000000179");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000000179") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000000179").ServiceComponentFieldValues[0].v_Value1;
                txtHoras.Attributes.Add("Tag", "N002-MF000001894");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001894") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001894").ServiceComponentFieldValues[0].v_Value1;

                txtActualAnio.Attributes.Add("Tag", "N009-MF000001933");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001933") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001933").ServiceComponentFieldValues[0].v_Value1;
                txtActualOD.Attributes.Add("Tag", "N009-MF000001934");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001934") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001934").ServiceComponentFieldValues[0].v_Value1;
                txtActualOI.Attributes.Add("Tag", "N009-MF000001935");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001935") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001935").ServiceComponentFieldValues[0].v_Value1;
                txtMenoscaboAuditivo.Attributes.Add("Tag", "N009-MF000001979");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001979") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001979").ServiceComponentFieldValues[0].v_Value1;

                txtCalibracion.Attributes.Add("Tag", "N009-MF000000084");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000084") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000084").ServiceComponentFieldValues[0].v_Value1;
                txtMarca.Attributes.Add("Tag", "N009-MF000000082");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000082") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000082").ServiceComponentFieldValues[0].v_Value1;
                txtModelo.Attributes.Add("Tag", "N009-MF000000083");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000083") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000083").ServiceComponentFieldValues[0].v_Value1;
                txtNivelRuidoAmbiental.Attributes.Add("Tag", "N009-MF000001874");//.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001874") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001874").ServiceComponentFieldValues[0].v_Value1;


                ddlTipoRuido.Attributes.Add("Tag", "N009-MF000001307");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001307") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001307").ServiceComponentFieldValues[0].v_Value1;
                ddlHorasPorDia.Attributes.Add("Tag", "N009-MF000001308");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001308") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001308").ServiceComponentFieldValues[0].v_Value1;
                ddlTapones.Attributes.Add("Tag", "N009-MF000001309");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001309") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001309").ServiceComponentFieldValues[0].v_Value1;
                ddlOrejeras.Attributes.Add("Tag", "N009-MF000001310");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001310") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001310").ServiceComponentFieldValues[0].v_Value1;
                ddlAmbos.Attributes.Add("Tag", "N009-MF000001895");//.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001895") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001895").ServiceComponentFieldValues[0].v_Value1;


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

                chkVertigo.Attributes.Add("Tag", "N009-MF000000090");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000090") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000090").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkOtitis.Attributes.Add("Tag", "N009-MF000000091");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000091") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000091").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkParatodiditis.Attributes.Add("Tag", "N009-MF000000092");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000092") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000092").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkMeningitis.Attributes.Add("Tag", "N009-MF000000093");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000093") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000093").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkGolpesCefalicos.Attributes.Add("Tag", "N009-MF000000094");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000094") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000094").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkParalisisFacial.Attributes.Add("Tag", "N009-MF000000095");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000095") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000095").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkTTOANTITBC.Attributes.Add("Tag", "N009-MF000000096");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000096") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000096").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkTTOOtotoxicos.Attributes.Add("Tag", "N009-MF000000097");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000097") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000097").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkConsumoMedicamento.Attributes.Add("Tag", "N009-MF000000098");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000098") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000098").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkExposicionSolventes.Attributes.Add("Tag", "N009-MF000000099");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000099") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000099").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;

                chkRuidoExtra.Attributes.Add("Tag", "N009-MF000000100");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000100") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000100").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkRuidoLaboral.Attributes.Add("Tag", "N009-MF000000101");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000101") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000101").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkServicioMilitar.Attributes.Add("Tag", "N009-MF000001299");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001299") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001299").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkDeportesAereos.Attributes.Add("Tag", "N009-MF000001300");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001300") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001300").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkDeportesSubmarinos.Attributes.Add("Tag", "N009-MF000001301");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001301") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001301").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkManipulacionArmas.Attributes.Add("Tag", "N009-MF000001302");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001302") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001302").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkExposicionMusica.Attributes.Add("Tag", "N009-MF000001303");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001303") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001303").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkUsoAudifonos.Attributes.Add("Tag", "N009-MF000001304");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001304") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001304").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkMotociclismo.Attributes.Add("Tag", "N009-MF000001305");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001305") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001305").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
                chkOtro.Attributes.Add("Tag", "N009-MF000001306");//.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001306") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001306").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;

                //Setear OsteoMuscular

                ddlNuca1.Attributes.Add("Tag", "N009-MF000001471");
                ddlNuca2.Attributes.Add("Tag", "N009-MF000001472");
                ddlNuca3.Attributes.Add("Tag", "N009-MF000001475");
                //Hombro
                ddlHombroDerecho1.Attributes.Add("Tag", "N009-MF000001473");
                ddlHombroDerecho2.Attributes.Add("Tag", "N009-MF000001477");
                ddlHombroDerecho3.Attributes.Add("Tag", "N009-MF000001478");

                ddlHombroIzquierdo1.Attributes.Add("Tag", "N009-MF000001474");
                ddlHombroIzquierdo2.Attributes.Add("Tag", "N009-MF000001476");
                ddlHombroIzquierdo3.Attributes.Add("Tag", "N009-MF000001479");

                ddlAmbosHombros1.Attributes.Add("Tag", "N009-MF000001480");
                ddlAmbosHombros2.Attributes.Add("Tag", "N009-MF000001481");
                ddlAmbosHombros3.Attributes.Add("Tag", "N009-MF000001521");

                //Codo
                ddlCodoDerecho1.Attributes.Add("Tag", "N009-MF000001520");
                ddlCodoDerecho2.Attributes.Add("Tag", "N009-MF000001522");
                ddlCodoDerecho3.Attributes.Add("Tag", "N009-MF000001525");

                ddlCodoIzquierdo1.Attributes.Add("Tag", "N009-MF000001519");
                ddlCodoIzquierdo2.Attributes.Add("Tag", "N009-MF000001523");
                ddlCodoIzquierdo3.Attributes.Add("Tag", "N009-MF000001524");

                ddlAmboscodos1.Attributes.Add("Tag", "N009-MF000001526");
                ddlAmboscodos2.Attributes.Add("Tag", "N009-MF000001527");
                ddlAmboscodos3.Attributes.Add("Tag", "N009-MF000001530");

                //Muñecas
                ddlManosDerecha1.Attributes.Add("Tag", "N009-MF000001528");
                ddlManosDerecha2.Attributes.Add("Tag", "N009-MF000001532");
                ddlManosDerecha3.Attributes.Add("Tag", "N009-MF000001533");

                ddlManosIzquierda1.Attributes.Add("Tag", "N009-MF000001529");
                ddlManosIzquierda2.Attributes.Add("Tag", "N009-MF000001531");
                ddlManosIzquierda3.Attributes.Add("Tag", "N009-MF000001534");

                ddlAmbasManos1.Attributes.Add("Tag", "N009-MF000001535");
                ddlAmbasManos2.Attributes.Add("Tag", "N009-MF000001536");
                ddlAmbasManos3.Attributes.Add("Tag", "N009-MF000001485");

                //Dorsal
                ddlColumnadorsal1.Attributes.Add("Tag", "N009-MF000001486");
                ddlColumnadorsal2.Attributes.Add("Tag", "N009-MF000001487");
                ddlColumnadorsal3.Attributes.Add("Tag", "N009-MF000001482");

                //Lumbar
                ddlColumnaLumbar1.Attributes.Add("Tag", "N009-MF000001491");
                ddlColumnaLumbar2.Attributes.Add("Tag", "N009-MF000001492");
                ddlColumnaLumbar3.Attributes.Add("Tag", "N009-MF000001493");

                //Caderas
                ddlCaderaDerecha1.Attributes.Add("Tag", "N009-MF000001489");
                ddlCaderaDerecha2.Attributes.Add("Tag", "N009-MF000001483");
                ddlCaderaDerecha3.Attributes.Add("Tag", "N009-MF000001490");

                ddlCaderaIzquierda1.Attributes.Add("Tag", "N009-MF000001970");
                ddlCaderaIzquierda2.Attributes.Add("Tag", "N009-MF000001971");
                ddlCaderaIzquierda3.Attributes.Add("Tag", "N009-MF000001972");

                //Rodillas
                ddlRodillaDerecha1.Attributes.Add("Tag", "N009-MF000001488");
                ddlRodillaDerecha2.Attributes.Add("Tag", "N009-MF000001484");
                ddlRodillaDerecha3.Attributes.Add("Tag", "N009-MF000001496");

                ddlRodillaIzquierda1.Attributes.Add("Tag", "N009-MF000001497");
                ddlRodillaIzquierda2.Attributes.Add("Tag", "N009-MF000001498");
                ddlRodillaIzquierda3.Attributes.Add("Tag", "N009-MF000001500");

                //Tobillos
                ddlTobillosDerecho1.Attributes.Add("Tag", "N009-MF000001973");
                ddlTobillosDerecho2.Attributes.Add("Tag", "N009-MF000001974");
                ddlTobillosDerecho3.Attributes.Add("Tag", "N009-MF000001975");

                ddlTobillosIzquierdo1.Attributes.Add("Tag", "N009-MF000001976");
                ddlTobillosIzquierdo2.Attributes.Add("Tag", "N009-MF000001977");
                ddlTobillosIzquierdo3.Attributes.Add("Tag", "N009-MF000001978");

                ///
                ddlCervical.Attributes.Add("Tag", "N009-MF000001501");
                ddlDorsalEjeLateral.Attributes.Add("Tag", "N009-MF000001518");
                ddlDorsal.Attributes.Add("Tag", "N009-MF000001504");
                ddlLumbarEjeLateral.Attributes.Add("Tag", "N009-MF000001494");
                ddlLumbar.Attributes.Add("Tag", "N009-MF000001505");

                ///MOVILIDAD DOLOR
                ddlCervicalFlexion.Attributes.Add("Tag", "N009-MF000001502");
                ddlCervicalExtension.Attributes.Add("Tag", "N009-MF000001506");
                ddlCervicalLatDere.Attributes.Add("Tag", "N009-MF000001503");
                ddlCervicalLatIzq.Attributes.Add("Tag", "N009-MF000001470");
                ddlCervicalRotaDere.Attributes.Add("Tag", "N009-MF000001499");
                ddlCervicalRotaIzq.Attributes.Add("Tag", "N009-MF000001495");
                ddlCervicalIrradiacion.Attributes.Add("Tag", "N009-MF000001508");

                ddlDorsoFlexion.Attributes.Add("Tag", "N009-MF000001509");
                ddlDorsoExtension.Attributes.Add("Tag", "N009-MF000001510");
                ddlDorsoLateDere.Attributes.Add("Tag", "N009-MF000001512");
                ddlDorsoLateIzq.Attributes.Add("Tag", "N009-MF000001513");
                ddlDorsoRotaDere.Attributes.Add("Tag", "N009-MF000001514");
                ddlDorsoRotaIzq.Attributes.Add("Tag", "N009-MF000001515");
                ddlDorsoIrradiacion.Attributes.Add("Tag", "N009-MF000001516");

                ddlLasegueDere.Attributes.Add("Tag", "N009-MF000001517");
                ddlLasegueIzq.Attributes.Add("Tag", "N009-MF000001511");
                ddlSchoberDere.Attributes.Add("Tag", "N009-MF000001469");
                ddlSchoberIzq.Attributes.Add("Tag", "N009-MF000001468");


                chkColumnaCervicalApofisis.Attributes.Add("Tag", "N009-MF000001226");
                chkColumnaCervicalContractura.Attributes.Add("Tag", "N009-MF000001221");
                chkColumnaDorsalApofisis.Attributes.Add("Tag", "N009-MF000001227");
                chkColumnaDorsalContractura.Attributes.Add("Tag", "N009-MF000001222");
                chkColumnaLumbarApofisis.Attributes.Add("Tag", "N009-MF000001220");
                chkColumnaLumbarContractura.Attributes.Add("Tag", "N009-MF000001224");


                //1
                ddlHD1.Attributes.Add("Tag", "N009-MF000001223");
                ddlHD2.Attributes.Add("Tag", "N009-MF000001228");
                ddlHD3.Attributes.Add("Tag", "N009-MF000001229");
                ddlHD4.Attributes.Add("Tag", "N009-MF000001230");
                ddlHD5.Attributes.Add("Tag", "N009-MF000001415");
                ddlHD6.Attributes.Add("Tag", "N009-MF000001416");
                ddlHD7.Attributes.Add("Tag", "N009-MF000001412");
                ddlHD8.Attributes.Add("Tag", "N009-MF000001413");


                //2
                ddlHI1.Attributes.Add("Tag", "N009-MF000001414");
                ddlHI2.Attributes.Add("Tag", "N009-MF000000547");
                ddlHI3.Attributes.Add("Tag", "N009-MF000000548");
                ddlHI4.Attributes.Add("Tag", "N009-MF000000544");
                ddlHI5.Attributes.Add("Tag", "N009-MF000000545");
                ddlHI6.Attributes.Add("Tag", "N009-MF000000144");
                ddlHI7.Attributes.Add("Tag", "N009-MF000000536");
                ddlHI8.Attributes.Add("Tag", "N009-MF000000064");

                //3
                ddlCD1.Attributes.Add("Tag", "N009-MF000000065");
                ddlCD2.Attributes.Add("Tag", "N009-MF000000059");
                ddlCD3.Attributes.Add("Tag", "N009-MF000000060");
                ddlCD4.Attributes.Add("Tag", "N009-MF000000062");
                ddlCD5.Attributes.Add("Tag", "N009-MF000000063");
                ddlCD6.Attributes.Add("Tag", "N009-MF000000813");
                ddlCD7.Attributes.Add("Tag", "N009-MF000000153");
                ddlCD8.Attributes.Add("Tag", "N009-MF000000819");

                //4
                ddlCI1.Attributes.Add("Tag", "N009-MF000000822");
                ddlCI2.Attributes.Add("Tag", "N009-MF000000820");
                ddlCI3.Attributes.Add("Tag", "N009-MF000000823");
                ddlCI4.Attributes.Add("Tag", "N009-MF000000821");
                ddlCI5.Attributes.Add("Tag", "N009-MF000000156");
                ddlCI6.Attributes.Add("Tag", "N009-MF000000154");
                ddlCI7.Attributes.Add("Tag", "N009-MF000000155");
                ddlCI8.Attributes.Add("Tag", "N009-MF000000146");

                //5
                ddlMuneD1.Attributes.Add("Tag", "N009-MF000000808");
                ddlMuneD2.Attributes.Add("Tag", "N009-MF000000152");
                ddlMuneD3.Attributes.Add("Tag", "N009-MF000000809");
                ddlMuneD4.Attributes.Add("Tag", "N009-MF000000157");
                ddlMuneD5.Attributes.Add("Tag", "N009-MF000000812");
                ddlMuneD6.Attributes.Add("Tag", "N009-MF000000815");
                ddlMuneD7.Attributes.Add("Tag", "N009-MF000000816");
                ddlMuneD8.Attributes.Add("Tag", "N009-MF000000814");

                //6
                ddlMuneI1.Attributes.Add("Tag", "N009-MF000000147");
                ddlMuneI2.Attributes.Add("Tag", "N009-MF000000829");
                ddlMuneI3.Attributes.Add("Tag", "N009-MF000000168");
                ddlMuneI4.Attributes.Add("Tag", "N009-MF000000833");
                ddlMuneI5.Attributes.Add("Tag", "N009-MF000000174");
                ddlMuneI6.Attributes.Add("Tag", "N009-MF000000831");
                ddlMuneI7.Attributes.Add("Tag", "N009-MF000000173");
                ddlMuneI8.Attributes.Add("Tag", "N009-MF000000832");

                //7
                ddlCaderaD1.Attributes.Add("Tag", "N009-MF000000171");
                ddlCaderaD2.Attributes.Add("Tag", "N009-MF000000169");
                ddlCaderaD3.Attributes.Add("Tag", "N009-MF000000167");
                ddlCaderaD4.Attributes.Add("Tag", "N009-MF000000170");
                ddlCaderaD5.Attributes.Add("Tag", "N009-MF000000837");
                ddlCaderaD6.Attributes.Add("Tag", "N009-MF000000839");
                ddlCaderaD7.Attributes.Add("Tag", "N009-MF000000836");
                ddlCaderaD8.Attributes.Add("Tag", "N009-MF000000835");

                //8
                ddlCaderaI1.Attributes.Add("Tag", "N009-MF000000838");
                ddlCaderaI2.Attributes.Add("Tag", "N009-MF000000845");
                ddlCaderaI3.Attributes.Add("Tag", "N009-MF000000842");
                ddlCaderaI4.Attributes.Add("Tag", "N009-MF000000843");
                ddlCaderaI5.Attributes.Add("Tag", "N009-MF000000841");
                ddlCaderaI6.Attributes.Add("Tag", "N009-MF000000844");
                ddlCaderaI7.Attributes.Add("Tag", "N009-MF000000850");
                ddlCaderaI8.Attributes.Add("Tag", "N009-MF000000621");

                //9
                ddlTobilloD1.Attributes.Add("Tag", "N009-MF000000773");
                ddlTobilloD2.Attributes.Add("Tag", "N009-MF000000825");
                ddlTobilloD3.Attributes.Add("Tag", "N009-MF000000826");
                ddlTobilloD4.Attributes.Add("Tag", "N009-MF000000827");
                ddlTobilloD5.Attributes.Add("Tag", "N009-MF000000828");
                ddlTobilloD6.Attributes.Add("Tag", "N009-MF000000830");
                ddlTobilloD7.Attributes.Add("Tag", "N009-MF000000145");
                ddlTobilloD8.Attributes.Add("Tag", "N009-MF000000818");

                //10
                ddlTobilloI1.Attributes.Add("Tag", "N009-MF000000824");
                ddlTobilloI2.Attributes.Add("Tag", "N009-MF000000158");
                ddlTobilloI3.Attributes.Add("Tag", "N009-MF000000810");
                ddlTobilloI4.Attributes.Add("Tag", "N009-MF000000159");
                ddlTobilloI5.Attributes.Add("Tag", "N009-MF000000160");
                ddlTobilloI6.Attributes.Add("Tag", "N009-MF000000846");
                ddlTobilloI7.Attributes.Add("Tag", "N009-MF000000811");
                ddlTobilloI8.Attributes.Add("Tag", "N009-MF000000817");

                //11
                ddlRodillaD1.Attributes.Add("Tag", "N009-MF000000207");
                ddlRodillaD2.Attributes.Add("Tag", "N009-MF000001104");
                ddlRodillaD3.Attributes.Add("Tag", "N009-MF000001103");
                ddlRodillaD4.Attributes.Add("Tag", "N009-MF000001102");
                ddlRodillaD5.Attributes.Add("Tag", "N009-MF000000048");
                ddlRodillaD6.Attributes.Add("Tag", "N009-MF000000546");
                ddlRodillaD7.Attributes.Add("Tag", "N009-MF000000543");
                ddlRodillaD8.Attributes.Add("Tag", "N009-MF000000055");

                //12
                ddlRodillaI1.Attributes.Add("Tag", "N009-MF000000056");
                ddlRodillaI2.Attributes.Add("Tag", "N009-MF000000057");
                ddlRodillaI3.Attributes.Add("Tag", "N009-MF000000053");
                ddlRodillaI4.Attributes.Add("Tag", "N009-MF000000054");
                ddlRodillaI5.Attributes.Add("Tag", "N009-MF000000066");
                ddlRodillaI6.Attributes.Add("Tag", "N009-MF000000051");
                ddlRodillaI7.Attributes.Add("Tag", "N009-MF000000058");
                ddlRodillaI8.Attributes.Add("Tag", "N009-MF000000061");


                ddlPhalenDerecha.Attributes.Add("Tag", "N009-MF000000847");
                ddlPhalenIzquierda.Attributes.Add("Tag", "N009-MF000000848");
                ddlTinelDerecha.Attributes.Add("Tag", "N009-MF000000849");
                ddlTinelIzquierda.Attributes.Add("Tag", "N009-MF000000172");
                ddlCodoDerecho.Attributes.Add("Tag", "N009-MF000000166");
                ddlCodoIzquierdo.Attributes.Add("Tag", "N009-MF000000834");
                ddlPieDerecho.Attributes.Add("Tag", "N009-MF000000840");
                ddlPieIzquierdo.Attributes.Add("Tag", "N009-MF000000539");


                txtAmpliar.Attributes.Add("Tag", "N009-MF000001507");
                txtConclusion.Attributes.Add("Tag", "N009-MF000000232");


                //Psicología
                ddlPresentacion.Attributes.Add("Tag", "N002-MF000000283");
                ddlPostura.Attributes.Add("Tag", "N002-MF000000282");
                ddlDisRitmo.Attributes.Add("Tag", "N002-MF000000280");
                ddlDisTono.Attributes.Add("Tag", "N002-MF000000281");
                ddlDisArticulacion.Attributes.Add("Tag", "N002-MF000000279");
                ddlOrientacionEspacio.Attributes.Add("Tag", "N009-MF000000078");
                ddlOrientacionTiempo.Attributes.Add("Tag", "N009-MF000000077");
                ddlOrientacionPersona.Attributes.Add("Tag", "N009-MF000000079");

                txtLucido.Attributes.Add("Tag", "N009-MF000000326");
                txtPensamiento.Attributes.Add("Tag", "N009-MF000000328");
                txtPercepcion.Attributes.Add("Tag", "N002-MF000000329");
                ddlMemoria.Attributes.Add("Tag", "N002-MF000000327");
                ddlInteligencia.Attributes.Add("Tag", "N002-MF000000331");
                txtApetito.Attributes.Add("Tag", "N002-MF000000263");
                txtSuenio.Attributes.Add("Tag", "N002-MF000000269");

                txtPtje1.Attributes.Add("Tag", "N002-MF000000266");
                txtPtje2.Attributes.Add("Tag", "N009-MF000002033");

                txtPtje3.Attributes.Add("Tag", "N002-MF000000268");
                txtPtje4.Attributes.Add("Tag", "N009-MF000002034");

                txtPtje5.Attributes.Add("Tag", "N002-MF000000267");
                txtPtje6.Attributes.Add("Tag", "N009-MF000002035");

                txtPtje7.Attributes.Add("Tag", "N009-MF000000635");
                txtPtje8.Attributes.Add("Tag", "N009-MF000002036");

                txtPtje9.Attributes.Add("Tag", "N002-MF000000264");
                txtPtje10.Attributes.Add("Tag", "N009-MF000002037");

                txtPtje11.Attributes.Add("Tag", "N002-MF000000265");
                txtPtje12.Attributes.Add("Tag", "N009-MF000002038");

                txtPtje13.Attributes.Add("Tag", "N002-MF000000270");
                txtPtje14.Attributes.Add("Tag", "N009-MF000002039");

                txtPtje15.Attributes.Add("Tag", "N002-MF000000277");
                txtPtje16.Attributes.Add("Tag", "N002-MF000002040");

                txtPtje17.Attributes.Add("Tag", "N002-MF000000272");
                txtPtje18.Attributes.Add("Tag", "N002-MF000002041");

                txtPtje19.Attributes.Add("Tag", "N009-MF000000273");
                txtPtje20.Attributes.Add("Tag", "N002-MF000002042");

                txtPtje21.Attributes.Add("Tag", "N004-MF000000274");
                txtPtje22.Attributes.Add("Tag", "N009-MF000002043");

                txtPtje23.Attributes.Add("Tag", "N004-MF000000275");
                txtPtje24.Attributes.Add("Tag", "N002-MF000002044");

                txtPtje25.Attributes.Add("Tag", "N002-MF000000276");
                txtPtje26.Attributes.Add("Tag", "N002-MF000002045");

                txtPtje27.Attributes.Add("Tag", "N009-MF000002030");

                txtPtje28.Attributes.Add("Tag", "N009-MF000002031");
                txtPtje29.Attributes.Add("Tag", "N009-MF000002046");

                txtPtje30.Attributes.Add("Tag", "N009-MF000002032");
                txtPtje31.Attributes.Add("Tag", "N009-MF000002047");

                txtNivelIntelectual.Attributes.Add("Tag", "N009-MF000000630");
                txtCoodinacionViso.Attributes.Add("Tag", "N009-MF000000631");
                txtNivelMemoria.Attributes.Add("Tag", "N009-MF000000632");
                txtPersonalidad.Attributes.Add("Tag", "N009-MF000000633");
                txtAfectividad.Attributes.Add("Tag", "N009-MF000000634");
                txtOtros.Attributes.Add("Tag", "N009-MF000002048");

                txtAreaCognitiva.Attributes.Add("Tag", "N002-MF000000336");
                txtAreaEmocional.Attributes.Add("Tag", "N002-MF000000337");
                txtEvaEspaciosConfinados.Attributes.Add("Tag", "N009-MF000002049");
                txtEvaTrabajoAltura.Attributes.Add("Tag", "N009-MF000002050");

                txtReco1.Attributes.Add("Tag", "N009-MF000000081");
                txtControl1.Attributes.Add("Tag", "N009-MF000002051");
                txtReco2.Attributes.Add("Tag", "N009-MF000002052");
                txtControl2.Attributes.Add("Tag", "N009-MF000002053");

                txtReco3.Attributes.Add("Tag", "N009-MF000002054");
                txtControl3.Attributes.Add("Tag", "N009-MF000002055");

                //TAMIZAJE DERMATOLOGICO
                ddlAlergiasDermicas.Attributes.Add("Tag", "N009-MF000001983");
                ddlAlergiasMedicamentosas.Attributes.Add("Tag", "N009-MF000001984");
                ddlEnfPropiaPiel.Attributes.Add("Tag", "N009-MF000001985");
                ddlLupusEritematoso.Attributes.Add("Tag", "N009-MF000001987");
                ddlEnfermedadTiroidea.Attributes.Add("Tag", "N009-MF000001988");
                ddlArtritisReumatoide.Attributes.Add("Tag", "N009-MF000001989");
                ddlHepatopatias.Attributes.Add("Tag", "N009-MF000001990");
                ddlPsoriasis.Attributes.Add("Tag", "N009-MF000001991");
                ddlSindromeOvario.Attributes.Add("Tag", "N009-MF000001992");
                ddlDiabetesMellitus.Attributes.Add("Tag", "N009-MF000001993");
                ddlOtrasEnfermedadesSistemicas.Attributes.Add("Tag", "N009-MF000001994");
                ddlMacula.Attributes.Add("Tag", "N009-MF000001996");
                ddlVesicula.Attributes.Add("Tag", "N009-MF000001997");
                ddlNodulo.Attributes.Add("Tag", "N009-MF000001998");
                ddlPurpura.Attributes.Add("Tag", "N009-MF000001999");
                ddlPapula.Attributes.Add("Tag", "N009-MF000002000");
                ddlAmpolla.Attributes.Add("Tag", "N009-MF000002001");
                ddlPlaca.Attributes.Add("Tag", "N009-MF000002002");
                ddlComedones.Attributes.Add("Tag", "N009-MF000002003");
                ddlTuberculo.Attributes.Add("Tag", "N009-MF000002004");
                ddlPustula.Attributes.Add("Tag", "N009-MF000002005");
                ddlQuiste.Attributes.Add("Tag", "N009-MF000002006");
                ddlTelangiectasia.Attributes.Add("Tag", "N009-MF000002007");

                ddlEscama.Attributes.Add("Tag", "N009-MF000002009");
                ddlPetequia.Attributes.Add("Tag", "N009-MF00000201");
                ddlAngioedema.Attributes.Add("Tag", "N009-MF000002011");
                ddlTumor.Attributes.Add("Tag", "N009-MF000002012");
                ddlHabon.Attributes.Add("Tag", "N009-MF000002013");
                ddlEquimosis.Attributes.Add("Tag", "N009-MF000002014");
                ddlDiscromias.Attributes.Add("Tag", "N009-MF000002015");
                ddlEscamas.Attributes.Add("Tag", "N009-MF000002017");

                ddlEscaras.Attributes.Add("Tag", "N009-MF000002018");
                ddlFisura.Attributes.Add("Tag", "N009-MF000002019");
                ddlExcoriaciones.Attributes.Add("Tag", "N009-MF000002020");
                ddlCostras.Attributes.Add("Tag", "N009-MF000002021");
                ddlCicatrices.Attributes.Add("Tag", "N009-MF000002022");
                ddlAtrofia.Attributes.Add("Tag", "N009-MF000002023");
                ddlLiquenificacion.Attributes.Add("Tag", "N009-MF000002024");
                ddlEsclerosis.Attributes.Add("Tag", "N009-MF000002026");

                ddlUlceras.Attributes.Add("Tag", "N009-MF000002057");
                ddlErosion.Attributes.Add("Tag", "N009-MF000002058");

                txtDescribirAnamnesis.Attributes.Add("Tag", "N009-MF000001986");
                txtDescribirEnfermedades.Attributes.Add("Tag", "N009-MF000001995");
                txtOtrosLesionesPrimarias.Attributes.Add("Tag", "N009-MF000002016");
                txtOtrosLesionesSecundarias.Attributes.Add("Tag", "N009-MF000002029");


                //Espirometría
                ddlEspiroCuestionario1Pregunta1.Attributes.Add("Tag", "N002-MF000000199");
                ddlEspiroCuestionario1Pregunta2.Attributes.Add("Tag", "N002-MF000000200");
                ddlEspiroCuestionario1Pregunta3.Attributes.Add("Tag", "N002-MF000000201");
                ddlEspiroCuestionario1Pregunta4.Attributes.Add("Tag", "N002-MF000000202");
                ddlEspiroCuestionario1Pregunta5.Attributes.Add("Tag", "N002-MF000000203");
                ddlAntecedentesHemoptisis.Attributes.Add("Tag", "N009-MF000000112");
                ddlAntecedentesInfarto.Attributes.Add("Tag", "N009-MF000000116");
                ddlAntecedentesPneumotorax.Attributes.Add("Tag", "N009-MF000000113");
                ddlAntecedentesInestabilidad.Attributes.Add("Tag", "N009-MF000000574");
                ddlAntecedentesTraqueostomia.Attributes.Add("Tag", "N009-MF000000114");
                ddlAntecedentesFiebre.Attributes.Add("Tag", "N009-MF000000117");
                ddlAntecedentesSonda.Attributes.Add("Tag", "N009-MF000000115");
                ddlAntecedentesEmbarazo.Attributes.Add("Tag", "N009-MF000000120");
                ddlAntecedentesAneurisma.Attributes.Add("Tag", "N009-MF000000118");
                ddlAntecedentesEmbarazoComplicado.Attributes.Add("Tag", "N009-MF000000121");
                ddlAntecedentesEmbolia.Attributes.Add("Tag", "N009-MF000000119");
                ddlEspiroCuestionario2Pregunta1.Attributes.Add("Tag", "N002-MF000000207");
                ddlEspiroCuestionario2Pregunta2.Attributes.Add("Tag", "N002-MF000000284");
                ddlEspiroCuestionario2Pregunta3.Attributes.Add("Tag", "N002-MF000000204");
                ddlEspiroCuestionario2Pregunta4.Attributes.Add("Tag", "N002-MF000000206");
                ddlEspiroCuestionario2Pregunta5.Attributes.Add("Tag", "N002-MF000000208");
                ddlEspiroCuestionario2Pregunta6.Attributes.Add("Tag", "N002-MF000000205");
                ddlEspiroCuestionario2Pregunta7.Attributes.Add("Tag", "N002-MF000000285");
                ddlOrigenEtnico.Attributes.Add("Tag", "N009-MF000000622");
                ddlTabaquismo.Attributes.Add("Tag", "N009-MF000000623");
                txtTiempotrabajo.Attributes.Add("Tag", "N009-MF000001038");
                txtEspiroCVF.Attributes.Add("Tag", "N002-MF000000286");
                txtEspiroCVFDescrip.Attributes.Add("Tag", "N009-MF000000578");
                txtEspiroVEF.Attributes.Add("Tag", "N002-MF000000287");
                txtEspiroVEFDescrip.Attributes.Add("Tag", "N009-MF000000579");
                txtEspiroVEF1.Attributes.Add("Tag", "N002-MF000000169");
                txtEspiroVEF1Descrip.Attributes.Add("Tag", "N009-MF000000580");
                txtEspiroFET.Attributes.Add("Tag", "N002-MF000000170");
                txtEspiroFETDescrip.Attributes.Add("Tag", "N009-MF000000581");
                txtEspiroFEF.Attributes.Add("Tag", "N002-MF000000171");
                txtEspiroFEFDescrip.Attributes.Add("Tag", "N009-MF000000582");
                txtEspiroPEF.Attributes.Add("Tag", "N002-MF000000168");
                txtEspiroPEFDescrip.Attributes.Add("Tag", "N009-MF000000583");
                txtEspiroMEF.Attributes.Add("Tag", "N009-MF000000575");
                txtEspiroMEFDescrip.Attributes.Add("Tag", "N009-MF000000584");
                txtEspiroR50.Attributes.Add("Tag", "N009-MF000000576");
                txtEspiroR50Descrip.Attributes.Add("Tag", "N009-MF000000585");
                txtEspiroMVV.Attributes.Add("Tag", "N009-MF000000577");
                txtEspiroMVVdescrip.Attributes.Add("Tag", "N009-MF000000586");

                //Oftalmología
                txtOftalmoAnamnesis.Attributes.Add("Tag", "N002-MF000000225");
                txtOftalmoAntecedentes.Attributes.Add("Tag", "N009-MF000000710");
                txtConCorrectLejosOD.Attributes.Add("Tag", "N002-MF000000231");
                txtConCorrectLejosOI.Attributes.Add("Tag", "N002-MF000000236");
                txtSinCorrectLejosOD.Attributes.Add("Tag", "N002-MF000000234");
                txtSinCorrectLejosOI.Attributes.Add("Tag", "N002-MF000000230");
                txtSinCorrectCercaOD.Attributes.Add("Tag", "N002-MF000000233");
                txtSinCorrectCercaOI.Attributes.Add("Tag", "N002-MF000000227");
                txtConCorrectCercaOI.Attributes.Add("Tag", "N009-MF000000646");
                txtConCorrectCercaOD.Attributes.Add("Tag", "N002-MF000000235");
                txtAELejosOD.Attributes.Add("Tag", "N002-MF000000247");
                txtAECercaOD.Attributes.Add("Tag", "N009-MF000000641");
                txtAECercaOI.Attributes.Add("Tag", "N002-MF000000237");
                txtAELejosOI.Attributes.Add("Tag", "N002-MF000000240");
                txtTestColoresOD.Attributes.Add("Tag", "N002-MF000000224");
                txtTestColoresOI.Attributes.Add("Tag", "N009-MF000000719");
                txtTonometriaOD.Attributes.Add("Tag", "N009-MF000000182");
                txtTonometriaOI.Attributes.Add("Tag", "N009-MF000000175");
                txtEstereopsisOD.Attributes.Add("Tag", "N009-MF000000177");
                txtEstereopsisOI.Attributes.Add("Tag", "N002-MF000000226");
                txtEstereopsisTiempo.Attributes.Add("Tag", "N002-MF000000258");
                txtTestEncandilamientoOD.Attributes.Add("Tag", "N002-MF000000246");
                txtTestEncandilamientoOI.Attributes.Add("Tag", "N002-MF000000261");
                txtFondoOjoOD.Attributes.Add("Tag", "N002-MF000000238");
                txtFondoOjoOI.Attributes.Add("Tag", "N002-MF000000239");
                chkParpadoDerecho.Attributes.Add("Tag", "N002-MF000000251");
                chkParpadoIzquierdo.Attributes.Add("Tag", "N002-MF000000252");
                chkConjuntivaDerecha.Attributes.Add("Tag", "N002-MF000000254");
                chkConjuntivaIzquierda.Attributes.Add("Tag", "N002-MF000000255");
                chkCorneaDerecha.Attributes.Add("Tag", "N009-MF000000524");
                chkCorneaIzquierda.Attributes.Add("Tag", "N009-MF000000525");
                chkIrisDerecho.Attributes.Add("Tag", "N009-MF000000530");
                chkIrisIzquierdo.Attributes.Add("Tag", "N009-MF000000531");
                chkMovOcularDerecho.Attributes.Add("Tag", "N009-MF000000533");
                chkMovOcularIzquierdo.Attributes.Add("Tag", "N009-MF000000534");


                //Rx
                txtRXNroPlaca.Attributes.Add("Tag", "N009-MF000001788");
                txtRXVertices.Attributes.Add("Tag", "N009-MF000000590");
                txtRXCamposPulmonares.Attributes.Add("Tag", "N009-MF000000591");
                txtRXHilios.Attributes.Add("Tag", "N009-MF000000592");
                txtRXSenosDiafrag.Attributes.Add("Tag", "N009-MF000000593");
                txtRXMediastinos.Attributes.Add("Tag", "N009-MF000000594");
                txtRXSiluetaCard.Attributes.Add("Tag", "N009-MF000000595");
                txtRXSenosCardiofre.Attributes.Add("Tag", "N009-MF000000883");
                txtRXInidice.Attributes.Add("Tag", "N009-MF000000884");
                txtRXPartesBlandas.Attributes.Add("Tag", "N009-MF000000886");
                txtRXConclusiones.Attributes.Add("Tag", "N009-MF000000233");

                //7D
                txt7DActividad.Attributes.Add("Tag", "N002-MF000000306");
                chk7DCardiacos.Attributes.Add("Tag", "N002-MF000000307");
                chk7DOftalmo.Attributes.Add("Tag", "N002-MF000000308");
                chk7DDigestivos.Attributes.Add("Tag", "N002-MF000000309");
                chk7DCoagulacion.Attributes.Add("Tag", "N002-MF000000310");
                chk7DRespiratorios.Attributes.Add("Tag", "N002-MF000000311");
                chk7DNeurologico.Attributes.Add("Tag", "N002-MF000000312");
                chkCirugiaMayor.Attributes.Add("Tag", "N002-MF000000313");
                chk7DInfecciones.Attributes.Add("Tag", "N002-MF000000314");
                chk7DHipertension.Attributes.Add("Tag", "N002-MF000000315");
                chk7DObesidad.Attributes.Add("Tag", "N002-MF000000316");
                chk7DDiabetes.Attributes.Add("Tag", "N002-MF000000317");
                chk7DApnea.Attributes.Add("Tag", "N002-MF000000318");
                chk7DEmbarazo.Attributes.Add("Tag", "N002-MF000000319");
                chk7DAlergias.Attributes.Add("Tag", "N002-MF000000320");
                chk7DAnemia.Attributes.Add("Tag", "N002-MF000000321");
                txt7DMedicaActual.Attributes.Add("Tag", "N002-MF000000322");
                txt7DOtraCondMedica.Attributes.Add("Tag", "N002-MF000000325");
                txt7DObeservaciones.Attributes.Add("Tag", "N009-MF000000230");
                ddl7DConclusion.Attributes.Add("Tag", "N002-MF000000323");

                //Laboratorio
                txtOrinaColor.Attributes.Add("Tag", "N009-MF000000444");
                txtOrinaAspecto.Attributes.Add("Tag", "N009-MF000001041");
                txtOrinaDensidad.Attributes.Add("Tag", "N009-MF000001043");
                txtOrinaPh.Attributes.Add("Tag", "N009-MF000001045");
                txtOrinaCelulas.Attributes.Add("Tag", "N009-MF000001059");
                txtOrinaLeucocitos.Attributes.Add("Tag", "N009-MF000001061");
                txtOrinaHematies.Attributes.Add("Tag", "N009-MF000001063");
                txtOrinaCristales.Attributes.Add("Tag", "N009-MF000001065");
                txtOrinaGermenes.Attributes.Add("Tag", "N009-MF000001067");
                txtOrinaCilindros.Attributes.Add("Tag", "N009-MF000001069");
                txtOrinaFilamento.Attributes.Add("Tag", "N009-MF000001071");
                txtOrinaSangre.Attributes.Add("Tag", "N009-MF000001047");
                txtOrinaUrobilinogeno.Attributes.Add("Tag", "N009-MF000001049");
                txtOrinaBilirrubina.Attributes.Add("Tag", "N009-MF000001051");
                txtOrinaProteina.Attributes.Add("Tag", "N009-MF000001053");
                txtOrinaNitritos.Attributes.Add("Tag", "N009-MF000001055");
                txtOrinaCetonico.Attributes.Add("Tag", "N009-MF000001057");
                txtOrinaGlucosa.Attributes.Add("Tag", "N009-MF000001313");
                txtOrinaHemoglobina.Attributes.Add("Tag", "N009-MF000001315");
                //txtHemogramaHematies.Attributes.Add("Tag", "N009-MF000001940");
                //txtHemogramaHematiesDeseable.Attributes.Add("Tag", "N009-MF000001941");
                //txtHemorgamaLeucocitos.Attributes.Add("Tag", "N009-MF000001952");
                //txtHemorgamaLeucocitosDeseable.Attributes.Add("Tag", "N009-MF000001953");
                txtHemogramaPlaquetas.Attributes.Add("Tag", "N009-MF000001948");
                txtHemogramaPlaquetasDeseable.Attributes.Add("Tag", "N009-MF000001949");
                //txtHemogramaHemoglobina.Attributes.Add("Tag", "N009-MF000001936");
                //txtHemogramaHemoglobinaDeseable.Attributes.Add("Tag", "N009-MF000001937");
                //txtHemogramaHematcrito.Attributes.Add("Tag", "N009-MF000001938");
                //txtHemogramaHematcritoDeseable.Attributes.Add("Tag", "N009-MF000001939");
                //txtHemogramaVCM.Attributes.Add("Tag", "N009-MF000001942");
                //txtHemogramaVCMDeseable.Attributes.Add("Tag", "N009-MF000001943");
                //txtHemogramaHCM.Attributes.Add("Tag", "N009-MF000001946");
                //txtHemogramaHCMDeseable.Attributes.Add("Tag", "N009-MF000001947");
                //txtHemogramaCCMH.Attributes.Add("Tag", "N009-MF000001944");
                //txtHemogramaCCMHDeseable.Attributes.Add("Tag", "N009-MF000001945");
                //txtHemogramaAbastonados.Attributes.Add("Tag", "N009-MF000001964");
                //txtHemogramaAbastonadosDeseable.Attributes.Add("Tag", "N009-MF000002093");
                //txtHemogramaSegmentados.Attributes.Add("Tag", "N009-MF000001956");
                //txtHemogramaSegmentadosDeseable.Attributes.Add("Tag", "N009-MF000002094");
                //txtHemogramaEosinofilos.Attributes.Add("Tag", "N009-MF000001951");
                //txtHemogramaEosinofilosDeseable.Attributes.Add("Tag", "N009-MF000002095");
                //txtHemogramaBasofilos.Attributes.Add("Tag", "N009-MF000001950");
                //txtHemogramaBasofilosDeseable.Attributes.Add("Tag", "N009-MF000002096");
                //txtHemogramaMonocitos.Attributes.Add("Tag", "N009-MF000001961");
                //txtHemogramaMonocitosDeseable.Attributes.Add("Tag", "N009-MF000002097");
                //txtHemogramaLinfocitos.Attributes.Add("Tag", "N009-MF000001954");
                //txtHemogramaLinfocitosDeseable.Attributes.Add("Tag", "N009-MF000002098");
                //txtHemogramaMiolocitos.Attributes.Add("Tag", "N009-MF000001960");
                //txtHemogramaMiolocitosDeseable.Attributes.Add("Tag", "N009-MF000002099");
                //txtHemogramaMetamielocitos.Attributes.Add("Tag", "N009-MF000001955");
                //txtHemogramaMetamielocitosDeseable.Attributes.Add("Tag", "N009-MF000002100");
                //txtHemoAbastonados.Attributes.Add("Tag", "N009-N009-MF000001957");
                //txtHemoAbastonadosDeseable.Attributes.Add("Tag", "N009-MF000002101");
                //txtHemoSegmentados.Attributes.Add("Tag", "N009-MF000002102");
                //txtHemoSegmentadosDeseable.Attributes.Add("Tag", "N009-MF000002103");
                //txtHemoEosinofilos.Attributes.Add("Tag", "N009-MF000001958");
                //txtHemoEosinofilosDeseable.Attributes.Add("Tag", "N009-MF000002104");
                //txtHemoBasofilos.Attributes.Add("Tag", "N009-MF000001963");
                //txtHemoBasofilosDeseable.Attributes.Add("Tag", "N009-MF000002105");
                //txtHemoMonocitos.Attributes.Add("Tag", "N009-MF000002106");
                //txtHemoMonocitosDeseable.Attributes.Add("Tag", "N009-MF000002107");
                //txtHemoLinfocitos.Attributes.Add("Tag", "N009-MF000002108");
                //txtHemoLinfocitosDeseable.Attributes.Add("Tag", "N009-MF000002109");
                //txtHemoMiolocitos.Attributes.Add("Tag", "N009-MF000001965");
                //txtHemoMiolocitosDeseable.Attributes.Add("Tag", "N009-MF000002110");
                //txtHemoMetamielocitos.Attributes.Add("Tag", "N009-MF000001959");
                //txtHemoMetamielocitosDeseable.Attributes.Add("Tag", "N009-MF000002113");
                //txtLabReticulocitos.Attributes.Add("Tag", "N009-MF000002111");
                //txtLabReticulocitosDeseable.Attributes.Add("Tag", "N009-MF000002112");
                txtLabVSG.Attributes.Add("Tag", "N009-MF000002114");
                txtLabVSGDeseable.Attributes.Add("Tag", "N009-MF000002115");
                txtLabTIempoCoagulacion.Attributes.Add("Tag", "N009-MF000002116");
                txtLabTIempoCoagulacionDeseable.Attributes.Add("Tag", "N009-MF000002117");
                txtLabTiempoSangria.Attributes.Add("Tag", "N009-MF000002118");
                txtLabTiempoSangriaDeseable.Attributes.Add("Tag", "N009-MF000002119");
                //txtLabTiempoTromboplast.Attributes.Add("Tag", "N009-MF000002120");
                //txtLabTiempoTromboplastDeseable.Attributes.Add("Tag", "N009-MF000002121");
                //txtLabTiempoProtombina.Attributes.Add("Tag", "N009-MF000002122");
                //txtLabTiempoProtombinaDeseable.Attributes.Add("Tag", "N009-MF000002123");
                txtLabCarboxihemoglobina.Attributes.Add("Tag", "N009-MF000000201");
                txtLabCarboxihemoglobinaDeseable.Attributes.Add("Tag", "N009-MF000000202");
                ddlLabVDRL.Attributes.Add("Tag", "N009-MF000000269");
                txtVDRDeseable.Attributes.Add("Tag", "N009-MF000001295");
                //ddlLabHepatitisB.Attributes.Add("Tag", "N009-MF000002124");
                //txtLabHepatitisBDeseable.Attributes.Add("Tag", "N009-MF000002125");
                ddlLabHAVIgM.Attributes.Add("Tag", "N009-MF000000264");
                txtLabHAVIgMDeseable.Attributes.Add("Tag", "N009-MF000001289");
                ddlLabHIV.Attributes.Add("Tag", "N009-MF000000257");
                txtLabHIVDeseable.Attributes.Add("Tag", "N009-MF000001288");
                //ddlLabAntiHepC.Attributes.Add("Tag", "N009-MF000000267");
                //txtLabAntiHepCDeseable.Attributes.Add("Tag", "N009-MF000001290");
                txtLabAcidoUrico.Attributes.Add("Tag", "N009-MF000001429");
                txtLabAcidoUricoDeseable.Attributes.Add("Tag", "N009-MF000001430");
                ddlLabTificoO.Attributes.Add("Tag", "N009-MF000001318");
                txtLabTificoODeseable.Attributes.Add("Tag", "N009-MF000001319");
                ddlLabTificoH.Attributes.Add("Tag", "N009-MF000001320");
                txtLabTificoHDeseable.Attributes.Add("Tag", "N009-MF000001321");
                ddlLabParatificoA.Attributes.Add("Tag", "N009-MF000000445");
                txtLabParatificoADeseable.Attributes.Add("Tag", "N009-MF000001322");
                ddlLabParatificoB.Attributes.Add("Tag", "N009-MF000001323");
                txtLabParatificoBDeseable.Attributes.Add("Tag", "N009-MF000001324");
                ddlLabBrucella.Attributes.Add("Tag", "N009-MF000001431");
                txtLabBrucellaDeseable.Attributes.Add("Tag", "N009-MF000001432");
                //ddlLabTuboTifico.Attributes.Add("Tag", "N009-MF000002126");
                //txtLabTuboTificoDeseable.Attributes.Add("Tag", "N009-MF000002127");
                //ddlTuboTificoH.Attributes.Add("Tag", "N009-MF000002128");
                //txtTuboTificoHDeseable.Attributes.Add("Tag", "N009-MF000002129");
                //ddlTuboParatificoA.Attributes.Add("Tag", "N009-MF000002130");
                //txtTuboParatificoADeseable.Attributes.Add("Tag", "N009-MF000002131");
                //ddlTuboParatificoB.Attributes.Add("Tag", "N009-MF000002132");
                //txtTuboParatificoBDeseable.Attributes.Add("Tag", "N009-MF000002133");
                //ddlTuboBrucella.Attributes.Add("Tag", "N009-MF000002134");
                //txtTuboBrucellaDeseable.Attributes.Add("Tag", "N009-MF000002135");
                ddlToxiCocaina.Attributes.Add("Tag", "N009-MF000000705");
                txtToxiCocainaDeseable.Attributes.Add("Tag", "N009-MF000002138");
                ddlToxiMarihuana.Attributes.Add("Tag", "N009-MF000001294");
                txtToxiMarihuanaDeseable.Attributes.Add("Tag", "N009-MF000002139");
                //ddlToxiExtasis.Attributes.Add("Tag", "N009-MF000002136");
                //txtToxiExtasisDeseable.Attributes.Add("Tag", "N009-MF000002137");
                ddlToxiBenzodiac.Attributes.Add("Tag", "N009-MF000000395");
                txtToxiBenzodiacDeseable.Attributes.Add("Tag", "N009-MF000000396");
                ddlToxiAnfetam.Attributes.Add("Tag", "N009-MF000000391");
                txtToxiAnfetamDeseable.Attributes.Add("Tag", "N009-MF000000392");
                //ddlToxiMetanfeta.Attributes.Add("Tag", "N009-MF000002140");
                //txtToxiMetanfetaDeseable.Attributes.Add("Tag", "N009-MF000002141");
                txtAntigenoProst.Attributes.Add("Tag", "N009-MF000000443");
                txtAntigenoProstDeseable.Attributes.Add("Tag", "N009-MF000001287");
                txtColesterolTotal.Attributes.Add("Tag", "N009-MF000001086");
                txtColesterolTotalDeseable.Attributes.Add("Tag", "N009-MF000001087");
                //txtLabColesterolHDL.Attributes.Add("Tag", "N009-MF000000254");
                //txtLabColesterolHDLDeseable.Attributes.Add("Tag", "N009-MF000000414");
                //txtLabColesterolLDL.Attributes.Add("Tag", "N009-MF000001073");
                //txtLabColesterolLDLDeseable.Attributes.Add("Tag", "N009-MF000001074");
                //txtLabColesterolVLDL.Attributes.Add("Tag", "N009-MF000001075");
                //txtLabColesterolVLDLDeseable.Attributes.Add("Tag", "N009-MF000001076");
                txtLabTrigliceridos.Attributes.Add("Tag", "N009-MF000001296");
                txtLabTrigliceridosDeseable.Attributes.Add("Tag", "N009-MF000001297");
                //txtLabLipidosTotales.Attributes.Add("Tag", "N009-MF000002142");
                //txtLabLipidosTotalesDeseable.Attributes.Add("Tag", "N009-MF000002143");
                txtLabCreatinina.Attributes.Add("Tag", "N009-MF000000518");
                txtLabCreatininaDeseable.Attributes.Add("Tag", "N009-MF000000519");
                txtLabGlucosa.Attributes.Add("Tag", "N009-MF000000261");
                txtLabGlucosaDeseable.Attributes.Add("Tag", "N009-MF000000418");

                txtLabUrea.Attributes.Add("Tag", "N009-MF000000253");
                txtLabUreaDeseable.Attributes.Add("Tag", "N009-MF000000256");
                txtLabPlomo.Attributes.Add("Tag", "N009-MF000001158");
                txtLabPlomoDeseable.Attributes.Add("Tag", "N009-MF000001291");
                //txtLabMercurio.Attributes.Add("Tag", "N009-MF000002144");
                //txtLabMercurioDeseable.Attributes.Add("Tag", "N009-MF000002145");
                //txtLabCromo.Attributes.Add("Tag", "N009-MF000002146");
                //txtLabCromoDeseable.Attributes.Add("Tag", "N009-MF000002147");
                //txtLabCadmio.Attributes.Add("Tag", "N009-MF000002148");
                //txtLabCadmioDeseable.Attributes.Add("Tag", "N009-MF000002149");
                //txtLabArsenico.Attributes.Add("Tag", "N009-MF000002150");
                //txtLabArsenicoDeseable.Attributes.Add("Tag", "N009-MF000002151");
                txtLabBenceno.Attributes.Add("Tag", "N009-MF000002152");
                txtLabBencenoDeseable.Attributes.Add("Tag", "N009-MF000002153");
                //txtLabCromoOrina.Attributes.Add("Tag", "N009-MF000002154");
                //txtLabCromoOrinaDeseable.Attributes.Add("Tag", "N009-MF000002155");
                //txtLabCobreOrina.Attributes.Add("Tag", "N009-MF000002156");
                //txtLabCobreOrinaDeseable.Attributes.Add("Tag", "N009-MF000002157");
                //txtLabCadmioOrina.Attributes.Add("Tag", "N009-MF000002158");
                //txtLabCadmioOrinaDeseable.Attributes.Add("Tag", "N009-MF000002159");
                //txtLabMercurioOrina.Attributes.Add("Tag", "N009-MF000002160");
                //txtLabMercurioOrinaDeseable.Attributes.Add("Tag", "N009-MF000002161");
                //txtLabPlomoOrina.Attributes.Add("Tag", "N009-MF000002162");
                //txtLabPlomoOrinaDeseable.Attributes.Add("Tag", "N009-MF000002163");
                //txtLabNiquelOrina.Attributes.Add("Tag", "N009-MF000002164");
                //txtLabNiquelOrinaDeseable.Attributes.Add("Tag", "N009-MF000002165");
                //txtLabMagnesioOrina.Attributes.Add("Tag", "N009-MF000002166");
                //txtLabMagnesioOrinaDeseable.Attributes.Add("Tag", "N009-MF000002167");
                //txtLabArsenicoOrina.Attributes.Add("Tag", "N009-MF000002168");
                //txtLabArsenicoOrinaDeseable.Attributes.Add("Tag", "N009-MF000002169");
                //txtLabAcMetilhipurico.Attributes.Add("Tag", "N009-MF000002170");
                //txtLabAcMetilhipuricoDeseable.Attributes.Add("Tag", "N009-MF000002171");
                //txtLabXileno.Attributes.Add("Tag", "N009-MF000002172");
                //txtLabXilenoDeseable.Attributes.Add("Tag", "N009-MF000002173");
                txtLabTGO.Attributes.Add("Tag", "N009-MF000000706");
                txtLabTGODeseable.Attributes.Add("Tag", "N009-MF000001292");
                txtLabTGP.Attributes.Add("Tag", "N009-MF000000707");
                txtLabTGPDeseable.Attributes.Add("Tag", "N009-MF000001293");
                //txtLabGGTP.Attributes.Add("Tag", "N009-MF000001804");
                //txtLabGGTPDeseable.Attributes.Add("Tag", "N009-MF000001805");
                //txtLabBilirrTotal.Attributes.Add("Tag", "N009-MF000001806");
                //txtLabBilirrTotalDeseable.Attributes.Add("Tag", "N009-MF000001807");
                //txtLabBilirrDirecta.Attributes.Add("Tag", "N009-MF000001808");
                //txtLabBilirrDirectaDeseable.Attributes.Add("Tag", "N009-MF000001809");
                //txtLabBilirrIndirecta.Attributes.Add("Tag", "N009-MF000001810");
                //txtLabBilirrIndirectaDeseable.Attributes.Add("Tag", "N009-MF000001811");
                //txtLabProteinasTotales.Attributes.Add("Tag", "N009-MF000001792");
                //txtLabProteinasTotalesDeseable.Attributes.Add("Tag", "N009-MF000001793");
                //txtLabFosfatasa.Attributes.Add("Tag", "N009-MF000001802");
                //txtLabFosfatasaDeseable.Attributes.Add("Tag", "N009-MF000001803");
                //txtLabAlbumina.Attributes.Add("Tag", "N009-MF000001794");
                //txtLabAlbuminaDeseable.Attributes.Add("Tag", "N009-MF000001795");
                //txtLabGlobulina.Attributes.Add("Tag", "N009-MF000001796");
                //txtLabGlobulinaDeseable.Attributes.Add("Tag", "N009-MF000001797");

                //txtLabIndiceAlbGlob.Attributes.Add("Tag", "N009-MF000002174");
                //txtLabIndiceAlbGlobDeseable.Attributes.Add("Tag", "N009-MF000002175");
                //txtLabIndiceRiesgoCoronario.Attributes.Add("Tag", "N009-MF000002176");
                //txtLabIndiceRiesgoCoronarioDeseable.Attributes.Add("Tag", "N009-MF000002177");
                txtLabColinesterasa.Attributes.Add("Tag", "N009-MF000000393");
                txtLabColinesterasaDeseable.Attributes.Add("Tag", "N009-MF000000394");
                //txtLabAntigenoCarcEmbrion.Attributes.Add("Tag", "N009-MF000002178");
                //txtLabAntigenoCarcEmbrionDeseable.Attributes.Add("Tag", "N009-MF000002179");
                //txtLabMetahemoglobina.Attributes.Add("Tag", "N009-MF000002180");
                //txtLabMetahemoglobinaDeseable.Attributes.Add("Tag", "N009-MF000002181");

                //txtLabVitaminaB12.Attributes.Add("Tag", "N009-MF000002182");
                //txtLabVitaminaB12Deseable.Attributes.Add("Tag", "N009-MF000002183");
                //tatLabPRC.Attributes.Add("Tag", "N009-MF000002185");
                //tatLabPRCDeseable.Attributes.Add("Tag", "N009-MF000002185");
                txtLabInsulina.Attributes.Add("Tag", "N009-MF000002186");
                txtLabInsulinaDeseable.Attributes.Add("Tag", "N009-MF000002187");
                //txtLabTiocinatoOrina.Attributes.Add("Tag", "N009-MF000002188");
                //txtLabTiocinatoOrinaDeseable.Attributes.Add("Tag", "N009-MF000002189");
                //txtLabHemoglobGlicos.Attributes.Add("Tag", "N009-MF000002190");
                //txtLabHemoglobGlicosDeseable.Attributes.Add("Tag", "N009-MF000002191");
                //txtLabCA19_9.Attributes.Add("Tag", "N009-MF000002192");
                //txtLabCA19_9Deseable.Attributes.Add("Tag", "N009-MF000002193");
                //txtLabTSH.Attributes.Add("Tag", "N009-MF000002194");
                //txtLabTSHDeseable.Attributes.Add("Tag", "N009-MF000002195");
                //txtLabT4Libre.Attributes.Add("Tag", "N009-MF000002196");
                //txtLabT4LibreDeseable.Attributes.Add("Tag", "N009-MF000002197");
                //txtLabT3Libre.Attributes.Add("Tag", "N009-MF000002198");
                //txtLabT3LibreDeseable.Attributes.Add("Tag", "N009-MF000002199");
                txtBKDirectoMuestra.Attributes.Add("Tag", "N009-MF000001371");
                txtBKDirectoColoracion.Attributes.Add("Tag", "N009-MF000001372");
                txtBKDirectoResultados.Attributes.Add("Tag", "N009-MF000001373");
                ddlGrupoSanguineo.Attributes.Add("Tag", "N009-MF000000262");
                ddlFactorSanguineo.Attributes.Add("Tag", "N009-MF000000263");
                txtLabHemoglobinaSolo.Attributes.Add("Tag", "N009-MF000000265");
                txtLabHemoglobinaSoloDeseable.Attributes.Add("Tag", "N009-MF000000420");
                txtLabHematocritoSolo.Attributes.Add("Tag", "N009-MF000000266");
                txtLabHematocritoSoloDesable.Attributes.Add("Tag", "N009-MF000000421");
                txtLabBetaHcg.Attributes.Add("Tag", "N009-MF000000270");
                txtLabBetaHcgDesable.Attributes.Add("Tag", "N009-MF000001436");
                txtParasitoResultados.Attributes.Add("Tag", "N009-MF000001339");
                txtParasitoObservacion.Attributes.Add("Tag", "N009-MF000002200");
                txtParasitoSeriadoResultados.Attributes.Add("Tag", "N009-MF000001370");
                txtParasitoSeriadoObservacion.Attributes.Add("Tag", "N009-MF000002201");
                txtThenevonResultados.Attributes.Add("Tag", "N009-MF000002202");
                txtThenevonObservacion.Attributes.Add("Tag", "N009-MF000002203");
                //txtLabCoprocultivoResultados.Attributes.Add("Tag", "N009-MF000002204");
                //txtLabCoprocultivoObservacion.Attributes.Add("Tag", "N009-MF000002205");
                //Paneles






                //PanelAc_metilhipurico_xileno.Hidden = true;//("Tag", "N009-ME000000143");
                //PanelArsenico.Hidden = true;//("Tag", "N009-ME000000133");
                //PanelArsenicoenorina.Hidden = true;//("Tag", "N009-ME000000142");
                //PanelBencenoenorina.Hidden = true;//("Tag", "N009-ME000000134");
                //PanelCadmio.Hidden = true;//("Tag", "N009-ME000000132");
                //PanelCadmioenorina.Hidden = true;//("Tag", "N009-ME000000137");
                //PanelCobreenorina.Hidden = true;//("Tag", "N009-ME000000136");
                //PanelCromo.Hidden = true;//("Tag", "N009-ME000000131");
                //PanelCromoenorina.Hidden = true;//("Tag", "N009-ME000000135");
                //PanelMagnesioenorina.Hidden = true;//("Tag", "N009-ME000000141");
                //PanelMercurioenorina.Hidden = true;//("Tag", "N009-ME000000138");
                //PanelMercurioensangre.Hidden = true;//("Tag", "N009-ME000000130");
                //PanelNiquelenorina.Hidden = true;//("Tag", "N009-ME000000140");
                //PanelPlomoenorina.Hidden = true;//("Tag", "N009-ME000000139");
                PanelPlomoensangre.Hidden = true;//("Tag", "N009-ME000000060");
                PanelToxicologicoanfetaminas.Hidden = true;//("Tag", "N009-ME000000043");
                PanelToxicologicobenzodiazepinas.Hidden = true;//("Tag", "N009-ME000000040");
                PanelToxicologicodecocaina.Hidden = true;//("Tag", "N009-ME000000053");
                //PanelToxicologicoextasis.Hidden = true;//("Tag", "N009-ME000000127");
                //PanelToxicologicometanfetaminas.Hidden = true;//("Tag", "N009-ME000000128");
                //PanelXilenoac_metilhipurico.Hidden = true;//("Tag", "N009-ME000000144");


                PanelHemoglobinaHematocrito.Hidden = true;
                PanelToxicologicocarboxihemoglobina.Hidden = true;
                PanelGrupoyfactorsanguineo.Hidden = true;//("Tag", "N009-ME000000000");               
                PanelVdrl.Hidden = true;//("Tag", "N009-ME000000003");
                PanelHavigmhepatitisa.Hidden = true;//("Tag", "N009-ME000000004");
                //PanelAntihepatitisc.Hidden = true;//("Tag", "N009-ME000000005");              
                PanelGlucosa.Hidden = true;//("Tag", "N009-ME000000008");
                PanelAntigenoprostatico.Hidden = true;//("Tag", "N009-ME000000009");
                PanelParasitologicosimple.Hidden = true;//("Tag", "N009-ME000000010");
                PanelColesteroltotal.Hidden = true;//("Tag", "N009-ME000000016");
                PanelTrigliceridos.Hidden = true;//("Tag", "N009-ME000000017");
                PanelAglutinacionesenlamina.Hidden = true;//("Tag", "N009-ME000000025");
                PanelSubunidadbetacualitativo.Hidden = true;//("Tag", "N009-ME000000027");
                PanelCreatinina.Hidden = true;//("Tag", "N009-ME000000028");
                PanelExamendeelisahiv.Hidden = true;//("Tag", "N009-ME000000030");              
                PanelToxicologicocolinesterasa.Hidden = true;//("Tag", "N009-ME000000042");              
                PanelExamencompletodeorina.Hidden = true;//("Tag", "N009-ME000000046");
                PanelParasitologicoseriado.Hidden = true;//("Tag", "N009-ME000000049");              
                PanelTgo.Hidden = true;//("Tag", "N009-ME000000054");
                PanelTgp.Hidden = true;//("Tag", "N009-ME000000055");               
                PanelUrea.Hidden = true;//("Tag", "N009-ME000000073");
                //PanelColesterolhdl.Hidden = true;//("Tag", "N009-ME000000074");
                //PanelColesterolldl.Hidden = true;//("Tag", "N009-ME000000075");
                //PanelColesterolvldl.Hidden = true;//("Tag", "N009-ME000000076");
                PanelBkdirecto.Hidden = true;//("Tag", "N009-ME000000081");
                PanelAcidourico.Hidden = true;//("Tag", "N009-ME000000088");
                PanelHemograma.Hidden = true;//("Tag", "N009-ME000000113");
                //PanelReticulocitos.Hidden = true;//("Tag", "N009-ME000000119");
                PanelVsg.Hidden = true;//("Tag", "N009-ME000000120");
                PanelTiempodecoagulacion.Hidden = true;//("Tag", "N009-ME000000121");
                PanelTiempodesangria.Hidden = true;//("Tag", "N009-ME000000122");
                //PanelTiempoparcialdetromboplastina.Hidden = true;//("Tag", "N009-ME000000123");
                //PanelTiempodeprotombina.Hidden = true;//("Tag", "N009-ME000000124");
                //PanelHepatitisb.Hidden = true;//("Tag", "N009-ME000000125");
                //PanelAglutinacionesentubo.Hidden = true;//("Tag", "N009-ME000000126");               
                //PanelLipidostotales.Hidden = true;//("Tag", "N009-ME000000129");
                //PanelIndicealbumina_globulina.Hidden = true;//("Tag", "N009-ME000000145");
                //PanelIndicederiesgocoronario.Hidden = true;//("Tag", "N009-ME000000146");
                //PanelCeaantigenocarcinoembrionario.Hidden = true;//("Tag", "N009-ME000000147");
                //PanelMetahemoglobina.Hidden = true;//("Tag", "N009-ME000000148");
                //PanelVitaminab12.Hidden = true;//("Tag", "N009-ME000000149");
                //PanelPcr.Hidden = true;//("Tag", "N009-ME000000150");
                PanelInsulina.Hidden = true;//("Tag", "N009-ME000000151");
                //PanelTiocianatoenorina.Hidden = true;//("Tag", "N009-ME000000152");
                //PanelHemoglobinaglicosilada.Hidden = true;//("Tag", "N009-ME000000153");
                //PanelCa19_9.Hidden = true;//("Tag", "N009-ME000000154");
                //PanelTsh.Hidden = true;//("Tag", "N009-ME000000155");
                //PanelT4libre.Hidden = true;//("Tag", "N009-ME000000156");
                //PanelT3libre.Hidden = true;//("Tag", "N009-ME000000157");
                PanelThevenon.Hidden = true;//("Tag", "N009-ME000000158");
                //PanelCoprocultivo.Hidden = true;//("Tag", "N009-ME000000159");
                //PanelProteinastotales.Hidden = true;//("Tag", "N009-ME000000160");
                //PanelAlbumina.Hidden = true;//("Tag", "N009-ME000000161");
                //PanelGlobulina.Hidden = true;//("Tag", "N009-ME000000162");
                //PanelFosfatasaalcalina.Hidden = true;//("Tag", "N009-ME000000163");
                //PanelGgtp.Hidden = true;//("Tag", "N009-ME000000164");
                //PanelBilirrubinatotal.Hidden = true;//("Tag", "N009-ME000000165");
                //PanelBilirrubinadirecta.Hidden = true;//("Tag", "N009-ME000000166");
                //PanelBilirrubinaindirecta.Hidden = true;//("Tag", "N009-ME000000167");






















                //PanelAc_metilhipurico_xileno.Attributes.Add("Tag", "N009-ME000000143");
                //PanelArsenico.Attributes.Add("Tag", "N009-ME000000133");
                //PanelArsenicoenorina.Attributes.Add("Tag", "N009-ME000000142");
                PanelBencenoenorina.Attributes.Add("Tag", "N009-ME000000134");
                //PanelCadmio.Attributes.Add("Tag", "N009-ME000000132");
                //PanelCadmioenorina.Attributes.Add("Tag", "N009-ME000000137");
                //PanelCobreenorina.Attributes.Add("Tag", "N009-ME000000136");
                //PanelCromo.Attributes.Add("Tag", "N009-ME000000131");
                //PanelCromoenorina.Attributes.Add("Tag", "N009-ME000000135");
                //PanelMagnesioenorina.Attributes.Add("Tag", "N009-ME000000141");
                //PanelMercurioenorina.Attributes.Add("Tag", "N009-ME000000138");
                //PanelMercurioensangre.Attributes.Add("Tag", "N009-ME000000130");
                //PanelNiquelenorina.Attributes.Add("Tag", "N009-ME000000140");
                //PanelPlomoenorina.Attributes.Add("Tag", "N009-ME000000139");
                PanelPlomoensangre.Attributes.Add("Tag", "N009-ME000000060");
                PanelToxicologicoanfetaminas.Attributes.Add("Tag", "N009-ME000000043");
                PanelToxicologicobenzodiazepinas.Attributes.Add("Tag", "N009-ME000000040");
                PanelToxicologicodecocaina.Attributes.Add("Tag", "N009-ME000000053");
                //PanelToxicologicoextasis.Attributes.Add("Tag", "N009-ME000000127");
                //PanelToxicologicometanfetaminas.Attributes.Add("Tag", "N009-ME000000128");
                //PanelXilenoac_metilhipurico.Attributes.Add("Tag", "N009-ME000000144");










                PanelToxicologicocarboxihemoglobina.Attributes.Add("Tag", "N002-ME000000042");
                PanelGrupoyfactorsanguineo.Attributes.Add("Tag", "N009-ME000000000");
                PanelVdrl.Attributes.Add("Tag", "N009-ME000000003");
                PanelHavigmhepatitisa.Attributes.Add("Tag", "N009-ME000000004");
                //PanelAntihepatitisc.Attributes.Add("Tag", "N009-ME000000005");
                PanelGlucosa.Attributes.Add("Tag", "N009-ME000000008");
                PanelAntigenoprostatico.Attributes.Add("Tag", "N009-ME000000009");
                PanelParasitologicosimple.Attributes.Add("Tag", "N009-ME000000010");
                PanelColesteroltotal.Attributes.Add("Tag", "N009-ME000000016");
                PanelTrigliceridos.Attributes.Add("Tag", "N009-ME000000017");
                PanelAglutinacionesenlamina.Attributes.Add("Tag", "N009-ME000000025");
                PanelSubunidadbetacualitativo.Attributes.Add("Tag", "N009-ME000000027");
                PanelCreatinina.Attributes.Add("Tag", "N009-ME000000028");
                PanelExamendeelisahiv.Attributes.Add("Tag", "N009-ME000000030");
                PanelToxicologicocolinesterasa.Attributes.Add("Tag", "N009-ME000000042");
                PanelExamencompletodeorina.Attributes.Add("Tag", "N009-ME000000046");
                PanelParasitologicoseriado.Attributes.Add("Tag", "N009-ME000000049");
                PanelTgo.Attributes.Add("Tag", "N009-ME000000054");
                PanelTgp.Attributes.Add("Tag", "N009-ME000000055");
                PanelUrea.Attributes.Add("Tag", "N009-ME000000073");
                //PanelColesterolhdl.Attributes.Add("Tag", "N009-ME000000074");
                //PanelColesterolldl.Attributes.Add("Tag", "N009-ME000000075");
                //PanelColesterolvldl.Attributes.Add("Tag", "N009-ME000000076");
                PanelBkdirecto.Attributes.Add("Tag", "N009-ME000000081");
                PanelAcidourico.Attributes.Add("Tag", "N009-ME000000088");
                PanelHemograma.Attributes.Add("Tag", "N009-ME000000113");
                //PanelReticulocitos.Attributes.Add("Tag", "N009-ME000000119");
                PanelVsg.Attributes.Add("Tag", "N009-ME000000120");
                PanelTiempodecoagulacion.Attributes.Add("Tag", "N009-ME000000121");
                PanelTiempodesangria.Attributes.Add("Tag", "N009-ME000000122");
                //PanelTiempoparcialdetromboplastina.Attributes.Add("Tag", "N009-ME000000123");
                //PanelTiempodeprotombina.Attributes.Add("Tag", "N009-ME000000124");
                //PanelHepatitisb.Attributes.Add("Tag", "N009-ME000000125");
                //PanelAglutinacionesentubo.Attributes.Add("Tag", "N009-ME000000126");
                //PanelLipidostotales.Attributes.Add("Tag", "N009-ME000000129");
                //PanelIndicealbumina_globulina.Attributes.Add("Tag", "N009-ME000000145");
                //PanelIndicederiesgocoronario.Attributes.Add("Tag", "N009-ME000000146");
                //PanelCeaantigenocarcinoembrionario.Attributes.Add("Tag", "N009-ME000000147");
                //PanelMetahemoglobina.Attributes.Add("Tag", "N009-ME000000148");
                //PanelVitaminab12.Attributes.Add("Tag", "N009-ME000000149");
                //PanelPcr.Attributes.Add("Tag", "N009-ME000000150");
                PanelInsulina.Attributes.Add("Tag", "N009-ME000000151");
                //PanelTiocianatoenorina.Attributes.Add("Tag", "N009-ME000000152");
                //PanelHemoglobinaglicosilada.Attributes.Add("Tag", "N009-ME000000153");
                //PanelCa19_9.Attributes.Add("Tag", "N009-ME000000154");
                //PanelTsh.Attributes.Add("Tag", "N009-ME000000155");
                //PanelT4libre.Attributes.Add("Tag", "N009-ME000000156");
                //PanelT3libre.Attributes.Add("Tag", "N009-ME000000157");
                PanelThevenon.Attributes.Add("Tag", "N009-ME000000158");
                //PanelCoprocultivo.Attributes.Add("Tag", "N009-ME000000159");
                //PanelProteinastotales.Attributes.Add("Tag", "N009-ME000000160");
                //PanelAlbumina.Attributes.Add("Tag", "N009-ME000000161");
                //PanelGlobulina.Attributes.Add("Tag", "N009-ME000000162");
                //PanelFosfatasaalcalina.Attributes.Add("Tag", "N009-ME000000163");
                //PanelGgtp.Attributes.Add("Tag", "N009-ME000000164");
                //PanelBilirrubinatotal.Attributes.Add("Tag", "N009-ME000000165");
                //PanelBilirrubinadirecta.Attributes.Add("Tag", "N009-ME000000166");
                //PanelBilirrubinaindirecta.Attributes.Add("Tag", "N009-ME000000167");


                LoadCombosFiltro();
                LoadCombos312();
                LoadCombosOsteo();
                LoadCombosAudiometria();
                LoadCombosPsicologia();
                LoadCombosDermatologico();
                LoadCombosEspirometria();
                LoadCombos7D();
                LoadCombosLaboratorio();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");
                CargarGrillaHistoriaOcupacionalLimpia();
                CargarGrillaHabitosNocivosLimpia();
                CargarGrillaPatologicosPersonalesLimpia();
                CargarGrillaPatologicosFamiliaresLimpia();
                CargarGrillaOrganoLimpia();
            }
         
        }
        #endregion      
            
        #region BANDEJA
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlEmpresaCliente.SelectedValue.ToString() != "-1") Filters.Add("v_CustomerOrganizationId==\"" + ddlEmpresaCliente.SelectedValue + "\"");
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
            Accordion1.Enabled = false;
            ActualizaGrillasDx(null, null);
            ActualizaGrillasRecoYRestri(null);
            var _objData = _serviceBL.GetAllServices(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));
            var ListaComponentePermiso = (List<string>)Session["ComponentesPermisoLectura"];
           var oFiltrado =  _objData.FindAll(p => ListaComponentePermiso.Contains(p.v_ComponentId));
           var oAgrupado = oFiltrado.AsEnumerable()
                         .GroupBy(x => x.v_ServiceId)
                         .Select(group => group.First());

           List<ServiceList> objData1 = oAgrupado.ToList();
            if (_objData.Count == 0)
            {
                TabAnexo312.Hidden = true;
                TabOsteomuscular.Hidden = true;
                TabAudiometria.Hidden = true;
                TabPsicologia.Hidden = true;
                TabDermatologico.Hidden = true;
                TabEspirometria.Hidden = true;
                TabOftalmologia.Hidden = true;
                TabRx.Hidden = true;
                Tab7D.Hidden = true;
                TabOIT.Hidden = true;
                TabAltura18.Hidden = true;
                TabSintomaticoRespiratorio.Hidden = true;
                TabLaboratorio.Hidden = true;
            }
            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return objData1;
        }

        #endregion

        #region FILIACION

        #endregion

        #region ANTECEDENTES OCUPACIONALES
        private void CargarGrillaHistoriaOcupacionalLimpia()
        {
            List<HistoryList> ListaHistoryList = new List<HistoryList>();

            ListaHistoryList.Insert(0, new HistoryList { v_Organization = null, v_TypeActivity = null, v_workstation = null, v_Fechas = null, v_Exposicion = null, v_TiempoTrabajo = null, v_Epps = null, v_HistoryId = null });
            ListaHistoryList.Insert(1, new HistoryList { v_Organization = null, v_TypeActivity = null, v_workstation = null, v_Fechas = null, v_Exposicion = null, v_TiempoTrabajo = null, v_Epps = null, v_HistoryId = null });
            ListaHistoryList.Insert(2, new HistoryList { v_Organization = null, v_TypeActivity = null, v_workstation = null, v_Fechas = null, v_Exposicion = null, v_TiempoTrabajo = null, v_Epps = null, v_HistoryId = null });
            ListaHistoryList.Insert(3, new HistoryList { v_Organization = null, v_TypeActivity = null, v_workstation = null, v_Fechas = null, v_Exposicion = null, v_TiempoTrabajo = null, v_Epps = null, v_HistoryId = null });
            ListaHistoryList.Insert(4, new HistoryList { v_Organization = null, v_TypeActivity = null, v_workstation = null, v_Fechas = null, v_Exposicion = null, v_TiempoTrabajo = null, v_Epps = null, v_HistoryId = null });

            grdDataHistoriaOcupacional.DataSource = ListaHistoryList;
            grdDataHistoriaOcupacional.DataBind();
            Session["ListaHistoryListLimpio"] = ListaHistoryList;
        }
        #endregion

        #region ANTECEDENTES PATOLÓGICOS PERSONALES
        private void CargarGrillaPatologicosPersonalesLimpia()
        {
            List<PersonMedicalHistoryList> ListaPersonMedicalHistory = new List<PersonMedicalHistoryList>();

            ListaPersonMedicalHistory.Insert(0, new PersonMedicalHistoryList { Enfermedad = "HIPERTENSIÓN", v_DiseasesId = "N002-DD000000009", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null});
            ListaPersonMedicalHistory.Insert(1, new PersonMedicalHistoryList { Enfermedad = "DIABETES", v_DiseasesId = "N002-DD000000106", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(2, new PersonMedicalHistoryList { Enfermedad = "ASMA", v_DiseasesId = "N002-DD000000176", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(3, new PersonMedicalHistoryList { Enfermedad = "BRONQUITIS", v_DiseasesId = "N002-DD000000178", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(4, new PersonMedicalHistoryList { Enfermedad = "INFECCIONES", v_DiseasesId = "N002-DD000000214", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(5, new PersonMedicalHistoryList { Enfermedad = "TUBERCULOSIS", v_DiseasesId = "N002-DD000000253", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(6, new PersonMedicalHistoryList { Enfermedad = "HEPATITIS B", v_DiseasesId = "N002-DD000000255", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(7, new PersonMedicalHistoryList { Enfermedad = "TIFOIDEA", v_DiseasesId = "N002-DD000000259", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(8, new PersonMedicalHistoryList { Enfermedad = "ALERGIA", v_DiseasesId = "N002-DD000000272", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(9, new PersonMedicalHistoryList { Enfermedad = "QUEMADURAS", v_DiseasesId = "N009-DD000000145", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(10, new PersonMedicalHistoryList { Enfermedad = "CONVULSIONES", v_DiseasesId = "N009-DD000000166", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(11, new PersonMedicalHistoryList { Enfermedad = "CIRUGIA", v_DiseasesId = "N009-DD000000167", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(12, new PersonMedicalHistoryList { Enfermedad = "INTOXICACIONES", v_DiseasesId = "N009-DD000000168", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(13, new PersonMedicalHistoryList { Enfermedad = "NEOPLASIAS", v_DiseasesId = "N009-DD000000169", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(14, new PersonMedicalHistoryList { Enfermedad = "OTROS", v_DiseasesId = "N009-DD000000614", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(15, new PersonMedicalHistoryList { Enfermedad = "OTROS", v_DiseasesId = "N009-DD000000614", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(16, new PersonMedicalHistoryList { Enfermedad = "OTROS", v_DiseasesId = "N009-DD000000614", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            ListaPersonMedicalHistory.Insert(17, new PersonMedicalHistoryList { Enfermedad = "OTROS", v_DiseasesId = "N009-DD000000614", v_FechaInicio = null, AsociadoTrabajo = false, v_DiagnosticDetail = null, v_TreatmentSite = null, v_PersonMedicalHistoryId = null });
            

            grdDataPatologicoPersonal.DataSource = ListaPersonMedicalHistory;
            grdDataPatologicoPersonal.DataBind();
            Session["ListaPersonMedicalHistoryLimpio"] = ListaPersonMedicalHistory;
        }
        #endregion

        #region HÁBITOS NOCIVOS
        private void CargarGrillaHabitosNocivosLimpia()
        {
            List<NoxiousHabitsList> ListaNoxiousHabitsList = new List<NoxiousHabitsList>();

            ListaNoxiousHabitsList.Insert(0, new NoxiousHabitsList { Habito = "TABAQUISMO", v_Comment = null, i_TypeHabitsId = 1, v_NoxiousHabitsId =null});
            ListaNoxiousHabitsList.Insert(0, new NoxiousHabitsList { Habito = "CONSUMO ALCOHOL", v_Comment = null, i_TypeHabitsId = 2, v_NoxiousHabitsId = null });
            ListaNoxiousHabitsList.Insert(0, new NoxiousHabitsList { Habito = "CONSUMO DE DROGAS", v_Comment = null, i_TypeHabitsId = 3, v_NoxiousHabitsId = null });
            //ListaNoxiousHabitsList.Insert(0, new Sigesoft.Node.WinClient.BE.NoxiousHabitsList { Habito = "ACTIVIDAD FÍSICA", v_Comment = null });

            grdDataHabitosNocivos.DataSource = ListaNoxiousHabitsList;
            grdDataHabitosNocivos.DataBind();
            Session["ListaNoxiousHabitsListLimpio"] = ListaNoxiousHabitsList;

        }
        #endregion

        #region ANTECEDENTES PATOLÓGICOS FAMILIARES
        private void CargarGrillaPatologicosFamiliaresLimpia()
        {
            List<FamilyMedicalAntecedentsList> ListaFamilyMedicalAntecedents = new List<FamilyMedicalAntecedentsList>();

            ListaFamilyMedicalAntecedents.Insert(0, new FamilyMedicalAntecedentsList { i_TypeFamilyId = 1, v_Familia = "PADRE", v_CommentFamili = null, v_FamilyMedicalAntecedentsId=null });
            ListaFamilyMedicalAntecedents.Insert(1, new FamilyMedicalAntecedentsList { i_TypeFamilyId = 2, v_Familia = "MADRE", v_CommentFamili = null, v_FamilyMedicalAntecedentsId = null });
            ListaFamilyMedicalAntecedents.Insert(2, new FamilyMedicalAntecedentsList { i_TypeFamilyId = 3, v_Familia = "HERMANOS", v_CommentFamili = null, v_FamilyMedicalAntecedentsId = null });
            ListaFamilyMedicalAntecedents.Insert(3, new FamilyMedicalAntecedentsList { i_TypeFamilyId = 4, v_Familia = "ESPOSO(A)", v_CommentFamili = null, v_FamilyMedicalAntecedentsId = null });

            grdDataAntecedentesPatologicosFamiliares.DataSource = ListaFamilyMedicalAntecedents;
            grdDataAntecedentesPatologicosFamiliares.DataBind();

            Session["ListaFamilyMedicalAntecedentsLimpio"] = ListaFamilyMedicalAntecedents;
        }
        #endregion

        #region EVALUACIÓN MÉDICA
        private void CargarGrillaOrganoLimpia()
        {
            List<ListOrgano> ListaListOrgano = new List<ListOrgano>();
            ListaListOrgano.Insert(0, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_PIEL_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID, Organo = "PIEL", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(1, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_CABELLO_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID, Organo = "CABELLO", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(2, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_OJOSANEXOS_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID, Organo = "OJO Y ANEXOS", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(3, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_OIDOS_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID, Organo = "OÍDOS", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(4, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_NARIZ_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID, Organo = "NARIZ", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(5, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_BOCA_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID, Organo = "BOCA", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(6, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_FARINGE_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID, Organo = "FARINGE", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(7, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_CUELLO_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID, Organo = "CUELLO", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(8, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID, Organo = "PULMONES", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(9, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID, Organo = "CARDIO-VASCULAR", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(10, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID, Organo = "ABDOMEN", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(11, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_GENITOURINARIO_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID, Organo = "APARATO GENITOURINARIO", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(12, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID, Organo = "APARATO LOCOMOTOR", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(13, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_MARCHA_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID, Organo = "MARCHA", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(14, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_COLMNA_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID, Organo = "COLUMNA", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(15, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID, Organo = "MIEMBROS SUPERIORES", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(16, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID, Organo = "MIEMBROS INFERIORES", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(17, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_LINFATICOS_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID, Organo = "GANGLIOS", SinHallazgo = true, Hallazgos = "" });
            ListaListOrgano.Insert(18, new ListOrgano { v_ComponentFieldId_1 = Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID, v_ComponentFieldId_2 = Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID, Organo = "SISTEMA NERVIOSO", SinHallazgo = true, Hallazgos = "" });

            Session["ListaListOrgano"] = ListaListOrgano;
            grdDataOrgano.DataSource = ListaListOrgano;
            grdDataOrgano.DataBind();
        }
        #endregion
        
        #region EVENTOS
        private void LoadCombos312()
        {
            OperationResult objOperationResult = new OperationResult();
            // 312
            Utils.LoadDropDownList(ddlDocumento, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 106), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDepartamento, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDepartamento(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProvincia, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDistrito, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlResideSiNo, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTipoSeguro, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 188), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEstadoCivil, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 101), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlGradoInstruccion, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 108), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMac, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 134), DropDownListAction.Select);
          
        }

        private void LoadCombosOsteo()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            var Combo253 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 253);
            var Combo183 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 183);
            var Combo229 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 229);
            var Combo203 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 203);
            var Combo254 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 254);
            //OSTEOMUSCULAR
            Utils.LoadDropDownList(ddlNuca1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNuca2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNuca3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHombroDerecho1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroDerecho2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroDerecho3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHombroIzquierdo1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroIzquierdo2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHombroIzquierdo3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAmbosHombros1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbosHombros2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbosHombros3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCodoDerecho1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoDerecho2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoDerecho3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCodoIzquierdo1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoIzquierdo2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoIzquierdo3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAmboscodos1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmboscodos2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmboscodos3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlManosDerecha1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosDerecha2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosDerecha3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlManosIzquierda1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosIzquierda2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlManosIzquierda3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlAmbasManos1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbasManos2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbasManos3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlColumnadorsal1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnadorsal2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnadorsal3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlColumnaLumbar1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnaLumbar2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlColumnaLumbar3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaDerecha1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaDerecha2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaDerecha3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaIzquierda1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaIzquierda2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaIzquierda3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaDerecha1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaDerecha2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaDerecha3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaIzquierda1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaIzquierda2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaIzquierda3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobillosDerecho1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosDerecho2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosDerecho3, "Value1", "Id", Combo111, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobillosIzquierdo1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosIzquierdo2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobillosIzquierdo3, "Value1", "Id", Combo111, DropDownListAction.Select);



            Utils.LoadDropDownList(ddlHD1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHD8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlHI1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHI8, "Value1", "Id",Combo253, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlCD1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCD8, "Value1", "Id",Combo253, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlCI1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCI8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMuneD1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneD8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMuneI1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMuneI8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaD1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaD8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCaderaI1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCaderaI8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobilloD1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloD8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlTobilloI1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTobilloI8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaD1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaD8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlRodillaI1, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI2, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI3, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI4, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI5, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI6, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI7, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRodillaI8, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCervical, "Value1", "Id", Combo183, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsal, "Value1", "Id", Combo183, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLumbar, "Value1", "Id", Combo183, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlDorsalEjeLateral, "Value1", "Id", Combo229, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLumbarEjeLateral, "Value1", "Id", Combo229, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCervicalFlexion, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalExtension, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalLatDere, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalLatIzq, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalRotaDere, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalRotaIzq, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCervicalIrradiacion, "Value1", "Id",Combo253, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDorsoFlexion, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoExtension, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoLateDere, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoLateIzq, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoRotaDere, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoRotaIzq, "Value1", "Id",Combo253, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDorsoIrradiacion, "Value1", "Id",Combo253, DropDownListAction.Select);


            Utils.LoadDropDownList(ddlLasegueDere, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLasegueIzq, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSchoberDere, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSchoberIzq, "Value1", "Id", Combo203, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlCodoDerecho, "Value1", "Id", Combo254, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCodoIzquierdo, "Value1", "Id", Combo254, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPieDerecho, "Value1", "Id", Combo254, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPieIzquierdo, "Value1", "Id", Combo254, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlPhalenDerecha, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPhalenIzquierda, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTinelDerecha, "Value1", "Id", Combo203, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTinelIzquierda, "Value1", "Id", Combo203, DropDownListAction.Select);
        }

        private void LoadCombosAudiometria()
        {
            OperationResult objOperationResult = new OperationResult();

            //AUDIOMETRÍA
            var Combo250 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 250);
            var Combo251 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 251);
            var Combo252 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 252);
            var Combo249 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 249);

            Utils.LoadDropDownList(ddlTipoRuido, "Value1", "Id", Combo250, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlHorasPorDia, "Value1", "Id", Combo251, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTapones, "Value1", "Id", Combo252, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrejeras, "Value1", "Id", Combo252, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAmbos, "Value1", "Id", Combo252, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAU_Condicion, "Value1", "Id", Combo249, DropDownListAction.Select);
        }

        private void LoadCombosPsicologia()
        {
            OperationResult objOperationResult = new OperationResult();
            //PSICOLOGÍA
            var Combo175 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 175);
            var Combo173 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 173);
            var Combo214 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 214);
            var Combo215 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 215);
            var Combo216 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 216);
            var Combo189 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 189);
            var Combo179 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 179);

            Utils.LoadDropDownList(ddlPresentacion, "Value1", "Id", Combo175, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPostura, "Value1", "Id", Combo173, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDisRitmo, "Value1", "Id", Combo214, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDisTono, "Value1", "Id", Combo215, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDisArticulacion, "Value1", "Id", Combo216, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrientacionEspacio, "Value1", "Id", Combo189, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrientacionTiempo, "Value1", "Id", Combo189, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrientacionPersona, "Value1", "Id", Combo189, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMemoria, "Value1", "Id", Combo179, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInteligencia, "Value1", "Id", Combo179, DropDownListAction.Select);

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
            
        }

        private void LoadCombosEspirometria()
        {
            OperationResult objOperationResult = new OperationResult();
            var Combo111 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111);
            //Espirometría
            Utils.LoadDropDownList(ddlEspiroCuestionario1Pregunta1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario1Pregunta2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario1Pregunta3, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario1Pregunta4, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario1Pregunta5, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesHemoptisis, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesInfarto, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesPneumotorax, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesInestabilidad, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesTraqueostomia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesFiebre, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesSonda, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesEmbarazo, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesAneurisma, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesEmbarazoComplicado, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAntecedentesEmbolia, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta1, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta2, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta3, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta4, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta5, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta6, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEspiroCuestionario2Pregunta7, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlOrigenEtnico, "Value1", "Id", Combo111, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTabaquismo, "Value1", "Id", Combo111, DropDownListAction.Select);
        }

        private void LoadCombos7D()
        {
            OperationResult objOperationResult = new OperationResult();

            //7D
            Utils.LoadDropDownList(ddl7DConclusion, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 163), DropDownListAction.Select);
        }

        private void LoadCombosLaboratorio()
        {
            OperationResult objOperationResult = new OperationResult();
            //var Combo152 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 152);
            var ComboGrupo = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 154);
            var Combofactor = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 155);

            Utils.LoadDropDownList(ddlGrupoSanguineo, "Value1", "Id", ComboGrupo, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFactorSanguineo, "Value1", "Id", Combofactor, DropDownListAction.Select);

            //Lab
            //Utils.LoadDropDownList(ddlLabVDRL, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabHepatitisB, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabHAVIgM, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabHIV, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabAntiHepC, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabTificoO, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabTificoO, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabTificoH, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabParatificoA, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabParatificoB, "Value1", "Id", Combo152, DropDownListAction.Select);

            //Utils.LoadDropDownList(ddlLabBrucella, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlLabTuboTifico, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlTuboTificoH, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlTuboParatificoA, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlTuboParatificoB, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlTuboBrucella, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlToxiCocaina, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlToxiMarihuana, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlToxiExtasis, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlToxiBenzodiac, "Value1", "Id", Combo152, DropDownListAction.Select);

            //Utils.LoadDropDownList(ddlToxiAnfetam, "Value1", "Id", Combo152, DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlToxiMetanfeta, "Value1", "Id", Combo152, DropDownListAction.Select);


        }

        private void LoadCombosFiltro()
        {
            OperationResult objOperationResult = new OperationResult();
            OrganizationBL oOrganizationBL = new OrganizationBL();
            Utils.LoadDropDownList(ddlEmpresaCliente, "Value1", "Id", oOrganizationBL.GetAllOrganizations(ref objOperationResult), DropDownListAction.All);
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);
            
        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {
            TabAnexo312.Hidden = true;
            TabOsteomuscular.Hidden = true;
            TabAudiometria.Hidden = true;
            TabPsicologia.Hidden = true;
            TabDermatologico.Hidden = true;
            TabEspirometria.Hidden = true;
            TabOftalmologia.Hidden = true;
            TabRx.Hidden = true;
            Tab7D.Hidden = true;
            TabOIT.Hidden = true;
            TabAltura18.Hidden = true;
            TabSintomaticoRespiratorio.Hidden = true;
            TabLaboratorio.Hidden = true;

            LlenarLista();
            int ProfesionId = int.Parse(((ClientSession)Session["objClientSession"]).i_ProfesionId.Value.ToString());
              
            int index = e.RowIndex;
            var dataKeys = grdData.DataKeys[index];
            Session["ServiceId"] = dataKeys[0].ToString();
            Session["PersonId"] = dataKeys[1].ToString();
            ViewState["i_AptitudeStatusId"] = dataKeys[2].ToString();
            var genero = dataKeys[4].ToString();
            if (genero.ToUpper() == "FEMENINO")
            {
                PanelGinecologico.Enabled = true;
            }
            else
            {
                PanelGinecologico.Enabled = false;
            }

            //Pintar los examenes correpondientes por servicio
            if (ProfesionId == (int)TipoProfesional.Auditor)
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());
                foreach (var item in ListaComponentes)
                {
                    //if (item.ComponentId == TabAnexo312.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombos312();
                    //    //TabAnexo312.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabOsteomuscular.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombosOsteo();
                    //    //TabOsteomuscular.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabAudiometria.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombosAudiometria();
                    //    //TabAudiometria.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabPsicologia.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombosPsicologia();
                    //    //TabPsicologia.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabDermatologico.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombosDermatologico();
                    //    //TabDermatologico.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabEspirometria.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombosEspirometria();
                    //    //TabEspirometria.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabOftalmologia.Attributes.GetValue("Tag").ToString())
                    //{

                    //    //TabOftalmologia.Hidden = false;
                    //}
                     if (item.ComponentId == TabRx.Attributes.GetValue("Tag").ToString())
                    {

                        TabRx.Hidden = false;
                    }
                    //else if (item.ComponentId == Tab7D.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombos7D();
                    //    //Tab7D.Hidden = false;
                    //}
                    else if (item.ComponentId == TabOIT.Attributes.GetValue("Tag").ToString())
                    {

                        TabOIT.Hidden = false;
                    }
                    //else if (item.ComponentId == TabAltura18.Attributes.GetValue("Tag").ToString())
                    //{

                    //    //TabAltura18.Hidden = false;
                    //}
                    //else if (item.ComponentId == TabSintomaticoRespiratorio.Attributes.GetValue("Tag").ToString())
                    //{

                    //    //TabSintomaticoRespiratorio.Hidden = false;
                    //}
                    else if (item.ComponentId == TabLaboratorio.Attributes.GetValue("Tag").ToString())
                    {
                        //LoadCombosLaboratorio();
                        TabLaboratorio.Hidden = false;
                    }
                }
            }
            else
            {
                var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());

                var ListaComponenentesConPermiso = (List<string>)Session["ComponentesPermisoLectura"];

                foreach (var item in ListaComponentes)
                {
                    //if (item == TabAnexo312.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //LoadCombos312();
                    //    //TabAnexo312.Hidden = false;
                    //}
                    //else if (item == TabOsteomuscular.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabOsteomuscular.Hidden = false;
                    //}
                    //else if (item == TabAudiometria.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabAudiometria.Hidden = false;
                    //}
                    //else if (item == TabPsicologia.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabPsicologia.Hidden = false;
                    //}
                    //else if (item == TabDermatologico.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabDermatologico.Hidden = false;
                    //}
                    //else if (item == TabEspirometria.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabEspirometria.Hidden = false;
                    //}
                    //else if (item == TabOftalmologia.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabOftalmologia.Hidden = false;
                    //}
                    if (item.ComponentId == TabRx.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.RX_TORAX_ID);
                       if (Resultado != null)
                        {
                            TabRx.Hidden = false;
                        }
                       
                    }
                    //else if (item == Tab7D.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //Tab7D.Hidden = false;
                    //}
                    else if (item.ComponentId == TabOIT.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.OIT_ID);
                         if (Resultado != null)
                         {
                             TabOIT.Hidden = false;
                         }
                    }
                    //else if (item == TabAltura18.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabAltura18.Hidden = false;
                    //}
                    //else if (item == TabSintomaticoRespiratorio.Attributes.GetValue("Tag").ToString())
                    //{
                    //    //TabSintomaticoRespiratorio.Hidden = false;
                    //}
                    else if (item.ComponentId == TabLaboratorio.Attributes.GetValue("Tag").ToString())
                    {
                        var Resultado = ListaComponenentesConPermiso.Find(p => p.ToString() == Constants.INFORME_LABORATORIO_ID);
                         if (Resultado != null)
                         {
                             TabLaboratorio.Hidden = false;
                         }
                    }
                }
            }

            Accordion1.Enabled = true;

            ObtenerDatosAnexo312(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
        }

        private void ObtenerDatosAnexo312(string pServiceId, string pPersonId)
        {

            OperationResult objOperationResult = new OperationResult();

            //Cargar Datos de Rx
            var oExamenRx = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 6);

            var objExamenRX = _serviceBL.GetServiceComponentFields(oExamenRx == null ? "" : oExamenRx.ServicioComponentId, pServiceId);
            Session["ServicioComponentIdExamenRx"] = oExamenRx == null ? null : oExamenRx.ServicioComponentId;
            if (objExamenRX.Count != 0)
            {
                SearchControlAndLoadData(TabRx, oExamenRx.ServicioComponentId, objExamenRX);
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabRx, _tmpServiceComponentsForBuildMenuList);
            }
            //Cargar Datos de OIT
            var oExamenOIT = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 6);

            var objExamenOIT = _serviceBL.GetServiceComponentFields(oExamenOIT == null ? "" : oExamenOIT.ServicioComponentId, pServiceId);
            Session["ServicioComponentIdExamenOIT"] = oExamenOIT == null ? null : oExamenOIT.ServicioComponentId;
            if (objExamenOIT.Count != 0)
            {

                SearchControlAndLoadData(TabOIT, oExamenOIT.ServicioComponentId, objExamenOIT);
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabOIT, _tmpServiceComponentsForBuildMenuList);
            }
            //Cargar Datos de Laboratorio
            var oExamenLaboratorio = _serviceBL.ObtenerIdsParaImportacion_(pServiceId, 1);

            var objExamenLaboratorio = _serviceBL.GetServiceComponentFields(oExamenLaboratorio == null ? "" : oExamenLaboratorio.ServicioComponentId, pServiceId);
            var Examenes = _serviceBL.DevolverExamenesPorCategoria(Session["ServiceId"].ToString(), 1);
            SearchControlAndShow(TabLaboratorio, Examenes);
            Session["ServicioComponentIdExamenLaboratorio"] = oExamenLaboratorio == null ? null : oExamenLaboratorio.ServicioComponentId;
            if (objExamenLaboratorio.Count != 0)
            {
                SearchControlAndLoadData(TabLaboratorio, oExamenLaboratorio.ServicioComponentId, objExamenLaboratorio);
            }
            else
            {
                var _tmpServiceComponentsForBuildMenuList = new ServiceBL().ObtenerValoresPorDefecto(ref objOperationResult, pServiceId);
                SearchControlAndClean(TabLaboratorio, _tmpServiceComponentsForBuildMenuList);
            }
            #region MyRegion

            var Obj312 = _serviceBL.RetornarAnexo312(pServiceId, pPersonId);
            // //Filiación
            //txtDia.Text =Obj312[0].FechaNacimiento== null?"": Obj312[0].FechaNacimiento.Value.Day.ToString();
            //txtMes.Text = Obj312[0].FechaNacimiento == null ? "" : Obj312[0].FechaNacimiento.Value.Month.ToString();
            //txtAnio.Text = Obj312[0].FechaNacimiento == null ? "" : Obj312[0].FechaNacimiento.Value.Year.ToString();
            //txtEdad.Text = _serviceBL.GetAge(Obj312[0].FechaNacimiento.Value).ToString();
            //ddlDocumento.SelectedValue =Obj312[0].TipoDocumentoId== null?"-1": Obj312[0].TipoDocumentoId.Value.ToString();
            //txtNroDocumento.Text = Obj312[0].NroDocumento == null ? "" : Obj312[0].NroDocumento.ToString();
            //txtDireccionFiscal.Text = Obj312[0].DireccionFiscal == null ? "" : Obj312[0].DireccionFiscal.ToString().ToUpper();
            //ddlDepartamento.SelectedValue  =Obj312[0].DepartamentoId== null?"-1":Obj312[0].DepartamentoId.Value.ToString();
            //Utils.LoadDropDownList(ddlProvincia, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamento.SelectedValue.ToString())), DropDownListAction.Select);
            //ddlProvincia.SelectedValue = Obj312[0].ProvinciaId == null ? "-1" : Obj312[0].ProvinciaId.Value.ToString();
            //Utils.LoadDropDownList(ddlDistrito, "Value1", "Id", _objDataHierarchyBL.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvincia.SelectedValue.ToString())), DropDownListAction.Select);       
            //ddlDistrito.SelectedValue = Obj312[0].DistritoId == null ? "-1" : Obj312[0].DistritoId.Value.ToString();
            //ddlResideSiNo.SelectedValue = Obj312[0].ResideLugarTrabajo == null ? "-1" : Obj312[0].ResideLugarTrabajo.Value.ToString();
            //txtTiempoResidenci.Text = Obj312[0].TiempoResidencia == null ? "" : Obj312[0].TiempoResidencia.ToString();
            //txtTelefonos.Text = Obj312[0].Telefono == null ? "" : Obj312[0].Telefono.ToString();
            //ddlTipoSeguro.SelectedValue = Obj312[0].TipoSeguroId == null ? "-1" : Obj312[0].TipoSeguroId.Value.ToString();
            //txtEmail.Text = Obj312[0].Email == null ? "" : Obj312[0].Email.ToString().ToUpper();
            //ddlEstadoCivil.SelectedValue = Obj312[0].EstadoCivilId == null?"-1": Obj312[0].EstadoCivilId.Value.ToString();
            //ddlGradoInstruccion.SelectedValue = Obj312[0].GradoInstruccionId == null ? "-1" : Obj312[0].GradoInstruccionId.Value.ToString();
            //txtNroHijosVivos.Text = Obj312[0].HijosVivos== null? "": Obj312[0].HijosVivos.Value.ToString();
            //txtNroHijosMuertos.Text = Obj312[0].HijosMuertos == null ? "" : Obj312[0].HijosMuertos.Value.ToString();
            //txtAnamnesis.Text = Obj312[0].Anamnesis;
            //txtMenarquia.Text = Obj312[0].v_Menarquia;
            //txtGestacion.Text = Obj312[0].v_Gestapara;
            //txtFum.Text = Obj312[0].v_FechaUltimaRegla;
            //ddlMac.SelectedValue =  Obj312[0].i_MacId.ToString();
            //txtRegimenCatamenial.Text = Obj312[0].v_CatemenialRegime;
            //txtCirugiaGineco.Text = Obj312[0].v_CiruGine;
            //txtUltimoPAP.Text = Obj312[0].v_FechaUltimoPAP;
            //txtResultadoPAP.Text = Obj312[0].v_ResultadoMamo;
            //txtUltimaMamo.Text = Obj312[0].v_FechaUltimaMamo;
            //txtResultadoMamo.Text = Obj312[0].v_ResultadosPAP;
            ddlAptitud.SelectedValue = Obj312[0].i_StatusAptitud.ToString();
            txtComentarioAptitud.Text = Obj312[0].Comentario == null ? "" : Obj312[0].Comentario.ToString();
            //txtComentarioMedico.Text = Obj312[0].ComentarioMedico== null ? "" :Obj312[0].ComentarioMedico.ToString();
            //txtRestriccionesAptitud.Text = Obj312[0].Restricciones == null ? "" : Obj312[0].Restricciones.ToString();
            // //Historia Ocupacional
            //  List<HistoryList> ListaHistoryList = new List<HistoryList>();
            //  var ListaHistoryListLimpio = (List<HistoryList>)Session["ListaHistoryListLimpio"];

            //  int Contador = 0;
            //  foreach (var item in ListaHistoryListLimpio)
            //  {
            //      if (Obj312[0].ListaHistoriaOcupacional.Count>Contador)
            //      {
            //          item.v_Organization = Obj312[0].ListaHistoriaOcupacional[Contador].v_Organization;
            //          item.v_TypeActivity = Obj312[0].ListaHistoriaOcupacional[Contador].v_TypeActivity;
            //          item.v_workstation = Obj312[0].ListaHistoriaOcupacional[Contador].v_workstation;
            //          item.v_Fechas = Obj312[0].ListaHistoriaOcupacional[Contador].v_Fechas;
            //          item.v_Exposicion = Obj312[0].ListaHistoriaOcupacional[Contador].v_Exposicion;
            //          item.v_Epps = Obj312[0].ListaHistoriaOcupacional[Contador].v_Epps;
            //          item.v_TiempoTrabajo = Obj312[0].ListaHistoriaOcupacional[Contador].v_TiempoTrabajo;
            //          item.v_HistoryId = Obj312[0].ListaHistoriaOcupacional[Contador].v_HistoryId;
            //      }
            //      Contador++;
            //  }
            //  grdDataHistoriaOcupacional.DataSource = ListaHistoryListLimpio;
            //  grdDataHistoriaOcupacional.DataBind();


            //  //Medicos Personales        
            //  var ListaPersonMedicalHistoryLimpio = (List<PersonMedicalHistoryList>)Session["ListaPersonMedicalHistoryLimpio"];

            //  foreach (var item in ListaPersonMedicalHistoryLimpio)
            //  {
            //      foreach (var item1 in Obj312[0].ListaMedicosPersonales)
            //      {
            //          if (item1.v_DiseasesId == item.v_DiseasesId)
            //          {
            //              item.Enfermedad = item.Enfermedad;
            //              item.v_FechaInicio = item1.v_FechaInicio;
            //              item.AsociadoTrabajo = item1.i_AsociadoTrabajo == 0 ? false : true;
            //              item.v_DiagnosticDetail = item1.v_DiagnosticDetail;
            //              item.v_TreatmentSite = item1.v_TreatmentSite;
            //              item.v_PersonMedicalHistoryId = item1.v_PersonMedicalHistoryId;
            //          }
            //      }
            //  }
            //  grdDataPatologicoPersonal.DataSource = ListaPersonMedicalHistoryLimpio;
            //  grdDataPatologicoPersonal.DataBind();


            // //Habitos Nocivos

            //  var ListaNoxiousHabitsListLimpio = (List<NoxiousHabitsList>)Session["ListaNoxiousHabitsListLimpio"];

            //  foreach (var item in ListaNoxiousHabitsListLimpio)
            //  {
            //      foreach (var item1 in Obj312[0].ListaHabitosNosivos)
            //      {
            //          if (item1.i_TypeHabitsId == item.i_TypeHabitsId)
            //          {
            //              item.Habito = item.Habito;
            //              item.i_FrequencyId = int.Parse(item1.i_FrequencyId.ToString());
            //              item.v_Comment = item1.v_Comment;
            //              item.v_NoxiousHabitsId = item1.v_NoxiousHabitsId;
            //          }
            //      }
            //  }
            //  grdDataHabitosNocivos.DataSource = ListaNoxiousHabitsListLimpio;
            //  grdDataHabitosNocivos.DataBind();


            //  //Medicos Familiares

            //  var ListaFamilyMedicalAntecedentsLimpio = (List<FamilyMedicalAntecedentsList>)Session["ListaFamilyMedicalAntecedentsLimpio"];


            //  foreach (var item in ListaFamilyMedicalAntecedentsLimpio)
            //  {
            //      foreach (var item1 in Obj312[0].ListaMedicosFamiliares)
            //      {
            //          if (item1.i_TypeFamilyId == item.i_TypeFamilyId)
            //          {
            //              item.v_Familia = item.v_Familia;
            //              item.v_CommentFamili = item1.v_CommentFamili;
            //              item.v_FamilyMedicalAntecedentsId = item1.v_FamilyMedicalAntecedentsId;
            //          }
            //      }
            //  }
            //  grdDataAntecedentesPatologicosFamiliares.DataSource = ListaFamilyMedicalAntecedentsLimpio;
            //  grdDataAntecedentesPatologicosFamiliares.DataBind();

            // //Evaluación Triaje
            // var o1 = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 10);
            // ViewState["ServicioComponentIdTriaje"] = o1.ServicioComponentId;
            // var objTriaje = _serviceBL.GetServiceComponentFields(o1 == null ? "" : o1.ServicioComponentId, pServiceId);

            // if (objTriaje.Count !=0)
            // {
            //     txtTalla.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID) == null ?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_TALLA_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtPeso.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID) == null ?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PESO_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtImc.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID) == null ?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_IMC_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtIcc.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID) == null ? "": objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).ServiceComponentFieldValues[0].v_Value1;

            //     txtfres.Text =  objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID) == null ?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtFcar.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID)== null?"": objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtParterial.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID) == null?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_PAS_ID).ServiceComponentFieldValues[0].v_Value1;

            //     txtTemp.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_TEMPERATURA_ID) == null?"": objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_TEMPERATURA_ID).ServiceComponentFieldValues[0].v_Value1;

            //     txtPcadera.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID) == null?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtPadb.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID) == null?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtGcorporal.Text =objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID)== null?"": objTriaje.Find(p => p.v_ComponentFieldsId == Constants.ANTROPOMETRIA_PORCENTAJE_GRASA_CORPORAL_ID).ServiceComponentFieldValues[0].v_Value1;
            //     txtSatO2.Text = objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_SAT_O2_ID) == null?"":objTriaje.Find(p => p.v_ComponentFieldsId == Constants.FUNCIONES_VITALES_SAT_O2_ID).ServiceComponentFieldValues[0].v_Value1;

            // }

            // //Evaluación Examen Físico
            // var oExamenFisico = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 11);
            // ViewState["ServicioComponentIdExamenFisico"] = oExamenFisico.ServicioComponentId;
            // var objExamenFisico = _serviceBL.GetServiceComponentFields(oExamenFisico== null ?"" :oExamenFisico.ServicioComponentId, pServiceId);
            // if (objExamenFisico.Count !=0)
            // {
            //     var x = (List<ListOrgano>)Session["ListaListOrgano"];

            //     var prueba = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID);

            //     x[0].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_PIEL_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[0].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_PIEL_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[1].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CABELLO_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[1].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CABELLO_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[2].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_OJOSANEXOS_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[2].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_OJOSANEXOS_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[3].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_OIDOS_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[3].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_OIDOS_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[4].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_NARIZ_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[4].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_NARIZ_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[5].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_BOCA_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[5].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_BOCA_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[6].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_FARINGE_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[6].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_FARINGE_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[7].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CUELLO_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[7].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CUELLO_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[8].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATORESPIRATORIO_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[8].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_RESPIRATORIO_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[9].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[9].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_CARDIO_VASCULAR_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[10].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[10].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_DIGESTIVO_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[11].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_GENITOURINARIO_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[11].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_GENITOURINARIO_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[12].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[12].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_APARATO_LOCOMOTOR_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[13].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_MARCHA_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[13].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_MARCHA_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[14].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_COLMNA_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[14].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_COLUMNA_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[15].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_EXTREMIDADE_SUPERIORES_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[15].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_EXTREMIDADES_SUPERIORES_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[16].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[16].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_EXTREMIDADES_INFERIORES_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[17].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_LINFATICOS_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[17].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_LINFATICOS_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     x[18].SinHallazgo = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_ID).ServiceComponentFieldValues[0].v_Value1 == "2" ? false : true;
            //     x[18].Hallazgos = objExamenFisico.Find(p => p.v_ComponentFieldsId == Constants.EXAMEN_FISICO_SISTEMA_NERVIOSO_DESCRIPCION_ID).ServiceComponentFieldValues[0].v_Value1;

            //     grdDataOrgano.DataSource = x;
            //     grdDataOrgano.DataBind();

            //     //Cargar Datos de Ostemuscular
            //     SearchControlAndLoadData(TabOsteomuscular, oExamenFisico.ServicioComponentId, objExamenFisico);

            // }

            ////Cargar Datos de Psico
            //var oExamenPsico = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 7);

            //var objExamenPsico = _serviceBL.GetServiceComponentFields(oExamenPsico == null ? "" : oExamenPsico.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamenPsico"] = oExamenPsico == null ? null : oExamenPsico.ServicioComponentId;
            //if (objExamenPsico.Count !=0)
            //{

            //    SearchControlAndLoadData(TabPsicologia, oExamenPsico.ServicioComponentId, objExamenPsico);
            //}

            ////Cargar Datos de Tamizaje
            //var oExamenTamizaje = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 11);

            //var objExamenTamizaje = _serviceBL.GetServiceComponentFields(oExamenTamizaje == null ? "" : oExamenTamizaje.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamenTamizaje"] = oExamenTamizaje == null ? null : oExamenTamizaje.ServicioComponentId;
            //if (objExamenTamizaje.Count != 0)
            //{

            //    SearchControlAndLoadData(TabDermatologico, oExamenTamizaje.ServicioComponentId, objExamenTamizaje);
            //}

            ////Cargar Datos de Espirometría
            //var oExamenEspirometria= _serviceBL.ObtenerIdsParaImportacion(pServiceId, 16);

            //var objExamenEspirometria = _serviceBL.GetServiceComponentFields(oExamenEspirometria == null ? "" : oExamenEspirometria.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamenEspirometria"] = oExamenEspirometria == null ? null : oExamenEspirometria.ServicioComponentId;
            //if (objExamenEspirometria.Count != 0)
            //{

            //    SearchControlAndLoadData(TabEspirometria, oExamenEspirometria.ServicioComponentId, objExamenEspirometria);
            //}

            ////Cargar Datos de Oftalmología
            //var oExamenOftalmologia = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 14);

            //var objExamenOftalmologia = _serviceBL.GetServiceComponentFields(oExamenOftalmologia == null ? "" : oExamenOftalmologia.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamenOftalmologia"] = oExamenOftalmologia == null ? null : oExamenOftalmologia.ServicioComponentId;
            //if (objExamenOftalmologia.Count != 0)
            //{

            //    //Session["objExamenOftalmologia"] = objExamenOftalmologia;
            //    SearchControlAndLoadData(TabOftalmologia, oExamenOftalmologia.ServicioComponentId, objExamenOftalmologia);
            //}


            ////Cargar Datos de 7D
            //var oExamen7D = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 11);

            //var objExamen7D = _serviceBL.GetServiceComponentFields(oExamen7D == null ? "" : oExamen7D.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamen7D"] = oExamen7D == null ? null : oExamen7D.ServicioComponentId;
            //if (objExamenRX.Count != 0)
            //{

            //    SearchControlAndLoadData(Tab7D, oExamen7D.ServicioComponentId, objExamenRX);
            //}



            ////Cargar Datos de Altura 18
            //var oExamenAltura18 = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 11);

            //var objExamenAltura18 = _serviceBL.GetServiceComponentFields(oExamenAltura18 == null ? "" : oExamenAltura18.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamenAltura18"] = oExamenAltura18 == null ? null : oExamenAltura18.ServicioComponentId;
            //if (objExamenAltura18.Count != 0)
            //{

            //    SearchControlAndLoadData(TabAltura18, oExamenAltura18.ServicioComponentId, objExamenAltura18);
            //}

            ////Cargar Datos de Simtomatico
            //var oExamenSintomatico = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 11);

            //var objExamenSintomatico = _serviceBL.GetServiceComponentFields(oExamenSintomatico == null ? "" : oExamenSintomatico.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdExamenSintomaticoResp"] = oExamenSintomatico== null?null:oExamenSintomatico.ServicioComponentId;
            //if (objExamenSintomatico.Count != 0)
            //{

            //    SearchControlAndLoadData(TabSintomaticoRespiratorio, oExamenSintomatico.ServicioComponentId, objExamenSintomatico);
            //}



            //  //Evaluación Audiometría
            //var oAudiometria = _serviceBL.ObtenerIdsParaImportacion(pServiceId, 15);

            //var objAudiometria = _serviceBL.GetServiceComponentFields(oAudiometria == null ? "" : oAudiometria.ServicioComponentId, pServiceId);
            //ViewState["ServicioComponentIdAudimetria"] = oAudiometria == null ? null : oAudiometria.ServicioComponentId;
            //if (objAudiometria.Count != 0)
            //{

            //    ddlAU_Condicion.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001378") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001378").ServiceComponentFieldValues[0].v_Value1;
            //    txtAU_Observaciones.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000000178") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000000178").ServiceComponentFieldValues[0].v_Value1;

            //    chkSupuracion.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000089") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000089").ServiceComponentFieldValues[0].v_Value1 == "0"?false:true;

            //    chkVertigo.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000090") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000090").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //     chkOtitis.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000091") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000091").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkParatodiditis.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000092") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000092").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkMeningitis.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000093") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000093").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkGolpesCefalicos.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000094") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000094").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkParalisisFacial.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000095") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000095").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkTTOANTITBC.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000096") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000096").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkTTOOtotoxicos.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000097") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000097").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkConsumoMedicamento.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000098") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000098").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkExposicionSolventes.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000099") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000099").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;

            //    chkRuidoExtra.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000100") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000100").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkRuidoLaboral.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000101") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000101").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkServicioMilitar.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001299") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001299").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkDeportesAereos.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001300") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001300").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkDeportesSubmarinos.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001301") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001301").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkManipulacionArmas.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001302") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001302").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkExposicionMusica.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001303") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001303").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkUsoAudifonos.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001304") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001304").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkMotociclismo.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001305") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001305").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;
            //    chkOtro.Checked = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001306") == null ? false : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001306").ServiceComponentFieldValues[0].v_Value1 == "0" ? false : true;


            //    txtAnioServicioMilitar.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001876") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001876").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaServicioMilitar.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001879") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001879").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioDeportesAereos.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001880") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001880").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaDeportesAereos.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001881") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001881").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioDeporteSubmarino.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001882") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001882").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaDeporteSubmarino.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001883") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001883").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioManipulacionArmas.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001884") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001884").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaManipulacionArmas.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001885") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001885").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioExposicionMusica.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001886") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001886").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaExposicionMusica.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001887") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001887").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioUsoaudifnos.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001888") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001888").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaUsoaudifnos.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001889") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001889").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioMotociclismo.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001890") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001890").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaMotociclismo.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001891") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001891").ServiceComponentFieldValues[0].v_Value1;

            //    txtAnioOtro.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001892") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001892").ServiceComponentFieldValues[0].v_Value1;
            //    txtFrecuenciaOtro.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001893") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001893").ServiceComponentFieldValues[0].v_Value1;


            //    ddlTipoRuido.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001307") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001307").ServiceComponentFieldValues[0].v_Value1;
            //    ddlHorasPorDia.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001308") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001308").ServiceComponentFieldValues[0].v_Value1;
            //    ddlTapones.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001309") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001309").ServiceComponentFieldValues[0].v_Value1;
            //    ddlOrejeras.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001310") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001310").ServiceComponentFieldValues[0].v_Value1;
            //    ddlAmbos.SelectedValue = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001895") == null ? "-1" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001895").ServiceComponentFieldValues[0].v_Value1;


            //    txtIntensidad.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000000179") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000000179").ServiceComponentFieldValues[0].v_Value1;
            //    txtHoras.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001894") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N002-MF000001894").ServiceComponentFieldValues[0].v_Value1;

            //    //OD
            //    txtOD_VA_125.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_125).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_250.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_250).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_500.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_500).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_1000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_1000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_2000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_2000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_3000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_3000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_4000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_4000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_6000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_6000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VA_8000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OD_8000).ServiceComponentFieldValues[0].v_Value1;

            //    txtOD_VO_125.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_125).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_250.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_250).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_500.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_500).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_1000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_1000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_2000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_2000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_3000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_3000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_4000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_4000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_6000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_6000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_VO_8000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OD_8000).ServiceComponentFieldValues[0].v_Value1;

            //    txtOD_EM_125.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_125).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_250.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_250).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_500.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_500).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_1000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_1000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_2000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_2000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_3000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_3000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_4000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_4000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_6000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_6000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOD_EM_8000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OD_8000).ServiceComponentFieldValues[0].v_Value1;


            //    //OI
            //    txtOI_VA_125.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_125).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_250.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_250).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_500.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_500).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_1000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_1000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_2000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_2000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_3000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_3000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_4000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_4000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_6000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_6000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VA_8000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VA_OI_8000).ServiceComponentFieldValues[0].v_Value1;

            //    txtOI_VO_125.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_125).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_250.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_250).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_500.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_500).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_1000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_1000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_2000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_2000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_3000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_3000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_4000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_4000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_6000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_6000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_VO_8000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_VO_OI_8000).ServiceComponentFieldValues[0].v_Value1;

            //    txtOI_EM_125.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_125) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_125).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_250.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_250) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_250).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_500.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_500) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_500).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_1000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_1000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_1000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_2000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_2000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_2000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_3000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_3000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_4000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_4000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_4000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_6000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_6000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_6000).ServiceComponentFieldValues[0].v_Value1;
            //    txtOI_EM_8000.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_8000) == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == Constants.txt_EM_OI_8000).ServiceComponentFieldValues[0].v_Value1;



            //    txtActualAnio.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001933") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001933").ServiceComponentFieldValues[0].v_Value1;
            //    txtActualOD.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001934") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001934").ServiceComponentFieldValues[0].v_Value1;
            //    txtActualOI.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001935") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001935").ServiceComponentFieldValues[0].v_Value1;
            //    txtMenoscaboAuditivo.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001979") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001979").ServiceComponentFieldValues[0].v_Value1;

            //    txtCalibracion.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000084") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000084").ServiceComponentFieldValues[0].v_Value1;
            //    txtMarca.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000082") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000082").ServiceComponentFieldValues[0].v_Value1;
            //    txtModelo.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000083") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000000083").ServiceComponentFieldValues[0].v_Value1;
            //    txtNivelRuidoAmbiental.Text = objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001874") == null ? "" : objAudiometria.Find(p => p.v_ComponentFieldsId == "N009-MF000001874").ServiceComponentFieldValues[0].v_Value1;

            //}

            #endregion
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            personDto objpersonDto = new personDto();
            PacientBL objPacientBL = new PacientBL();

            //Actualizar Datos del Filiación
            objpersonDto = objPacientBL.GetPerson(ref objOperationResult, Session["PersonId"].ToString());
            string FechaNacimiento = txtDia.Text + "/" + txtMes.Text + "/" + txtAnio.Text;
            objpersonDto.d_Birthdate = DateTime.Parse(FechaNacimiento);
            objpersonDto.i_DocTypeId = int.Parse(ddlDocumento.SelectedValue.ToString());
            objpersonDto.v_DocNumber = txtNroDocumento.Text;
            objpersonDto.v_AdressLocation = txtDireccionFiscal.Text;
            objpersonDto.i_DepartmentId = int.Parse(ddlDepartamento.SelectedValue.ToString());
            objpersonDto.i_ProvinceId = int.Parse(ddlProvincia.SelectedValue.ToString());
            objpersonDto.i_DistrictId = int.Parse(ddlDistrito.SelectedValue.ToString());
            objpersonDto.i_ResidenceInWorkplaceId = int.Parse(ddlResideSiNo.SelectedValue.ToString());
            objpersonDto.v_ResidenceTimeInWorkplace = txtTiempoResidenci.Text;
            objpersonDto.v_TelephoneNumber = txtTelefonos.Text;
            objpersonDto.i_TypeOfInsuranceId = int.Parse(ddlTipoSeguro.SelectedValue.ToString());
            objpersonDto.v_Mail = txtEmail.Text;
            objpersonDto.i_MaritalStatusId = int.Parse(ddlEstadoCivil.SelectedValue.ToString());
            objpersonDto.i_LevelOfId = int.Parse(ddlGradoInstruccion.SelectedValue.ToString());
            objpersonDto.i_NumberLiveChildren = txtNroHijosVivos.Text == "" ? (int?)null : int.Parse(txtNroHijosVivos.Text.ToString());
            objpersonDto.i_NumberDeadChildren = txtNroHijosMuertos.Text == "" ? (int?)null : int.Parse(txtNroHijosMuertos.Text.ToString());

            //Grabar Datos Filiacion
            //objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, ((ClientSession)Session["objClientSession"]).GetAsList(), txtNroDocumento.Text, txtNroDocumento.Text);
            

            //Cargar Data Historia
            historyDto ohistoryDto = null;
            List<historyDto> ListaHistoryDto = new List<historyDto>();
            HistoryBL oHistoryBL = new HistoryBL();


            for (int i = 0; i < grdDataHistoriaOcupacional.Rows.Count; i++)
            {
                ohistoryDto = new historyDto();
                  GridRow row = grdDataHistoriaOcupacional.Rows[i];

                  System.Web.UI.WebControls.TextBox txtOrganization = (System.Web.UI.WebControls.TextBox)row.FindControl("v_Organization");
                  System.Web.UI.WebControls.TextBox txtTypeActivity = (System.Web.UI.WebControls.TextBox)row.FindControl("v_TypeActivity");
                  System.Web.UI.WebControls.TextBox txtWorkstation = (System.Web.UI.WebControls.TextBox)row.FindControl("v_Workstation");
                  System.Web.UI.WebControls.TextBox txtFechas = (System.Web.UI.WebControls.TextBox)row.FindControl("v_Fechas");

                  System.Web.UI.WebControls.TextBox txtExposicion = (System.Web.UI.WebControls.TextBox)row.FindControl("v_Exposicion");
                  System.Web.UI.WebControls.TextBox txtTiempoTrabajo = (System.Web.UI.WebControls.TextBox)row.FindControl("v_TiempoTrabajo");
                  System.Web.UI.WebControls.TextBox txtEpps = (System.Web.UI.WebControls.TextBox)row.FindControl("v_Epps");

                  ohistoryDto.v_HistoryId = grdDataHistoriaOcupacional.Rows[i].Values[7];
                  ohistoryDto.v_Organization = txtOrganization.Text;
                  ohistoryDto.v_TypeActivity = txtTypeActivity.Text;
                  ohistoryDto.v_workstation = txtWorkstation.Text;
                  //ohistoryDto.v_Fechas = txtFechas.Text;
                  //ohistoryDto.v_Exposicion = txtExposicion.Text;
                  //ohistoryDto.v_TiempoTrabajo = txtTiempoTrabajo.Text;
                  //ohistoryDto.v_Epps = txtEpps.Text;
                  ohistoryDto.v_PersonId = Session["PersonId"].ToString();
                  if (txtOrganization.Text != "")
                  {
                      ListaHistoryDto.Add(ohistoryDto);
                  }
            }

            //Cargar Data Médicos Personales
            personmedicalhistoryDto oPersonmedicalhistoryDto = null;
            List<personmedicalhistoryDto> ListaPersonmedicalhistoryDto = new List<personmedicalhistoryDto>();

            for (int i = 0; i < grdDataPatologicoPersonal.Rows.Count; i++)
            {
                oPersonmedicalhistoryDto = new personmedicalhistoryDto();
                GridRow row = grdDataPatologicoPersonal.Rows[i];

                System.Web.UI.WebControls.TextBox txtFechaInicio = (System.Web.UI.WebControls.TextBox)row.FindControl("v_FechaInicio");
                System.Web.UI.WebControls.TextBox txtDiagnosticDetail = (System.Web.UI.WebControls.TextBox)row.FindControl("v_DiagnosticDetail");
                System.Web.UI.WebControls.TextBox txtTreatmentSite = (System.Web.UI.WebControls.TextBox)row.FindControl("v_TreatmentSite");

                oPersonmedicalhistoryDto.v_PersonId = Session["PersonId"].ToString();
                //oPersonmedicalhistoryDto.v_FechaInicio = txtFechaInicio.Text;
                oPersonmedicalhistoryDto.v_DiagnosticDetail = txtDiagnosticDetail.Text;
                oPersonmedicalhistoryDto.v_TreatmentSite = txtTreatmentSite.Text;
                CheckBoxField fieldTabajo = (CheckBoxField)grdDataPatologicoPersonal.FindColumn("CheckBoxField2");
                bool Trabajo = fieldTabajo.GetCheckedState(i);
                //oPersonmedicalhistoryDto.i_AsociadoTrabajo = Trabajo == true ? 1 : 0;
                oPersonmedicalhistoryDto.v_PersonMedicalHistoryId = grdDataPatologicoPersonal.Rows[i].Values[5];
                oPersonmedicalhistoryDto.v_DiseasesId = grdDataPatologicoPersonal.Rows[i].Values[6];
                if (txtFechaInicio.Text != "")
                {
                    ListaPersonmedicalhistoryDto.Add(oPersonmedicalhistoryDto);
                }
            }

            //Cargar Data Médicos Personales
            noxioushabitsDto oNoxioushabitsDto = null;
            List<noxioushabitsDto> ListaNoxioushabitsDto = new List<noxioushabitsDto>();

            for (int i = 0; i < grdDataHabitosNocivos.Rows.Count; i++)
            {
                oNoxioushabitsDto = new noxioushabitsDto();
                GridRow row = grdDataHabitosNocivos.Rows[i];

                System.Web.UI.WebControls.TextBox txtComment = (System.Web.UI.WebControls.TextBox)row.FindControl("v_Comment");
                System.Web.UI.WebControls.DropDownList ddlFrecuencia = (System.Web.UI.WebControls.DropDownList)grdDataHabitosNocivos.Rows[i].FindControl("ddlFrecuencia");
                      
                oNoxioushabitsDto.v_PersonId = Session["PersonId"].ToString();
                oNoxioushabitsDto.v_Comment = txtComment.Text;
                //oNoxioushabitsDto.i_FrequencyId = int.Parse(ddlFrecuencia.SelectedValue.ToString());
                oNoxioushabitsDto.v_NoxiousHabitsId = grdDataHabitosNocivos.Rows[i].Values[3];
                oNoxioushabitsDto.i_TypeHabitsId = int.Parse(grdDataHabitosNocivos.Rows[i].Values[4].ToString());
                ListaNoxioushabitsDto.Add(oNoxioushabitsDto);

            }

            //Cargar Data Antecedentes Familiares
            familymedicalantecedentsDto ofamilymedicalantecedentsDto = null;
            List<familymedicalantecedentsDto> ListafamilymedicalantecedentsDto = new List<familymedicalantecedentsDto>();

            for (int i = 0; i < grdDataAntecedentesPatologicosFamiliares.Rows.Count; i++)
            {
                ofamilymedicalantecedentsDto = new familymedicalantecedentsDto();
                GridRow row = grdDataAntecedentesPatologicosFamiliares.Rows[i];
                System.Web.UI.WebControls.TextBox txtComment = (System.Web.UI.WebControls.TextBox)row.FindControl("v_CommentFamili");

                ofamilymedicalantecedentsDto.v_PersonId = Session["PersonId"].ToString();
                ofamilymedicalantecedentsDto.v_Comment = txtComment.Text;
                ofamilymedicalantecedentsDto.i_TypeFamilyId = int.Parse(grdDataAntecedentesPatologicosFamiliares.Rows[i].Values[3].ToString());
                ofamilymedicalantecedentsDto.v_FamilyMedicalAntecedentsId = grdDataAntecedentesPatologicosFamiliares.Rows[i].Values[2];
                ListafamilymedicalantecedentsDto.Add(ofamilymedicalantecedentsDto);
            }

            //oHistoryBL.GrabarHistoria312(ref objOperationResult, ListaHistoryDto, ListaPersonmedicalhistoryDto, ListaNoxioushabitsDto, ListafamilymedicalantecedentsDto,((ClientSession)Session["objClientSession"]).GetAsList());


            //Grabar Antecedentes Ginecológicos
            serviceDto serviceDTO = new serviceDto();

            serviceDTO.v_ServiceId =Session["ServiceId"].ToString(); ;
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
            serviceDTO.i_AptitudeStatusId = int.Parse(ViewState["i_AptitudeStatusId"].ToString());
            //_serviceBL.UpdateAnamnesis(ref objOperationResult, serviceDTO, ((ClientSession)Session["objClientSession"]).GetAsList());



            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = new List<Node.WinClient.BE.ServiceComponentFieldsList>();
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList serviceComponentFields = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList serviceComponentFieldValues = null;


            #region Triaje


            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>();


            //Talla**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_TALLA_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtTalla.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Temperatura**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_TEMPERATURA_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtTemp.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Peso**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_PESO_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPeso.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //PAS**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_PAS_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtParterial.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //IMC**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_IMC_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtImc.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            ////PAD**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            //serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            //serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_PAD_ID;
            //serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            //_serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            //serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            //serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            //serviceComponentFieldValues.v_Value1 = txtPAD.Text;
            //_serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            //serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            //// Agregar a mi lista
            //_serviceComponentFieldsList.Add(serviceComponentFields);




            //Perímetro Abdomen**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_PERIMETRO_ABDOMINAL_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPadb.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Frecuencia Cardiaca**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_FREC_CARDIACA_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtFcar.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Perímetro Cadera**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPcadera.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //Frecuencia Respiratoria**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtfres.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //ICC**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ANTROPOMETRIA_INDICE_CINTURA_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtIcc.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Saturación Oxígeno**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_SAT_O2_ID;
            serviceComponentFields.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSatO2.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);





            var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                                 Session["PersonId"].ToString(),
                                                                ViewState["ServicioComponentIdTriaje"] .ToString());

            //lIMPIAR LA LISTA DE DXS
            List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList> ListaDxByComponent = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            MedicalExamFieldValuesBL oMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
            //Elminar los Dx antiguos
            _serviceBL.EliminarDxAniguosPorComponente(Session["ServiceId"].ToString(), Constants.FUNCIONES_VITALES_ID,((ClientSession)Session["objClientSession"]).GetAsList());
            _serviceBL.EliminarDxAniguosPorComponente(Session["ServiceId"].ToString(), Constants.ANTROPOMETRIA_ID,((ClientSession)Session["objClientSession"]).GetAsList());
            ListaDxByComponent = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

            if (txtImc.Text != "")
            {
                double IMC = double.Parse(txtImc.Text.ToString());
                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList DxByComponent = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();

                List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendations = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();
                List<Sigesoft.Node.WinClient.BE.RestrictionList> Restrictions = new List<Sigesoft.Node.WinClient.BE.RestrictionList>();

                DxByComponent.i_AutoManualId = 1;
                DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                DxByComponent.i_PreQualificationId = 1;
                DxByComponent.v_ComponentFieldsId = Constants.ANTROPOMETRIA_IMC_ID;

                //Obtener el Componente que está amarrado al DX
                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(Constants.ANTROPOMETRIA_IMC_ID);
                string DiseasesId = "";
                if (IMC <= 18.49)
                {
                    DiseasesId = "N009-DD000000033";
                }
                else if (IMC >= 18.5 && IMC <= 24.99)
                {
                    DiseasesId = "N009-DD000000033";
                    //DiseasesId = "N009-DD000000788";
                }
                else if (IMC >= 25 && IMC <= 29.99)
                {
                    DiseasesId = "N009-DD000000033";
                    //DiseasesId = "N009-DD000000516";
                }
                else if (IMC >= 30 && IMC <= 34.99)
                {
                    DiseasesId = "N009-DD000000033";
                    //DiseasesId = "N009-DD000000602";
                }
                else if (IMC >= 35 && IMC <= 39.99)
                {
                    DiseasesId = "N009-DD000000033";
                    //DiseasesId = "N009-DD000000603";
                }
                else if (IMC >= 40)
                {
                    DiseasesId = "N009-DD000000033";
                    //DiseasesId = "N009-DD000000604";
                }
                string ComponentFieldId = Constants.ANTROPOMETRIA_IMC_ID;

                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(Session["ServiceId"].ToString(), DiseasesId, ComponentDx, ComponentFieldId);
                if (oDiagnosticRepositoryListOld != null)
                {
                    oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                    oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                    oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                    oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                    ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                }

                DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                DxByComponent.i_RecordType = (int)RecordType.Temporal;
                DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                DxByComponent.v_ComponentId = ComponentDx;
                DxByComponent.v_DiseasesId = DiseasesId;
                DxByComponent.v_ServiceId =Session["ServiceId"].ToString();


                //Obtener las recomendaciones

                DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId,Session["ServiceId"].ToString(), Constants.FUNCIONES_VITALES_ID);

                ListaDxByComponent.Add(DxByComponent);



                //Llenar entidad ServiceComponent
                servicecomponentDto serviceComponentDto = new servicecomponentDto();
                serviceComponentDto.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();
                serviceComponentDto.v_Comment = "";
                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                serviceComponentDto.v_ComponentId = Constants.FUNCIONES_VITALES_ID;
                serviceComponentDto.v_ServiceId =Session["ServiceId"].ToString();


                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                            ListaDxByComponent,
                                            serviceComponentDto,
                                           ((ClientSession)Session["objClientSession"]).GetAsList(),
                                            true);
            }

            ListaDxByComponent = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();

            if (txtParterial.Text != "")
            {
                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList DxByComponent = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();

                List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendations = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();
                List<Sigesoft.Node.WinClient.BE.RestrictionList> Restrictions = new List<Sigesoft.Node.WinClient.BE.RestrictionList>();
                int PAS = int.Parse(txtParterial.Text.ToString());

                DxByComponent.i_AutoManualId = 1;
                DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                DxByComponent.i_PreQualificationId = 1;
                DxByComponent.v_ComponentFieldsId = Constants.FUNCIONES_VITALES_PAS_ID;
                //Obtener el Componente que está amarrado al DX
                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(Constants.FUNCIONES_VITALES_PAS_ID);
                string DiseasesId = "";
                if (PAS > 140)
                {
                    DiseasesId = "N002-DD000000523";
                    string ComponentFieldId = Constants.FUNCIONES_VITALES_PAS_ID;

                    Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(Session["ServiceId"].ToString(), DiseasesId, ComponentDx, ComponentFieldId);
                    if (oDiagnosticRepositoryListOld != null)
                    {
                        oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                        oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                        oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                        oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                        ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                    }

                    DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    DxByComponent.i_RecordType = (int)RecordType.Temporal;
                    DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                    DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                    DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                    string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                    DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                    DxByComponent.v_ComponentId = ComponentDx;
                    DxByComponent.v_DiseasesId = DiseasesId;
                    DxByComponent.v_ServiceId =Session["ServiceId"].ToString();


                    //Obtener las recomendaciones

                    DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId,Session["ServiceId"].ToString(), Constants.FUNCIONES_VITALES_ID);

                    ListaDxByComponent.Add(DxByComponent);

                    //Llenar entidad ServiceComponent
                    servicecomponentDto serviceComponentDto = new servicecomponentDto();
                    serviceComponentDto.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();
                    serviceComponentDto.v_Comment = "";
                    serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                    serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                    serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                    serviceComponentDto.v_ComponentId = Constants.FUNCIONES_VITALES_ID;
                    serviceComponentDto.v_ServiceId =Session["ServiceId"].ToString();


                    _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                ListaDxByComponent,
                                                serviceComponentDto,
                                               ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                true);
                }
            }


            ListaDxByComponent = new List<Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList>();
            if (txtIcc.Text != "")
            {
                Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList DxByComponent = new Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList();

                List<Sigesoft.Node.WinClient.BE.RecomendationList> Recomendations = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();
                List<Sigesoft.Node.WinClient.BE.RestrictionList> Restrictions = new List<Sigesoft.Node.WinClient.BE.RestrictionList>();
                double ICC = double.Parse(txtIcc.Text.ToString());

                DxByComponent.i_AutoManualId = 1;
                DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                DxByComponent.i_PreQualificationId = 1;
                DxByComponent.v_ComponentFieldsId = Constants.ANTROPOMETRIA_INDICE_CINTURA_ID;
                //Obtener el Componente que está amarrado al DX
                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(Constants.ANTROPOMETRIA_INDICE_CINTURA_ID);
                string DiseasesId = "";
                if (ICC > 1)
                {
                    DiseasesId = "N002-DD000000089";

                    string ComponentFieldId = Constants.ANTROPOMETRIA_INDICE_CINTURA_ID;

                    Sigesoft.Node.WinClient.BE.DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(Session["ServiceId"].ToString(), DiseasesId, ComponentDx, ComponentFieldId);
                    if (oDiagnosticRepositoryListOld != null)
                    {
                        oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                        oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                        oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                        oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                        ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                    }

                    DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    DxByComponent.i_RecordType = (int)RecordType.Temporal;
                    DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                    DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                    DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                    string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                    DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                    DxByComponent.v_ComponentId = ComponentDx;
                    DxByComponent.v_DiseasesId = DiseasesId;
                    DxByComponent.v_ServiceId =Session["ServiceId"].ToString();


                    //Obtener las recomendaciones

                    DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId,Session["ServiceId"].ToString(), Constants.FUNCIONES_VITALES_ID);

                    ListaDxByComponent.Add(DxByComponent);

                    //Llenar entidad ServiceComponent
                    servicecomponentDto serviceComponentDto = new servicecomponentDto();
                    serviceComponentDto.v_ServiceComponentId =ViewState["ServicioComponentIdTriaje"] .ToString();
                    serviceComponentDto.v_Comment = "";
                    serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                    serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                    serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                    serviceComponentDto.v_ComponentId = Constants.FUNCIONES_VITALES_ID;
                    serviceComponentDto.v_ServiceId =Session["ServiceId"].ToString();


                    _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                ListaDxByComponent,
                                                serviceComponentDto,
                                               ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                true);
                }



            }
            _serviceBL.ActualizarEstadoComponentesPorCategoria(ref objOperationResult, 10,Session["ServiceId"].ToString(), (int)ServiceComponentStatus.Evaluado,((ClientSession)Session["objClientSession"]).GetAsList());


            #endregion

            #region Examen Físico
            //Cargar Datos del ESO Examen Físico

            for (int i = 0; i < grdDataOrgano.Rows.Count; i++)
            {
                serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();
                GridRow row = grdDataOrgano.Rows[i];

                serviceComponentFields.v_ComponentFieldsId = grdDataOrgano.Rows[i].Values[3];
                serviceComponentFields.v_ServiceComponentId = ViewState["ServicioComponentIdExamenFisico"].ToString();

                _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                CheckBoxField fieldSinHallazgo = (CheckBoxField)grdDataOrgano.FindColumn("CheckBoxField1");
                bool SinHallazgo = fieldSinHallazgo.GetCheckedState(i);
                serviceComponentFieldValues.v_Value1 = SinHallazgo == false ? "0" : "1";

                _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;
                // Agregar a mi lista
                _serviceComponentFieldsList.Add(serviceComponentFields);


                serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();


                serviceComponentFields.v_ComponentFieldsId = grdDataOrgano.Rows[i].Values[4];
                serviceComponentFields.v_ServiceComponentId = ViewState["ServicioComponentIdExamenFisico"].ToString();

                _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
                serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

                serviceComponentFieldValues.v_ComponentFieldValuesId = null;

                System.Web.UI.WebControls.TextBox txtHallazgo = (System.Web.UI.WebControls.TextBox)row.FindControl("Hallazgos");
                serviceComponentFieldValues.v_Value1 = txtHallazgo.Text.ToString();

                _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                // Agregar a mi lista
                _serviceComponentFieldsList.Add(serviceComponentFields);
            }

             _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                            _serviceComponentFieldsList,
                                                            ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                            Session["PersonId"].ToString(),
                                                            ViewState["ServicioComponentIdExamenFisico"].ToString());
            #endregion


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
                
        protected void btnGrabarAudiometria_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabAudiometria, ViewState["ServicioComponentIdAudimetria"].ToString());
        }

        protected void btnOsteoMuscular_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabOsteomuscular, ViewState["ServicioComponentIdExamenFisico"].ToString());
        }

        protected void btnGrabarPsico_Click(object sender, EventArgs e)
        {
            //SearchControlAndSetValues(TabPsicologia, ViewState["ServicioComponentIdExamenPsico"].ToString());
        }

        protected void btnGrabarTamizaje_Click(object sender, EventArgs e)
        {
            //SearchControlAndSetValues(TabDermatologico, ViewState["ServicioComponentIdExamenTamizaje"].ToString());
        }

        protected void btnGrabarEspirometria_Click(object sender, EventArgs e)
        {
            //SearchControlAndSetValues(TabEspirometria, ViewState["ServicioComponentIdExamenEspirometria"].ToString());
        }

        protected void btnGrabarOftalmologia_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabOftalmologia, ViewState["ServicioComponentIdExamenOftalmologia"].ToString());
        }

        protected void btnGrabarRx_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabRx, Session["ServicioComponentIdExamenRx"].ToString());
        }

        protected void btnGrabar7D_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(Tab7D, ViewState["ServicioComponentIdExamen7D"].ToString());
        }

        protected void btnGrabarOIT_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabOIT, Session["ServicioComponentIdExamenOIT"].ToString());
        }

        protected void btnAltura18_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabAltura18, ViewState["ServicioComponentIdExamenAltura18"].ToString());
        }

        protected void btnGrabarSintomaticoResp_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabSintomaticoRespiratorio, ViewState["ServicioComponentIdExamenSintomaticoResp"].ToString());
        }
             
        protected void btnGrabarLaboratio_Click(object sender, EventArgs e)
        {
            SearchControlAndSetValues(TabLaboratorio, Session["ServicioComponentIdExamenLaboratorio"].ToString());
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
                        ((Panel)ctrl).Hidden = x == null ? true : false;

                    }
                }


                if (ctrl.HasControls())
                    SearchControlAndShow(ctrl, objExamenLaboratorio);

            }
        }

        private void SearchControlAndLoadData(Control ctrlContainer, string ServiceComponentId,List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> ListaValores)
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

                if (ctrl.HasControls())
                    SearchControlAndLoadData(ctrl, ServiceComponentId, ListaValores);

            }
        }

        private void SearchControlAndClean(Control ctrlContainer,List<Sigesoft.Node.WinClient.BE.ComponentFieldsList> ListaValoresPorDefecto)
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

                if (ctrl.HasControls())
                    SearchControlAndClean(ctrl, ListaValoresPorDefecto);

            }
        }

        private void SearchControlAndSetValues(Control ctrlContainer, string pstrServiceComponentId)
        {

            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = new List<Node.WinClient.BE.ServiceComponentFieldsList>();
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList serviceComponentFields = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList serviceComponentFieldValues = null;
            OperationResult objOperationResult = new OperationResult();
            foreach (Control ctrl in ctrlContainer.Controls)
            {
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
                        var x = ((CheckBox)ctrl);
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
         
                if (ctrl.HasControls())
                    SearchControlAndSetValues(ctrl, pstrServiceComponentId);
            }

            var result = _serviceBL.AddServiceComponentValues_(ref objOperationResult,
                                                         _serviceComponentFieldsList,
                                                        ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                         Session["PersonId"].ToString(),
                                                        pstrServiceComponentId);

            //Grabar el Id del Usuairo en el capo i_ApprovedUpdateUserId y su fecha

            _serviceBL.ActualizarServiceComponentUsuario(ref objOperationResult, pstrServiceComponentId, ((ClientSession)Session["objClientSession"]).GetAsList());
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

        protected void grdDataHabitosNocivos_RowDataBound(object sender, GridRowEventArgs e)
        {
            System.Web.UI.WebControls.DropDownList ddlFrecuencia = (System.Web.UI.WebControls.DropDownList)grdDataHabitosNocivos.Rows[e.RowIndex].FindControl("ddlFrecuencia");

            NoxiousHabitsList row = (NoxiousHabitsList)e.DataItem;

            int Frecuencia = Convert.ToInt32(row.i_FrequencyId);
            ddlFrecuencia.SelectedValue = Frecuencia.ToString();
        }

        protected void txtTalla_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text != "" && txtTalla.Text != "")
            {
                double Peso = double.Parse(txtPeso.Text.ToString());
                double Talla = double.Parse(txtTalla.Text.ToString());

                txtImc.Text = ((Peso / Talla) / Talla).ToString("#.##");
            }
        }

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text != "" && txtTalla.Text != "")
            {
                double Peso = double.Parse(txtPeso.Text.ToString());
                double Talla = double.Parse(txtTalla.Text.ToString());

                txtImc.Text = ((Peso / Talla) / Talla).ToString("#.##");
            }
        }

        protected void txtPadb_TextChanged(object sender, EventArgs e)
        {
            if (txtPadb.Text != "" && txtPcadera.Text != "")
            {
                double PerAbd = double.Parse(txtPadb.Text.ToString());
                double PerCad = double.Parse(txtPcadera.Text.ToString());

                txtIcc.Text = (PerAbd / PerCad).ToString("#.##");
            }
        }

        protected void txtPcadera_TextChanged(object sender, EventArgs e)
        {
            if (txtPadb.Text != "" && txtPcadera.Text != "")
            {
                double PerAbd = double.Parse(txtPadb.Text.ToString());
                double PerCad = double.Parse(txtPcadera.Text.ToString());

                txtIcc.Text = (PerAbd / PerCad).ToString("#.##");
            }
        }

        #endregion    

        #region Diagnósticos
        protected void ActualizaGrillasDx(string ServiceId, string PersonId)
        {

            OperationResult objOperationResult = new OperationResult();
            var ListaDx = _serviceBL.GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, ServiceId);
            var ListaComponentes = (List<string>)Session["ComponentesPermisoLectura"];
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
               
                 _tmpTotalDiagnostic = _serviceBL.GetServiceComponentTotalDiagnostics(ref objOperationResult, Session["ServiceId"].ToString());

                grdRecomendaciones.DataSource = _tmpTotalDiagnostic.Recomendations;
                grdRecomendaciones.DataBind();

                grdRestricciones.DataSource = _tmpTotalDiagnostic.Restrictions;
                grdRestricciones.DataBind();
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

        protected void WindowAddDX_Close(object sender, WindowCloseEventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString()); 
        }

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

        protected void WindowAddDXFrecuente_Close(object sender, WindowCloseEventArgs e)
        {
            ActualizaGrillasDx(Session["ServiceId"].ToString(), Session["PersonId"].ToString());
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

        protected void btnGrabarAptitud_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //Actualizar Aptitud del Servicio
            _serviceBL.ActualizarAptitudServicio(ref objOperationResult, Session["ServiceId"].ToString(), int.Parse(ddlAptitud.SelectedValue.ToString()), ((ClientSession)Session["objClientSession"]).GetAsList(), txtComentarioAptitud.Text, txtComentarioMedico.Text, txtRestriccionesAptitud.Text,-1);

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

        protected void WindowReporte_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void btnReporteOftalmologia_Click(object sender, EventArgs e)
        {
            List<Sigesoft.Node.WinClient.BE.ReportOftalmologia> dataListForReport = new List<Node.WinClient.BE.ReportOftalmologia>();
            Sigesoft.Node.WinClient.BE.ReportOftalmologia objOftalmologia = new Node.WinClient.BE.ReportOftalmologia();
            objOftalmologia.NombrePaciente = Session["PersonId"].ToString();
            dataListForReport.Add(objOftalmologia);

            Session["ListaReporteOftalmologia"] = dataListForReport;

        }

        protected void winEdit3_Close(object sender, WindowCloseEventArgs e)
        {

        }

        private List<MyListWeb> LlenarLista()
        {
            List<MyListWeb> lista = new List<MyListWeb>();
            int selectedCount = grdData.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {

                //btnNewFichaOcupacional.Enabled = true;
                btnNewExamenes.Enabled = true;
                //btnObtenerIds.Enabled = true;

            }
            else
            {

                //btnNewFichaOcupacional.Enabled = false;
                btnNewExamenes.Enabled = false;
                //btnObtenerIds.Enabled = false;
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

        protected void Window1_Close(object sender, EventArgs e)
        {

        }  
 
    }
}