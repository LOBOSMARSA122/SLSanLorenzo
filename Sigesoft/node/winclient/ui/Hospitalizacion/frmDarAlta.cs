using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmDarAlta : Form
    {
        private string _hospitalizacionId;
        private string _mode;
        private DateTime? _fechaAlta;
        private string _comentario;
        private hospitalizacionDto _hospitalizacionDto = null;
        private HospitalizacionBL _hospitalizacionBL = new HospitalizacionBL();
       

        public frmDarAlta(string HopitalizacionId, string mode, DateTime? fechaAlta, string comentario )
        {
            _hospitalizacionId = HopitalizacionId;
            _mode = mode;
            _fechaAlta = fechaAlta;
            _comentario = comentario;
            InitializeComponent();
        }

        private void frmDarAlta_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
          
            if (_mode == "New")
            {
                dtpFechaAlta.Checked = false;
            }
            else if (_mode == "Edit")
            {
                if (_fechaAlta != null) dtpFechaAlta.Value = _fechaAlta.Value;
                txtComentario.Text = _comentario;
            }
        }

        private void btnGuardarTicket_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (_hospitalizacionDto == null)
            {
                _hospitalizacionDto = new hospitalizacionDto();
            }

            if (_mode == "New")
            {

            }
             else if (_mode == "Edit")
            {
                _hospitalizacionDto = _hospitalizacionBL.GetHospitalizacion(ref objOperationResult, _hospitalizacionId);
                _hospitalizacionDto.d_FechaAlta = (DateTime?)(dtpFechaAlta.Checked == false ? (ValueType)null : dtpFechaAlta.Value);
                _hospitalizacionDto.v_Comentario = txtComentario.Text;
                
                _hospitalizacionBL.UpdateHospitalizacion(ref objOperationResult, _hospitalizacionDto, Globals.ClientSession.GetAsList());


                

                
            }
            // hacer update al service culminado
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
