using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAddSolicitudCarta : Form
    {
        string _service;
        public frmAddSolicitudCarta(string service)
        {
            _service = service;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click()
        {
            CalendarBL _calendar = new CalendarBL();
            string nroCarta = txtNroCartaSolicitud.Text;
            _calendar.RegistrarCarta(_service, nroCarta);
        }
    }
}
