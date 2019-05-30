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
    public partial class frmServiceDetail : Form
    {

        private string _ServiceId;
        private string _Fecha;
        private string _Name;
        public frmServiceDetail(string serviceId, string fecha, string pacientName)
        {
            _ServiceId = serviceId;
            _Fecha = fecha;
            _Name = pacientName;


            InitializeComponent();
        }

        private void frmServiceDetail_Load(object sender, EventArgs e)
        {
            this.Text = "Detalle de Atención de " + _Name + " - " + _Fecha;
            var DataSource = new ServiceDetailBL().GetDetailsByserviceId(_ServiceId);
            grdDetails.DataSource = DataSource;
            if (DataSource != null)
            {
                if (DataSource.Count > 0)
                {
                    if (DataSource[0].Aseguradora == null)
                    {
                        grdDetails.Rows[0].Cells["Aseguradora"].Column.Hidden = true;
                    }
                }
            }

        }

    }
}
