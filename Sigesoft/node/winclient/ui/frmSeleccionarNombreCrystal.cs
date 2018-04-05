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
using System.IO;
using System.Drawing.Imaging;
using Infragistics.Win.UltraWinGrid;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmSeleccionarNombreCrystal : Form
    {
        string _ComponenteId ="";
        public string NombreCrystal = "";
        public int IdNombreCrystal = 0;

        public frmSeleccionarNombreCrystal(string pstrComponenteId)
        {
            _ComponenteId = pstrComponenteId;
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            NombreCrystal = cboNombreReporte.Text.ToString();
            IdNombreCrystal = int.Parse(cboNombreReporte.SelectedValue.ToString());
            this.DialogResult = DialogResult.OK;

        }

        private void frmSeleccionarNombreCrystal_Load(object sender, EventArgs e)
        { OperationResult objOperationResult = new OperationResult();

        SystemParameterBL oSystemParameterBL = new SystemParameterBL();

            //Obtner Id del Padre , grupo 123
       var o =  oSystemParameterBL.ObtenerIdPadreDataHierarchy(ref objOperationResult, _ComponenteId);


       Utils.LoadDropDownList(cboNombreReporte, "Value1", "Id", BLL.Utils.GetDataHierarchyByParentIdForCombo(ref objOperationResult, 123, o.i_ItemId, ""), DropDownListAction.Select);
        }
    }
}
