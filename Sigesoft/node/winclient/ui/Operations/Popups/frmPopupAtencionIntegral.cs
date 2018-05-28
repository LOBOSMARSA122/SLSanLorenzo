using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmPopupAtencionIntegral : Form
    {
        string _fromulario;
        string _modo;
        string _personId;
        planintegralDto obj;
        problemaDto objProblema;
        public frmPopupAtencionIntegral(string formulario, string modo, string personId)
        {
            InitializeComponent();
            _fromulario = formulario;
            _modo = modo;
            _personId = personId;
        }

        private void frmPopupAtencionIntegral_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (_fromulario == "Cronico")
            {
                Utils.LoadDropDownList(cboEsControlado, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                cboTipoAtencionIntegral.Enabled = false;
            }
            else if (_fromulario == "Agudo")
            {
                cboTipoAtencionIntegral.Enabled = false;
                cboEsControlado.Enabled = false;
            }
            else if (_fromulario == "Plan")
            {
                Utils.LoadDropDownList(cboTipoAtencionIntegral, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 257, null), DropDownListAction.Select);
                cboEsControlado.Enabled = false;
                label4.Text = "Descripción";
            }

            if (_modo == "New")
            {
                
            }
            else if (_modo == "Edit")
            {
                if (_fromulario == "Cronico")
                {
                    objProblema = new problemaDto();
                    objProblema = new ProblemaBL().GetProblema(ref objOperationResult, _personId);                   
                    dtpFecha.Value = objProblema.d_Fecha.Value;
                    txtObservacion.Text = objProblema.v_Descripcion;
                }
                else if (_fromulario == "Agudo")
                {

                    objProblema = new problemaDto();
                    objProblema = new ProblemaBL().GetProblema(ref objOperationResult, _personId);
                    dtpFecha.Value = objProblema.d_Fecha.Value;
                    txtObservacion.Text = objProblema.v_Descripcion;
                }
                else if (_fromulario == "Plan")
                {

                    obj = new planintegralDto();
                    obj = new PlanIntegralBL().GetPlanIntegral(ref objOperationResult, _personId);
                    cboTipoAtencionIntegral.SelectedValue = obj.i_TipoId;
                    dtpFecha.Value = obj.d_Fecha.Value;
                    txtObservacion.Text = obj.v_Descripcion;
                    txtLugar.Text = obj.v_Lugar;
                }

            }

        }

        
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            OperationResult objOperationResult = new OperationResult();

            if (cboTipoAtencionIntegral.SelectedValue == "-1")
            {
                MessageBox.Show("Por favor ingrese un Tipo de Plan.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtProblema.Text == "")
            {
                MessageBox.Show("Por favor ingrese un Tipo de Plan.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_fromulario == "Plan")
            {
                if (_modo == "New")
                {
                    obj = new planintegralDto();
                    obj.v_PersonId = _personId;
                    obj.i_TipoId = int.Parse(cboTipoAtencionIntegral.SelectedValue.ToString());
                    obj.v_Descripcion = txtObservacion.Text;
                    obj.d_Fecha = dtpFecha.Value;
                    obj.v_Lugar = txtLugar.Text;
                    new PlanIntegralBL().AddPlanIntegral(ref objOperationResult, obj, Globals.ClientSession.GetAsList());
                }
                else if (_modo == "Edit")
                {
                    obj.i_TipoId = int.Parse(cboTipoAtencionIntegral.SelectedValue.ToString());
                    obj.v_Descripcion = txtObservacion.Text;
                    obj.d_Fecha = dtpFecha.Value;
                    obj.v_Lugar = txtLugar.Text;
                    new PlanIntegralBL().UpdatePlanIntegral(ref objOperationResult, obj, Globals.ClientSession.GetAsList());
                }
            }
            else if (_fromulario == "Cronico")
            {
                if (_modo == "New")
                {
                    objProblema = new problemaDto();
                    objProblema.v_PersonId = _personId;
                    objProblema.v_Descripcion = txtObservacion.Text;
                    objProblema.d_Fecha = dtpFecha.Value;
                    objProblema.i_Tipo = (int)TipoProblema.Cronico;
                    new ProblemaBL().AddProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
                else if (_modo == "Edit")
                {
                    objProblema.i_Tipo = (int)TipoProblema.Cronico;
                    objProblema.v_Descripcion = txtObservacion.Text;
                    objProblema.d_Fecha = dtpFecha.Value;
                    new ProblemaBL().AddProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
            }
            else if (_fromulario == "Agudo")
            {
                if (_modo == "New")
                {
                    objProblema = new problemaDto();
                    objProblema.v_PersonId = _personId;
                    objProblema.v_Descripcion = txtObservacion.Text;
                    objProblema.d_Fecha = dtpFecha.Value;
                    objProblema.i_Tipo = (int)TipoProblema.Agudo;
                    new ProblemaBL().AddProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
                else if (_modo == "Edit")
                {
                    objProblema.i_Tipo = (int)TipoProblema.Agudo;
                    objProblema.v_Descripcion = txtObservacion.Text;
                    objProblema.d_Fecha = dtpFecha.Value;
                    new ProblemaBL().UpdateProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
            }

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else  // Operación con error
            {
                if (objOperationResult.ErrorMessage != null)
                {
                    MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }
            }
        }
    }
}
