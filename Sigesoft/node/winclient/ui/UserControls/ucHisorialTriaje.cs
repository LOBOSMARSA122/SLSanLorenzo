using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucHisorialTriaje : UserControl
    {
        private ServiceBL oServiceBL = new ServiceBL();
        public string PersonId { get; set; }

        public ucHisorialTriaje()
        {
            InitializeComponent();
        }

        private void ucHisorialTriaje_Load(object sender, EventArgs e)
        {
            var result = oServiceBL.historialComponente(PersonId, Constants.ANTROPOMETRIA_ID);
            List<HistorialTriaje> listaHistTriaje = new List<HistorialTriaje>();
            HistorialTriaje oHistorialTriaje;

            var servicios = result.GroupBy(p => p.v_ServicioId).Select(s => s.First());

            foreach (var oServicio in servicios)
            {
                var triajeServicio = result.FindAll(p => p.v_ServicioId == oServicio.v_ServicioId);
                
                oHistorialTriaje = new HistorialTriaje();

                var dServiceDate = triajeServicio[0].d_ServiceDate;
                if (dServiceDate != null)
                    oHistorialTriaje.v_FechaServicio = dServiceDate.Value.ToString("dd-MM-yyyy");
                oHistorialTriaje.v_Peso = triajeServicio.Find(p => p.v_ComponentFieldId == "N002-MF000000008").v_Value1;
                oHistorialTriaje.v_Talla = triajeServicio.Find(p => p.v_ComponentFieldId == "N002-MF000000007").v_Value1;
                oHistorialTriaje.v_Imc = triajeServicio.Find(p => p.v_ComponentFieldId == "N002-MF000000009").v_Value1;

                listaHistTriaje.Add(oHistorialTriaje);
            }

            grdHistorialTriaje.DataSource = listaHistTriaje;

        }

      
    }
}
