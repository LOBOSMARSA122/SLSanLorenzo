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
using System.Linq.Dynamic;
using System.Threading;
using System.Windows.Shell;
using Infragistics.Win.UltraWinDataSource;
using Sigesoft.Node.WinClient.UI.Reports;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.draw;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmService : Form
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
        private OrganizationBL _organizationBL = new OrganizationBL() ;
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip _customizedToolTip = null;
        private SaveFileDialog saveFileDialog2 = new SaveFileDialog();
        private BindingList<ServiceGridJerarquizadaList> ListaGrilla = new BindingList<ServiceGridJerarquizadaList>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        private bool mergeRow = false;
        private List<string> _ComponentsIdsOrdenados = new List<string>();
        public frmService()
        {
            InitializeComponent();
            //grdDataService.DataSource = new BindingList<ServiceGridJerarquizadaList>();

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

            //UltraGridColumn c = grdDataService.DisplayLayout.Bands[0].Columns["b_FechaEntrega"];
            //c.CellActivation = Activation.AllowEdit;
            //c.CellClickAction = CellClickAction.Edit;

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


            //var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

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

            grdDataService.DisplayLayout.Bands[0].Columns["v_PersonId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["b_FechaEntrega"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ServiceStatusName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceStatusId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_LocationName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ProtocolId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_ComponentId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_LineStatusId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_DiagnosticRepositoryId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_DiseasesName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_ExpirationDateDiagnostic"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["v_Recommendation"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ServiceTypeId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_CustomerOrganizationId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_CustomerLocationId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_MasterServiceId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_AptitudeStatusId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_EsoTypeId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeleted"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_CreationUser"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["v_UpdateUser"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_CreationDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_UpdateDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_StatusLiquidation"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_MasterServiceName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_EsoTypeName"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["CIE10"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_FechaNacimiento"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["NroPoliza"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_FechaEntrega"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["Moneda"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["NroFactura"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["Valor"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_FinalQualificationId"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["v_Restriccion"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_Deducible"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeletedDx"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["LogoEmpresaPropietaria"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeletedRecomendaciones"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_IsDeletedRestricciones"].Hidden = true;

            grdDataService.DisplayLayout.Bands[0].Columns["i_age"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["d_BirthDate"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["UsuarioMedicina"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["item"].Hidden = true;
            grdDataService.DisplayLayout.Bands[0].Columns["i_ApprovedUpdateUserId"].Hidden = true;

            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            //if (grdDataService.Rows.Count > 0)
            //{
            //    grdDataService.Rows[0].Selected = true;
            //    btnExport.Enabled = true;
            //}

        }

        private BindingList<ServiceGridJerarquizadaList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
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

            var _objData = _serviceBL.GetServicesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, _componentIds, FCI, FCF, txtDiagnostico.Text);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return _objData;
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
            try
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
                        int UserId = Globals.ClientSession.i_SystemUserId;
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
                        _pacientId = grdDataService.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
                        frm = new Operations.frmContainerEso(_serviceId, null, "Service", (int)MasterService.Eso, _pacientId);
                        //frm = new Operations.frmEso(_serviceId, null, "Service", (int)MasterService.Eso);
                        frm.ShowDialog();
                        this.Enabled = true;
                    }


                }

                btnFilter_Click(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("SELECCIONE UNA SERVICIO A MODIFICAR", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFilter_Click(sender, e);
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
                                  Audiometria, // Psicologia, RX, RX1, , Espirometria,
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("SELECCIONE UNA SERVICIO A GENERAR", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFilter_Click(sender, e);
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

            //foreach (UltraGridRow rowSelected in this.grdDataService.Rows)
            //{
                var banda = e.Row.Band.Index.ToString();
                var row = e.Row;
                if (banda == "0")
                {
                    if (row.Band.Index.ToString() == "0")
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
                            if (e.Row.Cells["v_AptitudeStatusName"].Value.ToString() == "OBSERVADO")
                            {
                                e.Row.Appearance.BackColor = Color.OrangeRed;
                                e.Row.Appearance.BackColor2 = Color.White;
                                //Y doy el efecto degradado vertical
                                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                            }
                            else
                            {
                                e.Row.Appearance.BackColor = Color.GreenYellow;
                                e.Row.Appearance.BackColor2 = Color.White;
                                //Y doy el efecto degradado vertical
                                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                            }
                            
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

            //}

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

                //if ((bool)item.Cells["b_FechaEntrega"].Value)
                //{
                //    string x = item.Cells["v_ServiceId"].Value.ToString();
                //    _ListaServicios.Add(x);
                //}
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
            //if ((e.Cell.Column.Key == "b_FechaEntrega"))
            //{
            //    if ((e.Cell.Value.ToString() == "False"))
            //    {
            //        e.Cell.Value = true;

            //        //btnFechaEntrega.Enabled = true;
            //        //btnAdjuntarArchivo.Enabled = true;
            //    }
            //    else
            //    {
            //        e.Cell.Value = false;
            //        //btnFechaEntrega.Enabled = false;
            //        //btnAdjuntarArchivo.Enabled = false;
            //    }

            //}
        }

        private void btnAdjuntarArchivo_Click(object sender, EventArgs e)
        {
            _ListaServiciosAdjuntar = new List<string>();
            foreach (var item in grdDataService.Rows)
            {
                //CheckBox ck = (CheckBox)item.Cells["b_FechaEntrega"].Value;

                //if ((bool)item.Cells["b_FechaEntrega"].Value)
                //{
                //    string x = item.Cells["v_ServiceId"].Value.ToString();
                //    _ListaServiciosAdjuntar.Add(x);
                //}
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

                     var serviceComponents_ = _serviceBL.GetServiceComponentsForManagementReport(item_0.ServiceId);
                     var empresaClienteId = item_0.EmpresaCliente;
                       var ordenReportes = _organizationBL.GetOrdenReportes_(ref _objOperationResult, empresaClienteId);
                       var componentIds = ordenReportes.Select(p => p.v_ComponentId).ToList();
                       var reportsCrystal = ordenReportes.FindAll(p => p.v_ComponentId.Contains("N00"));
                       var reportsPdf = ordenReportes.Except(reportsCrystal).ToList();


                       serviceComponents_ = serviceComponents_.FindAll(p => componentIds.Contains(p.v_ComponentId)).ToList();
                       var list = serviceComponents.Union(reportsPdf).ToList();

                       var ListOrdenada = new List<ServiceComponentList>();

                       //var serviceComponenteEstado = _serviceBL.GetServiceComponentsReport(item_0);
                       var listCompExec = new List<string>();
                       listCompExec.Add(Sigesoft.Common.Constants.EXCEPCIONES_RX_AUTORIZACION_ID);
                       listCompExec.Add(Sigesoft.Common.Constants.EXCEPCIONES_RX_ID);
                       listCompExec.Add(Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_ID);
                       listCompExec.Add(Sigesoft.Common.Constants.EXCEPCIONES_ESPIROMETRIA_ID);

                       var serviceComponenteEstado = _serviceBL.ValoresComponente_ManagerReport(item_0.ServiceId, listCompExec);

                       //var serviceComponenteEstado = _serviceBL.ValoresComponente_ManagerReport(string.Empty, listCompExec);

                       foreach (var item in ordenReportes)
                       {
                           var obj = new ServiceComponentList();
                           var exist = list.Find(p => p.v_ComponentId == item.v_ComponentId);

                           if (exist != null)
                           {
                               obj.v_ComponentId = item.v_ComponentId + "|" + item.i_NombreCrystalId;
                               obj.v_ComponentName = item.v_ComponentName;
                               obj.i_Orden = item.i_Orden;

                               ListOrdenada.Add(obj);
                           }
                       }

                       #region RX
                       var rx = serviceComponenteEstado.FindAll(p => p.i_CategoryId == 6 && p.i_ServiceComponentStatusId == 7);
                       if (rx.Count != 0)
                       {
                           if (rx[0].i_GenderId == (int)Sigesoft.Common.Gender.FEMENINO)
                           {
                               ///
                               var mujeres_AUTORIZACION_Si = serviceComponenteEstado.Find(p =>
                                    p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_RX_AUTORIZACION_SI);


                               var mujeres_AUTORIZACION_No = serviceComponenteEstado.Find(p =>
                                   p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_RX_AUTORIZACION_NO);

                               var si_AUTORIZACION = mujeres_AUTORIZACION_Si == null ? "" : mujeres_AUTORIZACION_Si.v_Value1;
                               var no_AUTORIZACION = mujeres_AUTORIZACION_No == null ? "" : mujeres_AUTORIZACION_No.v_Value1;

                               ////
                               var mujeres_EXONERACION_Si = serviceComponenteEstado.Find(p =>
                                   p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_SI);


                               var mujeres_EXONERACION_No = serviceComponenteEstado.Find(p =>
                                   p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_NO);

                               var si_EXONERACION = mujeres_EXONERACION_Si == null ? "" : mujeres_EXONERACION_Si.v_Value1;
                               var no_EXONERACION = mujeres_EXONERACION_No == null ? "" : mujeres_EXONERACION_No.v_Value1;

                               if (no_AUTORIZACION == "1")
                               {
                                   if (si_EXONERACION == "1")
                                   {
                                       ListOrdenada = ListOrdenada.FindAll(
                                           p =>
                                               p.v_ComponentId.Split('|')[0] != "N002-ME000000032" &&
                                               p.v_ComponentId.Split('|')[0] != "N009-ME000000302" &&
                                               p.v_ComponentId.Split('|')[0] != "N009-ME000000062" &&
                                               p.v_ComponentId.Split('|')[0] != "N009-ME000000130");
                                   }
                                   else
                                   {
                                       ListOrdenada = ListOrdenada.FindAll(
                                           p =>
                                               p.v_ComponentId.Split('|')[0] != "N009-ME000000442" && p.v_ComponentId.Split('|')[0] != "N009-ME000000440");
                                   }
                               }
                               else if (si_AUTORIZACION == "1")
                               {
                                   ListOrdenada = ListOrdenada.FindAll(
                                       p =>
                                           p.v_ComponentId.Split('|')[0] != "N009-ME000000442" && p.v_ComponentId.Split('|')[0] != "N009-ME000000440");
                               }
                           }
                           else
                           {
                               var exoneracionsi = serviceComponenteEstado.Find(p =>
                               p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_SI);

                               var exoneracionno = serviceComponenteEstado.Find(p =>
                               p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_NO);

                               //var exoneracionsi = serviceComponenteEstado.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_SI);
                               //var exoneracionno = serviceComponenteEstado.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_NO);

                               var si = exoneracionsi == null ? "" : exoneracionsi.v_Value1; //exoneracion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_SI) == null ? "" : exoneracion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_SI).v_Value1;
                               var no = exoneracionno == null ? "" : exoneracionno.v_Value1; //exoneracion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_NO) == null ? "" : exoneracion.ServiceComponentFields.Find(p => p.v_ComponentFieldsId == Sigesoft.Common.Constants.EXCEPCIONES_RX_EXO_NO).v_Value1;
                               if (si == "1")
                               {
                                   ListOrdenada = ListOrdenada.FindAll(
                                p =>
                                    p.v_ComponentId.Split('|')[0] != "N002-ME000000032" && p.v_ComponentId.Split('|')[0] != "N009-ME000000062" &&
                                    p.v_ComponentId.Split('|')[0] != "N009-ME000000130" && p.v_ComponentId.Split('|')[0] != "N009-ME000000302"
                                    && p.v_ComponentId != "N009-ME000000442");
                               }
                               else
                               {
                                   ListOrdenada = ListOrdenada.FindAll(
                                       p =>
                                           p.v_ComponentId.Split('|')[0] != "N009-ME000000440" && p.v_ComponentId.Split('|')[0] != "N009-ME000000442");
                               }
                           }

                       }
                       else
                       {
                           ListOrdenada = ListOrdenada.FindAll(
                              p =>
                                  p.v_ComponentId.Split('|')[0] != "N009-ME000000440" && p.v_ComponentId.Split('|')[0] != "N009-ME000000442");
                       }



                       #endregion

                       #region Lab
                       var lab = serviceComponenteEstado.FindAll(p => p.i_CategoryId == 1 && p.i_ServiceComponentStatusId == 7);
                       if (lab.Count != 0)
                       {
                           ///
                           var laboratorio_si = serviceComponenteEstado.Find(p =>
                               p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_EXO_SI);


                           var laboratorio_no = serviceComponenteEstado.Find(p =>
                               p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_EXO_NO);

                           var si_lab = laboratorio_si == null ? "" : laboratorio_si.v_Value1;
                           var no_lab = laboratorio_no == null ? "" : laboratorio_no.v_Value1;

                           //var si_lab = serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_EXO_SI) == null ? "" : serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_EXO_SI).v_Value1;
                           //var no_lab = serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_EXO_NO) == null ? "" : serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_LABORATORIO_EXO_NO).v_Value1;

                           if (si_lab == "1")
                           {
                               ListOrdenada = ListOrdenada.FindAll(
                                   p =>
                                       p.v_ComponentId.Split('|')[0] != "N001-ME000000000" && p.v_ComponentId.Split('|')[0] != "N009-ME000000461" &&
                                       p.v_ComponentId.Split('|')[0] != "N009-ME000000053" && p.v_ComponentId.Split('|')[0] != "ILAB_CLINICO");
                           }
                           else
                           {
                               ListOrdenada = ListOrdenada.FindAll(
                                   p =>
                                       p.v_ComponentId.Split('|')[0] != "N009-ME000000441");
                           }
                       }
                       else
                       {
                           ListOrdenada = ListOrdenada.FindAll(
                               p =>
                                   p.v_ComponentId.Split('|')[0] != "N009-ME000000441");
                       }


                       #endregion

                       #region Espiro
                       var espiro = serviceComponenteEstado.FindAll(p => p.i_CategoryId == 16 && p.i_ServiceComponentStatusId == 7);

                       if (espiro.Count != 0)
                       {

                           var si_esp = serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_ESPIROMETRIA_SI) == null ? "" : serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_ESPIROMETRIA_SI).v_Value1;
                           var no_esp = serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_ESPIROMETRIA_NO) == null ? "" : serviceComponenteEstado.Find(p => p.v_ComponentFieldId == Sigesoft.Common.Constants.EXCEPCIONES_ESPIROMETRIA_NO).v_Value1;

                           if (si_esp == "1")
                           {
                               ListOrdenada = ListOrdenada.FindAll(
                                   p =>
                                       p.v_ComponentId.Split('|')[0] != "N002-ME000000031" && p.v_ComponentId.Split('|')[0] != "INFORME_ESPIRO" && p.v_ComponentId.Split('|')[0] != "N009-ME000000516");
                           }
                           else
                           {
                               ListOrdenada = ListOrdenada.FindAll(
                                   p =>
                                       p.v_ComponentId.Split('|')[0] != "N009-ME000000513");
                           }
                       }
                       else
                       {
                           ListOrdenada = ListOrdenada.FindAll(
                               p =>
                                   p.v_ComponentId.Split('|')[0] != "N009-ME000000513");
                       }


                       #endregion

                       _ComponentsIdsOrdenados = ListOrdenada.Select(p => p.v_ComponentId).ToList();


                       #region ....
                       OperationResult objOperationResult = new OperationResult();

                       string _ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                       string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
                       string rutaConsolidado = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();
                       var filesNameToMergeOrder = new List<string>();
                       using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                       {
                           frmManagementReports_Async frm = new frmManagementReports_Async("","","","","","");

                           System.Threading.Tasks.Task.Factory.StartNew(() => frm.CrearReportesCrystal(item_0.ServiceId, item_0.PacienteId, _ComponentsIdsOrdenados, null, true)).Wait();

                           foreach (var item in _ComponentsIdsOrdenados)
                           {
                               var componentId = item.Split('|')[0];
                               var path = _ruta + item_0.ServiceId + "-" + componentId + ".pdf";
                               if (frm._filesNameToMerge.Find(p => p == path) != null)
                               {
                                   filesNameToMergeOrder.Add(path);
                               }
                           }
                       };

                       //using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                       //{
                       //    CrearReportesCrystal(item_0.ServiceId, _pacientId, Reportes, _listaDosaje, Result == System.Windows.Forms.DialogResult.Yes ? true : false);
                       //};

                       var x = filesNameToMergeOrder.ToList();
                       //var x = _filesNameToMerge.ToList();
                       _mergeExPDF.FilesName = x;
                       _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + item_0.ServiceId + ".pdf";
                       _mergeExPDF.DestinationFile = _ruta + item_0.ServiceId + ".pdf";
                       _mergeExPDF.Execute();
                       //_mergeExPDF.RunFile();

                       var oService = _serviceBL.GetServiceShort(item_0.ServiceId);
                       _mergeExPDF.FilesName = x;
                       _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";
                       if (oService.Empresa != oService.Contract)
                       {
                           _mergeExPDF.DestinationFile = rutaConsolidado + oService.Empresa + " - Contrata (" + oService.Contract + ") - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";
                           _mergeExPDF.Execute();
                       }
                       else if (oService.Empresa == oService.Contract)
                       {
                           _mergeExPDF.DestinationFile = rutaConsolidado + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";
                           _mergeExPDF.Execute();
                       }

                       //Cambiar de estado a generado de reportes
                       _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult, 2, item_0.ServiceId, Globals.ClientSession.GetAsList());


                       #endregion

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

            if (_serviceId == null)
            {
                return;
            }
            // if we are not entering a cell, then don't anything
            else
            {
                if (!(e.Element is CellUIElement))
                {
                    return;
                }

                // find the cell that the cursor is over, if any
                UltraGridCell cell = e.Element.GetContext(typeof(UltraGridCell)) as UltraGridCell;
                if(cell.Band.ToString() == "Diagnosticos")return;
                
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
                    if (FirmaMedicoMedicina != null)
                    {
                        Cadena.Append("\n");
                        Cadena.Append("MÉDICO");
                        Cadena.Append("\n");
                        Cadena.Append(FirmaMedicoMedicina.Value2);
                    }

                    _customizedToolTip.AutomaticDelay = 1;
                    _customizedToolTip.AutoPopDelay = 20000;
                    _customizedToolTip.ToolTipMessage = Cadena.ToString();
                    _customizedToolTip.StopTimerToolTip();
                    _customizedToolTip.StartTimerToolTip();
                    //}

                }
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

          //AMC
            //var frm = new Reports.frmCargoHistoria(ListaGrilla);
            //frm.ShowDialog();
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
                            if (personData != null)
                            {
                                if (personData.i_ServiceTypeId == 1)
                                {
                                    btnHistoriaCl.Enabled = false;
                                }
                                else
                                {
                                    btnHistoriaCl.Enabled = true;
                                }
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

            try
            {
                var StatusLiquidation = grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value == null
                    ? 1
                    : int.Parse(grdDataService.Selected.Rows[0].Cells["i_StatusLiquidation"].Value.ToString());

                if (StatusLiquidation == 2)
                {
                    var DialogResult =
                        MessageBox.Show("Este servicio ya tiene, reportes generados, ¿Desea volver a generar?",
                            "INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult == DialogResult.No)
                    {
                        string ruta = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();
                        System.Diagnostics.Process.Start(ruta);
                        Clipboard.SetText(grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString());

                        return;
                    }

                }

                int flagPantalla =
                    int.Parse(grdDataService.Selected.Rows[0].Cells["i_MasterServiceId"].Value
                        .ToString()); // int.Parse(ddlServiceTypeId.SelectedValue.ToString());
                int eso = 1;
                if (flagPantalla == 2)
                {
                    var dni = grdDataService.Selected.Rows[0].Cells["v_DocNumber"].Value.ToString();
                    var pacientName = grdDataService.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
                    var frm = new Reports.frmManagementReports_Async(_serviceId, _EmpresaClienteId, _pacientId,
                        _customerOrganizationName, dni, pacientName);
                    frm.ShowDialog();
                }
                else
                {
                    var edad = int.Parse(grdDataService.Selected.Rows[0].Cells["i_age"].Value.ToString());
                    var frm = new Reports.frmManagementReportsMedical(_serviceId, _pacientId, _customerOrganizationName,
                        _personFullName, _EmpresaClienteId, edad);
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show( "","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFilter_Click(sender, e);
            }

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
        
        private static void InitializeRow(UltraGridRow row)
        {
            row.Cells["A"].Value = "AA";
            row.Cells["B"].Value = "BB";
            row.Cells["C"].Value = "CC";
        }
        
        private void SetFocus(string field)
        {
            var lastRow = grdDataService.Rows.LastOrDefault();
            if (lastRow == null) return;
            grdDataService.Focus();
            grdDataService.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
            grdDataService.ActiveCell = lastRow.Cells[field];
            grdDataService.PerformAction(UltraGridAction.EnterEditMode, false, false);
        }

        private void grdPrueba_AfterRowExpanded(object sender, RowEventArgs e)
        {
            if (mergeRow == false)
            {
                foreach (var ultraGridRow in grdDataService.Selected.Rows)
                {
                    ultraGridRow.Selected = false;
                }
                e.Row.Selected = true;
                e.Row.Activated = true;
                DeleteAllChildRows(e.Row);

                var serviceId = e.Row.Cells["v_ServiceId"].Value.ToString();
                var list = new DxFrecuenteBL().getDataService(serviceId);

                foreach (var item in list)
                {
                    if (grdDataService.ActiveRow != null)
                    {
                        var row = grdDataService.DisplayLayout.Bands[1].AddNew();
                        InitializeRowDetail(row, item);
                    }
                    else
                    {
                        //e.Row.Cells["Diagnosticos"].Value = new BindingList<DiagnosticRepositoryJerarquizada>(); 
                        //var row = grdPrueba.DisplayLayout.Bands[1].AddNew();
                        //if (row == null) return;
                        //InitializeRowDetail(row, item);
                    }
                }
            }
            
        }



        private static void InitializeRowDetail(UltraGridRow row, DiagnosticRepositoryJerarquizada item)
        {
            row.Cells["v_DiseasesName"].Value = item.v_DiseasesName;
            row.Cells["v_RecomendationsName"].Value = item.v_RecomendationsName;
            row.Cells["v_RestricctionName"].Value = item.v_RestricctionName;
        }

        private void DeleteAllChildRows(UltraGridRow parentRow)
        {
            foreach (UltraGridChildBand childBand in parentRow.ChildBands)
            {
                UltraGridRow[] rows = (UltraGridRow[])childBand.Rows.All;
                foreach (UltraGridRow row in rows)
                {
                    row.Delete();
                    //row.Cells["v_DiseasesName"].Value = "";
                    //row.Cells["v_RecomendationsName"].Value = "";
                    //row.Cells["v_RestricctionName"].Value = "";
                }
            }
        }



        private void grdDataService_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.DisplayPromptMsg = false;
        }

        private void grdDataService_InitializeGroupByRow(object sender, InitializeGroupByRowEventArgs e)
        {
            mergeRow = true;
        }

        private void grdDataService_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                Form frm;
                int TserviceId = int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceId"].Value.ToString());
                if (TserviceId == (int)MasterService.AtxMedicaParticular || TserviceId == (int)MasterService.AtxMedicaSeguros)
                {
                    #region ESO V2 (Asíncrono)
                    frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                    frm.ShowDialog();
                    #endregion
                }
                else
                {
                    //Obtener Estado del servicio
                    var estadoAptitud = int.Parse(grdDataService.Selected.Rows[0].Cells["i_AptitudeStatusId"].Value.ToString());

                    if (estadoAptitud != (int)AptitudeStatus.SinAptitud || estadoAptitud == (int)AptitudeStatus.AptoObs)
                    {
                        //Obtener el usuario
                        int UserId = Globals.ClientSession.i_SystemUserId;
                        if (UserId == 11 || UserId == 175 || UserId == 173 || UserId == 172 || UserId == 171 || UserId == 168 || UserId == 169)
                        {
                            this.Enabled = false;
                            var t = new Thread(() =>
                            {
                                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                                {
                                    Thread.Sleep(2500);
                                };
                                ;
                            });
                            t.Start();
                            frm = new Operations.frmContainerEso(_serviceId, "TRIAJE", "Service", TserviceId, _pacientId);
                            //frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                            
                            frm.ShowDialog();
                            this.Enabled = true;
                        }
                        else
                        {
                            this.Enabled = false;
                            frm = new Operations.frmContainerEso(_serviceId, "TRIAJE", "Service", TserviceId, _pacientId);
                            //frm = new Operations.FrmEsoV2(_serviceId, "TRIAJE", "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                            frm.ShowDialog();
                            this.Enabled = true;
                        }

                    }
                    else
                    {
                        this.Enabled = false;
                        frm = new Operations.frmContainerEso(_serviceId, null, "Service",TserviceId, _pacientId);
                        //frm = new Operations.FrmEsoV2(_serviceId, null, "Service", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, TserviceId);
                        frm.ShowDialog();
                        this.Enabled = true;
                    }


                }

                btnFilter_Click(sender, e);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFilter_Click(sender, e);
            }
        }

        private void btnCompaginaSelected_Click(object sender, EventArgs e)
        {
            try
            {

                List<string> PersonasNoGeneradas = new List<string>();
                string rutaInterconsulta = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();

                using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
                {
                    if (grdDataService.Selected.Rows.Count == 0)
                    {
                        return;
                    }

                    string _ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                    List<ServiceComponentList> _listaDosaje = new List<ServiceComponentList>();
                    OperationResult _objOperationResult = new OperationResult();
                    foreach (var row in grdDataService.Selected.Rows)
                    {
                        var ServiceStatusId = row.Cells["i_ServiceStatusId"].Value == null ? 0 : int.Parse(grdDataService.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());
                        var EmpresaClienteId = row.Cells["v_CustomerOrganizationId"].Value.ToString();
                        var ServiceId = row.Cells["v_ServiceId"].Value.ToString();
                        var PacientId = row.Cells["v_PersonId"].Value.ToString();
                        var _pacientName = row.Cells["v_Pacient"].Value.ToString();
                        if (ServiceStatusId == (int)ServiceStatus.Culminado)
                        {
                            frmManagementReports_Async frm = new frmManagementReports_Async("", "", "", "", "", "");

                            var filesNameToMergeOrder = new List<string>();                           
                            var ordenReportes = _organizationBL.GetOrdenReportes_(ref _objOperationResult, EmpresaClienteId);
                            var componentIds = ordenReportes.Select(p => p.v_ComponentId).ToList();
                            var reportsCrystal = ordenReportes.FindAll(p => p.v_ComponentId.Contains("N00"));
                            var reportsPdf = ordenReportes.Except(reportsCrystal).ToList();                            
                            
                            var serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(ServiceId);
                            serviceComponents = serviceComponents.FindAll(p => componentIds.Contains(p.v_ComponentId)).ToList();
                            var list = serviceComponents.Union(reportsPdf).ToList();
                            var ListOrdenada = new List<ServiceComponentList>();

                            foreach (var item in ordenReportes)
                            {
                                var obj = new ServiceComponentList();
                                var exist = list.Find(p => p.v_ComponentId == item.v_ComponentId);

                                if (exist != null)
                                {
                                    obj.v_ComponentId = item.v_ComponentId + "|" + item.i_NombreCrystalId;
                                    obj.v_ComponentName = item.v_ComponentName;
                                    obj.i_Orden = item.i_Orden;

                                    ListOrdenada.Add(obj);
                                }
                            }

                            List<string> OrdenReporte = new List<string>();
                            foreach (var obj in ListOrdenada)
                            {
                                OrdenReporte.Add(obj.v_ComponentId);
                            }
                            #region GeneraReporte

                            System.Threading.Tasks.Task.Factory.StartNew(() => frm.CrearReportesCrystal(ServiceId, PacientId, OrdenReporte, _listaDosaje, false)).Wait();
                            frm._filesNameToMerge.Add(rutaInterconsulta + ServiceId + "-" + oService.Paciente + ".pdf");
                            foreach (var item in OrdenReporte)
                            {
                                var componentId = item.Split('|')[0];
                                var path = _ruta + ServiceId + "-" + componentId + ".pdf";
                                if (frm._filesNameToMerge.Find(p => p == path) != null)
                                {
                                    filesNameToMergeOrder.Add(path);
                                }
                            }
                        
                            ListaFinalRutaArchivos.AddRange(filesNameToMergeOrder.ToList());
                            #endregion
                        }
                        else
                        {
                            PersonasNoGeneradas.Add(row.Cells["v_Pacient"].Value.ToString());
                        }
         
                    }
                };

                if (PersonasNoGeneradas.Count > 0)
                {
                    string personas = "";
                    foreach (var persona in PersonasNoGeneradas)
                    {
                        personas = persona + ", ";
                    }
                    
                    personas += ".";
                    string message =
                        "No se genero el reporte de las siguientes personas por no contar con un servicio CULMINADO: " + personas;
                    MessageBox.Show(message, "¡ AVISO !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _mergeExPDF.Execute();
                        }
                        else if (oService.Empresa == oService.Contract)
                        {
                            _mergeExPDF.DestinationFile = rutaConsolidado + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";
                            _mergeExPDF.Execute();
                        }
                        var adjunto = frm._filesNameToMerge.FindAll(p => p.Contains(_dni));
                        var adjunto_2 = frm._filesNameToMerge.FindAll(p => p.Contains(ServiceId + "-" + _pacientName));
                        if (adjunto.Count() > 0 && adjunto_2.Count() > 0)
                        {
                            foreach (var pdf in frm._filesNameToMerge) { foreach (var adj in adjunto) { foreach (var otros in adjunto_2) { if ((pdf != adj || pdf != otros) && pdf != _ruta + ServiceId + "-CAP.pdf") { System.IO.File.Delete(pdf); } } } }
                        }
                        else if (adjunto.Count() > 0 && adjunto_2.Count() == 0)
                        {
                            foreach (var pdf in frm._filesNameToMerge) { foreach (var adj in adjunto) { if (pdf != adj && pdf != _ruta + ServiceId + "-CAP.pdf") { System.IO.File.Delete(pdf); } } }
                        }
                        else if (adjunto.Count() == 0 && adjunto_2.Count() > 0)
                        {
                            foreach (var pdf in frm._filesNameToMerge) { foreach (var adj in adjunto_2) { if (pdf != adj && pdf != _ruta + ServiceId + "-CAP.pdf") { System.IO.File.Delete(pdf); } } }
                        }
                }

                if (ListaFinalRutaArchivos.Count > 0)
                {
                    _mergeExPDF.FilesName = ListaFinalRutaArchivos;
                    _mergeExPDF.DestinationFile = _rutaTemporal + "ReportesAgrupados-" + DateTime.Now.Millisecond.ToString() + ".pdf";
                    _mergeExPDF.Execute();
                    _mergeExPDF.RunFile();
                }            
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedió un error al generar los reportes: " + ex.Message, "¡ ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
           
        }

    }
}
