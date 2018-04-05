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
    public partial class frmWaiting : Form
    {
        private string _messageText;

        public frmWaiting(string messageText)
        {
            InitializeComponent();
            _messageText = messageText;
            lblMessageText.Text = _messageText;

        }
    }
}
