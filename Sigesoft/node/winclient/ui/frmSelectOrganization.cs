using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmSelectOrganization : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        OrganizationBL oOrganizationBL = new OrganizationBL();
        public string organizationId { get; set; }
        private string[] _serviceIds;
        private List<string> _ClientSession;
        public frmSelectOrganization(string[] serviceIds, List<string> ClientSession)
        {
            _serviceIds = serviceIds;
            _ClientSession = ClientSession;
            InitializeComponent();
        }

        private void frmSelectOrganization_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);          
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //validar Empresa
            if (!oOrganizationBL.OrganizacionExisteByName(ddlCustomerOrganization.Text)) 
            {
                MessageBox.Show("La empresa no existe", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            organizationId = ddlCustomerOrganization.SelectedValue.ToString();

            _serviceBL.GenerarLiquidacion_Ocupacional(ref objOperationResult, _serviceIds, Globals.ClientSession.GetAsList(), organizationId);

            if (objOperationResult.Success == 1)
            {
                MessageBox.Show("Se grabó correctamente.", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else  
            {              
                MessageBox.Show(Constants.GenericErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
