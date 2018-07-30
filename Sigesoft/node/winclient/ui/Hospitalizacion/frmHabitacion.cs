using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmHabitacion : Form
    {
        private string _hospitalizacion;
        public frmHabitacion(string hopitalizacionId, string mode, string hospitalizacionHabitacionId)
        {
            _hospitalizacion = hopitalizacionId;
            InitializeComponent();
        }

        private void frmHabitacion_Load(object sender, EventArgs e)
        {

        }
    }
}
