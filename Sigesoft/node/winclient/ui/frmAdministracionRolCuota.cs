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
using Microsoft.Office.Interop.Excel;
using Infragistics.Documents.Excel;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System.Globalization;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdministracionRolCuota : Form
    {
        string _RolCuotaId;
        public frmAdministracionRolCuota()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmRolCuota frm = new frmRolCuota("New","");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                LLenarGrilla();
            }
        }

        private void frmAdministracionRolCuota_Load(object sender, EventArgs e)
        {
            LLenarGrilla();
        }

        void LLenarGrilla()
        {
            OperationResult objOperationResult = new OperationResult();
            RolCuotaBL oRolCuotaBL = new RolCuotaBL();

          
             var Lista=  oRolCuotaBL.GetRolCuotaPagedAndFiltered(ref objOperationResult, 0, null, "", "");

             grdData.DataSource= Lista;
             lblRecordCount.Text = string.Format("Se encontraron {0} registros.", Lista.Count());

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
          
            frmRolCuota frm = new frmRolCuota("Edit", _RolCuotaId);
            frm.ShowDialog();


        }

        private void grdData_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnEditar.Enabled=
                btnRemover.Enabled=
                (grdData.Selected.Rows.Count > 0);

            if (grdData.Selected.Rows.Count == 0)
                return;

            _RolCuotaId = grdData.Selected.Rows[0].Cells["v_RolCuotaId"].Value.ToString();
        }

      
    }
}
