using NetPdf;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRMOrdenReportes : System.Web.UI.Page
    {
        List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
        ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId;
        private string _EmpresaClienteId;
        private int _flagPantalla;
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        private string _pacientId;
        private string _tempSourcePath;
        private string _customerOrganizationName;
        private string _personFullName;
        private List<string> _filesNameToMerge = new List<string>();
        List<ServiceComponentList> _listaDosaje = new List<ServiceComponentList>();
        DataSet dsGetRepo = null;
        string ruta;
        int _Eso;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            _serviceId = Request.QueryString["_serviceId"].ToString();
            _pacientId = Request.QueryString["_pacientId"].ToString();
            _customerOrganizationName = Request.QueryString["_customerOrganizationName"].ToString();
            _personFullName = Request.QueryString["_personFullName"].ToString();
            _flagPantalla = int.Parse(Request.QueryString["flagPantalla"].ToString());
            _EmpresaClienteId = Request.QueryString["_EmpresaClienteId"].ToString();
            _Eso = int.Parse(Request.QueryString["Eso"].ToString());

            OrganizationBL oOrganizationBL = new OrganizationBL();
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> ListaFinalOrdena = new List<ServiceComponentList>();
            List<ServiceComponentList> ListaOrdenada = new List<ServiceComponentList>();
            ListaOrdenada = ConsolidadoReportes.OrderBy(p => p.Orden).ToList();

            var ListaOrdenReportes = oOrganizationBL.GetOrdenReportes(ref objOperationResult, _EmpresaClienteId);

            chkregistros.DataTextField = "v_NombreReporte";
            chkregistros.DataValueField = "v_ComponenteId";
            chkregistros.DataSource = ListaOrdenReportes;
            chkregistros.DataBind();

        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }

        protected void chkregistros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}