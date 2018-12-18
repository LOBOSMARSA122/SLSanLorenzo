using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmConfiguracionPagoEspecialista : Form
    {
        private OperationResult _operationResult = new OperationResult();
        private UltraCombo cbOrganizationInvoice = new UltraCombo(); 
        private UltraCombo cbOrganization = new UltraCombo();
        private UltraCombo cbIntermediaryOrganization = new UltraCombo();
        private UltraCombo cbCategory = new UltraCombo();
        private UltraCombo cbSystemUser = new UltraCombo();
        private SpecialistConfigurationBl oSpecialistConfigurationBl = new SpecialistConfigurationBl();
        private List<SpecialistConfiguration> _list;
        
        public FrmConfiguracionPagoEspecialista()
        {
            InitializeComponent();
            grdConfiguration.DataSource = new BindingList<SpecialistConfiguration>();
        }

        private void FrmConfiguracionPagoEspecialista_Load(object sender, EventArgs e)
        {
            grdConfiguration.DataSource = oSpecialistConfigurationBl.LoadGrid();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (grdConfiguration.ActiveRow != null)
            {
                var row = grdConfiguration.DisplayLayout.Bands[0].AddNew();
                if (row == null) return;
                grdConfiguration.Rows.Move(row, grdConfiguration.Rows.Count - 1);
                grdConfiguration.ActiveRowScrollRegion.ScrollRowIntoView(row);
                InitializeRow(row);
            }
            else
            {
                var row = grdConfiguration.DisplayLayout.Bands[0].AddNew();
                if (row == null) return;
                InitializeRow(row);
            }

            SetFocus("SystemUser");
        }

        private void SetFocus(string field)
        {
            var lastRow = grdConfiguration.Rows.LastOrDefault();
            if (lastRow == null) return;
            grdConfiguration.Focus();
            grdConfiguration.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
            grdConfiguration.ActiveCell = lastRow.Cells[field];
            grdConfiguration.PerformAction(UltraGridAction.EnterEditMode, false, false);
        }

        private static void InitializeRow(UltraGridRow row)
        {
            row.Cells["i_SystemUserId"].Value = -1;
            row.Cells["i_CategoryId"].Value = -1;
            row.Cells["v_EmployerOrganizationId"].Value = -1;
            row.Cells["v_CustomerOrganizationId"].Value = -1;
            row.Cells["v_WorkingOrganizationId"].Value = -1;
            row.Cells["Price"].Value = 0.00f;
        }

        private void grdConfiguration_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            var nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocationUltra(ref _operationResult, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocationUltra(ref _operationResult, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocationUltra(ref _operationResult, nodeId);
            var dataCategory = BLL.Utils.GetSystemParameterForComboTreeBoxUltra(ref _operationResult, 116, null);
            var dataSystemUser = BLL.Utils.GetProfessionalUltra(ref _operationResult, "");

            #region Configura cbOrganizationInvoice
            UltraGridBand _ultraGridBandaEmployer = new UltraGridBand("Band 0", -1);
            UltraGridColumn _ultraGridColumnaIDEmployer = new UltraGridColumn("Id");
            UltraGridColumn _ultraGridColumnaDescripcionEmployer = new UltraGridColumn("Value1");

            _ultraGridColumnaIDEmployer.Header.Caption = "Id";
            _ultraGridColumnaDescripcionEmployer.Header.Caption = "Empresa";
            _ultraGridColumnaIDEmployer.Width = 1;
            _ultraGridColumnaDescripcionEmployer.Width = 300;

            _ultraGridBandaEmployer.Columns.AddRange(new object[] { _ultraGridColumnaIDEmployer, _ultraGridColumnaDescripcionEmployer });
            cbOrganizationInvoice.DisplayLayout.BandsSerializer.Add(_ultraGridBandaEmployer);
            cbOrganizationInvoice.DropDownStyle = UltraComboStyle.DropDownList;
            cbOrganizationInvoice.DropDownWidth = 330;

            Utils.LoadUltraComboList(cbOrganizationInvoice,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.All);

            e.Layout.Bands[0].Columns["v_CustomerOrganizationId"].ValueList = cbOrganizationInvoice;
            #endregion

            #region Configura cbOrganization
            UltraGridBand _ultraGridBandaCustomer = new UltraGridBand("Band 0", -1);
            UltraGridColumn _ultraGridColumnaIDCustomer = new UltraGridColumn("Id");
            UltraGridColumn _ultraGridColumnaDescripcionCustomer = new UltraGridColumn("Value1");

            _ultraGridColumnaIDCustomer.Header.Caption = "Id";
            _ultraGridColumnaDescripcionCustomer.Header.Caption = "Empresa";
            _ultraGridColumnaIDCustomer.Width = 1;
            _ultraGridColumnaDescripcionCustomer.Width = 300;

            _ultraGridBandaCustomer.Columns.AddRange(new object[] { _ultraGridColumnaIDCustomer, _ultraGridColumnaDescripcionCustomer });
            cbOrganization.DisplayLayout.BandsSerializer.Add(_ultraGridBandaCustomer);
            cbOrganization.DropDownStyle = UltraComboStyle.DropDownList;
            cbOrganization.DropDownWidth = 330;

            Utils.LoadUltraComboList(cbOrganization,
                "Value1",
                "Id",
                dataListOrganization1,
                DropDownListAction.All);

            e.Layout.Bands[0].Columns["v_EmployerOrganizationId"].ValueList = cbOrganization;
            #endregion

            #region Configura cbIntermediaryOrganization
            UltraGridBand _ultraGridBandaIntermediaryOrganization = new UltraGridBand("Band 0", -1);
            UltraGridColumn _ultraGridColumnaIDIntermediaryOrganization = new UltraGridColumn("Id");
            UltraGridColumn _ultraGridColumnaDescripcionIntermediaryOrganization = new UltraGridColumn("Value1");

            _ultraGridColumnaIDIntermediaryOrganization.Header.Caption = "Id";
            _ultraGridColumnaDescripcionIntermediaryOrganization.Header.Caption = "Empresa";
            _ultraGridColumnaIDIntermediaryOrganization.Width = 1;
            _ultraGridColumnaDescripcionIntermediaryOrganization.Width = 300;

            _ultraGridBandaIntermediaryOrganization.Columns.AddRange(new object[] { _ultraGridColumnaIDIntermediaryOrganization, _ultraGridColumnaDescripcionIntermediaryOrganization });
            cbIntermediaryOrganization.DisplayLayout.BandsSerializer.Add(_ultraGridBandaIntermediaryOrganization);
            cbIntermediaryOrganization.DropDownStyle = UltraComboStyle.DropDownList;
            cbIntermediaryOrganization.DropDownWidth = 330;

            Utils.LoadUltraComboList(cbIntermediaryOrganization,
                "Value1",
                "Id",
                dataListOrganization2,
                DropDownListAction.All);

            e.Layout.Bands[0].Columns["v_WorkingOrganizationId"].ValueList = cbIntermediaryOrganization;
            #endregion

            #region Configura cbCategory
            UltraGridBand _ultraGridBandaCategory = new UltraGridBand("Band 0", -1);
            UltraGridColumn _ultraGridColumnaIDCategory = new UltraGridColumn("Id");
            UltraGridColumn _ultraGridColumnaDescripcionCategory = new UltraGridColumn("Value1");

            _ultraGridColumnaIDCategory.Header.Caption = "Id";
            _ultraGridColumnaDescripcionCategory.Header.Caption = "Categoría";
            _ultraGridColumnaIDCategory.Width = 1;
            _ultraGridColumnaDescripcionCategory.Width = 300;

            _ultraGridBandaCategory.Columns.AddRange(new object[] { _ultraGridColumnaIDCategory, _ultraGridColumnaDescripcionCategory });
            cbCategory.DisplayLayout.BandsSerializer.Add(_ultraGridBandaCategory);
            cbCategory.DropDownStyle = UltraComboStyle.DropDownList;
            cbCategory.DropDownWidth = 330;

            Utils.LoadUltraComboList(cbCategory,
                "Value1",
                "Id",
                dataCategory,
                DropDownListAction.Select);

            e.Layout.Bands[0].Columns["i_CategoryId"].ValueList = cbCategory;
            #endregion

            #region Configura cbSystemUser
            UltraGridBand _ultraGridBandaSystemUser = new UltraGridBand("Band 0", -1);
            UltraGridColumn _ultraGridColumnaIDSystemUser = new UltraGridColumn("Id");
            UltraGridColumn _ultraGridColumnaDescripcionSystemUser = new UltraGridColumn("Value1");

            _ultraGridColumnaIDSystemUser.Header.Caption = "Id";
            _ultraGridColumnaDescripcionSystemUser.Header.Caption = "System User";
            _ultraGridColumnaIDSystemUser.Width = 1;
            _ultraGridColumnaDescripcionSystemUser.Width = 300;

            _ultraGridBandaSystemUser.Columns.AddRange(new object[] { _ultraGridColumnaIDSystemUser, _ultraGridColumnaDescripcionSystemUser });
            cbSystemUser.DisplayLayout.BandsSerializer.Add(_ultraGridBandaSystemUser);
            cbSystemUser.DropDownStyle = UltraComboStyle.DropDownList;
            cbSystemUser.DropDownWidth = 330;

            Utils.LoadUltraComboList(cbSystemUser,
                "Value1",
                "Id",
                dataSystemUser,
                DropDownListAction.Select);

            e.Layout.Bands[0].Columns["i_SystemUserId"].ValueList = cbSystemUser;
            #endregion
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ReadRecords();
            if (!ValidateRecords()) return;

            if (oSpecialistConfigurationBl.SaveChange(_list, Globals.ClientSession.GetAsList()))
                MessageBox.Show("Se Actualizó correctamente", @"Registros Válidos", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            else
            {
                MessageBox.Show("No se actualizó", @"ERROR", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateRecords()
        {
            var sbMessageValidation = new StringBuilder();

            var totalRecordsToValidate = _list.Count;
            if (totalRecordsToValidate <= 0) return false;

        
            for (var i = 0; i < totalRecordsToValidate; i++)
            {
                var record = _list[i];
                var msm = ValidateCell(record.i_SystemUserId.ToString(), "i_SystemUserId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.i_CategoryId.ToString(), "i_CategoryId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.v_EmployerOrganizationId, "v_EmployerOrganizationId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.v_CustomerOrganizationId, "v_CustomerOrganizationId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }

                msm = ValidateCell(record.v_WorkingOrganizationId, "v_WorkingOrganizationId");
                if (msm != "")
                {
                    sbMessageValidation.Append("Registro " + (i + 1));
                    sbMessageValidation.Append(": " + msm);
                    sbMessageValidation.Append("\n");
                }
            }

            if (sbMessageValidation.ToString() == "") return true;

            MessageBox.Show(sbMessageValidation.ToString(), @"Registros Invalidos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private string ValidateCell(string value, string field)
        {
            var message = "";
            var valueLength = value.Length;
            var Vvalue = value.ToString();
            if (field == "i_SystemUserId")
            {
                if (Vvalue == "-1")
                    message = "\"i_SystemUserId \" es invalido";
            }
            else if (field == "i_CategoryId")
            {
                if (Vvalue == "-1")
                    message = "\"i_CategoryId \" es invalido";
            }
            else if (field == "v_EmployerOrganizationId")
            {
                if (Vvalue == "-1")
                    message = "\"v_EmployerOrganizationId \" es invalido";
            }
            else if (field == "v_CustomerOrganizationId")
            {
                if (Vvalue == "-1")
                    message = "\"v_CustomerOrganizationId \" es invalido";
            }
            else if (field == "v_WorkingOrganizationId")
            {
                if (Vvalue == "-1")
                    message = "\"v_WorkingOrganizationId \" es invalido";
            }

            return message;

        }


        private void ReadRecords()
        {
            _list = new List<SpecialistConfiguration>();

            foreach (var row in grdConfiguration.Rows)
            {
                var oSpecialistConfiguration = new SpecialistConfiguration();
                foreach (var cell in grdConfiguration.Rows[row.Index].Cells)
                {
                    var columnKey = cell.Column.Key;
                    if (columnKey == "i_SystemUserId")
                    {
                        oSpecialistConfiguration.i_SystemUserId = cell.Value.ToString();
                    }
                    else if (columnKey == "i_CategoryId")
                    {
                        oSpecialistConfiguration.i_CategoryId = cell.Value.ToString();
                    }
                    else if (columnKey == "v_EmployerOrganizationId")
                    {
                        oSpecialistConfiguration.v_EmployerOrganizationId = cell.Value.ToString();
                    }
                    else if (columnKey == "v_CustomerOrganizationId")
                    {
                        oSpecialistConfiguration.v_CustomerOrganizationId = cell.Value.ToString();
                    }
                    else if (columnKey == "v_WorkingOrganizationId")
                    {
                        oSpecialistConfiguration.v_WorkingOrganizationId = cell.Value.ToString();
                    }
                    else if (columnKey == "Price")
                    {
                        oSpecialistConfiguration.Price = float.Parse(cell.Value.ToString());
                    }
                }
                _list.Add(oSpecialistConfiguration);
            }

            if (_list.Count == 0)
                MessageBox.Show(@"Ningún registro leido", @"INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void grdConfiguration_ClickCellButton(object sender, CellEventArgs e)
        {
            try
            {
                var medicoId = e.Cell.Row.Cells["v_MedicoId"].Value.ToString();
                if (e.Cell == null) return;
                if (e.Cell.Column.Key != "Delete") return;
                if (oSpecialistConfigurationBl.DeleteRow(medicoId))
                    MessageBox.Show(@"Se elimino correctamente", @"INFORMACIÓN", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                else
                    MessageBox.Show(@"Ocurrió un error al eliminar registro", @"ERROR", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


                grdConfiguration.DataSource = oSpecialistConfigurationBl.LoadGrid();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
          
        }
    }
}
