using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Common
{
    public partial class LoadingForm : Form
    {
        private string _message;

        public LoadingForm(string message)
        {
            InitializeComponent();
            //lblMessage.Show();
            lblMessage.Text = message;
            //lblMessage.Refresh();
            _message = message;
        }

        public LoadingForm()
        {
            InitializeComponent();
           
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            //lblMessage.Text = _message;
        }
    }
}