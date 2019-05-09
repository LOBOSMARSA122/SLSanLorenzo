﻿
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmProtocolEdit : Form
    {
        #region Declarations
        private string _mode = null;
        private string _protocolId = string.Empty;
        private string _protocolComponentId = string.Empty;
        private ProtocolBL _protocolBL = new ProtocolBL();
        private protocolDto _protocolDTO = null;
        private List<protocolcomponentDto> _protocolcomponentListDTO = null;
        private List<protocolcomponentDto> _protocolcomponentListDTODelete = null;
        private List<protocolcomponentDto> _protocolcomponentListDTOUpdate = null;
        private List<ProtocolComponentList> _tmpProtocolcomponentList = null;
        private string _protocolName;
        private int _rowIndexPc;
        private string _personId;
        private int? _systemUserId;

        
        #endregion

        #region GetChanges
        string[] nombreCampos =
        {

            "cbEmpresaTrabajo", "cbEmpresaCliente", "cbEmpresaEmpleadora", "cbGeso", "cbTipoServicio",
            "cbServicio", "txtCentroCosto", "chkEsComisionable", "txtComision", "chkEsActivo", "cboVendedor",
            "txtNombreProtocolo", "cbTipoExamen"
        };

        private List<Campo> ListValuesCampo = new List<Campo>();

        private class Campo
        {
            public string NombreCampo { get; set; }
            public string ValorCampo { get; set; }
        }


        private string SetChanges()
        {
            string cadena = "";
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;
                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampo.Find(x => x.NombreCampo == item).ValorCampo;
                    if (ValorCampo != value1)
                    {
                        cadena += item + ":" + ValorCampo + "|";
                    }
                }
            }
            if (cadena.Length > 0)
            {
                cadena += "FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            }

            return cadena;
        }

        private void SetOldValues()
        {

            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    Campo _Campo = new Campo();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampo.Add(_Campo);
                }
            }
        }

        private string GetValueControl(string ControlId, Control ctrl)
        {
            string value1 = null;

            switch (ControlId)
            {
                case "TextBox":
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case "ComboBox":
                    value1 = ((ComboBox)ctrl).Text;
                    break;
                case "CheckBox":
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString();
                    break;
                case "RadioButton":
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;

                default:
                    break;
            }

            return value1;
        }

        #endregion
        
        public frmProtocolEdit(string id, string mode)
        {
            InitializeComponent();
            _protocolId = id;
            _mode = mode;

        }

        private void frmProtocolEdit_Load(object sender, EventArgs e)
        {
            
            LoadData();
            if (grdExternalUser.Rows.Count != 0)
                grdExternalUser.Rows[0].Selected = true;
            if (grdProtocolComponent.Rows.Count != 0)
                grdProtocolComponent.Rows[0].Selected = true;

            SetOldValues();

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

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();

            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion

            LoadComboBox();
            BindGridSystemUserExternal();

            if (_mode == "New")
            {
                // Additional logic here.
                txtNombreProtocolo.Select();

            }
            else if (_mode == "Edit")
            {
               
                _protocolDTO = _protocolBL.GetProtocol(ref objOperationResult, _protocolId);
                string idOrgInter = "-1";

                // cabecera del protocolo
                txtNombreProtocolo.Text = _protocolDTO.v_Name;
                cbTipoExamen.SelectedValue = _protocolDTO.i_EsoTypeId.ToString();
                // Almacenar temporalmente
                _protocolName = txtNombreProtocolo.Text;

                if (_protocolDTO.v_WorkingOrganizationId != "-1" && _protocolDTO.v_WorkingLocationId != "-1")
                {
                    idOrgInter = string.Format("{0}|{1}", _protocolDTO.v_WorkingOrganizationId, _protocolDTO.v_WorkingLocationId);
                }

                cbEmpresaTrabajo.SelectedValue = idOrgInter;
                cbEmpresaCliente.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_CustomerOrganizationId, _protocolDTO.v_CustomerLocationId);
                cbEmpresaEmpleadora.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_EmployerOrganizationId, _protocolDTO.v_EmployerLocationId);
                cbGeso.SelectedValue = _protocolDTO.v_GroupOccupationId;
                cbTipoServicio.SelectedValue = _protocolDTO.i_MasterServiceTypeId.ToString();
                cbServicio.SelectedValue = _protocolDTO.i_MasterServiceId.ToString();
                txtCentroCosto.Text = _protocolDTO.v_CostCenter;
                chkEsComisionable.Checked = Convert.ToBoolean(_protocolDTO.i_HasVigency);
                txtComision.Enabled = chkEsComisionable.Checked;
                txtComision.Text = _protocolDTO.i_ValidInDays.ToString();
                chkEsActivo.Checked = Convert.ToBoolean(_protocolDTO.i_IsActive);
                cboVendedor.Text = _protocolDTO.v_NombreVendedor;

                // Componentes del protocolo
                var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, _protocolId);

                grdProtocolComponent.DataSource = dataListPc;

                _tmpProtocolcomponentList = dataListPc;
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());

                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (_mode == "Clon")
            {
                txtNombreProtocolo.Select();

                // Componentes del protocolo
                var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, _protocolId);

                grdProtocolComponent.DataSource = dataListPc;

                _tmpProtocolcomponentList = dataListPc;
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());

                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsExistsProtocolName()
        {
            // validar
            OperationResult objOperationResult = new OperationResult();
            return _protocolBL.IsExistsProtocolName(ref objOperationResult, txtNombreProtocolo.Text);          
        }

        

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _protocolcomponentListDTO = new List<protocolcomponentDto>();

            if (uvProtocol.Validate(true, false).IsValid)
            {
                #region Validations

                if (_tmpProtocolcomponentList == null || _tmpProtocolcomponentList.Count == 0)
                {
                    MessageBox.Show("Por favor agregue Examenes al protocolo", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                #region En un protocolo No se debe permitir agregar un Componente que tenga un campo formula que depende de otr componente que NO está en mismo protocolo. Si esto ocurre debe decir indicar lo siguiente: "El campo formula XXXXX depende de los campos YYY, ZZZZ que están en los componentes LLLLLL, y MMMMMM. Por favor agrege previamente los componentes LLLL y MMMM al protocolo.

                OperationResult objOperationResult1 = new OperationResult();

                string[] componentIdFromProtocol = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico)
                                                                                   .Select(p => p.v_ComponentId).ToArray();
                foreach (var item in componentIdFromProtocol)
                {
                    SiNo IsExists__ = _protocolBL.IsExistsFormula(ref objOperationResult1, componentIdFromProtocol, item);

                    if (IsExists__ == SiNo.NO)
                    {
                        MessageBox.Show(objOperationResult1.ReturnValue, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion

                var id = cbEmpresaEmpleadora.SelectedValue.ToString().Split('|');
                var id1 = cbEmpresaCliente.SelectedValue.ToString().Split('|');         
                var id2 = cbEmpresaTrabajo.SelectedValue.ToString().Split('|');

                if (_protocolDTO == null)
                {
                    _protocolDTO = new protocolDto();
                }

                _protocolDTO.v_Name = txtNombreProtocolo.Text;
                _protocolDTO.v_EmployerOrganizationId = id[0];
                _protocolDTO.v_EmployerLocationId = id[1];
                _protocolDTO.i_EsoTypeId = int.Parse(cbTipoExamen.SelectedValue.ToString());
                _protocolDTO.v_GroupOccupationId = cbGeso.SelectedValue.ToString();
                _protocolDTO.v_CustomerOrganizationId = id1[0];
                _protocolDTO.v_CustomerLocationId = id1[1];            
                _protocolDTO.v_WorkingOrganizationId = id2[0];
                _protocolDTO.v_WorkingLocationId = cbEmpresaTrabajo.SelectedValue.ToString() != "-1" ? id2[1] : "-1";           
                _protocolDTO.i_MasterServiceId = int.Parse(cbServicio.SelectedValue.ToString());                    
                _protocolDTO.v_CostCenter = txtCentroCosto.Text;
                _protocolDTO.i_MasterServiceTypeId = int.Parse(cbTipoServicio.SelectedValue.ToString());
                _protocolDTO.i_HasVigency = Convert.ToInt32(chkEsComisionable.Checked);
                _protocolDTO.i_ValidInDays = txtComision.Text != string.Empty ? int.Parse(txtComision.Text) : (int?)null;
                _protocolDTO.i_IsActive = Convert.ToInt32(chkEsActivo.Checked);
                _protocolDTO.v_NombreVendedor = cboVendedor.Text;


                // Grabar componentes del protocolo
                if (_mode == "New" || _mode == "Clon")
                {
                    #region Validar Nombre del prorocolo
                  
                    if (IsExistsProtocolName())
                    {
                        MessageBox.Show("Por favor Ingrese otro nombre de protocolo, este nombre ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                   
                    #endregion

                    foreach (var item in _tmpProtocolcomponentList)
                    {
                        protocolcomponentDto protocolComponent = new protocolcomponentDto();

                        protocolComponent.v_ComponentId = item.v_ComponentId;
                        protocolComponent.r_Price = item.r_Price;
                        protocolComponent.i_OperatorId = item.i_OperatorId;
                        protocolComponent.i_Age = item.i_Age;
                        protocolComponent.i_GenderId = item.i_GenderId;
                        protocolComponent.i_IsAdditional = item.i_isAdditional;
                        protocolComponent.i_IsConditionalId = item.i_IsConditionalId;
                        protocolComponent.i_GrupoEtarioId = item.i_GrupoEtarioId;
                        protocolComponent.i_IsConditionalIMC = item.i_IsConditionalIMC;
                        protocolComponent.r_Imc = item.r_Imc;

                        _protocolcomponentListDTO.Add(protocolComponent);
                    }
                            
                   _protocolId = _protocolBL.AddProtocol(ref objOperationResult, _protocolDTO, _protocolcomponentListDTO, Globals.ClientSession.GetAsList());

                   if (!string.IsNullOrEmpty(_protocolId))
                   {
                       _mode = "Edit";
                       _protocolName = txtNombreProtocolo.Text;
                   }

                }
                else if (_mode == "Edit")
                {
                    #region Validar Nombre del prorocolo

                    if (txtNombreProtocolo.Text != _protocolName)
                    {
                        if (IsExistsProtocolName())
                        {
                            MessageBox.Show("Por favor Ingrese otro nombre de protocolo, este nombre ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    #endregion

                    _protocolDTO.v_ProtocolId = _protocolId;

                    _protocolcomponentListDTOUpdate = new List<protocolcomponentDto>();
                    _protocolcomponentListDTODelete = new List<protocolcomponentDto>();

                    foreach (var item in _tmpProtocolcomponentList)
                    {
                        // Add
                        if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                        {
                            protocolcomponentDto protocolComponent = new protocolcomponentDto();

                            protocolComponent.v_ProtocolComponentId = item.v_ProtocolComponentId;
                            protocolComponent.v_ComponentId = item.v_ComponentId;
                            protocolComponent.r_Price = item.r_Price;
                            protocolComponent.i_OperatorId = item.i_OperatorId;
                            protocolComponent.i_Age = item.i_Age;
                            protocolComponent.i_GenderId = item.i_GenderId;
                            protocolComponent.i_IsAdditional = item.i_isAdditional;
                            protocolComponent.i_IsConditionalIMC = item.i_IsConditionalIMC;
                            protocolComponent.i_GrupoEtarioId = item.i_GrupoEtarioId;
                            protocolComponent.r_Imc = item.r_Imc;

                            protocolComponent.i_IsConditionalId = item.i_IsConditionalId;
                            _protocolcomponentListDTO.Add(protocolComponent);
                        }

                        // Update
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.Modificado)
                        {
                            protocolcomponentDto protocolComponent = new protocolcomponentDto();

                            protocolComponent.v_ProtocolComponentId = item.v_ProtocolComponentId;
                            protocolComponent.v_ComponentId = item.v_ComponentId;
                            protocolComponent.r_Price = item.r_Price;
                            protocolComponent.i_OperatorId = item.i_OperatorId;
                            protocolComponent.i_Age = item.i_Age;
                            protocolComponent.i_GenderId = item.i_GenderId;
                            protocolComponent.i_IsAdditional = item.i_isAdditional;
                            protocolComponent.i_IsConditionalIMC = item.i_IsConditionalIMC;
                            protocolComponent.i_GrupoEtarioId = item.i_GrupoEtarioId;
                            protocolComponent.r_Imc = item.r_Imc;
                            protocolComponent.i_IsConditionalId = item.i_IsConditionalId;
                            _protocolcomponentListDTOUpdate.Add(protocolComponent);
                        }

                        // Delete
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            protocolcomponentDto protocolComponent = new protocolcomponentDto();

                            protocolComponent.v_ProtocolComponentId = item.v_ProtocolComponentId;
                            _protocolcomponentListDTODelete.Add(protocolComponent);
                        }

                    }
                    _protocolBL.UpdateProtocol(ref objOperationResult,
                        _protocolDTO,
                        _protocolcomponentListDTO,
                        _protocolcomponentListDTOUpdate.Count == 0 ? null : _protocolcomponentListDTOUpdate,
                        _protocolcomponentListDTODelete.Count == 0 ? null : _protocolcomponentListDTODelete,
                        Globals.ClientSession.GetAsList());

                }

                // Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {


                    //this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    //_mode = "Edit";
                    LoadData();
                    //this.Close();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                SetChanges();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }


        private void LoadComboBox()
        {
            // Llenado de combos
            // Tipos de eso
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbTipoExamen, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cboVendedor, "Value1", "", BLL.Utils.GetVendedor(ref objOperationResult));

            // Lista de empresas por nodo
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            OperationResult objOperationResult1 = new OperationResult();
            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            

            Utils.LoadDropDownList(cbEmpresaEmpleadora,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbEmpresaTrabajo,
               "Value1",
               "Id",
               dataListOrganization1,
               DropDownListAction.Select);

            Utils.LoadDropDownList(cbEmpresaCliente,
              "Value1",
              "Id",
              dataListOrganization2,
              DropDownListAction.Select);

            

            //Llenado de los tipos de servicios [Emp/Part]
            Utils.LoadDropDownList(cbTipoServicio, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1, null), DropDownListAction.Select);
            // combo servicio
            Utils.LoadDropDownList(cbServicio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.Select);
          
           
        }

        private void LoadComboSoloEmpresas()
        {
            string idOrgInter = "-1";

            // Lista de empresas por nodo
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));

            OperationResult objOperationResult1 = new OperationResult();

            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);

            Utils.LoadDropDownList(cbEmpresaEmpleadora,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbEmpresaTrabajo,
               "Value1",
               "Id",
               dataListOrganization1,
               DropDownListAction.Select);

            Utils.LoadDropDownList(cbEmpresaCliente,
              "Value1",
              "Id",
              dataListOrganization2,
              DropDownListAction.Select);

            // Set combos
            if (_mode == "Edit")
            {
                cbEmpresaEmpleadora.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_EmployerOrganizationId, _protocolDTO.v_EmployerLocationId);

                if (_protocolDTO.v_WorkingOrganizationId != "-1" && _protocolDTO.v_WorkingLocationId != "-1")
                {
                    idOrgInter = string.Format("{0}|{1}", _protocolDTO.v_WorkingOrganizationId, _protocolDTO.v_WorkingLocationId);
                }

                cbEmpresaTrabajo.SelectedValue = idOrgInter;
                cbEmpresaCliente.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_CustomerOrganizationId, _protocolDTO.v_CustomerLocationId);
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolComponentEdit(string.Empty, "New");

            if (_tmpProtocolcomponentList != null)
            {
                frm._tmpProtocolcomponentList = _tmpProtocolcomponentList;
            }
           
            frm.ShowDialog();
       
            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpProtocolcomponentList != null)
            {
                _tmpProtocolcomponentList = frm._tmpProtocolcomponentList;

                var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdProtocolComponent.DataSource = new ProtocolComponentList();
                grdProtocolComponent.DataSource = dataList;
                grdProtocolComponent.Refresh();
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count()); 
            }             
           
        }

        private void delete_Click(object sender, EventArgs e)
        {       
            if (_mode == "New" || _mode == "Clon")
            {
                _tmpProtocolcomponentList.RemoveAt(_rowIndexPc);
            }
            else if (_mode == "Edit")
            {               
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            }

            var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            grdProtocolComponent.DataSource = new ProtocolComponentList();
            grdProtocolComponent.DataSource = dataList;
            grdProtocolComponent.Refresh();
            lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count()); 
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolComponentEdit(_protocolComponentId, "Edit");

            if (_tmpProtocolcomponentList != null)
            {
                frm._tmpProtocolcomponentList = _tmpProtocolcomponentList;
            }
            frm.ShowDialog();

            if (frm._tmpProtocolcomponentList != null)
            {
                _tmpProtocolcomponentList = frm._tmpProtocolcomponentList;

                var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdProtocolComponent.DataSource = new ProtocolComponentList();
                grdProtocolComponent.DataSource = dataList;
                grdProtocolComponent.Refresh();
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void LoadcbGESO()
        {
            var index = cbEmpresaEmpleadora.SelectedIndex;

            if (index == 0 || index == -1)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
                return;
            }

            var dataList = cbEmpresaEmpleadora.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];
            string idLoc = dataList[1];

            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESOByOrgIdAndLocationId(ref objOperationResult1, idOrg, idLoc), DropDownListAction.Select);
        }

        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbGESO();
        }

        private void grdProtocolComponent_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {      
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                // Capturar valor de una celda especifica al hace click derecho sobre la celda k se quiere su valor
                Infragistics.Win.UltraWinGrid.UltraGridCell cell = (Infragistics.Win.UltraWinGrid.UltraGridCell)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    _rowIndexPc = row.Index;
                  

                    if (row.Cells["v_ProtocolComponentId"].Value != null)
                    {
                         _protocolComponentId = row.Cells["v_ProtocolComponentId"].Value.ToString();
                    }

                    grdProtocolComponent.Rows[_rowIndexPc].Selected = true;

                    cmProtocol.Items["Edit"].Enabled = true;

                    if (_mode == "Edit")
                    {
                        OperationResult objOperationResult = new OperationResult();
                        var isProtocolInService = _protocolBL.IsExistsProtocol(ref objOperationResult, _protocolId);

                        if (isProtocolInService)
                        {
                            cmProtocol.Items["delete"].Enabled = false; 
                        }
                        else
                        {
                            cmProtocol.Items["delete"].Enabled = true; 
                        }
                       
                    }
                    else
                    {
                        cmProtocol.Items["delete"].Enabled = true;  
                    }                                                  
                }
                else
                {
                    cmProtocol.Items["delete"].Enabled = false;
                    cmProtocol.Items["Edit"].Enabled = false;
                }
                             
            } 
        }

        private void cbServiceType_TextChanged(object sender, EventArgs e)
        {
            if (cbTipoServicio.SelectedIndex == 0 || cbTipoServicio.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbTipoServicio.SelectedValue.ToString());
            Utils.LoadDropDownList(cbServicio, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        private void chkIsHasVigency_CheckedChanged(object sender, EventArgs e)
        {
            txtComision.Focus();
            txtComision.Enabled = (chkEsComisionable.Checked);
        }

        private void txtValidDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
            }
        }

        private void cbService_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbServicio.SelectedValue == null)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(cbServicio, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.Select);
                return;
            }

            if (cbServicio.SelectedValue.ToString() == ((int)MasterService.ConsultaMedica).ToString())
            {
                cbTipoExamen.Enabled = false;
                uvProtocol.GetValidationSettings(cbTipoExamen).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvProtocol.GetValidationSettings(cbTipoExamen).IsRequired = false;
            }
            else
            {
                cbTipoExamen.Enabled = true;
                uvProtocol.GetValidationSettings(cbTipoExamen).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvProtocol.GetValidationSettings(cbTipoExamen).IsRequired = true;
            }
        }
       
        #region Usuarios Externos

        private void BtnNew_Click(object sender, EventArgs e)
        {
            var frm = new frmExternalUserEdit("New", null, null, _protocolId);
            frm.ShowDialog();

            if (frm.DialogResult != DialogResult.OK) 
                return;

            BindGridSystemUserExternal();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            _personId = grdExternalUser.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            _systemUserId = int.Parse(grdExternalUser.Selected.Rows[0].Cells["i_SystemUserId"].Value.ToString());

            var frm = new frmExternalUserEdit("Edit", _personId, _systemUserId, _protocolId);
            frm.ShowDialog();

            if (frm.DialogResult != DialogResult.OK)
                return;

            BindGridSystemUserExternal();
        }

        private string BuildFilterExpression()
        {
            // Get the filters from the UI
            string filterExpression = string.Empty;

            List<string> Filters = new List<string>();

            if (!string.IsNullOrEmpty(txtUser.Text)) Filters.Add("v_UserName.Contains(\"" + txtUser.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtDocNumber.Text)) Filters.Add("v_DocNumber==" + "\"" + txtDocNumber.Text.Trim() + "\"");
            if (!string.IsNullOrEmpty(_protocolId)) Filters.Add("v_ProtocolId==" + "\"" + _protocolId + "\"");

            filterExpression = null;

            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    filterExpression = filterExpression + item + " && ";
                }
                filterExpression = filterExpression.Substring(0, filterExpression.Length - 4);
            }

            return filterExpression;
        }

        private void BindGridSystemUserExternal()
        {
            if (BuildFilterExpression() == null) 
                return;

            var dataList = GetSystemUserExternal(0, null, "v_PersonName ASC", BuildFilterExpression());

            if (dataList != null)
            {
                if (dataList.Count != 0)
                {
                    grdExternalUser.DataSource = dataList;
                    lblRecordCountExternalUSer.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
                }
                else
                {
                    grdExternalUser.DataSource = dataList;
                    lblRecordCountExternalUSer.Text = string.Format("Se encontraron {0} registros.", 0);
                }
            }

        }

        private List<SystemUserList> GetSystemUserExternal(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataList = _protocolBL.GetSystemUserExternalPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataList;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(_protocolId))
                {
                    tabControl1.SelectedIndex = 0;
                    MessageBox.Show("Por favor grabe antes el protocolo para poder continuar con la creación de usuarios externos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            BindGridSystemUserExternal();

            if (grdExternalUser.Rows.Count > 0)
            {
                grdExternalUser.Rows[0].Selected = true;
               
            }                   
        }

        private void grdExternalUser_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnDelete.Enabled = btnEdit.Enabled = (grdExternalUser.Selected.Rows.Count > 0);
          
        }

        #endregion

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbGeso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtProtocolName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbOrganizationInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mode !="Edit")
            {
                if (cbEmpresaCliente.SelectedValue == "-1") return;
                if (cbEmpresaCliente.SelectedValue != null)
                {
                    var id1 = cbEmpresaCliente.SelectedValue.ToString();

                    cbEmpresaEmpleadora.SelectedValue = id1;
                    cbEmpresaTrabajo.SelectedValue = id1;
                }
            }
           

        }

        private void txtCostCenter_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbIntermediaryOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbEsoType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtValidDays_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblRecordCount2_Click(object sender, EventArgs e)
        {

        }    

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolComponentEdit(string.Empty, "New");

            if (_tmpProtocolcomponentList != null)
            {
                frm._tmpProtocolcomponentList = _tmpProtocolcomponentList;
            }

            frm.ShowDialog();

            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpProtocolcomponentList != null)
            {
                _tmpProtocolcomponentList = frm._tmpProtocolcomponentList;

                var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdProtocolComponent.DataSource = new ProtocolComponentList();
                grdProtocolComponent.DataSource = dataList;
                grdProtocolComponent.Refresh();
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }        
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolComponentEdit(_protocolComponentId, "Edit");

            if (_tmpProtocolcomponentList != null)
            {
                frm._tmpProtocolcomponentList = _tmpProtocolcomponentList;
            }
            frm.ShowDialog();

            if (frm._tmpProtocolcomponentList != null)
            {
                _tmpProtocolcomponentList = frm._tmpProtocolcomponentList;

                var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdProtocolComponent.DataSource = new ProtocolComponentList();
                grdProtocolComponent.DataSource = dataList;
                grdProtocolComponent.Refresh();
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (_mode == "New" || _mode == "Clon")
            {
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId);
                _tmpProtocolcomponentList.Remove(findResult);
            }
            else if (_mode == "Edit")
            {
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _protocolComponentId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            }

            var dataList = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            grdProtocolComponent.DataSource = new ProtocolComponentList();
            grdProtocolComponent.DataSource = dataList;
            grdProtocolComponent.Refresh();
            lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count()); 
        }

        private void grdProtocolComponent_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnEditar.Enabled = btnRemover.Enabled = (grdProtocolComponent.Selected.Rows.Count > 0);

            if (grdProtocolComponent.Selected.Rows.Count == 0)
                return;

            _rowIndexPc = ((Infragistics.Win.UltraWinGrid.UltraGrid)sender).Selected.Rows[0].Index;
            _protocolComponentId = grdProtocolComponent.Selected.Rows[0].Cells["v_ProtocolComponentId"].Value.ToString();
           
        }

        private void btnAgregarEmpresaContrata_Click(object sender, EventArgs e)
        {
            var frm = new frmEmpresa();
            frm.ShowDialog();

           
                LoadComboSoloEmpresas();
           
        }

        private void btnAddUserExternal_Click(object sender, EventArgs e)
        {
            var frm = new frmAddUserExternal(_protocolId);
            frm.ShowDialog();

            BindGridSystemUserExternal();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAddPlain_Click(object sender, EventArgs e)
        {
            var frm = new FrmAddPlain();


            frm.ShowDialog();

 
        }
    }
}
