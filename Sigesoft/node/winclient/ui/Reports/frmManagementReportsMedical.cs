﻿using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmManagementReportsMedical : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        PacientBL _pacientBL = new PacientBL();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        private List<string> _filesNameToMerge = new List<string>();
        private string _serviceId;
        private string _EmpresaClienteId;
        private string _pacientId;
        private string _customerOrganizationName;
        private string _personFullName;
        string ruta;

        public frmManagementReportsMedical(string serviceId, string pacientId, string customerOrganizationName, string personFullName, string pstrEmpresaCliente)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _pacientId = pacientId;
            _customerOrganizationName = customerOrganizationName;
            _personFullName = personFullName;           
            _EmpresaClienteId = pstrEmpresaCliente;
        }

        private void frmManagementReportsMedical_Load(object sender, EventArgs e)
        {
            List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
            serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "HISTORIA CLÍNICA", v_ComponentId = Constants.FORMATO_ATENCION });
            chklConsolidadoReportes.DataSource = serviceComponents;
            chklConsolidadoReportes.DisplayMember = "v_ComponentName";
            chklConsolidadoReportes.ValueMember = "v_ComponentId";
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
            {
                chklChekedAll(chklConsolidadoReportes, true);
                chklConsolidadoReportes.Enabled = false;
                SelectChangeConsolidadoReportes();
                chkTodos.Text = "Deseleccionar Todos";
            }
            else
            {
                chklConsolidadoReportes.Enabled = true;
                chklChekedAll(chklConsolidadoReportes, false);
                SelectChangeConsolidadoReportes();
                chkTodos.Text = "Seleccionar Todos";
            }
        }

        private void chklChekedAll(CheckedListBox chkl, bool checkedState)
        {
            for (int i = 0; i < chkl.Items.Count; i++)
            {
                chkl.SetItemChecked(i, checkedState);
            }
        }

        private void SelectChangeConsolidadoReportes()
        {
            var s = GetChekedItems(chklConsolidadoReportes);

        }

        private List<string> GetChekedItems(CheckedListBox chkl)
        {
            List<string> componentId = new List<string>();

            for (int i = 0; i < chkl.CheckedItems.Count; i++)
            {
                ServiceComponentList com = (ServiceComponentList)chkl.CheckedItems[i];
                componentId.Add(com.v_ComponentId);
            }

            return componentId.Count == 0 ? null : componentId;
        }

        private void btnConsolidadoReportes_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Desea publicar a la WEB?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            OperationResult objOperationResult = new OperationResult();

            string ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
            string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
            string rutaConsolidado = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();

            var Reportes = GetChekedItems(chklConsolidadoReportes);
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                CrearReportesCrystal(_serviceId, _pacientId, Reportes, Result == System.Windows.Forms.DialogResult.Yes ? true : false);
            };

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                var x = _filesNameToMerge.ToList();
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf";
                _mergeExPDF.DestinationFile = ruta + _serviceId + ".pdf"; ;
                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();

                var oService = _serviceBL.GetServiceShort(_serviceId);
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                _mergeExPDF.DestinationFile = rutaConsolidado + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";
                _mergeExPDF.Execute();


                //Cambiar de estado a generado de reportes
                _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult, 2, _serviceId, Globals.ClientSession.GetAsList());
            }
            else
            {
                var x = _filesNameToMerge.ToList();
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
                _mergeExPDF.DestinationFile = rutaBasura + _serviceId + ".pdf"; ;
                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();
            }

        }

        private void CrearReportesCrystal(string serviceId, string pPacienteId, List<string> reportesId, bool Publicar)
        {
            OperationResult objOperationResult = new OperationResult();
            MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
            crConsolidatedReports rp = null;
            ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
            rp = new Reports.crConsolidatedReports();
            _filesNameToMerge = new List<string>();

            foreach (var com in reportesId)
            {
                //string CompnenteId = "";
                int IdCrystal = 0;
                //Obtener el Id del componente 

                var array = com.Split('|');

                if (array.Count() == 1)
                {
                    IdCrystal = 0;
                }
                else if (array[1] == "")
                {
                    IdCrystal = 0;
                }
                else
                {
                    IdCrystal = int.Parse(array[1].ToString());
                }

                ChooseReport(array[0], serviceId, pPacienteId, IdCrystal);

            }
        }

        private void ChooseReport(string componentId, string serviceId, string pPacienteId, int pintIdCrystal)
        {
            ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();           

            switch (componentId)
            {
                case Constants.FORMATO_ATENCION:
                    GenerateConsultaMedica(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;
            }   
        }

        private void GenerateConsultaMedica(string pathFile)
        {
            HistoryBL _historyBL = new HistoryBL();
             var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
             var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
             var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
             var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
             var noxiousHabit = _historyBL.GetNoxiousHabitsReport(_pacientId);
             var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport_(_pacientId);
             var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
             ConsultaMedica.CreateConsultaMedica(filiationData, MedicalCenter, personMedicalHistory, familyMedicalAntecedent, noxiousHabit, serviceComponents, diagnosticRepository, pathFile);
        }

    }
}