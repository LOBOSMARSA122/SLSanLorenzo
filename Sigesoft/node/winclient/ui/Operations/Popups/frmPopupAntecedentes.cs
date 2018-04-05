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

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmPopupAntecedentes : Form
    {
        private ServiceBL _serviceBL = new ServiceBL();
        private string _personId;
        public frmPopupAntecedentes(string psrtPersonId)
        {
            InitializeComponent();
            _personId = psrtPersonId;
        }

        private void frmPopupAntecedentes_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var antecedent = _serviceBL.GetAntecedentConsolidateForService(ref objOperationResult, _personId);

            if (antecedent == null)
                return;

            grdAntecedentes.DataSource = antecedent;

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerEditarAntecedentes_Click(object sender, EventArgs e)
        {
            ViewEditAntecedent();
        }
        private void ViewEditAntecedent()
        {
            frmHistory frm = new frmHistory(_personId);
            frm.ShowDialog();
            // refresca grilla de antecedentes
            GetAntecedentConsolidateForService(_personId);
        }

        private void GetAntecedentConsolidateForService(string personId)
        {
            OperationResult objOperationResult = new OperationResult();
            var antecedent = _serviceBL.GetAntecedentConsolidateForService(ref objOperationResult, personId);

            if (antecedent == null)
                return;

            grdAntecedentes.DataSource = antecedent;

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
