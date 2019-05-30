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

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmHolidays : Form
    {
        private int Accion = 0;
        private int Agregar = 1;
        private int Actualizar = 2;
        private string holidayId = "";
        HolidayBL _holidayBL = new HolidayBL();

        public frmHolidays()
        {
            InitializeComponent();
        }

        private void frmHolidays_Load(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void BindingGrid()
        {
            var DataSource = _holidayBL.GetHolidays(txtReason.Text);
            grdHolidays.DataSource = DataSource;
        }

        private void itemNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            Accion = Agregar;
            gbUpdateAdd.Enabled = true;            
        }

        private void itemEditar_Click(object sender, EventArgs e)
        {
            
            Accion = Actualizar;           
            gbUpdateAdd.Enabled = true;

            var razon = grdHolidays.Selected.Rows[0].Cells["v_Reason"].Value.ToString();
            holidayId = grdHolidays.Selected.Rows[0].Cells["v_HolidayId"].Value.ToString();
            DateTime fecha = DateTime.Parse(grdHolidays.Selected.Rows[0].Cells["d_Date"].Value.ToString()).Date;

            txtMotivoAdd.Text = razon;
            dtFechaAdd.Value = fecha;
        }

        private void itemEliminar_Click(object sender, EventArgs e)
        {
            bool result = false;
            using (new LoadingClass.PleaseWait(this.Location, "Eliminando..."))
            {
                result = _holidayBL.DeleteHoliday(holidayId);
            }

            
            if (!result)
            {
                MessageBox.Show("Sucedió un error al eliminar, por favor vuelva a intentar.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            gbUpdateAdd.Enabled = false;
            LimpiarCampos();
            BindingGrid();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            holidaysDto objHoliday = new holidaysDto();
            
            objHoliday.d_Date = dtFechaAdd.Value.Date;
            objHoliday.i_Year = DateTime.Now.Year;
            objHoliday.v_Reason = txtMotivoAdd.Text;
            objHoliday.i_IsDeleted = (int)SiNo.NO;

            if (Accion == Actualizar)
            {
                
                objHoliday.v_HolidayId = holidayId;

                bool result = false;
                using (new LoadingClass.PleaseWait(this.Location, "Actualizando..."))
                {
                    result = _holidayBL.UpdateHoliday(objHoliday);
                }

                
                if (!result)
                {
                    MessageBox.Show("Sucedió un error al actualizar, por favor vuelva a intentar.", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gbUpdateAdd.Enabled = false;
                LimpiarCampos();
            }
            else if (Accion == Agregar)
            {
                bool result = false;
                using (new LoadingClass.PleaseWait(this.Location, "Insertando..."))
                {
                    result = _holidayBL.AddHoliday(objHoliday, Globals.ClientSession.i_CurrentExecutionNodeId);
                }

                if (!result)
                {
                    MessageBox.Show("Sucedió un error al agregar, por favor vuelva a intentar.", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gbUpdateAdd.Enabled = false;
                LimpiarCampos();
            }

            BindingGrid();
        }

        private void LimpiarCampos()
        {
            txtMotivoAdd.Text = "";
            dtFechaAdd.Value = DateTime.Now.Date;
        }

        private void grdHolidays_MouseDown(object sender, MouseEventArgs e)
        {
            if (grdHolidays.Rows.Count == 0)
            {
                cmHolidays.Items["itemEditar"].Enabled = false;
                cmHolidays.Items["itemEliminar"].Enabled = false;
            }
            if (grdHolidays.Selected.Rows.Count == 0)
            {
                cmHolidays.Items["itemEditar"].Enabled = false;
                cmHolidays.Items["itemEliminar"].Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void grdHolidays_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            LimpiarCampos();
            gbUpdateAdd.Enabled = false;
            cmHolidays.Items["itemEditar"].Enabled = true;
            cmHolidays.Items["itemEliminar"].Enabled = true;
        }

        
    }
}
