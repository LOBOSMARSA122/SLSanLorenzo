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
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Sigesoft.Node.Contasol.Integration;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmLiquidacionReport : Form
    {
        private ServiceBL _serviceBL = new ServiceBL();
        private PacientBL _pacientBL = new PacientBL();
        private OperationResult _objOperationResult = new OperationResult();
        private HospitalizacionBL _hospitBL = new HospitalizacionBL();
        string hospiId = null;
        private OperationResult objOperationResult = new OperationResult();
        private hospitalizacionserviceDto hospser = new hospitalizacionserviceDto();
        public frmLiquidacionReport(string _hospiId)
        {
            hospiId = _hospiId;
            InitializeComponent();
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                var lista = _hospitBL.GetHospitalizcion(ref _objOperationResult, hospiId);

                //var serviceId = lista.SelectMany(p => p.Servicios.Select(q=>q.v_ServiceId));
                int doctor = 1;
                hospser = _hospitBL.GetHospitServ(hospiId);

                var _DataService = _serviceBL.GetServiceReport(hospser.v_ServiceId);
                var datosP = _pacientBL.DevolverDatosPaciente(hospser.v_ServiceId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();
                ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, hospser.v_ServiceId);

                string nombre = personData.v_DocNumber + "_" + personData.v_ProtocolName;
                Liquidacion_Hospitalizacion.CreateLiquidacion(ruta + nombre + ".pdf", MedicalCenter, lista, _DataService, datosP, doctor);
                this.Enabled = true;
            }
            this.Close();
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                var lista = _hospitBL.GetHospitalizcion(ref _objOperationResult, hospiId);

                //var serviceId = lista.SelectMany(p => p.Servicios.Select(q=>q.v_ServiceId));
                int paciente = 2;
                hospser = _hospitBL.GetHospitServ(hospiId);

                var _DataService = _serviceBL.GetServiceReport(hospser.v_ServiceId);
                var datosP = _pacientBL.DevolverDatosPaciente(hospser.v_ServiceId);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, hospser.v_ServiceId);

                string nombre = personData.v_DocNumber + "_" + personData.v_ProtocolName;
                Liquidacion_Hospitalizacion.CreateLiquidacion(ruta + nombre + ".pdf", MedicalCenter, lista, _DataService, datosP, paciente);
                this.Enabled = true;
            }
            this.Close();
        }
    }
}
