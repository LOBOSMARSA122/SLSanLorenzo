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


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPopupArea : Form
    {

        string _EmpresaSede;
        string _EmpresaId;
        string _SedeId;
        string _ServicioId;

        public frmPopupArea(string ServicioId,string EmpresaSede, string EmpresaId, string SedeId)
        {
            InitializeComponent();
            _ServicioId = ServicioId;
            _EmpresaSede = EmpresaSede;
            _EmpresaId = EmpresaId;
            _SedeId = SedeId;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            ServiceBL oServiceBL = new ServiceBL();

            if (uvArea.Validate(true, false).IsValid)
            {
                oServiceBL.ActualizarServicioArea(_ServicioId, cbArea.SelectedValue.ToString());

                MessageBox.Show("Se grabó correctamenete", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Seleccione un Área de Trabajo.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void frmPopupArea_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            lblEmpresaSede.Text = _EmpresaSede;

            Utils.LoadDropDownList(cbArea, "Value1", "Id", BLL.Utils.GetAreaSede(ref objOperationResult, _EmpresaId, _SedeId), DropDownListAction.Select);

        }
    }
}
