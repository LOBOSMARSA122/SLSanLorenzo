using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Navigation;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmConfigurationProtocols : Form
    {
        PacientBL _pacientBl = new PacientBL();
        ProtocolBL _protocolBl = new ProtocolBL();

        private OperationResult _operationResult = new OperationResult();
        private readonly ProtocolsAndWorkers _protocolsAndWorkers;

        public FrmConfigurationProtocols(ProtocolsAndWorkers protocolsAndWorkers)
        {
            _protocolsAndWorkers = protocolsAndWorkers;
            InitializeComponent();
        }

        private void FrmConfigurationProtocols_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            grdProtocols.DataSource = _protocolsAndWorkers.Protocols.GroupBy(g => g.v_ProtocolId).Select(s => s.First()).ToList();
            grdWorkers.DataSource = _protocolsAndWorkers.Workers;
        }

        private void LoadComboBox()
        {
            Utils.LoadDropDownList(cboDocumentType, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref _operationResult, 106, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboSexType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref _operationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbOperator, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref _operationResult, 117, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbGender, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref _operationResult, 130, null));
            Utils.LoadDropDownList(cbGrupoEtario, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref _operationResult, 254, null), DropDownListAction.All);


        }

        private void btnContinueSchedule_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancelSchedule_Click(object sender, EventArgs e)
        {

        }

        private void grdWorkers_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdWorkers.Selected.Rows.Count == 0)
                return;

            string personId = grdWorkers.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            LoadDataPerson(personId);

        }

        private string _personId;
        private void LoadDataPerson(string personId)
        {
            var pacientList = _pacientBl.GetPacient(ref _operationResult, personId, null);
            _personId = pacientList.v_PersonId;
            cboDocumentType.SelectedValue = pacientList.i_DocTypeId.ToString();
            cboSexType.SelectedValue = pacientList.i_SexTypeId.ToString();
            txtNroDocument.Text = pacientList.v_DocNumber;
            txtFirstName.Text = pacientList.v_FirstName;
            txtLastName.Text = pacientList.v_FirstLastName;
            txtSecondLastName.Text = pacientList.v_SecondLastName;
            if (pacientList.d_Birthdate != null) dtpDateTimeStar.Value = pacientList.d_Birthdate.Value;
            txtCurrentOcupation.Text = pacientList.v_CurrentOccupation;
        }

        private void btnEditWorker_Click(object sender, EventArgs e)
        {
           var personDto = _pacientBl.GetPerson(ref _operationResult, _personId);
            personDto.i_DocTypeId = int.Parse(cboDocumentType.SelectedValue.ToString());
            personDto.v_FirstName = txtFirstName.Text;
            personDto.v_FirstLastName = txtLastName.Text;
            personDto.v_SecondLastName= txtSecondLastName.Text;
            personDto.d_Birthdate = dtpDateTimeStar.Value;
            personDto.i_SexTypeId = int.Parse(cboSexType.SelectedValue.ToString());
            personDto.v_CurrentOccupation = txtCurrentOcupation.Text;

           if(_pacientBl.UpdatePacient(ref _operationResult, personDto, Globals.ClientSession.GetAsList(), txtNroDocument.Text, txtNroDocument.Text) != "")
               MessageBox.Show(@"Se actualizó los datos del trabajador", @"INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
           else
               MessageBox.Show(@"Error al actualizar", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSaveConditional_Click(object sender, EventArgs e)
        {
            var protocolComponentId = grdComponents.Selected.Rows[0].Cells["v_ProtocolComponentId"].Value.ToString();
            var oprotocolcomponentDto  =  _protocolBl.GetProtocolComponentDto(ref _operationResult, protocolComponentId);
            oprotocolcomponentDto.v_ProtocolComponentId = protocolComponentId;
            oprotocolcomponentDto.r_Price = float.Parse(txtFinalPrice.Value.ToString());
            oprotocolcomponentDto.i_IsAdditional = chkExaAdd.Checked ? 1 : 0;
            oprotocolcomponentDto.i_IsConditionalId = chkIsConditional.Checked ? 1 : 0;
            oprotocolcomponentDto.i_OperatorId = int.Parse(cbOperator.SelectedValue.ToString());
            oprotocolcomponentDto.i_Age = int.Parse(txtAge.Value.ToString());
            oprotocolcomponentDto.i_GenderId = int.Parse(cbGender.SelectedValue.ToString());
            oprotocolcomponentDto.i_GrupoEtarioId = int.Parse(cbGrupoEtario.SelectedValue.ToString());
            oprotocolcomponentDto.i_IsConditionalIMC = chkIMC.Checked ? 1 : 0;
            oprotocolcomponentDto.r_Imc = decimal.Parse(txtMayorque.Value.ToString());
            _protocolBl.UpdateProtocolComponent(ref _operationResult, oprotocolcomponentDto, Globals.ClientSession.GetAsList());

            MessageBox.Show(@"Datos Actualizados", @"INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void grdProtocols_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdProtocols.Selected.Rows.Count == 0)
                return;

            var protocolId = grdProtocols.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();

            var dataListPc = _protocolBl.GetProtocolComponents(ref _operationResult, protocolId).ToList();

            grdComponents.DataSource = dataListPc;

            lblRecordCountProtocolComponents.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count);
            
        }

        private void grdComponents_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (((UltraGrid) sender).Selected.Rows.Count == 0) return;
            UltraGrid grd = ((UltraGrid)sender);
           
            txtFinalPrice.Value = grd.Selected.Rows[0].Cells["r_Price"].Value.ToString();
            lblExamenSeleccionado.Text = grd.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();

            chkExaAdd.Checked = Convert.ToBoolean(grd.Selected.Rows[0].Cells["i_isAdditional"].Value);
            chkIsConditional.Checked = Convert.ToBoolean(grd.Selected.Rows[0].Cells["i_IsConditionalId"].Value);
            cbOperator.SelectedValue = grd.Selected.Rows[0].Cells["i_OperatorId"].Value.ToString();
            txtAge.Value = grd.Selected.Rows[0].Cells["i_Age"].Value.ToString();
            cbGender.SelectedValue = grd.Selected.Rows[0].Cells["i_GenderId"].Value.ToString();
            cbGrupoEtario.SelectedValue = grd.Selected.Rows[0].Cells["i_GrupoEtarioId"].Value.ToString();
            chkIMC.Checked = grd.Selected.Rows[0].Cells["i_IsConditionalIMC"].Value.ToString() == "1";
            txtMayorque.Value = grd.Selected.Rows[0].Cells["r_Imc"].Value.ToString();
        }

        private void chkIsConditional_CheckedChanged(object sender, EventArgs e)
        {
            gbConditional.Enabled = chkIsConditional.Checked;

            if (chkIsConditional.Checked) return;
            cbOperator.SelectedValue = "-1";
            txtAge.Value = 0;
            cbGender.SelectedValue = ((int)GenderConditional.AMBOS).ToString();
            cbGrupoEtario.SelectedValue = "-1";
        }

        private void grdProtocols_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void grdProtocols_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells["d_InsertDate"].Value == null) return;

            if (DateTime.Parse(e.Row.Cells["d_InsertDate"].Value.ToString()).Date != DateTime.Today.Date) return;
       
            e.Row.Appearance.BackColor = Color.White;
            e.Row.Appearance.BackColor2 = Color.Yellow;
            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
        }

        private void grdWorkers_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells["d_InsertDate"].Value == null) return;

            if (DateTime.Parse(e.Row.Cells["d_InsertDate"].Value.ToString()).Date != DateTime.Today.Date) return;

            e.Row.Appearance.BackColor = Color.White;
            e.Row.Appearance.BackColor2 = Color.Yellow;
            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
        }
    }
}
