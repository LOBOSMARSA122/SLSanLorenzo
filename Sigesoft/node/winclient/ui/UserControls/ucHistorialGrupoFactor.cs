using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucHistorialGrupoFactor : UserControl
    {
        private ServiceBL oServiceBL = new ServiceBL();
        public string PersonId { get; set; }

        public ucHistorialGrupoFactor()
        {
            InitializeComponent();
        }

        private void ucHistorialGrupoFactor_Load(object sender, EventArgs e)
        {
            var result = oServiceBL.historialComponente(PersonId, "N009-ME000000000");
            List<HistoriaGrupoFactor> listaHistGrupSanguineo = new List<HistoriaGrupoFactor>();
            HistoriaGrupoFactor oHistorialGrupFactor;

            var servicios = result.GroupBy(p => p.v_ServicioId).Select(s => s.First());

            foreach (var oServicio in servicios)
            {
                var grupoFactorServicio = result.FindAll(p => p.v_ServicioId == oServicio.v_ServicioId);

                oHistorialGrupFactor = new HistoriaGrupoFactor();

                var dServiceDate = grupoFactorServicio[0].d_ServiceDate;
                if (dServiceDate != null)
                    oHistorialGrupFactor.v_FechaServicio = dServiceDate.Value.ToString("dd-MM-yyyy");
                oHistorialGrupFactor.v_GrupoSanguineo = grupoFactorServicio.Find(p => p.v_ComponentFieldId == "N009-MF000000262") == null ? string.Empty : grupoFactorServicio.Find(p => p.v_ComponentFieldId == "N009-MF000000262").v_Value1Name;
                oHistorialGrupFactor.v_FactorSanguineo = grupoFactorServicio.Find(p => p.v_ComponentFieldId == "N009-MF000000263") == null ? string.Empty : grupoFactorServicio.Find(p => p.v_ComponentFieldId == "N009-MF000000263").v_Value1Name;

                listaHistGrupSanguineo.Add(oHistorialGrupFactor);
            }

            grdDataService.DataSource = listaHistGrupSanguineo;
        }

       
    }
}
