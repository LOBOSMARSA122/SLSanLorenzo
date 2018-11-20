using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using Sigesoft.Node.WinClient.UI.Configuration;
using Sigesoft.Node.WinClient.UI.NatclarXML;
using NetPdf;

using Sigesoft.Node.Contasol.Integration;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCalendar : Form
    {
        string strFilterExpression;
        string _PacientId;
        List<string> _componentIds;
        private string _ProtocolId;
        List<string> _ListaCalendar;
        string _v_OrganizationLocationProtocol;
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
      
        string _v_CustomerOrganizationId;
        string _v_CustomerLocationId;

        byte[] _personImage;
        string _personName;
        string _serviceId;
        int _RowIndexgrdDataCalendar;
        int _RowIndexgrdDataComponentes;
        string _strServicelId;    
        CalendarBL _objCalendarBL = new CalendarBL();
        ServiceBL _objServiceBL = new ServiceBL();

        string _calendarId;
        List<CalendarList> _objData = new List<CalendarList>();
        private Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip _customizedToolTip = null;
        private List<KeyValueDTO> _formActions = null;

        private bool _sendEmailEnabled;
        List<string> ListaComponentes = new List<string>();

        private string _NroHospitalizacion;
        private string _dni;
        public frmCalendar()
        {
            InitializeComponent();          
        }

        public frmCalendar(string NroHospitalizacion, string dni, string serviceId)
        {
            InitializeComponent();
            _NroHospitalizacion = NroHospitalizacion;
            _dni = dni;
            _serviceId = serviceId;
        }

        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }

        private void frmCalendar_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            #region FormActions

            _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmCalendar",
                                                                               Globals.ClientSession.i_CurrentExecutionNodeId,
                                                                               Globals.ClientSession.i_RoleId.Value,
                                                                               Globals.ClientSession.i_SystemUserId);

            _sendEmailEnabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmCalendar_SENDEMAIL", _formActions);

            #endregion  
            
            UltraGridColumn c = grdDataCalendar.DisplayLayout.Bands[0].Columns["b_Seleccionar"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;

            _customizedToolTip = new Sigesoft.Node.WinClient.UI.Utils.CustomizedToolTip(ugComponentes);
            ddlConsultorio.SelectedValueChanged -= ddlConsultorio_SelectedValueChanged;

            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult,Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlVipId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlNewContinuationId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 121, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlLineStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 120, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlCalendarStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 122, null), DropDownListAction.All);

            var componentProfile = _objServiceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);           
            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);
            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);
            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();
            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));           
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));
            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", results, DropDownListAction.Select);
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, nodeId);
            Utils.LoadDropDownList(cbCustomerOrganization,
                "Value1",
                "Id",
                dataListOrganization1,
                DropDownListAction.All);
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
            ddlConsultorio.SelectedValueChanged += ddlConsultorio_SelectedValueChanged;

            ddlServiceTypeId.SelectedValue= "1";
            ddlMasterServiceId.SelectedValue= "2";
            btnFilter_Click(sender, e);
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


        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Por favor seleccionar Tipo de Servicio", "Validación!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (ddlMasterServiceId.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Por favor seleccionar Servicio", "Validación!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            //if (ddlServiceTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceTypeId==" + ddlServiceTypeId.SelectedValue);
            if (ddlMasterServiceId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceId==" + ddlMasterServiceId.SelectedValue);
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_Pacient.Contains(\"" + txtPacient.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtNroDocument.Text)) Filters.Add("v_NumberDocument==" + "\"" + txtNroDocument.Text.Trim() + "\"");
            if (ddlVipId.SelectedValue.ToString() != "-1") Filters.Add("i_IsVipId==" + ddlVipId.SelectedValue);
            if (ddlCalendarStatusId.SelectedValue.ToString() != "-1") Filters.Add("i_CalendarStatusId==" + ddlCalendarStatusId.SelectedValue);
            if (ddlNewContinuationId.SelectedValue.ToString() != "-1") Filters.Add("i_NewContinuationId==" + ddlNewContinuationId.SelectedValue);
            if (ddlLineStatusId.SelectedValue.ToString() != "-1") Filters.Add("i_LineStatusId==" + ddlLineStatusId.SelectedValue);
            if (cbCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id2 = cbCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id2[0] + "\"&&v_CustomerLocationId==" + "\"" + id2[1] + "\"");

            }
            Filters.Add("i_IsDeleted==0");
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

            this.BindGrid();
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, "d_EntryTimeCM ASC", strFilterExpression);

            grdDataCalendar.DataSource = objData;
            this.ugComponentes.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            txtTrabajador.Text = "";
            WorkingOrganization.Text = "";
            txtProtocol.Text = "";
            txtService.Text = "";
            txtTypeESO.Text = "";
            pbImage.Image = null;
            txtExisteHuella.Text = "";
            ugComponentes.DataSource = new List<Categoria>();

            if (grdDataCalendar.Rows.Count > 0)
            {
                grdDataCalendar.Rows[0].Selected = true;
                ServiceBL objServiceBL = new ServiceBL();
                OperationResult objOperationResult = new OperationResult();

              var  ListServiceComponent = objServiceBL.GetServiceComponents_(ref objOperationResult, _strServicelId);
                ListaComponentes = new List<string>();
                foreach (var item in ListServiceComponent)
                {
                    ListaComponentes.Add(item.v_ComponentId);
                }
            }

        }

        private List<CalendarList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            _objData = _objCalendarBL.GetCalendarsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate, _componentIds);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        
        private void btnPerson_Click(object sender, EventArgs e)
        {
            frmSchedulePerson frm = new frmSchedulePerson("","New","", _NroHospitalizacion, _dni);
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void btnMassive_Click(object sender, EventArgs e)
        {
            frmschedulePeople frm = new frmschedulePeople();
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuCancelCalendar_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objCalendarDto = new calendarDto();
            OperationResult objOperationResult = new OperationResult();

               DialogResult Result = MessageBox.Show("¿Está seguro de CANCELAR este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

               if (Result == System.Windows.Forms.DialogResult.Yes)
               {
                   string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells[0].Value.ToString();

                   objCalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, strCalendarId);
                   objCalendarDto.v_CalendarId = strCalendarId;
                   objCalendarDto.i_CalendarStatusId = (int)Common.CalendarStatus.Cancelado;
                   objCalendarDto.i_LineStatusId = (int)Common.LineStatus.FueraCircuito;

                   objCalendarBL.UpdateCalendar(ref objOperationResult, objCalendarDto, Globals.ClientSession.GetAsList());

                   btnFilter_Click(sender, e);
               }
           
        }

        private void mnuFinCircuito_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objCalendarDto = new calendarDto();
            OperationResult objOperationResult = new OperationResult();

                DialogResult Result = MessageBox.Show("¿Está seguro de TERMINAR CIRCUITO este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells[0].Value.ToString();

                    objCalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, strCalendarId);
                    objCalendarDto.v_CalendarId = strCalendarId;
                    objCalendarDto.i_LineStatusId = (int)Common.LineStatus.FueraCircuito;

                    objCalendarBL.UpdateCalendar(ref objOperationResult, objCalendarDto, Globals.ClientSession.GetAsList());

                    btnFilter_Click(sender, e);
                }
           
        }

        private void mnuComenzarCircuito_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();            
            calendarDto objCalendarDto = new calendarDto();
            ServiceBL objServiceBL = new ServiceBL();
            serviceDto objServiceDto = new serviceDto();
            OperationResult objOperationResult = new OperationResult();
            List<Categoria> ListServiceComponent = new List<Categoria>();

            DateTime FechaAgenda = DateTime.Parse(grdDataCalendar.Selected.Rows[0].Cells[4].Value.ToString());
            if (FechaAgenda.Date != DateTime.Now.Date)
            {
                MessageBox.Show("No se permite Iniciar Circuito con una fecha que no sea la actual.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
              DialogResult Result = MessageBox.Show("¿Está seguro de INICIAR CIRCUITO este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

              if (Result == System.Windows.Forms.DialogResult.Yes)
              {
                  string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();

                  objCalendarBL.CircuitStart(ref objOperationResult, strCalendarId, DateTime.Now, Globals.ClientSession.GetAsList());

                  objServiceDto = objServiceBL.GetService(ref objOperationResult, grdDataCalendar.Selected.Rows[0].Cells[5].Value.ToString());

                  var NewCont =grdDataCalendar.Selected.Rows[0].Cells["i_NewContinuationId"].Value;
                  if ((int)NewCont  == (int)Common.modality.NuevoServicio)
                  {
                      objServiceDto.i_ServiceStatusId = (int)Common.ServiceStatus.Iniciado;
                  }
                  else if ((int)NewCont  == (int)Common.modality.ContinuacionServicio)
                  {
                      objServiceDto.i_ServiceStatusId =int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());
                  }
                  
                  objServiceBL.UpdateService(ref objOperationResult, objServiceDto, Globals.ClientSession.GetAsList());

                  _strServicelId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

                  btnFilter_Click(sender, e);

                  ListServiceComponent = objServiceBL.GetAllComponentsByService(ref objOperationResult, _strServicelId);
                  ugComponentes.DataSource = ListServiceComponent;
                  
                  grdDataCalendar.Rows[_RowIndexgrdDataCalendar].Selected = true;


                  //Grabar datos por defecto en RX, Espirmetrpia y EKG

                       ProtocolBL oProtocolBL = new ProtocolBL();
                        List<string> servicios = new List<string>();
                        servicios.Add(_strServicelId);


                  //var RX = ListServiceComponent.FindAll(p => p.i_CategoryId == 6);
                  //if (RX.Count >0)
                  //{
                  //    foreach (var item in RX)
                  //      {
                  //          _objServiceBL.CulminarServicioPorDefecto(item.v_ServiceComponentId, 6);
                  //      }
                  //}

                  //var ESPIRO = ListServiceComponent.FindAll(p => p.i_CategoryId == 16);
                  //if (ESPIRO.Count > 0)
                  //{
                  //    foreach (var item in ESPIRO)
                  //    {
                  //        _objServiceBL.CulminarServicioPorDefecto(item.v_ServiceComponentId, 16);
                  //    }
                  //}

                  //var EKG = ListServiceComponent.FindAll(p => p.i_CategoryId == 5);
                  //if (EKG.Count > 0)
                  //{
                  //    foreach (var item in EKG)
                  //    {
                  //        _objServiceBL.CulminarServicioPorDefecto(item.v_ServiceComponentId, 5);
                  //    }
                  //}

                  MessageBox.Show("Circuito iniciado, paciente disponible para su atención", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);

              }
           
        }

        private void mnuRefrescar_Click(object sender, EventArgs e)
        {
            btnFilter_Click(sender, e);
        }

        private void mnuReagendarCita_Click(object sender, EventArgs e)
        {
            string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells[0].Value.ToString();
            string strProtocolId = grdDataCalendar.Selected.Rows[0].Cells[12].Value.ToString();

              DialogResult Result = MessageBox.Show("¿Está seguro de REAGENDAR este registro?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

              if (Result == System.Windows.Forms.DialogResult.Yes)
              {
                  frmSchedulePerson frm = new frmSchedulePerson(strCalendarId, "Reschedule", strProtocolId, "", "");
                  frm.ShowDialog();
                  if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                  {
                      // Refrescar grilla
                      btnFilter_Click(sender, e);
                  }
              }
        }
        
        private void ddlServiceTypeId_TextChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, int.Parse(ddlServiceTypeId.SelectedValue.ToString()), Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            
        }

        private void grdDataCalendar_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            //Si el contenido de la columna Vip es igual a SI
            if (e.Row.Cells["v_IsVipName"].Value.ToString().Trim() == Common.SiNo.SI.ToString())
            {
                //Escojo 2 colores
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Pink;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
        
            }
            
            if ((int)e.Row.Cells["i_CalendarStatusId"].Value == (int)CalendarStatus.Atendido && (int)e.Row.Cells["i_LineStatusId"].Value == (int)LineStatus.FueraCircuito)
            {
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Gray;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }
            else if ((int)e.Row.Cells["i_CalendarStatusId"].Value == (int)CalendarStatus.Cancelado)
            {
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.Yellow;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }

            if (e.Row.Cells["i_StatusLiquidation"].Value == null)
                return;

            if ((int)e.Row.Cells["i_StatusLiquidation"].Value == (int)PreLiquidationStatus.Generada)
            {
                e.Row.Appearance.BackColor = Color.White;
                e.Row.Appearance.BackColor2 = Color.SkyBlue;
                //Y doy el efecto degradado vertical
                e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            }
        }

        private void grdDataCalendar_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);
           

            if (uiElement == null || uiElement.Parent == null)
                return;

            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));
            if (e.Button == MouseButtons.Right)
            {

                if (row != null)
                {
                    _RowIndexgrdDataCalendar = row.Index;
                    grdDataCalendar.Rows[row.Index].Selected = true;
                    int CalendarStatusId = int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_CalendarStatusId"].Value.ToString());
                    int LineStatusId = int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_LineStatusId"].Value.ToString());
                    int ServiceStatusId = int.Parse(grdDataCalendar.Selected.Rows[0].Cells["i_ServiceStatusId"].Value.ToString());
                    _PacientId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
                    _ProtocolId = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
                    if (CalendarStatusId == (int)Common.CalendarStatus.Agendado)
                    {
                        contextMenuStrip1.Items["mnuCancelCalendar"].Enabled = true;
                        contextMenuStrip1.Items["mnuFinCircuito"].Enabled = false;
                        contextMenuStrip1.Items["mnuComenzarCircuito"].Enabled = true;
                        contextMenuStrip1.Items["mnuReagendarCita"].Enabled = true;
                        contextMenuStrip1.Items["mnuMarcarSalida"].Enabled = false;
                    }
                    else if (CalendarStatusId == (int)Common.CalendarStatus.Atendido)
                    {
                        contextMenuStrip1.Items["mnuCancelCalendar"].Enabled = false;
                        contextMenuStrip1.Items["mnuFinCircuito"].Enabled = true;
                        contextMenuStrip1.Items["mnuMarcarSalida"].Enabled = true;
                        contextMenuStrip1.Items["mnuComenzarCircuito"].Enabled = false;
                        contextMenuStrip1.Items["mnuReagendarCita"].Enabled = false;   
                    }
                    else if (CalendarStatusId == (int)Common.CalendarStatus.Cancelado)
                    {
                        contextMenuStrip1.Items["mnuCancelCalendar"].Enabled = false;
                        contextMenuStrip1.Items["mnuFinCircuito"].Enabled = false;
                        contextMenuStrip1.Items["mnuComenzarCircuito"].Enabled = false;
                        contextMenuStrip1.Items["mnuReagendarCita"].Enabled = true;
                        contextMenuStrip1.Items["mnuMarcarSalida"].Enabled = false;
                    }

                    if (LineStatusId == (int)Common.LineStatus.FueraCircuito && CalendarStatusId == (int)Common.CalendarStatus.Atendido )
                    {
                        contextMenuStrip1.Items["mnuCancelCalendar"].Enabled = false;
                        contextMenuStrip1.Items["mnuFinCircuito"].Enabled = false;
                        contextMenuStrip1.Items["mnuComenzarCircuito"].Enabled = false;
                        contextMenuStrip1.Items["mnuReagendarCita"].Enabled = false;
                        contextMenuStrip1.Items["mnuMarcarSalida"].Enabled = false;
                    }

                    OperationResult objOperationResult = new OperationResult();
                    ServiceBL objServiceBL = new ServiceBL();
                    List<Categoria> ListServiceComponent = new List<Categoria>();
                    _strServicelId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    ListServiceComponent = objServiceBL.GetAllComponentsByService(ref objOperationResult, _strServicelId);
                    ugComponentes.DataSource = ListServiceComponent;
                    var ListServiceComponentAMC = objServiceBL.GetServiceComponents_(ref objOperationResult, _strServicelId);

                    ListaComponentes = new List<string>();
                    foreach (var item in ListServiceComponentAMC)
                    {
                        ListaComponentes.Add(item.v_ComponentId);
                    }
                    

                } 
                else
                {
                    contextMenuStrip1.Items["mnuCancelCalendar"].Enabled = false;
                    contextMenuStrip1.Items["mnuFinCircuito"].Enabled = false;
                    contextMenuStrip1.Items["mnuComenzarCircuito"].Enabled = false;
                    contextMenuStrip1.Items["mnuReagendarCita"].Enabled = false;
                    contextMenuStrip1.Items["mnuMarcarSalida"].Enabled = false;
                }               
            }

            if (e.Button == MouseButtons.Left)
            {
                if (row != null)
                {
                    OperationResult objOperationResult = new OperationResult();
                    ServiceBL objServiceBL = new ServiceBL();
                    List<Categoria> ListServiceComponent = new List<Categoria>();
                    PacientBL objPacientBL = new PacientBL();
                    personDto objpersonDto = new personDto();
                    if (grdDataCalendar.Selected.Rows.Count == 0) return;
                    _PacientId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
                    //string strServicelId = grdDataCalendar.Selected.Rows[0].Cells[5].Value.ToString();
                    _strServicelId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    ListServiceComponent = objServiceBL.GetAllComponentsByService(ref objOperationResult, _strServicelId);
                   var ListServiceComponentAMC = objServiceBL.GetServiceComponents_(ref objOperationResult, _strServicelId);
                   ugComponentes.DataSource = ListServiceComponent;

                    ListaComponentes = new List<string>();
                    foreach (var item in ListServiceComponentAMC)
                    {
                        ListaComponentes.Add(item.v_ComponentId);
                    }
                    

                    txtTrabajador.Text = grdDataCalendar.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();

                    if (grdDataCalendar.Selected.Rows[0].Cells["CompMinera"].Value != null)
                        WorkingOrganization.Text = grdDataCalendar.Selected.Rows[0].Cells["CompMinera"].Value.ToString();
                                    
                    txtProtocol.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value == null ? "" : grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
                    txtService.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceName"].Value.ToString();

                    if (grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value != null)
                    {
                        if (grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == Constants.CONSULTAMEDICA)
                        {
                            txtTypeESO.Text = "";
                        }
                        else
                        {
                            txtTypeESO.Text = grdDataCalendar.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
                        }
                    }
                   
                    _personName = grdDataCalendar.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();

                    objpersonDto = objPacientBL.GetPerson(ref objOperationResult, _PacientId);

                    Byte[] ooo = objpersonDto.b_PersonImage;

                    if (ooo == null)
                    {
                        pbImage.Image = Resources.nofoto;
                    }
                    else
                    {
                        pbImage.Image = Common.Utils.BytesArrayToImageOficce(ooo, pbImage);
                        _personImage = ooo;
                    }

                    // Huella y Firma
                    if (objpersonDto.b_FingerPrintImage == null)
                    {
                        txtExisteHuella.Text = "NO REGISTRADO";
                        txtExisteHuella.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtExisteHuella.Text = "REGISTRADO";
                        txtExisteHuella.ForeColor = Color.DarkBlue;
                    }

                    // Firma
                    if (objpersonDto.b_RubricImage == null)
                    {
                        txtExisteFirma.Text = "NO REGISTRADO";
                        txtExisteFirma.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtExisteFirma.Text = "REGISTRADO";
                        txtExisteFirma.ForeColor = Color.DarkBlue;
                    }

                }

            }
        }

        private void mnuVerPaciente_Click(object sender, EventArgs e)
        {
            var frm = new frmPacient(_PacientId);
            frm.ShowDialog();
            btnFilter_Click(sender, e);

        }

        private void grdDataCalendar_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnConsentimiento.Enabled = btnExportExcel.Enabled = btnExportPdf.Enabled = btnAdjuntar.Enabled = (grdDataCalendar.Selected.Rows.Count > 0);
            btnSendEmail.Enabled = (grdDataCalendar.Selected.Rows.Count > 0 && _sendEmailEnabled);
            

            if (grdDataCalendar.Selected.Rows.Count != 0)
            {
                mnuAreaTrabajo.Enabled = true;
                OperationResult objOperationResult = new OperationResult();
                ServiceBL objServiceBL = new ServiceBL();
                PacientBL objPacientBL = new PacientBL();
                personDto objpersonDto = new personDto();
                List<Categoria> ListServiceComponent = new List<Categoria>();
                //string strServicelId = grdDataCalendar.Selected.Rows[0].Cells[5].Value.ToString();
                _strServicelId = grdDataCalendar.Selected.Rows[0].Cells[5].Value.ToString();
                ListServiceComponent = objServiceBL.GetAllComponentsByService(ref objOperationResult, _strServicelId);
                ugComponentes.DataSource = ListServiceComponent;

                _v_OrganizationLocationProtocol = grdDataCalendar.Selected.Rows[0].Cells["v_OrganizationLocationProtocol"].Value.ToString();

                _v_CustomerOrganizationId = grdDataCalendar.Selected.Rows[0].Cells["v_CustomerOrganizationId"].Value.ToString();
                _v_CustomerLocationId = grdDataCalendar.Selected.Rows[0].Cells["v_CustomerLocationId"].Value.ToString();


                txtTrabajador.Text = grdDataCalendar.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();
                _PacientId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();

                _serviceId = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

                _calendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();

                if (grdDataCalendar.Selected.Rows[0].Cells["CompMinera"].Value != null)
                    WorkingOrganization.Text = grdDataCalendar.Selected.Rows[0].Cells["CompMinera"].Value.ToString();

                txtProtocol.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value == null ? "" : grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolName"].Value.ToString();
                txtService.Text = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceName"].Value.ToString();

                if (grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value != null)
                {
                    if (grdDataCalendar.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString() == Constants.CONSULTAMEDICA)
                    {
                        txtTypeESO.Text = "";
                    }
                    else
                    {
                        txtTypeESO.Text = grdDataCalendar.Selected.Rows[0].Cells["v_EsoTypeName"].Value.ToString();
                    }
                }

                objpersonDto = objPacientBL.GetPerson(ref objOperationResult, _PacientId);

                //Byte[] ooo = (byte[])grdDataCalendar.Selected.Rows[0].Cells["b_PersonImage"].Value;
                Byte[] ooo = objpersonDto.b_PersonImage;
                if (ooo == null)
                {
                    //pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    pbImage.Image = Resources.nofoto;
                    _personImage = null;

                }
                else
                {
                    //pbImage.SizeMode = PictureBoxSizeMode.Zoom;
                    pbImage.Image = Common.Utils.BytesArrayToImageOficce(ooo, pbImage);
                    _personImage = ooo;
                }

                // Huella y Firma
                if (objpersonDto.b_FingerPrintImage == null)
                {
                    txtExisteHuella.Text = "NO REGISTRADO";
                    txtExisteHuella.ForeColor = Color.Red;
                }
                else
                {
                    txtExisteHuella.Text = "REGISTRADO";
                    txtExisteHuella.ForeColor = Color.DarkBlue;
                }

                // Firma
                if (objpersonDto.b_RubricImage == null)
                {
                    txtExisteFirma.Text = "NO REGISTRADO";
                    txtExisteFirma.ForeColor = Color.Red;
                }
                else
                {
                    txtExisteFirma.Text = "REGISTRADO";
                    txtExisteFirma.ForeColor = Color.DarkBlue;
                }

            }

            btnImprimirHojaRuta.Enabled = (ugComponentes.Rows.Count > 0);

        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            if (_personImage != null)
            {
                var frm = new Sigesoft.Node.WinClient.UI.Operations.Popups.frmPreviewImagePerson(_personImage, _personName);
                frm.ShowDialog();
            }
        }
      
      
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataCalendar, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }       
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridDocumentExporter1.Export(this.grdDataCalendar, saveFileDialog1.FileName,GridExportFileFormat.PDF);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }    
        }

        private void VerAntecedentes_Click(object sender, EventArgs e)
        {
            string pstrPacientId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();        
            frmHistory frm = new frmHistory(pstrPacientId);
            frm.ShowDialog();
        }

        private void btnImprimirHojaRuta_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmRoadMap(_strServicelId, _calendarId);
            frm.ShowDialog();
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            CalendarListEmail oCalendarListEmail = new CalendarListEmail();
            List<CalendarListEmail> oList = new List<CalendarListEmail>();

            try
            {
                foreach (var item in _objData)
                {
                    oCalendarListEmail = new CalendarListEmail();
                    oCalendarListEmail.v_EntryTimeCM = item.d_EntryTimeCM.ToString();
                    oCalendarListEmail.v_Pacient = item.v_Pacient;
                    oCalendarListEmail.v_NumberDocument = item.v_NumberDocument;
                    oCalendarListEmail.v_ProtocolName = item.v_AptitudeStatusName;
                    oCalendarListEmail.v_ServiceTypeName = item.v_ServiceStatusName;

                    oCalendarListEmail.Restricciones = item.Restricciones == null ? "----------" : item.Restricciones;
                    oCalendarListEmail.Observaciones = item.Observaciones == null ? "----------" : item.Observaciones;

                    oCalendarListEmail.v_EsoTypeName = item.v_EsoTypeName;

                    oList.Add(oCalendarListEmail);
                }
                //LogList xxx = new LogList();
                oCalendarListEmail = new CalendarListEmail();
                oCalendarListEmail.v_EntryTimeCM = "INGRESO CENTRO MÉDICO";
                oCalendarListEmail.v_Pacient = "PACIENTE";
                oCalendarListEmail.v_NumberDocument = "NRO. DOCUMENTO";
                oCalendarListEmail.v_ProtocolName = "APTITUD";
                oCalendarListEmail.v_ServiceTypeName = "ESTADO SERVICIO";

                oCalendarListEmail.Restricciones = "RESTRICCIONES";
                oCalendarListEmail.Observaciones = "OBSERVACIÓN";

                oCalendarListEmail.v_EsoTypeName = "TIPO ESO";

                oList.Insert(0, oCalendarListEmail);

                frmEmail frm = new frmEmail(oList, cbCustomerOrganization.Text.ToString(), dtpDateTimeStar.Value.ToShortDateString(), dptDateTimeEnd.Value.ToShortDateString());
                frm.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

  
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNroDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtPacient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }          
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void btnConsentimiento_Click(object sender, EventArgs e)
        {
            var frm = new Reports.frmConsentimiento(_serviceId);
            frm.ShowDialog();
        }

        private void mnuListaNegra_Click(object sender, EventArgs e)
        {
            string pstrPersonId = grdDataCalendar.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            string pstrPaciente = grdDataCalendar.Selected.Rows[0].Cells["v_Pacient"].Value.ToString();

            frmBlackList frm = new frmBlackList(pstrPaciente,pstrPersonId);
            frm.ShowDialog();
        }

        private MergeExPDF _mergeExPDF = new MergeExPDF();
        private List<string> _filesNameToMerge = new List<string>();
        private void btnDetallado_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                   
            OperationResult objOperationResult = new OperationResult();
            ServiceBL objServiceBL = new ServiceBL();
            List<CalendarDetail> Services = new List<CalendarDetail>();
            CalendarDetail oCalendarDetail;

            foreach (var item in grdDataCalendar.Rows)
            {
                var serviceId = item.Cells["v_ServiceId"].Value.ToString();
                oCalendarDetail = new CalendarDetail();


                oCalendarDetail.v_ServiceId = serviceId;
                oCalendarDetail.Pacient = item.Cells["v_Pacient"].Value.ToString();
                oCalendarDetail.EmpresaCliente = item.Cells["v_OrganizationLocationProtocol"].Value.ToString();
                oCalendarDetail.EmpresaEmpleadora = item.Cells["v_OrganizationIntermediaryName"].Value.ToString();
                oCalendarDetail.EmpresaTrabajo = item.Cells["CompMinera"].Value.ToString();
                oCalendarDetail.FechaService = item.Cells["d_ServiceDate"].Value.ToString();
                oCalendarDetail.Protocol = item.Cells["v_ProtocolName"].Value.ToString();

                List<Category> oCategories = new List<Category>();
                Category oCategory;
                var Categories = objServiceBL.GetAllComponentsByService(ref objOperationResult, serviceId);
                foreach (var category in Categories)
                {
                    oCategory = new Category();
                    oCategory.CategoryId = category.i_CategoryId.Value;
                    oCategory.CategoryName = category.v_CategoryName;
                    oCategories.Add(oCategory);

                    List<Components> oComponents = new List<Components>();
                    Components oComponent;
                    var components = category.Componentes;
                    foreach (var component in components)
                    {
                        oComponent = new Components();
                        oComponent.ComponentId = component.v_ComponentId;
                        oComponent.Component = component.v_ComponentName;
                        oComponent.StatusComponentId = component.StatusComponentId;
                        oComponent.StatusComponent = component.StatusComponent;
                        oComponent.User = component.ApprovedUpdateUser;
                        oComponents.Add(oComponent);
                    }

                    oCategory.Components = oComponents;
                }

                oCalendarDetail.Categories = oCategories;

                Services.Add(oCalendarDetail);
            }
            
            string ruta;
            ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
            var pathFile = string.Format("{0}.pdf", Path.Combine(ruta, "Agenda Detallada" + DateTime.Now.Date.ToString("dd-MM-yyy")));
            AgendaDetallada.CreateAgendaDetallada(Services, pathFile);

            _filesNameToMerge.Add(ruta + "Agenda Detallada" + DateTime.Now.Date.ToString("dd-MM-yyy") + ".pdf");

            _mergeExPDF.FilesName = _filesNameToMerge;
            //_mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf";
            _mergeExPDF.DestinationFile = ruta + "Agenda Detallada" + DateTime.Now.Date.ToString("dd-MM-yyy") + "1.pdf"; ;
            _mergeExPDF.Execute();
            _mergeExPDF.RunFile();

            };

        }

        private void btnAgregarAdiconal_Click(object sender, EventArgs e)
        {
            ServiceBL oServiceBL = new ServiceBL();
            var frm = new frmAddAdditionalExam();
            frm._serviceId = _serviceId;
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            OperationResult objOperationResult = new OperationResult();
            var ListServiceComponent = oServiceBL.GetServiceComponents(ref objOperationResult, _strServicelId);
            ugComponentes.DataSource = ListServiceComponent;
        }

        private void btnRemoverEsamen_Click(object sender, EventArgs e)
        {

            if (ugComponentes.Selected.Rows.Count == 0)
                return;

            //if (ugComponentes.Selected.Rows[0].Cells["v_serviceComponentId"] == null)
            //{
            //    MessageBox.Show("¿Por favor seleccione una categoría?", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            ServiceBL oServiceBL = new ServiceBL();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?", "ADVERTENCIA!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                var _auxiliaryExams = new List<ServiceComponentList>();
                OperationResult objOperationResult = new OperationResult();

                //int categoryId = int.Parse(ugComponentes.Selected.Rows[0].Cells["i_CategoryId"].Value.ToString());
                string v_ServiceComponentId = ugComponentes.Selected.Rows[0].Cells["v_serviceComponentId"].Value.ToString();
                //var serviceComponentId = ugComponentes.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();



                //frmRemoverExamen frm = new frmRemoverExamen(categoryId,_serviceId);
                //frm.ShowDialog();



                ServiceComponentList auxiliaryExam = new ServiceComponentList();
                auxiliaryExam.v_ServiceComponentId = v_ServiceComponentId;
                _auxiliaryExams.Add(auxiliaryExam);

                _objCalendarBL.UpdateAdditionalExam(_auxiliaryExams, _serviceId, (int?)SiNo.NO, Globals.ClientSession.GetAsList());
                var ListServiceComponent = oServiceBL.GetAllComponentsByService(ref objOperationResult, _strServicelId);
                ugComponentes.DataSource = ListServiceComponent;







                //if (categoryId == -1)
                //{
                //    ServiceComponentList auxiliaryExam = new ServiceComponentList();
                //    auxiliaryExam.v_ServiceComponentId = serviceComponentId;
                //    _auxiliaryExams.Add(auxiliaryExam);
                //}
                //else
                //{
                //    var oServiceComponentList = oServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, categoryId, _strServicelId);

                //    foreach (var scid in oServiceComponentList)
                //    {
                //        ServiceComponentList auxiliaryExam = new ServiceComponentList();
                //        auxiliaryExam.v_ServiceComponentId = scid.v_ServiceComponentId;
                //        _auxiliaryExams.Add(auxiliaryExam);
                //    }

                //}

                //_objCalendarBL.UpdateAdditionalExam(_auxiliaryExams, _serviceId, (int?)SiNo.NO, Globals.ClientSession.GetAsList());
                //var ListServiceComponent = oServiceBL.GetServiceComponents(ref objOperationResult, _strServicelId);
                //ugComponentes.DataSource = ListServiceComponent;
                ////MessageBox.Show("Se grabo correctamente", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void mnuAreaTrabajo_Click(object sender, EventArgs e)
        {
            var frm = new frmPopupArea(_serviceId, _v_OrganizationLocationProtocol, _v_CustomerOrganizationId, _v_CustomerLocationId);
            frm.ShowDialog();
        }

        private void grdDataCalendar_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void grdDataCalendar_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
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

            var grid = grdDataCalendar.Rows;
            var count = 0;
            foreach (var item in grid)
            {
                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    count += 1;
                }
            }

            if (count > 0)
            {
                btnCambiarProtocolo.Enabled = true;
            }
            else
            {
                btnCambiarProtocolo.Enabled = false;
            }
        }

        private void btnIniciarCircuitoMasivo_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            _ListaCalendar = new List<string>();
            foreach (var item in grdDataCalendar.Rows)
            {
                //CheckBox ck = (CheckBox)item.Cells["b_FechaEntrega"].Value;

                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    string x = item.Cells["v_CalendarId"].Value.ToString();
                    _ListaCalendar.Add(x);
                }
            }

            if (_ListaCalendar.Count == 0)
            {
                MessageBox.Show("No hay ningún servicio con check, por favor seleccionar uno.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                OperationResult objOperationResult = new OperationResult();
                foreach (var item in _ListaCalendar)
                {
                    //oServiceBL.ActualizarFechaIniciarCircuitoCalendar(item.ToString(), dtpDateTimeStar.Value);
                    objCalendarBL.CircuitStart(ref objOperationResult, item.ToString(), DateTime.Now, Globals.ClientSession.GetAsList());

                }
            }

            //frmPopupFechaEntrega frm = new frmPopupFechaEntrega(_ListaCalendar,"calendar");
            //frm.ShowDialog();
            MessageBox.Show("Se inició correctamente el inicio de circuito", "SISTEMAS!", MessageBoxButtons.OK, MessageBoxIcon.Information);
     
            btnFilter_Click(sender, e);
        }

        private void nmuPesoTalla_Click(object sender, EventArgs e)
        {
            //Obttener Service Component
            ServiceBL _serviceBL = new ServiceBL();
            List<string> servicios = new List<string>();
            servicios.Add(_serviceId);

            var obj  = _serviceBL.ObtenerIdsParaImportacionExcel(servicios, 10);
            if (obj.Count > 0)
            {
                frmTriaje frm = new frmTriaje(_PacientId, obj[0].ServicioComponentId.ToString(), obj[0].ServicioId.ToString());
                frm.ShowDialog();
            }
        }

        private void mnuMarcarSalida_Click(object sender, EventArgs e)
        {
             OperationResult objOperationResult = new OperationResult();
             string strCalendarId = grdDataCalendar.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();

             _objCalendarBL.MarcarHoraSalida(ref objOperationResult, strCalendarId, Globals.ClientSession.GetAsList());

             MessageBox.Show("La hora de salida se marcó correctamente.", "SISTEMAS!", MessageBoxButtons.OK, MessageBoxIcon.Information);

             btnFilter_Click(sender, e);
        }

        private void btnEnviarCertificados_Click(object sender, EventArgs e)
        {

            CalendarBL objCalendarBL = new CalendarBL();
            List<string> listaServiciosId = new List<string>();
            List<string>  listaEmpresas = new List<string>();
            string EmpresaIdValidar = "";
            //Validar que todos los resgistros seleccionados sean de la misma empresa
            int Contador = 0;
            foreach (var item in grdDataCalendar.Rows)
            {

                if ((bool)item.Cells["b_Seleccionar"].Value)
                {                 
                    if (EmpresaIdValidar == "")
                    {
                           EmpresaIdValidar = item.Cells["v_CustomerOrganizationId"].Value.ToString();
                    }
                    else
	                {
                        if (EmpresaIdValidar != item.Cells["v_CustomerOrganizationId"].Value.ToString())
                        {
                            MessageBox.Show("Los registros seleccionados no pueden ser de varias Empresas", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
	                }
                    Contador++;
                }
            }


            if (Contador == 0)
            {
                MessageBox.Show("No ha seleccionado ningún registro", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var item in grdDataCalendar.Rows)
            {
                   if ((bool)item.Cells["b_Seleccionar"].Value)
                   {
                       string x = item.Cells["v_ServiceId"].Value.ToString();
                       listaServiciosId.Add(x);
                   }
            }

            var id2 = cbCustomerOrganization.Text.ToString().Split('/');

            frmEnviarCertificados frm = new frmEnviarCertificados(listaServiciosId, id2[0], dtpDateTimeStar.Value.ToString("dd MMMM"), dptDateTimeEnd.Value.ToString("dd MMMM"));
            frm.ShowDialog();
       
        }

        private void btnEnviarAsistencia_Click(object sender, EventArgs e)
        {
            if (cbCustomerOrganization.SelectedValue.ToString() =="-1")
            {
                  MessageBox.Show("Seleccione una Empresa", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
            }
            var FechaInicio = dtpDateTimeStar.Value.ToString("dd MM yyyy");
            var FechaFin = dptDateTimeEnd.Value.ToString("dd MM yyyy");
            var ee = cbCustomerOrganization.Text.Split('/');
            string ruta = Common.Utils.GetApplicationConfigValue("Asistencia").ToString();
            //this.ultraGridDocumentExporter1.Export(this.grdDataCalendar, Ruta + "Asistencia del  " + FechaInicio + " al " + FechaFin + " " + ee[0].ToString()+ ".pdf", GridExportFileFormat.PDF);

            List<ReporteAsistencia> ListaAsistencia = new List<ReporteAsistencia>();
            ReporteAsistencia oReporteAsistencia = null;
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
            foreach (var item in grdDataCalendar.Rows)
            {
                oReporteAsistencia = new ReporteAsistencia();

                oReporteAsistencia.FechaHora = item.Cells["d_DateTimeCalendar"].Value == null?"": ((DateTime)item.Cells["d_DateTimeCalendar"].Value).ToString("dd/MM/yyyy");
                oReporteAsistencia.HoraIngreso = item.Cells["d_EntryTimeCM"].Value == null ? "" : ((DateTime)item.Cells["d_EntryTimeCM"].Value).ToString("hh:mm");
                oReporteAsistencia.HoraSalida = item.Cells["d_SalidaCM"].Value == null ? "" : ((DateTime)item.Cells["d_SalidaCM"].Value).ToString("hh:mm");
                oReporteAsistencia.Paciente = item.Cells["v_Pacient"].Value.ToString();

                oReporteAsistencia.DNI = item.Cells["v_DocNumber"].Value.ToString();
                oReporteAsistencia.Edad = item.Cells["i_Edad"].Value.ToString();
                oReporteAsistencia.Empresa = item.Cells["v_OrganizationLocationProtocol"].Value.ToString();
                oReporteAsistencia.TipoEso = item.Cells["v_EsoTypeName"].Value.ToString();
                oReporteAsistencia.GrupoRiesgo = item.Cells["GESO"].Value.ToString();
                oReporteAsistencia.Puesto =item.Cells["Puesto"].Value == null ?"": item.Cells["Puesto"].Value.ToString();
                oReporteAsistencia.EstadoCita = item.Cells["v_CalendarStatusName"].Value.ToString();
                oReporteAsistencia.EstadoAptitud = item.Cells["v_AptitudeStatusName"].Value.ToString();
                oReporteAsistencia.LogoPropietaria = MedicalCenter.b_Image;
                oReporteAsistencia.NombreEmpresaPropietaria = MedicalCenter.v_Name;

                ListaAsistencia.Add(oReporteAsistencia);
            }
            ReportDocument rp;
            DataSet dsGetRepo = null;
            DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ListaAsistencia);
            dt.TableName = "dtReporteAsistencia";
            dsGetRepo.Tables.Add(dt);
            rp = new Reports.crReporteAsistencia();
            rp.SetDataSource(dsGetRepo);

            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta  +"Asistencia del  " + FechaInicio + " al " + FechaFin + " " + ee[0].ToString() + ".pdf";           
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            frmEnvioEmailCalendar frm = new frmEnvioEmailCalendar(FechaInicio, FechaFin, ee[0].ToString());
            frm.ShowDialog();

        }

        private void btnAgregarExamen_Click(object sender, EventArgs e)
        {
            ServiceBL oServiceBL = new ServiceBL();
            var frm = new frmAddExam(ListaComponentes,"",_ProtocolId,"","","","");
            frm._serviceId = _serviceId;
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            OperationResult objOperationResult = new OperationResult();
            //var ListServiceComponent = oServiceBL.GetServiceComponents(ref objOperationResult, _strServicelId);
            var ListServiceComponent = oServiceBL.GetAllComponentsByService(ref objOperationResult, _strServicelId);
            ugComponentes.DataSource = ListServiceComponent;

           
        }

        private void chkServiciosTerminados_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEnviarInformes_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            List<string> listaServiciosId = new List<string>();
            List<string> listaEmpresas = new List<string>();
            string EmpresaIdValidar = "";
            //Validar que todos los resgistros seleccionados sean de la misma empresa

            int Contador = 0;
          
            foreach (var item in grdDataCalendar.Rows)
            {

                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    if (EmpresaIdValidar == "")
                    {
                        EmpresaIdValidar = item.Cells["v_CustomerOrganizationId"].Value.ToString();
                    }
                    else
                    {
                        if (EmpresaIdValidar != item.Cells["v_CustomerOrganizationId"].Value.ToString())
                        {
                            MessageBox.Show("Los registros seleccionados no pueden ser de varias Empresas", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    Contador++;
                }
               
            }

            if (Contador == 0)
            {
                MessageBox.Show("No ha seleccionado ningún registro", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var item in grdDataCalendar.Rows)
            {
                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    string x = item.Cells["v_ServiceId"].Value.ToString();
                    listaServiciosId.Add(x);
                }
            }

            var id2 = cbCustomerOrganization.Text.ToString().Split('/');

            frmEnviarInformes frm = new frmEnviarInformes(listaServiciosId, id2[0], dtpDateTimeStar.Value.ToString("dd MMMM"), dptDateTimeEnd.Value.ToString("dd MMMM"));
            frm.ShowDialog();
        }

      

      
        private void ultraGridExcelExporter1_ExportStarted(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.ExportStartedEventArgs e)
        {

        }

        private void ugComponentes_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            ////Si el contenido de la columna Vip es igual a SI
            //if (e.Row.Cells["i_ServiceComponentStatusId"].Value.ToString() == ((int)Common.ServiceComponentStatus.Evaluado).ToString() || e.Row.Cells["i_ServiceComponentStatusId"].Value.ToString() == ((int)Common.ServiceComponentStatus.PorAprobacion).ToString())
            //{
            //    //Escojo 2 colores
            //    e.Row.Appearance.BackColor = Color.White;
            //    e.Row.Appearance.BackColor2 = Color.Cyan;
            //    //Y doy el efecto degradado vertical
            //    e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            //}
        }

        private void ugComponentes_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null)
                return;

            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (row != null)
            {
                //ugComponentes.Rows[row.Index].Selected = true;
                contextMenuStrip2.Items["btnRemoverEsamen"].Enabled = true;
            }
            else
            {
                contextMenuStrip2.Items["btnRemoverEsamen"].Enabled = false;
            }
        }

        private void ugComponentes_MouseEnterElement(object sender, UIElementEventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            //ServiceBL oServiceBL = new ServiceBL();
            //List<ServiceComponentList> oServiceComponentList = new List<ServiceComponentList>();
            //StringBuilder Cadena = new StringBuilder();


            //// if we are not entering a cell, then don't anything
            //if (!(e.Element is CellUIElement))
            //{
            //    return;
            //}

            //// find the cell that the cursor is over, if any
            //UltraGridCell cell = e.Element.GetContext(typeof(UltraGridCell)) as UltraGridCell;

            //if (cell != null)
            //{
            //    int categoryId = int.Parse(cell.Row.Cells["i_CategoryId"].Value.ToString());
            //    oServiceComponentList = oServiceBL.GetServiceComponentByCategoryId(ref objOperationResult, categoryId, _strServicelId);


            //    if (categoryId != -1)
            //    {

            //        foreach (var item in oServiceComponentList)
            //        {
            //            Cadena.Append(item.v_ComponentName);
            //            Cadena.Append("\n");
            //        }

            //        _customizedToolTip.AutomaticDelay = 1;
            //        _customizedToolTip.AutoPopDelay = 20000;
            //        _customizedToolTip.ToolTipMessage = Cadena.ToString();
            //        _customizedToolTip.StopTimerToolTip();
            //        _customizedToolTip.StartTimerToolTip();
            //    }

            //}
        }

        private void ugComponentes_MouseLeaveElement(object sender, UIElementEventArgs e)
        {
            //// if we are not leaving a cell, then don't anything
            //if (!(e.Element is CellUIElement))
            //{
            //    return;
            //}

            //// prevent the timer from ticking again
            //_customizedToolTip.StopTimerToolTip();

            //// destroy the tooltip
            //_customizedToolTip.DestroyToolTip(this);
        }

        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            frmAdjuntarDeclaracionJurada frm = new frmAdjuntarDeclaracionJurada(_serviceId);
            frm.Show();
        }

        private void tsmDeclaracionDrogas_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var result = new ProtocolBL().GetProtocolComponentByProtocol(ref objOperationResult, _ProtocolId,Constants.TOXICOLOGICO_ID);
            if (result == null)
            {
                MessageBox.Show("Este servicio no lleva el examen Toxicológico de Drogas", "Información");
                return;
            }
           
            var frm = new ReporteDeclaracionDrogas(_serviceId);
            frm.ShowDialog();
        }

        private void ugComponentes_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void btnGenerarXML_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Natclar oNatclarBL = new Natclar();

            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.xml;*.xml;*)|*.xml;*.xml;*";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> ServicioIds = new List<string>();
                foreach (var item in grdDataCalendar.Rows)
                {
                    if ((bool) item.Cells["b_Seleccionar"].Value)
                    {
                        string serviceId = item.Cells["v_ServiceId"].Value.ToString();
                        ServicioIds.Add(serviceId);
                    }
                    
                }

                var consolidadoDatos = new ServiceBL().DevolverValorCampoPorServicioMejorado(ServicioIds);
               

                foreach (var item in grdDataCalendar.Rows)
                {
                 
                    if ((bool) item.Cells["b_Seleccionar"].Value)
                    {

                        var serviceId = item.Cells["v_ServiceId"].Value.ToString();
                        var datosPaciente = oNatclarBL.DatosXmlNatclar(serviceId);
                        var ubigeoPaciente = oNatclarBL.DevolverUbigue(datosPaciente.DepartamentoNacimiento, datosPaciente.ProvinciaNacimiento, datosPaciente.ProvinciaNacimiento);
                        var datosServicioCompleto = consolidadoDatos.Find(p => p.ServicioId == serviceId);

                        #region Transformación de la data
                        if (datosPaciente.FechaNacimientoSigesoft != null)
                            datosPaciente.FechaNacimiento = datosPaciente.FechaNacimientoSigesoft.Value.ToString("dd/MM/yyyy");
                        #endregion

                        #region XML

                        #region Datos Paciente

                        var eDatosPaciente = new List<string>();
                        eDatosPaciente.Add("HC");
                        eDatosPaciente.Add("DNI");
                        eDatosPaciente.Add("Sexo");
                        eDatosPaciente.Add("PrimerApellido");
                        eDatosPaciente.Add("SegundoApellido");
                        eDatosPaciente.Add("Nombre");
                        eDatosPaciente.Add("EstadoCivil");
                        eDatosPaciente.Add("FechaNacimiento");
                        eDatosPaciente.Add("ProvinciaNacimiento");
                        eDatosPaciente.Add("DistritoNacimiento");
                        eDatosPaciente.Add("DepartamentoNacimiento");
                        eDatosPaciente.Add("email");
                        eDatosPaciente.Add("ResidenciaActual");
                        eDatosPaciente.Add("Direccion");

                        var xeRoot = new XElement("Registro");
                        var xeDatosPaciente = new XElement("DatosPaciente");

                        foreach (var eDatoPaciente in eDatosPaciente)
                        {
                            xeDatosPaciente.Add(new XElement(eDatoPaciente));
                        }

                       
                        foreach (var elementName in eDatosPaciente)
                        {
                            switch (elementName)
                            {
                                case "HC":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.Hc;
                                    break;
                                case "DNI":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.Dni;
                                    break;
                                case "Sexo":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.Sexo.Value.ToString();
                                    break;
                                case "PrimerApellido":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.PrimerApellido;
                                    break;
                                case "SegundoApellido":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.SegundoApellido;
                                    break;
                                case "Nombre":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.Nombre;
                                    break;
                                case "EstadoCivil":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.EstadoCivil.Value.ToString();
                                    break;
                                case "FechaNacimiento":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.FechaNacimiento;
                                    break;
                                case "ProvinciaNacimiento":
                                    xeDatosPaciente.Element(elementName).Value = ubigeoPaciente.prov;
                                    break;
                                case "DistritoNacimiento":
                                    xeDatosPaciente.Element(elementName).Value = ubigeoPaciente.distr;
                                    break;
                                case "DepartamentoNacimiento":
                                    xeDatosPaciente.Element(elementName).Value = ubigeoPaciente.depar;
                                    break;
                                case "email":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.Email;
                                    break;
                                case "ResidenciaActual":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.ResidenciaActual;
                                    break;
                                case "Direccion":
                                    xeDatosPaciente.Element(elementName).Value = datosPaciente.Direccion;
                                    break;
                                default: break;
                            }

                        }
                        xeRoot.Add(new XElement(xeDatosPaciente));

                        #endregion

                        #region Datos del Examen

                            var eDatosExamen = new List<string>();
                            eDatosExamen.Add("IDEstructura");
                            eDatosExamen.Add("IDCentro");
                            eDatosExamen.Add("IDExamen");
                            eDatosExamen.Add("IDActuacion");
                            eDatosExamen.Add("TipoExamen");
                            eDatosExamen.Add("IDEstado");
                            eDatosExamen.Add("FechaRegistro");

                            var xeDatosExamen = new XElement("DatosExamen");

                            foreach (var eDatoExamen in eDatosExamen)
                            {
                                xeDatosExamen.Add(new XElement(eDatoExamen));
                            }

                            foreach (var edatoExamen in eDatosExamen)
                            {
                                switch (edatoExamen)
                                {
                                    case "IDEstructura":
                                        xeDatosExamen.Element(edatoExamen).Value = datosPaciente.IDEstructura;
                                        break;
                                    case "IDCentro":
                                        xeDatosExamen.Element(edatoExamen).Value = datosPaciente.IDCentro;
                                        break;
                                    case "IDExamen":
                                        xeDatosExamen.Element(edatoExamen).Value = datosPaciente.IDExamen;
                                        break;
                                    case "IDActuacion":
                                        xeDatosExamen.Element(edatoExamen).Value = datosPaciente.IDActuacion;
                                        break;
                                    case "TipoExamen":
                                        var tipoExamen = "";
                                        switch (datosPaciente.TipoExamen)
                                        {
                                            case (int) TypeESO.PreOcupacional:
                                                tipoExamen = ConstantsNatclar.TIPO_EXAMEN_INGRESO;
                                                break;
                                            case (int) TypeESO.PeriodicoAnual:
                                                tipoExamen = ConstantsNatclar.TIPO_EXAMEN_PERIODICO;
                                                break;
                                            case (int) TypeESO.Retiro:
                                                tipoExamen = ConstantsNatclar.TIPO_EXAMEN_RETIRO;
                                                break;
                                            case (int) TypeESO.Especialista:
                                                tipoExamen = ConstantsNatclar.TIPO_EXAMEN_ESPECIAL;
                                                break;
                                            case (int) TypeESO.Visita:
                                                tipoExamen = ConstantsNatclar.TIPO_EXAMEN_VISITA;
                                                break;
                                            default:
                                                break;
                                        }

                                        xeDatosExamen.Element(edatoExamen).Value = tipoExamen;
                                        break;
                                    case "IDEstado":
                                        xeDatosExamen.Element(edatoExamen).Value = datosPaciente.IDEstado.ToString() == "0"
                                            ? "A"
                                            : "I";
                                        break;
                                    case "FechaRegistro":
                                        xeDatosExamen.Element(edatoExamen).Value =
                                            datosPaciente.FechaRegistro.Value.ToString("dd/MM/yyyy");
                                        break;

                                    default:
                                        break;
                                }
                            }
                            xeRoot.Add(new XElement(xeDatosExamen));

                        #endregion
                           
                        #region Analítica
                            var perfiles = datosServicioCompleto.CampoValores.FindAll(p => p.CategoryId == (int)CategoryTypeExam.Laboratorio).GroupBy(p => p.IdComponente).Select(group => group.First()).ToList();
                            if (perfiles.Count > 0)
                            {
                                var xeAnalitica = new XElement("Analitica");
                                var contadorPerfil = 1;

                          
                                foreach (var perfil in perfiles)
                                {
                                    var xePerfil = new XElement("Perfil" + contadorPerfil);
                                    xePerfil.Value = CodigoNatclarLaboratorio(perfil.IdComponente);

                                    //var pruebas = perfiles.FindAll(p => p.IdComponente == perfil.IdComponente).ToList();
                                    var contadorPrueba = 1;

                                    var campos = datosServicioCompleto.CampoValores.FindAll(p => p.IdComponente == perfil.IdComponente).ToList();

                                    foreach (var prueba in campos)
                                    {
                                        var xePrueba = new XElement("Prueba" + contadorPrueba);
                                        xePerfil.Add(xePrueba);
                                        xePerfil.Element("Prueba" + contadorPrueba).Value = CodigoNatclarLaboratorio(prueba.IdCampo);
                                        contadorPrueba++;

                                        var xeValorPrueba = new XElement("ValorPrueba1");
                                        xeValorPrueba.Value = prueba.ValorName;
                                        xePrueba.Add(xeValorPrueba);

                                    }
                                    xeAnalitica.Add(xePerfil);

                                    contadorPerfil ++;
                                }
                                xeRoot.Add(new XElement(xeAnalitica));
                            }
                                #endregion
                            
                        #region Antecedentes Médicos
                        var antecedentesMedicos = new HistoryBL().GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "d_StartDate DESC", null, _PacientId);
                        if (antecedentesMedicos.Count > 0)
                        {

                            var eAntecedentes = new List<string>();
                            eAntecedentes.Add("CodigoCIE");
                            eAntecedentes.Add("Descripcion");
                            eAntecedentes.Add("FechaInicio");
                            eAntecedentes.Add("FechaFin");
                            eAntecedentes.Add("AntecedenteLaboral");

                            var xeAntecedente = new XElement("ANTECEDENTES");

                            foreach (var eAntecedente in eAntecedentes)
                            {
                                xeAntecedente.Add(new XElement(eAntecedente));
                            }

                            foreach (var antecedente in antecedentesMedicos)
                            {
                                foreach (var elementName in eAntecedentes)
                                {
                                    switch (elementName)
                                    {
                                        case "CodigoCIE":
                                            xeAntecedente.Element(elementName).Value = antecedente.v_CIE10Id;
                                            break;
                                        case "Descripcion":
                                            xeAntecedente.Element(elementName).Value = antecedente.v_TreatmentSite;
                                            break;
                                        case "FechaInicio":
                                            xeAntecedente.Element(elementName).Value =
                                                antecedente.d_StartDate.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "FechaFin":
                                            xeAntecedente.Element(elementName).Value =
                                                antecedente.d_StartDate.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "AntecedenteLaboral":
                                            xeAntecedente.Element(elementName).Value = antecedente.i_TypeDiagnosticId ==4? "S": "N";
                                            break;
                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeAntecedente));
                                }
                            }

                        }

                #endregion

                        #region Antecedentes Quirurgico
                        var antecedentesQuirurgicos = new HistoryBL().GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "d_StartDate DESC", null, _PacientId).FindAll(p => p.v_DiseasesId == "N009-DD000000637");
                        if (antecedentesQuirurgicos.Count > 0)
                        {

                            var eAntecedentes = new List<string>();
                            eAntecedentes.Add("CodigoCIE");
                            eAntecedentes.Add("Descripcion");
                            eAntecedentes.Add("FechaInicio");
                            eAntecedentes.Add("FechaFin");
                            eAntecedentes.Add("AntecedenteLaboral");

                            var xeAntecedente = new XElement("ANTECEDENTES");

                            foreach (var eAntecedente in eAntecedentes)
                            {
                                xeAntecedente.Add(new XElement(eAntecedente));
                            }

                            foreach (var antecedente in antecedentesQuirurgicos)
                            {
                                foreach (var elementName in eAntecedentes)
                                {
                                    switch (elementName)
                                    {
                                        case "CodigoCIE":
                                            xeAntecedente.Element(elementName).Value = antecedente.v_CIE10Id;
                                            break;
                                        case "Descripcion":
                                            xeAntecedente.Element(elementName).Value = antecedente.v_TreatmentSite;
                                            break;
                                        case "FechaInicio":
                                            xeAntecedente.Element(elementName).Value =
                                                antecedente.d_StartDate.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "FechaFin":
                                            xeAntecedente.Element(elementName).Value =
                                                antecedente.d_StartDate.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "AntecedenteLaboral":
                                            xeAntecedente.Element(elementName).Value = antecedente.i_TypeDiagnosticId == 4 ? "S" : "N";
                                            break;
                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeAntecedente));
                                }
                            }

                        }

                        #endregion
                          
                        #region audiometria
                        var audiometriaValores = new ServiceBL().ValoresComponentesUserControl(_serviceId, Constants.AUDIOMETRIA_ID);
                        if (audiometriaValores.Count > 0)
                        {

                            var eAudiometria = new List<string>();
                            eAudiometria.Add("ATAND125");
                            eAudiometria.Add("ATAND250");
                            eAudiometria.Add("ATAND500");
                            eAudiometria.Add("ATAND750");
                            eAudiometria.Add("ATAND1000");
                            eAudiometria.Add("ATAND1500");
                            eAudiometria.Add("ATAND2000");
                            eAudiometria.Add("ATAND3000");
                            eAudiometria.Add("ATAND4000");
                            eAudiometria.Add("ATAND6000");
                            eAudiometria.Add("ATAND8000");

                            eAudiometria.Add("ATANI125");
                            eAudiometria.Add("ATANI250");
                            eAudiometria.Add("ATANI500");
                            eAudiometria.Add("ATANI750");
                            eAudiometria.Add("ATANI1000");
                            eAudiometria.Add("ATANI1500");
                            eAudiometria.Add("ATANI2000");
                            eAudiometria.Add("ATANI3000");
                            eAudiometria.Add("ATANI4000");
                            eAudiometria.Add("ATANI6000");
                            eAudiometria.Add("ATANI8000");

                            eAudiometria.Add("ATAED125");
                            eAudiometria.Add("ATAED250");
                            eAudiometria.Add("ATAED500");
                            eAudiometria.Add("ATAED750");
                            eAudiometria.Add("ATAED1000");
                            eAudiometria.Add("ATAED1500");
                            eAudiometria.Add("ATAED2000");
                            eAudiometria.Add("ATAED3000");
                            eAudiometria.Add("ATAED4000");
                            eAudiometria.Add("ATAED6000");
                            eAudiometria.Add("ATAED8000");

                            eAudiometria.Add("ATAEI125");
                            eAudiometria.Add("ATAEI250");
                            eAudiometria.Add("ATAEI500");
                            eAudiometria.Add("ATAEI750");
                            eAudiometria.Add("ATAEI1000");
                            eAudiometria.Add("ATAEI1500");
                            eAudiometria.Add("ATAEI2000");
                            eAudiometria.Add("ATAEI3000");
                            eAudiometria.Add("ATAEI4000");
                            eAudiometria.Add("ATAEI6000");
                            eAudiometria.Add("ATAEI8000");

                            eAudiometria.Add("ATOND125");
                            eAudiometria.Add("ATOND250");
                            eAudiometria.Add("ATOND500");
                            eAudiometria.Add("ATOND750");
                            eAudiometria.Add("ATOND1000");
                            eAudiometria.Add("ATOND1500");
                            eAudiometria.Add("ATOND2000");
                            eAudiometria.Add("ATOND3000");
                            eAudiometria.Add("ATOND4000");
                            eAudiometria.Add("ATOND6000");
                            eAudiometria.Add("ATOND8000");

                            eAudiometria.Add("ATONI125");
                            eAudiometria.Add("ATONI250");
                            eAudiometria.Add("ATONI500");
                            eAudiometria.Add("ATONI750");
                            eAudiometria.Add("ATONI1000");
                            eAudiometria.Add("ATONI1500");
                            eAudiometria.Add("ATONI2000");
                            eAudiometria.Add("ATONI3000");
                            eAudiometria.Add("ATONI4000");
                            eAudiometria.Add("ATONI6000");
                            eAudiometria.Add("ATONI8000");

                            eAudiometria.Add("ATOED125");
                            eAudiometria.Add("ATOED250");
                            eAudiometria.Add("ATOED500");
                            eAudiometria.Add("ATOED750");
                            eAudiometria.Add("ATOED1000");
                            eAudiometria.Add("ATOED1500");
                            eAudiometria.Add("ATOED2000");
                            eAudiometria.Add("ATOED3000");
                            eAudiometria.Add("ATOED4000");
                            eAudiometria.Add("ATOED6000");
                            eAudiometria.Add("ATOED8000");

                            eAudiometria.Add("ATOEI125");
                            eAudiometria.Add("ATOEI250");
                            eAudiometria.Add("ATOEI500");

                            eAudiometria.Add("ATOEI750");
                            eAudiometria.Add("ATOEI1000");
                            eAudiometria.Add("ATOEI1500");
                            eAudiometria.Add("ATOEI2000");
                            eAudiometria.Add("ATOEI3000");
                            eAudiometria.Add("ATOEI4000");
                            eAudiometria.Add("ATOEI6000");
                            eAudiometria.Add("ATOEI8000");
                            eAudiometria.Add("DESCATOTOD");
                            eAudiometria.Add("OBSATOTOD");

                            eAudiometria.Add("DESCATOTOI");
                            eAudiometria.Add("OBSATOTOI");

                            var xeAudiometria = new XElement("Audiometría");

                            foreach (var eCampo in eAudiometria)
                            {
                                xeAudiometria.Add(new XElement(eCampo));
                            }

                        foreach (var elementName in eAudiometria)
                        {
                            switch (elementName)
                            {
                                case "ATAND125":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_125) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_125).v_Value1;
                                    break;
                                case "ATAND250":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_250) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_250).v_Value1;
                                    break;
                                case "ATAND500":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_500) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_500).v_Value1;
                                    break;
                                case "ATAND750":
                                    xeAudiometria.Element(elementName).Value = "";//audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_125).v_Value1;
                                    break;
                                case "ATAND1000":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_1000) == null ? "": audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_1000).v_Value1;
                                    break;
                                case "ATAND1500":
                                    xeAudiometria.Element(elementName).Value = "";//audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_125).v_Value1;
                                    break;
                                case "ATAND2000":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_2000) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_2000).v_Value1;
                                    break;
                                case "ATAND3000":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_3000) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_3000).v_Value1;
                                    break;
                                case "ATAND4000":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_4000) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_4000).v_Value1;
                                    break;
                                case "ATAND6000":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_6000) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_6000).v_Value1;
                                    break;
                                case "ATAND8000":
                                    xeAudiometria.Element(elementName).Value = audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_8000) == null ? "" : audiometriaValores.Find(p => p.v_ComponentFieldId == Constants.txt_VA_OD_8000).v_Value1;
                                    break;


                                default:
                                    break;
                            }
                        }
                        xeRoot.Add(new XElement(xeAudiometria));

                        }

                        #endregion
                           
                        #region Antecedentes
                        var constantes = new ServiceBL().ValoresComponenteconstantes(_serviceId);
                        if (constantes.Count > 0)
                        {
                            var eConstantes = new List<string>();
                            eConstantes.Add("Peso");
                            eConstantes.Add("Talla");
                            eConstantes.Add("IMC");
                            eConstantes.Add("PresionSistolica");
                            eConstantes.Add("PresionDiastolica");
                            eConstantes.Add("Respiracion");
                            eConstantes.Add("Pulso");
                            eConstantes.Add("SaturacionOxigeno");
                            eConstantes.Add("Cintura");
                            eConstantes.Add("Cadera");
                            eConstantes.Add("ICC");
                            eConstantes.Add("Temperatura");
                            eConstantes.Add("FechaUltimaRegla");

                            var xeConstantes = new XElement("Constantes");

                            foreach (var eConstante in eConstantes)
                            {
                                xeConstantes.Add(new XElement(eConstante));
                            }

                            foreach (var elementName in eConstantes)
                            {
                                switch (elementName)
                                {
                                    case "Peso":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
                                        break;
                                    case "Talla":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_TALLA_ID).v_Value1;
                                        break;
                                    case "IMC":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_IMC_ID).v_Value1;
                                        break;
                                    case "PresionSistolica":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.FUNCIONES_VITALES_PAS_ID).v_Value1;
                                        break;
                                    case "PresionDiastolica":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.FUNCIONES_VITALES_PAD_ID).v_Value1;
                                        break;
                                    case "Respiracion":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.FUNCIONES_VITALES_FREC_RESPIRATORIA_ID).v_Value1;
                                        break;
                                    case "Pulso":
                                        xeConstantes.Element(elementName).Value = "";//constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
                                        break;
                                    case "SaturacionOxigeno":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.FUNCIONES_VITALES_SAT_O2_ID).v_Value1;
                                        break;
                                    case "Cintura":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_INDICE_CINTURA_ID).v_Value1;
                                        break;
                                    case "Cadera":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_PERIMETRO_CADERA_ID).v_Value1;
                                        break;
                                    case "ICC":
                                        xeConstantes.Element(elementName).Value = "";//constantes.Find(p => p.v_ComponentFieldId == Constants.ANTROPOMETRIA_PESO_ID).v_Value1;
                                        break;
                                    case "Temperatura":
                                        xeConstantes.Element(elementName).Value = constantes.Find(p => p.v_ComponentFieldId == Constants.FUNCIONES_VITALES_TEMPERATURA_ID).v_Value1;
                                        break;
                                    case "FechaUltimaRegla":
                                        xeConstantes.Element(elementName).Value = datosPaciente.FechaUltimaRegla == null ? "" : datosPaciente.FechaUltimaRegla;
                                        break;
                                    default:
                                        break;
                                }
                                         
                            }
                            xeRoot.Add(new XElement(xeConstantes));

                        }

                        #endregion

                        #region Datos Episodio

                            var eDatosEpisodio = new List<string>();
                            eDatosEpisodio.Add("EmpresaTitular");
                            eDatosEpisodio.Add("EmpTitularRUC");
                            eDatosEpisodio.Add("Contratista");
                            eDatosEpisodio.Add("ContratanteCodigo");
                            eDatosEpisodio.Add("Unidad");
                            eDatosEpisodio.Add("UnidadCodigo");
                            eDatosEpisodio.Add("Ocupacion");
                            eDatosEpisodio.Add("GradoInstruccion");
                            eDatosEpisodio.Add("ZonaTrabajo");
                            eDatosEpisodio.Add("AreaTrabajo");
                            eDatosEpisodio.Add("FechaExamen");
                            eDatosEpisodio.Add("TipodeExamen");
                            eDatosEpisodio.Add("TipoTarea");
                            eDatosEpisodio.Add("Observaciones");
                            eDatosEpisodio.Add("Vigencia");
                            eDatosEpisodio.Add("Caducidad");


                            var xeDatosEpisodio = new XElement("DatosEpisodio");

                            foreach (var eDatoEpisodio in eDatosEpisodio)
                            {
                                xeDatosEpisodio.Add(new XElement(eDatoEpisodio));
                            }

                            foreach (var elementName in eDatosEpisodio)
                            {
                                switch (elementName)
                                {
                                    case "EmpresaTitular":
                                        xeDatosEpisodio.Element(elementName).Value = "";// datosPaciente.Direccion;
                                        break;
                                    case "EmpTitularRUC":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "Contratista":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "ContratanteCodigo":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "Unidad":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "UnidadCodigo":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "Ocupacion":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "GradoInstruccion":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "ZonaTrabajo":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "AreaTrabajo":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "FechaExamen":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "TipodeExamen":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "TipoTarea":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "Observaciones":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "Vigencia":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    case "Caducidad":
                                        xeDatosEpisodio.Element(elementName).Value =  "";//datosPaciente.Direccion;
                                        break;
                                    default:
                                        break;
                                }

                            }
                            xeRoot.Add(new XElement(xeDatosEpisodio));


                        #endregion
                           
                        #region DOCIMG
                        var docimgs = new ServiceBL().GetFilePdfsByServiceId(ref objOperationResult, _serviceId);
                        if (docimgs.Count > 0)
                        {
                            var eDocimgs = new List<string>();
                            eDocimgs.Add("Fecha");
                            eDocimgs.Add("Tipo");
                            eDocimgs.Add("Codigo");
                            eDocimgs.Add("Titulo");
                            eDocimgs.Add("Observaciones");
                            eDocimgs.Add("IdentificadorDocumento");

                            var xeDocimg = new XElement("DOCIMG");

                            foreach (var eDocimg in eDocimgs)
                            {
                                xeDocimg.Add(new XElement(eDocimg));
                            }

                            foreach (var docimg in docimgs)
                            {
                                //Crear Carpeta
                                var ext = Path.GetExtension(docimg.v_FileName);
                                var splitName = docimg.v_FileName.Split('-');
                                var consultorio = splitName[2].Substring(0, splitName[2].Length - ext.Length);
                                CreateEmptyFile(folderBrowserDialog1.SelectedPath + @"\" + consultorio, consultorio, docimg.v_FileName, ext);

                                foreach (var elementName in eDocimgs)
                                {
                                    switch (elementName)
                                    {
                                        case "Fecha":
                                            xeDocimg.Element(elementName).Value = docimg.FechaServicio.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "Tipo":
                                            xeDocimg.Element(elementName).Value = ObtenerTipoEstudio(docimg.v_FileName);
                                            break;
                                        case "Codigo":
                                            xeDocimg.Element(elementName).Value = ObtenerCodigo(docimg.v_FileName);
                                            break;
                                        case "Titulo":
                                            xeDocimg.Element(elementName).Value = docimg.v_FileName;
                                            break;
                                        case "Observaciones":
                                            xeDocimg.Element(elementName).Value = "";
                                            break;
                                        case "IdentificadorDocumento":
                                            xeDocimg.Element(elementName).Value = docimg.v_FileName;
                                            break;
                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeDocimg));
                                }
                            }

                        }

                    #endregion

                        #region ExFISICO

                        var examenFisico = new ServiceBL().ValoresComponenteExamenFisico(_serviceId);
                        var examenFisicoId = examenFisico == null ? "": examenFisico[0].v_ComponentId;
                        #region ...
                            var eExFisico = new List<string>();
                            eExFisico.Add("CodAnamnesis");
                            eExFisico.Add("ObsAnamnesis");
                            eExFisico.Add("CodEctoscopia");
                            eExFisico.Add("ObsEctoscopia");
                            eExFisico.Add("CodPiel");
                            eExFisico.Add("ObsPiel");
                            eExFisico.Add("CodCabellos");
                            eExFisico.Add("ObsCabellos");
                            eExFisico.Add("CodOidos");
                            eExFisico.Add("ObsOidos");
                            eExFisico.Add("CodCabeza");
                            eExFisico.Add("ObsCabeza");
                            eExFisico.Add("CodCuello");
                            eExFisico.Add("ObsCuello");

                            eExFisico.Add("CodNariz");
                            eExFisico.Add("ObsNariz");
                            eExFisico.Add("CodBoca");
                            eExFisico.Add("ObsBoca");
                            eExFisico.Add("CodFaringe");
                            eExFisico.Add("ObsFaringe");
                            eExFisico.Add("CodLaringe");
                            eExFisico.Add("ObsLaringe");
                            eExFisico.Add("CodAparatoRespiratorio");
                            eExFisico.Add("ObsAparatoRespiratorio");

                            eExFisico.Add("CodAparatoCardiovascular");
                            eExFisico.Add("ObsAparatoCardiovascular");
                            eExFisico.Add("CodAbdomen");
                            eExFisico.Add("DesAbdomen");
                            eExFisico.Add("CodOrganosGenitales");
                            eExFisico.Add("DesOrganosGenitales");
                            eExFisico.Add("CodMSD");
                            eExFisico.Add("ObsMSD");
                            eExFisico.Add("CodMSI");
                            eExFisico.Add("ObsMSI");

                            eExFisico.Add("CodMID");
                            eExFisico.Add("ObsMID");
                            eExFisico.Add("CodMII");
                            eExFisico.Add("ObsMII");
                            eExFisico.Add("CodMovimientoFuerza");
                            eExFisico.Add("ObsMovimientoFuerza");
                            eExFisico.Add("CodReflejosOsteotendinos");
                            eExFisico.Add("ObsReflejosOsteotendinos");
                            eExFisico.Add("CodMarcha");
                            eExFisico.Add("ObsMarcha");

                            eExFisico.Add("CodColumnaVertebral");
                            eExFisico.Add("ObsColumnaVertebral");
                            eExFisico.Add("CodTactoRectal");
                            eExFisico.Add("ObsTactoRectal");
                            eExFisico.Add("CodAnillosInguinales");
                            eExFisico.Add("ObsAnillosInguinales");
                            eExFisico.Add("CodSistemaLinfatico");
                            eExFisico.Add("ObsSistemaLinfatico");
                            eExFisico.Add("CodHernias");
                            eExFisico.Add("ObsHernias");

                            eExFisico.Add("CodVarices");
                            eExFisico.Add("ObsVarices");
                            eExFisico.Add("CodGanglios");
                            eExFisico.Add("ObsGanglios");
                            eExFisico.Add("CodReflejosPupilaresOD");
                            eExFisico.Add("bsReflejosPupilaresOD");
                            eExFisico.Add("CodReflejosPupilaresOI");
                            eExFisico.Add("ObsReflejosPupilaresOI");
                            eExFisico.Add("CodOjos");
                            eExFisico.Add("DesOjos");

                        

                        #endregion
                            var xeExFISICO = new XElement("ExFISICO");

                            foreach (var itemExFisico in eExFisico)
                            {
                                xeExFISICO.Add(new XElement(itemExFisico));
                            }

                        #region ...
                            foreach (var elementName in eExFisico)
                            {
                                switch (elementName)
                                {
                                    case "CodAnamnesis":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "ObsAnamnesis":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "CodEctoscopia":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_FISICO_ECTOSCOPIA_ID) == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_FISICO_ECTOSCOPIA_GENERAL_DESCRIPCION_ID).v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "ObsEctoscopia":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "CodPiel":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "ObsPiel":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "CodCabellos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "ObsCabellos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "CodOidos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "ObsOidos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;
                                    case "CodCabeza":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_FISICO_7C_CABEZA_ID) == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_FISICO_7C_CABEZA_ID).v_Value1Name;

                                        break;
                                    case "ObsCabeza":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION) == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == Constants.EXAMEN_FISICO_7C_EXAMEN_FISICO_CABEZA_DESCRIPCION).v_Value1;

                                        break;
                                    case "CodCuello":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                        break;

                                    case "ObsCuello":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    //---------------

                                    case "CodNariz":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsNariz":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodBoca":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsBoca":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodFaringe":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsFaringe":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodLaringe":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsLaringe":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodAparatoRespiratorio":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsAparatoRespiratorio":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    //---------------

                                    case "CodAparatoCardiovascular":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsAparatoCardiovascular":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodAbdomen":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "DesAbdomen":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodOrganosGenitales":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "DesOrganosGenitales":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodMSD":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsMSD":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodMSI":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsMSI":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    //---------------

                                    case "CodMID":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsMID":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodMII":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsMII":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodMovimientoFuerza":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsMovimientoFuerza":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodReflejosOsteotendinos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsReflejosOsteotendinos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodMarcha":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsMarcha":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    //---------------

                                    case "CodColumnaVertebral":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsColumnaVertebral":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodTactoRectal":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsTactoRectal":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodAnillosInguinales":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsAnillosInguinales":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodSistemaLinfatico":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsSistemaLinfatico":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodHernias":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsHernias":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    //---------------

                                    case "CodVarices":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsVarices":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodGanglios":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsGanglios":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodReflejosPupilaresOD":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "bsReflejosPupilaresOD":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodReflejosPupilaresOI":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "ObsReflejosPupilaresOI":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "CodOjos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;

                                    case "DesOjos":
                                        if (examenFisicoId == Constants.EXAMEN_FISICO_ID)
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        else
                                            xeExFISICO.Element(elementName).Value = examenFisico.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenFisico.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                        break;


                                    default: break;
                                }

                            }
                        

                    #endregion

                        xeRoot.Add(new XElement(xeExFISICO));
                    #endregion

                        #region Examen Ocular VB
                        var examenOcular = new ServiceBL().ValoresComponenteExamenOcular(_serviceId);
                        var examenOftalmoId = examenOcular == null ? "" : examenOcular[0].v_ComponentId;
