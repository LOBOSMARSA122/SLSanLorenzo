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
    public partial class frmService_ : Form
    {
        OperationResult _objOperationResult = new OperationResult();
        List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
        List<string> _componentIds;
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        string strFilterExpression;
        ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId;
        private string _EmpresaClienteId;
        private string _pacientId;
        private string _protocolId;
        private string _customerOrganizationName;
        private string _personFullName;
        List<string> _ListaServicios;
        List<string> _ListaServiciosHistoria;
        List<string> _ListaServiciosAdjuntar;
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip _customizedToolTip = null;
        private SaveFileDialog saveFileDialog2 = new SaveFileDialog();
        private List<ServiceGridJerarquizadaList> ListaGrilla = new List<ServiceGridJerarquizadaList>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public frmService_()
        {
            InitializeComponent();
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            this.Show();
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


            dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(0);

            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            ////Llenado de los tipos de servicios [Emp/Part]
            //Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1, null), DropDownListAction.Select);
            //// combo servicio
            //Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.Select);
          
            //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.All);

            Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);
                       
            Utils.LoadDropDownList(ddServiceStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 125, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlStatusAptitudId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 124, null), DropDownListAction.All);

            Utils.LoadDropDownList(cboHistoriaGenerada, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 258, null), DropDownListAction.All);

            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);
            
            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

             groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
             // Remover los componentes que no estan asignados al rol del usuario
             var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));

             Utils.LoadDropDownList(cboUserMed, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
          
            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", results, DropDownListAction.Select);

            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;

            ddlConsultorio.SelectedValueChanged += ddlConsultorio_SelectedValueChanged;


            btnHistoriaCl.Enabled = false;

          
     
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
            if (!string.IsNullOrEmpty(txtServicioId.Text)) Filters.Add("v_ServiceId.Contains(\"" + txtServicioId.Text.Trim() + "\")");
            
            //if (!string.IsNullOrEmpty(txtDiagnostico.Text)) Filters.Add("v_DiseasesName.Contains(\"" + txtDiagnostico.Text.Trim() + "\")");
            if (ddServiceStatusId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceStatusId==" + ddServiceStatusId.SelectedValue);

            if (cboHistoriaGenerada.SelectedValue.ToString() != "-1")
            {
                if (cboHistoriaGenerada.SelectedValue.ToString() =="2")
                {
                    Filters.Add("i_StatusLiquidation==2");
                }
                else
                {
                    Filters.Add("i_StatusLiquidation==NULL");
                }
                //Filters.Add("i_StatusLiquidation==" + cboHistoriaGenerada.SelectedValue.ToString() == "2" ? "2" : "i_StatusLiquidation==NULL");
            }


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
            if (cboUserMed.SelectedValue.ToString() != "-1") Filters.Add("i_ApprovedUpdateUserId==" + cboUserMed.SelectedValue);


            
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

            //var _objData = _serviceBL.GetServicesPagedAndFiltered_F(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, _componentIds, FCI, FCF, txtDiagnostico.Text);

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
            else
            {
                ddlMasterServiceId.Enabled = true;
            }
            //ddlMasterServiceId.Enabled = true;
            //OperationResult objOperationResult = new OperationResult();
            //Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, int.Parse(ddlServiceTypeId.SelectedValue.ToString()), Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
          
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
               #region ESO V1
                   frm = new Operations.frmEso(_serviceId, null, null, TserviceId);
                   frm.ShowDialog();
               #endregion
               #region ESO V2 (Asíncrono)
               //frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
               //frm.ShowDialog();
               #endregion   
           }
           else
           {
               //Obtener Estado del servicio
               var estadoAptitud = int.Parse(grdDataService.Selected.Rows[0].Cells["i_AptitudeStatusId"].Value.ToString());

               if (estadoAptitud != (int)AptitudeStatus.SinAptitud || estadoAptitud == (int)AptitudeStatus.AptoObs)
               {
                   //Obtener el usuario
                   int UserId= Globals.ClientSession.i_SystemUserId ;
                   if (UserId == 11 || UserId == 175 || UserId == 173 || UserId == 172 || UserId == 171 || UserId == 168 || UserId == 169)
	                {
                        this.Enabled = false;
                        frm = new Operations.frmEso(_serviceId, null, "Service", TserviceId);
                        frm.ShowDialog();
                        //frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                        //frm.ShowDialog();
                        this.Enabled = true;
	                }
                   else
                   {
                        this.Enabled = false;
                        frm = new Operations.frmEso(_serviceId, null, "View", TserviceId);
                        frm.ShowDialog();
                        //frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                        //frm.ShowDialog();
                        this.Enabled = true;                   
                   }
                  
               }
               else 
               {
                   this.Enabled = false;
                   frm = new Operations.frmEso(_serviceId, null, "Service",(int)MasterService.Eso);
                   frm.ShowDialog();
                   this.Enabled = true;
               }

             
           }

           btnFilter_Click(sender, e);
                  
           
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

                        //var TestIhihara = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ISHIHARA_ID);
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
                        var serviceComponents = _serviceBL.GetServiceComponentsReport_New312(_serviceId);

                        FichaMedicaOcupacional312.CreateFichaMedicalOcupacional312Report(_DataService,
                                  filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                                  _listMedicoPersonales, _listaHabitoNocivos,
                                  Audiometria,//Psicologia, RX, RX1, , Espirometria,
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

            var StatusLiquidation = grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value == null ? 1 : int.Parse(grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value.ToString());

            if (StatusLiquidation == 2)
            {
                var DialogResult = MessageBox.Show("Este servicio ya tiene, reportes generados, ¿Desea volver a generar?", "INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DialogResult == DialogResult.No) 
                   {
                       string ruta = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();
                       System.Diagnostics.Process.Start(ruta);
                       Clipboard.SetText(grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString());

                       //var companiaMinera = grdDataService.Selected.Rows[0].Cells["CompMinera"].Value.ToString();
                       //var paciente = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
                       //var fecha = DateTime.Parse(grdDataService.Selected.Rows[0].Cells["d_ServiceDate"].Value.ToString()).ToString("dd MMMM,  yyyy");

                       //var namePdf = companiaMinera + " - " + paciente + " - " + fecha;
                       //string pdfPath = Path.Combine(ruta, namePdf + ".pdf");

                       //Process.Start(pdfPath);
                       return;
                   }
 
            }

            int flagPantalla = int.Parse(grdDataService.Selected.Rows[0].Cells["i_MasterServiceId"].Value.ToString()); // int.Parse(ddlServiceTypeId.SelectedValue.ToString());
            int eso = 1;
            if (flagPantalla == 2)
            {
                var frm = new Reports.frmManagementReports(_serviceId, _pacientId, _customerOrganizationName, _personFullName, flagPantalla, _EmpresaClienteId, eso);
                frm.ShowDialog();
            }
            else
            {
                var edad = int.Parse(grdDataService.Selected.Rows[0].Cells["i_age"].Value.ToString());
                var frm = new Reports.frmManagementReportsMedical(_serviceId, _pacientId, _customerOrganizationName, _personFullName, _EmpresaClienteId, edad);
                frm.ShowDialog();
            }     

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
                            e.Row.Appearance.BackColor = Color.GreenYellow;
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
                                                        false, false);





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
                    serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 3", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3 });
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






                    //Verificar si tiene orden de resportes
                    List<string> reportesId = new List<string>();
                    OrganizationBL oOrganizationBL = new OrganizationBL();
                    OperationResult objOperationResult = new OperationResult();
                    List<ServiceComponentList> ListaFinalOrdena = new List<ServiceComponentList>();
                    var ListaOrdenReportes = oOrganizationBL.GetOrdenReportes(ref objOperationResult, item_0.EmpresaCliente);
                    if (ListaOrdenReportes.Count > 0)
                    {
                        serviceComponents = new List<ServiceComponentList>();
                        serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(item_0.ServiceId);

                        //serviceComponents.Add(new ServiceComponentList {  v_ComponentName = "CONSENTIMIENTO INFORMADO ", v_ComponentId = Constants.CONSENTIMIENTO_INFORMADO });

                        serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CONSENTIMIENTO INFORMADO ", v_ComponentId = Constants.CONSENTIMIENTO_INFORMADO });
                        serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD SIN Diagnósticos ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX });
                        serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD EMPRESARIAL ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL });
                        serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
                        serviceComponents.Add(new ServiceComponentList { Orden = 3, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 1", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR });
                        serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 2", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2 });
                        serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 3", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3 });
                        serviceComponents.Add(new ServiceComponentList { Orden = 27, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });

                        //var serviceComponents11 = _serviceBL.GetServiceComponentsForManagementReport(_serviceId);
                        var ResultadoAnexo312 = serviceComponents.FindAll(p => InformeAnexo3121.Contains(p.v_ComponentId)).ToList();
                        if (ResultadoAnexo312.Count() != 0)
                        {
                            serviceComponents.Add(new ServiceComponentList { Orden = 5, v_ComponentName = "ANEXO 312", v_ComponentId = Constants.INFORME_ANEXO_312 });
                        }
                        var ResultadoFisico7C = serviceComponents.FindAll(p => InformeFisico7C1.Contains(p.v_ComponentId)).ToList();
                        if (ResultadoFisico7C.Count() != 0)
                        {
                            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 7C", v_ComponentId = Constants.INFORME_ANEXO_7C });
                        }
                        serviceComponents.Add(new ServiceComponentList { Orden = 7, v_ComponentName = "HISTORIA OCUPACIONAL", v_ComponentId = Constants.INFORME_HISTORIA_OCUPACIONAL });
            


                        ListaOrdenada = new List<ServiceComponentList>();
                        ServiceComponentList oServiceComponentList = null;


                        foreach (var item in ListaOrdenReportes)
                        {
                            oServiceComponentList = new ServiceComponentList();
                            oServiceComponentList.v_ComponentName = item.v_NombreReporte;
                            oServiceComponentList.v_ComponentId = item.v_ComponenteId +'|' +item.i_NombreCrystalId;
                            ListaOrdenada.Add(oServiceComponentList);
                        }


                        foreach (var item in ListaOrdenada)
                        {
                            //var ComponenteId = "";
                            //if (item.v_ComponentId.Length > 16)
                            //{
                            //    ComponenteId = item.v_ComponentId.Substring(0, 16);
                            //}
                            //else
                            //{
                            //    ComponenteId = item.v_ComponentId;
                            //}
                            var array = item.v_ComponentId.Split('|');
                            foreach (var item1 in serviceComponents)
                            {
                                if (array[0].ToString() == item1.v_ComponentId)
                                {
                                    ListaFinalOrdena.Add(item);
                                }
                            }
                        }

                       
                        foreach (var item1 in ListaFinalOrdena)
                        {
                            reportesId.Add(item1.v_ComponentId);
                        }


                    }
                    else
                    {                       
                        foreach (var item1 in ListaOrdenada)
                        {
                            reportesId.Add(item1.v_ComponentId);
                        }
                    }

                                       
                        Reports.frmManagementReports frm = new Reports.frmManagementReports();

                        frm.GenerarReportes(item_0.ServiceId, item_0.PacienteId, reportesId);
                  
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
                string serviceId = cell.Row.Cells["v_ServiceId"].Value.ToString();
                oServiceComponentList = oServiceBL.GetServiceComponents(ref objOperationResult, serviceId);
                //if (categoryId != -1)
                //{

                foreach (var item in oServiceComponentList)
                {
                    Cadena.Append(item.v_CategoryName + " - ");
                    Cadena.Append(item.v_ServiceComponentStatusName);
                    Cadena.Append("\n");
                }

                var FirmaMedicoMedicina = new ServiceBL().ObtenerFirmaMedicoExamen(serviceId, Constants.EXAMEN_FISICO_ID, Constants.EXAMEN_FISICO_7C_ID);
                if (FirmaMedicoMedicina != null )
                {
                    Cadena.Append("\n");
                    Cadena.Append("MÉDICO");
                    Cadena.Append("\n");
                    Cadena.Append(FirmaMedicoMedicina.Value2);
                }

                _customizedToolTip.AutomaticDelay = 1;
                _customizedToolTip.AutoPopDelay = 20000;
                _customizedToolTip.ToolTipMessage = Cadena.ToString() ;
                _customizedToolTip.StopTimerToolTip();
                _customizedToolTip.StartTimerToolTip();
                //}

            }
        }

      

        private void btnImprimirExamenes_Click(object sender, EventArgs e)
        {

          //  //obtener  todos los dx eliminados
          //var LDxEliminados=  _serviceBL.DevolverDxELiminados();

          //foreach (var item in LDxEliminados)
          //{
          //    _serviceBL.ObtenerRecomendacionesPorDiagnosticRepository(item.v_DiagnosticRepositoryId);            
              
          //}





            //List<string> _filesNameToMerge = new List<string>();
            //string ServiceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            //string Paciente = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
            //var ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
      
            //string[] files = Directory.GetFiles(ruta, "*.pdf");

            //int strIndex = 0;
            //string findThisString = ruta + ServiceId;
            //for (int i = 0; i < files.Length; i++)
            //{

            //    strIndex = files[i].IndexOf(findThisString);

            //    if (strIndex >= 0)
            //    {
            //        _filesNameToMerge.Add(files[i].ToString());

            //    }
            //}

            //var x = _filesNameToMerge.ToList();
            //_mergeExPDF.FilesName = x;
            //_mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
            //_mergeExPDF.DestinationFile = ruta + "Paciente" + ".pdf";
            //_mergeExPDF.Execute();
            //_mergeExPDF.RunFile();
   
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

        private void btnBotonOculto_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                //Obtener Lista de MultifileId
                OperationResult operationResult = new OperationResult();
                MultimediaFileBL oMultimediaFileBL = new MultimediaFileBL();
                var Lista = oMultimediaFileBL.DevolverTodosArchivos();
                foreach (var item in Lista)
                {
                    var multimediaFile = oMultimediaFileBL.GetMultimediaFileById(ref operationResult, item.v_MultimediaFileId);

                    if (multimediaFile != null)
                    {
                        string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        using (SaveFileDialog sfd = new SaveFileDialog())
                        {

                            string Fecha = multimediaFile.FechaServicio.Value.Day.ToString().PadLeft(2, '0') + multimediaFile.FechaServicio.Value.Month.ToString().PadLeft(2, '0') + multimediaFile.FechaServicio.Value.Year.ToString();

                            //Obtener la extensión del archivo
                            string Ext = multimediaFile.FileName.Substring(multimediaFile.FileName.Length - 3, 3);

                            sfd.Title = multimediaFile.dni + "-" + Fecha + "-" + multimediaFile.FileName + "." + Ext;
                            sfd.FileName = mdoc + "\\" + sfd.Title;

                            string path = sfd.FileName;
                            if (multimediaFile.ByteArrayFile != null)
                            {
                                File.WriteAllBytes(path, multimediaFile.ByteArrayFile);
                            }
                          
                            
                        }
                    }
                  
                }
            }

        }

        private void btnQuitarChek_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            foreach (var item in grdDataService.Selected.Rows)
            {
                var serviceId = item.Cells["v_ServiceId"].Value.ToString(); // grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

                _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult, null, serviceId, Globals.ClientSession.GetAsList());
            }
         

            MessageBox.Show("Se actualizó estado", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnFilter_Click(sender, e);

        }

        private void btnActualizarPerson_Click(object sender, EventArgs e)
        {
            _serviceBL.ActualizarContrasenaPersona();
            MessageBox.Show("Se actualizó", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
        }

        private void btnActualizarAptitud_Click(object sender, EventArgs e)
        {

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                foreach (var item in grdDataService.Rows)
                {
                    if (item.Cells["i_ServiceStatusId"].Value.ToString() == "6")
                    {
                        var pServiceId = item.Cells["v_ServiceId"].Value.ToString();

                        _serviceBL.ListarServiciosEsperandoAptitud(pServiceId,Globals.ClientSession.GetAsList());
                    }
                }
                btnFilter_Click(sender, e);
            };

          
                    

        }

        private void btnActualizarCulminado_Click(object sender, EventArgs e)
        {

            if (grdDataService.Rows.Count ==0)
            {
                 MessageBox.Show("No hay ningún registro en la búsqueda", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;    
            }
            OperationResult objOperationResult = new OperationResult();
            frmCulminarMasivo frm = new frmCulminarMasivo();
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;


            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                foreach (var item in grdDataService.Rows)
                {
                    //if (item.Cells["i_ServiceStatusId"].Value.ToString() == "2")
                    //{
                        //Actualizar Firmas
                        var pServiceId = item.Cells["v_ServiceId"].Value.ToString();
                        _serviceBL.BuscarServiceComponentParaFirmas(pServiceId, frm.CategoriaId.Value, frm.TecnicoId.Value, frm.MedicoId.Value);

                        var alert = _serviceBL.GetServiceComponentsCulminados(ref objOperationResult, pServiceId);

                        if (alert != null && alert.Count > 0)// consulta trae datos.... examenes que no estan evaluados
                        {

                        }
                        else
                        {
                            serviceDto objserviceDto = new serviceDto();
                            objserviceDto = _serviceBL.GetService(ref objOperationResult, pServiceId);
                            objserviceDto.i_ServiceStatusId = (int)ServiceStatus.EsperandoAptitud;
                            objserviceDto.v_Motive = "Esperando Aptitud";
                            _serviceBL.UpdateService(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());
                        }
                    //}     
                }
            }

            btnFilter_Click(sender, e);

        }

        private void btnCargoHistorias_Click(object sender, EventArgs e)
        {
          var resultado =  MessageBox.Show("¿Desea enviar historia?","¡ INFORMACIÓN !", MessageBoxButtons.YesNo , MessageBoxIcon.Information);

          

          if (resultado ==DialogResult.Yes)
          {
              //Actualizar Flag i_EnvioHistoria
            _ListaServiciosHistoria = new List<string>();
            foreach (var item in grdDataService.Rows)
            {
                    string x = item.Cells["v_ServiceId"].Value.ToString();
                    _serviceBL.ActualizarEnvioHistoria(x, 1);
            }                            

          }

            var frm = new Reports.frmCargoHistoria(ListaGrilla);
            frm.ShowDialog();
        }

        private void btnCargoFactura_Click(object sender, EventArgs e)
        {
            frmCargoFactura frm = new frmCargoFactura();
            frm.ShowDialog();
        }

        private void btnMigrarEmpresa_Click(object sender, EventArgs e)
        {
            ////Obtener empresa antiguas
            //OrganizationBL oOrganizationBL = new OrganizationBL();
            //LocationBL oLocationBL = new LocationBL();
            //GroupOccupationBL _objGroupOccupationBL = new GroupOccupationBL();
            //MigracionBL oMigracionBL = new MigracionBL();
            //OperationResult objOperationResult = new OperationResult();

            //var lEmpresasOLD = oMigracionBL.DevolverDatosEmpresaOLD();
            //int count = 0;
            //organizationDto objOrganizationDto;

            //foreach (var item in lEmpresasOLD)
            //{
            //    if (!oMigracionBL.VerificarSiExisteEmpresaAntigua(item.v_IdentificationNumber))
            //    {
            //        count++;
            //        objOrganizationDto = new organizationDto();
            //        // Populate the entity
            //        objOrganizationDto.i_OrganizationTypeId = item.i_OrganizationTypeId;
            //        objOrganizationDto.i_SectorTypeId = item.i_OrganizationTypeId;
            //        objOrganizationDto.v_SectorName = item.v_SectorName;
            //        objOrganizationDto.v_SectorCodigo = item.v_SectorCodigo;
            //        objOrganizationDto.v_IdentificationNumber = item.v_IdentificationNumber;
            //        objOrganizationDto.v_Name = item.v_Name;
            //        objOrganizationDto.v_Address = item.v_Address;
            //        objOrganizationDto.v_PhoneNumber = item.v_PhoneNumber;
            //        objOrganizationDto.v_Mail = item.v_Mail;
            //        objOrganizationDto.v_ContacName = item.v_ContacName;
            //        objOrganizationDto.v_Contacto = item.v_Contacto;
            //        objOrganizationDto.v_EmailContacto = item.v_EmailContacto;
            //        objOrganizationDto.v_Observation = item.v_Observation;
            //        objOrganizationDto.i_NumberQuotasOrganization = item.i_NumberQuotasOrganization;
            //        objOrganizationDto.i_NumberQuotasMen = item.i_NumberQuotasMen;
            //        objOrganizationDto.i_DepartmentId = item.i_DepartmentId;
            //        objOrganizationDto.i_ProvinceId = item.i_ProvinceId;
            //        objOrganizationDto.i_DistrictId = item.i_DistrictId;
            //        objOrganizationDto.i_IsDeleted = item.i_IsDeleted;
            //        objOrganizationDto.i_InsertUserId = item.i_InsertUserId;
            //        objOrganizationDto.d_InsertDate = item.d_InsertDate;
            //        objOrganizationDto.i_UpdateUserId = item.i_UpdateUserId;
            //        objOrganizationDto.d_UpdateDate = item.d_UpdateDate;
            //        objOrganizationDto.b_Image = item.b_Image;
            //        objOrganizationDto.v_ContactoMedico = item.v_ContactoMedico;
            //        objOrganizationDto.v_EmailMedico = item.v_EmailMedico;
            //        var OrganizationId_Old = item.v_OrganizationId;
            //        var OrganizationId = oOrganizationBL.AddOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());

            //        //Obtener Lista de Sedes Antiguos por IdEmpresa

            //        var ListaSedesAntiguas = oMigracionBL.DevolverSedesAntiguasPorIdEmpresa(OrganizationId_Old);
            //        locationDto objLocationDto;
            //        foreach (var itemSede in ListaSedesAntiguas)
            //        {
            //            objLocationDto = new locationDto();
            //            objLocationDto.v_OrganizationId = OrganizationId;
            //            objLocationDto.v_Name = itemSede.v_Name;
            //            objLocationDto.i_IsDeleted = itemSede.i_IsDeleted;
            //            objLocationDto.i_InsertUserId = itemSede.i_InsertUserId;
            //            objLocationDto.d_InsertDate = itemSede.d_InsertDate;
            //            objLocationDto.i_UpdateUserId = itemSede.i_UpdateUserId;
            //            objLocationDto.d_UpdateDate = itemSede.d_UpdateDate;

            //            var v_LocationId_OLD = itemSede.v_LocationId;
            //            var locationId = oLocationBL.AddLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());

            //            //Rutina para Asignar la Empresa creada automaticamente al nodo actual
            //            NodeOrganizationLoactionWarehouseList objNodeOrganizationLoactionWarehouseList = new NodeOrganizationLoactionWarehouseList();
            //            List<nodeorganizationlocationwarehouseprofileDto> objnodeorganizationlocationwarehouseprofileDto = new List<nodeorganizationlocationwarehouseprofileDto>();

            //            //Llenar Entidad Empresa/sede
            //            objNodeOrganizationLoactionWarehouseList.i_NodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
            //            objNodeOrganizationLoactionWarehouseList.v_OrganizationId = OrganizationId;
            //            objNodeOrganizationLoactionWarehouseList.v_LocationId = locationId;

            //            oOrganizationBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult, objNodeOrganizationLoactionWarehouseList, null, Globals.ClientSession.GetAsList());

            //            //Obtener GESO antiguos

            //            var ListaGESOAntiguas = oMigracionBL.DevolverGESOporLocationId(v_LocationId_OLD);
            //            groupoccupationDto objgroupoccupationDto;
            //            foreach (var itemGESO in ListaGESOAntiguas)
            //            {
            //                objgroupoccupationDto = new groupoccupationDto();
            //                objgroupoccupationDto.v_Name = itemGESO.v_Name;
            //                objgroupoccupationDto.v_LocationId = locationId;
            //                // Save the data
            //                _objGroupOccupationBL.AddGroupOccupation(ref objOperationResult, objgroupoccupationDto, Globals.ClientSession.GetAsList());
            //            }
            //        }
            //    }
            //}

            //var x = count;
        }

        private void btnPErson_Click(object sender, EventArgs e)
        {
            //MigracionBL oMigracionBL = new MigracionBL();
            //PacientBL oPacientBL = new PacientBL();
            //personDto objpersonDto = null;
            //HistoryBL oHistoryBL = new HistoryBL();

            //int count = 0;
            ////Obtener PErsonasOLD
            //var ListaPacientOLD = oMigracionBL.DevolverDatosPacientLD();
            //foreach (var item in ListaPacientOLD)
            //{
            //    //Verificar si la persona existe en la BD Actual
            //    var oPacient = oPacientBL.GetPersonByNroDocument(ref _objOperationResult, item.v_DocNumber);
            //    if (oPacient == null)
            //    {
            //        var PersonId_OLD = item.v_PersonId;
            //        objpersonDto = new personDto();
            //        objpersonDto.v_PersonId = item.v_PersonId;
            //        objpersonDto.v_FirstName = item.v_FirstName;
            //        objpersonDto.v_FirstLastName = item.v_FirstLastName;
            //        objpersonDto.v_SecondLastName = item.v_SecondLastName;
            //        objpersonDto.i_DocTypeId = item.i_DocTypeId;
            //        objpersonDto.v_DocNumber = item.v_DocNumber;
            //        objpersonDto.d_Birthdate = item.d_Birthdate;
            //        objpersonDto.v_BirthPlace = item.v_BirthPlace;
            //        objpersonDto.i_SexTypeId = item.i_SexTypeId;
            //        objpersonDto.i_MaritalStatusId = item.i_MaritalStatusId;
            //        objpersonDto.i_LevelOfId = item.i_LevelOfId;
            //        objpersonDto.v_TelephoneNumber = item.v_TelephoneNumber;
            //        objpersonDto.v_AdressLocation = item.v_AdressLocation;
            //        objpersonDto.v_GeografyLocationId = item.v_GeografyLocationId;
            //        objpersonDto.v_ContactName = item.v_ContactName;
            //        objpersonDto.v_EmergencyPhone = item.v_EmergencyPhone;
            //        objpersonDto.b_PersonImage = item.b_PersonImage;
            //        objpersonDto.v_Mail = item.v_Mail;
            //        objpersonDto.i_BloodGroupId = item.i_BloodGroupId;
            //        objpersonDto.i_BloodFactorId = item.i_BloodFactorId;
            //        objpersonDto.b_FingerPrintTemplate = item.b_FingerPrintTemplate;
            //        objpersonDto.b_RubricImage = item.b_RubricImage;
            //        objpersonDto.b_FingerPrintImage = item.b_FingerPrintImage;
            //        objpersonDto.t_RubricImageText = item.t_RubricImageText;
            //        objpersonDto.v_CurrentOccupation = item.v_CurrentOccupation;
            //        objpersonDto.i_DepartmentId = item.i_DepartmentId;
            //        objpersonDto.i_ProvinceId = item.i_ProvinceId;
            //        objpersonDto.i_DistrictId = item.i_DistrictId;
            //        objpersonDto.i_ResidenceInWorkplaceId = item.i_ResidenceInWorkplaceId;
            //        objpersonDto.v_ResidenceTimeInWorkplace = item.v_ResidenceTimeInWorkplace;
            //        objpersonDto.i_TypeOfInsuranceId = item.i_TypeOfInsuranceId;
            //        objpersonDto.i_NumberLivingChildren = item.i_NumberLivingChildren;
            //        objpersonDto.i_NumberDependentChildren = item.i_NumberDependentChildren;
            //        objpersonDto.i_OccupationTypeId = item.i_OccupationTypeId;
            //        objpersonDto.v_OwnerName = item.v_OwnerName;
            //        objpersonDto.i_NumberLiveChildren = item.i_NumberLiveChildren;
            //        objpersonDto.i_NumberDeadChildren = item.i_NumberDeadChildren;
            //        objpersonDto.i_IsDeleted = item.i_IsDeleted;
            //        objpersonDto.i_InsertUserId = item.i_InsertUserId;
            //        objpersonDto.d_InsertDate = item.d_InsertDate.Value;
            //        objpersonDto.i_UpdateUserId = item.i_UpdateUserId;
            //        objpersonDto.d_UpdateDate = item.d_UpdateDate;
            //        objpersonDto.i_InsertNodeId = item.i_InsertNodeId;
            //        objpersonDto.i_UpdateNodeId = item.i_UpdateNodeId;
            //        objpersonDto.i_Relationship = item.i_Relationship;
            //        objpersonDto.v_ExploitedMineral = item.v_ExploitedMineral;
            //        objpersonDto.i_AltitudeWorkId = item.i_AltitudeWorkId;
            //        objpersonDto.i_PlaceWorkId = item.i_PlaceWorkId;
            //        objpersonDto.v_NroPoliza = item.v_NroPoliza;
            //        objpersonDto.v_Deducible = item.v_Deducible == null ? 0 : item.v_Deducible.Value;
            //        objpersonDto.i_NroHermanos = item.i_NroHermanos;
            //        objpersonDto.v_Password = item.v_Password;

            //        var PersonId_Nuevo = oPacientBL.AddPacient(ref _objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());

                    //#region Ocupacional

                    //Obtener historia Ocupacional por PersonaId
                    //var ListaHistoriaPErsonId = oMigracionBL.ObtenerLisatHistoriaPorPersonId(PersonId_OLD);
                    //List<WorkstationDangersList> _TempWorkstationDangersList = new List<WorkstationDangersList>();
                    //WorkstationDangersList oWorkstationDangersList;
                    //List<TypeOfEEPList> _TempTypeOfEEPList = new List<TypeOfEEPList>();
                    //TypeOfEEPList oTypeOfEEPList;
                    //historyDto _objhistoryDto;
                    //foreach (var itemHistoria in ListaHistoriaPErsonId)
                    //{
                    //    Cargar Tabla History
                    //    _objhistoryDto = new historyDto();
                    //    _objhistoryDto.v_PersonId = PersonId_Nuevo;
                    //    _objhistoryDto.d_StartDate = itemHistoria.d_StartDate;
                    //    _objhistoryDto.d_EndDate = itemHistoria.d_EndDate;
                    //    _objhistoryDto.v_Organization = itemHistoria.v_Organization;
                    //    _objhistoryDto.v_TypeActivity = itemHistoria.v_TypeActivity;
                    //    _objhistoryDto.i_GeografixcaHeight = itemHistoria.i_GeografixcaHeight;
                    //    _objhistoryDto.v_workstation = itemHistoria.v_workstation;

                    //    _objhistoryDto.b_RubricImage = itemHistoria.b_RubricImage;
                    //    _objhistoryDto.b_FingerPrintImage = itemHistoria.b_FingerPrintImage;
                    //    _objhistoryDto.t_RubricImageText = itemHistoria.t_RubricImageText;
                    //    _objhistoryDto.i_TypeOperationId = itemHistoria.i_TypeOperationId;

                    //    Obtener Lista Peligros en puesto
                    //    var ListaPeligros = oMigracionBL.ObtenerListaPeligrosPorHistoryId(itemHistoria.v_HistoryId);

                    //    foreach (var itemPeligros in ListaPeligros)
                    //    {
                    //        oWorkstationDangersList = new WorkstationDangersList();
                    //        oWorkstationDangersList.i_DangerId = itemPeligros.i_DangerId;
                    //        oWorkstationDangersList.i_NoiseSource = itemPeligros.i_NoiseSource;
                    //        oWorkstationDangersList.i_NoiseLevel = itemPeligros.i_NoiseLevel;
                    //        oWorkstationDangersList.v_TimeOfExposureToNoise = itemPeligros.v_TimeOfExposureToNoise;
                    //        _TempWorkstationDangersList.Add(oWorkstationDangersList);

                    //    }

                    //    Obtener Lista EPP
                    //    var ListaEpps = oMigracionBL.ObtenerListaEPPSPorHistoryId(itemHistoria.v_HistoryId);
                    //    foreach (var itemEpp in ListaEpps)
                    //    {
                    //        oTypeOfEEPList = new TypeOfEEPList();
                    //        oTypeOfEEPList.i_TypeofEEPId = itemEpp.i_TypeofEEPId;
                    //        oTypeOfEEPList.r_Percentage = itemEpp.r_Percentage;
                    //        _TempTypeOfEEPList.Add(oTypeOfEEPList);

                    //    }

                    //    oHistoryBL.AddHistory(ref _objOperationResult, _TempWorkstationDangersList, _TempTypeOfEEPList, _objhistoryDto, Globals.ClientSession.GetAsList());


                    //}

                    //#endregion

                    //#region Personal
                    //var ListaPersonal = oMigracionBL.DevolverListaMedicoPersonalesPorPersonId(PersonId_OLD);
                    //List<personmedicalhistoryDto> _personmedicalhistoryDto = new List<personmedicalhistoryDto>();
                    //personmedicalhistoryDto opersonmedicalhistoryDto;
                    //foreach (var itemPersonal in ListaPersonal)
                    //{
                    //    opersonmedicalhistoryDto = new personmedicalhistoryDto();
                    //    opersonmedicalhistoryDto.v_PersonId = PersonId_Nuevo;
                    //    Verificar si Existe Disease
                    //    var DiseaseId = oMigracionBL.ValidarDiseaseSiExiste(itemPersonal.v_CIE10Id, itemPersonal.v_Name, Globals.ClientSession.GetAsList());
                    //    opersonmedicalhistoryDto.v_DiseasesId = DiseaseId;// itemPersonal.v_DiseasesId;

                    //    opersonmedicalhistoryDto.i_TypeDiagnosticId = itemPersonal.i_TypeDiagnosticId;
                    //    opersonmedicalhistoryDto.d_StartDate = itemPersonal.d_StartDate;
                    //    opersonmedicalhistoryDto.v_DiagnosticDetail = itemPersonal.v_DiagnosticDetail;
                    //    opersonmedicalhistoryDto.v_TreatmentSite = itemPersonal.v_TreatmentSite;
                    //    opersonmedicalhistoryDto.i_AnswerId = itemPersonal.i_Answer;

                    //    _personmedicalhistoryDto.Add(opersonmedicalhistoryDto);
                    //}

                    //oHistoryBL.AddPersonMedicalHistory(ref _objOperationResult,
                    //                             _personmedicalhistoryDto,
                    //                             null,
                    //                             null,
                    //                             Globals.ClientSession.GetAsList());
                    //#endregion

                    //#region Familiar
                    //var ListaFamiliar = oMigracionBL.DevolverListaMedicoFamiliaresPorPersonId(PersonId_OLD);
                    //List<familymedicalantecedentsDto> _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                    //familymedicalantecedentsDto ofamilymedicalantecedentsDto;
                    //foreach (var itemFamiliar in ListaFamiliar)
                    //{
                    //    ofamilymedicalantecedentsDto = new familymedicalantecedentsDto();
                    //    ofamilymedicalantecedentsDto.v_PersonId = PersonId_Nuevo;
                    //    var DiseaseId = oMigracionBL.ValidarDiseaseSiExiste(itemFamiliar.v_CIE10Id, itemFamiliar.v_Name, Globals.ClientSession.GetAsList());

                    //    ofamilymedicalantecedentsDto.v_DiseasesId = DiseaseId;
                    //    ofamilymedicalantecedentsDto.i_TypeFamilyId = itemFamiliar.i_TypeFamilyId;
                    //    ofamilymedicalantecedentsDto.v_Comment = itemFamiliar.v_Comment;
                    //    _familymedicalantecedentsDto.Add(ofamilymedicalantecedentsDto);
                    //}
                    //oHistoryBL.AddFamilyMedicalAntecedents(ref _objOperationResult,
                    //                             _familymedicalantecedentsDto,
                    //                             null,
                    //                             null,
                    //                             Globals.ClientSession.GetAsList());

                    //#endregion

                    //#region Habitos

                    //var ListaHabitos = oMigracionBL.DevolverListaHabitosPorPersonId(PersonId_OLD);
                    //List<noxioushabitsDto> _noxioushabitsDto = new List<noxioushabitsDto>();
                    //noxioushabitsDto onoxioushabitsDto;
                    //foreach (var itemHabitos in ListaHabitos)
                    //{
                    //    onoxioushabitsDto = new noxioushabitsDto();
                    //    onoxioushabitsDto.v_PersonId = PersonId_Nuevo;

                    //    onoxioushabitsDto.i_TypeHabitsId = itemHabitos.i_TypeHabitsId;
                    //    onoxioushabitsDto.v_Frequency = itemHabitos.v_Frequency; ;
                    //    onoxioushabitsDto.v_Comment = itemHabitos.v_Comment; ;
                    //    onoxioushabitsDto.v_DescriptionHabit = itemHabitos.v_DescriptionHabit; ;
                    //    onoxioushabitsDto.v_DescriptionQuantity = itemHabitos.v_DescriptionQuantity; ;

                    //    _noxioushabitsDto.Add(onoxioushabitsDto);
                    //}


                    //oHistoryBL.AddNoxiousHabits(ref _objOperationResult,
                    //                                        _noxioushabitsDto,
                    //                                        null,
                    //                                        null,
                    //                                        Globals.ClientSession.GetAsList());
                    //#endregion

            //    }
            //}
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            //using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            //{
            //    ProcesoSErvicio();
            //}
        }

        private void btnClonar_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmClonServicio frm = new frmClonServicio(_serviceId);
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void grdDataService_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdDataService.Selected.Rows.Count == 0) return ;
            if (grdDataService.Selected.Rows[0].Cells != null)
            {
                OperationResult objOperationResult = new OperationResult();
                List<string> Filters = new List<string>();

                foreach (UltraGridRow rowSelected in this.grdDataService.Selected.Rows)
                {

                    if (rowSelected.Band.Index.ToString() == "0")
                    {
                            var ServiceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                            ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, ServiceId);

                            if (personData.i_ServiceTypeId == 1)
                            {
                                btnHistoriaCl.Enabled = false;
                            }
                            else
                            {
                                btnHistoriaCl.Enabled = true;
                            }

                    }
                    if (rowSelected.Band.Index.ToString() == "1")
                    {
                        btnEditarESO.Enabled = false;
                        button1.Enabled = false;
                        button1.Enabled = false;
                        btnAdminReportes.Enabled = false;
                        btnReportAsync.Enabled = false;
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
                    button1.Enabled =
                    btnImprimirCertificadoAptitud.Enabled =
                    btnInformeMedicoTrabajador.Enabled =
                    btnImprimirInformeMedicoEPS.Enabled =
                    btnAdminReportes.Enabled =
                    btnReportAsync.Enabled =
                    btnInforme312.Enabled =
                    btnInformeMusculoEsqueletico.Enabled =
                    btnInformeAlturaEstructural.Enabled =
                    btnInformePsicologico.Enabled =
                    btnInformeOftalmo.Enabled =
                    btnGenerarLiquidacion.Enabled =
                    btnInterconsulta.Enabled =
                    btnTiempos.Enabled =


                    //btnFechaEntrega.Enabled =
                        //btnImprimirFichaOcupacional.Enabled = 
                    (grdDataService.Selected.Rows.Count > 0);

                    if (grdDataService.Selected.Rows.Count == 0)
                        return;


                    _serviceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    _EmpresaClienteId = grdDataService.Selected.Rows[0].Cells["v_CustomerOrganizationId"].Value.ToString();
                    _pacientId = grdDataService.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
                    _protocolId = grdDataService.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
                    _customerOrganizationName = grdDataService.Selected.Rows[0].Cells["v_OrganizationName"].Value.ToString();
                    _personFullName = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();

                    if (grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value == null)
                    {
                        btnImprimirExamenes.Enabled = false;
                    }
                    else
                    {
                        btnImprimirExamenes.Enabled = true;
                    }
            }
            
           
        }

        private void grdDataService_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

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

        private void btnEditarESO_Click(object sender, DoubleClickRowEventArgs e)
        {
            Form frm;
            int TserviceId = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceId"].Value.ToString());
            if (TserviceId == (int)MasterService.AtxMedicaParticular)
            {
                #region ESO V1
                //frm = new Operations.frmEso(_serviceId, null, null, TserviceId);
                //frm.ShowDialog();
                #endregion
                #region ESO V2 (Asíncrono)
                frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                frm.ShowDialog();
                #endregion
                
            }
            else
            {
                //Obtener Estado del servicio
                var EstadoServicio = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());

                if (EstadoServicio == (int)ServiceStatus.Culminado)
                {
                    //Obtener el usuario
                    int UserId = Globals.ClientSession.i_SystemUserId;
                    if (UserId == 11 || UserId == 175 || UserId == 173 || UserId == 172 || UserId == 171 || UserId == 168 || UserId == 169)
                    {
                        this.Enabled = false;
                        frm = new Operations.frmEso(_serviceId, null, "Service", TserviceId);
                        frm.ShowDialog();
                        this.Enabled = true;
                    }
                    else
                    {
                        this.Enabled = false;
                        frm = new Operations.frmEso(_serviceId, null, "View", TserviceId);
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


        private void btnHistoriaCl_Click(object sender, EventArgs e)
        {
            OperationResult _objOperationResult = new OperationResult();
            //var doc = grdDataCalendar.Selected.Rows[0].Cells["v_DocDumber"].Value.ToString();
            var serviceID = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                var exams = new ServiceBL().GetServiceComponentsReport(serviceID);

                var datosP = new PacientBL().DevolverDatosPaciente(serviceID);

                var _DataService = new ServiceBL().GetMedicoTratante(serviceID);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaHistoriaClinica").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Historia Clinica N° " + serviceID + " - CSL";

                //var obtenerInformacionEmpresas = new ServiceBL().ObtenerInformacionEmpresas(serviceID);

                Historia_Clinica.CreateHistoria_Clinica(ruta + nombre + ".pdf", MedicalCenter, datosP, _DataService, exams);
                this.Enabled = true;
            }
        }

        private void btnReportAsync_Click(object sender, EventArgs e)
        {
        
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                //var lista = new ServiceBL().GetListaLiquidacionByEmpresa_Name(ref objOperationResult, fechaInicio, fechaFin, _empresa);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "EGRESOS DE PRUEBA - CSL";

                Reporte_Egresos.CreateReporte_Egresos(ruta + nombre + ".pdf", MedicalCenter);
                this.Enabled = true;
            }
        }

        private void ddlServiceTypeId_TextChanged(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedIndex == 0 || ddlServiceTypeId.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(ddlServiceTypeId.SelectedValue.ToString());
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        private void frmService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void grdDataService_DoubleClick(object sender, EventArgs e)
        {
            Form frm;
            int TserviceId = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceId"].Value.ToString());
            if (TserviceId == (int)MasterService.AtxMedicaParticular)
            {
                #region ESO V1
                //frm = new Operations.frmEso(_serviceId, null, null, TserviceId);
                //frm.ShowDialog();
                #endregion
                #region ESO V2 (Asíncrono)
                frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                frm.ShowDialog();
                #endregion

            }
            else
            {
                //Obtener Estado del servicio
                var EstadoServicio = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());

                if (EstadoServicio == (int)ServiceStatus.Culminado)
                {
                    //Obtener el usuario
                    int UserId = Globals.ClientSession.i_SystemUserId;
                    if (UserId == 11 || UserId == 175 || UserId == 173 || UserId == 172 || UserId == 171 || UserId == 168 || UserId == 169)
                    {
                        this.Enabled = false;
                        #region ESO V1
                        //frm = new Operations.frmEso(_serviceId, null, null, TserviceId);
                        //frm.ShowDialog();
                        #endregion
                        #region ESO V2 (Asíncrono)
                        frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                        frm.ShowDialog();
                        #endregion
                        this.Enabled = true;
                    }
                    else
                    {
                        this.Enabled = false;
                        #region ESO V1
                        //frm = new Operations.frmEso(_serviceId, null, null, TserviceId);
                        //frm.ShowDialog();
                        #endregion
                        #region ESO V2 (Asíncrono)
                        frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                        frm.ShowDialog();
                        #endregion
                        this.Enabled = true;
                    }

                }
                else
                {
                    this.Enabled = false;
                    #region ESO V1
                    //frm = new Operations.frmEso(_serviceId, null, null, TserviceId);
                    //frm.ShowDialog();
                    #endregion
                    #region ESO V2 (Asíncrono)
                    frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                    frm.ShowDialog();
                    #endregion
                    this.Enabled = true;
                }


            }

            btnFilter_Click(sender, e);
            
        }
        
        //void ProcesoSErvicio()
        //{
        //    MigracionBL oMigracionBL = new MigracionBL();
        //    PacientBL oPacientBL = new PacientBL();
        //    serviceDto oserviceDto;
        //    ServiceBL oServiceBL = new ServiceBL();
        //    List<ServiceImportacionList> lGrilla = new List<ServiceImportacionList>();
        //    ServiceImportacionList oServiceImportacionList;
        //    var ListaServiciosOLD = oMigracionBL.DevolverListaServiciosOLD();


        //    foreach (var item in ListaServiciosOLD)
        //    {


        //        oserviceDto = new serviceDto();
        //        var ServiceID_OLD = item.v_ServiceId;
        //        var ProtocolId_OLD = item.v_ProtocolId;
        //        var FechaServicio = item.d_ServiceDate.ToString();

        //        var ProtocolID_Nuevo = oMigracionBL.DevolverProtocoloOLD(ProtocolId_OLD, Globals.ClientSession.GetAsList());
        //        if (ProtocolID_Nuevo == null)
        //        {

        //        }
        //        oserviceDto.v_ProtocolId = ProtocolID_Nuevo;

        //        var oPersonNew = oPacientBL.GetPersonByNroDocument(ref _objOperationResult, item.v_DocNumber);
        //        var Paciente = oPersonNew.v_FirstName + " " + oPersonNew.v_FirstLastName + " " + oPersonNew.v_SecondLastName;
        //        oserviceDto.v_PersonId = oPersonNew.v_PersonId;
        //        item.d_EndDateRestriction = item.d_EndDateRestriction;
        //        oserviceDto.d_FechaEntrega = item.d_FechaEntrega;
        //        oserviceDto.d_Fur = item.d_Fur;
        //        oserviceDto.d_GlobalExpirationDate = item.d_GlobalExpirationDate;
        //        oserviceDto.d_InsertDateMedicalAnalyst = item.d_InsertDateMedicalAnalyst;
        //        oserviceDto.d_InsertDateOccupationalMedical = item.d_InsertDateOccupationalMedical;
        //        oserviceDto.d_Mamografia = item.d_Mamografia;
        //        oserviceDto.d_MedicalBreakEndDate = item.d_MedicalBreakEndDate;
        //        oserviceDto.d_MedicalBreakStartDate = item.d_MedicalBreakStartDate;
        //        oserviceDto.d_NextAppointment = item.d_NextAppointment;
        //        oserviceDto.d_ObsExpirationDate = item.d_ObsExpirationDate;
        //        oserviceDto.d_PAP = item.d_PAP;
        //        oserviceDto.d_ServiceDate = item.d_ServiceDate;
        //        oserviceDto.d_StartDateRestriction = item.d_StartDateRestriction;
        //        oserviceDto.d_UpdateDate = item.d_UpdateDate;
        //        oserviceDto.d_UpdateDateMedicalAnalyst = item.d_UpdateDateMedicalAnalyst;
        //        oserviceDto.d_UpdateDateOccupationalMedical = item.d_UpdateDateOccupationalMedical;
        //        oserviceDto.i_AppetiteId = item.i_AppetiteId;
        //        oserviceDto.i_AptitudeStatusId = item.i_AptitudeStatusId;
        //        oserviceDto.i_CursoEnf = item.i_CursoEnf;
        //        oserviceDto.i_DepositionId = item.i_DepositionId;
        //        oserviceDto.i_DestinationMedicationId = item.i_DestinationMedicationId;
        //        oserviceDto.i_DreamId = item.i_DreamId;
        //        oserviceDto.i_Evolucion = item.i_Evolucion;
        //        oserviceDto.i_FlagAgentId = item.i_FlagAgentId;
        //        oserviceDto.i_HasMedicalBreakId = item.i_HasMedicalBreakId;
        //        oserviceDto.i_HasRestrictionId = item.i_HasRestrictionId;
        //        oserviceDto.i_HasSymptomId = item.i_HasSymptomId;
        //        oserviceDto.i_HazInterconsultationId = item.i_HazInterconsultationId;
        //        oserviceDto.i_InicioEnf = item.i_InicioEnf;
        //        oserviceDto.i_InsertUserMedicalAnalystId = item.i_InsertUserMedicalAnalystId;
        //        oserviceDto.i_InsertUserOccupationalMedicalId = item.i_InsertUserOccupationalMedicalId;
        //        oserviceDto.i_IsDeleted = item.i_IsDeleted;
        //        oserviceDto.i_IsFac = item.i_IsFac;
        //        oserviceDto.i_IsNewControl = item.i_IsNewControl;
        //        oserviceDto.i_MacId = item.i_MacId;
        //        oserviceDto.i_MasterServiceId = item.i_MasterServiceId;
        //        oserviceDto.i_ModalityOfInsurance = item.i_ModalityOfInsurance;
        //        oserviceDto.i_SendToTracking = item.i_SendToTracking;
        //        oserviceDto.i_ServiceStatusId = item.i_ServiceStatusId;
        //        oserviceDto.i_ServiceTypeOfInsurance = item.i_ServiceTypeOfInsurance;
        //        oserviceDto.i_StatusLiquidation = item.i_StatusLiquidation;
        //        oserviceDto.i_ThirstId = item.i_ThirstId;
        //        oserviceDto.i_TimeOfDisease = item.i_TimeOfDisease;
        //        oserviceDto.i_TimeOfDiseaseTypeId = item.i_TimeOfDiseaseTypeId;
        //        oserviceDto.i_TransportMedicationId = item.i_TransportMedicationId;
        //        oserviceDto.i_UpdateUserMedicalAnalystId = item.i_UpdateUserMedicalAnalystId;
        //        oserviceDto.i_UpdateUserOccupationalMedicaltId = item.i_UpdateUserOccupationalMedicaltId;
        //        oserviceDto.i_UrineId = item.i_UrineId;
        //        //oserviceDto.r_Costo = item.r_Costo;
        //        oserviceDto.v_AreaId = item.v_AreaId;
        //        oserviceDto.v_CatemenialRegime = item.v_CatemenialRegime;
        //        oserviceDto.v_CiruGine = item.v_CiruGine;
        //        oserviceDto.v_ExaAuxResult = item.v_ExaAuxResult;
        //        oserviceDto.v_FechaUltimaMamo = item.v_FechaUltimaMamo;
        //        oserviceDto.v_FechaUltimoPAP = item.v_FechaUltimoPAP;
        //        oserviceDto.v_Findings = item.v_Findings;
        //        oserviceDto.v_GeneralRecomendations = item.v_GeneralRecomendations;
        //        oserviceDto.v_Gestapara = item.v_Gestapara;
        //        oserviceDto.v_LocationId = item.v_LocationId;
        //        oserviceDto.v_MainSymptom = item.v_MainSymptom;
        //        oserviceDto.v_Menarquia = item.v_Menarquia;
        //        oserviceDto.v_Motive = item.v_Motive;
        //        oserviceDto.v_ObsStatusService = item.v_ObsStatusService;
        //        oserviceDto.v_OrganizationId = item.v_OrganizationId;
        //        oserviceDto.v_ResultadoMamo = item.v_ResultadoMamo;
        //        oserviceDto.v_ResultadosPAP = item.v_ResultadosPAP;
        //        oserviceDto.v_ServiceId = item.v_ServiceId;
        //        oserviceDto.v_Story = item.v_Story;

        //        var ServiceId_New = oServiceBL.AddService(ref _objOperationResult, oserviceDto, Globals.ClientSession.GetAsList());




        //        //obtener Lista de DX por SevicioID_OLD

        //        var ListaDx_OLD = oMigracionBL.DevolverListaDiagnosticOLD(ServiceID_OLD);
        //        List<DiagnosticRepositoryList> oListDiagnosticRepositoryList = new List<DiagnosticRepositoryList>();
        //        DiagnosticRepositoryList oDiagnosticRepositoryList;
        //        foreach (var itemDx in ListaDx_OLD)
        //        {
        //            oDiagnosticRepositoryList = new DiagnosticRepositoryList();

        //            oDiagnosticRepositoryList.v_ServiceId = ServiceId_New;
        //            oDiagnosticRepositoryList.v_DiseasesId = oMigracionBL.ValidarDiseaseSiExiste(itemDx.v_Cie10, itemDx.v_Name, Globals.ClientSession.GetAsList());
        //            oDiagnosticRepositoryList.v_ComponentId = itemDx.v_ComponentId;
        //            oDiagnosticRepositoryList.v_ComponentFieldId = itemDx.v_ComponentFieldId;
        //            oDiagnosticRepositoryList.i_AutoManualId = itemDx.i_AutoManualId == null ? (int?)null : itemDx.i_AutoManualId.Value;
        //            oDiagnosticRepositoryList.i_PreQualificationId = itemDx.i_PreQualificationId == null ? (int?)null : itemDx.i_PreQualificationId.Value;
        //            oDiagnosticRepositoryList.i_FinalQualificationId = itemDx.i_FinalQualificationId == null ? (int?)null : itemDx.i_FinalQualificationId.Value;

        //            oDiagnosticRepositoryList.i_DiagnosticTypeId = itemDx.i_DiagnosticTypeId == null ? (int?)null : itemDx.i_DiagnosticTypeId.Value;
        //            oDiagnosticRepositoryList.i_IsSentToAntecedent = itemDx.i_IsSentToAntecedent == null ? (int?)null : itemDx.i_IsSentToAntecedent.Value;
        //            oDiagnosticRepositoryList.d_ExpirationDateDiagnostic = itemDx.d_ExpirationDateDiagnostic;
        //            oDiagnosticRepositoryList.i_GenerateMedicalBreak = itemDx.i_GenerateMedicalBreak == null ? (int?)null : itemDx.i_GenerateMedicalBreak.Value;
        //            oDiagnosticRepositoryList.v_Recomendations = itemDx.v_Recomendations;

        //            oDiagnosticRepositoryList.i_DiagnosticSourceId = itemDx.i_DiagnosticSourceId == null ? (int?)null : itemDx.i_DiagnosticSourceId.Value;
        //            oDiagnosticRepositoryList.i_ShapeAccidentId = itemDx.i_ShapeAccidentId == null ? (int?)null : itemDx.i_ShapeAccidentId.Value;
        //            oDiagnosticRepositoryList.i_BodyPartId = itemDx.i_BodyPartId == null ? (int?)null : itemDx.i_BodyPartId.Value;
        //            oDiagnosticRepositoryList.i_ClassificationOfWorkAccidentId = itemDx.i_ClassificationOfWorkAccidentId == null ? (int?)null : itemDx.i_ClassificationOfWorkAccidentId.Value;
        //            oDiagnosticRepositoryList.i_RiskFactorId = itemDx.i_RiskFactorId == null ? (int?)null : itemDx.i_RiskFactorId.Value;
        //            oDiagnosticRepositoryList.i_RecordStatus = (int)RecordStatus.Agregado;
        //            oDiagnosticRepositoryList.i_RecordType = (int)RecordType.Temporal;
        //            oDiagnosticRepositoryList.i_ClassificationOfWorkdiseaseId = itemDx.i_ClassificationOfWorkdiseaseId == null ? (int?)null : itemDx.i_ClassificationOfWorkdiseaseId.Value;
        //            oDiagnosticRepositoryList.i_SendToInterconsultationId = itemDx.i_SendToInterconsultationId == null ? (int?)null : itemDx.i_SendToInterconsultationId.Value;
        //            oDiagnosticRepositoryList.i_InterconsultationDestinationId = itemDx.i_InterconsultationDestinationId == null ? (int?)null : itemDx.i_InterconsultationDestinationId.Value;
        //            oDiagnosticRepositoryList.v_InterconsultationDestinationId = itemDx.v_InterconsultationDestinationId;

        //            oListDiagnosticRepositoryList.Add(oDiagnosticRepositoryList);
        //        }

        //        oServiceBL.AddDiagnosticRepository(ref _objOperationResult,
        //                                                   oListDiagnosticRepositoryList,
        //                                                   null,
        //                                                   Globals.ClientSession.GetAsList(),
        //                                                   null);

        //        //Obtener la Lista de Calendar por SevicioID_OLD
        //        var ListaCalendarOLD = oMigracionBL.DevolverListaCalendarOLD(ServiceID_OLD);
        //        calendarDto ocalendarDto;
        //        CalendarBL oCalendarBL = new CalendarBL();

        //        foreach (var itemCalendar in ListaCalendarOLD)
        //        {
        //            ocalendarDto = new calendarDto();
        //            ocalendarDto.v_PersonId = oPersonNew.v_PersonId;
        //            ocalendarDto.v_ProtocolId = ProtocolID_Nuevo;
        //            ocalendarDto.v_ServiceId = ServiceId_New;
        //            ocalendarDto.d_CircuitStartDate = itemCalendar.d_CircuitStartDate;
        //            ocalendarDto.d_DateTimeCalendar = itemCalendar.d_DateTimeCalendar;
        //            ocalendarDto.d_EntryTimeCM = itemCalendar.d_EntryTimeCM;
        //            ocalendarDto.d_SalidaCM = itemCalendar.d_SalidaCM;
        //            ocalendarDto.i_CalendarStatusId = itemCalendar.i_CalendarStatusId;
        //            ocalendarDto.i_IsVipId = itemCalendar.i_IsVipId;
        //            ocalendarDto.i_LineStatusId = itemCalendar.i_LineStatusId;
        //            ocalendarDto.i_NewContinuationId = itemCalendar.i_NewContinuationId;
        //            ocalendarDto.i_ServiceId = itemCalendar.i_ServiceId;
        //            ocalendarDto.i_ServiceTypeId = itemCalendar.i_ServiceTypeId;

        //            oCalendarBL.AddCalendar(ref _objOperationResult, ocalendarDto, Globals.ClientSession.GetAsList());
        //        }

        //        ////Obtener ServiceComponent con ServiceID_OLD

        //        //var ListaServiceComponenteOLD = oMigracionBL.GetServiceComponents_OLD(ServiceID_OLD);
        //        //var ServiceComponentId_NEW = "";
        //        //servicecomponentDto oservicecomponentDto;
        //        //foreach (var itemSC in ListaServiceComponenteOLD)
        //        //{
        //        //    oservicecomponentDto = new servicecomponentDto();
        //        //    var ServiceComponentId_OLD = itemSC.v_ServiceComponentId;
        //        //    oservicecomponentDto.v_ComponentId = itemSC.v_ComponentId;
        //        //    oservicecomponentDto.i_ServiceComponentStatusId = itemSC.i_ServiceComponentStatusId;
        //        //    oservicecomponentDto.d_StartDate = itemSC.d_StartDate == null ? (DateTime?)null : itemSC.d_StartDate.Value;
        //        //    oservicecomponentDto.d_EndDate = itemSC.d_EndDate == null ? (DateTime?)null : itemSC.d_EndDate.Value;
        //        //    oservicecomponentDto.i_QueueStatusId = itemSC.i_QueueStatusId;
        //        //    oservicecomponentDto.v_ServiceComponentId = itemSC.v_ServiceComponentId;
        //        //    oservicecomponentDto.d_ApprovedInsertDate = itemSC.d_ApprovedInsertDate;
        //        //    oservicecomponentDto.d_ApprovedUpdateDate = itemSC.d_ApprovedUpdateDate;
        //        //    oservicecomponentDto.d_CalledDate = itemSC.d_CalledDate;
        //        //    oservicecomponentDto.d_InsertDateMedicalAnalyst = itemSC.d_InsertDateMedicalAnalyst;
        //        //    oservicecomponentDto.d_InsertDateTechnicalDataRegister = itemSC.d_InsertDateTechnicalDataRegister;
        //        //    oservicecomponentDto.d_UpdateDateMedicalAnalyst = itemSC.d_UpdateDateMedicalAnalyst;
        //        //    oservicecomponentDto.d_UpdateDateTechnicalDataRegister = itemSC.d_UpdateDateTechnicalDataRegister;
        //        //    oservicecomponentDto.i_ApprovedInsertUserId = itemSC.i_ApprovedInsertUserId;
        //        //    oservicecomponentDto.i_ApprovedUpdateUserId = itemSC.i_ApprovedUpdateUserId;
        //        //    oservicecomponentDto.i_ExternalInternalId = itemSC.i_ExternalInternalId;
        //        //    oservicecomponentDto.i_index = itemSC.i_index;
        //        //    oservicecomponentDto.i_InsertUserMedicalAnalystId = itemSC.i_InsertUserMedicalAnalystId;
        //        //    oservicecomponentDto.i_InsertUserTechnicalDataRegisterId = itemSC.i_InsertUserTechnicalDataRegisterId;
        //        //    oservicecomponentDto.i_IsApprovedId = itemSC.i_IsApprovedId;
        //        //    oservicecomponentDto.i_Iscalling = itemSC.i_Iscalling;
        //        //    oservicecomponentDto.i_Iscalling_1 = itemSC.i_Iscalling_1;
        //        //    oservicecomponentDto.i_IsInheritedId = itemSC.i_IsInheritedId;
        //        //    oservicecomponentDto.i_IsInvoicedId = itemSC.i_IsInvoicedId;
        //        //    oservicecomponentDto.i_IsManuallyAddedId = itemSC.i_IsManuallyAddedId;
        //        //    oservicecomponentDto.i_IsRequiredId = itemSC.i_IsRequiredId;
        //        //    oservicecomponentDto.i_IsVisibleId = itemSC.i_IsVisibleId;
        //        //    oservicecomponentDto.i_ServiceComponentTypeId = itemSC.i_ServiceComponentTypeId;
        //        //    oservicecomponentDto.i_UpdateUserMedicalAnalystId = itemSC.i_UpdateUserMedicalAnalystId;
        //        //    oservicecomponentDto.i_UpdateUserTechnicalDataRegisterId = itemSC.i_UpdateUserTechnicalDataRegisterId;
        //        //    oservicecomponentDto.r_Price = itemSC.r_Price;
        //        //    oservicecomponentDto.v_Comment = itemSC.v_Comment;
        //        //    oservicecomponentDto.v_NameOfice = itemSC.v_NameOfice;
        //        //    oservicecomponentDto.v_ServiceId = ServiceId_New;

        //        //    ServiceComponentId_NEW = oMigracionBL.AddServiceComponent(ref _objOperationResult, oservicecomponentDto, Globals.ClientSession.GetAsList());


        //        //    //Obtener Lista de ServiceComponentFields Antiguo 
        //        //    var ListaServiceComponentFields = oMigracionBL.GetServiceComponentFields_Y_Values_OLD(ServiceComponentId_OLD, ServiceComponentId_NEW, Globals.ClientSession.GetAsList());

        //        //    //servicecomponentfieldsDto oservicecomponentfieldsDto;
        //        //    //  foreach (var itemFileds in ListaServiceComponentFields)
        //        //    //{
        //        //    //    var Fileds_OLD = itemFileds.v_ServiceComponentFieldsId;

        //        //    //    oservicecomponentfieldsDto = new servicecomponentfieldsDto();
        //        //    //    oservicecomponentfieldsDto.v_ServiceComponentId = ServiceComponentId_NEW;
        //        //    //    oservicecomponentfieldsDto.v_ComponentId = itemFileds.v_ComponentId;
        //        //    //    oservicecomponentfieldsDto.v_ComponentFieldId = itemFileds.v_ComponentFieldId;

        //        //    //    //var Fileds_NEW = oMigracionBL.AddServiceComponentField(oservicecomponentfieldsDto,Globals.ClientSession.GetAsList());


        //        //    //    //obtener valores antiguos
        //        //    //    var ListaValores_OLD = oMigracionBL.GetServiceComponentFieldsValues_OLD(Fileds_OLD);

        //        //    //    servicecomponentfieldvaluesDto oservicecomponentfieldvaluesDto;
        //        //    //    List<servicecomponentfieldvaluesDto> lservicecomponentfieldvaluesDto = new List<servicecomponentfieldvaluesDto>();
        //        //    //    foreach (var itemValores in ListaValores_OLD)
        //        //    //    {

        //        //    //        oservicecomponentfieldvaluesDto = new servicecomponentfieldvaluesDto();

        //        //    //        oservicecomponentfieldvaluesDto.v_ComponentFieldValuesId = itemValores.v_ComponentFieldValuesId;
        //        //    //        //oservicecomponentfieldvaluesDto.v_ServiceComponentFieldsId = Fileds_NEW;
        //        //    //        oservicecomponentfieldvaluesDto.v_Value1 = itemValores.v_Value1;
        //        //    //        oservicecomponentfieldvaluesDto.v_Value2 = itemValores.v_Value2;
        //        //    //        oservicecomponentfieldvaluesDto.i_Index = itemValores.i_Index;
        //        //    //        oservicecomponentfieldvaluesDto.i_Value1 = itemValores.i_Value1;
        //        //    //        lservicecomponentfieldvaluesDto.Add(oservicecomponentfieldvaluesDto);
        //        //    //    }

        //        //    //    //oMigracionBL.AddServiceComponentFieldValues(lservicecomponentfieldvaluesDto, Globals.ClientSession.GetAsList());


        //        //    //}
        //        //}
        //        //oServiceImportacionList = new ServiceImportacionList();
        //        //oServiceImportacionList.v_ServiceId = ServiceId_New;
        //        //oServiceImportacionList.v_FechaService = FechaServicio;
        //        //oServiceImportacionList.v_Paciente = Paciente;


        //        //lGrilla.Add(oServiceImportacionList);

        //        //grdDataService.DataSource = lGrilla;
        //        //lblContadoServicio.Text = lGrilla.Count().ToString();
        //    }

        //}


    }
}
