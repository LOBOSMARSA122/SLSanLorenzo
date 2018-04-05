namespace Sigesoft.Node.WinClient.UI
{
    partial class frmEntryToMedical
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Pacient");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OrganizationIntermediaryName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ServiceName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocolName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CalendarStatusName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_EntryTimeCM");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_EntryToMedicalCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_Pacient");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_FirstLastName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SecondLastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SexTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Birthdate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_DocTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_SexTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OccupationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_OrganitationName");
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.txtDocNumber = new System.Windows.Forms.TextBox();
            this.ddlDocTypeId = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.grdData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuGridCancelar = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblRecordCountTotal = new System.Windows.Forms.Label();
            this.uvCM = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.txtNameOrOrganization = new System.Windows.Forms.TextBox();
            this.lblRecordCountPendiente = new System.Windows.Forms.Label();
            this.btnUpdateandSelect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSearchAuthoritation = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grdDataPeopleAuthoritation = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.uvPersonAuthoritation = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.btnCancelPersonAuthoritation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvCM)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataPeopleAuthoritation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvPersonAuthoritation)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDocNumber
            // 
            this.txtDocNumber.Location = new System.Drawing.Point(405, 21);
            this.txtDocNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtDocNumber.MaxLength = 20;
            this.txtDocNumber.Name = "txtDocNumber";
            this.txtDocNumber.Size = new System.Drawing.Size(137, 20);
            this.txtDocNumber.TabIndex = 16;
            this.uvCM.GetValidationSettings(this.txtDocNumber).DataType = typeof(string);
            this.uvCM.GetValidationSettings(this.txtDocNumber).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvCM.GetValidationSettings(this.txtDocNumber).IsRequired = true;
            this.txtDocNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocNumber_KeyPress);
            // 
            // ddlDocTypeId
            // 
            this.ddlDocTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlDocTypeId.DropDownWidth = 220;
            this.ddlDocTypeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDocTypeId.FormattingEnabled = true;
            this.ddlDocTypeId.Location = new System.Drawing.Point(101, 21);
            this.ddlDocTypeId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlDocTypeId.Name = "ddlDocTypeId";
            this.ddlDocTypeId.Size = new System.Drawing.Size(166, 21);
            this.ddlDocTypeId.TabIndex = 15;
            this.uvCM.GetValidationSettings(this.ddlDocTypeId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvCM.GetValidationSettings(this.ddlDocTypeId).IsRequired = true;
            this.ddlDocTypeId.SelectedIndexChanged += new System.EventHandler(this.ddlDocTypeId_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(292, 24);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Número Documento";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(11, 24);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Tipo Documento";
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.ForeColor = System.Drawing.Color.Black;
            this.btnFilter.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFilter.Location = new System.Drawing.Point(601, 17);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(128, 24);
            this.btnFilter.TabIndex = 19;
            this.btnFilter.Text = "Buscar y Registrar";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // grdData
            // 
            this.grdData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdData.CausesValidation = false;
            this.grdData.ContextMenuStrip = this.contextMenuStrip1;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdData.DisplayLayout.Appearance = appearance1;
            ultraGridColumn37.Header.Caption = "Documento";
            ultraGridColumn37.Header.VisiblePosition = 0;
            ultraGridColumn37.Width = 70;
            ultraGridColumn38.Header.Caption = "Número";
            ultraGridColumn38.Header.VisiblePosition = 1;
            ultraGridColumn38.Width = 79;
            ultraGridColumn12.Header.Caption = "Trabajador(a)";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Width = 216;
            ultraGridColumn13.Header.Caption = "Empresa";
            ultraGridColumn13.Header.VisiblePosition = 3;
            ultraGridColumn13.Width = 165;
            ultraGridColumn16.Header.Caption = "Servicio";
            ultraGridColumn16.Header.VisiblePosition = 4;
            ultraGridColumn16.Width = 150;
            ultraGridColumn15.Header.Caption = "Protocolo";
            ultraGridColumn15.Header.VisiblePosition = 5;
            ultraGridColumn15.Width = 233;
            ultraGridColumn14.Header.Caption = "Estado Cita";
            ultraGridColumn14.Header.VisiblePosition = 6;
            ultraGridColumn40.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn40.Header.Caption = "Fecha Ingreso CM";
            ultraGridColumn40.Header.VisiblePosition = 7;
            ultraGridColumn40.Width = 117;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn16,
            ultraGridColumn15,
            ultraGridColumn14,
            ultraGridColumn40});
            this.grdData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdData.DisplayLayout.InterBandSpacing = 10;
            this.grdData.DisplayLayout.MaxColScrollRegions = 1;
            this.grdData.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdData.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdData.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdData.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdData.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdData.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdData.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdData.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdData.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.Color.DarkGray;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdData.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdData.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdData.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.BorderColor2 = System.Drawing.SystemColors.GradientActiveCaption;
            appearance7.FontData.BoldAsString = "False";
            appearance7.ForeColor = System.Drawing.Color.Black;
            this.grdData.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdData.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdData.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdData.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdData.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdData.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdData.Location = new System.Drawing.Point(17, 81);
            this.grdData.Margin = new System.Windows.Forms.Padding(2);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(753, 290);
            this.grdData.TabIndex = 44;
            this.grdData.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.grdData_InitializeRow);
            this.grdData.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.grdData_AfterSelectChange);
            this.grdData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdData_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridCancelar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 26);
            // 
            // mnuGridCancelar
            // 
            this.mnuGridCancelar.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.mnuGridCancelar.Name = "mnuGridCancelar";
            this.mnuGridCancelar.Size = new System.Drawing.Size(196, 22);
            this.mnuGridCancelar.Text = "Cancelar Ingreso al CM";
            this.mnuGridCancelar.Click += new System.EventHandler(this.mnuGridCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtDocNumber);
            this.groupBox1.Controls.Add(this.ddlDocTypeId);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 48);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registro Ingreso Agendados";
            // 
            // lblRecordCountTotal
            // 
            this.lblRecordCountTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountTotal.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountTotal.Location = new System.Drawing.Point(552, 63);
            this.lblRecordCountTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountTotal.Name = "lblRecordCountTotal";
            this.lblRecordCountTotal.Size = new System.Drawing.Size(83, 16);
            this.lblRecordCountTotal.TabIndex = 52;
            this.lblRecordCountTotal.Text = "Total :";
            this.lblRecordCountTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNameOrOrganization
            // 
            this.txtNameOrOrganization.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNameOrOrganization.Location = new System.Drawing.Point(127, 16);
            this.txtNameOrOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.txtNameOrOrganization.MaxLength = 100;
            this.txtNameOrOrganization.Name = "txtNameOrOrganization";
            this.txtNameOrOrganization.Size = new System.Drawing.Size(416, 20);
            this.txtNameOrOrganization.TabIndex = 16;
            this.uvPersonAuthoritation.GetValidationSettings(this.txtNameOrOrganization).DataType = typeof(string);
            this.uvPersonAuthoritation.GetValidationSettings(this.txtNameOrOrganization).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.uvPersonAuthoritation.GetValidationSettings(this.txtNameOrOrganization).IsRequired = true;
            this.txtNameOrOrganization.TextChanged += new System.EventHandler(this.txtNameOrOrganization_TextChanged);
            // 
            // lblRecordCountPendiente
            // 
            this.lblRecordCountPendiente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountPendiente.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountPendiente.Location = new System.Drawing.Point(654, 63);
            this.lblRecordCountPendiente.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountPendiente.Name = "lblRecordCountPendiente";
            this.lblRecordCountPendiente.Size = new System.Drawing.Size(101, 16);
            this.lblRecordCountPendiente.TabIndex = 54;
            this.lblRecordCountPendiente.Text = "Pendientes  : ";
            this.lblRecordCountPendiente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRecordCountPendiente.Click += new System.EventHandler(this.lblRecordCountPendiente_Click);
            // 
            // btnUpdateandSelect
            // 
            this.btnUpdateandSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateandSelect.Enabled = false;
            this.btnUpdateandSelect.ForeColor = System.Drawing.Color.Black;
            this.btnUpdateandSelect.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.btnUpdateandSelect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUpdateandSelect.Location = new System.Drawing.Point(774, 81);
            this.btnUpdateandSelect.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateandSelect.Name = "btnUpdateandSelect";
            this.btnUpdateandSelect.Size = new System.Drawing.Size(92, 43);
            this.btnUpdateandSelect.TabIndex = 55;
            this.btnUpdateandSelect.Text = "Cancelar Ingreso al CM";
            this.btnUpdateandSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateandSelect.UseVisualStyleBackColor = true;
            this.btnUpdateandSelect.Click += new System.EventHandler(this.btnUpdateandSelect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSearchAuthoritation);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtNameOrOrganization);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(17, 376);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(753, 43);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registro Ingreso Autorizados";
            // 
            // btnSearchAuthoritation
            // 
            this.btnSearchAuthoritation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchAuthoritation.ForeColor = System.Drawing.Color.Black;
            this.btnSearchAuthoritation.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnSearchAuthoritation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchAuthoritation.Location = new System.Drawing.Point(601, 14);
            this.btnSearchAuthoritation.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearchAuthoritation.Name = "btnSearchAuthoritation";
            this.btnSearchAuthoritation.Size = new System.Drawing.Size(73, 24);
            this.btnSearchAuthoritation.TabIndex = 19;
            this.btnSearchAuthoritation.Text = "Buscar";
            this.btnSearchAuthoritation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchAuthoritation.UseVisualStyleBackColor = true;
            this.btnSearchAuthoritation.Click += new System.EventHandler(this.btnSearchAuthoritation_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(27, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Nombre / Empresa";
            // 
            // grdDataPeopleAuthoritation
            // 
            this.grdDataPeopleAuthoritation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataPeopleAuthoritation.CausesValidation = false;
            appearance8.BackColor = System.Drawing.Color.White;
            appearance8.BackColor2 = System.Drawing.Color.LightGray;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataPeopleAuthoritation.DisplayLayout.Appearance = appearance8;
            ultraGridColumn17.Format = "dd/MM/yyyy hh:mm tt";
            ultraGridColumn17.Header.Caption = "Hora de Ingreso";
            ultraGridColumn17.Header.VisiblePosition = 0;
            ultraGridColumn17.Width = 115;
            ultraGridColumn18.Header.Caption = "Trabajador(a)";
            ultraGridColumn18.Header.VisiblePosition = 1;
            ultraGridColumn18.Width = 228;
            ultraGridColumn3.Header.Caption = "Nombre";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 172;
            ultraGridColumn6.Header.Caption = "Apellido Paterno";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn6.Width = 118;
            ultraGridColumn7.Header.Caption = "Apellido Materno";
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 123;
            ultraGridColumn2.Header.Caption = "Género";
            ultraGridColumn2.Header.VisiblePosition = 11;
            ultraGridColumn4.Header.Caption = "Tipo Documento";
            ultraGridColumn4.Header.VisiblePosition = 8;
            ultraGridColumn5.Header.Caption = "Número Documento";
            ultraGridColumn5.Header.VisiblePosition = 9;
            ultraGridColumn9.Header.Caption = "Fecha Nacimiento";
            ultraGridColumn9.Header.VisiblePosition = 12;
            ultraGridColumn1.Header.Caption = "ID Tipo Documento";
            ultraGridColumn1.Header.VisiblePosition = 7;
            ultraGridColumn8.Header.Caption = "ID Género";
            ultraGridColumn8.Header.VisiblePosition = 10;
            ultraGridColumn10.Header.Caption = "Puesto";
            ultraGridColumn10.Header.VisiblePosition = 6;
            ultraGridColumn10.Width = 119;
            ultraGridColumn11.Header.Caption = "Empresa";
            ultraGridColumn11.Header.VisiblePosition = 5;
            ultraGridColumn11.Width = 211;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn3,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn2,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn9,
            ultraGridColumn1,
            ultraGridColumn8,
            ultraGridColumn10,
            ultraGridColumn11});
            this.grdDataPeopleAuthoritation.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdDataPeopleAuthoritation.DisplayLayout.InterBandSpacing = 10;
            this.grdDataPeopleAuthoritation.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataPeopleAuthoritation.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataPeopleAuthoritation.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BackColor = System.Drawing.Color.White;
            appearance10.BackColor2 = System.Drawing.Color.White;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.CellAppearance = appearance10;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.Color.White;
            appearance11.BackColor2 = System.Drawing.Color.LightGray;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance11.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance12.AlphaLevel = ((short)(187));
            appearance12.BackColor = System.Drawing.Color.Gainsboro;
            appearance12.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance12.ForeColor = System.Drawing.Color.Black;
            appearance12.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.RowAlternateAppearance = appearance12;
            appearance13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.RowSelectorAppearance = appearance13;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance14.BackColor = System.Drawing.SystemColors.Highlight;
            appearance14.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance14.FontData.BoldAsString = "True";
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.SelectedRowAppearance = appearance14;
            this.grdDataPeopleAuthoritation.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataPeopleAuthoritation.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataPeopleAuthoritation.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataPeopleAuthoritation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataPeopleAuthoritation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataPeopleAuthoritation.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataPeopleAuthoritation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataPeopleAuthoritation.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataPeopleAuthoritation.Location = new System.Drawing.Point(17, 424);
            this.grdDataPeopleAuthoritation.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataPeopleAuthoritation.Name = "grdDataPeopleAuthoritation";
            this.grdDataPeopleAuthoritation.Size = new System.Drawing.Size(753, 224);
            this.grdDataPeopleAuthoritation.TabIndex = 62;
            this.grdDataPeopleAuthoritation.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.grdDataPeopleAuthoritation_InitializeRow);
            this.grdDataPeopleAuthoritation.DoubleClick += new System.EventHandler(this.grdDataPeopleAuthoritation_DoubleClick);
            this.grdDataPeopleAuthoritation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdDataPeopleAuthoritation_MouseDown);
            // 
            // btnCancelPersonAuthoritation
            // 
            this.btnCancelPersonAuthoritation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelPersonAuthoritation.Enabled = false;
            this.btnCancelPersonAuthoritation.ForeColor = System.Drawing.Color.Black;
            this.btnCancelPersonAuthoritation.Image = global::Sigesoft.Node.WinClient.UI.Resources.delete;
            this.btnCancelPersonAuthoritation.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCancelPersonAuthoritation.Location = new System.Drawing.Point(774, 424);
            this.btnCancelPersonAuthoritation.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelPersonAuthoritation.Name = "btnCancelPersonAuthoritation";
            this.btnCancelPersonAuthoritation.Size = new System.Drawing.Size(92, 43);
            this.btnCancelPersonAuthoritation.TabIndex = 63;
            this.btnCancelPersonAuthoritation.Text = "Cancelar Ingreso al CM";
            this.btnCancelPersonAuthoritation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelPersonAuthoritation.UseVisualStyleBackColor = true;
            this.btnCancelPersonAuthoritation.Click += new System.EventHandler(this.btnCancelPersonAuthoritation_Click);
            // 
            // frmEntryToMedical
            // 
            this.AcceptButton = this.btnFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(886, 655);
            this.Controls.Add(this.btnCancelPersonAuthoritation);
            this.Controls.Add(this.grdDataPeopleAuthoritation);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnUpdateandSelect);
            this.Controls.Add(this.grdData);
            this.Controls.Add(this.lblRecordCountPendiente);
            this.Controls.Add(this.lblRecordCountTotal);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEntryToMedical";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Ingreso Centro Médico";
            this.Load += new System.EventHandler(this.frmEntryToMedical_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uvCM)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataPeopleAuthoritation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvPersonAuthoritation)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDocNumber;
        private System.Windows.Forms.ComboBox ddlDocTypeId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnFilter;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRecordCountTotal;
        private Infragistics.Win.Misc.UltraValidator uvCM;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuGridCancelar;
        private System.Windows.Forms.Label lblRecordCountPendiente;
        private System.Windows.Forms.Button btnUpdateandSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSearchAuthoritation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameOrOrganization;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataPeopleAuthoritation;
        private Infragistics.Win.Misc.UltraValidator uvPersonAuthoritation;
        private System.Windows.Forms.Button btnCancelPersonAuthoritation;
    }
}