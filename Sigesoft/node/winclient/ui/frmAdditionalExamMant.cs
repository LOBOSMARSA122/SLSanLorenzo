using Sigesoft.Node.WinClient.BE.Custom;
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
    public partial class frmAdditionalExamMant : Form
    {
        private string _serviceId = "";
        private List<AdditionalExamUpdate> _DataSource = new List<AdditionalExamUpdate>();
        public frmAdditionalExamMant(string serviceId, List<AdditionalExamUpdate> data)
        {
            if (data != null)
            {
                _DataSource = data;
            }
            _serviceId = serviceId;
            InitializeComponent();
        }

        private void frmAdditionalExamMant_Load(object sender, EventArgs e)
        {
            grdDataAdditionalExam.DataSource = _DataSource;
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (_DataSource.Count > 0)
            {
                
                var data = _DataSource.FindAll(x => x.v_ComponentName.Contains(txtName.Text.ToUpper())).ToList();
                grdDataAdditionalExam.DataSource = data;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var Dialog = MessageBox.Show("¿ Seguro de eliminar las filas seleccionadas ?", "CONFIRMACIÓN",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Dialog == DialogResult.Yes)
	        {
                foreach (var row in grdDataAdditionalExam.Selected.Rows)
                {
                    var serviceId = row.Cells["v_ServiceId"].Value.ToString();
                    var componentId = row.Cells["v_ComponentId"].Value.ToString();

                    //new AdditionalExamBL().DeleteAdditionalExam(serviceId, componentId, Globals.ClientSession.i_SystemUserId);
                } 
	        }
            else
            {
                return;
            }
            
        }
    }
}
