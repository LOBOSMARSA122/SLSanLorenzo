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
    public partial class MensajeAlerta : Form
    {
        string _Comentario;
        string _Fecha;
        public MensajeAlerta(string pstrComentario, DateTime pstrFecha)
        {
            _Comentario = pstrComentario;
            _Fecha = pstrFecha.Date.ToShortDateString();
            InitializeComponent();
        }

        private void MensajeAlerta_Load(object sender, EventArgs e)
        {
            lblFecha.Text = _Fecha;
            txtComentario.Text = _Comentario;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
