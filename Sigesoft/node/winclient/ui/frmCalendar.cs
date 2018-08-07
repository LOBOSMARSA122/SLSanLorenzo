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

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCalendar : Form
    {
        string strFilterExpression;
        string _PacientId;
        private string _ProtocolId;
        List<string> _ListaCalendar;
        string _v_OrganizationLocationProtocol;

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

        public frmCalendar()
        {
            InitializeComponent();
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

            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult,Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlVipId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlNewContinuationId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 121, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlLineStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 120, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlCalendarStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 122, null), DropDownListAction.All);
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
            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlServiceTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_ServiceTypeId==" + ddlServiceTypeId.SelectedValue);
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
            
             _objData = _objCalendarBL.GetCalendarsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        
        private void btnPerson_Click(object sender, EventArgs e)
        {
            frmSchedulePerson frm = new frmSchedulePerson("","New","");
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
                  frmSchedulePerson frm = new frmSchedulePerson(strCalendarId, "Reschedule", strProtocolId);
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

                    if (grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value != null)
                        WorkingOrganization.Text = grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();
                                    
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

                if (grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value != null)
                    WorkingOrganization.Text = grdDataCalendar.Selected.Rows[0].Cells["v_WorkingOrganizationName"].Value.ToString();

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

        private void btnDetallado_Click(object sender, EventArgs e)
        {

            OperationResult objOperationResult = new OperationResult();
            var frm = new Reports.frmRoadMapDetaail(_serviceId, _calendarId);
            frm.ShowDialog();

            //DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            //DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
            //var frm = new Reports.frmDetallado(strFilterExpression, pdatBeginDate.Value, pdatEndDate.Value);
            //frm.ShowDialog();
            

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
            var frm = new frmAddExam(ListaComponentes,"",_ProtocolId);
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

       
    }
}
