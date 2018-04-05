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
using NetPdf;
using Infragistics.Win.UltraWinGrid;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdministrarPuntos : Form
    {
        public frmAdministrarPuntos()
        {
            InitializeComponent();
        }

        int? _RolVentaId;
        private void frmAdministrarPuntos_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            dtpDateTimeStar.Value = fecha1;
            dptDateTimeEnd.Value = DateTime.Now;
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";


            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);


        }

        private void ddlUsuario_SelectedValueChanged(object sender, EventArgs e)
        {
            ProfessionalBL oProfessionalBL = new ProfessionalBL();
            OperationResult objOperationResult = new OperationResult();
            SystemUserList oSystemUserList = new SystemUserList();

            if (ddlUsuario.SelectedValue == null)
                return;

            if (ddlUsuario.SelectedValue.ToString() == "-1")
            {
                txtMedico.Text = "Nombres y Apellidos del Profesional";
                return;
            }

            oSystemUserList = oProfessionalBL.GetSystemUserName(ref objOperationResult, int.Parse(ddlUsuario.SelectedValue.ToString()));

            txtMedico.Text = oSystemUserList.v_PersonName;
            txtRolVenta.Text = oSystemUserList.v_RolVenta;
            _RolVentaId = oSystemUserList.i_RolVenta;

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
             OperationResult objOperationResult = new OperationResult();
            MovementBL oMovementBL = new MovementBL();


            if (uvFiltro.Validate(true, false).IsValid)
            {
                int IdAlmacen = int.Parse(Common.Utils.GetApplicationConfigValue("AlmacenId"));


                var Lista = oMovementBL.GetRevisionPuntosList(ref objOperationResult, int.Parse(ddlUsuario.SelectedValue.ToString()), _RolVentaId, dtpDateTimeStar.Value, dptDateTimeEnd.Value, IdAlmacen);
                grdData.DataSource = Lista;
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", Lista.Count());
            }
          
        
        }
    }
}
