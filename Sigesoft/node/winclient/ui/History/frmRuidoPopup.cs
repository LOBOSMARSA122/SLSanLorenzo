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
    public partial class frmRuidoPopup : Form
    {
        public int TiempoExposicionRuidoId { get; set; }
        public int NivelRuidoId { get; set; }
        public string FuenteRuido { get; set; }

        public frmRuidoPopup()
        {
            InitializeComponent();
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


        private void frmRuidoPopup_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(cbTiempoExposicionRuido, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, (int)SystemParameterGroups.TiempoExpsosicionRuido, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbNivelRuido, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, (int)SystemParameterGroups.NivelRuido, null), DropDownListAction.Select);
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TiempoExposicionRuidoId = Convert.ToInt32(cbTiempoExposicionRuido.SelectedValue);
            NivelRuidoId = Convert.ToInt32(cbNivelRuido.SelectedValue);
            FuenteRuido = txtFuenteRuido.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
