using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmChangeGeso : Form
    {
        private readonly string _organizationName;
        private readonly List<KeyValueDTO> _gesos;

        public string Geso { get; set; }
        public string GesoId { get; set; }

        public FrmChangeGeso(string organizationName, List<KeyValueDTO> gesos)
        {
            _organizationName = organizationName;
            _gesos = gesos;
            InitializeComponent();
        }

        private void FrmChangeGeso_Load(object sender, EventArgs e)
        {
            txtOrganizationName.Text = _organizationName;
            Utils.LoadDropDownList(cboGeso, "Value1", "Id", _gesos, DropDownListAction.Select);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Geso = cboGeso.Text;
            GesoId = cboGeso.SelectedValue.ToString();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