// N009-MF000003547	VISIÓNDECERCA SC OD
//N009-MF000003548	VISIÓNDECERCA SC OI
//N009-MF000003549	VISIÓNDECERCA CC OD
//N009-MF000003550	VISIÓNDECERCA CC OI
//N009-MF000003551	VISIÓNDELEJOS SC OD
//N009-MF000003552	VISIÓNDELEJOS SC OI
//N009-MF000003553	VISIÓNDELEJOS CC OD
//N009-MF000003554	VISIÓNDELEJOS CC OI

        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VL SC OD = "N009-MF000003568";
        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VL SC OI = "N009-MF000003566";
        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VL CC OD = "N009-MF000003567";
        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VL CC OI = "N009-MF000003568";

        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VC SC OD = "N009-MF000003569";
        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VC SC OI = "N009-MF000003570";
        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VC CC OD = "N009-MF000003571";
        //public const string EXAMEN_OFTALMOLOGICO_COMPLETO_VC CC OI = "N009-MF000003572";

//N009-MF000003614	VISIÓNDECERCA SC OD
//N009-MF000003615	VISIÓNDECERCA SC OI
//N009-MF000003616	VISIÓNDECERCA CC OD
//N009-MF000003617	VISIÓNDECERCA CC OI

//N009-MF000003618	VISIÓNDELEJOS SC OD
//N009-MF000003619	VISIÓNDELEJOS SC OI
//N009-MF000003620	VISIÓNDELEJOS CC OD
//N009-MF000003621	VISIÓNDELEJOS C COI

                        var eExOcularVB = new List<string>();
                        eExOcularVB.Add("VisionCercaODSCVB");
                        eExOcularVB.Add("VisionCercaOISCVB");
                        eExOcularVB.Add("VisionLejosODSCVB");
                        eExOcularVB.Add("VisionLejosOISCVB");
                        eExOcularVB.Add("VisionCercaODCVB");
                        eExOcularVB.Add("VisionCercaOICVB");
                        eExOcularVB.Add("VisionLejosODCVB");
                        eExOcularVB.Add("VisionLejosOICVB");
                        eExOcularVB.Add("ProvinciaNacimiento");
                        eExOcularVB.Add("TestColores");
                        eExOcularVB.Add("RestriccionActual");
                        eExOcularVB.Add("TipoRestriccion");

                        var xeExOcularVB = new XElement("ExOcularVB");

                        foreach (var itemeExOcularVB in eExOcularVB)
                        {
                            xeExOcularVB.Add(new XElement(itemeExOcularVB));
                        }


                        foreach (var elementName in eExOcularVB)
                        {
                            switch (elementName)
                            {
                                case "VisionCercaODSCVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003547") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003547").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003569") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003569").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003614") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003614").v_Value1;

                                    break;
                                case "VisionCercaOISCVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003548") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003548").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003570") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003570").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003615") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003615").v_Value1;

                                    break;
                                case "VisionLejosODSCVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003551") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003551").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003568") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003568").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003618") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003618").v_Value1;

                                    break;
                                case "VisionLejosOISCVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003552") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003552").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003566") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003566").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003619") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003619").v_Value1;

                                    break;
                                case "VisionCercaODCVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003549") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003549").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003571") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003571").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003616") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003616").v_Value1;

                                    break;
                                case "VisionCercaOICVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003550") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003550").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003572") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003572").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003617") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003617").v_Value1;

                                    break;
                                case "VisionLejosODCVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003553") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003553").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003567") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003567").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003620") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003620").v_Value1;

                                    break;
                                case "VisionLejosOICVB":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003554") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003554").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003568") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003568").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003621") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "N009-MF000003621").v_Value1;

                                    break;
                                case "ProvinciaNacimiento":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                    break;
                                case "TestColores":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                    break;
                                case "RestriccionActual":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                    break;
                                case "TipoRestriccion":
                                    if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;
                                    else if (examenOftalmoId == Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                                        xeExOcularVB.Element(elementName).Value = examenOcular.Find(p => p.v_ComponentFieldId == "") == null ? "" : examenOcular.Find(p => p.v_ComponentFieldId == "").v_Value1;

                                    break;
                              
                                default: break;
                            }

                        }
                        xeRoot.Add(new XElement(xeExOcularVB));

                        #endregion

                        #region Hábitos
                        var habitos = new HistoryBL().GetNoxiousHabitsPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "i_TypeHabitsId DESC", null, _PacientId);
                        if (habitos.Count > 0)
                        {
                            var eHabitos = new List<string>();
                            eHabitos.Add("Descripcion");
                            eHabitos.Add("Vigencia");
                            eHabitos.Add("TipoHabito");
                            eHabitos.Add("FechaInicio");
                            eHabitos.Add("FechaFin");

                            var xeHabitos = new XElement("HABITOS_TOXICOS");

                            foreach (var eAntecedente in eHabitos)
                            {
                                xeHabitos.Add(new XElement(eAntecedente));
                            }

                            foreach (var habito in habitos)
                            {
                                foreach (var elementName in eHabitos)
                                {
                                    switch (elementName)
                                    {
                                        case "Descripcion":
                                            xeHabitos.Element(elementName).Value = habito.v_TypeHabitsName;
                                            break;
                                        case "Vigencia":
                                            xeHabitos.Element(elementName).Value = "S";
                                            break;
                                        case "TipoHabito":
                                            xeHabitos.Element(elementName).Value = CodigoNatclarHabito(habito.i_TypeHabitsId);
                                            break;
                                        case "FechaInicio":
                                            xeHabitos.Element(elementName).Value = "";
                                            break;
                                        case "FechaFin":
                                            xeHabitos.Element(elementName).Value = "";
                                            break;
                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeHabitos));
                                }
                            }

                        }

                        #endregion

                        #region Antecedentes Familiares
                        var antecedentesFamiliares = new HistoryBL().GetFamilyMedicalAntecedentsPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "v_DiseasesId DESC", null, _PacientId);
                        if (antecedentesFamiliares.Count > 0)
                        {

                            var eAntecedentesFamiliares = new List<string>();
                            eAntecedentesFamiliares.Add("CodigoCIE");
                            eAntecedentesFamiliares.Add("Descripcion");
                            eAntecedentesFamiliares.Add("FechaInicio");
                            eAntecedentesFamiliares.Add("FechaFin");

                            var xeAntecedente = new XElement("HISTORIA_FAMILIAR");

                            foreach (var eAntecedente in eAntecedentesFamiliares)
                            {
                                xeAntecedente.Add(new XElement(eAntecedente));
                            }

                            foreach (var antecedente in antecedentesFamiliares)
                            {
                                foreach (var elementName in eAntecedentesFamiliares)
                                {
                                    switch (elementName)
                                    {
                                        case "CodigoCIE":
                                            xeAntecedente.Element(elementName).Value = antecedente.v_CIE10Id;
                                            break;
                                        case "Descripcion":
                                            xeAntecedente.Element(elementName).Value = antecedente.v_Comment;
                                            break;
                                        case "FechaInicio":
                                            xeAntecedente.Element(elementName).Value = "";
                                            break;
                                        case "FechaFin":
                                            xeAntecedente.Element(elementName).Value = "";
                                            break;
                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeAntecedente));
                                }
                            }

                        }

                        #endregion

                        #region Antecedentes Fisiologico

                        var antecedentesFisiologicos = new List<FamilyMedicalAntecedentsList>();// new HistoryBL().GetFamilyMedicalAntecedentsPagedAndFilteredByPersonId(ref objOperationResult, 0, null, "d_StartDate DESC", null, _PacientId);
                  
                            var eAntecedentesFisiologico = new List<string>();
                            eAntecedentesFisiologico.Add("Descripcion");
                            eAntecedentesFisiologico.Add("Tipo");
                            eAntecedentesFisiologico.Add("FechaInicio");
                            eAntecedentesFisiologico.Add("FechaFin");
                            eAntecedentesFisiologico.Add("Vigencia");

                            var xeAntecedenteFisiologico = new XElement("HISTORIAL_FISIOLOGICO");

                            foreach (var eAntecedente in eAntecedentesFisiologico)
                            {
                                xeAntecedenteFisiologico.Add(new XElement(eAntecedente));
                            }

                            foreach (var antecedente in antecedentesFisiologicos)
                            {
                                foreach (var elementName in eAntecedentesFisiologico)
                                {
                                    switch (elementName)
                                    {
                                        case "Descripcion":
                                            xeAntecedenteFisiologico.Element(elementName).Value = "";
                                            break;
                                        case "Tipo":
                                            xeAntecedenteFisiologico.Element(elementName).Value = "";
                                            break;
                                        case "FechaInicio":
                                            xeAntecedenteFisiologico.Element(elementName).Value = "";
                                            break;
                                        case "FechaFin":
                                            xeAntecedenteFisiologico.Element(elementName).Value = "";
                                            break;
                                        case "Vigencia":
                                            xeAntecedenteFisiologico.Element(elementName).Value = "";
                                            break;
                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeAntecedenteFisiologico));
                                }
                            }


                        #endregion

                        #region Historia Laboral

                            var historiaLaboral = new HistoryBL().GetHistoryPagedAndFiltered(ref objOperationResult, 0, null, "d_StartDate DESC", null, _PacientId);

                            var eHistoriaLaboral = new List<string>();
                            eHistoriaLaboral.Add("EmpresaLaboral");
                            eHistoriaLaboral.Add("RCTEmpresa");
                            eHistoriaLaboral.Add("PuestosHistorial");
                            eHistoriaLaboral.Add("DuracionPuestoA");
                            eHistoriaLaboral.Add("DuracionPuestoM");
                            eHistoriaLaboral.Add("FechaInicioPuesto");
                            eHistoriaLaboral.Add("FechaFinPuesto");
                            eHistoriaLaboral.Add("ActividadCNAE");
                            eHistoriaLaboral.Add("PuestoActual");
                            eHistoriaLaboral.Add("DescripcionTareas");

                            eHistoriaLaboral.Add("ValoracionRiesgo");
                            eHistoriaLaboral.Add("AreaTrabajo");
                            eHistoriaLaboral.Add("ZonaLaboral");
                            eHistoriaLaboral.Add("AlturaLaboral");
                            eHistoriaLaboral.Add("DuracionTrabajosAlturaA");
                            eHistoriaLaboral.Add("DuracionTrabajosAlturaM");
                            eHistoriaLaboral.Add("PostulaPuesto");


                            var xeHistoriaLaboral = new XElement("HISTORIA_LABORAL");

                            foreach (var eLaboral in eAntecedentesFisiologico)
                            {
                                xeHistoriaLaboral.Add(new XElement(eLaboral));
                            }

                            foreach (var itemLaboral in historiaLaboral)
                            {
                                foreach (var elementName in eAntecedentesFisiologico)
                                {
                                    switch (elementName)
                                    {
                                        case "EmpresaLaboral":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.v_Organization;
                                            break;
                                        case "RCTEmpresa":
                                            xeHistoriaLaboral.Element(elementName).Value = "";
                                            break;
                                        case "PuestosHistorial":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.v_TypeActivity;
                                            break;
                                        case "DuracionPuestoA":
                                            xeHistoriaLaboral.Element(elementName).Value = Common.Utils.YearMonthDiff(itemLaboral.d_EndDate.Value, itemLaboral.d_StartDate.Value).Years.ToString();
                                            break;
                                        case "DuracionPuestoM":
                                            xeHistoriaLaboral.Element(elementName).Value = Common.Utils.YearMonthDiff(itemLaboral.d_EndDate.Value, itemLaboral.d_StartDate.Value).TotalMonths.ToString();
                                            break;
                                        case "FechaInicioPuesto":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.d_StartDate.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "FechaFinPuesto":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.d_EndDate.Value.ToString("dd/MM/yyyy");
                                            break;
                                        case "ActividadCNAE":
                                            xeHistoriaLaboral.Element(elementName).Value = "";
                                            break;
                                        case "PuestoActual":
                                            xeHistoriaLaboral.Element(elementName).Value =  itemLaboral.i_Trabajo_Actual == 0 ?"N" : "S";
                                            break;
                                        case "DescripcionTareas":
                                            xeHistoriaLaboral.Element(elementName).Value = "";
                                            break;
                                        case "ValoracionRiesgo":
                                            xeHistoriaLaboral.Element(elementName).Value = "";
                                            break;
                                        case "AreaTrabajo":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.v_workstation;
                                            break;
                                        case "ZonaLaboral":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.v_TypeOperationName;
                                            break;
                                        case "AlturaLaboral":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.i_GeografixcaHeight.ToString();
                                            break;
                                        case "DuracionTrabajosAlturaA":
                                            xeHistoriaLaboral.Element(elementName).Value = "";
                                            break;
                                        case "DuracionTrabajosAlturaM":
                                            xeHistoriaLaboral.Element(elementName).Value = "";
                                            break;
                                        case "PostulaPuesto":
                                            xeHistoriaLaboral.Element(elementName).Value = itemLaboral.i_Trabajo_Actual == 0 ? "S" : "N";
                                            break;

                                        default:
                                            break;
                                    }
                                    xeRoot.Add(new XElement(xeAntecedenteFisiologico));
                                }
                            }


                        #endregion

                        #region Lectura OIT
                        var examenOIT = new ServiceBL().ValoresComponente(_serviceId, Constants.OIT_ID);
                        if (examenOIT.Count > 0)
                        {
                            var eLecturaOIT = new List<string>();
                            eLecturaOIT.Add("Calidad");
                            eLecturaOIT.Add("ObservacionCalidad");
                            eLecturaOIT.Add("AnormalidadParenquitamosa");
                            eLecturaOIT.Add("OpacidadesPequeñas");
                            eLecturaOIT.Add("OpacidadesAbundancia");
                            eLecturaOIT.Add("AnormalidadPleural");
                            eLecturaOIT.Add("DescripcionAnormalidadPleural");
                            eLecturaOIT.Add("OtrasAnomalias");
                            eLecturaOIT.Add("DescripcionOtrasAnomalias");

                            var xeLecturaOIT = new XElement("LecturasOIT");

                            foreach (var itemLecturaOIT in eLecturaOIT)
                            {
                                xeLecturaOIT.Add(new XElement(itemLecturaOIT));
                            }

                            foreach (var elementName in eLecturaOIT)
                            {
                                switch (elementName)
                                {
                                    case "Calidad":
                                        xeLecturaOIT.Element(elementName).Value = examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_CALIDAD_ID).v_Value1Name;
                                        break;
                                    case "ObservacionCalidad":
                                        xeLecturaOIT.Element(elementName).Value = examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_COMENTARIOS_ID).v_Value1;
                                        break;
                                    case "AnormalidadParenquitamosa":
                                        xeLecturaOIT.Element(elementName).Value = examenOIT.Find(p => p.v_ComponentFieldId == Constants.RX_0_0_ID).v_Value1 != "1" ? "S" : "N";
                                        break;
                                    case "OpacidadesPequeñas":
                                        xeLecturaOIT.Element(elementName).Value = ObtenerOpacidadesPequenias(examenOIT);
                                        break;
                                    case "OpacidadesAbundancia":
                                        xeLecturaOIT.Element(elementName).Value = ObtenerOpacidadesAbundancia(examenOIT);
                                        break;
                                    case "AnormalidadPleural":
                                        xeLecturaOIT.Element(elementName).Value = ObtenerAnormalidadPleural(examenOIT);
                                        break;
                                    case "DescripcionAnormalidadPleural":
                                        xeLecturaOIT.Element(elementName).Value = "";//WALTER FALTA CREAR CAMPO;
                                        break;
                                    case "OtrasAnomalias":
                                        xeLecturaOIT.Element(elementName).Value = "";//WALTER FALTA CREAR CAMPO;
                                        break;
                                    case "DescripcionOtrasAnomalias":
                                        xeLecturaOIT.Element(elementName).Value = "";//WALTER FALTA CREAR CAMPO;
                                        break;

                                    default: break;
                                }

                            }
                            xeRoot.Add(new XElement(xeLecturaOIT));

                        }
                        #endregion

                        #region Psicologia
                        var examenPsicologia = new ServiceBL().ValoresComponenteExamenFisico(_serviceId);//AMC?
                        if (examenPsicologia.Count > 0)
                        {
                            var ePsicologia = new List<string>();
                            ePsicologia.Add("Informe");
                            ePsicologia.Add("Recomendaciones");
                            ePsicologia.Add("Aptitud");

                            var xePsicologia = new XElement("Psicologia");

                            foreach (var itemPsicologia in ePsicologia)
                            {
                                xePsicologia.Add(new XElement(itemPsicologia));
                            }

                            foreach (var elementName in ePsicologia)
                            {
                                switch (elementName)
                                {
                                    case "Informe":
                                        xePsicologia.Element(elementName).Value = "";
                                        break;
                                    case "Recomendaciones":
                                        xePsicologia.Element(elementName).Value = "";
                                        break;
                                    case "Aptitud":
                                        xePsicologia.Element(elementName).Value = "";
                                        break;
                                    
                                    default: break;
                                }

                            }
                            xeRoot.Add(new XElement(xePsicologia));

                        }
                        #endregion

                    #endregion

                    xeRoot.Save(folderBrowserDialog1.SelectedPath + @"\" + datosPaciente.Hc);

                    WSRIProveedorExternoClient client = new WSRIProveedorExternoClient();

                        //client.EnviarDatosAPMedicos(
                 
                    }
                }
            }

            MessageBox.Show("Se generó los archivos XML", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ObtenerAnormalidadPleural(List<ServiceComponentFieldValuesList> listaValoresOit)
        {
            string[] Ids = new string[] 
            { 
                Constants.RX_SIMBOLO_SI_ID,
                "N009-MF000003194"
            };
            var idsPleural = listaValoresOit.FindAll(p => Ids.Contains(p.v_ComponentFieldId));
            var ResultCode = idsPleural.Find(p => p.v_Value1 == "1").v_ComponentFielName.Substring(0,1);

            return ResultCode;

        }

        private string ObtenerOpacidadesAbundancia(List<ServiceComponentFieldValuesList> listaValoresOit)
        {
            string[] Ids = new string[] 
            { 
                Constants.RX_0_NADA_ID,
                Constants.RX_0_0_ID,
                Constants.RX_0_1_ID,

                Constants.RX_1_0_ID,
                Constants.RX_1_1_ID,
                Constants.RX_1_2_ID,

                Constants.RX_2_1_ID,
                Constants.RX_2_2_ID,
                Constants.RX_2_3_ID,

                Constants.RX_3_2_ID,
                Constants.RX_3_3_ID,
                Constants.RX_3_MAS_ID
            };

            var idsOpacidadPeque = listaValoresOit.FindAll(p => Ids.Contains(p.v_ComponentFieldId));
            var ResultCode = idsOpacidadPeque.Find(p => p.v_Value1 == "1").v_ComponentFielName.Trim().Replace('-','/');

            if (ResultCode == "0//")
            {
                ResultCode = "01";
            }
            else if (ResultCode == "0/0")
            {
                ResultCode = "02";
            }
            //WALTER
            return ResultCode;
        }

        private string ObtenerOpacidadesPequenias(List<ServiceComponentFieldValuesList> listaValoresOit)
        {
            string[] Ids = new string[] 
            { 
                Constants.RX_INFERIOR_DERECHO_ID,
                Constants.RX_INFERIOR_IZQUIERDO_ID,
                Constants.RX_MEDIO_DERECHO_ID,

                Constants.RX_MEDIO_IZQUIERDO_ID,
                Constants.RX_SUPERIOR_DERECHO_ID,
                Constants.RX_SUPERIOR_IZQUIERDO_ID

            };

            var idsOpacidadPeque = listaValoresOit.FindAll(p => Ids.Contains( p.v_ComponentFieldId));
            var ResultCode = idsOpacidadPeque.Find(p => p.v_Value1 == "1").v_ComponentFielName;

            return ResultCode;
        }

        private string CodigoNatclarHabito(int tipoHabitoId)
        {
            var codigoNatclar = "";
            switch (tipoHabitoId)
            {
                case (int)TypeHabit.Tabaco:
                    codigoNatclar = "T";
                    break;
                case (int)TypeHabit.Alcohol:
                    codigoNatclar = "A";
                    break;
                case (int)TypeHabit.Drogas:
                    codigoNatclar = "D";
                    break;
                case (int)TypeHabit.ActividadFisica:
                    codigoNatclar = "";
                    break;
            }
            return codigoNatclar;
        }
        
        private string ObtenerTipoEstudio(string fileName)
        {
            var ext = Path.GetExtension(fileName);
            switch (ext)
            {
                case ".pdf":
                    return "1";
                case ".dcm":
                    return "2";
                default:
                    return "";
            }
        }

        private string ObtenerCodigo(string fileName)
        {
            var codigo = fileName.Split('-')[1].Substring(0, fileName.Split('-')[1].Length - 4);
            switch (codigo)
            {
                case "ESPIROMETRÍA":
                    return "6";
                case "CARDIOLOGÍA":
                    return "7";
                case "LABORATORIO":
                    return "19";
                default:
                    return "";
            }
        }

        //Walter Natclar Laboratorio
        private string CodigoNatclarLaboratorio(string Id)
        {
            var prefijo = Id.Split('-')[1].Substring(0,2);

            var codigoNat = "";
            if (prefijo == "ME")
            {
                #region Codigos Perfiles
                if (Id == Constants.PERFIL_LIPIDICO)
                {
                    codigoNat = "000001";
                }
                else if (Id == Constants.PERFIL_HEPATICO_ID)
                {
                    codigoNat = "000002";
                }
                else if (Id == Constants.GRUPO_Y_FACTOR_SANGUINEO_ID)
                {
                    codigoNat = "GRUPO Y FACTOR";
                }
                #endregion

                #region Codigos Pruebas Laboratorio
                else if (Id == Constants.COLESTEROL_ID)
                {
                    codigoNat = "LB0001";
                }
                else if(Id == Constants.METADONA_ID)
                {
                    codigoNat = "LT0065";
                }
                #endregion
            }
            else if(prefijo == "MF")
            {
                if (Id == Constants.GRUPO_SANGUINEO_ID)
                {
                    codigoNat = "LH0026";
                }
                else if (Id == Constants.FACTOR_SANGUINEO_ID)
                {
                    codigoNat = "LH0027";
                }
            }

            return codigoNat;
        }

        public static void CreateEmptyFile(string fullPath, string consultorio, string fileName, string ext)
        {
            string rutaOrigenArchivo = "";
            string archivo = "";
            if (!File.Exists(fullPath))
            {
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
            }

            if (consultorio == "ESPIROMETRÍA")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen") + fileName;
            }
            else if (consultorio == "RAYOS X")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgRxOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgRxOrigen") + fileName;
            }
            else if (consultorio == "CARDIOLOGÍA")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgEKGOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgEKGOrigen") + fileName;
            }
            else if (consultorio == "LABORATORIO")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgLABOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgLABOrigen") + fileName;
            }
            else if (consultorio == "PSICOLOGÍA")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgPSICOOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgPSICOOrigen") + fileName;
            }
            else if (consultorio == "OFTALMOLOGÍA")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgOftalmoOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgOftalmoOrigen") + fileName;
            }
            else if (consultorio == "MEDICINA")
            {
                rutaOrigenArchivo = Common.Utils.GetApplicationConfigValue("ImgMedicinaOrigen");
                archivo = Common.Utils.GetApplicationConfigValue("ImgMedicinaOrigen") + fileName;
            }

            if (rutaOrigenArchivo !="")
            {
                string[] files = Directory.GetFiles(rutaOrigenArchivo);

                foreach (var item in files)
                {
                    if (item == archivo)
                        File.Copy(archivo, fullPath + @"\" + fileName);
                }
               
            }
        }

        private void btnCambiarProtocolo_Click(object sender, EventArgs e)
        {
            ServiceBL oServiceBL = new ServiceBL();
            frmProtocolManagement frm = new frmProtocolManagement("View", (int)ServiceType.Empresarial, (int)MasterService.Eso);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            if (string.IsNullOrEmpty(frm._pstrProtocolId))
                return;

            var protocolId = frm._pstrProtocolId;

            var check = grdDataCalendar.Rows;
            foreach (var item in check)
            {
                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    var serviceId = item.Cells["v_ServiceId"].Value.ToString();
                    oServiceBL.CambiarProtocoloDeServicio(serviceId, protocolId);
                }
            }
            btnFilter_Click(sender, e);
            MessageBox.Show("Se completo correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnHistoria_Click(object sender, EventArgs e)
        {
            OperationResult _objOperationResult = new OperationResult();
            //var doc = grdDataCalendar.Selected.Rows[0].Cells["v_DocDumber"].Value.ToString();
            var serviceID = grdDataCalendar.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;

                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                var datosP = new PacientBL().DevolverDatosPaciente(serviceID);
                var exams = new ServiceBL().GetServiceComponentsReport(serviceID);
                var medicoTratante = new ServiceBL().GetMedicoTratante(serviceID);

                string ruta = Common.Utils.GetApplicationConfigValue("rutaHistoriaClinica").ToString();

                string fecha = DateTime.Now.ToString().Split('/')[0] + "-" + DateTime.Now.ToString().Split('/')[1] + "-" + DateTime.Now.ToString().Split('/')[2];
                string nombre = "Historia Clinica N° " + serviceID + " - CSL";

                //var obtenerInformacionEmpresas = new ServiceBL().ObtenerInformacionEmpresas(serviceID);

                Historia_Clinica.CreateHistoria_Clinica(ruta + nombre + ".pdf", MedicalCenter, datosP, medicoTratante, exams);
                this.Enabled = true;
            }
        }
       
    }
}
