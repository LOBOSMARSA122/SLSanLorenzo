namespace Sigesoft.Node.WinClient.UI
{
    partial class frmschedulePeople
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_FirstLastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SecondLastName", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_SexTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocTypeName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_DocNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("d_Birthdate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_DocTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("i_SexTypeId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_CurrentOccupation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("v_ProtocoloId");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.grdDataPeople = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.btnschedule = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.lblRecordCountPacients = new System.Windows.Forms.Label();
            this.uvschedule = new Infragistics.Win.Misc.UltraValidator(this.components);
            this.ddlCalendarStatusId = new System.Windows.Forms.ComboBox();
            this.ddlNewContinuationId = new System.Windows.Forms.ComboBox();
            this.txtViewProtocol = new System.Windows.Forms.TextBox();
            this.ddlLineStatusId = new System.Windows.Forms.ComboBox();
            this.ddlServiceTypeId = new System.Windows.Forms.ComboBox();
            this.ddlVipId = new System.Windows.Forms.ComboBox();
            this.ddlMasterServiceId = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ddlOrganization = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpDateTimeCalendar = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtProtocolId = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.btnSearchProtocol = new System.Windows.Forms.Button();
            this.txtViewIntermediaryOrganization = new System.Windows.Forms.TextBox();
            this.txtIntermediaryOrganization = new System.Windows.Forms.TextBox();
            this.txtViewOccupation = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtViewGes = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtViewGroupOccupation = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtViewComponentType = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtViewOrganization = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtViewLocation = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataPeople)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvschedule)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDataPeople
            // 
            this.grdDataPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataPeople.CausesValidation = false;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.grdDataPeople.DisplayLayout.Appearance = appearance1;
            ultraGridColumn3.Header.Caption = "Nombre";
            ultraGridColumn3.Header.VisiblePosition = 1;
            ultraGridColumn3.Width = 172;
            ultraGridColumn6.Header.Caption = "Apellido Paterno";
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 165;
            ultraGridColumn7.Header.Caption = "Apellido Materno";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn7.Width = 177;
            ultraGridColumn2.Header.Caption = "Género";
            ultraGridColumn2.Header.VisiblePosition = 8;
            ultraGridColumn4.Header.Caption = "Tipo Documento";
            ultraGridColumn4.Header.VisiblePosition = 5;
            ultraGridColumn5.Header.Caption = "Número Documento";
            ultraGridColumn5.Header.VisiblePosition = 6;
            ultraGridColumn9.Header.Caption = "Fecha Nacimiento";
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn1.Header.Caption = "ID Tipo Documento";
            ultraGridColumn1.Header.VisiblePosition = 4;
            ultraGridColumn8.Header.Caption = "ID Género";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn10.Header.Caption = "Puesto de Trabajo";
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn11.Header.Caption = "Protocolo ID";
            ultraGridColumn11.Header.VisiblePosition = 0;
            ultraGridColumn11.Width = 101;
            ultraGridBand1.Columns.AddRange(new object[] {
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
            this.grdDataPeople.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDataPeople.DisplayLayout.InterBandSpacing = 10;
            this.grdDataPeople.DisplayLayout.MaxColScrollRegions = 1;
            this.grdDataPeople.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdDataPeople.DisplayLayout.NewColumnLoadStyle = Infragistics.Win.UltraWinGrid.NewColumnLoadStyle.Hide;
            this.grdDataPeople.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDataPeople.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataPeople.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDataPeople.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.False;
            this.grdDataPeople.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDataPeople.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grdDataPeople.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.White;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            this.grdDataPeople.DisplayLayout.Override.CellAppearance = appearance3;
            this.grdDataPeople.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance4.BackColor = System.Drawing.Color.White;
            appearance4.BackColor2 = System.Drawing.Color.LightGray;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.grdDataPeople.DisplayLayout.Override.HeaderAppearance = appearance4;
            this.grdDataPeople.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance5.AlphaLevel = ((short)(187));
            appearance5.BackColor = System.Drawing.Color.Gainsboro;
            appearance5.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance5.ForeColor = System.Drawing.Color.Black;
            appearance5.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            this.grdDataPeople.DisplayLayout.Override.RowAlternateAppearance = appearance5;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grdDataPeople.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.grdDataPeople.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance7.FontData.BoldAsString = "True";
            this.grdDataPeople.DisplayLayout.Override.SelectedRowAppearance = appearance7;
            this.grdDataPeople.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDataPeople.DisplayLayout.RowConnectorColor = System.Drawing.SystemColors.ControlDarkDark;
            this.grdDataPeople.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Dashed;
            this.grdDataPeople.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDataPeople.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDataPeople.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDataPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDataPeople.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.grdDataPeople.Location = new System.Drawing.Point(30, 123);
            this.grdDataPeople.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataPeople.Name = "grdDataPeople";
            this.grdDataPeople.Size = new System.Drawing.Size(952, 258);
            this.grdDataPeople.TabIndex = 59;
            this.grdDataPeople.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grdDataPeople_InitializeLayout);
            this.grdDataPeople.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdDataPeople_MouseDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_close;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(907, 385);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 24);
            this.button1.TabIndex = 61;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnschedule
            // 
            this.btnschedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnschedule.Image = global::Sigesoft.Node.WinClient.UI.Resources.book_edit;
            this.btnschedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnschedule.Location = new System.Drawing.Point(30, 385);
            this.btnschedule.Margin = new System.Windows.Forms.Padding(2);
            this.btnschedule.Name = "btnschedule";
            this.btnschedule.Size = new System.Drawing.Size(75, 24);
            this.btnschedule.TabIndex = 60;
            this.btnschedule.Text = "Agendar";
            this.btnschedule.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnschedule.UseVisualStyleBackColor = true;
            this.btnschedule.Click += new System.EventHandler(this.btnschedule_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportExcel.Image = global::Sigesoft.Node.WinClient.UI.Resources.page_excel;
            this.btnImportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportExcel.Location = new System.Drawing.Point(109, 385);
            this.btnImportExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(102, 24);
            this.btnImportExcel.TabIndex = 58;
            this.btnImportExcel.Text = "Importar Excel";
            this.btnImportExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // lblRecordCountPacients
            // 
            this.lblRecordCountPacients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCountPacients.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCountPacients.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblRecordCountPacients.Location = new System.Drawing.Point(751, 102);
            this.lblRecordCountPacients.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordCountPacients.Name = "lblRecordCountPacients";
            this.lblRecordCountPacients.Size = new System.Drawing.Size(231, 19);
            this.lblRecordCountPacients.TabIndex = 63;
            this.lblRecordCountPacients.Text = "No se ha realizado la búsqueda aún.";
            this.lblRecordCountPacients.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ddlCalendarStatusId
            // 
            this.ddlCalendarStatusId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCalendarStatusId.Enabled = false;
            this.ddlCalendarStatusId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCalendarStatusId.FormattingEnabled = true;
            this.ddlCalendarStatusId.Location = new System.Drawing.Point(99, 45);
            this.ddlCalendarStatusId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlCalendarStatusId.Name = "ddlCalendarStatusId";
            this.ddlCalendarStatusId.Size = new System.Drawing.Size(134, 21);
            this.ddlCalendarStatusId.TabIndex = 42;
            this.uvschedule.GetValidationSettings(this.ddlCalendarStatusId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.ddlCalendarStatusId).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.ddlCalendarStatusId).IsRequired = true;
            this.ddlCalendarStatusId.SelectedIndexChanged += new System.EventHandler(this.ddlCalendarStatusId_SelectedIndexChanged);
            // 
            // ddlNewContinuationId
            // 
            this.ddlNewContinuationId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlNewContinuationId.Enabled = false;
            this.ddlNewContinuationId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlNewContinuationId.FormattingEnabled = true;
            this.ddlNewContinuationId.Location = new System.Drawing.Point(99, 20);
            this.ddlNewContinuationId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlNewContinuationId.Name = "ddlNewContinuationId";
            this.ddlNewContinuationId.Size = new System.Drawing.Size(134, 21);
            this.ddlNewContinuationId.TabIndex = 41;
            this.uvschedule.GetValidationSettings(this.ddlNewContinuationId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.ddlNewContinuationId).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.ddlNewContinuationId).IsRequired = true;
            this.ddlNewContinuationId.SelectedIndexChanged += new System.EventHandler(this.ddlNewContinuationId_SelectedIndexChanged);
            // 
            // txtViewProtocol
            // 
            this.txtViewProtocol.Enabled = false;
            this.txtViewProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewProtocol.Location = new System.Drawing.Point(565, 41);
            this.txtViewProtocol.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewProtocol.Name = "txtViewProtocol";
            this.txtViewProtocol.ReadOnly = true;
            this.txtViewProtocol.Size = new System.Drawing.Size(332, 20);
            this.txtViewProtocol.TabIndex = 39;
            this.uvschedule.GetValidationSettings(this.txtViewProtocol).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.txtViewProtocol).EmptyValueCriteria = Infragistics.Win.Misc.EmptyValueCriteria.NullOrEmptyString;
            this.txtViewProtocol.TextChanged += new System.EventHandler(this.txtViewProtocol_TextChanged);
            // 
            // ddlLineStatusId
            // 
            this.ddlLineStatusId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLineStatusId.Enabled = false;
            this.ddlLineStatusId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlLineStatusId.FormattingEnabled = true;
            this.ddlLineStatusId.Location = new System.Drawing.Point(327, 46);
            this.ddlLineStatusId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlLineStatusId.Name = "ddlLineStatusId";
            this.ddlLineStatusId.Size = new System.Drawing.Size(132, 21);
            this.ddlLineStatusId.TabIndex = 21;
            this.uvschedule.GetValidationSettings(this.ddlLineStatusId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.ddlLineStatusId).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.ddlLineStatusId).IsRequired = true;
            this.ddlLineStatusId.SelectedIndexChanged += new System.EventHandler(this.ddlLineStatusId_SelectedIndexChanged);
            // 
            // ddlServiceTypeId
            // 
            this.ddlServiceTypeId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlServiceTypeId.Enabled = false;
            this.ddlServiceTypeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlServiceTypeId.FormattingEnabled = true;
            this.ddlServiceTypeId.Location = new System.Drawing.Point(565, 20);
            this.ddlServiceTypeId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlServiceTypeId.Name = "ddlServiceTypeId";
            this.ddlServiceTypeId.Size = new System.Drawing.Size(135, 21);
            this.ddlServiceTypeId.TabIndex = 9;
            this.uvschedule.GetValidationSettings(this.ddlServiceTypeId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.ddlServiceTypeId).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.ddlServiceTypeId).IsRequired = true;
            this.ddlServiceTypeId.SelectedIndexChanged += new System.EventHandler(this.ddlServiceTypeId_SelectedIndexChanged);
            this.ddlServiceTypeId.TextChanged += new System.EventHandler(this.ddlServiceTypeId_TextChanged);
            // 
            // ddlVipId
            // 
            this.ddlVipId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlVipId.Enabled = false;
            this.ddlVipId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlVipId.FormattingEnabled = true;
            this.ddlVipId.Location = new System.Drawing.Point(795, 45);
            this.ddlVipId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlVipId.Name = "ddlVipId";
            this.ddlVipId.Size = new System.Drawing.Size(131, 21);
            this.ddlVipId.TabIndex = 15;
            this.uvschedule.GetValidationSettings(this.ddlVipId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.ddlVipId).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.ddlVipId).IsRequired = true;
            this.ddlVipId.SelectedIndexChanged += new System.EventHandler(this.ddlVipId_SelectedIndexChanged);
            // 
            // ddlMasterServiceId
            // 
            this.ddlMasterServiceId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMasterServiceId.Enabled = false;
            this.ddlMasterServiceId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlMasterServiceId.FormattingEnabled = true;
            this.ddlMasterServiceId.Location = new System.Drawing.Point(565, 45);
            this.ddlMasterServiceId.Margin = new System.Windows.Forms.Padding(2);
            this.ddlMasterServiceId.Name = "ddlMasterServiceId";
            this.ddlMasterServiceId.Size = new System.Drawing.Size(135, 21);
            this.ddlMasterServiceId.TabIndex = 11;
            this.uvschedule.GetValidationSettings(this.ddlMasterServiceId).Condition = new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
            this.uvschedule.GetValidationSettings(this.ddlMasterServiceId).DataType = typeof(string);
            this.uvschedule.GetValidationSettings(this.ddlMasterServiceId).IsRequired = true;
            this.ddlMasterServiceId.SelectedIndexChanged += new System.EventHandler(this.ddlMasterServiceId_SelectedIndexChanged);
            this.ddlMasterServiceId.SelectedValueChanged += new System.EventHandler(this.ddlMasterServiceId_SelectedValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ddlOrganization);
            this.groupBox6.Controls.Add(this.label32);
            this.groupBox6.Controls.Add(this.ddlCalendarStatusId);
            this.groupBox6.Controls.Add(this.ddlNewContinuationId);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.ddlLineStatusId);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.ddlServiceTypeId);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.ddlVipId);
            this.groupBox6.Controls.Add(this.ddlMasterServiceId);
            this.groupBox6.Controls.Add(this.dtpDateTimeCalendar);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox6.Location = new System.Drawing.Point(32, 19);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(950, 81);
            this.groupBox6.TabIndex = 64;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Datos de Agenda";
            // 
            // ddlOrganization
            // 
            this.ddlOrganization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlOrganization.Enabled = false;
            this.ddlOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlOrganization.FormattingEnabled = true;
            this.ddlOrganization.Location = new System.Drawing.Point(795, 20);
            this.ddlOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.ddlOrganization.Name = "ddlOrganization";
            this.ddlOrganization.Size = new System.Drawing.Size(131, 21);
            this.ddlOrganization.TabIndex = 45;
            this.ddlOrganization.SelectedIndexChanged += new System.EventHandler(this.ddlOrganization_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(739, 23);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(51, 13);
            this.label32.TabIndex = 43;
            this.label32.Text = "Empresa ";
            this.label32.Click += new System.EventHandler(this.label32_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(26, 48);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(61, 13);
            this.label29.TabIndex = 40;
            this.label29.Text = "Estado Cita";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(26, 23);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(56, 13);
            this.label28.TabIndex = 23;
            this.label28.Text = "Modalidad";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(259, 23);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Fecha Hora";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(489, 23);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "Tipo Servicio";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(259, 50);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(64, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Estado Cola";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(489, 48);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 10;
            this.label16.Text = "Servicio";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // dtpDateTimeCalendar
            // 
            this.dtpDateTimeCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateTimeCalendar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTimeCalendar.Location = new System.Drawing.Point(326, 21);
            this.dtpDateTimeCalendar.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDateTimeCalendar.Name = "dtpDateTimeCalendar";
            this.dtpDateTimeCalendar.Size = new System.Drawing.Size(133, 20);
            this.dtpDateTimeCalendar.TabIndex = 4;
            this.dtpDateTimeCalendar.ValueChanged += new System.EventHandler(this.dtpDateTimeCalendar_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(739, 49);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "Vip";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.txtProtocolId);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.btnSearchProtocol);
            this.groupBox2.Controls.Add(this.txtViewIntermediaryOrganization);
            this.groupBox2.Controls.Add(this.txtIntermediaryOrganization);
            this.groupBox2.Controls.Add(this.txtViewProtocol);
            this.groupBox2.Controls.Add(this.txtViewOccupation);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.txtViewGes);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.txtViewGroupOccupation);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.txtViewComponentType);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.txtViewOrganization);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.txtViewLocation);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.MediumBlue;
            this.groupBox2.Location = new System.Drawing.Point(30, 459);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(943, 10);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Protocolo";
            this.groupBox2.Visible = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(489, 66);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(76, 15);
            this.label33.TabIndex = 45;
            this.label33.Text = "Emp. Trabajo";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(26, 65);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(69, 16);
            this.label30.TabIndex = 44;
            this.label30.Text = "Emp. Cliente";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label30.Click += new System.EventHandler(this.label30_Click);
            // 
            // txtProtocolId
            // 
            this.txtProtocolId.Enabled = false;
            this.txtProtocolId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProtocolId.Location = new System.Drawing.Point(99, 17);
            this.txtProtocolId.Margin = new System.Windows.Forms.Padding(2);
            this.txtProtocolId.Name = "txtProtocolId";
            this.txtProtocolId.ReadOnly = true;
            this.txtProtocolId.Size = new System.Drawing.Size(134, 20);
            this.txtProtocolId.TabIndex = 43;
            this.txtProtocolId.TextChanged += new System.EventHandler(this.txtProtocolId_TextChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(26, 20);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(21, 13);
            this.label31.TabIndex = 42;
            this.label31.Text = "ID ";
            // 
            // btnSearchProtocol
            // 
            this.btnSearchProtocol.Enabled = false;
            this.btnSearchProtocol.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchProtocol.Image = global::Sigesoft.Node.WinClient.UI.Resources.system_search;
            this.btnSearchProtocol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchProtocol.Location = new System.Drawing.Point(901, 38);
            this.btnSearchProtocol.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearchProtocol.Name = "btnSearchProtocol";
            this.btnSearchProtocol.Size = new System.Drawing.Size(26, 24);
            this.btnSearchProtocol.TabIndex = 36;
            this.btnSearchProtocol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearchProtocol.UseVisualStyleBackColor = true;
            this.btnSearchProtocol.Click += new System.EventHandler(this.btnSearchProtocol_Click);
            // 
            // txtViewIntermediaryOrganization
            // 
            this.txtViewIntermediaryOrganization.Enabled = false;
            this.txtViewIntermediaryOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewIntermediaryOrganization.Location = new System.Drawing.Point(565, 64);
            this.txtViewIntermediaryOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewIntermediaryOrganization.Name = "txtViewIntermediaryOrganization";
            this.txtViewIntermediaryOrganization.ReadOnly = true;
            this.txtViewIntermediaryOrganization.Size = new System.Drawing.Size(361, 20);
            this.txtViewIntermediaryOrganization.TabIndex = 38;
            // 
            // txtIntermediaryOrganization
            // 
            this.txtIntermediaryOrganization.Enabled = false;
            this.txtIntermediaryOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntermediaryOrganization.Location = new System.Drawing.Point(99, 64);
            this.txtIntermediaryOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.txtIntermediaryOrganization.Name = "txtIntermediaryOrganization";
            this.txtIntermediaryOrganization.ReadOnly = true;
            this.txtIntermediaryOrganization.Size = new System.Drawing.Size(360, 20);
            this.txtIntermediaryOrganization.TabIndex = 41;
            // 
            // txtViewOccupation
            // 
            this.txtViewOccupation.Enabled = false;
            this.txtViewOccupation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewOccupation.Location = new System.Drawing.Point(99, 40);
            this.txtViewOccupation.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewOccupation.Name = "txtViewOccupation";
            this.txtViewOccupation.ReadOnly = true;
            this.txtViewOccupation.Size = new System.Drawing.Size(134, 20);
            this.txtViewOccupation.TabIndex = 36;
            this.txtViewOccupation.TextChanged += new System.EventHandler(this.txtViewOccupation_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(26, 43);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(40, 13);
            this.label27.TabIndex = 35;
            this.label27.Text = "Puesto";
            // 
            // txtViewGes
            // 
            this.txtViewGes.Enabled = false;
            this.txtViewGes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewGes.Location = new System.Drawing.Point(795, 17);
            this.txtViewGes.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewGes.Name = "txtViewGes";
            this.txtViewGes.ReadOnly = true;
            this.txtViewGes.Size = new System.Drawing.Size(131, 20);
            this.txtViewGes.TabIndex = 34;
            this.txtViewGes.TextChanged += new System.EventHandler(this.txtViewGes_TextChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(739, 20);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 13);
            this.label25.TabIndex = 33;
            this.label25.Text = "GES";
            this.label25.Click += new System.EventHandler(this.label25_Click);
            // 
            // txtViewGroupOccupation
            // 
            this.txtViewGroupOccupation.Enabled = false;
            this.txtViewGroupOccupation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewGroupOccupation.Location = new System.Drawing.Point(326, 41);
            this.txtViewGroupOccupation.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewGroupOccupation.Name = "txtViewGroupOccupation";
            this.txtViewGroupOccupation.ReadOnly = true;
            this.txtViewGroupOccupation.Size = new System.Drawing.Size(133, 20);
            this.txtViewGroupOccupation.TabIndex = 32;
            this.txtViewGroupOccupation.TextChanged += new System.EventHandler(this.txtViewGroupOccupation_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(259, 44);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 13);
            this.label23.TabIndex = 31;
            this.label23.Text = "GESO";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // txtViewComponentType
            // 
            this.txtViewComponentType.Enabled = false;
            this.txtViewComponentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewComponentType.Location = new System.Drawing.Point(565, 17);
            this.txtViewComponentType.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewComponentType.Name = "txtViewComponentType";
            this.txtViewComponentType.ReadOnly = true;
            this.txtViewComponentType.Size = new System.Drawing.Size(135, 20);
            this.txtViewComponentType.TabIndex = 30;
            this.txtViewComponentType.TextChanged += new System.EventHandler(this.txtViewComponentType_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(489, 20);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 13);
            this.label24.TabIndex = 29;
            this.label24.Text = "Tipo ESO";
            this.label24.Click += new System.EventHandler(this.label24_Click);
            // 
            // txtViewOrganization
            // 
            this.txtViewOrganization.Enabled = false;
            this.txtViewOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewOrganization.Location = new System.Drawing.Point(116, 88);
            this.txtViewOrganization.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewOrganization.Name = "txtViewOrganization";
            this.txtViewOrganization.ReadOnly = true;
            this.txtViewOrganization.Size = new System.Drawing.Size(811, 20);
            this.txtViewOrganization.TabIndex = 26;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(26, 91);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 17);
            this.label21.TabIndex = 25;
            this.label21.Text = "Emp. Empleadora (Contratista)";
            // 
            // txtViewLocation
            // 
            this.txtViewLocation.Enabled = false;
            this.txtViewLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViewLocation.Location = new System.Drawing.Point(326, 17);
            this.txtViewLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtViewLocation.Name = "txtViewLocation";
            this.txtViewLocation.ReadOnly = true;
            this.txtViewLocation.Size = new System.Drawing.Size(133, 20);
            this.txtViewLocation.TabIndex = 28;
            this.txtViewLocation.TextChanged += new System.EventHandler(this.txtViewLocation_TextChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(259, 20);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(32, 13);
            this.label22.TabIndex = 27;
            this.label22.Text = "Sede";
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(489, 44);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 13);
            this.label20.TabIndex = 23;
            this.label20.Text = "Protocolo";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(30, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Lista de resultados";
            // 
            // frmschedulePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(993, 418);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.lblRecordCountPacients);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnschedule);
            this.Controls.Add(this.grdDataPeople);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnImportExcel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmschedulePeople";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agendar Cita Masiva";
            this.Load += new System.EventHandler(this.frmschedulePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataPeople)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvschedule)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportExcel;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDataPeople;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnschedule;
        private System.Windows.Forms.Label lblRecordCountPacients;
        private Infragistics.Win.Misc.UltraValidator uvschedule;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox ddlOrganization;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox ddlCalendarStatusId;
        private System.Windows.Forms.ComboBox ddlNewContinuationId;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtProtocolId;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtIntermediaryOrganization;
        private System.Windows.Forms.Button btnSearchProtocol;
        private System.Windows.Forms.TextBox txtViewIntermediaryOrganization;
        private System.Windows.Forms.TextBox txtViewProtocol;
        private System.Windows.Forms.TextBox txtViewOccupation;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtViewGes;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtViewGroupOccupation;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtViewComponentType;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtViewOrganization;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtViewLocation;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox ddlLineStatusId;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox ddlServiceTypeId;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox ddlVipId;
        private System.Windows.Forms.ComboBox ddlMasterServiceId;
        private System.Windows.Forms.DateTimePicker dtpDateTimeCalendar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}