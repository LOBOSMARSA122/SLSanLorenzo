using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Reports;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.WinClient.UI.Operations.Popups;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmEmbarazo : Form
    {
        private string _mode = null;
        private string _idperson = string.Empty;
        private string index = string.Empty;

        private EmbarazoBL _objEmbarazoBl = new EmbarazoBL();

        private embarzoDto embarazoDto = null;
        private string embarazoId = string.Empty;
        public frmEmbarazo(string mode, string personId, string id)
        {
            InitializeComponent();
            _mode = mode;
            _idperson = personId;
            index = id;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmbarazo_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();

            if (_mode == "Edit")
            {
                embarazoDto = _objEmbarazoBl.GetEmbarazo(ref objOperationResult, index);

                txtAnio.Text = embarazoDto.v_Anio;
                txtCpn.Text = embarazoDto.v_Cpn;
                textComplicacion.Text = embarazoDto.v_Complicacion;
                txtParto.Text = embarazoDto.v_Parto;
                txtPesoRn.Text = embarazoDto.v_PesoRn;
                txtPuerperio.Text = embarazoDto.v_Puerpio;

                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
           
            if (embarazoDto == null)
            {
                embarazoDto = new embarzoDto();
            }

            if (_mode == "New")
            {
                embarazoDto.v_PersonId = _idperson;
                embarazoDto.v_Anio = txtAnio.Text;
                embarazoDto.v_Cpn = txtCpn.Text;
                embarazoDto.v_Complicacion = textComplicacion.Text;
                embarazoDto.v_Parto = txtParto.Text;
                embarazoDto.v_PesoRn = txtPesoRn.Text;
                embarazoDto.v_Puerpio = txtPuerperio.Text;

                DialogResult Result = MessageBox.Show("¿Desea Guardar Embarazo?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    embarazoId = _objEmbarazoBl.AddEmbarazo(ref objOperationResult, embarazoDto, Globals.ClientSession.GetAsList());
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else if (_mode == "Edit")
            {
                embarazoDto.v_Anio = txtAnio.Text;
                embarazoDto.v_Cpn = txtCpn.Text;
                embarazoDto.v_Complicacion = textComplicacion.Text;
                embarazoDto.v_Parto = txtParto.Text;
                embarazoDto.v_PesoRn = txtPesoRn.Text;
                embarazoDto.v_Puerpio = txtPuerperio.Text;


                DialogResult Result = MessageBox.Show("¿Desea Guardar Embarazo?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    _objEmbarazoBl.UpdEmbarazo(ref objOperationResult, embarazoDto, Globals.ClientSession.GetAsList());
                    this.Close();
                }
                else
                {
                    this.Close();
                }

            }
        }
    }
}
