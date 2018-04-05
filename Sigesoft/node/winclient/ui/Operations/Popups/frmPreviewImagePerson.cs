using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmPreviewImagePerson : Form
    {
        byte[] _imagePerson;
        string _personName;

        public frmPreviewImagePerson(byte[] imagePerson, string personName)
        {
            InitializeComponent();
            _imagePerson = imagePerson;
            _personName = personName;
        }

        private void frmPreviewImagePerson_Load(object sender, EventArgs e)
        {
            this.Text = _personName;
            pbImagePerson.Image = Common.Utils.byteArrayToImage(_imagePerson);
            
        }
        /// <summary>
        /// liberar los recursos usado por el pictureBox (pbFile). es necesario para evitar errores de memoria insuficiente
        /// </summary>
        private void ImageDisposing()
        {
            if (pbImagePerson.Image != null)
            {
                pbImagePerson.Image.Dispose();

                pbImagePerson.Image = null;
            }
        }

        private void frmPreviewImagePerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageDisposing();
        }
    }
}
