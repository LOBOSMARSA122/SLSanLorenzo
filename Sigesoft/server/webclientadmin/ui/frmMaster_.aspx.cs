using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class frmMaster_ : System.Web.UI.Page
    {
        SecurityBL _objSecurityBL = new SecurityBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                   if (Session["IdPersona"] != null)
                {
                    treeMenu.DataSource = XmlDataSource1;
                    treeMenu.DataBind();
                }
                else
                {                 
                        LoadData();                  
                 
                }
            }
        }

        private void LoadData()
        {
            #region Variables Session

            var objClientSession = (ClientSession)Session["objClientSession"];

            // Actualizar variable de sesión
            Session["objClientSession"] = objClientSession;

            int systemUserId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            int currentExecutionNodeId = ((ClientSession)Session["objClientSession"]).i_CurrentExecutionNodeId;
            string systemUserName = ((ClientSession)Session["objClientSession"]).v_UserName;
            string personId = ((ClientSession)Session["objClientSession"]).v_PersonId;
            int systemUserTypeId = ((ClientSession)Session["objClientSession"]).i_SystemUserTypeId;

            #endregion

            lblDescripcion.Text = string.Format("Bienvenido(a): {0} / ", systemUserName);
           
            HyperLink3.Text = new ServiceBL().GetInfoMedicalCenter().v_Name.ToString();

            LoadTreeMenu(currentExecutionNodeId, systemUserId, systemUserName, systemUserTypeId);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl, false);
            Response.End();
        }

        private void LoadTreeMenu(int pintNodeId, int pintSystemUserId, string pstrUserName, int pintSystemUserTypeId)
        {
            // Cargar permisos contextuales / Globales
            OperationResult objOperationResult2 = new OperationResult();
            List<AutorizationList> objAuthorizationList = null;
            //
            if (pintSystemUserTypeId == (int)SystemUserTypeId.Internal)
            {
                objAuthorizationList = _objSecurityBL.GetAuthorization(ref objOperationResult2, pintNodeId, pintSystemUserId);
            }
            else if (pintSystemUserTypeId == (int)SystemUserTypeId.External)
            {
                objAuthorizationList = _objSecurityBL.GetAuthorizationExternal(ref objOperationResult2, pintNodeId, pintSystemUserId);
                Session["CertificadoAptitud"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 1087) != null ? true : false);
                Session["FichaOcupacional"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 1086) != null ? true : false);
                Session["Examenes"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 165) != null ? true : false);

                Session["ExamenAltura"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 199) != null ? true : false);
                Session["FMT1"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 200) != null ? true : false);
                Session["Interconsultas"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 201) != null ? true : false);
                Session["ArchivosAdjuntos"] = (objAuthorizationList.Find(p => p.I_ApplicationHierarchyId == 202) != null ? true : false);

                objAuthorizationList = objAuthorizationList.FindAll(p => p.I_ApplicationHierarchyTypeId == (int)AppHierarchyType.PantallaOpcionDeMenu || p.I_ApplicationHierarchyTypeId == (int)AppHierarchyType.AgrupadorDeMenu);
            }

            Sigesoft.Server.WebClientAdmin.BLL.Utils.AddPaginasToCache(pstrUserName, objAuthorizationList, System.Web.HttpContext.Current);

            treeMenu.Nodes.Clear();

            Utils.loadTreeMenuAuthorized(treeMenu, objAuthorizationList);

        }    
    }
}