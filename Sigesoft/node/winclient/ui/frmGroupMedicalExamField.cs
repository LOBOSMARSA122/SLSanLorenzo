using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmGroupMedicalExamField : Form
    {
        public string _GroupName;
        public frmGroupMedicalExamField(string Group)
        {
            InitializeComponent();
            _GroupName = Group;
        }

        private void frmGroupMedicalExamField_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlGroup, "Value1", "", BLL.Utils.GetMedicalExamGrupo(ref objOperationResult,null), DropDownListAction.Select);
            if (_GroupName !="")
            {
                ddlGroup.Text = _GroupName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ultraValidator1.Validate(true, false).IsValid)
            {
                if (ddlGroup.Text.Trim() == "")
                {
                    MessageBox.Show("Este campo no puede estar vacio ", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _GroupName = ddlGroup.Text.ToString();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           

        }
    }
}
