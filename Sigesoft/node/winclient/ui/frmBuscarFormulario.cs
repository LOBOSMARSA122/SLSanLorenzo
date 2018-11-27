using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmBuscarFormulario : Form
    {
        private List<AutorizationList> _formularios;
        public frmBuscarFormulario( List<AutorizationList> frms)
        {
            this._formularios = frms;
            InitializeComponent();
        }

        private void btnBuscarFormulario_Click(object sender, EventArgs e)
        {
            try
            {
                string NombreFormulario = cboFormularios.SelectedValue.ToString();// ((ToolStripItem)sender).Tag.ToString();
                Assembly asm = Assembly.GetEntryAssembly();
                Type formtype = asm.GetType(NombreFormulario);

                if (formtype != null)
                {
                    Form f = (Form)Activator.CreateInstance(formtype);
                    f.ShowDialog();
                }         
            }
            catch (Exception ex)
            {
                MessageBox.Show("El formulario no existe, por favor revise su búsqueda", "INFORMACIÓN",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmBuscarFormulario_Load(object sender, EventArgs e)
        {
            var list = new List<KeyValueDTO>();

            foreach (var item in _formularios)
            {
                list.Add(new KeyValueDTO { Value1 = item.V_Description, Id = item.V_Form });
            }

            Utils.LoadDropDownList(cboFormularios, "Value1", "Id", list, DropDownListAction.Select);
        }

        private void frmBuscarFormulario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
