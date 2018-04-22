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

namespace Sigesoft.Node.WinClient.UI.History
{
    public partial class frmPersonMedicalPopup : Form
    {
        public int _TypeDiagnosticId;
        public string _TypeDiagnosticName;
        public DateTime? _StartDate = null;
        public string _DiagnosticDetail;
        public DateTime _Date;
        public string _TreatmentSite;
        public string _Hospital;
        public string _Complicaciones;


        public frmPersonMedicalPopup(string DiagnosticName, int TypeDiagnosticId, DateTime StartDate, string DiagnosticDetail, DateTime? Date, string TreatmentSite, string Hospital, string Complicaciones)
        {
            InitializeComponent();

            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlTypeDiagnosticId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 139, null), DropDownListAction.Select);
        
            this.Text = this.Text + DiagnosticName;
            ddlTypeDiagnosticId.SelectedValue = TypeDiagnosticId.ToString();
            dtpDateTimeStar.Value = StartDate;
            txtDxDetail.Text = DiagnosticDetail;
            txtTreatmentSite.Text = TreatmentSite;
            txtHospital.Text = Hospital;
            txtComplicaciones.Text = Complicaciones;
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


        private void frmPersonMedicalPopup_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (uvPersonMedicalPopup.Validate(true, false).IsValid)
            {
                _TypeDiagnosticId = int.Parse(ddlTypeDiagnosticId.SelectedValue.ToString());
                _TypeDiagnosticName = ddlTypeDiagnosticId.Text;
                _StartDate = dtpDateTimeStar.Value.Date;
                _DiagnosticDetail = txtDxDetail.Text;
                _TreatmentSite = txtTreatmentSite.Text;

                _Hospital = txtHospital.Text;
                _Complicaciones = txtComplicaciones.Text;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtComplicaciones_Click(object sender, EventArgs e)
        {

        }
    }
}
