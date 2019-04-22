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
using System.Diagnostics;
  
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.draw;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmConsultaFacturacion : Form
    {
        List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
        List<string> _componentIds;
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId;
        private string _pacientId;
        private string _protocolId;
        private string _customerOrganizationName;
        private string _personFullName;
        List<string> _ListaServicios;
        List<string> _ListaServiciosAdjuntar;
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip _customizedToolTip = null;
        private SaveFileDialog saveFileDialog2 = new SaveFileDialog();
        private List<ServiceGridJerarquizadaList> ListaGrilla = new List<ServiceGridJerarquizadaList>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public frmConsultaFacturacion()
        {
            InitializeComponent();
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            #region Simular sesion
            //ClientSession objClientSession = new ClientSession();
            //objClientSession.i_SystemUserId = 1;
            //objClientSession.v_UserName = "sa";
            //objClientSession.i_CurrentExecutionNodeId = 9;
            //objClientSession.v_CurrentExecutionNodeName = "SALUS";
            ////_ClientSession.i_CurrentOrganizationId = 57;
            //objClientSession.v_PersonId = "N000-P0000000001";

            //// Pasar el objeto de sesión al gestor de objetos babales
            //Globals.ClientSession = objClientSession;
            #endregion     

            _customizedToolTip = new Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip(grdDataService);

            UltraGridColumn c = grdDataService.DisplayLayout.Bands[0].Columns["b_FechaEntrega"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            ddlConsultorio.SelectedValueChanged -= ddlConsultorio_SelectedValueChanged;


            dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(-1);

            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.All);

            Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);
                       
            Utils.LoadDropDownList(ddServiceStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 125, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlStatusAptitudId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 124, null), DropDownListAction.All);

         

            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);
            
            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

             groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
             // Remover los componentes que no estan asignados al rol del usuario
             var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));
         

          
            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", results, DropDownListAction.Select);

            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;

            ddlConsultorio.SelectedValueChanged += ddlConsultorio_SelectedValueChanged;

          
     
        }

        private void ddlMasterServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (ddlMasterServiceId.SelectedValue !=null )
            {
                var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);

                if (ddlMasterServiceId.SelectedValue.ToString() == ((int)Common.MasterService.Eso).ToString())
                {
                    ddlEsoType.Enabled = true;
                    ddlProtocolId.Enabled = true;
                    Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
          
                    //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

                }
                else
                {
                    Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
          
                    //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.All);
                    ddlEsoType.Enabled = false;
                    ddlProtocolId.Enabled = false;
                }
            }

        }

        private void ddlEsoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            //string idOrg = String.Empty;
            //string idLoc = String.Empty;
            //if (ddlEsoType.SelectedValue !=null)
            //{
            //    if (ddlEsoType.SelectedValue.ToString() != "-1")
            //    {
            //        if (ddlCustomerOrganization.SelectedValue.ToString() == "-1") return;
            //        var dataList = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            //        idOrg = dataList[1];
            //        idLoc = dataList[2];
            //    }
            //    Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolByLocation(ref objOperationResult, idLoc, int.Parse(ddlEsoType.SelectedValue.ToString())), DropDownListAction.All);
       
            //}
          
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_PacientDocument.Contains(\"" + txtPacient.Text.Trim() + "\")");
            if (ddServiceStatusId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceStatusId==" + ddServiceStatusId.SelectedValue);

            var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }

            //if (ddlCustomerOrganization.SelectedValue.ToString() != "-1") Filters.Add("v_LocationId==" + "\"" + id1[2] + "\"");

            if (ddlMasterServiceId.SelectedValue.ToString() != "-1") Filters.Add("i_MasterServiceId==" + ddlMasterServiceId.SelectedValue);
            if (ddlServiceTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceTypeId==" + ddlServiceTypeId.SelectedValue);
            if (ddlEsoType.SelectedValue.ToString() != "-1") Filters.Add("i_EsoTypeId==" + ddlEsoType.SelectedValue);
            if (ddlProtocolId.SelectedValue.ToString() != "-1") Filters.Add("v_ProtocolId=="+ "\"" + ddlProtocolId.SelectedValue + "\"");
            if (ddlStatusAptitudId.SelectedValue.ToString() != "-1") Filters.Add("i_AptitudeStatusId==" + ddlStatusAptitudId.SelectedValue);
            
            // Create the Filter Expression
            strFilterExpression = null;   
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            };
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "", strFilterExpression);
            ListaGrilla = objData;
            grdDataService.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdDataService.Rows.Count > 0)
            {
                grdDataService.Rows[0].Selected = true;
                btnExport.Enabled = true;
            }

        }

        private List<ServiceGridJerarquizadaList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            DateTime? FCI = FechaControlIni.Value.Date;
            DateTime? FCF = FechaControlFin.Value.Date.AddDays(1);

            if (!chkFC.Checked)
            {
                FCI = DateTime.Parse("01/01/2000");
                FCF = DateTime.Parse("01/01/2050");
            }

            //var _objData = _serviceBL.GetServicesPagedAndFiltered_(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, _componentIds, FCI, FCF);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private void ddlOrganizationLocationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }

            ddlProtocolId.Enabled = true;

            OperationResult objOperationResult = new OperationResult();
                
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);          
            
        }

        private void ddlServiceTypeId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedValue.ToString() == "-1")
            {
                ddlMasterServiceId.SelectedValue = "-1";
                ddlMasterServiceId.Enabled = false;
                return;
            }

            ddlMasterServiceId.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, int.Parse(ddlServiceTypeId.SelectedValue.ToString()), Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
          
        }

        private void ddlMasterServiceId_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddlMasterServiceId.SelectedValue == null)
                return;

            if (ddlMasterServiceId.SelectedValue.ToString() == "-1")
            {
                ddlEsoType.SelectedValue = "-1";
                ddlEsoType.Enabled = false;
                return;
            }

            OperationResult objOperationResult = new OperationResult();
          
            //var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);

            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)Common.MasterService.Eso).ToString() ||
                ddlMasterServiceId.SelectedValue.ToString() == "12")
            {

                ddlEsoType.Enabled = true;

                //ddlProtocolId.Enabled = true;
                ddlStatusAptitudId.Enabled = true;
                //Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

                //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

            }
            else
            {

                ddlEsoType.SelectedValue = "-1";
                ddlEsoType.Enabled = false;

                //Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

                ////Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.All);
                
                //ddlProtocolId.SelectedValue = "-1";
                //ddlProtocolId.Enabled = false;
                ddlStatusAptitudId.Enabled = false;
                ddlStatusAptitudId.SelectedValue = "-1";
            }
            
        }

        private void CertificadoAptitud_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmOccupationalMedicalAptitudeCertificate(_serviceId);
            frm.ShowDialog();
        }

        private void btnEditarESO_Click(object sender, EventArgs e)
        {
            Form frm;
           int TserviceId = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceId"].Value.ToString());
           if (TserviceId == (int)MasterService.AtxMedicaParticular || TserviceId == (int)MasterService.AtxMedicaSeguros)
           {
               frm = new Operations.frmMedicalConsult(_serviceId, null, null);
               frm.ShowDialog();
           }
           else
           {
               //Obtener Estado del servicio
               var EstadoServicio = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());

               if (EstadoServicio == (int)ServiceStatus.Culminado)
               {
                   //Obtener el usuario
                   int UserId= Globals.ClientSession.i_SystemUserId ;
                   if (UserId==11)
	                {
                        this.Enabled = false;
                        frm = new Operations.frmEso(_serviceId, null, "Service", (int)MasterService.Eso);
                        frm.ShowDialog();
                        this.Enabled = true;
	                }
                   else
                   {
                        this.Enabled = false;
                        frm = new Operations.frmEso(_serviceId, null, "View", (int)MasterService.Eso);
                        frm.ShowDialog();
                        this.Enabled = true;                   
                   }
                  
               }
               else 
               {
                   this.Enabled = false;
                   frm = new Operations.frmEso(_serviceId, null, "Service", (int)MasterService.Eso);
                   frm.ShowDialog();
                   this.Enabled = true;
               }

             
           }

           btnFilter_Click(sender, e);
                  
           
        }

        private void grdDataService_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdDataService.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "1")
                {
                    btnEditarESO.Enabled = false;
                    btnAdminReportes.Enabled = false;
                    btnGenerarLiquidacion.Enabled = false;
                    btnInterconsulta.Enabled = false;
                    btnTiempos.Enabled = false;
                    btnFechaEntrega.Enabled = false;
                 
                    return;                
                } 
            }
            if (_ListaServicios != null)
            {
                btnFechaEntrega.Enabled = true;
            }
            else
            {
                btnFechaEntrega.Enabled = false;
            }
            btn7D.Enabled = 
            btnOdontograma.Enabled =
            btnHistoriaOcupacional.Enabled = 
            btnRadiologico.Enabled =
            btnOsteomuscular.Enabled = 
            btnPruebaEsfuerzo.Enabled = 
            btnInformeRadiologicoOIT.Enabled = 
            btnEstudioEKG.Enabled =
            btnDermatologico.Enabled = 
            btnEditarESO.Enabled = 
            btnImprimirCertificadoAptitud.Enabled = 
            btnInformeMedicoTrabajador.Enabled =
            btnImprimirInformeMedicoEPS.Enabled = 
            btnAdminReportes.Enabled = 
            btnInforme312.Enabled = 
            btnInformeMusculoEsqueletico.Enabled = 
            btnInformeAlturaEstructural.Enabled = 
            btnInformePsicologico.Enabled = 
            btnInformeOftalmo.Enabled = 
            btnGenerarLiquidacion.Enabled =
            btnInterconsulta.Enabled=
            btnTiempos.Enabled=

               
            //btnFechaEntrega.Enabled =
            //btnImprimirFichaOcupacional.Enabled = 
            (grdDataService.Selected.Rows.Count > 0);
       
            if (grdDataService.Selected.Rows.Count == 0)
                return;

            
            _serviceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            _pacientId = grdDataService.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            _protocolId = grdDataService.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            _customerOrganizationName = grdDataService.Selected.Rows[0].Cells["v_OrganizationName"].Value.ToString();
            _personFullName = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();

            if (grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value== null)
            {
                btnImprimirExamenes.Enabled = false;
            }
            else
            {
                btnImprimirExamenes.Enabled = true;
            }
        }

        private void Examenes_Click(object sender, EventArgs e)
        {
           List<string>  _filesNameToMerge = new List<string>();
            string ServiceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
              var  ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();      
            //ReportCustom.FichaOcupacional frm = new ReportCustom.FichaOcupacional(_serviceId, _pacientId, _protocolId, (int)TypePrinter.Printer);


              string[] files = Directory.GetFiles(ruta, "*.pdf");
           
              int strIndex = 0;
            string findThisString = ruta + ServiceId;
            int contador= 0;
              for (int i = 0; i < files.Length; i++)
              {

                  strIndex = files[i].IndexOf(findThisString);

                  if (strIndex >= 0)
                  {
                      _filesNameToMerge.Add(files[i].ToString());
               
                  }
              }

              var x = _filesNameToMerge.ToList();
              _mergeExPDF.FilesName = x;
              _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
              _mergeExPDF.DestinationFile = ruta + "xxx" + ".pdf"; 
              _mergeExPDF.Execute();
              _mergeExPDF.RunFile();
              //SendToPrinter();
              //System.Diagnostics.Process oProc = new System.Diagnostics.Process();
              //oProc.StartInfo.FileName = "C:\\Program Files (x86)\\Adobe\\Reader 11.0\\Reader\\AcroRd32.exe";
              ////dobleslash, conitene la cadena completa de la ruta 
              //oProc.StartInfo.Arguments = "/n/t \"" + ruta + "xxx" + ".pdf\" \"" + "HP Deskjet 3540 series" + "\" ";
              //oProc.Start();
              //oProc.Close();


              //MessageBox.Show(contador.ToString());

        }

        private void SendToPrinter()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = @"D:\Reportes Medicos\xxx.pdf";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }

        private void btnImprimirCertificadoAptitud_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmOccupationalMedicalAptitudeCertificate(_serviceId);
            frm.ShowDialog();
        }

        private void btnImprimirFichaOcupacional_Click(object sender, EventArgs e)
        {

        }

        private void vistaPreviaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _serviceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            _pacientId = grdDataService.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            _protocolId = grdDataService.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            ReportCustom.FichaOcupacional frm = new ReportCustom.FichaOcupacional(_serviceId, _pacientId, _protocolId, (int)TypePrinter.Image);
        }

        private void dtpDateTimeStar_Validating(object sender, CancelEventArgs e)
        {
            if (dtpDateTimeStar.Value.Date > dptDateTimeEnd.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Inicial no puede ser Mayor a la fecha final.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dptDateTimeEnd_Validating(object sender, CancelEventArgs e)
        {
            if (dptDateTimeEnd.Value.Date < dtpDateTimeStar.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Final no puede ser Menor a la fecha Inicial.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtPacient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void btnImprimirInformeMedicoEPS_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Informe Médico EPS";
            saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";

            try
            {
                
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                   
                    using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                    {
                        this.Enabled = false;                    

                        var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

                        var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(_pacientId);

                        var noxiousHabit = _historyBL.GetNoxiousHabitsReport(_pacientId);

                        var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);

                        var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId);

                        var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);

                        var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

                        ReportPDF.CreateMedicalReportEPS(filiationData,
                                                        personMedicalHistory,
                                                        noxiousHabit,
                                                        familyMedicalAntecedent,
                                                        anamnesis,
                                                        serviceComponents,
                                                        diagnosticRepository,
                                                        saveFileDialog1.FileName);

                        this.Enabled = true;
                    }

                  

                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }         
   
        }

        private void btnInformeMedicoTrabajador_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Format("{0} Informe Resumen", _personFullName);
            saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";

            //try
            //{

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {               
                    using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                    {
                        this.Enabled = false;
                      
                        var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);

                        var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(_pacientId);

                        var noxiousHabit = _historyBL.GetNoxiousHabitsReport(_pacientId);

                        var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);

                        var anamnesis = _serviceBL.GetAnamnesisReport(_serviceId);

                        var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);

                        var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);

                        var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                        ReportPDF.CreateMedicalReportForTheWorker(filiationData,
                                                        personMedicalHistory,
                                                        noxiousHabit,
                                                        familyMedicalAntecedent,
                                                        anamnesis,
                                                        serviceComponents,
                                                        diagnosticRepository,
                                                        _customerOrganizationName,
                                                        MedicalCenter,                                                      
                                                        saveFileDialog1.FileName);

                        this.Enabled = true;
                    }
                                  
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Enabled = true;
            //}              
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var inside = _serviceBL.IsPsicoExamIntoServiceComponent(_serviceId);

            if (!inside)
            {
                MessageBox.Show("El examen de Psicologia no aplica a la atención", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new Reports.frmInformePsicologicoOcupacional(_serviceId);
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog2.FileName = string.Format("{0} 312", _personFullName);         
            saveFileDialog2.Filter = "Files (*.pdf;)|*.pdf;";

            //try
            //{
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                    {
                        this.Enabled = false;

                        var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
                        var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(_pacientId);
                        var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacientId);
                        var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacientId);
                        var _DataService = _serviceBL.GetServiceReport(_serviceId);
                        var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacientId);

                        var Antropometria = _serviceBL.ValoresComponente(_serviceId, Constants.ANTROPOMETRIA_ID);
                        var FuncionesVitales = _serviceBL.ValoresComponente(_serviceId, Constants.FUNCIONES_VITALES_ID);
                        var ExamenFisico = _serviceBL.ValoresComponente(_serviceId, Constants.EXAMEN_FISICO_ID);
                        var Oftalmologia = _serviceBL.ValoresComponente(_serviceId, Constants.OFTALMOLOGIA_ID);

                       // var TestIhihara = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ISHIHARA_ID);
                        //var TestEstereopsis = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ESTEREOPSIS_ID);

                        var Psicologia = _serviceBL.ValoresExamenComponete(_serviceId, Constants.PSICOLOGIA_ID, 195);
                        var RX = _serviceBL.ValoresExamenComponete(_serviceId, Constants.RX_TORAX_ID, 211);
                        var RX1 = _serviceBL.ValoresExamenComponete(_serviceId, Constants.RX_TORAX_ID, 135);
                        var Laboratorio = _serviceBL.ValoresComponente(_serviceId, Constants.INFORME_LABORATORIO_ID);
                        //var Audiometria = _serviceBL.ValoresComponente(_serviceId, Constants.AUDIOMETRIA_ID);
                        var Audiometria = _serviceBL.GetDiagnosticForAudiometria(_serviceId, Constants.AUDIOMETRIA_ID);
                        var Espirometria = _serviceBL.ValoresExamenComponete(_serviceId, Constants.ESPIROMETRIA_ID, 210);
                        var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(_serviceId);
                        var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(_serviceId);
                        var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);

                        var ElectroCardiograma = _serviceBL.ValoresComponente(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
                        var PruebaEsfuerzo = _serviceBL.ValoresComponente(_serviceId, Constants.PRUEBA_ESFUERZO_ID);
                        var Altura7D = _serviceBL.ValoresComponente(_serviceId, Constants.ALTURA_7D_ID);
                        var AlturaEstructural = _serviceBL.ValoresComponente(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
                        var Odontologia = _serviceBL.ValoresComponente(_serviceId, Constants.ODONTOGRAMA_ID);
                        var OsteoMuscular = _serviceBL.ValoresComponente(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);

                        var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
                        var ValoresDxLab = _serviceBL.ValoresComponenteAMC_(_serviceId, 1);
                        var serviceComponents = _serviceBL.GetServiceComponentsReport_New(_serviceId);

                        FichaMedicaOcupacional312.CreateFichaMedicalOcupacional312Report(_DataService,
                                  filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                                  _listMedicoPersonales, _listaHabitoNocivos, 
                                  Audiometria, // Psicologia, RX, RX1,  , Espirometria,
                                  _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter,//TestIhihara,TestEstereopsis,
                                  serviceComponents, saveFileDialog2.FileName);

                        this.Enabled = true;
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //}

        }
     
        private void button3_Click(object sender, EventArgs e)
        {
           
            var frm = new Reports.frmMusculoesqueletico(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            frm.ShowDialog();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmOftalmologia(_serviceId, Constants.OFTALMOLOGIA_ID);
            frm.ShowDialog();
        }

        private void btn7D_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmAnexo7D(_serviceId, Constants.ALTURA_7D_ID);
            frm.ShowDialog();
        }

        private void btnOdontograma_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmOdontograma(_serviceId, Constants.ODONTOGRAMA_ID);
            //frm.ShowDialog();
        }

        private void btnHistoriaOcupacional_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmHistoriaOcupacional(_serviceId);
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmRadiologico(_serviceId, Constants.RX_TORAX_ID);
            //frm.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void btnRadiologico_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmRadiologico(_serviceId, Constants.RX_TORAX_ID);
            //frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmOsteomuscular(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmPruebaEsfuerzo(_serviceId, Constants.PRUEBA_ESFUERZO_ID);
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //var frm = new Reports.frmInformeRadiograficoOIT(_serviceId, Constants.RX_TORAX_ID);
            //frm.ShowDialog();
            
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            //var frm = new Reports.frmEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            //frm.ShowDialog();
        }

        private void btnConsolidadoReportes_Click(object sender, EventArgs e)
        {
            int flagPantalla = int.Parse(ddlServiceTypeId.SelectedValue.ToString());


            var frm = new Reports.frmManagementReports(_serviceId, _pacientId, _customerOrganizationName, _personFullName, flagPantalla, "", 1);
                frm.ShowDialog();         

        }

        private void btnDermatologico_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmTamizajeDermatologico(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
            frm.ShowDialog();
        }

        private void btnGenerarLiquidacion_Click(object sender, EventArgs e)
        {
            var service = new ServiceList();

            service.v_ServiceId = _serviceId;
            service.v_ProtocolName = grdDataService.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
            service.v_CustomerOrganizationName = _customerOrganizationName;
            service.v_Pacient = _personFullName;
            service.v_MasterServiceName = grdDataService.Selected.Rows[0].Cells["v_MasterServiceName"].Value.ToString();
            service.v_EsoTypeName = grdDataService.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
            service.i_StatusLiquidation = (int?)grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value;

            var frm = new frmBeforeLiquidationProcess(service);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;
            
            BindGrid();

        }

        private void grdDataService_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
          
        }

        private void grdDataService_InitializeRow(object sender, InitializeRowEventArgs e)
        {

            foreach (UltraGridRow rowSelected in this.grdDataService.Rows)
            {
                var banda = e.Row.Band.Index.ToString();

                if (banda == "0")
                {
                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                        if (e.Row.Cells["i_ServiceStatusId"].Value.ToString() == ((int)ServiceStatus.EsperandoAptitud).ToString())
                        {
                            e.Row.Appearance.BackColor = Color.Yellow;
                            e.Row.Appearance.BackColor2 = Color.White;
                            //Y doy el efecto degradado vertical
                            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                        }
                        else if (e.Row.Cells["i_ServiceStatusId"].Value.ToString() == ((int)ServiceStatus.Culminado).ToString())
                        {
                            e.Row.Appearance.BackColor = Color.SkyBlue;
                            e.Row.Appearance.BackColor2 = Color.White;
                            //Y doy el efecto degradado vertical
                            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                        }
                    }

                    if (e.Row.Cells["i_StatusLiquidation"].Value == null)
                        return;

                    if ((int)e.Row.Cells["i_StatusLiquidation"].Value == (int)PreLiquidationStatus.Generada)
                    {
                        e.Row.Cells["Liq"].Value = Resources.accept;
                        e.Row.Cells["Liq"].ToolTipText = "Generada";
                    }
                }
  
            }


         

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ddlConsultorio_TabIndexChanged(object sender, EventArgs e)
        {
          
           
        }

        private void ddlConsultorio_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlConsultorio.SelectedIndex == 0)
            {
                _componentIds = null;
                return;
            }

            _componentIds = new List<string>();
            var eee = (KeyValueDTO)ddlConsultorio.SelectedItem;

            if (eee.Value4.ToString() == "-1")
            {
                _componentIds.Add(eee.Value2);
            }
            else
            {
                _componentIds = _componentListTemp.FindAll(p => p.Value4 == eee.Value4)

                                                .Select(s => s.Value2)
                                                .OrderBy(p => p).ToList();
            }
        }

        private void btnInterconsulta_Click(object sender, EventArgs e)
        {
            frmInterconsulta frm = new frmInterconsulta(_serviceId);
            frm.ShowDialog();
        }

        private void btnTiempos_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var frm = new Reports.TiemposTrabajadores(strFilterExpression, pdatBeginDate, pdatEndDate, _componentIds);
            frm.ShowDialog();
        }

        private void chkFC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFC.Checked)
            {
                FechaControlIni.Enabled = true;
                FechaControlFin.Enabled = true;
            }
            else
            {
                FechaControlIni.Enabled = false;
                FechaControlFin.Enabled = false;
            }

        }

        private void btnFechaEntrega_Click(object sender, EventArgs e)
        {




            _ListaServicios = new List<string>();
            foreach (var item in grdDataService.Rows)
            {
                //CheckBox ck = (CheckBox)item.Cells["b_FechaEntrega"].Value;

                if ((bool)item.Cells["b_FechaEntrega"].Value)
                {
                    string x = item.Cells["v_ServiceId"].Value.ToString();
                    _ListaServicios.Add(x);
                }
            }

            if (_ListaServicios.Count==0)
            {
                 MessageBox.Show("No hay ningún servicio con check, por favor seleccionar uno.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
            }
            frmPopupFechaEntrega frm = new frmPopupFechaEntrega(_ListaServicios,"service");
            frm.ShowDialog();

            btnFilter_Click(sender, e);
        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_FechaEntrega"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;

                    //btnFechaEntrega.Enabled = true;
                    //btnAdjuntarArchivo.Enabled = true;
                }
                else
                {
                    e.Cell.Value = false;
                    //btnFechaEntrega.Enabled = false;
                    //btnAdjuntarArchivo.Enabled = false;
                }

            }
        }

        private void btnAdjuntarArchivo_Click(object sender, EventArgs e)
        {
            _ListaServiciosAdjuntar = new List<string>();
            foreach (var item in grdDataService.Rows)
            {
                //CheckBox ck = (CheckBox)item.Cells["b_FechaEntrega"].Value;

                if ((bool)item.Cells["b_FechaEntrega"].Value)
                {
                    string x = item.Cells["v_ServiceId"].Value.ToString();
                    _ListaServiciosAdjuntar.Add(x);
                }
            }

            if (_ListaServiciosAdjuntar.Count == 0)
            {
                MessageBox.Show("No hay ningún servicio con check, por favor seleccionar uno.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmAdjuntarArchivos frm = new frmAdjuntarArchivos(_ListaServiciosAdjuntar);
            frm.ShowDialog();


        

        }

        private void btnHistoriaClinica_Click(object sender, EventArgs e)
        {

           string ruta = Application.StartupPath + "\\pdf\\pdf01.pdf";
           System.Diagnostics.Process.Start(ruta);
        }

        private void btnHis_Click(object sender, EventArgs e)
        {
            string ruta = Application.StartupPath + "\\pdf\\pdf02.pdf";
            System.Diagnostics.Process.Start(ruta);
        }

        private void btnFichaControl_Click(object sender, EventArgs e)
        {
            string ruta = Application.StartupPath + "\\pdf\\pdf03.pdf";
            System.Diagnostics.Process.Start(ruta);
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {

            frmSeleccionarCategoriaImportar frm = new frmSeleccionarCategoriaImportar();
            frm.ShowDialog();

            return;
            OperationResult objOperationResult = new OperationResult();
            ServiceComponentFieldsList serviceComponentFields = null;
            ServiceComponentFieldValuesList serviceComponentFieldValues = null;
            List<ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            List<ServiceComponentFieldsList> _serviceComponentFieldsList = null;


            MedicalExamFieldValuesBL oMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();

            string ServiceComponentId = "";
            string Personid = "";
            //string ServiceComponentId = "N009-SC000044981";
            //string ComponentId = "N002-ME000000028";


            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();




            //Obtener Datos del Excel

             openFileDialog1.FileName = string.Empty;
             openFileDialog1.Filter = "Image Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var Ext = Path.GetExtension(openFileDialog1.FileName).ToUpper();

                if (Ext == ".XLSX" || Ext == ".XLS" || Ext == ".XLSM")
                {
                    Infragistics.Documents.Excel.Workbook workbook1 = Infragistics.Documents.Excel.Workbook.Load(openFileDialog1.FileName);

                    Infragistics.Documents.Excel.Worksheet worksheet1 = workbook1.Worksheets["EXAMEN"];

                    int fila = 3;
                    int colInicio = 0;
                    int colFinal = 0;
                    int filaAntecedentes = 3;
                    int tope = 0;
                    //Obtener el nombre del Examen
                    var Examen = worksheet1.Rows[fila].Cells[2].Value.ToString();
                    servicecomponentDto serviceComponentDto = new servicecomponentDto();
                    List<DiagnosticRepositoryList> ListaDxByComponent = new List<DiagnosticRepositoryList>();
                        while (worksheet1.Rows[fila].Cells[0].Value != null)
                        {
                            tope = fila;
                            //Cargar Entidad Service Component
                            serviceComponentDto = new servicecomponentDto();
                            serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                            serviceComponentDto.v_Comment = "";
                            serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                            serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                            serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                            string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                            serviceComponentDto.v_ComponentId = ComponentId;
                            string ServiceId=  worksheet1.Rows[fila].Cells[0].Value.ToString();
                            serviceComponentDto.v_ServiceId =ServiceId;

                            ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                            Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();
                                                     

                            if (Examen == Constants.OFTALMOLOGIA_ID)
                            {
                                colInicio = 49;
                                colFinal = 82;
                            }
                            else if (Examen == Constants.ANTROPOMETRIA_ID || Examen == Constants.FUNCIONES_VITALES_ID )
                            {
                                colInicio = 58;
                                colFinal = 70;
                            }

                            for (int c = colInicio; c < colFinal; c++)
                            {
                                serviceComponentFields = new ServiceComponentFieldsList();

                                serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                // Agregar a mi lista
                                _serviceComponentFieldsList.Add(serviceComponentFields);

                            }
                            //lIMPIAR LA LISTA DE DXS
                            ListaDxByComponent = new List<DiagnosticRepositoryList>();
                            if (worksheet1.Rows[fila].Cells[73].Value.ToString() != "0")
                            {
                                DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();
                              
                                List<RecomendationList> Recomendations = new List<RecomendationList>();
                                List<RestrictionList> Restrictions = new List<RestrictionList>();
                               
                               
                                DxByComponent.i_AutoManualId = 1;
                                DxByComponent.i_FinalQualificationId = 1;
                                DxByComponent.i_PreQualificationId = 1;
                                DxByComponent.v_ComponentFieldsId = worksheet1.Rows[0].Cells[73].Value.ToString();
                                //Obtener el Componente que está amarrado al DX
                                string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(worksheet1.Rows[0].Cells[73].Value.ToString());
                                string DiseasesId = worksheet1.Rows[fila].Cells[73].Value.ToString();
                                string ComponentFieldId = worksheet1.Rows[0].Cells[73].Value.ToString();

                                DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(ServiceId, DiseasesId, ComponentDx, ComponentFieldId);
                                if (oDiagnosticRepositoryListOld != null)
                                {
                                    oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                                    oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                                    oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                                    oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Ocupacional;
                                    ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                                }
                              
                                    DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                    DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                    DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                    DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Ocupacional;
                                

                                                            

                                DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;
                                
                                string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                                DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;
                                

                                DxByComponent.v_ComponentId = ComponentDx;
                                DxByComponent.v_DiseasesId = DiseasesId;
                                DxByComponent.v_ServiceId = ServiceId;

                                
                                //Obtener las recomendaciones

                                DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId, ServiceId, ComponentId);

                                ListaDxByComponent.Add(DxByComponent);

                            }

                            fila +=10;


                            var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                 Globals.ClientSession.GetAsList(),
                                                                 Personid,
                                                                 ServiceComponentId);



                            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                        ListaDxByComponent,
                                                        serviceComponentDto,
                                                        Globals.ClientSession.GetAsList(),
                                                        false,false);





                            #region Antedecentes Ocupacionales

                            //Eliminar todo lo que es Historia del Paciente
                            HistoryBL oHistoryBL = new HistoryBL();
                            oHistoryBL.EliminarHystoriaPaciente(Personid);
                       

                            for (int i = filaAntecedentes; i < tope+10; i++)
                            {

                                #region Ocupacionales
                                
                            
                                if (worksheet1.Rows[filaAntecedentes].Cells[20].Value != null)
                                {
                               
                                //HISTORy
                                historyDto ohistoryDto = new historyDto();

                                WorkstationDangersList objWorkstationDangers;
                                List<WorkstationDangersList> _TempWorkstationDangersList = new List<WorkstationDangersList>();

                                TypeOfEEPList objTypeOfEEP;
                                List<TypeOfEEPList> _TempTypeOfEEPList = new List<TypeOfEEPList>();


        
                                ohistoryDto.v_PersonId = Personid;
                                ohistoryDto.d_StartDate = DateTime.Parse(worksheet1.Rows[filaAntecedentes].Cells[20].Value.ToString());
                                ohistoryDto.d_EndDate = DateTime.Parse(worksheet1.Rows[filaAntecedentes].Cells[21].Value.ToString());
                                ohistoryDto.v_Organization = worksheet1.Rows[filaAntecedentes].Cells[22].Value.ToString();
                                ohistoryDto.v_TypeActivity = worksheet1.Rows[filaAntecedentes].Cells[23].Value.ToString();
                                ohistoryDto.v_workstation = worksheet1.Rows[filaAntecedentes].Cells[24].Value.ToString();
                                ohistoryDto.i_TrabajoActual = worksheet1.Rows[filaAntecedentes].Cells[25].Value.ToString() == "SI" ? 1 : 0;
                                ohistoryDto.i_GeografixcaHeight = worksheet1.Rows[filaAntecedentes].Cells[26].Value.ToString() == "- - -" ? 0 : int.Parse(worksheet1.Rows[filaAntecedentes].Cells[26].Value.ToString());
                                int TipoOperacionId =int.Parse(worksheet1.Rows[filaAntecedentes].Cells[27].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[27].Value.ToString().Length - 1, 1));
                                ohistoryDto.i_TypeOperationId = TipoOperacionId;

                                

                              
                                //Peligro en el Puesto 1
                                if (worksheet1.Rows[filaAntecedentes].Cells[28].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId =int.Parse(worksheet1.Rows[filaAntecedentes].Cells[28].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[28].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }

                                //Peligro en el Puesto 2
                                if (worksheet1.Rows[filaAntecedentes].Cells[29].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[29].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[29].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }

                                //Peligro en el Puesto 3
                                if (worksheet1.Rows[filaAntecedentes].Cells[30].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[30].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[30].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }

                                //Peligro en el Puesto 4
                                if (worksheet1.Rows[filaAntecedentes].Cells[31].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[31].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[31].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }

                                //Peligro en el Puesto 5
                                if (worksheet1.Rows[filaAntecedentes].Cells[32].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[32].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[32].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }

                                //Peligro en el Puesto 6
                                if (worksheet1.Rows[filaAntecedentes].Cells[33].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[33].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[33].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }

                                //Peligro en el Puesto 7
                                if (worksheet1.Rows[filaAntecedentes].Cells[34].Value != null)
                                {
                                    objWorkstationDangers = new WorkstationDangersList();
                                    int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[34].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[34].Value.ToString().Length - 2, 2));
                                    objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                                }


                                //EPP 1
                                if (worksheet1.Rows[filaAntecedentes].Cells[35].Value != null)
                                {
                                    objTypeOfEEP = new TypeOfEEPList();
                                    int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[35].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[35].Value.ToString().Length - 2, 2));
                                    objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                    objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                    _TempTypeOfEEPList.Add(objTypeOfEEP);
                                }

                                //EPP 2
                                if (worksheet1.Rows[filaAntecedentes].Cells[36].Value != null)
                                {
                                    objTypeOfEEP = new TypeOfEEPList();
                                    int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[36].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[36].Value.ToString().Length - 2, 2));
                                    objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                    objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                    _TempTypeOfEEPList.Add(objTypeOfEEP);
                                }
                                //EPP 3
                                if (worksheet1.Rows[filaAntecedentes].Cells[37].Value != null)
                                {
                                    objTypeOfEEP = new TypeOfEEPList();
                                    int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[37].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[37].Value.ToString().Length - 2, 2));
                                    objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                    objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                    _TempTypeOfEEPList.Add(objTypeOfEEP);
                                }

                                //EPP 4
                                if (worksheet1.Rows[filaAntecedentes].Cells[38].Value != null)
                                {
                                    objTypeOfEEP = new TypeOfEEPList();
                                    int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[38].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[38].Value.ToString().Length - 2, 2));
                                    objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                    objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                    _TempTypeOfEEPList.Add(objTypeOfEEP);
                                }

                                //EPP 5
                                if (worksheet1.Rows[filaAntecedentes].Cells[39].Value != null)
                                {
                                    objTypeOfEEP = new TypeOfEEPList();
                                    int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[39].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[39].Value.ToString().Length - 2, 2));
                                    objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                    objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                    objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                    _TempTypeOfEEPList.Add(objTypeOfEEP);
                                }

                                oHistoryBL.AddHistory(ref objOperationResult, _TempWorkstationDangersList, _TempTypeOfEEPList, ohistoryDto, Globals.ClientSession.GetAsList());

                                }
                                #endregion

                                #region Personales
                                List<personmedicalhistoryDto> _personmedicalhistoryDto = new List<personmedicalhistoryDto>();
                                personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();


                                if (worksheet1.Rows[filaAntecedentes].Cells[41].Value != null)
                                {
                                    int TipoDxId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[43].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[43].Value.ToString().Length - 1, 1));
                                    personmedicalhistoryDtoDto.i_TypeDiagnosticId = TipoDxId;
                                    personmedicalhistoryDtoDto.v_PersonId = Personid;
                                    string PersonalDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[41].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[41].Value.ToString().Length - 16, 16);
                                    personmedicalhistoryDtoDto.v_DiseasesId = PersonalDiseasesId;
                                    personmedicalhistoryDtoDto.d_StartDate = DateTime.Parse(worksheet1.Rows[filaAntecedentes].Cells[42].Value.ToString());
                                    personmedicalhistoryDtoDto.v_DiagnosticDetail = worksheet1.Rows[filaAntecedentes].Cells[44].Value.ToString();
                                    personmedicalhistoryDtoDto.v_TreatmentSite = worksheet1.Rows[filaAntecedentes].Cells[45].Value.ToString();
                                    personmedicalhistoryDtoDto.i_AnswerId = (int)SiNo.SI;

                                    _personmedicalhistoryDto.Add(personmedicalhistoryDtoDto);

                                    oHistoryBL.AddPersonMedicalHistory(ref objOperationResult,
                                              _personmedicalhistoryDto,
                                              null,
                                              null,
                                              Globals.ClientSession.GetAsList());
                                 
                                }
                                #endregion

                                #region Hábitos Noxivos
                                List<noxioushabitsDto> _noxioushabitsDto = new List<noxioushabitsDto>();
                                noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();


                                if (worksheet1.Rows[filaAntecedentes].Cells[46].Value != null)
                                {
                                    int TipoHabitoId =-1;
                                    if (worksheet1.Rows[filaAntecedentes].Cells[46].Value.ToString().Trim() =="TABAQUISMO")
                                    {
                                        TipoHabitoId = 1;
                                    }
                                    else if (worksheet1.Rows[filaAntecedentes].Cells[46].Value.ToString().Trim() == "CONSUMO DE ALCOHOL")
                                    {
                                        TipoHabitoId = 2;
                                    }
                                    else if (worksheet1.Rows[filaAntecedentes].Cells[46].Value.ToString().Trim() == "CONSUMO DE DROGAS")
                                    {
                                        TipoHabitoId = 3;
                                    }
                                    else if (worksheet1.Rows[filaAntecedentes].Cells[46].Value.ToString().Trim() == "ACTIVIDAD FÍSICA")
                                    {
                                        TipoHabitoId = 4;
                                    }
                                    noxioushabitsDto.i_TypeHabitsId = TipoHabitoId;
                                  
                                    noxioushabitsDto.v_Frequency = worksheet1.Rows[filaAntecedentes].Cells[47].Value.ToString();
                                    noxioushabitsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[48].Value.ToString();
                                    noxioushabitsDto.v_PersonId = Personid;
                                    
                                    _noxioushabitsDto.Add(noxioushabitsDto);
                                    oHistoryBL.AddNoxiousHabits(ref objOperationResult,
                                                   _noxioushabitsDto,
                                                   null,
                                                   null,
                                                   Globals.ClientSession.GetAsList());

                                }
                                #endregion
                                #region Antecedentes Familiares

                                List<familymedicalantecedentsDto> _familymedicalantecedentsDto;
                                familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                                //Padre
                                if (worksheet1.Rows[filaAntecedentes].Cells[50].Value != null)
                                {
                                    _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                                    familymedicalantecedentsDto.i_TypeFamilyId = 53;//Padre
                                    familymedicalantecedentsDto.v_PersonId = Personid;
                                    string FamiliarDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[50].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[50].Value.ToString().Length - 16, 16);

                                    familymedicalantecedentsDto.v_DiseasesId = FamiliarDiseasesId;
                                    familymedicalantecedentsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[51].Value.ToString();

                                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                                   oHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                                            _familymedicalantecedentsDto,
                                                                            null,
                                                                            null,
                                                                            Globals.ClientSession.GetAsList());
                                }

                                //Madre
                                if (worksheet1.Rows[filaAntecedentes].Cells[52].Value != null)
                                {
                                    _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                                    familymedicalantecedentsDto.i_TypeFamilyId = 43;//Padre
                                    familymedicalantecedentsDto.v_PersonId = Personid;
                                    string FamiliarDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[52].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[52].Value.ToString().Length - 16, 16);

                                    familymedicalantecedentsDto.v_DiseasesId = FamiliarDiseasesId;
                                    familymedicalantecedentsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[53].Value.ToString();

                                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                                    oHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                                             _familymedicalantecedentsDto,
                                                                             null,
                                                                             null,
                                                                             Globals.ClientSession.GetAsList());
                                }

                                //Esposos
                                if (worksheet1.Rows[filaAntecedentes].Cells[54].Value != null)
                                {
                                    _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                                    familymedicalantecedentsDto.i_TypeFamilyId = 22;//Padre
                                    familymedicalantecedentsDto.v_PersonId = Personid;
                                    string FamiliarDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[54].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[54].Value.ToString().Length - 16, 16);

                                    familymedicalantecedentsDto.v_DiseasesId = FamiliarDiseasesId;
                                    familymedicalantecedentsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[55].Value.ToString();

                                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                                    oHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                                             _familymedicalantecedentsDto,
                                                                             null,
                                                                             null,
                                                                             Globals.ClientSession.GetAsList());
                                }

                                #endregion



                                filaAntecedentes += 1;
                            }
                           

                           

                            #endregion


                        }
                


                  


                    MessageBox.Show("La importación fue correcta.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);





                  


                }

            }































            //serviceComponentFields = new ServiceComponentFieldsList();

            //serviceComponentFields.v_ComponentFieldsId = "N009-MF000000719";
            //serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

            //_serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
            //serviceComponentFieldValues = new ServiceComponentFieldValuesList();

            //serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            //serviceComponentFieldValues.v_Value1 = ((int)SiNo.SI).ToString();
            //_serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            //serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            //// Agregar a mi lista
            //_serviceComponentFieldsList.Add(serviceComponentFields);




            //var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
            //                                               _serviceComponentFieldsList,
            //                                               Globals.ClientSession.GetAsList(),
            //                                               Personid,
            //                                               ServiceComponentId);

        }

        private void btnGenerarIds_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ProtocolBL oProtocolBL = new ProtocolBL();
            List<string> ListaServiciosID = new List<string>();
            foreach (var item in ListaGrilla)
            {
                ListaServiciosID.Add(item.v_ServiceId);
            }
            var x = (KeyValueDTO)ddlConsultorio.SelectedItem;
            List<ObtenerIdsImporacion> Ids = new List<ObtenerIdsImporacion>();
            if ((int)x.Value4 == 11 || (int)x.Value4 == 10)
            {
                Ids = oProtocolBL.ObtenerIdsParaImportacion(ListaServiciosID, (int)x.Value4);
            }
            else
            {
                Ids = oProtocolBL.ObtenerIdsParaImportacion_(ListaServiciosID, (int)x.Value4);
            }
          


            var Cadena = "";
            string Saltos = "\n";
            if ((int)x.Value4 == 11)//MEd
            {
                  Saltos = "\n\n\n\n\n\n\n\n\n\n\n";
            }
            else if ((int)x.Value4 == 1)//LAb
            {
                Saltos = "\n\n\n\n\n";
            }
            else if ((int)x.Value4 == 15)//AUD
            {
                Saltos = "\n\n\n";
            }
            else if ((int)x.Value4 == 10)//Tria
            {
                Saltos = "\n\n\n\n\n\n\n\n\n\n";
            }
            else if ((int)x.Value4 == 14)//Oft
            {
                Saltos = "\n";
            }
            else if ((int)x.Value4 == 7)//Psic
            {
                Saltos = "\n\n\n\n\n";
            }

            foreach (var item in Ids)
            {
                Cadena += item.ServicioId + "\t" + item.ServicioComponentId + "\t" + item.ComponentId + "\t" + item.PersonId + "\t" + item.Paciente + "\t" + item.DNI + Saltos;
            }

            Clipboard.SetDataObject(Cadena, true);

            MessageBox.Show("La información se copió correctamente en el porta papeles ", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
   
        }

        private void btnGeneracionMasivaReportes_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
                List<ServiceComponentList> ListaOrdenada = new List<ServiceComponentList>();

    

                //Obtener todos los servicios que no tienes generación de reportes y estén culminados
                var ListaServicios = _serviceBL.ListarServiciosSinReportes();

                foreach (var item_0 in ListaServicios)
                {
                    serviceComponents = new List<ServiceComponentList>();
                    ConsolidadoReportes = new List<ServiceComponentList>();
                    serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(item_0.ServiceId);

                    foreach (var item in serviceComponents)
                    {


                        if (item.v_ComponentId == Constants.ALTURA_7D_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 8;
                        }
                        else if (item.v_ComponentId == Constants.EVA_ERGONOMICA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 9;
                        }
                        if (item.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 10;
                        }
                        else if (item.v_ComponentId == Constants.ALTURA_ESTRUCTURAL_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 11;
                        }
                        else if (item.v_ComponentId == Constants.TEST_VERTIGO_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 12;
                        }
                        else if (item.v_ComponentId == Constants.EVA_NEUROLOGICA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 13;
                        }
                        else if (item.v_ComponentId == Constants.SINTOMATICO_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 14;
                        }
                        else if (item.v_ComponentId == Constants.TAMIZAJE_DERMATOLOGIO_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 15;
                        }
                        else if (item.v_ComponentId == Constants.TESTOJOSECO_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 16;
                        }
                        else if (item.v_ComponentId == Constants.OFTALMOLOGIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 17;
                        }
                        else if (item.v_ComponentId == Constants.RX_TORAX_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 18;
                        }
                        else if (item.v_ComponentId == Constants.OIT_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 19;
                        }
                        else if (item.v_ComponentId == Constants.LUMBOSACRA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 20;
                        }
                        else if (item.v_ComponentId == Constants.ACUMETRIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 21;
                        }
                        else if (item.v_ComponentId == Constants.OTOSCOPIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 22;
                        }
                        else if (item.v_ComponentId == Constants.AUDIOMETRIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 23;
                        }
                        else if (item.v_ComponentId == Constants.ODONTOGRAMA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 24;
                        }
                        else if (item.v_ComponentId == Constants.ESPIROMETRIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 25;
                        }
                        else if (item.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 26;
                        }

                        else if (item.v_ComponentId == Constants.HISTORIA_CLINICA_PSICOLOGICA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 28;
                        }
                        else if (item.v_ComponentId == Constants.PSICOLOGIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 29;
                        }
                        else if (item.v_ComponentId == Constants.EVALUACION_PSICOLABORAL)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 30;
                        }
                        else if (item.v_ComponentId == Constants.SOMNOLENCIA_ID)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 31;
                        }
                        else if (item.v_ComponentId == Constants.FICHA_OTOSCOPIA)
                        {
                            var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                            ent.Orden = 32;
                        }


                    }
                    string[] ConsolidadoReportesForPrint = new string[] 
                    {
                        Constants.ALTURA_ESTRUCTURAL_ID,
                        Constants.ALTURA_7D_ID,
                        Constants.CUESTIONARIO_ACTIVIDAD_FISICA,
                        Constants.OSTEO_MUSCULAR_ID_1,                
                        Constants.ESPIROMETRIA_ID,     
                        Constants.AUDIOMETRIA_ID,  
                        Constants.OFTALMOLOGIA_ID,
                        Constants.TESTOJOSECO_ID, 
                        Constants.ELECTROCARDIOGRAMA_ID,
                        Constants.EVA_CARDIOLOGICA_ID,
                        Constants.EVA_NEUROLOGICA_ID,
                        Constants.OSTEO_MUSCULAR_ID_2,
                        Constants.EVA_OSTEO_ID,
                        Constants.HISTORIA_CLINICA_PSICOLOGICA_ID,
                        Constants.EVALUACION_PSICOLABORAL,
                        Constants.ECOGRAFIA_ABDOMINAL_ID,
                        Constants.INFORME_ECOGRAFICO_PROSTATA_ID,
                        Constants.ECOGRAFIA_RENAL_ID,
                        Constants.OIT_ID,
                        Constants.RX_TORAX_ID,
                        Constants.ODONTOGRAMA_ID,
                        Constants.TAMIZAJE_DERMATOLOGIO_ID,
                        //Constants.PRUEBA_ESFUERZO_ID,
                        Constants.PSICOLOGIA_ID,
                        //Constants.GINECOLOGIA_ID,
                        Constants.C_N_ID,
                        Constants.TEST_VERTIGO_ID,
                        Constants.EVA_ERGONOMICA_ID,
                        Constants.SOMNOLENCIA_ID,
                        Constants.ACUMETRIA_ID,
                        Constants.OTOSCOPIA_ID,
                        Constants.SINTOMATICO_ID,
                        Constants.LUMBOSACRA_ID
                    };

                    string[] InformeAnexo3121 = new string[] 
                    { 
                        Constants.EXAMEN_FISICO_ID
               
                    };
                    string[] InformeFisico7C1 = new string[] 
                    { 
                        Constants.EXAMEN_FISICO_7C_ID               
                    };
                    string[] ExamenBioquimica1 = new string[] 
                    { 
                         Constants.GLUCOSA_ID,
                        Constants.COLESTEROL_ID,
                        Constants.TRIGLICERIDOS_ID,
                        Constants.COLESTEROL_HDL_ID,
                        Constants.COLESTEROL_LDL_ID,
                        Constants.COLESTEROL_VLDL_ID,
                        Constants.UREA_ID,
                        Constants.CREATININA_ID,
                        Constants.TGO_ID,
                        Constants.TGP_ID,
                        Constants.TEST_ESTEREOPSIS_ID,
                        Constants.ANTIGENO_PROSTATICO_ID,
                        Constants.PLOMO_SANGRE_ID,
                        Constants.BIOQUIMICA01_ID,
                        Constants.BIOQUIMICA02_ID,
                        Constants.BIOQUIMICA03_ID
                    };
                    string[] ExamenEspeciales1 = new string[] 
                    { 
                        Constants.BK_DIRECTO_ID,
                        Constants.EXAMEN_ELISA_ID,
                        Constants.HEPATITIS_A_ID,
                        Constants.HEPATITIS_C_ID,
                        Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID,
                        Constants.VDRL_ID,
                    };


                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CONSENTIMIENTO INFORMADO ", v_ComponentId = Constants.CONSENTIMIENTO_INFORMADO });
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD SIN Diagnósticos ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX });
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD EMPRESARIAL ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL });
                    //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD Completo", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_COMPLETO });
                    //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 5, v_ComponentName = "CERTIFICADO APTITUD SM ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SM });
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
                    //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 7, v_ComponentName = "INFORME MÉDICO RESUMEN", v_ComponentId = Constants.INFORME_MEDICO_RESUMEN });
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 3, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 1", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR });
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 2", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2 });

                    var serviceComponents11 = _serviceBL.GetServiceComponentsForManagementReport(item_0.ServiceId);
                    var ResultadoAnexo3121 = serviceComponents11.FindAll(p => InformeAnexo3121.Contains(p.v_ComponentId)).ToList();
                    if (ResultadoAnexo3121.Count() != 0)
                    {
                        ConsolidadoReportes.Add(new ServiceComponentList { Orden = 5, v_ComponentName = "ANEXO 312", v_ComponentId = Constants.INFORME_ANEXO_312 });
                    }
                    var ResultadoFisico7C1 = serviceComponents11.FindAll(p => InformeFisico7C1.Contains(p.v_ComponentId)).ToList();
                    if (ResultadoFisico7C1.Count() != 0)
                    {
                        ConsolidadoReportes.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 7C", v_ComponentId = Constants.INFORME_ANEXO_7C });
                    }
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 7, v_ComponentName = "HISTORIA OCUPACIONAL", v_ComponentId = Constants.INFORME_HISTORIA_OCUPACIONAL });
                    var ResultadoBioquimico1 = serviceComponents11.FindAll(p => ExamenBioquimica1.Contains(p.v_ComponentId)).ToList();
                    if (ResultadoBioquimico1.Count() != 0)
                    {
                        ConsolidadoReportes.Add(new ServiceComponentList { Orden = 27, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });
                    }
                    
                    ConsolidadoReportes.AddRange(serviceComponents.FindAll(p => ConsolidadoReportesForPrint.Contains(p.v_ComponentId)));

                    ListaOrdenada = ConsolidadoReportes.OrderBy(p => p.Orden).ToList();
                    List<string> reportesId = new List<string>();



                    foreach (var item1 in ListaOrdenada)
                    {
                        reportesId.Add(item1.v_ComponentId);
                    }

                    Reports.frmManagementReports frm = new Reports.frmManagementReports();

                    frm.GenerarReportes(item_0.ServiceId,item_0.PacienteId, reportesId);



                }

                //Cambiar de estado a generado de reportes

                   OperationResult objOperationResult = new OperationResult();
                foreach (var item in ListaServicios)
                {

                    _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult,2, item.ServiceId, Globals.ClientSession.GetAsList());

                }

                BindGrid();




            }

            MessageBox.Show("Los reportes se generaron correctamente", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
   

        }

        private void grdDataService_MouseEnterElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceBL oServiceBL = new ServiceBL();
            List<ServiceComponentList> oServiceComponentList = new List<ServiceComponentList>();
            StringBuilder Cadena = new StringBuilder();


            // if we are not entering a cell, then don't anything
            if (!(e.Element is CellUIElement))
            {
                return;
            }

            // find the cell that the cursor is over, if any
            UltraGridCell cell = e.Element.GetContext(typeof(UltraGridCell)) as UltraGridCell;

            if (cell != null)
            {
                //int categoryId = int.Parse(cell.Row.Cells["i_CategoryId"].Value.ToString());
                //oServiceComponentList = oServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, categoryId, _serviceId);
                string serviceId =cell.Row.Cells["v_ServiceId"].Value.ToString();
                oServiceComponentList = oServiceBL.GetServiceComponents(ref objOperationResult, serviceId);
                //if (categoryId != -1)
                //{

                    foreach (var item in oServiceComponentList)
                    {
                        Cadena.Append(item.v_CategoryName + " - ");
                        Cadena.Append(item.v_ServiceComponentStatusName);
                        Cadena.Append("\n");
                    }

                    _customizedToolTip.AutomaticDelay = 1;
                    _customizedToolTip.AutoPopDelay = 20000;
                    _customizedToolTip.ToolTipMessage = Cadena.ToString();
                    _customizedToolTip.StopTimerToolTip();
                    _customizedToolTip.StartTimerToolTip();
                //}

            }
        }

        private void grdDataService_MouseLeaveElement(object sender, Infragistics.Win.UIElementEventArgs e)
        {
            // if we are not leaving a cell, then don't anything
            if (!(e.Element is CellUIElement))
            {
                return;
            }

            // prevent the timer from ticking again
            _customizedToolTip.StopTimerToolTip();

            // destroy the tooltip
            _customizedToolTip.DestroyToolTip(this);
        }

        private void btnImprimirExamenes_Click(object sender, EventArgs e)
        {
            List<string> _filesNameToMerge = new List<string>();
            string ServiceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            string Paciente = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
            var ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
      
            string[] files = Directory.GetFiles(ruta, "*.pdf");

            int strIndex = 0;
            string findThisString = ruta + ServiceId;
            for (int i = 0; i < files.Length; i++)
            {

                strIndex = files[i].IndexOf(findThisString);

                if (strIndex >= 0)
                {
                    _filesNameToMerge.Add(files[i].ToString());

                }
            }

            var x = _filesNameToMerge.ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
            _mergeExPDF.DestinationFile = ruta + "Paciente" + ".pdf";
            _mergeExPDF.Execute();
            _mergeExPDF.RunFile();
   
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            //if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            //{
            //    NombreArchivo = "Reporte Vacunas de " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            //    //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            //}
            //else
            //{
            NombreArchivo = "Reporte Facturación de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
            //NombreArchivo = "Matriz de datos";
            //}

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataService, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
