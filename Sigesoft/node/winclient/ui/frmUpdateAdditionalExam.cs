using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmUpdateAdditionalExam : Form
    {
        private string _ComponentId = "";
        private string _AdditionalExamId = "";
        public frmUpdateAdditionalExam(string componentId, string AdditionalExamId)
        {
            _AdditionalExamId = AdditionalExamId;
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
            bool result = true;
            if (cbComponent.SelectedValue.ToString() == _ComponentId)
            {
                MessageBox.Show("Por favor, escoja otro examen.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            var newComponentId = cbComponent.SelectedValue.ToString();
            using (new LoadingClass.PleaseWait(this.Location, "Actualizando..."))
            {
                result = new AdditionalExamBL().UpdateComponentAdditionalExam(newComponentId, _AdditionalExamId, Globals.ClientSession.i_SystemUserId);
            }
          
            if (result)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Sucedió un error, vuel a a intentar.", "ERROR", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        
    }
}
