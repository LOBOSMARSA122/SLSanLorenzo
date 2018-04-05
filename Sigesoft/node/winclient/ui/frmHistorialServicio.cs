using Sigesoft.Node.WinClient.BE;
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
    public partial class frmHistorialServicio : Form
    {
        public frmHistorialServicio(List<ServiceList> listaServicios)
        {
            InitializeComponent();
            grdDataService.DataSource = listaServicios;
            lblRecordCountService.Text = string.Format("Se encontraron {0} registros.", listaServicios.Count());
           
        }
    }
}
