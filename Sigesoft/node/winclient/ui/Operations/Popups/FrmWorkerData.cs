using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class FrmWorkerData : Form
    {
        private readonly string _serviceId;

        public FrmWorkerData(string serviceId)
        {
            _serviceId = serviceId;
            InitializeComponent();
        }

        private void FrmWorkerData_Load(object sender, EventArgs e)
        {
            WorkerData datos = null;
            Task.Factory.StartNew(() =>
            {
                 datos = new ServiceBL().GetWorkerData(_serviceId);
            }).ContinueWith(t =>
            {
                if (datos == null) return;
                lblTrabajador.Text = datos.Trabajador;
                if (datos.FechaNacimiento != null)
                    lblEdad.Text = Common.Utils.GetAge(datos.FechaNacimiento.Value).ToString();
                lblPuesto.Text = datos.Puesto;
                lblGenero.Text = datos.Genero;
                lblProtocolName.Text = datos.Protocolo;
                lblGeso.Text = datos.Grupo;
                lblTipoEso.Text = datos.TipoExamen;
                if (datos.PersonImage != null)
                    pbPersonImage.Image = Common.Utils.byteArrayToImage(datos.PersonImage);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
