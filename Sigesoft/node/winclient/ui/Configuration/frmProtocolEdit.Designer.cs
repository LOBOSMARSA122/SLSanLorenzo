namespace Sigesoft.Node.WinClient.UI.Configuration
{
    partial class frmProtocolEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("r_Price");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_Age");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Gender");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_IsConditional");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ComponentTypeName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProtocolEdit));
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_SystemUserId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PersonId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_PersonName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_UserName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_ExpireDate");
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.grdProtocolComponent = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cmProtocol = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.New = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.delete = new System.Windows.Forms.ToolStripMenuItem();
            this.verCambiosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecordCount2 = new System.Windows.Forms.Label();
            this.uvProtocol = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.cbEmpresaCliente = new System.Windows.Forms.ComboBox();
            this.cbGeso = new System.Windows.Forms.ComboBox();
            this.cbTipoExamen = new System.Windows.Forms.ComboBox();
            this.cbEmpresaEmpleadora = new System.Windows.Forms.ComboBox();
            this.cbTipoServicio = new System.Windows.Forms.ComboBox();
            this.cbServicio = new System.Windows.Forms.ComboBox();
            this.txtNombreProtocolo = new System.Windows.Forms.TextBox();
            this.cbEmpresaTrabajo = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtDocNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCamaHosp = new System.Windows.Forms.TextBox();
            this.btnAgregarEmpresaContrata = new System.Windows.Forms.Button();
            this.chkEsActivo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEps = new System.Windows.Forms.TextBox();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.txtCentroCosto = new System.Windows.Forms.TextBox();
            this.lblEps = new System.Windows.Forms.Label();
            this.lblFactor = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBedHospital = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboVendedor = new System.Windows.Forms.ComboBox();
            this.txtComision = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEsComisionable = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpExamenes = new System.Windows.Forms.TabPage();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.tpUsuariosExternos = new System.Windows.Forms.TabPage();
            this.btnAddUserExternal = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.BtnNew = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grdExternalUser = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblRecordCountExternalUSer = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdProtocolComponent)).BeginInit();
            this.cmProtocol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvProtocol)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpExamenes.SuspendLayout();
            this.tpUsuariosExternos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExternalUser)).BeginInit();
            this.SuspendLayout();
            // 
            // grdProtocolComponent
            // 
            this.grdProtocolComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdProtocolComponent.CausesValidation = false;
            this.grdProtocolComponent.ContextMenuStrip = this.cmProtocol;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdProtocolComponent.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "Componente";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 305;
            ultraGridColumn4.Header.Caption = "Precio";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.Header.Caption = "Operador";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 68;
            ultraGridColumn6.Header.Caption = "Edad";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 61;
            ultraGridColumn7.Header.Caption = "Género";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.Caption = "Es Condic.";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 75;
            ultraGridColumn9.Header.Caption = "Tipo";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 99;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9});
            this.grdProtocolComponent.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdProtocolComponent.DisplayLayout.InterBandSpacing = 10;
            this.grdProtocolComponent.DisplayLayout.MaxColScrollRegions = 1;
            this.grdProtocolComponent.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdProtocolComponent.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdProtocolComponent.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdProtocolComponent.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdProtocolComponent.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdProtocolComponent.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdProtocolComponent.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdProtocolComponent.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdProtocolComponent.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdProtocolComponent.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdProtocolComponent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdProtocolComponent.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdProtocolComponent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdProtocolComponent.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdProtocolComponent.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdProtocolComponent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdProtocolComponent.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdProtocolComponent.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdProtocolComponent.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdProtocolComponent.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdProtocolComponent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdProtocolComponent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdProtocolComponent.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdProtocolComponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdProtocolComponent.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdProtocolComponent.Location = new System.Drawing.Point(17, 28);
            this.grdProtocolComponent.Margin = new System.Windows.Forms.Padding(2);
            this.grdProtocolComponent.Name = "grdProtocolComponent";
            this.grdProtocolComponent.Size = new System.Drawing.Size(854, 383);
            this.grdProtocolComponent.TabIndex = 46;
            this.grdProtocolComponent.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdProtocolComponent_AfterSelectChange);
            this.grdProtocolComponent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdProtocolComponent_MouseDown);
            // 
            // cmProtocol
            // 
            this.cmProtocol.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New,
            this.Edit,
            this.delete,
            this.verCambiosToolStripMenuItem});
            this.cmProtocol.Name = "contextMenuStrip1";
            this.cmProtocol.Size = new System.Drawing.Size(141, 92);
            // 
            // New
            // 
            this.New.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_form;
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(140, 22);
            this.New.Text = "Nuevo";
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // Edit
            // 
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(140, 22);
            this.Edit.Text = "Modificar";
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // delete
            // 
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(140, 22);
            this.delete.Text = "Eliminar";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // verCambiosToolStripMenuItem
            // 
            this.verCambiosToolStripMenuItem.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.verCambiosToolStripMenuItem.Name = "verCambiosToolStripMenuItem";
            this.verCambiosToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.verCambiosToolStripMenuItem.Text = "Ver Cambios";
            this.verCambiosToolStripMenuItem.Click += new System.EventHandler(this.verCambiosToolStripMenuItem_Click);
            // 
            // lblRecordCount2
            // 
            this.lblRecordCount2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount2.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCount2.Location = new System.Drawing.Point(603, 8);
            this.lblRecordCount2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCount2.Name = "lblRecordCount2";
            this.lblRecordCount2.Size = new System.Drawing.Size(259, 18);
            this.lblRecordCount2.TabIndex = 46;
            this.lblRecordCount2.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCount2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRecordCount2.Click += new System.EventHandler(this.lblRecordCount2_Click);
            // 
            // cbEmpresaCliente
            // 
            this.cbEmpresaCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmpresaCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresaCliente.DropDownWidth = 500;
            this.cbEmpresaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpresaCliente.FormattingEnabled = true;
            this.cbEmpresaCliente.Location = new System.Drawing.Point(112, 51);
            this.cbEmpresaCliente.Name = "cbEmpresaCliente";
            this.cbEmpresaCliente.Size = new System.Drawing.Size(437, 21);
            this.cbEmpresaCliente.TabIndex = 22;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaCliente).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbEmpresaCliente).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbEmpresaCliente).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaCliente).IsRequired = true;
            this.cbEmpresaCliente.SelectedIndexChanged += new System.EventHandler(this.cbOrganizationInvoice_SelectedIndexChanged);
            // 
            // cbGeso
            // 
            this.cbGeso.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbGeso.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbGeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGeso.FormattingEnabled = true;
            this.cbGeso.Location = new System.Drawing.Point(686, 24);
            this.cbGeso.Name = "cbGeso";
            this.cbGeso.Size = new System.Drawing.Size(285, 21);
            this.cbGeso.TabIndex = 18;
            this.uvProtocol.GetValidationSettings(this.cbGeso).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbGeso).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbGeso).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.cbGeso).IsRequired = true;
            this.cbGeso.SelectedIndexChanged += new System.EventHandler(this.cbGeso_SelectedIndexChanged);
            // 
            // cbTipoExamen
            // 
            this.cbTipoExamen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTipoExamen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTipoExamen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoExamen.FormattingEnabled = true;
            this.cbTipoExamen.Location = new System.Drawing.Point(112, 132);
            this.cbTipoExamen.Name = "cbTipoExamen";
            this.cbTipoExamen.Size = new System.Drawing.Size(437, 21);
            this.cbTipoExamen.TabIndex = 16;
            this.uvProtocol.GetValidationSettings(this.cbTipoExamen).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbTipoExamen).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbTipoExamen).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.cbTipoExamen).IsRequired = true;
            this.cbTipoExamen.SelectedIndexChanged += new System.EventHandler(this.cbEsoType_SelectedIndexChanged);
            // 
            // cbEmpresaEmpleadora
            // 
            this.cbEmpresaEmpleadora.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmpresaEmpleadora.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresaEmpleadora.DropDownWidth = 500;
            this.cbEmpresaEmpleadora.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpresaEmpleadora.FormattingEnabled = true;
            this.cbEmpresaEmpleadora.Location = new System.Drawing.Point(112, 78);
            this.cbEmpresaEmpleadora.Name = "cbEmpresaEmpleadora";
            this.cbEmpresaEmpleadora.Size = new System.Drawing.Size(437, 21);
            this.cbEmpresaEmpleadora.TabIndex = 14;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaEmpleadora).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbEmpresaEmpleadora).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbEmpresaEmpleadora).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaEmpleadora).IsRequired = true;
            this.cbEmpresaEmpleadora.SelectedIndexChanged += new System.EventHandler(this.cbOrganization_SelectedIndexChanged);
            // 
            // cbTipoServicio
            // 
            this.cbTipoServicio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTipoServicio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTipoServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoServicio.FormattingEnabled = true;
            this.cbTipoServicio.Location = new System.Drawing.Point(686, 51);
            this.cbTipoServicio.Name = "cbTipoServicio";
            this.cbTipoServicio.Size = new System.Drawing.Size(285, 21);
            this.cbTipoServicio.TabIndex = 26;
            this.uvProtocol.GetValidationSettings(this.cbTipoServicio).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbTipoServicio).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbTipoServicio).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.cbTipoServicio).IsRequired = true;
            this.cbTipoServicio.SelectedValueChanged += new System.EventHandler(this.cbTipoServicio_SelectedValueChanged);
            this.cbTipoServicio.TextChanged += new System.EventHandler(this.cbServiceType_TextChanged);
            // 
            // cbServicio
            // 
            this.cbServicio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbServicio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbServicio.FormattingEnabled = true;
            this.cbServicio.Location = new System.Drawing.Point(686, 78);
            this.cbServicio.Name = "cbServicio";
            this.cbServicio.Size = new System.Drawing.Size(285, 21);
            this.cbServicio.TabIndex = 32;
            this.uvProtocol.GetValidationSettings(this.cbServicio).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbServicio).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbServicio).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.cbServicio).IsRequired = true;
            this.cbServicio.SelectedIndexChanged += new System.EventHandler(this.cbService_SelectedIndexChanged);
            // 
            // txtNombreProtocolo
            // 
            this.txtNombreProtocolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreProtocolo.Location = new System.Drawing.Point(112, 25);
            this.txtNombreProtocolo.MaxLength = 100;
            this.txtNombreProtocolo.Name = "txtNombreProtocolo";
            this.txtNombreProtocolo.Size = new System.Drawing.Size(437, 20);
            this.txtNombreProtocolo.TabIndex = 11;
            this.uvProtocol.GetValidationSettings(this.txtNombreProtocolo).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.txtNombreProtocolo).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvProtocol.GetValidationSettings(this.txtNombreProtocolo).IsRequired = true;
            this.txtNombreProtocolo.TextChanged += new System.EventHandler(this.txtProtocolName_TextChanged);
            // 
            // cbEmpresaTrabajo
            // 
            this.cbEmpresaTrabajo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmpresaTrabajo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresaTrabajo.DropDownWidth = 500;
            this.cbEmpresaTrabajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpresaTrabajo.FormattingEnabled = true;
            this.cbEmpresaTrabajo.Location = new System.Drawing.Point(112, 105);
            this.cbEmpresaTrabajo.Name = "cbEmpresaTrabajo";
            this.cbEmpresaTrabajo.Size = new System.Drawing.Size(437, 21);
            this.cbEmpresaTrabajo.TabIndex = 20;
            this.uvProtocol.GetValidationSettings(this.cbEmpresaTrabajo).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvProtocol.GetValidationSettings(this.cbEmpresaTrabajo).DataType = typeof(string);
            this.uvProtocol.GetValidationSettings(this.cbEmpresaTrabajo).IsRequired = true;
            this.cbEmpresaTrabajo.SelectedIndexChanged += new System.EventHandler(this.cbIntermediaryOrganization_SelectedIndexChanged);
            // 
            // txtUser
            // 
            this.txtUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUser.Location = new System.Drawing.Point(68, 23);
            this.txtUser.MaxLength = 100;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(338, 20);
            this.txtUser.TabIndex = 49;
            this.uvProtocol.GetValidationSettings(this.txtUser).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            // 
            // txtDocNumber
            // 
            this.txtDocNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocNumber.Location = new System.Drawing.Point(475, 23);
            this.txtDocNumber.MaxLength = 100;
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(133, 20);
            this.txtDocNumber.TabIndex = 51;
            this.uvProtocol.GetValidationSettings(this.txtDocNumber).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.lblDescuento);
            this.groupBox1.Controls.Add(this.chkEsActivo);
            this.groupBox1.Controls.Add(this.txtCamaHosp);
            this.groupBox1.Controls.Add(this.btnAgregarEmpresaContrata);
            this.groupBox1.Controls.Add(this.cbEmpresaCliente);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbServicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbTipoServicio);
            this.groupBox1.Controls.Add(this.txtEps);
            this.groupBox1.Controls.Add(this.txtFactor);
            this.groupBox1.Controls.Add(this.txtCentroCosto);
            this.groupBox1.Controls.Add(this.lblEps);
            this.groupBox1.Controls.Add(this.lblFactor);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbEmpresaTrabajo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbGeso);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbTipoExamen);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbEmpresaEmpleadora);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtNombreProtocolo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblBedHospital);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(17, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1017, 181);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Protocolo";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtCamaHosp
            // 
            this.txtCamaHosp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCamaHosp.Location = new System.Drawing.Point(834, 132);
            this.txtCamaHosp.MaxLength = 250;
            this.txtCamaHosp.Name = "txtCamaHosp";
            this.txtCamaHosp.Size = new System.Drawing.Size(42, 20);
            this.txtCamaHosp.TabIndex = 24;
            this.txtCamaHosp.Text = "0.00";
            this.txtCamaHosp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCamaHosp_KeyPress);
            // 
            // btnAgregarEmpresaContrata
            // 
            this.btnAgregarEmpresaContrata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAgregarEmpresaContrata.BackColor = System.Drawing.SystemColors.Control;
            this.btnAgregarEmpresaContrata.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnAgregarEmpresaContrata.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAgregarEmpresaContrata.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnAgregarEmpresaContrata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarEmpresaContrata.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarEmpresaContrata.ForeColor = System.Drawing.Color.Black;
            this.btnAgregarEmpresaContrata.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarEmpresaContrata.Image")));
            this.btnAgregarEmpresaContrata.Location = new System.Drawing.Point(553, 91);
            this.btnAgregarEmpresaContrata.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarEmpresaContrata.Name = "btnAgregarEmpresaContrata";
            this.btnAgregarEmpresaContrata.Size = new System.Drawing.Size(27, 21);
            this.btnAgregarEmpresaContrata.TabIndex = 61;
            this.toolTip1.SetToolTip(this.btnAgregarEmpresaContrata, "Agregar Empresa Contratista");
            this.btnAgregarEmpresaContrata.UseVisualStyleBackColor = false;
            this.btnAgregarEmpresaContrata.Click += new System.EventHandler(this.btnAgregarEmpresaContrata_Click);
            // 
            // chkEsActivo
            // 
            this.chkEsActivo.AutoSize = true;
            this.chkEsActivo.Checked = true;
            this.chkEsActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEsActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEsActivo.ForeColor = System.Drawing.Color.Black;
            this.chkEsActivo.Location = new System.Drawing.Point(911, 164);
            this.chkEsActivo.Name = "chkEsActivo";
            this.chkEsActivo.Size = new System.Drawing.Size(56, 17);
            this.chkEsActivo.TabIndex = 36;
            this.chkEsActivo.Text = "Activo";
            this.chkEsActivo.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "Emp. Cliente";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(622, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 19);
            this.label1.TabIndex = 31;
            this.label1.Text = "Servicio";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(596, 52);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "Tipo Servicio";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txtEps
            // 
            this.txtEps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEps.Location = new System.Drawing.Point(720, 132);
            this.txtEps.MaxLength = 250;
            this.txtEps.Name = "txtEps";
            this.txtEps.Size = new System.Drawing.Size(42, 20);
            this.txtEps.TabIndex = 24;
            this.txtEps.Text = "0.00";
            this.txtEps.TextChanged += new System.EventHandler(this.txtCostCenter_TextChanged);
            this.txtEps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEps_KeyPress);
            // 
            // txtFactor
            // 
            this.txtFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFactor.Location = new System.Drawing.Point(626, 132);
            this.txtFactor.MaxLength = 250;
            this.txtFactor.Name = "txtFactor";
            this.txtFactor.Size = new System.Drawing.Size(42, 20);
            this.txtFactor.TabIndex = 24;
            this.txtFactor.Text = "0.00";
            this.toolTip1.SetToolTip(this.txtFactor, "Solo acepta números.");
            this.txtFactor.TextChanged += new System.EventHandler(this.txtCostCenter_TextChanged);
            this.txtFactor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFactor_KeyPress);
            // 
            // txtCentroCosto
            // 
            this.txtCentroCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCentroCosto.Location = new System.Drawing.Point(686, 105);
            this.txtCentroCosto.MaxLength = 250;
            this.txtCentroCosto.Name = "txtCentroCosto";
            this.txtCentroCosto.Size = new System.Drawing.Size(285, 20);
            this.txtCentroCosto.TabIndex = 24;
            this.txtCentroCosto.TextChanged += new System.EventHandler(this.txtCostCenter_TextChanged);
            // 
            // lblEps
            // 
            this.lblEps.BackColor = System.Drawing.Color.Transparent;
            this.lblEps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEps.ForeColor = System.Drawing.Color.Black;
            this.lblEps.Location = new System.Drawing.Point(686, 132);
            this.lblEps.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEps.Name = "lblEps";
            this.lblEps.Size = new System.Drawing.Size(48, 20);
            this.lblEps.TabIndex = 23;
            this.lblEps.Text = "E.P.S";
            this.lblEps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEps.Visible = false;
            // 
            // lblFactor
            // 
            this.lblFactor.BackColor = System.Drawing.Color.Transparent;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.ForeColor = System.Drawing.Color.Black;
            this.lblFactor.Location = new System.Drawing.Point(564, 132);
            this.lblFactor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(48, 20);
            this.lblFactor.TabIndex = 23;
            this.lblFactor.Text = "Factor";
            this.lblFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFactor.Visible = false;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(624, 104);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 20);
            this.label13.TabIndex = 23;
            this.label13.Text = "C/Costo";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(8, 105);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 21);
            this.label7.TabIndex = 19;
            this.label7.Text = "Emp. de Trabajo";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(622, 27);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 19);
            this.label9.TabIndex = 17;
            this.label9.Text = "GESO";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(8, 134);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 19);
            this.label10.TabIndex = 15;
            this.label10.Text = "Tipo de Examen";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(8, 75);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 27);
            this.label11.TabIndex = 13;
            this.label11.Text = "Emp. Empleadora (Contratista)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(8, 25);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 21);
            this.label12.TabIndex = 12;
            this.label12.Text = "Nombre Proto.";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBedHospital
            // 
            this.lblBedHospital.BackColor = System.Drawing.Color.Transparent;
            this.lblBedHospital.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBedHospital.ForeColor = System.Drawing.Color.Black;
            this.lblBedHospital.Location = new System.Drawing.Point(772, 132);
            this.lblBedHospital.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBedHospital.Name = "lblBedHospital";
            this.lblBedHospital.Size = new System.Drawing.Size(71, 20);
            this.lblBedHospital.TabIndex = 23;
            this.lblBedHospital.Text = "Cama Hosp.";
            this.lblBedHospital.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBedHospital.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(624, 642);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 20);
            this.label4.TabIndex = 63;
            this.label4.Text = "Vendedor";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // cboVendedor
            // 
            this.cboVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVendedor.FormattingEnabled = true;
            this.cboVendedor.Location = new System.Drawing.Point(689, 642);
            this.cboVendedor.Margin = new System.Windows.Forms.Padding(2);
            this.cboVendedor.Name = "cboVendedor";
            this.cboVendedor.Size = new System.Drawing.Size(47, 21);
            this.cboVendedor.TabIndex = 62;
            this.cboVendedor.Visible = false;
            // 
            // txtComision
            // 
            this.txtComision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtComision.Enabled = false;
            this.txtComision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComision.Location = new System.Drawing.Point(458, 642);
            this.txtComision.MaxLength = 250;
            this.txtComision.Name = "txtComision";
            this.txtComision.Size = new System.Drawing.Size(47, 20);
            this.txtComision.TabIndex = 35;
            this.txtComision.Visible = false;
            this.txtComision.TextChanged += new System.EventHandler(this.txtValidDays_TextChanged);
            this.txtComision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValidDays_KeyPress);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(510, 642);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "% de Comisión";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // chkEsComisionable
            // 
            this.chkEsComisionable.AutoSize = true;
            this.chkEsComisionable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEsComisionable.ForeColor = System.Drawing.Color.Black;
            this.chkEsComisionable.Location = new System.Drawing.Point(344, 646);
            this.chkEsComisionable.Name = "chkEsComisionable";
            this.chkEsComisionable.Size = new System.Drawing.Size(108, 17);
            this.chkEsComisionable.TabIndex = 33;
            this.chkEsComisionable.Text = "Es comisionable?";
            this.chkEsComisionable.UseVisualStyleBackColor = true;
            this.chkEsComisionable.Visible = false;
            this.chkEsComisionable.CheckedChanged += new System.EventHandler(this.chkIsHasVigency_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpExamenes);
            this.tabControl1.Controls.Add(this.tpUsuariosExternos);
            this.tabControl1.Location = new System.Drawing.Point(17, 198);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(971, 442);
            this.tabControl1.TabIndex = 47;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpExamenes
            // 
            this.tpExamenes.Controls.Add(this.btnRemover);
            this.tpExamenes.Controls.Add(this.btnEditar);
            this.tpExamenes.Controls.Add(this.btnNuevo);
            this.tpExamenes.Controls.Add(this.grdProtocolComponent);
            this.tpExamenes.Controls.Add(this.lblRecordCount2);
            this.tpExamenes.Location = new System.Drawing.Point(4, 22);
            this.tpExamenes.Name = "tpExamenes";
            this.tpExamenes.Padding = new System.Windows.Forms.Padding(3);
            this.tpExamenes.Size = new System.Drawing.Size(963, 416);
            this.tpExamenes.TabIndex = 0;
            this.tpExamenes.Text = "Examenes";
            this.tpExamenes.UseVisualStyleBackColor = true;
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemover.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemover.Enabled = false;
            this.btnRemover.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRemover.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnRemover.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.ForeColor = System.Drawing.Color.Black;
            this.btnRemover.Image = ((System.Drawing.Image)(resources.GetObject("btnRemover.Image")));
            this.btnRemover.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemover.Location = new System.Drawing.Point(875, 86);
            this.btnRemover.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(75, 24);
            this.btnRemover.TabIndex = 94;
            this.btnRemover.Text = "     Eliminar";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.BackColor = System.Drawing.SystemColors.Control;
            this.btnEditar.Enabled = false;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnEditar.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.Black;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(875, 58);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 24);
            this.btnEditar.TabIndex = 61;
            this.btnEditar.Text = "      Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevo.BackColor = System.Drawing.SystemColors.Control;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.Black;
            this.btnNuevo.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_form;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(875, 30);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 24);
            this.btnNuevo.TabIndex = 60;
            this.btnNuevo.Text = "     Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // tpUsuariosExternos
            // 
            this.tpUsuariosExternos.Controls.Add(this.btnAddUserExternal);
            this.tpUsuariosExternos.Controls.Add(this.btnDelete);
            this.tpUsuariosExternos.Controls.Add(this.btnEdit);
            this.tpUsuariosExternos.Controls.Add(this.BtnNew);
            this.tpUsuariosExternos.Controls.Add(this.btnFilter);
            this.tpUsuariosExternos.Controls.Add(this.txtDocNumber);
            this.tpUsuariosExternos.Controls.Add(this.label6);
            this.tpUsuariosExternos.Controls.Add(this.txtUser);
            this.tpUsuariosExternos.Controls.Add(this.label5);
            this.tpUsuariosExternos.Controls.Add(this.grdExternalUser);
            this.tpUsuariosExternos.Controls.Add(this.lblRecordCountExternalUSer);
            this.tpUsuariosExternos.Location = new System.Drawing.Point(4, 22);
            this.tpUsuariosExternos.Name = "tpUsuariosExternos";
            this.tpUsuariosExternos.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsuariosExternos.Size = new System.Drawing.Size(938, 416);
            this.tpUsuariosExternos.TabIndex = 1;
            this.tpUsuariosExternos.Text = "Usuarios Externos";
            this.tpUsuariosExternos.UseVisualStyleBackColor = true;
            // 
            // btnAddUserExternal
            // 
            this.btnAddUserExternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUserExternal.Image = global::Sigesoft.Node.WinClient.UI.Resources.user_add;
            this.btnAddUserExternal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddUserExternal.Location = new System.Drawing.Point(632, 385);
            this.btnAddUserExternal.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddUserExternal.Name = "btnAddUserExternal";
            this.btnAddUserExternal.Size = new System.Drawing.Size(167, 26);
            this.btnAddUserExternal.TabIndex = 57;
            this.btnAddUserExternal.Text = "Agregar Usuarios Externos";
            this.btnAddUserExternal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddUserExternal.UseVisualStyleBackColor = true;
            this.btnAddUserExternal.Click += new System.EventHandler(this.btnAddUserExternal_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(803, 155);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 24);
            this.btnDelete.TabIndex = 56;
            this.btnDelete.Text = "    Eliminar";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(803, 127);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(88, 24);
            this.btnEdit.TabIndex = 55;
            this.btnEdit.Text = "Modificar";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNew.Image = global::Sigesoft.Node.WinClient.UI.Resources.application_form;
            this.BtnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnNew.Location = new System.Drawing.Point(803, 99);
            this.BtnNew.Margin = new System.Windows.Forms.Padding(2);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(88, 24);
            this.BtnNew.TabIndex = 54;
            this.BtnNew.Text = "    Nuevo";
            this.BtnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnNew.UseVisualStyleBackColor = true;
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.find;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(705, 20);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 24);
            this.btnFilter.TabIndex = 53;
            this.btnFilter.Text = "  Filtrar";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(421, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 32);
            this.label6.TabIndex = 52;
            this.label6.Text = "DNI";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 32);
            this.label5.TabIndex = 50;
            this.label5.Text = "Usuario";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdExternalUser
            // 
            this.grdExternalUser.CausesValidation = false;
            appearance8.BackColor = System.Drawing.Color.White;
            appearance8.BackColor2 = System.Drawing.Color.LightGray;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdExternalUser.DisplayLayout.Appearance = appearance8;
            ultraGridColumn10.Header.VisiblePosition = 0;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.Caption = "Nombres";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Width = 340;
            ultraGridColumn13.Header.Caption = "Usuario";
            ultraGridColumn13.Header.VisiblePosition = 3;
            ultraGridColumn13.Width = 172;
            ultraGridColumn14.Header.Caption = "DNI";
            ultraGridColumn14.Header.VisiblePosition = 4;
            ultraGridColumn14.Width = 123;
            ultraGridColumn15.Header.Caption = "Fecha Caducidad";
            ultraGridColumn15.Header.VisiblePosition = 5;
            ultraGridColumn15.Width = 147;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15});
            this.grdExternalUser.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdExternalUser.DisplayLayout.InterBandSpacing = 10;
            this.grdExternalUser.DisplayLayout.MaxColScrollRegions = 1;
            this.grdExternalUser.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdExternalUser.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdExternalUser.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdExternalUser.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdExternalUser.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdExternalUser.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdExternalUser.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdExternalUser.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.grdExternalUser.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.BackColor2 = System.Drawing.Color.White;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdExternalUser.DisplayLayout.Override.CellAppearance = appearance10;
            this.grdExternalUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.Color.White;
            appearance11.BackColor2 = System.Drawing.Color.LightGray;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance11.BorderColor = System.Drawing.Color.DarkGray;
            appearance11.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdExternalUser.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdExternalUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance12.AlphaLevel = ((short)(187));
            appearance12.BackColor = System.Drawing.Color.Gainsboro;
            appearance12.BackColor2 = System.Drawing.Color.LightGray;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdExternalUser.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdExternalUser.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.grdExternalUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance14.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance14.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance14.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance14.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance14.FontData.BoldAsString = "False";
            appearance14.ForeColor = System.Drawing.Color.Black;
            this.grdExternalUser.DisplayLayout.Override.SelectedRowAppearance = appearance14;
            this.grdExternalUser.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdExternalUser.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdExternalUser.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdExternalUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdExternalUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdExternalUser.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdExternalUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdExternalUser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdExternalUser.Location = new System.Drawing.Point(17, 77);
            this.grdExternalUser.Margin = new System.Windows.Forms.Padding(2);
            this.grdExternalUser.Name = "grdExternalUser";
            this.grdExternalUser.Size = new System.Drawing.Size(782, 295);
            this.grdExternalUser.TabIndex = 47;
            this.grdExternalUser.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdExternalUser_AfterSelectChange);
            // 
            // lblRecordCountExternalUSer
            // 
            this.lblRecordCountExternalUSer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountExternalUSer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountExternalUSer.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountExternalUSer.Location = new System.Drawing.Point(572, 57);
            this.lblRecordCountExternalUSer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountExternalUSer.Name = "lblRecordCountExternalUSer";
            this.lblRecordCountExternalUSer.Size = new System.Drawing.Size(259, 18);
            this.lblRecordCountExternalUSer.TabIndex = 48;
            this.lblRecordCountExternalUSer.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountExternalUSer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(915, 642);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Salir";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(836, 642);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Guardar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(929, 132);
            this.txtDiscount.MaxLength = 250;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(42, 20);
            this.txtDiscount.TabIndex = 63;
            this.txtDiscount.Text = "0.00";
            // 
            // lblDescuento
            // 
            this.lblDescuento.BackColor = System.Drawing.Color.Transparent;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.ForeColor = System.Drawing.Color.Black;
            this.lblDescuento.Location = new System.Drawing.Point(881, 132);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(43, 20);
            this.lblDescuento.TabIndex = 62;
            this.lblDescuento.Text = "Dscto";
            this.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescuento.Visible = false;
            // 
            // frmProtocolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1010, 677);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboVendedor);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComision);
            this.Controls.Add(this.chkEsComisionable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProtocolEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Protocolo";
            this.Load += new System.EventHandler(this.frmProtocolEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdProtocolComponent)).EndInit();
            this.cmProtocol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uvProtocol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpExamenes.ResumeLayout(false);
            this.tpUsuariosExternos.ResumeLayout(false);
            this.tpUsuariosExternos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExternalUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdProtocolComponent;
        private System.Windows.Forms.Label lblRecordCount2;
        private Infragistics.Win.Misc.UltraValidator uvProtocol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCentroCosto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbEmpresaCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEmpresaTrabajo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbGeso;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbTipoExamen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbEmpresaEmpleadora;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNombreProtocolo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ContextMenuStrip cmProtocol;
        private System.Windows.Forms.ToolStripMenuItem New;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem delete;
        private System.Windows.Forms.ComboBox cbTipoServicio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbServicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComision;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEsComisionable;
        private System.Windows.Forms.CheckBox chkEsActivo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpExamenes;
        private System.Windows.Forms.TabPage tpUsuariosExternos;
        private System.Windows.Forms.TextBox txtDocNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label5;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdExternalUser;
        private System.Windows.Forms.Label lblRecordCountExternalUSer;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button BtnNew;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnAgregarEmpresaContrata;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAddUserExternal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboVendedor;
        private System.Windows.Forms.TextBox txtEps;
        private System.Windows.Forms.TextBox txtFactor;
        private System.Windows.Forms.Label lblEps;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.TextBox txtCamaHosp;
        private System.Windows.Forms.Label lblBedHospital;
        private System.Windows.Forms.ToolStripMenuItem verCambiosToolStripMenuItem;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblDescuento;
    }
}