using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Infragistics.Win.UltraWinToolbars;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmManagementReports : Form
    {
        #region Declarations
        GerenciaBl oGerenciaBl = new GerenciaBl();
        #endregion

        public FrmManagementReports()
        {
            InitializeComponent();
        }

        private void FrmManagementReports_Load(object sender, EventArgs e)
        {
        }

        private void ultraToolbarsManager1_ToolClick(object sender, ToolClickEventArgs e)
        {
            if (e.Tool.GetType() == typeof(ButtonTool))
            {
                var buttonKey = ((ButtonTool)e.Tool).Key;
                OpenForm(buttonKey);
            }
        }

        private void OpenForm(string formName)
        {
            var asm = Assembly.GetEntryAssembly();
            var formType = asm.GetType(formName);

            if (formType == null) return;
            var form = Application.OpenForms[formType.Name];
            if (form == null)
            {
                var f = (Form)Activator.CreateInstance(formType);
                f.MdiParent = this;
                f.Show();
            }
            else
                form.Activate();
        }

    }
}
