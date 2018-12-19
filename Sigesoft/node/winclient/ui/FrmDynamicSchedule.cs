using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using ScrapperReniecSunat;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmDynamicSchedule : Form
    {
        #region Declarations

        private PacientBL _pacientBl = new PacientBL();
        private ProtocolBL _protocolBl = new ProtocolBL();
        private MedicalExamBL _medicalExamBl = new MedicalExamBL();
        private DynamicScheduleBl _dynamicScheduleBl = new DynamicScheduleBl();
        //private GroupOccupationBL _groupOccupationBl = new GroupOccupationBL();
        private CalendarBL _calendarBl = new CalendarBL();
        private OperationResult _operationResult = new OperationResult();
        private readonly Random _rnd = new Random();
        private List<ScheduleForProcess> _scheduleForProcess;
        private List<IdsPersonAndProtocol> _listIdsPersonAndProtocols = new List<IdsPersonAndProtocol>();
        private ProtocolsAndWorkers _protocolsAndWorkers = new ProtocolsAndWorkers();
        private List<protocolDto> _protocolDtos = new List<protocolDto>();
        private List<personDto> _personDtos = new List<personDto>();
        #endregion

        public FrmDynamicSchedule()
        {
            InitializeComponent();
            grdSchedule.DataSource = new BindingList<DynamicSchedule>();
        }
       
        private void frmAgendaDinamica_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadStaticColumns();
            LoadDynamicColumns();
        }

        private void LoadComboBox()
        {
            var nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref _operationResult, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref _operationResult, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref _operationResult, nodeId);


            Utils.LoadDropDownList(cbOrganization,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbIntermediaryOrganization,
                "Value1",
                "Id",
                dataListOrganization1,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbOrganizationInvoice,
                "Value1",
                "Id",
                dataListOrganization2,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref _operationResult, 118, null), DropDownListAction.Select);

        }

        private static void InitializeRow(UltraGridRow row)
        {
            row.Cells["NroDocument"].Value = "";
            row.Cells["FirstName"].Value = "";
            row.Cells["LastName"].Value = "";
            row.Cells["SecondLastName"].Value = "";
            row.Cells["Birthdate"].Value = DateTime.Now.AddYears(-34);
            row.Cells["SexTypeId"].Value = 1;
            row.Cells["CurrentOccupation"].Value = "";
            row.Cells["Geso"].Value = "";
            row.Cells["GesoId"].Value = "";
        }

        private void LoadStaticColumns()
        {
            var band = grdSchedule.DisplayLayout.Bands[0];
            var workerData = band.Groups.Add("WorkerData", "Datos del Trabajador");
            band.Columns["Select"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["Clone"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["NroDocument"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["FirstName"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["LastName"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["SecondLastName"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["Birthdate"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["SexTypeId"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["CurrentOccupation"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["Geso"].RowLayoutColumnInfo.ParentGroup = workerData;
            band.Columns["GesoId"].RowLayoutColumnInfo.ParentGroup = workerData;
        }

        private void LoadDynamicColumns()
        {
            var columns = _dynamicScheduleBl.GetAllComponent();
            var mostUsedComponents = _protocolBl.ComponentsOfMostUsedProtocol();

            var grouperColumns = columns.GroupBy(g => g.CategoryName).Select(s => s.First()).ToList();

            foreach (var grouper in grouperColumns)
                CreateColumnGroup(grouper, columns, mostUsedComponents);
        }
        //refactoring
        private void CreateColumnGroup(ComponentsForHeaderGrid category, List<ComponentsForHeaderGrid> allComponents,string[] mostUsedComponents)
        {
            var band = grdSchedule.DisplayLayout.Bands[0];
            band.RowLayoutStyle = RowLayoutStyle.GroupLayout;
            var group = band.Groups.Add(category.CategoryName, category.CategoryName);

            GroupHeaderStyle(group);
            var componentsCategory = allComponents.FindAll(p => p.CategoryName == category.CategoryName);

            foreach (var component in componentsCategory)
            {
                band.Columns.Add(component.ComponentId, component.ComponentName);
                band.Columns[component.ComponentId].DataType = typeof(Boolean);
                band.Columns[component.ComponentId].CellActivation = Activation.AllowEdit;
                band.Columns[component.ComponentId].CellClickAction = CellClickAction.Edit;
                band.Columns[component.ComponentId].RowLayoutColumnInfo.ParentGroup = group;
                if(!mostUsedComponents.Contains(component.ComponentId)) band.Columns[component.ComponentId].Hidden = true;
            }
        }

        private void GroupHeaderStyle(UltraGridGroup @group)
        {
            var color = GetColorRandom();
            group.CellAppearance.BackColor = color;
            group.Header.Appearance.BackColor = color;

        }

        private void GetPersonData(string nroDocument)
        {
            if (!SearchPersonExistDb(nroDocument)) SearchReniec(nroDocument);
        }

        private void SearchReniec(string nroDocument)
        {
            var frmSearchData = new frmBuscarDatos(nroDocument);
            if (frmSearchData.ConexionDisponible)
            {
                frmSearchData.ShowDialog();
                if (frmSearchData.Estado == Estado.NoResul) return;
                SetPersonData((ReniecResultDto)frmSearchData.Datos);
            }
            else
                MessageBox.Show(@"No se pudo conectar la página", @"Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool SearchPersonExistDb(string nroDocument)
        {
            var pacientDto = _pacientBl.GetPacient(ref _operationResult, null, nroDocument);
            if (pacientDto == null) return false;
            SetPersonData(pacientDto);
            return true;
        }

        private void SetPersonData(object data)
        {
            if (data is PacientList)
            {
                var bdData = data as PacientList;
                grdSchedule.ActiveRow.Cells["FirstName"].Value = bdData.v_FirstName;
                grdSchedule.ActiveRow.Cells["LastName"].Value = bdData.v_FirstLastName;
                grdSchedule.ActiveRow.Cells["SecondLastName"].Value = bdData.v_SecondLastName;
                grdSchedule.ActiveRow.Cells["Birthdate"].Value = bdData.d_Birthdate;
            }
            else if (data is ReniecResultDto)
            {
                var reniecData = data as ReniecResultDto;
                grdSchedule.ActiveRow.Cells["FirstName"].Value = reniecData.Nombre;
                grdSchedule.ActiveRow.Cells["LastName"].Value = reniecData.ApellidoPaterno;
                grdSchedule.ActiveRow.Cells["SecondLastName"].Value = reniecData.ApellidoMaterno;
                grdSchedule.ActiveRow.Cells["Birthdate"].Value = reniecData.FechaNacimiento;
            }

            SetFocus("CurrentOccupation");
        }

        private void SetFocus(string field)
        {
            var lastRow = grdSchedule.Rows.LastOrDefault();
            if (lastRow == null) return;
            grdSchedule.Focus();
            grdSchedule.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
            grdSchedule.ActiveCell = lastRow.Cells[field];
            grdSchedule.PerformAction(UltraGridAction.EnterEditMode, false, false);
        }

        private Color GetColorRandom()
        {
            return Color.FromArgb(_rnd.Next(0, 256), _rnd.Next(0, 256), _rnd.Next(0, 256));
        }

        private void LoadSexType(InitializeLayoutEventArgs e)
        {
            ValueList vl;
            if (!e.Layout.ValueLists.Exists("MyValueList"))
            {
                vl = e.Layout.ValueLists.Add("MyValueList");
                vl.ValueListItems.Add(1, "Masculino");
                vl.ValueListItems.Add(2, "Femenino");
            }

            e.Layout.Bands[0].Columns["SexTypeId"].ValueList = e.Layout.ValueLists["MyValueList"];
        }
        //Refactoring
        private bool ValidateRecords()
        {
            var sbMessageValidation = new StringBuilder();

            var totalRecordsToValidate = _scheduleForProcess.Count;
            if (totalRecordsToValidate <= 0) return false;

            ultraStatusBarSchedule.Panels["Validating"].Visible = true;
            ultraStatusBarSchedule.Panels["Validating"].ProgressBarInfo.Maximum = totalRecordsToValidate;
            for (var i = 0; i < totalRecordsToValidate; i++)
            {
                ultraStatusBarSchedule.Panels["Validating"].ProgressBarInfo.Value = i;
                ultraStatusBarSchedule.Refresh();

                var record = _scheduleForProcess[i];
                var msm = ValidateCell(record.NroDocument, "NroDocument");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.FirstName, "FirstName");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.LastName, "LastName");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.SecondLastName, "SecondLastName");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.Birthdate.ToString("dd/MM/yyyy"), "Birthdate");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.SexTypeId.ToString(), "SexTypeId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.CurrentOccupation, "CurrentOccupation");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.GesoId, "GesoId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

            }
            ultraStatusBarSchedule.Panels["Validating"].Visible = false;

            if (sbMessageValidation.ToString() == "") return true;

            MessageBox.Show(sbMessageValidation.ToString(), @"Registros Invalidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }
        //Refactoring
        private string ValidateCell(string value, string field)
        {
            var message = "";
            var valueLength = value.Length;
            if (field == "NroDocument")
            {
                if (valueLength < 8)
                    message = "\"Nro. Documento \" es invalido";
            }
            else if (field == "FirstName")
            {
                if (valueLength == 0)
                    message = "\"Nombres \" es invalido";
            }
            else if (field == "LastName")
            {
                if (valueLength == 0)
                    message = "\"Apellido Materno \" es invalido";
            }
            else if (field == "SecondLastName")
            {
                if (valueLength == 0)
                    message = "\"Apellido Paterno \" es invalido";
            }
            else if (field == "Birthdate")
            {
                var date = DateTime.Parse(value);
                if (date >= DateTime.Now.Date)
                    message = "\"Fecha Nacimiento \" Fecha Invalida";
            }
            else if (field == "SexTypeId")
            {

            }
            else if (field == "CurrentOccupation")
            {
                if (valueLength == 0)
                    message = "\"Puesto Actual \" es invalido";
            }
            else if (field == "GesoId")
            {
                if (valueLength < 16)
                    message = "\"GesoId \" es Invalida. Debe Cambiar GESO";
            }

            return message;

        }

        private bool ProcessRecords()
        {
            var totalRecordsProcess = _scheduleForProcess.Count;

            ultraStatusBarSchedule.Panels["Processing"].Visible = true;

            for (var i = 0; i < totalRecordsProcess; i++)
            {
                ultraStatusBarSchedule.Panels["Processing"].ProgressBarInfo.Value = i;
                ultraStatusBarSchedule.Refresh();

                var personId = GetPersonId(_scheduleForProcess[i]);
                var protocolId = GetProtocolId(_scheduleForProcess[i]);
                _listIdsPersonAndProtocols.Add(new IdsPersonAndProtocol {PersonId = personId, ProtocolId = protocolId});

                if (!Process(personId, protocolId)) return false;
            }

            _protocolsAndWorkers.Protocols= _protocolDtos;
            _protocolsAndWorkers.Workers = _personDtos;

            var frm = new FrmConfigurationProtocols(_protocolsAndWorkers);
            frm.ShowDialog();

            ultraStatusBarSchedule.Panels["Processing"].Visible = false;
            return frm.DialogResult == DialogResult.OK;
        }
        
        private bool Process(string personId, string protocolId)
        {
            try
            {
                var oprotocolDto = _protocolBl.GetProtocol(ref _operationResult, protocolId);
                var opersonDto = _pacientBl.GetPerson(ref _operationResult, personId);
                
                _protocolDtos.Add(oprotocolDto);
                _personDtos.Add(opersonDto);

                return true;
            }
            catch (Exception )
            {
                return false;
            }
            
        }

        private void Schedule(List<IdsPersonAndProtocol> list)
        {
            foreach (var item in list)
            {
                var objCalendarDto = new calendarDto();
                objCalendarDto.v_PersonId = item.PersonId;
                objCalendarDto.d_DateTimeCalendar = dtpCalendarDate.Value;
                objCalendarDto.i_ServiceTypeId = (int)ServiceType.Empresarial;
                objCalendarDto.i_CalendarStatusId = (int)CalendarStatus.Agendado;
                objCalendarDto.i_ServiceId = (int)MasterService.Eso;
                objCalendarDto.v_ProtocolId = item.ProtocolId;
                objCalendarDto.i_NewContinuationId = 1;
                objCalendarDto.i_LineStatusId = (int)LineStatus.EnCircuito;
                objCalendarDto.i_IsVipId = (int)SiNo.NO;

                _calendarBl.AddShedule(ref _operationResult, objCalendarDto, Globals.ClientSession.GetAsList(), item.ProtocolId, item.PersonId, (int)MasterService.Eso, "Nuevo");
            }

        }

        private string GetProtocolId(ScheduleForProcess record)
        {
            var componentsBd = _medicalExamBl.GetAllComponent();
            var protocolsBd = _protocolBl.GetAllProtocol();

            var protocolId = ProtocolExist(record, protocolsBd);

            if (string.IsNullOrEmpty(protocolId))
                protocolId = CreateProtocol(record, componentsBd);

            return protocolId;
        }

        private string CreateProtocol(ScheduleForProcess record, List<ComponentList> componentsBd)
        {
            var oprotocolDto = new protocolDto();

            var organizationIds = cbOrganization.SelectedValue.ToString().Split('|');
            var organizationInvoiceIds = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
            var organizationIntermediaryIds = cbIntermediaryOrganization.SelectedValue.ToString().Split('|');

            oprotocolDto.v_Name = "&&&&" + CreateProtocolName(record.Geso);
            oprotocolDto.v_EmployerOrganizationId = organizationIds[0];
            oprotocolDto.v_EmployerLocationId = organizationIds[1];
            oprotocolDto.i_EsoTypeId = int.Parse(cbEsoType.SelectedValue.ToString());
            oprotocolDto.v_GroupOccupationId = record.GesoId;// GetGesoId(organizationInvoiceIds[1]);
            oprotocolDto.v_CustomerOrganizationId = organizationInvoiceIds[0];
            oprotocolDto.v_CustomerLocationId = organizationInvoiceIds[1];
            oprotocolDto.v_WorkingOrganizationId = organizationIntermediaryIds[0];
            oprotocolDto.v_WorkingLocationId = cbIntermediaryOrganization.SelectedValue.ToString() != "-1" ? organizationIntermediaryIds[1] : "-1";
            oprotocolDto.i_MasterServiceId = (int)MasterService.Eso;
            oprotocolDto.v_CostCenter = "";
            oprotocolDto.i_MasterServiceTypeId = (int)ServiceType.Empresarial;
            oprotocolDto.i_HasVigency = Convert.ToInt32(true);
            oprotocolDto.i_ValidInDays = null;
            oprotocolDto.i_IsActive = Convert.ToInt32(true);
            oprotocolDto.v_NombreVendedor = "";

            var inputComps = record.ComponentsByRecord.FindAll(p => p.Check);

            var protocolcomponentListDto = new List<protocolcomponentDto>();
            foreach (var item in inputComps)
            {
                var protocolComponent = new protocolcomponentDto();
                protocolComponent.v_ComponentId = item.ComponetId;
                protocolComponent.r_Price = componentsBd.Find( p => p.v_ComponentId == item.ComponetId).r_BasePrice;
                protocolComponent.i_OperatorId = -1;
                protocolComponent.i_Age = 0;
                protocolComponent.i_GenderId = 3;
                protocolComponent.i_IsAdditional = 0;
                protocolComponent.i_IsConditionalId = 0;
                protocolComponent.i_GrupoEtarioId = -1;
                protocolComponent.i_IsConditionalIMC = 0;
                protocolComponent.r_Imc = (decimal?) 0.00f;

                protocolcomponentListDto.Add(protocolComponent);
            }

            return _protocolBl.AddProtocol(ref _operationResult, oprotocolDto, protocolcomponentListDto, Globals.ClientSession.GetAsList());

        }

        private string CreateProtocolName(string gesoName)
        {
            var organizationInvoice = cbOrganizationInvoice.SelectedValue.ToString();
            var organization = cbOrganization.SelectedValue.ToString();
            var intermediaryOrganization = cbIntermediaryOrganization.SelectedValue.ToString();

            if (organizationInvoice == organization && organizationInvoice == intermediaryOrganization)
            {
                var invoiceName = cbOrganizationInvoice.Text.Split('/');

                return invoiceName[0] + " - " + cbEsoType.Text + " - " + gesoName;
            }
            else
            {
                var invoiceName = cbOrganizationInvoice.Text.Split('/');
                var organizationName = cbOrganization.Text.Split('/');

                return invoiceName[0] + " - " + cbEsoType.Text + " - " + gesoName + " - " + organizationName[0];
            }

        }

        //private string GetGesoId(string locationId)
        //{
        //   return _groupOccupationBl.GetFirstGroupOccupationByLocationId(ref _operationResult, locationId).v_GroupOccupationId;
        //}

        private string ProtocolExist(ScheduleForProcess record, List<ProtocolProcess> protocolsBd)
        {
            var equalProtocols = EqualProtocols(protocolsBd);
            if (equalProtocols.Count == 0) return null;

            return EqualComponents(record, equalProtocols);
        }

        private string EqualComponents(ScheduleForProcess record, List<ProtocolProcess> equalProtocols)
        {
            var inputComps = record.ComponentsByRecord.FindAll(p => p.Check);

            var equalProtocolsComponents = equalProtocols.FindAll(p => p.Components.Count == inputComps.Count);

            var totalMatches = inputComps.Count;
            var count = 0;
            foreach (var protocolBd in equalProtocolsComponents)
            {
                var compsBd = protocolBd.Components;
               
                foreach (var compBd in compsBd)
                {
                    var existsComp = inputComps.Find(p => p.ComponetId == compBd.ComponentId);
                    if (existsComp != null)
                        count++;
                    else
                        return null;
                }

                if (count == totalMatches)
                    return protocolBd.ProtocolId;
            }

            return null;
        }

        private List<ProtocolProcess> EqualProtocols(List<ProtocolProcess> protocolsBd)
        {
            return  protocolsBd.FindAll(p =>
                p.CustomerOrganizationId == cbOrganizationInvoice.SelectedValue.ToString()
                && p.EmployerOrganizationId == cbOrganization.SelectedValue.ToString()
                && p.WorkingOrganizationId == cbIntermediaryOrganization.SelectedValue.ToString()
                && p.EsoTypeId == int.Parse(cbEsoType.SelectedValue.ToString())
                ).ToList();
        }

        private string GetPersonId(ScheduleForProcess record)
        {
            var oPersonDto = _pacientBl.GetPersonByNroDocument(ref _operationResult, record.NroDocument);
            if (oPersonDto != null)
            {
                oPersonDto.v_CurrentOccupation = record.CurrentOccupation;
                _pacientBl.UpdatePacient(ref _operationResult, oPersonDto, Globals.ClientSession.GetAsList(), oPersonDto.v_DocNumber, oPersonDto.v_DocNumber);
                return oPersonDto.v_PersonId;
            }

            return  _pacientBl.AddPacient(ref _operationResult, PopulatePeronsDto(record), Globals.ClientSession.GetAsList());
        }

        private personDto PopulatePeronsDto(ScheduleForProcess record)
        {
            var oPersonDto = new personDto();
            oPersonDto.v_FirstName = record.FirstName;
            oPersonDto.v_FirstLastName = record.LastName;
            oPersonDto.v_SecondLastName = record.LastName;
            oPersonDto.i_DocTypeId = (int)Document.DNI;
            oPersonDto.v_DocNumber = record.NroDocument;
            oPersonDto.i_SexTypeId = record.SexTypeId;
            oPersonDto.d_Birthdate = record.Birthdate;
            oPersonDto.v_CurrentOccupation = record.CurrentOccupation;

            return oPersonDto;
        }

        private void cbOrganizationInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)cbOrganizationInvoice.SelectedValue == "-1") return;

            if (cbOrganizationInvoice.SelectedValue == null) return;

            var id1 = cbOrganizationInvoice.SelectedValue.ToString();
            cbOrganization.SelectedValue = id1;
            cbIntermediaryOrganization.SelectedValue = id1;
        }
        
        #region Buttons

        private void btnNewRow_Click(object sender, EventArgs e)
        {
            if (grdSchedule.ActiveRow != null)
            {
                var row = grdSchedule.DisplayLayout.Bands[0].AddNew();
                if (row == null) return;
                grdSchedule.Rows.Move(row, grdSchedule.Rows.Count - 1);
                grdSchedule.ActiveRowScrollRegion.ScrollRowIntoView(row);
                InitializeRow(row);
            }
            else
            {
                var row = grdSchedule.DisplayLayout.Bands[0].AddNew();
                if (row == null) return;
                InitializeRow(row);
            }

            SetFocus("NroDocument");
        }

        private void btnStartSchedule_Click(object sender, EventArgs e)
        {
            if (!uvschedule.Validate(true, false).IsValid) return;
            ReadRecords();

            if (!ValidateRecords()) return;
            if (ProcessRecords())
                Schedule();
                MessageBox.Show(@"Se agendó correctamente");

            _scheduleForProcess = null;
            _listIdsPersonAndProtocols = null;
            grdSchedule.DataSource = new BindingList<DynamicSchedule>();
        }

        private void Schedule()
        {
            ultraStatusBarSchedule.Panels["scheduling"].Visible = true;
            Schedule(_listIdsPersonAndProtocols);
            ultraStatusBarSchedule.Panels["scheduling"].Visible = false;
        }
        
        private void ReadRecords()
        {
            _scheduleForProcess = new List<ScheduleForProcess>();

            foreach (var row in grdSchedule.Rows)
            {
                var oScheduleForProcess = new ScheduleForProcess();

                var componentsByRecord = new List<ComponentsByRecord>();
                foreach (var cell in grdSchedule.Rows[row.Index].Cells)
                {
                    var oComponentsByRecord = new ComponentsByRecord();

                    var columnKey = cell.Column.Key;
                    
                    if (columnKey == "NroDocument" )
                    {
                        oScheduleForProcess.NroDocument = cell.Value.ToString();
                    }
                    else if (columnKey == "FirstName")
                    {
                        oScheduleForProcess.FirstName = cell.Value.ToString();
                    }
                    else if (columnKey == "LastName")
                    {
                        oScheduleForProcess.LastName = cell.Value.ToString();
                    }
                    else if (columnKey == "SecondLastName")
                    {
                        oScheduleForProcess.SecondLastName = cell.Value.ToString();
                    }
                    else if (columnKey == "Birthdate")
                    {
                        oScheduleForProcess.Birthdate = DateTime.Parse(cell.Value.ToString());
                    }
                    else if (columnKey == "SexTypeId")
                    {
                        oScheduleForProcess.SexTypeId = int.Parse(cell.Value.ToString());
                    }
                    else if (columnKey == "CurrentOccupation")
                    {
                        oScheduleForProcess.CurrentOccupation = cell.Value.ToString();
                    }
                    else if (columnKey == "GesoId")
                    {
                        oScheduleForProcess.GesoId = cell.Value.ToString();
                    }
                    else if (columnKey == "Geso")
                    {
                        oScheduleForProcess.Geso = cell.Value.ToString();
                    }
                    else if (columnKey == "Select" || columnKey == "Clone")
                    {

                    }
                    else
                    {
                        oComponentsByRecord.CategoryName = cell.Column.GroupResolved.Key;
                        oComponentsByRecord.ComponetId = cell.Column.Key;
                        oComponentsByRecord.Check = bool.Parse(cell.Value.ToString());
                        componentsByRecord.Add(oComponentsByRecord);
                    }

                    oScheduleForProcess.ComponentsByRecord = componentsByRecord;

                }
                _scheduleForProcess.Add(oScheduleForProcess);
            }
            
            if (_scheduleForProcess.Count == 0)
                MessageBox.Show(@"Ningún registro leido", @"INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderRows_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeGeso_Click(object sender, EventArgs e)
        {
            var workers = grdSchedule.Rows.Where(c => Convert.ToBoolean(c.Cells["Select"].Value));
            var ultraGridRows = workers as UltraGridRow[] ?? workers.ToArray();
            if (!ultraGridRows.Any()) return;

            if (!uvschedule.Validate(true, false).IsValid) return;
            
            var organizationIds = cbOrganization.SelectedValue.ToString().Split('|');
            var gesos = BLL.Utils.GetGESOByOrgIdAndLocationId(ref _operationResult, organizationIds[0], organizationIds[1]);
            var frm = new FrmChangeGeso(cbOrganizationInvoice.Text, gesos);
            frm.ShowDialog();

            foreach (var row in ultraGridRows)
            {
                if (!bool.Parse(row.Cells["Select"].Value.ToString())) continue;
                row.Cells["Geso"].Value = frm.Geso;
                row.Cells["GesoId"].Value = frm.GesoId;
            }
        }

        #endregion

        #region Grid Events

        private void grdSchedule_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            LoadSexType(e);
        }

        private void grdSchedule_KeyDown(object sender, KeyEventArgs e)
        {
            var grid = (UltraGrid)sender;
            if (grid.ActiveCell == null) return;

            var column = grid.ActiveCell.Column.Key;
            if (column == "NroDocument")
            {
                if (e.KeyCode != Keys.Enter) return;
                var dni = grdSchedule.ActiveRow.Cells["NroDocument"].Text;
                if (dni.Length < 8)
                {
                    MessageBox.Show(@"El Nro. Documento no puede tener menos de 8 caracteres", @"Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    SetFocus("NroDocument");
                    return;
                }
                GetPersonData(dni);
            }
            else if (column == "CurrentOccupation")
            {
                if (e.KeyCode != Keys.Enter) return;
                grdSchedule.ActiveRow.Cells["CurrentOccupation"].Value = "";

                var aCell = grdSchedule.ActiveRow.Cells["CurrentOccupation"];
                grdSchedule.ActiveCell = aCell;
                grdSchedule.Focus();
                grdSchedule.PerformAction(UltraGridAction.EnterEditMode, false, false);
            }

        }

        private void grdSchedule_KeyUp(object sender, KeyEventArgs e)
        {
            //var grid = (UltraGrid)sender;

            //if (e.KeyCode == Keys.Down)
            //{
            //    btnAgregarRegistro_Click(sender, e);
            //    // Go down one row
            //    grid.PerformAction(UltraGridAction.BelowCell);
            //}
        }

        #endregion

       
    }
}
