using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmUpdateAdditionalExam : Form
    {
        private string _ComponentId = "";
        public frmUpdateAdditionalExam(string componentId)
        {
            _ComponentId = componentId;
            InitializeComponent();
        }

        private void frmUpdateAdditionalExam_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbComponent, "Value1", "Id", BLL.Utils.GetAllComponents_(ref objOperationResult), DropDownListAction.Select);
            cbComponent.SelectedValue = _ComponentId;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (cbComponent.SelectedValue.ToString() == _ComponentId)
            {
                MessageBox.Show("Por favor, escoja otro examen.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var newComponentId = cbComponent.SelectedValue.ToString();

        }

        
    }
}
