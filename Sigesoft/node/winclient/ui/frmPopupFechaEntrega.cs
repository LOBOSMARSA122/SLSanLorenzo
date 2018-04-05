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
    public partial class frmPopupFechaEntrega : Form
    {

        List<string> _ListaServicios = new List<string>();
        string _formulario = "";
        public frmPopupFechaEntrega(List<string> ListaServicios,string formulario)
        {
            InitializeComponent();
            _ListaServicios = ListaServicios;
            _formulario = formulario;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            ServiceBL oServiceBL = new ServiceBL();

            if (_formulario=="service")
            {
                foreach (var item in _ListaServicios)
                {
                    oServiceBL.ActualizarFechaEntregaServicio(item.ToString(), dtpDateTimeStar.Value);
                }

            }
            else if (_formulario == "calendar")
            {
                //OperationResult objOperationResult = new OperationResult();
                //foreach (var item in _ListaServicios)
                //{
                //    //oServiceBL.ActualizarFechaIniciarCircuitoCalendar(item.ToString(), dtpDateTimeStar.Value);
                //    objCalendarBL.CircuitStart(ref objOperationResult, item.ToString(), DateTime.Now, Globals.ClientSession.GetAsList());

                //}

            }
          
            MessageBox.Show("Se grabó correctamente", "SISTEMAS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
     

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void frmPopupFechaEntrega_Load(object sender, EventArgs e)
        {

        }
    }
}
