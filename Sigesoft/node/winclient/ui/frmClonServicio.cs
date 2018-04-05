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
    public partial class frmClonServicio : Form
    {
        string pServiceId = "";
        public frmClonServicio(string pstrService)
        {
            pServiceId = pstrService;
            InitializeComponent();
        }

        private void btnClonar_Click(object sender, EventArgs e)
        {
            if (uvClon.Validate(true, false).IsValid)
            {
                using (new LoadingClass.PleaseWait(this.Location, "Clonando..."))
                {
                    ServiceBL bl = new ServiceBL();
                    bl.ClonarServicio(pServiceId, ddlProtocolId.SelectedValue.ToString(), ddlCustomerOrganization.SelectedValue.ToString(), dtpDateTimeStar.Value);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                };
            }
          
        }

        private void frmClonServicio_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.Select);
           
        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }

            ddlProtocolId.Enabled = true;

            OperationResult objOperationResult = new OperationResult();

            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);          
            
        }
    }
}
