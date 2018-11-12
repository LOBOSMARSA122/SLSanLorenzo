using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmProduccionCategoria : Form
    {
        public frmProduccionCategoria()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ProfessionalBL oProfessionalBL = new ProfessionalBL();

           grdData.DataSource =  oProfessionalBL.GetFilterProduccionCategoria(dtpDateTimeStar.Value.Date, dptDateTimeEnd.Value.AddDays(1).Date);
        }
    }
}
