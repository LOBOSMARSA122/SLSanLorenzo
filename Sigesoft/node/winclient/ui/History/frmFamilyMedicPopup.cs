using Sigesoft.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.History
{
    public partial class frmFamilyMedicPopup : Form
    {
        public string _CommentFamilyMedic;
        public string _Group;
        public int _ParentParameterId;
        public int? _ParameterId;
        public string _DiseasesId;
        public string _DiseasesName;

        public frmFamilyMedicPopup(string DiagnosticName, string Comment, string Group, int p_ParentParameterId, int? p_ParameterId)
        {
            InitializeComponent();
            this.Text = this.Text + Group  +" / " + DiagnosticName;
            txtComment.Text = Comment;
            _Group = Group;
            _ParentParameterId = p_ParentParameterId;
            if (_ParentParameterId ==-1)
            {
                cboDiagnostio.Visible = false;
                lblDx.Visible = true;
                lblDx.Text = DiagnosticName;
            }
           
            _ParameterId = p_ParameterId;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _CommentFamilyMedic = txtComment.Text;
            _ParameterId = int.Parse(cboDiagnostio.SelectedValue.ToString());
            _DiseasesId = ((KeyValueDTO)cboDiagnostio.SelectedItem).Value2;
            _DiseasesName = cboDiagnostio.Text.ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFamilyMedicPopup_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            Utils.LoadDropDownList(cboDiagnostio, "Value1", "Id", BLL.Utils.GetSystemParameterByParentForComboAntecedentesFamiliares(ref objOperationResult,_ParentParameterId), DropDownListAction.Select);

            if (_ParameterId != null)
            {
                cboDiagnostio.SelectedValue = _ParameterId.ToString();
            }
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }
    }
}
