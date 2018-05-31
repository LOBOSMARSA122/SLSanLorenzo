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
        string _id;
        planintegralDto obj;
        problemaDto objProblema;
        public frmPopupAtencionIntegral(string formulario, string modo, string personId, string id)
        {
            InitializeComponent();
            _fromulario = formulario;
            _modo = modo;
            _personId = personId;
            _id = id;
        }

        private void frmPopupAtencionIntegral_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (_fromulario == "Cronico")
            {
                Utils.LoadDropDownList(cboEsControlado, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                cboTipoAtencionIntegral.Enabled = false;
                txtLugar.Visible = false;
                label6.Visible = false;
            }
            else if (_fromulario == "Agudo")
            {
                cboTipoAtencionIntegral.Enabled = false;
                cboEsControlado.Enabled = false;
                txtLugar.Visible = false;
                label6.Visible = false;
            }
            else if (_fromulario == "Plan")
            {
                Utils.LoadDropDownList(cboTipoAtencionIntegral, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 281, null), DropDownListAction.Select);
                cboEsControlado.Enabled = false;
                txtProblema.Enabled = false;
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
                    objProblema = new ProblemaBL().GetProblema(ref objOperationResult, _id);                   
                    dtpFecha.Value = objProblema.d_Fecha.Value;
                    txtProblema.Text = objProblema.v_Descripcion;
                    cboEsControlado.SelectedValue = objProblema.i_EsControlado.ToString(); 
                    txtObservacion.Text = objProblema.v_Observacion;
                }
                else if (_fromulario == "Agudo")
                {

                    objProblema = new problemaDto();
                    objProblema = new ProblemaBL().GetProblema(ref objOperationResult, _id);
                    dtpFecha.Value = objProblema.d_Fecha.Value;
                    txtProblema.Text = objProblema.v_Descripcion;
                    txtObservacion.Text = objProblema.v_Observacion;
                }
                else if (_fromulario == "Plan")
                {

                    obj = new planintegralDto();
                    obj = new PlanIntegralBL().GetPlanIntegral(ref objOperationResult, _id);
                    cboTipoAtencionIntegral.SelectedValue = obj.i_TipoId;
                    dtpFecha.Value = obj.d_Fecha.Value;
                    txtObservacion.Text = obj.v_Descripcion;
                    txtLugar.Text = obj.v_Lugar;
                    cboTipoAtencionIntegral.SelectedValue = obj.i_TipoId.ToString();
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

            //if (txtProblema.Text == "")
            //{
            //    MessageBox.Show("Por favor ingrese un Tipo de Plan.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

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
                    obj.v_PlanIntegral = _id;
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
                    objProblema.i_Tipo = (int)TipoProblema.Cronico;
                    objProblema.d_Fecha = dtpFecha.Value;
                    objProblema.v_Descripcion = txtProblema.Text;
                    objProblema.i_EsControlado = int.Parse(cboEsControlado.SelectedValue.ToString());
                    objProblema.v_Observacion = txtObservacion.Text;
                    
                    new ProblemaBL().AddProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
                else if (_modo == "Edit")
                {
                    objProblema.v_ProblemaId = _id;
                    objProblema.i_Tipo = (int)TipoProblema.Cronico;
                    objProblema.d_Fecha = dtpFecha.Value;
                    objProblema.v_Descripcion = txtProblema.Text;
                    objProblema.i_EsControlado = int.Parse(cboEsControlado.SelectedValue.ToString());
                    objProblema.v_Observacion = txtObservacion.Text;
                    new ProblemaBL().UpdateProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
            }
            else if (_fromulario == "Agudo")
            {
                if (_modo == "New")
                {
                    objProblema = new problemaDto();
                    objProblema.v_PersonId = _personId;
                    objProblema.v_Descripcion = txtProblema.Text;
                    objProblema.d_Fecha = dtpFecha.Value;
                    objProblema.v_Observacion = txtObservacion.Text;
                    objProblema.i_Tipo = (int)TipoProblema.Agudo;
                    new ProblemaBL().AddProblema(ref objOperationResult, objProblema, Globals.ClientSession.GetAsList());
                }
                else if (_modo == "Edit")
                {
                    objProblema.v_ProblemaId = _id;
                    objProblema.i_Tipo = (int)TipoProblema.Agudo;
                    objProblema.v_Descripcion = txtProblema.Text;
                    objProblema.d_Fecha = dtpFecha.Value;
                    objProblema.v_Observacion = txtObservacion.Text;
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
