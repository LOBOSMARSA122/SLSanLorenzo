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
        public FRMOrdenReportes(string serviceId, string pacientId, string customerOrganizationName, string personFullName, int pintFlagPantalla, string pstrEmpresaCliente, int eso)
        {
            _serviceId = serviceId;
            _pacientId = pacientId;
            _customerOrganizationName = customerOrganizationName;
            _personFullName = personFullName;
            _flagPantalla = pintFlagPantalla;
            _EmpresaClienteId = pstrEmpresaCliente;
            _Eso = eso;
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
            
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }
    }
}