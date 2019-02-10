using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.NatclarXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.OperationsNatclar
{
    public partial class FormNatclar : Form
    {
        OperationsNatclarBl oNataclarBl = new OperationsNatclarBl(); 
        public FormNatclar()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            grdDataService.DataSource = oNataclarBl.Filter(dtpDateTimeStar.Value, dptDateTimeEnd.Value);
        }

        private void grdDataService_KeyDown(object sender, KeyEventArgs e)
        {
         

        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            var grid = (UltraGrid)sender;
            if (grid.ActiveCell == null) return;

            var column = grid.ActiveCell.Column.Key;

            if (column == "DatosPersonales")
            {
                MessageBox.Show("ssss");
            }
        }

        private void grdDataService_ClickCellButton(object sender, CellEventArgs e)
        {
            var serviceId = e.Cell.Row.Cells["v_ServiceId"].Value.ToString();
            if (e.Cell.Column.Key == "DatosPersonales")
            {
                WSRIProveedorExternoClient client = new WSRIProveedorExternoClient();

                var obj = new EstructuraDatosAPMedicos();
                obj.DatosPaciente = new XmlDatosPaciente();
                obj.DatosPaciente.Nombre = "Alberto";

                var result = client.EnviarDatosAPMedicos(obj);

                if (result.EstadoXML == "OK")
                {
                    MessageBox.Show("hola");
                }
            }
        }
    }
}
