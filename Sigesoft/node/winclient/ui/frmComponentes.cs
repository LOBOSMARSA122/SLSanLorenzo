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
    public partial class frmComponentes : Form
    {
        public frmComponentes()
        {
            InitializeComponent();
        }
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();

        private void frmComponentes_Load(object sender, EventArgs e)
        {
            ddlComponentId.SelectedValueChanged -= ddlComponentId_SelectedValueChanged;
            OperationResult objOperationResult = new OperationResult();
          var ListaComponentes =    _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);
          Utils.LoadDropDownList(ddlComponentId, "Value3", "Value2", ListaComponentes, DropDownListAction.Select);

          ddlComponentId.SelectedValueChanged += ddlComponentId_SelectedValueChanged;
        }

        private void ddlComponentId_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

        }
    }
}
