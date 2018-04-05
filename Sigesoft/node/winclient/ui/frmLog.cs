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

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmLog : Form
    {
        LogBL _objBL = new LogBL();
        string strFilterExpression;
        private int _intNodeId;
        OperationResult _objOperationResult = new OperationResult();
        public frmLog()
        {
            InitializeComponent();
        }

        private void frmAdministracion_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _intNodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));  
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
            //_Util.SetFormActionsInSession("FRM001");
            //btnNew.Enabled = _Util.IsActionEnabled("FRM001_ADD");

            //Llenado de combos
            Utils.LoadDropDownList(ddlEventTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 102,null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlSuccess, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.All);

            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            //btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlEventTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_EventTypeId==" + ddlEventTypeId.SelectedValue);
            if (ddlSuccess.SelectedValue.ToString() != "-1") Filters.Add("i_Success==" + ddlSuccess.SelectedValue);
            Filters.Add("i_NodeId==" + _intNodeId);
      
            if (!string.IsNullOrEmpty(txtUserName.Text)) Filters.Add("v_SystemUserName.Contains(\"" + txtUserName.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtProcessEntity.Text)) Filters.Add("v_ProcessEntity.Contains(\"" + txtProcessEntity.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtElementItem.Text)) Filters.Add("v_ElementItem.Contains(\"" + txtElementItem.Text.Trim() + "\")");
      
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

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdData));
        }

        private void BindGrid()
        {

            var objData = GetData(0, 5000, "d_Date DESC", strFilterExpression);
            
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<LogList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
          
            var _objData = _objBL.GetLogsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);
            
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void mnuGridVer_Click(object sender, EventArgs e)
        {
            string strLogId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            frmLogEdicion frm = new frmLogEdicion(strLogId);
            frm.ShowDialog();
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdData.Rows[row.Index].Selected = true;
                    contextMenuStrip1.Items["toolStripMenuItem2"].Enabled = true;
                }
                else
                {
                    contextMenuStrip1.Items["toolStripMenuItem2"].Enabled = false;
                }
            } 
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ddlSuccess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateandSelect_Click(object sender, EventArgs e)
        {
            string strLogId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            frmLogEdicion frm = new frmLogEdicion(strLogId);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlEventTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_EventTypeId==" + ddlEventTypeId.SelectedValue);
            if (ddlSuccess.SelectedValue.ToString() != "-1") Filters.Add("i_Success==" + ddlSuccess.SelectedValue);
            Filters.Add("i_NodeId==" + _intNodeId);

            if (!string.IsNullOrEmpty(txtUserName.Text)) Filters.Add("v_SystemUserName.Contains(\"" + txtUserName.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtProcessEntity.Text)) Filters.Add("v_ProcessEntity.Contains(\"" + txtProcessEntity.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtElementItem.Text)) Filters.Add("v_ElementItem.Contains(\"" + txtElementItem.Text.Trim() + "\")");

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

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdData));
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
            MigracionBL oMigracionBL = new MigracionBL();
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

                }
            //}
        //}


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
        //        if (oPersonNew == null)
        //        {
        //           var v_PersonId_NEW = CrearPaciente(item.v_DocNumber);
        //            oserviceDto.v_PersonId =v_PersonId_NEW;
        //            oPersonNew = oPacientBL.GetPersonByNroDocument(ref _objOperationResult, item.v_DocNumber);
        //        }
        //        else
        //        {
        //            oserviceDto.v_PersonId = oPersonNew.v_PersonId;
        //        }


        //        var Paciente = oPersonNew.v_FirstName + " " + oPersonNew.v_FirstLastName + " " + oPersonNew.v_SecondLastName;
                
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

        private void btnServicios_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                //ProcesoSErvicio();
            }
        }


        //public string CrearPaciente(string pstrDNI)
        //{
        //    personDto objpersonDto = null;
        //    PacientBL oPacientBL = new PacientBL();

        //    //var oPacient = oPacientBL.GetPersonByNroDocument(ref _objOperationResult, pstrDNI);
        //    var oPacient_OLD = oPacientBL.GetPersonByNroDocument_OLD(ref _objOperationResult, pstrDNI);
                
        //        var PersonId_OLD = oPacient_OLD.v_PersonId;
        //        objpersonDto = new personDto();
        //        objpersonDto.v_PersonId = oPacient_OLD.v_PersonId;
        //        objpersonDto.v_FirstName = oPacient_OLD.v_FirstName;
        //        objpersonDto.v_FirstLastName = oPacient_OLD.v_FirstLastName;
        //        objpersonDto.v_SecondLastName = oPacient_OLD.v_SecondLastName;
        //        objpersonDto.i_DocTypeId = oPacient_OLD.i_DocTypeId;
        //        objpersonDto.v_DocNumber = oPacient_OLD.v_DocNumber;
        //        objpersonDto.d_Birthdate = oPacient_OLD.d_Birthdate;
        //        objpersonDto.v_BirthPlace = oPacient_OLD.v_BirthPlace;
        //        objpersonDto.i_SexTypeId = oPacient_OLD.i_SexTypeId;
        //        objpersonDto.i_MaritalStatusId = oPacient_OLD.i_MaritalStatusId;
        //        objpersonDto.i_LevelOfId = oPacient_OLD.i_LevelOfId;
        //        objpersonDto.v_TelephoneNumber = oPacient_OLD.v_TelephoneNumber;
        //        objpersonDto.v_AdressLocation = oPacient_OLD.v_AdressLocation;
        //        objpersonDto.v_GeografyLocationId = oPacient_OLD.v_GeografyLocationId;
        //        objpersonDto.v_ContactName = oPacient_OLD.v_ContactName;
        //        objpersonDto.v_EmergencyPhone = oPacient_OLD.v_EmergencyPhone;
        //        objpersonDto.b_PersonImage = oPacient_OLD.b_PersonImage;
        //        objpersonDto.v_Mail = oPacient_OLD.v_Mail;
        //        objpersonDto.i_BloodGroupId = oPacient_OLD.i_BloodGroupId;
        //        objpersonDto.i_BloodFactorId = oPacient_OLD.i_BloodFactorId;
        //        objpersonDto.b_FingerPrintTemplate = oPacient_OLD.b_FingerPrintTemplate;
        //        objpersonDto.b_RubricImage = oPacient_OLD.b_RubricImage;
        //        objpersonDto.b_FingerPrintImage = oPacient_OLD.b_FingerPrintImage;
        //        objpersonDto.t_RubricImageText = oPacient_OLD.t_RubricImageText;
        //        objpersonDto.v_CurrentOccupation = oPacient_OLD.v_CurrentOccupation;
        //        objpersonDto.i_DepartmentId = oPacient_OLD.i_DepartmentId;
        //        objpersonDto.i_ProvinceId = oPacient_OLD.i_ProvinceId;
        //        objpersonDto.i_DistrictId = oPacient_OLD.i_DistrictId;
        //        objpersonDto.i_ResidenceInWorkplaceId = oPacient_OLD.i_ResidenceInWorkplaceId;
        //        objpersonDto.v_ResidenceTimeInWorkplace = oPacient_OLD.v_ResidenceTimeInWorkplace;
        //        objpersonDto.i_TypeOfInsuranceId = oPacient_OLD.i_TypeOfInsuranceId;
        //        objpersonDto.i_NumberLivingChildren = oPacient_OLD.i_NumberLivingChildren;
        //        objpersonDto.i_NumberDependentChildren = oPacient_OLD.i_NumberDependentChildren;
        //        objpersonDto.i_OccupationTypeId = oPacient_OLD.i_OccupationTypeId;
        //        objpersonDto.v_OwnerName = oPacient_OLD.v_OwnerName;
        //        objpersonDto.i_NumberLiveChildren = oPacient_OLD.i_NumberLiveChildren;
        //        objpersonDto.i_NumberDeadChildren = oPacient_OLD.i_NumberDeadChildren;
        //        objpersonDto.i_IsDeleted = oPacient_OLD.i_IsDeleted;
        //        objpersonDto.i_InsertUserId = oPacient_OLD.i_InsertUserId;
        //        objpersonDto.d_InsertDate = oPacient_OLD.d_InsertDate.Value;
        //        objpersonDto.i_UpdateUserId = oPacient_OLD.i_UpdateUserId;
        //        objpersonDto.d_UpdateDate = oPacient_OLD.d_UpdateDate;
        //        objpersonDto.i_InsertNodeId = oPacient_OLD.i_InsertNodeId;
        //        objpersonDto.i_UpdateNodeId = oPacient_OLD.i_UpdateNodeId;
        //        objpersonDto.i_Relationship = oPacient_OLD.i_Relationship;
        //        objpersonDto.v_ExploitedMineral = oPacient_OLD.v_ExploitedMineral;
        //        objpersonDto.i_AltitudeWorkId = oPacient_OLD.i_AltitudeWorkId;
        //        objpersonDto.i_PlaceWorkId = oPacient_OLD.i_PlaceWorkId;
        //        objpersonDto.v_NroPoliza = oPacient_OLD.v_NroPoliza;
        //        objpersonDto.v_Deducible = oPacient_OLD.v_Deducible == null ? 0 : oPacient_OLD.v_Deducible.Value;
        //        objpersonDto.i_NroHermanos = oPacient_OLD.i_NroHermanos;
        //        objpersonDto.v_Password = oPacient_OLD.v_Password;

        //        return oPacientBL.AddPacient(ref _objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());
               
        //    }

        private void btnDx_Click(object sender, EventArgs e)
        {

            MigracionBL oMigracionBL = new MigracionBL();

            List<string> ListaServiciosId = new List<string>();

            //ListaServiciosId.Add("N009-SR000003194");
            //ListaServiciosId.Add("N009-SR000003195");
            //ListaServiciosId.Add("N009-SR000003196");
            //ListaServiciosId.Add("N009-SR000003197");
            //ListaServiciosId.Add("N009-SR000003198");
            //ListaServiciosId.Add("N009-SR000003199");
            //ListaServiciosId.Add("N009-SR000003200");
            //ListaServiciosId.Add("N009-SR000003201");
            //ListaServiciosId.Add("N009-SR000003202");
            //ListaServiciosId.Add("N009-SR000003203");
            //ListaServiciosId.Add("N009-SR000003204");
            //ListaServiciosId.Add("N009-SR000003205");
            //ListaServiciosId.Add("N009-SR000003206");
            //ListaServiciosId.Add("N009-SR000003207");
            //ListaServiciosId.Add("N009-SR000003208");
            //ListaServiciosId.Add("N009-SR000003209");
            //ListaServiciosId.Add("N009-SR000003210");
            //ListaServiciosId.Add("N009-SR000003211");
            //ListaServiciosId.Add("N009-SR000003212");
            //ListaServiciosId.Add("N009-SR000003213");
            //ListaServiciosId.Add("N009-SR000003214");
            //ListaServiciosId.Add("N009-SR000003215");
            //ListaServiciosId.Add("N009-SR000003216");
            //ListaServiciosId.Add("N009-SR000003217");
            //ListaServiciosId.Add("N009-SR000003218");
            //ListaServiciosId.Add("N009-SR000003219");
            //ListaServiciosId.Add("N009-SR000003220");
            //ListaServiciosId.Add("N009-SR000003221");
            //ListaServiciosId.Add("N009-SR000003222");
            //ListaServiciosId.Add("N009-SR000003223");
            //ListaServiciosId.Add("N009-SR000003224");
            //ListaServiciosId.Add("N009-SR000003225");
            //ListaServiciosId.Add("N009-SR000003226");
            //ListaServiciosId.Add("N009-SR000003227");
            //ListaServiciosId.Add("N009-SR000003228");
            //ListaServiciosId.Add("N009-SR000003229");
            //ListaServiciosId.Add("N009-SR000003230");
            ListaServiciosId.Add("N009-SR000003231");
            ListaServiciosId.Add("N009-SR000003232");
            ListaServiciosId.Add("N009-SR000003233");
            ListaServiciosId.Add("N009-SR000003234");
            ListaServiciosId.Add("N009-SR000003235");
            ListaServiciosId.Add("N009-SR000003236");
            ListaServiciosId.Add("N009-SR000003237");
            ListaServiciosId.Add("N009-SR000003238");
            ListaServiciosId.Add("N009-SR000003239");
            ListaServiciosId.Add("N009-SR000003240");
            ListaServiciosId.Add("N009-SR000003241");
            ListaServiciosId.Add("N009-SR000003242");
            ListaServiciosId.Add("N009-SR000003243");
            ListaServiciosId.Add("N009-SR000003244");
            ListaServiciosId.Add("N009-SR000003245");
            ListaServiciosId.Add("N009-SR000003246");
            ListaServiciosId.Add("N009-SR000003247");
            ListaServiciosId.Add("N009-SR000003248");
            ListaServiciosId.Add("N009-SR000003249");
            ListaServiciosId.Add("N009-SR000003250");
            ListaServiciosId.Add("N009-SR000003251");
            ListaServiciosId.Add("N009-SR000003252");
            ListaServiciosId.Add("N009-SR000003253");
            ListaServiciosId.Add("N009-SR000003254");
            ListaServiciosId.Add("N009-SR000003255");
            ListaServiciosId.Add("N009-SR000003256");
            ListaServiciosId.Add("N009-SR000003257");
            ListaServiciosId.Add("N009-SR000003258");
            ListaServiciosId.Add("N009-SR000003259");
            ListaServiciosId.Add("N009-SR000003260");
            ListaServiciosId.Add("N009-SR000003261");
            ListaServiciosId.Add("N009-SR000003262");
            ListaServiciosId.Add("N009-SR000003263");
            ListaServiciosId.Add("N009-SR000003264");
            ListaServiciosId.Add("N009-SR000003265");
            ListaServiciosId.Add("N009-SR000003266");
            ListaServiciosId.Add("N009-SR000003267");
            ListaServiciosId.Add("N009-SR000003268");
            ListaServiciosId.Add("N009-SR000003269");
            ListaServiciosId.Add("N009-SR000003270");
            ListaServiciosId.Add("N009-SR000003271");
            ListaServiciosId.Add("N009-SR000003272");
            ListaServiciosId.Add("N009-SR000003273");
            ListaServiciosId.Add("N009-SR000003274");
            ListaServiciosId.Add("N009-SR000003275");
            ListaServiciosId.Add("N009-SR000003276");
            ListaServiciosId.Add("N009-SR000003277");
            ListaServiciosId.Add("N009-SR000003278");
            ListaServiciosId.Add("N009-SR000003279");
            ListaServiciosId.Add("N009-SR000003280");
            ListaServiciosId.Add("N009-SR000003281");
            ListaServiciosId.Add("N009-SR000003282");
            ListaServiciosId.Add("N009-SR000003283");
            ListaServiciosId.Add("N009-SR000003284");
            ListaServiciosId.Add("N009-SR000003285");
            ListaServiciosId.Add("N009-SR000003286");
            ListaServiciosId.Add("N009-SR000003287");
            ListaServiciosId.Add("N009-SR000003288");
            ListaServiciosId.Add("N009-SR000003289");
            ListaServiciosId.Add("N009-SR000003290");
            ListaServiciosId.Add("N009-SR000003291");
            ListaServiciosId.Add("N009-SR000003292");
            ListaServiciosId.Add("N009-SR000003293");
            ListaServiciosId.Add("N009-SR000003294");
            ListaServiciosId.Add("N009-SR000003295");
            ListaServiciosId.Add("N009-SR000003296");
            ListaServiciosId.Add("N009-SR000003297");
            ListaServiciosId.Add("N009-SR000003298");
            ListaServiciosId.Add("N009-SR000003299");
            ListaServiciosId.Add("N009-SR000003300");
            ListaServiciosId.Add("N009-SR000003301");
            ListaServiciosId.Add("N009-SR000003302");
            ListaServiciosId.Add("N009-SR000003303");
            ListaServiciosId.Add("N009-SR000003304");
            ListaServiciosId.Add("N009-SR000003305");
            ListaServiciosId.Add("N009-SR000003306");
            ListaServiciosId.Add("N009-SR000003307");
            ListaServiciosId.Add("N009-SR000003308");
            ListaServiciosId.Add("N009-SR000003309");
            ListaServiciosId.Add("N009-SR000003310");
            ListaServiciosId.Add("N009-SR000003311");
            ListaServiciosId.Add("N009-SR000003312");
            ListaServiciosId.Add("N009-SR000003313");
            ListaServiciosId.Add("N009-SR000003314");
            ListaServiciosId.Add("N009-SR000003315");
            ListaServiciosId.Add("N009-SR000003316");
            ListaServiciosId.Add("N009-SR000003317");
            ListaServiciosId.Add("N009-SR000003318");
            ListaServiciosId.Add("N009-SR000003319");
            ListaServiciosId.Add("N009-SR000003320");
            ListaServiciosId.Add("N009-SR000003321");
            ListaServiciosId.Add("N009-SR000003322");
            ListaServiciosId.Add("N009-SR000003323");
            ListaServiciosId.Add("N009-SR000003324");
            ListaServiciosId.Add("N009-SR000003325");
            ListaServiciosId.Add("N009-SR000003326");
            ListaServiciosId.Add("N009-SR000003327");
            ListaServiciosId.Add("N009-SR000003328");
            ListaServiciosId.Add("N009-SR000003329");
            ListaServiciosId.Add("N009-SR000003330");
            ListaServiciosId.Add("N009-SR000003331");
            ListaServiciosId.Add("N009-SR000003332");
            ListaServiciosId.Add("N009-SR000003333");
            ListaServiciosId.Add("N009-SR000003334");
            ListaServiciosId.Add("N009-SR000003335");
            ListaServiciosId.Add("N009-SR000003336");
            ListaServiciosId.Add("N009-SR000003337");
            ListaServiciosId.Add("N009-SR000003338");
            ListaServiciosId.Add("N009-SR000003339");
            ListaServiciosId.Add("N009-SR000003340");
            ListaServiciosId.Add("N009-SR000003341");
            ListaServiciosId.Add("N009-SR000003342");
            ListaServiciosId.Add("N009-SR000003343");
            ListaServiciosId.Add("N009-SR000003344");
            ListaServiciosId.Add("N009-SR000003345");
            ListaServiciosId.Add("N009-SR000003346");
            ListaServiciosId.Add("N009-SR000003347");
            ListaServiciosId.Add("N009-SR000003348");
            ListaServiciosId.Add("N009-SR000003349");
            ListaServiciosId.Add("N009-SR000003350");
            ListaServiciosId.Add("N009-SR000003351");
            ListaServiciosId.Add("N009-SR000003352");
            ListaServiciosId.Add("N009-SR000003353");
            ListaServiciosId.Add("N009-SR000003354");
            ListaServiciosId.Add("N009-SR000003355");
            ListaServiciosId.Add("N009-SR000003356");
            ListaServiciosId.Add("N009-SR000003357");
            ListaServiciosId.Add("N009-SR000003358");
            ListaServiciosId.Add("N009-SR000003359");
            ListaServiciosId.Add("N009-SR000003360");
            ListaServiciosId.Add("N009-SR000003361");
            ListaServiciosId.Add("N009-SR000003362");
            ListaServiciosId.Add("N009-SR000003363");
            ListaServiciosId.Add("N009-SR000003364");
            ListaServiciosId.Add("N009-SR000003365");
            ListaServiciosId.Add("N009-SR000003366");
            ListaServiciosId.Add("N009-SR000003367");
            ListaServiciosId.Add("N009-SR000003368");
            ListaServiciosId.Add("N009-SR000003369");
            ListaServiciosId.Add("N009-SR000003370");
            ListaServiciosId.Add("N009-SR000003371");
            ListaServiciosId.Add("N009-SR000003372");
            ListaServiciosId.Add("N009-SR000003373");
            ListaServiciosId.Add("N009-SR000003374");
            ListaServiciosId.Add("N009-SR000003375");
            ListaServiciosId.Add("N009-SR000003376");
            ListaServiciosId.Add("N009-SR000003377");
            ListaServiciosId.Add("N009-SR000003378");
            ListaServiciosId.Add("N009-SR000003379");
            ListaServiciosId.Add("N009-SR000003380");
            ListaServiciosId.Add("N009-SR000003381");
            ListaServiciosId.Add("N009-SR000003382");
            ListaServiciosId.Add("N009-SR000003383");
            ListaServiciosId.Add("N009-SR000003384");
            ListaServiciosId.Add("N009-SR000003385");
            ListaServiciosId.Add("N009-SR000003386");
            ListaServiciosId.Add("N009-SR000003387");
            ListaServiciosId.Add("N009-SR000003388");
            ListaServiciosId.Add("N009-SR000003389");
            ListaServiciosId.Add("N009-SR000003390");
            ListaServiciosId.Add("N009-SR000003391");
            ListaServiciosId.Add("N009-SR000003392");
            ListaServiciosId.Add("N009-SR000003393");
            ListaServiciosId.Add("N009-SR000003394");
            ListaServiciosId.Add("N009-SR000003395");
            ListaServiciosId.Add("N009-SR000003396");
            ListaServiciosId.Add("N009-SR000003397");
            ListaServiciosId.Add("N009-SR000003398");
            ListaServiciosId.Add("N009-SR000003399");
            ListaServiciosId.Add("N009-SR000003400");
            ListaServiciosId.Add("N009-SR000003401");
            ListaServiciosId.Add("N009-SR000003402");
            ListaServiciosId.Add("N009-SR000003403");
            ListaServiciosId.Add("N009-SR000003404");
            ListaServiciosId.Add("N009-SR000003405");
            ListaServiciosId.Add("N009-SR000003406");
            ListaServiciosId.Add("N009-SR000003407");
            ListaServiciosId.Add("N009-SR000003408");
            ListaServiciosId.Add("N009-SR000003409");
            ListaServiciosId.Add("N009-SR000003410");
            ListaServiciosId.Add("N009-SR000003411");
            ListaServiciosId.Add("N009-SR000003412");
            ListaServiciosId.Add("N009-SR000003413");
            ListaServiciosId.Add("N009-SR000003414");
            ListaServiciosId.Add("N009-SR000003415");
            ListaServiciosId.Add("N009-SR000003416");
            ListaServiciosId.Add("N009-SR000003417");
            ListaServiciosId.Add("N009-SR000003418");
            ListaServiciosId.Add("N009-SR000003419");
            ListaServiciosId.Add("N009-SR000003420");
            ListaServiciosId.Add("N009-SR000003421");
            ListaServiciosId.Add("N009-SR000003422");
            ListaServiciosId.Add("N009-SR000003423");
            ListaServiciosId.Add("N009-SR000003424");
            ListaServiciosId.Add("N009-SR000003425");
            ListaServiciosId.Add("N009-SR000003426");
            ListaServiciosId.Add("N009-SR000003427");
            ListaServiciosId.Add("N009-SR000003428");
            ListaServiciosId.Add("N009-SR000003429");
            ListaServiciosId.Add("N009-SR000003430");
            ListaServiciosId.Add("N009-SR000003431");
            ListaServiciosId.Add("N009-SR000003432");
            ListaServiciosId.Add("N009-SR000003433");
            ListaServiciosId.Add("N009-SR000003434");
            ListaServiciosId.Add("N009-SR000003435");
            ListaServiciosId.Add("N009-SR000003436");
            ListaServiciosId.Add("N009-SR000003437");
            ListaServiciosId.Add("N009-SR000003438");
            ListaServiciosId.Add("N009-SR000003439");
            ListaServiciosId.Add("N009-SR000003440");
            ListaServiciosId.Add("N009-SR000003441");
            ListaServiciosId.Add("N009-SR000003442");
            ListaServiciosId.Add("N009-SR000003443");
            ListaServiciosId.Add("N009-SR000003444");
            ListaServiciosId.Add("N009-SR000003445");
            ListaServiciosId.Add("N009-SR000003446");
            ListaServiciosId.Add("N009-SR000003447");
            ListaServiciosId.Add("N009-SR000003448");
            ListaServiciosId.Add("N009-SR000003449");
            ListaServiciosId.Add("N009-SR000003450");
            ListaServiciosId.Add("N009-SR000003451");
            ListaServiciosId.Add("N009-SR000003452");
            ListaServiciosId.Add("N009-SR000003453");
            ListaServiciosId.Add("N009-SR000003454");
            ListaServiciosId.Add("N009-SR000003455");
            ListaServiciosId.Add("N009-SR000003456");
            ListaServiciosId.Add("N009-SR000003457");
            ListaServiciosId.Add("N009-SR000003458");
            ListaServiciosId.Add("N009-SR000003459");
            ListaServiciosId.Add("N009-SR000003460");
            ListaServiciosId.Add("N009-SR000003461");
            ListaServiciosId.Add("N009-SR000003462");
            ListaServiciosId.Add("N009-SR000003463");
            ListaServiciosId.Add("N009-SR000003464");
            ListaServiciosId.Add("N009-SR000003465");
            ListaServiciosId.Add("N009-SR000003466");
            ListaServiciosId.Add("N009-SR000003467");
            ListaServiciosId.Add("N009-SR000003468");
            ListaServiciosId.Add("N009-SR000003469");
            ListaServiciosId.Add("N009-SR000003470");
            ListaServiciosId.Add("N009-SR000003471");
            ListaServiciosId.Add("N009-SR000003472");
            ListaServiciosId.Add("N009-SR000003473");
            ListaServiciosId.Add("N009-SR000003474");
            ListaServiciosId.Add("N009-SR000003475");
            ListaServiciosId.Add("N009-SR000003476");
            ListaServiciosId.Add("N009-SR000003477");
            ListaServiciosId.Add("N009-SR000003478");
            ListaServiciosId.Add("N009-SR000003479");
            ListaServiciosId.Add("N009-SR000003480");
            ListaServiciosId.Add("N009-SR000003481");
            ListaServiciosId.Add("N009-SR000003482");
            ListaServiciosId.Add("N009-SR000003483");
            ListaServiciosId.Add("N009-SR000003484");
            ListaServiciosId.Add("N009-SR000003485");
            ListaServiciosId.Add("N009-SR000003486");
            ListaServiciosId.Add("N009-SR000003487");
            ListaServiciosId.Add("N009-SR000003488");
            ListaServiciosId.Add("N009-SR000003489");
            ListaServiciosId.Add("N009-SR000003490");
            ListaServiciosId.Add("N009-SR000003491");
            ListaServiciosId.Add("N009-SR000003492");
            ListaServiciosId.Add("N009-SR000003493");
            ListaServiciosId.Add("N009-SR000003494");
            ListaServiciosId.Add("N009-SR000003495");
            ListaServiciosId.Add("N009-SR000003496");
            ListaServiciosId.Add("N009-SR000003497");
            ListaServiciosId.Add("N009-SR000003498");
            ListaServiciosId.Add("N009-SR000003499");
            ListaServiciosId.Add("N009-SR000003500");
            ListaServiciosId.Add("N009-SR000003501");
            ListaServiciosId.Add("N009-SR000003502");
            ListaServiciosId.Add("N009-SR000003503");
            ListaServiciosId.Add("N009-SR000003504");
            ListaServiciosId.Add("N009-SR000003505");
            ListaServiciosId.Add("N009-SR000003506");
            ListaServiciosId.Add("N009-SR000003507");
            ListaServiciosId.Add("N009-SR000003508");
            ListaServiciosId.Add("N009-SR000003509");
            ListaServiciosId.Add("N009-SR000003510");
            ListaServiciosId.Add("N009-SR000003511");
            ListaServiciosId.Add("N009-SR000003512");
            ListaServiciosId.Add("N009-SR000003513");
            ListaServiciosId.Add("N009-SR000003514");
            ListaServiciosId.Add("N009-SR000003515");
            ListaServiciosId.Add("N009-SR000003516");
            ListaServiciosId.Add("N009-SR000003517");
            ListaServiciosId.Add("N009-SR000003518");
            ListaServiciosId.Add("N009-SR000003519");
            ListaServiciosId.Add("N009-SR000003520");
            ListaServiciosId.Add("N009-SR000003521");
            ListaServiciosId.Add("N009-SR000003522");
            ListaServiciosId.Add("N009-SR000003523");
            ListaServiciosId.Add("N009-SR000003524");
            ListaServiciosId.Add("N009-SR000003525");
            ListaServiciosId.Add("N009-SR000003526");
            ListaServiciosId.Add("N009-SR000003527");
            ListaServiciosId.Add("N009-SR000003528");
            ListaServiciosId.Add("N009-SR000003529");
            ListaServiciosId.Add("N009-SR000003530");
            ListaServiciosId.Add("N009-SR000003531");
            ListaServiciosId.Add("N009-SR000003532");
            ListaServiciosId.Add("N009-SR000003533");
            ListaServiciosId.Add("N009-SR000003534");
            ListaServiciosId.Add("N009-SR000003535");
            ListaServiciosId.Add("N009-SR000003536");
            ListaServiciosId.Add("N009-SR000003537");
            ListaServiciosId.Add("N009-SR000003538");
            ListaServiciosId.Add("N009-SR000003539");
            ListaServiciosId.Add("N009-SR000003540");
            ListaServiciosId.Add("N009-SR000003541");
            ListaServiciosId.Add("N009-SR000003542");
            ListaServiciosId.Add("N009-SR000003543");
            ListaServiciosId.Add("N009-SR000003544");
            ListaServiciosId.Add("N009-SR000003545");
            ListaServiciosId.Add("N009-SR000003546");
            ListaServiciosId.Add("N009-SR000003547");
            ListaServiciosId.Add("N009-SR000003548");
            ListaServiciosId.Add("N009-SR000003549");
            ListaServiciosId.Add("N009-SR000003550");
            ListaServiciosId.Add("N009-SR000003551");
            ListaServiciosId.Add("N009-SR000003552");
            ListaServiciosId.Add("N009-SR000003553");
            ListaServiciosId.Add("N009-SR000003554");
            ListaServiciosId.Add("N009-SR000003555");
            ListaServiciosId.Add("N009-SR000003556");
            ListaServiciosId.Add("N009-SR000003557");
            ListaServiciosId.Add("N009-SR000003558");
            ListaServiciosId.Add("N009-SR000003559");
            ListaServiciosId.Add("N009-SR000003560");
            ListaServiciosId.Add("N009-SR000003561");
            ListaServiciosId.Add("N009-SR000003562");
            ListaServiciosId.Add("N009-SR000003563");
            ListaServiciosId.Add("N009-SR000003564");
            ListaServiciosId.Add("N009-SR000003565");
            ListaServiciosId.Add("N009-SR000003566");
            ListaServiciosId.Add("N009-SR000003567");
            ListaServiciosId.Add("N009-SR000003568");
            ListaServiciosId.Add("N009-SR000003569");
            ListaServiciosId.Add("N009-SR000003570");
            ListaServiciosId.Add("N009-SR000003571");
            ListaServiciosId.Add("N009-SR000003572");
            ListaServiciosId.Add("N009-SR000003573");
            ListaServiciosId.Add("N009-SR000003574");
            ListaServiciosId.Add("N009-SR000003575");
            ListaServiciosId.Add("N009-SR000003576");
            ListaServiciosId.Add("N009-SR000003577");
            ListaServiciosId.Add("N009-SR000003578");
            ListaServiciosId.Add("N009-SR000003579");
            ListaServiciosId.Add("N009-SR000003580");
            ListaServiciosId.Add("N009-SR000003581");
            ListaServiciosId.Add("N009-SR000003582");
            ListaServiciosId.Add("N009-SR000003583");
            ListaServiciosId.Add("N009-SR000003584");
            ListaServiciosId.Add("N009-SR000003585");
            ListaServiciosId.Add("N009-SR000003586");
            ListaServiciosId.Add("N009-SR000003587");
            ListaServiciosId.Add("N009-SR000003588");
            ListaServiciosId.Add("N009-SR000003589");
            ListaServiciosId.Add("N009-SR000003590");
            ListaServiciosId.Add("N009-SR000003591");
            ListaServiciosId.Add("N009-SR000003592");
            ListaServiciosId.Add("N009-SR000003593");
            ListaServiciosId.Add("N009-SR000003594");
            ListaServiciosId.Add("N009-SR000003595");
            ListaServiciosId.Add("N009-SR000003596");
            ListaServiciosId.Add("N009-SR000003597");
            ListaServiciosId.Add("N009-SR000003598");
            ListaServiciosId.Add("N009-SR000003599");
            ListaServiciosId.Add("N009-SR000003600");
            ListaServiciosId.Add("N009-SR000003601");
            ListaServiciosId.Add("N009-SR000003602");
            ListaServiciosId.Add("N009-SR000003603");
            ListaServiciosId.Add("N009-SR000003604");
            ListaServiciosId.Add("N009-SR000003605");
            ListaServiciosId.Add("N009-SR000003606");
            ListaServiciosId.Add("N009-SR000003607");
            ListaServiciosId.Add("N009-SR000003608");
            ListaServiciosId.Add("N009-SR000003609");
            ListaServiciosId.Add("N009-SR000003610");
            ListaServiciosId.Add("N009-SR000003611");
            ListaServiciosId.Add("N009-SR000003612");
            ListaServiciosId.Add("N009-SR000003613");
            ListaServiciosId.Add("N009-SR000003614");
            ListaServiciosId.Add("N009-SR000003615");
            ListaServiciosId.Add("N009-SR000003616");
            ListaServiciosId.Add("N009-SR000003617");
            ListaServiciosId.Add("N009-SR000003618");
            ListaServiciosId.Add("N009-SR000003619");
            ListaServiciosId.Add("N009-SR000003620");
            ListaServiciosId.Add("N009-SR000003621");
            ListaServiciosId.Add("N009-SR000003622");
            ListaServiciosId.Add("N009-SR000003623");
            ListaServiciosId.Add("N009-SR000003624");
            ListaServiciosId.Add("N009-SR000003625");
            ListaServiciosId.Add("N009-SR000003626");
            ListaServiciosId.Add("N009-SR000003627");
            ListaServiciosId.Add("N009-SR000003628");
            ListaServiciosId.Add("N009-SR000003629");
            ListaServiciosId.Add("N009-SR000003630");
            ListaServiciosId.Add("N009-SR000003631");
            ListaServiciosId.Add("N009-SR000003632");
            ListaServiciosId.Add("N009-SR000003633");
            ListaServiciosId.Add("N009-SR000003634");
            ListaServiciosId.Add("N009-SR000003635");
            ListaServiciosId.Add("N009-SR000003636");
            ListaServiciosId.Add("N009-SR000003637");
            ListaServiciosId.Add("N009-SR000003638");
            ListaServiciosId.Add("N009-SR000003639");
            ListaServiciosId.Add("N009-SR000003640");
            ListaServiciosId.Add("N009-SR000003641");
            ListaServiciosId.Add("N009-SR000003642");
            ListaServiciosId.Add("N009-SR000003643");
            ListaServiciosId.Add("N009-SR000003644");
            ListaServiciosId.Add("N009-SR000003645");
            ListaServiciosId.Add("N009-SR000003646");
            ListaServiciosId.Add("N009-SR000003647");
            ListaServiciosId.Add("N009-SR000003648");
            ListaServiciosId.Add("N009-SR000003649");
            ListaServiciosId.Add("N009-SR000003650");
            ListaServiciosId.Add("N009-SR000003651");
            ListaServiciosId.Add("N009-SR000003652");
            ListaServiciosId.Add("N009-SR000003653");
            ListaServiciosId.Add("N009-SR000003654");
            ListaServiciosId.Add("N009-SR000003655");
            ListaServiciosId.Add("N009-SR000003656");
            ListaServiciosId.Add("N009-SR000003657");
            ListaServiciosId.Add("N009-SR000003658");
            ListaServiciosId.Add("N009-SR000003659");
            ListaServiciosId.Add("N009-SR000003660");
            ListaServiciosId.Add("N009-SR000003661");
            ListaServiciosId.Add("N009-SR000003662");
            ListaServiciosId.Add("N009-SR000003663");
            ListaServiciosId.Add("N009-SR000003664");
            ListaServiciosId.Add("N009-SR000003665");
            ListaServiciosId.Add("N009-SR000003666");
            ListaServiciosId.Add("N009-SR000003667");
            ListaServiciosId.Add("N009-SR000003668");
            ListaServiciosId.Add("N009-SR000003669");
            ListaServiciosId.Add("N009-SR000003670");
            ListaServiciosId.Add("N009-SR000003671");
            ListaServiciosId.Add("N009-SR000003672");
            ListaServiciosId.Add("N009-SR000003673");
            ListaServiciosId.Add("N009-SR000003674");
            ListaServiciosId.Add("N009-SR000003675");
            ListaServiciosId.Add("N009-SR000003676");
            ListaServiciosId.Add("N009-SR000003677");
            ListaServiciosId.Add("N009-SR000003678");
            ListaServiciosId.Add("N009-SR000003679");
            ListaServiciosId.Add("N009-SR000003680");
            ListaServiciosId.Add("N009-SR000003681");
            ListaServiciosId.Add("N009-SR000003682");
            ListaServiciosId.Add("N009-SR000003683");
            ListaServiciosId.Add("N009-SR000003684");
            ListaServiciosId.Add("N009-SR000003685");
            ListaServiciosId.Add("N009-SR000003686");
            ListaServiciosId.Add("N009-SR000003687");
            ListaServiciosId.Add("N009-SR000003688");
            ListaServiciosId.Add("N009-SR000003689");
            ListaServiciosId.Add("N009-SR000003690");
            ListaServiciosId.Add("N009-SR000003691");
            ListaServiciosId.Add("N009-SR000003692");
            ListaServiciosId.Add("N009-SR000003693");
            ListaServiciosId.Add("N009-SR000003694");
            ListaServiciosId.Add("N009-SR000003695");
            ListaServiciosId.Add("N009-SR000003696");
            ListaServiciosId.Add("N009-SR000003697");
            ListaServiciosId.Add("N009-SR000003698");
            ListaServiciosId.Add("N009-SR000003699");
            ListaServiciosId.Add("N009-SR000003700");
            ListaServiciosId.Add("N009-SR000003701");
            ListaServiciosId.Add("N009-SR000003702");
            ListaServiciosId.Add("N009-SR000003703");
            ListaServiciosId.Add("N009-SR000003704");
            ListaServiciosId.Add("N009-SR000003705");
            ListaServiciosId.Add("N009-SR000003706");
            ListaServiciosId.Add("N009-SR000003707");
            ListaServiciosId.Add("N009-SR000003708");
            ListaServiciosId.Add("N009-SR000003709");
            ListaServiciosId.Add("N009-SR000003710");
            ListaServiciosId.Add("N009-SR000003711");
            ListaServiciosId.Add("N009-SR000003712");
            ListaServiciosId.Add("N009-SR000003713");
            ListaServiciosId.Add("N009-SR000003714");
            ListaServiciosId.Add("N009-SR000003715");
            ListaServiciosId.Add("N009-SR000003716");
            ListaServiciosId.Add("N009-SR000003717");
            ListaServiciosId.Add("N009-SR000003718");
            ListaServiciosId.Add("N009-SR000003719");
            ListaServiciosId.Add("N009-SR000003720");
            ListaServiciosId.Add("N009-SR000003721");
            ListaServiciosId.Add("N009-SR000003722");
            ListaServiciosId.Add("N009-SR000003723");
            ListaServiciosId.Add("N009-SR000003724");
            ListaServiciosId.Add("N009-SR000003725");
            ListaServiciosId.Add("N009-SR000003726");
            ListaServiciosId.Add("N009-SR000003727");
            ListaServiciosId.Add("N009-SR000003728");
            ListaServiciosId.Add("N009-SR000003729");
            ListaServiciosId.Add("N009-SR000003730");
            ListaServiciosId.Add("N009-SR000003731");
            ListaServiciosId.Add("N009-SR000003732");
            ListaServiciosId.Add("N009-SR000003733");
            ListaServiciosId.Add("N009-SR000003734");
            ListaServiciosId.Add("N009-SR000003735");
            ListaServiciosId.Add("N009-SR000003736");
            ListaServiciosId.Add("N009-SR000003737");
            ListaServiciosId.Add("N009-SR000003738");
            ListaServiciosId.Add("N009-SR000003739");
            ListaServiciosId.Add("N009-SR000003740");
            ListaServiciosId.Add("N009-SR000003741");
            ListaServiciosId.Add("N009-SR000003742");
            ListaServiciosId.Add("N009-SR000003743");
            ListaServiciosId.Add("N009-SR000003744");
            ListaServiciosId.Add("N009-SR000003745");
            ListaServiciosId.Add("N009-SR000003746");
            ListaServiciosId.Add("N009-SR000003747");
            ListaServiciosId.Add("N009-SR000003748");
            ListaServiciosId.Add("N009-SR000003749");
            ListaServiciosId.Add("N009-SR000003750");
            ListaServiciosId.Add("N009-SR000003751");
            ListaServiciosId.Add("N009-SR000003752");
            ListaServiciosId.Add("N009-SR000003753");
            ListaServiciosId.Add("N009-SR000003754");
            ListaServiciosId.Add("N009-SR000003755");
            ListaServiciosId.Add("N009-SR000003756");
            ListaServiciosId.Add("N009-SR000003757");
            ListaServiciosId.Add("N009-SR000003758");
            ListaServiciosId.Add("N009-SR000003759");
            ListaServiciosId.Add("N009-SR000003760");
            ListaServiciosId.Add("N009-SR000003761");
            ListaServiciosId.Add("N009-SR000003762");
            ListaServiciosId.Add("N009-SR000003763");
            ListaServiciosId.Add("N009-SR000003764");
            ListaServiciosId.Add("N009-SR000003765");
            ListaServiciosId.Add("N009-SR000003766");
            ListaServiciosId.Add("N009-SR000003767");
            ListaServiciosId.Add("N009-SR000003768");
            ListaServiciosId.Add("N009-SR000003769");
            ListaServiciosId.Add("N009-SR000003770");
            ListaServiciosId.Add("N009-SR000003771");
            ListaServiciosId.Add("N009-SR000003772");
            ListaServiciosId.Add("N009-SR000003773");
            ListaServiciosId.Add("N009-SR000003774");
            ListaServiciosId.Add("N009-SR000003775");
            ListaServiciosId.Add("N009-SR000003776");
            ListaServiciosId.Add("N009-SR000003777");
            ListaServiciosId.Add("N009-SR000003778");
            ListaServiciosId.Add("N009-SR000003779");
            ListaServiciosId.Add("N009-SR000003780");
            ListaServiciosId.Add("N009-SR000003781");
            ListaServiciosId.Add("N009-SR000003782");
            ListaServiciosId.Add("N009-SR000003783");
            ListaServiciosId.Add("N009-SR000003784");
            ListaServiciosId.Add("N009-SR000003785");
            ListaServiciosId.Add("N009-SR000003786");
            ListaServiciosId.Add("N009-SR000003787");
            ListaServiciosId.Add("N009-SR000003788");
            ListaServiciosId.Add("N009-SR000003789");
            ListaServiciosId.Add("N009-SR000003790");
            ListaServiciosId.Add("N009-SR000003791");
            ListaServiciosId.Add("N009-SR000003792");
            ListaServiciosId.Add("N009-SR000003793");
            ListaServiciosId.Add("N009-SR000003794");
            ListaServiciosId.Add("N009-SR000003795");
            ListaServiciosId.Add("N009-SR000003796");
            ListaServiciosId.Add("N009-SR000003797");
            ListaServiciosId.Add("N009-SR000003798");
            ListaServiciosId.Add("N009-SR000003799");
            ListaServiciosId.Add("N009-SR000003800");
            ListaServiciosId.Add("N009-SR000003801");
            ListaServiciosId.Add("N009-SR000003802");
            ListaServiciosId.Add("N009-SR000003803");
            ListaServiciosId.Add("N009-SR000003804");
            ListaServiciosId.Add("N009-SR000003805");
            ListaServiciosId.Add("N009-SR000003806");
            ListaServiciosId.Add("N009-SR000003807");
            ListaServiciosId.Add("N009-SR000003808");
            ListaServiciosId.Add("N009-SR000003809");
            ListaServiciosId.Add("N009-SR000003810");
            ListaServiciosId.Add("N009-SR000003811");
            ListaServiciosId.Add("N009-SR000003812");
            ListaServiciosId.Add("N009-SR000003813");
            ListaServiciosId.Add("N009-SR000003814");
            ListaServiciosId.Add("N009-SR000003815");
            ListaServiciosId.Add("N009-SR000003816");
            ListaServiciosId.Add("N009-SR000003817");
            ListaServiciosId.Add("N009-SR000003818");
            ListaServiciosId.Add("N009-SR000003819");
            ListaServiciosId.Add("N009-SR000003820");
            ListaServiciosId.Add("N009-SR000003821");
            ListaServiciosId.Add("N009-SR000003822");
            ListaServiciosId.Add("N009-SR000003823");
            ListaServiciosId.Add("N009-SR000003824");
            ListaServiciosId.Add("N009-SR000003825");
            ListaServiciosId.Add("N009-SR000003826");
            ListaServiciosId.Add("N009-SR000003827");
            ListaServiciosId.Add("N009-SR000003828");
            ListaServiciosId.Add("N009-SR000003829");
            ListaServiciosId.Add("N009-SR000003830");
            ListaServiciosId.Add("N009-SR000003831");
            ListaServiciosId.Add("N009-SR000003832");
            ListaServiciosId.Add("N009-SR000003833");
            ListaServiciosId.Add("N009-SR000003834");
            ListaServiciosId.Add("N009-SR000003835");
            ListaServiciosId.Add("N009-SR000003836");
            ListaServiciosId.Add("N009-SR000003837");
            ListaServiciosId.Add("N009-SR000003838");
            ListaServiciosId.Add("N009-SR000003839");
            ListaServiciosId.Add("N009-SR000003840");
            ListaServiciosId.Add("N009-SR000003841");
            ListaServiciosId.Add("N009-SR000003842");
            ListaServiciosId.Add("N009-SR000003843");
            ListaServiciosId.Add("N009-SR000003844");
            ListaServiciosId.Add("N009-SR000003845");
            ListaServiciosId.Add("N009-SR000003846");
            ListaServiciosId.Add("N009-SR000003847");
            ListaServiciosId.Add("N009-SR000003848");
            ListaServiciosId.Add("N009-SR000003849");
            ListaServiciosId.Add("N009-SR000003850");
            ListaServiciosId.Add("N009-SR000003851");
            ListaServiciosId.Add("N009-SR000003852");
            ListaServiciosId.Add("N009-SR000003853");
            ListaServiciosId.Add("N009-SR000003854");
            ListaServiciosId.Add("N009-SR000003855");
            ListaServiciosId.Add("N009-SR000003856");
            ListaServiciosId.Add("N009-SR000003857");
            ListaServiciosId.Add("N009-SR000003858");
            ListaServiciosId.Add("N009-SR000003859");
            ListaServiciosId.Add("N009-SR000003860");
            ListaServiciosId.Add("N009-SR000003861");
            ListaServiciosId.Add("N009-SR000003862");
            ListaServiciosId.Add("N009-SR000003863");
            ListaServiciosId.Add("N009-SR000003864");
            ListaServiciosId.Add("N009-SR000003865");
            ListaServiciosId.Add("N009-SR000003866");
            ListaServiciosId.Add("N009-SR000003867");
            ListaServiciosId.Add("N009-SR000003868");
            ListaServiciosId.Add("N009-SR000003869");
            ListaServiciosId.Add("N009-SR000003870");
            ListaServiciosId.Add("N009-SR000003871");
            ListaServiciosId.Add("N009-SR000003872");
            ListaServiciosId.Add("N009-SR000003873");
            ListaServiciosId.Add("N009-SR000003874");
            ListaServiciosId.Add("N009-SR000003875");
            ListaServiciosId.Add("N009-SR000003876");
            ListaServiciosId.Add("N009-SR000003877");
            ListaServiciosId.Add("N009-SR000003878");
            ListaServiciosId.Add("N009-SR000003879");
            ListaServiciosId.Add("N009-SR000003880");
            ListaServiciosId.Add("N009-SR000003881");
            ListaServiciosId.Add("N009-SR000003882");
            ListaServiciosId.Add("N009-SR000003883");
            ListaServiciosId.Add("N009-SR000003884");
            ListaServiciosId.Add("N009-SR000003885");
            ListaServiciosId.Add("N009-SR000003886");
            ListaServiciosId.Add("N009-SR000003887");
            ListaServiciosId.Add("N009-SR000003888");
            ListaServiciosId.Add("N009-SR000003889");
            ListaServiciosId.Add("N009-SR000003890");
            ListaServiciosId.Add("N009-SR000003891");
            ListaServiciosId.Add("N009-SR000003892");
            ListaServiciosId.Add("N009-SR000003893");
            ListaServiciosId.Add("N009-SR000003894");
            ListaServiciosId.Add("N009-SR000003895");
            ListaServiciosId.Add("N009-SR000003896");
            ListaServiciosId.Add("N009-SR000003897");
            ListaServiciosId.Add("N009-SR000003898");
            ListaServiciosId.Add("N009-SR000003899");
            ListaServiciosId.Add("N009-SR000003900");
            ListaServiciosId.Add("N009-SR000003901");
            ListaServiciosId.Add("N009-SR000003902");
            ListaServiciosId.Add("N009-SR000003903");
            ListaServiciosId.Add("N009-SR000003904");
            ListaServiciosId.Add("N009-SR000003905");
            ListaServiciosId.Add("N009-SR000003906");
            ListaServiciosId.Add("N009-SR000003907");
            ListaServiciosId.Add("N009-SR000003908");
            ListaServiciosId.Add("N009-SR000003909");
            ListaServiciosId.Add("N009-SR000003910");
            ListaServiciosId.Add("N009-SR000003911");
            ListaServiciosId.Add("N009-SR000003912");
            ListaServiciosId.Add("N009-SR000003913");
            ListaServiciosId.Add("N009-SR000003914");
            ListaServiciosId.Add("N009-SR000003915");
            ListaServiciosId.Add("N009-SR000003916");
            ListaServiciosId.Add("N009-SR000003917");
            ListaServiciosId.Add("N009-SR000003918");
            ListaServiciosId.Add("N009-SR000003919");
            ListaServiciosId.Add("N009-SR000003920");
            ListaServiciosId.Add("N009-SR000003921");
            ListaServiciosId.Add("N009-SR000003922");
            ListaServiciosId.Add("N009-SR000003923");
            ListaServiciosId.Add("N009-SR000003924");
            ListaServiciosId.Add("N009-SR000003925");
            ListaServiciosId.Add("N009-SR000003926");
            ListaServiciosId.Add("N009-SR000003927");
            ListaServiciosId.Add("N009-SR000003928");
            ListaServiciosId.Add("N009-SR000003929");
            ListaServiciosId.Add("N009-SR000003930");
            ListaServiciosId.Add("N009-SR000003931");
            ListaServiciosId.Add("N009-SR000003932");
            ListaServiciosId.Add("N009-SR000003933");
            ListaServiciosId.Add("N009-SR000003934");
            ListaServiciosId.Add("N009-SR000003935");
            ListaServiciosId.Add("N009-SR000003936");
            ListaServiciosId.Add("N009-SR000003937");
            ListaServiciosId.Add("N009-SR000003938");
            ListaServiciosId.Add("N009-SR000003939");
            ListaServiciosId.Add("N009-SR000003940");
            ListaServiciosId.Add("N009-SR000003941");
            ListaServiciosId.Add("N009-SR000003942");
            ListaServiciosId.Add("N009-SR000003943");
            ListaServiciosId.Add("N009-SR000003944");
            ListaServiciosId.Add("N009-SR000003945");
            ListaServiciosId.Add("N009-SR000003946");
            ListaServiciosId.Add("N009-SR000003947");
            ListaServiciosId.Add("N009-SR000003948");
            ListaServiciosId.Add("N009-SR000003949");
            ListaServiciosId.Add("N009-SR000003950");
            ListaServiciosId.Add("N009-SR000003951");
            ListaServiciosId.Add("N009-SR000003952");
            ListaServiciosId.Add("N009-SR000003953");
            ListaServiciosId.Add("N009-SR000003954");
            ListaServiciosId.Add("N009-SR000003955");
            ListaServiciosId.Add("N009-SR000003956");
            ListaServiciosId.Add("N009-SR000003957");
            ListaServiciosId.Add("N009-SR000003958");
            ListaServiciosId.Add("N009-SR000003959");
            ListaServiciosId.Add("N009-SR000003960");
            ListaServiciosId.Add("N009-SR000003961");
            ListaServiciosId.Add("N009-SR000003962");
            ListaServiciosId.Add("N009-SR000003963");
            ListaServiciosId.Add("N009-SR000003964");
            ListaServiciosId.Add("N009-SR000003965");
            ListaServiciosId.Add("N009-SR000003966");
            ListaServiciosId.Add("N009-SR000003967");
            ListaServiciosId.Add("N009-SR000003968");
            ListaServiciosId.Add("N009-SR000003969");
            ListaServiciosId.Add("N009-SR000003970");
            ListaServiciosId.Add("N009-SR000003971");
            ListaServiciosId.Add("N009-SR000003972");
            ListaServiciosId.Add("N009-SR000003973");
            ListaServiciosId.Add("N009-SR000003974");
            ListaServiciosId.Add("N009-SR000003975");
            ListaServiciosId.Add("N009-SR000003976");
            ListaServiciosId.Add("N009-SR000003977");
            ListaServiciosId.Add("N009-SR000003978");
            ListaServiciosId.Add("N009-SR000003979");
            ListaServiciosId.Add("N009-SR000003980");
            ListaServiciosId.Add("N009-SR000003981");
            ListaServiciosId.Add("N009-SR000003982");
            ListaServiciosId.Add("N009-SR000003983");
            ListaServiciosId.Add("N009-SR000003984");
            ListaServiciosId.Add("N009-SR000003985");
            ListaServiciosId.Add("N009-SR000003986");
            ListaServiciosId.Add("N009-SR000003987");
            ListaServiciosId.Add("N009-SR000003988");
            ListaServiciosId.Add("N009-SR000003989");
            ListaServiciosId.Add("N009-SR000003990");
            ListaServiciosId.Add("N009-SR000003991");
            ListaServiciosId.Add("N009-SR000003992");
            ListaServiciosId.Add("N009-SR000003993");
            ListaServiciosId.Add("N009-SR000003994");
            ListaServiciosId.Add("N009-SR000003995");
            ListaServiciosId.Add("N009-SR000003996");
            ListaServiciosId.Add("N009-SR000003997");
            ListaServiciosId.Add("N009-SR000003998");
            ListaServiciosId.Add("N009-SR000003999");
            ListaServiciosId.Add("N009-SR000004000");
            ListaServiciosId.Add("N009-SR000004001");
            ListaServiciosId.Add("N009-SR000004002");
            ListaServiciosId.Add("N009-SR000004003");
            ListaServiciosId.Add("N009-SR000004004");
            ListaServiciosId.Add("N009-SR000004005");
            ListaServiciosId.Add("N009-SR000004006");
            ListaServiciosId.Add("N009-SR000004007");
            ListaServiciosId.Add("N009-SR000004008");
            ListaServiciosId.Add("N009-SR000004009");
            ListaServiciosId.Add("N009-SR000004010");
            ListaServiciosId.Add("N009-SR000004011");
            ListaServiciosId.Add("N009-SR000004012");
            ListaServiciosId.Add("N009-SR000004013");
            ListaServiciosId.Add("N009-SR000004014");
            ListaServiciosId.Add("N009-SR000004015");
            ListaServiciosId.Add("N009-SR000004016");
            ListaServiciosId.Add("N009-SR000004017");
            ListaServiciosId.Add("N009-SR000004018");
            ListaServiciosId.Add("N009-SR000004019");
            ListaServiciosId.Add("N009-SR000004020");
            ListaServiciosId.Add("N009-SR000004021");
            ListaServiciosId.Add("N009-SR000004022");
            ListaServiciosId.Add("N009-SR000004023");
            ListaServiciosId.Add("N009-SR000004024");
            ListaServiciosId.Add("N009-SR000004025");
            ListaServiciosId.Add("N009-SR000004026");
            ListaServiciosId.Add("N009-SR000004027");
            ListaServiciosId.Add("N009-SR000004028");
            ListaServiciosId.Add("N009-SR000004029");
            ListaServiciosId.Add("N009-SR000004030");
            ListaServiciosId.Add("N009-SR000004031");
            ListaServiciosId.Add("N009-SR000004032");
            ListaServiciosId.Add("N009-SR000004033");
            ListaServiciosId.Add("N009-SR000004034");
            ListaServiciosId.Add("N009-SR000004035");
            ListaServiciosId.Add("N009-SR000004036");
            ListaServiciosId.Add("N009-SR000004037");
            ListaServiciosId.Add("N009-SR000004038");
            ListaServiciosId.Add("N009-SR000004039");
            ListaServiciosId.Add("N009-SR000004040");
            ListaServiciosId.Add("N009-SR000004041");
            ListaServiciosId.Add("N009-SR000004042");
            ListaServiciosId.Add("N009-SR000004043");
            ListaServiciosId.Add("N009-SR000004044");
            ListaServiciosId.Add("N009-SR000004045");


            //foreach (var item in ListaServiciosId)
            //{
            //    ServiceBL oServiceBL = new ServiceBL();
            //    var ListaDx_OLD = oMigracionBL.DevolverListaDiagnosticOLD(item);
            //    List<DiagnosticRepositoryList> oListDiagnosticRepositoryList = new List<DiagnosticRepositoryList>();
            //    DiagnosticRepositoryList oDiagnosticRepositoryList;
            //    foreach (var itemDx in ListaDx_OLD)
            //    {
            //        oDiagnosticRepositoryList = new DiagnosticRepositoryList();

            //        oDiagnosticRepositoryList.v_ServiceId = item;
            //        oDiagnosticRepositoryList.v_DiseasesId = oMigracionBL.ValidarDiseaseSiExiste(itemDx.v_Cie10, itemDx.v_Name, Globals.ClientSession.GetAsList());
            //        oDiagnosticRepositoryList.v_ComponentId = itemDx.v_ComponentId;
            //        oDiagnosticRepositoryList.v_ComponentFieldId = itemDx.v_ComponentFieldId;
            //        oDiagnosticRepositoryList.i_AutoManualId = itemDx.i_AutoManualId == null ? (int?)null : itemDx.i_AutoManualId.Value;
            //        oDiagnosticRepositoryList.i_PreQualificationId = itemDx.i_PreQualificationId == null ? (int?)null : itemDx.i_PreQualificationId.Value;
            //        oDiagnosticRepositoryList.i_FinalQualificationId = itemDx.i_FinalQualificationId == null ? (int?)null : itemDx.i_FinalQualificationId.Value;

            //        oDiagnosticRepositoryList.i_DiagnosticTypeId = itemDx.i_DiagnosticTypeId == null ? (int?)null : itemDx.i_DiagnosticTypeId.Value;
            //        oDiagnosticRepositoryList.i_IsSentToAntecedent = itemDx.i_IsSentToAntecedent == null ? (int?)null : itemDx.i_IsSentToAntecedent.Value;
            //        oDiagnosticRepositoryList.d_ExpirationDateDiagnostic = itemDx.d_ExpirationDateDiagnostic;
            //        oDiagnosticRepositoryList.i_GenerateMedicalBreak = itemDx.i_GenerateMedicalBreak == null ? (int?)null : itemDx.i_GenerateMedicalBreak.Value;
            //        oDiagnosticRepositoryList.v_Recomendations = itemDx.v_Recomendations;

            //        oDiagnosticRepositoryList.i_DiagnosticSourceId = itemDx.i_DiagnosticSourceId == null ? (int?)null : itemDx.i_DiagnosticSourceId.Value;
            //        oDiagnosticRepositoryList.i_ShapeAccidentId = itemDx.i_ShapeAccidentId == null ? (int?)null : itemDx.i_ShapeAccidentId.Value;
            //        oDiagnosticRepositoryList.i_BodyPartId = itemDx.i_BodyPartId == null ? (int?)null : itemDx.i_BodyPartId.Value;
            //        oDiagnosticRepositoryList.i_ClassificationOfWorkAccidentId = itemDx.i_ClassificationOfWorkAccidentId == null ? (int?)null : itemDx.i_ClassificationOfWorkAccidentId.Value;
            //        oDiagnosticRepositoryList.i_RiskFactorId = itemDx.i_RiskFactorId == null ? (int?)null : itemDx.i_RiskFactorId.Value;
            //        oDiagnosticRepositoryList.i_RecordStatus = (int)RecordStatus.Agregado;
            //        oDiagnosticRepositoryList.i_RecordType = (int)RecordType.Temporal;
            //        oDiagnosticRepositoryList.i_ClassificationOfWorkdiseaseId = itemDx.i_ClassificationOfWorkdiseaseId == null ? (int?)null : itemDx.i_ClassificationOfWorkdiseaseId.Value;
            //        oDiagnosticRepositoryList.i_SendToInterconsultationId = itemDx.i_SendToInterconsultationId == null ? (int?)null : itemDx.i_SendToInterconsultationId.Value;
            //        oDiagnosticRepositoryList.i_InterconsultationDestinationId = itemDx.i_InterconsultationDestinationId == null ? (int?)null : itemDx.i_InterconsultationDestinationId.Value;
            //        oDiagnosticRepositoryList.v_InterconsultationDestinationId = itemDx.v_InterconsultationDestinationId;

            //        oListDiagnosticRepositoryList.Add(oDiagnosticRepositoryList);
            //    }

            //    oServiceBL.AddDiagnosticRepository(ref _objOperationResult,
            //                                               oListDiagnosticRepositoryList,
            //                                               null,
            //                                               Globals.ClientSession.GetAsList(),
            //                                               null);
            //}

           
        }
    
    }
}
