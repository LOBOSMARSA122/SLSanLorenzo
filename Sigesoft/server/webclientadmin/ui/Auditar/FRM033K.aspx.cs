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
    public partial class FRM033K : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL oProtocolBL = new ProtocolBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["v_ServiceId"] != null)
                    Session["ServiceId"] = Request.QueryString["v_ServiceId"].ToString();
                if (Request.QueryString["v_IdTrabajador"] != null)
                    Session["IdTrabajador"] = Request.QueryString["v_IdTrabajador"].ToString();

                List<string> servicios = new List<string>();
                servicios.Add(Session["ServiceId"].ToString());
                var obj = oProtocolBL.ObtenerIdsParaImportacion(servicios, int.Parse(Session["ConsultorioId"].ToString()));
                Session["ServiceComponentId"] = obj[0].ServicioComponentId.ToString();



                var Valores = new ServiceBL().ValoresComponente(Session["ServiceId"].ToString(), Constants.OIT_ID);

                if (Valores.Count() != 0)
                {
                    //ZONAS AFECTADAS
                    var _chkSupDer = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SUPERIOR_DERECHO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SUPERIOR_DERECHO_ID).v_Value1;
                    chkSupDer.Checked = _chkSupDer== "1" ?true:false;

                    var _chkSupIzq = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SENOS_CARDIOFRENICOS_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SENOS_CARDIOFRENICOS_ID).v_Value1;
                    chkSupIzq.Checked = _chkSupIzq == "1" ? true : false;

                    var _chkMedDer = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_MEDIO_DERECHO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_MEDIO_DERECHO_ID).v_Value1;
                    chkMedDer.Checked = _chkMedDer == "1" ? true : false;

                    var _chkMedIzq = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_MEDIO_IZQUIERDO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_MEDIO_IZQUIERDO_ID).v_Value1;
                    chkMedIzq.Checked = _chkMedIzq == "1" ? true : false;

                    var _chkInfDer = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_INFERIOR_DERECHO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_INFERIOR_DERECHO_ID).v_Value1;
                    chkInfDer.Checked = _chkInfDer == "1" ? true : false;

                    var _chkInfIzq = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_INFERIOR_IZQUIERDO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_INFERIOR_IZQUIERDO_ID).v_Value1;
                    chkInfIzq.Checked = _chkInfIzq == "1" ? true : false;
               
                
                //PROFSIÓN
                    var _chk0menos = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_0_NADA_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_0_NADA_ID).v_Value1;
                    chk0menos.Checked = _chk0menos == "1" ? true : false;
                    var _chk00 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_0_0_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_0_0_ID).v_Value1;
                    chk00.Checked = _chk00 == "1" ? true : false;
                    var _chk01 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_0_1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_0_1_ID).v_Value1;
                    chk01.Checked = _chk01 == "1" ? true : false;


                    var _chk10 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_1_0_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_1_0_ID).v_Value1;
                    chk10.Checked = _chk10 == "1" ? true : false;
                    var _chk11 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_1_1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_1_1_ID).v_Value1;
                    chk11.Checked = _chk11 == "1" ? true : false;
                    var _chk12 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_1_2_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_1_2_ID).v_Value1;
                    chk12.Checked = _chk12 == "1" ? true : false;


                    var _chk21 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_2_1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_2_1_ID).v_Value1;
                    chk21.Checked = _chk21 == "1" ? true : false;
                    var _chk22 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_2_2_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_2_2_ID).v_Value1;
                    chk22.Checked = _chk22 == "1" ? true : false;
                    var _chk23 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_2_3_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_2_3_ID).v_Value1;
                    chk23.Checked = _chk23 == "1" ? true : false;

                    var _chk32 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_3_2_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_3_2_ID).v_Value1;
                    chk32.Checked = _chk32 == "1" ? true : false;
                    var _chk33 = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_3_3_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_3_3_ID).v_Value1;
                    chk33.Checked = _chk33 == "1" ? true : false;
                    var _chk3mas = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_3_MAS_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_3_MAS_ID).v_Value1;
                    chk3mas.Checked = _chk3mas == "1" ? true : false;


                    //FORMA Y TAMAÑO

                    var _chkPriP = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_P_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_P_ID).v_Value1;
                    chkPriP.Checked = _chkPriP == "1" ? true : false;
                    var _chkPriS = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_S_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_S_ID).v_Value1;
                    chkPriS.Checked = _chkPriS == "1" ? true : false;
                    var _chkSecP = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_P1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_P1_ID).v_Value1;
                    chkSecP.Checked = _chkSecP == "1" ? true : false;
                    var _chkSecS = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_S1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_S1_ID).v_Value1;
                    chkSecS.Checked = _chkSecS == "1" ? true : false;

                    var _chkPriQ = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_Q_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_Q_ID).v_Value1;
                    chkPriQ.Checked = _chkPriQ == "1" ? true : false;
                    var _chkPriT = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_T_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_T_ID).v_Value1;
                    chkPriT.Checked = _chkPriT == "1" ? true : false;
                    var _chkSecQ = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_Q1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_Q1_ID).v_Value1;
                    chkSecQ.Checked = _chkSecQ == "1" ? true : false;
                    var _chkSecT = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_T1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_T1_ID).v_Value1;
                    chkSecT.Checked = _chkSecT == "1" ? true : false;

                    var _chkPriR = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_R_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_R_ID).v_Value1;
                    chkPriR.Checked = _chkPriR == "1" ? true : false;
                    var _chkPriU = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_U_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_U_ID).v_Value1;
                    chkPriU.Checked = _chkPriU == "1" ? true : false;
                    var _chkSecR = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_R1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_R1_ID).v_Value1;
                    chkSecR.Checked = _chkSecR == "1" ? true : false;
                    var _chkSecU = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_U1_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_U1_ID).v_Value1;
                    chkSecU.Checked = _chkSecU == "1" ? true : false;

                    //OPACIDADES GRANDRES
                    var _chkO = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_D_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_D_ID).v_Value1;
                    chkO.Checked = _chkO == "1" ? true : false;
                    var _chkA = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_A_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_A_ID).v_Value1;
                    chkA.Checked = _chkA == "1" ? true : false;
                    var _chkB = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_B_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_B_ID).v_Value1;
                    chkB.Checked = _chkB == "1" ? true : false;
                    var _chkC = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_C_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_C_ID).v_Value1;
                    chkC.Checked = _chkC == "1" ? true : false;

                    //SÍMBOLOS
                    var _chkSI = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SIMBOLO_SI_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SIMBOLO_SI_ID).v_Value1;
                    chkSI.Checked = _chkSI == "1" ? true : false;
                    var _chkNO = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SIMBOLO_NO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SIMBOLO_NO_ID).v_Value1;
                    chkNO.Checked = _chkNO == "1" ? true : false;


                    var _chkaa = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_AA_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_AA_ID).v_Value1;
                    chkaa.Checked = _chkaa == "1" ? true : false;
                    var _chkat = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_AT_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_AT_ID).v_Value1;
                    chkat.Checked = _chkat == "1" ? true : false;
                    var _chkax = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_AX_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_AX_ID).v_Value1;
                    chkax.Checked = _chkax == "1" ? true : false;
                    var _chkbu = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_BU_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_BU_ID).v_Value1;
                    chkbu.Checked = _chkbu == "1" ? true : false;
                    var _chkca = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CA_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CA_ID).v_Value1;
                    chkca.Checked = _chkca == "1" ? true : false;
                    var _chkcg = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CG_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CG_ID).v_Value1;
                    chkcg.Checked = _chkcg == "1" ? true : false;
                    var _chken = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CN_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CN_ID).v_Value1;
                    chken.Checked = _chken == "1" ? true : false;
                    var _chkco = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CO_ID).v_Value1;
                    chkco.Checked = _chkco == "1" ? true : false;
                    var _chkcp = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CP_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CP_ID).v_Value1;
                    chkcp.Checked = _chkcp == "1" ? true : false;
                    var _chkcv = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CV_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CV_ID).v_Value1;
                    chkcv.Checked = _chkcv == "1" ? true : false;
                    var _chkdi = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_DI_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_DI_ID).v_Value1;
                    chkdi.Checked = _chkdi == "1" ? true : false;
                    var _chkef = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_EF_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_EF_ID).v_Value1;
                    chkef.Checked = _chkef == "1" ? true : false;



                    var _chkem = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_EM_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_EM_ID).v_Value1;
                    chkem.Checked = _chkem == "1" ? true : false;
                    var _chkes = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_ES_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_ES_ID).v_Value1;
                    chkes.Checked = _chkes == "1" ? true : false;
                    var _chkod = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_OD_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_OD_ID).v_Value1;
                    chkod.Checked = _chkod == "1" ? true : false;
                    var _chkfr = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_FR_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_FR_ID).v_Value1;
                    chkfr.Checked = _chkfr == "1" ? true : false;
                    var _chkhi = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_HI_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_HI_ID).v_Value1;
                    chkhi.Checked = _chkhi == "1" ? true : false;
                    var _chkid = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_ID_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_ID_ID).v_Value1;
                    chkid.Checked = _chkid == "1" ? true : false;
                    var _chkoh = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_HO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_HO_ID).v_Value1;
                    chkoh.Checked = _chkoh == "1" ? true : false;
                    var _chkih = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_IH_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_IH_ID).v_Value1;
                    chkih.Checked = _chkih == "1" ? true : false;
                    var _chkkl = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_KL_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_KL_ID).v_Value1;
                    chkkl.Checked = _chkkl == "1" ? true : false;
                    var _chkme = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_ME_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_ME_ID).v_Value1;
                    chkme.Checked = _chkme == "1" ? true : false;
                    var _chkpa = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PA_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PA_ID).v_Value1;
                    chkpa.Checked = _chkpa == "1" ? true : false;
                    var _chkpb = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PB_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PB_ID).v_Value1;
                    chkpb.Checked = _chkpb == "1" ? true : false;
                    var _chkpi = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PI_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PI_ID).v_Value1;
                    chkpi.Checked = _chkpi == "1" ? true : false;
                    var _chkpx = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PX_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PX_ID).v_Value1;
                    chkpx.Checked = _chkpx == "1" ? true : false;

                    var _chkra = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_RA_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_RA_ID).v_Value1;
                    chkra.Checked = _chkra == "1" ? true : false;
                    var _chkrq = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_RP_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_RP_ID).v_Value1;
                    chkrq.Checked = _chkrq == "1" ? true : false;
                    var _chktb = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_TB_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_TB_ID).v_Value1;
                    chktb.Checked = _chktb == "1" ? true : false;


                    txtComentario.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_COMENTARIO_OD_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_COMENTARIO_OD_ID).v_Value1;

                    txtConclusiones.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID).v_Value1;

                }

            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceBL _serviceBL = new ServiceBL();
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList serviceComponentFieldValues = null;
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList serviceComponentFields = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = null;

            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>();


            //chkSupDer**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_SUPERIOR_DERECHO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSupDer.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkSupIzq**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_SENOS_CARDIOFRENICOS_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSupIzq.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkMedDer**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_MEDIO_DERECHO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkMedDer.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkMedIzq**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_MEDIO_IZQUIERDO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkMedIzq.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkInfDer**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_INFERIOR_DERECHO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkInfDer.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkInfIzq**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_INFERIOR_IZQUIERDO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkInfIzq.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chk0menos**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_0_NADA_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk0menos.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chk00**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_0_0_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk00.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chk01**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_0_1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk01.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chk10**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_1_0_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk10.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chk11**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_1_1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk11.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chk12**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_1_2_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk12.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chk21**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_2_1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk21.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chk22**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_2_2_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk22.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chk23**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_2_3_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk23.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chk32**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_3_2_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk32.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chk33**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_3_3_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk33.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chk3mas**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_3_MAS_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chk3mas.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkPriP**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_P_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkPriP.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //chkPriS**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_S_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkPriS.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkSecP**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_P1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSecP.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkSecS**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_S1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSecS.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkPriQ**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_Q_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkPriQ.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkPriT**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_T_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkPriT.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkSecQ**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_Q1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSecQ.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkSecT**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_T1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSecT.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkPriR**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_R_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkPriR.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkPriU**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_U_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkPriU.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkSecR**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_R1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSecR.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkSecU**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_U1_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSecU.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //chkSecU**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_D_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkO.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkA**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_A_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkA.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkB**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_B_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkB.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkC**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_C_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkC.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkSI**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_SIMBOLO_SI_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkSI.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkNO**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_SIMBOLO_NO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkNO.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkaa**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_AA_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkaa.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkat**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_AT_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkat.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkax**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_AX_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkax.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkbu**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_BU_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkbu.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkca**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CA_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkca.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkcg**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CG_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkcg.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chken**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CN_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chken.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkco**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkco.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkcp**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CP_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkcp.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkcv**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CV_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkcv.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkdi**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_DI_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkdi.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkef**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_EF_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkef.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkem**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_EM_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkem.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkes**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_ES_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkes.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkod**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_OD_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkod.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkfr**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_FR_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkfr.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkhi**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_HI_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkhi.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkid**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_ID_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkid.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkoh**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_HO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkoh.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkih**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_IH_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkih.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkkl**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_KL_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkkl.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkme**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_ME_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkme.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkpa**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_PA_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkpa.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkpb**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_PB_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkpb.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkpi**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_PI_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkpi.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkpx**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_PX_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkpx.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //chkra**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_RA_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkra.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chkrq**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_RP_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chkrq.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //chktb**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_TB_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = chktb.Checked == true ? "1" : "0";
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //txtComentario**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_COMENTARIO_OD_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtComentario.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);

            //txtConclusiones**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CONCLUSIONES_OIT_DESCRIPCION_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtConclusiones.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                           _serviceComponentFieldsList,
                                                            ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                           Session["IdTrabajador"].ToString(),
                                                           Session["ServiceComponentId"].ToString());


            Alert.ShowInTop("Datos grabados correctamente.");
        }
    }
}