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
    public partial class frmBlackList : Form
    {
        BlackListBL _objBlackListBL = new BlackListBL();
        blacklistpersonDto objblacklistpersonDto;
        string _PersonId;
        public frmBlackList(string pstrPaciente, string pstrPersonId)
        {
            InitializeComponent();
            lblPaciente.Text = pstrPaciente;
            _PersonId = pstrPersonId;
        }

        private void frmBlackList_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            objblacklistpersonDto =  new blacklistpersonDto();
            objblacklistpersonDto.v_PersonId = _PersonId;
            objblacklistpersonDto.v_Comment = txtComentario.Text;
            objblacklistpersonDto.d_DateRegister = DateTime.Now;
            objblacklistpersonDto.i_Status = (int)StatusBlackListPerson.Registrado;

            var x = _objBlackListBL.GetBlackListByDay(ref objOperationResult, _PersonId, objblacklistpersonDto.d_DateRegister.Value.Date);

            if (x !=null)
            {
                 MessageBox.Show("El paciente ya se encuentra registrado el día de hoy en la Lista Negra", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
            }

            _objBlackListBL.AddBlackList(ref objOperationResult, objblacklistpersonDto, Globals.ClientSession.GetAsList());

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnAdmLista_Click(object sender, EventArgs e)
        {
            frmAdmBlackList frm = new frmAdmBlackList();
            frm.ShowDialog();
        }
    }
}
