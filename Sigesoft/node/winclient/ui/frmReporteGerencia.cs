using Sigesoft.Common;
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
    public partial class frmReporteGerencia : Form
    {
        public frmReporteGerencia()
        {
            InitializeComponent();
        }

        private void frmReporteGerencia_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cboTipoCaja, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 316, null), DropDownListAction.All);

        }
    }
}
