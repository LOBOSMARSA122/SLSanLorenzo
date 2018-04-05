using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmRegisterFinding : Form
    {

        #region Properties

        public string ComponentName { get; set; }
        public string FieldName { get; set; }
        public string FindingText { get; set; }
        public string TextField { get; set; }
        public string ComponentId { get; set; }

        #endregion

        #region Contructor

        public frmRegisterFinding()
        {
            InitializeComponent();
        }

        public frmRegisterFinding(string examen, string textField, string fieldName, string componentId = null)
        {
            InitializeComponent();
            ComponentName = examen;
            TextField = textField;
            FieldName = fieldName;
            ComponentId = componentId;

        }
        #endregion
      
        private void frmRegisterFinding_Load(object sender, EventArgs e)
        {
            txtHallazgos.Select();
            this.Text = string.Format("Registro de Hallazgos <{0} / {1}> ", ComponentName, FieldName);
           
            if (ComponentId != null)
            {
                if (ComponentId == Constants.EXAMEN_FISICO_7C_ID)
                    txtHallazgos.Text = TextField;
            }
           
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FindingText = string.Format("{0}: {1}", FieldName, txtHallazgos.Text);                                   
            this.DialogResult = DialogResult.OK;
              
          
        }


    }
}
